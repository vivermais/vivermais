﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ViverMais.Model
{
    [Serializable]
    public class UsuarioProfissionalUrgence
    {
        private int id_usuario;
        public virtual int Id_Usuario 
        {
            get { return id_usuario; }
            set { id_usuario = value; }
        }

        private string id_profissional;
        public virtual string Id_Profissional
        {
            get { return id_profissional; }
            set { id_profissional = value; }
        }

        private string identificacao;
        public virtual string Identificacao
        {
            get { return identificacao; }
            set { identificacao = value; }
        }

        private string codigocbo;
        public virtual string CodigoCBO
        {
            get { return codigocbo; }
            set { codigocbo = value; }
        }

        private string unidadevinculo;
        public virtual string UnidadeVinculo
        {
            get { return unidadevinculo; }
            set { unidadevinculo = value; }
        }

        public UsuarioProfissionalUrgence()
        {
        }
    }
}
