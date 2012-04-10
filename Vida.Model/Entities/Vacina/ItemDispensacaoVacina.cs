using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemDispensacaoVacina
    {
        public static char AMPOLA_ABERTA = 'S';
        public static char AMPOLA_FECHADA = 'N';

        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        DispensacaoVacina dispensacao;

        public virtual DispensacaoVacina Dispensacao
        {
            get { return dispensacao; }
            set { dispensacao = value; }
        }

        LoteVacina lote;

        public virtual LoteVacina Lote
        {
            get { return lote; }
            set { lote = value; }
        }

        Estrategia estrategia;

        public virtual Estrategia Estrategia
        {
            get { return estrategia; }
            set { estrategia = value; }
        }

        DoseVacina dose;

        public virtual DoseVacina Dose
        {
            get { return dose; }
            set { dose = value; }
        }

        public virtual string DescricaoDose
        {
            get
            {
                return Dose.Descricao;
            }
            set { }
        }

        char aberturaAmpola;

        public virtual char AberturaAmpola
        {
            get { return aberturaAmpola; }
            set { aberturaAmpola = value; }
        }

        public virtual string DescricaoAberturaAmpola
        {
            get
            {
                return AberturaAmpola.Equals('S') ? "Sim" : "Não";
            }
            set
            {
            }
        }

        public virtual string Vacina
        {
            get
            {
                return Lote.ItemVacina.Vacina.Nome;
            }
            set { }
        }

        public virtual string DescricaoLote
        {
            get
            {
                return Lote.Identificacao;
            }
            set
            {
            }
        }

        public virtual string VacinaFabricante
        {
            get { return Lote.ItemVacina.FabricanteVacina.Nome; }
        }

        public virtual int VacinaAplicacao
        {
            get { return this.Lote.ItemVacina.Aplicacao; }
        }

        public virtual DateTime ValidadeLote
        {
            get { return this.Lote.DataValidade; }
        }

        public ItemDispensacaoVacina()
        {
        }
    }
}
