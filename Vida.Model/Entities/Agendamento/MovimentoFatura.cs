﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class MovimentoFatura
    {

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Fatura fatura;
        public virtual Fatura Fatura
        {
            get { return fatura; }
            set { fatura = value; }
        }

        private string id_procedimento;
        public virtual string ID_Procedimento
        {
            get { return id_procedimento; }
            set { id_procedimento = value; }
        }

        private string cns_profissional;
        public virtual string Cns_Profissional
        {
            get { return cns_profissional; }
            set { cns_profissional = value; }
        }

        private string cod_cbo;
        public virtual string Cod_Cbo
        {
            get { return cod_cbo; }
            set { cod_cbo = value; }
        }

        private string cns_paciente;
        public virtual string Cns_Paciente
        {
            get { return cns_paciente; }
            set { cns_paciente = value; }
        }

        private string sexo;
        public virtual string Sexo
        {
            get { return sexo; }
            set { sexo = value; }
        }

        private string ibge;
        public virtual string IBGE
        {
            get { return ibge; }
            set { ibge = value; }
        }

        private string cid;
        public virtual string CID
        {
            get { return cid; }
            set { cid = value; }
        }

        private int idade;
        public virtual int Idade
        {
            get { return idade; }
            set { idade = value; }
        }

        private int qtd;
        public virtual int Qtd
        {
            get { return qtd; }
            set { qtd = value; }
        }

        private string origem;
        public virtual string Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        private string nome_paciente;
        public virtual string Nome_Paciente
        {
            get { return nome_paciente; }
            set { nome_paciente = value; }
        }

        private DateTime data_atendimento;
        public virtual DateTime Data_Atendimento
        {
            get { return data_atendimento; }
            set { data_atendimento = value; }
        }

        private DateTime data_nascimento;
        public virtual DateTime Data_Nascimento
        {
            get { return data_nascimento; }
            set { data_nascimento = value; }
        }

        private string racacor;
        public virtual string RacaCor
        {
            get { return racacor; }
            set { racacor = value; }
        }

        private string autorizacao;
        public virtual string Autorizacao
        {
            get { return autorizacao; }
            set { autorizacao = value; }
        }

        public MovimentoFatura() 
        {
        }
    }
}
