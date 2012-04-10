using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using ViverMais.DAL;
using ViverMais.Model;

namespace ViverMais.DAOOracle
{
    public class EtniaDAO:ADAO<Etnia>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_pms_etnia ");
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(Etnia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(Etnia objeto)
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
            sqlText.Append("select * from tb_pms_etnia ");
            sqlText.Append("where cod_etnia = :Codigo ");
        }

        protected override void GeraParametrosPesquisaPorCodigo(Etnia objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Etnia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, Etnia objeto)
        {
            objeto.Codigo = Convert.ToString(drc["COD_ETNIA"]);
            objeto.Descricao = Convert.ToString(drc["NO_ETNIA"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Etnia objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Etnia objeto)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Etnia etnia)
        {
            sqlText.Append("insert into tb_pms_etnia ");
            sqlText.Append("values ");
            sqlText.Append("(:Codigo, :Descricao )");

            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Descricao", OracleDbType.Varchar2));

            parametros[0].Value = etnia.Codigo;
            parametros[1].Value = etnia.Descricao;

            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }
    }
}
