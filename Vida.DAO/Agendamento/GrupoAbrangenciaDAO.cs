using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Agendamento;

namespace ViverMais.DAO.Agendamento
{
    public class GrupoAbrangenciaDAO : ViverMaisServiceFacadeDAO, IGrupoAbrangencia
    {
        public GrupoAbrangenciaDAO()
        {

        }

        IList<T> IGrupoAbrangencia.ListarMunicipiosPorGrupoAbrangencia<T>(string codigoGrupo)
        {
            string hql = "Select grupo.Municipios from ViverMais.Model.GrupoAbrangencia grupo where grupo.Codigo='" + codigoGrupo + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IGrupoAbrangencia.ListarGruposAtivos<T>()
        {
            string hql = "from ViverMais.Model.GrupoAbrangencia grupo where grupo.Ativo = 1";
            hql += " order by grupo.NomeGrupo";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IGrupoAbrangencia.ListarGrupoPorMunicipio<T>(string codigoMunicipio)
        { 
            string hql = "select grupo from ViverMais.Model.GrupoAbrangencia grupo,ViverMais.Model.Municipio municipio ";
            hql += " where grupo.Ativo = 1 and municipio.Codigo = '"+codigoMunicipio+ "' and municipio in elements(grupo.Municipios)";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IGrupoAbrangencia.ListarGruposInativos<T>()
        {
            string hql = "from ViverMais.Model.GrupoAbrangencia grupo where grupo.Ativo = 0";
            hql += " order by grupo.NomeGrupo";
            return Session.CreateQuery(hql).List<T>();
        }

        T IGrupoAbrangencia.BuscarGrupoPorNome<T>(string nome)
        {
            //string hql = "from ViverMais.Model.GrupoAbrangencia grupo where";
            //hql += " grupo.NomeGrupo='" + nome + "'";
            //return Session.CreateQuery(hql).UniqueResult<T>();

            string hql = "from ViverMais.Model.GrupoAbrangencia grupo where grupo.Ativo = 1";
            hql += " AND TRANSLATE(UPPER(grupo.NomeGrupo),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            hql += " LIKE ";
            hql += " TRANSLATE(UPPER('" + nome + "'),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc')";
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        void IGrupoAbrangencia.AddMunicipioAoGrupo(string id_grupo, string id_municipio)
        {
            string hql = "INSERT INTO AGD_GRUPOABRANG_MUNICIPIO VALUES ('"+id_grupo+"','"+id_municipio+"')";
            Session.CreateSQLQuery(hql).ExecuteUpdate();
        }

        void IGrupoAbrangencia.DeletarMunicipioDoGrupo(string id_grupo, string id_municipio)
        {
            string hql = "DELETE FROM AGD_GRUPOABRANG_MUNICIPIO WHERE CODIGO_GRUPO='" + id_grupo + "' AND CO_MUNICIPIO='" + id_municipio + "'";
            Session.CreateSQLQuery(hql).ExecuteUpdate();
        }

        void IGrupoAbrangencia.DeletarGrupoAbrangencia(string id_grupo)
        {
            string hql = "DELETE FROM AGD_GRUPOABRANG_MUNICIPIO WHERE CODIGO_GRUPO='" + id_grupo + "'";
            Session.CreateSQLQuery(hql).ExecuteUpdate();
            hql = "DELETE FROM AGD_GRUPO_ABRANGENCIA WHERE CODIGO_GRUPO ='" + id_grupo + "'";
            Session.CreateSQLQuery(hql).ExecuteUpdate();
        }
    }
}
