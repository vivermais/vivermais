using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;
using ViverMais.ServiceFacade.ServiceFacades.Agregado;

namespace ViverMais.DAO.Agregado
{
    public class AgregadoDAO : ViverMaisServiceFacadeDAO, IAgregado
    {
        #region IAgregado Members

        public AgregadoDAO()
        {
            
        }

        IList<T> IAgregado.BuscaPorSubGrupo<T>(string id_subgrupo)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.Agregado agregado";
            hql += " where agregado.SubGrupoAgregado.Codigo=" + id_subgrupo;
            hql += " order by agregado.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        

        //T IAgregado.VerificaExistenciaProcedimentoNoAgregado<T>(string id_procedimento, string id_municipio)
        //{
        //    string hql = String.Empty;
        //    hql += "from ViverMais.Model.Agregado agregado";
        //    hql += " where agregado.SubGrupoAgregado.Codigo=" + id_subgrupo;
        //    hql += " order by agregado.Nome";
        //    return Session.CreateQuery(hql).List<T>();
        //}

        //T IPreparo.BuscaPreparo<T>(int id_preparo)
        //{
        //    string hql = "from ViverMais.Model.Preparo preparo";
        //    hql += " where preparo.Codigo="+ id_preparo;
        //    T resultados = Session.CreateQuery(hql).UniqueResult<T>();
        //    return resultados;
        //}

        //IList<T> IPreparo.BuscarPreparoPorProcedimento<T>(string id_procedimento)
        //{
        //    string hql = String.Empty;
        //    hql += "Select elements(tp.Preparos) from ViverMais.Model.TipoProcedimento tp";
        //    hql += " where tp.Procedimento.Codigo="+id_procedimento;
        //    return Session.CreateQuery(hql).List<T>();
        //}

        //#region IParametroAgendaDistrital Members

        //public ParametroAgendaDistritalDAO()
        //{

        //}

        //T IParametroAgendaDistrital.BuscarDuplicidade<T>(int id_parametroagenda, int id_distrito)
        //{
        //    string hql = "FROM ViverMais.Model.ParametroAgendaDistrital parametro ";
        //    hql += " WHERE parametro.ParametroAgenda.Codigo ='" + id_parametroagenda + "'";
        //    hql += " and parametro.ID_Distrito ='" + id_distrito + "'";
        //    T resultados = Session.CreateQuery(hql).UniqueResult<T>();
        //    return resultados;
        //}

        //IList<T> IParametroAgendaDistrital.BuscarParametros<T>(int id_parametroagenda)
        //{
        //    string hql = string.Empty;
        //    hql = "FROM ViverMais.Model.ParametroAgendaDistrital parametros WHERE parametros.ParametroAgenda.Codigo = '" + id_parametroagenda + "'";
        //    return Session.CreateQuery(hql).List<T>();
        //}
        #endregion
    }
}
