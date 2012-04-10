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
using ViverMais.ServiceFacade.ServiceFacades.Agregado.Misc;

namespace ViverMais.DAO.Agregado.Misc
{
    public class SubGrupoAgregadoDAO : ViverMaisServiceFacadeDAO, ISubGrupoAgregado
    {
        #region ISubGrupoAgregado Members

        public SubGrupoAgregadoDAO()
        {
            
        }

        IList<T> ISubGrupoAgregado.BuscaPorGrupo<T>(string id_grupo)
        {
            string hql = "from ViverMais.Model.SubGrupoAgregado subGrupo";
            hql += " where subGrupo.GrupoAgregado=" + id_grupo;
            hql += " order by subGrupo.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

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
