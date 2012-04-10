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
    public class ControleCartaoSUSDAO:ADAO<ControleCartaoSUS>
    {

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(ControleCartaoSUS objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(ControleCartaoSUS objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(ControleCartaoSUS objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, ControleCartaoSUS objeto)
        {
            objeto.NumeroCartao = Convert.ToString(dataReader["numero_cartao"]);
            objeto.DataEmissao = Convert.ToDateTime(dataReader["data_emissao"]);
            objeto.ViaCartao = Convert.ToInt32(dataReader["via_cartao"]);
            if (objeto.Usuario == null)
            {
                objeto.Usuario = new Usuario();
                try
                {
                    objeto.Usuario.Codigo = Convert.ToInt32(dataReader["co_usuario"]);
                }
                catch (InvalidCastException)
                {
                    //Devido a cadastros na tabela que estão sem código de usuário,
                    //será atribuido o usuário de Maurilio até resolvermos o problema.
                    //05/11/2010 - Ozias
                    objeto.Usuario.Codigo = 1;
                }
            }
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, ControleCartaoSUS objeto)
        {
            throw new NotImplementedException();
        }

        public List<ControleCartaoSUS> PesquisarPorNumeroCartao(string numeroCartao)
        {
            sqlText.Append("select * from tb_pms_controle_cns ");
            sqlText.Append("where numero_cartao = :NumeroCartao ");

            parametros.Add(new OracleParameter("NumeroCartao", OracleDbType.Varchar2));
            parametros[0].Value = numeroCartao;

            List<ControleCartaoSUS> controles = new List<ControleCartaoSUS>();
            ControleCartaoSUS controle = null;

            DataSet dataReader = DALOracle.ExecuteReaderDs(CommandType.Text, sqlText.ToString(), parametros.ToArray());
            sqlText.Remove(0, sqlText.Length);
            parametros.Clear();

            foreach (DataRow drc in dataReader.Tables[0].Rows)
            {
                controle = new ControleCartaoSUS();
                MontarObjeto(drc, controle);
                controles.Add(controle);
            }            

            return controles;
        }



        protected override void MontarObjeto(DataRow drc, ControleCartaoSUS objeto)
        {
            objeto.NumeroCartao = Convert.ToString(drc["numero_cartao"]);
            objeto.DataEmissao = Convert.ToDateTime(drc["data_emissao"]);
            objeto.ViaCartao = Convert.ToInt32(drc["via_cartao"]);
            if (objeto.Usuario == null)
            {
                objeto.Usuario = new Usuario();
                try
                {
                    objeto.Usuario.Codigo = Convert.ToInt32(drc["co_usuario"]);
                }
                catch (InvalidCastException)
                {
                    //Devido a cadastros na tabela que estão sem código de usuário,
                    //será atribuido o usuário de Maurilio até resolvermos o problema.
                    //05/11/2010 - Ozias
                    objeto.Usuario.Codigo = 1;
                }
            }
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, ControleCartaoSUS objeto)
        {
            throw new NotImplementedException();
        }
    }
}
