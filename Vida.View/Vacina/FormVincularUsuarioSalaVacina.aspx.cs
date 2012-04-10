using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.Drawing;

namespace ViverMais.View.Vacina
{
    public partial class FormVincularUsuarioSalaVacina : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_USUARIO_SALA_VACINA",Modulo.VACINA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    Session["usuariossalavinculo"] = new List<Usuario>();

                    Session.Remove("salasdisponiveis");
                    Session.Remove("salasvinculadas");
                }
            }

            this.WUC_PesquisarUsuario.Usuarios = (IList<Usuario>)Session["usuariossalavinculo"];
            GridView grid = this.WUC_PesquisarUsuario.WUC_GridView_Usuarios;
            this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos.SelectedIndexChanged += new EventHandler(ListarTodosUsuarios);
            grid.RowCommand += new GridViewCommandEventHandler(OnRowCommand_Usuario);
            this.WUC_PesquisarUsuario.WUC_LinkButton_ListarTodosUsuarios.Click += new EventHandler(ListarTodosUsuarios);
            this.WUC_PesquisarUsuario.WUC_LinkButton_Voltar.Click += new EventHandler(OnClick_Voltar);

            grid.EmptyDataText = "Não foi encontrado nenhum usuário ativo do módulo vacina na unidade selecionada.";
            grid.HeaderStyle.BackColor = ColorTranslator.FromHtml("#dcb74a");
            grid.HeaderStyle.ForeColor = ColorTranslator.FromHtml("#383838");
            grid.HeaderStyle.Font.Bold = true;
            grid.HeaderStyle.Height = Unit.Pixel(24);
            grid.HeaderStyle.Font.Size = FontUnit.Parse("13px");

            grid.RowStyle.ForeColor = ColorTranslator.FromHtml("#333333");
            grid.RowStyle.BackColor = ColorTranslator.FromHtml("#f9e5a9");
            grid.PagerStyle.BackColor = ColorTranslator.FromHtml("#f9e5a9");
            grid.PagerStyle.ForeColor = ColorTranslator.FromHtml("#dcb74a");
            grid.FooterStyle.BackColor = ColorTranslator.FromHtml("#B5C7DE");
            grid.FooterStyle.ForeColor = ColorTranslator.FromHtml("#dcb74a");
            grid.AlternatingRowStyle.BackColor = ColorTranslator.FromHtml("#F7F7F7");

            this.WUC_PesquisarUsuario.WUC_Image_ListarTodosUsuarios.Attributes.Add("onmouseover", "this.src='img/btn_listar_todos2.png';");
            this.WUC_PesquisarUsuario.WUC_Image_ListarTodosUsuarios.Attributes.Add("onmouseout", "this.src='img/btn_listar_todos1.png';");
            this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseout", "this.src='img/btn_filtrar1.png';");
            this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseover", "this.src='img/btn_filtrar2.png';");
            this.WUC_PesquisarUsuario.WUC_Image_Voltar.Attributes.Add("onmouseout", "this.src='img/btn_voltar1.png';");
            this.WUC_PesquisarUsuario.WUC_Image_Voltar.Attributes.Add("onmouseover", "this.src='img/btn_voltar2.png';");

            this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Src = "~/Vacina/img/btn_filtrar1.png";
            this.WUC_PesquisarUsuario.WUC_Image_ListarTodosUsuarios.Src = "~/Vacina/img/btn_listar_todos1.png";
            this.WUC_PesquisarUsuario.WUC_Image_Voltar.Src = "~/Vacina/img/btn_voltar1.png";

            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(grid);

            AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            trig.ControlID = grid.UniqueID;
            trig.EventName = "RowCommand";
            UpdatePanel_VinculoUsuarioSala.Triggers.Add(trig);

            ((ButtonField)grid.Columns[0]).CommandName = "CommandName_VerSalasVacinaUsuario";
        }

        protected void OnClick_Voltar(object sender, EventArgs e) 
        {
            Response.Redirect("Default.aspx");
        }

        protected void ListarTodosUsuarios(object sender, EventArgs e)
        {
            string cnesunidade = this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos.SelectedValue;

            if (cnesunidade != "-1")
            {
                this.WUC_PesquisarUsuario.RemoverCamposPesquisa();
                this.CarregaUsuarios(cnesunidade);
                this.WUC_PesquisarUsuario.CarregaUsuarios(this.WUC_PesquisarUsuario.Usuarios, string.Empty, DateTime.MinValue, string.Empty);
            }
        }

        private void CarregaUsuarios(string cnesunidade)
        {
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarPorModulo<Usuario>(Modulo.VACINA, cnesunidade).Where(p => p.Ativo == 1).ToList();
            Session["usuariossalavinculo"] = usuarios;
            this.WUC_PesquisarUsuario.Usuarios = usuarios;
        }

        protected void OnRowCommand_Usuario(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerSalasVacinaUsuario")
            {
                int co_usuario = int.Parse(this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                ViewState["co_usuario"] = co_usuario;

                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(co_usuario);
                Label_Unidade.Text = usuario.Unidade.NomeFantasia;
                Label_Usuario.Text = usuario.Nome;
                Label_CartaoSUSUsuario.Text = usuario.CartaoSUS;
                Label_DataNascimentoUsuario.Text = usuario.DataNascimento.ToString("dd/MM/yyyy");

                Panel_UsuarioSala.Visible = true;

                IList<SalaVacina> salasunidade = Factory.GetInstance<ISalaVacina>().BuscarPorUnidadeSaude<SalaVacina>(this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos.SelectedValue);
                IList<SalaVacina> salasusuario = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<SalaVacina, Usuario>(usuario, false, false);
                //IList<SalaVacina> salasusuario = Factory.GetInstance<ISalaVacina>().BuscarPorUsuario<ViverMais.Model.SalaVacina>(usuario.Codigo).Where(p => p.EstabelecimentoSaude.CNES == this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos.SelectedValue).ToList();
                this.CarregaListBoxesSalas(salasunidade.Where(p => !salasusuario.Select(p2 => p2.Codigo).Contains(p.Codigo)).ToList().OrderBy(p=>p.Nome).ToList(), salasusuario.ToList().OrderBy(p=>p.Nome).ToList());
            }
        }

        /// <summary>
        /// Carrega os listboxes com as salas do usuário e as disponíveis para vínculo
        /// </summary>
        /// <param name="salasdisponiveis"></param>
        /// <param name="salasusuario"></param>
        private void CarregaListBoxesSalas(IList<SalaVacina> salasdisponiveis, IList<SalaVacina> salasusuario)
        {
            //ListBox_SalasDisponiveis.Items.Clear();
            //ListBox_SalasVinculadas.Items.Clear();

            //foreach (SalaVacina sala in salasdisponiveis)
            //{
            //    ListItem item = new ListItem(sala.Nome, sala.Codigo.ToString());
            //    item.Attributes.Add("title", sala.ToolTipResponsaveis());
            //    ListBox_SalasDisponiveis.Items.Add(item);
            //}

            //foreach (SalaVacina sala in salasusuario)
            //{
            //    ListItem item = new ListItem(sala.Nome, sala.Codigo.ToString());
            //    item.Attributes.Add("title", sala.ToolTipResponsaveis());
            //    ListBox_SalasVinculadas.Items.Add(item);
            //}

            //Session["salasdisponiveis"] = salasdisponiveis;
            //Session["salasvinculadas"] = salasusuario;
        }

        protected void OnClick_CancelarVinculo(object sender, EventArgs e)
        {
            Panel_UsuarioSala.Visible = false;
        }

        protected void OnClick_SalvarVinculo(object sender, EventArgs e)
        {
            try
            {
                Factory.GetInstance<ISalaVacina>().SalvarVinculoUsuarioSala<SalaVacina>((IList<SalaVacina>)Session["salasvinculadas"], (IList<SalaVacina>)Session["salasdisponiveis"], int.Parse(ViewState["co_usuario"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo salvo com sucesso.');", true);
                this.OnClick_CancelarVinculo(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void OnClick_InserirSala(object sender, EventArgs e)
        {
            IList<SalaVacina> salasdisponiveis = (IList<SalaVacina>)Session["salasdisponiveis"];
            IList<SalaVacina> salasusuario = (IList<SalaVacina>)Session["salasvinculadas"];

            if (CustomValidator_InserirSala.IsValid)
            {
                foreach (ListItem i in ListBox_SalasDisponiveis.Items)
                {
                    if (i.Selected)
                    {
                        salasusuario.Add(salasdisponiveis.Where(p => p.Codigo == int.Parse(i.Value)).First());
                        salasdisponiveis.RemoveAt(salasdisponiveis.Select((Sala, index) => new { index, Sala }).Where(p => p.Sala.Codigo == int.Parse(i.Value)).First().index);
                    }
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_InserirSala.ErrorMessage + "');", true);

            this.CarregaListBoxesSalas(salasdisponiveis.OrderBy(p => p.Nome).ToList(), salasusuario.OrderBy(p => p.Nome).ToList());
        }

        protected void OnClick_RetirarSala(object sender, EventArgs e)
        {
            IList<SalaVacina> salasdisponiveis = (IList<SalaVacina>)Session["salasdisponiveis"];
            IList<SalaVacina> salasusuario = (IList<SalaVacina>)Session["salasvinculadas"];

            if (CustomValidator_RetirarSala.IsValid)
            {
                foreach (ListItem i in ListBox_SalasVinculadas.Items)
                {
                    if (i.Selected)
                    {
                        salasdisponiveis.Add(salasusuario.Where(p => p.Codigo == int.Parse(i.Value)).First());
                        salasusuario.RemoveAt(salasusuario.Select((Sala, index) => new { index, Sala }).Where(p => p.Sala.Codigo == int.Parse(i.Value)).First().index);
                    }
                }
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_RetirarSala.ErrorMessage + "');", true);

            this.CarregaListBoxesSalas(salasdisponiveis.OrderBy(p => p.Nome).ToList(), salasusuario.OrderBy(p => p.Nome).ToList());
        }

        protected void OnServerValidate_InserirSala(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            foreach (ListItem i in ListBox_SalasDisponiveis.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }

        protected void OnServerValidate_RetirarSala(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            foreach (ListItem i in ListBox_SalasVinculadas.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }
    }
}
