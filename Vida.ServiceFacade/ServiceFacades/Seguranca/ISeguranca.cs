using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace ViverMais.ServiceFacade.ServiceFacades.Seguranca
{
    public interface ISeguranca : IServiceFacade
    {
        T Login<T>(string cartaoSUS, string senha);
        
        bool VerificarPermissao(int codigoUsuario, string operacao, int co_sistema);
        bool VerificarPermissao(int codigoUsuario, string operacao);
        bool ValidarCadastroUsuario(string co_cartao, string co_unidade, int co_usuario);

        IList<T> ObterPermissoes<T>(int codigoUsuario, int codigoSistema);
        IList<T> BuscarModulosEdicaoUsuario<T,U>(U _usuario);
        IList<T> BuscarOperacaoPorModulo<T>(int co_modulo);
        
        string RetornaMenuModulo(XmlDocument xml, string urlpart, int co_usuario, int co_modulo);
        string RetornaMenuPrincipal(XmlDocument xml, string urlpart, int co_usuario);
    }
}
