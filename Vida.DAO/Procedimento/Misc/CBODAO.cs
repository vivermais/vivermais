using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using ViverMais.Model;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace ViverMais.DAO.Procedimento.Misc
{
    public class CBODAO : ViverMaisServiceFacadeDAO, ICBO
    {
        /// <summary>
        /// Lista os CBOs referentes ao Procedimento
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_procedimento"></param>
        /// <returns></returns>
        IList<T> ICBO.ListarCBOsPorProcedimento<T>(string id_procedimento)
        {
            string hql = "select distinct po.CBO";
            hql += " from ViverMais.Model.ProcedimentoOcupacao po";
            hql += " where po.Procedimento.Codigo = '" + id_procedimento + "'";
            hql += " order by po.CBO.Nome";
            return Session.CreateQuery(hql).List<T>();
            //IQuery query = NHibernateHttpHelper.GetCurrentSession("ViverMais").CreateQuery(hql);
            //return query.List<T>();
        }

        /// <summary>
        /// Lista os CBOs que fazem parte do Grupo. Ex: 2231, possui os CBOS 223113 - Clínico, 223110
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="grupoCBO"></param>
        /// <returns></returns>
        IList<T> ICBO.ListarPorGrupo<T>(string grupoCBO)
        {
            string hql = "from ViverMais.Model.CBO cbo where cbo.GrupoCBO.Codigo = '" + grupoCBO + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ICBO.ListarCBOsSaude<T>()
        {
            string hql = "FROM ViverMais.Model.CBO cbo WHERE cbo.OcupacaoSaude = 'S' ORDER BY cbo.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        bool ICBO.CBOPertenceMedico<T>(T _cbo)
        {
            CBO cbo = (CBO)(object)_cbo;
            
            if (cbo.CategoriaOcupacao != null && cbo.CategoriaOcupacao.Codigo == CategoriaOcupacao.MEDICO)
                return true;

            return false;
        }

        bool ICBO.CBOPertenceEnfermeiro<T>(T _cbo)
        {
            CBO cbo = (CBO)(object)_cbo;
            ICBO icbo = Factory.GetInstance<ICBO>();

            if (cbo.CategoriaOcupacao != null && cbo.CategoriaOcupacao.Codigo == CategoriaOcupacao.ENFERMEIRO
             && !icbo.ListarCBOsTecnicosAuxiliarEnfermagem<CBO>().Select(p => p.Codigo).Contains(cbo.Codigo))
                return true;

            return false;
        }

        bool ICBO.CBOPertenceAuxiliarTecnicoEnfermagem<T>(T _cbo)
        {
            CBO cbo = (CBO)(object)_cbo;
            ICBO icbo = Factory.GetInstance<ICBO>();

            if (icbo.ListarCBOsTecnicosAuxiliarEnfermagem<CBO>().Select(p=>p.Codigo).Contains(cbo.Codigo))
                return true;

            return false;
        }

        IList<T> ICBO.ListarCBOsTecnicosAuxiliarEnfermagem<T>()
        {
            string[] cbosEnfermagem = Regex.Split(ViverMais.DAO.Properties.Resources.CBOsTecnicosAuxiliarEnfermagem, "\r\n");
            //StreamReader sr = new StreamReader(ViverMais.DAO.Properties.Resources.CBOsTecnicosAuxiliarEnfermagem);
            IList<CBO> cbos = new List<CBO>();
            ICBO icbo = Factory.GetInstance<ICBO>();

            foreach(string cbo in cbosEnfermagem)
            //while ((cbo = sr.ReadLine()) != null)
                cbos.Add(icbo.BuscarPorCodigo<CBO>(cbo));

            return (IList<T>)(object)cbos;
        }
    }
}
