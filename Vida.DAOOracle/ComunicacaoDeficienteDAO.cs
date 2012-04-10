using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public class ComunicacaoDeficienciaDAO : ADAO<ComunicacaoDeficiencia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select co_comunicacao, no_comunicacao from tb_pms_comunicacaodeficiencia");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_comunicacaodeficiencia ");
            sqlText.Append("where co_comunicacao = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(ComunicacaoDeficiencia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, ComunicacaoDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_comunicacao"]);
            objeto.Nome = Convert.ToString(dataReader["no_comunicacao"]);
        }

        protected override void MontarObjeto(DataRow drc, ComunicacaoDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_comunicacao"]);
            objeto.Nome = Convert.ToString(drc["no_comunicacao"]);
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(ComunicacaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(ComunicacaoDeficiencia objeto)
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

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, ComunicacaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, ComunicacaoDeficiencia objeto)
        {
            throw new NotImplementedException();
        }
    }
}
