using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_Exames : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregaListaExames();
            }
        }

        /// <summary>
        /// Carrega a lista de exames para possíveis solicitações
        /// </summary>
        private void CarregaListaExames()
        {
            ddlExames.Items.Clear();
            ddlExames.Items.Add(new ListItem("Selecione...", "0"));
            IList<ViverMais.Model.Exame> exames = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.Exame>().Where(p => p.Status == 'A').OrderBy(s => s.Descricao).ToList();
            foreach (ViverMais.Model.Exame ex in exames)
                ddlExames.Items.Add(new ListItem(ex.Descricao, ex.Codigo.ToString()));
        }

        /// <summary>
        /// Adiciona o exame na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdicionarExames_Click(object sender, EventArgs e)
        {
            IList<ProntuarioExame> lista = RetornaListaExames();

            if (lista.Where(p => p.Exame.Codigo.ToString() == ddlExames.SelectedValue).FirstOrDefault() == null)
            {
                ProntuarioExame pe = new ProntuarioExame();
                pe.Exame = Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<Exame>(int.Parse(ddlExames.SelectedValue));
                pe.Data = DateTime.Now;
                pe.Profissional = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(((Usuario)Session["Usuario"]).Codigo).Id_Profissional;
                lista.Add(pe);

                Session["ListaExame"] = lista;
                CarregaGridExames(lista);

                ddlExames.SelectedValue = "0";
            }
        }

        /// <summary>
        /// Retorna a Lista de Exames Solicitados
        /// </summary>
        /// <returns></returns>
        public IList<ProntuarioExame> RetornaListaExames()
        {
            return Session["ListaExame"] != null ? (IList<ProntuarioExame>)Session["ListaExame"] : new List<ProntuarioExame>();
        }

        /// <summary>
        /// Carrega o grid de exames solicitados
        /// </summary>
        /// <param name="iList"></param>
        private void CarregaGridExames(IList<ProntuarioExame> iList)
        {
            gridExames.DataSource = iList;
            gridExames.DataBind();
        }

        /// <summary>
        /// Deleta o exame adicionado na lista temporária
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridExames_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            IList<ProntuarioExame> lista = RetornaListaExames();
            lista.RemoveAt(e.RowIndex);
            Session["ListaExame"] = lista;

            CarregaGridExames(lista);
        }
    }
}