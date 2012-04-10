﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Configuration;
using System.Web.Configuration;
using System.Net.Configuration;
using System.Net.Mail;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using System.IO;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using System.Data;
using System.Data.OleDb;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;

namespace ViverMais.View.Vacina
{
    public partial class Apagarduplicatas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Factory.GetInstance<IItemPA>().BuscarItem<ItemPA>("ádjagdjad üdkagdakdg pópoais");
            //if (!IsPostBack)
            //{
            //Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration("~/Web.config");
            //MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            //if (mailSettings != null)
            //{
            //    int port = mailSettings.Smtp.Network.Port;
            //    string host = mailSettings.Smtp.Network.Host;
            //    string password = mailSettings.Smtp.Network.Password;
            //    string username = mailSettings.Smtp.Network.UserName;

            //    MailMessage email = new MailMessage();
            //    email.From = new MailAddress("danisiomarcal@yahoo.com.br");
            //    email.To.Add("danisiosilva@salvador.ba.gov.br");
            //    email.Subject = "Email Teste";
            //    email.IsBodyHtml = true;

            //    email.Body = "Teste";
            //    email.Priority = MailPriority.High;

            //    SmtpClient smtp = new SmtpClient(host, port);
            //    //smtp.UseDefaultCredentials = true;
            //    smtp.Credentials = new
            //    System.Net.NetworkCredential(username, password);
            //    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //    //smtp.EnableSsl = true;
            //    smtp.Send(email);
            //}

