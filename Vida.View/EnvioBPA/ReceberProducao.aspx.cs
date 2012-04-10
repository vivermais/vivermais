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
using System.Collections.Generic;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.EnvioBPA
{
    public partial class ReceberProducao : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                lblUsuario.Text = ((Usuario)Session["Usuario"]).Nome;
                var competencias = Factory.GetInstance<IEnviarBPA>().ListarCompetencias<CompetenciaBPA>(false).OrderBy(x=>x.Ano).ThenBy(x=>x.Mes);
                ddlCompetencias.Items.Add(new ListItem("Selecione", "-1"));
                foreach (var item in competencias)
                {
                    ddlCompetencias.Items.Add(new ListItem(item.Ano + "" + item.Mes.ToString("00"), item.Codigo.ToString()));
                }
            }
        }

        void AtualizarGrid() 
        {
            CompetenciaBPA competencia = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CompetenciaBPA>(int.Parse(ddlCompetencias.SelectedValue));
            IList<ProtocoloEnvioBPA> protocolos = Factory.GetInstance<IEnviarBPA>().ListarProtocolosPorCompetencia<ProtocoloEnvioBPA>(competencia);
            GridView1.DataSource = protocolos;
            GridView1.DataBind();
            Session["protocolos"] = protocolos;            
        }

        protected void imgBtnEnviar_Click(object sender, EventArgs e)
        {
            AtualizarGrid();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            IList<ProtocoloEnvioBPA> protocolos = (IList<ProtocoloEnvioBPA>)Session["protocolos"];
            ProtocoloEnvioBPA protocolo = protocolos[int.Parse(e.CommandArgument.ToString())];
            Factory.GetInstance<IViverMaisServiceFacade>().Deletar(protocolo);
            AtualizarGrid();
        }
    }
}