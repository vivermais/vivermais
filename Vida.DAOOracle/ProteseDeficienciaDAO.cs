﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class ProteseDeficienciaDAO : ADAO<ProteseDeficiencia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select co_protese, no_protese from tb_pms_protesedeficiencia");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_protesedeficiencia ");
            sqlText.Append("where co_protese = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(ProteseDeficiencia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, ProteseDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_protese"]);
            objeto.Nome = Convert.ToString(dataReader["no_protese"]);
        }

        protected override void MontarObjeto(DataRow drc, ProteseDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_protese"]);
            objeto.Nome = Convert.ToString(drc["no_protese"]);
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(ProteseDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(ProteseDeficiencia objeto)
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

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, ProteseDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, ProteseDeficiencia objeto)
        {
            throw new NotImplementedException();
        }
    }
}
