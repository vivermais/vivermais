using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model.Entities;

namespace ViverMais.Model
{
    [Serializable]
    [RelatorioAttribute(true, true)]
    public class LogViverMais:AModel
    {
        DateTime data;
        
        [RelatorioAttribute(true, true)]
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        Usuario usuario;

        [RelatorioAttribute(false, true)]
        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        int evento;

        public virtual int Evento
        {
            get { return evento; }
            set { evento = value; }
        }

        //EventoViverMais evento;

        //public virtual EventoViverMais Evento
        //{
        //    get { return evento; }
        //    set { evento = value; }
        //}

        string valor;

        public virtual string Valor
        {
            get { return valor; }
            set { valor = value; }
        }

        public LogViverMais()
        {

        }

        public LogViverMais(DateTime data, Usuario usuario, int evento, string valor)
        {
            this.Data = data;
            this.Usuario = usuario;
            this.Evento = evento;
            this.Valor = valor;
        }

        public override bool Equals(object obj)
        {
            return
                this.Data.Equals(((LogViverMais)obj).Data) &&
                this.Usuario.Codigo.Equals(((LogViverMais)obj).Usuario.Codigo) &&
                this.Evento.Equals(((LogViverMais)obj).Evento);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 43;
        }

        public override bool Persistido()
        {
            throw new NotImplementedException();
        }
    }
}
