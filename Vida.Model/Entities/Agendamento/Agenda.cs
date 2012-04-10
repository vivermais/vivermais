using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Agenda
    {
        public virtual String Dia
        {
            get { return GenericsFunctions.DiaDaSemana(this.Data.DayOfWeek).ToUpper().Substring(0, 3); }
        }

        public virtual String Bairro
        {

            get
            {
                if (this.Estabelecimento != null && this.Estabelecimento.Bairro != null)
                    return this.estabelecimento.Bairro.Nome;
                else
                    return "-";
                    //String.IsNullOrEmpty(this.Estabelecimento.Bairro) ? "-" : 
            }
        }

        public virtual String Horario
        {
            get { return this.Hora_Inicial + "-" + this.Hora_Final; }
        }

        public virtual String NomeProfissional
        {
            get { return this.ID_Profissional.Nome; }
        }

        public virtual int QuantidadeDisponivel
        {
            get { return this.Quantidade - this.QuantidadeAgendada; }
        }

        public virtual String TurnoToString
        {
            get 
            {
                if (this.Turno == "M")
                    return "Manhã";
                else if (this.Turno == "T")
                    return "Tarde";
                else if (this.Turno == "N")
                    return "Noite";
                else
                    return this.Turno;
            }
        }

        public virtual String BloqueadaToString
        {
            get 
            {
                if (this.Bloqueada)
                    return "Sim";
                else 
                    return "Não";
            }
        }

        public virtual String StatusToString
        {
            get 
            {
                if (this.Publicada)
                    return "Publicada";
                else
                    return "Não Publicada";
            }
        }

        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private ViverMais.Model.EstabelecimentoSaude estabelecimento;

        public virtual ViverMais.Model.EstabelecimentoSaude Estabelecimento
        {
            get { return estabelecimento; }
            set { estabelecimento = value; }
        }


        private CBO cbo;

        public virtual CBO Cbo
        {
            get { return cbo; }
            set { cbo = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        

        private SubGrupo subGrupo;

        public virtual SubGrupo SubGrupo
        {
            get { return subGrupo; }
            set { subGrupo = value; }
        }

        private Profissional id_profissional;
        public virtual Profissional ID_Profissional
        {
            get { return id_profissional; }
            set { id_profissional = value; }
        }

        public virtual string DataAgendaFormatada
        {
            get { return this.Data.ToString("dd/MM/yyyy"); }
        }
        private DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private int competencia;
        public virtual int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        private string turno;
        public virtual string Turno
        {
            get { return turno; }
            set { turno = value; }
        }

        private string hora_inicial;
        public virtual string Hora_Inicial
        {
            get { return hora_inicial; }
            set { hora_inicial = value; }
        }

        private string hora_final;
        public virtual string Hora_Final
        {
            get { return hora_final; }
            set { hora_final = value; }
        }

        private int quantidade;
        public virtual int Quantidade
        {
            get { return quantidade; }
            set { quantidade = value; }
        }

        private int quantidadeAgendada;

        public virtual int QuantidadeAgendada
        {
            get { return quantidadeAgendada; }
            set { quantidadeAgendada = value; }
        }

        private bool bloqueada;

        public virtual bool Bloqueada
        {
            get { return bloqueada; }
            set { bloqueada = value; }
        }

        private bool publicada;

        public virtual bool Publicada
        {
            get { return publicada; }
            set { publicada = value; }
        }

        private string motivoBloqueio;
        public virtual string MotivoBloqueio
        {
            get { return motivoBloqueio; }
            set { motivoBloqueio = value; }
        }

        private string observacaoBloqueio;
        public virtual string ObservacaoBloqueio
        {
            get { return observacaoBloqueio; }
            set { observacaoBloqueio = value; }
        }



        public Agenda()
        {
        }
    }
}
