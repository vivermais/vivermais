using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametrizacaoVacina
    {
        public static char POR_EVENTO = 'E';
        public static char POR_FAIXA_ETARIA = 'F';

        public enum UNIDADE_TEMPO: int { HORAS = 1, DIAS = 2, MESES = 3, ANOS = 4 };

        int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        float faixaetariainicial;

        /// <summary>
        /// Utilizada tanto para indicar faixa etária 
        /// quanto o período de tempo do após
        /// </summary>
        public virtual float FaixaEtariaInicial
        {
            get { return faixaetariainicial; }
            set { faixaetariainicial = value; }
        }

        float faixaetariafinal;
        public virtual float FaixaEtariaFinal
        {
            get { return faixaetariafinal; }
            set { faixaetariafinal = value; }
        }

        int unidadetempoinicial;

        /// <summary>
        /// Utilizada tanto para indicar faixa etária 
        /// quanto o período de tempo do após
        /// </summary>
        public virtual int UnidadeTempoInicial
        {
            get { return unidadetempoinicial; }
            set { unidadetempoinicial = value; }
        }

        int unidadetempofinal;
        public virtual int UnidadeTempoFinal
        {
            get { return unidadetempofinal; }
            set { unidadetempofinal = value; }
        }

        ItemDoseVacina itemdosevacina;
        public virtual ItemDoseVacina ItemDoseVacina
        {
            get { return itemdosevacina; }
            set { itemdosevacina = value; }
        }

        ItemDoseVacina proximadose;

        /// <summary>
        /// Indica a próxima dose para o evento após
        /// </summary>
        public virtual ItemDoseVacina ProximaDose
        {
            get { return proximadose; }
            set { proximadose = value; }
        }

        //OperadorVacina operador;

        //public virtual OperadorVacina Operador
        //{
        //    get { return operador; }
        //    set { operador = value; }
        //}

        PropriedadeVacina propriedade;

        public virtual PropriedadeVacina Propriedade
        {
            get { return propriedade; }
            set { propriedade = value; }
        }

        //string valor;

        //public virtual string Valor
        //{
        //    get { return valor; }
        //    set { valor = value; }
        //}

        char tipo;

        /// <summary>
        /// Testa se a parametrização se refere a um período de tempo 
        /// ou um após
        /// E - Evento (após, a partir de, etc...)
        /// F - Faixa Etária
        /// </summary>
        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        //char unidadetempo;

        //public virtual char UnidadeTempo
        //{
        //    get { return unidadetempo; }
        //    set { unidadetempo = value; }
        //}

        //public virtual string UnidadeTempoToString
        //{
        //    get
        //    {
        //        switch (UnidadeTempo) 
        //        {
        //            case 'D': return "dia(s)";
        //            case 'M': return "mes(es)";
        //            case 'A': return "ano(s)";
        //            default : return "";
        //        }
        //    }
        //}

        public virtual string FaixaEtariaToString
        {
            get
            {
                if (UnidadeTempoInicial == UnidadeTempoFinal && FaixaEtariaInicial == FaixaEtariaFinal)
                    return faixaetariainicial.ToString() + " " + UnidadeTempoInicialToString;
                else
                    return faixaetariainicial.ToString() + " "
                    + (UnidadeTempoInicial == UnidadeTempoFinal ? "" : UnidadeTempoInicialToString) + " a "
                    + faixaetariafinal.ToString() + " " + UnidadeTempoFinalToString;
            }
        }

        public virtual string UnidadeTempoInicialToString
        {
            get
            {
                switch (UnidadeTempoInicial)
                {
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.HORAS: { return this.FaixaEtariaInicial != 1 ? "horas" : "hora"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.DIAS: { return this.FaixaEtariaInicial != 1 ? "dias" : "dia"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.MESES: { return this.FaixaEtariaInicial != 1 ? "meses" : "mês"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS: { return this.FaixaEtariaInicial != 1 ? "anos" : "ano"; }
                    default: return string.Empty;
                }
            }
        }

        public virtual string UnidadeTempoFinalToString
        {
            get
            {
                switch (UnidadeTempoFinal)
                {
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.HORAS: { return this.FaixaEtariaFinal != 1 ? "horas" : "hora"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.DIAS: { return this.FaixaEtariaFinal != 1 ? "dias" : "dia"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.MESES: { return this.FaixaEtariaFinal != 1 ? "meses" : "mês"; }
                    case (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS: { return this.FaixaEtariaFinal != 1 ? "anos" : "ano"; }
                    default: return string.Empty;
                }
            }
        }

        public override string ToString()
        {
            if (this.Tipo == ParametrizacaoVacina.POR_EVENTO)
                return ProximaDose.DoseVacina + " após " + this.FaixaEtariaInicial + " " + UnidadeTempoInicialToString;
            else
                return FaixaEtariaToString;

        }

        public virtual string DescricaoParametrizacao
        {
            get { return this.ToString(); }
        }

        //public virtual string DescricaoParametrizacao
        //{
        //    get
        //    {
        //        if (this.Tipo == ParametrizacaoVacina.POR_EVENTO)
        //            return ProximaDose.DoseVacina + " após " + this.FaixaEtariaInicial + " " + UnidadeTempoInicialToString;

        //        return FaixaEtariaToString;
        //    }
        //}

        /// <summary>
        /// Converte um valor de uma unidade de tempo (origem) para outra (destino)
        /// </summary>
        /// <param name="valor"></param>
        /// <param name="unidadeorigem"></param>
        /// <param name="unidadedestino"></param>
        /// <returns></returns>
        public static float ConverteUnidadeTempo(float valor, int unidadeorigem, int unidadedestino)
        {
            if (unidadeorigem == unidadedestino)
                return valor;

            float temp = 0;

            if (unidadeorigem == (int)ParametrizacaoVacina.UNIDADE_TEMPO.HORAS)
                temp = valor;
            else if (unidadeorigem == (int)ParametrizacaoVacina.UNIDADE_TEMPO.DIAS)
                temp = valor * 24;
            else if (unidadeorigem == (int)ParametrizacaoVacina.UNIDADE_TEMPO.MESES)
                temp = valor * 24 * 30;
            else if (unidadeorigem == (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS)
                temp = valor * 24 * 365;

            if (unidadedestino == (int)ParametrizacaoVacina.UNIDADE_TEMPO.HORAS)
                return temp;
            else if (unidadedestino == (int)ParametrizacaoVacina.UNIDADE_TEMPO.DIAS)
                return temp / 24;
            else if (unidadedestino == (int)ParametrizacaoVacina.UNIDADE_TEMPO.MESES)
                return (temp / 24) / 30;
            else if (unidadedestino == (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS)
                return (temp / 24) / 365;

            return 0;
        }

        public static IList<ItemDoseVacina> ProximasDosesParametrizacao(ItemDoseVacina dose, IList<ItemDoseVacina> itens)
        {
            if (dose.DoseVacina.Codigo == DoseVacina.UNICA)
            {
                return (from item in itens
                        where item.DoseVacina.Codigo == DoseVacina.PRIMEIRO_REFORCO
                        || item.DoseVacina.Codigo == DoseVacina.SEGUNDO_REFORCO
                        || item.DoseVacina.Codigo == DoseVacina.REFORCO
                        select item).ToList();
            }
            else if (dose.DoseVacina.Codigo == DoseVacina.REFORCO)
            {
                return (from item in itens
                        where item.DoseVacina.Codigo == DoseVacina.REFORCO
                        select item).ToList();
            }
            else if (dose.DoseVacina.Codigo == DoseVacina.PRIMEIRO_REFORCO)
            {
                return (from item in itens
                        where item.DoseVacina.Codigo == DoseVacina.SEGUNDO_REFORCO
                        || item.DoseVacina.Codigo == DoseVacina.REFORCO
                        select item).ToList();
            }
            else if (dose.DoseVacina.Codigo == DoseVacina.SEGUNDO_REFORCO)
            {
                return (from item in itens
                        where item.DoseVacina.Codigo == DoseVacina.REFORCO
                        select item).ToList();
            }
            else if (dose.DoseVacina.NumeracaoDose <= DoseVacina.ULTIMA_DOSE_NORMAL)
            {
                return (from item in itens
                        where item.DoseVacina.RetornarPesoDose() == (dose.DoseVacina.RetornarPesoDose() + 1)
                        || (ParametrizacaoVacina.eUltimaDose(dose.DoseVacina,itens) == true && (item.DoseVacina.Codigo == DoseVacina.REFORCO
                        || item.DoseVacina.Codigo == DoseVacina.PRIMEIRO_REFORCO
                        || item.DoseVacina.Codigo == DoseVacina.SEGUNDO_REFORCO))
                        select item).ToList();
            }

            return new List<ItemDoseVacina>();
        }

        public static bool eUltimaDose(DoseVacina dose, IList<ItemDoseVacina> itens)
        {
            if (int.Parse(itens.Where(p => p.DoseVacina.RetornarPesoDose() <= DoseVacina.ULTIMA_DOSE_NORMAL).Max(p => p.DoseVacina.RetornarPesoDose()).ToString()) == dose.RetornarPesoDose())
                return true;

            return false;
        }

        public ParametrizacaoVacina()
        {

        }
    }
}
