﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class EstoqueVacina
    {
        LoteVacina lotevacina;
        public virtual LoteVacina Lote
        {
            get { return lotevacina; }
            set { lotevacina = value; }
        }

        SalaVacina sala;
        public virtual SalaVacina Sala
        {
            get { return sala; }
            set { sala = value; }
        }

        int quantidadeestoque;
        public virtual int QuantidadeEstoque
        {
            get { return quantidadeestoque; }
            set { quantidadeestoque = value; }
        }

        public EstoqueVacina()
        {
        }

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public virtual string NomeVacina
        {
            get { return Lote.ItemVacina.Vacina.Nome; }
        }

        public virtual string NomeFabricanteVacina
        {
            get { return Lote.ItemVacina.FabricanteVacina.Nome; }
        }

        public virtual int QtdAplicacaoVacina
        {
            get { return Lote.ItemVacina.Aplicacao; }
        }

        public virtual string IdentificacaoLote
        {
            get { return Lote.Identificacao; }
        }

        public virtual DateTime ValidadeLote
        {
            get { return Lote.DataValidade; }
        }

        //public override bool Equals(object obj)
        //{
        //    return this.Lote.Codigo == ((EstoqueVacina)obj).Lote.Codigo &&
        //           this.Sala.Codigo == ((EstoqueVacina)obj).Sala.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 11;
        //}
    }
}