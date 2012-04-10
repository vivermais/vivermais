﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.DAO;
using ViverMais.Model;

namespace ViverMais.View.Urgencia
{
    public partial class Inc_PesquisarCid : System.Web.UI.UserControl
    {
        public ImageButton WUC_ImageButtonBuscarCID
        {
            get { return this.ImageButton_BuscarCodigoCID; }
        }

        public ImageButton WUC_ImageButtonBuscarCIDPorNome
        {
            get { return this.ImageButton_BuscarNomeCID; }
        }

        public TextBox WUC_TextBoxCodigoCID
        {
            get { return this.TextBox_CodigoCID; }
        }

        public TextBox WUC_TextBoxNomeCID
        {
            get { return this.TextBox_CID; }
        }

        public DropDownList WUC_DropDownListGrupoCID
        {
            get { return this.DropDownList_GrupoCID; }
        }

        public UpdatePanel WUC_UpdatePanelPesquisarCID
        {
            get { return this.UpdatePanel_PesquisarCID; }
        }

        public Panel WUC_PanelPesquisarCID
        {
            get { return this.Panel_PesquisarCID; }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            string validationcodigocid = this.UniqueID + "_PesquisarCodigoCID";
            this.ImageButton_BuscarCodigoCID.ValidationGroup = validationcodigocid;
            this.ValidationSummary_BuscarCodigoCid.ValidationGroup = validationcodigocid;
            this.RequiredFieldValidator_BuscarCodigoCid.ValidationGroup = validationcodigocid;

            string validationnomecid = this.UniqueID + "_PesquisarNomeCID";
            this.ImageButton_BuscarNomeCID.ValidationGroup = validationnomecid;
            this.ValidationSummary_BuscarNomeCid.ValidationGroup = validationnomecid;
            this.RequiredFieldValidator_BuscarNomeCid.ValidationGroup = validationnomecid;
            this.RegularExpressionValidator_BuscarNomeCid.ValidationGroup = validationnomecid;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IList<string> gruposcid = Factory.GetInstance<ICid>().ListarGrupos();

                DropDownList_GrupoCID.DataSource = gruposcid;
                DropDownList_GrupoCID.DataBind();

                DropDownList_GrupoCID.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }
        }
    }
}