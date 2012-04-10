﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.Model;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;

namespace ViverMais.DAO.Vacina.Movimentacao
{
    public class InventarioVacinaDAO : VacinaServiceFacadeDAO, IInventarioVacina
    {
        IList<T> IInventarioVacina.BuscarPorSalaVacina<T>(int co_salavacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.InventarioVacina AS iv WHERE iv.Sala.Codigo = " + co_salavacina;
            hql += " ORDER BY iv.DataInventario DESC";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IInventarioVacina.BuscarPorSituacao<T>(char situacao, int co_salavacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.InventarioVacina AS iv WHERE iv.Sala.Codigo = " + co_salavacina;
            hql += " AND iv.Situacao = '" + situacao + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IInventarioVacina.ListarItensInventario<T>(int co_inventario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemInventarioVacina AS i WHERE i.Inventario.Codigo = " + co_inventario;
            //hql += " ORDER BY i.LoteVacina.ItemVacina.Vacina.Nome, i.LoteVacina.Identificacao";

            var result = (from item in Session.CreateQuery(hql).List<ItemInventarioVacina>()
                          orderby item.LoteVacina.ItemVacina.Vacina.Nome,
                          item.LoteVacina.Identificacao
                          select item);

            return (IList<T>)(object)result.ToList();
            //return Session.CreateQuery(hql).List<T>();
        }

        void IInventarioVacina.AbrirInventario<T>(T inventario)
        {
            using (Session.BeginTransaction())
            {
                try
                {
                    InventarioVacina inventariovacina = (InventarioVacina)(object)inventario;
                    //IList<EstoqueVacina> estoques = Factory.GetInstance<IEstoqueVacina>().BuscarPorSalaVacina<EstoqueVacina>(inventariovacina.Sala.Codigo);
                    IList<EstoqueVacina> estoques = Factory.GetInstance<IEstoqueVacina>().BuscarPorSalaVacinaValidadeLoteSuperior<EstoqueVacina>(inventariovacina.Sala.Codigo, inventariovacina.DataInventario);
                    //object objetocodigoinventario = Session.CreateQuery("SELECT MAX(i.Codigo) FROM InventarioVacina AS i").List()[0];
                    //int co_inventario = int.Parse(objetocodigoinventario != null ? objetocodigoinventario.ToString() : "1");

                    Session.Save(inventariovacina);

                    foreach (ViverMais.Model.EstoqueVacina estoque in estoques)
                    {
                        ViverMais.Model.ItemInventarioVacina iteminventario = new ViverMais.Model.ItemInventarioVacina();
                        //object objetocodigoiteminventario = Session.CreateQuery("SELECT MAX(i.Codigo) FROM ItemInventarioVacina AS i").List()[0];
                        //iteminventario.Codigo = objetocodigoinventario != null ? long.Parse(objetocodigoiteminventario.ToString()) + 1 : 1;
                        iteminventario.Inventario = inventariovacina;
                        iteminventario.LoteVacina = estoque.Lote;
                        iteminventario.QtdEstoque = estoque.QuantidadeEstoque;
                        iteminventario.QtdContada = estoque.QuantidadeEstoque;
                        Session.Save(iteminventario);
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

        void IInventarioVacina.EncerrarInventario<T>(T inventario)
        {
            ViverMais.Model.InventarioVacina inventariovacina = (ViverMais.Model.InventarioVacina)(object)inventario;

            using (Session.BeginTransaction())
            {
                try
                {
                    inventariovacina = (InventarioVacina)Session.SaveOrUpdateCopy(inventariovacina);

                    IList<ItemInventarioVacina> itensinventario = Factory.GetInstance<IInventarioVacina>().ListarItensInventario<ItemInventarioVacina>(inventariovacina.Codigo);
                    IList<EstoqueVacina> estoquessala = Factory.GetInstance<IEstoqueVacina>().BuscarPorSalaVacina<EstoqueVacina>(inventariovacina.Sala.Codigo);

                    foreach (ItemInventarioVacina item in itensinventario)
                    {
                        EstoqueVacina estoque = estoquessala.Where(p => p.Lote.Codigo == item.LoteVacina.Codigo).FirstOrDefault();

                        if (estoque != null)
                        {
                            estoque.QuantidadeEstoque = item.QtdContada;
                            Session.Update(estoque);
                        }
                        else
                        {
                            estoque = new EstoqueVacina();
                            estoque.Lote = item.LoteVacina;
                            estoque.Sala = item.Inventario.Sala;
                            estoque.QuantidadeEstoque = item.QtdContada;
                            Session.Save(estoque);
                        }
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

        int IInventarioVacina.AbrirInventario(DateTime dataabertura, int co_sala)
        {
            SalaVacina sala = Factory.GetInstance<ISalaVacina>().BuscarPorCodigo<SalaVacina>(co_sala);

            using (Session.BeginTransaction())
            {
                try
                {
                    InventarioVacina inventario = new InventarioVacina();
                    inventario.DataInventario = dataabertura;
                    inventario.Sala = sala;
                    inventario.Situacao = Convert.ToChar(InventarioVacina.DescricaoSituacao.Aberto);
                    Session.Save(inventario);

                    //IList<EstoqueVacina> estoques = Factory.GetInstance<IEstoqueVacina>().BuscarPorSalaVacina<EstoqueVacina>(inventario.Sala.Codigo);
                    IList<EstoqueVacina> estoques = Factory.GetInstance<IEstoqueVacina>().BuscarPorSalaVacinaValidadeLoteSuperior<EstoqueVacina>(inventario.Sala.Codigo, inventario.DataInventario);

                    foreach (ViverMais.Model.EstoqueVacina estoque in estoques)
                    {
                        ViverMais.Model.ItemInventarioVacina iteminventario = new ViverMais.Model.ItemInventarioVacina();
                        iteminventario.Inventario = inventario;
                        iteminventario.LoteVacina = estoque.Lote;
                        iteminventario.QtdEstoque = estoque.QuantidadeEstoque;
                        iteminventario.QtdContada = estoque.QuantidadeEstoque;
                        Session.Save(iteminventario);
                    }

                    Session.Transaction.Commit();

                    return inventario.Codigo;
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        T IInventarioVacina.BuscarItemInventario<T>(int co_inventario, long co_lotevacina)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemInventarioVacina AS i WHERE i.Inventario.Codigo = " + co_inventario;
            hql += " AND i.LoteVacina.Codigo = " + co_lotevacina;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }
    }
}
