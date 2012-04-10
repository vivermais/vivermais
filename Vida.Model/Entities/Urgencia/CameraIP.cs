﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class CameraIP
    {
        public CameraIP()
        {
        }

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string local;
        public virtual string Local
        {
            get { return local; }
            set { local = value; }
        }

        private string endereco;
        public virtual string Endereco
        {
            get { return endereco; }
            set { endereco = value; }
        }

        private string cnesunidade;
        public virtual string CNESUnidade
        {
            get { return cnesunidade; }
            set { cnesunidade = value; }
        }
    }
}
