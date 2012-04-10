using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.Model;
using System.Data;
using System.Collections;

namespace ViverMais.DAO.Farmacia.Movimentacao
{
    public class MovimentacaoDAO : FarmaciaServiceFacadeDAO, IMovimentacao
    {
        IList<T> IMovimentacao.BuscarPorTipoMovimento<T>(int co_tipomovimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Movimento AS m WHERE m.TipoMovimento.Codigo = " + co_tipomovimento;
            hql += " ORDER BY m.Data DESC";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentacao.BuscarPorFarmacia<T>(int co_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Movimento AS m WHERE m.Farmacia.Codigo = " + co_farmacia;
            hql += " ORDER BY m.Data DESC";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentacao.BuscarItensPorMovimento<T>(int co_movimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemMovimentacao AS im WHERE im.Movimento.Codigo = " + co_movimento;
            hql += " ORDER BY im.LoteMedicamento.Lote";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentacao.BuscarItensRemanejamentoPorRemanejamento<T>(int co_remanejamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemRemanejamento AS ir WHERE ir.Remanejamento.Codigo = " + co_remanejamento;
            hql += " ORDER BY ir.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentacao.BuscarRemanejamentosPorFarmacia<T>(int co_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoMedicamento AS r WHERE r.Movimento.Farmacia_Destino.Codigo = " + co_farmacia;
            hql += " ORDER BY r.DataAbertura, r.Status";
            return Session.CreateQuery(hql).List<T>();
        }

        void IMovimentacao.ConfirmarRecebimentoMedicamento<T>(T _itemremanejamento)
        {
            ItemRemanejamento itemRemanejamento = (ItemRemanejamento)(object)_itemremanejamento;

            using (Session.BeginTransaction())
            {
                try
                {
                    //if (ir.DataAlteracao.HasValue)
                    //    ir.QuantidadeRecebida = ir.QuantidadeAlterada.Value;

                    Session.Update(itemRemanejamento);

                    Estoque estoqueItem = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemRemanejamento.Remanejamento.Movimento.Farmacia_Destino.Codigo, itemRemanejamento.LoteMedicamento.Codigo);

                    if (estoqueItem == null)
                    {
                        estoqueItem = new Estoque();
                        estoqueItem.Farmacia = itemRemanejamento.Remanejamento.Movimento.Farmacia_Destino;
                        estoqueItem.LoteMedicamento = itemRemanejamento.LoteMedicamento;
                        estoqueItem.QuantidadeEstoque = itemRemanejamento.QuantidadeRecebida.Value;

                        Session.Save(estoqueItem);
                    }
                    else
                    {
                        estoqueItem.QuantidadeEstoque += itemRemanejamento.QuantidadeRecebida.Value;
                        Session.Update(estoqueItem);
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        private DataTable RetornaCabecalhoRemanejamento()
        {
            DataTable tab = new DataTable();
            tab.Columns.Add(new DataColumn("idremanejamento", typeof(string)));
            tab.Columns.Add(new DataColumn("farmacia", typeof(string)));
            tab.Columns.Add(new DataColumn("farmaciaorigem", typeof(string)));
            tab.Columns.Add(new DataColumn("dataenvio", typeof(string)));
            tab.Columns.Add(new DataColumn("observacao", typeof(string)));
            tab.Columns.Add(new DataColumn("responsavelenvio", typeof(string)));
            tab.Columns.Add(new DataColumn("responsavelrecebimento", typeof(string)));

            return tab;
        }

        private DataTable RetornaCorpoRemanejamento()
        {
            DataTable tab = new DataTable();
            tab.Columns.Add(new DataColumn("idremanejamento", typeof(string)));
            tab.Columns.Add(new DataColumn("medicamento", typeof(string)));
            tab.Columns.Add(new DataColumn("lote", typeof(string)));
            tab.Columns.Add(new DataColumn("quantidade", typeof(string)));

            return tab;
        }

        private DataTable RetornaCabecalhoMovimento()
        {
            DataTable tab = new DataTable();
            tab.Columns.Add(new DataColumn("idmovimento", typeof(string)));
            tab.Columns.Add(new DataColumn("farmacia", typeof(string)));
            tab.Columns.Add(new DataColumn("tipomovimento", typeof(string)));
            tab.Columns.Add(new DataColumn("data", typeof(string)));
            tab.Columns.Add(new DataColumn("motivo", typeof(string)));
            tab.Columns.Add(new DataColumn("situacao", typeof(string)));
            tab.Columns.Add(new DataColumn("observacao", typeof(string)));
            tab.Columns.Add(new DataColumn("responsavelenvio", typeof(string)));
            tab.Columns.Add(new DataColumn("responsavelrecebimento", typeof(string)));
            tab.Columns.Add(new DataColumn("farmaciadestino", typeof(string)));
            tab.Columns.Add(new DataColumn("setordestino", typeof(string)));
            tab.Columns.Add(new DataColumn("responsavel", typeof(string)));

            return tab;
        }

        private DataTable RetornaCorpoMovimento()
        {
            DataTable tab = new DataTable();
            tab.Columns.Add(new DataColumn("idmedicamento", typeof(string)));
            tab.Columns.Add(new DataColumn("idmovimento", typeof(string)));
            tab.Columns.Add(new DataColumn("medicamento", typeof(string)));
            tab.Columns.Add(new DataColumn("lote", typeof(string)));
            tab.Columns.Add(new DataColumn("fabricante", typeof(string)));
            tab.Columns.Add(new DataColumn("quantidade", typeof(string)));

            return tab;
        }

        Hashtable IMovimentacao.RetornaHashMovimentacaoRemanejamento<T>(int co_tipo, T lista)
        {
            Hashtable hash = new Hashtable();

            if (co_tipo == 1) //Movimentação
            {
                IList <ViverMais.Model.Movimento> movimentos = (IList<ViverMais.Model.Movimento>)(object)lista;
                DataTable cab = RetornaCabecalhoMovimento();
                DataTable corpo = RetornaCorpoMovimento();

                foreach (ViverMais.Model.Movimento movimento in movimentos)
                {
                    DataRow rowDataTable = cab.NewRow();
                    rowDataTable["idmovimento"] = movimento.Codigo;
                    rowDataTable["farmacia"] = movimento.Farmacia.Nome;
                    rowDataTable["tipomovimento"] = movimento.TipoMovimento.Nome;
                    rowDataTable["data"] = movimento.Data.ToString("dd/MM/yyyy HH:mm:ss");
                    rowDataTable["observacao"] = string.IsNullOrEmpty(movimento.Observacao) ? " - " : movimento.Observacao;

                    if (movimento.TipoOperacaoMovimento != null)
                        rowDataTable["situacao"] = movimento.TipoOperacaoMovimento.Descricao;
                    if (movimento.Motivo != null)
                        rowDataTable["motivo"] = movimento.Motivo.Nome;
                    if (!string.IsNullOrEmpty(movimento.Responsavel_Envio))
                        rowDataTable["responsavelenvio"] = movimento.Responsavel_Envio;
                    if (!string.IsNullOrEmpty(movimento.Responsavel_Recebimento))
                        rowDataTable["responsavelrecebimento"] = movimento.Responsavel_Recebimento;
                    if (movimento.Farmacia_Destino != null)
                        rowDataTable["farmaciadestino"] = movimento.Farmacia_Destino.Nome;
                    if (movimento.Setor_Destino != null)
                        rowDataTable["setordestino"] = movimento.Setor_Destino.Nome;
                    if (!string.IsNullOrEmpty(movimento.ResponsavelMovimento))
                        rowDataTable["responsavel"] = movimento.ResponsavelMovimento;

                    cab.Rows.Add(rowDataTable);

                    IList<ItemMovimentacao> lim = Factory.GetInstance<IMovimentacao>().BuscarItensPorMovimento<ItemMovimentacao>(movimento.Codigo);
                    foreach (ItemMovimentacao im in lim)
                    {
                        rowDataTable = corpo.NewRow();
                        rowDataTable["idmovimento"] = im.Movimento.Codigo;
                        rowDataTable["idmedicamento"] = im.LoteMedicamento.Medicamento.CodMedicamento;
                        rowDataTable["medicamento"] = im.LoteMedicamento.Medicamento.Nome;
                        rowDataTable["lote"] = im.LoteMedicamento.Lote;
                        rowDataTable["fabricante"] = im.LoteMedicamento.Fabricante.Nome;
                        rowDataTable["quantidade"] = im.Quantidade;
                        corpo.Rows.Add(rowDataTable);
                    }
                }

                hash.Add("cabecalho", cab);
                hash.Add("corpo", corpo);
            }
            else //Remanejamento
            {
                IList<RemanejamentoMedicamento> lrm = (IList<RemanejamentoMedicamento>)(object)lista;
                DataTable cab = RetornaCabecalhoRemanejamento();
                DataTable corpo = RetornaCorpoRemanejamento();

                foreach (RemanejamentoMedicamento rm in lrm)
                {
                    DataRow r = cab.NewRow();
                    r["idremanejamento"] = rm.Codigo;
                    r["farmacia"] = rm.Movimento.Farmacia_Destino.Nome;
                    r["farmaciaorigem"] = rm.Movimento.Farmacia.Nome;
                    r["dataenvio"] = rm.DataAbertura.ToString("dd/MM/yyyy HH:mm:ss");
                    r["observacao"] = string.IsNullOrEmpty(rm.Movimento.Observacao) ? " - " : rm.Movimento.Observacao;
                    r["responsavelenvio"] = rm.Movimento.Responsavel_Envio.ToString();
                    r["responsavelrecebimento"] = rm.Movimento.Responsavel_Recebimento.ToString();
                    cab.Rows.Add(r);

                    IList<ItemRemanejamento> lir = Factory.GetInstance<IMovimentacao>().BuscarItensRemanejamentoPorRemanejamento<ItemRemanejamento>(rm.Codigo);

                    foreach (ItemRemanejamento ir in lir)
                    {
                        r = corpo.NewRow();
                        r["idremanejamento"] = ir.Remanejamento.Codigo;
                        r["medicamento"] = ir.LoteMedicamento.Medicamento.Nome;
                        r["lote"] = ir.LoteMedicamento.Lote;
                        r["quantidade"] = ir.QuantidadeRegistrada;

                        corpo.Rows.Add(r);
                    }
                }

                hash.Add("cabecalho", cab);
                hash.Add("corpo", corpo);
            }

            return hash;
        }
    }
}
