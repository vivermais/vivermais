using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ViverMais.Model;
using System.Collections.Generic;

namespace ViverMais.View
{
    public partial class WUCExibeDadosMedicos : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (datasorce != null)
            {
                gridCid.DataSource = datasorce;
                gridCid.DataBind();
            }
        }
        public Label Observacao {
            get 
            {
                return lblAvaliacaoMedica;
            }
            set 
            {
                lblAvaliacaoMedica = value;
            }
        }
        public GridView Cids {
            get
            {
                return gridCid;
            }
            set
            {
                gridCid = value;
                this.Controls.Add(value);
            }
        }

        object datasorce;

        public object DataSource {
            get
            {
                return datasorce;
            }
            set
            {
                datasorce = value;
            }
        }
    }
}