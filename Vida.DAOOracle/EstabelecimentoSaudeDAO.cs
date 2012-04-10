﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class EstabelecimentoSaudeDAO:ADAO<EstabelecimentoSaude>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from pms_cnes_lfces004 ");
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(EstabelecimentoSaude objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(EstabelecimentoSaude objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(EstabelecimentoSaude objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, EstabelecimentoSaude objeto)
        {
            throw new NotImplementedException();
        }

        //Falta completar
        protected override void MontarObjeto(DataRow drc, EstabelecimentoSaude objeto)
        {
            objeto.CNES = Convert.ToString(drc["CNES"]);
            objeto.NomeFantasia = Convert.ToString(drc["nome_fanta"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, EstabelecimentoSaude objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, EstabelecimentoSaude objeto)
        {
            throw new NotImplementedException();
        }
    }
}
