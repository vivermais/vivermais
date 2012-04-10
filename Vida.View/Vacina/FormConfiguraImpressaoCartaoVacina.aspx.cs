using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;

namespace ViverMais.View.Vacina
{
    public partial class FormConfiguraImpressaoCartaoVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

                if (paciente != null)
                {
                    Label_Paciente.Text = paciente.Nome;
                    DropDownCartaoVacina.Items.Add(new ListItem("Criança", CartaoVacina.CRIANCA.ToString()));
                    DropDownCartaoVacina.Items.Add(new ListItem("Adolescente", CartaoVacina.ADOLESCENTE.ToString()));
                    DropDownCartaoVacina.Items.Add(new ListItem("Adulto e Idoso", CartaoVacina.ADULTOIDOSO.ToString()));
                    DropDownCartaoVacina.Items.Add(new ListItem("Histórico", CartaoVacina.HISTORICO.ToString()));

                    AvatarCartaoVacina avatar = new AvatarCartaoVacina(AvatarCartaoVacina.PADRAO);
                    this.CarregaHistoricoCartaoVacina(paciente.Codigo);
                    Session["avatarimpressao"] = avatar;
                    this.OnSelectedIndexChanged_CartaoVacina(new object(), new EventArgs());
                    //this.SelecionarAvatarCartaoVacina(int.Parse(DropDownCartaoVacina.SelectedValue), avatar);

                    DataList_Thumb.DataSource = AvatarCartaoVacina.RetornaTodosAvatares();
                    DataList_Thumb.DataBind();

                    Image_ImagemPrincipalAvatar.ImageUrl = avatar.ImagemPrincipal();
                    Image_ImagemPrincipalAvatar.DataBind();
                }
            }
        }

        void CarregaHistoricoCartaoVacina(string co_paciente)
        {
            Hashtable hash = Factory.GetInstance<ICartaoVacina>().RetornaCartoesPaciente(co_paciente);
            IList<CartaoVacina> cartaocrianca = (IList<CartaoVacina>)hash["cartaocrianca"];
            IList<CartaoVacina> cartaoadolescente = (IList<CartaoVacina>)hash["cartaoadolescente"];
            IList<CartaoVacina> cartaoadulto = (IList<CartaoVacina>)hash["cartaoadulto"];
            IList<CartaoVacina> cartaohistorico = (IList<CartaoVacina>)hash["cartaohistorico"];

            Session["cartaocrianca"] = cartaocrianca;
            Session["cartaoadolescente"] = cartaoadolescente;
            Session["cartaoadulto"] = cartaoadulto;
            Session["cartaohistorico"] = cartaohistorico;
        }

        protected void OnSelectedIndexChanged_CartaoVacina(object sender, EventArgs e)
        {
            IList<CartaoVacina> cartao = new List<CartaoVacina>();
            string cartaoselecionado = DropDownCartaoVacina.SelectedValue;

            if (cartaoselecionado == CartaoVacina.CRIANCA.ToString())
                cartao = (IList<CartaoVacina>)Session["cartaocrianca"];
            else if (cartaoselecionado == CartaoVacina.ADULTOIDOSO.ToString())
                cartao = (IList<CartaoVacina>)Session["cartaoadulto"];
            else if (cartaoselecionado == CartaoVacina.ADOLESCENTE.ToString())
                cartao = (IList<CartaoVacina>)Session["cartaoadolescente"];
            else
                cartao = (IList<CartaoVacina>)Session["cartaohistorico"];

            GridView_CartaoVacina.DataSource = cartao;
            GridView_CartaoVacina.DataBind();

            AvatarCartaoVacina avatar = (AvatarCartaoVacina)Session["avatarimpressao"];
            avatar.CartaoVacinaSelecionado = int.Parse(cartaoselecionado);
            this.SelecionarAvatarCartaoVacina(avatar);
        }

        void SelecionarAvatarCartaoVacina(AvatarCartaoVacina avatar)
        {
            Session["avatarimpressao"] = avatar;
            ImagemTopo.ImageUrl = avatar.RetornaImagemTopo();
            ImagemTopo.DataBind();

            img_dadosvacina.ImageUrl = avatar.RetornaImagemCabecalho();
            img_dadosvacina.DataBind();
        }

        protected void OnClick_SelecionaAvatar(object sender, EventArgs e)
        {
            Session["avatarimpressao"] = new AvatarCartaoVacina(int.Parse(ViewState["co_avatar"].ToString()));
            this.OnSelectedIndexChanged_CartaoVacina(new object(), new EventArgs());
            this.OnClick_CancelarEscolhaTema(new object(), new EventArgs());
        }

        protected void OnClick_LnkImprimir(object sender, EventArgs e)
        {
            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)Session["pacienteSelecionado"];

            if (paciente != null)
            {
                ReportDocument doc = new ReportDocument();
                //doc.Load(Server.MapPath("RelatoriosCrystal/RelImpressaoCartaoVacina.rpt"));

                AvatarCartaoVacina avatar = (AvatarCartaoVacina)Session["avatarimpressao"];
                IList<CartaoVacina> cartoes = new List<CartaoVacina>();

                if (avatar.CartaoVacinaSelecionado == CartaoVacina.CRIANCA)
                    cartoes = (IList<CartaoVacina>)Session["cartaocrianca"];
                else if (avatar.CartaoVacinaSelecionado == CartaoVacina.ADULTOIDOSO)
                    cartoes = (IList<CartaoVacina>)Session["cartaoadulto"];
                else if (avatar.CartaoVacinaSelecionado == CartaoVacina.ADOLESCENTE)
                    cartoes = (IList<CartaoVacina>)Session["cartaoadolescente"];
                else
                    cartoes = (IList<CartaoVacina>)Session["cartaohistorico"];

                //Hashtable hash = Factory.GetInstance<IRelatorioVacina>().ObterCartaoVacina<ViverMais.Model.Paciente, AvatarCartaoVacina, CartaoVacina>(paciente, avatar, cartoes);

                //doc.Database.Tables["dadoscartao"].SetDataSource((DataTable)hash["cabecalho"]);
                //doc.Database.Tables["vacinas"].SetDataSource((DataTable)hash["corpo"]);

                doc = Factory.GetInstance<IRelatorioVacina>().ObterCartaoVacina<ViverMais.Model.Paciente, AvatarCartaoVacina, CartaoVacina>(paciente, avatar, cartoes);

                Session["documentoImpressaoVacina"] = doc;
                Response.Redirect("FormMostrarRelatorioCrystalImpressao.aspx?nome_arquivo=cartaovacina.pdf");
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "open", "window.open('FormMostrarRelatorioCrystalImpressao.aspx','_self','toolbar=no,width=500,height=400');", true);
            }
        }

        protected void OnClick_EscolherTema(object sender, EventArgs e)
        {
            Panel_TemaCartao.Visible = true;
        }

        protected void OnClick_CancelarEscolhaTema(object sender, EventArgs e)
        {
            Panel_TemaCartao.Visible = false;
        }

        protected void OnClick_VisualizarImagemPrincipal(object sender, EventArgs e)
        {
            ImageButton img = (ImageButton)sender;
            AvatarCartaoVacina avatar = new AvatarCartaoVacina(int.Parse(img.CommandArgument.ToString()));
            ViewState["co_avatar"] = avatar.Codigo;
            Image_ImagemPrincipalAvatar.ImageUrl = avatar.ImagemPrincipal();
            Image_ImagemPrincipalAvatar.DataBind();
        }
    }
}
