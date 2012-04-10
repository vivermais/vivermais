﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Paciente;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Web;
//using Excel = Microsoft.Office.Interop.Excel;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using CrystalDecisions.CrystalReports.Engine;
using ViverMais.DAO.Vacina.Relatorios;

namespace ViverMais.DAO.Vacina
{
    public class RelatorioVacinaDAO : VacinaServiceFacadeDAO, IRelatorioVacina
    {
        //Hashtable IRelatorioVacina.ObterRelatorioConferenciaInventario(int co_inventario)
        //{
        //    Hashtable hash = new Hashtable();
        //    DataTable tab = new DataTable();
        //    tab.Columns.Add(new DataColumn("vacina", typeof(string)));
        //    tab.Columns.Add(new DataColumn("unidade", typeof(string)));
        //    tab.Columns.Add(new DataColumn("lote", typeof(string)));
        //    tab.Columns.Add(new DataColumn("fabricante", typeof(string)));
        //    tab.Columns.Add(new DataColumn("validade", typeof(string)));

        //    IList<ItemInventarioVacina> itensinventario = Factory.GetInstance<IInventarioVacina>().ListarItensInventario<ItemInventarioVacina>(co_inventario);

        //    foreach (ItemInventarioVacina item in itensinventario)
        //    {
        //        DataRow row = tab.NewRow();
        //        row["vacina"] = item.LoteVacina.ItemVacina.Vacina.Nome;
        //        row["unidade"] = item.LoteVacina.ItemVacina.Vacina.UnidadeMedida.Sigla;
        //        row["lote"] = item.LoteVacina.Identificacao;
        //        row["fabricante"] = item.LoteVacina.ItemVacina.FabricanteVacina.Nome;
        //        row["validade"] = item.LoteVacina.DataValidade.ToString("dd/MM/yyyy");
        //        tab.Rows.Add(row);
        //    }

        //    hash.Add("cabecalho", this.RetornarCabecalhoRelatorioInventario(co_inventario));
        //    hash.Add("corpo", tab);
        //    return hash;
        //}

        //Hashtable IRelatorioVacina.ObterRelatorioFinalInventario(int co_inventario)
        //{
        //    Hashtable hash = new Hashtable();
        //    DataTable tab = new DataTable();
        //    tab.Columns.Add(new DataColumn("vacina", typeof(string)));
        //    tab.Columns.Add(new DataColumn("unidade", typeof(string)));
        //    tab.Columns.Add(new DataColumn("lote", typeof(string)));
        //    tab.Columns.Add(new DataColumn("fabricante", typeof(string)));
        //    tab.Columns.Add(new DataColumn("validade", typeof(string)));
        //    tab.Columns.Add(new DataColumn("quantidadecontada", typeof(string)));
        //    tab.Columns.Add(new DataColumn("quantidadeestoque", typeof(string)));
        //    tab.Columns.Add(new DataColumn("quantidadediferenca", typeof(string)));

        //    IList<ItemInventarioVacina> itensinventario = Factory.GetInstance<IInventarioVacina>().ListarItensInventario<ItemInventarioVacina>(co_inventario);

        //    foreach (ItemInventarioVacina item in itensinventario)
        //    {
        //        DataRow row = tab.NewRow();
        //        row["vacina"] = item.LoteVacina.ItemVacina.Vacina.Nome;
        //        row["unidade"] = item.LoteVacina.ItemVacina.Vacina.UnidadeMedida.Sigla;
        //        row["lote"] = item.LoteVacina.Identificacao;
        //        row["fabricante"] = item.LoteVacina.ItemVacina.FabricanteVacina.Nome;
        //        row["validade"] = item.LoteVacina.DataValidade.ToString("dd/MM/yyyy");
        //        row["quantidadecontada"] = item.QtdContada;
        //        row["quantidadeestoque"] = item.QtdEstoque;
        //        row["quantidadediferenca"] = item.QtdContada - item.QtdEstoque;
        //        tab.Rows.Add(row);
        //    }

        //    hash.Add("cabecalho", this.RetornarCabecalhoRelatorioInventario(co_inventario));
        //    hash.Add("corpo", tab);
        //    return hash;
        //}

        //Hashtable IRelatorioVacina.ObterRelatorioDispensacao(int co_dispensacao)
        //{
        //    Hashtable hash = new Hashtable();
        //    DataTable corpo = new DataTable();
        //    DataRow row = null;

        //    corpo.Columns.Add(new DataColumn("Vacina", typeof(string)));
        //    corpo.Columns.Add(new DataColumn("Lote", typeof(string)));
        //    corpo.Columns.Add(new DataColumn("Fabricante", typeof(string)));
        //    corpo.Columns.Add(new DataColumn("Validade", typeof(string)));
        //    corpo.Columns.Add(new DataColumn("Dose", typeof(string)));
        //    corpo.Columns.Add(new DataColumn("ProximaVacina", typeof(string)));
        //    //corpo.Columns.Add(new DataColumn("CodigoDispensacao", typeof(string)));

        //    IList<ItemDispensacaoVacina> itens = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc.IDispensacao>().BuscarItensDispensacao<ItemDispensacaoVacina>(co_dispensacao);

        //    foreach (ItemDispensacaoVacina item in itens)
        //    {
        //        row = corpo.NewRow();
        //        row["Vacina"] = item.Lote.ItemVacina.Vacina.Nome;
        //        row["Lote"] = item.Lote.Identificacao;
        //        row["Fabricante"] = item.Lote.ItemVacina.FabricanteVacina.Nome;
        //        row["Validade"] = item.Lote.DataValidade.ToString("dd/MM/yyyy");
        //        row["Dose"] = item.Dose.Descricao;

