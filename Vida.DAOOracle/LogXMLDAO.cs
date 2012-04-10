using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data;
using System.Xml.Serialization;
using ViverMais.Model;
using System.IO;
using System.Xml;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Reflection;
using ViverMais.DAL;

namespace ViverMais.DAOOracle
{
    public class DAOLogXml : ADAO<LogViverMaisXML>
    {
        //Lista de tipos que serão aplicados o Log
        //deve adicionar no construtor da classe
        protected List<Type> tipos = new List<Type>();

        public DAOLogXml()
        {
            //Adiciona os tipos necessários para chamar o Log
            tipos.Add(typeof(Paciente));
            tipos.Add(typeof(ControleDocumento));
            tipos.Add(typeof(Documento));
            tipos.Add(typeof(Endereco));
            tipos.Add(typeof(ControleEndereco));
        }


        /// <summary>
        ///  Método responsável por salvar log
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser salvo</typeparam>
        /// <param name="trans">Deve-se passar a transação porque The Best não fez o singleton</param>
        /// <param name="objeto">Objeto que será gerado o Log</param>
        /// <param name="evento">Código do evento que foi gerado. Ex: Salvar, Atualização...</param>
        public void SalvarLog<T>(T objeto, int evento)
        {
            LogViverMaisXML ViverMaisXml = new LogViverMaisXML();

            //Resgata o Usuário corrente
            string nomeUsuario = "Não Definido";
            if (HttpContext.Current != null && HttpContext.Current.Equals(""))
                nomeUsuario = HttpContext.Current.User.Identity.Name;
            ViverMaisXml.Usuario = nomeUsuario;
            ViverMaisXml.Data = DateTime.Now;
            ViverMaisXml.LogEvento = evento;

            //Resgata o nome da entidade
            Type type = typeof(T);
            ViverMaisXml.Entidade = type.Name;

            //Serializa o objeto em XML
            //FileStream fileStream = new FileStream("Serialization.XML", FileMode.Create);
            //XmlSerializer xmlSerializer = new XmlSerializer(type);
            //xmlSerializer.Serialize(fileStream, objeto);
            //ViverMaisXml.Dados = fileStream.ToString();
            try
            {
                ViverMaisXml.Dados = HelperXmlSerializer.ToXml(objeto, type);
            }
            catch (Exception e)
            {
                throw new Exception("Problema na Reflexão do Objeto no momento de geração de Log. Por favor entre em contato com o NGI");
                return;
            }

            //Percorre o Xml para resgatar o valor do código da entidade
            //Representação do XML para entendimento : 
            //<Estabelecimento codigo="100">
            //int posicaoInicioCodigo = fileStream.ToString().IndexOf("=") + 2;
            //int posicaoCodigo = fileStream.ToString().IndexOf(">", posicaoInicioCodigo) - 2;
            //string codigoEntidade =
            //    fileStream.ToString().Substring(posicaoInicioCodigo, posicaoCodigo - posicaoInicioCodigo);

            //ViverMaisXml.CodigoEntidade = codigoEntidade;

            this.ExecutarSalvar(ViverMaisXml);
        }


        /// <summary>
        ///  Método responsável por salvar log
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser salvo</typeparam>
        /// <param name="trans">Deve-se passar a transação porque The Best não fez o singleton</param>
        /// <param name="objeto">Objeto que será gerado o Log</param>
        /// <param name="evento">Código do evento que foi gerado. Ex: Salvar, Atualização...</param>
        public void SalvarLog<T>(ref OracleTransaction trans, T objeto, int evento)
        {
            //Chama a função para gerar log através de xml
            //HOJE ESTA FUNÇÃO ESTÁ RESTRITA A algumas classes
            if (tipos.Contains(typeof(T)))
            {
                LogViverMaisXML ViverMaisXml = new LogViverMaisXML();

                //Resgata o Usuário corrente
                string nomeUsuario = "Não Definido";
                if (HttpContext.Current != null && !HttpContext.Current.Equals(""))
                {
                    nomeUsuario = ((Usuario)HttpContext.Current.Session["Usuario"]).Codigo.ToString();
                    //nomeUsuario = HttpContext.Current.User.Identity.Name;
                }
                ViverMaisXml.Usuario = nomeUsuario;
                ViverMaisXml.Data = DateTime.Now;
                ViverMaisXml.LogEvento = evento;

                //Resgata o nome da entidade
                Type type = typeof(T);
                ViverMaisXml.Entidade = type.Name;

                //Serializa o objeto em XML
                try
                {
                    ViverMaisXml.Dados = HelperXmlSerializer.ToXml(objeto, type);
                }
                catch (Exception e)
                {
                    throw new Exception("Problema na Reflexão do Objeto no momento de geração de Log. Por favor entre em contato com o NGI");
                    return;
                }

                //Percorre o Xml para resgatar o valor do código da entidade
                //Representação do XML para entendimento : 
                //<Estabelecimento codigo="100">
                //int posicaoInicioCodigo = fileStream.ToString().IndexOf("=") + 2;
                //int posicaoCodigo = fileStream.ToString().IndexOf(">", posicaoInicioCodigo) - 2;
                //string codigoEntidade =
                //    fileStream.ToString().Substring(posicaoInicioCodigo, posicaoCodigo - posicaoInicioCodigo);

                //ViverMaisXml.CodigoEntidade = codigoEntidade;

                this.ExecutarSalvar(ViverMaisXml);
            }
        }

