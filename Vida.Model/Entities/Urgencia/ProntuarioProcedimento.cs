using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    public class ProntuarioProcedimento
    {
        ProntuarioMedico prontuariomedico;

        public virtual ProntuarioMedico ProntuarioMedico
        {
            get { return prontuariomedico; }
            set { prontuariomedico = value; }
        }

        private string codigoprocedimento;

        public virtual string CodigoProcedimento
        {
            get { return codigoprocedimento; }
            set { codigoprocedimento = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento 
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        public virtual string ProcedimentoToString 
        {
            get 
            {
                if ( Procedimento != null )
                    return Procedimento.Nome;
                return "";
            }
        }

        private string codigoprofissional;

        public virtual string CodigoProfissional
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }
        
        double quantidade;

        public virtual double Quantidade
        {
            get { return quantidade;}
            set { quantidade = value;}
        }

        string observacao;

        public virtual string Observacao
        {
            get 
            {
                if (string.IsNullOrEmpty(observacao))
                    return " - ";
                return observacao;
            }
            set { observacao = value;}
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return ProntuarioMedico.Data; }
        }

        private bool excluir;

        public virtual bool Excluir 
        {
            get { return excluir; }
            set { excluir = value; }
        }

        private bool atualizar;

        public virtual bool Atualizar 
        {
            get { return atualizar; }
            set { atualizar = value; }
        }

        public ProntuarioProcedimento()
        {
            
        }
        
        public override bool Equals(object obj)
        {
            return this.ProntuarioMedico.Equals(obj) &&
                   this.CodigoProcedimento.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }
    }
}
