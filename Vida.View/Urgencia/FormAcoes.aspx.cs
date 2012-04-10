using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using System.Web.Services.Description;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace ViverMais.View.Urgencia
{
    public partial class FormAcoes : System.Web.UI.Page
    {
        private int IMPORTAR_PROCEDIMENTOS_NAO_FATURAVEIS = 1, IMPORTAR_UNIDADES_NAOMUNICIPAIS = 2;
        private int IMPORTAR_PENSOS = 3;
        private int LISTAR_PROFISSIONAIS = 4;
        private int IMPORTAR_MEDICAMENTOSPRESCRICAO = 5;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Request["co_prontuario"] != null)
                //{
                //    StringWriter _writer = new StringWriter();
                //    HttpContext.Current.Server.Execute("FormImprimirHistoricoProntuario.aspx?tipo=atestadosreceitas&co_prontuario=" + Request["co_prontuario"].ToString(), _writer);
                //    string strHtml = HttpUtility.HtmlDecode(_writer.ToString()).Replace("&nbsp", "");

                //    MemoryStream m = new MemoryStream();
                //    Document document = new Document();
                //    PdfWriter.GetInstance(document, m);
                //    HTMLWorker parser = new HTMLWorker(document);

                //    document.Open();
                //    parser.Parse(new StringReader(strHtml));
                //    document.Close();

                //    Response.Clear();
                //    Response.ClearContent();
                //    Response.ClearHeaders();
                //    Response.ContentType = "application/pdf";
                //    Response.AppendHeader("Content-Disposition", "inline;filename=download_report.pdf");
                //    Response.AppendHeader("Content-Length", m.GetBuffer().Length.ToString());
                //    Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
                //    Response.OutputStream.Flush();
                //    Response.OutputStream.Close();
                //    Response.End();
                //}

//                //StreamReader sr = new StreamReader(Server.MapPath("~/Urgencia/Documentos/temp/CNSProvisorios20110509_171430.txt"));
//                //string[] cartoes = new string[50000];
//                //int pos = 0;

//                //while (!sr.EndOfStream)
//                //{
//                //    cartoes[pos] = "'" + sr.ReadLine() + "'";
//                //    pos++;
//                //}

//                //Response.Write(string.Join(",", cartoes));
//                //return;
//                //System.Net.WebClient client = new System.Net.WebClient();
//                //System.IO.Stream stream = client.OpenRead("http://www.webserviceViverMais.saude.salvador.ba.gov.br/ViverMaisWS.asmx?wsdl");
//                //// Get a WSDL file describing a service.
//                //ServiceDescription description = ServiceDescription.Read(stream);

//                //// Initialize a service description importer.
//                //ServiceDescriptionImporter importer = new ServiceDescriptionImporter();
//                //importer.ProtocolName = "Soap12";  // Use SOAP 1.2.
//                //importer.AddServiceDescription(description, null, null);

//                ////// Report on the service descriptions.
//                ////Console.WriteLine("Importing {0} service descriptions with {1} associated schemas.",
//                ////                  importer.ServiceDescriptions.Count, importer.Schemas.Count);

//                //// Generate a proxy client.
//                //importer.Style = ServiceDescriptionImportStyle.Client;

//                //// Generate properties to represent primitive values.
//                //importer.CodeGenerationOptions = System.Xml.Serialization.CodeGenerationOptions.GenerateProperties;

//                //// Initialize a Code-DOM tree into which we will import the service.
//                //CodeNamespace nmspace = new CodeNamespace();
//                //CodeCompileUnit unit1 = new CodeCompileUnit();
//                //unit1.Namespaces.Add(nmspace);

//                //// Import the service into the Code-DOM tree. This creates proxy code
//                //// that uses the service.
//                //ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit1);

//                //if (warning == 0)
//                //{
//                //    // Generate and print the proxy code in C#.
//                //    CodeDomProvider provider1 = CodeDomProvider.CreateProvider("CSharp");

//                //    // Compile the assembly with the appropriate references
//                //    string[] assemblyReferences = new string[2] { "System.Web.Services.dll", "System.Xml.dll" };
//                //    CompilerParameters parms = new CompilerParameters(assemblyReferences);
//                //    CompilerResults results = provider1.CompileAssemblyFromDom(parms, unit1);

//                //    foreach (CompilerError oops in results.Errors)
//                //    {
//                //        Response.Write("========Compiler error============");
//                //        Response.Write(oops.ErrorText);
//                //    }

//                //    //Invoke the web service method
//                //    object o = results.CompiledAssembly.CreateInstance("ViverMaisWS");

//                //    Type t = o.GetType();
//                //    object[] _args = new object[3] { "danisio marcal", "", new DateTime(1986, 12, 12) };

//                //    object objeto = t.InvokeMember("Pesquisar", System.Reflection.BindingFlags.InvokeMethod,null, o, _args);
//                //    //ArrayList pacientes = objeto as ArrayList;
//                //    //Response.Write(pacientes[0]);
//                //    //ds.AcceptChanges();

//                //    //String XmlizedString = null;

//                //    XmlSerializer xs = new XmlSerializer(objeto.GetType());
//                //    System.Text.StringBuilder sb = new System.Text.StringBuilder();
//                //    System.IO.StringWriter writer = new System.IO.StringWriter(sb);
//                //    xs.Serialize(writer, objeto);

//                //    DataSet ds = new DataSet();
//                //    ds.ReadXml(new StringReader(sb.ToString()));

//                //    //Response.Write(ds.Tables["Paciente"].Rows[0]["Nome"].ToString());
//                //}
//                //else
//                //{
//                //    // Print an error message.
//                //    Response.Write("Warning: " + warning);
//                //}

//                WebServiceDinamico _web = new WebServiceDinamico("http://172.22.6.16:28971/webrun/webservices/TKTServices.jws?wsdl", "TKTServicesService");
                
//                ////Tipo operador A->Atendente (Não precisa), M->Médico, E->Enfermeiro
//                //object usuario1 = _web.ExecutarMetodoObj("SmsCadastrarUsuario", new string[3] { "1", "160519226960009", "M" });
//                //object usuario2 = _web.ExecutarMetodoObj("SmsCadastrarUsuario", new string[3] { "2", "898002252111641", "E" });

//                //Response.Write(usuario1.ToString());
//                //Response.Write(usuario2.ToString());

//                ////Prioridade: 0 a 3(Vermelho)
//                ////Codigo do servico: A-> Acolhimento, R->Consulta Médica
//                object bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "C", "1", "3", null, "CLINICA MEDICA" } );
//                Response.Write(bilheteatendimento.ToString());

//                bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "A", "1", null, null, "ACOLHIMENTO" });
//                Response.Write(bilheteatendimento.ToString());

//                bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "P", "1", "3", null, "PEDIATRIA" });
//                Response.Write(bilheteatendimento.ToString());

//                bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "O", "1", "0", null, "ORTOPEDIA" });
//                Response.Write(bilheteatendimento.ToString());

//                bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "O", "1", "0", null, "ORTOPEDIA" });
//                Response.Write(bilheteatendimento.ToString());

//                bilheteatendimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "O", "1", "3", null, "ORTOPEDIA" });
//                Response.Write(bilheteatendimento.ToString());

//                //object bilheteacolhimento = _web.ExecutarMetodoObj("SmsEmitirBilhete", new string[5] { "A", "1", null, null, "teste acolhimento" } );
//                //Response.Write("<br />" + bilheteacolhimento.ToString());
                
//                //object[] _args = new object[3] { "danisio marcal", "", new DateTime(1986, 12, 12) };
//                //DataSet ds = _web.ExecutarMetodoDs("Pesquisar", _args);

//                //Response.Write(ds.Tables["Paciente"].Rows[0]["Nome"].ToString());

//                //_args = new object[3] { "thiago marcal", "", new DateTime(1984, 6, 11) };
//                //ds = _web.ExecutarMetodoDs("Pesquisar", _args);

//                //Response.Write(ds.Tables["Paciente"].Rows[0]["Nome"].ToString());

//                int opcao;

//                if (Request["opcao"] != null && int.TryParse(Request["opcao"].ToString(), out opcao))
//                {
//                    if (opcao == this.IMPORTAR_PROCEDIMENTOS_NAO_FATURAVEIS &&
//                        Request["file"] != null && Request["file"].ToString().Contains(".xls"))
//                    {
//                        string file = Request["file"].ToString();

//                        DataSet ds = new DataSet();
//                        OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Urgencia/Documentos/temp/" + file) + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
//                        OleDbDataAdapter da = new OleDbDataAdapter("Select * From [Plan1$]", conexao);
//                        da.Fill(ds);

//                        long i = 1;
//                        foreach (DataRow r in ds.Tables[0].Rows)
//                        {
//                            Response.Write("INSERT INTO URG_PROCEDIMENTONAOFATURAVEL (CO_PROCEDIMENTO,NOME,STATUS) VALUES (" + i + ",'" + retiraAcentos(r["descricao"].ToString()) + "','A');" + "<br/>");
//                            i++;
//                        }
//                    }
//                    else
//                    {
//                        if (opcao == this.IMPORTAR_PENSOS &&
//        Request["file"] != null && Request["file"].ToString().Contains(".xls"))
//                        {
//                            string file = Request["file"].ToString();

//                            DataSet ds = new DataSet();
//                            OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Urgencia/Documentos/temp/" + file) + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
//                            OleDbDataAdapter da = new OleDbDataAdapter("Select * From [Plan1$]", conexao);
//                            da.Fill(ds);

//                            long i = 1;
//                            foreach (DataRow r in ds.Tables[0].Rows)
//                            {
//                                string nome = r[0].ToString().Substring(12, r[0].ToString().Length - 12);
//                                string codigo = r[0].ToString().Substring(0, 9);

//                                Response.Write("INSERT INTO URG_ITEMPA (CO_ITEM,CODIGO,NOME,STATUS) VALUES (" + i + ",'" + codigo + "','" + retiraAcentos(nome) + "','A');" + "<br/>");
//                                i++;
//                            }
//                        }
//                        else
//                        {
//                            if (opcao == this.IMPORTAR_UNIDADES_NAOMUNICIPAIS &&
//                            Request["file"] != null && Request["file"].ToString().Contains(".xls"))
//                            {
//                                string file = Request["file"].ToString();

//                                DataSet ds = new DataSet();
//                                OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Urgencia/Documentos/temp/" + file) + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
//                                OleDbDataAdapter da = new OleDbDataAdapter("Select [ESTABELECIMENTO], [CNES] From [Plan1$]", conexao);
//                                da.Fill(ds);

//                                long i = 1;
//                                foreach (DataRow r in ds.Tables[0].Rows)
//                                {
//                                    Response.Write("INSERT INTO ViverMais_UNIDADEEMERGENCIAL (CO_UNIDADE,NOME,CNES) VALUES (" + i + ",'" + retiraAcentos(r["ESTABELECIMENTO"].ToString()) + "','" + r["CNES"].ToString() + "');" + "<br/>");
//                                    i++;
//                                }

//                                Response.Write("DROP ViverMais_EASEMERGENCIAL_SEQUENCE; <br/>");
//                                Response.Write(@"CREATE SEQUENCE ViverMais_EASEMERGENCIAL_SEQUENCE START WITH " + i.ToString() + @" NOCACHE
//                INCREMENT BY 1;<br/>");
//                            }
//                            else
//                            {
//                                if (opcao == this.LISTAR_PROFISSIONAIS)
//                                {
//                                    DataTable medicos = new DataTable();
//                                    medicos.Columns.Add("Nome", typeof(string));
//                                    medicos.Columns.Add("Especialidade", typeof(string));
//                                    DataTable enfermeiros = new DataTable();
//                                    enfermeiros.Columns.Add("Nome", typeof(string));
//                                    enfermeiros.Columns.Add("Especialidade", typeof(string));
//                                    DataTable tecnicos = new DataTable();
//                                    tecnicos.Columns.Add("Nome", typeof(string));
//                                    tecnicos.Columns.Add("Especialidade", typeof(string));

//                                    IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
//                                    IList<UsuarioProfissionalUrgence> usuarios = iUsuarioProfissional.ListarTodos<UsuarioProfissionalUrgence>();

//                                    ICBO iViverMais = Factory.GetInstance<ICBO>();
//                                    IProfissional iProfissional = Factory.GetInstance<IProfissional>();

//                                    foreach (UsuarioProfissionalUrgence usuario in usuarios)
//                                    {
//                                        CBO cbo = iViverMais.BuscarPorCodigo<CBO>(usuario.CodigoCBO);
//                                        if (iViverMais.CBOPertenceMedico<CBO>(cbo))
//                                        {
//                                            DataRow row = medicos.NewRow();
//                                            row["Nome"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional).Nome;
//                                            row["Especialidade"] = cbo.Nome;
//                                            medicos.Rows.Add(row);
//                                        }
//                                        else if (iViverMais.CBOPertenceEnfermeiro<CBO>(cbo))
//                                        {
//                                            DataRow row = medicos.NewRow();
//                                            row["Nome"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional).Nome;
//                                            row["Especialidade"] = cbo.Nome;
//                                            enfermeiros.Rows.Add(row);
//                                            //enfermeiros.Add(new { Nome = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional).Nome, Especialidade = cbo.Nome });
//                                        }
//                                        else if (iViverMais.CBOPertenceAuxiliarTecnicoEnfermagem<CBO>(cbo))
//                                        {
//                                            DataRow row = medicos.NewRow();
//                                            row["Nome"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional).Nome;
//                                            row["Especialidade"] = cbo.Nome;
//                                            tecnicos.Rows.Add(row);
//                                            //tecnicos.Add(new { Nome = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(usuario.Id_Profissional).Nome, Especialidade = cbo.Nome });
//                                        }
//                                    }

//                                    this.GridView_Medicos.DataSource = medicos;
//                                    this.GridView_Medicos.DataBind();

//                                    this.GridView_Enfermeiros.DataSource = enfermeiros;
//                                    this.GridView_Enfermeiros.DataBind();

//                                    this.GridView_Tecnicos.DataSource = tecnicos;
//                                    this.GridView_Tecnicos.DataBind();
//                                }
//                                else
//                                {
//                                    if (opcao == this.IMPORTAR_MEDICAMENTOSPRESCRICAO)
//                                    {
//                                        DataSet ds = new DataSet();
//                                        OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Urgencia/Documentos/temp/Lista_de_Precrições_CMUE.xls") + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
//                                        OleDbDataAdapter da = new OleDbDataAdapter("Select [Nome] From [Plan1$]", conexao);
//                                        da.Fill(ds);
//                                        IMedicamento iMedicamento = Factory.GetInstance<IMedicamento>();
//                                        int max = iMedicamento.ListarTodos<Medicamento>().Select(p => p.Codigo).Max() + 1;

//                                        foreach (DataRow r in ds.Tables[0].Rows)
//                                        {
//                                            Response.Write("INSERT INTO FAR_MEDICAMENTO (ID_MEDICAMENTO,CODMEDICAMENTO,MEDICAMENTO,ID_UNIDADEMEDIDA,IND_ANTIBIO,PERTENCEAREDE) VALUES (" + max + ",'" + "URG" + max.ToString() + "','" + r["Nome"].ToString().ToUpper() + "',1,'N','N');<br/>");
//                                            max++;
//                                        }

//                                        Response.Write(@"DROP SEQUENCE FAR_MEDICAMENTO_SEQUENCE; <br/>
//                                        CREATE SEQUENCE FAR_MEDICAMENTO_SEQUENCE START WITH " + max.ToString() + " NOCACHE INCREMENT BY 1;");
//                                    }
//                                    else
//                                    {
//                                        IList<EventoUrgencia> eventos = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<EventoUrgencia>();
//                                        foreach (EventoUrgencia evento in eventos)
//                                            Response.Write("INSERT INTO URG_EVENTOS (CO_EVENTO,EVENTO) VALUES (" + evento.Codigo + ",'" + evento.Descricao + "') ;<br/>");
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
            }
        }

        private string retiraAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }
    }
}