            if (Request["opcao"] != null)
            {

                if (Request["opcao"].ToString().Equals("1"))
                {
                    StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Retirando_Duplicatas_Na_Mesma_Dispensacao.txt"), false);
                    writer.WriteLine("=======****RETIRAR DUPLICATA NA MESMA DISPENSAÇÃO****=======");
                    IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();

                    var consulta2 = from cartao in
                                        Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>()
                                    group cartao by new { CodigoDispensacao = cartao.Dispensacao.Codigo, Paciente = cartao.Dispensacao.Paciente.Codigo, Vacina = cartao.Lote.ItemVacina.Vacina.Codigo, DoseV = cartao.Dose.Codigo, DataAplicacao = cartao.Dispensacao.Data.ToString("dd/MM/yyyy") }
                                        into cartaoresposta
                                        where cartaoresposta.Count() > 1
                                        select cartaoresposta;

                    foreach (var sub in consulta2)
                    {
                        //Response.Write("DELETE FROM VCN_CARTAOVACINA WHERE ROWID IN (SELECT ROWID FROM VCN_CARTAOVACINA WHERE ROWNUM <= " + (sub.Count() - 1) + " AND CO_VACINACAO=" + sub.Key.CodigoDispensacao +
                        //    " AND CO_DOSEVACINA=" + sub.Key.DoseV + " AND CO_VACINA=" + sub.Key.Vacina + " AND CO_USUARIO='" + sub.Key.Paciente + "'" +
                        //    " AND TO_CHAR(DATA_APLICACAO,'DD/MM/YYYY')='" + sub.Key.DataAplicacao + "'); <br/>");
                        writer.WriteLine("DELETE FROM VCN_ITEM_DISPENSACAO VI WHERE VI.ROWID IN (SELECT VI.ROWID FROM VCN_ITEM_DISPENSACAO VI, VCN_DISPENSACAO VD, VCN_LOTE VL," +
                        " VCN_ITEMVACINA VIV" +
                        " WHERE ROWNUM  <= " + (sub.Count() - 1) + " AND VD.CO_DISPENSACAOVACINA=" + sub.Key.CodigoDispensacao + " AND VD.CO_DISPENSACAOVACINA = VI.CO_DISPENSACAO " +
                            " AND VI.CO_DOSE=" + sub.Key.DoseV + " AND VIV.CO_VACINA=" + sub.Key.Vacina + " AND VD.CO_PACIENTE='" + sub.Key.Paciente + "'" +
                            " AND TO_CHAR(VD.DATA,'DD/MM/YYYY')='" + sub.Key.DataAplicacao + "' AND VL.CO_ITEM = VIV.CO_ITEM AND VL.CO_LOTE = VI.CO_LOTE);");
                    }

                    writer.WriteLine("TOTAL: " + consulta2.Count());
                    writer.Close();

                    Response.Write("ARQUIVO GERADO");
                }
                else
                {
                    if (Request["opcao"].ToString().Equals("2"))
                    {
                        StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Retirando_Duplicatas_em_Dispensacoes_Diferentes.txt"), false);

                        writer.WriteLine("========****RETIRAR DUPLICATA EM DISPENSAÇÕES DIFERENTES****========");
                        var consulta = from cartao in
                                           Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>()
                                       group cartao by new { Paciente = cartao.Dispensacao.Paciente.Codigo, Vacina = cartao.Lote.ItemVacina.Vacina.Codigo, DoseV = cartao.Dose.Codigo, DataAplicacao = cartao.Dispensacao.Data.ToString("dd/MM/yy") }
                                           into cartaoresposta
                                           where cartaoresposta.Count() > 1
                                           select cartaoresposta;

                        IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();
                        int count = 0;
                        foreach (var cartao in consulta)
                        {
                            var codigocartao = cartao.Min(p => p.Codigo);
                            var sub = cartao.Where(p => p.Codigo != long.Parse(codigocartao.ToString())).ToList();

                            foreach (var ccartao in sub)
                            {
                                //Response.Write("DELETE FROM VCN_CARTAOVACINA WHERE CO_CARTAOVACINA=" + ccartao.Codigo + "; <br/>");

                                foreach (ItemDispensacaoVacina itemdispensacao in itens.Where(p => p.Codigo == ccartao.Codigo).ToList())
                                {
                                    writer.WriteLine("DELETE FROM VCN_ITEM_DISPENSACAO WHERE CO_ITEMDISPENSACAO = " + itemdispensacao.Codigo + ";");
                                    count++;
                                }
                                //Response.Write("DELETE FROM VCN_DISPENSACAO WHERE CO_DISPENSACAOVACINA = " + ccartao.Dispensacao.Codigo + "; <br/>");
                            }
                        }
                        writer.WriteLine("TOTAL DE DISPENSAÇÕES APAGADAS: " + count);
                        writer.WriteLine("=========FIM DAS RETIRADAS DE DUPLICATAS=========");
                        writer.Close();

                        Response.Write("ARQUIVO GERADO");
                    }
                    else
                    {
                        if (Request["opcao"].ToString().Equals("3"))
                        {
                            StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Apagando_dispensacoes_sem_itens.txt"), false);

                            int c = 0;
                            IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();

                            IList<DispensacaoVacina> dispensacoes = Factory.GetInstance<IDispensacao>().ListarTodos<DispensacaoVacina>().Where(p => !itens.Select(p2 => p2.Dispensacao.Codigo).ToList().Contains(p.Codigo)).ToList();
                            foreach (DispensacaoVacina dispensacao in dispensacoes)
                            {
                                c++;
                                writer.WriteLine("DELETE FROM VCN_DISPENSACAO WHERE CO_DISPENSACAOVACINA = " + dispensacao.Codigo + ";");
                            }
                            writer.WriteLine("TOTAL SEM ITENS: " + c);
                            writer.Close();

                            Response.Write("ARQUIVO GERADO");
                        }
                        else
                        {
                            if (Request["opcao"].ToString().Equals("4"))
                            {
                                StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Atualizando_Estrategia_Item_Dispensado.txt"), false);
                                IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();
                                IList<Estrategia> estrategias = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<Estrategia>();

                                foreach (ItemDispensacaoVacina item in itens)
                                    writer.WriteLine("UPDATE VCN_ITEM_DISPENSACAO SET CO_ESTRATEGIA= " + estrategias.Where(p => p.Vacinas.Select(p2 => p2.Codigo).Contains(item.Lote.ItemVacina.Vacina.Codigo)).First().Codigo + " WHERE CO_ITEMDISPENSACAO=" + item.Codigo + ";");

                                writer.WriteLine("TOTAL: " + itens.Count());
                                writer.Close();
                                Response.Write("ARQUIVO GERADO");
                            }
                            else
                            {
                                if (Request["opcao"].ToString().Equals("5"))
                                {
                                    //int count = int.Parse(Factory.GetInstance<ICartaoVacina>().ListarTodos<CartaoVacina>().Max(p => p.Codigo).ToString()) + 1;

                                    StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Inserindo_Cartao_Vacina.txt"), false);
                                    int count = 1;
                                    //int pos = 1;
                                    IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();
                                    foreach (ItemDispensacaoVacina item in itens)
                                    {
                                        writer.WriteLine("INSERT INTO VCN_CARTAOVACINA (CO_CARTAOVACINA,CO_ITEMDISPENSACAO, CO_USUARIO, CO_VACINA, CO_DOSEVACINA, DATA_APLICACAO, LOTE, VALIDADE_LOTE, LOCAL, MOTIVO) VALUES (" + count + "," + item.Codigo + ",'" + item.Dispensacao.Paciente.Codigo + "'," + item.Lote.ItemVacina.Vacina.Codigo + ", " + item.Dose.Codigo + ",'" + item.Dispensacao.Data.ToString("dd/MM/yyyy") + "','" + item.Lote.Identificacao + "','" + item.Lote.DataValidade.ToString("dd/MM/yyyy") + "','" + item.Dispensacao.Sala.Nome + "','" + item.Estrategia.Descricao + "');");
                                        count++;
                                    }

                                    writer.WriteLine("DROP SEQUENCE VCN_CARTAOVACINA_SEQUENCE;");
                                    writer.WriteLine("CREATE SEQUENCE VCN_CARTAOVACINA_SEQUENCE START WITH " + count + " INCREMENT BY 1;");
                                    writer.WriteLine("TOTAL CARTAO: " + itens.Count());
                                    writer.Close();

                                    //select co_usuario,data_aplicacao,co_vacina,co_dosevacina from vcn_cartaovacina where co_vacinacao is null
                                    //group by data_aplicacao, co_vacina, co_dosevacina, co_usuario
                                    //having (count(*)) > 1
                                    //LEMBRAR DE APAGAR TODOS OS CARTOES NÃO GERADOS PELAS DISPENSAÇÕES

                                    Response.Write("ARQUIVO GERADO");
                                }
                                else
                                {
                                    if (Request["opcao"].ToString().Equals("6")) //Criando os usuários vacina com seus respectivos responsáveis
                                    {
                                        StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/Usuarios_Responsaveis_Vacina.txt"), false);

                                        IList<SalaVacina> salas = Factory.GetInstance<ISalaVacina>().ListarTodos<SalaVacina>();
                                        int count = 1;

                                        foreach (SalaVacina sala in salas)
                                        {
                                            foreach (int co_usuario in sala.CodigosUsuarios)
                                            {
                                                Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(co_usuario);

                                                if (usuario.Unidade.CNES == sala.EstabelecimentoSaude.CNES)
                                                {
                                                    char responsavel = 'N';

                                                    if (sala.Responsaveis.Where(p => p.Unidade.CNES == sala.EstabelecimentoSaude.CNES && p.Codigo == co_usuario).FirstOrDefault() != null)
                                                        responsavel = 'Y';

                                                    writer.WriteLine("INSERT INTO VCN_USUARIO (CODIGO,CO_USUARIO,CO_SALA,RESPONSAVEL) VALUES (" + count + "," + co_usuario + "," + sala.Codigo + ",'" + responsavel + "');");
                                                    count++;
                                                }
                                            }
                                        }

                                        writer.WriteLine("DROP SEQUENCE VCN_USUARIO_SALA;");
                                        writer.WriteLine("CREATE SEQUENCE VCN_USUARIO_SALA START WITH " + count + " INCREMENT BY 1;");
                                        writer.WriteLine("=====TOTAL DE USUÁRIOS VACINA IMPORTADOS: " + (count - 1) + "=====");
                                        writer.Close();
                                        Response.Write("ARQUIVO GERADO");
                                    }
                                    else
                                    {
                                        if (Request["opcao"].ToString().Equals("7"))
                                        {
                                            IList<MovimentoVacina> movimentacoes = Factory.GetInstance<IVacina>().ListarTodos<MovimentoVacina>();
                                            var agrupar = from movimento in movimentacoes
                                                          group movimento by new { Data = movimento.Data.ToString("yyyyMM") } into _movimento
                                                          select _movimento;

                                            foreach (var _movimento in agrupar)
                                            {
                                                long numero = long.Parse(_movimento.Key.Data.Substring(2, 2) + _movimento.Key.Data.Substring(4, 2) + 1.ToString("00000"));
                                                foreach (MovimentoVacina movimento in _movimento)
                                                {
                                                    Response.Write("UPDATE vcn_movimento SET numero=" + numero + " WHERE co_movimento =" + movimento.Codigo + ";<br/>");
                                                    numero++;
                                                }
                                            }
                                            Response.Write("ARQUIVO GERADO");
                                        }
                                        else
                                        {
                                            if (Request["opcao"].ToString().Equals("8"))
                                            {
                                                DataSet ds = new DataSet();
                                                OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Vacina/ExclusaoVacina/update1.xls") + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
                                                OleDbDataAdapter da = new OleDbDataAdapter("Select * From [1$]", conexao);
                                                da.Fill(ds);

                                                long i = 1;
                                                foreach (DataRow r in ds.Tables[0].Rows)
                                                {
                                                    Response.Write("Objcon.Execute(\"UPDATE SET ID_VINCULO = 6 WHERE CPF ='" + r["CPF"].ToString() + "'\") <br/>");
                                                    i++;
                                                }
                                            }
                                            else
                                            {
                                                if (Request["opcao"].ToString().Equals("9"))
                                                {
                                                    DataSet ds = new DataSet();
                                                    OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Vacina/ExclusaoVacina/update2.xls") + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"");
                                                    OleDbDataAdapter da = new OleDbDataAdapter("Select * From [1$]", conexao);
                                                    da.Fill(ds);

                                                    long i = 1;
                                                    foreach (DataRow r in ds.Tables[0].Rows)
                                                    {
                                                        Response.Write("Objcon.Execute(\"UPDATE SET ID_VINCULO = 1 WHERE CPF ='" + r["CPF"].ToString() + "'\") <br/>");
                                                        i++;
                                                    }
                                                }
                                                else
                                                {
                                                    if (Request["opcao"].ToString().Equals("10"))
                                                    {
                                                        DataSet ds = new DataSet();
                                                        OleDbConnection conexao = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Server.MapPath("~/Vacina/ExclusaoVacina/agenda.xls") + ";Extended Properties=\"Excel 8.0;HDR=YES\"");
                                                        
                                                        OleDbDataAdapter da = new OleDbDataAdapter("Select CARTAO_SUS From [agenda$]", conexao);
                                                        da.Fill(ds);
                                                        IPaciente iPaciente = Factory.GetInstance<IPaciente>();
                                                        conexao.Open();
                                                        da.Dispose();

                                                        long i = 1;
                                                        foreach (DataRow r in ds.Tables[0].Rows)
                                                        {
                                                            ViverMais.Model.Paciente paciente = iPaciente.PesquisarPacientePorCNS<ViverMais.Model.Paciente>(r["CARTAO_SUS"].ToString().Trim());
                                                            if (paciente != null)
                                                            {
                                                                OleDbCommand command = new OleDbCommand("UPDATE [agenda$] SET DATA_NASCIMENTO=? WHERE CARTAO_SUS=?", conexao);
                                                                command.Parameters.Add(new OleDbParameter("nascimento", paciente.DataNascimento.ToString("dd/MM/yyyy")));
                                                                command.Parameters.Add(new OleDbParameter("cartaosus", r["CARTAO_SUS"].ToString()));
                                                                
                                                                command.ExecuteNonQuery();
                                                                command.Dispose();
                                                            }
                                                            //Response.Write("Objcon.Execute(\"UPDATE SET ID_VINCULO = 1 WHERE CPF ='" + r["CPF"].ToString() + "'\") <br/>");
                                                            //i++;
                                                        }
                                                        //IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
                                                        // iMovimento.CorrigirMovimentacoes();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Response.Write("=========FIM DAS RETIRADAS DE DUPLICATAS 2========= <br/>");

            //ANTIGO DUPLICATAS
            //Response.Write("========****RETIRAR DUPLICATA EM DISPENSAÇÕES DIFERENTES****======== <br/>");
            //var consulta = from cartao in
            //                   Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<CartaoVacina>().Where(p => p.ItemDispensacao == null && p.SalaVacina != null).ToList()
            //               group cartao by new { Paciente = cartao.Paciente.Codigo, Vacina = cartao.Vacina.Codigo, DoseV = cartao.DoseVacina.Codigo, DataAplicacao = cartao.DataAplicacao.Value.ToString("dd/MM/yyyy") }
            //                   into cartaoresposta
            //                   where cartaoresposta.Count() > 1
            //                   select cartaoresposta;

            //IList<ItemDispensacaoVacina> itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();

            //foreach (var cartao in consulta)
            //{
            //    var codigocartao = cartao.Min(p => p.Codigo);
            //    var sub = cartao.Where(p => p.Codigo != long.Parse(codigocartao.ToString())).ToList();

            //    foreach (var ccartao in sub)
            //    {
            //        Response.Write("DELETE FROM VCN_CARTAOVACINA WHERE CO_CARTAOVACINA=" + ccartao.Codigo + "; <br/>");

            //        foreach (ItemDispensacaoVacina itemdispensacao in itens.Where(p => p.Dispensacao.Codigo == ccartao.DispensacaoVacina.Codigo).ToList())
            //        {
            //            Response.Write("DELETE FROM VCN_ITEM_DISPENSACAO WHERE CO_ITEMDISPENSACAO = " + itemdispensacao.Codigo + "; <br/>");
            //        }

            //        Response.Write("DELETE FROM VCN_DISPENSACAO WHERE CO_DISPENSACAOVACINA = " + ccartao.DispensacaoVacina.Codigo + "; <br/>");
            //    }
            //}
            //Response.Write("=========FIM DAS RETIRADAS DE DUPLICATAS========= <br/>");

            //Response.Write("=========****ASSOCIANDO CARTÕES E DISPENSAÇÕES****========= <br/>");
            //var consulta = from cartao in
            //                   Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<CartaoVacina>().Where(p => p.ItemDispensacao == null && p.SalaVacina != null).ToList()
            //               group cartao by new { Paciente = cartao.Paciente.Codigo, Vacina = cartao.Vacina.Codigo, DoseV = cartao.DoseVacina.Codigo, DataAplicacao = cartao.DataAplicacao.Value.ToString("dd/MM/yyyy") }
            //                   into cartaoresposta
            //                   where cartaoresposta.Count() <= 1
            //                   select cartaoresposta;

            //itens = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<ItemDispensacaoVacina>();
            //int total_atualizados = 0;
            //foreach (var cartao in consulta)
            //{
            //    foreach (var ccartao in cartao)
            //    {
            //        if (ccartao.DispensacaoVacina != null)
            //        {
            //            IList<ItemDispensacaoVacina> item = itens.Where(p => p.Lote.ItemVacina.Vacina.Codigo == ccartao.Vacina.Codigo && p.Dose.Codigo == ccartao.DoseVacina.Codigo
            //                && p.Dispensacao.Codigo == ccartao.DispensacaoVacina.Codigo && p.Dispensacao.Paciente.Codigo == ccartao.Paciente.Codigo).ToList();

            //            if (item.Count > 1)
            //                Response.Write("MERDA PRO CARTAO: " + ccartao.Codigo + " mais de um item relacionado <br/>");
            //            else
            //            {
            //                if (item.Count == 0)
            //                    Response.Write("DELETE FROM VCN_CARTAOVACINA WHERE CO_CARTAOVACINA = " + ccartao.Codigo + "<br/>");
            //                else
            //                {
            //                    Response.Write("UPDATE VCN_CARTAOVACINA SET CO_ITEMDISPENSACAO=" + item[0].Codigo + " WHERE CO_CARTAOVACINA=" + ccartao.Codigo + "; <br/>");
            //                    total_atualizados++;
            //                }
            //            }
            //        }
            //    }
            //}
            //Response.Write("TOTAL DE ATUALIZACOES: " + total_atualizados + "<br/>");
            //Response.Write("=========FIM DAS ATUALIZAÇÕES========= <br/>");

            //RODAR ALGORITMO DE BAIXO PARA CIMA

            //IList<ViverMais.Model.Paciente> pacientes = Factory.GetInstance<IVacina>().Listar3000Pacientes<ViverMais.Model.Paciente>();

            //IList<DispensacaoVacina> dispensacoes = Factory.GetInstance<IDispensacao>().ListarTodos<DispensacaoVacina>();
            //Hashtable codigospassados = new Hashtable();

            //foreach (DispensacaoVacina dispensacao in dispensacoes)
            //{
            //    ViverMais.Model.Paciente paciente = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(dispensacao.CodigoPaciente);
            //    int pos = 0;

            //    if (paciente == null)
            //    {
            //        ViverMais.Model.Paciente pacientescolhido = null;

            //        if (codigospassados.ContainsKey(dispensacao.CodigoPaciente))
            //        {
            //            //Response.Write("Já passou");
            //            pacientescolhido = (ViverMais.Model.Paciente)codigospassados[dispensacao.CodigoPaciente];
            //        }
            //        else
            //        {
            //            ViverMais.Model.Paciente pac2 = pacientes[pos];
            //            codigospassados.Add(dispensacao.CodigoPaciente, pac2);
            //            pos++;
            //            pacientescolhido = pac2;
            //            //Response.Write("Novo");
            //        }

            //        dispensacao.CodigoPaciente = pacientescolhido.Codigo;
            //        Factory.GetInstance<IDispensacao>().Atualizar(dispensacao);
            //    }
            //}

            //Response.Write("Terminou");
        }
        //}
    }
}
