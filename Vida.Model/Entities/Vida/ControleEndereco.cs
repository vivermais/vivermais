using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControleEndereco:AModel
    {
        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DateTime dt_operacao;

        public virtual DateTime DT_OPERACAO
        {
            get { return dt_operacao; }
            set { dt_operacao = value; }
        }

        char nu_versao;

        public virtual char NU_VERSAO
        {
            get { return nu_versao; }
            set { nu_versao = value; }
        }

        string co_origem;

        public virtual string CO_ORIGEM
        {
            get { return co_origem; }
            set { co_origem = value; }
        }

        char st_controle;

        public virtual char ST_CONTROLE
        {
            get { return st_controle; }
            set { st_controle = value; }
        }

        char st_ativo;

        public virtual char ST_ATIVO
        {
            get { return st_ativo; }
            set { st_ativo = value; }
        }

        char st_excluido;

        public virtual char ST_EXCLUIDO
        {
            get { return st_excluido; }
            set { st_excluido = value; }
        }

        char fl_erros;

        public virtual char FL_ERROS
        {
            get { return fl_erros; }
            set { fl_erros = value; }
        }

        char st_ja_retornou;

        public virtual char ST_JA_RETORNOU
        {
            get { return st_ja_retornou; }
            set { st_ja_retornou = value; }
        }

        string co_Tipo_Endereco;

        public virtual string CO_TIPO_ENDERECO
        {
            get { return co_Tipo_Endereco; }
            set { co_Tipo_Endereco = value; }
        }

        char st_vinculo;

        public virtual char ST_VINCULO
        {
            get { return st_vinculo; }
            set { st_vinculo = value; }
        }
        

        public ControleEndereco()
        {
            NU_VERSAO = '0';
            CO_ORIGEM = "023";
            ST_EXCLUIDO = '0';
            FL_ERROS = '0';
            ST_JA_RETORNOU = '0';
        }

        public override bool Equals(object obj)
        {
            return this.Codigo == ((ControleEndereco)obj).Codigo;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public override bool Persistido()
        {
            return this.codigo != null && this.codigo != string.Empty;
        }

        public virtual void GerarCodigo()
        {
            if (this.codigo == null || this.codigo == string.Empty)
                this.codigo = DateTime.Now.ToString("yyyyMMdd-hhmm") + "-endereco-pms-" + Guid.NewGuid().ToString().Remove(21);
        }
    }
}
