using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.EstabelecimentoSaude;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;
using System.Collections;
using System.Data;

namespace ViverMais.DAO.AtendimentoMedico.Urgencia
{
    public class UsuarioProfissionalUrgenceDAO : UrgenciaServiceFacadeDAO, IUsuarioProfissionalUrgence
    {
        #region IUsuarioProfissionalUrgence

        bool IUsuarioProfissionalUrgence.ValidaCodigoIdentificacao(string codigoidentificacao, int co_usuario, string co_unidade)
        {
            string hql = "FROM ViverMais.Model.UsuarioProfissionalUrgence u WHERE u.Identificacao = '" + codigoidentificacao + "' AND u.UnidadeVinculo='" + co_unidade + "'";
            hql += " AND u.Id_Usuario <> " + co_usuario;
            return Session.CreateQuery(hql).List<ViverMais.Model.UsuarioProfissionalUrgence>().Count > 0 ? false : true;
            //if (Factory.GetInstance<IUrgenciaServiceFacade>().ListarTodos<ViverMais.Model.UsuarioProfissionalUrgence>().Where(p => p.Identificacao == codigoidentificacao && p.Id_Usuario != co_usuario && Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<ViverMais.Model.Usuario>(co_usuario).Unidade.CNES == co_unidade).FirstOrDefault() != null)
            //return false;

            //return true;
        }

        T IUsuarioProfissionalUrgence.BuscarPorCodigoIdentificacao<T>(string codigoidentificacao, string codigounidade)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.UsuarioProfissionalUrgence AS usuario ";
            hql += "WHERE usuario.Identificacao = '" + codigoidentificacao + "' AND usuario.UnidadeVinculo='" + codigounidade + "'";
            //busca o usuário com o codigo de iudentificacao fornecido
            UsuarioProfissionalUrgence usuarioprofissional = Session.CreateQuery(hql).UniqueResult<UsuarioProfissionalUrgence>();
            if (usuarioprofissional == null)
                return default(T);

            Usuario usuario = Factory.GetInstance<IViverMaisServiceFacade>().BuscarPorCodigo<Usuario>(((UsuarioProfissionalUrgence)usuarioprofissional).Id_Usuario);
            if (usuario == null)
                return default(T);
            else
                if (usuario.Unidade.CNES == codigounidade)
                    return (T)(object)usuarioprofissional;
                else
                    return default(T);
        }

        T IUsuarioProfissionalUrgence.BuscarPorCodigoViverMais<T>(int co_usuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.UsuarioProfissionalUrgence AS up WHERE up.Id_Usuario = " + co_usuario;
            return Session.CreateQuery(hql).UniqueResult<T>();
        }