        //        IList<ParametrizacaoVacina> parametrizacoes = Factory.GetInstance<IParametrizacaoVacina>().BuscaProximaDose<ParametrizacaoVacina>(item.Dose.Codigo, item.Lote.ItemVacina.Vacina.Codigo);
        //        string dataproxima = CartaoVacina.RetornaProximaDose(parametrizacoes, item.Dispensacao.Data);
        //        row["ProximaVacina"] = string.IsNullOrEmpty(dataproxima) ? "NoRegisters" : dataproxima;

        //        //row["CodigoDispensacao"] = co_dispensacao;
        //        corpo.Rows.Add(row);
        //    }

        //    hash.Add("cabecalho", this.RetornarCabecalhoRelatorioDispensacao(co_dispensacao));
        //    hash.Add("corpo", corpo);
        //    return hash;
        //}

        //Hashtable IRelatorioVacina.ObterCartaoVacina<P, A, C>(P _paciente, A _avatar, IList<C> _cartoes)
        //{
        //    ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)(object)_paciente;
        //    AvatarCartaoVacina avatar = (AvatarCartaoVacina)(object)_avatar;
        //    IList<CartaoVacina> cartoes = (IList<CartaoVacina>)(object)_cartoes;

        //    Hashtable hash = new Hashtable();
        //    DataTable header = new DataTable();
        //    header.Columns.Add(new DataColumn("imagemtopo", typeof(Byte[])));
        //    header.Columns.Add(new DataColumn("imagemcabecalho", typeof(Byte[])));
        //    header.Columns.Add(new DataColumn("imagemnome", typeof(Byte[])));
        //    header.Columns.Add(new DataColumn("nome", typeof(string)));
        //    header.Columns.Add(new DataColumn("co_paciente", typeof(string)));

        //    DataRow linha = header.NewRow();

        //    linha["imagemtopo"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath(avatar.RetornaImagemTopo()));
        //    linha["imagemcabecalho"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath(avatar.RetornaImagemCabecalho()));
        //    linha["imagemnome"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath("~/Vacina/img/CartaoVacina/Temas/nome-campo.png"));
        //    linha["nome"] = paciente.Nome;
        //    linha["co_paciente"] = paciente.Codigo;
        //    header.Rows.Add(linha);

        //    DataTable body = new DataTable();
        //    body.Columns.Add(new DataColumn("vacina", typeof(string)));
        //    body.Columns.Add(new DataColumn("dose", typeof(string)));
        //    body.Columns.Add(new DataColumn("dataaplicacao", typeof(string)));
        //    body.Columns.Add(new DataColumn("proximadose", typeof(string)));
        //    body.Columns.Add(new DataColumn("lote", typeof(string)));
        //    body.Columns.Add(new DataColumn("validadelote", typeof(string)));
        //    body.Columns.Add(new DataColumn("cnesunidade", typeof(string)));
        //    body.Columns.Add(new DataColumn("estabelecimento", typeof(string)));
        //    body.Columns.Add(new DataColumn("co_paciente", typeof(string)));
        //    body.Columns.Add(new DataColumn("codigo", typeof(long)));

        //    foreach (CartaoVacina cartao in cartoes)
        //    {
        //        DataRow row = body.NewRow();
        //        row["codigo"] = cartao.Codigo;
        //        row["vacina"] = cartao.VacinaImpressaoCartao;
        //        row["dose"] = cartao.DoseVacinaImpressaoCartao;
        //        row["dataaplicacao"] = cartao.DataAplicacaoVacinaImpressaoCartao;
        //        row["proximadose"] = cartao.ProximaDoseVacinaImpressaoCartao;
        //        row["lote"] = cartao.LoteVacinaImpressaoCartao;
        //        row["validadelote"] = cartao.ValidadeLoteImpressaoCartao;
        //        row["cnesunidade"] = cartao.CNESImpressaoCartao;
        //        row["estabelecimento"] = cartao.UnidadeVacinaImpressaoCartao;
        //        row["co_paciente"] = paciente.Codigo;
        //        body.Rows.Add(row);
        //    }

        //    hash.Add("cabecalho", header);
        //    hash.Add("corpo", body);
        //    return hash;
        //}
        //Hashtable IRelatorioVacina.ObterRelatorioMovimento(long co_movimento)
        //{
        //    Hashtable hash = new Hashtable();
        //    IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
        //    DataTable cabecalho = new DataTable();

        //    cabecalho.Columns.Add("co_movimento", typeof(long));
        //    cabecalho.Columns.Add("numero", typeof(long));
        //    cabecalho.Columns.Add("motivo", typeof(string));
        //    cabecalho.Columns.Add("tipomovimento", typeof(string));
        //    cabecalho.Columns.Add("situacao", typeof(string));
        //    cabecalho.Columns.Add("sala", typeof(string));
        //    cabecalho.Columns.Add("data", typeof(string));
        //    cabecalho.Columns.Add("observacao", typeof(string));
        //    cabecalho.Columns.Add("estabelecimentosaude", typeof(string));
        //    cabecalho.Columns.Add("saladestino", typeof(string));
        //    cabecalho.Columns.Add("responsavel", typeof(string));
        //    cabecalho.Columns.Add("responsavelenvio", typeof(string));
        //    cabecalho.Columns.Add("responsavelrecebimento", typeof(string));
        //    cabecalho.Columns.Add("dataenvio", typeof(string));
        //    cabecalho.Columns.Add("datarecebimento", typeof(string));

        //    DataTable corpo = new DataTable();
        //    corpo.Columns.Add("co_movimento", typeof(long));
        //    corpo.Columns.Add("co_item", typeof(long));
        //    corpo.Columns.Add("vacina", typeof(string));
        //    corpo.Columns.Add("fabricante", typeof(string));
        //    corpo.Columns.Add("lote", typeof(string));
        //    corpo.Columns.Add("aplicacao", typeof(string));
        //    corpo.Columns.Add("validade", typeof(string));
        //    corpo.Columns.Add("quantidade", typeof(int));

