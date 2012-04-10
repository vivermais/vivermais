﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.Web;
using System.Threading;
using Oracle.DataAccess.Client;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAL;

namespace ViverMais.DAO.Paciente
{
    public class PacienteDAO : ViverMaisServiceFacadeDAO, IPaciente
    {

        string connectionString = string.Empty;

        //public string ConnectionString
        //{
        //    get
        //    {
        //        if (connectionString == string.Empty)
        //        {
        //            if (ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString.Contains(Session.Connection.ConnectionString))
        //            {
        //                this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisGaiaConnectionString;
        //            }
        //            if (ViverMais.DAO.Properties.Resources.ViverMaisMorfeuConnectionString.Contains(Session.Connection.ConnectionString))
        //            {
        //                this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisMorfeuConnectionString;
        //            }
        //            if (ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString.Contains(Session.Connection.ConnectionString))
        //            {
        //                this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisProducaoConectionString;
        //            }
        //            if (ViverMais.DAO.Properties.Resources.ViverMaisServidorUrgenciaConnectionString.Contains(Session.Connection.ConnectionString))
        //            {
        //                this.connectionString = ViverMais.DAO.Properties.Resources.ViverMaisServidorUrgenciaConnectionString;
        //            }

        //            this.connectionString = this.connectionString
        //                                        .Replace("SERVER", "Data Source")
        //                                        .Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=")
        //                                        .Replace("))(CONNECT", ")))(CONNECT")
        //                                        .Replace("uid", "User Id")
        //                                        .Replace("pwd", "Password");
        //        }
        //        return connectionString;
        //    }
        //}


        #region IPaciente Members

        public PacienteDAO()
        {
        }

        void IPaciente.CadastrarPaciente<T>(T paciente)
        {
            //using (Session.BeginTransaction())
            ////using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            //{
            //    try
            //    {

            //        Session.BeginTransaction();
            //        ICriteria criteria = Session.CreateCriteria(typeof(ViverMais.Model.CartaoBase));
            //        IList<CartaoBase> cartoes = criteria.Add(Expression.Eq("Atribuido", 0)).List<ViverMais.Model.CartaoBase>();
            //        if (cartoes.Count == 0)
            //            throw new Exception("Não existem números de cartão disponíveis. Favor entrar em contato de imediato com o NGI.");
            //        CartaoBase cartao = cartoes[0];
            //        ViverMais.Model.Paciente p = (ViverMais.Model.Paciente)(object)paciente;
            //        ControlePaciente controle = new ControlePaciente();
            //        controle.Codigo = DateTime.Now.ToString("yyyyMMdd-hhmm") + "-ViverMais-pms-" + Guid.NewGuid().ToString().Remove(34);
            //        Session.Save(controle);
            //        p.Codigo = controle.Codigo;
            //        Session.Save(p);
            //        CartaoSUS cns = new CartaoSUS();
            //        cns.DataAtribuicao = DateTime.Now;
            //        cns.Paciente = p;
            //        cns.Tipo = 'P';
            //        cns.Numero = cartao.Numero;
            //        Session.Save(cns);
            //        cartao.Atribuido = 1;
            //        Session.Update(cartao);
            //        Session.Transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        Session.Transaction.Rollback();
            //        throw ex;
            //    }
            //}

            //INFORMAÇÂO
            //Por questões de desempenho dessa funcionalidade, o código para
            //persistir essas entidades foi mudado do nhibernate para o "hard coded"
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();

                cmd.CommandText = "select * from tb_pms_cns where st_atribuido=0";
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    string cns = reader["co_cartao"].ToString();
                    reader.Close();

                    try
                    {
                        con.BeginTransaction();
                        cmd = con.CreateCommand();
                        cmd.CommandText = "update tb_pms_cns set st_atribuido=1 where co_cartao='" + cns + "'";
                        cmd.ExecuteNonQuery();

                        string co_paciente = DateTime.Now.ToString("yyyyMMdd-hhmm") + "-ViverMais-pms-" + Guid.NewGuid().ToString().Remove(34);

                        cmd = con.CreateCommand();
                        cmd.CommandText = "insert into tb_ms_controle_usuario(co_usuario) values('" + co_paciente + "')";
                        cmd.ExecuteNonQuery();

                        ViverMais.Model.Paciente p = (ViverMais.Model.Paciente)(object)paciente;
                        cmd = con.CreateCommand();
                        cmd.CommandText =
                            "insert into tb_ms_usuario(" +
                                "co_usuario," +
                                "no_usuario," +
                                "no_mae," +
                                "no_pai," +
                                "dt_nascimento," +
                                "co_sexo," +
                                "co_raca," +
                                "st_frequenta_escola," +
                                (p.MunicipioNascimento == null ? "" : "co_municipio_nasc,") +
                                "st_vivo," +
                                (p.Pais == null ? "" : "co_pais,") +
                                "ds_email" +
                                ")values(" +
                                "'" + co_paciente + "'," +
                                "'" + p.Nome + "'," +
                                "'" + p.NomeMae + "'," +
                                "'" + p.NomePai + "'," +
                                "'" + p.DataNascimento.ToString("dd/MM/yyyy") + "'," +
                                "'" + p.Sexo + "'," +
                                "'" + p.RacaCor.Codigo + "'," +
                                "'" + p.FrequentaEscola + "'," +
                                (p.MunicipioNascimento == null ? "" : "'" + p.MunicipioNascimento.Codigo + "',") +
                                "'" + p.Vivo + "'," +
                                (p.Pais == null ? "" : "'" + p.Pais.Codigo + "',") +
                                "'" + p.Email + "'" +
                                ")";

                        cmd.ExecuteNonQuery();

                        cmd = con.CreateCommand();
                        cmd.CommandText = "insert into tb_ms_usuario_cns_elos(" +
                            "co_numero_cartao," +
                            "tp_cartao," +
                            "dt_atribuicao," +
                            "co_usuario" +
                            ")values(" +
                            "'" + cns + "'," +
                            "'P'," +
                            "'" + DateTime.Today.ToString("dd/MM/yyyy") + "'," +
                            "'" + co_paciente + "')";
                        cmd.ExecuteNonQuery();
                        p.Codigo = co_paciente;
                        cmd.Transaction.Commit();
                    }
                    catch
                    {
                        cmd.Transaction.Rollback();
                        throw;
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    throw new Exception("Não existem números de cartão disponíveis. Favor entrar em contato de imediato com o NGI.");
                }
            }
        }

