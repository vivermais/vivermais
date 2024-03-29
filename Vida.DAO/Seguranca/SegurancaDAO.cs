﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;
using NHibernate;
using NHibernate.Criterion;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades;
using System.Xml;
using System.Collections;
using ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia;
using ViverMais.ServiceFacade.ServiceFacades.Procedimento.Misc;

namespace ViverMais.DAO.Seguranca
{
    public class SegurancaDAO : ViverMaisServiceFacadeDAO, ISeguranca
    {
        public SegurancaDAO() //: base("ViverMais")
        {
        }

        //~SegurancaDAO()
        //{
        //    this.Session.Disconnect();
        //}

        #region ISeguranca Members

        T ISeguranca.Login<T>(string cartaoSUS, string senha)
        {
            ICriteria criteria = this.Session.CreateCriteria(typeof(ViverMais.Model.Usuario));
            criteria.Add(Expression.And(Expression.Eq("CartaoSUS", cartaoSUS), Expression.Eq("Senha", senha))).Add(Expression.Eq("Ativo", 1));
            T result = (T)(object)criteria.UniqueResult<ViverMais.Model.Usuario>();
            return result;
        }

        bool ISeguranca.VerificarPermissao(int codigoUsuario, string operacao, int co_sistema)
        {
            string hql =
                "select count(permissao.Operacao.Codigo) from ViverMais.Model.Permissao permissao, ViverMais.Model.Usuario usuario " +
                " where usuario.Codigo='" + codigoUsuario + "' and permissao.Perfil in elements(usuario.Perfis) " +
                " and permissao.Operacao.Nome='" + operacao + "' and permissao.Operacao.Modulo.Codigo=" + co_sistema + " GROUP BY permissao.Operacao.Codigo";
            object o = Session.CreateQuery(hql).UniqueResult<object>();

            if (o != null && int.Parse(o.ToString()) > 0)
                return true;

            return false;
        }
        bool ISeguranca.VerificarPermissao(int codigoUsuario, string operacao)
        {
            string hql =
                "select permissao from ViverMais.Model.Permissao permissao, ViverMais.Model.Usuario usuario " +
                " where usuario.Codigo='" + codigoUsuario + "' and permissao.Perfil in elements(usuario.Perfis) " +
                " and permissao.Operacao.Nome='" + operacao + "'";
            return Session.CreateQuery(hql).List<ViverMais.Model.Permissao>().Count > 0;
        }
        bool ISeguranca.ValidarCadastroUsuario(string co_cartao, string co_unidade, int co_usuario)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Usuario u WHERE u.Codigo <> " + co_usuario;
            hql += " AND u.Unidade.CNES='" + co_unidade + "'";
            hql += " AND u.CartaoSUS='" + co_cartao + "'";
            return Session.CreateQuery(hql).List<Usuario>().Count() > 0 ? false : true;
        }

