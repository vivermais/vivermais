using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ViverMais.Model
{
    [Serializable]
    public class Pais:AModel
    {
        public static string BRASIL = "010";
        public static string NATURALIZADO = "020";

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

        public Pais()
        {

        }

        public override bool Persistido()
        {
            return this.codigo != string.Empty;
        }
    }
}
