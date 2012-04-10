using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model.Entities.ViverMais
{
    [Serializable]
    public class BpaIndividualizado : BPA
    {
        public BpaIndividualizado()
        {
        }

        private string cnsMedico;
        public virtual string CnsMedico
        {
            get { return cnsMedico; }
            set { cnsMedico = value; }
        }

        private string cnsPaciente;
        public virtual string CnsPaciente
        {
            get { return cnsPaciente; }
            set { cnsPaciente = value; }
        }

        private string cid;
        public virtual string Cid
        {
            get { return cid; }
            set { cid = value; }
        }

        public string CidFormatado
        {
            get
            {
                string cidFormatado = this.Cid;

                while (cidFormatado.Length < 4)
                    cidFormatado += " ";

                return cidFormatado;
            }
        }

        private string numeroAutorizacao;
        public virtual string NumeroAutorizacao
        {
            get { return numeroAutorizacao; }
            set { numeroAutorizacao = value; }
        }

        private string codigoMunicipioResidencia;
        public virtual string CodigoMunicipioResidencia
        {
            get { return codigoMunicipioResidencia; }
            set { codigoMunicipioResidencia = value; }
        }

        public string NumeroAutorizacaoFormatado()
        {
            string temp = this.numeroAutorizacao;

            if (temp.Length > 13)
                return temp.Remove(13);
            else
                while (temp.Length != 13)
                    temp += " ";

            return temp;
        }

        private DateTime dataatendimento;
        public virtual DateTime DataAtendimento
        {
            get { return dataatendimento; }
            set { dataatendimento = value; }
        }

        public override int IdadePaciente()
        {
            return this.CalcularIdade(this.dataatendimento, this.paciente.DataNascimento);
        }
    }
}
