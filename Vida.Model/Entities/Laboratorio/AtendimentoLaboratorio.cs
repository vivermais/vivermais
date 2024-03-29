﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AtendimentoLaboratorio
    {
        int codigo;

        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string numeroAtendimento;

        public string NumeroAtendimento
        {
            get { return numeroAtendimento; }
            set { numeroAtendimento = value; }
        }

        DateTime dataAtendimento;

        public DateTime DataAtendimento
        {
            get { return dataAtendimento; }
            set { dataAtendimento = value; }
        }

        PacienteLaboratorio paciente;

        public PacienteLaboratorio Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        SolicitanteExameLaboratorio medicoSolicitante;

        public SolicitanteExameLaboratorio MedicoSolicitante
        {
            get { return medicoSolicitante; }
            set { medicoSolicitante = value; }
        }

        bool urgente;

        public bool Urgente
        {
            get { return urgente; }
            set { urgente = value; }
        }

        string responsavel;

        public string Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        string numeroAtendimentoOriginal;

        public string NumeroAtendimentoOriginal
        {
            get { return numeroAtendimentoOriginal; }
            set { numeroAtendimentoOriginal = value; }
        }

        string senhaInternet;

        public string SenhaInternet
        {
            get { return senhaInternet; }
            set { senhaInternet = value; }
        }

        int usuario;

        public int Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        DateTime dataRegistro;

        public DateTime DataRegistro
        {
            get { return dataRegistro; }
            set { dataRegistro = value; }
        }

        private UnidadeLaboratorio unidadeOrigem;

        public UnidadeLaboratorio UnidadeOrigem
        {
            get { return unidadeOrigem; }
            set { unidadeOrigem = value; }
        }
        private UnidadeLaboratorio unidadeEntrega;

        public UnidadeLaboratorio UnidadeEntrega
        {
            get { return unidadeEntrega; }
            set { unidadeEntrega = value; }
        }

        private IList<ExameLaboratorio> exames;

        public IList<ExameLaboratorio> Exames
        {
            get { return exames; }
            set { exames = value; }
        }

    }
}
