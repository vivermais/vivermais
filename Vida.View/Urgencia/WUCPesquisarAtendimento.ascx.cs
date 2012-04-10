using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class WUCPesquisarAtendimento : System.Web.UI.UserControl
    {
        //public CustomValidator WUC_CustomValidator_PesquisarAtendimento
        //{
        //    get
        //    {
        //        return this.CustomValidator_PesquisarAtendimento;
        //    }
        //}

        //public Panel WUC_Panel_ResultadoPesquisa
        //{
        //    get
        //    {
        //        return this.Panel_ResultadoPesquisa;
        //    }
        //}

        //public UpdatePanel WUC_UpdatePanel_ResultadoPesquisa
        //{
        //    get
        //    {
        //        return this.UpdatePanel_BuscaRegistro;
        //    }
        //}

        //public LinkButton WUC_LinkButton_Pesquisar 
        //{
        //    get { return this.LinkButton_Pesquisar; }
        //}

        //public LinkButton WUC_LinkButton_ListarTodos
        //{
        //    get { return this.LinkButton_ListarTodos; }
        //}

        //public TextBox WUC_TextBox_NumeroAtendimento 
        //{
        //    get { return this.TextBox_NumeroAtendimento; }
        //}

        //public TextBox WUC_TextBox_DataInicialAtendimento
        //{
        //    get { return this.TextBox_DataInicialAtendimento; }
        //}

        //public TextBox WUC_TextBox_DataFinalAtendimento
        //{
        //    get { return this.TextBox_DataFinalAtendimento; }
        //}

        //public GridView WUC_GridViewAtendimentos
        //{
        //    get
        //    {
        //        return this.GridView_PacienteUrgence;
        //    }
        //}

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //}

        //protected void OnPageIndexChanging_PacienteUrgence(object sender, GridViewPageEventArgs e)
        //{
        //    this.GridView_PacienteUrgence.DataSource = (DataTable)Session["atendimentosUrgencePesquisados"];
        //    this.GridView_PacienteUrgence.DataBind();
        //    this.GridView_PacienteUrgence.PageIndex = e.NewPageIndex;
        //    this.GridView_PacienteUrgence.DataBind();
        //}

        //protected void OnServerValidate_PesquisarAtendimento(object sender, ServerValidateEventArgs e)
        //{
        //    e.IsValid = true;

        //    if (string.IsNullOrEmpty(this.TextBox_NumeroAtendimento.Text)
        //        && ((string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)) ||
        //        (!string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)) ||
        //        (string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && !string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)))
        //        )
        //        e.IsValid = false;
        //}

        public CustomValidator WUC_CustomValidator_PesquisarAtendimento
        {
            get
            {
                return this.CustomValidator_PesquisarAtendimento;
            }
        }

        public Panel WUC_Panel_ResultadoPesquisa
        {
            get
            {
                return this.Panel_ResultadoPesquisa;
            }
        }

        public UpdatePanel WUC_UpdatePanel_ResultadoPesquisa
        {
            get
            {
                return this.UpdatePanel_BuscaRegistro;
            }
        }

        public LinkButton WUC_LinkButton_Pesquisar
        {
            get { return this.LinkButton_Pesquisar; }
        }

        public LinkButton WUC_LinkButton_ListarTodos
        {
            get { return this.LinkButton_ListarTodos; }
        }

        public TextBox WUC_TextBox_NumeroAtendimento
        {
            get { return this.TextBox_NumeroAtendimento; }
        }

        public TextBox WUC_TextBox_DataInicialAtendimento
        {
            get { return this.TextBox_DataInicialAtendimento; }
        }

        public TextBox WUC_TextBox_DataFinalAtendimento
        {
            get { return this.TextBox_DataFinalAtendimento; }
        }

        public GridView WUC_GridViewAtendimentos
        {
            get
            {
                return this.GridView_PacienteUrgence;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void OnPageIndexChanging_PacienteUrgence(object sender, GridViewPageEventArgs e)
        {
            this.GridView_PacienteUrgence.DataSource = (IList<Prontuario>)Session["atendimentosUrgencePesquisados"];
            this.GridView_PacienteUrgence.DataBind();
            this.GridView_PacienteUrgence.PageIndex = e.NewPageIndex;
            this.GridView_PacienteUrgence.DataBind();
        }

        protected void OnServerValidate_PesquisarAtendimento(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = true;

            if (string.IsNullOrEmpty(this.TextBox_NumeroAtendimento.Text)
                && ((string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)) ||
                (!string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)) ||
                (string.IsNullOrEmpty(this.TextBox_DataInicialAtendimento.Text) && !string.IsNullOrEmpty(this.TextBox_DataFinalAtendimento.Text)))
                )
                e.IsValid = false;
        }
    }
}