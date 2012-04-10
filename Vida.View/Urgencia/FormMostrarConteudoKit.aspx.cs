﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Urgencia
{
    public partial class FormMostrarConteudoKit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int temp;
                if (Request["co_kit"] != null && int.TryParse(Request["co_kit"].ToString(), out temp))
                {
                    try
                    {
                        IKitPA iKit = Factory.GetInstance<IKitPA>();
                        IList<KitMedicamentoPA> medicamentos = iKit.BuscarMedicamentoPA<KitMedicamentoPA>(temp).OrderBy(pt => pt.Medicamento.Nome).ToList();
                        Label_Kit.Text = medicamentos[0].KitPA.Nome;

                        string conteudo = string.Empty;
                        conteudo = "<ul>";
                        foreach (KitMedicamentoPA medicamento in medicamentos)
                            conteudo += "<li>" + medicamento.Medicamento.Nome + "</li>";

                        conteudo += "</ul>";
                        div_medicamentos.InnerHtml = conteudo;

                        IList<KitItemPA> itens = iKit.BuscarItemPA<KitItemPA>(temp).OrderBy(pt => pt.ItemPA.Nome).ToList();

                        conteudo = string.Empty;
                        conteudo = "<ul>";
                        foreach (KitItemPA item in itens)
                            conteudo += "<li>" + item.ItemPA.Nome + "</li>";

                        conteudo += "</ul>";
                        div_itens.InnerHtml = conteudo;
                    }
                    catch (Exception f)
                    {
                        throw f;
                    }
                }
            }
        }
    }
}
