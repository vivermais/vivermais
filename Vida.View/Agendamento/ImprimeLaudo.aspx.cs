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
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.Model;
using ViverMais.DAO;
using SpiceLogic.BLOBControl;

namespace ViverMais.View.Agendamento
{
    public partial class ImprimeLaudo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Busca endereco da Imagem
                int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
                IList<ViverMais.Model.Laudo> laudos = Factory.GetInstance<ISolicitacao>().BuscaLaudos<ViverMais.Model.Laudo>(id_solicitacao);

                if (laudos.Count != 0)
                {
                    foreach (Laudo laudo in laudos)
                    {
                        Image img = new Image();
                        if (laudo.Imagem == null)
                        {
                            img.ImageUrl = "~/Agendamento/laudos/" + laudo.Endereco;
                            img.DataBind();
                            this.Page.Controls.Add(img);
                            this.Page.DataBind();
                        }
                        else
                        {
                            BlobImage blobImg = new BlobImage();
                            //SpiceLogic.BLOBControl.Core.ThumbnailSettings settings = new SpiceLogic.BLOBControl.Core.ThumbnailSettings();
                            //settings.KeepAspectRatio = true;
                            blobImg.ThumbnailDisplay.KeepAspectRatio = true;
                            blobImg.BlobData = laudo.Imagem;
                            blobImg.MimeType = "image/jpg";
                            blobImg.DataBind();
                            img = (Image)blobImg;
                            this.Page.Controls.Add(img);
                            this.Page.DataBind();
                            //blobImg.ImageUrl
                        }
                    }
                    Response.Write("<script>window.print();</script>");
                }
                else
                {
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                }
                
            }
        }
    }
}
