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
    public class DistritoDAO:ADAO<Distrito>
    {

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into tb_pms_distrito ");
            sqlText.Append("(co_distrito, no_distrito, co_municipio) ");
            sqlText.Append("values ");
            sqlText.Append("(DISTRITO_SEQUENCE.NextVal,:Nome, :CodigoMunicipio)");
        }

        protected override void GerarParametrosCadastro(Distrito objeto)
        {
            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoMunicipio", OracleDbType.Varchar2));

            parametros[0].Value = objeto.Nome;
            parametros[1].Value = objeto.Municipio.Codigo;
        }

        protected override void GerarQueryAtualizacao()
        {
            sqlText.Append("update tb_pms_distrito set ");
            sqlText.Append("no_distrito = :Nome ");
            sqlText.Append("where co_distrito = :Codigo ");
        }

        protected override void GerarParametrosAtualizacao(Distrito objeto)
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
            sqlText.Append("select DISTRITO_SEQUENCE.currval as id from dual");
        }

        protected override void GerarQueryPesquisaPorCodigo()
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(Distrito objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Distrito objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["co_distrito"]);
            objeto.Nome = Convert.ToString(dataReader["no_distrito"]);
            if (objeto.Municipio == null)
            {
                objeto.Municipio = new Municipio();
                objeto.Municipio.Codigo = Convert.ToString(dataReader["co_municipio"]);                  
            }
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Distrito objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataReader["id"]);
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Distrito objeto)
        {
            objeto.Codigo = Convert.ToInt32(dataRow["id"]);
        }

        public List<Distrito> PesquisarPorMunicipio(Municipio municipio)
        {
            sqlText.Append("select * from tb_pms_distrito ");
            sqlText.Append("where co_municipio = :CodigoMunicipio ");

            parametros.Add(new OracleParameter("CodigoMunicipio", OracleDbType.Varchar2));
            parametros[0].Value = municipio.Codigo;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();


            List<Distrito> distritos = new List<Distrito>();
            Distrito distrito = null;

            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                distrito = new Distrito();
                distrito.Municipio = municipio;
                MontarObjeto(dr, distrito);
                distritos.Add(distrito);
            }

            return distritos;
        }

        public Distrito PesquisarPorNome(string nomeDistrito)
        {
            sqlText.Append("select * from tb_pms_distrito ");
            sqlText.Append("where no_distrito = :Nome ");

            parametros.Add(new OracleParameter("Nome", OracleDbType.Varchar2));
            parametros[0].Value = nomeDistrito;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
                        
            Distrito distrito = null;

            if (dataReader != null)
            {
                distrito = new Distrito();                
                MontarObjeto(dataReader.Tables[0].Rows[0], distrito);                
            }

            return distrito;
        }

        protected override void MontarObjeto(DataRow drc, Distrito objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_distrito"]);
            objeto.Nome = Convert.ToString(drc["no_distrito"]);
            if (objeto.Municipio == null)
            {
                objeto.Municipio = new Municipio();
                objeto.Municipio.Codigo = Convert.ToString(drc["co_municipio"]);
            }
        }

        
    }
}