using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using System.Collections;

namespace ViverMais.DAO.Seguranca
{
    public class PerfilDAO: ViverMaisServiceFacadeDAO, IPerfil
    {
        void IPerfil.Salvar<P, H>(P _perfil, IList<H> _horarios, int co_usuario)
        {
            Perfil perfil = (Perfil)(object)_perfil;
            IList<HorarioPerfil> horarios = (IList<HorarioPerfil>)(object)_horarios;
            IPerfil iPerfil = Factory.GetInstance<IPerfil>();

            using (Session.BeginTransaction())
            {
                try
                {
                    perfil = (Perfil)Session.SaveOrUpdateCopy(perfil);

                    IList<HorarioPerfil> horariosregistrados = iPerfil.BuscarHorarios<HorarioPerfil>(perfil.Codigo);

                    if (horariosregistrados.Count() == 0)
                    {
                        foreach (HorarioPerfil horario in  horarios)
                        {
                            horario.Perfil = perfil;
                            Session.SaveOrUpdate(Session.Merge(horario));
                        }
                    }
                    else
                    {
                        foreach (HorarioPerfil horario in horariosregistrados)
                        {
                            HorarioPerfil _novohorario = horarios.Where(p => p.Dia == horario.Dia).First();

                            horario.Bloqueado = _novohorario.Bloqueado;
                            horario.HoraInicial = _novohorario.HoraInicial;
                            horario.HoraFinal = _novohorario.HoraFinal;
                            Session.Update(horario);
                        }
                    }

                    Session.Save((new LogViverMais(DateTime.Now, Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Usuario>(co_usuario),13, "Codigo: " + perfil.Codigo.ToString())));
                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }

        IList<T> IPerfil.BuscarPorModulo<T>(int co_modulo) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Perfil AS p WHERE p.Modulo.Codigo = " + co_modulo;
            return Session.CreateQuery(hql).List<T>();
        }
        IList<T> IPerfil.ListarPermissoesPerfil<T>(int co_perfil) 
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Permissao AS p WHERE p.Perfil.Codigo = " + co_perfil;
            return Session.CreateQuery(hql).List<T>();
        }
        IList<T> IPerfil.BuscarHorarios<T>(int co_perfil)
        {
            return Session.CreateQuery("FROM ViverMais.Model.HorarioPerfil h WHERE h.Perfil.Codigo=" + co_perfil).List<T>();
        }

        T IPerfil.BuscarHorario<T>(int co_perfil, DayOfWeek dia)
        {
            return Session.CreateQuery("FROM ViverMais.Model.HorarioPerfil h WHERE h.Perfil.Codigo=" + co_perfil + " AND h.Dia = " + (int)dia).UniqueResult<T>();
        }
    }
}
