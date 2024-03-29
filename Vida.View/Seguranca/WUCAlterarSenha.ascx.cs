﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ViverMais.View.Seguranca
{
    public partial class WUCAlterarSenha : System.Web.UI.UserControl
    {
        public TextBox SenhaAtual
        {
            get { return this.tbxSenhaAtual; }
        }

        public TextBox NovaSenha 
        {
            get { return this.tbxSenhaNova; }
        }

        public LinkButton SalvarAlteracao
        {
            get { return this.LnkbtnSalvar; }
        }

        public LinkButton VoltarPagina
        {
            get { return this.LinkButtonVoltar; }
        }

        public System.Web.UI.HtmlControls.HtmlImage ImgVoltar 
        {
            get { return imgvoltar; }
        }

        public System.Web.UI.HtmlControls.HtmlImage ImgSalvar
        {
            get { return imgsalvar; }
        }

        public void SetSenhaAtual(String senha)
        {
            this.tbxSenhaAtual.Attributes.Add("value",senha);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
    }
}