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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Collections.Generic;

namespace ViverMais.View.Vacina
{
    public partial class FormExibeSalaVacina : PageViverMais
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.InserirTrigger(this.WUC_PesquisarSala.WUC_LinkButtonPesquisarNome.UniqueID, "Click", this.UpdatePanelPesquisaSalaVacina);
            this.InserirTrigger(this.WUC_PesquisarSala.WUC_LinkButtonListarTodos.UniqueID, "Click", this.UpdatePanelPesquisaSalaVacina);

            this.WUC_PesquisarSala.WUC_LinkButtonPesquisarNome.Click += new EventHandler(OnClick_Pesquisar);
            this.WUC_PesquisarSala.WUC_LinkButtonListarTodos.Click += new EventHandler(OnClick_Pesquisar);

            if (!IsPostBack)
            {
                this.WUC_PesquisarSala.WUC_SalasPesquisadas = new List<SalaVacina>();

                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SALAVACINA", Modulo.VACINA))
                {
                    this.GridView_SalaVacina.Columns.RemoveAt(1);
                    BoundField bound = new BoundField();
                    bound.HeaderText = "Nome";
                    bound.DataField = "Nome";
                    bound.ItemStyle.Width = Unit.Pixel(300);
                    bound.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    this.GridView_SalaVacina.Columns.Insert(1, bound);
                    this.Lkn_Novo.Visible = false;
                }
            }
        }

        protected void OnClick_Pesquisar(object sender, EventArgs e)
        {
            this.GridView_SalaVacina.DataSource = this.WUC_PesquisarSala.WUC_SalasPesquisadas;
            this.GridView_SalaVacina.DataBind();
            this.Panel_ResultadoPequisa.Visible = true;
            this.UpdatePanelPesquisaSalaVacina.Update();
        }

        private void InserirTrigger(string idcontrole, string nomeevento, UpdatePanel updatepanel)
        {
            AsyncPostBackTrigger trigger = new AsyncPostBackTrigger();
            trigger.ControlID = idcontrole;
            trigger.EventName = nomeevento;
            updatepanel.Triggers.Add(trigger);
        }

        /// <summary>
        /// Paginação do gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnPageIndexChanging_Paginacao(object sender, GridViewPageEventArgs e)
        {
            GridView_SalaVacina.DataSource = this.WUC_PesquisarSala.WUC_SalasPesquisadas;
            GridView_SalaVacina.DataBind();

            GridView_SalaVacina.PageIndex = e.NewPageIndex;
            GridView_SalaVacina.DataBind();
        }
    }
}
