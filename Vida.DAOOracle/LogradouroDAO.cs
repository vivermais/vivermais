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
    public class LogradouroDAO:ADAO<Logradouro>
    {
        public void Cadastrar(Logradouro objeto)
        {
            sqlText.Append("insert into tb_pms_logradouro ");
            sqlText.Append("(cep, tipo, logradouro, bairro, cidade) ");
            sqlText.Append("values ");
            sqlText.Append("(:CEP, :Tipo, :Logradouro, :Bairro, :Cidade) ");

            parametros.Add(new OracleParameter("CEP", OracleDbType.Int64));
            parametros.Add(new OracleParameter("Tipo", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Logradouro", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Bairro", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Cidade", OracleDbType.Varchar2));

            parametros[0].Value = objeto.CEP;
            parametros[1].Value = objeto.Tipo;
            parametros[2].Value = objeto.NomeLogradouro;
            parametros[3].Value = objeto.Bairro;
            parametros[4].Value = objeto.Cidade;

            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

        }

        public void Cadastrar(Logradouro objeto, OracleTransaction trans)
        {
            sqlText.Append("insert into tb_pms_logradouro ");
            sqlText.Append("(cep, tipo, logradouro, bairro, cidade) ");
            sqlText.Append("values ");
            sqlText.Append("(:CEP, :Tipo, :Logradouro, :Bairro, :Cidade) ");

            parametros.Add(new OracleParameter("CEP", OracleDbType.Int64));
            parametros.Add(new OracleParameter("Tipo", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Logradouro", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Bairro", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Cidade", OracleDbType.Varchar2));

            parametros[0].Value = objeto.CEP;
            parametros[1].Value = objeto.Tipo;
            parametros[2].Value = objeto.NomeLogradouro;
            parametros[3].Value = objeto.Bairro;
            parametros[4].Value = objeto.Cidade;

            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

        }


        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(Logradouro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(Logradouro objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(Logradouro objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Logradouro objeto)
        {
            objeto.Bairro = Convert.ToString(dataReader["bairro"]);
            objeto.CEP = Convert.ToInt64(dataReader["CEP"]);
            objeto.Cidade = Convert.ToString(dataReader["cidade"]);
            objeto.Tipo = Convert.ToString(dataReader["tipo"]);
            objeto.NomeLogradouro = Convert.ToString(dataReader["logradouro"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Logradouro objeto)
        {
            throw new NotImplementedException();
        }

        public Logradouro PesquisarPorCep(long cep)
        {
            sqlText.Append("select * from tb_pms_logradouro ");
            sqlText.Append("where cep = :CEP");

            parametros.Add(new OracleParameter("CEP", OracleDbType.Int64));
            parametros[0].Value = cep;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            Logradouro retorno = null;

            if (dataReader!=null && dataReader.Tables[0].Rows.Count > 0)
            {
                retorno = new Logradouro();
                MontarObjeto(dataReader.Tables[0].Rows[0], retorno);
            }
            return retorno;
        }

        protected override void MontarObjeto(DataRow drc, Logradouro objeto)
        {
            objeto.Bairro = Convert.ToString(drc["bairro"]);
            objeto.CEP = Convert.ToInt64(drc["CEP"]);
            objeto.Cidade = Convert.ToString(drc["cidade"]);
            objeto.Tipo = Convert.ToString(drc["tipo"]);
            objeto.NomeLogradouro = Convert.ToString(drc["logradouro"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Logradouro objeto)
        {
            throw new NotImplementedException();
        }
    }
}
