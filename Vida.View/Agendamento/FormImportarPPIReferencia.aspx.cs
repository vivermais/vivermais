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
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.View.Agendamento
{
    public partial class FormImportarPPIReferencia : System.Web.UI.Page
    {
        protected System.Timers.Timer timer;
        protected void Page_Load(object sender, EventArgs e)
        {
            //timer1.Tick += new EventHandler<EventArgs>(Timer1_Tick);
            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            //IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            //IList<CBO> cbos = iViverMais.ListarTodos<CBO>();
            //foreach (CBO cbo in cbos)
            //{
            //    string codigoGrupo = cbo.Codigo.Substring(0, 4);
            //    GrupoCBO grupoCBO = iViverMais.BuscarPorCodigo<GrupoCBO>(codigoGrupo);
            //    if (grupoCBO != null)
            //    {
            //        cbo.GrupoCBO = grupoCBO;
            //        iViverMais.Salvar(cbo);
            //    }
            //}
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lblTotalRegistros.Text = hiddenTotalRegistro.Value;
            lblRegistroAtual.Text = hiddenRegistroAtual.Value;
        }

        void Timer1_Tick(object sender, EventArgs e)
        {
            //lblTotalRegistros.Text = hiddenTotalRegistro.Value;
            //lblRegistroAtual.Text = hiddenRegistroAtual.Value;
        }

        protected void btnUpLoad_OnClick(object sender, EventArgs e)
        {
            //if (ArquivoValidado())
            //{
                InativaTodosOsPactosAtivos();
                ImportarPPI();
            //}
        }

        protected static void GerarPactoReferenciaParaDozeMeses(PactoAgregadoProcedCBO pactoAgregadoProcedCBO)
        {
            //Define os Valores Mensais para este Pacto durante 12 meses
            for (int i = 0; i <= 11; i++)
            {
                PactoReferenciaSaldo pactoReferenciaSaldo = new PactoReferenciaSaldo();
                DateTime data = DateTime.Now.AddMonths(i);
                pactoReferenciaSaldo.PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
                //pactoReferenciaSaldo.Ano = data.Year;
                pactoReferenciaSaldo.Mes = data.Month;
                //pactoReferenciaSaldo.ValorMensal = pactoAgregadoProcedCBO.ValorPactuado / 12;
                pactoReferenciaSaldo.ValorRestante = pactoReferenciaSaldo.PactoAgregadoProcedCBO.ValorMensal;
                //pactosReferenciaSaldo.Add(pactoReferenciaSaldo);
                Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoReferenciaSaldo);
            }
        }

        //protected void AtualizarOsPactosMensais(PactoAgregadoProcedCBO pactoAgregadoProcedCBO)
        //{
        //    IPactoReferenciaSaldo iPactoReferenciaSaldo = Factory.GetInstance<IPactoReferenciaSaldo>();
        //    IList<PactoReferenciaSaldo> pactosReferenciaSaldo = iPactoReferenciaSaldo.BuscarPorPactoAgregado<PactoReferenciaSaldo>(pactoAgregadoProcedCBO.Codigo);
        //    if (pactosReferenciaSaldo.Count != 0)
        //    {
        //        for (int i = 0; i < pactosReferenciaSaldo.Count; i++)
        //        {
        //            pactosReferenciaSaldo[i].ValorMensal = pactoAgregadoProcedCBO.ValorPactuado / 12;
        //            iPactoReferenciaSaldo.Salvar(pactosReferenciaSaldo[i]);
        //        }
        //    }
        //}

        public long RetornaValorMenosPercentualUrgenciaEmergencia(long valor)
        {
            long valorAlterado = valor - ((valor * Pacto.PercentualUrgenciaEmergencia) / 100);
            return valorAlterado;
        }

        void InativaTodosOsPactosAtivos()
        {
            Factory.GetInstance<IPactoAgregadoProcedCbo>().InativarTodosOsPactos(((Usuario)Session["Usuario"]).Codigo);
            Factory.GetInstance<IViverMaisServiceFacade>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 37, "INATIVOU OS PACTOS"));
        }

        void IncluiNovoPacto(Agregado agregado, Procedimento procedimento, List<CBO> cbos, string valor, Municipio municipio, string tipo)
        {
            PactoAgregadoProcedCBO pactoAgregadoProcedCBO = new PactoAgregadoProcedCBO();
            //pactoAgregadoProcedCBO.Pacto = pacto;
            pactoAgregadoProcedCBO.Agregado = agregado;
            switch (tipo)
            {
                case "A":
                    pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.AGREGADO);
                    break;
                //Caso seja Por Procedimento
                case "P":
                    pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.PROCEDIMENTO);
                    pactoAgregadoProcedCBO.Procedimento = procedimento;
                    break;
                //Se for por CBO
                case "C":
                    pactoAgregadoProcedCBO.TipoPacto = Convert.ToChar(PactoAgregadoProcedCBO.TipoDePacto.CBO);
                    pactoAgregadoProcedCBO.Procedimento = procedimento;
                    //foreach (CBO cbo in cbos)
                    pactoAgregadoProcedCBO.Cbos = cbos;
                    break;
            }

            pactoAgregadoProcedCBO.Ativo = true;
            pactoAgregadoProcedCBO.BloqueiaCota = Convert.ToInt32(PactoAgregadoProcedCBO.BloqueadoPorCota.SIM);

            long valorPactuadoMenosPercentualUrgencia = RetornaValorMenosPercentualUrgenciaEmergencia(long.Parse(valor.Replace(",", "").Replace(".", "")));
            pactoAgregadoProcedCBO.ValorPactuado = valorPactuadoMenosPercentualUrgencia;
            pactoAgregadoProcedCBO.PercentualUrgenciaEmergencia = Pacto.PercentualUrgenciaEmergencia;
            pactoAgregadoProcedCBO.DataPacto = DateTime.Now;
            pactoAgregadoProcedCBO.Usuario = (Usuario)Session["Usuario"];
            Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoAgregadoProcedCBO);
            Factory.GetInstance<IViverMaisServiceFacade>().Salvar(new LogAgendamento(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, 36, "ID:" + pactoAgregadoProcedCBO.Codigo));

            //Define os Valores Mensais para este Pacto durante 12 meses
            GerarPactoReferenciaParaDozeMeses(pactoAgregadoProcedCBO);
            //for (int i = 0; i <= 11; i++)
            //{
            //    PactoReferenciaSaldo pactoReferenciaSaldo = new PactoReferenciaSaldo();
            //    DateTime data = DateTime.Now.AddMonths(i);
            //    pactoReferenciaSaldo.PactoAgregadoProcedCBO = pactoAgregadoProcedCBO;
            //    pactoReferenciaSaldo.Ano = data.Year;
            //    pactoReferenciaSaldo.Mes = data.Month;
            //    pactoReferenciaSaldo.ValorMensal = pactoReferenciaSaldo.PactoAgregadoProcedCBO.ValorPactuado / 12;
            //    pactoReferenciaSaldo.ValorRestante = pactoReferenciaSaldo.ValorMensal;
            //    Factory.GetInstance<IViverMaisServiceFacade>().Salvar(pactoReferenciaSaldo);
            //}
            ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Registro Incluído com Sucesso!');", true);
        }

        public bool ArquivoValidado()
        {
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            string filepath = Server.MapPath("/Agendamento/docs/");
            if (FileUpload1.HasFile)
            {
                string[] tipo = FileUpload1.FileName.Split('.');
                if (tipo[1].ToUpper() == "CSV")
                {
                    //Salva o arquivo na pasta DOCS temporáriamente, pois ao final do processamento, excluiremos
                    FileUpload1.SaveAs(filepath + FileUpload1.FileName);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "O Arquivo precisa ser do tipo CSV!'); window.location='FormImportarPPIReferencia.aspx'", true);
                    return false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Selecione um Arquivo Válido!');", true);
                return false;
            }
            string[] linhas = File.ReadAllLines(filepath + FileUpload1.FileName);

            string codigoIBGE;
            string codigoAgregadoOuProcecimento;
            string codigoCBO;
            //string valorPactuado;
            Municipio municipio;

            //Estrutura para Validação dos Dados no Arquivo
            for (int i = 1; i < linhas.Length; i++)
            {
                string[] valores = linhas[i].Split(';');

                //Verifico se o Município é Válido
                codigoIBGE = valores[0];
                municipio = iViverMais.BuscarPorCodigo<Municipio>(codigoIBGE);
                if (municipio != null)
                {
                    codigoAgregadoOuProcecimento = valores[2].Split('-')[0];

                    //Verifico se o Pacto será por Agregado
                    if (codigoAgregadoOuProcecimento.Substring(6, 4).ToUpper() == "XXXX" || codigoAgregadoOuProcecimento.Substring(6, 4).ToUpper() == "0000")
                    {
                        //Nesse caso será por Agregado
                        string codigoAgregado = codigoAgregadoOuProcecimento.Substring(0, 6);
                        Agregado agregado = iViverMais.BuscarPorCodigo<Agregado>(codigoAgregado);
                        if (agregado == null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Agregado informado na Linha " + (i + 1) + " é Inválido!');", true);
                            return false;
                        }
                    }
                    else
                    {
                        //Nesse Caso Testamos para saber se é Por Procedimento ou CBO
                        Procedimento procedimento = iViverMais.BuscarPorCodigo<Procedimento>(codigoAgregadoOuProcecimento.Trim());
                        if (procedimento == null)
                        {
                            ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Procedimento informado na Linha " + (i + 1) + " é Inválido!');", true);
                            return false;
                        }
                        else
                        {
                            //Faço o Teste para saber se é Por CBO
                            codigoCBO = valores[3].Replace("t", "").Replace("\t   \"", "").Replace("\"", "").Trim();
                            if (!String.IsNullOrEmpty(codigoCBO))
                            {
                                if (codigoCBO.Length == 4)//Se for um Grupo de CBO
                                {
                                    IList<CBO> cbos = Factory.GetInstance<ICBO>().ListarPorGrupo<CBO>(codigoCBO);
                                    if (cbos.Count == 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Grupo de CBO Informado na Linha " + i + " não possui nenhum CBO vinculado a ele!');", true);
                                        return false;
                                    }
                                }
                                else
                                {
                                    CBO cbo = iViverMais.BuscarPorCodigo<CBO>(codigoCBO);
                                    if (cbo == null)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O CBO informado na Linha " + (i + 1) + " é Inválido!');", true);
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('O Código IBGE Informado na Linha " + i + " é Inválido!');", true);
                    return false;
                }
            }
            return true;
        }

        protected void ImportarPPI()
        {
            string filepath = Server.MapPath("/Agendamento/docs/");
            if (FileUpload1.HasFile)
            {
                string[] tipo = FileUpload1.FileName.Split('.');
                if (tipo[1].ToUpper() == "CSV")
                {
                    //Salva o arquivo na pasta DOCS temporáriamente, pois ao final do processamento, excluiremos
                    FileUpload1.SaveAs(filepath + FileUpload1.FileName);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "O Arquivo precisa ser do tipo CSV!'); window.location='FormImportarPPIReferencia.aspx'", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "ok", "alert('Selecione um Arquivo Válido!');", true);
                return;
            }

            IViverMaisServiceFacade iViverMaisServiceFacade = Factory.GetInstance<IViverMaisServiceFacade>();
            string[] linhas = File.ReadAllLines(filepath + FileUpload1.FileName);

            string codigoIBGE;
            string codigoAgregadoOuProcecimento;
            string codigoCBO;
            string valorPactuado;
            Municipio municipio;
            timer.Enabled = true;
            timer.Start();
            hiddenTotalRegistro.Value = linhas.Length.ToString();
            //Código para Salvar no Banco            
            for (int i = 1; i < linhas.Length; i++)
            {
                hiddenRegistroAtual.Value = i.ToString();
                string[] campos = linhas[i].Split(';');
                codigoIBGE = campos[0];
                codigoAgregadoOuProcecimento = campos[2].Split('-')[0];
                codigoCBO = campos[3];
                valorPactuado = campos[6];

                municipio = iViverMaisServiceFacade.BuscarPorCodigo<Municipio>(codigoIBGE);
                if (municipio != null)
                {
                    Pacto pactoReferencia = Factory.GetInstance<IPacto>().BuscarPactoPorMunicipio<Pacto>(municipio.Codigo);
                    if (pactoReferencia == null)
                    {
                        pactoReferencia = new Pacto();
                        pactoReferencia.Municipio = municipio;
                        iViverMaisServiceFacade.Salvar(pactoReferencia);
                    }
                    PactoAgregadoProcedCBO pactoAgregado = new PactoAgregadoProcedCBO();

                    //Verifico se o Pacto será por Agregado

                    if (codigoAgregadoOuProcecimento.Substring(6, 4).ToUpper() == "XXXX" || codigoAgregadoOuProcecimento.Substring(6, 4).ToUpper() == "0000")
                    {
                        //string id_agregado = codigoAgregadoOuProcecimento.Substring(0, 6);
                        //Agregado agregado = iViverMaisServiceFacade.BuscarPorCodigo<Agregado>(id_agregado);
                        //if (agregado != null)
                        //{
                        //    IncluiNovoPacto(agregado, null, null, valorPactuado, municipio, "A");
                        //}
                    }
                    else //Se não for um Agregado, faremos um teste pra saber se o Pacto é Por Procedimento ou CBO
                    {
                        string id_agregado = codigoAgregadoOuProcecimento.Substring(0, 6);
                        Agregado agregado = iViverMaisServiceFacade.BuscarPorCodigo<Agregado>(id_agregado);
                        Procedimento procedimento = iViverMaisServiceFacade.BuscarPorCodigo<Procedimento>(codigoAgregadoOuProcecimento.Trim());
                        if (procedimento != null)
                        {
                            //Se o campo do CBO não estiver Nulo, O Pacto será por CBO ou Por Grupo de CBO
                            if (!String.IsNullOrEmpty(codigoCBO))
                            {
                                List<CBO> cbos = new List<CBO>();
                                if (codigoCBO.Length == 4)//Verifico se é por grupo, que contém 4 dígitos
                                {
                                    //Listo os CBOS referentes ao Grupo
                                    cbos = (List<CBO>)Factory.GetInstance<ICBO>().ListarPorGrupo<CBO>(codigoCBO);
                                    //if (cbos.Count != 0)
                                    //{
                                    //    //Para Cada CBO, alimento a tabela relacional de Pacto com CBO
                                    //    for (int j = 0; j < cbos.Count; j++)
                                    //    {
                                    //        pactoAgregado.Cbos.Add(cbos[j]);
                                    //    }
                                    //    iViverMaisServiceFacade.Salvar(pactoAgregado);
                                    //}
                                }
                                else
                                {
                                    CBO cbo = iViverMaisServiceFacade.BuscarPorCodigo<CBO>(codigoCBO);
                                    if (cbo != null)
                                    {
                                        cbos.Add(cbo);
                                    }
                                }
                                IncluiNovoPacto(agregado, procedimento, cbos, valorPactuado, municipio, "C");
                            }
                            else
                            {
                                //Será por procedimento
                                //IncluiNovoPacto(agregado, procedimento, null, valorPactuado, municipio, "P");
                            }
                        }
                    }
                }

            }
        }
    }
}