        IList<T> ISeguranca.ObterPermissoes<T>(int codigoUsuario, int codigoSistema)
        {
            throw new NotImplementedException();
        }
        IList<T> ISeguranca.BuscarModulosEdicaoUsuario<T, U>(U _usuario)
        {
            Usuario usuario = (Usuario)(object)_usuario;
            ISeguranca iseguranca = Factory.GetInstance<ISeguranca>();
            IList<Modulo> modulos = iseguranca.ListarTodos<Modulo>();
            IList<Modulo> modulosusuarios = new List<Modulo>();

            foreach (var modulo in modulos)
            {
                if (iseguranca.VerificarPermissao(usuario.Codigo, "EDITAR_USUARIO_" + modulo.Nome, Modulo.SEGURANCA))
                    modulosusuarios.Add(modulo);
            }

            return (IList<T>)(object)modulosusuarios;
        }
        IList<T> ISeguranca.BuscarOperacaoPorModulo<T>(int co_modulo)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.Operacao o WHERE o.Modulo.Codigo=" + co_modulo;
            hql += " ORDER BY o.Nome";
            return Session.CreateQuery(hql).List<T>();
        }

        string ISeguranca.RetornaMenuModulo(XmlDocument xml, string urlpart, int co_usuario, int co_modulo)
        {
            XmlElement principal = xml.DocumentElement;

            IList<XmlNode> retirarNodes = new List<XmlNode>();
            IList<XmlNode> substituirNodes = new List<XmlNode>();

            this.LeXML(principal, co_modulo, co_usuario, "/" + principal.Name, retirarNodes, substituirNodes);

            foreach (XmlNode node in retirarNodes.Cast<XmlNode>())
            {
                if (node != null)
                {
                    node.RemoveAll();
                    node.ParentNode.RemoveChild(node);
                }
            }

            foreach (XmlNode noAntigo in substituirNodes.Cast<XmlNode>())
            {
                if (noAntigo != null)
                {
                    XmlElement element = (XmlElement)noAntigo.FirstChild;
                    XmlElement no = xml.CreateElement(element.Name);
                    no.InnerXml = element.InnerXml;

                    if (noAntigo.ParentNode != null)
                        noAntigo.ParentNode.ReplaceChild(no, noAntigo);
                }
            }

            return principal.InnerXml.Replace("URLPART/", urlpart);
        }
        string ISeguranca.RetornaMenuPrincipal(XmlDocument xml, string urlpart, int co_usuario)
        {
            int tamanho_div = 75, tamanho_max_div = 678, valor_div = 0;
            XmlElement principal = xml.DocumentElement;
            XmlNodeList nodes = principal.SelectNodes("/Menu/div/div/ul/Modulo");
            StringBuilder builder = new StringBuilder();
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["Codigo"] != null)
                {
                    if (iSeguranca.VerificarPermissao(co_usuario, node.Attributes["Permissao"].Value, int.Parse(node.Attributes["Codigo"].Value)))
                    {
                        builder.Append(node.InnerXml);
                        valor_div += tamanho_div;
                    }
                }
                else
                {
                    builder.Append(node.InnerXml);
                    valor_div += tamanho_div;
                }
            }

            if (valor_div > tamanho_max_div)
                valor_div = tamanho_max_div;

            principal.SelectSingleNode("/Menu/div/div/ul").InnerXml = builder.ToString();
            return principal.InnerXml.Replace("TAMANHODIV", valor_div.ToString()).Replace("URLPART/", urlpart);
        }

        private void LeXML(XmlNode node, int co_modulo, int co_usuario, string parent, IList<XmlNode> retirarNodes, IList<XmlNode> substituirNodes)
        {
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();

            foreach (XmlNode no in node.ChildNodes)
                this.LeXML(no, co_modulo, co_usuario, parent + "/" + no.Name, retirarNodes, substituirNodes);

            if (node.NodeType == XmlNodeType.Element)
            {
                if (node.Name.Equals("li"))
                {
                    XmlAttribute atributo = ((XmlElement)node).GetAttributeNode("Permissao");
                    if (atributo != null)
                    {
                        if (!iSeguranca.VerificarPermissao(co_usuario, atributo.Value, co_modulo))
                            retirarNodes.Add(node);
                        else
                            node.Attributes.Remove(atributo);
                    }
                    else
                    {
                        if (node.HasChildNodes)
                        {
                            IList<XmlNode> filhos = node.ChildNodes.Cast<XmlNode>().Where(p => p.Name == "ul")
                            .SelectMany(l => l.ChildNodes.Cast<XmlNode>().ToList()
                            , (l, x) => new { x }).Select(z => z.x).ToList();

                            int count = 0;

                            foreach(XmlNode no in filhos)
                            {
                                if (retirarNodes.Contains(no))
                                    count++;
                            }
                            //IList<XmlNode> filhosPermissao = filhos.Where(z => z.Attributes["Permissao"]
                            //    != null && z.Name.Equals("li")).ToList();

                            //int count = 0;
                            //foreach (XmlNode no in filhosPermissao)
                            //{
                            //    if (retirarNodes.Where(p => p.Attributes["Permissao"] != null &&
                            //        p.Attributes["Permissao"].Value
                            //        == no.Attributes["Permissao"].Value).FirstOrDefault() != null)
                            //        count++;
                            //}

                            //filhosPermissao = filhos.Where(z =>z.Name.Equals("PermissaoEspecial") &&
                            //    z.Attributes["Nome"] != null).ToList();

                            //foreach (XmlNode no in filhosPermissao)
                            //{
                            //    if (retirarNodes.Where(p => p.Name.Equals("PermissaoEspecial") &&
                            //        p.Attributes["Nome"] != null &&
                            //        p.Attributes["Nome"].Value
                            //        == no.Attributes["Nome"].Value).FirstOrDefault() != null)
                            //        count++;
                            //}

                            //IList<XmlNode> filhos = node.ChildNodes.Cast<XmlNode>().Where(p => p.Name == "ul")
                            //.SelectMany(l => l.ChildNodes.Cast<XmlNode>().ToList()
                            //, (l, x) => new { x }).Select(z => z.x).ToList();

                            //IList<XmlNode> filhosPermissao = filhos.Where(z => z.Attributes["Permissao"]
                            //!= null && z.Name.Equals("li")).ToList();

                            //int count = 0;
                            //foreach (XmlNode no in filhosPermissao)
                            //{
                            //    if (retirarNodes.Where(p => p.Attributes["Permissao"] != null &&
                            //        p.Attributes["Permissao"].Value
                            //        == no.Attributes["Permissao"].Value).FirstOrDefault() != null)
                            //        count++;
                            //}

                            if (count != 0 && count == filhos.Count())
                                retirarNodes.Add(node); //Retirar nó pai
                        }
                    }
                }
                else
                {
                    if (node.Name.Equals("PermissaoEspecial"))
                    {
                        bool substituir = false;

                        if (co_modulo == Modulo.VACINA)
                            substituir = this.VerificarAcessoPermissaoVacina(co_usuario, ((XmlElement)node).GetAttribute("Nome"));
                        else
                        {
                            if (co_modulo == Modulo.SEGURANCA)
                                substituir = this.VerificarAcessoPermissaoSeguranca(co_usuario, ((XmlElement)node).GetAttribute("Nome"));
                            else
                                if (co_modulo == Modulo.URGENCIA)
                                    substituir = this.VerificaAcessoPermissaoUrgencia(co_usuario, node);
                        }

                        if (substituir)
                            substituirNodes.Add(node);
                        else
                            retirarNodes.Add(node);
                    }
                }
            }
        }

        private bool VerificaAcessoPermissaoUrgencia(int co_usuario, XmlNode node)
        {
            string permissao = ((XmlElement)node).GetAttribute("Nome");
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
            bool valor = false;

            switch (permissao)
            {
                case "RELATORIOS":
                    if (iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_SITUACAO", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_ATENDIMENTO_CID", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_EVASAO", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_ATENDIMENTO_FAIXA_ETARIA", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_LEITOS_FAIXA_ETARIA", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_TEMPO_PERMANENCIA", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_PROCEDIMENTO", Modulo.URGENCIA) ||
                        iSeguranca.VerificarPermissao(co_usuario, "GERAR_RELATORIO_PROCEDENCIA", Modulo.URGENCIA))
                        valor = true;
                    break;
                case "SENHADOR":
                    Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(co_usuario);
                    IUsuarioProfissionalUrgence iUsuarioProfissional = Factory.GetInstance<IUsuarioProfissionalUrgence>();
                    UsuarioProfissionalUrgence usuarioProfissional = iUsuarioProfissional.BuscarPorCodigo<UsuarioProfissionalUrgence>(usuario.Codigo);

                    if (usuarioProfissional != null)
                    {
                        ICBO iCbo = Factory.GetInstance<ICBO>();
                        CBO cbo = iCbo.BuscarPorCodigo<CBO>(usuarioProfissional.CodigoCBO);

                        if (iCbo.CBOPertenceEnfermeiro<CBO>(cbo) || iCbo.CBOPertenceMedico<CBO>(cbo))
                        {
                            SenhadorUrgence senhador = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<SenhadorUrgence>().Where(p => p.CNESUnidade == usuario.Unidade.CNES).FirstOrDefault();

                            if (senhador != null)
                            {
                                string linknode = node.FirstChild.InnerXml.Replace("LINKSENHADOR", senhador.EnderecoLoginSenhador.Replace("USUARIO", usuario.CartaoSUS).Replace("SENHA", usuario.CartaoSUS));
                                node.FirstChild.InnerXml = linknode;
                                valor = true;
                            }
                        }
                    }
                    break;
            }

            return valor;
        }

        /// <summary>
        /// Permissão de acordo com o menu XML
        /// </summary>
        /// <param name="co_usuario"></param>
        /// <param name="permissao">permissão escrita como atributo no xml</param>
        /// <returns></returns>
        private bool VerificarAcessoPermissaoVacina(int co_usuario, string permissao)
        {
            ISeguranca iSeguranca = Factory.GetInstance<ISeguranca>();
            bool valor = false;

            switch (permissao)
            {
                case "INVENTARIO":
                    if (iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_INVENTARIO_VACINA", Modulo.VACINA)
               || iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_ABERTURA_ENCERRAMENTO_INVENTARIO_VACINA", Modulo.VACINA))
                        valor = true;
                    break;
                case "NOVAMOVIMENTACAO":
                    bool permissao_doacao = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_DOACAO", Modulo.VACINA);
                    bool permissao_devolucao = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_DEVOLUCAO", Modulo.VACINA);
                    bool permissao_emprestimo = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_EMPRESTIMO", Modulo.VACINA);
                    bool permissao_perda = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_PERDA", Modulo.VACINA);
                    bool permissao_remanejamento = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_REMANEJAMENTO", Modulo.VACINA);
                    bool permissao_acerto_balanco = iSeguranca.VerificarPermissao(co_usuario, "REALIZAR_MOVIMENTACAO_ACERTO_BALANCO", Modulo.VACINA);

                    if (permissao_doacao || permissao_devolucao ||
                         permissao_emprestimo || permissao_perda ||
                         permissao_remanejamento ||
                         permissao_acerto_balanco
                        )
                        valor = true;
                    break;
                case "RELATORIOS":
                        valor = iSeguranca.VerificarPermissao(co_usuario, "RELATORIO_PRODUCAO_DIARIA_VACINA", Modulo.VACINA);
                    break;
            }

            return valor;
        }

        /// <summary>
        /// Permissão de acordo com o menu XML
        /// </summary>
        /// <param name="co_usuario"></param>
        /// <param name="permissao">permissão escrita como atributo no xml</param>
        /// <returns></returns>
        private bool VerificarAcessoPermissaoSeguranca(int co_usuario, string permissao)
        {
            bool valor = false;
            Usuario usuario = Factory.GetInstance<IUsuario>().BuscarPorCodigo<Usuario>(co_usuario);

            switch (permissao)
            {
                case "LISTARUSUARIOS":
                    IList<Modulo> modulosedicao = Factory.GetInstance<ISeguranca>().BuscarModulosEdicaoUsuario<Modulo, Usuario>(usuario);
                    if (modulosedicao.Count() > 0)
                        valor = true;
                    break;
            }

            return valor;
        }

        #endregion

    }
}
