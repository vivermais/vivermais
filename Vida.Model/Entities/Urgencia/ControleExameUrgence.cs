using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControleExameUrgence
    {
        //public enum DescricaoTipoDocumento { ConsultaMedica = 'C', EvolucaoMedica = 'E' };

        private ProntuarioExame prontuarioexame;
        public virtual ProntuarioExame ProntuarioExame
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

        //public override bool Equals(object obj)
        //{
        //    return this.ProntuarioExame.Codigo == ((ControleExameUrgence)obj).ProntuarioExame.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 53;
        //}

        private long codigo;
        public virtual long Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

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

        public ControleExameUrgence()
        {
        }
    }
}
