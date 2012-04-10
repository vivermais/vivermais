using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ProfissionalSolicitante
    {
        public ProfissionalSolicitante()
        {

        }

        private int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        private string nome;

        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        private string numeroRegistro;

        public virtual string NumeroRegistro
        {
            get { return numeroRegistro; }
            set { numeroRegistro = value; }
        }

        private OrgaoEmissor orgaoEmissorRegistro;

        public virtual OrgaoEmissor OrgaoEmissorRegistro
        {
            get { return orgaoEmissorRegistro; }
            set { orgaoEmissorRegistro = value; }
        }

        private UF ufProfssional;

        public virtual UF UfProfssional
        {
            get { return ufProfssional; }
            set { ufProfssional = value; }
        }

        private string status;

        public virtual string Status
        {
            get { return status; }
            set { status = value; }
        }

        //public override string OrgaoEmissorRegistro
        //{
        //    get { return this.OrgaoEmissorRegistro.Nome; }
        //}

    }
}
