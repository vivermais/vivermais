﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ReceitaDispensacao
    {
        //Quantidade usual de dias da dispensação
        private int tempoUsualDispensacao = 30;
        //Quandidade de dias usual para dispensação restrita/autorizada
        private int tempoEsperaDispensacaoAutorizada = 15;
        //Quantidade de dias usual para uma nova dispensação
        private int tempoEsperaDispensacao = 20;

        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string codigoPaciente;
        public virtual string CodigoPaciente
        {
            get { return codigoPaciente; }
            set { codigoPaciente = value; }
        }

        string codigoProfissional;
        public virtual string CodigoProfissional
        {
            get { return codigoProfissional; }
            set { codigoProfissional = value; }
        }

        DateTime datareceita;
        public virtual DateTime DataReceita
        {
            get { return datareceita; }
            set { datareceita = value; }
        }

        DateTime datacadastro;
        public virtual DateTime DataCadastro
        {
            get { return datacadastro; }
            set { datacadastro = value; }
        }

        string codigoMunicipio;
        public virtual string CodigoMunicipio
        {
            get { return codigoMunicipio; }
            set { codigoMunicipio = value; }
        }

        private int codigodistrito;
        public virtual int CodigoDistrito
        {
            get { return codigodistrito; }
            set { codigodistrito = value; }
        }

        string codigoUnidade;
        public virtual string CodigoUnidade
        {
            get { return codigoUnidade; }
            set { codigoUnidade = value; }
        }

        private int medicamentosForaRede;
        public virtual int MedicamentosForaRede
        {
            get { return medicamentosForaRede; }
            set { medicamentosForaRede = value; }
        }

        private List<ItemReceitaDispensacao> itensPrescritos;
        public virtual List<ItemReceitaDispensacao> ItensPrescritos
        {
            get { return itensPrescritos; }
            set { itensPrescritos = value; }
        }

        private List<Dispensacao> dispensacoes;

        public virtual List<Dispensacao> Dispensacoes
        {
            get { return dispensacoes; }
            set { dispensacoes = value; }
        }

       public ReceitaDispensacao()
        {

        }

        /// <summary>
       /// Caso a data da receita somada com 6 meses seja menor que a data atual, esta está vencida.
        /// </summary>
        public virtual bool ReceitaVencida
        {
            get
            {   return DataReceita.AddMonths(6).CompareTo(DateTime.Today) < 0;
            }
        }

        /// <summary>
        /// Caso a data da receita somada com 4 meses seja menor que a data atual, esta está prestes a vencer.
        /// </summary>
        public virtual bool ReceitaPrestesAVencer
        {
            get
            {
                return DataReceita.AddMonths(4).CompareTo(DateTime.Today) < 0;
            }
        }

        /// <summary>
        /// Tempo restante de validade da receita
        /// </summary>
        public virtual int TempoRestanteValidade
        {
            get
            {
                TimeSpan diferencaDatas = this.DataReceita.AddMonths(6).Subtract(DateTime.Now);
                return diferencaDatas.Days;
            }
        }

        public virtual ItemReceitaDispensacao LocalizarPrescricao(ItemDispensacao itemDispensacao)
        {
            return ItensPrescritos.Find(x => x.Medicamento.Codigo == itemDispensacao.LoteMedicamento.Medicamento.Codigo);
        }

        public virtual DateTime DataUltimaDispensacao(int codigoMedicamento)
        {
            List<Dispensacao> auxDispensacoesOrdenadas = Dispensacoes.OrderBy(x => x.DataAtendimento).ToList();

            for (int cont = auxDispensacoesOrdenadas.Count - 1; cont >= 0; cont--)
            {
                foreach (ItemDispensacao item in auxDispensacoesOrdenadas[cont].ItensDispensados)
                {
                    if (item.LoteMedicamento.Medicamento.Codigo == codigoMedicamento)
                        return auxDispensacoesOrdenadas[cont].DataAtendimento;
                }
            }
            return DateTime.Today;            
        }

        public virtual Dispensacao UltimaDispensacaoMedicamento(int codigoMedicamento)
        {
            List<Dispensacao> auxDispensacoesOrdenadas = Dispensacoes.OrderBy(x => x.DataAtendimento).ToList();

            for (int cont = auxDispensacoesOrdenadas.Count - 1; cont >= 0; cont--)
            {
                foreach (ItemDispensacao item in auxDispensacoesOrdenadas[cont].ItensDispensados)
                {
                    if (item.LoteMedicamento.Medicamento.Codigo == codigoMedicamento)
                        return auxDispensacoesOrdenadas[cont];
                }
            }
            return null;
        }

        public virtual int SaldoRestanteMedicamento(int codigoMedicamento)
        {
            int retorno = ItensPrescritos.Find(x=>x.Medicamento.Codigo == codigoMedicamento).QtdPrescrita;
            foreach(Dispensacao dispensacao in Dispensacoes)
            {
                foreach (ItemDispensacao item in dispensacao.ItensDispensados)
                {
                    if (item.LoteMedicamento.Medicamento.Codigo == codigoMedicamento)
                        retorno -= item.QtdDispensada;                        
                }
            }
            return retorno;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoMedicamento"></param>
        /// <returns></returns>
        public virtual int TempoEsperaDispensacaoAutorizada(int codigoMedicamento)
        {
            return this.tempoEsperaDispensacaoAutorizada * SomaDiasUltimaDispensacao(codigoMedicamento) / this.tempoUsualDispensacao;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigoMedicamento"></param>
        /// <returns></returns>
        public virtual int TempoEsperaNovaDispensacao(int codigoMedicamento)
        {            
            return this.tempoEsperaDispensacao * SomaDiasUltimaDispensacao(codigoMedicamento) / this.tempoUsualDispensacao;
        }

        public virtual int SomaDiasUltimaDispensacao(int codigoMedicamento)
        {

            int dias = 0;
            List<Dispensacao> dispensacoes = Dispensacoes.FindAll(
                delegate(Dispensacao dispensacao)
                {
                    return dispensacao.DataAtendimento.Date == DataUltimaDispensacao(codigoMedicamento).Date;
                }
            );            
            
            foreach (Dispensacao dispensacao in dispensacoes)
            {                
                dias += dispensacao.SomaDiasDispensacoes(codigoMedicamento);
            }
            return dias;
        }

        public virtual int SomaQuantidadesUltimaDispensacao(int codigoMedicamento)
        {

            int qt = 0;
            List<Dispensacao> dispensacoes = Dispensacoes.FindAll(
                delegate(Dispensacao dispensacao)
                {
                    return dispensacao.DataAtendimento.Date == DataUltimaDispensacao(codigoMedicamento).Date;
                }
            );            

            foreach (Dispensacao dispensacao in dispensacoes)
            {
                qt += dispensacao.SomaQuantidadesDispensadas(codigoMedicamento);
            }
            return qt;
        }

        public virtual bool PodeDispensarComAutorizacao(ItemDispensacao itemDispensacao)
        {
            Dispensacao ultima = UltimaDispensacaoMedicamento(itemDispensacao.LoteMedicamento.Medicamento.Codigo);
            return ultima.DiasDecorridos() > TempoEsperaDispensacaoAutorizada(itemDispensacao.LoteMedicamento.Medicamento.Codigo)
                && ultima.DiasDecorridos() <= TempoEsperaNovaDispensacao(itemDispensacao.LoteMedicamento.Medicamento.Codigo);
        }

        public virtual bool PodeDispensarSemAutorizacao(ItemDispensacao itemDispensacao)
        {
            Dispensacao ultima = UltimaDispensacaoMedicamento(itemDispensacao.LoteMedicamento.Medicamento.Codigo);
            return ultima.DiasDecorridos() > TempoEsperaNovaDispensacao(itemDispensacao.LoteMedicamento.Medicamento.Codigo);
        }

        public virtual bool Passou24HorasUltimaDispensacao(int codigoMedicamento)
        {
            return UltimaDispensacaoMedicamento(codigoMedicamento).DiasDecorridos() > 0;
        }

        

        /// <summary>
        /// Verifica se pode dispensar novamente o medicamento de acordo com a ultima data
        /// e a regra de três sobre a quantidade de dias.
        /// </summary>
        /// <param name="itemDispensacao"></param>
        /// <returns></returns>
        public virtual bool PodeDispensarNovamente(ItemDispensacao itemDispensacao)
        {
            Dispensacao ultima = UltimaDispensacaoMedicamento(itemDispensacao.LoteMedicamento.Medicamento.Codigo);

            return ultima.DiasDecorridos() > TempoEsperaDispensacaoAutorizada(itemDispensacao.LoteMedicamento.Medicamento.Codigo);
        }
    }
}
