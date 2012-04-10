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
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Drawing;

namespace ViverMais.View.Vacina
{
    public partial class FormSalaVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region PESQUISA_ESTABELECIMENTO
            LinkButton eas_pesquisarcnes = this.EAS.WUC_LinkButton_PesquisarCNES;
            LinkButton eas_pesquisarnome = this.EAS.WUC_LinkButton_PesquisarNomeFantasia;

            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.UpdatePanel_Unidade);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.UpdatePanel_Unidade);
            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios);
            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.UpdatePanel_UsuariosSala);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.UpdatePanel_UsuariosSala);

            eas_pesquisarcnes.Click += new EventHandler(OnClick_PesquisarCNES);
            eas_pesquisarnome.Click += new EventHandler(OnClick_PesquisarNomeFantasiaUnidade);
            #endregion PESQUISA_ESTABELECIMENTO

            GridView grid = this.WUC_PesquisarUsuario.WUC_GridView_Usuarios;
            grid.RowCommand += new GridViewCommandEventHandler(OnRowCommand_Usuario);

            DropDownList dropunidadewuc = this.WUC_PesquisarUsuario.WUC_DropDownList_Estabelecimentos;
            dropunidadewuc.SelectedIndexChanged += new EventHandler(DropDown_EAS_SelectedIndexChanged);

            this.WUC_PesquisarUsuario.WUC_LinkButton_FiltrarUsuarios.Click += new EventHandler(OnClick_PesquisarUsuarioVacina);

            this.InserirTrigger(dropunidadewuc.UniqueID, "SelectedIndexChanged", this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios);
            this.InserirTrigger(dropunidadewuc.UniqueID, "SelectedIndexChanged", this.UpdatePanel_UsuariosSala);
            this.InserirTrigger(grid.UniqueID, "RowCommand", this.UpdatePanel_UsuariosSala);

            //AsyncPostBackTrigger trig2 = new AsyncPostBackTrigger();
            //trig2.ControlID = DropDown_EAS.UniqueID;
            //trig2.EventName = "SelectedIndexChanged";

            //AsyncPostBackTrigger trig = new AsyncPostBackTrigger();
            //trig.ControlID = grid.UniqueID;
            //trig.EventName = "RowCommand";

            //this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios.Triggers.Add(trig2);

            //UpdatePanel_UsuariosSala.Triggers.Add(trig);
            //UpdatePanel_UsuariosSala.Triggers.Add(trig2);

            if (!IsPostBack)
            {
                this.WUC_PesquisarUsuario.MostrarCampoMunicipio = false;

                this.WUC_PesquisarUsuario.WUC_LinkButton_FiltrarUsuarios.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                this.WUC_PesquisarUsuario.WUC_CompareValidator_DataNascimento.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                this.WUC_PesquisarUsuario.WUC_CompareValidator_DataNascimento2.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                //this.WUC_PesquisarUsuario.WUC_ValidationSummary_PesquisaUsuario.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                this.WUC_PesquisarUsuario.WUC_RegularExpressionValidator_NomeUsuario.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";
                this.WUC_PesquisarUsuario.WUC_RegularExpressionValidator_CartaoSUS.ValidationGroup = "ValidationGroup_PesquisarUsuariosEstabelecimento";

                this.WUC_PesquisarUsuario.WUC_CustomPesquisarUsuario.Enabled = false;
                this.DropDown_EAS.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
                dropunidadewuc.Enabled = false;

                this.WUC_PesquisarUsuario.WUC_LinkButton_Voltar.Visible = false;
                this.WUC_PesquisarUsuario.WUC_PanelEstabelecimento.Visible = false;

                grid.EmptyDataText = "Não foi encontrado nenhum usuário ativo do módulo vacina na unidade selecionada.";
                grid.HeaderStyle.BackColor = ColorTranslator.FromHtml("#dcb74a");
                grid.HeaderStyle.ForeColor = ColorTranslator.FromHtml("#383838");
                grid.HeaderStyle.Font.Bold = true;
                grid.HeaderStyle.Height = Unit.Pixel(24);
                grid.HeaderStyle.Font.Size = FontUnit.Parse("13px");
                grid.BorderStyle = BorderStyle.None;

                grid.RowStyle.ForeColor = ColorTranslator.FromHtml("#333333");
                grid.RowStyle.BackColor = ColorTranslator.FromHtml("#f9e5a9");
                grid.PagerStyle.BackColor = ColorTranslator.FromHtml("#f9e5a9");
                grid.PagerStyle.ForeColor = ColorTranslator.FromHtml("#dcb74a");
                grid.FooterStyle.BackColor = ColorTranslator.FromHtml("#B5C7DE");
                grid.FooterStyle.ForeColor = ColorTranslator.FromHtml("#dcb74a");
                grid.AlternatingRowStyle.BackColor = ColorTranslator.FromHtml("#F7F7F7");

                this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseout", "this.src='img/btn_buscar1.png';");
                this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Attributes.Add("onmouseover", "this.src='img/btn_buscar2.png';");

                this.WUC_PesquisarUsuario.WUC_Image_FiltrarUsuarios.Src = "~/Vacina/img/btn_buscar1.png";

                grid.Columns.RemoveAt(0);
                BoundField bnomeusuario = new BoundField();
                bnomeusuario.DataField = "Nome";
                bnomeusuario.HeaderText = "Nome";
                grid.Columns.Insert(0, bnomeusuario);

                grid.Columns.RemoveAt(grid.Columns.Count - 1); //Removendo o nome da unidade

                ButtonField btadicionar = new ButtonField();
                btadicionar.CommandName = "CommandName_AdicionarUsuarioSala";
                btadicionar.ButtonType = ButtonType.Link;
                btadicionar.Text = "<img src='img/add-vac.png' border=0 title='Vincular Usuário a Sala de Vacina?' />";
                btadicionar.CausesValidation = true;
                grid.Columns.Insert(3, btadicionar);

                Session["atuaisusuariossala"] = new List<UsuarioVacina>();
                Session["usuariossalapesquisa"] = new List<Usuario>();
                this.EAS.WUC_EstabelecimentosPesquisados = new List<ViverMais.Model.EstabelecimentoSaude>();
                this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados = new List<Usuario>();

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SALAVACINA", Modulo.VACINA))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    int temp;

                    if (Request["co_salavacina"] != null && int.TryParse(Request["co_salavacina"].ToString(), out temp))
                    {
                        try
                        {
                            SalaVacina salavacina = Factory.GetInstance<ISalaVacina>().BuscarPorCodigo<SalaVacina>(temp);
                            ViewState["co_salavacina"] = salavacina.Codigo;
                            TextBox_Nome.Text = salavacina.Nome;

                            IList<UsuarioVacina> usuarios = Factory.GetInstance<ISalaVacina>().ListarUsuariosPorSala<UsuarioVacina, SalaVacina>(salavacina);
                            Session["atuaisusuariossala"] = usuarios;

                            DropDown_EAS.Items.Insert(1, new ListItem(salavacina.EstabelecimentoSaude.NomeFantasia, salavacina.EstabelecimentoSaude.CNES));
                            DropDown_EAS.SelectedValue = salavacina.EstabelecimentoSaude.CNES;
                            DropDown_EAS_SelectedIndexChanged(new object(), new EventArgs());

                            if (salavacina.Ativo == Convert.ToChar(SalaVacina.DescricaoSituacao.Nao))
                            {
                                RadioButton_Ativo.Checked = false;
                                RadioButton_Inativo.Checked = true;
                            }

                            if (salavacina.Bloqueado == Convert.ToChar(SalaVacina.DescricaoSituacao.Sim))
                            {
                                RadioButton_Bloqueado.Checked = true;
                                RadioButton_NaoBloqueado.Checked = false;
                            }
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                    else
                        this.CarregaUsuarios((IList<UsuarioVacina>)Session["atuaisusuariossala"]);
                }
            }
        }

        protected void OnClick_PesquisarUsuarioVacina(object sender, EventArgs e)
        {
            this.DropDown_EAS_SelectedIndexChanged(new object(), new EventArgs());
            IList<Usuario> usuarios = this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados;
            string cartaosus = this.WUC_PesquisarUsuario.WUC_CartaoSUS.Text;
            string nomeusuario = this.WUC_PesquisarUsuario.WUC_NomeUsuario.Text;
            DateTime datanascimento = string.IsNullOrEmpty(this.WUC_PesquisarUsuario.WUC_DataNascimento.Text) ? DateTime.MinValue : DateTime.Parse(this.WUC_PesquisarUsuario.WUC_DataNascimento.Text);
            
            if (!string.IsNullOrEmpty(cartaosus))
                usuarios = usuarios.Where(p => p.CartaoSUS == cartaosus).OrderBy(p => p.Nome).ToList();
            else
            {
                if (!string.IsNullOrEmpty(nomeusuario) && datanascimento != DateTime.MinValue)
                    usuarios = usuarios.Where(p => p.NomeUsuarioSemCaracterEspecial(p.Nome).StartsWith(p.NomeUsuarioSemCaracterEspecial(nomeusuario.Trim()), true, null) && p.DataNascimento.ToString("dd/MM/yyyy") == datanascimento.ToString("dd/MM/yyyy")).ToList();
                else
                {
                    if (datanascimento != DateTime.MinValue)
                        usuarios = usuarios.Where(p => p.DataNascimento.ToString("dd/MM/yyyy") == datanascimento.ToString("dd/MM/yyyy")).ToList();
                    else
                        if (!string.IsNullOrEmpty(nomeusuario))
                            usuarios = usuarios.Where(p => p.NomeUsuarioSemCaracterEspecial(p.Nome).StartsWith(p.NomeUsuarioSemCaracterEspecial(nomeusuario.Trim()), true, null)).ToList();
                }
            }

            this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados = usuarios;
            this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataSource = usuarios;
            this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataBind();
            this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios.Update();
        }

        protected void DropDown_EAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cnesunidade = DropDown_EAS.SelectedValue;

            //if (cnesunidade != "-1")
            //{
            IList<UsuarioVacina> usuariosvacina = (IList<UsuarioVacina>)Session["atuaisusuariossala"];
            IList<Usuario> usuariosmodulo = Factory.GetInstance<IUsuario>().BuscarPorModulo<Usuario>(Modulo.VACINA, cnesunidade);

            Session["usuariossalapesquisa"] = usuariosmodulo;

            this.CarregaUsuarios(usuariosvacina, usuariosmodulo);

            this.WUC_PesquisarUsuario.WUC_UpdatePanelUsuarios.Update();
            this.UpdatePanel_UsuariosSala.Update();
            //this.WUC_PesquisarUsuario.WUC_UpdatePanelEstabelecimento.Update();
            //}
        }

        protected void OnRowCommand_Usuario(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CommandName_AdicionarUsuarioSala")
            {
                int co_usuario = int.Parse(this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataKeys[int.Parse(e.CommandArgument.ToString())]["Codigo"].ToString());
                Usuario usuario = this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados
                    .Where(p => p.Codigo == co_usuario).First();

                IList<UsuarioVacina> usuariosvacina = (IList<UsuarioVacina>)Session["atuaisusuariossala"];

                UsuarioVacina usuariovacina = new UsuarioVacina();
                usuariovacina.Usuario = usuario;
                usuariovacina.Responsavel = false;

                usuariosvacina.Add(usuariovacina);
                Session["atuaisusuariossala"] = usuariosvacina;

                this.CarregaUsuarios(usuariosvacina, (IList<Usuario>)Session["usuariossalapesquisa"]);
                UpdatePanel_UsuariosSala.Update();
            }
        }

        private void CarregaUsuarios(IList<UsuarioVacina> _usuariosvacina, IList<Usuario> usuariosmodulopesquisa)
        {
            IList<UsuarioVacina> usuariosvacina = this.CarregaUsuarios(_usuariosvacina);
            this.CarregaUsuariosWUC_PesquisarUsuario(usuariosvacina, usuariosmodulopesquisa);
        }

        private void CarregaUsuariosWUC_PesquisarUsuario(IList<UsuarioVacina> usuariosvacina, IList<Usuario> usuariosmodulopesquisa)
        {
            IList<Usuario> usuariosmodulo = (from usuariomodulo in usuariosmodulopesquisa
                                             where !usuariosvacina.Select(p => p.Usuario.Codigo).Contains(usuariomodulo.Codigo)
                                             select usuariomodulo).ToList();

            this.WUC_PesquisarUsuario.WUC_UsuariosPesquisados = usuariosmodulo;
            this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataSource = usuariosmodulo;
            this.WUC_PesquisarUsuario.WUC_GridView_Usuarios.DataBind();
        }

        private IList<UsuarioVacina> CarregaUsuarios(IList<UsuarioVacina> _usuariosvacina)
        {
            IList<UsuarioVacina> usuariosvacina = this.RetornaUsuariosVacinaUnidade(_usuariosvacina);

            GridView_UsuariosSala.DataSource = usuariosvacina;
            GridView_UsuariosSala.DataBind();

            return usuariosvacina;
        }

        IList<UsuarioVacina> RetornaUsuariosVacinaUnidade(IList<UsuarioVacina> usuariosvacina)
        {
            return (from usuario in usuariosvacina
                    where usuario.Usuario.Unidade.CNES == DropDown_EAS.SelectedValue
                    select usuario).OrderBy(p => p.Usuario.Nome).ToList();
        }

        protected void OnSelectedIndexChanging_UsuarioSala(object sender, GridViewSelectEventArgs e)
        {
            int co_usuario = int.Parse(GridView_UsuariosSala.DataKeys[e.NewSelectedIndex]["CodigoUsuario"].ToString());
            IList<UsuarioVacina> usuariosvacina = (IList<UsuarioVacina>)Session["atuaisusuariossala"];
            var usuariovacina = usuariosvacina.Select((Usuario, index) => new { index, Usuario }).Where(p => p.Usuario.CodigoUsuario == co_usuario).First();

            usuariovacina.Usuario.Responsavel = !usuariovacina.Usuario.Responsavel;
            usuariosvacina[usuariovacina.index] = usuariovacina.Usuario;

            Session["atuaisusuariossala"] = usuariosvacina;
            this.CarregaUsuarios((IList<UsuarioVacina>)Session["atuaisusuariossala"]);
        }

        //protected void OnRowCommand_UsuarioSala(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "CommandName_Responsavel")
        //    {
        //        int co_usuario = int.Parse(GridView_UsuariosSala.DataKeys[int.Parse(e.CommandArgument.ToString())]["CodigoUsuario"].ToString());
        //        IList<UsuarioVacina> usuariosvacina = (IList<UsuarioVacina>)Session["atuaisusuariossala"];
        //        var usuariovacina = usuariosvacina.Select((Usuario, index) => new { index, Usuario }).Where(p => p.Usuario.CodigoUsuario == co_usuario).First();

        //        usuariovacina.Usuario.Responsavel = !usuariovacina.Usuario.Responsavel;
        //        usuariosvacina[usuariovacina.index] = usuariovacina.Usuario;

        //        Session["atuaisusuariossala"] = usuariosvacina;
        //        this.CarregaUsuarios((IList<UsuarioVacina>)Session["atuaisusuariossala"]);
        //    }
        //}

        protected void OnRowDeleting_UsuarioSala(object sender, GridViewDeleteEventArgs e)
        {
            int co_usuario = int.Parse(GridView_UsuariosSala.DataKeys[e.RowIndex]["CodigoUsuario"].ToString());
            IList<UsuarioVacina> usuariosvacina = (IList<UsuarioVacina>)Session["atuaisusuariossala"];
            var usuariovacina = usuariosvacina.Select((Usuario, index) => new { index, Usuario }).Where(p => p.Usuario.CodigoUsuario == co_usuario).First();

            usuariosvacina.RemoveAt(usuariovacina.index);
            Session["atuaisusuariossala"] = usuariosvacina;
            this.CarregaUsuarios(usuariosvacina);
            //this.CarregaUsuariosWUC_PesquisarUsuario(usuariosvacina, (IList<Usuario>)Session["usuariossalapesquisa"]);
        }

        protected void OnPageIndexChanging_UsuarioSala(object sender, GridViewPageEventArgs e)
        {
            this.CarregaUsuarios((IList<UsuarioVacina>)Session["atuaisusuariossala"]);
            GridView_UsuariosSala.PageIndex = e.NewPageIndex;
            GridView_UsuariosSala.DataBind();
        }

        protected void OnRowDataBound_UsuarioSala(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int co_usuario = int.Parse(GridView_UsuariosSala.DataKeys[e.Row.RowIndex]["CodigoUsuario"].ToString());
                IList<UsuarioVacina> usuariosvacina = this.RetornaUsuariosVacinaUnidade((IList<UsuarioVacina>)Session["atuaisusuariossala"]);

                bool responsavel = (from usuario in usuariosvacina
                                    where usuario.CodigoUsuario == co_usuario
                                    select usuario).First().Responsavel;

                LinkButton lbresponsavel = (LinkButton)e.Row.Controls[3].Controls[0];

                if (!responsavel)
                    lbresponsavel.Text = "<img src='img/ok-not.png' border=0 title='Este usuário não foi marcado como responsável da sala de vacina.'/>";
                else
                    lbresponsavel.Text = "<img src='img/ok-yes.png' border=0 title='Usuário marcado como responsável da sala de vacina.'/>";
            }
        }

        /// <summary>
        /// Salva a sala de vacina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                ISalaVacina iVacina = Factory.GetInstance<ISalaVacina>();
                SalaVacina salavacina = ViewState["co_salavacina"] != null ? iVacina.BuscarPorCodigo<SalaVacina>(int.Parse(ViewState["co_salavacina"].ToString())) : new SalaVacina();
                IList<UsuarioVacina> usuariosvacina = this.RetornaUsuariosVacinaUnidade((IList<UsuarioVacina>)Session["atuaisusuariossala"]);
                salavacina.EstabelecimentoSaude = iVacina.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(DropDown_EAS.SelectedValue);
                salavacina.Nome = TextBox_Nome.Text;
                salavacina.Ativo = RadioButton_Ativo.Checked ? Convert.ToChar(SalaVacina.DescricaoSituacao.Sim) : Convert.ToChar(SalaVacina.DescricaoSituacao.Nao);
                salavacina.Bloqueado = RadioButton_Bloqueado.Checked ? Convert.ToChar(SalaVacina.DescricaoSituacao.Sim) : Convert.ToChar(SalaVacina.DescricaoSituacao.Nao);

                if (salavacina.Codigo == 0)
                    salavacina.PertenceCMADI = false;

                if (usuariosvacina.Where(p => p.Responsavel).FirstOrDefault() == null)
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('É necessário informar pelo menos um responsável para a sala de vacina.');", true);
                else
                {
                    Factory.GetInstance<ISalaVacina>().SalvarSala<SalaVacina, UsuarioVacina>(salavacina, usuariosvacina, ((Usuario)Session["Usuario"]).Codigo);
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Sala salva com sucesso.');location='FormExibeSalaVacina.aspx';", true);
                }
            }
            catch (Exception f)
            {
                throw f;
            }
        }


        #region ESTABELECIMENTO
        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude unidade = this.EAS.WUC_EstabelecimentosPesquisados.FirstOrDefault();

            if (unidade != null)
            {
                this.DropDown_EAS.Items.Clear();
                this.DropDown_EAS.Items.Add(new ListItem(unidade.NomeFantasia, unidade.CNES));
                this.DropDown_EAS.Items.Insert(0, new ListItem("SELECIONE...", "-1"));
                this.DropDown_EAS.SelectedValue = unidade.CNES;
                this.DropDown_EAS.Focus();
                this.UpdatePanel_Unidade.Update();
                this.DropDown_EAS_SelectedIndexChanged(new object(), new EventArgs());
            }
        }

        protected void OnClick_PesquisarNomeFantasiaUnidade(object sender, EventArgs e)
        {
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = this.EAS.WUC_EstabelecimentosPesquisados;

            this.DropDown_EAS.DataSource = unidades;
            this.DropDown_EAS.DataBind();
            this.DropDown_EAS.Items.Insert(0, new ListItem("SELECIONE...", "-1"));

            this.DropDown_EAS.Focus();
            this.UpdatePanel_Unidade.Update();
            this.DropDown_EAS_SelectedIndexChanged(new object(), new EventArgs());
        }
        #endregion
    }
}