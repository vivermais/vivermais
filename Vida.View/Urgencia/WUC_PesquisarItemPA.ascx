﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_PesquisarItemPA.ascx.cs" Inherits="ViverMais.View.Urgencia.WUC_PesquisarItemPA" %>
<p>
    <span class="rotulo">Nome</span>
    <span>
        <asp:TextBox ID="TextBoxItem" runat="server" CssClass="campo" Width="300px"></asp:TextBox>
        <asp:LinkButton ID="LinkButtonPesquisar" runat="server" OnClick="OnClick_BuscarPorNome"
         ValidationGroup="ValidationGroup_BuscarItem"
         Text="<img src='img/procurar.png' alt='Pesquisar Item'/>" ></asp:LinkButton>
    </span>
</p>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxItem"
    Display="None" ValidationGroup="ValidationGroup_BuscarItem" ErrorMessage="Informe o nome do item."></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Informe pelo menos as três primeiras letras do item."
    Display="None" ControlToValidate="TextBoxItem" ValidationGroup="ValidationGroup_BuscarItem"
    ValidationExpression="^\S{3}[\W-\w]*$">
</asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_BuscarItem"
    ShowMessageBox="true" ShowSummary="false" />