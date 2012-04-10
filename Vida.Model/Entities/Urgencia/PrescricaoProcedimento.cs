using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PrescricaoProcedimento
    {
        public enum UNIDADETEMPO { HORAS = 1, MINUTOS = 2, UNICA = 3 };
        public PrescricaoProcedimento()
        {
        }

        private int intervalo;
        /// <summary>
        /// Intervalo em minutos para aplicar o procedimento prescrito
        /// </summary>
        public virtual int Intervalo
        {
            get { return intervalo; }
            set { intervalo = value; }
        }

        /// <summary>
        /// Seta o intervalo e unidade de tempo escolhida
        /// </summary>
        /// <param name="intervalo">intervalo inicial</param>
        /// <param name="unidademedida">unidade de tempo</param>
        /// <returns></returns>
        public virtual void SetIntervalo(string intervalo, int unidadetempo)
        {
            if (unidadetempo != (int)PrescricaoProcedimento.UNIDADETEMPO.UNICA)
            {
                this.intervalo = int.Parse(intervalo);
                this.aplicacaounica = false;

                if (unidadetempo == (int)PrescricaoProcedimento.UNIDADETEMPO.HORAS)
                    this.intervalo *= 60;
            }
            else
            {
                this.intervalo = 0;
                this.aplicacaounica = true;
            }
        }

        private bool aplicacaounica;
        public virtual bool AplicacaoUnica
        {
            get { return aplicacaounica; }
            set { aplicacaounica = value; }
        }

        /// <summary>
        /// Descrição em texto do intervalo para aplicação do procedimento
        /// </summary>
        public virtual string DescricaoIntervalo
        {
            get
            {
                if (!this.aplicacaounica)
                {
                    if (intervalo >= 60)
                    {
                        int horas = (intervalo / 60);
                        int minutos = (intervalo % 60);

                        string tempo = horas.ToString("00") + " hora(s)";

                        if (minutos > 0)
                            tempo += " e " + minutos.ToString("00") + " minuto(s)";

                        tempo += " em " + tempo;

                        return tempo;
                    }

                    return intervalo.ToString("00") + " em " + intervalo.ToString("00") + " minuto(s)";
                }

                return "Aplicação Única";
            }
        }

        /// <summary>
        /// Valida o intervalo para o período de 24 horas
        /// </summary>
        /// <returns>verdadeiro ou falso</returns>
        public virtual bool IntervaloValido()
        {
            if (!this.aplicacaounica)
            {
                if (this.intervalo > (24 * 60))
                    return false;
            }

            return true;
        }

        private ViaAdministracao viaadministracao;

        public virtual ViaAdministracao ViaAdministracao
        {
            get { return viaadministracao; }
            set { viaadministracao = value; }
        }

        public virtual string NomeViaAdministracao
        {
            get { return ViaAdministracao == null ? " - " : ViaAdministracao.Nome; }
        }

        //private FormaAdministracao formaadministracao;

        //public virtual FormaAdministracao FormaAdministracao
        //{
        //    get { return formaadministracao; }
        //    set { formaadministracao = value; }
        //}

        //public virtual string NomeFormaAdministracao
        //{
        //    get { return FormaAdministracao == null ? " - " : FormaAdministracao.Nome; }
        //}

        ViverMais.Model.Prescricao prescricao;

        public virtual ViverMais.Model.Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
        }

        private string codigoprocedimento;

        public virtual string CodigoProcedimento
        {
            get { return codigoprocedimento; }
            set { codigoprocedimento = value; }
        }

        private bool suspenso;

        public virtual bool Suspenso
        {
            get { return suspenso; }
            set { suspenso = value; }
        }

        private string codigoprofissional;

        public virtual string CodigoProfissional 
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual string NomeProcedimento 
        {
            get { return Procedimento.Nome; }
        }

        public virtual string SuspensoToString
        {
            get { return suspenso ? "Suspenso" : "Ativo"; }
        }

        private string codigocid;
        public virtual string CodigoCid
        {
            get { return this.codigocid; }
            set { this.codigocid = value; }
        }

        private Cid cid;
        public virtual Cid Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public virtual string DescricaoCIDVinculado
        {
            get
            {
                if (!string.IsNullOrEmpty(this.CodigoCid) && cid != null)
                    return this.Cid.DescricaoCodigoNome;

                return " - ";
            }
        }

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private bool executarprimeiromomento;
        /// <summary>
        /// Indica se o procedimento será logo executado mediante a prescrição
        /// </summary>
        public virtual bool ExecutarPrimeiroMomento
        {
            get { return executarprimeiromomento; }
            set { executarprimeiromomento = value; }
        }

        public virtual string DescricaoExecutarPrimeiroMomento
        {
            get
            {
                if (executarprimeiromomento)
                    return "Sim";

                return "Não";
            }
        }
    }
}
