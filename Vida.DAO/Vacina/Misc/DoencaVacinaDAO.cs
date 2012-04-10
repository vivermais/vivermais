using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;

namespace ViverMais.DAO.Vacina.Misc
{
    public class DoencaVacinaDAO : VacinaServiceFacadeDAO, IDoencaVacina
    {
        bool IDoencaVacina.ValidarCadastroDoenca(int co_doenca, string nome)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Doenca AS doenca WHERE TRANSLATE(UPPER(doenca.Nome),'ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜ','ACEIOUAEIOUAEIOUAOEU')" + 
                  " = TRANSLATE(UPPER('" + nome + "'),'ÁÇÉÍÓÚÀÈÌÒÙÂÊÎÔÛÃÕËÜ','ACEIOUAEIOUAEIOUAOEU') AND doenca.Codigo <> " + co_doenca;
            return Session.CreateQuery(hql).List<Doenca>().Count <= 0;
        }
    }
}
