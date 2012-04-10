﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Threading;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.EstabelecimentoSaude
{
    public partial class FormImportarEstabelecimentoSaude : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IMPORTAR_CNES"))
                    Response.Redirect("FormAcessoNegado.aspx?opcao=1");
                    //ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
        }

        protected void OnClick_Importar(object sender, EventArgs e)
        {
            if (FileUpload_Estabelecimento.HasFile)
            {
                if (FileUpload_Estabelecimento.PostedFile.ContentLength <= (1048576 * 50))
                {
                    MemoryStream memo = new MemoryStream(FileUpload_Estabelecimento.FileBytes);
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.Load(memo);
                    }
                    catch (Exception f)
                    {
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Não foi possível ler o arquivo. Arquivo XML inválido.');", true);
                        return;
                    }
                    //FileInfo fi = new FileInfo(FileUpload_Estabelecimento.FileName);
                    
                    try
                    {
                        ImportacaoCNES importacao = new ImportacaoCNES();
                        importacao.Status = Convert.ToChar(ImportacaoCNES.DescricaoStatus.Inicializada);
                        importacao.Usuario = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Seguranca.IUsuario>().BuscarPorCodigo<ViverMais.Model.Usuario>(((Usuario)Session["Usuario"]).Codigo);
                        importacao.Arquivo = FileUpload_Estabelecimento.FileName;
                        importacao.TamanhoArquivo = memo.Length.ToString();
                        //fi.Length.ToString();
                        importacao.HorarioInicio = DateTime.Now;

                        //Factory.GetInstance<IViverMaisServiceFacade>().Inserir(importacao);

                        StartBackgroundThread(delegate { this.ImportarEstabelecimento(doc, importacao); });
                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Acompanhe o status da importação pelo quadro de importações realizadas até o momento.');location='FormImportacoesRealizadas.aspx';", true);
                    }
                    catch (Exception f)
                    {
                        throw f;
                    }
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O arquivo deve ter no máximo 50 MB.');", true);
            }
            else
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Informe um arquivo no formato XML.');", true);
        }

        public static void StartBackgroundThread(ThreadStart threadStart)
        {
            if (threadStart != null)
            {
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void ImportarEstabelecimento(XmlDocument doc, ImportacaoCNES importacao)
        {
            Factory.GetInstance<IEstabelecimentoSaude>().ImportarEstabelecimento<XmlDocument, ImportacaoCNES>(doc, importacao);
        }
    }
}