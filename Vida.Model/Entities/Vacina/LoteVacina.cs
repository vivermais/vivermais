﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class LoteVacina
    {
        private long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        //private Vacina vacina;

        //public virtual Vacina Vacina
        //{
        //    get { return vacina; }
        //    set { vacina = value; }
        //}

        private ItemVacina itemvacina;
        public virtual ItemVacina ItemVacina
        {
            get { return itemvacina; }
            set { itemvacina = value; }
        }

        public virtual string NomeVacina
        {
            get { return ItemVacina.Vacina.Nome; }
        }

        //private FabricanteVacina fabricante;

        //public virtual FabricanteVacina Fabricante
        //{
        //    get { return fabricante; }
        //    set { fabricante = value; }
        //}

        public virtual string NomeFabricante
        {
            get { return ItemVacina.FabricanteVacina.Nome; }
        }

        public virtual int AplicacaoVacina
        {
            get { return ItemVacina.Aplicacao; }
        }

        private string identificacao;

        public virtual string Identificacao
        {
            get { return identificacao; }
            set { identificacao = value; }
        }

        private DateTime datavalidade;

        public virtual DateTime DataValidade
        {
            get { return datavalidade; }
            set { datavalidade = value; }
        }

        public virtual string DescricaoAgregada
        {
            get
            {
                return @"Lote: " + this.Identificacao + " - Validade: " + this.DataValidade.ToString("dd/MM/yyyy")
                    + " - Aplicação: " + this.ItemVacina.Aplicacao.ToString();
            }
        }

        public LoteVacina()
        {
        }
    }
}
