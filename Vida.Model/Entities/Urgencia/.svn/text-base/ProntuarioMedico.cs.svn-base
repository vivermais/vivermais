using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    //*-----------------Excluir esta classe
    
    public class ProntuarioMedico
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

        Prontuario prontuario;

        public virtual Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        IList<ProntuarioProcedimento> prontuarioprocedimento;
        public virtual IList<ProntuarioProcedimento> ProntuarioProcedimento
        {
            get { return prontuarioprocedimento; }
            set { prontuarioprocedimento = value; }
        }

        IList<Cid> cids;
        public virtual IList<Cid> Cids
        {
            get { return cids; }
            set { cids = value; }
        }

        string aprazamento;

        public virtual string Aprazamento
        {
            get { return aprazamento; }
            set { aprazamento = value; }
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
            set { observacao = value; }
        }

        string profissional;

        public virtual string Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }
       
        public ProntuarioMedico()
        {
            cids = new List<Cid>();
            prontuarioprocedimento = new List<ProntuarioProcedimento>();
        }
    }
}
