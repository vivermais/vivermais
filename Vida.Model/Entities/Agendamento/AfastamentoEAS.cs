﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AfastamentoEAS
    {

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string id_unidade;
        public virtual string ID_Unidade
        {
            get { return id_unidade; }
            set { id_unidade = value; }
        }

        private DateTime data_inicial;
        public virtual DateTime Data_Inicial
        {
            get { return data_inicial; }
            set { data_inicial = value; }
        }

        private string motivo;
        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
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

        public virtual string DataInicialToString
        {
            get { return Data_Inicial.ToString("dd/MM/yyyy"); }
        }

        public virtual string DataReativacaoToString
        {
            get { return Data_Reativacao != null ? Data_Reativacao.Value.ToString("dd/MM/yyyy") : ""; }
        }

        public AfastamentoEAS() 
        {
        }
    }
}
