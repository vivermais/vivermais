using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Prontuario
    {
        long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        int numero;
        public virtual int Numero
        {
            get { return numero; }
            set { numero = value; }
        }

        private bool desacordado;
        public virtual bool Desacordado
        {
            get { return desacordado; }
            set { desacordado = value; }
        }

        public virtual string NumeroToString
        {
            get
            {
                if (this.numero.ToString().Length == 9)
                    return this.numero.ToString();
                else
                    return "0" + this.numero.ToString();
            }
        }

        DateTime data;
        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        int idade;
        public virtual int Idade
        {
            get { return idade; }
            set { idade = value; }
        }

        FaixaEtaria faixaetaria;
        public virtual FaixaEtaria FaixaEtaria
        {
            get { return faixaetaria; }
            set { faixaetaria = value; }
        }

        private PacienteUrgence paciente;
        public virtual PacienteUrgence Paciente
        {
            get { return paciente; }
            set { paciente = value; }
        }

        public virtual string NomePacienteToString
        {
            get
            {
                if (this.Paciente.PacienteViverMais != null)
                    return this.Paciente.PacienteViverMais.Nome;

                if (!string.IsNullOrEmpty(Paciente.Nome))
                    return this.Paciente.Nome;

                return "Não Identificado";
            }
        }

        private string codigounidade;
        public virtual string CodigoUnidade
        {
            get { return codigounidade; }
            set { codigounidade = value; }
        }

        //private EstabelecimentoSaude unidade;
        //public virtual EstabelecimentoSaude Unidade
        //{
        //    get { return unidade; }
        //    set { unidade = value; }
        //}

        //float peso;
        //public virtual float Peso
        //{
        //    get { return peso; }
        //    set { peso = value; }
        //}

        SituacaoAtendimento situacao;
        public virtual SituacaoAtendimento Situacao
        {
            get { return situacao; }
            set { situacao = value; }
        }

        private ClassificacaoRisco classificacaorisco;
        /// <summary>
        /// Classificação de Risco Atual do Paciente neste prontuário
        /// </summary>
        public virtual ClassificacaoRisco ClassificacaoRisco
        {
            get { return classificacaorisco; }
            set { classificacaorisco = value; }
        }

        private char? classificacaoacolhimento;
        public virtual char? ClassificacaoAcolhimento
        {
            get { return classificacaoacolhimento; }
            set { classificacaoacolhimento = value; }
        }

        DateTime? dataacolhimento;
        public virtual DateTime? DataAcolhimento
        {
            get { return dataacolhimento; }
            set { dataacolhimento = value; }
        }

        DateTime? dataconsultamedica;
        public virtual DateTime? DataConsultaMedica
        {
            get { return dataconsultamedica; }
            set { dataconsultamedica = value; }
        }

        DateTime? datafinalizacao;
        public virtual DateTime? DataFinalizacao
        {
            get { return datafinalizacao; }
            set { datafinalizacao = value; }
        }

        string sumarioalta;
        public virtual string SumarioAlta
        {
            get { return sumarioalta; }
            set { sumarioalta = value; }
        }

        public Prontuario()
        {
        }

        public virtual string PacienteDescricao
        {
            get
            {
                string descricao = this.Paciente.Descricao;

                if (string.IsNullOrEmpty(descricao))
                    return " - ";

                return descricao;
            }
        }

        public virtual string ImagemClassificacaoRisco
        {
            get
            {
                return this.ClassificacaoRisco.Imagem;
            }
        }

        public virtual string SituacaoEntradaPaciente
        {
            get
            {
                if (this.Desacordado)
                    return "Desacordado";
                else if (!string.IsNullOrEmpty(this.Paciente.Descricao))
                    return "Desorientado";
                
                return "Consciente";
            }
        }
            
        private CBO especialidadeatendimento;
        public virtual CBO EspecialidadeAtendimento
        {
            get { return especialidadeatendimento; }
            set { especialidadeatendimento = value; }
        }

        public override string ToString()
        {
            return this.NumeroToString;
        }

        private int? codigounidadetransferencia;
        public virtual int? CodigoUnidadeTransferencia
        {
            get { return codigounidadetransferencia; }
            set { codigounidadetransferencia = value; }
        }

        /******MÉTODOS PARA TEMPO DE ESPERA******/
        public virtual int EsperaFilaAtendimento
        {
            get
            {
                if (this.Desacordado)
                    return this.DiferencaMinutos(DateTime.Now, this.data);

                return this.DiferencaMinutos(DateTime.Now, this.dataacolhimento.Value);
            }
        }

        public virtual string EsperaAtualFilaAtendimento
        {
            get
            {
                if (this.Desacordado)
                    return this.TempoEspera(DateTime.Now, this.data);
                
                return this.TempoEspera(DateTime.Now, this.dataacolhimento.Value);
            }
        }

        public virtual string TempoAcolhimento
        {
            get
            {
                if (this.dataacolhimento.HasValue)
                    return this.TempoEspera(this.dataacolhimento.Value, this.data);

                return " - ";
            }
        }

        public virtual string TempoConsultaMedica
        {
            get
            {
                if (this.dataacolhimento.HasValue && this.dataconsultamedica.HasValue)
                    return this.TempoEspera(this.dataconsultamedica.Value, this.dataacolhimento.Value);

                return " - ";
            }
        }

        public virtual string TempoFinalizacao
        {
            get
            {
                if (this.dataconsultamedica.HasValue && this.datafinalizacao.HasValue)
                    return this.TempoEspera(this.datafinalizacao.Value, this.dataconsultamedica.Value);

                return " - ";
            }
        }

        public virtual string TempoUniaoAcolhimentoConsultaFinalizacao
        {
            get
            {
                int totalminutos = -1;

                if (this.dataacolhimento.HasValue)
                    totalminutos += this.DiferencaMinutos(this.dataacolhimento.Value, this.data);

                if (this.dataacolhimento.HasValue && this.dataconsultamedica.HasValue)
                    totalminutos += this.DiferencaMinutos(this.dataconsultamedica.Value, this.dataacolhimento.Value);

                if (this.dataconsultamedica.HasValue && this.datafinalizacao.HasValue)
                    totalminutos += this.DiferencaMinutos(this.datafinalizacao.Value, this.dataconsultamedica.Value);

                if (totalminutos > -1)
                    return this.TempoEspera(totalminutos);

                return " - ";
            }
        }

        private string TempoEspera(int totalminutos)
        {
            int horas = totalminutos / 60;
            int minutos = totalminutos % 60;
            int dias = horas / 24;

            StringBuilder espera = new StringBuilder();

            if (dias > 0)
            {
                horas = horas % 24;
                espera.Append(dias.ToString());
                espera.Append(" dia(s), ");
            }

            espera.Append(horas.ToString());
            espera.Append(" hora(s) e ");
            espera.Append(minutos.ToString());
            espera.Append(" minuto(s)");

            return espera.ToString();
        }

        private int DiferencaMinutos(DateTime minuendo, DateTime subtraendo)
        {
            return int.Parse(Math.Round(minuendo.Subtract(subtraendo).TotalMinutes).ToString());
        }

        private string TempoEspera(DateTime minuendo, DateTime subtraendo)
        {
            return this.TempoEspera(this.DiferencaMinutos(minuendo, subtraendo));
        }

        private string senhaacolhimento;
        public virtual string SenhaAcolhimento
        {
            get { return senhaacolhimento; }
            set { senhaacolhimento = value; }
        }

        private string senhaatendimento;
        public virtual string SenhaAtendimento
        {
            get { return senhaatendimento; }
            set { senhaatendimento = value; }
        }
    }
}
