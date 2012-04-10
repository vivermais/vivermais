using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ViverMais.Model
{
    [Serializable]
    public class Profissional
    {
        public enum DescricaoStatus { Ativo = 'A', Inativo = 'I' };

        //private string codigo;

        ///// <summary>
        ///// Chave primária de Profissional - SMS (Utilizada Por Nós na Busca pela Chave Primária)
        ///// </summary>
        //public virtual string Codigo
        //{
        //    get { return codigo; }
        //    set { codigo = value; }
        //}

        //private string prof_ID;

        ///// <summary>
        ///// Chave primária de Profissional - Ministério
        ///// </summary>
        //public virtual string Prof_ID
        //{
        //    get { return prof_ID; }
        //    set { prof_ID = value; }
        //}

        private string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private string nacionalidade;

        public virtual string Nacionalidade
        {
            get { return nacionalidade; }
            set { nacionalidade = value; }
        }

        private RacaCor racacor;

        public virtual RacaCor RacaCor
        {
            get { return racacor; }
            set { racacor = value; }
        }

        private string cpf;

        public virtual string CPF
        {
            get { return cpf; }
            set { cpf = value; }
        }

        private string rg;

        public virtual string RG
        {
            get { return rg; }
            set { rg = value; }
        }

        private OrgaoEmissor orgaoEmissorRG;

        public virtual OrgaoEmissor OrgaoEmissorRG
        {
            get { return orgaoEmissorRG; }
            set { orgaoEmissorRG = value; }
        }

        private Municipio municipio;

        public virtual Municipio Municipio
        {
            get { return municipio; }
            set { municipio = value; }
        }

        private UF ufOrgaoEmissor;

        public virtual UF UfOrgaoEmissor
        {
            get { return ufOrgaoEmissor; }
            set { ufOrgaoEmissor = value; }
        }

        private DateTime dataEmissaoRG;

        public virtual DateTime DataEmissaoRG
        {
            get { return dataEmissaoRG; }
            set { dataEmissaoRG = value; }
        }

        private DateTime datanascimento;

        public virtual DateTime DataNascimento
        {
            get { return datanascimento; }
            set { datanascimento = value; }
        }

        private char sexo;

        public virtual char Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        private TipoLogradouro tipoLogradouro;

        public virtual TipoLogradouro TipoLogradouro
        {
            get { return tipoLogradouro; }
            set { tipoLogradouro = value; }
        }

        private UF ufResidencia;

        public virtual UF UfResidencia
        {
            get { return ufResidencia; }
            set { ufResidencia = value; }
        }

        private string nomemae;

        public virtual string NomeMae
        {
            get { return nomemae; }
            set { nomemae = value; }
        }

        private string nomepai;

        public virtual string NomePai
        {
            get { return nomepai; }
            set { nomepai = value; }
        }

        private string telefone;

        public virtual string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        private string tituloeleitor;

        public virtual string TituloEleitor
        {
            get { return tituloeleitor; }
            set { tituloeleitor = value; }
        }

        private string zonaeleitoral;

        public virtual string ZonaEleitoral
        {
            get { return zonaeleitoral; }
            set { zonaeleitoral = value; }
        }

        private string secaoeleitoral;

        public virtual string SecaoEleitoral
        {
            get { return secaoeleitoral; }
            set { secaoeleitoral = value; }
        }

        private char vinculosus;

        public virtual char VinculoSUS
        {
            get { return vinculosus; }
            set { vinculosus = value; }
        }

        private string pispasep;

        public virtual string PISPASEP
        {
            get { return pispasep; }
            set { pispasep = value; }
        }

        private string numerocarteiratrabalho;

        public virtual string NumeroCarteiraTrabalho
        {
            get { return numerocarteiratrabalho; }
            set { numerocarteiratrabalho = value; }
        }

        private string seriecarteiratrabalho;

        public virtual string SerieCarteiraTrabalho
        {
            get { return seriecarteiratrabalho; }
            set { seriecarteiratrabalho = value; }
        }

        private string logradouro;

        public virtual string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        private string numero;

        public virtual string Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private string complemento;

        public virtual string Complemento
        {
            get { return complemento; }
            set { complemento = value; }
        }

        private string cep;

        public virtual string CEP
        {
            get { return cep; }
            set { cep = value; }
        }

        private Bairro bairro;

        public virtual Bairro Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        private char statusprofissional;

        public virtual char StatusProfissional
        {
            get { return statusprofissional; }
            set { statusprofissional = value; }
        }

        private DateTime dataatualizacao;

        public virtual DateTime DataAtualizacao
        {
            get { return dataatualizacao; }
            set { dataatualizacao = value; }
        }

        private string usuarioresponsavelatualizacao;

        public virtual string UsuarioResponsavelAtualizacao
        {
            get { return usuarioresponsavelatualizacao; }
            set { usuarioresponsavelatualizacao = value; }
        }

        //string codigoPacienteViverMais;

        ///// <summary>
        ///// Codigo do Paciente do ViverMais associado. Use os dados do paciente
        ///// ao invés desses caso essa propriedade esteja definida
        ///// </summary>
        //public virtual string CodigoPacienteViverMais
        //{
        //    get { return codigoPacienteViverMais; }
        //    set { codigoPacienteViverMais = value; }
        //}

        private Banco banco;

        public virtual Banco Banco
        {
            get { return banco; }
            set { banco = value; }
        }

        string numeroAgencia;

        public virtual string NumeroAgencia
        {
            get { return numeroAgencia; }
            set { numeroAgencia = value; }
        }

        private string cartaoSUS;

        public virtual string CartaoSUS
        {
            get { return cartaoSUS; }
            set { cartaoSUS = value; }
        }

        string contaCorrente;

        public virtual string ContaCorrente
        {
            get { return contaCorrente; }
            set { contaCorrente = value; }
        }

        public override string ToString()
        {
            return this.Nome;
        }

        public Profissional()
        {
        }
    }
}
