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
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.DAO;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormImportarFPO : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IMPORTAR_FPO",Modulo.AGENDAMENTO))
                {
                    btnVoltar.CausesValidation = false;
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
            }
                
        }

        protected void btnFazerUpload_Click(object sender, EventArgs e)
        {

            string filepath = Server.MapPath("~/Agendamento/docs/");
            // VERIFICA SE O USUÁRIO SELECIONOU ALGUM ARQUIVO
            if (FileUpload1.HasFile)
            {

                string[] tipo = FileUpload1.FileName.Split('.');
                if ((tipo[1] == "csv") || (tipo[1] == "CSV"))
                {
                    //SALVA O ARQUIVO COM O MESMO NOME, NA PASTA C:\
                    FileUpload1.SaveAs(filepath + FileUpload1.FileName);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Arquivo precisa ser do tipo CSV!'); window.location='FormImportarFPO.aspx'</script>");
                    return;
                }
            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Selecione um Arquivo');</script>");
                return;
            }
            string[] linhas = File.ReadAllLines(filepath + FileUpload1.FileName);
            string[] result = new string[linhas.Length];

            //Verifica se os dados estão corretos
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] valores = linhas[i].Split(';');
                //VERIFICA O TIPO
                if (valores[0] != "FPO")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Tipo inválido na Linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;
                }
                //VERIFICA SE A COMPETÊNCIA POSSUI 6 DÍGITOS
                if (valores[1].Length < 6)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Comptência deve ser do seguinte formato AAAAMM na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                // VERIFICA SE O ANO DA COMPETÊNCIA É MAIOR QUE 2008
                if (int.Parse(valores[1].Substring(0, 4)) < 2008)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Ano da Competência deve ser maior que 2009 na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;
                }
                // VERIFICA SE O MÊS ESTA ENTRE 01 E 12 
                if ((int.Parse(valores[1].Substring(4, 2)) < 01) || (int.Parse(valores[1].Substring(4, 2)) > 12))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O mês da competência inválido na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                //VERIFICA SE O CNES POSSUI 7 DÍGITOS
                if ((valores[2].Length < 7) || (valores[2].Length > 7))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O CNES da unidade deve ter 7 digitos na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                // VERIFICA SE EXISTE O CNES
                if (valores[2].Length == 7)
                {
                    IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                    ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(valores[2]);
                    if (estabelecimento == null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O CNES não encontrado na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                        return;

                    }

                }
                // VERIFICA SE O PROCEDIMENTO POSSUI 10 DIGITOS
                if ((valores[3].Length < 10) || (valores[3].Length > 10))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Código do Procedimento deve ter 10 digitos na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                if (valores[3].Length == 10)
                {
                    IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
                    ViverMais.Model.Procedimento procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(valores[3]);
                    if (procedimento == null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Código do Procedimento não existe na linha" + i + "');window.location='FormImportarFPO.aspx'</script>");
                        return;
                    }
                    else
                    {
                        //Verifica se o procedimento está parametrizado
                        TipoProcedimento tipoProcedimento = Factory.GetInstance<ITipoProcedimento>().BuscaTipoProcedimento<TipoProcedimento>(procedimento.Codigo);
                        if (tipoProcedimento == null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('O Procedimento não está Parametrizado. Entre em Contato IMEDIATAMENTE com a Regulação!');", true);
                            return;
                        }
                    }
                }
                if (valores[4] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Quantidade em Branco na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                if (valores[5] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Valor Total em Branco na linha " + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;

                }
                if (valores[6] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Nível de Apuração em Branco na linha" + i + "');window.location='FormImportarFPO.aspx'</script>");
                    return;
                }
            }

            //Código para Salvar no Banco            
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] campos = linhas[i].Split(';');
                IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                ViverMais.Model.FPO fpo = new FPO();
                fpo.Competencia = int.Parse(campos[1]);
                IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                fpo.Estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(campos[2]);
                
                fpo.Procedimento = iEstabelecimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(campos[3]); ;
                fpo.QTD_Total = int.Parse(campos[4]);
                fpo.ValorTotal = float.Parse(campos[5]);
                fpo.NivelApuracao = char.Parse(campos[6]);
                IFPO iFPO = Factory.GetInstance<IFPO>();
                ViverMais.Model.FPO fpo2 = iFPO.BuscarFPO<ViverMais.Model.FPO>(fpo.Estabelecimento.CNES, int.Parse(campos[1]), fpo.Procedimento.Codigo);
                if (fpo2 != null)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Já existe FPO cadastrado na linha" + i + "' );window.location='FormImportarFPO.aspx'</script>");
                    return;
                }
                iAgendamento.Salvar(fpo);
                iAgendamento.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 34, "CNES:" + fpo.Estabelecimento.CNES+" COMPETENCIA:"+ fpo.Competencia+" ID_PROCED:"+ fpo.Procedimento.Codigo));
            }
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Importação do FPO realizada com Sucesso' );window.location='FormImportarFPO.aspx'</script>");


            //Deleta o arquivo
            FileInfo file = new FileInfo(Server.MapPath("~/Agendamento/docs/" + FileUpload1.FileName));
            file.Delete();
            return;
        }
    }
}
