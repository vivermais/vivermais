﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public class BairroDAO : ADAO<Bairro>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into tb_pms_bairro ");
            sqlText.Append("(co_bairro, no_bairro, co_distrito) ");
            sqlText.Append("values ");
            sqlText.Append("(BAIRRO_SEQUENCE.NextVal, :Nome, :CodigoDistrito)");
        }

        protected override void GerarParametrosCadastro(Bairro objeto)
        {
            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoDistrito", OracleDbType.Int32));

            parametros[0].Value = objeto.Nome;
            parametros[1].Value = objeto.Distrito.Codigo;
        }

        protected override void GerarQueryAtualizacao()
        {
            sqlText.Append("update tb_pms_bairro set ");
            sqlText.Append("no_bairro = :Nome ");
            sqlText.Append("where co_bairro = :Codigo ");
        }

        protected override void GerarParametrosAtualizacao(Bairro objeto)
        {
            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));

            parametros[0].Value = objeto.Nome;
            parametros[1].Value = objeto.Codigo;
        }

        protected override void GerarQueryRemocao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryBuscarID()
        {
            sqlText.Append("select BAIRRO_SEQUENCE.currval as id from dual");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            sqlText.Append("select * from tb_pms_bairro ");
            sqlText.Append("where co_bairro = :Codigo");
        }

        protected override void GeraParametrosPesquisaPorCodigo(Bairro objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Int32));
            parametros[0].Value = objeto.Codigo;
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Bairro objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_bairro"]);
            objeto.Nome = Convert.ToString(dataReader["no_bairro"]);
            if (objeto.Distrito == null)
            {
                objeto.Distrito = new Distrito();
                objeto.Distrito.Codigo = Convert.ToInt32(dataReader["co_distrito"]);
            }
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Bairro objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["id"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Bairro objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataRow["id"]);
        }

        public List<Bairro> PesquisarPorDistrito(Distrito distrito)
        {
            sqlText.Append("select * from tb_pms_bairro ");
            sqlText.Append("where co_distrito = :CodigoDistrito");

            parametros.Add(new OracleParameter("CodigoDistrito", OracleDbType.Int32));
            parametros[0].Value = distrito.Codigo;
            
            List<Bairro> bairros = new List<Bairro>();
            Bairro bairro = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            
            
            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                bairro = new Bairro();
                bairro.Distrito = distrito;
                MontarObjeto(dr, bairro);
                bairros.Add(bairro);
            }

            return bairros;
        }

        public Bairro PesquisarPorNome(string nome)
        {
            sqlText.Append("select * from tb_pms_bairro ");
            sqlText.Append("where no_bairro = :Nome");

            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
            parametros[0].Value = nome;
                        
            Bairro bairro = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                bairro = new Bairro();
                MontarObjeto(dr, bairro);                
            }

            return bairro;
        }

        protected override void MontarObjeto(DataRow drc, Bairro objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_bairro"]);
            objeto.Nome = Convert.ToString(drc["no_bairro"]);
            if (objeto.Distrito == null)
            {
                objeto.Distrito = new Distrito();
                objeto.Distrito.Codigo = Convert.ToInt32(drc["co_distrito"]);
            }
        }

    }
}
