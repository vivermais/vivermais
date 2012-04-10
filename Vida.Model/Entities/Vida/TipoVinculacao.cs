using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.Model
{
    [Serializable]
    public class TipoVinculacao
    {
        Vinculacao vinculacao;

        public virtual Vinculacao Vinculacao
        {
            get { return vinculacao; }
            set { vinculacao = value; }
        }

        string codigo;

        public virtual string Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        

        string descricaoVinculo;

        public virtual string DescricaoVinculo
        {
            get { return descricaoVinculo; }
            set { descricaoVinculo = value; }
        }

        EsferaAdministrativa esferaAdministrativa;

        public virtual EsferaAdministrativa EsferaAdministrativa
        {
            get { return esferaAdministrativa; }
            set { esferaAdministrativa = value; }
        }

        public TipoVinculacao()
        {

        }
    }
}
