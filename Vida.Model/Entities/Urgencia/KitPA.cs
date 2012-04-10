﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class KitPA
    {
        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int codigomedicamento;

        public virtual int CodigoMedicamento
        {
            get { return codigomedicamento; }
            set { codigomedicamento = value; }
        }

        private string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
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
                if (this.Status == 'A')
                    return "Ativo";
                return "Inativo";
            }
        }

        public KitPA()
        {
        }
    }
}
