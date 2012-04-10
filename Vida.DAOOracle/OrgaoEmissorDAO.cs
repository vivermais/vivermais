using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class OrgaoEmissorDAO:ADAO<OrgaoEmissor>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_orgao_emissor ");
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(OrgaoEmissor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(OrgaoEmissor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryRemocao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryBuscarID()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(OrgaoEmissor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, OrgaoEmissor objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_orgao_emissor"]);
            objeto.Nome = Convert.ToString(dataReader["ds_orgao_emissor"]);
            if (objeto.CategoriaOcupacao == null)
            {
                objeto.CategoriaOcupacao = new CategoriaOcupacao();
                objeto.CategoriaOcupacao.Codigo = Convert.ToString(dataReader["co_categoria"]);
            }
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, OrgaoEmissor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, OrgaoEmissor objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_orgao_emissor"]);
            objeto.Nome = Convert.ToString(drc["ds_orgao_emissor"]);
            if (objeto.CategoriaOcupacao == null)
            {
                objeto.CategoriaOcupacao = new CategoriaOcupacao();
                objeto.CategoriaOcupacao.Codigo = Convert.ToString(drc["co_categoria"]);
            }
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, OrgaoEmissor objeto)
        {
            throw new NotImplementedException();
        }
    }
}
