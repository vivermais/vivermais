﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AprazamentoProcedimentoNaoFaturavel
    {
        public enum StatusItem { Ativo = 'A', Bloqueado = 'B', Finalizado = 'F' };

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public virtual int CodigoProcedimento
        {
            get { return ProcedimentoNaoFaturavel.Codigo; }
        }

        private Prescricao prescricao;
        public virtual Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
        }

        private ProcedimentoNaoFaturavel procedimentoNaoFaturavel;
        public virtual ProcedimentoNaoFaturavel ProcedimentoNaoFaturavel
        {
            get { return procedimentoNaoFaturavel; }
            set { procedimentoNaoFaturavel = value; }
        }

        
        public virtual string NomeProcedimento
        {
            get { return ProcedimentoNaoFaturavel.Nome; }
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
                if (this.status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo)) return "Aplicação não confirmada";
                else if (this.status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Bloqueado)) return "Aplicação Bloqueada";
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

        public AprazamentoProcedimentoNaoFaturavel()
        {
        }

        //public override bool Equals(object obj)
        //{
        //    return this.Prescricao.Codigo == ((AprazamentoProcedimentoNaoFaturavel)obj).Prescricao.Codigo
        //        && this.ProcedimentoNaoFaturavel.Codigo == ((AprazamentoProcedimentoNaoFaturavel)obj).ProcedimentoNaoFaturavel.Codigo
        //        && this.Horario == ((AprazamentoProcedimentoNaoFaturavel)obj).Horario;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 97;
        //}
    }
}