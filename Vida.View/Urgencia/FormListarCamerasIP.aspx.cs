﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using AjaxControlToolkit;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormListarCamerasIP : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //string[] cnes = Factory.GetInstance<IRelatorioUrgencia>().RetornaPASAtivos<PASAtivos>().Select(p => p.Codigo).ToArray();

                IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IRelatorioUrgencia>().EstabelecimentosAtivos<ViverMais.Model.EstabelecimentoSaude>();
                //IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

                //for (int pos = 0; pos < cnes.Length; pos++)
                //    unidades.Add(iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(cnes[pos]));

                GridView_Unidades.DataSource = unidades; //.OrderBy(p=>p.NomeFantasia);
                GridView_Unidades.DataBind();
            }
        }

        protected void OnRowDataBound_FormataGridViewUnidades(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Accordion a = (Accordion)e.Row.FindControl("Accordion_Cameras");
                GridView gv = (GridView)a.Panes[0].FindControl("GridView_Cameras");

                gv.DataSource = Factory.GetInstance<ICameraIP>().BuscarPorUnidade<CameraIP>(GridView_Unidades.DataKeys[e.Row.RowIndex]["CNES"].ToString()).OrderBy(p => p.Local).ToList();
                gv.DataBind();
            }
        }

        protected void OnRowDataBound_FormataGridViewCameras(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton linkCamera = (LinkButton)e.Row.FindControl("LinkButton_Ver");
                linkCamera.Attributes.Add("onclick", "javascript:GB_showFullScreen('Câmera','" + linkCamera.CommandArgument.ToString() + "');");
            }
        }
    }
}
