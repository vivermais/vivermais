using System;
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
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Collections.Generic;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.View.Agendamento.Helpers;

namespace ViverMais.View.Agendamento
{
    public partial class ExibeLaudo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Busca endereco da Imagem
                int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
                //List<string> enderecos = new List<string>();
                
                IList<ViverMais.Model.Laudo> laudos = Factory.GetInstance<ISolicitacao>().BuscaLaudos<ViverMais.Model.Laudo>(id_solicitacao);
                if (laudos.Count != 0)
                {
                    lblSemLaudo.Visible = false;
                    Session["LaudosSolicitacao"] = laudos;
                    Session["indexLaudo"] = 0;
                    if (laudos[0].Imagem != null)
                    {
                        BlobImage1.BlobData = laudos[0].Imagem;
                        BlobImage1.MimeType = "image/jpg";
                        BlobImage1.DataBind();
                        Image1.Visible = false;
                    }
                    else
                    {
                        Image1.ImageUrl = "laudos/" + laudos[0].Endereco;
                        Image1.Visible = true;
                        Image1.DataBind();
                        BlobImage1.Visible = false;
                    }
                    if (laudos.Count == 1)
                    {
                        lknProximo.Enabled = false;
                        lknAnterior.Enabled = false;
                    }
                }
                else
                {
                    lknAnterior.Enabled = false;
                    lknProximo.Enabled = false;
                    lblSemLaudo.Visible = true;
                }
                ///IList<ViverMais.Model.Laudo> laudo = iSolicitacao.BuscaLaudos<ViverMais.Model.Laudo>(id_solicitacao);
                //DataTable table = new DataTable();
                //DataColumn c0 = new DataColumn("Endereco");
                //table.Columns.Add(c0);
                //if (laudo.Count != 0)
                //{
                //    lblSemLaudo.Visible = false;
                //    foreach (Laudo ag in laudo)
                //    {
                //        enderecos.Add(ag.Endereco);
                //    }
                //    if (laudo.Count == 1)//Se Existir somente um Laudo
                //    {
                //        lknAnterior.Enabled = false;
                //        lknProximo.Enabled = false;
                //        Image1.ImageUrl = "laudos/" + enderecos[0];
                //        Image1.DataBind();
                //        Image1.Visible = true;
                //    }
                //    else
                //    {
                //        Session["QtdEndereco"] = enderecos.Count();
                //        Session["atual"] = 0;
                //        string endereco = enderecos[0];//Pega o Primeiro Caminho para Mostrar
                //        lknAnterior.Enabled = false;
                //        Session["enderecos"] = enderecos; //Salva na Sessão Os endereços dos Laudos
                //        Image1.ImageUrl = "laudos/" + endereco;
                //        Image1.DataBind();
                //        Image1.Visible = true;
                //    }
                //}
                //else
                //{
                //    lblSemLaudo.Visible = true;
                //    Image1.Visible = false;
                //    lknAnterior.Enabled = false;
                //    lknProximo.Enabled = false;
                //}
            }
        }

        protected void lknProximo_Click(object sender, EventArgs e)
        {
            lknAnterior.Enabled = true;
            IList<ViverMais.Model.Laudo> laudos = (IList<ViverMais.Model.Laudo>)Session["LaudosSolicitacao"];
            int indiceLaudo = int.Parse(Session["indexLaudo"].ToString());

            // Busca a Imagem Proxima, se tiver
            if (indiceLaudo <= laudos.Count - 1)
            {
                indiceLaudo++;
                if (laudos[indiceLaudo].Imagem != null)
                {
                    BlobImage1.BlobData = laudos[indiceLaudo].Imagem;
                    BlobImage1.MimeType = "image/jpg";
                    BlobImage1.DataBind();
                    Image1.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "laudos/" + laudos[indiceLaudo].Endereco;
                    Image1.Visible = true;
                    Image1.DataBind();
                    BlobImage1.Visible = false;
                }
            }
            if ((indiceLaudo + 1) == laudos.Count)
            {
                lknProximo.Enabled = false;
            }
            Session["indexLaudo"] = indiceLaudo;
            //lknAnterior.Enabled = true;

            //// Busca a Imagem Proxima, se tiver
            //int qtdEndereco = int.Parse(Session["QtdEndereco"].ToString());
            //int i = 0;
            //int atual = int.Parse(Session["atual"].ToString());
            //List<string> enderecos = (List<string>)Session["enderecos"];
            //string endereco = "";
            //if ((atual + 1) < qtdEndereco)// Se a página atual Não for o último laudo, ele incrementa e pega o próximo endereço
            //{
            //    i = atual + 1;
            //    endereco = enderecos[i];
            //}
            //if ((i + 1) == qtdEndereco)//Se for o ultimo Laudo, ele desabilita o botão Próximo
            //{
            //    lknProximo.Enabled = false;
            //}
            //Session["atual"] = i;
            //Image1.ImageUrl = "laudos/" + enderecos[i];
            //Image1.DataBind();
        }

        protected void lknAnterior_Click(object sender, EventArgs e)
        {
            // Busca a Imagem anterior
            lknProximo.Enabled = true;
            IList<ViverMais.Model.Laudo> laudos = (IList<ViverMais.Model.Laudo>)Session["LaudosSolicitacao"];
            int indiceLaudo = int.Parse(Session["indexLaudo"].ToString());

            // Busca a Imagem Anterior, se tiver
            if (indiceLaudo > 0)
            {
                indiceLaudo--;
                if (laudos[indiceLaudo].Imagem != null)
                {
                    BlobImage1.BlobData = laudos[indiceLaudo].Imagem;
                    BlobImage1.MimeType = "image/jpg";
                    BlobImage1.DataBind();
                    Image1.Visible = false;
                }
                else
                {
                    Image1.ImageUrl = "laudos/" + laudos[indiceLaudo].Endereco;
                    Image1.Visible = true;
                    Image1.DataBind();
                    BlobImage1.Visible = false;
                }
            }
            if (indiceLaudo == 0)
            {
                lknAnterior.Enabled = false;
            }
            Session["indexLaudo"] = indiceLaudo;
            
            //// Busca a Imagem anterior
            //lknProximo.Enabled = true;
            //int qtdEndereco = int.Parse(Session["QtdEndereco"].ToString());
            //int i = 0;
            //int atual = int.Parse(Session["atual"].ToString());
            //List<string> enderecos = (List<string>)Session["enderecos"];
            //string endereco = "";
            //if (atual > 0) // Se a imagem mostrada não for a primeira da lista, ele busca a imagem anterior
            //{
            //    i = atual - 1;
            //    endereco = enderecos[i];
            //}
            //if ((i) == 0) //Se for a primeira, ele desabilita o botão Anterior
            //{
            //    lknAnterior.Enabled = false;
            //}
            //Session["atual"] = i;
            //Image1.ImageUrl = "laudos/" + endereco;
            //Image1.DataBind();
        }

        protected void lknImprimir_Click(object sender, EventArgs e)
        {
            int id_solicitacao = int.Parse(Request.QueryString["id_solicitacao"]);
            Redirector.Redirect("ImprimeLaudo.aspx?id_solicitacao=" + id_solicitacao, "_blank", "");
        }


    }
}
