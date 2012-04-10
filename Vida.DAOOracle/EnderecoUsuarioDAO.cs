using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using ViverMais.DAL;
using System.Data;

namespace ViverMais.DAOOracle
{
    public class EnderecoUsuarioDAO : ADAO<EnderecoUsuario>
    {
        //public void InativarEndereco(EnderecoUsuario enderecoUsuario, ref OracleTransaction transaction)
        //{
        //    sqlText.Remove(0, sqlText.Length);
        //    parametros.Clear();

        //    sqlText.Append("UPDATE rl_ms_usuario_endereco SET st_excluido = '1' WHERE co_usuario='" + enderecoUsuario.CodigoPaciente + "' AND co_endereco='" + enderecoUsuario.Endereco.Codigo + "'");
        //    DALOracle.ExecuteNonQuery(ref transaction, CommandType.Text, sqlText.ToString(), parametros.ToArray());
        //    sqlText.Remove(0, sqlText.Length);
        //    parametros.Clear();
        //}

        public new void Cadastrar(EnderecoUsuario enderecoUsuario, ref OracleTransaction transaction)
        {
            GerarQueryCadastro();
            GerarParametrosCadastro(enderecoUsuario);
            DALOracle.ExecuteNonQuery(ref transaction, CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        public void Atualizar(EnderecoUsuario enderecoUsuario, ref OracleTransaction transaction)
        {
            GerarQueryAtualizacao();
            GerarParametrosAtualizacao(enderecoUsuario);
            DALOracle.ExecuteNonQuery(ref transaction, CommandType.Text, sqlText.ToString().Trim(), true, parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();
        }

        protected override void GerarQueryCadastro()
        {
            sqlText.Append("insert into rl_ms_usuario_endereco ");
            sqlText.Append("(co_endereco, co_usuario, co_tipo_endereco, ");
            sqlText.Append("ST_CONTROLE, ST_EXCLUIDO, DT_OPERACAO, ST_VINCULO) ");
            sqlText.Append("values ");
            sqlText.Append("(:CodigoEndereco, :CodigoUsuario, :CodigoTipoEndereco, ");
            sqlText.Append(":Controle, :Excluido, :DataOperacao, :Vinculo)");
        }

        protected override void GerarParametrosCadastro(EnderecoUsuario objeto)
        {
            parametros.Add(new OracleParameter("CodigoEndereco", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoTipoEndereco", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Controle", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Excluido", OracleDbType.Char));
            parametros.Add(new OracleParameter("DataOperacao", OracleDbType.Date));
            parametros.Add(new OracleParameter("Vinculo", OracleDbType.Char));

            parametros[0].Value = objeto.Endereco.Codigo;
            parametros[1].Value = objeto.CodigoPaciente;
            parametros[2].Value = objeto.TipoEndereco.Codigo;
            if (objeto.Controle != null && objeto.Controle != string.Empty)
                parametros[3].Value = objeto.Controle;
            else
                parametros[3].Value = DBNull.Value;
            if (objeto.Excluido != 0)
                parametros[4].Value = objeto.Excluido;
            else
                parametros[4].Value = DBNull.Value;
            if (objeto.Operacao != null)
                objeto.Operacao = DateTime.Now;
            parametros[5].Value = objeto.Operacao;
            if (objeto.Vinculo != 0)
                parametros[6].Value = objeto.Vinculo;
            else
                parametros[6].Value = DBNull.Value;
        }

        protected override void GerarQueryAtualizacao()
        {
            sqlText.Append("update rl_ms_usuario_endereco set ");
            sqlText.Append(" co_tipo_endereco = :CodigoTipoEndereco, ST_CONTROLE = :Controle");
            sqlText.Append(", ST_EXCLUIDO = :Excluido, DT_OPERACAO = :DataOperacao, ST_VINCULO = :Vinculo ");
            sqlText.Append(" where co_usuario = :CodigoUsuario ");
            sqlText.Append(" and co_endereco = :CodigoEndereco  ");
        }

        protected override void GerarParametrosAtualizacao(EnderecoUsuario objeto)
        {
            parametros.Add(new OracleParameter("CodigoEndereco", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("CodigoTipoEndereco", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Controle", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Excluido", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("DataOperacao", OracleDbType.Date));
            parametros.Add(new OracleParameter("Vinculo", OracleDbType.Char));

            parametros[0].Value = objeto.Endereco.Codigo;
            parametros[1].Value = objeto.CodigoPaciente;
            parametros[2].Value = objeto.TipoEndereco.Codigo;
            if (objeto.Controle != null && objeto.Controle != string.Empty)
                parametros[3].Value = objeto.Controle;
            else
                parametros[3].Value = DBNull.Value;
            //if (objeto.Excluido != 0)
            parametros[4].Value = objeto.Excluido.ToString();
            // else
            //    parametros[4].Value = DBNull.Value;
            if (objeto.Operacao != null)
                objeto.Operacao = DateTime.Now;
            parametros[5].Value = objeto.Operacao;
            if (objeto.Vinculo != 0)
                parametros[6].Value = objeto.Vinculo;
            else
                parametros[6].Value = DBNull.Value;
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

        protected override void MontarObjeto(Oracle.DataAccess.Client.OracleDataReader dataReader, EnderecoUsuario objeto)
        {
            objeto.CodigoPaciente = Convert.ToString(dataReader["co_usuario"]);
            objeto.Endereco = new Endereco();
            objeto.Endereco.ControleEndereco = new ControleEndereco();
            objeto.Endereco.ControleEndereco.Codigo = Convert.ToString(dataReader["co_endereco"]);
            objeto.TipoEndereco = new TipoEndereco();
            objeto.TipoEndereco.Codigo = Convert.ToString(dataReader["co_tipo_endereco"]);
            objeto.Controle = Convert.ToString(dataReader["st_controle"]);
            try
            {
                objeto.Excluido = Convert.ToChar(dataReader["st_excluido"]);
            }
            catch (InvalidCastException)
            {
                objeto.Excluido = char.MinValue;
            }

            try
            {
                objeto.Operacao = Convert.ToDateTime(dataReader["dt_operacao"]);
            }
            catch (InvalidCastException)
            {
                objeto.Operacao = DateTime.MinValue;
            }
            try
            {
                objeto.Vinculo = Convert.ToChar(dataReader["st_vinculo"]);
            }
            catch (InvalidCastException)
            {
                objeto.Vinculo = char.MinValue;
            }
        }

        public EnderecoUsuario PesquisarPorUsuario(string codigoUsuario)
        {
            sqlText.Append("select * from rl_ms_usuario_endereco ");
            sqlText.Append("where co_usuario = :CodigoUsuario and st_excluido='0'");

            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = codigoUsuario;

            DataSet dataReader = DALOracle.ExecuteReaderDs(System.Data.CommandType.Text, sqlText.ToString(), parametros.ToArray());

            EnderecoUsuario endereco = null;

            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                endereco = new EnderecoUsuario();
                MontarObjeto(dataReader.Tables[0].Rows[0], endereco);
            }

            return endereco;
        }

        public EnderecoUsuario Pesquisar(string codigoUsuario, string codigoEndereco)
        {
            sqlText.Append("select * from rl_ms_usuario_endereco ");
            sqlText.Append("where co_usuario = :CodigoUsuario ");
            sqlText.Append("and co_endereco = :CodigoEndereco ");
            parametros.Add(new OracleParameter("CodigoUsuario", OracleDbType.Varchar2));
            parametros[0].Value = codigoUsuario;
            parametros.Add(new OracleParameter("CodigoEndereco", OracleDbType.Varchar2));            parametros[1].Value = codigoEndereco;
            DataSet dataReader = DALOracle.ExecuteReaderDs(System.Data.CommandType.Text, sqlText.ToString(), parametros.ToArray());

            EnderecoUsuario endereco = null;

            if (dataReader != null && dataReader.Tables[0].Rows.Count > 0)
            {
                endereco = new EnderecoUsuario();
                MontarObjeto(dataReader.Tables[0].Rows[0], endereco);
            }

            return endereco;
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, EnderecoUsuario objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GeraParametrosPesquisaPorCodigo(EnderecoUsuario objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        public EnderecoUsuario PesquisarPorPaciente(Paciente paciente)
        {
            return PesquisarPorUsuario(paciente.Codigo);
        }

        protected override void MontarObjeto(DataRow drc, EnderecoUsuario objeto)
        {
            objeto.CodigoPaciente = Convert.ToString(drc["co_usuario"]);
            objeto.Endereco = new Endereco();
            objeto.Endereco.ControleEndereco = new ControleEndereco();
            objeto.Endereco.ControleEndereco.Codigo = Convert.ToString(drc["co_endereco"]);
            objeto.TipoEndereco = new TipoEndereco();
            objeto.TipoEndereco.Codigo = Convert.ToString(drc["co_tipo_endereco"]);
            objeto.Controle = Convert.ToString(drc["st_controle"]);
            try
            {
                objeto.Excluido = Convert.ToChar(drc["st_excluido"]);
            }
            catch (InvalidCastException)
            {
                objeto.Excluido = char.MinValue;
            }

            try
            {
                objeto.Operacao = Convert.ToDateTime(drc["dt_operacao"]);
            }
            catch (InvalidCastException)
            {
                objeto.Operacao = DateTime.MinValue;
            }
            try
            {
                objeto.Vinculo = Convert.ToChar(drc["st_vinculo"]);
            }
            catch (InvalidCastException)
            {
                objeto.Vinculo = char.MinValue;
            }
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, EnderecoUsuario objeto)
        {
            throw new NotImplementedException();
        }


    }
}
