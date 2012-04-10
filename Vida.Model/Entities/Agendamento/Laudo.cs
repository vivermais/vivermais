﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Laudo
    {

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private byte[] imagem;

        public virtual byte[] Imagem
        {
            get { return imagem; }
            set { imagem = value; }
        }

        private Solicitacao solicitacao;
        public virtual Solicitacao Solicitacao
        {
            get { return solicitacao; }
            set { solicitacao = value; }
        }

        private string endereco;
        public virtual string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        public Laudo() 
        {
        }
    }
}
