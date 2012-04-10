﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Drawing;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.View.Farmacia
{
    public partial class VincularUsuarioFarmacia : System.Web.UI.Page
    {
        protected void Page_load(object sender, EventArgs e)
        {
            GridView grid = this.WUC_PesquisarUsuario.WUC_GridView_Usuarios;

            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "VINCULAR_USUARIO_FARMACIA", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    this.WUC_PesquisarUsuario.MostrarCampoMunicipio = false;

                    this.WUC_PesquisarUsuario.WUC_LinkButton_FiltrarUsuarios.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                    this.WUC_PesquisarUsuario.WUC_CompareValidator_DataNascimento.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                    this.WUC_PesquisarUsuario.WUC_CompareValidator_DataNascimento2.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                    //this.WUC_PesquisarUsuario.WUC_ValidationSummary_PesquisaUsuario.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                    this.WUC_PesquisarUsuario.WUC_RegularExpressionValidator_NomeUsuario.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                    this.WUC_PesquisarUsuario.WUC_RegularExpressionValidator_CartaoSUS.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";

                    this.WUC_PesquisarUsuario.WUC_CustomPesquisarUsuario.Enabled = false;
                    //this.DropDown_EAS.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                    //dropunidadewuc.Enabled = false;

                    this.WUC_PesquisarUsuario.WUC_LinkButton_Voltar.Visible = false;
                    this.WUC_PesquisarUsuario.WUC_PanelEstabelecimento.Visible = false;

                    this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseout", "this.src='img/btn_buscar1.png';");
                    this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseover", "this.src='img/btn_buscar2.png';");

                    this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Src = "~/Farmacia/img/btn_buscar1.png";

                   // this.WUC_PesquisarUsuario.WUC_LinkButton_FiltrarUsuarios.Click += new EventHandler(OnClick_PesquisarUsuarioVacina);
                    
                    
                    Session["usuariosfarmaciavinculo"] = new List<Usuario>();

                    Session.Remove("farmaciasdisponiveis");
                    Session.Remove("farmaciasvinculadas");

                    grid.EmptyDataText = "Não foi encontrado nenhum usuário ativo do módulo farmácia na unidade selecionada.";
                    grid.HeaderStyle.BackColor = ColorTranslator.FromHtml("#194129");
                    grid.HeaderStyle.ForeColor = Color.White;
                    grid.RowStyle.ForeColor = Color.Black;
                    grid.RowStyle.BackColor = ColorTranslator.FromHtml("#f0f0f0");
                    grid.PagerStyle.BackColor = ColorTranslator.FromHtml("#194129");
                    grid.PagerStyle.ForeColor = Color.White;

                    ((ButtonField)grid.Columns[0]).CommandName = "CommandName_VerFarmaciasUsuario";
                }
            }

            this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados = (IList<Usuario>)Session["usuariosfarmaciavinculo"];
            this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos.SelectedIndexChanged += new EventHandler(ListarTodosUsuarios);
            grid.RowCommand += new GridViewCommandEventHandler(OnRowCommand_Usuario);
            //this.WUC_PesquisarUsuario.WUC_LinkButton_ListarTodosUsuarios.Click += new EventHandler(ListarTodosUsuarios);
            //this.WUC_PesquisarUsuario.WUC_LinkButton_Voltar.Click += new EventHandler(OnClick_Voltar);

            MasterMain mm = (MasterMain)Master.Master;
            ((ScriptManager)mm.FindControl("ScriptManager1")).RegisterAsyncPostBackControl(grid);
            AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            trig.ControlID = grid.UniqueID;
            trig.EventName = "RowCommand";
            UpdatePanel_VinculoUsuarioFarmacia.Triggers.Add(trig);
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
               // this.WUC_PesquisarUsuario.RemoverCamposPesquisa();
                this.CarregaUsuarios(cnesunidade);
               // this.WUC_PesquisarUsuario.CarregaUsuarios(this.WUC_PesquisarUsuario.Usuarios, string.Empty, DateTime.MinValue, string.Empty);
            }
        }

        private void CarregaUsuarios(string cnesunidade)
        {
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().BuscarPorModulo<Usuario>(Modulo.FARMACIA, cnesunidade);
            Session["usuariosfarmaciavinculo"] = usuarios;
            this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados = usuarios;
        }

        /// <summary>
        /// Verifica qual ação está sendo executada pelo usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowCommand_Usuario(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_VerFarmaciasUsuario")
            {
                int co_usuario = int.Parse(this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                ViewState["co_usuario"] = co_usuario;

                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(co_usuario);
                Label_Unidade.Text = usuario.Unidade.NomeFantasia;
                Label_Usuario.Text = usuario.Nome;
                Label_CartaoSUSUsuario.Text = usuario.CartaoSUS;
                Label_DataNascimentoUsuario.Text = usuario.DataNascimento.ToString("dd/MM/yyyy");

                Panel_UsuarioFarmacia.Visible = true;
                IList<ViverMais.Model.Farmacia> farmaciasunidade = Factory.GetInstance<IFarmacia>().BuscarPorEstabelecimentoSaude<ViverMais.Model.Farmacia>(usuario.Unidade.CNES);
                IList<ViverMais.Model.Farmacia> farmaciasusuario = Factory.GetInstance<IFarmacia>().BuscarPorUsuario<ViverMais.Model.Farmacia, Usuario>(usuario, false, false);
                this.CarregaListBoxesFarmacias(farmaciasunidade.Where(p => !farmaciasusuario.Select(p2 => p2.Codigo).Contains(p.Codigo)).ToList(), farmaciasusuario.ToList());
            }
        }

        /// <summary>
        /// Carrega os listboxes com as farmácias do usuário e as disponíveis para vínculo
        /// </summary>
        /// <param name="farmaciasdisponiveis"></param>
        /// <param name="farmaciasusuario"></param>
        private void CarregaListBoxesFarmacias(IList<ViverMais.Model.Farmacia> farmaciasdisponiveis, IList<ViverMais.Model.Farmacia> farmaciasusuario)
        {
            ListBox_FarmaciasDisponiveis.DataSource = farmaciasdisponiveis.OrderBy(p => p.Nome).ToList();
            ListBox_FarmaciasDisponiveis.DataBind();
            Session["farmaciasdisponiveis"] = farmaciasdisponiveis;
            ListBox_FarmaciasVinculadas.DataSource = farmaciasusuario.OrderBy(p => p.Nome).ToList();
            ListBox_FarmaciasVinculadas.DataBind();
            Session["farmaciasvinculadas"] = farmaciasusuario;
        }

        /// <summary>
        /// Cancela a realização do vínculo com as farmácias
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_CancelarVinculo(object sender, EventArgs e)
        {
            Panel_UsuarioFarmacia.Visible = false;
        }

        /// <summary>
        /// Salva o vínculo do usuário com a farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_SalvarVinculo(object sender, EventArgs e)
        {
            try
            {
                Factory.GetInstance<IFarmacia>().SalvarVinculoUsuarioFarmacia<ViverMais.Model.Farmacia>((IList<ViverMais.Model.Farmacia>)Session["farmaciasvinculadas"], (IList<ViverMais.Model.Farmacia>)Session["farmaciasdisponiveis"], int.Parse(ViewState["co_usuario"].ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Vínculo salvo com sucesso.');", true);
                this.OnClick_CancelarVinculo(new object(), new EventArgs());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Validação para vincular da(s) farmácia(s) ao usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_InserirFarmacia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            foreach (ListItem i in ListBox_FarmaciasDisponiveis.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Validação para retirada da(s) farmácia(s) vinculada(s) ao usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnServerValidate_RetirarFarmacia(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = false;

            foreach (ListItem i in ListBox_FarmaciasVinculadas.Items)
            {
                if (i.Selected)
                {
                    e.IsValid = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Vincula a farmácia ao usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_InserirFarmacia(object sender, EventArgs e)
        {
            if (CustomValidator_InserirFarmacia.IsValid)
            {
                IList<ViverMais.Model.Farmacia> farmaciasdisponiveis = (IList<ViverMais.Model.Farmacia>)Session["farmaciasdisponiveis"];
                IList<ViverMais.Model.Farmacia> farmaciasusuario = (IList<ViverMais.Model.Farmacia>)Session["farmaciasvinculadas"];

                foreach (ListItem i in ListBox_FarmaciasDisponiveis.Items)
                {
                    if (i.Selected)
                    {
                        farmaciasusuario.Add(farmaciasdisponiveis.Where(p => p.Codigo == int.Parse(i.Value)).First());
                        farmaciasdisponiveis.RemoveAt(farmaciasdisponiveis.Select((Farmacia, index) => new { index, Farmacia }).Where(p => p.Farmacia.Codigo == int.Parse(i.Value)).First().index);
                    }
                }

                this.CarregaListBoxesFarmacias(farmaciasdisponiveis, farmaciasusuario);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_InserirFarmacia.ErrorMessage + "');", true);
        }

        /// <summary>
        /// Retira a farmácia vinculada ao usuário
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_RetirarFarmacia(object sender, EventArgs e)
        {
            if (CustomValidator_RetirarFarmacia.IsValid)
            {
                IList<ViverMais.Model.Farmacia> farmaciasdisponiveis = (IList<ViverMais.Model.Farmacia>)Session["farmaciasdisponiveis"];
                IList<ViverMais.Model.Farmacia> farmaciasusuario = (IList<ViverMais.Model.Farmacia>)Session["farmaciasvinculadas"];

                foreach (ListItem i in ListBox_FarmaciasVinculadas.Items)
                {
                    if (i.Selected)
                    {
                        farmaciasdisponiveis.Add(farmaciasusuario.Where(p => p.Codigo == int.Parse(i.Value)).First());
                        farmaciasusuario.RemoveAt(farmaciasusuario.Select((Farmacia, index) => new { index, Farmacia }).Where(p => p.Farmacia.Codigo == int.Parse(i.Value)).First().index);
                    }
                }

                this.CarregaListBoxesFarmacias(farmaciasdisponiveis, farmaciasusuario);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('" + CustomValidator_RetirarFarmacia.ErrorMessage + "');", true);
        }
    }
}
