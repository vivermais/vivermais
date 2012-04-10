﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Municipio : AModel
    {

        public static string SALVADOR = "292740";

        public Municipio()
        {

        }

        string codigo;

        public virtual string Codigo
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

        public virtual string NomeSemUF
        {
            get
            {
                if (nome.LastIndexOf('-') != -1)
                    return nome.Trim().Substring(0, nome.Trim().LastIndexOf('-')).Trim();
                else
                    return this.Nome;
                //return nome.Trim().Split('-')[0].Trim();
            }
        }

        UF uf;

        public virtual UF UF
        {
            get { return uf; }
            set { uf = value; }
        }

        //Utilizado para importar CEPS
        private long cep;
        public virtual long CEP
        {
            get { return cep; }
            set { cep = value; }
        }


        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
