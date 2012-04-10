using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Inventario;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Collections;
using System.Data;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.DAO.Farmacia.Inventario
{
    public class InventarioDAO : FarmaciaServiceFacadeDAO, IInventario
    {
        IList<T> IInventario.BuscarPorSituacao<T>(char situacao, int id_farmacia) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Inventario AS inventario WHERE inventario.Situacao = '" + situacao + "'";
            hql += " AND inventario.Farmacia.Codigo = " + id_farmacia;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IInventario.BuscarPorFarmacia<T>(int id_farmacia) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Inventario AS i WHERE i.Farmacia.Codigo = " + id_farmacia + " ORDER BY i.DataInventario DESC, i.Situacao ASC";
            return Session.CreateQuery(hql).List<T>();
        }

        void IInventario.AbrirInventario<T>(int co_farmacia, T _inventario) 
        {
            ViverMais.Model.Inventario inventario = (ViverMais.Model.Inventario)(object)_inventario;
            IList<ViverMais.Model.Estoque> estoquesFarmacia = Factory.GetInstance<IEstoque>().BuscarPorFarmacia<ViverMais.Model.Estoque>(co_farmacia);

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(inventario);

                    foreach (ViverMais.Model.Estoque e in estoquesFarmacia)
                    {
                        ViverMais.Model.ItemInventario itemInventario = new ViverMais.Model.ItemInventario();
                        itemInventario.Inventario = inventario;
                        itemInventario.LoteMedicamento = e.LoteMedicamento;
                        itemInventario.QtdEstoque = e.QuantidadeEstoque;
                        itemInventario.QtdContada = e.QuantidadeEstoque;
                        Session.Save(itemInventario);
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

        void IInventario.EncerrarInventario<T>(T _inventario)
        {
            ViverMais.Model.Inventario inventario = (ViverMais.Model.Inventario)(object)_inventario;

            using (Session.BeginTransaction())
            {
                try
                {
                    Session.Save(inventario);

                    IList<ItemInventario> itensInventario = Factory.GetInstance<IInventario>().ListarItensInventario<ItemInventario>(inventario.Codigo);
                    IList<Estoque> estoques = Factory.GetInstance<IEstoque>().BuscarPorFarmacia<Estoque>(inventario.Farmacia.Codigo);

                    foreach (ItemInventario i in itensInventario)
                    {
                        Estoque e = estoques.Where(p => p.LoteMedicamento.Codigo == i.LoteMedicamento.Codigo).FirstOrDefault();

                        if (e != null)
                        {
                            e.QuantidadeEstoque = i.QtdContada;
                            Session.Update(e);
                        }
                        else
                        {
                            e = new Estoque();
                            e.LoteMedicamento = i.LoteMedicamento;
                            e.Farmacia = i.Inventario.Farmacia;
                            e.QuantidadeEstoque = i.QtdContada;
                            Session.Save(e);
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

        IList<T> IInventario.ListarItensInventario<T>(int co_inventario) 
        {
            string hql = string.Empty;
            hql =  "FROM ViverMais.Model.ItemInventario AS i WHERE i.Inventario.Codigo = " + co_inventario;
            hql += " ORDER BY i.LoteMedicamento.Medicamento.Nome, i.LoteMedicamento.Lote";
            return Session.CreateQuery(hql).List<T>();
        }

        T IInventario.BuscarItemInventario<T>(int co_inventario, int co_lotemedicamento) 
        {
            string hql = string.Empty;
            hql  = "FROM ViverMais.Model.ItemInventario AS i WHERE i.Inventario.Codigo = " + co_inventario;
            hql += " AND i.LoteMedicamento.Codigo = " + co_lotemedicamento;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        Hashtable IInventario.RelatorioInventario(int co_inventario, int tipo)
        {
            Hashtable hash = new Hashtable();
            DataTable cabecalho = new DataTable();
            cabecalho.Columns.Add("CodigoInventario", typeof(int));
            cabecalho.Columns.Add("Farmacia", typeof(string));
            cabecalho.Columns.Add("Unidade", typeof(string));

            DataTable corpo = new DataTable();
            corpo.Columns.Add("CodigoInventario", typeof(int));
            corpo.Columns.Add("Medicamento", typeof(string));
            corpo.Columns.Add("UnidadeMedida", typeof(string));
            corpo.Columns.Add("Fabricante", typeof(string));
            corpo.Columns.Add("Lote", typeof(string));
            corpo.Columns.Add("Validade", typeof(string));

            if (tipo == (int)ViverMais.Model.Inventario.TipoRelatorio.FINAL)
            {
                corpo.Columns.Add("QtdContada", typeof(int));
                corpo.Columns.Add("QtdEstoque", typeof(int));
                corpo.Columns.Add("Diferenca", typeof(int));
            }

            ViverMais.Model.Inventario inventario = Factory.GetInstance<IInventario>().BuscarPorCodigo<ViverMais.Model.Inventario>(co_inventario);
            DataRow linha = cabecalho.NewRow();

            linha["CodigoInventario"] = co_inventario;
            linha["Farmacia"] = inventario.Farmacia.Nome;
            cabecalho.Rows.Add(linha);

            IList<ItemInventario> itensinventario = Factory.GetInstance<IInventario>().ListarItensInventario<ItemInventario>(co_inventario);

            foreach (ItemInventario iteminventario in itensinventario)
            {
                linha = corpo.NewRow();
                linha["CodigoInventario"] = co_inventario;
                linha["Medicamento"] = iteminventario.LoteMedicamento.Medicamento.Nome;
                linha["UnidadeMedida"] = iteminventario.LoteMedicamento.Medicamento.UnidadeMedida.Nome;
                linha["Fabricante"] = iteminventario.LoteMedicamento.Fabricante.Nome;
                linha["Lote"] = iteminventario.LoteMedicamento.Lote;
                linha["Validade"] = iteminventario.LoteMedicamento.Validade.ToString("dd/MM/yyyy");

                if (tipo == (int)ViverMais.Model.Inventario.TipoRelatorio.FINAL)
                {
                    linha["QtdContada"] = iteminventario.QtdContada;
                    linha["QtdEstoque"] = iteminventario.QtdEstoque;
                    linha["Diferenca"] = iteminventario.QtdContada - iteminventario.QtdEstoque;
                }

                corpo.Rows.Add(linha);
            }

            hash.Add("cabecalho", cabecalho);
            hash.Add("corpo", corpo);

            return hash;
        }
    }
}
