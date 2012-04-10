using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace ViverMais.DAO.Farmacia.Movimentacao
{
    public class EstoqueDAO : FarmaciaServiceFacadeDAO, IEstoque
    {
        IList<T> IEstoque.BuscarPorFarmacia<T>(int id_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque AS e WHERE e.Farmacia.Codigo = " + id_farmacia;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IEstoque.BuscarPorDescricao<T>(int co_farmacia, int co_fabricante, int co_medicamento, string lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque AS e WHERE e.Farmacia.Codigo = " + co_farmacia;

            if (co_fabricante != -1)
                hql += " AND e.LoteMedicamento.Fabricante.Codigo = " + co_fabricante;

            if (co_medicamento != -1)
                hql += " AND e.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;

            if (!string.IsNullOrEmpty(lote))
                hql += " AND e.LoteMedicamento.Lote LIKE '" + lote + "%'";

            hql += " ORDER BY e.LoteMedicamento.Medicamento.Nome, e.LoteMedicamento.Lote, e.LoteMedicamento.Fabricante.Nome";

            return Session.CreateQuery(hql).List<T>();
        }

        T IEstoque.BuscarItemEstoquePorFarmacia<T>(int co_farmacia, int co_lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque AS e WHERE e.LoteMedicamento.Codigo = " + co_lote;
            hql += " AND e.Farmacia.Codigo = " + co_farmacia;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        void IEstoque.MovimentarEstoque<M, L>(M _movimentacao, IList<L> _itensmovimentacao)
        {
            Movimento movimentacao = (Movimento)(object)_movimentacao;
            IList<ItemMovimentacao> itensmovimentacao = (IList<ItemMovimentacao>)(object)_itensmovimentacao;
            RemanejamentoMedicamento remanejamento = null;

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(movimentacao);

                    if (movimentacao.TipoMovimento.Codigo == TipoMovimento.REMANEJAMENTO) //Remanejamento
                    {
                        remanejamento = new RemanejamentoMedicamento();
                        remanejamento.Movimento = movimentacao;
                        remanejamento.Status = RemanejamentoMedicamento.ABERTO;
                        remanejamento.DataAbertura = movimentacao.Data;

                        Session.Save(remanejamento);
                    }

                    foreach (ItemMovimentacao itemMovimentacao in itensmovimentacao)
                    {
                        itemMovimentacao.Movimento = movimentacao;
                        Session.Save(itemMovimentacao);

                        Estoque item_estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemMovimentacao.Movimento.Farmacia.Codigo, itemMovimentacao.LoteMedicamento.Codigo);

                        if (movimentacao.TipoMovimento.Codigo == TipoMovimento.DEVOLUCAO_PACIENTE)
                        {
                            if (item_estoque != null)
                            {
                                item_estoque.QuantidadeEstoque += itemMovimentacao.Quantidade;
                                Session.Update(item_estoque);
                            }
                            else
                            {
                                item_estoque = new Estoque();
                                item_estoque.Farmacia = itemMovimentacao.Movimento.Farmacia;
                                item_estoque.LoteMedicamento = itemMovimentacao.LoteMedicamento;
                                item_estoque.QuantidadeEstoque = itemMovimentacao.Quantidade;
                                Session.Save(item_estoque);
                            }
                        }
                        else
                        {
                            if (movimentacao.TipoMovimento.Codigo == TipoMovimento.DOACAO)
                            {
                                if (movimentacao.Farmacia.Codigo == ViverMais.Model.Farmacia.ALMOXARIFADO)
                                {
                                    if (movimentacao.TipoOperacaoMovimento.Codigo == TipoOperacaoMovimento.ENTRADA) //Entrada
                                    {
                                        if (item_estoque != null)
                                        {
                                            item_estoque.QuantidadeEstoque += itemMovimentacao.Quantidade;
                                            Session.Update(item_estoque);
                                        }
                                        else
                                        {
                                            item_estoque = new Estoque();
                                            item_estoque.Farmacia = itemMovimentacao.Movimento.Farmacia;
                                            item_estoque.LoteMedicamento = itemMovimentacao.LoteMedicamento;
                                            item_estoque.QuantidadeEstoque = itemMovimentacao.Quantidade;
                                            Session.Save(item_estoque);
                                        }
                                    }
                                    else //Saída
                                    {
                                        item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                        Session.Update(item_estoque);
                                    }
                                }
                                else
                                {
                                    item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                    Session.Update(item_estoque);
                                }
                            }
                            else
                            {
                                if (movimentacao.TipoMovimento.Codigo == TipoMovimento.REMANEJAMENTO)
                                {
                                    item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                    Session.Update(item_estoque);

                                    ItemRemanejamento ir = new ItemRemanejamento();
                                    ir.Remanejamento = remanejamento;
                                    ir.LoteMedicamento = itemMovimentacao.LoteMedicamento;
                                    ir.QuantidadeRegistrada = itemMovimentacao.Quantidade;

                                    Session.Save(ir);
                                }
                                else
                                {
                                    if (movimentacao.TipoMovimento.Codigo == TipoMovimento.PERDA)
                                    {
                                        item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                        Session.Update(item_estoque);
                                    }
                                    else
                                    {
                                        if (movimentacao.TipoMovimento.Codigo == TipoMovimento.TRANSFERENCIA_INTERNA)
                                        {
                                            item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                            Session.Update(item_estoque);
                                        }
                                        else
                                        {
                                            if (movimentacao.TipoOperacaoMovimento.Codigo == TipoOperacaoMovimento.ENTRADA) //Entrada
                                            {
                                                if (item_estoque != null)
                                                {
                                                    item_estoque.QuantidadeEstoque += itemMovimentacao.Quantidade;
                                                    Session.Update(item_estoque);
                                                }
                                                else
                                                {
                                                    item_estoque = new Estoque();
                                                    item_estoque.Farmacia = itemMovimentacao.Movimento.Farmacia;
                                                    item_estoque.LoteMedicamento = itemMovimentacao.LoteMedicamento;
                                                    item_estoque.QuantidadeEstoque = itemMovimentacao.Quantidade;
                                                    Session.Save(item_estoque);
                                                }
                                            }
                                            else //Saída
                                            {
                                                item_estoque.QuantidadeEstoque -= itemMovimentacao.Quantidade;
                                                Session.Update(item_estoque);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        //switch (mov.TipoMovimento.Codigo)
                        //{
                        //    case 1: //Devolução de Paciente
                        //        {
                        //            if (item_estoque != null)
                        //            {
                        //                item_estoque.QuantidadeEstoque += im.Quantidade;
                        //                Session.Update(item_estoque);
                        //            }
                        //            else
                        //            {
                        //                item_estoque = new Estoque();
                        //                item_estoque.Farmacia = im.Movimento.Farmacia;
                        //                item_estoque.LoteMedicamento = im.LoteMedicamento;
                        //                item_estoque.QuantidadeEstoque = im.Quantidade;
                        //                Session.Save(item_estoque);
                        //            }
                        //        }
                        //        break;
                        //    case 2: //Doação
                        //        {
                        //            if (mov.Farmacia.Codigo == Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado))
                        //            {
                        //                if (mov.TipoOperacaoMovimento.Codigo == TipoOperacaoMovimento.ENTRADA) //Entrada
                        //                {
                        //                    if (item_estoque != null)
                        //                    {
                        //                        item_estoque.QuantidadeEstoque += im.Quantidade;
                        //                        Session.Update(item_estoque);
                        //                    }
                        //                    else
                        //                    {
                        //                        item_estoque = new Estoque();
                        //                        item_estoque.Farmacia = im.Movimento.Farmacia;
                        //                        item_estoque.LoteMedicamento = im.LoteMedicamento;
                        //                        item_estoque.QuantidadeEstoque = im.Quantidade;
                        //                        Session.Save(item_estoque);
                        //                    }
                        //                }
                        //                else //Saída
                        //                {
                        //                    item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //                    Session.Update(item_estoque);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //                Session.Update(item_estoque);
                        //            }
                        //        }
                        //        break;

                        //    case 5: //5->Remanejamento
                        //        item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //        Session.Update(item_estoque);

                        //        ItemRemanejamento ir = new ItemRemanejamento();
                        //        ir.Remanejamento = rem;
                        //        ir.LoteMedicamento = im.LoteMedicamento;
                        //        ir.QuantidadeRegistrada = im.Quantidade;

                        //        Session.Save(ir);
                        //        break;

                        //    case 4: //Perda
                        //        item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //        Session.Update(item_estoque);
                        //        break;

                        //    case 6: //Transferência Interna
                        //        item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //        Session.Update(item_estoque);
                        //        break;
                        //    default: //3->Empréstimo 7->Acerto de Balanço
                        //        {
                        //            if (mov.TipoOperacaoMovimento.Codigo == TipoOperacaoMovimento.ENTRADA) //Entrada
                        //            {
                        //                if (item_estoque != null)
                        //                {
                        //                    item_estoque.QuantidadeEstoque += im.Quantidade;
                        //                    Session.Update(item_estoque);
                        //                }
                        //                else
                        //                {
                        //                    item_estoque = new Estoque();
                        //                    item_estoque.Farmacia = im.Movimento.Farmacia;
                        //                    item_estoque.LoteMedicamento = im.LoteMedicamento;
                        //                    item_estoque.QuantidadeEstoque = im.Quantidade;
                        //                    Session.Save(item_estoque);
                        //                }
                        //            }
                        //            else //Saída
                        //            {
                        //                item_estoque.QuantidadeEstoque -= im.Quantidade;
                        //                Session.Update(item_estoque);
                        //            }
                        //        }
                        //        break;
                        //}
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

        void IEstoque.AtualizarEstoque(object estoque, object item, string acao)
        {
            ISession session = NHibernateHttpHelper.GetCurrentSession("farmacia");
            using (session.BeginTransaction())
            {
                try
                {
                    if (acao == "inserir")
                        session.SaveOrUpdate(item);
                    else
                        if (acao == "remover")
                            session.Delete(item);
                    session.SaveOrUpdate(estoque);
                    session.Transaction.Commit();
                    session.Flush();
                }
                catch (Exception e)
                {
                    session.Transaction.Rollback();
                    throw e;
                }
            }
        }

        IList<T> IEstoque.BuscarPorNomeMedicamentoQuantidadeSuperior<T>(string nomemedicamento, int co_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque AS e WHERE e.QuantidadeEstoque > 0 AND e.Farmacia.Codigo = " + co_farmacia;
            hql += " AND e.LoteMedicamento.Medicamento.Nome LIKE '" + nomemedicamento + "%'";
            hql += " AND e.LoteMedicamento.Medicamento.PertenceARede = 'Y' ORDER BY e.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        T IEstoque.BuscaQtdEstoque<T>(int co_lotemedicamento, int co_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque AS E WHERE E.LoteMedicamento.Codigo = " + co_lotemedicamento;
            hql += " AND E.Farmacia.Codigo = " + co_farmacia;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IEstoque.BuscarPorFarmaciaReceita<T>(int co_farmacia, int[] codigosMedicamentosReceita)
        {

            List<string> codigosString = new List<string>();
            foreach (int codigo in codigosMedicamentosReceita)
            {
                codigosString.Add(codigo.ToString());
            }
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Estoque as e WHERE e.Farmacia.Codigo = " + co_farmacia;
            hql += " and e.QuantidadeEstoque > 0";
            hql += " and e.LoteMedicamento.Medicamento.Codigo in (" + string.Join(",", codigosString.ToArray()) + ") ";
            hql += " AND e.LoteMedicamento.Medicamento.PertenceARede = 'Y' ORDER BY e.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        int IEstoque.BuscarQuantidadeEstoqueMedicamento(int co_farmacia, int co_medicamento)
        {
            string hql = string.Empty;
            hql = @"SELECT SUM(Estoque.QuantidadeEstoque) FROM ViverMais.Model.Estoque AS Estoque
                    WHERE Estoque.Farmacia.Codigo = " + co_farmacia + " AND Estoque.LoteMedicamento.Medicamento = " + co_medicamento;

            object quantidade = Session.CreateQuery(hql).UniqueResult();

            if (quantidade != null)
                return int.Parse(quantidade.ToString());

            return 0;
        }
    }
}
