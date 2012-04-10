using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Dispensacao
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ReceitaDispensacao receita;
        public virtual ReceitaDispensacao Receita
        {
            get { return receita; }
            set { receita = value; }
        }

        private Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }

        private DateTime dataAtendimento;        
        public virtual DateTime DataAtendimento
        {
            get { return dataAtendimento; }
            set { dataAtendimento = value; }
        }       

        public Dispensacao()
        {
            itensDispensados = new List<ItemDispensacao>();
        }

        private List<ItemDispensacao> itensDispensados;
        public virtual List<ItemDispensacao> ItensDispensados
        {
            get { return itensDispensados; }
            set { itensDispensados = value; }
        }

        public virtual long CodigoReceita
        {
            get { return Receita.Codigo; }
        }

        public virtual int CodigoFarmacia
        {
            get { return Farmacia.Codigo; }
        }

        public virtual string NomeFarmacia
        {
            get { return Farmacia.Nome; }
        }


        

        /// <summary>
        /// Soma a quantidade de dias das dispensações do medicamento
        /// </summary>
        /// <param name="codigoMedicamento"></param>
        /// <returns></returns>
        public virtual int SomaDiasDispensacoes(int codigoMedicamento)
        {
            List<ItemDispensacao> itens = ItensDispensados.FindAll(
                delegate(ItemDispensacao i)
                {
                    return i.LoteMedicamento.Medicamento.Codigo == codigoMedicamento;
                }
            );

            int totalDias = 0;

            foreach (ItemDispensacao item in itens)
            {
                totalDias += item.QtdDias;
            }
            return totalDias;
        }

        /// <summary>
        /// Soma a quantidade de itens dispensados por do medicamento
        /// </summary>
        /// <param name="codigoMedicamento"></param>
        /// <returns></returns>
        public virtual int SomaQuantidadesDispensadas(int codigoMedicamento)
        {
            List<ItemDispensacao> itens = ItensDispensados.FindAll(
                delegate(ItemDispensacao i)
                {
                    return i.LoteMedicamento.Medicamento.Codigo == codigoMedicamento;
                }
            );

            int quantidade = 0;

            foreach (ItemDispensacao item in itens)
            {
                quantidade += item.QtdDispensada;
            }
            return quantidade;
        }


        public virtual int DiasDecorridos()
        {
            TimeSpan span = DateTime.Today.Subtract(DataAtendimento.Date);
            return span.Days;
        }
    }
}
