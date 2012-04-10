﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.DAO.Farmacia.Medicamento
{
    public class FornecedorMedicamentoDAO : FarmaciaServiceFacadeDAO, IFornecedorMedicamento
    {
        bool IFornecedorMedicamento.ValidaCadastroFornecedor<T>(string cnpj, int co_fornecedor)
        {
            string hql = string.Empty;
            hql = "FROM ViverMais.Model.FornecedorMedicamento AS f WHERE f.Cnpj = '" + cnpj + "' AND f.Codigo <> " + co_fornecedor;
            return Session.CreateQuery(hql).List<T>().Count <= 0;
        }
    }
}