        //    DataTable historicoitens = new DataTable();
        //    historicoitens.Columns.Add("co_movimento", typeof(long));
        //    historicoitens.Columns.Add("co_item", typeof(long));
        //    historicoitens.Columns.Add("vacina", typeof(string));
        //    historicoitens.Columns.Add("fabricante", typeof(string));
        //    historicoitens.Columns.Add("lote", typeof(string));
        //    historicoitens.Columns.Add("aplicacao", typeof(string));
        //    historicoitens.Columns.Add("validade", typeof(string));
        //    historicoitens.Columns.Add("motivo", typeof(string));
        //    historicoitens.Columns.Add("horario", typeof(string));
        //    historicoitens.Columns.Add("qtdanterior", typeof(int));
        //    historicoitens.Columns.Add("qtdalterada", typeof(int));

        //    MovimentoVacina movimento = iMovimento.BuscarPorCodigo<MovimentoVacina>(co_movimento);

        //    DataRow row = cabecalho.NewRow();
        //    row["co_movimento"] = movimento.Codigo;
        //    row["numero"] = movimento.Numero;
        //    row["tipomovimento"] = movimento.TipoMovimento.Nome;
        //    row["situacao"] = movimento.Operacao.Nome;
        //    row["sala"] = movimento.Sala.Nome;
        //    row["data"] = movimento.Data.ToString("dd/MM/yyyy");
        //    row["observacao"] = movimento.Observacao;

        //    if (movimento.Motivo != null)
        //        row["motivo"] = movimento.Motivo.Nome;

        //    if (movimento.EstabelecimentoSaude != null)
        //        row["estabelecimentosaude"] = movimento.EstabelecimentoSaude.NomeFantasia;

        //    if (movimento.SalaDestino != null)
        //        row["saladestino"] = movimento.SalaDestino.Nome;

        //    if (!string.IsNullOrEmpty(movimento.Responsavel))
        //        row["responsavel"] = movimento.Responsavel;

        //    if (!string.IsNullOrEmpty(movimento.ResponsavelEnvio))
        //        row["responsavelenvio"] = movimento.ResponsavelEnvio;

        //    if (!string.IsNullOrEmpty(movimento.ResponsavelRecebimento))
        //        row["responsavelrecebimento"] = movimento.ResponsavelRecebimento;

        //    if (movimento.DataEnvio.HasValue)
        //        row["dataenvio"] = movimento.DataEnvio.Value.ToString("dd/MM/yyyy");

        //    if (movimento.DataRecebimento.HasValue)
        //        row["datarecebimento"] = movimento.DataRecebimento.Value.ToString("dd/MM/yyyy");

        //    cabecalho.Rows.Add(row);

        //    IList<ItemMovimentoVacina> itens = iMovimento.BuscarItensMovimentacao<ItemMovimentoVacina>(co_movimento);

        //    foreach (ItemMovimentoVacina item in itens)
        //    {
        //        row = corpo.NewRow();
        //        row["co_movimento"] = movimento.Codigo;
        //        row["co_item"] = item.Codigo;
        //        row["vacina"] = item.NomeVacina;
        //        row["fabricante"] = item.NomeFabricante;
        //        row["lote"] = item.Identificacao;
        //        row["aplicacao"] = item.AplicacaoVacina.ToString();
        //        row["validade"] = item.DataValidade.ToString("dd/MM/yyyy");
        //        row["quantidade"] = item.Quantidade;

        //        corpo.Rows.Add(row);

        //        IList<HistoricoItemMovimentoVacina> historicos = iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(item.Codigo);

        //        foreach (HistoricoItemMovimentoVacina historico in historicos)
        //        {
        //            row = historicoitens.NewRow();
        //            row["co_movimento"] = movimento.Codigo;
        //            row["co_item"] = item.Codigo;
        //            row["vacina"] = item.NomeVacina;
        //            row["fabricante"] = item.NomeFabricante;
        //            row["lote"] = item.Identificacao;
        //            row["aplicacao"] = item.AplicacaoVacina.ToString();
        //            row["validade"] = item.DataValidade.ToString("dd/MM/yyyy");
        //            row["qtdanterior"] = historico.QuantidadeAnterior;
        //            row["qtdalterada"] = historico.QuantidadeAlterada;
        //            row["motivo"] = historico.Motivo;
        //            row["horario"] = historico.Data.ToString("dd/MM/yyyy HH:mm");

        //            historicoitens.Rows.Add(row);
        //        }
        //    }

        //    hash.Add("cabecalho", cabecalho);
        //    hash.Add("corpo", corpo);
        //    hash.Add("historicoitens", historicoitens);

        //    return hash;
        //}

