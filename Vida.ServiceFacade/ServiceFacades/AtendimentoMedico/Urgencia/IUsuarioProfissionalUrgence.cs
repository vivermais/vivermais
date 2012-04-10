﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IUsuarioProfissionalUrgence : IServiceFacade
    {
        UsuarioProfissionalUrgence BuscarPorCodigoIdentificacao<UsuarioProfissionalUrgence>(string codigoidentificacao, string codigounidade);
        
        T BuscarPorCodigoViverMais<T>(int co_usuario);
        
        IList<T> BuscarPorVinculoUnidade<T>(string co_unidade);

        bool ValidaCodigoIdentificacao(string codigoidentificacao, int co_usuario, string co_unidade);
        bool VerificarAcessoMedico(int co_usuario, string permissao, string cnes);
        bool VerificarAcessoEnfermeiro(int co_usuario, string permissao, string cnes);
        bool VerificarAcessoAuxiliarTecnicoEnfermagem(int co_usuario, string permissao, string cnes);
        
        DataTable AtualizarUsuariosSenhador(string co_unidade);
        
        void CadastrarUsuarioSenhador<T>(T usuarioProfissional);
    }
}
