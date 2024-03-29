﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class EstadoCivilDAO:ADAO<EstadoCivil>
    {
        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(EstadoCivil objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(EstadoCivil objeto)
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
            sqlText.Append("select * from tb_ms_estado_civil ");
            sqlText.Append("where co_estado_civil = :Codigo ");
        }

        protected override void MontarObjeto(OracleDataReader dataReader, EstadoCivil objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_estado_civil"]);
            objeto.Descricao = Convert.ToString(dataReader["ds_estado_civil"]);
            objeto.DataAtivacaoDesativacao = Convert.ToDateTime(dataReader["dt_ativ_desat"]);
            objeto.SituacaoAtivacao = Convert.ToChar(dataReader["st_ativacao"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, EstadoCivil objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(EstadoCivil objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, EstadoCivil objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_estado_civil"]);
            objeto.Descricao = Convert.ToString(drc["ds_estado_civil"]);
            objeto.DataAtivacaoDesativacao = Convert.ToDateTime(drc["dt_ativ_desat"]);
            objeto.SituacaoAtivacao = Convert.ToChar(drc["st_ativacao"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, EstadoCivil objeto)
        {
            throw new NotImplementedException();
        }
    }
}
