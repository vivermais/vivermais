using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Web.UI.HtmlControls;
using ViverMais.View.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;

namespace ViverMais.View.Seguranca
{
    public partial class WUCPesquisarUsuario : System.Web.UI.UserControl
    {
        public GridView WUC_GridView_Usuarios
        {
            get { return this.GridView_Usuarios; }
        }

        public DropDownList WUC_DropDownList_Estabelecimentos
        {
            get { return this.DropDownList_Estabelecimento; }
        }

        public LinkButton WUC_LinkButton_FiltrarUsuarios
        {
            get { return this.LinkButton1; }
        }

        public LinkButton WUC_LinkButton_Voltar
        {
            get { return this.LinkButton3; }
        }

        public HtmlImage WUC_Image_FiltrarUsuarios
        {
            get { return this.imgfiltrar; }
        }

        public HtmlImage WUC_Image_Voltar
        {
            get { return this.imgVoltar; }
        }

        public Panel WUC_PanelEstabelecimento
        {
            get { return this.Panel_Estabelecimento; }
        }

        public UpdatePanel WUC_UpdatePanelUsuarios
        {
            get { return this.UpdatePanel1; }
        }

        public TextBox WUC_NomeUsuario
        {
            get
            {
                return this.TextBox_NomeUsuario;
            }
        }

        public TextBox WUC_CartaoSUS
        {
            get
            {
                return this.TextBox_CartaoSUS;
            }
        }

        public TextBox WUC_DataNascimento
        {
            get
            {
                return this.TextBox_DataNascimento;
            }
        }

        public DropDownList WUC_DropDownList_Municipio
        {
            get
            {
                return this.DropDownList_Municipio;
            }
        }

        public CustomValidator WUC_CustomPesquisarUsuario
        {
            get
            {
                return this.CustomValidator_PesquisarUsuario;
            }
        }

        public RegularExpressionValidator WUC_RegularExpressionValidator_CartaoSUS 
        {
            get { return this.RegularExpressionValidator_CartaoSUS; }
        }

        public RegularExpressionValidator WUC_RegularExpressionValidator_NomeUsuario
        {
            get { return this.RegularExpressionValidator_NomeUsuario; }
        }

        public CompareValidator WUC_CompareValidator_DataNascimento
        {
            get { return this.CompareValidator_DataNascimento; }
        }

        public CompareValidator WUC_CompareValidator_DataNascimento2
        {
            get { return this.CompareValidator_DataNascimento2; }
        }

        public ValidationSummary WUC_ValidationSummary_PesquisaUsuario
        {
            get { return this.ValidationSummary_PesquisaUsuario; }
        }

        public WUC_PesquisarEstabelecimento WUC_EAS
        {
            get
            {
                return this.EAS;
            }
        }

        public IList<Usuario> WUC_UsuariosPesquisados
        {
            get
            {
                if (Session[string.Format("UsuariosPesquisados_{0}", this.UniqueID)] != null)
                    return (IList<Usuario>)Session[string.Format("UsuariosPesquisados_{0}", this.UniqueID)];

                return new List<Usuario>();
            }

            set
            {
                Session[string.Format("UsuariosPesquisados_{0}", this.UniqueID)] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton eas_pesquisarcnes = this.EAS.WUC_LinkButton_PesquisarCNES;
            LinkButton eas_pesquisarnome = this.EAS.WUC_LinkButton_PesquisarNomeFantasia;

            eas_pesquisarcnes.Click += new EventHandler(OnClick_PesquisarCNES);
            eas_pesquisarnome.Click += new EventHandler(OnClick_PesquisarNomeFantasiaUnidade);

            this.InserirTrigger(eas_pesquisarcnes.UniqueID, "Click", this.UpdatePanel_Unidade);
            this.InserirTrigger(eas_pesquisarnome.UniqueID, "Click", this.UpdatePanel_Unidade);

            if (!IsPostBack)
                this.DropDownList_Estabelecimento.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        public bool MostrarCampoMunicipio
        {
            set 
            {
                this.Panel_Municipio.Visible = value;

                if (value) 
                {
                    this.DropDownList_Municipio.DataSource = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>("BA");
                    this.DropDownList_Municipio.DataBind();
                    this.DropDownList_Municipio.Items.Insert(0, new ListItem("SELECIONE...", "0"));
                }
            }
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        protected void OnClick_PesquisarUsuario(object sender, EventArgs e)
        {
            //DropDown_EAS_SelectedIndexChanged(new object(), new EventArgs());
            IList<Usuario> usuarios = Factory.GetInstance<IUsuario>().ListarTodos<Usuario>();
            string cartaosus = WUC_CartaoSUS.Text;
            string nomeusuario = WUC_NomeUsuario.Text;
            DateTime datanascimento = string.IsNullOrEmpty(WUC_DataNascimento.Text) ? DateTime.MinValue : DateTime.Parse(WUC_DataNascimento.Text);

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

            WUC_UsuariosPesquisados = usuarios;
            WUC_GridView_Usuarios.DataSource = usuarios;
            WUC_GridView_Usuarios.DataBind();
            WUC_UpdatePanelUsuarios.Update();
        }

        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
            ViverMais.Model.EstabelecimentoSaude unidade = this.EAS.WUC_EstabelecimentosPesquisados.FirstOrDefault();
            
            if (unidade != null)
            {
                this.DropDownList_Estabelecimento.Items.Clear();
                this.DropDownList_Estabelecimento.Items.Add(new ListItem(unidade.NomeFantasia, unidade.CNES));
                this.DropDownList_Estabelecimento.Items.Insert(0, new ListItem("SELECIONE...", "0"));
                this.DropDownList_Estabelecimento.SelectedValue = unidade.CNES;
                this.DropDownList_Estabelecimento.Focus();
                this.UpdatePanel_Unidade.Update();
            }
        }

        protected void OnClick_PesquisarNomeFantasiaUnidade(object sender, EventArgs e)
        {
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = this.EAS.WUC_EstabelecimentosPesquisados;

            this.DropDownList_Estabelecimento.DataSource = unidades;
            this.DropDownList_Estabelecimento.DataBind();
            this.DropDownList_Estabelecimento.Items.Insert(0, new ListItem("SELECIONE...", "0"));

            this.DropDownList_Estabelecimento.Focus();
            this.UpdatePanel_Unidade.Update();
        }

        protected void OnServerValidate_PesquisarUsuario(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            if (this.DropDownList_Estabelecimento.SelectedValue == "0"
                && string.IsNullOrEmpty(this.TextBox_CartaoSUS.Text)
                && string.IsNullOrEmpty(this.TextBox_NomeUsuario.Text)
                && string.IsNullOrEmpty(this.TextBox_DataNascimento.Text)
                && this.DropDownList_Municipio.SelectedValue == "0")
                e.IsValid = false;
        }

        /// <summary>
        /// Paginação do gridview de usuários da unidade pertencentes ao módulo farmácia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Usuarios(object sender, GridViewPageEventArgs e)
        {
            this.GridView_Usuarios.DataSource = this.WUC_UsuariosPesquisados;
            this.GridView_Usuarios.DataBind();

            this.GridView_Usuarios.PageIndex = e.NewPageIndex;
            this.GridView_Usuarios.DataBind();
        }
    }
}