﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class RacaCorDAO:ADAO<RacaCor>
    {
        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(RacaCor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(RacaCor objeto)
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
            sqlText.Append("select * from tb_ms_raca_cor ");
            sqlText.Append("where co_raca = :Codigo ");
        }

        protected override void MontarObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, RacaCor objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_raca"]);
            objeto.Descricao = Convert.ToString(dataReader["ds_raca"]);
        }

        protected override void SetarCodigoObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, RacaCor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, RacaCor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(RacaCor objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_raca_cor ");
        }

        protected override void MontarObjeto(DataRow drc, RacaCor objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_raca"]);
            objeto.Descricao = Convert.ToString(drc["ds_raca"]);
        }


    }
}
