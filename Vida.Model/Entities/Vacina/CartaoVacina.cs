using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class CartaoVacina
    {
        public static int CRIANCA = 1;
        public static int ADULTOIDOSO = 2;
        public static int ADOLESCENTE = 3;
        public static int HISTORICO = 4;

        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        Paciente paciente;

        public virtual Paciente Paciente
        {
            get { return paciente; }
            set { paciente = value; }
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

        DateTime? dataaplicacao;

        public virtual DateTime? DataAplicacao
        {
            get { return dataaplicacao; }
            set { dataaplicacao = value; }
        }

        public virtual string DataAplicacaoToString
        {
            get { return this.dataaplicacao != null ? dataaplicacao.Value.ToShortDateString() : string.Empty; }
        }

        SalaVacina salavacina;

        public virtual SalaVacina SalaVacina
        {
            get { return salavacina; }
            set { salavacina = value; }
        }

        DispensacaoVacina dispensacaovacina;
        public virtual DispensacaoVacina DispensacaoVacina
        {
            get { return dispensacaovacina; }
            set { dispensacaovacina = value; }
        }

        ItemDispensacaoVacina itemdispensacao;
        public virtual ItemDispensacaoVacina ItemDispensacao
        {
            get { return itemdispensacao; }
            set { itemdispensacao = value; }
        }

        public virtual string AbreviacaoVacina
        {
            get { return Vacina.NomeAbreviacao; }
        }

        /// <summary>
        /// Essa propriedade é preenchida qnd o cartão de vacina é atualizado pela dispensação
        /// </summary>
        LoteVacina lotevacina;

        public virtual LoteVacina LoteVacina
        {
            get { return lotevacina; }
            set { lotevacina = value; }
        }

        /// <summary>
        /// Esta propriedade é preenchida quando o cartão de vacina é atualizado manualmente
        /// </summary>
        string lote;

        public virtual string Lote
        {
            get { return lote; }
            set { lote = value; }
        }

        private DateTime? validadelote;

        public virtual DateTime? ValidadeLote
        {
            get { return validadelote; }
            set { validadelote = value; }
        }

        /// <summary>
        /// O local onde o paciente tomou a vacina, caso fora da rede pública
        /// </summary>
        string local;

        public virtual string Local
        {
            get { return local; }
            set { local = value; }
        }

        /// <summary>
        /// O motivo pelo qual o paciente tomou a vacina. Ex. campanha, viagem, rotina, epidemia...
        /// somente para vacinas fora da rede
        /// </summary>
        string motivo;

        public virtual string Motivo
        {
            get { return motivo; }
            set { motivo = value; }
        }

        //public virtual string AbreviacaoVacina 
        //{
        //    get { return Vacina.NomeAbreviacao; }
        //}

        public virtual string DoseAbreviada
        {
            get { return this.DoseVacina == null ? string.Empty : this.DoseVacina.Abreviacao; }
        }

        public virtual string CNESUnidade
        {
            get { return this.SalaVacina == null ? string.Empty : this.SalaVacina.EstabelecimentoSaude.CNES; }
        }

        public virtual string NomeUnidade
        {
            get { return this.SalaVacina == null ? string.Empty : this.SalaVacina.EstabelecimentoSaude.NomeFantasia; }
        }

        public virtual string SiglaEstabelecimento
        {
            get { return this.SalaVacina == null ? string.Empty : this.SalaVacina.EstabelecimentoSaude.SiglaEstabelecimento; }
        }

        //public virtual string LoteToString
        //{
        //    get { return this.LoteVacina == null ? this.Lote : string.Empty; }
        //}

        //public virtual string ValidadeLoteToString
        //{
        //    get { return this.LoteVacina == null ? "" : ""; }
        //}

        public CartaoVacina()
        {
        }

        public override bool Equals(object obj)
        {
            return this.Codigo == ((CartaoVacina)obj).Codigo ? true : false;
        }

        public override int GetHashCode()
        {
            return 47 * base.GetHashCode();
        }

        public virtual int CodigoVacina
        {
            get { return this.Vacina.Codigo; }
        }

        public virtual int CodigoDoseVacina
        {
            get { return this.DoseVacina.Codigo; }
        }

        /// <summary>
        /// Este campo é utilizado para carregar a data da proxima dose de uma vacina
        /// Não é carregado do banco
        /// </summary>
        string proximadose = "";

        public virtual string ProximaDose
        {
            get { return proximadose; }
            set { proximadose = value; }
        }

        //MÉTODOS UTILIZADOS PARA IMPRESSÃO DO CARTÃO DE VACINA
        public virtual string ProximaDoseVacinaImpressaoCartao
        {
            get { return ProximaDose; }
        }

        public virtual string VacinaImpressaoCartao
        {
            get
            {
                return this.Vacina.Nome;
            }
        }

        public virtual string DoseVacinaImpressaoCartao
        {
            get
            {
                return this.DoseVacina.Abreviacao;
            }
        }

        public virtual string LoteVacinaImpressaoCartao
        {
            get
            {
                return this.Lote;
            }
        }

        public virtual string DataAplicacaoVacinaImpressaoCartao
        {
            get
            {
                if (DataAplicacao.HasValue)
                    return DataAplicacao.Value.ToString("dd/MM/yyyy");

                return string.Empty;
            }
        }

        public virtual string UnidadeVacinaImpressaoCartao
        {
            get
            {
                return this.Local;
            }
        }

        public virtual string ValidadeLoteImpressaoCartao
        {
            get
            {
                if (this.ValidadeLote.HasValue)
                    return this.ValidadeLote.Value.ToString("dd/MM/yyyy");

                return string.Empty;
            }
        }

        public virtual string CNESImpressaoCartao
        {
            get
            {
                if (this.ItemDispensacao != null)
                    return this.ItemDispensacao.Dispensacao.Sala.EstabelecimentoSaude.CNES;

                return string.Empty;
            }
        }

        public static string RetornaProximaDose(IList<ParametrizacaoVacina> parametrizacoes, DateTime? dataaplicada)
        {
            if (parametrizacoes.Count != 0)
            {
                ParametrizacaoVacina parametrizacao = parametrizacoes.Where(p =>
                    p.ItemDoseVacina.DoseVacina.RetornarPesoDose() == int.Parse(parametrizacoes.Max(p2 => p2.ItemDoseVacina.DoseVacina.RetornarPesoDose()).ToString())).FirstOrDefault();

                if (parametrizacao.UnidadeTempoInicial == (int)ParametrizacaoVacina.UNIDADE_TEMPO.HORAS)
                    return dataaplicada.Value.AddHours(parametrizacao.FaixaEtariaInicial).ToString("dd/MM/yyyy");
                else if (parametrizacao.UnidadeTempoInicial == (int)ParametrizacaoVacina.UNIDADE_TEMPO.DIAS)
                    return dataaplicada.Value.AddDays(parametrizacao.FaixaEtariaInicial).ToString("dd/MM/yyyy");
                else if (parametrizacao.UnidadeTempoInicial == (int)ParametrizacaoVacina.UNIDADE_TEMPO.MESES)
                    return dataaplicada.Value.AddMonths(Convert.ToInt32(parametrizacao.FaixaEtariaInicial)).ToString("dd/MM/yyyy");
                else if (parametrizacao.UnidadeTempoInicial == (int)ParametrizacaoVacina.UNIDADE_TEMPO.ANOS)
                    return dataaplicada.Value.AddYears(Convert.ToInt32(parametrizacao.FaixaEtariaInicial)).ToString("dd/MM/yyyy");
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }

        public static IList<CartaoVacina> SubstituirCartao(IList<CartaoVacina> cartoes, CartaoVacina cartao)
        {
            var cartaopreenchido = cartoes.Select((Cartao, index) => new { index, Cartao }).Where
                    (p => p.Cartao.CodigoVacina == cartao.CodigoVacina && p.Cartao.CodigoDoseVacina == cartao.CodigoDoseVacina
                   && !p.Cartao.DataAplicacao.HasValue).FirstOrDefault();

            if (cartaopreenchido != null)
                cartoes[cartaopreenchido.index] = cartao;

            return cartoes;
        }

        public static int RetornaIndexCartao(IList<CartaoVacina> cartoes, long co_cartao)
        {
            var cartaopreenchido = cartoes.Select((Cartao, index) => new { index, Cartao }).Where
                (p => p.Cartao.Codigo == co_cartao).FirstOrDefault();

            return cartaopreenchido.index;
        }

        public static IList<CartaoVacina> RemoverCartaoCalendario(IList<CartaoVacina> cartoes, CartaoVacina cartao)
        {
            var cartaopreenchido = cartoes.Select((Cartao, index) => new { index, Cartao }).Where
                    (p => p.Cartao.CodigoVacina == cartao.CodigoVacina && p.Cartao.CodigoDoseVacina == cartao.CodigoDoseVacina
                   && p.Cartao.DataAplicacao.HasValue && p.Cartao.DataAplicacao.Value.ToString("dd/MM/yyyy").Equals(cartao.DataAplicacao.Value.ToString("dd/MM/yyyy"))).FirstOrDefault();

            if (cartaopreenchido != null)
            {
                CartaoVacina cremover = new CartaoVacina();
                cremover.Vacina = cartaopreenchido.Cartao.Vacina;
                cremover.DoseVacina = cartaopreenchido.Cartao.DoseVacina;
                cartoes[cartaopreenchido.index] = cremover;
            }

            return cartoes;
        }

        //private string informacoescartao;
        //public virtual string InformcaoesCartao
        //{
        //    get { return informacoescartao; }
        //    set { informacoescartao = value; }
        //}
    }
}
