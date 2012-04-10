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
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Laboratorio;
using System.Text;
using System.Net;
using System.IO;

namespace ViverMais.View.Laboratorio
{
    public partial class FormPesquisaLaudo : System.Web.UI.Page
    {
        static string ftpServerIP = "200.223.243.150";
        static string usuario = "laudo";
        static string senha = "iL4uD!s4$";

        static string ftpServerIPHomologa = "172.22.6.2";
        static string usuarioHomologa = "transfer";
        static string senhaHomologa = "tR@n$F3r@TUr";

        protected void Page_Load(object sender, EventArgs e)
        {
            GridView_Laudos.RowCommand += new GridViewCommandEventHandler(GridView_Laudos_RowCommand);
            if (!Page.IsPostBack)
            {
                if (Session["Lab_CartaoSUS"] != null)
                {
                    string cartaoSUS = Session["Lab_CartaoSUS"].ToString();
                    tbxCartaoSus.Text = cartaoSUS;
                    listarArquivos(cartaoSUS);

                }
            }
        }

        void GridView_Laudos_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int indice = int.Parse(e.CommandArgument.ToString());

            string uri = "ftp://" + ftpServerIP + "/" + GridView_Laudos.DataKeys[indice][0].ToString();
            FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));

            reqFTP.Credentials = new NetworkCredential(usuario,
                                           senha);

            reqFTP.UseBinary = true;
            reqFTP.Proxy = null;
            reqFTP.UsePassive = false;

            MemoryStream memoryStream = new MemoryStream();
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;

            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            Stream responseStream = response.GetResponseStream();

            int Length = 2048;
            Byte[] buffer = new Byte[Length];
            int bytesRead = responseStream.Read(buffer, 0, Length);
            while (bytesRead > 0)
            {
                memoryStream.Write(buffer, 0, bytesRead);
                bytesRead = responseStream.Read(buffer, 0, Length);
            }

            Session["memoryStreamLaudo"] = memoryStream;

            HelperRedirector.Redirect("FormVisualizaLaudo.aspx", "_blank", null);
        }

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            PanelGridView.Visible = true;
            lblNoRegisters.Visible = false;

            String[] arquivos = listarArquivos(tbxCartaoSus.Text);

            DataTable tableArquivos = new DataTable();
            tableArquivos.Columns.Add("NomeArquivo");
            DataRow row = tableArquivos.NewRow();

            if (arquivos != null)
            {
                for (int x = 0; x < arquivos.Length; x++)
                {
                    row = tableArquivos.NewRow();
                    row["NomeArquivo"] = arquivos[x];
                    tableArquivos.Rows.Add(row);
                }

                GridView_Laudos.DataSource = tableArquivos;
                GridView_Laudos.DataBind();
            }
            else
                lblNoRegisters.Visible = true;
        }

        protected string[] listarArquivos(string cartaoSUS)
        {

            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            try
            {
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(
                          "ftp://" + ftpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(usuario,
                                                           senha);

                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (line.Contains(cartaoSUS))
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                downloadFiles = null;
                return downloadFiles;
            }
        }
    }
}