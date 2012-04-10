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
using System.Globalization;
using System.IO;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.BPA;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EnvioBPA
{
    public partial class FormEnviarBPA : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            if (!iseguranca.VerificarPermissao(((ViverMais.Model.Usuario)Session["Usuario"]).Codigo, "ENVIAR_BPA", Modulo.ENVIO_BPA))
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Você não tem permissão para acessar essa página. Em caso de dúViverMais, entre em contato.');window.location='../Home.aspx';</script>");
            }

            if (!IsPostBack) 
            {
                ViverMais.Model.Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                lblUsuario.Text = usuario.Login;
                lblCNES.Text = usuario.Unidade.CNES;
                //IList<CompetenciaBPA> competencias = Factory.GetInstance<IEnviarBPA>().ListarCompetencias<CompetenciaBPA>(true);
                var competencias = Factory.GetInstance<IEnviarBPA>().ListarCompetencias<CompetenciaBPA>(true).OrderBy(x => x.Ano).ThenBy(x => x.Mes);
                
                if (competencias.Count() == 0) 
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Não há período para envio de dados disponível');window.location='Default.aspx';</script>");
                }

                foreach (CompetenciaBPA comp in competencias)
                {
                    ddlCompetencia.Items.Add(new ListItem(comp.ToString(), comp.Codigo.ToString()));
                }
            }
        }

        protected void imgbtnEnviar_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                CompetenciaBPA competencia = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<CompetenciaBPA>(int.Parse(ddlCompetencia.SelectedValue));
                //A extensão do arquivo deve ser igual ao mês da competência
                if (FileUpload1.FileName.Split('.')[1].ToUpper() != DateTime.ParseExact(competencia.ToString(), "yyyyMM", CultureInfo.InvariantCulture).ToString("MMM").ToUpper())
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A extensão do arquivo enviado não confere com a competência');</script>");
                    return;
                }
                ViverMais.Model.Usuario usuario = (ViverMais.Model.Usuario)Session["Usuario"];
                
                IList<ProtocoloEnvioBPA> protocolos = Factory.GetInstance<IEnviarBPA>().ListarProtocolosPorEstabelecimento<ProtocoloEnvioBPA>(usuario.Unidade);
                var result = from p in protocolos where p.Competencia.ToString() == competencia.ToString() select p;
                foreach (var item in result)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Arquivo referente a essa competência já foi enviado. Em caso de dúViverMais, consulte o relatório de envio de produção ou entre em contato.');</script>");
                    return;
                }

                string filepath = string.Empty;
                try
                {
                    filepath = Server.MapPath("/EnvioBPA/") + "upload\\" + competencia.Ano + "\\" + FileUpload1.FileName;
                    FileUpload1.SaveAs(filepath);
                    FileInfo info = new FileInfo(filepath);
                    StreamReader reader = info.OpenText();
                    string linha = string.Empty;
                    //cabeçalho
                    linha = reader.ReadLine();
                    if (linha.Substring(5, 6) != competencia.Ano + "" + competencia.Mes.ToString("00")) 
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Há um erro no cabeçalho do arquivo. Em caso de dúViverMais, consulte o relatório de envio de produção ou entre em contato.');</script>");
                        return;                        
                    }
                    while(!reader.EndOfStream)
                    {
                        linha = reader.ReadLine();
                        int lenght = linha.Length;
                        if (!(linha.Substring(0, 7) == usuario.Unidade.CNES) && 
                            !(linha.Substring(7, 6) == ddlCompetencia.SelectedValue))
                        {
                            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Há um erro no formato do arquivo. A competência está errada ou a Unidade não confere. Verifique se a formatação está de acordo com o padrão. Qualquer dúViverMais entre em contato.');</script>");
                            return;
                        }
                    }
                    reader.Close();
                    ProtocoloEnvioBPA protocolo = new ProtocoloEnvioBPA();
                    protocolo.Usuario = usuario;
                    protocolo.EstabelecimentoSaude = usuario.Unidade;
                    protocolo.DataEnvio = DateTime.Now;
                    protocolo.Arquivo = info.Name;
                    protocolo.TamanhoArquivo = (int)info.Length; //bytes
                    protocolo.Competencia = competencia;//ddlCompetencia.SelectedValue;
                    protocolo.NumeroControle = tbxNumeroControle.Text;
                    Factory.GetInstance<IViverMaisServiceFacade>().Inserir(protocolo);
                    Session["protocolo"] = protocolo;
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Arquivo enviado com sucesso.');window.location='ExibeProtocoloEnvio.aspx';</script>");
                }
                catch (Exception ex)
                {
                    new FileInfo(filepath).Delete();
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Houve um erro no envio do seu arquivo.Verifique se os dados estão corretos e tente novamente.');</script>");
                }
            }
        }
    }
}
