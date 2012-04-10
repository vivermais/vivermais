using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using ViverMais.Model;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public class UFDAO : ADAO<UF>
    {

        public UFDAO()
            : base()
        {
        }        

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(UF objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(UF objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryBuscarID()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryRemocao()
        {
            throw new NotImplementedException();
        }
        
        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_ms_unidade_federacao ");
            sqlText.Append("where co_uf = :Codigo ");
        }

        protected override void MontarObjeto(OracleDataReader dataReader, UF objeto)
        {
            objeto.Codigo = Convert.ToString(dataReader["co_uf"]);
            objeto.Nome = Convert.ToString(dataReader["ds_uf"]);
            objeto.Sigla = Convert.ToString(dataReader["sg_uf"]);
        }

        public override void Completar(UF estado)
        {
            sqlText.Append("select * from tb_ms_unidade_federacao ");
            sqlText.Append("where sg_uf = :Sigla");

            parametros.Add(new OracleParameter("Sigla", OracleDbType.Varchar2));
            parametros[0].Value = estado.Sigla;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());

            if (dataReader!=null)
            {
                MontarObjeto(dataReader.Tables[0].Rows[0], estado);
            }            
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, UF objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(UF objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            sqlText.Append("select * from tb_ms_unidade_federacao ");
        }

        public UF PesquisarPorSigla(string sigla)
        {
            sqlText.Append("select * from tb_ms_unidade_federacao ");
            sqlText.Append("where SG_UF = :Sigla ");

            parametros.Add(new OracleParameter("Sigla", OracleDbType.Varchar2));
            parametros[0].Value = sigla;

            UF retorno = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            if (dataReader!=null)
            {
                retorno = new UF();
                MontarObjeto(dataReader.Tables[0].Rows[0], retorno);                
            }

            return retorno;
        }

        protected override void MontarObjeto(DataRow drc, UF objeto)
        {
            objeto.Codigo = Convert.ToString(drc["co_uf"]);
            objeto.Nome = Convert.ToString(drc["ds_uf"]);
            objeto.Sigla = Convert.ToString(drc["sg_uf"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, UF objeto)
        {
            throw new NotImplementedException();
        }
    }
}
