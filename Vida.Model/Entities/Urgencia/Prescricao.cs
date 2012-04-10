using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Prescricao
    {
        public enum StatusPrescricao { Valida = 'V', Invalida = 'I', Suspensa = 'S', Agendada = 'A' };

        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        ViverMais.Model.Prontuario prontuario;
        public virtual ViverMais.Model.Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        DateTime datavigencia;
        public virtual DateTime DataVigencia
        {
            get { return datavigencia; }
            set { datavigencia = value; }
        }

        public virtual string DataCompleta
        {
            get { return GenericsFunctions.DiaDaSemana(data.DayOfWeek) + " " + data.ToString("dd/MM/yyyy HH:mm:ss"); }
        }

        private DateTime ultimadatavalida;
        public virtual DateTime UltimaDataValida
        {
            get { return ultimadatavalida; }
            set { ultimadatavalida = value; }
        }

        private char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string DescricaoStatus
        {
            get
            {
                switch (this.status) 
                {
                    case (char)Prescricao.StatusPrescricao.Invalida: { return "Inválida"; }
                    case (char)Prescricao.StatusPrescricao.Suspensa: { return "Suspenso"; }
                    case (char)Prescricao.StatusPrescricao.Agendada: { return "Agendada"; }
                    default : { return "Válida"; }
                }
            }
        }

        string profissional;

        public virtual string Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        //ViverMais.Model.EvolucaoMedica evolucaomedica;

        //public virtual ViverMais.Model.EvolucaoMedica EvolucaoMedica
        //{
        //    get { return evolucaomedica; }
        //    set { evolucaomedica = value; }
        //}

        //private EvolucaoEnfermagem evolucaoenfermagem;

        //public virtual EvolucaoEnfermagem EvolucaoEnfermagem
        //{
        //    get { return evolucaoenfermagem; }
        //    set { evolucaoenfermagem = value; }
        //}

        public Prescricao()
        {
        }
    }
}
