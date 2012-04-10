using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class GrupoFarmacia
    {
        ElencoMedicamento elenco;

        public virtual ElencoMedicamento Elenco
        {
            get { return elenco; }
            set { elenco = value; }
        }

        Farmacia farmacia;
        public virtual Farmacia Farmacia
        {
            get { return farmacia; }
            set { farmacia = value; }
        }

        public GrupoFarmacia()
        {

        }

        public virtual string NomeFarmacia
        {
            get
            {
                return this.Farmacia.Nome;
            }
        }       
        
        public override bool Equals(object obj)
        {
            return this.Elenco.Codigo == ((GrupoFarmacia)obj).Elenco.Codigo &&
                   this.Farmacia.Codigo == ((GrupoFarmacia)obj).Farmacia.Codigo;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }
    }
}
