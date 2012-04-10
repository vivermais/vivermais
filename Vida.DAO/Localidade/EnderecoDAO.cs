using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using NHibernate;
using Oracle.DataAccess.Client;

namespace ViverMais.DAO.Localidade
{
    public class EnderecoDAO : ViverMaisServiceFacadeDAO, IEndereco
    {
        string connectionString = string.Empty;

        public string ConnectionString
        {
            get
            {
                if (connectionString == string.Empty)
                {
                    if (ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString.Contains(Session.Connection.ConnectionString))
                    {
                        this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString;
                    }
                    if (ViverMais.DAO.Properties.Resources.ViverMaisMorfeuConnectionString.Contains(Session.Connection.ConnectionString))
                    {
                        this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisMorfeuConnectionString;
                    }
                    if (ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString.Contains(Session.Connection.ConnectionString))
                    {
                        this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString;
                    }
                    this.connectionString = this.connectionString
                            .Replace("SERVER", "Data Source")
                            .Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=")
                            .Replace("))(CONNECT", ")))(CONNECT")
                            .Replace("uid", "User Id")
                            .Replace("pwd", "Password");
                }
                return connectionString;
            }
        }

        public EnderecoDAO()
        {
          
        }

        #region IEndereco Members

        T IEndereco.BuscarPorPaciente<T>(string co_paciente)
        {
            string hql = "select eu.Endereco from ViverMais.Model.EnderecoUsuario eu where eu.CodigoPaciente='" + co_paciente + "'";
            hql += " and eu.Excluido = '0'";
            return Session.CreateQuery(hql).UniqueResult<T>();
            throw new NotImplementedException();
        }

        void IEndereco.CadastrarEndereco<T, P>(T endereco, P paciente)
        {
            //using (Session.BeginTransaction())
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    session.BeginTransaction();
                    ViverMais.Model.ControleEndereco controle = new ViverMais.Model.ControleEndereco();
                    controle.Codigo = DateTime.Now.ToString("yyyyMMdd-hhmm") + "-endereco-pms-" + Guid.NewGuid().ToString().Remove(21);
                    session.Save(controle);
                    ((ViverMais.Model.Endereco)(object)endereco).ControleEndereco = controle;
                    session.Save(endereco);
                    ViverMais.Model.EnderecoUsuario eu = new ViverMais.Model.EnderecoUsuario();
                    eu.Endereco = (ViverMais.Model.Endereco)(object)endereco;
                    eu.CodigoPaciente = ((ViverMais.Model.Paciente)(object)paciente).Codigo;
                    eu.TipoEndereco = session.Get<ViverMais.Model.TipoEndereco>("01");
                    session.Save(eu);
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        void IEndereco.AtualizarEndereco<T>(T endereco)
        {
            //this.Update(endereco);

            //using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            //{
            //    try
            //    {
            //        session.Transaction.Begin();
            //        if (!session.Contains(endereco))
            //        {
            //            session.Update(endereco);
            //        }
            //        session.Transaction.Commit();
            //    }
            //    catch (Exception e)
            //    {
            //        session.Transaction.Rollback();
            //        throw e;
            //    }
            //    finally
            //    {
            //        session.Close();
            //    }
            //}
            ViverMais.Model.Endereco e = (ViverMais.Model.Endereco)(object)endereco;
            //string strCon = Session.Connection.ConnectionString
            //                .Replace("SERVER", "Data Source")
            //                .Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=")
            //                .Replace("))(CONNECT", ")))(CONNECT")
            //                .Replace("uid", "User Id")
            //                + "Password=salvador;";
            using (OracleConnection con = new OracleConnection(this.ConnectionString))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                try
                {
                    cmd.Transaction = con.BeginTransaction();
                    cmd.CommandText =
                        "update tb_ms_endereco set " +
                        "no_logradouro = '" + e.Logradouro + "'," +
                        "nu_logradouro = '" + e.Numero + "'," +
                        "no_compl_logradouro = '" + e.Complemento + "'," +
                        "no_bairro = '" + e.Bairro + "'," +
                        "co_cep = '" + e.CEP + "'," +
                        "nu_ddd = '" + e.DDD + "'," +
                        "nu_telefone = '" + e.Telefone + "'," +
                        "co_municipio = '" + e.Municipio.Codigo + "'" +
                        " where " +
                        "co_endereco = '" + e.Codigo + "'";
                    //cmd.Parameters.Add("no_logradouro", e.Logradouro);
                    //cmd.Parameters.Add("nu_logradouro", e.Numero);
                    //cmd.Parameters.Add("no_compl_logradouro", e.Complemento);
                    //cmd.Parameters.Add("no_bairro", e.Bairro);
                    //cmd.Parameters.Add("co_cep", e.CEP);
                    //cmd.Parameters.Add("nu_ddd", e.DDD);
                    //cmd.Parameters.Add("nu_telefone", e.Telefone);
                    //cmd.Parameters.Add("co_municipio", e.Municipio.Codigo);
                    //cmd.Parameters.Add("co_endereco", e.Codigo);
                    cmd.ExecuteNonQuery();
                    cmd.Transaction.Commit();
                    con.Close();
                }
                catch (Exception x)
                {
                    cmd.Transaction.Rollback();
                    throw x;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        #endregion

        //#region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    return this.FindByPrimaryKey<T>(codigo);
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    this.Save(obj);
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
