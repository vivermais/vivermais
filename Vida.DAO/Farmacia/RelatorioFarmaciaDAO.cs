using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;

namespace ViverMais.DAO.Farmacia
{
    public class RelatorioFarmaciaDAO : FarmaciaServiceFacadeDAO, IRelatorioFarmacia
    {

        #region IRelatorioFarmacia Members

        IList IRelatorioFarmacia.ObterMovimentacaoDiaria(DateTime data, int codigoFarmacia)
        {
            string hql =
               "select item.LoteMedicamento.Medicamento.Nome, item.LoteMedicamento.Medicamento.UnidadeMedida.Sigla, sum(item.QtdDispensada) " +
               " from ViverMais.Model.ItensDispensacao item " +
               " where item.Farmacia.Codigo=" + codigoFarmacia + " and " +
               " item.DataAtendimento='" + data.ToString("yyyy-MM-dd") + "' " +
               " group by item.LoteMedicamento.Medicamento.Nome, item.LoteMedicamento.Medicamento.UnidadeMedida.Sigla";
            return Session.CreateQuery(hql).List();
        }

        IList<T> IRelatorioFarmacia.ObterRelatorioPosicaoEstoqueLote<T>(int codigoFarmacia)
        {
            string hql =
                "from ViverMais.Model.Estoque estoque " +
                " where estoque.Farmacia.Codigo=" + codigoFarmacia +
                " and estoque.QuantidadeEstoque > 0" +
                " order by estoque.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IRelatorioFarmacia.ObterRelatorioLotesAVencer<T>(int codigoFarmacia, DateTime data) 
        {
            string hql =
                "from ViverMais.Model.Estoque estoque " +
                " where estoque.Farmacia.Codigo=" + codigoFarmacia +
                " and estoque.LoteMedicamento.Validade='" + data.ToString("yyyy-MM-dd") + "'"+
                " order by estoque.LoteMedicamento.Validade, estoque.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList IRelatorioFarmacia.ObterRelatorioConsumoMedioMensal(int codigoFarmacia, DateTime dataInicial, DateTime dataFinal)
        {
            string hql =
               "select item.LoteMedicamento.Medicamento.Nome, sum(item.QtdDispensada) " +
               " from ViverMais.Model.ItensDispensacao item " +
               " where item.Farmacia.Codigo=" + codigoFarmacia + " and " +
               " item.DataAtendimento between '" + dataInicial.ToString("yyyy-MM-dd") + "' and '" + dataFinal.ToString("yyyy-MM-dd") + "' " +
               " group by item.LoteMedicamento.Medicamento.Nome order by item.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List();
        }

        IList IRelatorioFarmacia.ObterRelatorioProducaoUsuario(int codigoUsuario, DateTime data)
        {
            string hql =
               "select item.LoteMedicamento.Medicamento.Nome, sum(item.QtdDispensada) " +
               " from ViverMais.Model.ItensDispensacao item " +
               " where item.CodigoUsuario=" + codigoUsuario + " and " +
               " item.DataAtendimento=" + data.ToString("yyyy-MM-dd") + "' " +
               " group by item.LoteMedicamento.Medicamento.Nome";
            return Session.CreateQuery(hql).List();
        }

        IList IRelatorioFarmacia.ObterRelatorioConsolidadoRM(int codigoDistrito, int mes, int ano)
        {
            string hql =
               "select item.LoteMedicamento.Medicamento.Nome, item.LoteMedicamento.Medicamento.UnidadeMedida.Sigla, sum(item.QtdFornecida), sum(item.QtdPedida), item.Requisicao.Farmacia.CodigoUnidade" +
               " from ViverMais.Model.ItensRequisicao item " +
               " where month(item.Requisicao.DataRequisicao)="+mes +
               " and year(item.Requisicao.DataRequisicao)="+ano+
               " group by item.LoteMedicamento.Medicamento.Nome, item.LoteMedicamento.Medicamento.UnidadeMedida.Sigla, item.Requisicao.Farmacia.CodigoUnidade " +
               " order by item.LoteMedicamento.Medicamento.Nome";
            IList result = Session.CreateQuery(hql).List();
            IList resultFinal = new ArrayList();
            IList<ViverMais.Model.EstabelecimentoSaude> unidades = Factory.GetInstance<IEstabelecimentoSaude>().BuscarUnidadeDistrito<ViverMais.Model.EstabelecimentoSaude>(codigoDistrito);
            IList unids = new ArrayList();
            foreach (ViverMais.Model.EstabelecimentoSaude item in unidades)
            {
                unids.Add(item.CNES);
            }
            foreach (object item in result)
            {
                object[] valor = (object[])item;
                if (valor[4] != null && unids.Contains(valor[4].ToString()))
                    resultFinal.Add(item);
            }
            return resultFinal;
        }

        IList IRelatorioFarmacia.ObterRelatorioNotaFiscalLote(int codigoLoteMedicamento)
        {
            string hql =
               "select item.NotaFiscal.NumeroNotaFiscal, item.NotaFiscal.DataRecebimento, item.NotaFiscal.Fornecedor.RazaoSocial, item.NotaFiscal.Empenho " +
               " from ViverMais.Model.ItemNotaFiscal item " +
               " where item.LoteMedicamento.Codigo=" + codigoLoteMedicamento +
               " order by item.NotaFiscal.DataRecebimento";
            return Session.CreateQuery(hql).List();
        }

        IList IRelatorioFarmacia.ObterRelatorioValorUnitarioMedicamento(DateTime dataInicio, DateTime dataFim)
        {
            string hql =
               "select item.LoteMedicamento.Medicamento.Nome, item.ValorUnitario, item.Quantidade " +
               " from ViverMais.Model.ItemNotaFiscal item " +
               " where item.NotaFiscal.DataRecebimento between '" + dataInicio.ToString("yyyy-MM-dd") + "' and '" + dataFim.ToString("yyyy-MM-dd") + "'" +
               " order by item.NotaFiscal.DataRecebimento";
            return Session.CreateQuery(hql).List();
        }

        #endregion
    }
}
