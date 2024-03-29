﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProntuarioExame
    {
        long codigo;

        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private Prontuario prontuario;

        public virtual Prontuario Prontuario
        {
            get { return prontuario; }
            set { prontuario = value; }
        }

        private Exame exame;

        public virtual Exame Exame
        {
            get { return exame; }
            set { exame = value; }
        }

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        private string profissional;

        public virtual string Profissional
        {
            get { return profissional; }
            set { profissional = value; }
        }

        private string cboprofissional;
        public virtual string CBOProfissional
        {
            get { return cboprofissional; }
            set { cboprofissional = value; }
        }

        private string resultado;

        public virtual string Resultado
        {
            get { return resultado; }
            set { resultado = value; }
        }

        private DateTime? dataresultado;

        public virtual DateTime? DataResultado
        {
            get { return dataresultado; }
            set { dataresultado = value; }
        }

        private string profissionalresultado;

        public virtual string ProfissionalResultado
        {
            get { return profissionalresultado; }
            set { profissionalresultado = value; }
        }

        public override bool Equals(object obj)
        {
            return this.Prontuario.Codigo == ((ProntuarioExame)obj).Prontuario.Codigo &&
                   this.Exame.Codigo == ((ProntuarioExame)obj).Exame.Codigo;
        }

        private DateTime ? dataconfirmacaobaixa;
        public virtual DateTime? DataConfirmacaoBaixa
        {
            get { return dataconfirmacaobaixa; }
            set { dataconfirmacaobaixa = value; }
        }

        private int usuarioconfirmacaobaixa;
        public virtual int UsuarioConfirmacaoBaixa
        {
            get { return usuarioconfirmacaobaixa; }
            set { usuarioconfirmacaobaixa = value; }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() * 93;
        }

        public ProntuarioExame()
        {
        }

        public virtual string NomeExame
        {
            get { return this.exame.Descricao; }
        }

        public virtual string DataResultadoToString
        {
            get
            {
                if (this.dataresultado == null)
                    return "-";
                else
                    return this.DataResultado.Value.ToString();
            }
        }

        public virtual string NomePaciente
        {
            get
            {
                string nomepaciente = this.Prontuario.Paciente.Nome;
                return string.IsNullOrEmpty(nomepaciente) ? "Não Identificado" : nomepaciente;
            }
        }

        public virtual string ResultadoToString
        {
            get
            {
                if (this.resultado == null)
                    return "-";
                else
                    return this.resultado;
            }
        }
    }
}
