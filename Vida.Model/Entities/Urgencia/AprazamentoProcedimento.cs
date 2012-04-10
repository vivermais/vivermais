﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AprazamentoProcedimento
    {
        public enum StatusItem { Ativo = 'A', Bloqueado = 'B', Finalizado = 'F' };

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string codigocid;
        public virtual string CodigoCid
        {
            get { return codigocid; }
            set { codigocid = value; }
        }

        private Prescricao prescricao;
        public virtual Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
        }

        private string codigoprocedimento;
        public virtual string CodigoProcedimento
        {
            get { return codigoprocedimento; }
            set { codigoprocedimento = value; }
        }

        private Procedimento procedimento;
        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        public virtual string NomeProcedimento
        {
            get { return Procedimento.Nome; }
        }

        private DateTime horario;
        public virtual DateTime Horario
        {
            get { return horario; }
            set { horario = value; }
        }

        public virtual string DataAplicacao 
        {
            get { return Horario.ToString("dd/MM/yyyy"); }
        }

        public virtual string HoraAplicacao
        {
            get { return Horario.ToString("HH:mm"); }
        }

        private char status;
        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string DescricaoStatus
        {
            get 
            {
                if (this.status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo)) return "Aplicação não confirmada";
                else if (this.status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Bloqueado)) return "Aplicação Bloqueada";
                return "Aplicação Confirmada";
            }
        }

        private string codigoprofissional;
        public virtual string CodigoProfissional
        {
            get { return codigoprofissional; }
            set { codigoprofissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        private Profissional profissional;
        public virtual Profissional Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        public virtual string NomeProfissionalSolicitante
        {
            get { return Profissional.Nome; }
        }

        private string codigoprofissionalconfirmacao;
        public virtual string CodigoProfissionalConfirmacao
        {
            get { return codigoprofissionalconfirmacao; }
            set { codigoprofissionalconfirmacao = value; }
        }

        private string cboprofissionalconfirmacao;
        public virtual string CBOProfissionalConfirmacao
        {
            get { return cboprofissionalconfirmacao; }
            set { cboprofissionalconfirmacao = value; }
        }

        private Profissional profissionalconfirmacao;
        public virtual Profissional ProfissionalConfirmacao
        {
            get { return profissionalconfirmacao; }
            set { profissionalconfirmacao = value; }
        }

        public virtual string NomeProfissionalExecutor
        {
            get { return ProfissionalConfirmacao != null ? ProfissionalConfirmacao.Nome : " - "; }
        }

        private DateTime ? horarioconfirmacao;
        public virtual DateTime ? HorarioConfirmacao
        {
            get { return horarioconfirmacao; }
            set { horarioconfirmacao = value; }
        }

        private DateTime? horarioconfirmacaosistema;

        public virtual DateTime? HorarioConfirmacaoSistema
        {
            get { return horarioconfirmacaosistema; }
            set { horarioconfirmacaosistema = value; }
        }

        public virtual string DataExecucao 
        {
            get 
            {
                if (HorarioConfirmacao.HasValue) 
                {
                    return HorarioConfirmacao.Value.ToString("dd/MM/yyyy");
                }
                return " - ";
            }
        }

        public virtual string HoraExecucao
        {
            get
            {
                if (HorarioConfirmacao.HasValue)
                {
                    return HorarioConfirmacao.Value.ToString("HH:mm");
                }
                return " - ";
            }
        }

        private DateTime horariovalidoprescricao;
        public virtual DateTime HorarioValidoPrescricao 
        {
            get { return horariovalidoprescricao; }
            set { horariovalidoprescricao = value; }
        }

        public AprazamentoProcedimento()
        {
        }

        //public override bool Equals(object obj)
        //{
        //    return this.Prescricao.Codigo == ((AprazamentoProcedimento)obj).Prescricao.Codigo
        //        && this.CodigoProcedimento == ((AprazamentoProcedimento)obj).CodigoProcedimento
        //        && this.Horario == ((AprazamentoProcedimento)obj).Horario;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 101;
        //}
    }
}
