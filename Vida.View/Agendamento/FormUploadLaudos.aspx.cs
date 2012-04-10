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
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Text.RegularExpressions;

namespace ViverMais.View.Agendamento
{
    public partial class FormUploadLaudos : System.Web.UI.Page
    {
        protected void CarregaLaudos()
        {
            ArrayList laudos = (ArrayList)Session["Laudos"];
            string fileSelected = HiddenNomeAnexo.Value;
            listBox1.Items.Clear();
            for (int i = 0; i < laudos.Count; i++)
            {
                UploadedFile uploadedFile = (UploadedFile)laudos[i];
                if (listBox1.Items.FindByText(uploadedFile.FileName) == null)
                    listBox1.Items.Add(new ListItem(uploadedFile.FileName, uploadedFile.FileName));
            }
        }

        protected void CarregaLaudos(bool removeItem)
        {
            ArrayList laudos = (ArrayList)Session["Laudos"];
            string fileSelected = HiddenNomeAnexo.Value;
            int indexRemove = -1;
            if (!fileSelected.Equals(String.Empty))
            {
                listBox1.Items.Clear();
                for (int i = 0; i < laudos.Count; i++)
                {
                    UploadedFile uploadedFile = (UploadedFile)laudos[i];
                    if (fileSelected.Equals(uploadedFile.FileName))
                        indexRemove = i;
                    else
                    {
                        if (listBox1.Items.FindByText(uploadedFile.FileName) == null)
                            listBox1.Items.Add(new ListItem(uploadedFile.FileName, uploadedFile.FileName));
                    }
                }
                if(indexRemove != -1)
                 laudos.RemoveAt(indexRemove);
            }
            HiddenNomeAnexo.Value = String.Empty;
            Session["Laudos"] = laudos;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Laudos"] == null)
                    Session["Laudos"] = new ArrayList();
                else
                    CarregaLaudos();
            }
            else
            {
                CarregaLaudos(true);
            }
        }

        protected void btnFinalizaSolicitacao_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "script", "parent.parent.GB_hide();", true);
        }


        protected void btnRemoverArquivo_OnClick(object sender, EventArgs e)
        {
            //ArrayList laudos = (ArrayList)Session["Laudos"];
            //string fileSelected = HiddenNomeAnexo.Value;
            ////listBox1.SelectedValue;
            //for (int i = 0; i < laudos.Count; i++)//Pra cada arquivo de laudo
            //{
            //    UploadedFile uploadedFile = (UploadedFile)laudos[i];
            //    if (fileSelected.Equals(uploadedFile.FileName))
            //        laudos.RemoveAt(i);
            //}
            //Session["Laudos"] = laudos;
            //OnClientClick="javascript:(RemoveItem(listBox1));"
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "RemoveItem(listBox1); return false;", true);
        }

        protected string SavePostedFiles(UploadedFile uploadedFile)
        {
            ArrayList laudos = (ArrayList)Session["Laudos"];

            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "document.getElementById", true);
            FileInfo fileInfo = new FileInfo(uploadedFile.FileName);
            string ret = String.Empty;

            laudos.Add(uploadedFile);
            Session["Laudos"] = laudos;

            ret = string.Format("{0}|", uploadedFile.FileName);




            //if(fileInfo.Is)


            //if (uploadedFile.IsValid)
            //{
            //    for (int i = 0; i < laudos.Count; i++)
            //    {
            //        UploadedFile uploaded = (UploadedFile)laudos[i];
            //        if (!uploadedFile.FileName.Equals(uploaded.FileName))
            //        {
            //            laudos.Add(uploadedFile);

            //        }
            //    }

            //listBox1.Items.Add(new ListItem(uploadedFile.FileName, uploadedFile.FileName));
            //listBox1.DataBind();


            //string resFileName = MapPath("~/Agendamento/laudos/") + fileInfo.Name;
            //uploadedFile.SaveAs(resFileName);

            //string fileLabel = fileInfo.Name;
            //string fileType = uploadedFile.PostedFile.ContentType.ToString();
            //string fileLength = uploadedFile.PostedFile.ContentLength / 1024 + "K";


            //ret = string.Format("{0} | <i>({1})</i> {2}|{3}",
            //    fileLabel, fileType, fileLength, fileInfo.Name);
            //}
            return ret;
        }

        protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            try
            {

                //if (new Regex(@"[^?:\/*""<>|]%").IsMatch(e.UploadedFile.FileName))
                //".IsMatchPath.GetInvalidFileNameChars()) == -1)
                //if (e.UploadedFile.FileName.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) == -1)
                //{
                e.CallbackData = SavePostedFiles(e.UploadedFile);
                //}
            }
            catch (Exception ex)
            {
                e.IsValid = false;
                e.ErrorText = ex.Message;
            }
        }
    }
}

