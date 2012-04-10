using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class PrescricaoKitPA
    {
        private Vida.Model.KitPA kitPA;

        public virtual Vida.Model.KitPA KitPA
        {
            get { return kitPA; }
            set { kitPA = value; }
        }

        private string intervalo;

        public virtual string Intervalo
        {
            get { return intervalo; }
            set { intervalo = value; }
        }

        Vida.Model.Prescricao prescricao;

        public virtual Vida.Model.Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
        }

        private ViaAdministracao viaadministracao;

        public virtual ViaAdministracao ViaAdministracao
        {
            get { return viaadministracao; }
            set { viaadministracao = value; }
        }

        public virtual string NomeViaAdministracao
        {
            get { return ViaAdministracao == null ? " - " : ViaAdministracao.Nome; }
        }

        private FormaAdministracao formaadministracao;

        public virtual FormaAdministracao FormaAdministracao
        {
            get { return formaadministracao; }
            set { formaadministracao = value; }
        }

        public virtual string NomeFormaAdministracao
        {
            get { return FormaAdministracao == null ? " - " : FormaAdministracao.Nome; }
        }

        public PrescricaoKitPA()
        {
        }

        public virtual string NomeKitPA 
        {
            get { return this.kitPA.Nome; }
        }

        public virtual int CodigoKit 
        {
            get { return KitPA.Codigo; }
        }

        public override bool Equals(object obj)
        {
            if (this.Prescricao.Codigo == ((PrescricaoKitPA)obj).Prescricao.Codigo
                && this.kitPA.Codigo == ((PrescricaoKitPA)obj).KitPA.Codigo)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 47;
        }
    }
}
