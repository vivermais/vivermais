using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ListaProcura
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }
        
        private string id_paciente;

        public virtual string Id_paciente
        {
            get { return id_paciente; }
            set { id_paciente = value; }
        }

        private string id_procedimento;
        
        public virtual string Id_procedimento
        {
            get { return id_procedimento; }
            set { id_procedimento = value; }
        }

        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private DateTime dataInicial;

        public virtual DateTime DataInicial
        {
            get { return dataInicial; }
            set { dataInicial = value; }
        }

        private DateTime dataUltimaProcura;

        public virtual DateTime DataUltimaProcura
        {
            get { return dataUltimaProcura; }
            set { dataUltimaProcura = value; }
        }

        private string competencia;

        public virtual string Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        private bool agendado;

        public virtual bool Agendado
        {
            get { return agendado; }
            set { agendado = value; }
        }

        private int quantidade;

        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public ListaProcura()
        {

        }
    }
}
