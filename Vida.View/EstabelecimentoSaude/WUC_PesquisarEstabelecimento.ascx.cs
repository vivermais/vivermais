using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.DAO;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class WUC_PesquisarEstabelecimento : System.Web.UI.UserControl
    {
        //public TextBox WUC_PesquisaCNES
        //{
        //    get
        //    {
        //        return this.TextBox_CNES;
        //    }
        //}

        //public TextBox WUC_PesquisaNomeFantasia
        //{
        //    get
        //    {
        //        return this.TextBox_NomeFanatasia;
        //    }
        //}

        public void LimpaCamposPesquisa()
        {
            this.TextBox_CNES.Text = "";
            this.TextBox_NomeFanatasia.Text = "";
        }

        public LinkButton WUC_LinkButton_PesquisarNomeFantasia 
        {
            get
            {
                return this.LinkButton_PesquisarNomeFantasia;
            }
        }

        public LinkButton WUC_LinkButton_PesquisarCNES
        {
            get
            {
                return this.LinkButton_PesquisarCNES;
            }
        }

        public IList<ViverMais.Model.EstabelecimentoSaude> WUC_EstabelecimentosPesquisados
        {
            get
            {
                if (Session["EstabelecimentosPesquisados_" + this.UniqueID] != null)
                    return (IList<ViverMais.Model.EstabelecimentoSaude>)Session["EstabelecimentosPesquisados_" + this.UniqueID];

                return new List<ViverMais.Model.EstabelecimentoSaude>();
            }

            set
            {
                Session["EstabelecimentosPesquisados_" + this.UniqueID] = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string validationcnes = "ValidationGroup_" + this.UniqueID + "_PesquisaCNES";
            this.LinkButton_PesquisarCNES.ValidationGroup = validationcnes;
            this.RequiredFieldValidator_CNES.ValidationGroup = validationcnes;
            this.RegularExpressionValidator_CNES.ValidationGroup = validationcnes;
            this.ValidationSummary_CNES.ValidationGroup = validationcnes;

            string validationnomefantasia = "ValidationGroup_" + this.UniqueID + "_PesquisaNomeFantasia";
            this.RequiredFieldValidator_NomeFantasia.ValidationGroup = validationnomefantasia;
            this.RegularExpressionValidator_NomeFantasia.ValidationGroup = validationnomefantasia;
            this.ValidationSummary_NomeFantasia.ValidationGroup = validationnomefantasia;
            this.LinkButton_PesquisarNomeFantasia.ValidationGroup = validationnomefantasia;
        }

        protected void OnClick_PesquisarCNES(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEAS = Factory.GetInstance<IEstabelecimentoSaude>();
            ViverMais.Model.EstabelecimentoSaude unidade = iEAS.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(this.TextBox_CNES.Text);
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = new List<ViverMais.Model.EstabelecimentoSaude>();

            if (unidade != null && unidade.CNES != "0000000")
                unidades.Add(unidade);
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi encontrado nenhuma unidade com o CNES informado.');", true);

            this.WUC_EstabelecimentosPesquisados = unidades;
        }

        protected void OnClick_PesquisarNomeFantasia(object sender, EventArgs e)
        {
            IEstabelecimentoSaude iEAS = Factory.GetInstance<IEstabelecimentoSaude>();
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = iEAS.BuscarEstabelecimentoPorNomeFantasia<ViverMais.Model.EstabelecimentoSaude>(this.TextBox_NomeFanatasia.Text);
            this.WUC_EstabelecimentosPesquisados = unidades;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            MasterMain master = (MasterMain)this.Page.Master.Master;
            ScriptManager script = (ScriptManager)master.FindControl("ScriptManager1");
            script.RegisterAsyncPostBackControl(this.LinkButton_PesquisarCNES);
            script.RegisterAsyncPostBackControl(this.LinkButton_PesquisarNomeFantasia);
        }
    }
}