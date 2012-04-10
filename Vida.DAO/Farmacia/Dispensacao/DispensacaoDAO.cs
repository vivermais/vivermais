using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Dispensacao;
using System.Data;
using System.Collections;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Movimentacao;

namespace ViverMais.DAO.Farmacia.Dispensacao
{
    public class DispensacaoDAO : FarmaciaServiceFacadeDAO, IDispensacao
    {
        #region IDispensacao Members

        IList<T> IDispensacao.BuscarItensDispensacaoPorPaciente<T>(string codigoPaciente)
        {
            string hql = "from ViverMais.Model.ItensDispensacao item where item.Dispensacao.CodigoPaciente =" +codigoPaciente;
            return Session.CreateQuery(hql).List<T>();
        }

        T IDispensacao.BuscarPorItem<T>(long codigoReceita, int codigoLoteMedicamento, DateTime dataAtendimento) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDispensacao AS itemdispensacao WHERE itemdispensacao.Receita.Codigo = " + codigoReceita + " ";
            hql += "AND itemdispensacao.LoteMedicamento.Codigo = " + codigoLoteMedicamento + " AND to_char(itemdispensacao.DataAtendimento,'dd/mm/yyyy') = '" + dataAtendimento.ToString("dd/MM/yyyy") + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        T IDispensacao.BuscarPorProfissionalPaciente<T>(string id_profissional, string id_paciente, DateTime data) 
        {
            string hql = string.Empty;
            hql = "from ViverMais.Model.Dispensacao as dispensacao where dispensacao.CodigoProfissional = " + id_profissional;
            hql += " and dispensacao.CodigoPaciente = " + id_paciente + " and to_char(dispensacao.DataReceita,'dd/mm/yyyy') = '" + data.ToString("dd/MM/yyyy") + "'";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        DataTable IDispensacao.BuscarPorDispensacaoAgrupado(int id_dispensacao, string[] campos) 
        {
            DataTable tabela = new DataTable();
            tabela.Columns.Add("DataAtendimento", typeof(string));
            tabela.Columns.Add("Farmacia", typeof(string));
            tabela.Columns.Add("CodigoFarmacia", typeof(string));

            string hql = string.Empty;
            hql = "select item.DataAtendimento, item.Farmacia.Nome, item.Farmacia.Codigo FROM Sisfarma3.Model.ItensDispensacao AS item WHERE item.Dispensacao.Codigo = " + id_dispensacao;
            hql += " GROUP BY ";

            foreach (string c in campos)
                hql += " item." + c + ",";

            hql = hql.Remove(hql.Length - 1, 1);

            IList lista = Session.CreateQuery(hql).List();

            for (int i = 0; i < lista.Count; i++)
            {
                object[] obj = (object[])lista[i];

                DataRow linha = tabela.NewRow();
                linha["DataAtendimento"] = DateTime.Parse(obj[0].ToString()).ToString("dd/MM/yyyy");
                linha["Farmacia"] = obj[1].ToString();
                linha["CodigoFarmacia"] = obj[2].ToString();

                tabela.Rows.Add(linha);
            }
            return tabela;
        }

        IList<T> IDispensacao.BuscarItensPorAtendimento<T>(DateTime dataAtendimento, long co_receita, int co_farmacia) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDispensacao item WHERE to_char(item.DataAtendimento,'dd/mm/yyyy') = '" + dataAtendimento.ToString("dd/MM/yyyy") + "'";
            hql += " AND item.Farmacia.Codigo = " + co_farmacia + " AND item.Receita.Codigo = " + co_receita;
            hql += " ORDER BY item.DataAtendimento, item.LoteMedicamento.Medicamento.Nome, item.LoteMedicamento.Lote";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IDispensacao.BuscarItensPorAtendimentoEmedicamento<T>(int id_dispensacao, int id_farmacia, int id_medicamento) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItensDispensacao AS item WHERE";
            hql += " item.Farmacia.Codigo = " + id_farmacia + " AND item.Dispensacao.Codigo = " + id_dispensacao;
            hql += " AND item.LoteMedicamento.Medicamento.Codigo = " + id_medicamento;
            return Session.CreateQuery(hql).List<T>();
        }

        T IDispensacao.BuscarPrimeiroItemDispensadoPorReceita<T>(long co_receita, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDispensacao AS i WHERE i.Receita.Codigo = " + co_receita;
            hql += " AND i.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
            hql += " ORDER BY i.DataAtendimento";
            return Session.CreateQuery(hql).List<T>().FirstOrDefault();
        }

        //IList<T> IDispensacao.BuscarAtendimentosPorReceita<T>(long co_receita)
        //{
        //    string hql = string.Empty;
        //    hql = "SELECT Year(item.DataAtendimento),Month(item.DataAtendimento),Day(item.DataAtendimento),item.Farmacia.Nome,item.Farmacia.Codigo FROM ViverMais.Model.ItemDispensacao item";
        //    hql += " WHERE item.Receita.Codigo = " + co_receita;
        //    hql += " GROUP BY Year(item.DataAtendimento),Month(item.DataAtendimento),Day(item.DataAtendimento),item.Farmacia.Nome,item.Farmacia.Codigo";
        //    //hql += " ORDER BY item.DataAtendimento";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        IList<T> IDispensacao.BuscarDispensacaoPorReceita<T>(long co_receita)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Dispensacao dispensacao";
            hql += " WHERE dispensacao.Receita.Codigo = " + co_receita;                        
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IDispensacao.BuscarDispensacoesPorReceita<T>(long co_receita)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Dispensacao item";
            hql += " WHERE item.Receita.Codigo = " + co_receita;                        
            return Session.CreateQuery(hql).List<T>();
        }

