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
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using AjaxControlToolkit;

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioClassificacaoRiscoUnidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string[] unidadesvalidas = { "2927400004774", "2927400004030", "2927400004405", "2927400004154", "2927400004340", "2927400003999", "2927400028460", "2927400028452", "2927400028568" };
                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();

                //string[] cnes = Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p => p.Codigo).ToArray();

                //IList<ViverMais.Model.EstabelecimentoSaude> unidades = new List<ViverMais.Model.EstabelecimentoSaude>();
                //IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

                //for (int pos = 0; pos < cnes.Length; pos++)
                //    unidades.Add(iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes[pos]));

                GridView_QuadroClassificacao.DataSource = unidades;
                GridView_QuadroClassificacao.DataBind();
                //DataList_ClassificacaoRisco.DataSource = les;
                //DataList_ClassificacaoRisco.DataBind();
            }
        }

        /// <summary>
        /// Padroniza o DataList de acordo com as classificações de risco das unidades de saúde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnItemDataBound_FormataDataList(object sender, DataListItemEventArgs e) 
        //{
        //    IList<ViverMais.Model.Prontuario> fila = Factory.GetInstance<IProntuario>().BuscaFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(DataList_ClassificacaoRisco.DataKeys[e.Item.ItemIndex].ToString());
        //    IList<ViverMais.Model.ClassificacaoRisco> lc = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ClassificacaoRisco>().OrderBy(p => p.Codigo).ToList();

        //    DataTable tabela = new DataTable();
        //    tabela.Columns.Add(new DataColumn("QtdPaciente", typeof(string)));
        //    tabela.Columns.Add(new DataColumn("ClassificacaoRisco", typeof(string)));

        //    foreach (ViverMais.Model.ClassificacaoRisco c in lc) 
        //    {
        //        int count = fila.Where(p => p.ClassificacaoRisco != null && p.ClassificacaoRisco.Codigo == c.Codigo).ToList().Count();
        //        DataRow linha = tabela.NewRow();
        //        linha["QtdPaciente"] = count.ToString();
        //        linha["ClassificacaoRisco"] = c.Codigo;
        //        tabela.Rows.Add(linha);
        //    }

        //    GridView gv = (GridView)e.Item.FindControl("GridView_ClassificacaoRisco");
        //    gv.DataSource = tabela;
        //    gv.DataBind();
        //}

        /// <summary>
        /// Formata o gridview de classificação de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridViewClassificacao(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Accordion a = (Accordion)e.Row.FindControl("Accordion_Classificacao");
                GridView gv = (GridView)a.Panes[0].FindControl("GridView_ClassificacaoRisco");

                IList<ViverMais.Model.Prontuario> fila = Factory.GetInstance<IProntuario>().BuscarFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(GridView_QuadroClassificacao.DataKeys[e.Row.RowIndex]["CNES"].ToString());
                ViverMais.Model.ClassificacaoRisco[] classificacoesrisco = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.ClassificacaoRisco>().OrderBy(p => p.Ordem).ToArray();

                //DataTable tabela = new DataTable();
                //tabela.Columns.Add(new DataColumn("QtdPaciente", typeof(string)));
                //tabela.Columns.Add(new DataColumn("ClassificacaoRisco", typeof(string)));
                ArrayList tabela = new ArrayList();

                foreach (ViverMais.Model.ClassificacaoRisco classificacao in classificacoesrisco)
                {
                    int count = fila.Where(p => p.ClassificacaoRisco != null && p.ClassificacaoRisco.Codigo == classificacao.Codigo).Count();
                    tabela.Add(new { QtdPaciente = count, ClassificacaoRisco = classificacao.Imagem });
                    //DataRow linha = tabela.NewRow();
                    //linha["QtdPaciente"] = count.ToString();
                    //linha["ClassificacaoRisco"] = classificacao.Codigo;
                    //tabela.Rows.Add(linha);
                }

                gv.DataSource = tabela;
                gv.DataBind();
            }
        }

        ///// <summary>
        ///// Padroniza o gridview para formatar as imagens das classificações de risco
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e) 
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Image img = (Image)e.Row.FindControl("Image_Classificacao");
        //        img.ImageUrl = "~/Urgencia/img/" + Factory.GetInstance<IUrgenciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.ClassificacaoRisco>(int.Parse(img.ImageUrl)).Imagem;
        //    }
        //}
    }
}
