﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_PesquisarMedicamento.ascx.cs"
    Inherits="ViverMais.View.Farmacia.WUC_PesquisarMedicamento" %>
<p>
    <span class="rotulo">
        <asp:Label ID="Label_ChamadaPesquisa" runat="server" Text="Medicamento"></asp:Label>
    </span> <span>
        <asp:TextBox ID="TextBoxMedicamento" runat="server" CssClass="campo" Width="300px"></asp:TextBox>
        <asp:LinkButton ID="LinkButtonPesquisar" runat="server" OnClick="OnClick_BuscarPorNome"
            ValidationGroup="ValidationGroup_BuscarMedicamento"
            Text="<img src='img/procurar.png' alt='Pesquisar'/>"></asp:LinkButton>
    </span>
</p>
<asp:RequiredFieldValidator ID="RequiredFieldValidator_NomeMedicamento" runat="server" ControlToValidate="TextBoxMedicamento"
    Display="None" ValidationGroup="ValidationGroup_BuscarMedicamento" ErrorMessage="Informe o nome do medicamento."></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_NomeMedicamento" runat="server" ErrorMessage="Informe pelo menos as três primeiras letras do medicamento."
    Display="None" ControlToValidate="TextBoxMedicamento" ValidationGroup="ValidationGroup_BuscarMedicamento"
    ValidationExpression="^\S{3}[\W-\w]*$">
</asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_BuscarMedicamento"
    ShowMessageBox="true" ShowSummary="false" />
