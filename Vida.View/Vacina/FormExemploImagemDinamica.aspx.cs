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
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.View.Vacina.RelatoriosCrystal;

namespace ViverMais.View.Vacina
{
    public partial class FormExemploImagemDinamica : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                DataTable imagem = ImagemDinamica(System.Web.HttpContext.Current.Request.MapPath("img/CartaoVacina/topo_padrao1.jpg"));

                ReportDocument doc = new ReportDocument();
                DSExemploImagemDinamica ds = new DSExemploImagemDinamica();
                ds.Tables.Add(imagem);
                doc.Load(Server.MapPath("RelatoriosCrystal/ExemloImagemDinamica.rpt"));
                doc.SetDataSource(ds.Tables[1]);

                CrystalReportViewer_ImagemDinamica.ReportSource = doc;
                CrystalReportViewer_ImagemDinamica.DataBind();
            //}
        }

        private DataTable ImagemDinamica(string pathimagem)
        {
            DataTable data = new DataTable();
            DataRow row;
            data.Columns.Add("nome", System.Type.GetType("System.String"));
            data.Columns.Add("imagem", System.Type.GetType("System.Byte[]"));
            FileStream fs = new FileStream(pathimagem, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            row = data.NewRow();
            row[0] = "Teste Imagem Dinamica";
            row[1] = br.ReadBytes((int)br.BaseStream.Length);
            data.Rows.Add(row);
            br = null;
            fs.Close();
            fs = null;
            return data;
        }
    }
}