        IList<T> IUsuarioProfissionalUrgence.BuscarPorVinculoUnidade<T>(string co_unidade)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.UsuarioProfissionalUrgence up WHERE up.UnidadeVinculo='" + co_unidade + "'";
            return Session.CreateQuery(hql).List<T>();
        }

        bool IUsuarioProfissionalUrgence.VerificarAcessoMedico(int co_usuario, string permissao, string cnes)
        {
            bool acesso = false;

            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUsuarioProfissional.BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(co_usuario);

            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(co_usuario, permissao, Modulo.URGENCIA))
            {
                if (usuarioprofissional != null &&
                    usuarioprofissional.UnidadeVinculo == cnes)
                {
                    ICBO iCbo = Factory.GetInstance<ICBO>();
                    CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);

                    if (iCbo.CBOPertenceMedico<CBO>(cbo))
                        acesso = true;
                }
            }

            return acesso;
        }

        bool IUsuarioProfissionalUrgence.VerificarAcessoEnfermeiro(int co_usuario, string permissao, string cnes)
        {
            bool acesso = false;

            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUsuarioProfissional.BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(co_usuario);

            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(co_usuario, permissao, Modulo.URGENCIA))
            {
                if (usuarioprofissional != null &&
                    usuarioprofissional.UnidadeVinculo == cnes)
                {
                    ICBO iCbo = Factory.GetInstance<ICBO>();
                    CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);
                    if (iCbo.CBOPertenceEnfermeiro<CBO>(cbo))
                        acesso = true;
                }
            }

            return acesso;
        }

        bool IUsuarioProfissionalUrgence.VerificarAcessoAuxiliarTecnicoEnfermagem(int co_usuario, string permissao, string cnes)
        {
            bool acesso = false;

            IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
            ViverMais.Model.UsuarioProfissionalUrgence usuarioprofissional = iUsuarioProfissional.BuscarPorCodigoViverMais<ViverMais.Model.UsuarioProfissionalUrgence>(co_usuario);

            if (Factory.GetInstance<ISeguranca>().VerificarPermissao(co_usuario, permissao, Modulo.URGENCIA))
            {
                if (usuarioprofissional != null &&
                    usuarioprofissional.UnidadeVinculo == cnes)
                {
                    ICBO iCbo = Factory.GetInstance<ICBO>();
                    CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioprofissional.CodigoCBO);
                    if (iCbo.CBOPertenceAuxiliarTecnicoEnfermagem<CBO>(cbo))
                        acesso = true;
                }
            }

            return acesso;
        }

        DataTable IUsuarioProfissionalUrgence.AtualizarUsuariosSenhador(string co_unidade)
        {
            DataTable resultado = new DataTable();
            resultado.Columns.Add("Nome", typeof(string));
            resultado.Columns.Add("CartaoSUS", typeof(string));
            resultado.Columns.Add("Especialidade", typeof(string));

            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            SenhadorUrgence senhador = iViverMais.ListarTodos<SenhadorUrgence>().Where(p => p.CNESUnidade == co_unidade).FirstOrDefault();

            if (senhador != null)
            {
                WebServiceDinamico _webService  = null;

                try
                {
                    _webService = new WebServiceDinamico(senhador.EnderecoWebService, SenhadorUrgence.NomeServico);
                }
                catch
                {
                    throw;
                }

                IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                UsuarioProfissionalUrgence[] usuariosProfissionais = iUsuarioProfissional.BuscarPorVinculoUnidade<UsuarioProfissionalUrgence>(co_unidade).ToArray();
                ICBO iCbo = Factory.GetInstance<ICBO>();
                string tipoUsuarioSenhador = string.Empty;
                CBO cbo = null;
                Usuario usuario = null;

                foreach (UsuarioProfissionalUrgence usuarioProfissional in usuariosProfissionais)
                {
                    tipoUsuarioSenhador = string.Empty;
                    cbo = iCbo.BuscarPorCodigo<CBO>(usuarioProfissional.CodigoCBO);

                    if (iCbo.CBOPertenceMedico<CBO>(cbo))
                        tipoUsuarioSenhador = "M";
                    else if (iCbo.CBOPertenceEnfermeiro<CBO>(cbo))
                        tipoUsuarioSenhador = "E";

                    if (tipoUsuarioSenhador != string.Empty)
                    {
                        usuario = iViverMais.BuscarPorCodigo<Usuario>(usuarioProfissional.Id_Usuario);

                        try
                        {
                            _webService.ExecutarMetodoObj("SmsCadastrarUsuario", new string[4] { usuario.Codigo.ToString(), usuario.Nome, usuario.CartaoSUS, tipoUsuarioSenhador });
                        }
                        catch
                        {
                            DataRow row = resultado.NewRow();
                            row["Nome"] = usuario.Nome;
                            row["CartaoSUS"] = usuario.CartaoSUS;
                            row["Especialidade"] = cbo.Nome;
                            resultado.Rows.Add(row);
                        }
                    }
                }
            }else
                throw new Exception("Não existe endereço cadastrado para o Web Service do atual estabelecimento de saúde.");

            if (resultado.Rows.Count == 0)
                return resultado;

            return resultado.Select("","Nome ASC").CopyToDataTable();
        }

        void IUsuarioProfissionalUrgence.CadastrarUsuarioSenhador<T>(T _usuarioProfissional)
        {
            UsuarioProfissionalUrgence usuarioProfissional = (UsuarioProfissionalUrgence)(object)_usuarioProfissional;
            IViverMaisServiceFacade iViverMais = Factory.GetInstance<IViverMaisServiceFacade>();
            SenhadorUrgence senhador = iViverMais.ListarTodos<SenhadorUrgence>().Where(p => p.CNESUnidade == usuarioProfissional.UnidadeVinculo).FirstOrDefault();

            if (senhador != null)
            {
                WebServiceDinamico _webService = null;

                try
                {
                    _webService = new WebServiceDinamico(senhador.EnderecoWebService, SenhadorUrgence.NomeServico);
                }
                catch
                {
                    throw;
                }

                ICBO iCbo = Factory.GetInstance<ICBO>();

                string tipoUsuarioSenhador = string.Empty;
                CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioProfissional.CodigoCBO);

                if (iCbo.CBOPertenceMedico<CBO>(cbo))
                    tipoUsuarioSenhador = "M";
                else if (iCbo.CBOPertenceEnfermeiro<CBO>(cbo))
                    tipoUsuarioSenhador = "E";

                if (tipoUsuarioSenhador != string.Empty)
                {
                    Usuario usuario = iViverMais.BuscarPorCodigo<Usuario>(usuarioProfissional.Id_Usuario);

                    try
                    {
                        _webService.ExecutarMetodoObj("SmsCadastrarUsuario", new string[4] { usuario.Codigo.ToString(), usuario.Nome, usuario.CartaoSUS, tipoUsuarioSenhador });
                    }
                    catch
                    {
                    }
                }
            }
        }

        #endregion
    }
}
