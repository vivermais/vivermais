﻿using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.DAO.Farmacia.Movimentacao
{
    public class RequisicaoMedicamentoDAO : FarmaciaServiceFacadeDAO, IRequisicaoMedicamento
    {
        IList<T> IRequisicaoMedicamento.BuscarPorFarmacia<T>(int co_farmacia, int codstatus)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RequisicaoMedicamento AS rm WHERE rm.Farmacia.Codigo = " + co_farmacia;
            if (codstatus != -1)
                hql += " AND rm.Cod_Status = " + codstatus;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IRequisicaoMedicamento.BuscarPorDistrito<T>(int co_distrito)
        {
            //IList<ViverMais.Model.EstabelecimentoSaude> estabelecimentos = Factory.GetInstance<IEstabelecimentoSaude>().BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(co_distrito);
            string hql = string.Empty;
            hql = "SELECT rm FROM ViverMais.Model.RequisicaoMedicamento AS rm";
            hql += " WHERE rm.Cod_Status <> " + (int)RequisicaoMedicamento.StatusRequisicao.ABERTA;
            return Session.CreateQuery(hql).List<T>();
            //var result = from l in rms where rms.Contains(estabelecimentos) select l;
        }

        IList<T> IRequisicaoMedicamento.BuscarItensRequisicao<T>(int co_requisicao)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemRequisicao AS i WHERE i.Requisicao.Codigo = " + co_requisicao;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IRequisicaoMedicamento.ListarAutorizadas<T>()
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RequisicaoMedicamento AS rm WHERE rm.Cod_Status = " + (int)RequisicaoMedicamento.StatusRequisicao.AUTORIZADA;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IRequisicaoMedicamento.ListarAutorizadas<T>(int co_farmacia)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.RequisicaoMedicamento AS rm WHERE rm.Cod_Status = " + (int)RequisicaoMedicamento.StatusRequisicao.AUTORIZADA;
            hql += " and rm.Farmacia.Codigo = " + co_farmacia;
            return Session.CreateQuery(hql).List<T>();
        }

        int IRequisicaoMedicamento.CalculaConsumoDispensacao(DateTime dataInicial, DateTime dataFinal, int co_medicamento, int co_farmacia)
        {
            string hql = string.Empty;
            hql = "SELECT SUM(i.QtdDispensada) FROM ViverMais.Model.ItemDispensacao i";
            hql += " WHERE to_char(i.DataAtendimento,'dd/MM/yyyy') BETWEEN '" + dataInicial.ToString("dd/MM/yyyy") + "' AND '" + dataFinal.ToString("dd/MM/yyyy") + "'";
            hql += " AND i.LoteMedicamento.Medicamento.Codigo = " + co_medicamento.ToString();
            hql += " AND i.Farmacia.Codigo = " + co_farmacia.ToString();
            object obj = Session.CreateQuery(hql).UniqueResult<object>();
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        int IRequisicaoMedicamento.CalculaConsumoMovimentacao(DateTime dataInicial, DateTime dataFinal, int co_medicamento, int co_farmacia)
        {
            //Implementar
            return 0;
        }

        int IRequisicaoMedicamento.BuscarSaldoAtual(int co_medicamento, int co_farmacia)
        {
            string hql = string.Empty;
            hql = "SELECT SUM(e.QuantidadeEstoque) FROM ViverMais.Model.Estoque e";
            hql += " WHERE e.LoteMedicamento.Medicamento.Codigo = " + co_medicamento.ToString();
            hql += " AND e.Farmacia.Codigo = " + co_farmacia.ToString();
            object obj = Session.CreateQuery(hql).UniqueResult<object>();
            if (obj == null)
                return 0;
            else
                return int.Parse(obj.ToString());
        }

        DateTime IRequisicaoMedicamento.BuscarDataUltimaRequisicao()
        {
            string hql = string.Empty;
            hql = "SELECT MAX(requisicao.DataCriacao) FROM ViverMais.Model.RequisicaoMedicamento requisicao";
            return Session.CreateQuery(hql).UniqueResult<DateTime>();
        }

        void IRequisicaoMedicamento.AlterarItemRequisicao<T>(T _itemRequisicao, int qtdFornecidaAnterior, int co_loteMedicamento)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    IEstoque iEstoque = Factory.GetInstance<IEstoque>();

                    ItemRequisicao itemRequisicao = (ItemRequisicao)(object)_itemRequisicao;
                    if (co_loteMedicamento != 0)
                    {
                        if (itemRequisicao.LoteMedicamento.Codigo != co_loteMedicamento)
                        {
                            //Farmacia 22 é a farmácia estoque no isis
                            Estoque estoqueAnterior = iEstoque.BuscarItemEstoquePorFarmacia<Estoque>(ViverMais.Model.Farmacia.ALMOXARIFADO, co_loteMedicamento);
                            estoqueAnterior.QuantidadeEstoque += qtdFornecidaAnterior;
                            Session.Save(estoqueAnterior);
                        }
                    }

                    Estoque estoque = iEstoque.BuscarItemEstoquePorFarmacia<Estoque>(ViverMais.Model.Farmacia.ALMOXARIFADO, itemRequisicao.LoteMedicamento.Codigo);

                    if (itemRequisicao.LoteMedicamento.Codigo != co_loteMedicamento)
                        estoque.QuantidadeEstoque = estoque.QuantidadeEstoque - itemRequisicao.QtdFornecida;
                    else
                        estoque.QuantidadeEstoque = estoque.QuantidadeEstoque + (qtdFornecidaAnterior - itemRequisicao.QtdFornecida);
                    Session.Update(itemRequisicao);
                    Session.Update(estoque);
                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        IList<T> IRequisicaoMedicamento.PesquisarRequisicao<T>(int co_farmacia, DateTime dataabertura, int numerorequisicao, int status)
        {
            string hql = string.Empty;
            hql += "FROM RequisicaoMedicamento AS rm WHERE rm.Farmacia.Codigo = " + co_farmacia;

            if (dataabertura != DateTime.MinValue)
                hql += " AND TO_CHAR(rm.DataCriacao,'DD/MM/YYYY') = '" + dataabertura.ToString("dd/MM/yyyy") + "'";

            if (numerorequisicao != -1)
                hql += " and rm.Codigo = " + numerorequisicao;

            if (status != -1)
                hql += " AND rm.Cod_Status = " + status;

            return Session.CreateQuery(hql).List<T>();
        }

        //#region IServiceFacade Members

        //T ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.BuscarPorCodigo<T>(object codigo)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Atualizar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Inserir(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Salvar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //void ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.Deletar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>()
        //{
        //    throw new NotImplementedException();
        //}

        //IList<T> ViverMais.ServiceFacade.ServiceFacades.IServiceFacade.ListarTodos<T>(string orderField, bool asc)
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion
    }
}
