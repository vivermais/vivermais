using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ViverMais.Model
{
    [Serializable]
    public class Paciente:AModel
    {
        //ControlePaciente controlePaciente;

        //public virtual ControlePaciente ControlePaciente
        //{
        //    get { return controlePaciente; }
        //    set { controlePaciente = value; }
        //}

        string codigo;

        public virtual string Codigo
        {
            get { return this.codigo; }
            set { this.codigo = value; }
        }

        string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        string nomeMae;

        public virtual string NomeMae
        {
            get { return nomeMae; }
            set { nomeMae = value; }
        }

        string nomePai;

        public virtual string NomePai
        {
            get { return nomePai; }
            set { nomePai = value; }
        }

        DateTime dataNascimento;

        public virtual DateTime DataNascimento
        {
            get { return dataNascimento; }
            set { dataNascimento = value; }
        }

        char sexo;

        public virtual char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        RacaCor racaCor;
        public virtual RacaCor RacaCor
        {
            get { return racaCor; }
            set { racaCor = value; }
        }

        private Etnia etnia;

        public virtual Etnia Etnia
        {
            get { return etnia; }
            set { etnia = value; }
        }


        char frequentaEscola;

        public virtual char FrequentaEscola
        {
            get { return frequentaEscola; }
            set { frequentaEscola = value; }
        }

        Pais pais;
        public virtual Pais Pais
        {
            get { return pais; }
            set { pais = value; }
        }

        Municipio municipioNascimento;
        public virtual Municipio MunicipioNascimento
        {
            get { return municipioNascimento; }
            set { municipioNascimento = value; }
        }

        char vivo;

        public virtual char Vivo
        {
            get { return vivo; }
            set { vivo = value; }
        }

        string email;

        public virtual string Email
        {
            get { return email; }
            set { email = value; }
        }

        char st_profissional;

        public virtual char ST_PROFISSIONAL
        {
            get { return st_profissional; }
            set { st_profissional = value; }
        }

        char st_frequenta_escola;

        public virtual char ST_FREQUENTA_ESCOLA
        {
            get { return st_frequenta_escola; }
            set { st_frequenta_escola = value; }
        }

        EstadoCivil estadoCivil;        
        public virtual EstadoCivil EstadoCivil
        {
            get { return estadoCivil; }
            set { estadoCivil = value; }
        }

        DateTime dataInclusaoRegistro;

        public virtual DateTime DataInclusaoRegistro
        {
            get { return dataInclusaoRegistro; }
            set { dataInclusaoRegistro = value; }
        }

        DateTime dataPreenchimentoFormulario;

        public virtual DateTime DataPreenchimentoFormulario
        {
            get { return dataPreenchimentoFormulario; }
            set { dataPreenchimentoFormulario = value; }
        }

        Municipio municipioResidencia;
        
        public virtual Municipio MunicipioResidencia
        {
            get { return municipioResidencia; }
            set { municipioResidencia = value; }
        }

        char semDocumento;

        public virtual char SemDocumento
        {
            get { return semDocumento; }
            set { semDocumento = value; }
        }

        //IList<CartaoSUS> cartoesSUS;

        //public virtual IList<CartaoSUS> CartoesSUS
        //{
        //    get { return cartoesSUS; }
        //    set { cartoesSUS = value; }
        //}

        public Paciente()
        {
            //CartoesSUS = new List<CartaoSUS>();
        }

        public override bool Equals(object obj)
        {
            return this.Codigo == ((ViverMais.Model.Paciente)obj).Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public virtual int Idade
        {
            get { return new DateTime(DateTime.Now.Subtract(this.DataNascimento).Ticks).Year - 1; }
        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty & this.codigo != null;
        }

        private DeficienciaPaciente deficiencia;
        public virtual DeficienciaPaciente Deficiencia
        {
            get { return deficiencia; }
            set { deficiencia = value; }
        }
    }
}
