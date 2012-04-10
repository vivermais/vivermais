using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace ViverMais.ServiceFacade.ServiceFacades.Vacina
{
    public interface IRelatorioVacina
    {
        //Hashtable ObterRelatorioConferenciaInventario(int co_inventario);
        //Hashtable ObterRelatorioFinalInventario(int co_inventario);
        //Hashtable ObterRelatorioDispensacao(int co_dispensacao);
        //Hashtable ObterCartaoVacina<P, A, C>(P _paciente, A _avatar, IList<C> _cartoes);
        //Hashtable ObterRelatorioMovimento(long co_movimento);

        ReportDocument ObterRelatorioMovimento(long co_movimento);
        ReportDocument ObterCartaoVacina<P, A, C>(P _paciente, A _avatar, IList<C> _cartoes);
        ReportDocument ObterRelatorioDispensacao(int co_dispensacao);
        ReportDocument ObterRelatorioConferenciaInventario(int co_inventario);
        ReportDocument ObterRelatorioFinalInventario(int co_inventario);

        MemoryStream ObterRelatorioProducao<T>(IList<T> unidades, DateTime data);
        MemoryStream ObterRelatorioProducao<T>(IList<T> unidades, DateTime datainicio, DateTime datafim);
        //void CorrigirDuplicatas();
    }
}
