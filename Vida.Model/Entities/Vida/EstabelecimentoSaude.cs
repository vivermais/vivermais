﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EstabelecimentoSaude : AModel
    {
        public enum DescricaoStatus { Ativo = 'A', Bloqueado = 'I' };
        public static string NAO_SE_APLICA = "0000000";
        public static string CNES_SMS_SSA = "6385907";

        //private string codigo;

        //public virtual string Codigo 
        //{
        //    get { return codigo; }
        //    set { codigo = value; }
        //}

        private string cnes;

        public virtual string CNES
        {
            get { return cnes; }
            set { cnes = value; }
        }

        private string razaosocial;

        public virtual string RazaoSocial
        {
            get { return razaosocial; }
            set { razaosocial = value; }
        }

        private string siglaEstabelecimento;

        public virtual string SiglaEstabelecimento
        {
            get
            {
                if (siglaEstabelecimento == null)
                {
                    for (int i = 0; i < NomeFantasia.Length; i++)
                    {
                        if (i == 0)
                            this.siglaEstabelecimento = siglaEstabelecimento + NomeFantasia[i];
                        else if (i < 6)
                        {
                            if (NomeFantasia[i].Equals(' '))
                                this.siglaEstabelecimento = siglaEstabelecimento + NomeFantasia[i + 1];
                        }
                    }
                }
                return siglaEstabelecimento;
            }
            set { siglaEstabelecimento = value; }
        }
        private string telefone;

        public virtual string Telefone
        {
            get { return telefone; }
            set { telefone = value; }
        }

        //public string sigla()
        //{
        //    for(int i = 0; i<=NomeFantasia.Length; i++)
        //    {
        //        if (i == 0)
        //            this.SiglaEstabelecimento = SiglaEstabelecimento + NomeFantasia[i];
        //        else
        //        {
        //            if(NomeFantasia[i] == " ")
        //                this.SiglaEstabelecimento = SiglaEstabelecimento + NomeFantasia[i+1];
        //        }
        //    }

        //}

        private string nomefantasia;

        public virtual string NomeFantasia
        {
            get { return nomefantasia; }
            set { nomefantasia = value; }
        }

        private string logradouro;

        public virtual string Logradouro
        {
            get { return logradouro; }
            set { logradouro = value; }
        }

        private Bairro bairro;

        public virtual Bairro Bairro
        {
            get { return bairro; }
            set { bairro = value; }
        }

        public virtual string NomeBairro
        {
            get { return Bairro.Nome; }
        }

        private string cep;

        public virtual string CEP
        {
            get { return cep; }
            set { cep = value; }
        }

        private string cNPJ;

        public virtual string CNPJ
        {
            get { return cNPJ; }
            set { cNPJ = value; }
        }

        private string cNPJMantenedora;

        public virtual string CNPJMantenedora
        {
            get { return cNPJMantenedora; }
            set { cNPJMantenedora = value; }
        }

        private EsferaAdministrativa esferaAdministrativa;

        public virtual EsferaAdministrativa EsferaAdministrativa
        {
            get { return esferaAdministrativa; }
            set { esferaAdministrativa = value; }
        }

        private AtiViverMaisdeEnsino atiViverMaisdeensino;

        public virtual AtiViverMaisdeEnsino AtiViverMaisdeEnsino
        {
            get { return atiViverMaisdeensino; }
            set { atiViverMaisdeensino = value; }
        }

        private NaturezaOrganizacao naturezaorganizacao;

        public virtual NaturezaOrganizacao NaturezaOrganizacao
        {
            get { return naturezaorganizacao; }
            set { naturezaorganizacao = value; }
        }

        private Municipio municipiogestor;

        public virtual Municipio MunicipioGestor
        {
            get { return municipiogestor; }
            set { municipiogestor = value; }
        }

        private TipoEstabelecimento tipoestabelecimento;

        public virtual TipoEstabelecimento TipoEstabelecimento
        {
            get { return tipoestabelecimento; }
            set { tipoestabelecimento = value; }
        }

        //private Distrito distrito;

        //public virtual Distrito Distrito
        //{
        //    get { return distrito; }
        //    set { distrito = value; }
        //}

        //private DesativacaoEstabelecimento desativacaoEstabelecimento;

        //public virtual DesativacaoEstabelecimento DesativacaoEstabelecimento 
        //{
        //    get { return desativacaoEstabelecimento; }
        //    set { desativacaoEstabelecimento = value; }
        //}

        private string statusestabelecimento;

        public virtual string StatusEstabelecimento
        {
            get { return statusestabelecimento; }
            set { statusestabelecimento = value; }
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

        public virtual string DescricaoStatusEstabelecimento
        {
            get
            {
                if (Convert.ToChar(this.statusestabelecimento) == Convert.ToChar(EstabelecimentoSaude.DescricaoStatus.Ativo))
                    return "Ativo";

                return "Bloqueado";
            }
        }

        public EstabelecimentoSaude()
        {
        }

        public override string ToString()
        {
            return this.NomeFantasia;
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}