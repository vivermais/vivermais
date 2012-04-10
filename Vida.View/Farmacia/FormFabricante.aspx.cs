using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormFabricante : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FABRICANTE",Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    if (Request["co_fabricante"] != null && int.TryParse(Request["co_fabricante"].ToString(), out temp))
                    {
                        Button_Salvar.OnClientClick = "javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja alterar o registro deste fabricante ?'); return false;";

                        imgsalvar.Alt = "Alterar";
                        imgsalvar.Src = "img/btn/alterar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/alterar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/alterar1.png';");

                        try
                        {
                            FabricanteMedicamento f = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<FabricanteMedicamento>(int.Parse(Request["co_fabricante"].ToString()));
                            TextBox_Fabricante.Text = f.Nome;
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                    else 
                    {
                        imgsalvar.Alt = "Salvar";
                        imgsalvar.Src = "img/btn/salvar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/salvar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/salvar1.png';");
                    }
                }
            }
        }

        /// <summary>
        /// Salva o fabricante
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            try
            {
                IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();
                FabricanteMedicamento fabricante = Request["co_fabricante"] != null ? iFarmacia.BuscarPorCodigo<FabricanteMedicamento>(int.Parse(Request["co_fabricante"].ToString())) : new FabricanteMedicamento();
                fabricante.Nome = TextBox_Fabricante.Text;

                iFarmacia.Salvar(fabricante);
                iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_FABRICANTE,
                    "id fabricante:" + fabricante.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Fabricante salvo com sucesso!');location='Default.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "alert('" + ex.Message + "');", true);
               
                
            }
        }
    }
}