        int IDispensacao.QuantidadeDispensadaMedicamentoReceita(long co_receita, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemDispensacao AS i WHERE i.Receita.Codigo = " + co_receita;
            hql += " AND i.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
            return Session.CreateQuery(hql).List<ItemDispensacao>().Sum(p => p.QtdDispensada);

            //var consulta = from id in lid select new { Quantidade = lid.Sum(p=>p.QtdDispensada) };
        }

        DateTime IDispensacao.BuscarDataAtendimentoRecente(string codigoPaciente, int co_medicamento)
        {
            string hql = string.Empty;
            hql = "SELECT MAX(item.DataAtendimento) FROM ViverMais.Model.ItemDispensacao item WHERE item.Receita.CodigoPaciente = '" + codigoPaciente + "'";
            hql += " AND item.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
            return Session.CreateQuery(hql).UniqueResult<DateTime>();
        }

        DateTime IDispensacao.BuscarDataAtendimentoRecente(long co_receita)
        {
            string hql = string.Empty;
            hql = "SELECT MAX(item.DataAtendimento) FROM ViverMais.Model.ItemDispensacao item";
            hql += " WHERE item.Receita.Codigo = " + co_receita;
            return Session.CreateQuery(hql).UniqueResult<DateTime>();
        }

        object[] IDispensacao.BuscarUltimoAtendimento(string codigoPaciente, int co_medicamento, DateTime dataAtendimento)
        {
            string hql = string.Empty;
            hql = "SELECT SUM(item.QtdDispensada), SUM(item.QtdDias), item.Farmacia.Nome";
            hql += " FROM ViverMais.Model.ItemDispensacao item WHERE item.Receita.CodigoPaciente = '" + codigoPaciente + "'";
            hql += " AND item.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
            hql += " AND to_char(item.DataAtendimento,'dd/mm/yyyy') = '" + dataAtendimento.ToString("dd/MM/yyyy") + "'";
            hql += " GROUP BY item.Farmacia.Nome";
            return Session.CreateQuery(hql).UniqueResult<object[]>();
        }

        void IDispensacao.SalvarItensDispensacao<T>(IList<T> itens)
        {
            //Descomentar depois quando for salvar o item dispensacao

            //using (Session.BeginTransaction())
            //{
            //    try
            //    {
            //        IList<ItemDispensacao> ldi = (IList<ItemDispensacao>)(object)itens;
            //        foreach (ItemDispensacao id in ldi)
            //        {
            //            Estoque es = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(id.Farmacia.Codigo, id.LoteMedicamento.Codigo);
            //            es.QuantidadeEstoque -= id.QtdDispensada;

            //            Session.Save(id);
            //            Session.Save(es);
            //        }

            //        Session.Transaction.Commit();
            //    }
            //    catch (Exception f)
            //    {
            //        Session.Transaction.Rollback();
            //        throw f;
            //    }
            //}
        }

        void IDispensacao.SalvarItemDispensacao<T>(T item)
        {

            using (Session.BeginTransaction())
            {
                try
                {
                    ItemDispensacao itemDispensacao = (ItemDispensacao)(object)item;
                    Estoque estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemDispensacao.Dispensacao.Farmacia.Codigo, itemDispensacao.LoteMedicamento.Codigo);
                    estoque.QuantidadeEstoque -= itemDispensacao.QtdDispensada;

                    Session.Save(itemDispensacao);
                    Session.Save(estoque);

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        void IDispensacao.AlterarItemDispensacao<T>(T item, int qtdAnterior)
        {

            using (Session.BeginTransaction())
            {
                try
                {
                    ItemDispensacao itemDispensacao = (ItemDispensacao)(object)item;
                    Estoque estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemDispensacao.Dispensacao.Farmacia.Codigo, itemDispensacao.LoteMedicamento.Codigo);
                    estoque.QuantidadeEstoque = estoque.QuantidadeEstoque + (qtdAnterior - itemDispensacao.QtdDispensada);

                    Session.SaveOrUpdate(itemDispensacao);
                    Session.Save(estoque);

                    Session.Transaction.Commit();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        void IDispensacao.DeletarItemDispensacao<T>(T item)
        {

            using (Session.BeginTransaction())
            {
                try
                {
                    ItemDispensacao itemDispensacao = (ItemDispensacao)(object)item;
                    Estoque estoque = Factory.GetInstance<IEstoque>().BuscarItemEstoquePorFarmacia<Estoque>(itemDispensacao.Dispensacao.Farmacia.Codigo, itemDispensacao.LoteMedicamento.Codigo);
                    estoque.QuantidadeEstoque += itemDispensacao.QtdDispensada;

                    Session.Delete(itemDispensacao);
                    Session.Save(estoque);

                    Session.Transaction.Commit();
                    Session.Flush();
                }
                catch (Exception f)
                {
                    Session.Transaction.Rollback();
                    throw f;
                }
            }
        }

        List<T> IDispensacao.BuscarItensDispensacao<T>(int codigoDispensacao)
        {
            string hql = string.Empty;
            hql += "FROM ViverMais.Model.ItemDispensacao as itd";
            hql += " where itd.Dispensacao.Codigo = " + codigoDispensacao.ToString();
            return Session.CreateQuery(hql).List<T>().ToList();
        }

        T IDispensacao.BuscarUltimaComMedicamento<T>(string codigoPaciente, int codigoMedicamento)
        {
            string hql = string.Empty;
            //hql += "FROM ViverMais.Model.Dispensacao as d";
            //hql += " inner join ViverMais.Model.ItemDispensacao as id ";
            //hql += " WHERE d.Receita.CodigoPaciente = " + codigoPaciente;
            //hql += " AND id.Medicamento.Codigo = " + codigoMedicamento.ToString();
            //hql += " AND d.DataAtendimento = (selext max()";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        #endregion
    }
}
