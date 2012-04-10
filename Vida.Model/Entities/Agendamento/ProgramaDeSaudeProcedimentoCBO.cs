using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    public class ProgramaDeSaudeProcedimentoCBO
    {
        public ProgramaDeSaudeProcedimentoCBO()
        {

        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private bool ativo;

        public virtual bool Ativo
        {
            get { return ativo; }
            set { ativo = value; }
        }

        private ProgramaDeSaude programaDeSaude;

        public virtual ProgramaDeSaude ProgramaDeSaude
        {
            get { return programaDeSaude; }
            set { programaDeSaude = value; }
        }

        private Procedimento procedimento;

        public virtual Procedimento Procedimento
        {
            get { return procedimento; }
            set { procedimento = value; }
        }

        //public virtual string CodigoProcedimento
        //{
        //    get { return codigoProcedimento; }
        //    set { codigoProcedimento = value; }
        //}
        IList<CBO> cbos;

        public virtual IList<CBO> Cbos
        {
            get { return cbos; }
            set { cbos = value; }
        }

        //private string codigoCBO;

        //public virtual string CodigoCBO
        //{
        //    get { return codigoCBO; }
        //    set { codigoCBO = value; }
        //}

        //private CBO_Procedimento cbo_Procedimento;

        //public virtual CBO_Procedimento Cbo_Procedimento
        //{
        //    get { return cbo_Procedimento; }
        //    set { cbo_Procedimento = value; }
        //}

        

        //private string codigoCBO;

        //public virtual string CodigoCBO
        //{
        //    get { return codigoCBO; }
        //    set { codigoCBO = value; }
        //}
    }
}
