using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.DAOOracle
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException()
            : base("Objeto não encontrado com este ID")
        {
        }
    }
}
