using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class VinculoProfissional
    {
        public enum DescricaStatus
        {
            Ativo = 'A', Inativo='I'
        }
        
        private EstabelecimentoSaude estabelecimentosaude;

        public virtual EstabelecimentoSaude EstabelecimentoSaude 
        {
            get { return estabelecimentosaude; }
            set { estabelecimentosaude = value; }
        }

        OrgaoEmissor orgaoEmissorRegistroConselho;

        public virtual OrgaoEmissor OrgaoEmissorRegistroConselho
        {
            get { return orgaoEmissorRegistroConselho; }
            set { orgaoEmissorRegistroConselho = value; }
        }

        private string registroconselho;

        public virtual string RegistroConselho
        {
            get { return registroconselho; }
            set { registroconselho = value; }
        }

        private Profissional profissional;

        public virtual Profissional Profissional 
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private CBO cbo;

        public virtual CBO CBO 
        {
            get { return cbo; }
            set { cbo = value; }
        }

        //private Vinculacao vinculacao;

        //public virtual Vinculacao Vinculacao
        //{
        //    get { return vinculacao; }
        //    set { vinculacao = value; }
        //}

        //private TipoVinculacao tipoVinculacao;

        //public virtual TipoVinculacao TipoVinculacao
        //{
        //    get { return tipoVinculacao; }
        //    set { tipoVinculacao = value; }
        //}

        //public virtual string DescricaoTipoVinculacao
        //{
        //    get { return this.TipoVinculacao.DescricaoVinculo; }
        //}

        //private SubTipoVinculacao subTipoVinculacao;

        //public virtual SubTipoVinculacao SubTipoVinculacao
        //{
        //    get { return subTipoVinculacao; }
        //    set { subTipoVinculacao = value; }
        //}

        //public virtual string DescricaoSubTipoVinculacao
        //{
        //    get { return this.SubTipoVinculacao.DescricaoSubVinculo; }
        //}

        private char vinculosus;

        public virtual char VinculoSUS 
        {
            get { return vinculosus; }
            set { vinculosus = value; }
        }

        private string identificacaovinculo;

        public virtual string IdentificacaoVinculo 
        {
            get { return identificacaovinculo; }
            set { identificacaovinculo = value; }
        }

        private int cargaHorariaAmbulatorial;

        public virtual int CargaHorariaAmbulatorial
        {
            get { return cargaHorariaAmbulatorial; }
            set { cargaHorariaAmbulatorial = value; }
        }

        int cargaHorariaHospitalar;

        public virtual int CargaHorariaHospitalar
        {
            get { return cargaHorariaHospitalar; }
            set { cargaHorariaHospitalar = value; }
        }

        int cargaHorariaOutros;

        public virtual int CargaHorariaOutros
        {
            get { return cargaHorariaOutros; }
            set { cargaHorariaOutros = value; }
        }

        //private string registroconselho;

        //public virtual string RegistroConselho 
        //{
        //    get { return registroconselho; }
        //    set { registroconselho = value; }
        //}

        private DateTime dataatualizacao;

        public virtual DateTime DataAtualizacao 
        {
            get { return dataatualizacao; }
            set { dataatualizacao = value; }
        }

        private string status;

        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string NomeProfissional
        {
            get { return this.profissional.Nome; }
        }

        public virtual string CPFProfissional
        {
            get { return this.profissional.CPF; }
        }

        private string usuarioresponsavelatualizacao;

        public virtual string UsuarioResponsavelAtualizacao 
        {
            get { return usuarioresponsavelatualizacao; }
            set { usuarioresponsavelatualizacao = value; }
        }

        public virtual string NomeFantasia
        {
            get { return this.EstabelecimentoSaude.NomeFantasia; }
        }

        public virtual string NomeCBO
        {
            get { return this.CBO.Nome; }
        }

        public VinculoProfissional() 
        {
        }

        public override bool Equals(object obj)
        {
            return this.Profissional.Equals(obj) &&
                   this.EstabelecimentoSaude.Equals(obj) &&
                   this.CBO.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 29;
        }
    }
}
