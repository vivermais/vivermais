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
    public class UsuarioDAO:ADAO<Usuario>
    {
        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(Usuario objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(Usuario objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(Usuario objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, Usuario objeto)
        {
            throw new NotImplementedException();
        }

        //Completar
        protected override void MontarObjeto(DataRow drc, Usuario objeto)
        {
            objeto.Codigo = Convert.ToInt32(drc["co_usuario"]);
            objeto.Nome = Convert.ToString(drc["no_usuario"]);
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, Usuario objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, Usuario objeto)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> PesquisarPorUnidade(string CNES)
        {
            List<Usuario> usuarios = new List<Usuario>();

            sqlText.Append("select * from tb_pms_usuario ");
            sqlText.Append("where co_unidade = :CNES ");

            parametros.Add(new OracleParameter("CNES", OracleDbType.Varchar2));
            parametros[0].Value = CNES;

            DataSet dataSet = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            foreach (DataRow dr in dataSet.Tables[0].Rows)
            {
                Usuario usuario = new Usuario();
                MontarObjeto(dr, usuario);
                usuarios.Add(usuario);
            }

            return usuarios;
        }
    }
}
