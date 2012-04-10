using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using NHibernate;
using NHibernate.Criterion;

namespace ViverMais.DAO.Vacina.Misc
{
    public class SalaVacinaDAO : VacinaServiceFacadeDAO, ISalaVacina
    {
        IList<T> ISalaVacina.BuscarUnidadesPorDistritoSala<T>(int co_distrito)
        {
            return Session.CreateQuery("SELECT DISTINCT e FROM ViverMais.Model.SalaVacina s, ViverMais.Model.EstabelecimentoSaude e WHERE e.Bairro.Distrito.Codigo = " + co_distrito + " AND e.CNES = s.EstabelecimentoSaude.CNES ORDER BY e.NomeFantasia").List<T>();
        }

        IList<T> ISalaVacina.BuscarPorUnidadesPesquisadas<T>(IList<T> unidades)
        {
            if (unidades.Count() > 0)
            {
                IList<string> objCNES = Session.CreateQuery("SELECT DISTINCT e.CNES FROM ViverMais.Model.SalaVacina s, ViverMais.Model.EstabelecimentoSaude e WHERE s.EstabelecimentoSaude.CNES = e.CNES").List<string>();
                return (IList<T>)(unidades.Cast<ViverMais.Model.EstabelecimentoSaude>()).Where(p => objCNES.Where(p2 => p2.ToString() == p.CNES).FirstOrDefault() != null).ToList();
            }

            return (IList<T>)unidades;
        }

        IList<T> ISalaVacina.BuscarPorUnidadeSaude<T>(string co_unidade)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.SalaVacina AS s WHERE s.EstabelecimentoSaude.CNES = '" + co_unidade + "' ORDER BY s.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISalaVacina.BuscarPorNome<T>(string nome)
        {
            //ICriteria iCriteria = Session.CreateCriteria(typeof(T));
            //return iCriteria.Add(Expression.InsensitiveLike("Nome", nome, MatchMode.Anywhere)).List<T>();
            string hql = "FROM ViverMais.Model.SalaVacina s WHERE TRANSLATE(UPPER(s.Nome),'ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç','AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc') LIKE '%" + GenericsFunctions.RemoveDiacritics(nome).ToUpper() + "%' ORDER BY s.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISalaVacina.ListarUsuariosPorSala<T, S>(S _sala)
        {
            SalaVacina sala = (SalaVacina)(object)_sala;

            string hql = string.Empty;
            hql = "SELECT usuariovacina FROM ViverMais.Model.UsuarioVacina usuariovacina ";
            hql += " WHERE usuariovacina.Sala.Codigo = " + sala.Codigo + " AND usuariovacina.Usuario.Unidade.CNES='" + sala.EstabelecimentoSaude.CNES + "'";

            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> ISalaVacina.BuscarPorUsuario<T, U>(U _usuario, bool verificar_permissao_escolher_qualquer_sala_vacina, bool adicionar_sala_default)
        {
            Usuario usuario = (Usuario)(object)_usuario;
            IList<SalaVacina> salas = new List<SalaVacina>();
            bool listar_todas_salas = false;

            if (verificar_permissao_escolher_qualquer_sala_vacina)
                listar_todas_salas = Factory.GetInstance<ISeguranca>().VerificarPermissao(usuario.Codigo, "ESCOLHER_QUALQUER_SALA_VACINA", Modulo.VACINA);

            if (listar_todas_salas)  //Todas salas de vacinas
                salas = Factory.GetInstance<ISalaVacina>().ListarTodos<ViverMais.Model.SalaVacina>().OrderBy(p => p.Nome).ToList();
            else //Somente salas de vacinas vinculadas
            {
                string hql = "SELECT DISTINCT s FROM ViverMais.Model.SalaVacina AS s, ViverMais.Model.Usuario AS u, ViverMais.Model.UsuarioVacina us ";
                hql += " WHERE u.Codigo = us.Usuario.Codigo AND us.Usuario.Codigo=" + usuario.Codigo;
                hql += " AND us.Sala.EstabelecimentoSaude.CNES='" + usuario.Unidade.CNES + "'";
                hql += " AND s.Codigo = us.Sala.Codigo";
                hql += " ORDER BY s.Nome";
                salas = Session.CreateQuery(hql).List<SalaVacina>();
            }

            if (adicionar_sala_default)
            {
                SalaVacina saladefault = new ViverMais.Model.SalaVacina();
                saladefault.Nome = "Selecione...";
                saladefault.Codigo = -1;
                salas.Insert(0, saladefault);
            }

            return (IList<T>)(IList<ViverMais.Model.SalaVacina>)salas;
        }

        IList<int> ISalaVacina.SalasPertencesCMADI()
        {
            //string hql = string.Empty;
            //hql = "SELECT s.Codigo FROM ViverMais.Model.SalaVacina s WHERE s.PertenceCMADI";
            //IList<object[]> lista = Session.CreateQuery(hql).List<object[]>();
            IList<int> salas = new List<int>();
            return salas;
        }

        void ISalaVacina.SalvarSala<T, U>(T _sala, IList<U> _usuarios, int co_usuario)
        {
            SalaVacina sala = (SalaVacina)(object)_sala;
            IList<UsuarioVacina> usuarios = (IList<UsuarioVacina>)_usuarios;

            using (Session.BeginTransaction())
            {
                try
                {
                    sala = (SalaVacina)Session.SaveOrUpdateCopy(sala);

                    #region USUÁRIOS E RESPONSÁVEIS DA SALA DE VACINA
                    IList<UsuarioVacina> atuaisusuarios = Factory.GetInstance<ISalaVacina>().ListarUsuariosPorSala<UsuarioVacina, SalaVacina>(sala);

                    IList<UsuarioVacina> excluirusuarios = (from usuarioexcluir in atuaisusuarios
                                                            where !usuarios.Select(p => p.CodigoUsuario).Contains(usuarioexcluir.CodigoUsuario)
                                                            select usuarioexcluir).ToList();

                    foreach (UsuarioVacina excluirusuario in excluirusuarios)
                        Session.Delete(excluirusuario);

                    IList<UsuarioVacina> novosusuarios = (from novousuario in usuarios
                                                          where novousuario.Codigo == 0 &&
                                                          !atuaisusuarios.Select(p => p.CodigoUsuario).Contains(novousuario.CodigoUsuario)
                                                          select novousuario).ToList();

                    foreach (UsuarioVacina novousuario in novosusuarios)
                    {
                        novousuario.Sala = sala;
                        Session.Save(novousuario);
                    }

                    IList<UsuarioVacina> usuariosatualizacao = (from atualizacaousuario in usuarios
                                                                where !novosusuarios.Select(p => p.CodigoUsuario).Contains(atualizacaousuario.CodigoUsuario)
                                                                select atualizacaousuario).ToList();

                    foreach (UsuarioVacina atualizacaousuario in usuariosatualizacao)
                    {
                        UsuarioVacina _usuario = (from usuario in atuaisusuarios
                                                  where usuario.CodigoUsuario == atualizacaousuario.CodigoUsuario
                                                  select usuario).First();

                        _usuario.Responsavel = atualizacaousuario.Responsavel;
                        Session.Update(Session.Merge(_usuario));
                    }
                    #endregion

                    Session.Save(new LogVacina(DateTime.Now, co_usuario, 5, "id sala: " + sala.Codigo.ToString()));

                    Session.Transaction.Commit();
                }
                catch (Exception ex)
                {
                    Session.Transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
