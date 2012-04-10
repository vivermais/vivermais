﻿//using System;
//using System.Collections.Generic;
//using System.Text;
//using Oracle.DataAccess.Client;
//using System.Data;

//namespace ViverMais.DAOOracle
//{
//    public class CidadeDAO : ADAO<Cidade>
//    {
//        public CidadeDAO()
//            : base()
//        {
//        }
               

//        protected override void GerarQueryCadastro()
//        {
//            sqlText.Append("insert into cidade ");
//            sqlText.Append("values ");
//            sqlText.Append("(CIDADE_SEQUENCE.NextVal, :CodigoUf, :Nome)");
//        }

//        protected override void GararParametrosCadastro(Cidade objeto)
//        {
//            parametros.Add(new OracleParameter("CodigoUf", OracleDbType.Int32));
//            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
//            parametros[0].Value = objeto.Estado.Codigo;
//            parametros[1].Value = objeto.Nome;
//        }

//        protected override void GerarQueryAtualizacao()
//        {
//            sqlText.Append("update cidade set ");
//            sqlText.Append("codigo_uf = :CodigoUf ");
//            sqlText.Append("nome = :Nome ");
//            sqlText.Append("where codigo_cidade = :Codigo ");
//        }

//        protected override void GerarParametrosAtualizacao(Cidade objeto)
//        {
//            parametros.Add(new OracleParameter("CodigoUf", OracleDbType.Int32));
//            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
//            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
//            parametros[0].Value = objeto.Estado.Codigo;
//            parametros[1].Value = objeto.Nome;
//            parametros[2].Value = objeto.Codigo;
//        }

//        protected override void GerarQueryBuscarID()
//        {
//            sqlText.Remove(0, sqlText.Length);
//            sqlText.Append("select CIDADE_SEQUENCE.currval as id from dual");
//        }

//        protected override void GerarQueryRemocao()
//        {
//            sqlText.Append("delete from cidade ");
//            sqlText.Append("where codigo_cidade = :Codigo");
//        }
        
//        protected override void GerarQueryPesquisaPorCodigo()
//        {
//            sqlText.Append("select * from cidade ");
//            sqlText.Append("where codigo_cidade = :Codigo ");
//        }

//        protected override void MontarObjeto(OracleDataReader dataReader, Cidade objeto)
//        {
//            objeto.Codigo = Convert.ToInt32(dataReader["codigo_cidade"]);
//            objeto.Nome = Convert.ToString(dataReader["nome"]);            
//        }

//        public List<Cidade> PesquisarPorUF(int codigoUF)
//        {
//            sqlText.Append("select * from cidade ");
//            sqlText.Append("where codigo_uf = :CodigoEstado ");

//            parametros.Add(new OracleParameter("CodigoEstado", OracleDbType.Int32));
//            parametros[0].Value = codigoUF;

//            OracleDataReader dataReader = Dal.ExecuteReader(CommandType.Text, sqlText.ToString(), parametros.ToArray());

//            List<Cidade> cidades = new List<Cidade>();
//            Cidade cidade;
//            while (dataReader.Read())
//            {
//                cidade = new Cidade();
//                MontarObjeto(dataReader, cidade);
//                cidades.Add(cidade);
//            }
//            return cidades;
//        }
//    }
//}