using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using ViverMais.Model;

namespace ViverMais.DAOOracle
{
    public class DeficienciaDAO: ADAO<Deficiencia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select co_deficiencia, no_deficiencia from tb_pms_deficiencia");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_deficiencia ");
            sqlText.Append("where co_deficiencia = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(Deficiencia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Deficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_deficiencia"]);
            objeto.Nome = Convert.ToString(dataReader["no_deficiencia"]);
        }

        protected override void MontarObjeto(DataRow drc, Deficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_deficiencia"]);
            objeto.Nome = Convert.ToString(drc["no_deficiencia"]);
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(Deficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(Deficiencia objeto)
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

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Deficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Deficiencia objeto)
        {
            throw new NotImplementedException();
        }
    }
}
