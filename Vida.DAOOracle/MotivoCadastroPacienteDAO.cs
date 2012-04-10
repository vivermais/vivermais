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
    public class MotivoCadastroPacienteDAO:ADAO<MotivoCadastroPaciente>
    {
        public void Cadastrar(MotivoCadastroPaciente motivo)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(motivo);
            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        public void Cadastrar(MotivoCadastroPaciente motivo, OracleTransaction trans)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(motivo);
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        public void Atualizar(MotivoCadastroPaciente motivo)
        {
            GerarQueryAtualizacao();
            GerarParametrosAtualizacao(motivo);
            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }       

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into rl_ms_usuario_motivo ");
            sqlText.Append("(co_motivo, co_usuario, co_cnes) ");
            sqlText.Append("values ");
            sqlText.Append("(:CodigoMotivo, :CodigoUsuario, :CNES)");
        }

        protected override void GerarParametrosCadastro(MotivoCadastroPaciente objeto)
        {
            GerarParametrosAtualizacao(objeto);
        }


        protected override void GerarQueryAtualizacao()
        {
            sqlText.Append("update rl_ms_usuario_motivo set ");
            sqlText.Append("co_motivo = :CodigoMotivo ");
            sqlText.Append("where co_usuario = :CodigoUsuario ");
            sqlText.Append("and co_cnes = :CNES");
        }

        protected override void GerarParametrosAtualizacao(MotivoCadastroPaciente objeto)
        {
            parametros.Add(new OracleParameter("CodigoMotivo", OracleDbType.Int32));
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CNES", OracleDbType.Varchar2));

            parametros[0].Value = objeto.Motivo.Codigo;
            parametros[1].Value = objeto.Paciente.Codigo;
            parametros[2].Value = objeto.Cnes;
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

        protected override void GeraParametrosPesquisaPorCodigo(MotivoCadastroPaciente objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, MotivoCadastroPaciente objeto)
        {
            if (objeto.Motivo == null)
            {
                objeto.Motivo = new MotivoCadastro();
                objeto.Motivo.Codigo = Convert.ToInt32(dataReader["co_motivo"]);
            }

            if (objeto.Paciente == null)
            {
                objeto.Paciente = new Paciente();
                objeto.Paciente.Codigo = Convert.ToString(dataReader["co_usuario"]);
            }
            

            objeto.Cnes = Convert.ToString(dataReader["co_cnes"]);
            try
            {
                objeto.CodigoDocumentoReferencia = Convert.ToInt32(dataReader["co_doc_ref"]);
            }
            catch (InvalidCastException) { }

            objeto.CnsMae = Convert.ToString(dataReader["CO_NUMERO_CARTAO_MAE"]);
            objeto.NumeroDocumento = Convert.ToString(dataReader["NU_DOCUMENTO"]);
            
            try
            {
                objeto.DataOperacao = Convert.ToDateTime(dataReader["DT_OPERACAO"]);
            }
            catch (InvalidCastException)
            {
                objeto.DataOperacao = DateTime.MinValue;
            }
            objeto.Controle = Convert.ToString(dataReader["ST_CONTROLE"]);
            try
            {
                objeto.Excluido = Convert.ToChar(dataReader["ST_EXCLUIDO"]);
            }
            catch (InvalidCastException) { }

            try
            {
                objeto.NumeroVersao = Convert.ToInt32(dataReader["NU_VERSAO"]);
            }
            catch (InvalidCastException) { }
        }

        protected override void SetarCodigoObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, MotivoCadastroPaciente objeto)
        {
            throw new NotImplementedException();
        }

        public List<MotivoCadastroPaciente> PesquisarPorUsuario(Paciente paciente)
        {
            sqlText.Append("select * ");
            sqlText.Append("from rl_ms_usuario_motivo ");           
            sqlText.Append("where co_usuario = :CodigoUsuario ");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = paciente.Codigo;

            List<MotivoCadastroPaciente> motivos = new List<MotivoCadastroPaciente>();
            MotivoCadastroPaciente motivo = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                motivo = new MotivoCadastroPaciente();
                motivo.Paciente = paciente;
                MontarObjeto(dataReader.Tables[0].Rows[0], motivo);
                motivos.Add(motivo);
            }

            return motivos;
        }

        protected override void MontarObjeto(DataRow drc, MotivoCadastroPaciente objeto)
        {
            if (objeto.Motivo == null)
            {
                objeto.Motivo = new MotivoCadastro();
                objeto.Motivo.Codigo = Convert.ToInt32(drc["co_motivo"]);
            }

            if (objeto.Paciente == null)
            {
                objeto.Paciente = new Paciente();
                objeto.Paciente.Codigo = Convert.ToString(drc["co_usuario"]);
            }


            objeto.Cnes = Convert.ToString(drc["co_cnes"]);
            try
            {
                objeto.CodigoDocumentoReferencia = Convert.ToInt32(drc["co_doc_ref"]);
            }
            catch (InvalidCastException) { }

            objeto.CnsMae = Convert.ToString(drc["CO_NUMERO_CARTAO_MAE"]);
            objeto.NumeroDocumento = Convert.ToString(drc["NU_DOCUMENTO"]);

            try
            {
                objeto.DataOperacao = Convert.ToDateTime(drc["DT_OPERACAO"]);
            }
            catch (InvalidCastException)
            {
                objeto.DataOperacao = DateTime.MinValue;
            }
            objeto.Controle = Convert.ToString(drc["ST_CONTROLE"]);
            try
            {
                objeto.Excluido = Convert.ToChar(drc["ST_EXCLUIDO"]);
            }
            catch (InvalidCastException) { }

            try
            {
                objeto.NumeroVersao = Convert.ToInt32(drc["NU_VERSAO"]);
            }
            catch (InvalidCastException) { }
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, MotivoCadastroPaciente objeto)
        {
            throw new NotImplementedException();
        }
    }
}