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
    public class ControleEnderecoDAO:ADAO<ControleEndereco>
    {

        protected override ControleEndereco Cadastrar(ControleEndereco objeto, ref OracleTransaction trans)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(objeto);
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            //Chama a função para gerar log através de xml
            new DAOLogXml().SalvarLog(ref trans, objeto, 1);

            return objeto;
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into tb_ms_controle_endereco ");
            sqlText.Append("(co_endereco, DT_OPERACAO, NU_VERSAO, ");
            sqlText.Append("CO_ORIGEM, ST_CONTROLE, ST_ATIVO, ST_EXCLUIDO) ");
            sqlText.Append("values ");
            sqlText.Append("(:Codigo, :DataOperacao, :NumeroVersao, ");
            sqlText.Append(":CodigoOrigem, :Controle, :Ativo, :Excluido) ");
        }

        protected override void GerarParametrosCadastro(ControleEndereco objeto)
        {
            parametros.Add(new OracleParameter("Codigo", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("DataOperacao", OracleDbType.Date));
            parametros.Add(new OracleParameter("NumeroVersao", OracleDbType.Int16));
            parametros.Add(new OracleParameter("CodigoOrigem", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Controle", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Ativo", OracleDbType.Char));
            parametros.Add(new OracleParameter("Excluido", OracleDbType.Char));

            objeto.GerarCodigo();
            parametros[0].Value = objeto.Codigo;
            parametros[1].Value = objeto.DT_OPERACAO;
            
            if (objeto.NU_VERSAO != 0)
                parametros[2].Value = objeto.NU_VERSAO;
            else
                parametros[2].Value = DBNull.Value;
            
            if (objeto.CO_ORIGEM != null)
                parametros[3].Value = objeto.CO_ORIGEM;
            else
                parametros[3].Value = DBNull.Value;
            
            if (objeto.ST_CONTROLE != 0)
                parametros[4].Value = objeto.ST_CONTROLE;
            else
                parametros[4].Value = DBNull.Value;
            
            if (objeto.ST_ATIVO != 0)
                parametros[5].Value = objeto.ST_ATIVO;
            else
                parametros[5].Value = DBNull.Value;
            
            if (objeto.ST_EXCLUIDO != 0)
                parametros[6].Value = objeto.ST_EXCLUIDO;
            else
                parametros[6].Value = DBNull.Value;
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(ControleEndereco objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(ControleEndereco objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, ControleEndereco objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, ControleEndereco objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, ControleEndereco objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, ControleEndereco objeto)
        {
            throw new NotImplementedException();
        }
    }
}