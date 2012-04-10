using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Vacina
{
    public partial class WUC_PesquisarSalaVacina : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MasterMain master = (MasterMain)this.Page.Master.Master;
            ScriptManager script = (ScriptManager)master.FindControl("ScriptManager1");
            script.RegisterAsyncPostBackControl(this.LinkButton_PesquisarNome);
            script.RegisterAsyncPostBackControl(this.WUC_LinkButtonListarTodos);
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string validationnome = "ValidationGroup_" + this.UniqueID + "_PesquisaNome";
            this.LinkButton_PesquisarNome.ValidationGroup = validationnome;
            this.RequiredFieldValidator_Nome.ValidationGroup = validationnome;
            this.RegularExpressionValidator_Nome.ValidationGroup = validationnome;
            this.ValidationSummary_Nome.ValidationGroup = validationnome;
        }

        protected void OnClick_PesquisarNome(object sender, EventArgs e)
        {
            ISalaVacina iSala = Factory.GetInstance<ISalaVacina>();
            this.WUC_SalasPesquisadas = iSala.BuscarPorNome<SalaVacina>(this.TextBox_Nome.Text);
        }

        protected void OnClick_ListarTodos(object sender, EventArgs e)
        {
            this.WUC_SalasPesquisadas = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<SalaVacina>().OrderBy(p => p.EstabelecimentoSaude.NomeFantasia).ToList();
        }

        public LinkButton WUC_LinkButtonPesquisarNome
        {
            get
            {
                return this.LinkButton_PesquisarNome;
            }
        }

        public LinkButton WUC_LinkButtonListarTodos
        {
            get
            {
                return this.LinkButton_ListarTodos;
            }
        }

        public IList<SalaVacina> WUC_SalasPesquisadas
        {
            get
            {
                if (Session["SalasPesquisadas_" + this.UniqueID] != null)
                    return (IList<SalaVacina>)Session["SalasPesquisadas_" + this.UniqueID];

                return new List<SalaVacina>();
            }

            set
            {
                Session["SalasPesquisadas_" + this.UniqueID] = value;
            }
        }
    }
}