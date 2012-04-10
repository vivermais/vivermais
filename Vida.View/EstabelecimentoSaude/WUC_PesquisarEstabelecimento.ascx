<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_PesquisarEstabelecimento.ascx.cs"
    Inherits="ViverMais.View.EstabelecimentoSaude.WUC_PesquisarEstabelecimento"  %>

<p>
    <span class="rotulo">CNES</span> <span>
        <asp:TextBox ID="TextBox_CNES" runat="server" CssClass="campo" MaxLength="7" Width="50px"></asp:TextBox>
        <asp:LinkButton ID="LinkButton_PesquisarCNES" OnClick="OnClick_PesquisarCNES"
            runat="server" >
            <img src="img/procurar.png" alt="Pesquisar Por CNES" id="pesquisacnes" />
        </asp:LinkButton>
    </span>
</p>
<p>
    <span class="rotulo">Nome Fantasia da Unidade</span> <span>
        <asp:TextBox ID="TextBox_NomeFanatasia" runat="server" CssClass="campo" MaxLength="200"
            Width="350px"></asp:TextBox>
        <asp:LinkButton ID="LinkButton_PesquisarNomeFantasia" OnClick="OnClick_PesquisarNomeFantasia"
         runat="server" >
            <img src="img/procurar.png" alt="Pesquisar Por Nome Fantasia" id="pequisanomefantasia" />
        </asp:LinkButton>
    </span>
</p>


<asp:RequiredFieldValidator ID="RequiredFieldValidator_NomeFantasia" runat="server" ErrorMessage="Nome Fantasia é Obrigatório." Display="None" ControlToValidate="TextBox_NomeFanatasia"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_NomeFantasia" runat="server" ErrorMessage="Informe pelo menos as duas primeiras letras do Nome Fantasia da Unidade." ControlToValidate="TextBox_NomeFanatasia" ValidationExpression="^\S{2}[\W-\w]*$" Display="None"></asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummary_NomeFantasia" runat="server"  ShowMessageBox="true" ShowSummary="false" />

<asp:RequiredFieldValidator ID="RequiredFieldValidator_CNES" runat="server" ErrorMessage="CNES é Obrigatório." Display="None"  ControlToValidate="TextBox_CNES"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_CNES" runat="server" ErrorMessage="O CNES da Unidade deve conter 7 dígitos." ValidationExpression="^\d{7}$" ControlToValidate="TextBox_CNES" Display="None" ></asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummary_CNES" runat="server" ShowMessageBox="true" ShowSummary="false" />

