using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ParametrizacaoFPO
    {
        public enum TiposDeParametrizacao { CNES = '1', PROCEDIMENTO = '2' }


        private int codigo;
        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private int valorPrestador;

        public virtual int ValorPrestador
        {
            get { return valorPrestador; }
            set { valorPrestador = value; }
        }

        private int valorSolicitante;

        public virtual int ValorSolicitante
        {
            get { return valorSolicitante; }
            set { valorSolicitante = value; }
        }

        private string cnes;

        public virtual string Cnes
        {
            get { return cnes; }
            set { cnes = value; }
        }


        private string id_Procedimento;

        public virtual string Id_Procedimento
        {
            get { return id_Procedimento; }
            set { id_Procedimento = value; }
        }

        private char tipoParametrizacao;

        public virtual char TipoParametrizacao
        {
            get { return tipoParametrizacao; }
            set { tipoParametrizacao = value; }
        }

        public ParametrizacaoFPO()
        {

        }
    }
}
