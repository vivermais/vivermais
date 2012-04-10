<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormFabricante.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormFabricante" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formulário para Cadastro de Fabricante</h2>
        <fieldset>
            <legend>Fabricante</legend>
            <p>
                <span class="rotulo">CNPJ</span> <span>
                    <asp:TextBox ID="TextBox_CNPJ" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Razão Social</span> <span>
                    <asp:TextBox ID="TextBox_Nome" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Situação</span> <span class="radio1">
                    <asp:RadioButton ID="RadioButton_Ativo" Checked="true" GroupName="GroupName_Situacao"
                        runat="server" TextAlign="Left" Text="Ativo" />
                    <asp:RadioButton ID="RadioButton_Inativo" GroupName="GroupName_Situacao" runat="server"
                        TextAlign="Left" Text="Inativo" /></span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Salvar" runat="server" OnClick="OnClick_Salvar" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadFabricante')) return confirm('Todos os dados do fabricante estão corretos ?'); return false;"
                    ValidationGroup="ValidationGroup_cadFabricante">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Cancelar" runat="server" PostBackUrl="~/Vacina/FormExibeFabricante.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é obrigatório!"
                    Display="None" ControlToValidate="TextBox_Nome" ValidationGroup="ValidationGroup_cadFabricante"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="CNPJ é obrigatório!"
                    Display="None" ControlToValidate="TextBox_CNPJ" ValidationGroup="ValidationGroup_cadFabricante"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Formato inválido para CNPJ!"
                    ValidationExpression="^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$" Display="None" ValidationGroup="ValidationGroup_cadFabricante"
                    ControlToValidate="TextBox_CNPJ"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CNPJ inválido! Por favor, informe outro CNPJ."
                    Display="None" ControlToValidate="TextBox_CNPJ" ValidationGroup="ValidationGroup_cadFabricante" ValueToCompare="00.000.000/0000-00" Operator="NotEqual"></asp:CompareValidator>
                
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="ValidationGroup_cadFabricante" runat="server" />
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" MaskType="None" Mask="99,999,999/9999-99"
                    AutoComplete="true" InputDirection="LeftToRight" TargetControlID="TextBox_CNPJ"
                    ClearMaskOnLostFocus="false">
                </cc1:MaskedEditExtender>
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
