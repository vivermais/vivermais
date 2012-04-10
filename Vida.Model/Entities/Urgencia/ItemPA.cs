using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class ItemPA
    {
        public static char ATIVO = 'A';
        public static char INATIVO = 'I';

        int codigo;

        public virtual int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        string nome;
        public virtual string Nome
        {
            get { return nome; }
            set { nome = value; }
        }

        string codigosigtap;

        public virtual string CodigoSIGTAP
        {
            get { return codigosigtap; }
            set { codigosigtap = value; }
        }

        char status;

        public virtual char Status
        {
            get { return status; }
            set { status = value; }
        }

        public virtual string DescricaoStatus
        {
            get 
            {
                if (this.Status == 'A')
                    return "Ativo";
                return "Inativo";
            }
        }

        public ItemPA()
        {
        }
    }
}
