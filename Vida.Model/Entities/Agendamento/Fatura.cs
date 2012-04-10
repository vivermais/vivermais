using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class Fatura
    {
        public static char FATURA = 'F';
        public static char PREVIA = 'P';

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private EstabelecimentoSaude id_Unidade;

        public virtual EstabelecimentoSaude Id_Unidade
        {
            get { return id_Unidade; }
            set { id_Unidade = value; }
        }

        private int competencia;
        public virtual int Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        private char tipo;

        public virtual char Tipo
        {
            get { return tipo; }
            set { tipo = value; }
        }

        private Usuario usuario;

        public virtual Usuario Usuario
        {
            get { return usuario; }
            set { usuario = value; }
        }

        //private string modo;
        //public virtual string Modo
        //{
        //    get { return modo; }
        //    set { modo = value; }
        //}

        //private string situacao;
        //public virtual string Situacao
        //{
        //    get { return situacao; }
        //    set { situacao = value; }
        //}

        private DateTime data;

        public virtual DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public Fatura() 
        {
        }
    }
}
