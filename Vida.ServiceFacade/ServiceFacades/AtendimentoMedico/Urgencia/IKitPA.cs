﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViverMais.ServiceFacade.ServiceFacades.AtendimentoMedico.Urgencia
{
    public interface IKitPA : IServiceFacade
    {
        IList<T> BuscarItemPA<T>(int co_kit);
        IList<T> BuscarMedicamentoPA<T>(int co_kit);

        void SalvarKit<K, II, MI>(K _kit, IList<II> _itens, IList<MI> _medicamentos);

        T BuscarKitPorMedicamentoPrincipal<T>(int co_medicamento);
        
        bool ValidaCadastroKit(int co_kit, int co_medicamentoprincipal);
    }
}
