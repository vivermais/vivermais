﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class RequisicaoMedicamento
    {
        //public static int ABERTA = 3;
        //public static int PENDENTE = 2;
        //public static int ATENDIDA = 1;
        //public static int AUTORIZADA = 4;
        //public static int CONCLUIDA = 5;
        //public static int CANCELADA = 6;

        public enum StatusRequisicao { ABERTA = 3, PENDENTE = 2, ATENDIDA = 1, AUTORIZADA = 4, CONCLUIDA = 5, CANCELADA = 6 };

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DateTime datacriacao;
        public virtual DateTime DataCriacao
        {
            get { return datacriacao; }
            set { datacriacao = value; }
        }

        DateTime? dataenvio;
        public virtual DateTime? DataEnvio
        {
            get { return dataenvio; }
            set { dataenvio = value; }
        }

        DateTime? dataatendida;
        public virtual DateTime? DataAtendida
        {
            get { return dataatendida; }
            set { dataatendida = value; }
        }

        Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }
        
        int cod_status;
        public virtual int Cod_Status
        {
            get { return cod_status; }
            set { cod_status = value; }
        }

        DateTime data_status;
        public virtual DateTime Data_Status
        {
            get { return data_status; }
            set { data_status = value; }
        }

        string obs;
        public virtual string Obs
        {
            get { return obs; }
            set { obs = value; }
        }

        DateTime? data_autorizacao;
        public virtual DateTime? Data_Autorizacao
        {
            get { return data_autorizacao; }
            set { data_autorizacao = value; }
        }

        DateTime? data_fornecimento;
        public virtual DateTime? Data_Fornecimento
        {
            get { return data_fornecimento; }
            set { data_fornecimento = value; }
        }

        public virtual string Status
        {
            get
            {
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.ATENDIDA)
                {
                    return "ATENDIDA";
                }
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.PENDENTE)
                {
                    return "PENDENTE";
                }
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.ABERTA)
                {
                    return "ABERTA";
                }
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.AUTORIZADA)
                {
                    return "AUTORIZADA";
                }
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.CONCLUIDA)
                {
                    return "CONCLUIDA";
                }
                if (this.Cod_Status == (int)RequisicaoMedicamento.StatusRequisicao.CANCELADA)
                {
                    return "CANCELADA";
                }
                return "";
            }
        }

        public virtual string NomeFarmacia
        {
            get
            {
                return this.Farmacia.Nome;
            }
        }

        public virtual string DataEnvioToString
        {
            get
            {
                return this.DataEnvio == null?"-":((DateTime)this.DataEnvio.Value).ToString();
            }
        }

        public RequisicaoMedicamento()
        {

        }
    }
}