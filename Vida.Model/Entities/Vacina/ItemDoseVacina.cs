﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemDoseVacina
    {
        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Vacina vacina;
        public virtual Vacina Vacina
        {
            get { return vacina; }
            set { vacina = value; }
        }

        DoseVacina dosevacina;
        public virtual DoseVacina DoseVacina
        {
            get { return dosevacina; }
            set { dosevacina = value; }
        }

        public virtual string NomeDose
        {
            get { return dosevacina.Descricao; }
        }

        public ItemDoseVacina()
        {

        }

        public override bool Equals(object obj)
        {
            return (Vacina.Codigo == ((ItemDoseVacina)obj).Vacina.Codigo &&
                DoseVacina.Codigo == ((ItemDoseVacina)obj).DoseVacina.Codigo) ? true : false;
        }


    }
}
