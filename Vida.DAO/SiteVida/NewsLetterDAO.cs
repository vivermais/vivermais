using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.SiteViverMais;

namespace ViverMais.DAO.SiteViverMais
{
    class NewsLetterDAO: ViverMaisServiceFacadeDAO , INewsLetterServiceFacade
    {
        T INewsLetterServiceFacade.BuscarEmailUsuario<T>(string Email)
        {
            string hql = "FROM ViverMais.Model.NewsLetter newsLetter ";
            hql += "WHERE  newsLetter.Email =  '" + Email + "'";
            return Session.CreateQuery(hql).UniqueResult<T>() ;


        }
            
        
    }
}
