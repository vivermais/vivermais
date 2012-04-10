﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ViverMais.Model
{
    [Serializable]
    public class ProtocoloEnvioBPA
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        EstabelecimentoSaude estabelecimentoSaude;

        public virtual EstabelecimentoSaude EstabelecimentoSaude
        {
            get { return estabelecimentoSaude; }
            set { estabelecimentoSaude = value; }
        }

        Usuario usuario;

        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        DateTime dataEnvio;

        public virtual DateTime DataEnvio
        {
            get { return dataEnvio; }
            set { dataEnvio = value; }
        }

        //string competencia;

        //public virtual string Competencia
        //{
        //    get { return competencia; }
        //    set { competencia = value; }
        //}

        CompetenciaBPA competencia;

        public virtual CompetenciaBPA Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        public virtual DateTime CompetenciaDate 
        {
            get 
            {
                return DateTime.ParseExact(this.Competencia.ToString(), "yyyyMM", CultureInfo.InvariantCulture);
            }
            //set;
        }

        string arquivo;

        public virtual string Arquivo
        {
            get { return arquivo; }
            set { arquivo = value; }
        }

        public virtual string CaminhoArquivo 
        {
            get { return this.Competencia.Ano + "\\" + Arquivo; }
            //set; 
        }

        int tamanhoArquivo;

        public virtual int TamanhoArquivo
        {
            get { return tamanhoArquivo; }
            set { tamanhoArquivo = value; }
        }

        string numeroControle;

        public virtual string NumeroControle
        {
            get { return numeroControle; }
            set { numeroControle = value; }
        }

        public ProtocoloEnvioBPA()
        {

        }
    }
}
