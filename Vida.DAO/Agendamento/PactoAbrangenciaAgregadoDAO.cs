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


namespace ViverMais.DAO.Agendamento
{
    public class PactoAbrangenciaAgregadoDAO : ViverMaisServiceFacadeDAO, IPactoAbrangenciaAgregado
    {
        #region IPactoAbrangenciaAgregado Members

        public PactoAbrangenciaAgregadoDAO()
        {
            
        }

        /// <summary>
        /// Retorna uma Lista de ProcedimentosAgregados que Possuem o Agregado Informado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id_agregado"></param>
        /// <returns></returns>
        IList<T> IPactoAbrangenciaAgregado.BuscaPorAgregado<T>(string id_agregado)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAbrangenciaAgregado pa";
            hql += " where pa.Agregado.Codigo="+ id_agregado;
            return Session.CreateQuery(hql).List<T>();
        }

        T IPactoAbrangenciaAgregado.BuscarPorPactoAbrangenciaAgregado<T>(string id_agregado, string id_pactoabrangencia)
        {
            string hql = String.Empty;
            hql += "from ViverMais.Model.PactoAbrangenciaAgregado pa";
            hql += " where pa.Agregado.Codigo=" + id_agregado;
            hql += " and pa.PactoAbrangencia.Codigo=" + id_pactoabrangencia;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IPactoAbrangenciaAgregado.BuscaPorPactoAbrangencia<T>(int id_pactoabrangencia)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.PactoAbrangenciaAgregado pa";
            hql += " where pa.PactoAbrangencia.Codigo=" + id_pactoabrangencia;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<T> IPactoAbrangenciaAgregado.ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<T>(int id_pactoabrangencia)
        {
            string hql = string.Empty;
            hql += "from ViverMais.Model.PactoAbrangenciaGrupoMunicipio pa";
            hql += " where pa.PactoAbrangenciaAgregado.Codigo=" + id_pactoabrangencia;
            return Session.CreateQuery(hql).List<T>();
        }

        IList<object> IPactoAbrangenciaAgregado.ListarTodosPactoAbrangenciaGrupoMunicipio()
        {
            string hql = "Select pa.PactoAbrangenciaAgregado.Codigo, sum(pa.ValorUtilizado) from ViverMais.Model.PactoAbrangenciaGrupoMunicipio pa";
            hql += " where pa.PactoAbrangenciaAgregado.Ativo = 1";
            hql += " group by pa.PactoAbrangenciaAgregado.Codigo";
            return Session.CreateQuery(hql).List<object>();
        }

        
        bool IPactoAbrangenciaAgregado.RestricaoPactoAbrangencia<M,P>(M municipioParameter, P procedimentoSelecionadoParameter)
        {
            //Retorna os Grupos de Abrangencia que o Municipio Faz Parte
            Municipio municipio = (Municipio)(object)municipioParameter;
            ViverMais.Model.Procedimento procedimentoSelecionado = (ViverMais.Model.Procedimento)(object)procedimentoSelecionadoParameter;
            IList<GrupoAbrangencia> grupos = Factory.GetInstance<IGrupoAbrangencia>().ListarGrupoPorMunicipio<GrupoAbrangencia>(municipio.Codigo);

            //Percorre Todos Os Grupos
            foreach (GrupoAbrangencia grupo in grupos)
            {
                //Lista Os PactosAbrangencias desse Grupo
                IList<PactoAbrangencia> pactosAbrangencia = Factory.GetInstance<IPactoAbrangencia>().BuscarPactoAbrangenciaPorGrupoAbrangencia<PactoAbrangencia>(grupo.Codigo);

                foreach (PactoAbrangencia pactoAbrangencia in pactosAbrangencia)
                {
                    PactoAbrangenciaAgregado pactoAbrangenciaAgregado = null;
                    if (pactoAbrangencia.PactoAbrangenciaAgregado.Count != 0)
                    {
                        ////Busca os PactosAgregadosProcedimentosCBO primeiramente pelo CBO selecionado
                        //if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Cbo != null
                        //    && p.Cbo.Codigo == rbtnEspecialidade.SelectedValue
                        //    && p.Procedimento != null
                        //    && p.Procedimento.Codigo == procedimentoSelecionado.Codigo
                        //    && p.Ativo == true
                        //    && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.CBO)).ToList().Count == 0)
                        //{
                        ////Irei Buscar Se existe alguma Pacto do Tipo PROCEDIMENTO Para o Procedimento Selecionado
                        //if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Procedimento != null
                        //    && p.Procedimento.Codigo == procedimentoSelecionado.Codigo
                        //    && p.Ativo == true
                        //    && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.PROCEDIMENTO)).ToList().Count == 0)
                        //{
                        //Irei Buscar agora Pelo Agregado que tem o Procedimento Selecionado
                        ViverMais.Model.Agregado agregado = Factory.GetInstance<IProcedimentoAgregado>().BuscaAgregadoPorProcedimento<ViverMais.Model.Agregado>(procedimentoSelecionado.Codigo);
                        if (agregado != null)
                        {
                            if (pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo
                                && p.Ativo == true
                                && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().Count != 0)
                            {
                                pactoAbrangenciaAgregado = pactoAbrangencia.PactoAbrangenciaAgregado.Where(p => p.Agregado.Codigo == agregado.Codigo && p.Ativo == true && p.TipoPacto == Convert.ToChar(PactoAbrangenciaAgregado.TipoDePacto.AGREGADO)).ToList().FirstOrDefault();

                                PactoAbrangenciaGrupoMunicipio pactoAbrangenciaGrupoMunicipio = Factory.GetInstance<IPactoAbrangenciaAgregado>().ListarPactoAbrangenciaGrupoMunicipioPorPactoAbrangenciaAgregado<PactoAbrangenciaGrupoMunicipio>(pactoAbrangenciaAgregado.Codigo).Where(p => p.Municipio.Codigo == municipio.Codigo).FirstOrDefault();
                                Decimal valorProcedimento = Decimal.Parse(procedimentoSelecionado.VL_SA.ToString().Insert(procedimentoSelecionado.VL_SA.ToString().Length - 2, ","));

                                if (pactoAbrangenciaAgregado.ValorUtilizado + valorProcedimento <= pactoAbrangenciaAgregado.ValorPactuado)
                                {
                                    System.Web.HttpContext.Current.Session["PactoAbrangenciaGrupoMunicipio"] = pactoAbrangenciaGrupoMunicipio;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }


        #endregion

        #region IServiceFacade Members

        //public T BuscarPorCodigo<T>(object codigo) where T : new()
        //{
        //    throw new NotImplementedException();
        //}

        //public void Atualizar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Inserir(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Salvar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Deletar(object obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public IList<T> ListarTodos<T>()
        //{
        //    throw new NotImplementedException();
        //}

        #endregion
    }
}
