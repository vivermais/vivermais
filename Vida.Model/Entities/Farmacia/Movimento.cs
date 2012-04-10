using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Movimento
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }
        
        TipoMovimento tipomovimento;
        public virtual TipoMovimento TipoMovimento
        {
            get { return tipomovimento; }
            set { tipomovimento = value; }
        }

        MotivoMovimento motivo;
        public virtual MotivoMovimento Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        Setor setordestino;
        public virtual Setor Setor_Destino
        {
            get { return setordestino; }
            set { setordestino = value; }
        }

        Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }

        Farmacia farmacia_destino;
        public virtual Farmacia Farmacia_Destino
        {
            get { return farmacia_destino; }
            set { farmacia_destino = value; }
        }

        string codigoUnidade;

        public virtual string CodigoUnidade
        {
            get { return codigoUnidade; }
            set { codigoUnidade = value; }
        }

        //Unidade unidade;
        //public virtual Unidade Unidade
        //{
        //    get { return unidade; }
        //    set { unidade = value; }
        //}

        string resp_envio; 
        public virtual string Responsavel_Envio
        {
            get { return resp_envio; }
            set { resp_envio = value; }
        }

        string resp_receb;
        public virtual string Responsavel_Recebimento
        {
            get { return resp_receb; }
            set { resp_receb = value; }
        }

        DateTime ? data_envio;
        public virtual DateTime ? Data_Envio
        {
            get { return data_envio; }
            set { data_envio = value; }
        }

        DateTime ? data_receb;
        public virtual DateTime ?  Data_Recebimento
        {
            get { return data_receb; }
            set { data_receb = value; }
        }

        string observacao;
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        string status_movimento;
        public virtual string Status_Movimento
        {
            get { return status_movimento; }
            set { status_movimento = value; }
        }

        TipoOperacaoMovimento tipooperacaomovimento;
        public virtual TipoOperacaoMovimento TipoOperacaoMovimento
        {
            get { return tipooperacaomovimento; }
            set { tipooperacaomovimento = value; }
        }

        string responsavelmovimento;
        public virtual string ResponsavelMovimento
        {
            get { return responsavelmovimento; }
            set { responsavelmovimento = value; }
        }

        public virtual string NomeStatusMovimento
        {
            get
            {
                if (this.Status_Movimento == "A")
                    return "ABERTO";

                if (this.Status_Movimento == "E")
                    return "ENCERRADO";

                return "";
            }
        }

        public virtual string NomeTipoMovimento
        {
            get 
            {
                return TipoMovimento.Nome;
            }
        }

        public virtual string NomeFarmacia
        {
            get
            {
                return this.Farmacia.Nome;
            }
        }

        public virtual string SituacaoMovimento
        {
            get
            {
                if (this.TipoOperacaoMovimento.Codigo == TipoOperacaoMovimento.SAIDA)
                    return "Saída";
                //if (this.TipoMovimento.Codigo == TipoMovimento.DEVOLUCAO_PACIENTE)
                //    return "Entrada";
                //if (this.TipoMovimento.Codigo == TipoMovimento.PERDA || this.TipoMovimento.Codigo == TipoMovimento.REMANEJAMENTO || this.TipoMovimento.Codigo == TipoMovimento.TRANSFERENCIA_INTERNA)
                //    return "Saída";
                //if (this.TipoMovimento.Codigo == TipoMovimento.DOACAO)
                //{
                //    if (this.Farmacia.Codigo == (int)ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado)
                //        return "Saída";
                //    else
                //    {
                //        if (this.TipoOperacaoMovimento.Codigo == ViverMais.Model.TipoOperacaoMovimento.ENTRADA)
                //            return "Entrada";
                //        else
                //            return "Saída";
                //    }
                //}

                return "Entrada";
            }
        }

        public Movimento()
        {

        }
    }
}