        private void ExecutarSalvar(LogViverMaisXML ViverMaisXml)
        {
            sqlText.Append("INSERT INTO TB_PMS_LOG_EVENTOS_XML (CO_LOGViverMaisXML, ENTIDADE, DADOS,DATA,USUARIO,LOGEVENTO)");
            sqlText.Append("VALUES (ViverMais_LOG_XML_SEQUENCE.NextVal, :Entidade,:Dados,:Datalog,:Usuario,:LogEvento)");

            parametros.Add(new OracleParameter("Entidade", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Dados", OracleDbType.Clob));
            parametros.Add(new OracleParameter("Datalog", OracleDbType.Date));
            parametros.Add(new OracleParameter("Usuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("LogEvento", OracleDbType.Int32));

            parametros[0].Value = ViverMaisXml.Entidade;
            parametros[1].Value = ViverMaisXml.Dados;
            parametros[2].Value = ViverMaisXml.Data;
            parametros[3].Value = ViverMaisXml.Usuario;
            parametros[4].Value = ViverMaisXml.LogEvento;

            DALOracle.ExecuteNonQuery(CommandType.Text, sqlText.ToString(), parametros.ToArray());
        }

        private void ExecutarSalvar(ref OracleTransaction trans, LogViverMaisXML ViverMaisXml)
        {
            sqlText.Append("INSERT INTO TB_PMS_LOG_EVENTOS_XML (CO_LOGViverMaisXML,ENTIDADE, DADOS,DATA,USUARIO,LOGEVENTO)");
            sqlText.Append("VALUES (ViverMais_LOG_XML_SEQUENCE.NextVal,:Entidade,:Dados,:Datalog,:Usuario,:LogEvento)");

            parametros.Add(new OracleParameter("Entidade", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("Dados", OracleDbType.Clob));
            parametros.Add(new OracleParameter("Datalog", OracleDbType.Date));
            parametros.Add(new OracleParameter("Usuario", OracleDbType.Varchar2));
            parametros.Add(new OracleParameter("LogEvento", OracleDbType.Int32));

            parametros[0].Value = ViverMaisXml.Entidade;
            parametros[1].Value = ViverMaisXml.Dados;
            parametros[1].Value = ViverMaisXml.Data;
            parametros[1].Value = ViverMaisXml.Usuario;
            parametros[1].Value = ViverMaisXml.LogEvento;

            DALOracle.ExecuteNonQuery(ref trans, CommandType.Text, sqlText.ToString(), parametros.ToArray());
        }

        #region Métodos da classe abstrata

        protected override void GerarQueryPesquisaTodos()
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryCadastro()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosCadastro(LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }

        protected override void GerarQueryAtualizacao()
        {
            throw new NotImplementedException();
        }

        protected override void GerarParametrosAtualizacao(LogViverMaisXML objeto)
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

        protected override void GeraParametrosPesquisaPorCodigo(LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(OracleDataReader dataReader, LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }

        protected override void MontarObjeto(DataRow drc, LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(OracleDataReader dataReader, LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }

        protected override void SetarCodigoObjeto(DataRow dataRow, LogViverMaisXML objeto)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
