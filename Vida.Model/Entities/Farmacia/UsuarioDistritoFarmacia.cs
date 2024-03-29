﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class UsuarioDistritoFarmacia
    {
        int codigousuario;
        public virtual int CodigoUsuario
        {
            get { return codigousuario; }
            set { codigousuario = value; }
        }

        int codigodistrito;
        public virtual int CodigoDistrito
        {
            get { return codigodistrito; }
            set { codigodistrito = value; }
        }

        public UsuarioDistritoFarmacia()
        {
            
        }

        public override bool Equals(object obj)
        {
            if (this.CodigoDistrito == ((UsuarioDistritoFarmacia)obj).CodigoDistrito
                && this.CodigoUsuario == ((UsuarioDistritoFarmacia)obj).CodigoUsuario)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return 47 * base.GetHashCode();
        }
    }
}
