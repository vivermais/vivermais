using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class PaisDAO:ADAO<Pais>
    {
        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(Pais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(Pais objeto)
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
            sqlText.Append("select * from tb_ms_nacionalidade ");
            sqlText.Append("where co_pais = :Codigo ");
        }

        protected override void MontarObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, Pais objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_pais"]);
            objeto.Nome = Convert.ToString(dataReader["ds_pais"]);
        }

        protected override void SetarCodigoObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, Pais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Pais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(Pais objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_nacionalidade ");
        }

        protected override void MontarObjeto(DataRow drc, Pais objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_pais"]);
            objeto.Nome = Convert.ToString(drc["ds_pais"]);
        }


    }
}
