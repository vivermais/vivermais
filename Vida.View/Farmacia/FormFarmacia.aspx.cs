using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormFarmacia : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FARMACIA", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    IList<ViverMais.Model.EstabelecimentoSaude> les = Factory.GetInstance<IEstabelecimentoSaude>().BuscarEstabelecimentoPorNaturezaOrganizacao<ViverMais.Model.EstabelecimentoSaude>("1").OrderBy(p => p.NomeFantasia).ToList();
                    foreach (ViverMais.Model.EstabelecimentoSaude es in les)
                        DropDownList_Unidade.Items.Add(new ListItem(es.NomeFantasia, es.CNES));

                    //Label_Estabelecimento.Text = ((Usuario)Session["Usuario"]).Unidade.NomeFantasia;

                    if (Request["co_farmacia"] != null && int.TryParse(Request["co_farmacia"].ToString(), out temp))
                    {
                        imgsalvar.Alt = "Alterar";
                        imgsalvar.Src = "img/btn/alterar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/alterar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/alterar1.png';");

                        Button_Salvar.OnClientClick = "javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja alterar o registro desta farmácia ?'); return false;";

                        try
                        {
                            ViverMais.Model.Farmacia f = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(Request["co_farmacia"].ToString()));
                            TextBox_Farmacia.Text = f.Nome;
                            TextBox_Endereco.Text = f.Endereco;
                            TextBox_Responsavel.Text = f.Responsavel;
                            TextBox_Telefone.Text = !string.IsNullOrEmpty(f.Fone) ? f.Fone : "";
                            DropDownList_Unidade.SelectedValue = f.CodigoUnidade;
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
        /// Salva a farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            try
            {
                IFarmacia iFarmacia = Factory.GetInstance<IFarmacia>();
                ViverMais.Model.Farmacia farmacia = Request["co_farmacia"] != null ? iFarmacia.BuscarPorCodigo<ViverMais.Model.Farmacia>(int.Parse(Request["co_farmacia"].ToString())) : new ViverMais.Model.Farmacia();

                farmacia.Nome        = TextBox_Farmacia.Text;
                farmacia.Endereco    = TextBox_Endereco.Text;
                farmacia.Responsavel = TextBox_Responsavel.Text;
                farmacia.Fone        = TextBox_Telefone.Text;
                //f.CodigoUnidade = ((Usuario)Session["Usuario"]).Unidade.Codigo;
                farmacia.CodigoUnidade = DropDownList_Unidade.SelectedValue;

                iFarmacia.Salvar(farmacia);
                iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_FARMACIA,
                    "id farmacia:" + farmacia.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Farmácia salva com sucesso!');location='Default.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}
