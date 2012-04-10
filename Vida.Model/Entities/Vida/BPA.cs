using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class BPA
    {
        public static char APAC = 'A';
        public static char CONSOLIDADO = 'C';
        public static char INDIVIDUALIZADO = 'I';

        public BPA()
        {
        }

        protected Procedimento procedimento;
        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        protected CBO cbo;
        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        protected Paciente paciente;
        public Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        protected int quantidade;
        /// <summary>
        /// Quantidade de Procedimentos
        /// </summary>
        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public string NomePaciente()
        {
            string nomepaciente = paciente.Nome;

            if (nomepaciente.Length > 30)
                nomepaciente = nomepaciente.Remove(30);
            else
            {
                while (nomepaciente.Length != 30)
                    nomepaciente += " ";
            }

            return nomepaciente;
        }

        public virtual int IdadePaciente()
        {
            return this.CalcularIdade(DateTime.Now, this.paciente.DataNascimento);
        }

        protected int CalcularIdade(DateTime dataatual, DateTime datanascimento)
        {
            int idade = dataatual.Year - datanascimento.Year;

            if (dataatual.Month < datanascimento.Month ||
                (dataatual.Month == datanascimento.Month &&
                    dataatual.Day < datanascimento.Day))
                idade--;

            return idade;
        }
    }
}
