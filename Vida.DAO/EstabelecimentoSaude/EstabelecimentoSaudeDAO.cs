using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using System.Data;
using System.Xml;
using Oracle.DataAccess.Client;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAL;

namespace ViverMais.DAO.EstabelecimentoSaude
{
    public class EstabelecimentoSaudeDAO : ViverMaisServiceFacadeDAO, IEstabelecimentoSaude
    {
        #region IEstabelecimentoSaude Members

        public EstabelecimentoSaudeDAO()
        {

        }

        #endregion

        #region IEstabelecimentoSaude Members

        T IEstabelecimentoSaude.BuscarEstabelecimentoPorCNES<T>(string cnes)
        {
            string hql = "FROM ViverMais.Model.EstabelecimentoSaude es ";
            hql += " WHERE es.CNES='" + cnes + "'";
            hql += " AND es.CNES <> '0000000'";

            T resultados = Session.CreateQuery(hql).UniqueResult<T>();
            return resultados;
        }

        IList<T> IEstabelecimentoSaude.BuscarEstabelecimentoPorNomeFantasia<T>(string nome)
        {
            string hql = "FROM ViverMais.Model.EstabelecimentoSaude es ";
            //hql += "WHERE MYTRANSLATE(es.NomeFantasia) LIKE '" + GenericsFunctions.RemoveDiacritics(nome).ToUpper() + "%'";
            //hql += "WHERE UPPER(CONVERT(es.NomeFantasia, 'US7ASCII'))  LIKE '" + GenericsFunctions.RemoveDiacritics(nome).ToUpper() + "%'";
            hql += " WHERE TRANSLATE(UPPER(es.NomeFantasia),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')  LIKE '" + GenericsFunctions.RemoveDiacritics(nome).ToUpper() + "%'";
            hql += " AND es.CNES <> '0000000'";
            hql += " ORDER BY es.NomeFantasia";

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IEstabelecimentoSaude.BuscarUnidadeDistrito<T>(int distrito)
        {
            string hql = "FROM ViverMais.Model.EstabelecimentoSaude es";
            hql += " WHERE es.Bairro.Distrito.Codigo = '" + distrito + "'";
            hql += " AND es.CNES <> '0000000' ORDER BY es.NomeFantasia";
            //and es.NomeFantasia <> 'NAO SE APLICA'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IEstabelecimentoSaude.BuscarEstabelecimentoPorNaturezaOrganizacao<T>(string co_natureza)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.EstabelecimentoSaude AS es WHERE es.NaturezaOrganizacao.Codigo = '" + co_natureza + "' AND es.RazaoSocial <> 'NAO SE APLICA'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IEstabelecimentoSaude.ListarEstabelecimentosForaRedeMunicipal<T>()
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.EstabelecimentoSaude AS es WHERE es.NaturezaOrganizacao.Codigo <> '" + NaturezaOrganizacao.ADMINISTRACAO_DIRETA_SAUDE + "' ORDER BY es.RazaoSocial";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IEstabelecimentoSaude.BuscarPorBairro<T>(int co_bairro)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.EstabelecimentoSaude AS e WHERE e.Bairro.Codigo = " + co_bairro + "";
            return Session.CreateQuery(hql).List<T>();
        }

        private void SalvarStatusImportacao(ImportacaoCNES importacao)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    session.Transaction.Begin();
                    session.Update(importacao);
                    session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    session.Transaction.Rollback();
                    throw ex;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        void IEstabelecimentoSaude.ImportarEstabelecimento<T, A>(T xml, A importacao)
        {
            ImportacaoCNES import = (ImportacaoCNES)(object)importacao;
            XmlDocument doc = (XmlDocument)(object)xml;
            //string conexao = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.22.6.16)(PORT=1521)))" +
            //"(CONNECT_DATA=(SERVICE_NAME=ViverMais)));User Id=ngi;Password=salvador;";

            ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession();
            string stringconexao = session.Connection.ConnectionString;

            object objetocodigo = session.CreateQuery("SELECT MAX(i.Codigo) FROM ImportacaoCNES AS i").List()[0];
            import.Codigo = objetocodigo != null ? int.Parse(objetocodigo.ToString()) + 1 : 1;

            session.Transaction.Begin();
            session.Save(import);
            session.Transaction.Commit();
            session.Close();

            //string conexao = stringconexao
            //    .Replace("SERVER", "Data Source")
            //    .Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=")
            //    .Replace("))(CONNECT", ")))(CONNECT")
            //    .Replace("uid", "User Id")
            //    + "Password=salvador;";
            //    //+"Password=#Ng1s@3De$;";


            //ATENÇÃO
            //Alterar a conexão em ConexaoBancoSingle
            string conexao = ConexaoBancoSingle.conexao.Replace("SERVER", "Data Source").Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=").Replace("))(CONNECT", ")))(CONNECT").Replace("uid", "User Id").Replace("pwd", "Password");

            OracleConnection con = new OracleConnection(conexao);
            OracleCommand cmm = null;
            OracleDataReader dr = null;
            OracleCommand oraestabelecimento = null;
            OracleCommand oraprofissional = null;
            OracleCommand oravinculo = null;
            OracleCommand orasegmento = null;
            OracleCommand oraarea = null;
            OracleCommand oraequipe = null;
            OracleCommand oraequipeprof = null;

            try
            {
                con.Open();

                using (OracleTransaction tran = con.BeginTransaction())
                {
                    try
                    {
                        string path = "/ROOT/ESTABELECIMENTOS";

                        var nodes = doc.SelectNodes(path);
                        int i;
                        for (i = 0; i < nodes.Count; i++)
                        {
                            foreach (XmlNode no in nodes[i].ChildNodes)
                            {
                                XmlAttributeCollection xa = no.Attributes;
                                string UNIDADE_ID = xa["CNES"].Value;

                                cmm = new OracleCommand("SELECT CNES FROM PMS_CNES_LFCES004 WHERE CNES='" + UNIDADE_ID + "'", tran.Connection);
                                dr = cmm.ExecuteReader();

                                try
                                {
                                    if (!dr.HasRows)
                                        oraestabelecimento = StringInsertUpdateEstabelecimento(xa, tran.Connection, "inserir");
                                    else
                                        oraestabelecimento = StringInsertUpdateEstabelecimento(xa, tran.Connection, "atualizar");

                                    oraestabelecimento.ExecuteNonQuery();
                                }
                                catch (Exception ex)
                                {
                                    //Exception nex = new Exception("EXCECAO EM ESTABELECIMENTO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText);
                                    import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                    import.Erro = "EXCECAO EM ESTABELECIMENTO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                    SalvarStatusImportacao(import);
                                    return;
                                }
                                finally
                                {
                                    if (oraestabelecimento != null)
                                        oraestabelecimento.Dispose();

                                    if (cmm != null)
                                        cmm.Dispose();

                                    if (dr != null)
                                    {
                                        dr.Close();
                                        dr.Dispose();
                                    }
                                }

                                var nodesprofissionais = no.SelectNodes("PROFISSIONAIS");
                                int j;

                                for (j = 0; j < nodesprofissionais.Count; j++)
                                {
                                    foreach (XmlNode noprof in nodesprofissionais[j].ChildNodes)
                                    {
                                        xa = noprof.Attributes;
                                        string PROF_ID = string.Empty; //CÓDIGO GERADO EM NOSSA BASE

                                        cmm = new OracleCommand("SELECT CPF_PROF FROM PMS_CNES_LFCES018 WHERE CPF_PROF='" + xa["CPF_PROF"].Value + "'", tran.Connection);
                                        dr = cmm.ExecuteReader();
                                        PROF_ID = xa["CPF_PROF"].Value;

                                        try
                                        {
                                            if (dr.HasRows)
                                            {
                                                //dr.Read(); PROF_ID = dr["CPF_PROF"].ToString();
                                                oraprofissional = StringInsertUpdateProfissional(xa, PROF_ID, tran.Connection, "atualizar");
                                                oraprofissional.ExecuteNonQuery();
                                            }
                                            else
                                            {
                                                //cmm.Dispose(); dr.Dispose();
                                                //cmm = new OracleCommand("SELECT MAX(COD_PROF) FROM PMS_CNES_LFCES018", tran.Connection);
                                                //dr = cmm.ExecuteReader();
                                                //dr.Read(); PROF_ID = (long.Parse(dr[0].ToString()) + 1).ToString();

                                                oraprofissional = StringInsertUpdateProfissional(xa, string.Empty, tran.Connection, "inserir");
                                                oraprofissional.ExecuteNonQuery();

                                                //cmm.Dispose(); dr.Close(); dr.Dispose();
                                                //cmm = new OracleCommand("SELECT CPF_PROF FROM PMS_CNES_LFCES018 WHERE CPF_PROF='" + xa["CPF_PROF"].Value + "'", tran.Connection);
                                                //dr = cmm.ExecuteReader();
                                                //dr.Read(); PROF_ID = dr["COD_PROF"].ToString();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            //Exception nex = new Exception("EXCECAO EM PROFISSIONAL: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + oraprofissional.CommandText);
                                            import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                            import.Erro = "EXCECAO EM PROFISSIONAL: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                            SalvarStatusImportacao(import);
                                            return;
                                        }
                                        finally
                                        {
                                            if (oraprofissional != null)
                                                oraprofissional.Dispose();

                                            if (cmm != null)
                                                cmm.Dispose();

                                            if (dr != null)
                                            {
                                                dr.Close();
                                                dr.Dispose();
                                            }
                                        }

                                        var nodesvinculos = noprof.SelectNodes("VINCULOS_PROF");

                                        int y;

                                        for (y = 0; y < nodesvinculos.Count; y++)
                                        {
                                            foreach (XmlNode novinc in nodesvinculos[y].ChildNodes)
                                            {
                                                xa = novinc.Attributes;
                                                string COD_CBO = xa["COD_CBO"].Value;
                                                string TP_SUS_NAO_SUS = xa["VINCULO_SUS"].Value;
                                                string IND_VINC = xa["IND_VINC"].Value;
                                                cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES021 WHERE CPF_PROF='" + PROF_ID + "' AND UNIDADE_ID='" + UNIDADE_ID + "' AND COD_CBO='" + COD_CBO + "' AND IND_VINC='" + IND_VINC + "' AND TP_SUS_NAO_SUS='" + TP_SUS_NAO_SUS + "'", tran.Connection);
                                                dr = cmm.ExecuteReader();

                                                try
                                                {
                                                    if (dr.HasRows)
                                                        oravinculo = StringInsertUpdateVinculoProfissional(xa, tran.Connection, UNIDADE_ID, PROF_ID, COD_CBO, IND_VINC, TP_SUS_NAO_SUS, "atualizar");
                                                    else
                                                        oravinculo = StringInsertUpdateVinculoProfissional(xa, tran.Connection, UNIDADE_ID, PROF_ID, COD_CBO, IND_VINC, TP_SUS_NAO_SUS, "inserir");

                                                    oravinculo.ExecuteNonQuery();
                                                }
                                                catch (Exception ex)
                                                {
                                                    //Exception nex = new Exception("EXCECAO EM VINCULO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + oravinculo.CommandText);
                                                    import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                                    import.Erro = "EXCECAO EM VINCULO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                                    SalvarStatusImportacao(import);
                                                    return;
                                                }
                                                finally
                                                {
                                                    if (oravinculo != null)
                                                        oravinculo.Dispose();

                                                    if (cmm != null)
                                                        cmm.Dispose();

                                                    if (dr != null)
                                                    {
                                                        dr.Close();
                                                        dr.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //Equipes
                                var nodesequipes = no.SelectNodes("EQUIPES");
                                int z;

                                for (z = 0; z < nodesequipes.Count; z++)
                                {
                                    foreach (XmlNode noequipe in nodesequipes[z].ChildNodes)
                                    {
                                        xa = noequipe.Attributes;
                                        string ID_AREA = string.Empty, ID_MUN = string.Empty, SEQ_EQUIPE = string.Empty;

                                        //Verificando a existência de segmento
                                        try
                                        {
                                            cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES040 WHERE COD_MUN='" + xa["COD_MUN"].Value + "' AND CD_SEGMENTO='" + xa["CD_SEGMENTO"].Value + "'", tran.Connection);
                                            dr = cmm.ExecuteReader();

                                            if (!dr.HasRows)
                                                orasegmento = StringInsertUpdateSegmento(xa["COD_MUN"].Value, xa["CD_SEGMENTO"].Value, xa["DS_SEGMENTO"].Value, xa["TP_SEGMENTO"].Value, tran.Connection, "inserir");
                                            else
                                                orasegmento = StringInsertUpdateSegmento(xa["COD_MUN"].Value, xa["CD_SEGMENTO"].Value, xa["DS_SEGMENTO"].Value, xa["TP_SEGMENTO"].Value, tran.Connection, "atualizar");

                                            ID_MUN = xa["COD_MUN"].Value;

                                            orasegmento.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            //Exception nex = new Exception("EXCECAO EM SEGMENTO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + orasegmento.CommandText);
                                            import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                            import.Erro = "EXCECAO EM SEGMENTO: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                            SalvarStatusImportacao(import);
                                            return;
                                        }
                                        finally
                                        {
                                            if (orasegmento != null)
                                                orasegmento.Dispose();

                                            if (cmm != null)
                                                cmm.Dispose();

                                            if (dr != null)
                                            {
                                                dr.Close();
                                                dr.Dispose();
                                            }
                                        }

                                        //Verificando a existência da área
                                        try
                                        {
                                            cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES041 WHERE COD_MUN='" + xa["COD_MUN"].Value + "' AND COD_AREA='" + xa["COD_AREA"].Value + "'", tran.Connection);
                                            dr = cmm.ExecuteReader();

                                            if (!dr.HasRows)
                                                oraarea = StringInsertUpdateArea(xa["COD_MUN"].Value, xa["COD_AREA"].Value, xa["DS_AREA"].Value, xa["CD_SEGMENTO"].Value, tran.Connection, "inserir");
                                            else
                                                oraarea = StringInsertUpdateArea(xa["COD_MUN"].Value, xa["COD_AREA"].Value, xa["DS_AREA"].Value, xa["CD_SEGMENTO"].Value, tran.Connection, "atualizar");

                                            ID_AREA = xa["COD_AREA"].Value;
                                            oraarea.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            //Exception nex = new Exception("EXCECAO EM AREA: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + oraarea.CommandText);
                                            import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                            import.Erro = "EXCECAO EM AREA: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                            SalvarStatusImportacao(import);
                                            return;
                                        }
                                        finally
                                        {
                                            if (oraarea != null)
                                                oraarea.Dispose();

                                            if (cmm != null)
                                                cmm.Dispose();

                                            if (dr != null)
                                            {
                                                dr.Close();
                                                dr.Dispose();
                                            }
                                        }

                                        //Verificando a existência de Equipe
                                        try
                                        {
                                            cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES037 WHERE COD_MUN='" + xa["COD_MUN"].Value + "' AND COD_AREA='" + xa["COD_AREA"].Value + "' AND SEQ_EQUIPE=" + xa["SEQ_EQUIPE"].Value, tran.Connection);
                                            dr = cmm.ExecuteReader();

                                            if (!dr.HasRows)
                                                oraequipe = StringInsertUpdateEquipe(xa, UNIDADE_ID, tran.Connection, "inserir");
                                            else
                                                oraequipe = StringInsertUpdateEquipe(xa, UNIDADE_ID, tran.Connection, "atualizar");

                                            SEQ_EQUIPE = xa["SEQ_EQUIPE"].Value;
                                            oraequipe.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            //Exception nex = new Exception("EXCECAO EM EQUIPE: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + oraequipe.CommandText);
                                            import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                            import.Erro = "EXCECAO EM EQUIPE: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                            SalvarStatusImportacao(import);
                                            return;
                                        }
                                        finally
                                        {
                                            if (oraequipe != null)
                                                oraequipe.Dispose();

                                            if (cmm != null)
                                                cmm.Dispose();

                                            if (dr != null)
                                            {
                                                dr.Close();
                                                dr.Dispose();
                                            }
                                        }

                                        var nodesequipeprofissional = noequipe.SelectNodes("PROF_EQUIPE");
                                        int w;

                                        for (w = 0; w < nodesequipeprofissional.Count; w++)
                                        {
                                            foreach (XmlNode noequipeprof in nodesequipeprofissional[w].ChildNodes)
                                            {
                                                xa = noequipeprof.Attributes;
                                                //OracleDataReader dr2 = null;

                                                try
                                                {
                                                    cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES018 WHERE PROF_ID='" + xa["PROF_ID"].Value + "'", tran.Connection);
                                                    dr = cmm.ExecuteReader();

                                                    if (dr.HasRows)
                                                    {
                                                        dr.Read();
                                                        string COD_PROF = dr["CPF_PROF"].ToString();
                                                        cmm.Dispose(); dr.Close(); dr.Dispose();
                                                        cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES021 WHERE CPF_PROF='" + COD_PROF + "' AND UNIDADE_ID='" + UNIDADE_ID + "' AND COD_CBO='" + xa["COD_CBO"].Value + "'", tran.Connection);
                                                        dr = cmm.ExecuteReader();

                                                        //cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES018 WHERE PROF_ID = '" + xa["PROF_ID"].Value + "'", tran.Connection);
                                                        //dr2 = cmm.ExecuteReader();

                                                        if (dr.HasRows)
                                                        {
                                                            cmm.Dispose();
                                                            dr.Close(); dr.Dispose();

                                                            cmm = new OracleCommand("SELECT * FROM PMS_CNES_LFCES038 WHERE COD_MUN='" + ID_MUN + "' AND COD_AREA='" + ID_AREA + "' AND SEQ_EQUIPE=" + SEQ_EQUIPE + " AND CPF_PROF='" + COD_PROF + "'", tran.Connection);
                                                            dr = cmm.ExecuteReader();

                                                            if (!dr.HasRows)
                                                                oraequipeprof = StringInsertUpdateEquipeProfissional(xa, UNIDADE_ID, COD_PROF, ID_MUN, ID_AREA, SEQ_EQUIPE, tran.Connection, "inserir");
                                                            else
                                                                oraequipeprof = StringInsertUpdateEquipeProfissional(xa, UNIDADE_ID, COD_PROF, ID_MUN, ID_AREA, SEQ_EQUIPE, tran.Connection, "atualizar");

                                                            oraequipeprof.ExecuteNonQuery();
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    //Exception nex = new Exception("EXCECAO EM VINCULO EQUIPE PROFISSIONAL: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText + " COMANDO DE EXECUCAO: " + oraequipeprof.CommandText);
                                                    import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                                                    import.Erro = "EXCECAO EM VINCULO EQUIPE PROFISSIONAL: " + ex.Message + " CONSULTA SQL: " + cmm.CommandText;
                                                    SalvarStatusImportacao(import);
                                                    return;
                                                }
                                                finally
                                                {
                                                    if (oraequipeprof != null)
                                                        oraequipeprof.Dispose();

                                                    if (cmm != null)
                                                        cmm.Dispose();

                                                    //dr2.Close();
                                                    //dr2.Dispose();

                                                    if (dr != null)
                                                    {
                                                        dr.Close();
                                                        dr.Dispose();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        tran.Commit();

                        import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Finalizada);
                        import.HorarioFinal = DateTime.Now;
                        SalvarStatusImportacao(import);
                    }
                    catch (OracleException f)
                    {
                        tran.Rollback();
                        import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                        import.Erro = f.Message;
                        SalvarStatusImportacao(import);
                        return;
                    }
                    finally
                    {
                        if (cmm != null)
                            cmm.Dispose();

                        if (dr != null)
                        {
                            dr.Dispose();
                            dr.Close();
                        }

                        if (tran != null)
                            tran.Dispose();

                        if (con != null)
                        {
                            con.Close();
                            con.Dispose();
                        }
                    }
                }
            }
            catch (OracleException f)
            {
                import.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Falha);
                import.Erro = f.Message;
                SalvarStatusImportacao(import);
                return;
            }
            finally
            {
                if (cmm != null)
                    cmm.Dispose();

                if (dr != null)
                {
                    dr.Dispose();
                    dr.Close();
                }

                if (con != null)
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        private OracleCommand StringInsertUpdateEstabelecimento(XmlAttributeCollection xa, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    if (tipo.Equals("inserir"))
                    {
                        string s = string.Empty;
                        string s2 = string.Empty;

                        s = "INSERT INTO PMS_CNES_LFCES004 (";
                        s2 = " VALUES (";

                        foreach (XmlAttribute a in xa)
                        {
                            if (!string.IsNullOrEmpty(a.Value))
                            {
                                if (a.Name == "CODMUNGEST")
                                {
                                    Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + a.Value + "'").UniqueResult<Municipio>();
                                    //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(a.Value);

                                    if (mun != null)
                                    {
                                        s += a.Name + ",";
                                        s2 += ":" + a.Name + ",";
                                        ora.Parameters.Add(new OracleParameter(a.Name, mun.Codigo));
                                    }
                                }
                                else
                                {
                                    if (a.Name == "BAIRRO")
                                    {
                                        s += a.Name + ",";
                                        s2 += ":" + a.Name + ",";

                                        ora.Parameters.Add(new OracleParameter(a.Name, a.Value));

                                        Distrito d = RetornaDistritoValidoEstabelecimento(a.Value, xa["CODMUNGEST"].Value);

                                        if (d != null)
                                        {
                                            s += "CO_DISTRITO,";
                                            s2 += ":CO_DISTRITO,";
                                            ora.Parameters.Add(new OracleParameter("CO_DISTRITO", d.Codigo));
                                        }
                                    }
                                    else
                                    {
                                        if (a.Name == "TP_UNID_ID" || a.Name == "COD_NATORG" || a.Name == "COD_ATIV"
                                            || a.Name == "COD_ESFADM")
                                        {
                                            if (VerificaValidadeObjetoEstabelecimento(a.Value, a.Name))
                                            {
                                                s += a.Name + ",";
                                                s2 += ":" + a.Name + ",";

                                                ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                            }
                                        }
                                        else
                                        {
                                            s += a.Name + ",";
                                            s2 += ":" + a.Name + ",";

                                            if (a.Name == "DATA_ATU" || a.Name == "DATA_EXPED" || a.Name == "DTAVALIACAOPNASS" || a.Name == "DTACREDITACAO" || a.Name == "DT_VALIDACAO")
                                                ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                                            else
                                            {
                                                if (a.Name == "STATUSMOV")
                                                    ora.Parameters.Add(new OracleParameter(a.Name, Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Ativo)));
                                                else
                                                    ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //s += "CO_DISTRITO,";
                        //s2 += ":CO_DISTRITO,";
                        //ora.Parameters.Add(new OracleParameter("CO_DISTRITO", 11));

                        s = s.Remove(s.Length - 1, 1);
                        s += ")";
                        s2 = s2.Remove(s2.Length - 1, 1);
                        s2 += ")";

                        consulta = s + s2;
                    }
                    else
                    {
                        consulta = "UPDATE PMS_CNES_LFCES004 SET ";

                        foreach (XmlAttribute a in xa)
                        {
                            if (!string.IsNullOrEmpty(a.Value))
                            {
                                if (a.Name != "CNES")
                                {
                                    if (a.Name == "CODMUNGEST")
                                    {
                                        Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + a.Value + "'").UniqueResult<Municipio>();
                                        //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(a.Value);

                                        if (mun != null)
                                        {
                                            consulta += a.Name + "=:" + a.Name + ",";
                                            ora.Parameters.Add(new OracleParameter(a.Name, mun.Codigo));
                                        }
                                    }
                                    else
                                    {
                                        if (a.Name == "BAIRRO")
                                        {
                                            consulta += a.Name + "=:" + a.Name + ",";
                                            ora.Parameters.Add(new OracleParameter(a.Name, a.Value));

                                            Distrito d = RetornaDistritoValidoEstabelecimento(a.Value, xa["CODMUNGEST"].Value);

                                            if (d != null)
                                            {
                                                consulta += "CO_DISTRITO" + "=:CO_DISTRITO,";
                                                ora.Parameters.Add(new OracleParameter("CO_DISTRITO", d.Codigo));
                                            }
                                        }
                                        else
                                        {
                                            if (a.Name == "TP_UNID_ID" || a.Name == "COD_NATORG" || a.Name == "COD_ATIV"
                                                || a.Name == "COD_ESFADM")
                                            {
                                                if (VerificaValidadeObjetoEstabelecimento(a.Value, a.Name))
                                                {
                                                    consulta += a.Name + "=:" + a.Name + ",";
                                                    ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                                }
                                            }
                                            else
                                            {
                                                consulta += a.Name + "=:" + a.Name + ",";

                                                if (a.Name == "DATA_ATU" || a.Name == "DATA_EXPED" || a.Name == "DTAVALIACAOPNASS" || a.Name == "DTACREDITACAO" || a.Name == "DT_VALIDACAO")
                                                    ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                                                else
                                                {
                                                    if (a.Name == "STATUSMOV")
                                                        ora.Parameters.Add(new OracleParameter(a.Name, Convert.ToChar(ViverMais.Model.EstabelecimentoSaude.DescricaoStatus.Ativo)));
                                                    else
                                                        ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        consulta = consulta.Remove(consulta.Length - 1, 1);
                        consulta += " WHERE CNES=:CNES";
                        ora.Parameters.Add(new OracleParameter("CNES", xa["CNES"].Value));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    session.Close();
                }
            }

            ora.CommandText = consulta;
            ora.Connection = con;
            return ora;
        }

        /// <summary>
        /// Retorna a existência para validar a chave estrangeira
        /// </summary>
        /// <param name="codigo">código do objeto</param>
        /// <param name="coluna">nome da coluna na tabela</param>
        /// <returns>Resultado Booleano</returns>
        private bool VerificaValidadeObjetoEstabelecimento(string codigo, string coluna)
        {
            bool resultado = true;

            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    switch (coluna)
                    {
                        case "TP_UNID_ID":
                            if (session.CreateQuery("FROM ViverMais.Model.TipoEstabelecimento AS t WHERE t.Codigo = '" + codigo + "'").UniqueResult<TipoEstabelecimento>() == null)
                                resultado = false;
                            break;
                        case "COD_NATORG":
                            if (session.CreateQuery("FROM ViverMais.Model.NaturezaOrganizacao AS n WHERE n.Codigo = '" + codigo + "'").UniqueResult<NaturezaOrganizacao>() == null)
                                resultado = false;
                            break;
                        case "COD_ATIV":
                            if (session.CreateQuery("FROM ViverMais.Model.AtiViverMaisdeEnsino AS a WHERE a.Codigo = '" + codigo + "'").UniqueResult<AtiViverMaisdeEnsino>() == null)
                                resultado = false;
                            break;
                        default:
                            if (session.CreateQuery("FROM ViverMais.Model.EsferaAdministrativa AS e WHERE e.Codigo = '" + codigo + "'").UniqueResult<EsferaAdministrativa>() == null)
                                resultado = false;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    session.Close();
                }
            }

            return resultado;
        }

        private OracleCommand StringInsertUpdateProfissional(XmlAttributeCollection xa, string co_profissional, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                try
                {
                    if (tipo.Equals("inserir"))
                    {
                        string s = string.Empty;
                        string s2 = string.Empty;

                        s = "INSERT INTO PMS_CNES_LFCES018 (";
                        s2 = " VALUES (";

                        foreach (XmlAttribute a in xa)
                        {
                            if (!string.IsNullOrEmpty(a.Value))
                            {
                                if (a.Name == "BAIRRODIST")
                                {
                                    Bairro b = RetornaBairroValidoProfissional(a.Value, xa["COD_MUN"].Value);

                                    if (b != null)
                                    {
                                        ora.Parameters.Add(new OracleParameter("CO_BAIRRO", b.Codigo));
                                        s += "CO_BAIRRO,";
                                        s2 += ":CO_BAIRRO,";
                                    }
                                }
                                else
                                {
                                    if (a.Name == "CD_TP_LOGR")
                                    {
                                        TipoLogradouro lg = session.CreateQuery("FROM ViverMais.Model.TipoLogradouro AS l WHERE l.Codigo='" + a.Value + "'").UniqueResult<TipoLogradouro>();
                                            //Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<TipoLogradouro>(a.Value);
                                        if (lg != null)
                                        {
                                            s += a.Name + ",";
                                            s2 += ":" + a.Name + ",";
                                            ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                        }
                                    }
                                    else
                                    {
                                        if (a.Name == "COD_MUN")
                                        {
                                            Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + a.Value + "'").UniqueResult<Municipio>();
                                                //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(a.Value);
                                            if (mun != null)
                                            {
                                                s += a.Name + ",";
                                                s2 += ":" + a.Name + ",";
                                                ora.Parameters.Add(new OracleParameter(a.Name, mun.Codigo));
                                            }
                                        }
                                        else
                                        {
                                            s += a.Name + ",";
                                            s2 += ":" + a.Name + ",";

                                            if ((a.Name == "DATA_NASC" || a.Name == "DATA_EMISS" || a.Name == "DTEMIIDENT" ||
                                            a.Name == "DATA_ENTRA" || a.Name == "DTEMISCTPS" || a.Name == "DT_INIATIV"
                                            || a.Name == "DATA_ATU" || a.Name == "DT_NATUR"))
                                                ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                                            else
                                            {
                                                if (a.Name == "CD_RACA")
                                                {
                                                    int co_raca = a.Value == "99" ? 9 : int.Parse(a.Value);
                                                    ora.Parameters.Add(new OracleParameter(a.Name, co_raca));
                                                }
                                                else
                                                {
                                                    if (a.Name == "STATUSMOV")
                                                        ora.Parameters.Add(new OracleParameter(a.Name,Convert.ToChar(ViverMais.Model.Profissional.DescricaoStatus.Ativo)));
                                                    else
                                                        ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //s += "COD_PROF,";
                        //s2 += ":COD_PROF,";
                        //ora.Parameters.Add(new OracleParameter("COD_PROF", co_profissional));

                        //s += "N_REGISTRO,";
                        //s2 += ":N_REGISTRO,";
                        //ora.Parameters.Add(new OracleParameter("N_REGISTRO", numeroregistro));

                        s = s.Remove(s.Length - 1, 1);
                        s += ")";
                        s2 = s2.Remove(s2.Length - 1, 1);
                        s2 += ")";

                        consulta = s + s2;
                    }
                    else
                    {
                        consulta = "UPDATE PMS_CNES_LFCES018 SET ";

                        foreach (XmlAttribute a in xa)
                        {
                            if (!string.IsNullOrEmpty(a.Value))
                            {
                                //if (a.Name != "PROF_ID")
                                //{
                                if ((a.Name == "DATA_NASC" || a.Name == "DATA_EMISS" || a.Name == "DTEMIIDENT" ||
                                    a.Name == "DATA_ENTRA" || a.Name == "DTEMISCTPS" || a.Name == "DT_INIATIV"
                                    || a.Name == "DATA_ATU" || a.Name == "DT_NATUR"))
                                {
                                    consulta += a.Name + "=:" + a.Name + ",";
                                    ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                                }
                                else
                                {
                                    if (a.Name == "BAIRRODIST")
                                    {
                                        Bairro bairro = RetornaBairroValidoProfissional(a.Value, xa["COD_MUN"].Value);

                                        if (bairro != null)
                                        {
                                            consulta += "CO_BAIRRO=:CO_BAIRRO,";
                                            ora.Parameters.Add(new OracleParameter("CO_BAIRRO", bairro.Codigo));
                                        }
                                    }
                                    else
                                    {
                                        if (a.Name == "CD_TP_LOGR")
                                        {
                                            TipoLogradouro lg = session.CreateQuery("FROM ViverMais.Model.TipoLogradouro AS l WHERE l.Codigo='" + a.Value + "'").UniqueResult<TipoLogradouro>();
                                                //Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<TipoLogradouro>(a.Value);
                                            if (lg != null)
                                            {
                                                consulta += a.Name + "=:" + a.Name + ",";
                                                ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                            }
                                        }
                                        else
                                        {
                                            if (a.Name == "COD_MUN")
                                            {
                                                Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + a.Value + "'").UniqueResult<Municipio>();
                                                    //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(a.Value);
                                                if (mun != null)
                                                {
                                                    consulta += a.Name + "=:" + a.Name + ",";
                                                    ora.Parameters.Add(new OracleParameter(a.Name, mun.Codigo));
                                                }
                                            }
                                            else
                                            {
                                                consulta += a.Name + "=:" + a.Name + ",";

                                                if (a.Name == "CD_RACA")
                                                {
                                                    int co_raca = a.Value == "99" ? 9 : int.Parse(a.Value);
                                                    ora.Parameters.Add(new OracleParameter(a.Name, co_raca));
                                                }
                                                else
                                                {
                                                    if (a.Name == "STATUSMOV")
                                                        ora.Parameters.Add(new OracleParameter(a.Name, Convert.ToChar(ViverMais.Model.Profissional.DescricaoStatus.Ativo)));
                                                    else
                                                        ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                                                }
                                            }
                                        }
                                    }
                                    //}
                                }
                            }
                        }

                        consulta = consulta.Remove(consulta.Length - 1, 1);
                        consulta += " WHERE CPF_PROF=:CPF_PROF";
                        //ora.Parameters.Add(new OracleParameter("CPF_PROF", co_profissional));
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally 
                {
                    session.Close();
                }
            }

            ora.CommandText = consulta;
            ora.Connection = con;
            return ora;
        }

        /// <summary>
        /// Retorna o bairro válido para o profissional
        /// </summary>
        /// <param name="nomebairro">nome do bairro</param>
        /// <param name="codigomunicipio">código do munícipio</param>
        /// <returns>Obj. Bairro</returns>
        private Bairro RetornaBairroValidoProfissional(string nomebairro, string codigomunicipio)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                Bairro b = null;

                try
                {
                    IList<Bairro> bairros = session.CreateQuery("FROM ViverMais.Model.Bairro AS b WHERE b.Nome = '" + nomebairro + "'").List<Bairro>();
                    //Factory.GetInstance<IBairro>().BuscarPorNome<Bairro>(nomebairro);

                    if (bairros.Count > 0)
                    {
                        if (bairros.Count == 1)
                            b = bairros[0];
                        else
                        {
                            if (!string.IsNullOrEmpty(codigomunicipio))
                            {
                                Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + codigomunicipio + "'").UniqueResult<Municipio>();
                                //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(codigomunicipio);

                                if (mun != null)
                                {
                                    IList<Distrito> ld = session.CreateQuery("FROM ViverMais.Model.Distrito AS d WHERE d.Municipio.Codigo = '" + mun.Codigo + "'").List<Distrito>();
                                    //Factory.GetInstance<IDistrito>().BuscarPorMunicipio<Distrito>(mun.Codigo);
                                    var linq = from bar in bairros where ld.Contains(bar.Distrito) select bar;
                                    Bairro bairroresultado = linq.Cast<Bairro>().FirstOrDefault();
                                    b = bairroresultado;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    session.Close();
                }

                return b;
            }
        }

        private Distrito RetornaDistritoValidoEstabelecimento(string nomebairro, string co_municipio)
        {
            using (ISession session = NHibernateHttpHelper.SessionFactories["ViverMais"].OpenSession())
            {
                Distrito d = null;

                try
                {
                    IList<Bairro> bairros = session.CreateQuery("FROM ViverMais.Model.Bairro AS b WHERE b.Nome = '" + nomebairro + "'").List<Bairro>();
                    //Factory.GetInstance<IBairro>().BuscarPorNome<Bairro>(nomebairro);

                    if (bairros.Count > 0)
                    {
                        if (bairros.Count == 1)
                            d = bairros[0].Distrito;
                        else
                        {
                            if (!string.IsNullOrEmpty(co_municipio))
                            {
                                Municipio mun = session.CreateQuery("FROM ViverMais.Model.Municipio AS m WHERE m.Codigo = '" + co_municipio + "'").UniqueResult<Municipio>();
                                //Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(co_municipio);

                                if (mun != null)
                                {
                                    IList<Distrito> ld = session.CreateQuery("FROM ViverMais.Model.Distrito AS d WHERE d.Municipio.Codigo = '" + mun.Codigo + "'").List<Distrito>();
                                    //Factory.GetInstance<IDistrito>().BuscarPorMunicipio<Distrito>(mun.Codigo);
                                    var linq = from bar in bairros where ld.Contains(bar.Distrito) select bar;
                                    Bairro bairroresultado = linq.Cast<Bairro>().FirstOrDefault();

                                    if (bairroresultado != null)
                                        d = bairroresultado.Distrito;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    session.Close();
                }

                return d;
            }
        }

        private OracleCommand StringInsertUpdateVinculoProfissional(XmlAttributeCollection xa, OracleConnection con, string co_unidade, string co_profissional, string co_cbo, string ind_vinc, string tp_sus_nao_sus, string tipo)
        {
            string consulta = string.Empty;
            OracleCommand ora = new OracleCommand();

            if (tipo.Equals("inserir"))
            {
                string s = string.Empty;
                string s2 = string.Empty;

                s = "INSERT INTO PMS_CNES_LFCES021(";
                s2 = " VALUES(";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value) && a.Name != "COD_CBO"
                        && a.Name != "VINCULO_SUS" && a.Name != "IND_VINC")
                    {
                        //if (a.Name == "VINCULO_SUS")
                        //{
                        //    s += "TP_SUS_NAO_SUS,";
                        //    s2 += ":TP_SUS_NAO_SUS,";

                        //    ora.Parameters.Add(new OracleParameter(a.Name, char.Parse(a.Value)));
                        //}
                        //else
                        //{
                            s += a.Name + ",";
                            s2 += ":" + a.Name + ",";

                            if (a.Name == "CGHORAOUTR" || a.Name == "CG_HORAAMB" || a.Name == "CGHORAHOSP")
                                ora.Parameters.Add(new OracleParameter(a.Name, int.Parse(a.Value)));
                            else
                                ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                        //}
                    }
                }

                s += "STATUS,";
                s2 += ":STATUS,";
                ora.Parameters.Add(new OracleParameter("STATUS", Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo)));

                s += "UNIDADE_ID,";
                s2 += ":UNIDADE_ID,";
                ora.Parameters.Add(new OracleParameter("UNIDADE_ID", co_unidade));

                s += "CPF_PROF,";
                s2 += ":CPF_PROF,";
                ora.Parameters.Add(new OracleParameter("CPF_PROF", co_profissional));

                s += "COD_CBO,";
                s2 += ":COD_CBO,";
                ora.Parameters.Add(new OracleParameter("COD_CBO", co_cbo));

                s += "IND_VINC,";
                s2 += ":IND_VINC,";
                ora.Parameters.Add(new OracleParameter("IND_VINC", ind_vinc));

                s += "TP_SUS_NAO_SUS,";
                s2 += ":TP_SUS_NAO_SUS,";
                ora.Parameters.Add(new OracleParameter("TP_SUS_NAO_SUS", tp_sus_nao_sus));

                s = s.Remove(s.Length - 1, 1);
                s += ")";
                s2 = s2.Remove(s2.Length - 1, 1);
                s2 += ")";

                consulta = s + s2;
            }
            else
            {
                consulta = "UPDATE PMS_CNES_LFCES021 SET ";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value) && a.Name != "VINCULO_SUS" && a.Name != "IND_VINC"
                        && a.Name != "COD_CBO")
                    {
                        //if (a.Name == "VINCULO_SUS")
                        //    consulta += "TP_SUS_NAO_SUS=:TP_SUS_NAO_SUS,";
                        //else
                            consulta += a.Name + "=:" + a.Name + ",";

                        if (a.Name == "CGHORAOUTR" || a.Name == "CG_HORAAMB" || a.Name == "CGHORAHOSP")
                            ora.Parameters.Add(new OracleParameter(a.Name, int.Parse(a.Value)));
                        else
                            ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                    }
                }

                consulta += "STATUS=:STATUS,";
                ora.Parameters.Add(new OracleParameter("STATUS", Convert.ToChar(VinculoProfissional.DescricaStatus.Ativo)));

                consulta = consulta.Remove(consulta.Length - 1, 1);
                consulta += " WHERE CPF_PROF =:CPF_PROF AND UNIDADE_ID=:UNIDADE_ID AND COD_CBO=:COD_CBO AND TP_SUS_NAO_SUS=:TP_SUS_NAO_SUS AND IND_VINC=:IND_VINC";
                ora.Parameters.Add(new OracleParameter("CPF_PROF", co_profissional));
                ora.Parameters.Add(new OracleParameter("UNIDADE_ID", co_unidade));
                ora.Parameters.Add(new OracleParameter("COD_CBO", co_cbo));
                ora.Parameters.Add(new OracleParameter("TP_SUS_NAO_SUS", tp_sus_nao_sus));
                ora.Parameters.Add(new OracleParameter("IND_VINC", ind_vinc));
            }

            ora.CommandText = consulta;
            ora.Connection = con;

            return ora;
        }

        private OracleCommand StringInsertUpdateSegmento(string co_municipio, string cd_segmento, string ds_segmento, string co_tiposegmento, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            if (tipo.Equals("inserir"))
                consulta = "INSERT INTO PMS_CNES_LFCES040 (COD_MUN,CD_SEGMENTO,DS_SEGMENTO,TP_SEGMENTO) VALUES " +
                                               "(:COD_MUN,:CD_SEGMENTO,:DS_SEGMENTO,:TP_SEGMENTO)";
            else
                consulta = "UPDATE PMS_CNES_LFCES040 SET DS_SEGMENTO=:DS_SEGMENTO, TP_SEGMENTO=:TP_SEGMENTO" +
                           " WHERE COD_MUN=:COD_MUN AND CD_SEGMENTO=:CD_SEGMENTO";

            ora.Parameters.Add(new OracleParameter("COD_MUN", co_municipio));
            ora.Parameters.Add(new OracleParameter("CD_SEGMENTO", cd_segmento));
            ora.Parameters.Add(new OracleParameter("DS_SEGMENTO", ds_segmento));
            ora.Parameters.Add(new OracleParameter("TP_SEGMENTO", co_tiposegmento));

            ora.CommandText = consulta;
            ora.Connection = con;

            return ora;
        }

        private OracleCommand StringInsertUpdateArea(string co_municipio, string co_area, string ds_area, string cd_segmento, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            if (tipo.Equals("inserir"))
                consulta = "INSERT INTO PMS_CNES_LFCES041 (COD_MUN,COD_AREA,DS_AREA,CD_SEGMENTO) VALUES " +
                                               "(:COD_MUN,:COD_AREA,:DS_AREA,:CD_SEGMENTO)";
            else
                consulta = "UPDATE PMS_CNES_LFCES041 SET DS_AREA=:DS_AREA, CD_SEGMENTO=:CD_SEGMENTO" +
                           " WHERE COD_MUN=:COD_MUN AND COD_AREA=:COD_AREA";

            ora.Parameters.Add(new OracleParameter("COD_MUN", co_municipio));
            ora.Parameters.Add(new OracleParameter("COD_AREA", co_area));
            ora.Parameters.Add(new OracleParameter("DS_AREA", ds_area));
            ora.Parameters.Add(new OracleParameter("CD_SEGMENTO", cd_segmento));

            ora.CommandText = consulta;
            ora.Connection = con;

            return ora;
        }

        private OracleCommand StringInsertUpdateEquipe(XmlAttributeCollection xa, string co_unidade, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            if (tipo.Equals("inserir"))
            {
                string s = string.Empty;
                string s2 = string.Empty;

                s += "INSERT INTO PMS_CNES_LFCES037 (";
                s2 += " VALUES (";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value) && a.Name != "DS_AREA" && a.Name != "CD_SEGMENTO"
                        && a.Name != "DS_SEGMENTO" && a.Name != "TP_SEGMENTO" && a.Name != "DS_EQUIPE")
                    {
                        s += a.Name + ",";
                        s2 += ":" + a.Name + ",";

                        if (a.Name == "DT_ATIVACAO" || a.Name == "DT_DESATIVACAO" || a.Name == "DATA_ATU")
                            ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                        else
                        {
                            if (a.Name == "UNIDADE_ID")
                                ora.Parameters.Add(new OracleParameter(a.Name, co_unidade));
                            else
                                ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                        }
                    }
                }

                s = s.Remove(s.Length - 1, 1);
                s += ")";
                s2 = s2.Remove(s2.Length - 1, 1);
                s2 += ")";

                consulta = s + s2;
            }
            else
            {
                consulta = "UPDATE PMS_CNES_LFCES037 SET ";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value) && a.Name != "DS_AREA" && a.Name != "CD_SEGMENTO"
                        && a.Name != "DS_SEGMENTO" && a.Name != "TP_SEGMENTO" && a.Name != "DS_EQUIPE"
                        && (a.Name != "COD_MUN" && a.Name != "COD_ARRA" && a.Name != "SEQ_EQUIPE") && a.Name != "UNIDADE_ID")
                    {
                        consulta += a.Name + "=:" + a.Name + ",";

                        if (a.Name == "DT_ATIVACAO" || a.Name == "DT_DESATIVACAO" || a.Name == "DATA_ATU")
                            ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                        else
                        {
                            if (a.Name == "UNIDADE_ID")
                                ora.Parameters.Add(new OracleParameter(a.Name, co_unidade));
                            else
                                ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                        }
                    }
                }

                consulta = consulta.Remove(consulta.Length - 1, 1);
                consulta += " WHERE COD_MUN=:COD_MUN AND COD_AREA=:COD_AREA AND SEQ_EQUIPE=:SEQ_EQUIPE";

                ora.Parameters.Add(new OracleParameter("COD_MUN", xa["COD_MUN"].Value));
                ora.Parameters.Add(new OracleParameter("COD_AREA", xa["COD_AREA"].Value));
                ora.Parameters.Add(new OracleParameter("SEQ_EQUIPE", xa["SEQ_EQUIPE"].Value));
            }

            ora.CommandText = consulta;
            ora.Connection = con;

            return ora;
        }

        private OracleCommand StringInsertUpdateEquipeProfissional(XmlAttributeCollection xa, string co_unidade, string co_profissional, string co_mun, string co_area, string seq_equipe, OracleConnection con, string tipo)
        {
            OracleCommand ora = new OracleCommand();
            string consulta = string.Empty;

            if (tipo.Equals("inserir"))
            {
                string s = string.Empty;
                string s2 = string.Empty;

                s += "INSERT INTO PMS_CNES_LFCES038 (";
                s2 += " VALUES (";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value) && (a.Name != "CD_HORAAMB" && a.Name != "CGHORAOUTR"
                        && a.Name != "CG_HORAHOSP" && a.Name != "CHOUTROSCHDIFER_RESMED" && a.Name != "CNES_ATENDCOMP1"
                        && a.Name != "CNES_ATENDCOMP2" && a.Name != "CNES_ATENDCOMP3" && a.Name != "CNES1CHDIFER_SISTPENIT"
                        && a.Name != "CNES1CHDIFER_HPP" && a.Name != "PROF_ID"))
                    {
                        s += a.Name + ",";
                        s2 += ":" + a.Name + ",";

                        if (a.Name == "DT_DESLIGAMENTO" || a.Name == "DT_ENTRADA" || a.Name == "DATA_ATU")
                            ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                        else
                            ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                    }
                }

                s += "UNIDADE_ID,";
                s2 += ":UNIDADE_ID,";
                ora.Parameters.Add(new OracleParameter("UNIDADE_ID", co_unidade));

                s += "COD_MUN,";
                s2 += ":COD_MUN,";
                ora.Parameters.Add(new OracleParameter("COD_MUN", co_mun));

                s += "COD_AREA,";
                s2 += ":COD_AREA,";
                ora.Parameters.Add(new OracleParameter("COD_AREA", co_area));

                s += "SEQ_EQUIPE,";
                s2 += ":SEQ_EQUIPE,";
                ora.Parameters.Add(new OracleParameter("SEQ_EQUIPE", seq_equipe));

                s += "CPF_PROF,";
                s2 += ":CPF_PROF,";
                ora.Parameters.Add(new OracleParameter("CPF_PROF", co_profissional));

                s = s.Remove(s.Length - 1, 1);
                s += ")";
                s2 = s2.Remove(s2.Length - 1, 1);
                s2 += ")";

                consulta = s + s2;
            }
            else
            {
                consulta = "UPDATE PMS_CNES_LFCES038 SET ";

                foreach (XmlAttribute a in xa)
                {
                    if (!string.IsNullOrEmpty(a.Value)
                       && a.Name != "PROF_ID" && (a.Name != "CD_HORAAMB" && a.Name != "CGHORAOUTR"
                       && a.Name != "CG_HORAHOSP" && a.Name != "CHOUTROSCHDIFER_RESMED" && a.Name != "CNES_ATENDCOMP1"
                       && a.Name != "CNES_ATENDCOMP2" && a.Name != "CNES_ATENDCOMP3" && a.Name != "CNES1CHDIFER_SISTPENIT"
                       && a.Name != "CNES1CHDIFER_HPP"))
                    {
                        consulta += a.Name + "=:" + a.Name + ",";

                        if (a.Name == "DT_DESLIGAMENTO" || a.Name == "DT_ENTRADA" || a.Name == "DATA_ATU")
                            ora.Parameters.Add(new OracleParameter(a.Name, DateTime.Parse(a.Value)));
                        else
                            ora.Parameters.Add(new OracleParameter(a.Name, a.Value));
                    }
                }

                consulta += "UNIDADE_ID=:UNIDADE_ID,";
                ora.Parameters.Add(new OracleParameter("UNIDADE_ID", co_unidade));
                //consulta += "COD_MUN=:COD_MUN,";
                //ora.Parameters.Add(new OracleParameter("COD_MUN", co_mun));
                //consulta += "COD_AREA=:COD_AREA,";
                //ora.Parameters.Add(new OracleParameter("COD_AREA", co_area));
                //consulta += "SEQ_EQUIPE=:SEQ_EQUIPE,";
                //ora.Parameters.Add(new OracleParameter("SEQ_EQUIPE", seq_equipe));

                consulta = consulta.Remove(consulta.Length - 1, 1);
                consulta += " WHERE COD_MUN=:COD_MUN AND COD_AREA=:COD_AREA AND SEQ_EQUIPE=:SEQ_EQUIPE AND CPF_PROF=:CPF_PROF";

                ora.Parameters.Add(new OracleParameter("COD_MUN", co_mun));
                ora.Parameters.Add(new OracleParameter("COD_AREA", co_area));
                ora.Parameters.Add(new OracleParameter("SEQ_EQUIPE", seq_equipe));
                ora.Parameters.Add(new OracleParameter("CPF_PROF", co_profissional));
            }

            ora.CommandText = consulta;
            ora.Connection = con;

            return ora;
        }

        #endregion

    }
}
