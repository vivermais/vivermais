﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Farmacia.Misc
{
    public interface IFarmacia : IServiceFacade
    {
        IList<T> BuscarPorEstabelecimentoSaude<T>(string codigoUnidade);
        IList<T> BuscarPorUsuario<T, U>(U _usuario, bool verificar_permissao_escolher_qualquer_farmacia, bool adicionar_farmacia_default);
        void SalvarVinculoUsuarioFarmacia<T>(IList<T> novasfarmaciasusuario, IList<T> farmaciasdispensadas, int co_usuario);
        IList<T> BuscarPorElenco<T>(int co_elenco);
        void ImportarDadosSisfarmaViverMaisProducao(int opcao);
    }
}
