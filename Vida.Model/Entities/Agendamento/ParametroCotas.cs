using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ParametroCotas
    {
        private int codigo;
        public virtual int Codigo 
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int tipoAgenda;
        public virtual int TipoAgenda
        {
          get { return tipoAgenda; }
          set { tipoAgenda = value; }
        }

        private string id_unidade;
        public virtual string ID_Unidade
        {
            get { return id_unidade; }
            set { id_unidade = value; }
        }

        private string id_Procedimento;
        public virtual string Id_Procedimento
        {
          get { return id_Procedimento; }
          set { id_Procedimento = value; }
        }

        private int id_Grupo;
        public virtual int Id_Grupo
        {
          get { return id_Grupo; }
          set { id_Grupo = value; }
        }

        private int quantidadeTotal;
        public virtual int QuantidadeTotal
        {
          get { return quantidadeTotal; }
          set { quantidadeTotal = value; }
        }

        private int quantidadeAgendada;
        public virtual int QuantidadeAgendada
        {
          get { return quantidadeAgendada; }
          set { quantidadeAgendada = value; }
        }

        private string competencia;
        public virtual string Competencia
        {
            get { return competencia; }
            set { competencia = value; }
        }

        public ParametroCotas()
        {}

    }
}
