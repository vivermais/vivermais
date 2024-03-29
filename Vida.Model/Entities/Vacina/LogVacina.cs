﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class LogVacina
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        int codigoevento;

        public virtual int CodigoEvento
        {
            get { return codigoevento; }
            set { codigoevento = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private int codigousuario;

        public virtual int CodigoUsuario
        {
            get { return codigousuario; }
            set { codigousuario = value; }
        }

        private string valor;

        public virtual string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public LogVacina()
        {
        }

        public LogVacina(DateTime data, int codigousuario, int codigoevento, string valor)
        {
            this.Data = data;
            this.CodigoUsuario = codigousuario;
            this.CodigoEvento = codigoevento;
            this.Valor = valor;
        }
    }
}
