﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;

namespace ViverMais.View.Farmacia
{
    public partial class WUC_PesquisarMedicamento : System.Web.UI.UserControl
    {
        public Label WUC_Label_ChamadaPesquisa
        {
            get { return this.Label_ChamadaPesquisa; }
        }

        public RequiredFieldValidator WUC_RequiredFieldValidatorNomeMedicamento
        {
            get { return this.RequiredFieldValidator_NomeMedicamento; }
        }

        public RegularExpressionValidator WUC_RegularExpressionValidatorNomeMedicamento
        {
            get { return this.RegularExpressionValidator_NomeMedicamento; }
        }

        public IList<Medicamento> Medicamentos
        {
            get
            {
                if (Session[string.Format("MedicamentosPesquisados_{0}", this.UniqueID)] != null)
                    return (IList<Medicamento>)Session[string.Format("MedicamentosPesquisados_{0}", this.UniqueID)];

                return new List<Medicamento>();
            }

            set
            {
                Session[string.Format("MedicamentosPesquisados_{0}", this.UniqueID)] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public LinkButton WUC_LinkButtonPesquisarPorNome
        {
            get
            {
                return this.LinkButtonPesquisar;
            }
        }

        protected void OnClick_BuscarPorNome(object sender, EventArgs e)
        {
            this.Medicamentos = Factory.GetInstance<IMedicamento>().BuscarPorDescricao<Medicamento>(string.Empty, TextBoxMedicamento.Text);
        }
    }
}