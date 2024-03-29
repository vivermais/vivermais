﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using ViverMais.DAL;

namespace ViverMais.DAO.Farmacia.Misc
{
    public class FarmaciaDAO : FarmaciaServiceFacadeDAO, IFarmacia
    {
        #region IFarmacia Members

        IList<T> IFarmacia.BuscarPorEstabelecimentoSaude<T>(string codigoUnidade)
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.Farmacia as farmacia where farmacia.CodigoUnidade = '" + codigoUnidade + "' ORDER BY farmacia.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IFarmacia.BuscarPorElenco<T>(int co_elenco)
        {
            string hql = string.Empty;
            hql   = "SELECT farmacia FROM ViverMais.Model.Farmacia farmacia, ViverMais.Model.ElencoMedicamento elenco WHERE ";
            hql  += " elenco.Codigo = " + co_elenco + " AND elenco IN ELEMENTS(farmacia.Elencos)";
            return Session.CreateQuery(hql).List<T>();
        }

        void IFarmacia.SalvarVinculoUsuarioFarmacia<T>(IList<T> novasfarmaciasusuario, IList<T> farmaciasdispensadas, int co_usuario)
        {
            ViverMais.Model.Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<ViverMais.Model.Usuario>(co_usuario);
            IList<ViverMais.Model.Farmacia> _novasfarmaciasusuario = (IList<ViverMais.Model.Farmacia>)(object)novasfarmaciasusuario;
            IList<ViverMais.Model.Farmacia> _farmaciasdispensadas = (IList<ViverMais.Model.Farmacia>)(object)farmaciasdispensadas;
            IList<ViverMais.Model.Farmacia> farmaciasatuaisusuario = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia,Usuario>(usuario,false,false);

            using (Session.BeginTransaction())
            {
                try
                {
                    //Incluindo as novas farmácias, se existirem
                    foreach (ViverMais.Model.Farmacia farmacia in _novasfarmaciasusuario)
                    {
                        if (farmaciasatuaisusuario.Where(p => p.Codigo == farmacia.Codigo).FirstOrDefault() == null)
                        {
                            farmacia.CodigosUsuarios.Add(usuario.Codigo);
                            Session.Update(Session.Merge(farmacia));
                        }
                    }

                    //Retirando as antigas farmácias, se existirem
                    foreach (ViverMais.Model.Farmacia farmacia in _farmaciasdispensadas)
                    {
                        if (farmaciasatuaisusuario.Where(p => p.Codigo == farmacia.Codigo).FirstOrDefault() != null)
                        {
                            farmacia.CodigosUsuarios.Remove(usuario.Codigo);
                            Session.Update(Session.Merge(farmacia));
                        }
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        IList<T> IFarmacia.BuscarPorUsuario<T,U>(U _usuario, bool verificar_permissao_escolher_qualquer_farmacia, bool adicionar_farmacia_default)
        {
            Usuario usuario = (Usuario)(object)_usuario;
            IList<ViverMais.Model.Farmacia> farmacias = new List<ViverMais.Model.Farmacia>();
            bool listar_todas_farmacias = false;

            if (verificar_permissao_escolher_qualquer_farmacia) //Verifica se o usuário possui permissão para escolher qualquer uma das farmácias existentes
                listar_todas_farmacias = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "ESCOLHER_QUALQUER_FARMACIA", Modulo.FARMACIA);

            if (listar_todas_farmacias)  //Todas farmácias
                farmacias = Factory.GetInstance<IFarmacia>().ListarTodos<ViverMais.Model.Farmacia>().OrderBy(p => p.Nome).ToList();
            else //Somente farmácias vinculadas
            {
                string hql = "SELECT f FROM ViverMais.Model.Farmacia AS f, ViverMais.Model.Usuario AS u WHERE u.Codigo IN ELEMENTS(f.CodigosUsuarios) AND u.Codigo=" + usuario.Codigo;
                hql += " AND f.CodigoUnidade='" + usuario.Unidade.CNES + "' ORDER BY f.Nome";
                farmacias = Session.CreateQuery(hql).List<ViverMais.Model.Farmacia>();
            }
            
            if (adicionar_farmacia_default)
            {
                ViverMais.Model.Farmacia farmaciadefault = new ViverMais.Model.Farmacia();
                farmaciadefault.Nome = "Selecione...";
                farmaciadefault.Codigo = -1;
                farmacias.Insert(0, farmaciadefault);
            }

            return (IList<T>)(IList<ViverMais.Model.Farmacia>)farmacias;
        }

        void IFarmacia.ImportarDadosSisfarmaViverMaisProducao(int opcao)
        {
            SqlConnection con_sqlserver = new SqlConnection(@"Data Source=172.20.12.121,4433;database=sisfarma2;User id=sisfarma2;Password=#sisf123#;");
            OracleConnection con_oracle = new OracleConnection(ConexaoBancoSingle.conexao.Replace("SERVER", "Data Source").Replace("ADDRESS=", "ADDRESS_LIST=(ADDRESS=").Replace("))(CONNECT", ")))(CONNECT").Replace("uid", "User Id").Replace("pwd","Password"));
            
            con_sqlserver.Open();
            con_oracle.Open();

            if (opcao == 1) //Importar Unidade de Medida
            {
                using (OracleTransaction trans = con_oracle.BeginTransaction())
                {
                    OracleCommand ocmm, oexec = null;
                    OracleDataReader oreader = null;

                    try
                    {
                        SqlCommand scmm = con_sqlserver.CreateCommand();
                        scmm.CommandText = "select * from unidademedida";
                        SqlDataReader sreader = scmm.ExecuteReader();

                        while (sreader.Read())
                        {
                            ocmm = con_oracle.CreateCommand();
                            ocmm.CommandText = "SELECT ID_UNIDADEMEDIDA FROM FAR_UNIDADEMEDIDA WHERE ID_UNIDADEMEDIDA = " + sreader["id_unid"].ToString();
                            oreader = ocmm.ExecuteReader();

                            oexec = con_oracle.CreateCommand();

                            if (oreader.HasRows)
                            {
                                oexec.CommandText = "UPDATE FAR_UNIDADEMEDIDA SET UNIDADEMEDIDA='" + sreader["unidademedida"].ToString() + "',UND='" + sreader["und"].ToString() + "' WHERE ID_UNIDADEMEDIDA = " + sreader["id_unid"].ToString();
                                oexec.ExecuteNonQuery();
                            }
                            else
                            {
                                oexec.CommandText = "INSERT INTO FAR_UNIDADEMEDIDA (ID_UNIDADEMEDIDA,UNIDADEMEDIDA,UND,ATIVO) VALUES (" + sreader["id_unid"].ToString() + ",'" + sreader["unidademedida"].ToString() + "','" + sreader["und"].ToString() + "','Y')";
                                oexec.ExecuteNonQuery();
                            }

                            oreader.Close();
                        }

                        sreader.Close();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (trans != null)
                            trans.Rollback();

                        throw ex;
                    }
                    finally
                    {
                        con_oracle.Dispose();
                        con_oracle.Close();

                        con_sqlserver.Dispose();
                        con_sqlserver.Close();
                    }
                }
            }
            else if (opcao == 2) //Importar Medicamento
            {
                //con_sqlserver.Open();
                //con_oracle.Open();

                using (OracleTransaction trans = con_oracle.BeginTransaction())
                {
                    OracleCommand ocmm, oexec = null;
                    OracleDataReader oreader = null;

                    try
                    {
                        SqlCommand scmm = con_sqlserver.CreateCommand();
                        scmm.CommandText = "select * from medicamento where codmedicamento like '20000%' and tipo_material='M' and codmedicamento not in ('200007940','200011227','200005558','200005557','200003825','200011241')";
                        SqlDataReader sreader = scmm.ExecuteReader();

                        while (sreader.Read())
                        {
                            ocmm = con_oracle.CreateCommand();
                            ocmm.CommandText = "SELECT CODMEDICAMENTO FROM FAR_MEDICAMENTO WHERE CODMEDICAMENTO = '" + sreader["codmedicamento"].ToString() + "'";
                            oreader = ocmm.ExecuteReader();

                            oexec = con_oracle.CreateCommand();

                            if (oreader.HasRows)
                            {
                                oexec.CommandText = "UPDATE FAR_MEDICAMENTO SET MEDICAMENTO='" + sreader["medicamento"].ToString() + "', ID_UNIDADEMEDIDA=" + sreader["id_unid"].ToString() + ",IND_ANTIBIO='" + (int.Parse(sreader["ind_antibio"].ToString()) == 1 ? 'Y' : 'N') + "' WHERE CODMEDICAMENTO = '" + sreader["codmedicamento"].ToString() + "'";
                                oexec.ExecuteNonQuery();
                            }
                            else
                            {
                                oexec.CommandText = "INSERT INTO FAR_MEDICAMENTO (ID_MEDICAMENTO,CODMEDICAMENTO,MEDICAMENTO,ID_UNIDADEMEDIDA,IND_ANTIBIO,PERTENCEAREDE) VALUES (" + sreader["id_medicamento"].ToString() + ",'" + sreader["codmedicamento"].ToString() + "','" + sreader["medicamento"].ToString() + "'," + sreader["id_unid"].ToString() + ",'" + (int.Parse(sreader["ind_antibio"].ToString()) == 1 ? 'Y' : 'N') + "','Y')";
                                oexec.ExecuteNonQuery();
                            }

                            oreader.Close();
                        }

                        sreader.Close();
                        trans.Commit();
                    }
                    catch (Exception ex)
                    {
                        if (trans != null)
                            trans.Rollback();

                        throw ex;
                    }
                    finally
                    {
                        con_oracle.Dispose();
                        con_oracle.Close();

                        con_sqlserver.Dispose();
                        con_sqlserver.Close();
                    }
                }
            }
        }

        #endregion
    }
}
