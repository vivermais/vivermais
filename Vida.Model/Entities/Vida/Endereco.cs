using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ViverMais.Model
{
    [Serializable]
    public class Endereco : AModel
    {
        ControleEndereco controleEndereco;
        public virtual ControleEndereco ControleEndereco
        {
            get { return controleEndereco; }
            set { controleEndereco = value; }
        }

        [XmlIgnore]
        public virtual string Codigo
        {
            get { return this.ControleEndereco.Codigo; }
            set
            {
                if (this.controleEndereco == null)
                {
                    this.controleEndereco = new ControleEndereco();
                    this.ControleEndereco.Codigo = value;
                }
            }
        }

        string logradouro;

        public virtual string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        string numero;

        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        string complemento;

        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        string bairro;

        public virtual string Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        string cep;

        public virtual string CEP
        {
            get { return cep; }
            set { cep = value; }
        }

        string ddd;

        public virtual string DDD
        {
            get { return ddd; }
            set { ddd = value; }
        }

        string telefone;

        public virtual string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        TipoLogradouro tipoLogradouro;

        public virtual TipoLogradouro TipoLogradouro
        {
            get { return tipoLogradouro; }
            set { tipoLogradouro = value; }
        }

        Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        //IList<Paciente> pacientes;

        //public virtual IList<Paciente> Pacientes
        //{
        //    get { return pacientes; }
        //    set { pacientes = value; }
        //}

        public Endereco()
        {

        }

        public override bool Equals(object obj)
        {
            Endereco endereco = (Endereco)obj;

            if (this.CEP == null || this.CEP.Trim() != endereco.CEP.Trim() || this.Municipio == null || this.Municipio.Codigo != endereco.Municipio.Codigo
                || this.TipoLogradouro.Codigo != endereco.TipoLogradouro.Codigo
                || !this.Logradouro.Trim().ToUpper().Equals(endereco.Logradouro.Trim().ToUpper())
                || !this.Numero.Trim().ToUpper().Equals(endereco.Numero.Trim().ToUpper())
                || !this.Bairro.Trim().ToUpper().Equals(endereco.Bairro.Trim().ToUpper()))
            return false;

            return true;

            //return this.ControleEndereco.Codigo == ((Endereco)obj).ControleEndereco.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public override bool Persistido()
        {
            return this.controleEndereco != null;
        }

        //public virtual bool isEquals(Endereco endereco)
        //{
        //    if (this.CEP.Trim() != endereco.CEP.Trim() || this.Municipio.Codigo != endereco.Municipio.Codigo
        //        || this.TipoLogradouro.Codigo != endereco.TipoLogradouro.Codigo
        //        || !this.Logradouro.Trim().ToUpper().Equals(endereco.Logradouro.Trim().ToUpper())
        //        || !this.Numero.Trim().ToUpper().Equals(endereco.Numero.Trim().ToUpper())
        //        || !this.Bairro.Trim().ToUpper().Equals(endereco.Bairro.Trim().ToUpper()))
        //    return false;

        //    return true;
        //}
    }
}
