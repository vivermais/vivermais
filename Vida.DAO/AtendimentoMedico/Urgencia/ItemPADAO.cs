using System;
using NHibernate;
using ViverMais.Model;
using System.Linq;
using System.Text;
using NHibernate.Criterion;
using System.Collections.Generic;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using System.Globalization;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class ItemPADAO: UrgenciaServiceFacadeDAO, IItemPA
    {
        bool IItemPA.ValidaCadastroPorCodigoSIGTAP<T>(string codigosigtap, int co_item)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.ItemPA AS i WHERE i.CodigoSIGTAP = '" + codigosigtap + "' AND i.Codigo <> " + co_item;
            return Session.CreateQuery(hql).List<T>().Count() <= 0;
        }

        IList<T> IItemPA.BuscarItem<T>(string nome)
        {
            string hql = "FROM ViverMais.Model.ItemPA i WHERE TRANSLATE(UPPER(i.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') LIKE '" + GenericsFunctions.RemoveDiacritics(nome).ToUpper() + "%' ORDER BY i.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        //private 
    }
}
