﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class PrescricaoMedicamento
    {
        public enum UNIDADETEMPO { HORAS = 1, MINUTOS = 2, UNICA = 3 };

        private int medicamento;

        public virtual int Medicamento
        {
            get { return medicamento; }
            set { medicamento = value; }
        }

        private int intervalo;
        /// <summary>
        /// Intervalo em minutos para aplicar o medicamento prescrito
        /// </summary>
        public virtual int Intervalo
        {
            set { intervalo = value; }
            get { return intervalo; }
        }

        /// <summary>
        /// Seta o intervalo e unidade de tempo escolhida
        /// </summary>
        /// <param name="intervalo">intervalo inicial</param>
        /// <param name="unidademedida">unidade de tempo</param>
        /// <returns></returns>
        public virtual void SetIntervalo(string intervalo, int unidadetempo)
        {
            if (unidadetempo != (int)PrescricaoMedicamento.UNIDADETEMPO.UNICA)
            {
                this.intervalo = int.Parse(intervalo);
                this.aplicacaounica = false;

                if (unidadetempo == (int)PrescricaoMedicamento.UNIDADETEMPO.HORAS)
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
        /// Descrição em texto do intervalo para aplicação do medicamento
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

        ViverMais.Model.Prescricao prescricao;

        public virtual ViverMais.Model.Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
        }

        private ViverMais.Model.Medicamento objetomedicamento;

        public virtual ViverMais.Model.Medicamento ObjetoMedicamento
        {
            get { return objetomedicamento; }
            set { objetomedicamento = value; }
        }

        public virtual string NomeMedicamento
        {
            get { return this.ObjetoMedicamento.Nome; }
        }

        private string observacao;

        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        string codigoprofissional;

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

        bool suspenso;

        public virtual bool Suspenso
        {
            get { return suspenso; }
            set { suspenso = value; }
        }

        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual string SuspensoToString
        {
            get
            {
                if (suspenso)
                    return "Suspenso";
                else
                    return "Ativo";
            }
        }

        public virtual string DescricaoObservacao
        {
            get { return string.IsNullOrEmpty(Observacao) ? " - " : Observacao; }
        }

        public PrescricaoMedicamento()
        {
        }

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private bool executarprimeiromomento;
        /// <summary>
        /// Indica se o medicamento será logo executado mediante a prescrição
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
