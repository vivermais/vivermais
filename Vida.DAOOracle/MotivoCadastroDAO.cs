﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class MotivoCadastroDAO:ADAO<MotivoCadastro>
    {
        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(MotivoCadastro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(MotivoCadastro objeto)
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
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(MotivoCadastro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, MotivoCadastro objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_motivo"]);
            objeto.Motivo = Convert.ToString(dataReader["no_motivo"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, MotivoCadastro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_motivo ");
        }

        protected override void MontarObjeto(DataRow drc, MotivoCadastro objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_motivo"]);
            objeto.Motivo = Convert.ToString(drc["no_motivo"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, MotivoCadastro objeto)
        {
            throw new NotImplementedException();
        }
    }
}
