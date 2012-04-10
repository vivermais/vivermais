using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using System.IO;

namespace ViverMais.View.Urgencia
{
    public partial class FormGerarBPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "GERAR_BPA", Modulo.URGENCIA))
                {
                    for (int i = 1; i < 13; i++)
                        DropDownList_Mes.Items.Add(new ListItem(i.ToString("00"), i.ToString("00")));

                    RadioButtonList_Modo.Items.Add(new ListItem("PRÉVIA", FaturaBPAUrgencia.PREVIA.ToString()));
                    RadioButtonList_Modo.Items.Add(new ListItem("FATURA", FaturaBPAUrgencia.FATURA.ToString()));
                    //foreach (string modofatura in Enum.GetNames(typeof(FaturaBPAUrgencia.TIPOFATURA)).ToList())
                    //    RadioButtonList_Modo.Items.Add(new ListItem(modofatura, ((int)Enum.Parse(typeof(FaturaBPAUrgencia.TIPOFATURA), modofatura)).ToString()));

                    //foreach (string tipobpa in Enum.GetNames(typeof(BPA.TIPOBPA)).ToList())
                    //    DropDownList_Tipo.Items.Add(new ListItem(tipobpa, ((int)Enum.Parse(typeof(BPA.TIPOBPA), tipobpa)).ToString()));

                    //DropDownList_Tipo.Items.Add(new ListItem("APAC",BPA.APAC.ToString()));
                    DropDownList_Tipo.Items.Add(new ListItem("CONSOLIDADO", BPA.CONSOLIDADO.ToString()));
                    DropDownList_Tipo.Items.Add(new ListItem("INDIVIDUALIZADO", BPA.INDIVIDUALIZADO.ToString()));

                    this.InserirElementoDefault(this.DropDownList_Mes);
                    this.InserirElementoDefault(this.DropDownList_Tipo);

                    RadioButtonList_Modo.SelectedValue = FaturaBPAUrgencia.PREVIA.ToString();
                }
                else
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        private void InserirElementoDefault(DropDownList drop)
        {
            drop.Items.Insert(0, new ListItem("Selecione...", "-1"));
        }

        protected void OnSelectedIndexChanged_ConfirmarFatura(object sender, EventArgs e)
        {
            if (RadioButtonList_Modo.SelectedValue == FaturaBPAUrgencia.FATURA.ToString())
                LinkButtonSalvar.OnClientClick = "javascript:if (Page_ClientValidate('ValidationGroup_GerarBPA')) return confirm('Tem certeza que deseja fechar a fatura desta competência ?'); return false;";
            else
                LinkButtonSalvar.OnClientClick = LinkButtonSalvar.OnClientClick.Remove(0);
        }

        protected void OnClick_GerarBPA(object sender, EventArgs e)
        {
            this.Panel_Fatura.Visible = false;
            FaturaBPAUrgencia fatura = null;
            IFaturaBPAUrgencia iFatura = Factory.GetInstance<IFaturaBPAUrgencia>();
            int competencia = int.Parse((TextBox_Ano.Text + DropDownList_Mes.SelectedValue).ToString());
            int competenciaanterior = this.retornaCompetenciaAnterior(competencia);
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();

            int ultimodiacompetencia = System.DateTime.DaysInMonth(int.Parse(TextBox_Ano.Text),int.Parse(DropDownList_Mes.SelectedValue));

            Usuario usuario = (Usuario)Session["Usuario"];

            fatura = iFatura.BuscarPorCompetencia<FaturaBPAUrgencia>(competencia, usuario.Unidade.CNES, char.Parse(DropDownList_Tipo.SelectedValue));
            FaturaBPAUrgencia ultimafatura = iFatura.BuscarUltimaFatura<FaturaBPAUrgencia>(usuario.Unidade.CNES, char.Parse(DropDownList_Tipo.SelectedValue));

            DateTime datafinal = new DateTime(int.Parse(TextBox_Ano.Text), int.Parse(DropDownList_Mes.SelectedValue), ultimodiacompetencia, 23, 59, 59);
            DateTime datainicio = new DateTime(int.Parse(TextBox_Ano.Text), int.Parse(DropDownList_Mes.SelectedValue), 1, 0, 0, 0);

            if (fatura != null)
                datafinal = fatura.Data;
            
            if (RadioButtonList_Modo.SelectedValue == FaturaBPAUrgencia.FATURA.ToString()) //Fatura
            {
                if (fatura != null)
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('A fatura para a competência solicitada já foi fechada. Visualize suas informações abaixo.');", true);
                    ViewState["datainicio"] = datainicio;
                    ViewState["datafinal"] = datafinal;
                    this.MostrarInformacoesFatura(fatura);
                }
                else
                {
                    if (ultimafatura != null && competencia < ultimafatura.Competencia)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, informe uma competência maior do que a da última fatura: " + ultimafatura.Competencia + ".');", true);
                        return;
                    }
                    else
                    {
                        if (ultimafatura != null && iFatura.BuscarPorCompetencia<FaturaBPAUrgencia>(competenciaanterior, usuario.Unidade.CNES, char.Parse(DropDownList_Tipo.SelectedValue)) == null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Para fechar a fatura da competência solicitada deve-se fechar a competência: " + competenciaanterior + ".');", true);
                            return;
                        }
                        else
                        {
                            if (new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
                                .CompareTo(new DateTime(datafinal.Year, datafinal.Month, datafinal.Day)) < 0)
                            {
                                this.Panel_ConfirmarFechamentoBPA.Visible = true;
                                this.Label_InfoConfirmarFatura.Text = "Usuário, a data de fechamento para esta fatura deve ser feita a partir do dia " + datafinal.Day + " da competência solicitada.";
                                this.Label_InfoConfirmarFatura.Text += " Caso haja a confirmação para a data de hoje (" + DateTime.Now.ToString("dd/MM/yyyy")+ "), os procedimentos posteriores a mesma não serão contabilizados no BPA.";
                                this.Label_InfoConfirmarFatura.Text += " Deseja realmente continuar?";

                                ViewState["datainicio"] = datainicio;
                                ViewState["datafinal"] = DateTime.Now;
                            }
                            else
                            {
                                ViewState["datainicio"] = datainicio;
                                ViewState["datafinal"] = datafinal;
                                this.SalvarFatura();
                            }
                        }
                    }
                }
            }
            else //Prévia
            {
                if (char.Parse(this.DropDownList_Tipo.SelectedValue) == BPA.INDIVIDUALIZADO)
                    Session["ArquivoBPA"] = iRelatorio.GerarBPAI<ArquivoBPA>(usuario.Unidade.CNES, competencia, datainicio, datafinal);
                else if (char.Parse(this.DropDownList_Tipo.SelectedValue) == BPA.CONSOLIDADO)
                    Session["ArquivoBPA"] = iRelatorio.GerarBPAC<ArquivoBPA>(usuario.Unidade.CNES, competencia, datainicio, datafinal);
                
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "download", "window.open('../EnvioBPA/FormDownloadArquivoBPA.aspx?tipo=previa','Prévia','toolbar=no,width=500,height=400');", true);
            }
        }

        protected void OnClick_CancelarFatura(object sender, EventArgs e)
        {
            this.Panel_ConfirmarFechamentoBPA.Visible = false;
        }

        protected void OnClick_ConfirmarFatura(object sender, EventArgs e)
        {
            this.Panel_ConfirmarFechamentoBPA.Visible = false;
            this.SalvarFatura();
        }

        private void SalvarFatura()
        {
            IFaturaBPAUrgencia iFatura = Factory.GetInstance<IFaturaBPAUrgencia>();
            int competencia = int.Parse((TextBox_Ano.Text + DropDownList_Mes.SelectedValue).ToString());
            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
            Usuario usuario = (Usuario)Session["Usuario"];

            FaturaBPAUrgencia novafatura = new FaturaBPAUrgencia();
            novafatura.Competencia = competencia;
            novafatura.Data = (DateTime)ViewState["datafinal"];
            novafatura.Tipo = char.Parse(DropDownList_Tipo.SelectedValue);
            novafatura.Unidade = iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(usuario.Unidade.CNES);
            novafatura.Usuario = iEstabelecimento.BuscarPorCodigo<Usuario>(usuario.Codigo);

            iFatura.Salvar(novafatura);
            iFatura.Inserir(new LogUrgencia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 46, "ID FATURA:" + novafatura.Codigo));

            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Fatura fechada com sucesso. Visualize suas informações abaixo');", true);
            this.MostrarInformacoesFatura(novafatura);
        }

        private void MostrarInformacoesFatura(FaturaBPAUrgencia fatura)
        {
            Label_Competencia.Text = fatura.Competencia.ToString();
            Label_DataFechamento.Text = fatura.Data.ToString("dd/MM/yyyy HH:mm:ss");

            if (fatura.Tipo == BPA.CONSOLIDADO)
                Label_Tipo.Text = "Consolidado";
            else if (fatura.Tipo == BPA.INDIVIDUALIZADO)
                Label_Tipo.Text = "Individualizado";

            Label_Unidade.Text = fatura.Unidade.NomeFantasia;
            Label_UsuarioResponsavel.Text = fatura.Usuario.Nome;

            this.Panel_Fatura.Visible = true;
            Session["faturaUrgencia"] = fatura;
        }

        protected void OnClick_DownloadFatura(object sender, EventArgs e)
        {
            if (Session["faturaUrgencia"] != null && Session["faturaUrgencia"] is FaturaBPAUrgencia)
            {
                IRelatorioUrgencia iRelatorio = Factory.GetInstance<IRelatorioUrgencia>();
                FaturaBPAUrgencia fatura = (FaturaBPAUrgencia)Session["faturaUrgencia"];

                if (fatura.Tipo == BPA.INDIVIDUALIZADO)
                    Session["ArquivoBPA"] = iRelatorio.GerarBPAI<ArquivoBPA>(fatura.Unidade.CNES, fatura.Competencia, (DateTime)ViewState["datainicio"], (DateTime)ViewState["datafinal"]);
                else if (fatura.Tipo == BPA.CONSOLIDADO)
                    Session["ArquivoBPA"] = iRelatorio.GerarBPAC<ArquivoBPA>(fatura.Unidade.CNES, fatura.Competencia, (DateTime)ViewState["datainicio"], (DateTime)ViewState["datafinal"]);

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "download", "window.open('../EnvioBPA/FormDownloadArquivoBPA.aspx?tipo=fatura','Fatura','toolbar=no,width=500,height=400');", true);
            }
        }

        private int retornaCompetenciaAnterior(int competencia)
        {
            int tamanho = competencia.ToString().Length;
            int mes = int.Parse(competencia.ToString()[(tamanho - 2)].ToString() + competencia.ToString()[(tamanho - 1)].ToString());
            int ano = int.Parse(competencia.ToString().Substring(0, tamanho - 2));

            mes -= 1;

            if (mes == 0)
            {
                mes = 12;
                ano -= 1;
            }

            return int.Parse((ano.ToString() + (mes < 10 ? ("0" + mes.ToString()) : mes.ToString()).ToString()));
        }
    }
}
