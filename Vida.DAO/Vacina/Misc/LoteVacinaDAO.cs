using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Movimentacao;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Vacina.Misc
{
    public class LoteVacinaDAO : VacinaServiceFacadeDAO, ILoteVacina
    {
        bool ILoteVacina.ValidaCadastroLote<T>(string identificacao, DateTime datavalidade, int co_itemvacina, long co_lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LoteVacina AS l WHERE l.Identificacao = '" + identificacao + "'";
            hql += " AND l.ItemVacina.Codigo = " + co_itemvacina;
            hql += " AND TO_CHAR(l.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'";
            hql += " AND l.Codigo <> " + co_lote;
            return Session.CreateQuery(hql).List<T>().Count <= 0;
        }

        bool ILoteVacina.ValidaCadastroLote<T>(string identificacao, long co_lote)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LoteVacina AS l WHERE ";
            hql += " TRANSLATE(UPPER(l.Identificacao),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')=";
            hql += " TRANSLATE(UPPER('" + identificacao + "'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') ";
            hql += " AND l.Codigo <> " + co_lote;
            return Session.CreateQuery(hql).List<T>().Count <= 0;
        }

        IList<T> ILoteVacina.BuscarLote<T>(string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int numeroaplicacoes)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LoteVacina AS l WHERE";

            if (!string.IsNullOrEmpty(lote))
                hql += " l.Identificacao LIKE '" + lote + "%'";

            if (datavalidade != DateTime.MinValue)
                hql += !string.IsNullOrEmpty(lote) ? " AND TO_CHAR(l.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'" : " TO_CHAR(l.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'";

            if (co_vacina != -1)
                hql += !string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue ? " AND l.ItemVacina.Vacina.Codigo = " + co_vacina : " l.ItemVacina.Vacina.Codigo = " + co_vacina;

            if (co_fabricante != -1)
                hql += !string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue || co_vacina != -1 ? " AND l.ItemVacina.FabricanteVacina.Codigo = " + co_fabricante : " l.ItemVacina.FabricanteVacina.Codigo = " + co_fabricante;

            if (numeroaplicacoes != -1)
                hql += !string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue || co_vacina != -1 || co_fabricante != -1 ? " AND l.ItemVacina.Aplicacao=" + numeroaplicacoes : " l.ItemVacina.Aplicacao=" + numeroaplicacoes;

            var result = (from lotevacina in Session.CreateQuery(hql).List<LoteVacina>()
                          orderby lotevacina.ItemVacina.Vacina.Nome,
                          lotevacina.ItemVacina.FabricanteVacina.Nome, lotevacina.Identificacao, lotevacina.DataValidade
                          select lotevacina);

            return (IList<T>)(object)result.ToList();

            //hql += " ORDER BY l.ItemVacina.Vacina.Nome, l.ItemVacina.FabricanteVacina.Nome, l.Identificacao, l.DataValidade";
            //return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILoteVacina.BuscarLotesValidos<T>(string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int numeroaplicacoes, DateTime aberturainventario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.LoteVacina AS l WHERE";

            if (!string.IsNullOrEmpty(lote))
                hql += " l.Identificacao LIKE '" + lote + "%'";

            if (datavalidade != DateTime.MinValue)
                hql += !string.IsNullOrEmpty(lote) ? " AND TO_CHAR(l.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'" : " TO_CHAR(l.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'";
            else
            {
                if (co_vacina == -1 && co_fabricante == -1 && numeroaplicacoes == -1)
                    hql += !string.IsNullOrEmpty(lote) ? " AND l.DataValidade >= DATE '" + aberturainventario.ToString("yyyy-MM-dd") + "'" : " l.DataValidade >= DATE '" + aberturainventario.ToString("yyyy-MM-dd") + "'";
                else
                    hql += !string.IsNullOrEmpty(lote) ? " AND l.DataValidade >= DATE '" + aberturainventario.ToString("yyyy-MM-dd") + "' AND " : " l.DataValidade >= DATE '" + aberturainventario.ToString("yyyy-MM-dd") + "' AND ";
            }

            if (co_vacina != -1)
                hql += (!string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue) ? " AND l.ItemVacina.Vacina.Codigo = " + co_vacina : " l.ItemVacina.Vacina.Codigo = " + co_vacina;

            if (co_fabricante != -1)
                hql += (!string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue || co_vacina != -1) ? " AND l.ItemVacina.FabricanteVacina.Codigo = " + co_fabricante : " l.ItemVacina.FabricanteVacina.Codigo = " + co_fabricante;

            if (numeroaplicacoes != -1)
                hql += (!string.IsNullOrEmpty(lote) || datavalidade != DateTime.MinValue || co_vacina != -1 || co_fabricante != -1) ? " AND l.ItemVacina.Aplicacao=" + numeroaplicacoes : " l.ItemVacina.Aplicacao=" + numeroaplicacoes;

            var result = (from lotevacina in Session.CreateQuery(hql).List<LoteVacina>()
                          orderby lotevacina.ItemVacina.Vacina.Nome,
                          lotevacina.ItemVacina.FabricanteVacina.Nome, lotevacina.Identificacao, lotevacina.DataValidade
                          select lotevacina);

            return (IList<T>)(object)result.ToList();

            //hql += " ORDER BY l.ItemVacina.Vacina.Nome, l.ItemVacina.FabricanteVacina.Nome, l.Identificacao, l.DataValidade";
            //return Session.CreateQuery(hql).List<T>();
        }

        //        IList<T> ILoteVacina.ListarFabricantesLotesRegistrados<T>(int co_vacina)
        //        {
        //            string hql = string.Empty;
        //            hql = @"SELECT DISTINCT l.ItemVacina.FabricanteVacina
        //                    FROM ViverMais.Model.LoteVacina AS l WHERE l.ItemVacina.Vacina.Codigo = " + co_vacina +
        //                    //" AND l.DataValidade >= DATE '" + data.ToString("yyyy-MM-dd") + "'" +
        //                    " ORDER BY l.ItemVacina.FabricanteVacina.Nome";
        //            return Session.CreateQuery(hql).List<T>();
        //        }

        IList<T> ILoteVacina.BuscarLotesValidos<T>(int co_vacina, DateTime data)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.LoteVacina AS l WHERE l.ItemVacina.Vacina.Codigo = " + co_vacina +
                " AND l.DataValidade >= DATE '" + data.ToString("yyyy-MM-dd") + "'" +
                " ORDER BY l.Identificacao";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILoteVacina.BuscarLotesValidos<T>(DateTime data)
        {
            string hql = string.Empty;
            hql = @"FROM ViverMais.Model.LoteVacina AS l WHERE " +
                "l.DataValidade >= DATE '" + data.ToString("yyyy-MM-dd") + "'" +
                " ORDER BY l.Identificacao";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILoteVacina.BuscarLotesQuantidadeDisponivel<T>(int co_sala)
        {
            string hql = string.Empty;
            hql = "SELECT e.Lote FROM ViverMais.Model.EstoqueVacina e WHERE e.Sala.Codigo = " + co_sala + " AND e.QuantidadeEstoque > 0 ORDER BY e.Lote.ItemVacina.Vacina.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ILoteVacina.BuscarLotesQuantidadeDisponivel<T>(int co_sala, string lote, DateTime datavalidade, int co_vacina, int co_fabricante, int qtdaplicacao)
        {
            string hql = string.Empty;

            hql = "SELECT estoque.Lote FROM ViverMais.Model.EstoqueVacina estoque WHERE estoque.Sala.Codigo = " + co_sala + " AND estoque.QuantidadeEstoque > 0";

            if (!string.IsNullOrEmpty(lote))
                hql += " AND estoque.Lote.Identificacao LIKE '" + lote + "%'";

            if (co_vacina != -1)
                hql += " AND estoque.Lote.ItemVacina.Vacina.Codigo=" + co_vacina;

            if (co_fabricante != -1)
                hql += " AND estoque.Lote.ItemVacina.FabricanteVacina.Codigo=" + co_fabricante;

            if (qtdaplicacao != -1)
                hql += " AND estoque.Lote.ItemVacina.Aplicacao=" + qtdaplicacao;

            if (datavalidade != DateTime.MinValue)
                hql += " AND TO_CHAR(estoque.Lote.DataValidade,'DD/MM/YYYY') = '" + datavalidade.ToString("dd/MM/yyyy") + "'";

            //hql += @" ORDER BY estoque.Lote.ItemVacina.Vacina.Nome";
            var result = (from lotevacina in Session.CreateQuery(hql).List<LoteVacina>()
                          orderby lotevacina.ItemVacina.Vacina.Nome
                          select lotevacina);

            return (IList<T>)(object)result.ToList();

            //return Session.CreateQuery(hql).List<T>();
        }
    }
}
