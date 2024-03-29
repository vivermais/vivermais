﻿using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.NotaFiscal;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace ViverMais.DAO.Farmacia.NotaFiscal
{
    public class NotaFiscalDAO : FarmaciaServiceFacadeDAO, INotaFiscal
    {
        bool INotaFiscal.ValidarCadastroNotaFiscal<T>(int co_fornecedor, string numeronota, int co_notafiscal)
        {
            string hql = string.Empty;
            hql =  "FROM ViverMais.Model.NotaFiscal AS nf WHERE nf.Codigo <> " + co_notafiscal;
            hql += " AND nf.Fornecedor.Codigo = " + co_fornecedor + " AND nf.NumeroNota = '" + numeronota + "'";
            return Session.CreateQuery(hql).List<T>().Count <= 0;
        }

        IList<T> INotaFiscal.ListarLotesMedicamento<T>(int co_nota)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemNotaFiscal AS inf WHERE inf.NotaFiscal.Codigo = " + co_nota;
            hql += " ORDER BY inf.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        T INotaFiscal.BuscarItemNotaFiscal<T>(int co_itemnota)
        {
            string hql = string.Empty;
            hql =  "FROM ViverMais.Model.ItemNotaFiscal AS inf WHERE inf.Codigo = " + co_itemnota;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        int INotaFiscal.SalvarItemNotaFiscal(string lote, DateTime data_validade, int co_medicamento, int co_fabricante, int qtd_item, decimal valor_unitario, int co_nota) 
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    string hql = string.Empty;
                    hql = "FROM ViverMais.Model.LoteMedicamento AS lm WHERE lm.Lote = '" + lote + "'";
                    hql += " AND to_char(lm.Validade,'dd/MM/yyyy') = '" + data_validade.ToString("dd/MM/yyyy") + "'";
                    hql += " AND lm.Medicamento.Codigo = " + co_medicamento;
                    hql += " AND lm.Fabricante.Codigo = " + co_fabricante;
                    LoteMedicamento loteMecicamento = Session.CreateQuery(hql).UniqueResult<LoteMedicamento>();

                    if (loteMecicamento == null)
                    {
                        loteMecicamento = new LoteMedicamento(Factory.GetInstance<IMedicamento>().BuscarPorCodigo<ViverMais.Model.Medicamento>(co_medicamento), Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<FabricanteMedicamento>(co_fabricante), lote, data_validade);
                        Session.Save(loteMecicamento);
                    }

                    ItemNotaFiscal itemNotaFiscal = new ItemNotaFiscal();
                    itemNotaFiscal.NotaFiscal = Factory.GetInstance<INotaFiscal>().BuscarPorCodigo<ViverMais.Model.NotaFiscal>(co_nota);
                    itemNotaFiscal.LoteMedicamento = loteMecicamento;
                    itemNotaFiscal.Quantidade = qtd_item;
                    itemNotaFiscal.ValorUnitario = valor_unitario;

                    Session.Save(itemNotaFiscal);

                    //Atualiza o Estoque com os dados da Nota Fiscal
                    IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
                    IEstoque iestoque = Factory.GetInstance<IEstoque>();
                    //Farmácia Almoxarifado Central
                    //ViverMais.Model.Farmacia farmacia = ifarmacia.BuscarPorCodigo<ViverMais.Model.Farmacia>(Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado));
                    Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(itemNotaFiscal.NotaFiscal.Farmacia.Codigo, loteMecicamento.Codigo);
                    if (estoque != null)
                    {
                        estoque.QuantidadeEstoque += qtd_item;
                        Session.Update(estoque);
                        //iestoque.Atualizar(estoque);
                    }
                    else
                    {
                        estoque = new Estoque();
                        estoque.QuantidadeEstoque = qtd_item;
                        estoque.Farmacia = itemNotaFiscal.NotaFiscal.Farmacia;
                        estoque.LoteMedicamento = loteMecicamento;
                        Session.Save(estoque);
                        //iestoque.Salvar(estoque);
                    }

                    Session.Transaction.Commit();

                    return itemNotaFiscal.Codigo;
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        bool INotaFiscal.ValidaCadastroItemNotaFiscal(string lote, DateTime data_validade, int co_medicamento, int co_fabricante, int co_nota, int co_itemnota)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemNotaFiscal AS inf WHERE inf.LoteMedicamento.Lote = '" + lote + "'";
            hql += " AND to_char(inf.LoteMedicamento.Validade,'dd/MM/yyyy') = '" + data_validade.ToString("dd/MM/yyyy") + "'";
            hql += " AND inf.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
            hql += " AND inf.LoteMedicamento.Fabricante.Codigo = " + co_fabricante;
            hql += " AND inf.NotaFiscal.Codigo = " + co_nota + " AND inf.Codigo <> " + co_itemnota;

            return Session.CreateQuery(hql).List().Count <= 0;
        }

        IList<T> INotaFiscal.BuscarPorDescricao<T>(int co_fornecedor, string numero_nota) 
        {
            string hql = string.Empty;
            hql += "FROM ViverMais.Model.NotaFiscal AS nf WHERE";

            if (co_fornecedor != -1)
                hql += " nf.Fornecedor.Codigo = " + co_fornecedor;

            if (!string.IsNullOrEmpty(numero_nota)) 
            {
                if (co_fornecedor != -1)
                    hql += " AND nf.NumeroNota = '" + numero_nota + "'";
                else
                    hql += " nf.NumeroNota = '" + numero_nota + "'";
            }

            hql += " ORDER BY nf.Status, nf.NumeroNota";

            return Session.CreateQuery(hql).List<T>();
        }

        void INotaFiscal.AtualizarItemNotaFiscal<N>(N _itemNotaFiscal, int qtd_item, decimal valor_unitario)
        {
            ItemNotaFiscal itemNotaFiscal = (ItemNotaFiscal)(object)_itemNotaFiscal;

            using (Session.BeginTransaction())
            {
                try
                {
                    int diff = qtd_item - itemNotaFiscal.Quantidade;

                    itemNotaFiscal.Quantidade = qtd_item;
                    itemNotaFiscal.ValorUnitario = valor_unitario;
                    Session.Update(itemNotaFiscal);

                    //**********Atualiza Estoque**************
                    //Atualiza o Estoque com os dados da Nota Fiscal
                    IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
                    IEstoque iestoque = Factory.GetInstance<IEstoque>();
                    //Farmácia Almoxarifado Central
                    
                    //ViverMais.Model.Farmacia farmacia = ifarmacia.BuscarPorCodigo<ViverMais.Model.Farmacia>(Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado));
                    //Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(farmacia.Codigo, inf.LoteMedicamento.Codigo);
                    Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(itemNotaFiscal.NotaFiscal.Farmacia.Codigo, itemNotaFiscal.LoteMedicamento.Codigo);

                    estoque.QuantidadeEstoque += diff;
                    Session.Update(estoque);
                    //**********Atualiza Estoque**************

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        //void INotaFiscal.AtualizarItemNotaFiscal<N>(N item_nota, string nome_lote, int co_fabricante, DateTime data_validade, int qtd_item, float valor_unitario) 
        //{
        //    ItemNotaFiscal inf = (ItemNotaFiscal)(object)item_nota;

        //    using (Session.BeginTransaction())
        //    {
        //        try
        //        {
        //            string hql = string.Empty;
        //            hql = "FROM ViverMais.Model.LoteMedicamento AS l WHERE l.Medicamento.Codigo = " + inf.LoteMedicamento.Medicamento.Codigo;
        //            hql += " AND l.Lote = '" + nome_lote + "' AND l.Fabricante.Codigo = " + co_fabricante;
        //            hql += " AND CONVERT(VARCHAR(17),l.Validade ,103) = '" + data_validade.ToString("dd/MM/yyyy") + "'";
        //            LoteMedicamento lote = (LoteMedicamento)(object)Session.CreateQuery(hql).UniqueResult();

        //            bool alterar_mesmolote = lote == null || (lote != null && lote.Codigo == inf.LoteMedicamento.Codigo) ? true : false;
        //            ItemNotaFiscal inftemp = null;

        //            if (!alterar_mesmolote)
        //            {
        //                hql = string.Empty;
        //                hql += "FROM ViverMais.Model.ItemNotaFiscal AS inf WHERE inf.NotaFiscal.Codigo = " + inf.NotaFiscal.Codigo;
        //                hql += " AND inf.LoteMedicamento.Codigo = " + lote.Codigo;
        //                inftemp = (ItemNotaFiscal)(object)Session.CreateQuery(hql).UniqueResult();
        //            }

        //            if (alterar_mesmolote && inftemp == null)
        //            {
        //                inf.LoteMedicamento.Lote = nome_lote;
        //                inf.LoteMedicamento.Fabricante = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<FabricanteMedicamento>(co_fabricante);
        //                inf.LoteMedicamento.Validade = data_validade;
        //                inf.Quantidade = qtd_item;
        //                inf.ValorUnitario = valor_unitario;
        //                Session.Update(inf); //Atualizar Dados
        //            }
        //            else
        //            {
        //                if (inftemp == null)
        //                {
        //                    inf.LoteMedicamento = lote;
        //                    inf.Quantidade = qtd_item;
        //                    inf.ValorUnitario = valor_unitario;
        //                    Session.Update(inf); //Atualizar Dados
        //                }
        //            }

        //            //**********Atualiza Estoque**************
        //            //Atualiza o Estoque com os dados da Nota Fiscal
        //            IFarmacia ifarmacia = Factory.GetInstance<IFarmacia>();
        //            IEstoque iestoque = Factory.GetInstance<IEstoque>();
        //            //Farmácia Almoxarifado Central
        //            ViverMais.Model.Farmacia farmacia = ifarmacia.BuscarPorCodigo<ViverMais.Model.Farmacia>(118);
        //            Estoque estoque = iestoque.BuscarItemEstoquePorFarmacia<Estoque>(farmacia.Codigo, lote.Codigo);
        //            if (estoque != null)
        //            {
        //                estoque.QuantidadeEstoque += qtd_item;
        //                //iestoque.Atualizar(estoque);
        //                Session.Update(estoque);
        //            }
        //            else
        //            {
        //                estoque = new Estoque();
        //                estoque.QuantidadeEstoque = qtd_item;
        //                estoque.Farmacia = farmacia;
        //                estoque.LoteMedicamento = lote;
        //                //iestoque.Salvar(estoque);
        //                Session.SaveOrUpdate(estoque);
        //            }
        //            //**********Atualiza Estoque**************

        //            Session.Transaction.Commit();
        //        }
        //        catch (Exception f)
        //        {
        //            Session.Transaction.Rollback();
        //            throw f;
        //        }
        //    }
        //}

        void INotaFiscal.ExcluirItemNotaFiscal<N>(N _itemNotaFiscal)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    ItemNotaFiscal itemNotaFiscal = (ItemNotaFiscal)(object)_itemNotaFiscal;
                    //Farmácia Almoxarifado Central
                    //ViverMais.Model.Farmacia farmacia = Factory.GetInstance<IFarmacia>().BuscarPorCodigo<ViverMais.Model.Farmacia>(Convert.ToInt32(ViverMais.Model.Farmacia.QualFarmacia.Almoxarifado));
                    Estoque estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemNotaFiscal.NotaFiscal.Farmacia.Codigo, itemNotaFiscal.LoteMedicamento.Codigo);
                    estoque.QuantidadeEstoque -= itemNotaFiscal.Quantidade;
                    Session.Update(estoque);
                    //if (estoque != null)
                        //Session.Delete(estoque);
                    Session.Delete(itemNotaFiscal);
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
