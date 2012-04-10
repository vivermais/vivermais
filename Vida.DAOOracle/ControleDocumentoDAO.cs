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
    public class ControleDocumentoDAO:ADAO<ControleDocumento>
    {
        public void Cadastrar(ControleDocumento controleDocumento)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(controleDocumento);
            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            new DAOLogXml().SalvarLog(controleDocumento, 1);
        }


        public void Cadastrar(ControleDocumento controleDocumento,ref OracleTransaction trans)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(controleDocumento);
            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            //Chama a função para gerar log através de xml
            new DAOLogXml().SalvarLog(ref trans, controleDocumento, 1);
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into tb_ms_controle_documento ");
            sqlText.Append("(co_tipo_documento, co_usuario) ");
            sqlText.Append("values ");
            sqlText.Append("(:CodigoTipoDocumento, :CodigoPaciente) ");
        }

        protected override void GerarParametrosCadastro(ControleDocumento objeto)
        {
            parametros.Add(new OracleParameter("CodigoTipoDocumento", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoPaciente", OracleDbType.Varchar2));

            parametros[0].Value = objeto.TipoDocumento.Codigo;
            parametros[1].Value = objeto.Paciente.Codigo;
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(ControleDocumento objeto)
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

        protected override void MontarObjeto(OracleDataReader dataReader, ControleDocumento objeto)
        {
            if (objeto.Paciente == null)
            {
                objeto.Paciente = new Paciente();
                objeto.Paciente.Codigo = Convert.ToString(dataReader["co_usuario"]);
            }
            objeto.TipoDocumento = new TipoDocumento();
            objeto.TipoDocumento.Codigo = Convert.ToString(dataReader["co_tipo_documento"]);
            try
            {
                objeto.Controle = Convert.ToChar(dataReader["ST_CONTROLE"]);
            }
            catch (InvalidCastException)
            {
                objeto.Controle = char.MinValue;
            }
            try
            {
                objeto.Excluido = Convert.ToChar(dataReader["ST_EXCLUIDO"]);
            }
            catch (InvalidCastException)
            {
                objeto.Excluido = char.MinValue;
            }
            try
            {
                objeto.DataOperacao = Convert.ToDateTime(dataReader["dt_operacao"]);
            }
            catch (InvalidCastException)
            {
                objeto.DataOperacao = DateTime.MinValue;
            }
        }

        public List<ControleDocumento> PesquisarPorPaciente(Paciente paciente)
        {
            sqlText.Append("select * from tb_ms_controle_documento ");
            sqlText.Append("where co_usuario = :CodigoUsuario");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = paciente.Codigo;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            List<ControleDocumento> controles = new List<ControleDocumento>();
            ControleDocumento controle = null;
            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                controle = new ControleDocumento();
                controle.Paciente = paciente;
                MontarObjeto(dr, controle);
                controles.Add(controle);
            }
            return controles;
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, ControleDocumento objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(ControleDocumento objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        public List<ControleDocumento> PesquisarPorPaciente(string codigoDocumento, Paciente paciente)
        {
            sqlText.Append("select * from tb_ms_controle_documento ");
            sqlText.Append("where co_usuario = :CodigoUsuario ");
            sqlText.Append("and co_tipo_documento = :CodigoTipoDocumento ");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoTipoDocumento", OracleDbType.Varchar2));
            parametros[0].Value = paciente.Codigo;
            parametros[1].Value = codigoDocumento;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            List<ControleDocumento> controles = new List<ControleDocumento>();
            ControleDocumento controle = null;
            foreach(DataRow dr in dataReader.Tables[0].Rows)
            {
                controle = new ControleDocumento();
                controle.Paciente = paciente;
                MontarObjeto(dr, controle);
                controles.Add(controle);
            }
            return controles;
        }

        protected override void MontarObjeto(DataRow drc, ControleDocumento objeto)
        {
            if (objeto.Paciente == null)
            {
                objeto.Paciente = new Paciente();
                objeto.Paciente.Codigo = Convert.ToString(drc["co_usuario"]);
            }
            objeto.TipoDocumento = new TipoDocumento();
            objeto.TipoDocumento.Codigo = Convert.ToString(drc["co_tipo_documento"]);
            try
            {
                objeto.Controle = Convert.ToChar(drc["ST_CONTROLE"]);
            }
            catch (InvalidCastException)
            {
                objeto.Controle = char.MinValue;
            }
            try
            {
                objeto.Excluido = Convert.ToChar(drc["ST_EXCLUIDO"]);
            }
            catch (InvalidCastException)
            {
                objeto.Excluido = char.MinValue;
            }
            try
            {
                objeto.DataOperacao = Convert.ToDateTime(drc["dt_operacao"]);
            }
            catch (InvalidCastException)
            {
                objeto.DataOperacao = DateTime.MinValue;
            }
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, ControleDocumento objeto)
        {
            throw new NotImplementedException();
        }
    }
}
