using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ErroViverMais
    {
        private Exception exception;

        public virtual Exception Exception
        {
            get { return exception; }
            set { exception = value; }
        }
        private string pagina;

        public virtual string Pagina
        {
            get { return pagina; }
            set { pagina = value; }
        }

        public ErroViverMais(Exception exception, string pagina)
        {
            this.exception = exception;
            this.pagina = pagina;
        }
    }
}
