using System;
using ViverMais.DAO;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.IO;
using System.Collections;


namespace ViverMais.View.Urgencia
{
    public partial class ExibeFilaAcompanhamento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ((MasterUrgencia)Master).AjaxLoading = false;

                Session.Remove("FilaAcolhimento");

                ArrayList tiposAcolhimento = new ArrayList();
                tiposAcolhimento.Add(new { Codigo = AcolhimentoUrgence.ADULTO.ToString(), Nome = "Adulto".ToUpper() });
                tiposAcolhimento.Add(new { Codigo = AcolhimentoUrgence.INFANTIL.ToString(), Nome = "Infantil".ToUpper() });

                this.DataList_TipoAcolhimento.DataSource = tiposAcolhimento;
                this.DataList_TipoAcolhimento.DataBind();

                this.AcolhimentoSelecionado = AcolhimentoUrgence.ADULTO;
                this.CarregaAcolhimentos(true, false, this.AcolhimentoSelecionado);
                this.AlterarLayoutOpcoesEspecialidades(this.AcolhimentoSelecionado);
            }
        }

        protected void OnInit_gridFila(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session.Remove("acessoMedico");
                Session.Remove("acessoEnfermeiro");
            }

            bool acessoMedico;
            bool acessoEnfermeiro;

            if (Session["acessoMedico"] == null && Session["acessoEnfermeiro"] == null)
            {
                IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                Usuario usuario = (Usuario)Session["Usuario"];

                acessoMedico = iUsuarioProfissional.VerificarAcessoMedico(usuario.Codigo, "EXECUTAR_ACOLHIMENTO", usuario.Unidade.CNES);
                acessoEnfermeiro = iUsuarioProfissional.VerificarAcessoEnfermeiro(usuario.Codigo, "EXECUTAR_ACOLHIMENTO", usuario.Unidade.CNES);

                Session["acessoEnfermeiro"] = acessoEnfermeiro;
                Session["acessoMedico"] = acessoMedico;
            }
            else
            {

                acessoMedico = (bool)Session["acessoMedico"];
                acessoEnfermeiro = (bool)Session["acessoEnfermeiro"];
            }

            if (!acessoMedico && !acessoEnfermeiro)
            {
                gridFila.Columns.RemoveAt(1);

                BoundField numeroprontuario = new BoundField();
                numeroprontuario.DataField = "NumeroToString";
                numeroprontuario.HeaderText = "Identificador";
                numeroprontuario.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                numeroprontuario.ItemStyle.Width = Unit.Pixel(100);
                gridFila.Columns.Insert(1, numeroprontuario);
            }
        }

        /// <summary>
        /// Carrega os prontuários que estão na fila
        /// </summary>
        private void CarregaAcolhimentos(bool carregamentoinicial, bool ler_arquivo, char tipoacolhimento)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            bool existe_nova_entrada = false;

            if (ler_arquivo)
            {
                var stream = new FileStream(Server.MapPath("~/Urgencia/Documentos/FilaAcolhimento/" + usuario.Unidade.CNES + ".txt"), FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                TextReader leitura = new StreamReader(stream);
                TextWriter escrita = new StreamWriter(stream);

                if (Convert.ToChar(leitura.ReadLine()) == 'S')
                {
                    existe_nova_entrada = true;
                    stream.Seek(0, SeekOrigin.Begin);
                    escrita.WriteLine('N');
                    escrita.Flush();
                }

                stream.Close();
            }

            if (carregamentoinicial || existe_nova_entrada)
            {
                IProntuario iProntuario = Factory.GetInstance<IProntuario>();
                IList<ViverMais.Model.Prontuario> fila = iProntuario.BuscarFilaAcompanhamento<ViverMais.Model.Prontuario>(((ViverMais.Model.Usuario)Session["Usuario"]).Unidade.CNES,
                    tipoacolhimento);
                //DataTable tabela = iProntuario.getDataTablePronturario<IList<ViverMais.Model.Prontuario>>(fila);

                Session["FilaAcolhimento"] = fila;
            }

            gridFila.DataSource = (IList<ViverMais.Model.Prontuario>)Session["FilaAcolhimento"];
            gridFila.DataBind();
        }

        protected void OnTick_Temporizador(object sender, EventArgs e)
        {
            this.CarregaAcolhimentos(false, true, this.AcolhimentoSelecionado);
        }

        private char AcolhimentoSelecionado
        {
            get
            {
                return (ViewState["acolhimento_selecionado"] != null && ViewState["acolhimento_selecionado"] is char)
                    ? (char)ViewState["acolhimento_selecionado"] : 'X';
            }
            set
            {
                ViewState["acolhimento_selecionado"] = value;
            }
        }

        protected void OnClick_Acolhimento(object sender, EventArgs e)
        {
            char tipoacolhimento = Convert.ToChar(((LinkButton)sender).CommandArgument.ToString());
            this.AcolhimentoSelecionado = tipoacolhimento;

            this.CarregaAcolhimentos(true, false, tipoacolhimento);
            this.AlterarLayoutOpcoesEspecialidades(tipoacolhimento);
        }

        private void AlterarLayoutOpcoesEspecialidades(char acolhimentoselecionado)
        {
            foreach (DataListItem item in this.DataList_TipoAcolhimento.Items)
            {
                LinkButton lbbutton = (LinkButton)item.FindControl("LinkButton_EscolherFilaAcolhimento");

                if (lbbutton != null)
                {
                    if (Convert.ToChar(lbbutton.CommandArgument.ToString()) == acolhimentoselecionado)
                        lbbutton.CssClass = "seleciona-especialidadeon";
                    else
                        lbbutton.CssClass = "seleciona-especialidadeoff";
                }
            }

            this.UpdatePanel_Acolhimento.Update();
        }
    }
}
