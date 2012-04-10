﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;

namespace ViverMais.DAO.Vacina.Movimentacao
{
    public class MovimentoVacinaDAO : VacinaServiceFacadeDAO, IMovimentoVacina
    {
        IList<T> IMovimentoVacina.BuscarMovimentacao<T>(int co_sala, int co_tipo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MovimentoVacina m WHERE m.Sala.Codigo =" + co_sala + " AND m.TipoMovimento.Codigo = " + co_tipo
                + " ORDER BY m.Data DESC";


            if (co_tipo == TipoMovimento.REMANEJAMENTO)
            {
                IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                IList<MovimentoVacina> movimentos = Session.CreateQuery(hql).List<MovimentoVacina>();
                foreach (MovimentoVacina movimento in movimentos)
                {
                    RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(movimento.Codigo);

                    if (remanejamento.Status != RemanejamentoVacina.FECHADO
                            && movimento.Status != MovimentoVacina.FECHADO)
                            //!iVacina.VerificarConfirmacaoAlteracaoItensMovimento(movimento.Codigo))
                        movimento.Editar = true;
                }

                return (IList<T>)(object)movimentos;
            }

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarMovimentacao<T>(int co_sala, int co_tipo, DateTime datainicio, DateTime datafim)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MovimentoVacina m WHERE m.Sala.Codigo =" + co_sala + " AND m.TipoMovimento.Codigo = " + co_tipo
                + " AND m.Data BETWEEN TO_DATE('" + datainicio.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
                + " AND TO_DATE('" + datafim.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')"
                + " ORDER BY m.Data DESC";

            if (co_tipo == TipoMovimento.REMANEJAMENTO)
            {
                IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                IList<MovimentoVacina> movimentos = Session.CreateQuery(hql).List<MovimentoVacina>();
                foreach (MovimentoVacina movimento in movimentos)
                {
                    RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(movimento.Codigo);
                    
                    if (remanejamento.Status != RemanejamentoVacina.FECHADO
                            && movimento.Status != MovimentoVacina.FECHADO)
                        movimento.Editar = true;
                }

                return (IList<T>)(object)movimentos;
            }

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarMovimentacao<T>(int co_sala, int co_tipo, int co_saladestino)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MovimentoVacina m WHERE m.Sala.Codigo =" + co_sala + " AND m.TipoMovimento.Codigo = " + co_tipo
                + " AND m.SalaDestino.Codigo=" + co_saladestino + " ORDER BY m.Data DESC";

            if (co_tipo == TipoMovimento.REMANEJAMENTO)
            {
                IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                IList<MovimentoVacina> movimentos = Session.CreateQuery(hql).List<MovimentoVacina>();
                foreach (MovimentoVacina movimento in movimentos)
                {
                    RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(movimento.Codigo);

                    if (remanejamento.Status != RemanejamentoVacina.FECHADO
                            && movimento.Status != MovimentoVacina.FECHADO)
                        movimento.Editar = true;
                }

                return (IList<T>)(object)movimentos;
            }

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarMovimentacao<T>(int co_sala, int co_tipo, int co_saladestino, DateTime datainicio, DateTime datafim)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.MovimentoVacina m WHERE m.Sala.Codigo =" + co_sala + " AND m.TipoMovimento.Codigo = " + co_tipo
                + " AND m.SalaDestino.Codigo=" + co_saladestino
                + " AND m.Data BETWEEN TO_DATE('" + datainicio.ToString("dd/MM/yyyy") + " 00:00','DD/MM/YYYY HH24:MI')"
                + " AND TO_DATE('" + datafim.ToString("dd/MM/yyyy") + " 23:59','DD/MM/YYYY HH24:MI')"
                + " ORDER BY m.Data DESC";

            if (co_tipo == TipoMovimento.REMANEJAMENTO)
            {
                IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                IList<MovimentoVacina> movimentos = Session.CreateQuery(hql).List<MovimentoVacina>();
                foreach (MovimentoVacina movimento in movimentos)
                {
                    RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(movimento.Codigo);

                    if (remanejamento.Status != RemanejamentoVacina.FECHADO
                            && movimento.Status != MovimentoVacina.FECHADO)
                        movimento.Editar = true;
                }

                return (IList<T>)(object)movimentos;
            }

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarItensMovimentacao<T>(long co_movimentacao)
        {
            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            RemanejamentoVacina remanejamento = iMovimento.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(co_movimentacao);

            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemMovimentoVacina i WHERE i.Movimento.Codigo=" + co_movimentacao;
            IList<ItemMovimentoVacina> itens = Session.CreateQuery(hql).List<ItemMovimentoVacina>();

            if (remanejamento != null)
            {
                foreach (ItemMovimentoVacina item in itens)
                {
                    ItemRemanejamentoVacina itemremanejamento = iMovimento.BuscarItemRemanejamentoPorMovimentoLote<ItemRemanejamentoVacina>(co_movimentacao, item.Lote.Codigo);
                    if (!itemremanejamento.RecebimentoConfirmado && iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(item.Codigo).Count()
                    < HistoricoItemMovimentoVacina.LIMITE_ALTERACAO)
                        item.Editar = true;
                }
            }

            return (IList<T>)(object)itens;
        }

        IList<T> IMovimentoVacina.BuscarRemanejamentosSalaDestino<T>(int co_sala, char status)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoVacina r WHERE r.Movimento.SalaDestino.Codigo = " + co_sala +
                " AND r.Status = '" + status + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarRemanejamentosSalaDestino<T>(int co_sala)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoVacina r WHERE r.Movimento.SalaDestino.Codigo =" + co_sala;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarItensRemanejamento<T>(long co_remanejamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemRemanejamentoVacina i WHERE i.Remanejamento.Codigo = " + co_remanejamento;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarRemanejamentoPorSala<T>(int co_sala)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoVacina r WHERE r.Movimento.Sala.Codigo = " + co_sala;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarRemanejamentoPorSala<T>(int co_sala, char status)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoVacina r WHERE r.Movimento.Sala.Codigo = " + co_sala +
                " AND r.Status = '" + status + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IMovimentoVacina.BuscarHistoricoAlteracaoItemMovimento<T>(long co_itemmovimento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.HistoricoItemMovimentoVacina AS i WHERE i.Item.Codigo=" + co_itemmovimento;
            return Session.CreateQuery(hql).List<T>();
        }

        T IMovimentoVacina.BuscarRemanejamentoPorMovimentacao<T>(long co_movimentacao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RemanejamentoVacina AS r WHERE r.Movimento.Codigo=" + co_movimentacao;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IMovimentoVacina.BuscarItemRemanejamentoPorMovimentoLote<T>(long co_movimento, long co_lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemRemanejamentoVacina AS i WHERE i.Lote.Codigo=" + co_lote;
            hql += " AND i.Remanejamento.Movimento.Codigo=" + co_movimento;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IMovimentoVacina.BuscarItemMovimento<T>(long co_movimento, long co_lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemMovimentoVacina AS i WHERE i.Lote.Codigo=" + co_lote;
            hql += " AND i.Movimento.Codigo=" + co_movimento;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IMovimentoVacina.BuscarPorNumero<T, U>(long numero, U _usuario)
        {
            Usuario usuario = (Usuario)(object)_usuario;
            ISalaVacina iSala = Factory.GetInstance<ISalaVacina>();
            IList<SalaVacina> salas = iSala.BuscarPorUsuario<SalaVacina, Usuario>(usuario, true, false);
            MovimentoVacina movimento = (MovimentoVacina)(object)Session.CreateQuery("FROM ViverMais.Model.MovimentoVacina m WHERE m.Numero=" + numero).UniqueResult<T>();

            if (movimento != null)
            {
                if (movimento.TipoMovimento.Codigo == TipoMovimento.REMANEJAMENTO)
                {
                    IMovimentoVacina iVacina = Factory.GetInstance<IMovimentoVacina>();
                    RemanejamentoVacina remanejamento = iVacina.BuscarRemanejamentoPorMovimentacao<RemanejamentoVacina>(movimento.Codigo);

                    if (remanejamento.Status != RemanejamentoVacina.FECHADO
                            && movimento.Status != MovimentoVacina.FECHADO)
                        //!iVacina.VerificarConfirmacaoAlteracaoItensMovimento(movimento.Codigo))
                        movimento.Editar = true;
                }

                if (salas.Where(p => p.Codigo == movimento.Sala.Codigo).FirstOrDefault() != null)
                    return (T)(object)movimento;
            }

            return (T)(object)null;
        }

        bool IMovimentoVacina.AlterarQuantidadeItemMovimento<T, A, U>(ref T _itemmovimento, A _itemremanejamento, int novaquantidade, string motivo, U _usuario)
        {
            ItemMovimentoVacina itemmovimento = (ItemMovimentoVacina)(object)_itemmovimento;
            ItemRemanejamentoVacina itemremanejamento = (ItemRemanejamentoVacina)(object)_itemremanejamento;
            Usuario usuario = (Usuario)(object)_usuario;
            IMovimentoVacina iMovimento = Factory.GetInstance<IMovimentoVacina>();
            int quantidadealteracoes = -1;

            using (Session.BeginTransaction())
            {
                try
                {
                    int diferenca = novaquantidade - itemmovimento.Quantidade;

                    HistoricoItemMovimentoVacina historico = new HistoricoItemMovimentoVacina();
                    historico.Data = DateTime.Now;
                    historico.Item = itemmovimento;
                    historico.Motivo = motivo;
                    historico.Usuario = usuario;
                    historico.QuantidadeAlterada = novaquantidade;
                    historico.QuantidadeAnterior = itemmovimento.Quantidade;

                    Session.SaveOrUpdateCopy(historico);

                    itemmovimento.Quantidade = novaquantidade;
                    itemmovimento = (ItemMovimentoVacina)Session.SaveOrUpdateCopy(itemmovimento);

                    itemremanejamento.QuantidadeRegistrada = novaquantidade;
                    Session.SaveOrUpdateCopy(itemremanejamento);

                    quantidadealteracoes = iMovimento.BuscarHistoricoAlteracaoItemMovimento<HistoricoItemMovimentoVacina>(itemmovimento.Codigo).Count();

                    //Atualizar estoque da sala de vacina
                    IEstoque iEstoque = Factory.GetInstance<IEstoque>();
                    EstoqueVacina estoque = iEstoque.BuscarItemEstoque<EstoqueVacina>(itemmovimento.Lote.Codigo, itemmovimento.Movimento.Sala.Codigo);

                    estoque.QuantidadeEstoque -= diferenca;
                    Session.Update(estoque);

                    #region VERIFICANDO A NECESSIDADE DE FECHAR O MOVIMENTO PARA ALTERAÇÃO DE ITEM
                    int totalitensmovimento = 0;

                    object obj1 = Session.CreateQuery("SELECT COUNT(i.Codigo) FROM ViverMais.Model.ItemMovimentoVacina i WHERE i.Movimento.Codigo=" + itemmovimento.Movimento.Codigo).UniqueResult<object>();
                    if (obj1 != null)
                        totalitensmovimento = Int32.Parse(obj1.ToString());

                    IList<object> itensmovimentosagrupados = Session.CreateQuery(@"SELECT COUNT(i.Item.Lote.Codigo) FROM ViverMais.Model.HistoricoItemMovimentoVacina i 
                    WHERE i.Item.Movimento.Codigo=" + itemmovimento.Movimento.Codigo + " AND i.Item.Lote.Codigo <> " + itemmovimento.Lote.Codigo + " GROUP BY i.Item.Lote.Codigo").List<object>();

                    if((itensmovimentosagrupados.Sum(p => Convert.ToInt32(p.ToString())) + quantidadealteracoes + 1) == (totalitensmovimento * HistoricoItemMovimentoVacina.LIMITE_ALTERACAO))
                    {
                        MovimentoVacina movimento = itemmovimento.Movimento;
                        movimento.Status = MovimentoVacina.FECHADO;
                        Session.SaveOrUpdateCopy(movimento);
                    }
                    #endregion

                    //Log
                    Session.Save(new LogVacina(DateTime.Now, usuario.Codigo, 30, "item movimento: " + itemmovimento.Codigo + ", qtd anterior: " + (novaquantidade - diferenca).ToString() + ", nova qtd: " + novaquantidade));
                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }

            if (!itemremanejamento.RecebimentoConfirmado && (quantidadealteracoes + 1) < HistoricoItemMovimentoVacina.LIMITE_ALTERACAO)
                return true;

            return false;
        }

        void IMovimentoVacina.ConfirmarRemanejamento<T>(T itemremanejamento, bool fecharremanejamento)
        {
            using (Session.BeginTransaction())
            {
                ItemRemanejamentoVacina item = (ItemRemanejamentoVacina)(object)itemremanejamento;

                try
                {
                    Session.Update(item);
                    Session.Save(new LogVacina(DateTime.Now, item.UsuarioConfirmacao.Codigo, 29, "id item remanejmento: " + item.Codigo));

                    if (fecharremanejamento)
                    {
                        RemanejamentoVacina remanejamento = item.Remanejamento;
                        remanejamento.Status = RemanejamentoVacina.FECHADO;
                        Session.Update(remanejamento);
                    }

                    SalaVacina sala = item.Remanejamento.Movimento.SalaDestino;
                    LoteVacina lote = item.Lote;
                    IEstoque iEstoque = Factory.GetInstance<IEstoque>();
                    EstoqueVacina estoque = iEstoque.BuscarItemEstoque<EstoqueVacina>(lote.Codigo, sala.Codigo);

                    if (estoque == null)
                    {
                        estoque = new EstoqueVacina();
                        estoque.Sala = sala;
                        estoque.Lote = lote;
                        estoque.QuantidadeEstoque = 0;
                    }

                    estoque.QuantidadeEstoque += item.QuantidadeConfirmada;

                    Session.SaveOrUpdate(estoque);
                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}