        ReportDocument IRelatorioVacina.ObterRelatorioMovimento(long co_movimento)
        {
            ReportDocument relatorio = new ReportDocument();
            relatorio.Load(HttpContext.Current.Server.MapPath("~/bin/Vacina/Relatorios/RelMovimento.rpt"));

            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            DataTable cabecalho = new DataTable();

            cabecalho.Columns.Add("co_movimento", typeof(long));
            cabecalho.Columns.Add("numero", typeof(long));
            cabecalho.Columns.Add("motivo", typeof(string));
            cabecalho.Columns.Add("tipomovimento", typeof(string));
            cabecalho.Columns.Add("situacao", typeof(string));
            cabecalho.Columns.Add("sala", typeof(string));
            cabecalho.Columns.Add("data", typeof(string));
            cabecalho.Columns.Add("observacao", typeof(string));
            cabecalho.Columns.Add("estabelecimentosaude", typeof(string));
            cabecalho.Columns.Add("saladestino", typeof(string));
            cabecalho.Columns.Add("responsavel", typeof(string));
            cabecalho.Columns.Add("responsavelenvio", typeof(string));
            cabecalho.Columns.Add("responsavelrecebimento", typeof(string));
            cabecalho.Columns.Add("dataenvio", typeof(string));
            cabecalho.Columns.Add("datarecebimento", typeof(string));

            DataTable corpo = new DataTable();
            corpo.Columns.Add("co_movimento", typeof(long));
            corpo.Columns.Add("co_item", typeof(long));
            corpo.Columns.Add("vacina", typeof(string));
            corpo.Columns.Add("fabricante", typeof(string));
            corpo.Columns.Add("lote", typeof(string));
            corpo.Columns.Add("aplicacao", typeof(string));
            corpo.Columns.Add("validade", typeof(string));
            corpo.Columns.Add("quantidade", typeof(int));

            DataTable historicoitens = new DataTable();
            historicoitens.Columns.Add("co_movimento", typeof(long));
            historicoitens.Columns.Add("co_item", typeof(long));
            historicoitens.Columns.Add("vacina", typeof(string));
            historicoitens.Columns.Add("fabricante", typeof(string));
            historicoitens.Columns.Add("lote", typeof(string));
            historicoitens.Columns.Add("aplicacao", typeof(string));
            historicoitens.Columns.Add("validade", typeof(string));
            historicoitens.Columns.Add("motivo", typeof(string));
            historicoitens.Columns.Add("horario", typeof(string));
            historicoitens.Columns.Add("qtdanterior", typeof(int));
            historicoitens.Columns.Add("qtdalterada", typeof(int));

            MovimentoVacina movimento = iMovimento.BuscarPorCodigo<MovimentoVacina>(co_movimento);

            DataRow row = cabecalho.NewRow();
            row["co_movimento"] = movimento.Codigo;
            row["numero"] = movimento.Numero;
            row["tipomovimento"] = movimento.TipoMovimento.Nome;
            row["situacao"] = movimento.Operacao.Nome;
            row["sala"] = movimento.Sala.Nome;
            row["data"] = movimento.Data.ToString("dd/MM/yyyy");
            row["observacao"] = movimento.Observacao;

            if (movimento.Motivo != null)
                row["motivo"] = movimento.Motivo.Nome;

            if (movimento.EstabelecimentoSaude != null)
                row["estabelecimentosaude"] = movimento.EstabelecimentoSaude.NomeFantasia;

            if (movimento.SalaDestino != null)
                row["saladestino"] = movimento.SalaDestino.Nome;

            if (!string.IsNullOrEmpty(movimento.Responsavel))
                row["responsavel"] = movimento.Responsavel;

            if (!string.IsNullOrEmpty(movimento.ResponsavelEnvio))
                row["responsavelenvio"] = movimento.ResponsavelEnvio;

            if (!string.IsNullOrEmpty(movimento.ResponsavelRecebimento))
                row["responsavelrecebimento"] = movimento.ResponsavelRecebimento;

            if (movimento.DataEnvio.HasValue)
                row["dataenvio"] = movimento.DataEnvio.Value.ToString("dd/MM/yyyy");

            if (movimento.DataRecebimento.HasValue)
                row["datarecebimento"] = movimento.DataRecebimento.Value.ToString("dd/MM/yyyy");

            cabecalho.Rows.Add(row);

            IList<ItemMovimentoVacina> itens = iMovimento.BuscarItensMovimentacao<ItemMovimentoVacina>(co_movimento);

            foreach (ItemMovimentoVacina item in itens)
            {
                row = corpo.NewRow();
                row["co_movimento"] = movimento.Codigo;
                row["co_item"] = item.Codigo;
                row["vacina"] = item.NomeVacina;
                row["fabricante"] = item.NomeFabricante;
                row["lote"] = item.Identificacao;
                row["aplicacao"] = item.AplicacaoVacina.ToString();
                row["validade"] = item.DataValidade.ToString("dd/MM/yyyy");
                row["quantidade"] = item.Quantidade;

                corpo.Rows.Add(row);

                IList<HistoricoItemMovimentoVacina> historicos = iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(item.Codigo);

                foreach (HistoricoItemMovimentoVacina historico in historicos)
                {
                    row = historicoitens.NewRow();
                    row["co_movimento"] = movimento.Codigo;
                    row["co_item"] = item.Codigo;
                    row["vacina"] = item.NomeVacina;
                    row["fabricante"] = item.NomeFabricante;
                    row["lote"] = item.Identificacao;
                    row["aplicacao"] = item.AplicacaoVacina.ToString();
                    row["validade"] = item.DataValidade.ToString("dd/MM/yyyy");
                    row["qtdanterior"] = historico.QuantidadeAnterior;
                    row["qtdalterada"] = historico.QuantidadeAlterada;
                    row["motivo"] = historico.Motivo;
                    row["horario"] = historico.Data.ToString("dd/MM/yyyy HH:mm");

                    historicoitens.Rows.Add(row);
                }
            }

            relatorio.Database.Tables["cabecalho"].SetDataSource(cabecalho);
            relatorio.Database.Tables["corpo"].SetDataSource(corpo);
            relatorio.Subreports["RelHistoricoItemMovimentacao.rpt"].SetDataSource(historicoitens);

            return relatorio;
        }
        ReportDocument IRelatorioVacina.ObterCartaoVacina<P, A, C>(P _paciente, A _avatar, IList<C> _cartoes)
        {
            ReportDocument relatorio = new ReportDocument();
            relatorio.Load(HttpContext.Current.Server.MapPath("~/bin/Vacina/Relatorios/RelImpressaoCartaoVacina.rpt"));

            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)(object)_paciente;
            AvatarCartaoVacina avatar = (AvatarCartaoVacina)(object)_avatar;
            IList<CartaoVacina> cartoes = (IList<CartaoVacina>)(object)_cartoes;

