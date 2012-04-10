using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.Model;
using ViverMais.DAOOracle;

namespace ViverMais.BLL
{
    public static class UsuarioBLL
    {
        public static List<Usuario> ListarPorUnidade(EstabelecimentoSaude unidade)
        {
            UsuarioDAO dao = new UsuarioDAO();
            List<Usuario> usuarios = dao.PesquisarPorUnidade(unidade.CNES);
            usuarios = usuarios.OrderBy(x => x.Nome).ToList();
            return usuarios;
        }
    }
}
