using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Configuration;
using ViverMais.Model;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ViverMais.View.Farmacia
{
    public partial class FormImportarDados : System.Web.UI.Page
    {
        SqlConnection con_sqlserver = new SqlConnection(@"Data Source=172.22.6.21;database=DB_SIZ;User id=smssiz;Password=$m$$!z;");
        Oracle.DataAccess.Client.OracleConnection con_oracle = new Oracle.DataAccess.Client.OracleConnection("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=172.20.12.44)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ngi)));User Id=ngi;Password=#Ng1s@3De$;");

        private string FormatarNome(string nome)
        {
            nome = GenericsFunctions.RemoveDiacritics(nome);

            string nomeConcatenado = string.Empty;

            string[] nomes = nome.Split(' ');

            for (int i = 0; i < nomes.Length; i++)
            {
                if (i != 0)
                    nomeConcatenado += " ";

                if (Regex.IsMatch(nomes[i], @"^\d*\D{1}$"))
                    nomeConcatenado += nomes[i];
                else
                    nomeConcatenado += CultureInfo.CurrentCulture.TextInfo.ToTitleCase(nomes[i]);
            }

            return nomeConcatenado;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                con_sqlserver.Open();
                con_oracle.Open();

                OracleCommand objCmd = new OracleCommand("SELECT * FROM tb_pms_ocupacao_proc", con_oracle);
                OracleDataReader objReader = objCmd.ExecuteReader();
                SqlCommand cmm = null;

                while (objReader.Read())
                {
                    //cmm = con_sqlserver.CreateCommand();
                    //cmm.CommandText = "SELECT CO_UF FROM tb_localidade_uf WHERE sigla_uf='" + objReader["SG_UF"].ToString().ToUpper() + "'";

                    //SqlDataReader dr = cmm.ExecuteReader();

                    //if (dr.Read())
                    //{
                        //string co_uf = dr["CO_UF"].ToString();
                        
                        //dr.Dispose();
                        //cmm.Dispose();

                        cmm = con_sqlserver.CreateCommand();
                        cmm.CommandText = "INSERT INTO tb_profissional_ocupacao (co_ministerial, no_ocupacao) VALUES (@co_ministerio, @no_ocupacao)";

                        cmm.Parameters.Add(new SqlParameter("@co_ministerio", objReader["co_ocupacao"].ToString()));
                        cmm.Parameters.Add(new SqlParameter("@no_ocupacao", objReader["no_ocupacao"].ToString()));

                        cmm.ExecuteNonQuery();
                        cmm.Dispose();
                    //}

                    //dr.Dispose();
                    //cmm.Dispose();
                }

                //objCmd.Dispose();
                //objReader.Dispose();

                //objCmd = new OracleCommand("SELECT * FROM tb_pms_logradouro where cidade like '%SALVADOR%'", con_oracle);
                //objReader = objCmd.ExecuteReader();

                //while (objReader.Read())
                //{
                //    cmm = con_sqlserver.CreateCommand();
                //    cmm.CommandText = "SELECT * FROM tb_localidade_tipologradouro WHERE UPPER(no_tipo)='" + objReader["tipo"].ToString().ToUpper() + "'";

                //    SqlDataReader dr = cmm.ExecuteReader();

                //    if (dr.HasRows)
                //    {
                //        dr.Read();
                //        string co_tipo = dr["co_tipo"].ToString();

                //        dr.Dispose();
                //        cmm.Dispose();

                //        SqlCommand cmm2 = con_sqlserver.CreateCommand();
                //        cmm2.CommandText = "INSERT INTO tb_localidade_logradouro (no_logradouro,cep,cidade,bairro,co_tipologradouro,co_uf) VALUES (@no_logradouro,@cep,@cidade,@bairro,@co_tipologradouro,@co_uf)";

                //        cmm2.Parameters.Add(new SqlParameter("@no_logradouro", FormatarNome(objReader["logradouro"].ToString().ToLower())));
                //        cmm2.Parameters.Add(new SqlParameter("@cep", objReader["cep"].ToString()));
                //        cmm2.Parameters.Add(new SqlParameter("@cidade", FormatarNome(objReader["cidade"].ToString().ToLower())));
                //        cmm2.Parameters.Add(new SqlParameter("@bairro", FormatarNome(objReader["bairro"].ToString().ToLower())));
                //        cmm2.Parameters.Add(new SqlParameter("@co_tipologradouro", co_tipo));
                //        cmm2.Parameters.Add(new SqlParameter("@co_uf", 29));

                //        cmm2.ExecuteNonQuery();

                //        cmm2.Dispose();
                //    }

                //    dr.Dispose();
                //    cmm.Dispose();
                //}
                //DropDownList_Farmacia.DataSource = rd;
                //DropDownList_Farmacia.DataBind();

                //DropDownList_Farmacia.Items.Insert(0, new ListItem("SELECIONE...", "-1"));

                //GridView_Unidade.DataSource = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>().OrderBy(p => p.NomeFantasia).ToList();
                //GridView_Unidade.DataBind();
            }
        }

        //        //protected void OnClick_Teste(object sender, EventArgs e)
        //        //{
        //        //    using (OracleConnection objConn = new OracleConnection("User Id=ngi;Password=salvador;Data Source=172.22.6.20:1521/ViverMais;"))
        //        //    {
        //        //        OracleCommand objCmd = new OracleCommand();

        //        //        objCmd.Connection = objConn;
        //        //        objCmd.CommandText = "pckg_far_inventarioconf.Proc_ConferenciaInventario";
        //        //        objCmd.CommandType = CommandType.StoredProcedure;
        //        //        objCmd.Parameters.Add("resultado",OracleDbType.RefCursor).Direction = ParameterDirection.Output;
        //        //        objCmd.Parameters.Add("id_inventario", OracleDbType.Int16).Value = 42;
        //        //        //objCmd.Parameters.Add("cur_departments", OracleType.Cursor).Direction = ParameterDirection.Output;

        //        //        try
        //        //        {
        //        //            objConn.Open();
        //        //            OracleDataReader objReader = objCmd.ExecuteReader();
        //        //            objReader.NextResult();
        //        //        }

        //        //        catch (Exception ex)
        //        //        {
        //        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //        //        }

        //        //        objConn.Close();
        //        //    }

        //        //    //ReportDocument doc = new ReportDocument();
        //        //    //DataSet ds = new DataSet();
        //        //    //OracleDataAdapter da = new OracleDataAdapter("EXEC Proc_GeraRelatorioConferenciaInventario " + Request["co_inventario"].ToString(), ConfigurationManager.ConnectionStrings["ConnectionStringModuloFarmacia"].ConnectionString);
        //        //    //da.Fill(ds, "RelInventario");
        //        //    //doc.Database.Tables[0].SetDataSource(ds.Tables[0]);

        //        //    //doc.SetParameterValue("@FARMACIA", 1);

        //        //    //ConfigurationManager.ConnectionStrings["ConnectionStringModuloFarmacia"].ConnectionString
        //        //}

        //        protected void OnSelectedIndexChanged_CarregaCNES(object sender, EventArgs e)
        //        {
        //            if (DropDownList_Farmacia.SelectedValue != "-1")
        //            {
        //                con_sqlserver.Open();
        //                SqlCommand cmm = con_sqlserver.CreateCommand();
        //                cmm.CommandText = "select u.cnes, u.unidade from farmacia f, unidade u where f.id_unidade = u.id_unidade and f.id_farmacia = " + DropDownList_Farmacia.SelectedValue;
        //                SqlDataReader rd = cmm.ExecuteReader();
        //                rd.Read();
        //                Label_Unidade2.Text = rd["unidade"].ToString();
        //                TextBox_CNES.Text = rd["cnes"].ToString();
        //                con_sqlserver.Close();
        //                OnClick_VerificarCNES(new object(), new EventArgs());
        //            }
        //        }

        //        protected void OnClick_AdicionarFarmacia(object sender, EventArgs e)
        //        {
        //            IList<CFarmacia> lista = Session["FarmaciaImportacao"] != null ? (IList<CFarmacia>)Session["FarmaciaImportacao"] : new List<CFarmacia>();
        //            CFarmacia f = new CFarmacia();

        //            f.Codigofarmacia = int.Parse(DropDownList_Farmacia.SelectedValue);
        //            f.Farmacia = DropDownList_Farmacia.SelectedItem.Text;
        //            f.Cnesunidade = TextBox_CNES.Text;
        //            f.Unidade = Label_Unidade.Text;

        //            lista.Add(f);
        //            Session["FarmaciaImportacao"] = lista;
        //            GridView_Farmacia.DataSource = lista;
        //            GridView_Farmacia.DataBind();
        //        }

        //        protected void OnClick_VerificarCNES(object sender, EventArgs e)
        //        {
        //            ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(TextBox_CNES.Text);
        //            Label_Unidade.Text = unidade != null ? unidade.NomeFantasia : "UNIDADE NÃO ENCONTRADA.";
        //        }

        //        protected void OnRowDeleting_DeletarFarmacia(object sender, GridViewDeleteEventArgs e)
        //        {
        //            IList<CFarmacia> lista = (IList<CFarmacia>)Session["FarmaciaImportacao"];

        //            lista.RemoveAt(e.RowIndex);
        //            Session["FarmaciaImportacao"] = lista;
        //            GridView_Farmacia.DataSource = lista;
        //            GridView_Farmacia.DataBind();
        //        }

        protected void OnClick_ImportarUnidadeMedida(object sender, EventArgs e)
        {
            try
            {
                IFarmacia iFarmacia = Factory.GetInstance<IFarmacia>();
                iFarmacia.ImportarDadosSisfarmaViverMaisProducao(1);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
                return;
            }
            //            con_sqlserver.Open();
            //            con_oracle.Open();

            //            using (OracleTransaction trans = con_oracle.BeginTransaction())
            //            {
            //                OracleCommand ocmm, oexec = null;
            //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

            //                try
            //                {
            //                    SqlCommand scmm = con_sqlserver.CreateCommand();
            //                    scmm.CommandText = "select * from unidademedida";
            //                    SqlDataReader sreader = scmm.ExecuteReader();

            //                    while (sreader.Read())
            //                    {
            //                        ocmm = con_oracle.CreateCommand();
            //                        ocmm.CommandText = "SELECT ID_UNIDADEMEDIDA FROM FAR_UNIDADEMEDIDA WHERE ID_UNIDADEMEDIDA = " + sreader["id_unid"].ToString();
            //                        oreader = ocmm.ExecuteReader();

            //                        oexec = con_oracle.CreateCommand();

            //                        if (oreader.HasRows)
            //                        {
            //                            oexec.CommandText = "UPDATE FAR_UNIDADEMEDIDA SET UNIDADEMEDIDA='" + sreader["unidademedida"].ToString() + "',UND='" + sreader["und"].ToString() + "' WHERE ID_UNIDADEMEDIDA = " + sreader["id_unid"].ToString();
            //                            oexec.ExecuteNonQuery();
            //                        }
            //                        else
            //                        {
            //                            oexec.CommandText = "INSERT INTO FAR_UNIDADEMEDIDA (ID_UNIDADEMEDIDA,UNIDADEMEDIDA,UND,ATIVO) VALUES (" + sreader["id_unid"].ToString() + ",'" + sreader["unidademedida"].ToString() + "','" + sreader["und"].ToString() + "','Y')";
            //                            oexec.ExecuteNonQuery();
            //                        }

            //                        oreader.Close();
            //                    }

            //                    sreader.Close();
            //                    trans.Commit();
            //                }
            //                catch (Exception ex)
            //                {
            //                    trans.Rollback();
            //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
            //                }
            //            }

            //            con_sqlserver.Close();
            //            con_oracle.Close();

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        }

        //        protected void OnClick_ImportarFabricante(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select * from fabricante";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_FABRICANTE FROM FAR_FABRICANTE WHERE ID_FABRICANTE = " + sreader["id_fabricante"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "UPDATE FAR_FABRICANTE SET FABRICANTE='" + sreader["fabricante"].ToString() + "' WHERE ID_FABRICANTE = " + sreader["id_fabricante"].ToString();
        //                            oexec.ExecuteNonQuery();
        //                        }
        //                        else
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_FABRICANTE (ID_FABRICANTE,FABRICANTE) VALUES (" + sreader["id_fabricante"].ToString() + ",'" + sreader["fabricante"].ToString() + "')";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        protected void OnClick_ImportarMedicamento(object sender, EventArgs e)
        {
            try
            {
                IFarmacia iFarmacia = Factory.GetInstance<IFarmacia>();
                iFarmacia.ImportarDadosSisfarmaViverMaisProducao(2);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
                return;
            }

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        }

        //        protected void OnClick_ImportarElenco(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select * from grupo";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_ELENCO FROM FAR_ELENCO WHERE ID_ELENCO = " + sreader["id_grupo"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "UPDATE FAR_ELENCO SET ELENCO='" + sreader["grupo"].ToString() + "' WHERE ID_ELENCO = " + sreader["id_grupo"].ToString();
        //                            oexec.ExecuteNonQuery();
        //                        }
        //                        else
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_ELENCO (ID_ELENCO,ELENCO) VALUES (" + sreader["id_grupo"].ToString() + ",'" + sreader["grupo"].ToString() + "')";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarSubElenco(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select * from subgrupo";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_SUBELENCO FROM FAR_SUBELENCO WHERE ID_SUBELENCO = " + sreader["id_subgrupo"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "UPDATE FAR_SUBELENCO SET SUBELENCO='" + sreader["subgrupo"].ToString() + "' WHERE ID_SUBELENCO = " + sreader["id_subgrupo"].ToString();
        //                            oexec.ExecuteNonQuery();
        //                        }
        //                        else
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_SUBELENCO (ID_SUBELENCO,SUBELENCO) VALUES (" + sreader["id_subgrupo"].ToString() + ",'" + sreader["subgrupo"].ToString() + "')";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarElencoSubElenco(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select * from gruposubgrupo";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_ELENCO FROM FAR_ELENCO_SUBELENCO WHERE ID_ELENCO = " + sreader["id_grupo"].ToString() + " AND ID_SUBELENCO = " + sreader["id_subgrupo"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (!oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_ELENCO_SUBELENCO (ID_ELENCO,ID_SUBELENCO) VALUES (" + sreader["id_grupo"].ToString() + "," + sreader["id_subgrupo"].ToString() + ")";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarElencoMedicamento(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select * from grupomedicamento";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_ELENCO FROM FAR_ELENCOMEDICAMENTO WHERE ID_ELENCO = " + sreader["id_grupo"].ToString() + " AND ID_MEDICAMENTO = " + sreader["id_medicamento"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (!oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_ELENCOMEDICAMENTO (ID_ELENCO,ID_MEDICAMENTO) VALUES (" + sreader["id_grupo"].ToString() + "," + sreader["id_medicamento"].ToString() + ")";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarSubElencoMedicamento(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                OracleCommand ocmm, oexec = null;
        //                Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = "select id_medicamento,id_subgrupo from medicamento where id_subgrupo is not null";
        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {
        //                        ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_SUBELENCO FROM FAR_SUBELENCO_MEDICAMENTO WHERE ID_SUBELENCO = " + sreader["id_subgrupo"].ToString() + " AND ID_MEDICAMENTO = " + sreader["id_medicamento"].ToString();
        //                        oreader = ocmm.ExecuteReader();

        //                        oexec = con_oracle.CreateCommand();

        //                        if (!oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_SUBELENCO_MEDICAMENTO (ID_SUBELENCO,ID_MEDICAMENTO) VALUES (" + sreader["id_subgrupo"].ToString() + "," + sreader["id_medicamento"].ToString() + ")";
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();
        //                    trans.Commit();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            con_sqlserver.Close();
        //            con_oracle.Close();

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarFarmacia(object sender, EventArgs e)
        //        {
        //            if (Session["FarmaciaImportacao"] != null && ((IList<CFarmacia>)Session["FarmaciaImportacao"]).Count > 0)
        //            {
        //                IList<CFarmacia> lista = (IList<CFarmacia>)Session["FarmaciaImportacao"];

        //                con_sqlserver.Open();
        //                con_oracle.Open();

        //                using (OracleTransaction trans = con_oracle.BeginTransaction())
        //                {
        //                    OracleCommand ocmm, oexec = null;
        //                    Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                    try
        //                    {
        //                        foreach (CFarmacia farmacia in lista)
        //                        {
        //                            SqlCommand scmm = con_sqlserver.CreateCommand();
        //                            scmm.CommandText = "select * from farmacia where id_farmacia = " + farmacia.Codigofarmacia;
        //                            SqlDataReader sreader = scmm.ExecuteReader();
        //                            sreader.Read();

        //                            ocmm = con_oracle.CreateCommand();
        //                            ocmm.CommandText = "SELECT ID_FARMACIA FROM FAR_FARMACIA WHERE ID_FARMACIA = " + farmacia.Codigofarmacia;
        //                            oreader = ocmm.ExecuteReader();

        //                            oexec = con_oracle.CreateCommand();

        //                            if (!oreader.HasRows)
        //                            {
        //                                oexec.CommandText = "INSERT INTO FAR_FARMACIA (ID_FARMACIA,NOMEFARMACIA,ENDERECO,FONE,RESPONSAVEL,ID_UNIDADE_ViverMais) VALUES (" + farmacia.Codigofarmacia + ",'" + sreader["nomefarmacia"].ToString() + "','" + sreader["endereco"].ToString() + "','" + sreader["fone"].ToString() + "','" + sreader["responsavel"].ToString() + "','" + farmacia.Cnesunidade + "')";
        //                                oexec.ExecuteNonQuery();
        //                            }
        //                            else
        //                            {
        //                                oexec.CommandText = "UPDATE FAR_FARMACIA SET NOMEFARMACIA='" + sreader["nomefarmacia"].ToString() + "',ENDERECO='" + sreader["endereco"].ToString() + "',FONE='" + sreader["fone"].ToString() + "',RESPONSAVEL='" + sreader["responsavel"].ToString() + "',ID_UNIDADE_ViverMais='" + farmacia.Cnesunidade + "' WHERE ID_FARMACIA=" + farmacia.Codigofarmacia;
        //                                oexec.ExecuteNonQuery();
        //                            }

        //                            sreader.Close();
        //                        }

        //                        trans.Commit();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        trans.Rollback();
        //                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                    }
        //                }

        //                con_sqlserver.Close();
        //                con_oracle.Close();

        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //            }
        //            else
        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Adicione pelo menos uma farmácia para importação.');", true);
        //        }

        //        protected void OnClick_ImportarElencoFarmacia(object sender, EventArgs e)
        //        {
        //            if (Session["FarmaciaImportacao"] != null && ((IList<CFarmacia>)Session["FarmaciaImportacao"]).Count > 0)
        //            {
        //                IList<CFarmacia> lista = (IList<CFarmacia>)Session["FarmaciaImportacao"];

        //                con_sqlserver.Open();
        //                con_oracle.Open();

        //                using (OracleTransaction trans = con_oracle.BeginTransaction())
        //                {
        //                    OracleCommand ocmm, oexec = null;
        //                    Oracle.DataAccess.Client.OracleDataReader oreader = null;

        //                    try
        //                    {
        //                        foreach (CFarmacia farmacia in lista)
        //                        {
        //                            SqlCommand scmm = con_sqlserver.CreateCommand();
        //                            scmm.CommandText = "select * from grupofarmacia where id_farmacia=" + farmacia.Codigofarmacia;
        //                            SqlDataReader sreader = scmm.ExecuteReader();

        //                            while (sreader.Read())
        //                            {
        //                                ocmm = con_oracle.CreateCommand();
        //                                ocmm.CommandText = "SELECT ID_ELENCO FROM FAR_ELENCOFARMACIA WHERE ID_ELENCO = " + sreader["id_grupo"].ToString() + " AND ID_FARMACIA = " + sreader["id_farmacia"].ToString();
        //                                oreader = ocmm.ExecuteReader();

        //                                oexec = con_oracle.CreateCommand();

        //                                if (!oreader.HasRows)
        //                                {
        //                                    oexec.CommandText = "INSERT INTO FAR_ELENCOFARMACIA (ID_FARMACIA,ID_ELENCO) VALUES (" + sreader["id_farmacia"].ToString() + "," + sreader["id_grupo"].ToString() + ")";
        //                                    oexec.ExecuteNonQuery();
        //                                }
        //                            }

        //                            sreader.Close();
        //                        }

        //                        trans.Commit();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        trans.Rollback();
        //                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                    }
        //                }

        //                con_sqlserver.Close();
        //                con_oracle.Close();

        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //            }
        //            else
        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Adicione pelo menos uma farmácia para importação.');", true);
        //        }

        //        protected void OnClick_ImportarLoteMedicamento(object sender, EventArgs e)
        //        {
        //            con_sqlserver.Open();
        //            con_oracle.Open();

        //            using (OracleTransaction trans = con_oracle.BeginTransaction())
        //            {
        //                try
        //                {
        //                    SqlCommand scmm = con_sqlserver.CreateCommand();
        //                    scmm.CommandText = @"select min(id_lotemedicamento) as idlote, id_medicamento, lote, id_fabricante, CONVERT(VARCHAR(20),validade,103) as validade
        //                                    from lotemedicamento where lote is not null and ltrim(rtrim(lote)) <> '' and id_medicamento is not null
        //                                    and id_fabricante is not null and validade is not null
        //                                    group by id_medicamento, lote, id_fabricante, validade order by idlote";

        //                    SqlDataReader sreader = scmm.ExecuteReader();

        //                    while (sreader.Read())
        //                    {

        //                        OracleCommand ocmm = con_oracle.CreateCommand();
        //                        ocmm.CommandText = "SELECT ID_LOTEMEDICAMENTO FROM FAR_LOTEMEDICAMENTO WHERE ID_LOTEMEDICAMENTO=" + sreader["idlote"].ToString();
        //                        OracleDataReader oreader = ocmm.ExecuteReader();

        //                        OracleCommand oexec = con_oracle.CreateCommand();

        //                        if (!oreader.HasRows)
        //                        {
        //                            oexec.CommandText = "INSERT INTO FAR_LOTEMEDICAMENTO (ID_LOTEMEDICAMENTO,ID_MEDICAMENTO,LOTE,ID_FABRICANTE,VALIDADE) VALUES (" + sreader["idlote"].ToString() + "," + sreader["id_medicamento"].ToString() + ",'" + sreader["lote"].ToString() + "'," + sreader["id_fabricante"].ToString() + ",'" + sreader["validade"].ToString() + "')";
        //                            oexec.ExecuteNonQuery();
        //                        }
        //                        else
        //                        {
        //                            oexec.CommandText = "UPDATE FAR_LOTEMEDICAMENTO SET ID_MEDICAMENTO=" + sreader["id_medicamento"].ToString() + ",LOTE='" + sreader["lote"].ToString() + "',ID_FABRICANTE=" + sreader["id_fabricante"].ToString() + ",VALIDADE='" + sreader["validade"].ToString() + "' WHERE ID_LOTEMEDICAMENTO=" + sreader["idlote"].ToString();
        //                            oexec.ExecuteNonQuery();
        //                        }

        //                        oreader.Close();
        //                    }

        //                    sreader.Close();

        //                    trans.Commit();

        //                    con_sqlserver.Close();
        //                    con_oracle.Close();
        //                }
        //                catch (Exception ex)
        //                {
        //                    trans.Rollback();
        //                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                }
        //            }

        //            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);
        //        }

        //        protected void OnClick_ImportarEstoqueFarmacia(object sender, EventArgs e)
        //        {
        //            if (Session["FarmaciaImportacao"] != null && ((IList<CFarmacia>)Session["FarmaciaImportacao"]).Count > 0)
        //            {
        //                string sql = string.Empty;
        //                IList<CFarmacia> lista = (IList<CFarmacia>)Session["FarmaciaImportacao"];
        //                foreach (CFarmacia farmacia in lista)
        //                {
        //                    sql += farmacia.Codigofarmacia + ",";
        //                }

        //                sql = sql.Remove(sql.Length - 1, 1);

        //                con_sqlserver.Open();
        //                con_oracle.Open();

        //                using (OracleTransaction trans = con_oracle.BeginTransaction())
        //                {
        //                    try
        //                    {
        //                        SqlCommand scmm = con_sqlserver.CreateCommand();
        //                        scmm.CommandText = @"select estoque.id_farmacia, estoque.qtdestoque,estoque.id_lotemedicamento,
        //                                            lotemedicamento.id_medicamento, lotemedicamento.id_fabricante, lotemedicamento.lote,CONVERT(VARCHAR(20),lotemedicamento.validade,103)
        //                                            from estoque inner join lotemedicamento on lotemedicamento.id_lotemedicamento = estoque.id_lotemedicamento
        //                                            where estoque.id_farmacia in (" + sql + ") order by estoque.id_farmacia";
        //                        SqlDataReader sreader = scmm.ExecuteReader();

        //                        while(sreader.Read())
        //                        {
        //                            OracleCommand ocmm = con_oracle.CreateCommand();
        //                            ocmm.CommandText = "SELECT ID_LOTEMEDICAMENTO FROM FAR_LOTEMEDICAMENTO WHERE ID_LOTEMEDICAMENTO=" + sreader["id_lotemedicamento"].ToString();
        //                            OracleDataReader oreader = ocmm.ExecuteReader();
        //                            OracleCommand ocmm2 = null;
        //                            OracleDataReader oreader2 = null;

        //                            int id_lote = -1;

        //                            if (!oreader.HasRows)
        //                            {
        //                                ocmm2 = con_oracle.CreateCommand();
        //                                ocmm2.CommandText = @"SELECT ID_LOTEMEDICAMENTO FROM FAR_LOTEMEDICAMENTO WHERE ID_FABRICANTE="
        //                                                    + sreader["id_fabricante"].ToString() + " AND ID_MEDICAMENTO=" + sreader["id_medicamento"].ToString()
        //                                                    + " AND LOTE='" + sreader["lote"].ToString() + "'" + " AND TO_CHAR(VALIDADE,'DD/MM/YYYY')='" + sreader["validade"].ToString() + "'"
        //                                    ;
        //                                oreader2 = ocmm2.ExecuteReader();

        //                                if (oreader2.HasRows)
        //                                {
        //                                    oreader2.Read();
        //                                    id_lote = int.Parse(oreader2["id_lotemedicamento"].ToString());
        //                                    oreader2.Close();
        //                                }
        //                            }
        //                            else
        //                            {
        //                                oreader.Read();
        //                                id_lote = int.Parse(oreader["id_lotemedicamento"].ToString());
        //                            }

        //                            oreader.Close();

        //                            if (id_lote != -1)
        //                            {
        //                                ocmm = con_oracle.CreateCommand();
        //                                ocmm.CommandText = "SELECT QTDESTOQUE FROM FAR_ESTOQUE WHERE ID_LOTEMEDICAMENTO=" + id_lote + " AND ID_FARMACIA=" + sreader["id_farmacia"].ToString();
        //                                oreader = ocmm.ExecuteReader();

        //                                ocmm2 = con_oracle.CreateCommand();

        //                                if (oreader.HasRows)
        //                                    ocmm2.CommandText = "UPDATE FAR_ESTOQUE SET QTDESTOQUE=" + sreader["qtdestoque"].ToString() + " WHERE ID_LOTEMEDICAMENTO=" + id_lote + " AND ID_FARMACIA=" + sreader["id_farmacia"].ToString();
        //                                else
        //                                    ocmm2.CommandText = "INSERT INTO FAR_ESTOQUE (ID_FARMACIA,ID_LOTEMEDICAMENTO,QTDESTOQUE) VALUES (" + sreader["id_farmacia"].ToString() + "," + id_lote + "," + sreader["qtdestoque"].ToString() + ")";

        //                                ocmm2.ExecuteNonQuery();

        //                                oreader.Close();
        //                            }
        //                        }

        //                        sreader.Close();
        //                        trans.Commit();

        //                        con_sqlserver.Close();
        //                        con_oracle.Close();
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        trans.Rollback();
        //                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
        //                    }
        //                }

        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Importação realizada com sucesso.');", true);

        //            }else
        //                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Adicione pelo menos uma farmácia para importação.');", true);
        //        }

        //        public class CFarmacia
        //        {
        //            public CFarmacia()
        //            {
        //            }

        //            private string farmacia;

        //            public string Farmacia
        //            {
        //                get { return farmacia; }
        //                set { farmacia = value; }
        //            }

        //            private int codigofarmacia;

        //            public int Codigofarmacia
        //            {
        //                get { return codigofarmacia; }
        //                set { codigofarmacia = value; }
        //            }
        //            private string unidade;

        //            public string Unidade
        //            {
        //                get { return unidade; }
        //                set { unidade = value; }
        //            }
        //            private string cnesunidade;

        //            public string Cnesunidade
        //            {
        //                get { return cnesunidade; }
        //                set { cnesunidade = value; }
        //            }
        //        }
    }
}
