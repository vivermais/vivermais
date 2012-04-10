using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class TipoDocumentoDAO:ADAO<TipoDocumento>
    {
        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(TipoDocumento objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(TipoDocumento objeto)
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
            sqlText.Append("select * from tb_ms_tipo_documento ");
            sqlText.Append("where co_tipo_documento = :Codigo ");
        }

        protected override void MontarObjeto(OracleDataReader dataReader, TipoDocumento objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_tipo_documento"]);
            objeto.Descricao = Convert.ToString(dataReader["ds_tipo_documento"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, TipoDocumento objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, TipoDocumento objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(TipoDocumento objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros[0].Value = objeto.Codigo;            
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, TipoDocumento objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_tipo_documento"]);
            objeto.Descricao = Convert.ToString(drc["ds_tipo_documento"]);
        }


    }
}
