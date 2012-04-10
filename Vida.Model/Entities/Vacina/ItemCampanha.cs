using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vida.Model
{
    [Serializable]
    public class ItemCampanha
    {
        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ItemVacina itemvacina;

        public virtual ItemVacina ItemVacina
        {
            get { return itemvacina; }
            set { itemvacina = value; }
        }

        Campanha campanha;

        public virtual Campanha Campanha
        {
            get { return campanha; }
            set { campanha = value; }
        }

        int quantidade;

        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        public virtual string NomeFabricante
        {
            get { return ItemVacina.FabricanteVacina.Nome; }
        }

        public virtual string NomeVacina
        {
            get { return ItemVacina.Vacina.Nome; }
        }

        public ItemCampanha()
        {

        }
    }
}
