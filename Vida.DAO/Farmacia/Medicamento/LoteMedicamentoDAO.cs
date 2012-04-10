using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.DAO.Farmacia.Medicamento
{
    public class LoteMedicamentoDAO : FarmaciaServiceFacadeDAO, ILoteMedicamento
    {
        #region ILoteMedicamento Members
            bool ILoteMedicamento.ValidaCadastroLote<T>(string lote, DateTime data_validade, int co_medicamento, int co_fabricante, int co_lote) 
            {
                string hql = string.Empty;
                hql  = "FROM ViverMais.Model.LoteMedicamento AS l WHERE l.Lote = '" + lote + "'";
                hql += " AND l.Fabricante.Codigo = " + co_fabricante + " AND l.Medicamento.Codigo = " + co_medicamento;
                hql += " AND to_char(l.Validade,'dd/mm/yyyy') = '" + data_validade.ToString("dd/MM/yyyy") + "'";
                hql += " AND l.Codigo <> " + co_lote;
                return Session.CreateQuery(hql).List<T>().Count <= 0;
            }

            IList<T> ILoteMedicamento.BuscarPorDescricao<T>(string lote, DateTime data_validade, int co_medicamento, int co_fabricante) 
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.LoteMedicamento AS l WHERE";

                if (!string.IsNullOrEmpty(lote))
                    hql += " l.Lote LIKE '" + lote + "%'";

                if (data_validade != DateTime.MinValue)
                    hql += !string.IsNullOrEmpty(lote) ? " AND to_char(l.Validade,'dd/MM/yyyy) = '" + data_validade.ToString("dd/MM/yyyy") + "'" : " to_char(l.Validade,'dd/MM/yyyy) = '" + data_validade.ToString("dd/MM/yyyy") + "'";

                if (co_medicamento != -1)
                    hql += !string.IsNullOrEmpty(lote) || data_validade != DateTime.MinValue ? " AND l.Medicamento.Codigo = " + co_medicamento : " l.Medicamento.Codigo = " + co_medicamento;

                if (co_fabricante != -1)
                    hql += !string.IsNullOrEmpty(lote) || data_validade != DateTime.MinValue || co_medicamento != -1 ? " AND l.Fabricante.Codigo = " + co_fabricante : " l.Fabricante.Codigo = " + co_fabricante;

                hql += " ORDER BY l.Medicamento.Nome";
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> ILoteMedicamento.BuscarPorMedicamento<T>(int co_medicamento)
            {
                string hql = string.Empty;
                hql = "FROM ViverMais.Model.LoteMedicamento AS l WHERE l.Medicamento.Codigo = " + co_medicamento;
                return Session.CreateQuery(hql).List<T>();
            }

            IList<T> ILoteMedicamento.BuscarPorEstoqueAlmoxarifado<T>(int co_medicamento, int co_farmacia)
            {
                string hql = string.Empty;
                hql = "SELECT estoque.LoteMedicamento FROM ViverMais.Model.Estoque AS estoque WHERE estoque.Farmacia.Codigo = " + co_farmacia;
                hql += " AND estoque.LoteMedicamento.Medicamento.Codigo = " + co_medicamento;
                hql += " AND estoque.QuantidadeEstoque > 0";
                hql += " AND to_char(estoque.LoteMedicamento.Validade,'dd/MM/yyyy') >= '" + DateTime.Today.ToString("dd/MM/yyyy") + "'";
                return Session.CreateQuery(hql).List<T>();
            }

        #endregion
    }
}
