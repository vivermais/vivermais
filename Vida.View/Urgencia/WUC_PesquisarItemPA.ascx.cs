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
    public partial class WUC_PesquisarItemPA : System.Web.UI.UserControl
    {
        public LinkButton WUC_LinkButtonPesquisarPorNome
        {
            get
            {
                return this.LinkButtonPesquisar;
            }
        }

        public IList<ItemPA> Itens
        {
            get
            {
                if (Session[string.Format("ItensPesquisados_{0}", this.UniqueID)] != null)
                    return (IList<ItemPA>)Session[string.Format("ItensPesquisados_{0}", this.UniqueID)];

                return new List<ItemPA>();
            }

            set
            {
                Session[string.Format("ItensPesquisados_{0}", this.UniqueID)] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnClick_BuscarPorNome(object sender, EventArgs e)
        {
            this.Itens = Factory.GetInstance<IItemPA>().BuscarItem<ItemPA>(this.TextBoxItem.Text);
        }
    }
}