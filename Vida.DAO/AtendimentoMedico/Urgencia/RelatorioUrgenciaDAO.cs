using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using InfoSoftGlobal;
using System.IO;
using ViverMais.Model.Entities.ViverMais;
using ViverMais.BLL;
using CrystalDecisions.CrystalReports.Engine;
using System.Web;
using ViverMais.DAO.AtendimentoMedico.Relatorios;
using ViverMais.ServiceFacade.ServiceFacades.Profissional;
using ViverMais.ServiceFacade.ServiceFacades.Profissional.Misc;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class RelatorioUrgenciaDAO : UrgenciaServiceFacadeDAO, IRelatorioUrgencia
    {
        #region IRelatorioUrgencia Members
        DataTable IRelatorioUrgencia.ObterQuantitativoAtendimentoCID(object id_unidade, object cid, DateTime datainicial, DateTime datafinal)
        {
            IList<Cid> cids = new List<Cid>();
            ICid iCid = Factory.GetInstance<ICid>();

            string hql = string.Empty;

            hql = "FROM ViverMais.Model.EvolucaoMedica AS e WHERE e.Prontuario.CodigoUnidade='" + id_unidade + "' and ";
            hql += "e.Data BETWEEN TO_DATE('" + datainicial.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI') ";
            hql += "AND TO_DATE('" + datafinal.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";

            EvolucaoMedica[] evolucoesmedica = Session.CreateQuery(hql).List<EvolucaoMedica>().Where(p => p.CodigosCids.Count() > 0).ToArray();

            foreach (EvolucaoMedica evolucaomedica in evolucoesmedica)
            {
                foreach (string co_cid in evolucaomedica.CodigosCids)
                    cids.Add(iCid.BuscarPorCodigo<Cid>(co_cid));
            }

            if (cid.ToString() != "")
                cids = cids.Where(p => p.Codigo == cid.ToString()).ToList();

            var consulta = from c in cids
                           group c by c.Codigo into res
                           select new
                           {
                               res.Key,
                               quantidade = res.Count()
                           };

            DataTable tab = new DataTable();
            tab.Columns.Add("Codigo", typeof(string));
            tab.Columns.Add("Nome", typeof(string));
            tab.Columns.Add("Quantidade", typeof(string));

            foreach (var item in consulta)
            {
                DataRow linha = tab.NewRow();
                linha["Codigo"] = item.Key;
                linha["Nome"] = cids.Where(p => p.Codigo == item.Key).First().Nome;
                linha["Quantidade"] = item.quantidade;
                tab.Rows.Add(linha);
            }

            return tab;
        }
        DataTable IRelatorioUrgencia.ObterRelatorioAtendimentoFaixa(string id_unidade, string sexo, DateTime dataInicio, DateTime dataFim)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.CodigoUnidade = '" + id_unidade + "'" +
            " AND p.FaixaEtaria.Codigo IS NOT NULL AND p.Data BETWEEN "
            + "to_date('" + dataInicio.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')";
            hql += " AND to_date('" + dataFim.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";

            IList<ViverMais.Model.Prontuario> prontuarios = Session.CreateQuery(hql).List<ViverMais.Model.Prontuario>();
            DataTable tabela = new DataTable();
            tabela.Columns.Add(new DataColumn("Sexo", typeof(string)));
            tabela.Columns.Add(new DataColumn("FaixaEtaria", typeof(string)));
            tabela.Columns.Add(new DataColumn("Qtd", typeof(string)));

            foreach (ViverMais.Model.Prontuario prontuario in prontuarios)
            {
                if (!string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais))
                    prontuario.Paciente.PacienteViverMais = Factory.GetInstance<IPaciente>().BuscarPorCodigo<ViverMais.Model.Paciente>(prontuario.Paciente.CodigoViverMais);
            }

            IList<ViverMais.Model.FaixaEtaria> faixas = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.FaixaEtaria>();

            if (sexo.ToUpper() == "AMBOS")
            {
                foreach (ViverMais.Model.FaixaEtaria f in faixas)
                {
                    DataRow linha = tabela.NewRow();
                    linha["Sexo"] = "Masculino";
                    linha["FaixaEtaria"] = f.Inicial.ToString() + "-" + f.Final.ToString();
                    linha["Qtd"] = prontuarios.Where(p => p.FaixaEtaria.Codigo == f.Codigo && ((p.Paciente.PacienteViverMais != null && p.Paciente.PacienteViverMais.Sexo == 'M') || (p.Paciente.Sexo != '\0' && p.Paciente.Sexo == 'M'))).ToList().Count();
                    tabela.Rows.Add(linha);
                }

                foreach (ViverMais.Model.FaixaEtaria f in faixas)
                {
                    DataRow linha = tabela.NewRow();
                    linha["Sexo"] = "Feminino";
                    linha["FaixaEtaria"] = f.Inicial.ToString() + "-" + f.Final.ToString();
                    linha["Qtd"] = prontuarios.Where(p => p.FaixaEtaria.Codigo == f.Codigo && ((p.Paciente.PacienteViverMais != null && p.Paciente.PacienteViverMais.Sexo == 'F') || (p.Paciente.Sexo != '\0' && p.Paciente.Sexo == 'F'))).ToList().Count();
                    tabela.Rows.Add(linha);
                }
            }
            else
            {
                IList<ViverMais.Model.Prontuario> lptemp = prontuarios.Where(p => (p.Paciente.PacienteViverMais != null && p.Paciente.PacienteViverMais.Sexo.ToString() == sexo) || (p.Paciente.Sexo != '\0' && p.Paciente.Sexo.ToString() == sexo)).ToList();

                foreach (ViverMais.Model.FaixaEtaria f in faixas)
                {
                    DataRow linha = tabela.NewRow();
                    linha["Sexo"] = sexo == "M" ? "Masculino" : "Feminino";
                    linha["FaixaEtaria"] = f.Inicial.ToString() + "-" + f.Final.ToString();
                    linha["Qtd"] = lptemp.Where(p => p.FaixaEtaria.Codigo == f.Codigo).ToList().Count();
                    tabela.Rows.Add(linha);
                }
            }

            return tabela;
        }

        IList IRelatorioUrgencia.ObterRelatorioLeitosFaixa(string id_unidade)
        {
            string hql = "Select vaga.Prontuario.FaixaEtaria.Inicial, vaga.Prontuario.FaixaEtaria.Final, count(vaga.CodigoUnidade) ";
            hql += " from ViverMais.Model.VagaUrgencia vaga";
            hql += " where vaga.Prontuario is not null and vaga.CodigoUnidade = '" + id_unidade + "' group by vaga.Prontuario.FaixaEtaria.Inicial, vaga.Prontuario.FaixaEtaria.Final, vaga.CodigoUnidade";
            return Session.CreateQuery(hql).List();
        }
        IList IRelatorioUrgencia.ObterRelatorioAbsenteismo(string id_unidade, DateTime data_inicio, DateTime data_fim)
        {
            string hql = "Select prontuario.Numero, prontuario.Data";
            hql += " from ViverMais.Model.Prontuario prontuario";
            hql += " where prontuario.CodigoUnidade='" + id_unidade + "'";
            hql += " and prontuario.Situacao.Codigo='" + SituacaoAtendimento.EVASAO + "'";
            hql += " and prontuario.Data between TO_DATE('" + data_inicio.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI') and ";
            hql += "TO_DATE('" + data_fim.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            hql += " order by prontuario.Data";
            return Session.CreateQuery(hql).List();
        }
        IList IRelatorioUrgencia.ObterRelatorioAtendimentoPorFaixa(long id_unidade, string sexo, DateTime dataInicio, DateTime dataFim)
        {
            string hql = "Select prontuario.FaixaEtaria.Codigo, prontuario.Paciente.Codigo, count(prontuario.Codigo)";
            hql += " from ViverMais.Model.Prontuario prontuario ";
            hql += " where prontuario.CodigoUnidade='" + id_unidade + "'";
            if (!sexo.Equals("Ambos"))
                hql += " and prontuario.Paciente.Sexo='" + sexo + "'";
            hql += " and prontuario.FaixaEtaria.Codigo != ''";
            hql += " and prontuario.Paciente.Sexo IS NOT NULL";
            hql += " and prontuario.Data between '" + dataInicio.ToString("yyyy-MM-dd") + "' and '" + dataFim.ToString("yyyy-MM-dd") + "'";
            hql += " Group by prontuario.FaixaEtaria.Codigo, prontuario.Paciente.Codigo";

            return Session.CreateQuery(hql).List();
        }

        Hashtable IRelatorioUrgencia.ObterRelatorioTempoPermanencia(DateTime dataInicial, DateTime dataFinal, string id_unidade)
        {
            Hashtable hash = new Hashtable();
            DataTable corpo = new DataTable();
            corpo.Columns.Add(new DataColumn("CodigoUnidade", typeof(string)));
            corpo.Columns.Add(new DataColumn("Atendimento", typeof(string)));
            corpo.Columns.Add(new DataColumn("Paciente", typeof(string)));
            corpo.Columns.Add(new DataColumn("HorarioAtendimento", typeof(string)));
            corpo.Columns.Add(new DataColumn("SituacaoPaciente", typeof(string)));
            corpo.Columns.Add(new DataColumn("TempoAcolhimento", typeof(string)));
            corpo.Columns.Add(new DataColumn("TempoConsultaMedica", typeof(string)));
            corpo.Columns.Add(new DataColumn("TempoFinalizacao", typeof(string)));
            corpo.Columns.Add(new DataColumn("TempoTotal", typeof(string)));

            DataTable cabecalho = new DataTable();
            cabecalho.Columns.Add(new DataColumn("Unidade", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("DataInicio", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("DataFim", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("CodigoUnidade", typeof(string)));

            DataRow r = cabecalho.NewRow();
            r["CodigoUnidade"] = id_unidade;
            r["Unidade"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(id_unidade).NomeFantasia;
            r["DataInicio"] = dataInicial.ToString("dd/MM/yyyy");
            r["DataFim"] = dataFinal.ToString("dd/MM/yyyy");
            cabecalho.Rows.Add(r);

            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Data BETWEEN ";
            hql += "to_date('" + dataInicial.ToString("dd/MM/yyyy") + " 00:00:00','DD/MM/YYYY HH24:MI:SS')";
            hql += " AND to_date('" + dataFinal.ToString("dd/MM/yyyy") + " 23:59:59','DD/MM/YYYY HH24:MI:SS')";
            hql += " AND p.CodigoUnidade='" + id_unidade + "'";
            hql += " ORDER BY p.Data";
            IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>();

            //int horasacolhimento;
            //int horasconsultamedica;
            //int horasfinalizacao;
            //string temp;

            foreach (Prontuario prontuario in prontuarios)
            {
                //horasacolhimento = -1; horasconsultamedica = -1; horasfinalizacao = -1;
                r = corpo.NewRow();
                r["CodigoUnidade"] = id_unidade;
                r["Atendimento"] = prontuario.NumeroToString;
                r["Paciente"] = string.IsNullOrEmpty(prontuario.Paciente.CodigoViverMais) ? prontuario.Paciente.Nome : PacienteBLL.Pesquisar(prontuario.Paciente.CodigoViverMais).Nome;
                r["HorarioAtendimento"] = prontuario.Data.ToString("dd/MM/yyyy HH:mm:ss");
                r["SituacaoPaciente"] = prontuario.Situacao.Nome;
                r["TempoAcolhimento"] = prontuario.TempoAcolhimento;
                r["TempoConsultaMedica"] = prontuario.TempoConsultaMedica;
                r["TempoFinalizacao"] = prontuario.TempoFinalizacao;
                r["TempoTotal"] = prontuario.TempoUniaoAcolhimentoConsultaFinalizacao;

                //if (horasacolhimento < 0 && horasconsultamedica < 0 && horasfinalizacao < 0)
                //    r["TempoTotal"] = " - ";
                //else
                //{
                //    int horastotal = 0;
                //    horastotal += (horasacolhimento > -1 ? horasacolhimento : 0) + (horasconsultamedica > -1 ? horasconsultamedica : 0) + (horasfinalizacao > -1 ? horasfinalizacao : 0);
                //    temp = horastotal > 59 ? (horastotal % 60).ToString() : horastotal.ToString();

                //    r["TempoTotal"] = (horastotal / 60).ToString() + " horas e " + temp + " minutos";
                //}

                corpo.Rows.Add(r);
            }
            //hql = "select p.Codigo, p.Data, p.DataConsultaMedica " +
            //"from ViverMais.Model.Prontuario p " +
            //"where p.CodigoUnidade='" + id_unidade + "' and p.Data between '" + dataInicial.ToString("yyyy-MM-dd") + "' and '" + dataFinal.ToString("yyyy-MM-dd") + "' " +
            //"order by p.Codigo, p.Data";
            //"select p.Codigo, p.Data, p.DataAlteracaoEnfermagem, max(pm.Data) " +
            //"from ViverMais.Model.ProntuarioMedico pm right outer join pm.Prontuario p " +
            //"where p.CodigoUnidade='" + id_unidade + "' and p.Data between '" + dataInicial.ToString("yyyy-MM-dd") + "' and '" + dataFinal.ToString("yyyy-MM-dd") + "' " +
            //"group by p.Codigo, p.Data, p.DataAlteracaoEnfermagem";

            hash.Add("corpo", corpo);
            hash.Add("cabecalho", cabecalho);
            return hash;
        }
        Hashtable IRelatorioUrgencia.RetornaHashTableProcedencia(string co_unidade, string co_cid, DateTime datainicio, DateTime datafim)
        {
            Hashtable hash = new Hashtable();

            DataTable cabecalho = new DataTable();
            cabecalho.Columns.Add(new DataColumn("Unidade", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("CodigoUnidade", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("Cid", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("DataInicio", typeof(string)));
            cabecalho.Columns.Add(new DataColumn("DataFim", typeof(string)));

            DataRow r = cabecalho.NewRow();
            r["DataInicio"] = datainicio.ToString("dd/MM/yyyy");
            r["DataFim"] = datafim.ToString("dd/MM/yyyy");
            r["CodigoUnidade"] = co_unidade;
            r["Unidade"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade).NomeFantasia;
            r["Cid"] = Factory.GetInstance<ICid>().BuscarPorCodigo<ViverMais.Model.Cid>(co_cid).Nome;
            cabecalho.Rows.Add(r);

            DataTable corpo = new DataTable();
            corpo.Columns.Add(new DataColumn("Quantidade", typeof(string)));
            corpo.Columns.Add(new DataColumn("Bairro", typeof(string)));
            corpo.Columns.Add(new DataColumn("Distrito", typeof(string)));
            corpo.Columns.Add(new DataColumn("CodigoUnidade", typeof(string)));

            string hql = string.Empty;

            //hql = "FROM ViverMais.Model.Prontuario AS p WHERE p.Data BETWEEN to_date('" + new DateTime(datainicio.Year, datainicio.Month, datainicio.Day, 0, 0, 0).ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            //hql += " AND to_date('" + new DateTime(datafim.Year, datafim.Month, datafim.Day, 23, 59, 59).ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            //hql += " AND p.CodigoUnidade = '" + co_unidade + "'";
            //hql += " AND p.Paciente.CodigoViverMais IS NOT NULL AND LENGTH(p.Paciente.CodigoViverMais) <> 0";
            //IList<Prontuario> prontuarios = Session.CreateQuery(hql).List<Prontuario>().Where(p => p.CodigosCids.Contains(co_cid)).ToList();

            hql = "FROM ViverMais.Model.EvolucaoMedica AS e WHERE e.Data BETWEEN to_date('" + new DateTime(datainicio.Year, datainicio.Month, datainicio.Day, 0, 0, 0).ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            hql += " AND to_date('" + new DateTime(datafim.Year, datafim.Month, datafim.Day, 23, 59, 59).ToString("dd/MM/yyyy HH:mm:ss") + "','DD/MM/YYYY HH24:MI:SS')";
            hql += " AND e.Prontuario.CodigoUnidade = '" + co_unidade + "'";
            hql += " AND e.Prontuario.Paciente.CodigoViverMais IS NOT NULL AND LENGTH(e.Prontuario.Paciente.CodigoViverMais) <> 0";
            IList<EvolucaoMedica> evolucoesmedica = Session.CreateQuery(hql).List<EvolucaoMedica>().Where(p => p.CodigosCids.Contains(co_cid)).ToList();

            IList<Bairro> lb = new List<Bairro>();
            IEndereco iEndereco = Factory.GetInstance<IEndereco>();
            IBairro iBairro = Factory.GetInstance<IBairro>();

            //foreach (Prontuario p in prontuarios)
            //{
            //    Endereco end = iEndereco.BuscarPorPaciente<Endereco>(p.Paciente.CodigoViverMais);
            //    Bairro b = iBairro.ListarPorCidade<Bairro>(end.Municipio.Codigo).Where(pt => pt.Nome == end.Bairro).FirstOrDefault();

            //    if (b != null)
            //        lb.Add(b);
            //}

            foreach (EvolucaoMedica e in evolucoesmedica)
            {
                Endereco end = iEndereco.BuscarPorPaciente<Endereco>(e.Prontuario.Paciente.CodigoViverMais);
                Bairro b = iBairro.ListarPorCidade<Bairro>(end.Municipio.Codigo).Where(pt => pt.Nome == end.Bairro).FirstOrDefault();

                if (b != null)
                    lb.Add(b);
            }

            var consulta = from b in lb
                           group b by new { BA = b.Nome, DIS = b.Distrito.Codigo } into x
                           select new { NomeBairro = x.Key.BA, CodigoDistrito = x.Key.DIS, Quantidade = x.Count() };

            IDistrito iDistrito = Factory.GetInstance<IDistrito>();
            foreach (var item in consulta)
            {
                r = corpo.NewRow();
                r["CodigoUnidade"] = co_unidade;
                r["Quantidade"] = item.Quantidade;
                r["Bairro"] = item.NomeBairro;
                r["Distrito"] = iDistrito.BuscarPorCodigo<Distrito>(item.CodigoDistrito).Nome;

                corpo.Rows.Add(r);
            }

            hash.Add("corpo", corpo);
            hash.Add("cabecalho", cabecalho);

            return hash;
        }
        //Hashtable IRelatorioUrgencia.RetornarHashTableAprazamento(long co_prescricao, DateTime data)
        //{
        //    IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
        //    Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(co_prescricao);
        //    IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

        //    Hashtable hash = new Hashtable();

        //    DataTable corpo = new DataTable();
        //    corpo.Columns.Add("nome", typeof(string));
        //    corpo.Columns.Add("prescricao", typeof(string));

        //    for (int i = 0; i < 48; i++)
        //        corpo.Columns.Add(i.ToString(), typeof(string));

        //    IList<AprazamentoMedicamento> medicamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, data);
        //    var _medicamentos = from _medicamento in medicamentos
        //                        select new
        //                        {
        //                            _Codigo = "MED_" + _medicamento.Medicamento.Codigo,
        //                            _Prescricao = _medicamento.Prescricao,
        //                            _Nome = _medicamento.Medicamento.Nome,
        //                            _Horario = _medicamento.Horario,
        //                            _Profissional = _medicamento.Profissional
        //                        };

        //    IList<AprazamentoProcedimento> procedimentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, data);
        //    var _procedimentos = from _procedimento in procedimentos
        //                         select new
        //                         {
        //                             _Codigo = "PROC_" + _procedimento.Procedimento.Codigo,
        //                             _Prescricao = _procedimento.Prescricao,
        //                             _Nome = _procedimento.Procedimento.Nome,
        //                             _Horario = _procedimento.Horario,
        //                             _Profissional = _procedimento.Profissional
        //                         };

        //    IList<AprazamentoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, data);
        //    var _procedimentosnaofaturaveis = from _procedimento in procedimentosnaofaturaveis
        //                                      select new
        //                                      {
        //                                          _Codigo = "PROCNF_" + _procedimento.ProcedimentoNaoFaturavel.Codigo,
        //                                          _Prescricao = _procedimento.Prescricao,
        //                                          _Nome = _procedimento.ProcedimentoNaoFaturavel.Nome,
        //                                          _Horario = _procedimento.Horario,
        //                                          _Profissional = _procedimento.Profissional
        //                                      };

        //    var _aprazamentosconcatenados = _procedimentos.Concat(_procedimentosnaofaturaveis).Concat(_medicamentos);
        //    var _aprazamentos =
        //        from _aprazamento in
        //            _aprazamentosconcatenados
        //        orderby _aprazamento._Nome
        //        group _aprazamento by _aprazamento._Codigo into g
        //        select g;

        //    IList<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
        //    DataTable responsaveis = new DataTable();
        //    responsaveis.Columns.Add("nome", typeof(string));

        //    foreach (var aprazamento in _aprazamentos)
        //    {
        //        var listaordenada = aprazamento.OrderBy(p => p._Horario).ToList();
        //        DataRow row = corpo.NewRow();
        //        row["prescricao"] = prescricao.Codigo;
        //        row["nome"] = listaordenada.First()._Nome;

        //        foreach (var conteudo in listaordenada)
        //        {
        //            int pos = conteudo._Horario.Hour * 2;

        //            if (conteudo._Horario.Minute != 0)
        //                row[(pos + 1).ToString()] = "X";
        //            else
        //                row[pos.ToString()] = "X";

        //            profissionais.Add(conteudo._Profissional);
        //        }

        //        corpo.Rows.Add(row);
        //    }

        //    profissionais = profissionais.Distinct(new GenericComparer<ViverMais.Model.Profissional>("CPF")).ToList();
        //    foreach (ViverMais.Model.Profissional profissional in profissionais)
        //    {
        //        DataRow row = responsaveis.NewRow();
        //        row["nome"] = profissional.Nome;
        //        responsaveis.Rows.Add(row);
        //    }

        //    IProntuario iProntuario = Factory.GetInstance<IProntuario>();

        //    hash.Add("cabecalho", this.RetornarCabecalhoRelatorioProntuario(Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(prescricao.Prontuario.Codigo)));
        //    hash.Add("corpo", corpo);
        //    hash.Add("responsaveis", responsaveis);

        //    return hash;
        //}

        bool IRelatorioUrgencia.PAAtivo(string cnes)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ConfiguracaoPA configuracao WHERE configuracao.Estabelecimento.CNES = '" + cnes + "'";
            return Session.CreateQuery(hql).List<ConfiguracaoPA>().Count > 0;
        }

        IList<T> IRelatorioUrgencia.ObterRelatorioPorSituacao<T>(object id_unidade, object id_situacao)
        {
            string hql =
                "from ViverMais.Model.Prontuario prontuario" +
                " where prontuario.Situacao.Codigo=" + id_situacao +
                " and prontuario.CodigoUnidade='" + id_unidade + "'" +
                " order by prontuario.Data";
            return Session.CreateQuery(hql).List<T>();
        }
        IList<T> IRelatorioUrgencia.EstabelecimentosAtivos<T>()
        {
            string hql = string.Empty;
            hql = @"SELECT estabelecimento FROM ViverMais.Model.ConfiguracaoPA configuracao 
                    LEFT OUTER JOIN configuracao.Estabelecimento estabelecimento 
                    ORDER BY estabelecimento.NomeFantasia";

            return Session.CreateQuery(hql).List<T>();
        }
        IList<T> IRelatorioUrgencia.DistritosAtivos<T>()
        {
            return Session.CreateQuery("SELECT d FROM ViverMais.Model.Distrito d WHERE d IN (SELECT DISTINCT unidade.Bairro.Distrito FROM ViverMais.Model.ConfiguracaoPA pa, ViverMais.Model.EstabelecimentoSaude unidade WHERE pa.Estabelecimento.CNES = unidade.CNES) ORDER BY d.Nome").List<T>();
        }

        string IRelatorioUrgencia.GraficoFilaAtendimentoUnidade(string cnes)
        {
            IList<ViverMais.Model.Prontuario> fila = Factory.GetInstance<IProntuario>().BuscarFilaAtendimentoUnidade<ViverMais.Model.Prontuario>(cnes);

            string strXML = string.Empty;
            strXML += "<graph caption='Atendimento' xAxisName='Classificação de Risco' yAxisName='Quantidade' showPercentageValues='0' decimalPrecision='0' formatNumberScale='0'>";

            IList<ClassificacaoRisco> lc = Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ClassificacaoRisco>().OrderBy(p => p.Ordem).ToList();

            foreach (ClassificacaoRisco c in lc)
                strXML += "<set name='" + c.Descricao + "' hoverText='" + c.Descricao + "' value='" + fila.Where(p => p.ClassificacaoRisco.Codigo == c.Codigo).Count() + "' color='" + c.CorGrafico + "' />";

            strXML += "</graph>";

            return FusionCharts.RenderChartHTML("FusionCharts/FCF_Column3D.swf", "", strXML, "myNext", "600", "300", false);
        }

        T IRelatorioUrgencia.GerarBPAI<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            IRegistro iRegistro = Factory.GetInstance<IRegistro>();
            IList<AprazamentoProcedimento> aprazamentos = Factory.GetInstance<IAprazamento>().BuscarAprazamentosBPAI<AprazamentoProcedimento>(co_unidade, competencia, datainicio, datalimite);

            var agruparaprazamentos = from _aprazamento in aprazamentos
                                      //where UsuarioProfissionalUrgence.isMedico(iViverMais.BuscarPorCodigo<CBO>(_aprazamento.CBOProfissionalConfirmacao)) == true
                                      group _aprazamento by new
                                      {
                                          CodigoProcedimento = _aprazamento.CodigoProcedimento,
                                          CodigoPaciente = _aprazamento.Prescricao.Prontuario.Paciente.CodigoViverMais,
                                          CBOProfissional = _aprazamento.CBOProfissionalConfirmacao,
                                          CodigoProfissional = _aprazamento.CodigoProfissionalConfirmacao,
                                          DataConfirmacao = _aprazamento.HorarioConfirmacao.Value.ToString("dd/MM/yyyy")
                                      }
                                          into grupo
                                          select grupo;

            ArquivoBPA arquivoBPA = new ArquivoBPA();
            arquivoBPA.Competencia = competencia;
            arquivoBPA.Unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            arquivoBPA.Tipo = ViverMais.Model.BPA.INDIVIDUALIZADO;

            foreach (var grupo in agruparaprazamentos)
            {
                AprazamentoProcedimento aprazamento = grupo.First();
                IList<ProcedimentoRegistro> procedimentoregistro = iRegistro.BuscarPorProcedimento<ProcedimentoRegistro>(aprazamento.CodigoProcedimento, Registro.BPA_INDIVIDUALIZADO);//02 - Codigo Individualizado

                if (procedimentoregistro.Count != 0)
                {
                    BpaIndividualizado bpaIndividualizado = new BpaIndividualizado();
                    ViverMais.Model.Paciente paciente = PacienteBLL.Pesquisar(aprazamento.Prescricao.Prontuario.Paciente.CodigoViverMais);
                    ViverMais.Model.Profissional profissional = iViverMais.BuscarPorCodigo<ViverMais.Model.Profissional>(aprazamento.CodigoProfissionalConfirmacao);

                    bpaIndividualizado.CnsMedico = !string.IsNullOrEmpty(profissional.CartaoSUS) ? profissional.CartaoSUS : "               ";
                    bpaIndividualizado.Cbo = iViverMais.BuscarPorCodigo<CBO>(aprazamento.CBOProfissionalConfirmacao);
                    bpaIndividualizado.DataAtendimento = aprazamento.HorarioConfirmacao.Value;
                    bpaIndividualizado.Procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                    bpaIndividualizado.CnsPaciente = CartaoSUSBLL.ListarPorPaciente(paciente).Last().Numero;
                    bpaIndividualizado.Cid = aprazamento.CodigoCid;
                    bpaIndividualizado.Quantidade = grupo.Count();
                    bpaIndividualizado.NumeroAutorizacao = string.Empty;
                    bpaIndividualizado.Paciente = paciente;
                    Endereco endereco = EnderecoBLL.PesquisarCompletoPorPaciente(paciente);
                    if (endereco != null && endereco.Municipio != null)
                        bpaIndividualizado.CodigoMunicipioResidencia = endereco.Municipio.Codigo;
                    else
                        bpaIndividualizado.CodigoMunicipioResidencia = "292740";

                    arquivoBPA.Bpas.Add(bpaIndividualizado);
                }
            }

            return (T)(object)arquivoBPA;
        }
        T IRelatorioUrgencia.GerarBPAC<T>(string co_unidade, int competencia, DateTime datainicio, DateTime datalimite)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();

            IRegistro iRegistro = Factory.GetInstance<IRegistro>();
            IList<AprazamentoProcedimento> aprazamentos = Factory.GetInstance<IAprazamento>().BuscarAprazamentosBPAC<AprazamentoProcedimento>(co_unidade, competencia, datainicio, datalimite);

            var agruparaprazamentos = from _aprazamento in aprazamentos
                                      group _aprazamento by new
                                      {
                                          CodigoProcedimento = _aprazamento.CodigoProcedimento,
                                          CBOProfissional = _aprazamento.CBOProfissionalConfirmacao,
                                          Idade = _aprazamento.Prescricao.Prontuario.Idade
                                      }
                                          into grupo
                                          select grupo;

            ArquivoBPA arquivoBPA = new ArquivoBPA();
            arquivoBPA.Competencia = competencia;
            arquivoBPA.Unidade = iViverMais.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(co_unidade);
            arquivoBPA.Tipo = ViverMais.Model.BPA.CONSOLIDADO;

            foreach (var grupo in agruparaprazamentos)
            {
                AprazamentoProcedimento aprazamento = grupo.First();
                IList<ProcedimentoRegistro> procedimentoregistro = iRegistro.BuscarPorProcedimento<ProcedimentoRegistro>(aprazamento.CodigoProcedimento, Registro.BPA_CONSOLIDADO);//01 - Codigo Consolidado

                if (procedimentoregistro.Count != 0)
                {
                    BpaConsolidado bpaConsolidado = new BpaConsolidado();
                   
                    bpaConsolidado.Cbo = iViverMais.BuscarPorCodigo<CBO>(aprazamento.CBOProfissionalConfirmacao);
                    bpaConsolidado.Procedimento = iViverMais.BuscarPorCodigo<ViverMais.Model.Procedimento>(aprazamento.CodigoProcedimento);
                    bpaConsolidado.Quantidade = grupo.Count();
                    bpaConsolidado.Idade = grupo.Key.Idade;

                    arquivoBPA.Bpas.Add(bpaConsolidado);
                }
            }

            return (T)(object)arquivoBPA;
        }

        ReportDocument IRelatorioUrgencia.ObterRelatorioAcolhimento(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSRelAcolhimentoProntuario DsAcolhimento = new DSRelAcolhimentoProntuario();
            DSCabecalho DsCabecalho = new DSCabecalho();

            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);
            DataTable DtAcolhimento = this.RetornarDataTableAcolhimento(co_prontuario);

            DsAcolhimento.Tables.Add(DtAcolhimento);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelAcolhimentoProntuario.rpt"));
            relatorio.SetDataSource(DsAcolhimento.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCabecalho.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioConsultaMedica(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSEvolucaoMedica DsConsultaMedica = new DSEvolucaoMedica();
            DSCabecalho DsCabecalho = new DSCabecalho();
            DSCids DsCids = new DSCids();

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
            Hashtable HtConsultaMedica = this.RetornarHashTableConsultaMedica(prontuario.Codigo);

            DataTable DtConsultaMedica = (DataTable)HtConsultaMedica["consulta"];
            DataTable DtCids = (DataTable)HtConsultaMedica["cids"];

            DsConsultaMedica.Tables.Add(DtConsultaMedica);
            DsCids.Tables.Add(DtCids);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelConsultaMedicaProntuario.rpt"));
            relatorio.SetDataSource(DsConsultaMedica.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCabecalho.Tables[1]);
            relatorio.Subreports[1].SetDataSource(DsCids.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioEvolucoesMedicas(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSEvolucaoMedica DsEvolucaoMedica = new DSEvolucaoMedica();
            DSCabecalho DsCabecalho = new DSCabecalho();
            DSCids DsCids = new DSCids();

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
            Hashtable HtEvolucoesMedicas = this.RetornarHashTableEvolucoesMedicas(prontuario.Codigo);

            DataTable DtEvolucaoMedica = (DataTable)HtEvolucoesMedicas["evolucoes"];
            DataTable DtCids = (DataTable)HtEvolucoesMedicas["cids"];

            DsEvolucaoMedica.Tables.Add(DtEvolucaoMedica);
            DsCids.Tables.Add(DtCids);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelEvolucoesMedica.rpt"));
            relatorio.SetDataSource(DsEvolucaoMedica.Tables[1]);
            relatorio.Subreports["RelCabecalho.rpt"].SetDataSource(DsCabecalho.Tables[1]);
            relatorio.Subreports["RelCids.rpt"].SetDataSource(DsCids.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioEvolucoesEnfermagem(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSEvolucaoEnfermagem DsEvolucaoEnfermagem = new DSEvolucaoEnfermagem();
            DSCabecalho DsCabecalho = new DSCabecalho();

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
            DataTable DtEvolucaoEnfermagem = this.RetornarDataTableEvolucoesEnfermagem(prontuario.Codigo);

            DsEvolucaoEnfermagem.Tables.Add(DtEvolucaoEnfermagem);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelEvolucoesEnfermagem.rpt"));
            relatorio.SetDataSource(DsEvolucaoEnfermagem.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCabecalho.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioExamesInternos(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSExames DsExames = new DSExames();
            DSCabecalho DsCabecalho = new DSCabecalho();

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
            DataTable DtExames = this.RetornarDataTableExamesInternos(prontuario.Codigo);

            DsExames.Tables.Add(DtExames);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelExamesProntuario.rpt"));
            relatorio.SetDataSource(DsExames.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCabecalho.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioPrescricoes(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSPrescricao DsPrescricao = new DSPrescricao();
            DSPrescricaoMedicamento DsMedicamentos = new DSPrescricaoMedicamento();
            DSPrescricaoProcedimento DsProcedimentos = new DSPrescricaoProcedimento();
            DSPrescricaoProcedimentoNaoFaturavel DsProcedimentosNaoFaturaveis = new DSPrescricaoProcedimentoNaoFaturavel();
            DSCabecalho DsCabecalho = new DSCabecalho();

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorCodigo<Prontuario>(co_prontuario);
            Hashtable HtPrescricoes = this.RetornarHashTablePrescricoes(prontuario.Codigo);
            DataTable DtPrescricao = (DataTable)HtPrescricoes["prescricoes"];
            DataTable DtMedicamento = (DataTable)HtPrescricoes["medicamentos"];
            DataTable DtProcedimento = (DataTable)HtPrescricoes["procedimentos"];
            DataTable DtProcedimentoNaoFaturavel = (DataTable)HtPrescricoes["procedimentosnaofaturaveis"];

            DsPrescricao.Tables.Add(DtPrescricao);
            DsMedicamentos.Tables.Add(DtMedicamento);
            DsProcedimentos.Tables.Add(DtProcedimento);
            DsProcedimentosNaoFaturaveis.Tables.Add(DtProcedimentoNaoFaturavel);
            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioProntuario(prontuario));

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelPrescricoesProntuario.rpt"));
            relatorio.SetDataSource(DsPrescricao.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCabecalho.Tables[1]);
            relatorio.Subreports[1].SetDataSource(DsMedicamentos.Tables[1]);
            relatorio.Subreports[2].SetDataSource(DsProcedimentos.Tables[1]);
            relatorio.Subreports[3].SetDataSource(DsProcedimentosNaoFaturaveis.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioGeral(long co_prontuario, int co_usuario)
        {
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            ReportDocument relatorio = new ReportDocument();
            UsuarioProfissionalUrgence usuario = Factory.GetInstance<IUsuarioProfissionalUrgence>().BuscarPorCodigo<UsuarioProfissionalUrgence>(co_usuario);

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelRelatorioGeralProntuario.rpt"));

            if (usuario != null)
            {
                Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);

                DataTable DtCabecalho = this.RetornarCabecalhoRelatorioProntuario(prontuario);
                DataTable DtAcolhimento = this.RetornarDataTableAcolhimento(prontuario.Codigo);
                DataTable DtEvolucoesEnfermagem = this.RetornarDataTableEvolucoesEnfermagem(prontuario.Codigo);
                DataTable DtExamesInternos = this.RetornarDataTableExamesInternos(prontuario.Codigo);
                DataTable DtClassificacoesRisco = this.RetornarClassificacoesRisco(co_prontuario);
                Hashtable HtConsultaMedica = this.RetornarHashTableConsultaMedica(prontuario.Codigo);
                Hashtable HtEvolucoesMedicas = this.RetornarHashTableEvolucoesMedicas(prontuario.Codigo);
                Hashtable HtPrescricoes = this.RetornarHashTablePrescricoes(prontuario.Codigo);

                relatorio.Subreports["RelCabecalho.rpt"].SetDataSource(DtCabecalho); //cabeçalho
                relatorio.Subreports["RelAcolhimentoProntuario.rpt"].SetDataSource(DtAcolhimento); //acolhimento
                relatorio.Subreports["RelEvolucoesEnfermagem.rpt"].SetDataSource(DtEvolucoesEnfermagem); //evolução enfermagem
                relatorio.Subreports["RelExamesProntuario.rpt"].SetDataSource(DtExamesInternos); //exame prontuário
                relatorio.Subreports["RelClassificacoesRisco.rpt"].SetDataSource(DtClassificacoesRisco); //classificacoes de risco

                relatorio.Subreports["RelConsultaMedicaRelatorioGeral.rpt"].Database.Tables["evolucao"].SetDataSource((DataTable)HtConsultaMedica["consulta"]); //consulta médica anamnese
                relatorio.Subreports["RelConsultaMedicaRelatorioGeral.rpt"].Database.Tables["cids"].SetDataSource((DataTable)HtConsultaMedica["cids"]); //consulta médica cids

                relatorio.Subreports["RelEvolucaoMedicaRelatorioGeral.rpt"].Database.Tables["evolucao"].SetDataSource((DataTable)HtEvolucoesMedicas["evolucoes"]);
                relatorio.Subreports["RelEvolucaoMedicaRelatorioGeral.rpt"].Database.Tables["cids"].SetDataSource((DataTable)HtEvolucoesMedicas["cids"]);

                relatorio.Database.Tables[0].SetDataSource((DataTable)HtPrescricoes["prescricoes"]);
                relatorio.Subreports["RelPrescricaoMedicamento.rpt"].SetDataSource((DataTable)HtPrescricoes["medicamentos"]);
                relatorio.Subreports["RelPrescricaoProcedimento.rpt"].SetDataSource((DataTable)HtPrescricoes["procedimentos"]);
                relatorio.Subreports["RelPrescricaoProcedimentoNaoFaturavel.rpt"].SetDataSource((DataTable)HtPrescricoes["procedimentosnaofaturaveis"]);

                VinculoProfissional vinculo = Factory.GetInstance<IVinculo>().BuscarPorVinculoProfissional<VinculoProfissional>(usuario.UnidadeVinculo, usuario.Id_Profissional, usuario.CodigoCBO)[0];

                relatorio.SetParameterValue("@profissional", vinculo.Profissional.Nome);
                relatorio.SetParameterValue("@crmprofissional", string.IsNullOrEmpty(vinculo.RegistroConselho) ? "CRM não identificado" : vinculo.RegistroConselho);
            }

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioFichaAtendimento(long numeroprontuario)
        {
            ReportDocument relatorio = new ReportDocument();
            DSFichaAtendimentoProntuario DsFichaAtendimento = new DSFichaAtendimentoProntuario();

            DataTable DtFichaAtendimento = this.RetornarDataTableFichaAtendimento(numeroprontuario);

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelFichaAtendimentoProntuario.rpt"));
            DsFichaAtendimento.Tables.Add(DtFichaAtendimento);
            relatorio.SetDataSource(DsFichaAtendimento.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioAprazados(long co_prontuario)
        {
            ReportDocument relatorio = new ReportDocument();

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelItensAprazadosPrescricao.rpt"));

            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            Prontuario prontuario   = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);

            DataTable DtCabecalho = this.RetornarCabecalhoRelatorioProntuario(prontuario);
            Hashtable HtAprazamentos = this.RetornarHashTableAprazamentos(prontuario.Codigo);

            relatorio.Subreports["RelCabecalho.rpt"].SetDataSource(DtCabecalho); //cabeçalho

            relatorio.Database.Tables[0].SetDataSource((DataTable)HtAprazamentos["prescricoes"]);
            relatorio.Subreports["Inc_MedicamentosAprazados.rpt"].SetDataSource((DataTable)HtAprazamentos["medicamentos"]);
            relatorio.Subreports["Inc_ProcedimentosAprazados.rpt"].SetDataSource((DataTable)HtAprazamentos["procedimentos"]);
            relatorio.Subreports["Inc_ProcedimentosNaoFaturaveisAprazados.rpt"].SetDataSource((DataTable)HtAprazamentos["procedimentosnaofaturaveis"]);

            return relatorio;
        }
        ReportDocument IRelatorioUrgencia.ObterRelatorioAprazados(long co_prescricao, DateTime data)
        {
            ReportDocument relatorio = new ReportDocument();
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            Hashtable aprazados = this.RetornarHashTableAprazados(co_prescricao, data);

            Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(co_prescricao);
            Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(prescricao.Prontuario.Codigo);

            relatorio.Load(System.Web.Hosting.HostingEnvironment.MapPath("~/bin/AtendimentoMedico/Relatorios/RelTabelaAprazados.rpt"));

            relatorio.Database.Tables["itens"].SetDataSource((DataTable)aprazados["aprazamentos"]);
            relatorio.Subreports["RelResponsaveisAprazamento.rpt"].SetDataSource((DataTable)aprazados["responsaveis"]);
            relatorio.Subreports["RelCabecalho.rpt"].SetDataSource(this.RetornarCabecalhoRelatorioProntuario(prontuario));
            relatorio.SetParameterValue("@DataAprazamento", data.ToString("dd/MM/yyyy"));

            return relatorio;
        }

        private DataTable RetornarCabecalhoRelatorioProntuario(Prontuario prontuario)
        {
            DataTable cabecalho = new DataTable();

            //Início Cabeçalho
            cabecalho.Columns.Add("Paciente", typeof(string));
            cabecalho.Columns.Add("DataAtendimento", typeof(string));
            cabecalho.Columns.Add("NumeroAtendimento", typeof(string));
            cabecalho.Columns.Add("EstabelecimentoAtendimento", typeof(string));
            cabecalho.Columns.Add("SituacaoAtual", typeof(string));
            cabecalho.Columns.Add("ClassificacaoRisco", typeof(string));
            cabecalho.Columns.Add("SumarioAlta", typeof(string));
            cabecalho.Columns.Add("Prontuario", typeof(string));
            cabecalho.Columns.Add("DataNascimento", typeof(string));
            cabecalho.Columns.Add("Sexo", typeof(string));
            cabecalho.Columns.Add("UnidadeTransferencia", typeof(string));
            cabecalho.Columns.Add("Descricao", typeof(string));
            //Fim Cabeçalho

            IEstabelecimentoSaude iEstabelecimento = Factory.GetInstance<IEstabelecimentoSaude>();

            DataRow linha = cabecalho.NewRow();
            linha["Paciente"] = prontuario.NomePacienteToString;
            linha["DataNascimento"] = prontuario.Paciente.PacienteViverMais != null ? prontuario.Paciente.PacienteViverMais.DataNascimento.ToString("dd/MM/yyyy") : " - ";
            linha["Sexo"] = prontuario.Paciente.PacienteViverMais != null ? (prontuario.Paciente.PacienteViverMais.Sexo == 'F' ? "Feminino" : "Masculino") : (prontuario.Paciente.Sexo == 'F' ? "Feminino" : "Masculino");

            linha["DataAtendimento"] = prontuario.Data.ToString("dd/MM/yyyy");
            linha["NumeroAtendimento"] = prontuario.NumeroToString;
            linha["EstabelecimentoAtendimento"] = iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(prontuario.CodigoUnidade).NomeFantasia;
            linha["SituacaoAtual"] = prontuario.Situacao.Nome;
            linha["ClassificacaoRisco"] = prontuario.ClassificacaoRisco != null ? (prontuario.ClassificacaoRisco.Cor + "(" + prontuario.ClassificacaoRisco.Descricao + ")") : "Ainda sem classificação";
            linha["SumarioAlta"] = prontuario.SumarioAlta;
            linha["UnidadeTransferencia"] = prontuario.CodigoUnidadeTransferencia.HasValue
                    ? iEstabelecimento.BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaudeEmergencial>(prontuario.CodigoUnidadeTransferencia.Value).Nome
                    : string.Empty;
            linha["Prontuario"] = prontuario.Codigo;
            linha["Descricao"] = prontuario.Paciente.Descricao;

            cabecalho.Rows.Add(linha);

            return cabecalho;
        }
        private DataTable RetornarClassificacoesRisco(long co_prontuario)
        {
            DataTable classificacoes = new DataTable();
            classificacoes.Columns.Add("data", typeof(string));
            classificacoes.Columns.Add("classificacao", typeof(string));

            IProntuario iProntuario = Factory.GetInstance<IProntuario>();
            IEvolucaoMedica iEvolucao = Factory.GetInstance<IEvolucaoMedica>();
            Prontuario prontuario = iProntuario.BuscarPorCodigo<Prontuario>(co_prontuario);

            int ultimaclassificacao = -1;

            DataRow linha = classificacoes.NewRow();

            if (prontuario.DataAcolhimento.HasValue)
            {
                AcolhimentoUrgence acolhimento = iProntuario.BuscarAcolhimento<AcolhimentoUrgence>(prontuario.Codigo);

                if (acolhimento != null && acolhimento.ClassificacaoRisco != null && acolhimento.ClassificacaoRisco.Codigo != ultimaclassificacao)
                {
                    linha["data"] = acolhimento.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    linha["classificacao"] = acolhimento.ClassificacaoRisco.Cor + "(" + acolhimento.ClassificacaoRisco.Descricao + ")";
                    classificacoes.Rows.Add(linha);
                    ultimaclassificacao = acolhimento.ClassificacaoRisco.Codigo;
                }
            }

            IList<EvolucaoMedica> evolucoes = iEvolucao.BuscarPorProntuario<EvolucaoMedica>(prontuario.Codigo);
            EvolucaoMedica consultamedica = iEvolucao.BuscarConsultaMedica<EvolucaoMedica>(prontuario.Codigo);

            if (consultamedica != null)
                evolucoes.Add(consultamedica);

            evolucoes = evolucoes.OrderBy(p => p.Data).ToList();

            foreach (EvolucaoMedica evolucao in evolucoes)
            {
                if (evolucao.ClassificacaoRisco != null && ultimaclassificacao != evolucao.ClassificacaoRisco.Codigo)
                {
                    linha = classificacoes.NewRow();
                    linha["data"] = evolucao.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    linha["classificacao"] = evolucao.ClassificacaoRisco.Cor + "(" + evolucao.ClassificacaoRisco.Descricao + ")";
                    classificacoes.Rows.Add(linha);
                    ultimaclassificacao = evolucao.ClassificacaoRisco.Codigo;
                }
            }

            return classificacoes;
        }
        private DataTable RetornarDataTableAcolhimento(long co_prontuario)
        {
            DataTable DtAcolhimento = new DataTable();
            ICBO iCBO = Factory.GetInstance<ICBO>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            IProntuario iProntuario = Factory.GetInstance<IProntuario>();

            //Início Dados Acolhimento
            DtAcolhimento.Columns.Add("TensaoArterial", typeof(string));
            DtAcolhimento.Columns.Add("FrequenciaCardiaca", typeof(string));
            DtAcolhimento.Columns.Add("FrequenciaRespiratoria", typeof(string));
            DtAcolhimento.Columns.Add("Temperatura", typeof(string));
            DtAcolhimento.Columns.Add("HGT", typeof(string));
            DtAcolhimento.Columns.Add("Acidente", typeof(string));
            DtAcolhimento.Columns.Add("DorIntensa", typeof(string));
            DtAcolhimento.Columns.Add("Fratura", typeof(string));
            DtAcolhimento.Columns.Add("Convulsao", typeof(string));
            DtAcolhimento.Columns.Add("Alergia", typeof(string));
            DtAcolhimento.Columns.Add("SaturacaoOxigenio", typeof(string));
            DtAcolhimento.Columns.Add("DorToraxica", typeof(string));
            DtAcolhimento.Columns.Add("Asma", typeof(string));
            DtAcolhimento.Columns.Add("Diarreia", typeof(string));
            DtAcolhimento.Columns.Add("QueixaAcolhimento", typeof(string));
            DtAcolhimento.Columns.Add("Prontuario", typeof(string));
            DtAcolhimento.Columns.Add("Profissional", typeof(string));
            DtAcolhimento.Columns.Add("ClassificacaoRisco", typeof(string));
            DtAcolhimento.Columns.Add("DataAcolhimento", typeof(string));
            DtAcolhimento.Columns.Add("Peso", typeof(string));
            //Fim Dados Acolhimento

            AcolhimentoUrgence acolhimento = iProntuario.BuscarAcolhimento<AcolhimentoUrgence>(co_prontuario);

            if (acolhimento != null)
            {
                DataRow linha = DtAcolhimento.NewRow();
                linha["TensaoArterial"] = string.IsNullOrEmpty(acolhimento.TensaoArterialInicio) ? " - " : (acolhimento.TensaoArterialInicio + "X" + acolhimento.TensaoArterialFim + " mmHg");
                linha["FrequenciaCardiaca"] = string.IsNullOrEmpty(acolhimento.FrequenciaCardiaca) ? " - " : acolhimento.FrequenciaCardiaca + " bpm";
                linha["FrequenciaRespiratoria"] = string.IsNullOrEmpty(acolhimento.FrequenciaRespiratoria) ? " - " : acolhimento.FrequenciaRespiratoria + " i.m";
                linha["Temperatura"] = string.IsNullOrEmpty(acolhimento.Temperatura) ? " - " : acolhimento.Temperatura + " ºC";
                linha["HGT"] = string.IsNullOrEmpty(acolhimento.Hgt) ? " - " : acolhimento.Hgt + " m/mol";
                linha["Acidente"] = acolhimento.Acidente ? "Sim" : "Não";
                linha["DorIntensa"] = acolhimento.DorIntensa ? "Sim" : "Não";
                linha["Fratura"] = acolhimento.Fratura ? "Sim" : "Não";
                linha["Convulsao"] = acolhimento.Convulsao ? "Sim" : "Não";
                linha["Alergia"] = acolhimento.Alergia ? "Sim" : "Não";
                linha["Asma"] = acolhimento.Asma ? "Sim" : "Não";
                linha["Diarreia"] = acolhimento.Diarreia ? "Sim" : "Não";
                linha["DorToraxica"] = acolhimento.DorToraxica ? "Sim" : "Não";
                linha["SaturacaoOxigenio"] = acolhimento.SaturacaoOxigenio ? "Sim" : "Não";
                linha["QueixaAcolhimento"] = string.IsNullOrEmpty(acolhimento.Queixa) ? " - " : acolhimento.Queixa;
                linha["Prontuario"] = acolhimento.Prontuario.Codigo;
                linha["Profissional"] = !string.IsNullOrEmpty(acolhimento.CodigoProfissional) ? (iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(acolhimento.CodigoProfissional).Nome + " - " + iCBO.BuscarPorCodigo<CBO>(acolhimento.CBOProfissional).Nome) : "-";
                linha["ClassificacaoRisco"] = acolhimento.Prontuario.ClassificacaoRisco != null ? (acolhimento.Prontuario.ClassificacaoRisco.Cor + "(" + acolhimento.Prontuario.ClassificacaoRisco.Descricao + ")") : " - ";
                linha["DataAcolhimento"] = acolhimento.Data.ToString("dd/MM/yyyy HH:mm:ss");

                if (acolhimento.Peso.HasValue)
                    linha["Peso"] = acolhimento.Peso.Value.ToString("0.00");

                DtAcolhimento.Rows.Add(linha);
            }

            return DtAcolhimento;
        }
        private DataTable RetornarDataTableEvolucoesEnfermagem(long co_prontuario)
        {
            IList<EvolucaoEnfermagem> evolucoes = Factory.GetInstance<IEvolucaoEnfermagem>().BuscarPorProntuario<EvolucaoEnfermagem>(co_prontuario);

            DataTable DtEvolucaoEnfermagem = new DataTable();
            DtEvolucaoEnfermagem.Columns.Add("Profissional", typeof(string));
            DtEvolucaoEnfermagem.Columns.Add("Conteudo", typeof(string));
            DtEvolucaoEnfermagem.Columns.Add("Data", typeof(string));

            DataRow row;
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            ICBO iCBO = Factory.GetInstance<ICBO>();

            if (evolucoes.Count() > 0)
            {
                foreach (EvolucaoEnfermagem ev in evolucoes)
                {
                    row = DtEvolucaoEnfermagem.NewRow();
                    row["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional).Nome
                        + " - " + iCBO.BuscarPorCodigo<CBO>(ev.CBOProfissional).Nome;
                    row["Conteudo"] = ev.Observacao;
                    row["Data"] = ev.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    DtEvolucaoEnfermagem.Rows.Add(row);
                }
            }
            else
            {
                row = DtEvolucaoEnfermagem.NewRow();
                row["Profissional"] = " - ";
                row["Conteudo"] = " - ";
                row["Data"] = " - ";
                DtEvolucaoEnfermagem.Rows.Add(row);
            }

            return DtEvolucaoEnfermagem;
        }
        private DataTable RetornarDataTableExamesInternos(long co_prontuario)
        {
            IList<ProntuarioExame> exames = Factory.GetInstance<IExame>().BuscarExamesPorProntuario<ProntuarioExame>(co_prontuario);
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            ICBO iCBO = Factory.GetInstance<ICBO>();

            DataTable DtExames = new DataTable();
            DtExames.Columns.Add("DataSolicitacao", typeof(string));
            DtExames.Columns.Add("ProfissionalSolicitante", typeof(string));
            DtExames.Columns.Add("Exame", typeof(string));
            DtExames.Columns.Add("DataResultado", typeof(string));
            DtExames.Columns.Add("Resultado", typeof(string));

            DataRow row;

            foreach (ProntuarioExame exame in exames)
            {
                row = DtExames.NewRow();
                row["DataSolicitacao"] = exame.Data.ToString("dd/MM/yyyy");
                row["ProfissionalSolicitante"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(exame.Profissional).Nome + " - " + iCBO.BuscarPorCodigo<CBO>(exame.CBOProfissional).Nome;
                row["Exame"] = exame.Exame.Descricao;
                row["DataResultado"] = exame.DataResultado.HasValue ? exame.DataResultado.Value.ToString("dd/MM/yyyy") : " - ";
                row["Resultado"] = exame.DataResultado.HasValue ? exame.Resultado.ToString() : " - ";
                DtExames.Rows.Add(row);
            }

            return DtExames;
        }
        private DataTable RetornarDataTableFichaAtendimento(long numeroatendimento)
        {
            DataTable DtFichaAtendimento = new DataTable();
            DtFichaAtendimento.Columns.Add("CartaoSUS", typeof(string));
            DtFichaAtendimento.Columns.Add("Paciente", typeof(string));
            DtFichaAtendimento.Columns.Add("NumeroAtendimento", typeof(string));
            DtFichaAtendimento.Columns.Add("Sexo", typeof(string));
            DtFichaAtendimento.Columns.Add("DataAtendimento", typeof(string));
            DtFichaAtendimento.Columns.Add("DataNascimento", typeof(string));
            DtFichaAtendimento.Columns.Add("UnidadeSaude", typeof(string));
            DtFichaAtendimento.Columns.Add("Telefone", typeof(string));
            DtFichaAtendimento.Columns.Add("Idade", typeof(string));
            DtFichaAtendimento.Columns.Add("Endereco", typeof(string));
            DtFichaAtendimento.Columns.Add("Bairro", typeof(string));
            DtFichaAtendimento.Columns.Add("Municipio", typeof(string));
            DtFichaAtendimento.Columns.Add("Numero", typeof(string));
            DtFichaAtendimento.Columns.Add("Rg", typeof(string));

            Prontuario prontuario = Factory.GetInstance<IProntuario>().BuscarPorNumeroProntuario<Prontuario>(numeroatendimento);

            if (string.IsNullOrEmpty(prontuario.Paciente.Descricao))
            {
                ViverMais.Model.Paciente paciente = PacienteBLL.PesquisarCompleto(prontuario.Paciente.CodigoViverMais);

                DataRow row = DtFichaAtendimento.NewRow();
                row["NumeroAtendimento"] = numeroatendimento;
                row["DataAtendimento"] = prontuario.Data.ToString("dd/MM/yyyy HH:mm:ss");
                row["UnidadeSaude"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(prontuario.CodigoUnidade).NomeFantasia;
                row["Paciente"] = paciente.Nome;
                row["DataNascimento"] = paciente.DataNascimento.ToString("dd/MM/yyyy");

                List<ViverMais.Model.Documento> documentos = DocumentoBLL.PesqusiarPorPaciente(paciente);
                ViverMais.Model.Documento documento = (from _documento in documentos
                                                  where
                                                  int.Parse(_documento.ControleDocumento.TipoDocumento.Codigo) == 10
                                                  select _documento).FirstOrDefault();
                if (documento != null)
                    row["Rg"] = !string.IsNullOrEmpty(documento.Numero) ? documento.Numero : "";
                else
                    row["Rg"] = "";

                IList<ViverMais.Model.CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(paciente.Codigo);

                row["Idade"] = paciente.Idade;
                row["CartaoSUS"] = cartoes.Last().Numero;
                row["Sexo"] = paciente.Sexo == 'F' ? "Feminino" : "Masculino";

                ViverMais.Model.Endereco endereco = EnderecoBLL.PesquisarPorPaciente(paciente);

                if (endereco != null)
                {
                    row["Endereco"] = !string.IsNullOrEmpty(endereco.Logradouro) || !string.IsNullOrEmpty(endereco.Complemento) ? endereco.Logradouro + " " + endereco.Complemento : " - ";

                    MunicipioBLL.CompletarMunicipio(endereco.Municipio);

                    row["Bairro"] = !string.IsNullOrEmpty(endereco.Bairro) ? endereco.Bairro : " - ";
                    row["Municipio"] = endereco.Municipio != null ? endereco.Municipio.Nome : " - ";
                    row["Numero"] = !string.IsNullOrEmpty(endereco.Numero) ? endereco.Numero : " - ";
                    row["Telefone"] = endereco.DDD + " " + endereco.Telefone;
                }
                else
                {
                    row["Endereco"] = "";
                    row["Telefone"] = "";
                }

                DtFichaAtendimento.Rows.Add(row);
            }

            return DtFichaAtendimento;
        }

        private Hashtable RetornarHashTableAprazamentos(long co_prontuario)
        {
            Hashtable HtAprazamentos = new Hashtable();

            IAprazamento  iAprazamento = Factory.GetInstance<IAprazamento>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            IPrescricao   iPrescricao = Factory.GetInstance<IPrescricao>();
            ICBO iCBO = Factory.GetInstance<ICBO>();

            IList<Prescricao> prescricoes = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario);

            DataRow row;

            DataTable DtPrescricao = new DataTable();
            DtPrescricao.Columns.Add("Link_Item", typeof(string));
            DtPrescricao.Columns.Add("DataPrescricao", typeof(string));
            DtPrescricao.Columns.Add("ProfissionalSolicitante", typeof(string));
            DtPrescricao.Columns.Add("Status", typeof(string));

            DataTable DtMedicamento = new DataTable();
            DtMedicamento.Columns.Add("CodigoPrescricao", typeof(string));
            DtMedicamento.Columns.Add("Nome", typeof(string));
            DtMedicamento.Columns.Add("HorarioAplicacao", typeof(string));
            DtMedicamento.Columns.Add("HorarioExecucao", typeof(string));
            DtMedicamento.Columns.Add("StatusExecutor", typeof(string));
            DtMedicamento.Columns.Add("NomeProfissionalSolicitante", typeof(string));
            DtMedicamento.Columns.Add("MotivoRecusa", typeof(string));

            DataTable DtProcedimento = new DataTable();
            DtProcedimento.Columns.Add("CodigoPrescricao", typeof(string));
            DtProcedimento.Columns.Add("Nome", typeof(string));
            DtProcedimento.Columns.Add("HorarioAplicacao", typeof(string));
            DtProcedimento.Columns.Add("HorarioExecucao", typeof(string));
            DtProcedimento.Columns.Add("StatusExecutor", typeof(string));
            DtProcedimento.Columns.Add("NomeProfissionalSolicitante", typeof(string));

            DataTable DtProcedimentoNaoFaturavel = new DataTable();
            DtProcedimentoNaoFaturavel.Columns.Add("CodigoPrescricao", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("Nome", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("HorarioAplicacao", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("HorarioExecucao", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("StatusExecutor", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("NomeProfissionalSolicitante", typeof(string));

            foreach (Prescricao prescricao in prescricoes)
            {
                row = DtPrescricao.NewRow();
                row["Link_Item"] = prescricao.Codigo;
                row["DataPrescricao"] = prescricao.Data.ToString("dd/MM/yyyy");
                row["ProfissionalSolicitante"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(prescricao.Profissional).Nome
                    + " - " + iCBO.BuscarPorCodigo<CBO>(prescricao.CBOProfissional).Nome;

                if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida))
                    row["Status"] = "Inválida";
                else
                {
                    if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Agendada))
                        row["Status"] = "Agendada";
                    else
                        row["Status"] = prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida) ? "Válida" : "Suspensa";
                }

                DtPrescricao.Rows.Add(row);

                IList<AprazamentoMedicamento> medicamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo);

                if (medicamentos.Count() > 0)
                {
                    foreach (AprazamentoMedicamento medicamento in medicamentos)
                    {
                        row = DtMedicamento.NewRow();
                        row["CodigoPrescricao"] = prescricao.Codigo;
                        row["Nome"] = medicamento.Medicamento.Nome;
                        row["HorarioAplicacao"] = medicamento.Horario.ToString("dd/MM/yyyy HH:mm");
                        row["HorarioExecucao"] = medicamento.HorarioConfirmacao.HasValue ? medicamento.HorarioConfirmacao.Value.ToString("dd/MM/yyyy HH:mm") : " - ";
                        row["NomeProfissionalSolicitante"] = medicamento.Profissional.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(medicamento.CBOProfissional).Nome;

                        if (medicamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Finalizado))
                        {
                            row["StatusExecutor"] = medicamento.ProfissionalConfirmacao.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(medicamento.CBOProfissionalConfirmacao).Nome;
                            row["MotivoRecusa"] = " - ";
                        }
                        else if (medicamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Ativo))
                        {
                            row["StatusExecutor"] = "Aberto";
                            row["MotivoRecusa"] = " - ";
                        }
                        else if (medicamento.Status == Convert.ToChar(AprazamentoMedicamento.StatusItem.Bloqueado))
                        {
                            row["StatusExecutor"] = "Bloqueado";
                            row["MotivoRecusa"] = " - ";
                        }
                        else
                        {
                            row["StatusExecutor"] = "Recusado";
                            row["MotivoRecusa"] = medicamento.MotivoRecusa;
                        }

                        DtMedicamento.Rows.Add(row);
                    }
                }
                else
                {
                    row = DtMedicamento.NewRow();

                    row["CodigoPrescricao"] = prescricao.Codigo;
                    row["Nome"] = " - ";
                    row["HorarioAplicacao"] = " - ";
                    row["HorarioExecucao"] = " - ";
                    row["NomeProfissionalSolicitante"] = " - ";
                    row["StatusExecutor"] = " - ";
                    row["MotivoRecusa"] = " - ";

                    DtMedicamento.Rows.Add(row);
                }
                
                IList<AprazamentoProcedimento> procedimentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo);

                if (procedimentos.Count() > 0)
                {
                    foreach (AprazamentoProcedimento procedimento in procedimentos)
                    {
                        row = DtProcedimento.NewRow();
                        row["CodigoPrescricao"] = prescricao.Codigo;
                        row["Nome"] = procedimento.Procedimento.Nome;
                        row["HorarioAplicacao"] = procedimento.Horario.ToString("dd/MM/yyyy HH:mm");
                        row["HorarioExecucao"] = procedimento.HorarioConfirmacao.HasValue ? procedimento.HorarioConfirmacao.Value.ToString("dd/MM/yyyy HH:mm") : " - ";
                        row["NomeProfissionalSolicitante"] = procedimento.Profissional.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(procedimento.CBOProfissional).Nome;

                        if (procedimento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Finalizado))
                            row["StatusExecutor"] = procedimento.ProfissionalConfirmacao.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(procedimento.CBOProfissionalConfirmacao).Nome;
                        else if (procedimento.Status == Convert.ToChar(AprazamentoProcedimento.StatusItem.Ativo))
                            row["StatusExecutor"] = "Aberto";
                        else
                            row["StatusExecutor"] = "Bloqueado";

                        DtProcedimento.Rows.Add(row);
                    }
                }
                else 
                {
                    row = DtProcedimento.NewRow();

                    row["CodigoPrescricao"] = prescricao.Codigo;
                    row["Nome"] = " - ";
                    row["HorarioAplicacao"] = " - ";
                    row["HorarioExecucao"] = " - ";
                    row["NomeProfissionalSolicitante"] = " - ";
                    row["StatusExecutor"] = " - ";

                    DtProcedimento.Rows.Add(row);
                }

                IList<AprazamentoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo);

                if (procedimentosnaofaturaveis.Count() > 0)
                {
                    foreach (AprazamentoProcedimentoNaoFaturavel procedimento in procedimentosnaofaturaveis)
                    {
                        row = DtProcedimentoNaoFaturavel.NewRow();
                        row["CodigoPrescricao"] = prescricao.Codigo;
                        row["Nome"] = procedimento.ProcedimentoNaoFaturavel.Nome;
                        row["HorarioAplicacao"] = procedimento.Horario.ToString("dd/MM/yyyy HH:mm");
                        row["HorarioExecucao"] = procedimento.HorarioConfirmacao.HasValue ? procedimento.HorarioConfirmacao.Value.ToString("dd/MM/yyyy HH:mm") : " - ";
                        row["NomeProfissionalSolicitante"] = procedimento.Profissional.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(procedimento.CBOProfissional).Nome;

                        if (procedimento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Finalizado))
                            row["StatusExecutor"] = procedimento.ProfissionalConfirmacao.Nome + " - " + iCBO.BuscarPorCodigo<CBO>(procedimento.CBOProfissionalConfirmacao).Nome;
                        else if (procedimento.Status == Convert.ToChar(AprazamentoProcedimentoNaoFaturavel.StatusItem.Ativo))
                            row["StatusExecutor"] = "Aberto";
                        else
                            row["StatusExecutor"] = "Bloqueado";

                        DtProcedimentoNaoFaturavel.Rows.Add(row);
                    }
                }
                else
                {
                    row = DtProcedimentoNaoFaturavel.NewRow();
                    
                    row["CodigoPrescricao"] = prescricao.Codigo;
                    row["Nome"] = " - ";
                    row["HorarioAplicacao"] = " - ";
                    row["HorarioExecucao"] = " - ";
                    row["NomeProfissionalSolicitante"] = " - ";
                    row["StatusExecutor"] = " - ";

                    DtProcedimentoNaoFaturavel.Rows.Add(row);
                }
            }

            HtAprazamentos.Add("prescricoes", DtPrescricao);
            HtAprazamentos.Add("medicamentos", DtMedicamento);
            HtAprazamentos.Add("procedimentos", DtProcedimento);
            HtAprazamentos.Add("procedimentosnaofaturaveis", DtProcedimentoNaoFaturavel);

            return HtAprazamentos;
        }
        private Hashtable RetornarHashTableConsultaMedica(long co_prontuario)
        {
            Hashtable HtConsultaMedica = new Hashtable();

            //IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            IEvolucaoMedica iEvolucao = Factory.GetInstance<IEvolucaoMedica>();
            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            ICBO iCBO = Factory.GetInstance<ICBO>();

            DataTable DtConsultaMedica = new DataTable();
            DtConsultaMedica.Columns.Add("Profissional", typeof(string));
            DtConsultaMedica.Columns.Add("Conteudo", typeof(string));
            DtConsultaMedica.Columns.Add("Link_Item", typeof(string));
            DtConsultaMedica.Columns.Add("Data", typeof(string));
            DtConsultaMedica.Columns.Add("ClassificacaoRisco", typeof(string));

            DataTable DtCids = new DataTable();
            DtCids.Columns.Add("Nome", typeof(string));
            DtCids.Columns.Add("Codigo", typeof(string));
            DtCids.Columns.Add("Link_Item", typeof(string));

            EvolucaoMedica consulta = iEvolucao.BuscarConsultaMedica<EvolucaoMedica>(co_prontuario);

            if (consulta != null)
            {
                DataRow row = DtConsultaMedica.NewRow();
                row["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(consulta.CodigoProfissional).Nome + " - " + iCBO.BuscarPorCodigo<CBO>(consulta.CBOProfissional).Nome;
                row["Data"] = consulta.Data.ToString("dd/MM/yyyy HH:mm:ss");
                row["ClassificacaoRisco"] = consulta.ClassificacaoRisco != null ? (consulta.ClassificacaoRisco.Cor + "(" + consulta.ClassificacaoRisco.Descricao + ")") : "-";
                row["Conteudo"] = string.IsNullOrEmpty(consulta.Observacao) ? " - " : consulta.Observacao;
                row["Link_Item"] = consulta.Codigo;
                DtConsultaMedica.Rows.Add(row);

                if (consulta.CodigosCids.Count() > 0)
                {
                    foreach (string codigo in consulta.CodigosCids)
                    {
                        Cid c = iCBO.BuscarPorCodigo<Cid>(codigo);
                        row = DtCids.NewRow();
                        row["Nome"] = c.Nome;
                        row["Codigo"] = c.Codigo;
                        row["Link_Item"] = consulta.Codigo;
                        DtCids.Rows.Add(row);
                    }
                }
            }

            HtConsultaMedica.Add("consulta", DtConsultaMedica);
            HtConsultaMedica.Add("cids", DtCids);

            return HtConsultaMedica;
        }
        private Hashtable RetornarHashTableEvolucoesMedicas(long co_prontuario)
        {
            Hashtable HtEvolucoesMedicas = new Hashtable();

            IList<EvolucaoMedica> evolucoes = Factory.GetInstance<IEvolucaoMedica>().BuscarPorProntuario<EvolucaoMedica>(co_prontuario);

            DataTable DtEvolucaoMedica = new DataTable();
            DtEvolucaoMedica.Columns.Add("Profissional", typeof(string));
            DtEvolucaoMedica.Columns.Add("Conteudo", typeof(string));
            DtEvolucaoMedica.Columns.Add("Data", typeof(string));
            DtEvolucaoMedica.Columns.Add("ClassificacaoRisco", typeof(string));
            DtEvolucaoMedica.Columns.Add("Link_Item", typeof(string));

            DataTable DtCids = new DataTable();
            DtCids.Columns.Add("Nome", typeof(string));
            DtCids.Columns.Add("Codigo", typeof(string));
            DtCids.Columns.Add("Link_Item", typeof(string));

            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            ICBO iCBO = Factory.GetInstance<ICBO>();
            //IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            DataRow row;

            if (evolucoes.Count() > 0)
            {
                foreach (EvolucaoMedica ev in evolucoes)
                {
                    row = DtEvolucaoMedica.NewRow();
                    row["Profissional"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(ev.CodigoProfissional).Nome + " - " + iCBO.BuscarPorCodigo<CBO>(ev.CBOProfissional).Nome;
                    row["Conteudo"] = ev.Observacao;
                    row["Data"] = ev.Data.ToString("dd/MM/yyyy");
                    row["Link_Item"] = ev.Codigo;
                    row["ClassificacaoRisco"] = ev.ClassificacaoRisco.Cor + "(" + ev.ClassificacaoRisco.Descricao + ")";

                    DtEvolucaoMedica.Rows.Add(row);

                    if (ev.CodigosCids.Count() > 0)
                    {
                        foreach (string codigo in ev.CodigosCids)
                        {
                            Cid c = iCBO.BuscarPorCodigo<Cid>(codigo);
                            row = DtCids.NewRow();
                            row["Nome"] = c.Nome;
                            row["Codigo"] = c.Codigo;
                            row["Link_Item"] = ev.Codigo;
                            DtCids.Rows.Add(row);
                        }
                    }
                    else
                    {
                        row = DtCids.NewRow();
                        row["Nome"] = " - ";
                        row["Codigo"] = " - ";
                        row["Link_Item"] = ev.Codigo;
                        DtCids.Rows.Add(row);
                    }
                }
            }
            else
            {
                row = DtEvolucaoMedica.NewRow();
                row["Profissional"] = " - ";
                row["Conteudo"] = " - ";
                row["Data"] = " - ";
                row["Link_Item"] = "-1";
                DtEvolucaoMedica.Rows.Add(row);

                row = DtCids.NewRow();
                row["Nome"] = " - ";
                row["Codigo"] = " - ";
                row["Link_Item"] = "-1";
                DtCids.Rows.Add(row);
            }

            HtEvolucoesMedicas.Add("evolucoes", DtEvolucaoMedica);
            HtEvolucoesMedicas.Add("cids", DtCids);

            return HtEvolucoesMedicas;
        }
        private Hashtable RetornarHashTablePrescricoes(long co_prontuario)
        {
            Hashtable HtPrescricoes = new Hashtable();

            IProfissional iProfissional = Factory.GetInstance<IProfissional>();
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            ICBO iCBO = Factory.GetInstance<ICBO>();

            IList<Prescricao> prescricoes = iPrescricao.BuscarPorProntuario<Prescricao>(co_prontuario);

            DataRow row;

            DataTable DtPrescricao = new DataTable();
            DtPrescricao.Columns.Add("Link_Item", typeof(string));
            DtPrescricao.Columns.Add("DataPrescricao", typeof(string));
            DtPrescricao.Columns.Add("ProfissionalSolicitante", typeof(string));
            DtPrescricao.Columns.Add("Status", typeof(string));

            DataTable DtMedicamento = new DataTable();
            DtMedicamento.Columns.Add("Link_Item", typeof(string));
            DtMedicamento.Columns.Add("Medicamento", typeof(string));
            DtMedicamento.Columns.Add("Intervalo", typeof(string));
            DtMedicamento.Columns.Add("ViaAdministracao", typeof(string));
            DtMedicamento.Columns.Add("Observacao", typeof(string));

            DataTable DtProcedimento = new DataTable();
            DtProcedimento.Columns.Add("Link_Item", typeof(string));
            DtProcedimento.Columns.Add("Procedimento", typeof(string));
            DtProcedimento.Columns.Add("Intervalo", typeof(string));
            DtProcedimento.Columns.Add("Cid", typeof(string));

            DataTable DtProcedimentoNaoFaturavel = new DataTable();
            DtProcedimentoNaoFaturavel.Columns.Add("Link_Item", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("Procedimento", typeof(string));
            DtProcedimentoNaoFaturavel.Columns.Add("Intervalo", typeof(string));

            if (prescricoes.Count() > 0)
            {
                foreach (Prescricao prescricao in prescricoes)
                {
                    row = DtPrescricao.NewRow();
                    row["Link_Item"] = prescricao.Codigo;
                    row["DataPrescricao"] = prescricao.Data.ToString("dd/MM/yyyy");
                    row["ProfissionalSolicitante"] = iProfissional.BuscarPorCodigo<ViverMais.Model.Profissional>(prescricao.Profissional).Nome
                        + " - " + iCBO.BuscarPorCodigo<CBO>(prescricao.CBOProfissional).Nome;

                    if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Invalida))
                        row["Status"] = "Inválida";
                    else
                    {
                        if (prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Agendada))
                            row["Status"] = "Agendada";
                        else
                            row["Status"] = prescricao.Status == Convert.ToChar(Prescricao.StatusPrescricao.Valida) ? "Válida" : "Suspensa";
                    }

                    DtPrescricao.Rows.Add(row);

                    IList<PrescricaoMedicamento> medicamentos = iPrescricao.BuscarMedicamentos<PrescricaoMedicamento>(prescricao.Codigo);

                    if (medicamentos.Count() > 0)
                    {
                        foreach (PrescricaoMedicamento medicamento in medicamentos)
                        {
                            row = DtMedicamento.NewRow();
                            row["Link_Item"] = prescricao.Codigo;
                            row["Medicamento"] = medicamento.ObjetoMedicamento.Nome;
                            row["Intervalo"] = medicamento.DescricaoIntervalo;
                            row["ViaAdministracao"] = medicamento.ViaAdministracao != null ? medicamento.ViaAdministracao.Nome : " - ";
                            row["Observacao"] = !string.IsNullOrEmpty(medicamento.Observacao) ? medicamento.Observacao : " - ";
                            DtMedicamento.Rows.Add(row);
                        }
                    }
                    else
                    {
                        row = DtMedicamento.NewRow();
                        row["Link_Item"] = prescricao.Codigo;
                        row["Medicamento"] = " - ";
                        row["Intervalo"] = " - ";
                        row["ViaAdministracao"] = " - ";
                        row["Observacao"] = " - ";
                        DtMedicamento.Rows.Add(row);
                    }

                    IList<PrescricaoProcedimento> procedimentos = iPrescricao.BuscarProcedimentos<PrescricaoProcedimento>(prescricao.Codigo);

                    if (procedimentos.Count() > 0)
                    {
                        foreach (PrescricaoProcedimento procedimento in procedimentos)
                        {
                            row = DtProcedimento.NewRow();
                            row["Link_Item"] = prescricao.Codigo;
                            row["Procedimento"] = procedimento.Procedimento.Nome;
                            row["Intervalo"] = procedimento.DescricaoIntervalo;
                            row["Cid"] = procedimento.DescricaoCIDVinculado;
                            DtProcedimento.Rows.Add(row);
                        }
                    }
                    else
                    {
                        row = DtProcedimento.NewRow();
                        row["Link_Item"] = prescricao.Codigo;
                        row["Procedimento"] = " - ";
                        row["Intervalo"] = " - ";
                        row["Cid"] = "-";
                        DtProcedimento.Rows.Add(row);
                    }

                    IList<PrescricaoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iPrescricao.BuscarProcedimentosNaoFaturaveis<PrescricaoProcedimentoNaoFaturavel>(prescricao.Codigo);

                    if (procedimentosnaofaturaveis.Count() > 0)
                    {
                        foreach (PrescricaoProcedimentoNaoFaturavel procedimento in procedimentosnaofaturaveis)
                        {
                            row = DtProcedimentoNaoFaturavel.NewRow();
                            row["Link_Item"] = prescricao.Codigo;
                            row["Procedimento"] = procedimento.Procedimento.Nome;
                            row["Intervalo"] = procedimento.DescricaoIntervalo;
                            DtProcedimentoNaoFaturavel.Rows.Add(row);
                        }
                    }
                    else
                    {
                        row = DtProcedimentoNaoFaturavel.NewRow();
                        row["Link_Item"] = prescricao.Codigo;
                        row["Procedimento"] = " - ";
                        row["Intervalo"] = " - ";
                        DtProcedimentoNaoFaturavel.Rows.Add(row);
                    }
                }
            }
            else
            {
                row = DtPrescricao.NewRow();
                row["Link_Item"] = "-1";
                row["DataPrescricao"] = " - ";
                row["ProfissionalSolicitante"] = " - ";
                row["Status"] = " - ";
                DtPrescricao.Rows.Add(row);

                row = DtMedicamento.NewRow();
                row["Link_Item"] = "-1";
                row["Medicamento"] = " - ";
                row["Intervalo"] = " - ";
                row["ViaAdministracao"] = " - ";
                row["Observacao"] = " - ";
                DtMedicamento.Rows.Add(row);

                row = DtProcedimento.NewRow();
                row["Link_Item"] = "-1";
                row["Procedimento"] = " - ";
                row["Intervalo"] = " - ";
                DtProcedimento.Rows.Add(row);

                row = DtProcedimentoNaoFaturavel.NewRow();
                row["Link_Item"] = "-1";
                row["Procedimento"] = " - ";
                row["Intervalo"] = " - ";
                DtProcedimentoNaoFaturavel.Rows.Add(row);
            }

            HtPrescricoes.Add("prescricoes", DtPrescricao);
            HtPrescricoes.Add("medicamentos", DtMedicamento);
            HtPrescricoes.Add("procedimentos", DtProcedimento);
            HtPrescricoes.Add("procedimentosnaofaturaveis", DtProcedimentoNaoFaturavel);

            return HtPrescricoes;
        }
        private Hashtable RetornarHashTableAprazados(long co_prescricao, DateTime data)
        {
            IPrescricao iPrescricao = Factory.GetInstance<IPrescricao>();
            Prescricao prescricao = iPrescricao.BuscarPorCodigo<Prescricao>(co_prescricao);
            IAprazamento iAprazamento = Factory.GetInstance<IAprazamento>();

            Hashtable hash = new Hashtable();

            DataTable aprazamentos = new DataTable();
            aprazamentos.Columns.Add("nome", typeof(string));
            aprazamentos.Columns.Add("prescricao", typeof(string));

            for (int i = 0; i < 48; i++)
                aprazamentos.Columns.Add(i.ToString(), typeof(string));

            IList<AprazamentoMedicamento> medicamentos = iAprazamento.BuscarAprazamentoMedicamento<AprazamentoMedicamento>(prescricao.Codigo, data);
            var _medicamentos = from _medicamento in medicamentos
                                select new
                                {
                                    _Codigo = "MED_" + _medicamento.Medicamento.Codigo,
                                    _Prescricao = _medicamento.Prescricao,
                                    _Nome = _medicamento.Medicamento.Nome,
                                    _Horario = _medicamento.Horario,
                                    _Profissional = _medicamento.Profissional
                                };

            IList<AprazamentoProcedimento> procedimentos = iAprazamento.BuscarAprazamentoProcedimento<AprazamentoProcedimento>(prescricao.Codigo, data);
            var _procedimentos = from _procedimento in procedimentos
                                 select new
                                 {
                                     _Codigo = "PROC_" + _procedimento.Procedimento.Codigo,
                                     _Prescricao = _procedimento.Prescricao,
                                     _Nome = _procedimento.Procedimento.Nome,
                                     _Horario = _procedimento.Horario,
                                     _Profissional = _procedimento.Profissional
                                 };

            IList<AprazamentoProcedimentoNaoFaturavel> procedimentosnaofaturaveis = iAprazamento.BuscarAprazamentoProcedimentoNaoFaturavel<AprazamentoProcedimentoNaoFaturavel>(prescricao.Codigo, data);
            var _procedimentosnaofaturaveis = from _procedimento in procedimentosnaofaturaveis
                                              select new
                                              {
                                                  _Codigo = "PROCNF_" + _procedimento.ProcedimentoNaoFaturavel.Codigo,
                                                  _Prescricao = _procedimento.Prescricao,
                                                  _Nome = _procedimento.ProcedimentoNaoFaturavel.Nome,
                                                  _Horario = _procedimento.Horario,
                                                  _Profissional = _procedimento.Profissional
                                              };

            var _aprazamentosconcatenados = _procedimentos.Concat(_procedimentosnaofaturaveis).Concat(_medicamentos);
            var _aprazamentos =
                from _aprazamento in
                    _aprazamentosconcatenados
                orderby _aprazamento._Nome
                group _aprazamento by _aprazamento._Codigo into g
                select g;

            IList<ViverMais.Model.Profissional> profissionais = new List<ViverMais.Model.Profissional>();
            DataTable responsaveis = new DataTable();
            responsaveis.Columns.Add("nome", typeof(string));

            foreach (var aprazamento in _aprazamentos)
            {
                var listaordenada = aprazamento.OrderBy(p => p._Horario).ToList();
                DataRow row = aprazamentos.NewRow();
                row["prescricao"] = prescricao.Codigo;
                row["nome"] = listaordenada.First()._Nome;

                foreach (var conteudo in listaordenada)
                {
                    int pos = conteudo._Horario.Hour * 2;

                    if (conteudo._Horario.Minute != 0)
                        row[(pos + 1).ToString()] = "X";
                    else
                        row[pos.ToString()] = "X";

                    profissionais.Add(conteudo._Profissional);
                }

                aprazamentos.Rows.Add(row);
            }

            profissionais = profissionais.Distinct(new GenericComparer<ViverMais.Model.Profissional>("CPF")).ToList();
            foreach (ViverMais.Model.Profissional profissional in profissionais)
            {
                DataRow row = responsaveis.NewRow();
                row["nome"] = profissional.Nome;
                responsaveis.Rows.Add(row);
            }

            hash.Add("aprazamentos", aprazamentos);
            hash.Add("responsaveis", responsaveis);

            return hash;
        }
        #endregion
    }
}
