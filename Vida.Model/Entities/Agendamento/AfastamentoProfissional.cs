using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AfastamentoProfissional
    {

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Profissional profissional;

        public virtual Profissional Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }
        
        //private string id_profissional;
        //public virtual string ID_Profissional
        //{
        //    get { return id_profissional; }
        //    set { id_profissional = value; }
        //}
        private EstabelecimentoSaude unidade;
        public virtual EstabelecimentoSaude Unidade
        {
            get { return unidade; }
            set { unidade = value; }
        }
        //private string id_unidade;
        //public virtual string ID_Unidade
        //{
        //    get { return id_unidade; }
        //    set { id_unidade = value; }
        //}

        private DateTime data_inicial;
        public virtual DateTime Data_Inicial
        {
            get { return data_inicial; }
            set { data_inicial = value; }
        }

        private DateTime data_final;
        public virtual DateTime Data_Final
        {
            get { return data_final.Date; }
            set { data_final = value; }
        }

        private string motivo;
        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        private bool inativo;
        public virtual bool Inativo
        {
            get { return inativo; }
            set { inativo = value; }
        }

        private DateTime ? data_reativacao;
        public virtual DateTime ? Data_Reativacao
        {
            get { return data_reativacao; }
            set { data_reativacao = value; }
        }

        private string obs;
        public virtual string Obs
        {
            get { return obs; }
            set { obs = value; }
        }

        public AfastamentoProfissional() 
        {
        }
    }
}
