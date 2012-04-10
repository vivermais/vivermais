using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;

namespace ViverMais.DAO.Vacina.Misc
{
    public class DispensacaoDAO : VacinaServiceFacadeDAO, IDispensacao
    {
        #region IDispensacao Members

        IList<T> IDispensacao.BuscarItensDispensacao<T>(object codigo)
        {
            string hql = "FROM ViverMais.Model.ItemDispensacaoVacina as item "
            + "WHERE item.Dispensacao.Codigo = " + codigo;

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IDispensacao.BuscarItensDispensacao<T>(DateTime data, string co_paciente)
        {
            string hql = string.Empty;
            hql = @"SELECT i FROM ViverMais.Model.ItemDispensacaoVacina AS i, ViverMais.Model.DispensacaoVacina AS d
                    WHERE TO_CHAR(d.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "' AND " +
                    "d.Codigo = i.Dispensacao.Codigo AND d.Paciente.Codigo='" + co_paciente + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IDispensacao.BuscarPorPaciente<T>(string co_paciente, string co_unidade)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.DispensacaoVacina AS d WHERE d.Paciente.Codigo='" + co_paciente
                + "' AND d.Sala.EstabelecimentoSaude.CNES='" + co_unidade + "' ORDER BY d.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IDispensacao.BuscarPorPaciente<T, S>(string co_paciente, IList<S> _salas)
        {
            IList<SalaVacina> salas = (IList<SalaVacina>)(object)_salas;

            if (salas.Count() == 0)
                return (IList<T>)(object)new List<T>();

            string hql = string.Empty;

            foreach (SalaVacina sala in salas)
                hql += sala.Codigo + ",";
            hql = hql.Remove(hql.Length - 1, 1);

            hql = @"FROM ViverMais.Model.DispensacaoVacina AS d WHERE d.Paciente.Codigo='" + co_paciente
                + "' AND d.Sala.Codigo IN (" + hql + ") ORDER BY d.Data";
            return Session.CreateQuery(hql).List<T>();
        }

        int IDispensacao.ExcluirItensDispensacao<T>(IList<T> _itensexclusao, int co_usuario)
        {
            IList<ItemDispensacaoVacina> itensexclusao = (IList<ItemDispensacaoVacina>)(object)_itensexclusao;
            ICartaoVacina icartao = Factory.GetInstance<ICartaoVacina>();
            IEstoque iestoque = Factory.GetInstance<IEstoque>();
            int co_dispensacao = -1;
            IList<ItemDispensacaoVacina> itensatuais = Factory.GetInstance<IDispensacao>().BuscarItensDispensacao<ItemDispensacaoVacina>(itensexclusao[0].Dispensacao.Codigo);

            using (Session.BeginTransaction())
            {
                try
                {
                    foreach (ItemDispensacaoVacina item in itensexclusao)
                    {
                        if (item.AberturaAmpola == ItemDispensacaoVacina.AMPOLA_ABERTA)
                        {
                            EstoqueVacina estoque = iestoque.BuscarItemEstoque<EstoqueVacina>(item.Lote.Codigo, item.Dispensacao.Sala.Codigo);

                            if (estoque != null)
                            {
                                estoque.QuantidadeEstoque = estoque.QuantidadeEstoque + 1;
                                Session.Update(estoque);
                            }
                        }

                        CartaoVacina cartao = icartao.BuscarPorItemDispensacao<CartaoVacina>(item.Codigo);

                        if (cartao != null)
                        {
                            Session.Save(new LogVacina(DateTime.Now, co_usuario, 23, "co_paciente: " + cartao.Paciente.Codigo + ", vacina: " + cartao.Vacina.Nome + ", dose: " + cartao.DoseVacina.Descricao + ", data aplicacao: " + (cartao.DataAplicacao.HasValue ? cartao.DataAplicacao.Value.ToString("dd/MM/yyyy") : "Data de Aplicação não informada.")));
                            Session.Delete(cartao);
                        }

                        ItemDispensacaoVacina _item = itensatuais.Where(p => p.Codigo == item.Codigo).First();
                        Session.Save(new LogVacina(DateTime.Now, co_usuario, 25, "co_paciente: " + _item.Dispensacao.Paciente.Codigo + ", vacina: " + _item.Lote.ItemVacina.Vacina.Nome + ", dose: " + _item.Dose.Descricao + ", data aplicacao: " + _item.Dispensacao.Data.ToString("dd/MM/yyyy")));
                        Session.Delete(_item);
                    }

                    if (itensatuais.Count() == itensexclusao.Count())
                    {
                        DispensacaoVacina dispensacao = (DispensacaoVacina)Session.Merge(itensatuais[0].Dispensacao);
                        Session.Delete(dispensacao);

                        co_dispensacao = dispensacao.Codigo;
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }

            return co_dispensacao;
        }

        void IDispensacao.SalvarDispensacao<T, I>(T dispensacao, I itens, int co_usuario)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    DispensacaoVacina disp = (DispensacaoVacina)(object)dispensacao;
                    IList<ItemDispensacaoVacina> itensdispensacao = (IList<ItemDispensacaoVacina>)(object)itens;

                    Session.Save(disp);
                    Session.Save(new LogVacina(DateTime.Now, co_usuario, 22, "id dispensação: " + disp.Codigo));

                    foreach (ItemDispensacaoVacina item in itensdispensacao)
                    {
                        ItemDispensacaoVacina itemdispensacao = item;
                        itemdispensacao.Dispensacao = disp;
                        itemdispensacao = (ItemDispensacaoVacina)Session.SaveOrUpdateCopy(itemdispensacao);

                        if (itemdispensacao.AberturaAmpola.Equals(ItemDispensacaoVacina.AMPOLA_ABERTA))
                        {
                            EstoqueVacina estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoque<EstoqueVacina>(itemdispensacao.Lote.Codigo, disp.Sala.Codigo);
                            estoque.QuantidadeEstoque--;
                            Session.Update(estoque);
                        }

                        CartaoVacina cartao = new CartaoVacina();
                        cartao.DataAplicacao = disp.Data;
                        cartao.Paciente = disp.Paciente;
                        cartao.Vacina = itemdispensacao.Lote.ItemVacina.Vacina;
                        cartao.DoseVacina = itemdispensacao.Dose;
                        cartao.ItemDispensacao = itemdispensacao;
                        cartao.Lote = itemdispensacao.Lote.Identificacao;
                        cartao.ValidadeLote = itemdispensacao.Lote.DataValidade;
                        cartao.Local = disp.Sala.Nome;
                        cartao.Motivo = itemdispensacao.Estrategia.Descricao;

                        Session.Save(cartao);
                    }

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        string IDispensacao.LiberarVacinasDispensacao<I, P, S>(I itensdispensacao, P pacientedispensacao, S salavacina, DateTime data, bool dispensacaofinalizada)
        {
            string msginvalido = string.Empty;
            string hql = string.Empty;

            IList<ItemDispensacaoVacina> itenssolicitados = (IList<ItemDispensacaoVacina>)(object)itensdispensacao;
            ViverMais.Model.Paciente paciente = (ViverMais.Model.Paciente)(object)pacientedispensacao;
            SalaVacina sala = (SalaVacina)(object)salavacina;

            if (Factory.GetInstance<IInventarioVacina>().BuscarPorSituacao<InventarioVacina>(Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto), sala.Codigo).Count >= 1)
                return "Não é possível registrar esta dispensação, pois existe um inventário aberto para esta sala!";

            if (dispensacaofinalizada)
            {
                if (itenssolicitados.Count() <= 0)
                    return "Por favor, inclua pelo menos um item nesta dispensação!";
            }

            foreach (ItemDispensacaoVacina itemsolicitado in itenssolicitados)
            {
                hql = "FROM ViverMais.Model.ItemDispensacaoVacina AS iv WHERE iv.Dispensacao.Paciente.Codigo='" + paciente.Codigo + "'";
                hql += " AND TO_CHAR(iv.Dispensacao.Data,'DD/MM/YYYY') = '" + data.ToString("dd/MM/yyyy") + "' AND iv.Dose.Codigo = " + itemsolicitado.Dose.Codigo;
                hql += " AND iv.Lote.ItemVacina.Vacina.Codigo = " + itemsolicitado.Lote.ItemVacina.Vacina.Codigo;

                ItemDispensacaoVacina vacinadispensada = Session.CreateQuery(hql).UniqueResult<ItemDispensacaoVacina>();

                if (vacinadispensada != null)
                    msginvalido += "A dose " + itemsolicitado.Dose.Abreviacao + " da vacina " + itemsolicitado.Lote.ItemVacina.Vacina.Nome + " não pode ser dispensada para este paciente, pois hoje já foi registrada uma dispensação desta dose para este paciente na sala " + vacinadispensada.Dispensacao.Sala.Nome + ".\\n";
            }

            if (!string.IsNullOrEmpty(msginvalido))
                return msginvalido;

            msginvalido = string.Empty;
            var lotes = itenssolicitados.GroupBy(p => new { p.AberturaAmpola, p.Lote.Codigo }).Where(p => p.Key.AberturaAmpola == ItemDispensacaoVacina.AMPOLA_ABERTA).ToList();

            foreach (var lote in lotes)
            {
                int quantidadeestoque = Factory.GetInstance<IEstoqueVacina>().QuantidadeDisponivelEstoque(lote.First().Lote.Codigo, sala.Codigo);
                int quantidadesolicitada = lote.Count();

                if (quantidadeestoque < quantidadesolicitada)
                    msginvalido += "Não há disponibilidade no estoque da vacina " + lote.First().Lote.ItemVacina.Vacina.Nome + " no lote " + lote.First().Lote.Identificacao + ". Quantidade solicitada: " + quantidadesolicitada + " e Quantidade disponível: " + quantidadeestoque + ".\\n";
            }

            return !string.IsNullOrEmpty(msginvalido) ? msginvalido : "ok";
        }

        #endregion
    }
}
