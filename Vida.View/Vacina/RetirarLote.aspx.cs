﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.Model;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades.Vacina.Misc;
using ViverMais.ServiceFacade.ServiceFacades;
using System.IO;

namespace ViverMais.View.Vacina
{
    public partial class RetirarLote : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownList_Vacina.DataSource = Factory.GetInstance<IVacina>().ListarTodos<ViverMais.Model.Vacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Vacina.DataBind();
                DropDownList_Vacina.Items.Insert(0, new ListItem("Selecione...", "-1"));

                DropDownList_Fabricante.DataSource = Factory.GetInstance<IVacinaServiceFacade>().ListarTodos<FabricanteVacina>().OrderBy(p => p.Nome).ToList();
                DropDownList_Fabricante.DataBind();
                DropDownList_Fabricante.Items.Insert(0, new ListItem("Selecione...", "-1"));
            }
        }

        protected void OnClick_Incluir(object sender, EventArgs e)
        {
            IList<RetiradaDeLote> lista = Session["lista"] != null ? (IList<RetiradaDeLote>)Session["lista"] : new List<RetiradaDeLote>();
            //ViverMais.Model.Vacina vacina = Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(DropDownList_Vacina.SelectedValue));
            RetiradaDeLote retira = new RetiradaDeLote(Factory.GetInstance<IVacina>().BuscarPorCodigo<ViverMais.Model.Vacina>(int.Parse(DropDownList_Vacina.SelectedValue)), DropDownList_Fabricante.SelectedValue != "-1" ? Factory.GetInstance<IFabricanteVacina>().BuscarPorCodigo<ViverMais.Model.FabricanteVacina>(int.Parse(DropDownList_Fabricante.SelectedValue)) : null, TextBox_Lote.Text.Trim().ToUpper(), !string.IsNullOrEmpty(TextBox_Validade.Text) ? DateTime.Parse(TextBox_Validade.Text) : DateTime.MinValue);
            lista.Add(retira);
            Session["lista"] = lista;
            this.CarregaGrid(lista);
        }

        private void CarregaGrid(IList<RetiradaDeLote> lista)
        {
            GridView_Pesquisa.DataSource = lista;
            GridView_Pesquisa.DataBind();
        }

        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Text = DropDownList_Vacina.SelectedValue;
        }

        protected void OnClick_GerarArquivos(object sender, EventArgs e)
        {
            IList<RetiradaDeLote> lista = Session["lista"] != null ? (IList<RetiradaDeLote>)Session["lista"] : new List<RetiradaDeLote>();
            int i = 1;
            foreach (RetiradaDeLote retirar in lista)
            {
                StreamWriter writer = new StreamWriter(Server.MapPath("~/Vacina/ExclusaoVacina/ARQUIVO_" + i + ".txt"), false);

                writer.WriteLine("=====DADOS DO LOTE REQUIRIDO=====");
                writer.WriteLine("VACINA: " + retirar.NomeVacina);
                writer.WriteLine("FABRICANTE: " + retirar.NomeFabricante);
                writer.WriteLine("LOTE: " + retirar.Lote);
                writer.WriteLine("VALIDADE: " + retirar.DataValidade);

                IList<LoteVacina> lotes = null;

                if (retirar.Fabricante == null)
                {
                    if (!retirar.Validade.ToString("dd/MM/yyyy").Equals(DateTime.MinValue.ToString("dd/MM/yyyy")))
                    {
                        lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().Where(p => p.Identificacao.ToUpper() == retirar.Lote.ToUpper() &&
                        p.ItemVacina.Vacina.Codigo == retirar.Vacina.Codigo && p.DataValidade.ToString("dd/MM/yyyy").Equals(retirar.DataValidade)).ToList();
                    }
                    else
                    {
                        lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().Where(p => p.Identificacao.ToUpper() == retirar.Lote.ToUpper() &&
                        p.ItemVacina.Vacina.Codigo == retirar.Vacina.Codigo).ToList();
                    }
                }
                else
                {
                    if (!retirar.Validade.ToString("dd/MM/yyyy").Equals(DateTime.MinValue.ToString("dd/MM/yyyy")))
                    {
                        lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().Where(p => p.Identificacao.ToUpper() == retirar.Lote.ToUpper() &&
                            p.ItemVacina.Vacina.Codigo == retirar.Vacina.Codigo && p.DataValidade.ToString("dd/MM/yyyy").Equals(retirar.DataValidade) && p.ItemVacina.FabricanteVacina.Codigo == retirar.Fabricante.Codigo).ToList();
                    }
                    else 
                    {
                        lotes = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().Where(p => p.Identificacao.ToUpper() == retirar.Lote.ToUpper() &&
                         p.ItemVacina.Vacina.Codigo == retirar.Vacina.Codigo && p.ItemVacina.FabricanteVacina.Codigo == retirar.Fabricante.Codigo).ToList();
                    }
                }

                writer.WriteLine("=====VERIFICANDO EXISTENCIA DO LOTE=====");
                if (lotes.Count() > 0)
                {
                    writer.WriteLine("Lote(s) encontrado(s). Informações abaixo: ");
                    foreach (LoteVacina lote in lotes)
                        writer.WriteLine("Codigo: " + lote.Codigo + ", Vacina: " + lote.ItemVacina.Vacina.Nome + ", Fabricante: " + lote.ItemVacina.FabricanteVacina.Nome + ", Identificacao: " + lote.Identificacao + ", Validade: " + lote.DataValidade.ToString("dd/MM/yyyy"));
                }
                else
                    writer.WriteLine("Lote(s) não encontrado(s).");

                writer.WriteLine("=====OUTRAS INFORMAÇÕES=====");
                if (lotes.Count() > 0)
                {
                    foreach (LoteVacina lote in lotes)
                    {
                        writer.WriteLine("INFORMACOES DO LOTE");
                        writer.WriteLine("Codigo: " + lote.Codigo + ", Vacina: " + lote.ItemVacina.Vacina.Nome + ", Fabricante: " + lote.ItemVacina.FabricanteVacina.Nome + ", Identificacao: " + lote.Identificacao + ", Validade: " + lote.DataValidade.ToString("dd/MM/yyyy"));

                        writer.WriteLine("=====VERIFICANDO ESTOQUE DO(S) LOTE(S) ENCONTRADO(S)=====");
                        IList<EstoqueVacina> estoques = Factory.GetInstance<IEstoque>().ListarTodos<EstoqueVacina>().Where(p => p.Lote.Codigo == lote.Codigo).ToList();
                        foreach (EstoqueVacina estoque in estoques)
                            writer.WriteLine("Sala:" + estoque.Sala.Nome + ", Quantidade: " + estoque.QuantidadeEstoque + ".");

                        writer.WriteLine("=====VERIFICANDO ITENS DO INVENTARIO DO(S) LOTE(S) ENCONTRADO(S)=====");
                        IList<ItemInventarioVacina> itens = Factory.GetInstance<IItemInventarioVacina>().ListarTodos<ItemInventarioVacina>().Where(p => p.LoteVacina.Codigo == lote.Codigo).OrderBy(p => p.Inventario.Codigo).ToList();
                        foreach (ItemInventarioVacina item in itens)
                            writer.WriteLine("Inventario: " + item.Inventario.Codigo + ", Sala: " + item.Inventario.Sala.Nome + ", Item Inventario: " + item.Codigo + ".");

                        writer.WriteLine("=====VERIFICANDO ITENS DA DISPENSACAO DO(S) LOTE(S) ENCONTRADO(S)=====");
                        IList<ItemDispensacaoVacina> i2 = Factory.GetInstance<IEstoque>().ListarTodos<ItemDispensacaoVacina>().Where(p => p.Lote.Codigo == lote.Codigo).OrderBy(p => p.Dispensacao.Codigo).ToList();
                        foreach (ItemDispensacaoVacina item in i2)
                            writer.WriteLine("Dispensacao: " + item.Dispensacao.Codigo + ", Sala: " + item.Dispensacao.Sala.Nome + ", Item Dispensacao: " + item.Codigo + ".");

                        writer.WriteLine("=====LOTES DISPONÍVEIS PARA SUBSTITUÍ-LO(S)=====");
                        IList<LoteVacina> lotessub = Factory.GetInstance<ILoteVacina>().ListarTodos<LoteVacina>().Where(p => p.ItemVacina.Vacina.Codigo == lote.ItemVacina.Vacina.Codigo && !lotes.Select(p2 => p2.Codigo).ToList().Contains(p.Codigo)).ToList();
                        foreach (LoteVacina lotesub in lotessub)
                            writer.WriteLine("Lote: " + lotesub.Codigo + ", Identificacao: " + lotesub.Identificacao + ", Vacina: " + lotesub.ItemVacina.Vacina.Nome + ", Fabricante: " + lotesub.ItemVacina.FabricanteVacina.Nome + ", Validade: " + lotesub.DataValidade.ToString("dd/MM/yyyy") + ".");
                    }
                }

                writer.Dispose();
                writer.Close();
                i++;
            }
        }

        protected void OnRowDeleting_Excluir(object sender, GridViewDeleteEventArgs e)
        {
            IList<RetiradaDeLote> lista = Session["lista"] != null ? (IList<RetiradaDeLote>)Session["lista"] : new List<RetiradaDeLote>();
            lista.RemoveAt(e.RowIndex);
            Session["lista"] = lista;
            this.CarregaGrid(lista);
        }

        private class RetiradaDeLote
        {
            public RetiradaDeLote(ViverMais.Model.Vacina vacina, FabricanteVacina fabricante, string lote, DateTime validade)
            {
                this.vacina = vacina;
                this.fabricante = fabricante;
                this.lote = lote;
                this.validade = validade;
            }

            private FabricanteVacina fabricante;

            public FabricanteVacina Fabricante
            {
                get { return fabricante; }
                set { fabricante = value; }
            }

            public string NomeFabricante
            {
                get
                {
                    if (Fabricante == null)
                        return "";

                    return Fabricante.Nome;
                }
            }

            private DateTime validade;

            public DateTime Validade
            {
                get
                {
                    return validade;
                }

                set { validade = value; }
            }

            public string DataValidade
            {
                get
                {
                    if (Validade.Equals(DateTime.MinValue))
                        return "";

                    return Validade.ToString("dd/MM/yyyy");
                }
            }

            private string lote;

            public string Lote
            {
                get { return lote; }
                set { lote = value; }
            }

            private ViverMais.Model.Vacina vacina;

            public ViverMais.Model.Vacina Vacina
            {
                get { return vacina; }
                set { vacina = value; }
            }

            public string NomeVacina
            {
                get { return Vacina.Nome; }
            }
        }
    }
}
