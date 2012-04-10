﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ViverMais.Model
{
    public class GenericComparer<T>: IEqualityComparer<T>
    {
        private PropertyInfo atributo;

        public GenericComparer(string nomeatributo)
        {
            this.atributo = typeof(T).GetProperty(nomeatributo, BindingFlags.GetProperty |
                BindingFlags.Instance | BindingFlags.Public);

            if (this.atributo == null)
                throw new ArgumentException(string.Format("{0} nao possui o atributo {1}.", typeof(T),nomeatributo));
        }

        public bool Equals(T x, T y)
        {
            object xValor = this.atributo.GetValue(x, null);
            object yValor = this.atributo.GetValue(y, null);

            if (xValor == null)
                return yValor == null;

            return xValor.Equals(yValor);
        }

        public int GetHashCode(T objeto)
        {
            object valoratributo = this.atributo.GetValue(objeto, null);

            if (valoratributo == null)
                return 0;

            return valoratributo.GetHashCode();
        }
    }
}
