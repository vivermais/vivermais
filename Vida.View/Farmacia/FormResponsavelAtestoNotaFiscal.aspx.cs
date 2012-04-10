﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.View.Farmacia
{
    public partial class FormResponsavelAtestoNotaFiscal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int temp;

                    if (Request["co_responsavel"] != null && int.TryParse(Request["co_responsavel"].ToString(), out temp)) 
                    {
                        ResponsavelAtesto ra = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ResponsavelAtesto>(int.Parse(Request["co_responsavel"].ToString()));
                        TextBox_Nome.Text = ra.Nome;
                    }
                }
                catch (Exception f)
                {
                    throw f;
                }
            }
        }

        /// <summary>
        /// Salva o responsável pelo atesto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try 
            {
                ResponsavelAtesto ra = Request["co_responsavel"] != null ? Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ResponsavelAtesto>(int.Parse(Request["co_responsavel"].ToString())) : new ResponsavelAtesto();
                ra.Nome = TextBox_Nome.Text;

                Factory.GetInstance<IFarmaciaServiceFacade>().Salvar(ra);
                //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Responsável salvo com sucesso!');parent.parent.GB_hide();", true);
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Responsável salvo com sucesso!');location='Default.aspx';", true);
            }catch(Exception f)
            {
                throw f;
            }
        }
    }
}