            DataTable header = new DataTable();
            header.Columns.Add(new DataColumn("imagemtopo", typeof(Byte[])));
            header.Columns.Add(new DataColumn("imagemcabecalho", typeof(Byte[])));
            header.Columns.Add(new DataColumn("imagemnome", typeof(Byte[])));
            header.Columns.Add(new DataColumn("nome", typeof(string)));
            header.Columns.Add(new DataColumn("co_paciente", typeof(string)));

            DataRow linha = header.NewRow();

            linha["imagemtopo"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath(avatar.RetornaImagemTopo()));
            linha["imagemcabecalho"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath(avatar.RetornaImagemCabecalho()));
            linha["imagemnome"] = this.ImagemDinamica(HttpContext.Current.Request.MapPath("~/Vacina/img/CartaoVacina/Temas/nome-campo.png"));
            linha["nome"] = paciente.Nome;
            linha["co_paciente"] = paciente.Codigo;
            header.Rows.Add(linha);

            DataTable body = new DataTable();
            body.Columns.Add(new DataColumn("vacina", typeof(string)));
            body.Columns.Add(new DataColumn("dose", typeof(string)));
            body.Columns.Add(new DataColumn("dataaplicacao", typeof(string)));
            body.Columns.Add(new DataColumn("proximadose", typeof(string)));
            body.Columns.Add(new DataColumn("lote", typeof(string)));
            body.Columns.Add(new DataColumn("validadelote", typeof(string)));
            body.Columns.Add(new DataColumn("cnesunidade", typeof(string)));
            body.Columns.Add(new DataColumn("estabelecimento", typeof(string)));
            body.Columns.Add(new DataColumn("co_paciente", typeof(string)));
            body.Columns.Add(new DataColumn("codigo", typeof(long)));

            foreach (CartaoVacina cartao in cartoes)
            {
                DataRow row = body.NewRow();
                row["codigo"] = cartao.Codigo;
                row["vacina"] = cartao.VacinaImpressaoCartao;
                row["dose"] = cartao.DoseVacinaImpressaoCartao;
                row["dataaplicacao"] = cartao.DataAplicacaoVacinaImpressaoCartao;
                row["proximadose"] = cartao.ProximaDoseVacinaImpressaoCartao;
                row["lote"] = cartao.LoteVacinaImpressaoCartao;
                row["validadelote"] = cartao.ValidadeLoteImpressaoCartao;
                row["cnesunidade"] = cartao.CNESImpressaoCartao;
                row["estabelecimento"] = cartao.UnidadeVacinaImpressaoCartao;
                row["co_paciente"] = paciente.Codigo;
                body.Rows.Add(row);
            }

            relatorio.Database.Tables["dadoscartao"].SetDataSource(header);
            relatorio.Database.Tables["vacinas"].SetDataSource(body);

