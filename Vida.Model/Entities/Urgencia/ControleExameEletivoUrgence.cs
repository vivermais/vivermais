using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControleExameEletivoUrgence
    {
        //public enum DescricaoTipoDocumento { ConsultaMedica = 'C', EvolucaoMedica = 'E' };

        private ProntuarioExameEletivo prontuarioexame;
        public virtual ProntuarioExameEletivo ProntuarioExame
        {
            get { return prontuarioexame; }
            set { prontuarioexame = value; }
        }

        //private long documentocontrole;
        //public virtual long DocumentoControle
        //{
        //    get { return documentocontrole; }
        //    set { documentocontrole = value; }
        //}

        //private char tipodocumentocontrole;
        //public virtual char TipoDocumentoControle
        //{
        //    get { return tipodocumentocontrole; }
        //    set { tipodocumentocontrole = value; }
        //}

        private EvolucaoMedica evolucaomedica;
        public virtual EvolucaoMedica EvolucaoMedica
        {
            get { return evolucaomedica; }
            set { evolucaomedica = value; }
        }

        //private Prontuario atendimentomedico;
        //public virtual Prontuario AtendimentoMedico
        //{
        //    get { return atendimentomedico; }
        //    set { atendimentomedico = value; }
        //}

        //public override bool Equals(object obj)
        //{
        //    return this.ProntuarioExame.Codigo == ((ControleExameEletivoUrgence)obj).ProntuarioExame.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 71;
        //}

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        public ControleExameEletivoUrgence()
        {
        }
    }
}
