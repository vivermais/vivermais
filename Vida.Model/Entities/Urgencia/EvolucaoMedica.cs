using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EvolucaoMedica
    {
        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Prontuario prontuario;
        public virtual Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        private string codigoprofissional;

        public virtual string CodigoProfissional 
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }

        Profissional profissional;
        public virtual Profissional Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private ClassificacaoRisco classificacaorisco;
        public virtual ClassificacaoRisco ClassificacaoRisco
        {
            get { return classificacaorisco; }
            set { classificacaorisco = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        IList<string> codigoscids;
        public virtual IList<string> CodigosCids
        {
            get { return codigoscids; }
            set { codigoscids = value; }
        }

        public virtual string NomeProfissionalToString 
        {
            get 
            {
                if (Profissional != null)
                    return Profissional.Nome;
                return "";
            }
        }

        string observacao;
        public virtual string Observacao
        {
            get { return observacao; }
            set { observacao = value; }
        }

        //IList<string> codigosprocedimentos;
        //public virtual IList<string> CodigosProcedimentos
        //{
        //    get { return codigosprocedimentos; }
        //    set { codigosprocedimentos = value; }
        //}

        private bool primeiraconsulta;
        public virtual bool PrimeiraConsulta
        {
            get { return primeiraconsulta; }
            set { primeiraconsulta = value; }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public EvolucaoMedica()
        {
            this.data = new DateTime();
        }
    }
}
