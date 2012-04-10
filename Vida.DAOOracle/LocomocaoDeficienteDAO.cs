using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class LocomocaoDeficienciaDAO : ADAO<LocomocaoDeficiencia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select co_locomocao, no_locomocao from tb_pms_locomocaodeficiencia");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_locomocaodeficiencia ");
            sqlText.Append("where co_locomocao = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(LocomocaoDeficiencia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, LocomocaoDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_locomocao"]);
            objeto.Nome = Convert.ToString(dataReader["no_locomocao"]);
        }

        protected override void MontarObjeto(DataRow drc, LocomocaoDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_locomocao"]);
            objeto.Nome = Convert.ToString(drc["no_locomocao"]);
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(LocomocaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(LocomocaoDeficiencia objeto)
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

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, LocomocaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, LocomocaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }
    }
}
