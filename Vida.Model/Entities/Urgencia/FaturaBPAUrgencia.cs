using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class FaturaBPAUrgencia
    {
        public static char FATURA = 'F';
        public static char PREVIA = 'P';

        public FaturaBPAUrgencia()
        {
        }

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private EstabelecimentoSaude unidade;
        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }

        private int competencia;
        public virtual int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        private char tipo;

        /// <summary>
        /// Tipo do BPA Solicitado: BPA-I, BPA-C ou APAC
        /// </summary>
        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private Usuario usuario;
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual int CompetenciaAnterior
        {
            get
            {
                int lenghtCompetenciaAtual = competencia.ToString().Length;
                int mes = int.Parse(competencia.ToString()[(lenghtCompetenciaAtual - 2)].ToString() + competencia.ToString()[(lenghtCompetenciaAtual - 1)].ToString());
                int ano = int.Parse(competencia.ToString().Substring(0, lenghtCompetenciaAtual - 2));

                mes -= 1;

                if (mes == 0)
                {
                    mes = 12;
                    ano -= 1;
                }

                return int.Parse((ano.ToString() + (mes < 10 ? ("0" + mes.ToString()) : mes.ToString()).ToString()));
            }
        }
    }
}
