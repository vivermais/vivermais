using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model.Entities
{
    [Serializable]
    public class RelatorioExpression
    {
        string propriedade;

        public virtual string Propriedade
        {
            get { return propriedade; }
            set { propriedade = value; }
        }

        int operador;

        public virtual int Operador
        {
            get { return operador; }
            set { operador = value; }
        }

        object value;

        public virtual object Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public RelatorioExpression(string propriedade, int operador, object value)
        {
            this.Propriedade = propriedade;
            this.Operador = operador;
            this.Value = value;
        }

        public override string ToString()
        {
            string op = string.Empty;
            switch (this.Operador)
            {
                case 0:
                    op = "Igual";
                    break;
                case 1:
                    op = "Diferente";
                    break;
                case 2:
                    op = "Maior";
                    break;
                case 3:
                    op = "Maior Igual";
                    break;
                case 4:
                    op = "Menor";
                    break;
                case 5:
                    op = "Menor Igual";
                    break;
                case 6:
                    op = "Contém";
                    break;
            };
            return Propriedade + " " + op + " " + Value.ToString();
        }
    }
}