            return relatorio;
        }
        ReportDocument IRelatorioVacina.ObterRelatorioDispensacao(int co_dispensacao)
        {
            ReportDocument relatorio = new ReportDocument();
            relatorio.Load(HttpContext.Current.Server.MapPath("~/bin/Vacina/Relatorios/RelDispensacaoVacina.rpt"));

            IParametrizacaoVacina iParametrizacao = Factory.GetInstance<IParametrizacaoVacina>();

            DataTable conteudo = new DataTable();
            DataRow row = null;

            conteudo.Columns.Add(new DataColumn("Vacina", typeof(string)));
            conteudo.Columns.Add(new DataColumn("Lote", typeof(string)));
            conteudo.Columns.Add(new DataColumn("Fabricante", typeof(string)));
            conteudo.Columns.Add(new DataColumn("Validade", typeof(string)));
            conteudo.Columns.Add(new DataColumn("Dose", typeof(string)));
            conteudo.Columns.Add(new DataColumn("ProximaVacina", typeof(string)));

            IList<ItemDispensacaoVacina> itens = Factory.GetInstance<ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc.IDispensacao>().BuscarItensDispensacao<ItemDispensacaoVacina>(co_dispensacao);

            foreach (ItemDispensacaoVacina item in itens)
            {
                row = conteudo.NewRow();
                row["Vacina"] = item.Lote.ItemVacina.Vacina.Nome;
                row["Lote"] = item.Lote.Identificacao;
                row["Fabricante"] = item.Lote.ItemVacina.FabricanteVacina.Nome;
                row["Validade"] = item.Lote.DataValidade.ToString("dd/MM/yyyy");
                row["Dose"] = item.Dose.Descricao;

                IList<ParametrizacaoVacina> parametrizacoes = iParametrizacao.BuscaProximaDose<ParametrizacaoVacina>(item.Dose.Codigo, item.Lote.ItemVacina.Vacina.Codigo);
                string dataproxima = CartaoVacina.RetornaProximaDose(parametrizacoes, item.Dispensacao.Data);
                row["ProximaVacina"] = string.IsNullOrEmpty(dataproxima) ? "NoRegisters" : dataproxima;

                conteudo.Rows.Add(row);
            }

            DSCabecalhoDispensacaoVacina DsCabecalho = new DSCabecalhoDispensacaoVacina();
            DSCorpoDispensacaoVacina DsCorpo = new DSCorpoDispensacaoVacina();

            DsCabecalho.Tables.Add(this.RetornarCabecalhoRelatorioDispensacao(co_dispensacao));
            DsCorpo.Tables.Add(conteudo);

            relatorio.SetDataSource(DsCabecalho.Tables[1]);
            relatorio.Subreports[0].SetDataSource(DsCorpo.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioVacina.ObterRelatorioConferenciaInventario(int co_inventario)
        {
            ReportDocument relatorio = new ReportDocument();
            relatorio.Load(HttpContext.Current.Server.MapPath("~/bin/Vacina/Relatorios/RelInventarioConferenciaVacina.rpt"));

            DataTable conteudo = new DataTable();
            conteudo.Columns.Add(new DataColumn("vacina", typeof(string)));
            conteudo.Columns.Add(new DataColumn("unidade", typeof(string)));
            conteudo.Columns.Add(new DataColumn("lote", typeof(string)));
            conteudo.Columns.Add(new DataColumn("fabricante", typeof(string)));
            conteudo.Columns.Add(new DataColumn("validade", typeof(string)));

            IList<ItemInventarioVacina> itensinventario = Factory.GetInstance<IInventarioVacina>().ListarItensInventario<ItemInventarioVacina>(co_inventario);

            foreach (ItemInventarioVacina item in itensinventario)
            {
                DataRow row = conteudo.NewRow();
                row["vacina"] = item.LoteVacina.ItemVacina.Vacina.Nome;
                row["unidade"] = item.LoteVacina.ItemVacina.Vacina.UnidadeMedida.Sigla;
                row["lote"] = item.LoteVacina.Identificacao;
                row["fabricante"] = item.LoteVacina.ItemVacina.FabricanteVacina.Nome;
                row["validade"] = item.LoteVacina.DataValidade.ToString("dd/MM/yyyy");
                conteudo.Rows.Add(row);
            }

            DSCabecalhoInventarioVacina dscabecalho = new DSCabecalhoInventarioVacina();
            dscabecalho.Tables.Add(this.RetornarCabecalhoRelatorioInventario(co_inventario));

            DSRelInventarioConferencia dscorpo = new DSRelInventarioConferencia();
            dscorpo.Tables.Add(conteudo);

            relatorio.SetDataSource(dscorpo.Tables[1]);
            relatorio.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            return relatorio;
        }
        ReportDocument IRelatorioVacina.ObterRelatorioFinalInventario(int co_inventario)
        {
            ReportDocument relatorio = new ReportDocument();
            relatorio.Load(HttpContext.Current.Server.MapPath("~/bin/Vacina/Relatorios/RelInventarioFinalVacina.rpt"));

            DataTable conteudo = new DataTable();
            conteudo.Columns.Add(new DataColumn("vacina", typeof(string)));
            conteudo.Columns.Add(new DataColumn("unidade", typeof(string)));
            conteudo.Columns.Add(new DataColumn("lote", typeof(string)));
            conteudo.Columns.Add(new DataColumn("fabricante", typeof(string)));
            conteudo.Columns.Add(new DataColumn("validade", typeof(string)));
            conteudo.Columns.Add(new DataColumn("quantidadecontada", typeof(string)));
            conteudo.Columns.Add(new DataColumn("quantidadeestoque", typeof(string)));
            conteudo.Columns.Add(new DataColumn("quantidadediferenca", typeof(string)));

            IList<ItemInventarioVacina> itensinventario = Factory.GetInstance<IInventarioVacina>().ListarItensInventario<ItemInventarioVacina>(co_inventario);

            foreach (ItemInventarioVacina item in itensinventario)
            {
                DataRow row = conteudo.NewRow();
                row["vacina"] = item.LoteVacina.ItemVacina.Vacina.Nome;
                row["unidade"] = item.LoteVacina.ItemVacina.Vacina.UnidadeMedida.Sigla;
                row["lote"] = item.LoteVacina.Identificacao;
                row["fabricante"] = item.LoteVacina.ItemVacina.FabricanteVacina.Nome;
                row["validade"] = item.LoteVacina.DataValidade.ToString("dd/MM/yyyy");
                row["quantidadecontada"] = item.QtdContada;
                row["quantidadeestoque"] = item.QtdEstoque;
                row["quantidadediferenca"] = item.QtdContada - item.QtdEstoque;
                conteudo.Rows.Add(row);
            }

            DSCabecalhoInventarioVacina dscabecalho = new DSCabecalhoInventarioVacina();
            dscabecalho.Tables.Add(this.RetornarCabecalhoRelatorioInventario(co_inventario));

            DSRelInventarioFinal dscorpo = new DSRelInventarioFinal();
            dscorpo.Tables.Add(conteudo);

            relatorio.SetDataSource(dscorpo.Tables[1]);
            relatorio.Subreports[0].SetDataSource(dscabecalho.Tables[1]);

            return relatorio;
        }

        MemoryStream IRelatorioVacina.ObterRelatorioProducao<T>(IList<T> _unidades, DateTime datainicio, DateTime datafim)
        {
            //IRelatorioVacina iRelatorio = Factory.GetInstance<IRelatorioVacina>();
            HSSFWorkbook _doc = new HSSFWorkbook();
            HSSFSheet _planilha = _doc.CreateSheet("Produção Períodica");

            int posLinha = 1; //Linha inicial do documento

            //Criando Cabeçalho com colunas 'A' e 'B'
            int xMinColuna = 1, xMaxColuna = 7;
            int yMinColuna = xMaxColuna + 1, yMaxColuna = yMinColuna + 4;

            HSSFRow _linha = _planilha.CreateRow(posLinha);
            for (int i = xMinColuna; i <= yMaxColuna; i++)
                _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, true);

            //Mesclando celulas das colunas do cabeçalho
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, xMaxColuna));
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, yMinColuna, yMaxColuna));

            //Setando valores para o cabeçalho
            _linha.GetCell(xMinColuna).SetCellValue("ESTABELECIMENTO DE SAÚDE");
            _linha.GetCell(yMinColuna).SetCellValue("QUANTIDADE APLICADA");

            posLinha++;
            ViverMais.Model.EstabelecimentoSaude[] unidades = ((IList<ViverMais.Model.EstabelecimentoSaude>)(object)_unidades).ToArray();

            foreach (ViverMais.Model.EstabelecimentoSaude unidade in unidades)
            {
                _linha = _planilha.CreateRow(posLinha);
                for (int i = xMinColuna; i <= yMaxColuna; i++)
                    _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, false);

                _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, xMaxColuna));
                _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, yMinColuna, yMaxColuna));

                _linha.GetCell(xMinColuna).SetCellValue(unidade.NomeFantasia);
                _linha.GetCell(yMinColuna).SetCellValue(this.QuantidadeDispensada(unidade.CNES, datainicio, datafim).ToString());

                posLinha++;
            }

            //Criando rodapé
            _linha = _planilha.CreateRow(posLinha);
            for (int i = xMinColuna; i <= yMaxColuna; i++)
                _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, true);

            //Setando valor do rodapé
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, yMaxColuna));
            _linha.GetCell(xMinColuna).SetCellValue("Relatório referente ao período de " + datainicio.ToString("dd/MM/yyyy") + " a " + datafim.ToString("dd/MM/yyyy") + ".");

            MemoryStream ms = new MemoryStream();
            _doc.Write(ms);

            return ms;
        }
        MemoryStream IRelatorioVacina.ObterRelatorioProducao<T>(IList<T> unidades, DateTime data)
        {
            HSSFWorkbook _doc = new HSSFWorkbook();
            HSSFSheet _planilha = _doc.CreateSheet("Producao Diaria");

            int posLinha = 1; //Linha inicial do documento

            //Criando Cabeçalho com colunas 'A' e 'B'
            int xMinColuna = 1, xMaxColuna = 7;
            int yMinColuna = xMaxColuna + 1, yMaxColuna = yMinColuna + 4;

            HSSFRow _linha = _planilha.CreateRow(posLinha);
            for (int i = xMinColuna; i <= yMaxColuna; i++)
                _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, true);

            //Mesclando celulas das colunas do cabeçalho
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, xMaxColuna));
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, yMinColuna, yMaxColuna));

            //Setando valores para o cabeçalho
            _linha.GetCell(xMinColuna).SetCellValue("ESTABELECIMENTO DE SAÚDE");
            _linha.GetCell(yMinColuna).SetCellValue("QUANTIDADE APLICADA");

            posLinha++;

            foreach (ViverMais.Model.EstabelecimentoSaude unidade in (IList<ViverMais.Model.EstabelecimentoSaude>)(object)unidades)
            {
                _linha = _planilha.CreateRow(posLinha);
                for (int i = xMinColuna; i <= yMaxColuna; i++)
                    _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, false);

                _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, xMaxColuna));
                _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, yMinColuna, yMaxColuna));

                _linha.GetCell(xMinColuna).SetCellValue(unidade.NomeFantasia);
                _linha.GetCell(yMinColuna).SetCellValue(this.QuantidadeDispensada(unidade.CNES, data).ToString());

                posLinha++;
            }

            //Criando rodapé
            _linha = _planilha.CreateRow(posLinha);
            for (int i = xMinColuna; i <= yMaxColuna; i++)
                _linha.CreateCell(i).CellStyle = EstiloCabecalhoRelatorioProducao(_doc, true);

            //Setando valor do rodapé
            _planilha.AddMergedRegion(new CellRangeAddress(posLinha, posLinha, xMinColuna, yMaxColuna));
            _linha.GetCell(xMinColuna).SetCellValue("Relatório referente ao dia " + data.ToString("dd/MM/yyyy") + ".");

            MemoryStream ms = new MemoryStream();
            _doc.Write(ms);

            return ms;
        }

        private int QuantidadeDispensada(string co_unidade, DateTime data)
        {
            string hql = "FROM ViverMais.Model.ItemDispensacaoVacina AS i WHERE i.Dispensacao.Sala.EstabelecimentoSaude.CNES = '" + co_unidade + "'";
            hql += " AND TO_CHAR(i.Dispensacao.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "'";
            return Session.CreateQuery(hql).List<ItemDispensacaoVacina>().Count();
        }
        private int QuantidadeDispensada(string co_unidade, DateTime datainicio, DateTime datafim)
        {
            string hql = "SELECT COUNT(i.Codigo) FROM ViverMais.Model.ItemDispensacaoVacina AS i, ViverMais.Model.DispensacaoVacina d" +
                " WHERE i.Dispensacao.Codigo = d.Codigo AND i.Dispensacao.Sala.EstabelecimentoSaude.CNES = '" + co_unidade + "'" +
                " AND i.Dispensacao.Data BETWEEN TO_DATE('" + datainicio.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
                + " AND TO_DATE('" + datafim.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')";
            return int.Parse(Session.CreateQuery(hql).UniqueResult().ToString());
        }

        private DataTable RetornarCabecalhoRelatorioDispensacao(int co_dispensacao)
        {
            DataTable tab = new DataTable();
            tab.Columns.Add(new DataColumn("UnidadeSaude", typeof(string)));
            tab.Columns.Add(new DataColumn("Nome", typeof(string)));
            tab.Columns.Add(new DataColumn("CartaoSUS", typeof(string)));
            tab.Columns.Add(new DataColumn("Data", typeof(string)));
            tab.Columns.Add(new DataColumn("CodigoDispensacao", typeof(string)));

            DataRow row = tab.NewRow();

            DispensacaoVacina dispensacao = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<DispensacaoVacina>(co_dispensacao);
            row["UnidadeSaude"] = dispensacao.Sala.EstabelecimentoSaude.NomeFantasia;
            row["Nome"] = dispensacao.Paciente.Nome;
            IList<ViverMais.Model.CartaoSUS> cartoes = Factory.GetInstance<IPaciente>().ListarCartoesSUS<ViverMais.Model.CartaoSUS>(dispensacao.Paciente.Codigo);
            row["CartaoSUS"] = (from c in cartoes select long.Parse(c.Numero)).Min().ToString();
            row["Data"] = dispensacao.Data.ToString("dd/MM/yyyy");
            row["CodigoDispensacao"] = dispensacao.Codigo;

            tab.Rows.Add(row);

            return tab;
        }
        private DataTable RetornarCabecalhoRelatorioInventario(int co_inventario)
        {
            DataTable tab = new DataTable();
            InventarioVacina inventario = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<InventarioVacina>(co_inventario);
            tab.Columns.Add(new DataColumn("unidade", typeof(string)));
            tab.Columns.Add(new DataColumn("sala", typeof(string)));
            tab.Columns.Add(new DataColumn("dataabertura", typeof(string)));

            DataRow row = tab.NewRow();
            row["unidade"] = Factory.GetInstance<IEstabelecimentoSaude>().BuscarPorCodigo<ViverMais.Model.EstabelecimentoSaude>(inventario.Sala.EstabelecimentoSaude.CNES);
            row["sala"] = inventario.Sala.Nome;
            row["dataabertura"] = inventario.DataInventario.ToString("dd/MM/yyyy");

            tab.Rows.Add(row);

            return tab;
        }

        private Byte[] ImagemDinamica(string pathimagem)
        {
            FileStream fs = new FileStream(pathimagem, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            Byte[] temp = new Byte[(int)br.BaseStream.Length];
            //Byte[] temp = new Byte[fs.Length];
            temp = br.ReadBytes((int)br.BaseStream.Length);
            //fs.Read(temp, 0, Convert.ToInt32(fs.Length));
            br = null;
            fs.Close();
            fs = null;
            return temp;
        }

        private HSSFCellStyle EstiloCabecalhoRelatorioProducao(HSSFWorkbook _doc, bool cabecalhorodape)
        {
            HSSFCellStyle estilo = _doc.CreateCellStyle();

            if (cabecalhorodape)
            {
                estilo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.GREY_40_PERCENT.index; //Cor de fundo
                estilo.FillPattern = 1; //Aplicando estilo para cor de fundo

                HSSFFont fonte = _doc.CreateFont();
                fonte.Color = HSSFColor.BLACK.index;
                fonte.Boldweight = 14;
                fonte.FontHeightInPoints = 14;
                fonte.FontName = "Times New Roman";
                estilo.SetFont(fonte);
            }

            estilo.Alignment = HSSFCellStyle.ALIGN_CENTER; //Alinhando horizontalmente
            estilo.BorderBottom = HSSFCellStyle.BORDER_MEDIUM; //Borda de baixo
            estilo.BottomBorderColor = HSSFColor.BLACK.index; //Cor da borda de baixo
            estilo.BorderLeft = HSSFCellStyle.BORDER_MEDIUM; //Borda da esquerda
            estilo.LeftBorderColor = HSSFColor.BLACK.index;//Cor da borda da esquerda
            estilo.BorderRight = HSSFCellStyle.BORDER_MEDIUM; //Borda da direita
            estilo.RightBorderColor = HSSFColor.BLACK.index;//Cor da borda da direita
            estilo.BorderTop = HSSFCellStyle.BORDER_MEDIUM; //Borda de cima
            estilo.TopBorderColor = HSSFColor.BLACK.index;//Cor da borda de cima

            return estilo;
        }

        //void IRelatorioVacina.CorrigirDuplicatas()
        //{
        //    string hql = "select cv.Codigo, i.Codigo, d.Codigo from DispensacaoVacina d, ItemDispensacaoVacina i, CartaoVacina cv, LoteVacina l," +
        //            "ItemVacina iv, FabricanteVacina f" +
        //            " where d.Codigo = i.Dispensacao.Codigo " +
        //            " and cv.DispensacaoVacina.Codigo = d.Codigo " +
        //            " and cv.DoseVacina.Codigo = i.Dose.Codigo and cv.Vacina.Codigo = iv.Vacina.Codigo " +
        //            " and l.Codigo = i.Lote.Codigo and iv.Codigo = l.ItemVacina.Codigo " +
        //            " and l.Identificacao = cv.Lote and cv.SalaVacina.Codigo = d.Sala.Codigo " +
        //            " and f.Codigo = iv.FabricanteVacina.Codigo and cv.DispensacaoVacina is not null " +
        //            " group by cv.Codigo, i.Codigo, d.Codigo " +
        //            " order by i.Codigo";

        //    var objetos = Session.CreateQuery(hql).List().Cast<Object[]>().Select(p => new { CodigoCartaoVacina = ((object[])p)[0], CodigoItemDispensacao = ((object[])p)[1], CodigoDispensacao = ((object[])p)[2] }).ToList();

        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            foreach (var o in objetos)
        //            {
        //                CartaoVacina cartao = Factory.GetInstance<ICartaoVacina>().BuscarPorCodigo<CartaoVacina>(o.CodigoCartaoVacina);
        //                cartao.ItemDispensacao = Factory.GetInstance<IVacinaServiceFacade>().BuscarPorCodigo<ItemDispensacaoVacina>(o.CodigoItemDispensacao);
        //                Session.Update(cartao);
        //            }

        //            Session.Transaction.Commit();
        //        }
        //        catch (Exception ex)
        //        {
        //            Session.Transaction.Rollback();
        //            throw ex;
        //        }
        //    }
        //}
    }
}
