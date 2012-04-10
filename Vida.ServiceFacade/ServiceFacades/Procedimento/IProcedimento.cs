﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.Procedimento
{
    public interface IProcedimento: IServiceFacade
    {
        IList<T> BuscarPorGrupo<T>(string co_grupo);
        IList<T> BuscarPorSubGrupo<T>(string co_subgrupo);
        IList<T> BuscarPorForma<T>(string co_forma);
        IList<T> BuscarPorOcupacao<T>(string co_ocupacao);
        IList<T> BuscarPorNome<T>(string nome);
        IList<T> BuscarPorCid<T>(string co_cid);
        T BuscarProcedimentoAPAC<T>(string co_procedimento);
        bool CBOExecutaProcedimento(string co_procedimento, string cbo);
    }
}
