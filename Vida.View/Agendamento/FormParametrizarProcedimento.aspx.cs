using System;
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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;
using ViverMais.Model;
using System.Drawing;
using System.Reflection;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametrizarProcedimento : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_PROCEDIMENTO", Modulo.AGENDAMENTO))
                {
                    CriaTabelaPreparoSelecionados();
                    PanelForma.Visible = false;
                    PanelCodigo.Visible = false;
                    PanelProcedimento.Visible = false;
                    PanelPreparosSelecionados.Visible = false;
                    PanelPreparos.Visible = false;
                    IViverMaisServiceFacade iProcedimento = Factory.GetInstance<IViverMaisServiceFacade>();
                    IList<ViverMais.Model.GrupoProcedimento> grupos = iProcedimento.ListarTodos<ViverMais.Model.GrupoProcedimento>();
                    ddlGrupo.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (ViverMais.Model.GrupoProcedimento grupo in grupos)
                    {
                        ddlGrupo.Items.Add(new ListItem(grupo.Nome, grupo.Codigo.ToString()));
                    }

                    if (Request.QueryString["id_procedimento"] != null)
                    {
                        string id_procedimento = Request.QueryString["id_procedimento"];
                        string id_grupo = id_procedimento.ToString().Substring(0, 2);
                        string id_subgrupo = id_procedimento.ToString().Substring(0, 4);
                        string id_forma = id_procedimento.ToString().Substring(0, 6);
                        ddlGrupo.SelectedValue = id_grupo;
                        ISubGrupo iSubGrupo = Factory.GetInstance<ISubGrupo>();
                        IList<ViverMais.Model.SubGrupoProcedimento> subgrupos = iSubGrupo.BuscarPorGrupo<ViverMais.Model.SubGrupoProcedimento>(ddlGrupo.SelectedValue);
                        ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
                        foreach (ViverMais.Model.SubGrupoProcedimento subgrupo in subgrupos)
                        {
                            ddlSubGrupo.Items.Add(new ListItem(subgrupo.Nome, subgrupo.Codigo.ToString()));
                        }

                        ddlSubGrupo.SelectedValue = id_subgrupo;
                        IForma iForma = Factory.GetInstance<IForma>();
                        IList<ViverMais.Model.FormaOrganizacaoProcedimento> formas = iForma.BuscarPorGrupoSubGrupo<ViverMais.Model.FormaOrganizacaoProcedimento>(ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue);
                        ddlForma.Items.Add(new ListItem("Selecione...", "0"));
                        foreach (ViverMais.Model.FormaOrganizacaoProcedimento forma in formas)
                        {
                            ddlForma.Items.Add(new ListItem(forma.Nome, forma.Codigo.ToString()));
                        }
                        ddlForma.SelectedValue = id_forma;
                        tbxCodigo.Text = id_procedimento.ToString();
                        ViverMais.Model.Procedimento procedimentos = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(tbxCodigo.Text);
                        tbxProcedimento.Text = procedimentos.Nome;
                        lblRotuloProcedimento.Visible = true;
                        lblNomeProcedimento.Text = procedimentos.Nome;
                        lblRotuloProced.Visible = true;
                        lblNomeProcedimento.Visible = true;
                        tbxCodigo.Text = procedimentos.Codigo;
                        tbxProcedimento.Text = procedimentos.Nome;
                        ITipoProcedimento itipoProcedimento = Factory.GetInstance<ITipoProcedimento>();
                        TipoProcedimento tipoProcedimento = itipoProcedimento.BuscaTipoProcedimento<TipoProcedimento>(procedimentos.Codigo);
                        if (tipoProcedimento != null)
                        {
                            rbtnTipoProcedimento.SelectedValue = tipoProcedimento.Tipo.ToString();
                        }
                        rbtnTipo.SelectedValue = "3";
                        //Busco o preparo do procedimento informado                    
                        IPreparo ipreparo = Factory.GetInstance<IPreparo>();
                        IList<Preparo> preparos = ipreparo.BuscarPreparoPorProcedimento<Preparo>(id_procedimento);
                        if (preparos.Count == 0)
                        {
                            lblSemRegistros.Text = "O procedimento informado não possui Preparos";
                            lblSemRegistros.Visible = true;
                            PanelPreparosSelecionados.Visible = false;
                        }
                        else
                        {
                            DataTable table = (DataTable)Session["PreparosSelecionados"];
                            for (int i = 0; i < preparos.Count; i++)
                            {
                                DataRow row = table.NewRow();
                                row[0] = preparos[i].Codigo.ToString();
                                row[1] = preparos[i].Descricao;
                                table.Rows.Add(row);
                            }
                            PanelPreparosSelecionados.Visible = true;
                            lblSemRegistros.Visible = false;
                            GridViewPreparosSelecionados.DataSource = table;
                            GridViewPreparosSelecionados.DataBind();
                            Session["PreparosSelecionados"] = table;
                        }
                        //Listo todos os preparos para o usuário poder Incluir 
                        preparos = ipreparo.ListarTodos<Preparo>();
                        GridViewPreparo.DataSource = preparos;
                        GridViewPreparo.DataBind();
                        PanelPreparos.Visible = true;
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void GridViewPreparo_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //IList<Preparo> preparos = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Preparo>();
            //(IList<Preparo>)Session["ListaPreparos"];
            GridViewPreparo.DataSource = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Preparo>();
            GridViewPreparo.PageIndex = e.NewPageIndex;
            GridViewPreparo.DataBind();
        }

        protected void GridViewPreparosSelecionados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable tabela = (DataTable)Session["PreparosSelecionados"];
            //IList<Preparo> preparos = (IList<Preparo>)Session["PreparosSelecionados"];
            GridViewPreparosSelecionados.DataSource = tabela;
            GridViewPreparosSelecionados.PageIndex = e.NewPageIndex;
            GridViewPreparosSelecionados.DataBind();
        }

        //private DataTable ConverteListParaDataTable(IList<Preparo> list)
        //{
        //    DataTable dt = (DataTable)Session["PreparosSelecionados"];
        //    foreach (Preparo p in list)
        //    {
        //        DataRow row = dt.NewRow();
        //        row[0] = p.Codigo.ToString();
        //        row[1] = p.Descricao;
        //        dt.Rows.Add(row);
        //    }
        //    return dt;
        //}

        private DataTable CriaTabelaPreparoSelecionados()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.PrimaryKey = new DataColumn[] { table.Columns["Codigo"] };

            table.Columns.Add("Descricao");
            Session["PreparosSelecionados"] = table;
            return table;
        }

        private void IncluiPreparoNoDataTablePreparoSelecionado(int id_preparo, DataTable table)
        {
            //Verifica pela chave primária (Codigo) se o Item Selecionado 
            //já existe no DataTable de Preparos Selecionados            
            if (table.Rows.Find(id_preparo) == null)
            {
                IPreparo ipreparo = Factory.GetInstance<IPreparo>();
                Preparo preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                DataRow row = table.NewRow();
                row[0] = preparo.Codigo.ToString();
                row[1] = preparo.Descricao;
                table.Rows.Add(row);
            }
        }

        protected void GridViewPreparo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int id_preparo = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                IncluiPreparoNoDataTablePreparoSelecionado(id_preparo, (DataTable)Session["PreparosSelecionados"]);
                DataTable table = (DataTable)Session["PreparosSelecionados"];
                GridViewPreparosSelecionados.DataSource = table;
                GridViewPreparosSelecionados.DataBind();
                PanelPreparosSelecionados.Visible = true;
                //Pega a Linha selecionada para deixar marcada                
                //GridViewPreparo.Rows[RowIndex].BackColor = Color.LightBlue;                
            }
        }

        protected void GridViewPreparoSelecionados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Excluir")
            {
                DataTable tabela = (DataTable)Session["PreparosSelecionados"];
                int id_preparo = Convert.ToInt32(e.CommandArgument);
                tabela.Rows.Find(id_preparo).Delete();
                Session["PreparosSelecionados"] = tabela;
                GridViewPreparosSelecionados.DataSource = tabela;
                GridViewPreparosSelecionados.DataBind();
            }

        }

        protected void ddlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSubGrupo.Items.Clear();
            ISubGrupo iSubGrupo = Factory.GetInstance<ISubGrupo>();
            IList<ViverMais.Model.SubGrupoProcedimento> subgrupos = iSubGrupo.BuscarPorGrupo<ViverMais.Model.SubGrupoProcedimento>(ddlGrupo.SelectedValue);
            ddlSubGrupo.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.SubGrupoProcedimento subgrupo in subgrupos)
            {
                ddlSubGrupo.Items.Add(new ListItem(subgrupo.Nome, subgrupo.Codigo.ToString()));
            }
        }

        protected void ddlSubGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlForma.Items.Clear();
            IForma iForma = Factory.GetInstance<IForma>();
            IList<ViverMais.Model.FormaOrganizacaoProcedimento> formas = iForma.BuscarPorGrupoSubGrupo<ViverMais.Model.FormaOrganizacaoProcedimento>(ddlGrupo.SelectedValue, ddlSubGrupo.SelectedValue);
            ddlForma.Items.Add(new ListItem("Selecione...", "0"));
            foreach (ViverMais.Model.FormaOrganizacaoProcedimento forma in formas)
            {
                ddlForma.Items.Add(new ListItem(forma.Nome, forma.Codigo.ToString()));
            }
        }

        protected void ddlForma_SelectedIndexChanged(object sender, EventArgs e)
        {
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IList<ViverMais.Model.Procedimento> procedimentos = iProcedimento.BuscarPorForma<ViverMais.Model.Procedimento>(ddlForma.SelectedValue);
            Session["procedimentos"] = procedimentos;
            if (procedimentos.Count != 0)
            {
                GridViewProcedimento.DataSource = procedimentos;
                GridViewProcedimento.DataBind();
                //Proced(procedimentos);

                //IPreparo ipreparo = ;
                IList<Preparo> preparos = Factory.GetInstance<IPreparo>().ListarTodos<Preparo>();
                GridViewPreparo.DataSource = preparos;
                GridViewPreparo.DataBind();
                Session["ListaPreparos"] = preparos;
                PanelPreparos.Visible = true;
                PanelPreparosSelecionados.Visible = false;
            }
        }

        protected void GridViewProcedimento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //DataTable table = (DataTable)Session["Table"];
            IList<Procedimento> procedimentos = (IList<Procedimento>)Session["procedimentos"];
            GridViewProcedimento.DataSource = procedimentos;
            GridViewProcedimento.PageIndex = e.NewPageIndex;
            GridViewProcedimento.DataBind();
        }

        protected void codigo_TextChanged(object sender, EventArgs e)
        {
            //IViverMaisServiceFacade iProcedimento = ;
            //ITipoProcedimento iTipoProcedimento = ;
            lblRotuloProcedimento.Visible = true;
            lblProcedimento.Visible = true;

            ViverMais.Model.Procedimento procedimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Procedimento>(tbxCodigo.Text);
            if (procedimento == null)
            {
                tbxCodigo.Text = "";
                lblProcedimento.Text = "Procedimento não Existe";
                return;
            }
            List<Procedimento> procedimentos = new List<Procedimento>();
            procedimentos.Add(procedimento);
            tbxCodigo.Text = procedimento.Codigo;
            lblProcedimento.Text = procedimento.Nome;
            Session["procedimentos"] = procedimentos;
            TipoProcedimento tipoProcedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(procedimento.Codigo);
            if (tipoProcedimento != null)
            {
                rbtnTipoProcedimento.SelectedValue = tipoProcedimento.Tipo.ToString();
                IPreparo ipreparo = Factory.GetInstance<IPreparo>();
                //Carrega todos os Preparos
                PanelPreparos.Visible = true;
                PanelPreparosSelecionados.Visible = true;
                GridViewPreparo.DataSource = ipreparo.ListarTodos<Preparo>();
                GridViewPreparo.DataBind();

                //Lista os Preparos Vinculados ao Procedimento(os)
                IList<Preparo> preparos = ipreparo.BuscarPreparoPorProcedimento<Preparo>(procedimento.Codigo.ToString());
                DataTable table = (DataTable)Session["PreparosSelecionados"];
                foreach (Preparo preparo in preparos)
                {
                    DataRow row = table.NewRow();
                    row["Codigo"] = preparo.Codigo.ToString();
                    row["Descricao"] = preparo.Descricao;
                    table.Rows.Add(row);
                }
                Session["PreparosSelecionados"] = table;
                PanelPreparos.Visible = true;
                GridViewPreparosSelecionados.DataSource = table;
                GridViewPreparosSelecionados.DataBind();
                ////Verifico se o procedimento possui preparo
                //if (preparos.Count == 0)
                //{

                //}
                //else
                //{
                //    //Session["PreparosSelecionados"] = ConverteListParaDataTable(preparo);
                //    //preparos = ipreparo.ListarTodos<Preparo>();
                //    //GridViewPreparo.DataSource = preparos;
                //    //GridViewPreparo.DataBind();

                //    PanelPreparosSelecionados.Visible = true;
                //}
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Procedimento não está parametrizado');", true);
            }
        }

        //void Proced(IList<ViverMais.Model.Procedimento> procedimentos)
        //{
        //    DataTable table = new DataTable();
        //    DataColumn col0 = new DataColumn("Codigo");
        //    table.Columns.Add(col0);
        //    DataColumn col1 = new DataColumn("Procedimento");
        //    table.Columns.Add(col1);
        //    foreach (ViverMais.Model.Procedimento procedimento in procedimentos)
        //    {
        //        DataRow row = table.NewRow();
        //        row[0] = procedimento.Codigo;
        //        row[1] = procedimento.Nome;
        //        table.Rows.Add(row);
        //    }
        //    Session["procedimentos"] = procedimentos;
        //    Session["Table"] = table;
        //    GridViewProcedimento.DataSource = table;
        //    GridViewProcedimento.DataBind();
        //}

        protected void procedimento_TextChanged(object sender, EventArgs e)
        {
            IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
            IList<ViverMais.Model.Procedimento> procedimentos = iProcedimento.BuscarPorNome<ViverMais.Model.Procedimento>(tbxProcedimento.Text.ToUpper());
            if (procedimentos.Count == 0)
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não existe procedimento com esse nome!'); window.location='FormParametrizarProcedimento.aspx'</script>");
                return;
            }
            GridViewProcedimento.DataSource = procedimentos;
            GridViewProcedimento.DataBind();
            Session["procedimentos"] = procedimentos;
            //Proced(procedimentos);
        }

        protected void Incluir_Click(object sender, EventArgs e)
        {
            IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
            ViverMais.Model.Procedimento procedimento;
            ViverMais.Model.TipoProcedimento tipoprocedimento;
            IPreparo ipreparo = Factory.GetInstance<IPreparo>();
            Preparo preparo;
            bool existe = false;
            //IList<Preparo> preparosSelecionados = (IList<Preparo>)Session["PreparosSelecionados"];
            DataTable table = (DataTable)Session["PreparosSelecionados"];
            //Caso seja uma edição para um único Procedimento
            if (Request.QueryString["id_procedimento"] != null)
            {
                string id_procedimento = Request.QueryString["id_procedimento"].ToString();
                procedimento = iAgendamento.BuscarPorCodigo<ViverMais.Model.Procedimento>(id_procedimento);
                tipoprocedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<ViverMais.Model.TipoProcedimento>(id_procedimento);
                
                if (tipoprocedimento != null)//Caso o Procedimento já Possua Atributos Salvos, ele irá atualizar
                {
                    tipoprocedimento.Tipo = char.Parse(rbtnTipoProcedimento.SelectedValue);
                    

                    //Removo Todos os Preparos Para Sempre Inserir Os Selecionados pelo usuário
                    Factory.GetInstance<ITipoProcedimento>().RemoverTipoProcedimentoPorProcedimento(id_procedimento);

                    //Verifico se a qtd de Linhas é maior do que 0, para saber se existe alguma informação na table
                    //foreach (Preparo preparo in preparosSelecionados)
                    //{
                    //    if (!tipoprocedimento.Preparos.Contains(preparo))
                    //        tipoprocedimento.Preparos.Add(preparo);
                    //}

                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            preparo = new Preparo();
                            int id_preparo = int.Parse(row[0].ToString());
                            preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                            //Percorro a lista de Preparos do Procedimento Para Evitar Duplicidade
                            foreach (Preparo p in tipoprocedimento.Preparos)
                            {
                                if (p == preparo)
                                    existe = true;
                            }
                            if (existe == false)//Caso não Exista o Preparo ele insete
                                tipoprocedimento.Preparos.Add(preparo);
                        }
                    }
                    iAgendamento.Atualizar(tipoprocedimento);
                }
                else // Se não existir TipoProcedimento na Base com Esse Procedimento, ele irá Inserir um Novo
                {
                    tipoprocedimento = new TipoProcedimento();
                    tipoprocedimento.Procedimento = id_procedimento;
                    tipoprocedimento.Tipo = char.Parse(rbtnTipoProcedimento.SelectedValue);
                    //tipoprocedimento.Preparos = preparosSelecionados;
                    //DataTable table = (DataTable)Session["PreparosSelecionados"];
                    //Verifico se a qtd de Linhas é maior do que 0, para saber se existe alguma informação na table
                    if (table.Rows.Count > 0)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            preparo = new Preparo();
                            int id_preparo = int.Parse(row[0].ToString());
                            preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                            tipoprocedimento.Preparos.Add(preparo);
                        }
                    }
                    iAgendamento.Inserir(tipoprocedimento);
                }
            }
            else //Caso não Seja uma atualização de um Único Procedimento, Ele Irá Atualizar a Lista De Procedimentos Pesquisadas
            {
                IList<ViverMais.Model.Procedimento> procedimentos = (List<ViverMais.Model.Procedimento>)Session["procedimentos"];

                //Se a Lista não for vazia, os procedimentos são incluídos na tabela Procedimentos do Cygnus
                if (procedimentos.Count != 0)
                {
                    for (int i = 0; i < procedimentos.Count; i++)
                    {
                        tipoprocedimento = new ViverMais.Model.TipoProcedimento();
                        procedimento = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Procedimento>(procedimentos[i].Codigo);

                        //Removo Todos os Preparos Para Sempre Inserir Os Selecionados pelo usuário
                        Factory.GetInstance<ITipoProcedimento>().RemoverTipoProcedimentoPorProcedimento(procedimento.Codigo);

                        ViverMais.Model.TipoProcedimento proced = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<ViverMais.Model.TipoProcedimento>(procedimentos[i].Codigo);
                        
                        if (proced == null)//Se não encontrar Parametros para esse procedimento, ele irá inserir um novo
                        {
                            tipoprocedimento.Procedimento = procedimento.Codigo;
                            tipoprocedimento.Tipo = char.Parse(rbtnTipoProcedimento.SelectedValue);
                            //tipoprocedimento.Preparos = preparosSelecionados;
                            //DataTable table = (DataTable)Session["PreparosSelecionados"];
                            //Verifico se a qtd de Linhas é maior do que 0, para saber se existe alguma informação na table
                            if (table.Rows.Count > 0)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    preparo = new Preparo();
                                    int id_preparo = int.Parse(row[0].ToString());
                                    preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                                    tipoprocedimento.Preparos.Add(preparo);
                                }
                            }
                            iAgendamento.Inserir(tipoprocedimento);
                            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 14, "ID_PROCEDIMENTO:" + tipoprocedimento.Procedimento + " tipo:" + tipoprocedimento.Tipo));
                        }
                        else
                        {
                            proced.Procedimento = procedimento.Codigo;
                            proced.Tipo = char.Parse(rbtnTipoProcedimento.SelectedValue);
                            //proced.Preparos = preparosSelecionados;
                            //Salvo a Lista de Preparos Selecionados
                            //DataTable table = (DataTable)Session["PreparosSelecionados"];
                            if (table.Rows.Count > 0)
                            {
                                foreach (DataRow row in table.Rows)
                                {
                                    preparo = new Preparo();
                                    int id_preparo = int.Parse(row[0].ToString());
                                    preparo = ipreparo.BuscarPorCodigo<Preparo>(id_preparo);
                                    proced.Preparos.Add(preparo);
                                }
                            }
                            iAgendamento.Atualizar(proced);
                            Factory.GetInstance<IAgendamentoServiceFacade>().Inserir(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 15, "ID_PROCEDIMENTO:" + tipoprocedimento.Procedimento + " tipo:" + tipoprocedimento.Tipo.ToString()));
                        }
                        
                    }
                }
            }
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Parametrização Cadastrada com sucesso!'); location='FormParametrizarProcedimento.aspx';", true);
            //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Parametrização Cadastrada com sucesso!'); window.location='FormParametrizarProcedimento.aspx'</script>");
            Session["procedimentos"] = null;
            Session["PreparosSelecionados"] = null;
        }

        protected void rbtnTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRotuloProcedimento.Visible = false;
            lblProcedimento.Visible = false;
            lblRotuloProced.Visible = false;
            lblNomeProcedimento.Visible = false;
            switch (rbtnTipo.SelectedValue)
            {
                case "1":
                    GridViewProcedimento.DataSource = null;
                    GridViewProcedimento.DataBind();
                    tbxCodigo.Text = "";
                    lblProcedimento.Text = "";
                    tbxProcedimento.Text = "";
                    PanelForma.Visible = true;
                    PanelCodigo.Visible = false;
                    PanelProcedimento.Visible = false;
                    PanelPreparos.Visible = false;
                    PanelPreparosSelecionados.Visible = false;
                    //CriaTabelaPreparoSelecionados();
                    break;
                case "2":
                    GridViewProcedimento.DataSource = null;
                    GridViewProcedimento.DataBind();
                    ddlSubGrupo.Items.Clear();
                    ddlForma.Items.Clear();
                    tbxProcedimento.Text = "";
                    PanelForma.Visible = false;
                    PanelCodigo.Visible = true;
                    PanelProcedimento.Visible = false;
                    PanelPreparos.Visible = false;
                    PanelPreparosSelecionados.Visible = false;
                    //CriaTabelaPreparoSelecionados();
                    break;
                case "3":
                    GridViewProcedimento.DataSource = null;
                    GridViewProcedimento.DataBind();
                    ddlSubGrupo.Items.Clear();
                    ddlForma.Items.Clear();
                    tbxCodigo.Text = "";
                    lblProcedimento.Text = "";
                    tbxProcedimento.Text = "";
                    PanelForma.Visible = false;
                    PanelCodigo.Visible = false;
                    PanelProcedimento.Visible = true;
                    PanelPreparos.Visible = false;
                    PanelPreparosSelecionados.Visible = false;
                    //CriaTabelaPreparoSelecionados();
                    break;
            }
        }


    }
}