        IList<T> IPaciente.PesquisarPaciente<T>(string nomePaciente, string nomeMae, DateTime dataNascimento)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (!string.IsNullOrEmpty(nomePaciente))
                criteria.Add(Expression.Like("Nome", nomePaciente.ToUpper(), MatchMode.Start));
            if (!string.IsNullOrEmpty(nomeMae))
                criteria.Add(Expression.Like("NomeMae", nomeMae.ToUpper(), MatchMode.Start));
            if (!DateTime.MinValue.Equals(dataNascimento))
                criteria.Add(Expression.Eq("DataNascimento", dataNascimento));
            return criteria.List<T>();
        }

        IList<T> IPaciente.PesquisarPaciente<T>(string cpf, string nomePaciente, string nomeMae, DateTime dataNascimento)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (!string.IsNullOrEmpty(nomePaciente))
                criteria.Add(Expression.Like("Nome", nomePaciente.ToUpper(), MatchMode.Start));
            if (!string.IsNullOrEmpty(nomeMae))
                criteria.Add(Expression.Like("NomeMae", nomeMae.ToUpper(), MatchMode.Start));
            if (!DateTime.MinValue.Equals(dataNascimento))
                criteria.Add(Expression.Eq("DataNascimento", dataNascimento));
            return criteria.List<T>();
        }

        T IPaciente.PesquisarPacientePorCNS<T>(string numeroCartao)
        {
            string hql = "select cartao.Paciente from ViverMais.Model.CartaoSUS cartao where cartao.Numero='" + numeroCartao + "'";
            hql += " and cartao.Excluido <> '1'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IPaciente.PesquisarPacientePorRG<T>(string numeroRG)
        {
            string hql = "select doc.ControleDocumento.Paciente from ViverMais.Model.Documento doc where doc.Numero ='" + numeroRG + "'";
            //or doc.Numero+doc.Complemento ='" + numeroRG + "'
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        void IPaciente.AtualizarPaciente<T>(T paciente)
        {
            //this.Update(paciente);

            //using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            //{
            //    try
            //    {
            //        session.Transaction.Begin();
            //        if (!session.Contains(paciente))
            //        {
            //            session.Update(paciente);
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
            ViverMais.Model.Paciente p = (ViverMais.Model.Paciente)(object)paciente;
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                con.Open();
                OracleTransaction tran = con.BeginTransaction();
                OracleCommand cmd = con.CreateCommand();
                cmd.Transaction = tran;
                cmd.CommandText =
                    "update tb_ms_usuario set " +
                    "no_usuario = '" + p.Nome + "'," +
                    "no_mae = '" + p.NomeMae + "'," +
                    "no_pai = '" + p.NomePai + "'," +
                    "dt_nascimento = '" + p.DataNascimento.ToString("dd/MM/yyyy") + "'," +
                    "co_sexo = '" + p.Sexo + "'," +
                    "co_raca = '" + p.RacaCor.Codigo + "'," +
                    "st_frequenta_escola = '" + p.FrequentaEscola + "'," +
                    (p.Pais == null ? "" : "co_pais = '" + p.Pais.Codigo + "',") +
                    (p.MunicipioNascimento == null ? "" : "co_municipio_nasc = '" + p.MunicipioNascimento.Codigo + "',") +
                    "st_vivo = '" + p.Vivo + "'," +
                    "ds_email = '" + p.Email + "'" +
                    " where " +
                    "co_usuario = '" + p.Codigo + "'";
                cmd.ExecuteNonQuery();
                tran.Commit();
                con.Close();
            }
        }

        IList<T> IPaciente.ListarCartoesSUS<T>(object codigoPaciente)
        {
            string hql = "from CartaoSUS cartao where cartao.Paciente.Codigo='" + codigoPaciente + "' and cartao.Excluido = '0' order by cartao.Numero asc";
            return this.Session.CreateQuery(hql).List<T>();
        }

        IList<T> IPaciente.BuscarPorModificacao<T>(DateTime dataInicial, DateTime dataFinal)
        {
            return Session.CreateCriteria(typeof(ControlePaciente))
                .Add(Expression.Between("DataOperacao", dataInicial, dataFinal)).List<T>();
        }

        IList<T> IPaciente.BuscarCodigoUsuarioPorModificacaoDoDia<T>(DateTime data)
        {
            //string sql = "select co_usuario from tb_ms_controle_usuario controle";
            //sql = +" inner join tb_ms_usuario usuario";
            //sql = +" on controle.co_usuario = usuario.co_usuario";
            //sql = +" where dt_operacao is between to_date('20021219')";

            string sql = "select controle.co_usuario from tb_ms_controle_usuario controle";
            sql += " where TO_CHAR(dt_operacao,'DD/MM/YYYY HH24:MI')";
            sql +=  " between '" + data.ToString("dd/MM/yyyy") + " 00:00' and '" + data.ToString("dd/MM/yyyy") + " 23:59'";

            return this.Session.CreateSQLQuery(sql).List<T>();
        }

        IList<T> IPaciente.ListarControleCartaoSUS<T>(object numeroCartao)
        {
            string hql = "from ControleCartaoSUS cartao where cartao.NumeroCartao='" + numeroCartao + "'";
            return this.Session.CreateQuery(hql).List<T>();
        }

        IList<T> IPaciente.ListarDocumentos<T>(string codigoPaciente)
        {
            string hql = "from ViverMais.Model.Documento doc where doc.ControleDocumento.Paciente.Codigo='" + codigoPaciente + "'";
            return this.Session.CreateQuery(hql).List<T>();
        }

        void IPaciente.AtualizaControlePaciente<T>(T controlePaciente)
        {
            ControlePaciente controle = (ControlePaciente)(object)controlePaciente;
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                con.Open();
                OracleCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    "update tb_ms_controle_usuario set " +
                    "ST_CONTROLE = '" + controle.Controle.ToString() + "'," +
                    "ST_EXCLUIDO = '0'," +
                    "DT_OPERACAO = '" + controle.DataOperacao.ToString("dd/MM/yyyy") + "'," +
                    "NU_VERSAO = '" + controle.NumeroVersao + "' where " +
                    "co_usuario='" + controle.Codigo + "'";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        void IPaciente.SalvarControlePaciente<T>(T controlePaciente)
        {
            ControlePaciente controle = (ControlePaciente)(object)controlePaciente;
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                try
                {
                    con.Open();
                    OracleCommand cmd = con.CreateCommand();

                    cmd.CommandText =
                        "insert into tb_ms_controle_usuario(" +
                        "co_usuario," +
                        "ST_CONTROLE,ST_EXCLUIDO,DT_OPERACAO,NU_VERSAO)values(" +
                        "'" + controle.Codigo + "'," +
                        "'" + controle.Controle + "')" +
                        "'" + controle.Excluido + "'," +
                        "'" + controle.DataOperacao + "'," + controle.NumeroVersao + ")";
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

            }
        }

        void IPaciente.AtualizarDocumento<T>(T documento)
        {
            //using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            //{
            //    try
            //    {
            //        session.Transaction.Begin();
            //        if (!session.Contains(documento))
            //        {
            //            session.Update(documento);
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

            ViverMais.Model.Documento d = (ViverMais.Model.Documento)(object)documento;
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                con.Open();
                //OracleTransaction tran = con.BeginTransaction();
                OracleCommand cmd = con.CreateCommand();
                //cmd.Transaction = tran;

                cmd.CommandText =
                    "update rl_ms_usuario_documentos set " +
                    (d.OrgaoEmissor == null ? "" : "co_orgao_emissor = '" + d.OrgaoEmissor.Codigo + "',") +
                    "no_cartorio = '" + d.NomeCartorio + "'," +
                    "nu_livro = '" + d.NumeroLivro + "'," +
                    "nu_folha = '" + d.NumeroFolha + "'," +
                    "nu_termo = '" + d.NumeroTermo + "'," +
                    (!d.DataEmissao.HasValue ? "" : "dt_emissao='" + d.DataEmissao.Value.ToString("dd/MM/yyyy") + "',") +
                    (!d.DataChegadaBrasil.HasValue ? "" : "dt_chegada_brasil = '" + d.DataChegadaBrasil.Value.ToString("dd/MM/yyyy") + "',") +
                    "nu_portaria = '" + d.NumeroPortaria + "'," +
                    (!d.DataNaturalizacao.HasValue ? "" : "dt_naturalizacao = '" + d.DataNaturalizacao.Value.ToString("dd/MM/yyyy") + "',") +
                    "nu_documento = '" + d.Numero + "'," +
                    "nu_serie = '" + d.Serie + "'," +
                    "nu_zona_eleitoral = '" + d.ZonaEleitoral + "'," +
                    "nu_secao_eleitoral = '" + d.SecaoEleitoral + "'," +
                    (d.UF == null ? "" : "sg_uf = '" + d.UF.Sigla + "', ") +
                    "nu_documento_compl = '" + d.Complemento + "'" +
                    "where " +
                    "co_usuario='" + d.ControleDocumento.Paciente.Codigo + "' and " +
                    "co_tipo_documento = '" + d.ControleDocumento.TipoDocumento.Codigo + "'";

                cmd.ExecuteNonQuery();
                //tran.Commit();
                con.Close();
            }
        }

        void IPaciente.SalvarDocumento<T>(T documento)
        {
            //using (Session.BeginTransaction())
            //{
            //    try
            //    {
            //        ViverMais.Model.Documento doc = (ViverMais.Model.Documento)(object)documento;
            //        Session.SaveOrUpdate(doc.ControleDocumento);
            //        Session.SaveOrUpdate(doc);
            //        Session.Transaction.Commit();
            //    }
            //    catch (Exception ex)
            //    {
            //        Session.Transaction.Rollback();
            //        throw ex;
            //    }
            //}
            ViverMais.Model.Documento d = (ViverMais.Model.Documento)(object)documento;
            using (OracleConnection con = new OracleConnection(DALOracle.ConnectionString))
            {
                try
                {
                    con.Open();
                    //OracleTransaction tran = con.BeginTransaction();
                    OracleCommand cmd = con.CreateCommand();
                    //cmd.Transaction = tran;

                    cmd.CommandText =
                        "insert into tb_ms_controle_documento(" +
                        "co_tipo_documento," +
                        "co_usuario)values(" +
                        "'" + d.ControleDocumento.TipoDocumento.Codigo + "'," +
                        "'" + d.ControleDocumento.Paciente.Codigo + "')";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText =
                        "insert into rl_ms_usuario_documentos( " +
                        (d.OrgaoEmissor == null ? "" : "co_orgao_emissor, ") +
                        "no_cartorio, " +
                        "nu_livro, " +
                        "nu_folha, " +
                        "nu_termo, " +
                        (!d.DataEmissao.HasValue ? "" : "dt_emissao, ") +
                        (!d.DataChegadaBrasil.HasValue ? "" : "dt_chegada_brasil,") +
                        "nu_portaria, " +
                        (!d.DataNaturalizacao.HasValue ? "" : "dt_naturalizacao,") +
                        "nu_documento, " +
                        "nu_serie, " +
                        "nu_zona_eleitoral, " +
                        "nu_secao_eleitoral, " +
                        (d.UF == null ? "" : "sg_uf ,") +
                        "nu_documento_compl, " +
                        "co_usuario, " +
                        "co_tipo_documento)values(" +
                        (d.OrgaoEmissor == null ? "" : "'" + d.OrgaoEmissor.Codigo + "', ") +
                        "'" + d.NomeCartorio + "', " +
                        "'" + d.NumeroLivro + "', " +
                        "'" + d.NumeroFolha + "', " +
                        "'" + d.NumeroTermo + "', " +
                        (!d.DataEmissao.HasValue ? "" : "'" + d.DataEmissao.Value.ToString("dd/MM/yyyy") + "', ") +
                        (!d.DataChegadaBrasil.HasValue ? "" : "'" + d.DataChegadaBrasil.Value.ToString("dd/MM/yyyy") + "', ") +
                        "'" + d.NumeroPortaria + "', " +
                        (!d.DataNaturalizacao.HasValue ? "" : "'" + d.DataNaturalizacao.Value.ToString("dd/MM/yyyy") + "', ") +
                        "'" + d.Numero + "', " +
                        "'" + d.Serie + "', " +
                        "'" + d.ZonaEleitoral + "', " +
                        "'" + d.SecaoEleitoral + "', " +
                        (d.UF == null ? "" : "'" + d.UF.Sigla + "', ") +
                        "'" + d.Complemento + "', " +
                        "'" + d.ControleDocumento.Paciente.Codigo + "', " +
                        "'" + d.ControleDocumento.TipoDocumento.Codigo + "'" +
                        ")";

                    //(d.OrgaoEmissor == null ? "" : "co_orgao_emissor = '" + d.OrgaoEmissor.Codigo + "',") +
                    //"no_cartorio = '" + d.NomeCartorio + "'," +
                    //"nu_livro = '" + d.NumeroLivro + "'," +
                    //"nu_folha = '" + d.NumeroFolha + "'," +
                    //"nu_termo = '" + d.NumeroTermo + "'," +
                    //(!d.DataEmissao.HasValue ? "" : "dt_emissao='" + d.DataEmissao.Value.ToString("dd/MM/yyyy") + "',") +
                    //(!d.DataChegadaBrasil.HasValue ? "" : "dt_chegada_brasil = '" + d.DataChegadaBrasil.Value.ToString("dd/MM/yyyy") + "',") +
                    //"nu_portaria = '" + d.NumeroPortaria + "'," +
                    //(!d.DataNaturalizacao.HasValue ? "" : "dt_naturalizacao = '" + d.DataNaturalizacao.Value.ToString("dd/MM/yyyy") + "',") +
                    //"nu_documento = '" + d.Numero + "'," +
                    //"nu_serie = '" + d.Serie + "'," +
                    //"nu_zona_eleitoral = '" + d.ZonaEleitoral + "'," +
                    //"nu_secao_eleitoral = '" + d.SecaoEleitoral + "'," +
                    //(d.UF == null ? "" : "sg_uf = '" + d.UF.Sigla + "', ") +
                    //"nu_documento_compl = '" + d.Complemento + "'" +
                    //"where " +
                    //"co_usuario='" + d.ControleDocumento.Paciente.Codigo + "' and " +
                    //"co_tipo_documento = '" + d.ControleDocumento.TipoDocumento.Codigo + "'";

                    cmd.ExecuteNonQuery();
                    //tran.Commit();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    con.Close();
                }

            }
        }

        void IPaciente.AtualizarMotivo<T>(T motivo)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    if (!session.Contains(motivo))
                    {
                        session.Update(motivo);
                    }
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
                finally
                {
                    session.Close();
                }
            }
        }


        void IPaciente.SalvarMotivo<T>(T motivo)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    if (!session.Contains(motivo))
                    {
                        session.Save(motivo);
                    }
                    session.Transaction.Commit();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        T IPaciente.BuscarPacientePorCPF<T>(string cpf) 
        {
            string hql = "Select doc.ControleDocumento.Paciente from ViverMais.Model.Documento doc where doc.Numero = '" + cpf + "' and doc.ControleDocumento.TipoDocumento.Codigo='02'";
            return this.Session.CreateQuery(hql).List<T>().First();
        }


        T IPaciente.BuscarDocumento<T>(object codigoTipoDocumento, object codigoPaciente)
        {
            string hql = "from ViverMais.Model.Documento doc where doc.ControleDocumento.Paciente.Codigo='" + codigoPaciente + "' and doc.ControleDocumento.TipoDocumento.Codigo='" + codigoTipoDocumento + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IPaciente.BuscarMotivo<T>(object codigoPaciente, object cnes)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * ");
            sql.Append("from rl_ms_usuario_motivo um ");
            sql.Append("inner join tb_ms_usuario u on u.co_usuario = um.co_usuario ");
            sql.Append("inner join tb_ms_motivo m on m.co_motivo = um.co_motivo ");
            sql.Append("where u.co_usuario = :CodigoUsuario ");

            OracleConnection conn = new OracleConnection();
            conn.ConnectionString = DALOracle.ConnectionString;
            OracleCommand command = conn.CreateCommand();
            OracleDataReader dataReader = null;
            List<MotivoCadastroPaciente> lista = new List<MotivoCadastroPaciente>();
            MotivoCadastroPaciente motivo = null;
            try
            {
                conn.Open();
                command.CommandText = sql.ToString();
                command.Parameters.Add("CodigoUsuario", OracleDbType.Varchar2).Value = codigoPaciente;
                command.Prepare();
                dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    motivo = new MotivoCadastroPaciente();
                    motivo.Motivo = new MotivoCadastro();
                    motivo.Motivo.Codigo = Convert.ToInt32(dataReader["co_motivo"]);
                    motivo.Motivo.Motivo = Convert.ToString(dataReader["no_motivo"]);
                    motivo.Cnes = Convert.ToString(dataReader["co_cnes"]);
                    motivo.CodigoDocumentoReferencia = Convert.ToInt32(dataReader["co_doc_ref"]);
                    motivo.CnsMae = Convert.ToString(dataReader["CO_NUMERO_CARTAO_MAE"]);
                    motivo.NumeroDocumento = Convert.ToString(dataReader["NU_DOCUMENTO"]);
                    try
                    {
                        motivo.DataOperacao = Convert.ToDateTime(dataReader["DT_OPERACAO"]);
                    }
                    catch (Exception)
                    {
                        motivo.DataOperacao = DateTime.MinValue;
                    }
                    motivo.Controle = Convert.ToString(dataReader["ST_CONTROLE"]);
                    motivo.Excluido = Convert.ToChar(dataReader["ST_EXCLUIDO"]);
                    motivo.NumeroVersao = Convert.ToInt32(dataReader["NU_VERSAO"]);
                    motivo.Paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(codigoPaciente);
                    lista.Add(motivo);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (!dataReader.IsClosed)
                    dataReader.Close();
                if (conn.State != System.Data.ConnectionState.Closed)
                    conn.Close();
            }

            if (lista.Count != 0)
                return (T)((object)lista[0]);
            else
            {
                motivo = new MotivoCadastroPaciente();
                motivo.Paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(codigoPaciente);
                motivo.Cnes = cnes.ToString();
                motivo.Motivo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<MotivoCadastro>(99);
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(motivo);
                return (T)((object)motivo);
            }

        }

        T IPaciente.BuscarMotivo<T>(object codigoPaciente)
        {
            string hql = "from ViverMais.Model.MotivoCadastroPaciente motivo where motivo.Paciente.Codigo='" + codigoPaciente + "'";
            IList<T> lista = this.Session.CreateQuery(hql).List<T>();
            return lista[0];
        }

        T IPaciente.BuscaControlePaciente<T>(object codigoPaciente)
        {
            string hql = "from ViverMais.Model.ControlePaciente controle where controle.Codigo = '" + codigoPaciente + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion

        T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        {
            string hql = "from ViverMais.Model.Paciente paciente where paciente.Codigo='" + codigo + "'";
            return this.Session.CreateQuery(hql).UniqueResult<T>();
        }

    }
}
