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

namespace ViverMais.View.Urgencia
{
    public partial class RelatorioFilaAcolhimentoUnidades : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string[] unidadesvalidas = { "2927400004774", "2927400004030", "2927400004405", "2927400004154", "2927400004340", "2927400003999", "2927400028460", "2927400028452", "2927400028568" };

                //string[] cnes = Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p => p.Codigo).ToArray();

                //IList<ViverMais.Model.EstabelecimentoSaude> unidades = new List<ViverMais.Model.EstabelecimentoSaude>();
                //IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

                //for (int pos = 0; pos < cnes.Length; pos++)
                //    unidades.Add(iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes[pos]));
                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();

                GridView_Acolhimento.DataSource = unidades;
                GridView_Acolhimento.DataBind();
                //DataList_Acolhimento.DataSource = les;
                //DataList_Acolhimento.DataBind();
            }
        }

        /// <summary>
        /// Padroniza o DataList de acordo com a fila de acolhimento das unidades de saúde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void OnItemDataBound_FormataDataList(object sender, DataListItemEventArgs e)
        //{
        //    Label lbQuantidade = (Label)e.Item.FindControl("lbQuantidadePaciente");
        //    lbQuantidade.Text = "Número de Pacientes: " + Factory.GetInstance<IProntuario>().buscaFilaAcompanhamento<ViverMais.Model.Prontuario>(DataList_Acolhimento.DataKeys[e.Item.ItemIndex].ToString()).Count().ToString();
        //}

        /// <summary>
        /// Formata o gridview de acordo com o padrão informado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnRowDataBound_FormataGridView(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lb = (Label)e.Row.FindControl("Label_QtdPaciente");
                lb.Text = Factory.GetInstance<IProntuario>().BuscarFilaAcompanhamento<ViverMais.Model.Prontuario>(GridView_Acolhimento.DataKeys[e.Row.RowIndex]["CNES"].ToString()).Count().ToString();
            }
        }
    }
}
