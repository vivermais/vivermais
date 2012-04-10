using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class LogFarmacia
    {
        private int codigo;

        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        int evento;

        public virtual int Evento
        {
            get { return evento; }
            set { evento = value; }
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

        public LogFarmacia() 
        {
        }

        public LogFarmacia(DateTime data, int codigoUsuario, int evento, string valor)
        {
            this.Data = data;
            this.CodigoUsuario = codigoUsuario;
            this.Evento = evento;
            this.Valor = valor;
        }
    }
}
