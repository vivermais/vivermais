<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormFornecedor.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormFornecedor" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Fomulário para Cadastro de Fornecedor</h2>
        <fieldset>
            <legend>Fornecedor</legend>
            <p>
                <span class="rotulo">Razão Social</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_RazaoSocial" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Nome Fantasia</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_NomeFantasia" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">CNPJ</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_CNPJ" CssClass="campo" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">País</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_Pais" runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">UF</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_UF" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaCidades">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_UF" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Cidade</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="DropDownList_Cidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaBairros">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Cidade" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Bairro</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="DropDownList_Bairro" runat="server">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Endereço</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Endereco" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Telefone</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Telefone" CssClass="campo" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Fax</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Fax" CssClass="campo" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Situação</span> <span class="camporadio">
                    <asp:RadioButton ID="RadioButton_Ativo" CssClass="camporadio" Width="20px" Checked="true"
                        GroupName="GroupName_Situacao" runat="server" />Ativo
                    <asp:RadioButton ID="RadioButton_Inativo" CssClass="camporadio" Width="20px" GroupName="GroupName_Situacao"
                        runat="server" />Inativo </span>
            </p>
            <p align="center">
                <span>
                    <asp:Button ID="Button_Salvar" runat="server" Text="Salvar" OnClick="OnClick_Salvar"
                        ValidationGroup="ValidationGroup_cadFornecedor" />
                    <asp:Button ID="Button_Cancelar" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Razão Social é Obrigatório!"
                    Display="None" ControlToValidate="TextBox_RazaoSocial" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_RazaoSocial"
                    Display="None" ErrorMessage="Há caracters inválidos na Razão Social do Fornecedor."
                    Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox_NomeFantasia"
                    Display="None" ErrorMessage="Há caracters inválidos no Nome Fantasia do Fornecedor."
                    Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="CNPJ é Obrigatório!"
                    Display="None" ControlToValidate="TextBox_CNPJ" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="UF é Obrigatório!"
                    ControlToValidate="DropDownList_UF" ValueToCompare="-1" Operator="NotEqual" Display="None"
                    ValidationGroup="ValidationGroup_cadFornecedor"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Cidade é Obrigatório!"
                    ControlToValidate="DropDownList_Cidade" ValueToCompare="-1" Operator="NotEqual"
                    Display="None" ValidationGroup="ValidationGroup_cadFornecedor"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Bairro é Obrigatório!"
                    ControlToValidate="DropDownList_Bairro" ValueToCompare="-1" Operator="NotEqual"
                    Display="None" ValidationGroup="ValidationGroup_cadFornecedor"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Endereço é Obrigatório!"
                    Display="None" ControlToValidate="TextBox_Endereco" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox_Endereco"
                    Display="None" ErrorMessage="Há caracters inválidos no Endereço do Fornecedor."
                    Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFornecedor"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Formato inválido para CNPJ!"
                    ValidationExpression="^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$" Display="None" ValidationGroup="ValidationGroup_cadFornecedor"
                    ControlToValidate="TextBox_CNPJ"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Formato inválido para Telefone!"
                    ValidationExpression="^((\(\d{2}\))|(\(\d{2}\)\s))\d{4}\-\d{4}$" Display="None"
                    ValidationGroup="ValidationGroup_cadFornecedor" ControlToValidate="TextBox_Telefone"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Formato inválido para Fax!"
                    ValidationExpression="^((\(\d{2}\))|(\(\d{2}\)\s))\d{4}\-\d{4}$" Display="None"
                    ValidationGroup="ValidationGroup_cadFornecedor" ControlToValidate="TextBox_Fax"></asp:RegularExpressionValidator>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                    Mask="99,999,999/9999-99" ClearMaskOnLostFocus="false" TargetControlID="TextBox_CNPJ"
                    MaskType="None">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                    Mask="(99)9999-9999" ClearMaskOnLostFocus="false" TargetControlID="TextBox_Telefone"
                    MaskType="None">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" InputDirection="LeftToRight"
                    Mask="(99)9999-9999" ClearMaskOnLostFocus="false" TargetControlID="TextBox_Fax"
                    MaskType="None">
                </cc1:MaskedEditExtender>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="ValidationGroup_cadFornecedor" runat="server" />
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
