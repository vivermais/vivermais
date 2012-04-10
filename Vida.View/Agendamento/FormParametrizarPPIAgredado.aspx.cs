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
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agregado.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Globalization;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento.Agenda;

namespace ViverMais.View.Agendamento
{
    public partial class FormParametrizarPPIAgredado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "PARAMETRIZACAO_PARAMETRIZAR_PPI_REF", Modulo.AGENDAMENTO))
                {
                    GridViewPactosAtivos.PageIndexChanging += new GridViewPageEventHandler(GridViewPactosAtivos_PageIndexChanging);
                    GridViewPactosInativos.PageIndexChanging += new GridViewPageEventHandler(GridViewPactosInativos_PageIndexChanging);
                    
                    IGrupoAgregado iGrupoAgregado = Factory.GetInstance<IGrupoAgregado>();
                    IList<Municipio> municipios = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>("BA");
                    ddlMunicipios.Items.Add(new ListItem("Selecione...", "0"));
                    foreach (Municipio mun in municipios)
                    {
                        if (mun.Codigo != "292740")//Ele Retira Salvador da Lista de Pacto
                            ddlMunicipios.Items.Add(new ListItem(mun.Nome, mun.Codigo));
                    }

                    IList<GrupoAgregado> grupoAgregados = iGrupoAgregado.ListarTodos<GrupoAgregado>("Nome", true);
                    ddlGrupoAgregados.Items.Add(new ListItem("Selecione", "0"));
                    foreach (GrupoAgregado grupoAgregado in grupoAgregados)
                    {
                        ddlGrupoAgregados.Items.Add(new ListItem(grupoAgregado.Nome, grupoAgregado.Codigo));
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void GridViewPactosInativos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable table = (DataTable)Session["PactosInativos"];
            GridViewPactosInativos.DataSource = table;
            GridViewPactosInativos.PageIndex = e.NewPageIndex;
            GridViewPactosInativos.DataBind();
        }

        protected void GridViewPactosAtivos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataTable table = (DataTable)Session["PactosAtivos"];
            GridViewPactosAtivos.DataSource = table;
            GridViewPactosAtivos.PageIndex = e.NewPageIndex;
            GridViewPactosAtivos.DataBind();
        }

        /// <summary>
        /// Salva os Dados do Pacto para o Município Selecionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            PactoAgregadoProcedCBO pactoAgregadoProcedCbo;
            Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
            Municipio municipio = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
            if (pacto == null)//Se Não Existir Pacto Com Aquele Município ele gera um Novo Pacto
            {
                //Salva a Lista de Agregados que está na Sessão
                DataTable table = (DataTable)Session["AgregadoSelecionado"];
                if (table.Rows.Count != 0)
                {
                    pacto = new Pacto();
                    pacto.Municipio = municipio;
                    Factory.GetInstance<IPacto>().Inserir(pacto);
                    foreach (DataRow row in table.Rows)
                    {
                        //string id_agregado = row.Cells[0].Text;
                        pactoAgregadoProcedCbo = new PactoAgregadoProcedCBO();
                        //pactoAgregadoProcedCbo.Agregado.Codigo = id_agregado;
                        //pactoAgregadoProcedCbo.Ativo = Convert.ToInt32(PactoAgregadoProcedCBO.PactoAtivo.SIM);
                        pactoAgregadoProcedCbo.BloqueiaCota = ((bool)row["BloqueiaPorCota"]) == true ? 1 : 0;
                        pactoAgregadoProcedCbo.Pacto = pacto;
                        pactoAgregadoProcedCbo.Percentual = int.Parse(row["Percentual"].ToString());
                        pactoAgregadoProcedCbo.ValorPactuado = long.Parse(row["ValorPactuado"].ToString().Replace(",", "").Replace(".", ""));
                        string tipoPacto = row["TipoPacto"].ToString();
                        pactoAgregadoProcedCbo.TipoPacto = char.Parse(tipoPacto);
                        switch (tipoPacto)
                        {
                            case "P":
                                pactoAgregadoProcedCbo.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
                                break;
                            case "C":
                                pactoAgregadoProcedCbo.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
                                pactoAgregadoProcedCbo.Cbos.Add(Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(row["CodigoCBO"].ToString()));
                                break;
                        }
                        Factory.GetInstance<IPactoAgregadoProcedCbo>().Inserir(pactoAgregadoProcedCbo);
                    }
                    Factory.GetInstance<IPacto>().Salvar(pacto);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');window.location='FormParametrizarPPIAgredado.aspx';", true);
                //ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados Salvos com Sucesso!');window.location='FormParametrizarPPIAgredado.aspx'</script>");
                return;
            }
            else//Em Caso de Atualização
            {
                //Pega a Lista de Agregados que está na Sessão
                DataTable table = (DataTable)Session["AgregadoSelecionado"];
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        //string id_agregado = row.Cells[0].Text;
                        pactoAgregadoProcedCbo = new PactoAgregadoProcedCBO();
                        //pactoAgregadoProcedCbo.Agregado.Codigo = id_agregado;
                        //pactoAgregadoProcedCbo.Ativo = Convert.ToInt32(PactoAgregadoProcedCBO.PactoAtivo.SIM);
                        pactoAgregadoProcedCbo.BloqueiaCota = ((bool)row["BloqueiaPorCota"]) == true ? 1 : 0;
                        pactoAgregadoProcedCbo.Pacto = pacto;
                        pactoAgregadoProcedCbo.Percentual = int.Parse(row["Percentual"].ToString());
                        pactoAgregadoProcedCbo.ValorPactuado = long.Parse(row["ValorPactuado"].ToString().Replace(",", "").Replace(".", ""));
                        string tipoPacto = row["TipoPacto"].ToString();
                        pactoAgregadoProcedCbo.TipoPacto = char.Parse(tipoPacto);
                        switch (tipoPacto)
                        {
                            case "P":
                                pactoAgregadoProcedCbo.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
                                break;
                            case "C":
                                
                                pactoAgregadoProcedCbo.Procedimento.Codigo = row["CodigoProcedimento"].ToString();
                                pactoAgregadoProcedCbo.Cbos.Add(Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(row["CodigoCBO"].ToString()));
                                //pactoAgregadoProcedCbo.Cbo.Codigo = row["CodigoCBO"].ToString();
                                break;
                        }
                        Factory.GetInstance<IPactoAgregadoProcedCbo>().Inserir(pactoAgregadoProcedCbo);
                        //IList<PactoAgregadoProcedCBO> aux = new List<PactoAgregadoProcedCBO>();//Crio uma lista Auxiliar para poder preencher com os Agregados que foram selecionados pelo usuário e que não existam no Pacto
                        //Agregado agregado = iAgregado.BuscarPorCodigo<Agregado>(row[0].ToString());//Pega o Agregado do GridView e Busca pelo Código
                        //if (agregado != null)
                        //{
                        //    if (pacto.PactoAgregadoProcedCBO.Count != 0)//Se o Pacto já possui Agregado, Faço a busca para evitar duplicidade na inclusão
                        //    {

                        //        //Esta Consulta verifica na lista de Agregados do Pacto se já existe o Agregado que está no Gridview para evitar duplicidade
                        //        var result = from PactoAgregadoProcedCBO pa in pacto.PactoAgregadoProcedCBO
                        //                     where pa.Agregado.Codigo == agregado.Codigo
                        //                     select pa;
                        //        bool existe = false;

                        //        foreach (PactoAgregadoProcedCBO pa in result)
                        //        {
                        //            existe = true; //Caso Seja uma edição ele altera os valores para o pacto
                        //            string valorPactuado = row[2].ToString().Replace(".", "");
                        //            if (valorPactuado == "")
                        //            {
                        //                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Existe Agregado sem Valor Pactuado. Por Favor, Verifique!');</script>");
                        //                return;
                        //            }
                        //            pa.ValorPactuado = long.Parse(valorPactuado);
                        //            pa.BloqueiaCota = ((bool)row[3]) == true ? 1 : 0;
                        //            iViverMaisServiceFacade.Salvar(pa);
                        //        }
                        //        if (!existe)//Caso Não exista, ele insere na Lista Auxiliar
                        //        {
                        //            pactoAgregadoProcedCbo = new PactoAgregadoProcedCBO();
                        //            //Seta os parametros do PactoAgregado que estão na GridView
                        //            pactoAgregadoProcedCbo.BloqueiaCota = ((bool)row[3]) == true ? 1 : 0;
                        //            string valorPactuado = row[2].ToString().Replace(",", "");
                        //            if (valorPactuado == "")
                        //            {
                        //                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Existe Agregado sem Valor Pactuado. Por Favor, Verifique!');</script>");
                        //                return;
                        //            }
                        //            pactoAgregadoProcedCbo.ValorPactuado = long.Parse(valorPactuado);
                        //            pactoAgregadoProcedCbo.ValorRestante = long.Parse(valorPactuado);
                        //            agregado = iAgregado.BuscarPorCodigo<Agregado>(row[0].ToString());//Pega o Agregado do GridView e Busca pelo Código
                        //            pactoAgregadoProcedCbo.Agregado = agregado;

                        //            //Recarrego o Objeto Pacto porque o nhibernate não reconhece o sequence como mecanismo válido de geração de identity
                        //            //Para poder pegar o Código do Pacto
                        //            pacto = iPacto.BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
                        //            pactoAgregadoProcedCbo.Pacto = pacto;
                        //            iViverMaisServiceFacade.Inserir(pactoAgregadoProcedCbo);
                        //        }

                        //        iViverMaisServiceFacade.Atualizar(pacto);

                        //    }//Se o Pacto não possui PactoAgregado, ele adiciona
                        //    else
                        //    {
                        //        pactoAgregadoProcedCbo = new PactoAgregadoProcedCBO();
                        //        //Seta os parametros do PactoAgregado que estão na GridView
                        //        pactoAgregadoProcedCbo.BloqueiaCota = ((bool)row[3]) == true ? 1 : 0;
                        //        string valorPactuado = row[2].ToString().Replace(",", "").Replace(".", "");
                        //        if (valorPactuado == "")
                        //        {
                        //            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Existe Agregado sem Valor Pactuado. Por Favor, Verifique!');</script>");
                        //            return;
                        //        }
                        //        pactoAgregadoProcedCbo.ValorPactuado = long.Parse(valorPactuado);
                        //        pactoAgregadoProcedCbo.ValorRestante = long.Parse(valorPactuado);
                        //        pactoAgregadoProcedCbo.Agregado = iAgregado.BuscarPorCodigo<Agregado>(row[0].ToString());//Pega o Agregado do GridView e Busca pelo Código
                        //        pactoAgregadoProcedCbo.Pacto = pacto;
                        //        pactoAgregadoProcedCbo.Agregado = agregado;
                        //        iViverMaisServiceFacade.Inserir(pactoAgregadoProcedCbo);
                        //        pactoAgregadoProcedCbo = iPactoAgregado.BuscarPorPactoAgregado<PactoAgregadoProcedCBO>(agregado.Codigo, pacto.Codigo.ToString());
                        //    }
                        //}
                    }
                    //iViverMaisServiceFacade.Salvar(pacto);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Informe um Agregado para o Município Selecionado!');</script>");
                    return;
                }
            }
            Session["AgregadoSelecionado"] = null;
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script type='text/javascript'>alert('Dados Salvos com Sucesso!');window.location='FormParametrizarPPIAgredado.aspx'</script>");
            return;
        }

        protected void rbtlTipoPacto_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString())
            {
                PanelProcedimento.Visible = false;
                PanelCBO.Visible = false;
            }

            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString())
            {
                PanelProcedimento.Visible = true;
                PanelCBO.Visible = false;
                CarregaProcedimentos();
            }
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO).ToString())
            {
                CarregaProcedimentos();
                PanelProcedimento.Visible = true;
                PanelCBO.Visible = true;
            }
        }

        void CarregaCBOs(string id_procedimento)
        {
            IList<CBO> cbos = Factory.GetInstance<ICBO>().ListarCBOsPorProcedimento<CBO>(id_procedimento);
            ddlCBO.Items.Clear();
            ddlCBO.Items.Add(new ListItem("Selecione...", "0"));
            foreach (CBO cbo in cbos)
                ddlCBO.Items.Add(new ListItem(cbo.Nome, cbo.Codigo));
            ddlCBO.Attributes.Add("onmouseover", "javascript:showTooltip(this);");
        }

        protected void btnAddAgregado_Click(object sender, EventArgs e)
        {
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString())
                IncluiAgregadoNaTabelaAgregadoSelecionado(ddlAgregados.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text, Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM), "");
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString())
                IncluiPactoProcedimentoNaListaDeSelecionados(ddlAgregados.SelectedValue, ddlProcedimento.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text, Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM), "");
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO).ToString())
                IncluiPactoCBONaListaDeSelecionados(ddlAgregados.SelectedValue, ddlProcedimento.SelectedValue, ddlCBO.SelectedValue, (DataTable)Session["AgregadoSelecionado"], tbxValorPacto.Text, Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM), "");
        }

        protected void ddlGrupoAgregados_SelectedIndexChanged(object sender, EventArgs e)
        {
            ISubGrupoAgregado iSubGrupoAgregado = Factory.GetInstance<ISubGrupoAgregado>();
            IList<SubGrupoAgregado> subGrupoAgregados = iSubGrupoAgregado.BuscaPorGrupo<SubGrupoAgregado>(ddlGrupoAgregados.SelectedValue);
            ddlSubGrupoAgregado.Items.Clear();
            ddlAgregados.Items.Clear();
            ddlSubGrupoAgregado.Items.Add(new ListItem("Selecione", "0"));
            ddlAgregados.Items.Add(new ListItem("Selecione", "0"));
            foreach (SubGrupoAgregado subGrupoAgregado in subGrupoAgregados)
            {
                ddlSubGrupoAgregado.Items.Add(new ListItem(subGrupoAgregado.Nome, subGrupoAgregado.Codigo));
            }

        }

        protected void ddlSubGrupoAgregado_SelectedIndexChanged(object sender, EventArgs e)
        {
            IAgregado iAgregado = Factory.GetInstance<IAgregado>();
            IList<Agregado> agregados = iAgregado.BuscaPorSubGrupo<Agregado>(ddlSubGrupoAgregado.SelectedValue);
            ddlAgregados.Items.Clear();
            ddlAgregados.Items.Add(new ListItem("Selecione", "0"));
            foreach (Agregado agregado in agregados)
            {
                ddlAgregados.Items.Add(new ListItem(agregado.Nome, agregado.Codigo));
            }
        }

        /// <summary>
        /// Função que inicializa a Tabela que irá Obter a Lista Dos Pactos ativos do Estabelecimento
        /// </summary>
        /// <returns></returns>
        private DataTable CriaTabelaAgregadoSelecionado()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Codigo");
            table.Columns.Add("Ano");
            //table.PrimaryKey = new DataColumn[] { table.Columns["Codigo"] };//Define o código do agregado como chave primária, para facilitar a busca e exclusão
            table.Columns.Add("TipoPacto");
            table.Columns.Add("CodigoAgregado");
            table.Columns.Add("NomeAgregado");
            table.Columns.Add("CodigoProcedimento");
            table.Columns.Add("Procedimento");
            table.Columns.Add("CodigoCBO");
            table.Columns.Add("CBO");
            table.Columns.Add("ValorPactuado");
            table.Columns.Add("BloqueiaPorCota", typeof(bool));
            table.Columns.Add("Percentual");
            //Session["AgregadoSelecionado"] = table;
            return table;
        }

        void IncluiPactoCBONaListaDeSelecionados(string id_agregado, string id_procedimento, string id_cbo, DataTable table, string valor, int bloqueioCota, string percentual)
        {
            if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, id_procedimento, id_cbo))
            {
                Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
                if (pacto == null)
                {
                    pacto = new Pacto();
                    pacto.Municipio = Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
                    Factory.GetInstance<IPacto>().Salvar(pacto);
                }


                PactoAgregadoProcedCBO pactoAgregadoProcedCBO = new PactoAgregadoProcedCBO();
                pactoAgregadoProcedCBO.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
                pactoAgregadoProcedCBO.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(id_procedimento);
                pactoAgregadoProcedCBO.Cbos.Add(Factory.GetInstance<ICBO>().BuscarPorCodigo<CBO>(id_cbo));
                pactoAgregadoProcedCBO.Ativo = true;
                pactoAgregadoProcedCBO.BloqueiaCota = bloqueioCota;
                pactoAgregadoProcedCBO.Percentual = percentual == "" ? 0 : int.Parse(percentual);
                pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO);
                float valorPactuadoMenosPercentualUrgencia = RetornaValorMenosPercentualUrgenciaEmergencia(float.Parse(valor));
                pactoAgregadoProcedCBO.ValorPactuado = valorPactuadoMenosPercentualUrgencia;
                pactoAgregadoProcedCBO.ValorMensal = Decimal.Parse(pactoAgregadoProcedCBO.ValorPactuado.ToString()) / 12;
                //pactoAgregadoProcedCBO.ValorPactuado = long.Parse(valor.Replace(",", "").Replace(".", ""));
                pactoAgregadoProcedCBO.Pacto = pacto;
                pactoAgregadoProcedCBO.Ano = DateTime.Now.Year;
                pactoAgregadoProcedCBO.DataPacto = DateTime.Now;
                pactoAgregadoProcedCBO.Usuario = (Usuario)Session["Usuario"];
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAgregadoProcedCBO);

                //Define os Valores Mensais para este Pacto durante 12 meses
                for (int i = 0; i <= 11; i++)
                {
                    PactoReferenciaSaldo pactoReferenciaSaldo = new PactoReferenciaSaldo();
                    DateTime data = DateTime.Now.AddMonths(i);
                    pactoReferenciaSaldo.PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
                    pactoReferenciaSaldo.Mes = data.Month;
                    //pactoAgregadoProcedCBO.ValorMensal = pactoReferenciaSaldo.PactoAgregadoProcedCBO.ValorPactuado / 12;
                    pactoReferenciaSaldo.ValorRestante = pactoAgregadoProcedCBO.ValorMensal;
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoReferenciaSaldo);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluído com Sucesso!');", true);
            }
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
        }

        void IncluiPactoProcedimentoNaListaDeSelecionados(string id_agregado, string id_procedimento, DataTable table, string valor, int bloqueioCota, string percentual)
        {
            if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, id_procedimento, ""))
            {
                Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
                if (pacto == null)
                {
                    pacto = new Pacto();
                    pacto.Municipio = Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
                    Factory.GetInstance<IPacto>().Salvar(pacto);
                }

                PactoAgregadoProcedCBO pactoAgregadoProcedCBO = new PactoAgregadoProcedCBO();
                pactoAgregadoProcedCBO.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
                pactoAgregadoProcedCBO.Procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(id_procedimento);
                pactoAgregadoProcedCBO.Ativo = true;
                pactoAgregadoProcedCBO.BloqueiaCota = bloqueioCota;
                pactoAgregadoProcedCBO.Percentual = percentual == "" ? 0 : int.Parse(percentual);
                pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO);
                float valorPactuadoMenosPercentualUrgencia = RetornaValorMenosPercentualUrgenciaEmergencia(float.Parse(valor));
                pactoAgregadoProcedCBO.ValorPactuado = valorPactuadoMenosPercentualUrgencia;
                //pactoAgregadoProcedCBO.ValorPactuado = long.Parse(valor.Replace(",", "").Replace(".", ""));
                pactoAgregadoProcedCBO.Pacto = pacto;
                pactoAgregadoProcedCBO.PercentualUrgenciaEmergencia = Pacto.PercentualUrgenciaEmergencia;
                pactoAgregadoProcedCBO.DataPacto = DateTime.Now;
                pactoAgregadoProcedCBO.Ano = DateTime.Now.Year;
                pactoAgregadoProcedCBO.Usuario = (Usuario)Session["Usuario"];
                Factory.GetInstance<IViverMaisServiceFacade>().Inserir(pactoAgregadoProcedCBO);

                //Define os Valores Mensais para este Pacto durante 12 meses
                for (int i = 0; i <= 11; i++)
                {
                    PactoReferenciaSaldo pactoReferenciaSaldo = new PactoReferenciaSaldo();
                    DateTime data = DateTime.Now.AddMonths(i);
                    pactoReferenciaSaldo.PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
                    pactoReferenciaSaldo.Mes = data.Month;
                    //pactoReferenciaSaldo.ValorMensal = pactoReferenciaSaldo.PactoAgregadoProcedCBO.ValorPactuado / 12;
                    pactoReferenciaSaldo.ValorRestante = pactoAgregadoProcedCBO.ValorMensal;
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoReferenciaSaldo);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluído com Sucesso!');", true);
            }
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);

            //    Agregado agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
            //    DataRow row = table.NewRow();
            //    row["Codigo"] = agregado.Codigo;
            //    row["TipoPacto"] = "PROCEDIMENTO";//Tipo de Pacto - Por Procedimento
            //    row["Nome"] = agregado.Nome;
            //    row["CBO"] = string.Empty;
            //    row["CodigoCBO"] = string.Empty;
            //    row["BloqueiaPorCota"] = bloqueioCota == 1 ? true : false;
            //    if (String.IsNullOrEmpty(id_procedimento))//Caso seja uma nova Inserção de Registro
            //    {

            //        row["CodigoProcedimento"] = string.Empty;
            //        row["Procedimento"] = string.Empty;
            //        row["ValorPactuado"] = string.Empty;
            //        row["Percentual"] = string.Empty;
            //    }
            //    else
            //    {
            //        Procedimento procedimento = Factory.GetInstance<IProcedimento>().BuscarPorCodigo<Procedimento>(id_procedimento);
            //        row["CodigoProcedimento"] = procedimento.Codigo;
            //        row["Procedimento"] = procedimento.Nome;
            //        row["ValorPactuado"] = valor;
            //        row["Percentual"] = percentual;
            //    }
            //    table.Rows.Add(row);
            //}
            //else
            //{
            //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já Existe um Pacto do Tipo PROCEDIMENTO para o procedimento Selecionado. Não Foi Possível Incluir.');", true);
            //    return;
            //}
        }

        public float RetornaValorMenosPercentualUrgenciaEmergencia(float valor)
        {
            float valorAlterado = valor - ((valor * Pacto.PercentualUrgenciaEmergencia) / 100);
            return valorAlterado;
        }

        /// <summary>
        /// Esse método é utilizado para Incluir um Agregado na Tabela de Agregados Selecionados
        /// </summary>
        /// <param name="id_agregado"></param>
        /// <param name="table"></param>
        private void IncluiAgregadoNaTabelaAgregadoSelecionado(string id_agregado, DataTable table, string valor, int bloqueioCota, string percentual)
        {
            ////Verifica pela chave primária (Codigo) se o Item Selecionado já existe no DataTable de Agregados Selecionados
            //if (table.Rows.Find(id_agregado) == null)//Se não existir ele adiciona
            //{
            if (!VerificaSeExistePactoNaLista(rbtlTipoPacto.SelectedValue, id_agregado, "", ""))
            {
                Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
                if (pacto == null)
                {
                    pacto = new Pacto();
                    pacto.Municipio = Factory.GetInstance<IMunicipio>().BuscarPorCodigo<Municipio>(ddlMunicipios.SelectedValue);
                    Factory.GetInstance<IPacto>().Salvar(pacto);
                }

                PactoAgregadoProcedCBO pactoAgregadoProcedCBO = new PactoAgregadoProcedCBO();
                pactoAgregadoProcedCBO.Agregado = Factory.GetInstance<IAgregado>().BuscarPorCodigo<Agregado>(id_agregado);
                pactoAgregadoProcedCBO.Ativo = true;
                pactoAgregadoProcedCBO.BloqueiaCota = bloqueioCota;
                pactoAgregadoProcedCBO.Percentual = percentual == "" ? 0 : int.Parse(percentual);
                pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO);
                float valorPactuadoMenosPercentualUrgencia = RetornaValorMenosPercentualUrgenciaEmergencia(float.Parse(valor));
                pactoAgregadoProcedCBO.ValorPactuado = valorPactuadoMenosPercentualUrgencia;
                pactoAgregadoProcedCBO.Pacto = pacto;
                pactoAgregadoProcedCBO.PercentualUrgenciaEmergencia = Pacto.PercentualUrgenciaEmergencia;
                pactoAgregadoProcedCBO.DataPacto = DateTime.Now;
                pactoAgregadoProcedCBO.Ano = DateTime.Now.Year;
                pactoAgregadoProcedCBO.PercentualUrgenciaEmergencia = Pacto.PercentualUrgenciaEmergencia;
                pactoAgregadoProcedCBO.Usuario = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Usuario>(((Usuario)Session["Usuario"]).Codigo);
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAgregadoProcedCBO);

                //Define os Valores Mensais para este Pacto durante 12 meses
                //IList<PactoReferenciaSaldo> pactosReferenciaSaldo = new List<PactoReferenciaSaldo>();
                for (int i = 0; i <= 11; i++)
                {
                    PactoReferenciaSaldo pactoReferenciaSaldo = new PactoReferenciaSaldo();
                    DateTime data = DateTime.Now.AddMonths(i);
                    pactoReferenciaSaldo.PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
                    pactoReferenciaSaldo.Mes = data.Month;
                    //pactoReferenciaSaldo.ValorMensal = Decimal.Parse(pactoAgregadoProcedCBO.ValorPactuado.ToString()) / 12;
                    pactoReferenciaSaldo.ValorRestante = pactoAgregadoProcedCBO.ValorMensal;
                    //pactosReferenciaSaldo.Add(pactoReferenciaSaldo);
                    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoReferenciaSaldo);
                }
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluído com Sucesso!');", true);
            }
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
        }

        protected void ddlMunicipios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CriaTabelaAgregadoSelecionado();//Crio a tabela para os Agregados Selecionados
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
            CarregaPactosInativosDoMunicipio(ddlMunicipios.SelectedValue);
        }

        protected void GridViewPactosAtivos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int index = e.RowIndex;
            GridViewRow linha = GridViewPactosAtivos.Rows[e.RowIndex];
            string id_Pacto_Agregado_Proced_cbo = linha.Cells[0].Text;
            string percentual = ((TextBox)linha.FindControl("tbxPercentual")).Text;
            bool checkeed = ((CheckBox)(linha.FindControl("chkBoxBloqueiaCota"))).Checked;

            PactoAgregadoProcedCBO pactoAgregadoProced_Cbo = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<PactoAgregadoProcedCBO>(int.Parse(id_Pacto_Agregado_Proced_cbo));
            if (pactoAgregadoProced_Cbo != null)
            {
                pactoAgregadoProced_Cbo.Percentual = int.Parse(percentual);
                pactoAgregadoProced_Cbo.BloqueiaCota = checkeed == true ? 1 : 0;
                pactoAgregadoProced_Cbo.DataUltimaOperacao = DateTime.Now;
                Factory.GetInstance<IViverMaisServiceFacade>().Atualizar(pactoAgregadoProced_Cbo);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 44, "ID_PACTO: " + pactoAgregadoProced_Cbo.Codigo));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Dados Salvos com Sucesso!');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi Possível Alterar. Tente Novamente!');", true);
                return;
            }
            GridViewPactosAtivos.EditIndex = -1;
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
            //CarregaPactosInativosDoMunicipio(ddlMunicipios.SelectedValue);

            //CheckBox bloqueioCota = ((CheckBox)linha.FindControl("chkBoxBloqueiaCota"));
            //DataTable table = (DataTable)Session["AgregadoSelecionado"];
            //string cod_agregado = GridViewPactosAtivos.Rows[e.RowIndex].Cells[0].Text;

            ////Seta os Valores do Pacto

            //table.Rows[e.RowIndex][8] = checkeed;
            //table.Rows[e.RowIndex][9] = percentual;

            //Session["AgregadoSelecionado"] = table;
            //GridViewPactosAtivos.EditIndex = -1;
            //GridViewPactosAtivos.DataSource = table;
            //GridViewPactosAtivos.DataBind();
        }

        protected void GridViewPactosAtivos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int index = e.NewEditIndex;
            Session["IndexSelecionado"] = index;
            GridViewPactosAtivos.EditIndex = e.NewEditIndex;
            CheckBox chkBox = (CheckBox)GridViewPactosAtivos.Rows[index].FindControl("chkBoxBloqueiaCota");
            TextBox tbxPercentual = (TextBox)GridViewPactosAtivos.Rows[index].FindControl("tbxPercentual");

            if (chkBox.Checked)
                tbxPercentual.Enabled = false;
            else
                tbxPercentual.Enabled = true;
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
        }

        void CarregaProcedimentos()
        {
            ddlProcedimento.Items.Clear();
            IList<Procedimento> procedimentos = Factory.GetInstance<IProcedimentoAgregado>().ListarProcedimentosPorAgregado<Procedimento>(ddlAgregados.SelectedValue).Distinct(new GenericComparer<Procedimento>("Codigo")).ToList();
            if (procedimentos.Count != 0)
            {
                ddlProcedimento.Items.Add(new ListItem("Selecione...", "0"));
                foreach (Procedimento procedimento in procedimentos)
                    ddlProcedimento.Items.Add(new ListItem(procedimento.Nome, procedimento.Codigo));
            }
        }

        protected void ddlProcedimento_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbtlTipoPacto.SelectedValue == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO).ToString())//Se o Tipo de Pacto Não seja Por CBO, não é necessário Listar os CBOS
                CarregaCBOs(ddlProcedimento.SelectedValue);
        }

        protected void chkBoxBloqueiaCota_OnCheckedChanged(object sender, EventArgs e)
        {
            int indexSelecionado = int.Parse(Session["IndexSelecionado"].ToString());
            CheckBox chkBox = (CheckBox)GridViewPactosAtivos.Rows[indexSelecionado].FindControl("chkBoxBloqueiaCota");
            TextBox tbxPercentual = (TextBox)GridViewPactosAtivos.Rows[indexSelecionado].FindControl("tbxPercentual");
            RequiredFieldValidator RequiredFieldTbxPercentual = (RequiredFieldValidator)GridViewPactosAtivos.Rows[indexSelecionado].FindControl("RequiredFieldTbxPercentual");
            if (chkBox.Checked)
            {
                tbxPercentual.Enabled = false;
                tbxPercentual.Text = "0";
                RequiredFieldTbxPercentual.Enabled = false;
            }
            else
            {
                tbxPercentual.Enabled = true;
                RequiredFieldTbxPercentual.Enabled = true;
            }
        }

        private DataTable CriaTabelaPactosInativos()
        {
            DataTable table = new DataTable();
            table.Columns.Add("Ano");
            table.Columns.Add("TipoPacto");
            table.Columns.Add("Agregado");
            table.Columns.Add("Procedimento");
            table.Columns.Add("CBO");
            table.Columns.Add("ValorPactuado");
            table.Columns.Add("BloqueiaPorCota", typeof(bool));
            table.Columns.Add("Percentual");
            table.Columns.Add("DataOperacao");
            table.Columns.Add("Operador");
            return table;
        }

        void CarregaPactosInativosDoMunicipio(string id_municipio)
        {
            Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
            if (pacto != null)
            {
                IList<PactoAgregadoProcedCBO> pactosInativos = Factory.GetInstance<IPactoAgregadoProcedCbo>().BuscaPorPacto<PactoAgregadoProcedCBO>(pacto.Codigo).Where(p => p.Ativo == false).ToList();
                if (pactosInativos.Count != 0)
                {
                    lblSemRegistroInativo.Visible = false;

                    DataTable table = CriaTabelaPactosInativos();
                    //table.Columns.Add("TipoPacto");
                    //table.Columns.Add("Agregado");
                    //table.Columns.Add("Procedimento");
                    //table.Columns.Add("CBO");
                    //table.Columns.Add("ValorPacto");
                    //table.Columns.Add("BloqueiaPorCota", typeof(bool));
                    //table.Columns.Add("Percentual");
                    //table.Columns.Add("DataOperacao");
                    //table.Columns.Add("Operador");
                    foreach (PactoAgregadoProcedCBO pactoInativo in pactosInativos)
                    {
                        DataRow row = table.NewRow();
                        row["Ano"] = pactoInativo.Ano.ToString("0000");
                        switch (pactoInativo.TipoPacto.ToString())
                        {
                            case "A": row["TipoPacto"] = "AGREGADO";
                                break;
                            case "P": row["TipoPacto"] = "PROCEDIMENTO";
                                break;
                            case "C": row["TipoPacto"] = "CBO";
                                break;
                        }
                        row["Agregado"] = pactoInativo.Agregado.Nome;
                        row["Procedimento"] = pactoInativo.Procedimento == null ? "" : pactoInativo.Procedimento.Nome;
                        if (pactoInativo.Cbos.Count == 0)
                        {
                            row["CBO"] = string.Empty;
                        }
                        else if (pactoInativo.Cbos.Count == 1)
                        {
                            row["CBO"] = pactoInativo.Cbos.FirstOrDefault().Nome;
                        }
                        else
                        {
                            //Colocar no Nome do Grupo
                            //Colocar no Nome do Grupo
                            GrupoCBO grupoCBO = Factory.GetInstance<IGrupoCBO>().BuscarPorCBO<GrupoCBO>(pactoInativo.Cbos.FirstOrDefault().Codigo);
                            if (grupoCBO != null)
                                row["CBO"] = grupoCBO.NomeGrupo;
                        }

                        //double valor = double.Parse(pactoInativo.ValorPactuado.ToString());
                        //if (valor.ToString().Length <= 2)
                        //{
                        //    valor = double.Parse(valor.ToString("000").Insert(valor.ToString("000").Length - 2, ","));

                        //}
                        //else
                        //{
                        //    valor = double.Parse(valor.ToString().Insert(pactoInativo.ValorPactuado.ToString().Length - 2, ","));
                        //}

                        //row["CBO"] = pactoInativo.Cbos.Count == 0 ? "" : pactoInativo.Cbo.Nome;
                        //double valor = double.Parse(pactoInativo.ValorPactuado.ToString().Insert(pactoInativo.ValorPactuado.ToString().Length - 2, ","));
                        row["ValorPactuado"] = pactoInativo.ValorPactuado.ToString("C", new CultureInfo("pt-BR"));
                        row["BloqueiaPorCota"] = pactoInativo.BloqueiaCota == 1 ? true : false;
                        row["Percentual"] = pactoInativo.Percentual.ToString();
                        row["DataOperacao"] = pactoInativo.DataUltimaOperacao != DateTime.MinValue ? pactoInativo.DataUltimaOperacao.ToString("dd/MM/yyyy") : string.Empty;
                        row["Operador"] = pactoInativo.Usuario != null ? pactoInativo.Usuario.Nome : string.Empty;
                        table.Rows.Add(row);
                    }
                    Session["PactosInativos"] = table;
                    GridViewPactosInativos.DataSource = table;
                    GridViewPactosInativos.DataBind();
                }
                else
                {
                    lblSemRegistroInativo.Visible = true;
                }
            }
            else
            {
                lblSemRegistroInativo.Visible = true;
            }
        }

        void CarregaPactosAtivosDoMunicipio(string id_municipio)
        {
            Pacto pacto = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(ddlMunicipios.SelectedValue);
            if (pacto != null)
            {
                IList<PactoAgregadoProcedCBO> pactosAtivos = Factory.GetInstance<IPactoAgregadoProcedCbo>().BuscaPorPacto<PactoAgregadoProcedCBO>(pacto.Codigo).Where(p => p.Ativo == true).ToList();
                if (pactosAtivos.Count != 0)
                {
                    lblSemRegistroAtivo.Visible = false;
                    DataTable table = CriaTabelaAgregadoSelecionado();

                    foreach (PactoAgregadoProcedCBO pactoAtivo in pactosAtivos)
                    {
                        DataRow row = table.NewRow();
                        row["Codigo"] = pactoAtivo.Codigo.ToString();
                        row["Ano"] = pactoAtivo.Ano.ToString("0000");
                        switch (pactoAtivo.TipoPacto.ToString())
                        {
                            case "A": row["TipoPacto"] = "AGREGADO";
                                break;
                            case "P": row["TipoPacto"] = "PROCEDIMENTO";
                                break;
                            case "C": row["TipoPacto"] = "CBO";
                                break;
                        }
                        row["CodigoAgregado"] = pactoAtivo.Agregado.Codigo;
                        row["NomeAgregado"] = pactoAtivo.Agregado.Nome;
                        if (pactoAtivo.Procedimento != null)
                        {
                            row["CodigoProcedimento"] = pactoAtivo.Procedimento.Codigo;
                            row["Procedimento"] = pactoAtivo.Procedimento.Nome;
                        }
                        else
                        {
                            row["CodigoProcedimento"] = string.Empty;
                            row["Procedimento"] = string.Empty;
                        }

                        if (pactoAtivo.Cbos.Count == 0)
                        {
                            row["CodigoCBO"] = string.Empty;
                            row["CBO"] = string.Empty;
                            //row["CBO"] = string.Empty;
                            //row["CBO"] = pactoAtivo.Cbo.Nome != "" ? pactoAtivo.Cbo.Nome : "";
                        }
                        else if (pactoAtivo.Cbos.Count == 1)
                        {
                            row["CBO"] = pactoAtivo.Cbos.FirstOrDefault().Nome;
                            row["CodigoCBO"] = pactoAtivo.Cbos.FirstOrDefault().Codigo;
                        }
                        else
                        {
                            //Colocar no Nome do Grupo
                            GrupoCBO grupoCBO = Factory.GetInstance<IGrupoCBO>().BuscarPorCBO<GrupoCBO>(pactoAtivo.Cbos.FirstOrDefault().Codigo);
                            if(grupoCBO != null)
                                row["CBO"] = grupoCBO.NomeGrupo;
                            else
                                row["CBO"] = "Grupo Ñ Localizado";
                        }

                        //if (pactoAtivo.Cbo != null)
                        //{
                        //    row["CodigoCBO"] = pactoAtivo.Cbo.Codigo;
                        //    row["CBO"] = pactoAtivo.Cbo.Nome != "" ? pactoAtivo.Cbo.Nome : "";
                        //}
                        //else
                        //{
                        //    row["CodigoCBO"] = string.Empty;
                        //    row["CBO"] = string.Empty;
                        //}
                        //int campoTotal = pactoAtivo.ValorPactuado.ToString("C", new CultureInfo("pt-BR")).Length;
                        //row["ValorPactuado"] = pactoAtivo.ValorPactuado.ToString("C", new CultureInfo("pt-BR")).Remove(campoTotal-1,2);
                        
                        //double valor = double.Parse(pactoAtivo.ValorPactuado.ToString());
                        //if (valor.ToString().Length <= 2)
                        //{
                        //    valor = double.Parse(valor.ToString("000").Insert(valor.ToString("000").Length - 2, ","));
                            
                        //}
                        //else
                        //{
                        //    valor = double.Parse(valor.ToString().Insert(pactoAtivo.ValorPactuado.ToString().Length - 2, ","));
                        //}
                        row["ValorPactuado"] = pactoAtivo.ValorPactuado.ToString("C", new CultureInfo("pt-BR"));
                        //row["ValorPactuado"] = pactoAtivo.ValorPactuado.ToString("C2", new CultureInfo("pt-BR"));
                        row["BloqueiaPorCota"] = pactoAtivo.BloqueiaCota == 1 ? true : false;
                        row["Percentual"] = pactoAtivo.Percentual.ToString();
                        table.Rows.Add(row);
                    }

                    Session["PactosAtivos"] = table;
                    GridViewPactosAtivos.DataSource = table;
                    GridViewPactosAtivos.DataBind();
                }
                else
                {
                    GridViewPactosAtivos.DataSource = null;
                    GridViewPactosAtivos.DataBind();
                    lblSemRegistroAtivo.Visible = true;
                }
            }
            else
            {
                GridViewPactosAtivos.DataSource = null;
                GridViewPactosAtivos.DataBind();
                lblSemRegistroAtivo.Visible = true;
            }
        }

        public bool VerificaSeExistePactoNaLista(string tipoPactoAVerificar, string id_agregado, string id_procedimento, string id_cbo)
        {
            foreach (GridViewRow row in GridViewPactosAtivos.Rows)
            {
                string tipoPactoDoGrid = row.Cells[1].Text.Substring(0, 1).ToUpper();

                //Se ele está tentando incluir um Agregado
                if (tipoPactoAVerificar == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString())
                {
                    //Verifico se é o mesmo Agregado
                    if (id_agregado == row.Cells[2].Text)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não Foi Possível Incluir o Pacto. Verifique as Possíveis causas:\\n1.Já existe um pacto para este Agregado.');", true);
                        return true;
                    }
                }
                //Se estiver tentando incluir um Procedimento
                else if (tipoPactoAVerificar == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString())
                {
                    //if ((id_agregado == row.Cells[2].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString()))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um pacto do tipo AGREGADO para o Agregado Selecionado. Não Foi Possível Incluir!');", true);
                    //    return true;
                    //}
                    //else
                    //{
                    if ((id_agregado == row.Cells[2].Text) && (id_procedimento == row.Cells[4].Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não Foi Possível Incluir o Pacto. Verifique as Possíveis causas:\\n1.Já Existe um Pacto do tipo PROCEDIMENTO.');", true);
                        return true;
                    }
                    //}
                }
                else if (tipoPactoAVerificar == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO).ToString())
                {
                    //if ((id_agregado == row.Cells[2].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO).ToString()))
                    //{
                    //    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo AGREGADO Para o Agregado Selecionado. Não Foi Possível Incluir!');", true);
                    //    return true;
                    //}
                    //else
                    //{
                    //    //Verifico Se é o mesmo Procedimento
                    //    if ((id_procedimento == row.Cells[4].Text) && (tipoPactoDoGrid == Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO).ToString()))
                    //    {
                    //        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Existe Um Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado. Não Foi Possível Incluir!');", true);
                    //        return true;
                    //    }
                    //    else
                    //    {
                    if ((id_agregado == row.Cells[2].Text) && (id_procedimento == row.Cells[4].Text) && (id_cbo == row.Cells[6].Text))
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe Um Pacto com os dados informados. Não Foi Possível Incluir!');", true);
                        return true;
                    }
                    //    }
                    //}
                }
            }
            return false;
        }

        protected void GridViewPactosAtivos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewPactosAtivos.EditIndex = -1;
            Session["IndexSelecionado"] = null;
            CarregaPactosInativosDoMunicipio(ddlMunicipios.SelectedValue);
            CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
        }

        protected void GridViewPactosAtivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Inativar")
            {
                //Pega o Índice da Linha Selecionada
                GridViewRow row = (GridViewRow)((Control)e.CommandSource).Parent.Parent;
                int index = row.RowIndex;
                int id_PactoAgregadoProcedCBO = int.Parse(GridViewPactosAtivos.Rows[index].Cells[0].Text);
                PactoAgregadoProcedCBO pactoAgregadoProcedCBO = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<PactoAgregadoProcedCBO>(id_PactoAgregadoProcedCBO);
                pactoAgregadoProcedCBO.Ativo = false;
                pactoAgregadoProcedCBO.DataUltimaOperacao = DateTime.Now;
                pactoAgregadoProcedCBO.Usuario = (Usuario)Session["Usuario"];
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAgregadoProcedCBO);
                Factory.GetInstance<IAmbulatorial>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)(Session["Usuario"])).Codigo, 43, pactoAgregadoProcedCBO.Codigo.ToString()));
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Pacto Inativado com Sucesso!');", true);
                CarregaPactosAtivosDoMunicipio(ddlMunicipios.SelectedValue);
                CarregaPactosInativosDoMunicipio(ddlMunicipios.SelectedValue);
            }
        }
    }
}