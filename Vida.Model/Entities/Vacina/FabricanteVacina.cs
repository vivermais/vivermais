using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class FabricanteVacina
    {
        public static char ATIVO = 'A';
        public static char INATIVO = 'I';

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        char situacao;
        public virtual char Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        public virtual string SituacaoToString
        {
            get { return situacao == FabricanteVacina.ATIVO ? "ATIVO" : "INATIVO"; }
        }

        private string cnpj;
        public virtual string CNPJ
        {
            get { return cnpj; }
            set { cnpj = value; }
        }

        public FabricanteVacina()
        {
        }

        private bool ValidarCNPJ(string entradacnpj, int atualdigitoverificador)
        {
            int[] digitosverificadores = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            entradacnpj = entradacnpj.Replace(".", "").Replace("/", "").Replace("-", "");

            int soma = 0;
            int pos = 0;
            int digito;

            for (int verificador = atualdigitoverificador; verificador < digitosverificadores.Count(); verificador++)
            {
                soma += digitosverificadores[verificador] * int.Parse(entradacnpj[pos].ToString());
                pos++;
            }

            digito = soma % 11;
            digito = digito < 2 ? 0 : 11 - digito;

            if (digito != int.Parse(entradacnpj[pos].ToString()))
                return false;
            else
            {
                if (atualdigitoverificador != 0)
                    return this.ValidarCNPJ(entradacnpj, atualdigitoverificador - 1);
            }

            return true;
        }

        public virtual bool CNPJValido()
        {
            return this.ValidarCNPJ(this.CNPJ, 1);
        }

        public static IList<FabricanteVacina> RetornarFabricantes(IList<LoteVacina> lotes)
        {
            FabricanteComparable comparable = new FabricanteComparable();
            return (from lote in lotes
                    orderby lote.ItemVacina.FabricanteVacina.Nome
                    select lote.ItemVacina.FabricanteVacina).Distinct(comparable).ToList();
        }

        public static IList<LoteVacina> RetornarLotesFabricante(IList<LoteVacina> lotes, int co_fabricante)
        {
            return (from lote in lotes
                    orderby lote.Identificacao
                    where lote.ItemVacina.FabricanteVacina.Codigo == co_fabricante
                    select lote).ToList();
        }
    }

    public class FabricanteComparable : IEqualityComparer<FabricanteVacina>
    {
        public bool Equals(FabricanteVacina fab1, FabricanteVacina fab2)
        {
            return fab1.Codigo == fab2.Codigo;
        }

        public int GetHashCode(FabricanteVacina fab)
        {
            return fab.Codigo.GetHashCode();
        }
    }
}
