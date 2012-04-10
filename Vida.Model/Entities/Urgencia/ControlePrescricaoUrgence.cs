using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ControlePrescricaoUrgence
    {
        //public enum DescricaoTipoDocumento { ConsultaMedica = 'C', EvolucaoMedica = 'E' };

        private Prescricao prescricao;
        public virtual Prescricao Prescricao
        {
            get { return prescricao; }
            set { prescricao = value; }
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

        public ControlePrescricaoUrgence()
        {
        }

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

        //public override bool Equals(object obj)
        //{
        //    return this.Prescricao.Codigo == ((ControlePrescricaoUrgence)obj).Prescricao.Codigo;
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode() * 39;
        //}
    }
}
