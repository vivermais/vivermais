﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormSetor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_SETOR", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);

                int temp;

                if (Request["co_setor"] != null && int.TryParse(Request["co_setor"].ToString(), out temp))
                {
                    try
                    {
                        ViverMais.Model.Setor s = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<ViverMais.Model.Setor>(int.Parse(Request["co_setor"].ToString()));
                        TextBox_Setor.Text = s.Nome;

                        imgsalvar.Alt = "Alterar";
                        imgsalvar.Src = "img/btn/alterar1.png";
                        imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/alterar2.png';");
                        imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/alterar1.png';");
                    }
                    catch (Exception f)
                    {
                        throw f;
                    }
                }
                else
                {
                    imgsalvar.Alt = "Salvar";
                    imgsalvar.Src = "img/btn/salvar1.png";
                    imgsalvar.Attributes.Add("onmouseover", "this.src='img/btn/salvar2.png';");
                    imgsalvar.Attributes.Add("onmouseout", "this.src='img/btn/salvar1.png';");
                }
            }
        }

        /// <summary>
        /// Salva o setor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e)
        {
            try
            {
                IFarmaciaServiceFacade iFarmacia = Factory.GetInstance<IFarmaciaServiceFacade>();
                ViverMais.Model.Setor setor = Request["co_setor"] != null ? iFarmacia.BuscarPorCodigo<ViverMais.Model.Setor>(int.Parse(Request["co_setor"].ToString())) : new ViverMais.Model.Setor();
                setor.Nome = TextBox_Setor.Text;

                iFarmacia.Salvar(setor);
                iFarmacia.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_SETOR, "id setor:" + setor.Codigo));

                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Setor salvo com sucesso!');location='FormBuscaSetor.aspx';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('"+ex.Message+"');", true);
            }
        }
    }
}
