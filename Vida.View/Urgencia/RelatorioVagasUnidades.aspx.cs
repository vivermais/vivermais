﻿using System;
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
    public partial class RelatorioVagasUnidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string[] unidadesvalidas = { "2927400004774", "2927400004030", "2927400004405", "2927400004154", "2927400004340", "2927400003999", "2927400028460", "2927400028452", "2927400028568" };
                //IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().ListarTodos<ViverMais.Model.EstabelecimentoSaude>().Where(p=>p.RazaoSocial.ToUpper() != "NAO SE APLICA"
                //    && (Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p2 => p2.Codigo).Contains(p.CNES))
                //    ).OrderBy(p => p.NomeFantasia).ToList();


                //string[] cnes = Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p => p.Codigo).ToArray();

                //IList<ViverMais.Model.EstabelecimentoSaude> unidades = new List<ViverMais.Model.EstabelecimentoSaude>();
                //IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

                //for (int pos = 0; pos < cnes.Length; pos++)
                //    unidades.Add(iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes[pos]));
                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();

                GridView_QuadroVagas.DataSource = unidades;//.OrderBy(p => p.NomeFantasia);
                GridView_QuadroVagas.DataBind();
                //DataList_Vagas.DataSource = les;
                //DataList_Vagas.DataBind();
            }
        }

        /// <summary>
        /// Formata o gridview de acordo com o padrão específicado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e) 
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string cnes = this.GridView_QuadroVagas.DataKeys[e.Row.RowIndex]["CNES"].ToString();
                //ViverMais.Model.EstabelecimentoSaude unidade = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes);

                //((Label)e.Row.Controls[0].Controls[0]).Text = unidade.NomeFantasia;
                GridView gv = (GridView)((Accordion)e.Row.FindControl("Accordion_Vagas")).Panes[0].FindControl("GridView_Vagas");
                DataTable dt = Factory.GetInstance<IVagaUrgencia>().QuadroVagas(cnes, true);
                gv.DataSource = dt;
                gv.DataBind();
            }
        }

        /// <summary>
        /// Padroniza o DataList de acordo com o número de vagas da unidade de saúde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnItemDataBound_FormataDataList(object sender, DataListItemEventArgs e) 
        //{
        //    GridView gv  = (GridView)e.Item.FindControl("GridView_Vagas");
        //    DataTable dt = Factory.GetInstance<IVagaUrgencia>().retornaQuadroVagasUnidade(DataList_Vagas.DataKeys[e.Item.ItemIndex].ToString(), true);
        //    gv.DataSource = dt;
        //    gv.DataBind();
        //}
    }
}