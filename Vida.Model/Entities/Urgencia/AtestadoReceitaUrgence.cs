﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class AtestadoReceitaUrgence
    {
        //public enum Tipo { AtestadoMedico = "AM", AtestadoComparecimento = "AC", ReceitaMedica = "RM" };

        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Prontuario prontuario;

        public virtual Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
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

        private TipoAtestadoReceita tipoatestadoreceita;
        public virtual TipoAtestadoReceita TipoAtestadoReceita
        {
            get { return tipoatestadoreceita; }
            set { tipoatestadoreceita = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private string conteudo;
        public virtual string Conteudo
        {
            get { return conteudo; }
            set { conteudo = value; }
        }

        //private string tipodocumento;
        //public virtual string TipoDocumento
        //{
        //    get { return tipodocumento; }
        //    set { tipodocumento = value; }
        //}

        //private bool atestado;

        //public virtual bool Atestado
        //{
        //    get { return atestado; }
        //    set { atestado = value; }
        //}

        public AtestadoReceitaUrgence()
        {
        }

        public AtestadoReceitaUrgence(Prontuario prontuario, DateTime data, string codigoprofissional, string cboprofissional, string conteudo,TipoAtestadoReceita tipo)
        {
            this.Prontuario = prontuario;
            this.Data = data;
            this.CodigoProfissional = codigoprofissional;
            this.CBOProfissional = cboprofissional;
            this.Conteudo = conteudo;
            this.TipoAtestadoReceita = tipo;
        }
    }
}
