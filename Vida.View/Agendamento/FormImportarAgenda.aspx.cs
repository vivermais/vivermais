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
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Agendamento
{
    public partial class FormImportarAgenda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "IMPORTAR_AGENDA",Modulo.AGENDAMENTO))
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
        }

        protected void btnEnviarArquivo_Click(object sender, EventArgs e)
        {
            // VERIFICA SE SELECIONOU UM ARQUIVO
            if (FileUpload1.HasFile)
            {
                string[] tipo = FileUpload1.FileName.Split('.');
                //VERIFICA SE O ARQUIVO POSSUI A EXTENSÃO CORRETA
                if ((tipo[1] == "csv") || (tipo[1] == "CSV"))
                {
                    //SALVA O ARQUIVO COM O MESMO NOME, NA UNIDADE C:\
                    FileUpload1.SaveAs(Server.MapPath("/Agendamento/docs/") + FileUpload1.FileName);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Arquivo precisa ser do tipo CSV!'); window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }

            }
            string[] linhas = File.ReadAllLines(Server.MapPath("/Agendamento/docs/") + FileUpload1.FileName);
            string[] result = new string[linhas.Length];

            //Verifica se os dados estão corretos
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] valores = linhas[i].Split(';');
                IProfissional iProfissional = Factory.GetInstance<IProfissional>();
                //VERIFICA O TIPO
                if (valores[0] != "AGENDA")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Tipo inválido na Linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE A COMPETÊNCIA POSSUI 6 DÍGITOS
                if (valores[1].Length < 6)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Comptência deve ser do seguinte formato AAAAMM na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }
                // VERIFICA SE O ANO DA COMPETÊNCIA É MAIOR QUE 2008
                if (int.Parse(valores[1].Substring(0, 4)) < 2008)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Ano da Competência deve ser maior que 2009 na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                // VERIFICA SE O MÊS ESTA ENTRE 01 E 12 
                if ((int.Parse(valores[1].Substring(4, 2)) < 01) || (int.Parse(valores[1].Substring(4, 2)) > 12))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O mês da competência inválido na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }
                //VERIFICA SE O CNES POSSUI 7 DÍGITOS
                if ((valores[2].Length < 7) || (valores[2].Length > 7))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O CNES da unidade deve ter 7 digitos na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }
                // VERIFICA SE EXISTE O CNES
                if (valores[2].Length == 7)
                {
                    IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                    ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(valores[2]);
                    if (estabelecimento == null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O CNES não encontrado na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }

                    //Verifica se o estabelecimento não está afastado no Período da Agenda Importada
                    AfastamentoEAS afastamento = Factory.GetInstance<IAfastamentoEAS>().VerificaAfastamentosNaData<AfastamentoEAS>(valores[2], DateTime.Parse(valores[4]));
                    if (afastamento != null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Existe Afastamento da Unidade Para a Data Informada na Agenda!');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }
                }
                // VERIFICA SE O PROCEDIMENTO POSSUI 10 DIGITOS
                if ((valores[3].Length < 10) || (valores[3].Length > 10))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Código do Procedimento deve ter 10 digitos na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }
                //VERIFICA SE O PROCEDIMENTO EXISTE
                if (valores[3].Length == 10)
                {
                    IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
                    ViverMais.Model.Procedimento procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(valores[3]);
                    if (procedimento == null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('O Código do Procedimento não existe na linha" + i + "');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }
                }
                // VERIFICA SE A DATA ESTA EM BRANCO
                if (valores[4] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data em Branco na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }

                //VERIFICA SE A DATA INFORMADA É UM FERIADO
                if (Factory.GetInstance<IFeriado>().VerificaData(DateTime.Parse(valores[4].ToString())))
                {
                    //Verifico se o estabelecimento é Tolerante à feriado
                    if (!Factory.GetInstance<IUnidade>().VerificaEstabelecimentoToleranteFeriado(valores[2].ToString()) != true)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('A Data da Agenda é um Feriado. E o estabelecimento informado não é Tolerante à Feriado!');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }
                }
                //VERIFICA SE A DATA É VÁLIDA
                DateTime dt;
                if (!DateTime.TryParse(valores[4], out dt))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Data Inválida na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                // VERIFICA A HORA INICIAL É MENOR QUE 5
                if (valores[5].Length < 5 || valores[5].Length >5)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Hora Inicial inválida na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;

                }
                //VERIFICA SE A HORA INICIAL É VALIDA
                DateTime horainicial;
                if (!DateTime.TryParse(valores[5],out horainicial))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String),"ok","<script>alert('Hora Inválida na linha" + i +"');window.locatiion='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE A HORA FINAL É MENOR QUE 5
                if (valores[6].Length < 5 || valores[6].Length > 5)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Hora Final Inválida na linha" + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                // VERIFICA SE A HORA FINAL É VÁLIDA
                DateTime horafinal;
                if (!DateTime.TryParse(valores[6], out horafinal))
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Hora Final Inválida na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE O TURNO ESTA EM BRANCO
                if (valores[7] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Turno em Branco na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE O TURNO ESTA CORRETO
                if (valores[7] != "M" && valores[7] != "T")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Turno Inválido na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                // VERIFICA SE A QUANTIDADE ESTA EM BRANCO
                if (valores[8] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Quantidade em Branco na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE O PROFISSIONAL ESTA EM BRANCO
                if (valores[9] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Profissional em Branco na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                //VERIFICA SE O PROFISSIONAL EXISTE
                if (valores[9] != "")
                {
                    ViverMais.Model.Profissional profissional = iProfissional.BuscaProfissionalPorNumeroConselhoECategoria<ViverMais.Model.Profissional>(1, valores[9],"").FirstOrDefault();
                    if (profissional == null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Profissional não existe na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }

                    //Verifica se Existe Afastamento do Profissional Para A unidade e Na Data informada
                    AfastamentoProfissional afastamentoProfissional = Factory.GetInstance<IAfastamentoProfissional>().VerificaAfastamentosNaData<AfastamentoProfissional>(valores[2], DateTime.Parse(valores[4]), profissional.CPF);
                    if (afastamentoProfissional != null)
                    {
                        ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Existe Afastamento Para o Profissional Nesta Data e Unidade');window.location='FormImportarAgenda.aspx'</script>");
                        return;
                    }

                }
                //VERIFICA CBO SE ESTA EM BRANCO
                if (valores[10] == "")
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('CBO em Branco na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                 //VERIFICA SE ESSE PROFISSIONAL ESTA VINCULADO NAQUELE CBO
               // IProfissional iProfissional = Factory.GetInstance<IProfissional>();
                ViverMais.Model.VinculoProfissional vinculoprofissional = iProfissional.BuscaProfissionalPorVinculoCBO<ViverMais.Model.VinculoProfissional>(int.Parse(valores[9]),valores[10],valores[2]);
                if (vinculoprofissional == null)
                {
                    ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Profissional não existe na linha " + i + "');window.location='FormImportarAgenda.aspx'</script>");
                    return;
                }
                

            }
            string linha = "";
            int cont = 1;
            //Código para Salvar no Banco            
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] campos = linhas[i].Split(';');
                IAgendamentoServiceFacade iAgendamento = Factory.GetInstance<IAgendamentoServiceFacade>();
                ViverMais.Model.Agenda agenda = new Agenda();
                agenda.Competencia = int.Parse(campos[1]);
                IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();
                ViverMais.Model.EstabelecimentoSaude estabelecimento = iEstabelecimento.BuscarEstabelecimentoPorCNES<ViverMais.Model.EstabelecimentoSaude>(campos[2]);
                ViverMais.Model.Unidade unidade = iAgendamento.BuscarPorCodigo<ViverMais.Model.Unidade>(estabelecimento.CNES);
                if (unidade == null)
                {
                    ViverMais.Model.Unidade EAS = new Unidade();
                    EAS.CNES = estabelecimento.CNES;
                    EAS.IntoleranteFeriado = false;
                  //  EAS.Tipo = '2';
                    iAgendamento.Inserir(EAS);
                }
                agenda.Estabelecimento = estabelecimento;
                IProcedimento iProcedimento = Factory.GetInstance<IProcedimento>();
                ViverMais.Model.Procedimento procedimento = iProcedimento.BuscarPorCodigo<ViverMais.Model.Procedimento>(campos[3]);
                agenda.Procedimento = procedimento;
                agenda.Data = DateTime.Parse(campos[4]);
                agenda.Hora_Inicial = campos[5].ToString();
                agenda.Hora_Final = campos[6].ToString();
                agenda.Turno = char.Parse(campos[7]).ToString();
                agenda.Quantidade = int.Parse(campos[8]);
                agenda.Bloqueada = false;
                agenda.Publicada = true;
                IProfissional iProfissional = Factory.GetInstance<IProfissional>();
                IList<ViverMais.Model.Profissional> profissional = iProfissional.BuscaProfissionalPorNumeroConselhoECategoria<ViverMais.Model.Profissional>(1, campos[9],"");
                if (profissional != null)
                {
                    agenda.ID_Profissional = profissional[0];
                }
                agenda.Cbo.Codigo = campos[10].ToString();

                IParametroAgenda iParametro = Factory.GetInstance<IParametroAgenda>();
                ViverMais.Model.Agenda agenda2 = iParametro.BuscarAgenda<ViverMais.Model.Agenda>(agenda.Estabelecimento.CNES, agenda.Data, agenda.Turno, agenda.Procedimento.Codigo, agenda.Competencia, agenda.ID_Profissional.CPF, agenda.Cbo.Codigo);
                if (agenda2 != null)
                {
                    linha += cont + ";"; 
                   
                    // return;
                }
                else
                {
                    iAgendamento.Salvar(agenda);
                    iAgendamento.Salvar(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 35, "ID:" + agenda.Codigo ));
                }
                cont++;
            }
            ClientScript.RegisterClientScriptBlock(typeof(String), "erro", "<script>alert('Agenda repetida nas linhas: " + linha + "' );window.location='FormImportarAgenda.aspx'</script>");
            ClientScript.RegisterClientScriptBlock(typeof(String), "ok", "<script>alert('Importação da Agenda realizada com Sucesso' );window.location='FormImportarAgenda.aspx'</script>");


            //Deleta o arquivo
            FileInfo file = new FileInfo(Server.MapPath("/Agendamento/docs/" + FileUpload1.FileName));
            file.Delete();
            return;
 

        }
    }
}
