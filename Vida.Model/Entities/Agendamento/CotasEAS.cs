﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CotasEAS
    {

        private string id_EstabelecimentoSaude;

        public virtual string Id_EstabelecimentoSaude
        {
            get { return id_EstabelecimentoSaude; }
            set { id_EstabelecimentoSaude = value; }
        }

        private string id_Procedimento;

        public virtual string Id_Procedimento
        {
            get { return id_Procedimento; }
            set { id_Procedimento = value; }
        }

        private float percentual;
        public virtual float Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Id_EstabelecimentoSaude == ((CotasEAS)obj).Id_EstabelecimentoSaude &&
                   this.Id_Procedimento == ((CotasEAS)obj).Id_Procedimento;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }

        public CotasEAS()
        {

        }

    }
}
