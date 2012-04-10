﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class SalaVacina
    {
        //public static int CMADI = 14;
        public enum DescricaoSituacao { Sim = 'S', Nao = 'N' }

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        string responsavel;
        public virtual string Responsavel
        {
            get { return responsavel; }
            set { responsavel = value; }
        }

        EstabelecimentoSaude estabelecimentoSaude;

        public virtual EstabelecimentoSaude EstabelecimentoSaude
        {
            get { return estabelecimentoSaude; }
            set { estabelecimentoSaude = value; }
        }

        public virtual string NomeUnidade
        {
            get { return EstabelecimentoSaude.NomeFantasia; }
        }

        private char ativo;

        public virtual char Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private char bloqueado;

        public virtual char Bloqueado
        {
            get { return bloqueado; }
            set { bloqueado = value; }
        }

        private bool pertencecmadi;
        public virtual bool PertenceCMADI
        {
            get { return pertencecmadi; }
            set { pertencecmadi = value; }
        }

        public virtual string AtivoToString
        {
            get { return ativo == 'S' ? "Sim" : "Não"; }
        }

        public virtual string BloqueadoToString
        {
            get { return bloqueado == 'S' ? "Sim" : "Não"; }
        }

        private IList<int> codigosusuarios;
        public virtual IList<int> CodigosUsuarios
        {
            get { return codigosusuarios; }
            set { codigosusuarios = value; }
        }

        private IList<Usuario> responsaveis;
        public virtual IList<Usuario> Responsaveis
        {
            get { return responsaveis; }
            set { responsaveis = value; }
        }

        public SalaVacina()
        {

        }
    }
}