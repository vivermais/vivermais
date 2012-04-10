﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class TipoLogradouroDAO:ADAO<TipoLogradouro>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_tipo_logradouro");
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(TipoLogradouro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(TipoLogradouro objeto)
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
            sqlText.Append("select * from tb_ms_tipo_logradouro ");
            sqlText.Append("where co_tipo_logradouro = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(TipoLogradouro objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, TipoLogradouro objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_tipo_logradouro"]);
            objeto.Abreviatura = Convert.ToString(dataReader["ds_tipo_logradouro_abrev"]);
            objeto.Descricao = Convert.ToString(dataReader["ds_tipo_logradouro"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, TipoLogradouro objeto)
        {
            throw new NotImplementedException();
        }
        
        protected override void SetarCodigoObjeto(DataRow dataRow, TipoLogradouro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, TipoLogradouro objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_tipo_logradouro"]);
            objeto.Abreviatura = Convert.ToString(drc["ds_tipo_logradouro_abrev"]);
            objeto.Descricao = Convert.ToString(drc["ds_tipo_logradouro"]);
        }


    }
}
