﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class OrigemDeficienciaDAO : ADAO<OrigemDeficiencia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select co_origem, no_origem from tb_pms_origemdeficiencia");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_origemdeficiencia ");
            sqlText.Append("where co_origem = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(OrigemDeficiencia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, OrigemDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_origem"]);
            objeto.Nome = Convert.ToString(dataReader["no_origem"]);
        }

        protected override void MontarObjeto(DataRow drc, OrigemDeficiencia objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_origem"]);
            objeto.Nome = Convert.ToString(drc["no_origem"]);
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(OrigemDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(OrigemDeficiencia objeto)
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

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, OrigemDeficiencia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, OrigemDeficiencia objeto)
        {
            throw new NotImplementedException();
        }
    }
}
