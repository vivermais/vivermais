using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ViverMais.DAO;
using ViverMais.ServiceFacade.ServiceFacades;
using ViverMais.Model;
using ViverMais.ServiceFacade.ServiceFacades.Localidade;
using ViverMais.ServiceFacade.ServiceFacades.Farmacia.Medicamento;
using ViverMais.ServiceFacade.ServiceFacades.Seguranca;

namespace ViverMais.View.Farmacia
{
    public partial class FormFornecedor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Factory.GetInstance<ISeguranca>().VerificarPermissao(((Usuario)Session["Usuario"]).Codigo, "MANTER_FORNECEDOR", Modulo.FARMACIA))
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Usuário, você não tem permissão para acessar esta página! Por favor, entre em contato com a administração.');location='Default.aspx';", true);
                else
                {
                    IList<Pais> paises = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<Pais>().OrderBy(p => p.Nome).ToList();
                    foreach (Pais pais in paises)
                        DropDownList_Pais.Items.Add(new ListItem(pais.Nome, pais.Codigo));

                    IList<UF> estados = Factory.GetInstance<IViverMaisServiceFacade>().ListarTodos<UF>().OrderBy(p => p.Nome).ToList();
                    foreach (UF estado in estados)
                        DropDownList_UF.Items.Add(new ListItem(estado.Sigla, estado.Sigla));

                    int temp;

                    if (Request["co_fornecedor"] != null && int.TryParse(Request["co_fornecedor"].ToString(), out temp))
                    {
                        try
                        {
                            FornecedorMedicamento fornecedor = Factory.GetInstance<IFarmaciaServiceFacade>().BuscarPorCodigo<FornecedorMedicamento>(int.Parse(Request["co_fornecedor"].ToString()));
                            TextBox_RazaoSocial.Text = fornecedor.RazaoSocial;
                            TextBox_NomeFantasia.Text = fornecedor.NomeFantasia;
                            TextBox_CNPJ.Text = fornecedor.Cnpj;
                            DropDownList_Pais.SelectedValue = !string.IsNullOrEmpty(fornecedor.CodigoPais) ? fornecedor.CodigoPais : "-1";
                            DropDownList_UF.SelectedValue = fornecedor.CodigoUf;
                            OnSelectedIndexChanged_CarregaCidades(sender, e);
                            DropDownList_Cidade.SelectedValue = fornecedor.CodigoCidade;
                            OnSelectedIndexChanged_CarregaBairros(sender, e);
                            DropDownList_Bairro.SelectedValue = fornecedor.CodigoBairro.ToString();
                            TextBox_Endereco.Text = fornecedor.Endereco;
                            TextBox_Telefone.Text = fornecedor.Telefone;
                            TextBox_Fax.Text = fornecedor.Fax;

                            if (fornecedor.Situacao == FornecedorMedicamento.INATIVO)
                            {
                                RadioButton_Ativo.Checked = false;
                                RadioButton_Inativo.Checked = true;
                            }
                        }
                        catch (Exception f)
                        {
                            throw f;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Carrega as cidades relacionadas ao estado escolhido
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaCidades(object sender, EventArgs e) 
        {
            DropDownList_Cidade.Items.Clear();
            DropDownList_Cidade.Items.Add(new ListItem("Selecione...", "-1"));
            IList<Municipio> municipios = Factory.GetInstance<IMunicipio>().ListarPorEstado<Municipio>(DropDownList_UF.SelectedValue);

            foreach (Municipio municipio in municipios)
                DropDownList_Cidade.Items.Add(new ListItem(municipio.Nome, municipio.Codigo));
        }

        /// <summary>
        /// Carrega os bairros relacionados a cidade escolhida
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnSelectedIndexChanged_CarregaBairros(object sender, EventArgs e) 
        {
            DropDownList_Bairro.Items.Clear();
            DropDownList_Bairro.Items.Add(new ListItem("Selecione...", "-1"));
            IList<Bairro> bairros = Factory.GetInstance<IBairro>().ListarPorCidade<Bairro>(DropDownList_Cidade.SelectedValue);

            foreach (Bairro bairro in bairros)
                DropDownList_Bairro.Items.Add(new ListItem(bairro.Nome,bairro.Codigo.ToString()));
        }

        /// <summary>
        /// Salva o fornecedor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnClick_Salvar(object sender, EventArgs e) 
        {
            try
            {
                IFornecedorMedicamento iFornecedor = Factory.GetInstance<IFornecedorMedicamento>();
                FornecedorMedicamento fornecedor = Request["co_fornecedor"] != null ? iFornecedor.BuscarPorCodigo<FornecedorMedicamento>(int.Parse(Request["co_fornecedor"].ToString())) : new FornecedorMedicamento();

                if (iFornecedor.ValidaCadastroFornecedor<FornecedorMedicamento>(TextBox_CNPJ.Text, fornecedor.Codigo))
                {
                    fornecedor.RazaoSocial = TextBox_RazaoSocial.Text;
                    fornecedor.NomeFantasia = TextBox_NomeFantasia.Text;
                    fornecedor.Cnpj = TextBox_CNPJ.Text;

                    if (DropDownList_Pais.SelectedValue != "-1")
                        fornecedor.CodigoPais = DropDownList_Pais.SelectedValue;

                    fornecedor.CodigoUf = DropDownList_UF.SelectedValue;
                    fornecedor.CodigoCidade = DropDownList_Cidade.SelectedValue;
                    fornecedor.CodigoBairro = int.Parse(DropDownList_Bairro.SelectedValue);
                    fornecedor.Endereco = TextBox_Endereco.Text;
                    fornecedor.Telefone = TextBox_Telefone.Text;
                    fornecedor.Fax = TextBox_Fax.Text;
                    fornecedor.Situacao = RadioButton_Ativo.Checked ? FornecedorMedicamento.ATIVO : FornecedorMedicamento.INATIVO;

                    iFornecedor.Salvar(fornecedor);
                    iFornecedor.Salvar(new LogFarmacia(DateTime.Now, ((Usuario)Session["Usuario"]).Codigo, EventoFarmacia.MANTER_FORNECEDOR, "id fornecedor: " + fornecedor.Codigo));

                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Fornecedor salvo com sucesso!');location='Default.aspx';", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "alert", "alert('Já existe um fornecedor cadastrado com o CNPJ informado! Por favor, digite outro CNPJ.');", true);
            }
            catch (Exception f)
            {
                throw f;
            }
        }
    }
}
