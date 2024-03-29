﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ImportacaoPPIReferencia
    {
        public ImportacaoPPIReferencia()
        {

        }

        private string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nomeArquivo;

        public virtual string NomeArquivo
        {
            get { return nomeArquivo; }
            set { nomeArquivo = value; }
        }

        private string tamanhoArquivo;

        public virtual string TamanhoArquivo
        {
            get { return tamanhoArquivo; }
            set { tamanhoArquivo = value; }
        }

        private Usuario usuario;

        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        public virtual string NomeUsuario
        {
            get { return Usuario.Nome; }
        }
        
        
        private DateTime dataImportacao;

        public virtual DateTime DataImportacao
        {
            get { return dataImportacao; }
            set { dataImportacao = value; }
        }

    }
}
