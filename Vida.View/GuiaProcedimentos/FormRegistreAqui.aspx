<%@ Page Language="C#" MasterPageFile="~/GuiaProcedimentos/MasterCatalogo.Master"
    AutoEventWireup="true" CodeBehind="FormRegistreAqui.aspx.cs" Inherits="ViverMais.View.GuiaProcedimentos.FormRegistreAqui"
    Title="Catálogo de Serviços - Registre Aqui" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Formulário de Registro
    </h1>
    <p>
        <span>Tipo de Registro</span> <span>
            <asp:DropDownList ID="ddlTipoRegistro" runat="server">
                <asp:ListItem Text="Selecione..." Value="0" />
                <asp:ListItem Text="DúViverMaiss" Value="1" />
                <asp:ListItem Text="Elogio" Value="2" />
                <asp:ListItem Text="Reclamações" Value="3" />
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="">Nome: </span><span class="">
            <asp:TextBox ID="tbxNome" Width="320" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Email: </span><span class="">
            <asp:TextBox ID="tbxEmail" Width="280" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Telefone: </span><span class="">
            <asp:TextBox ID="tbxTelefone" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Mensagem: </span><span class="">
            <asp:TextBox ID="tbxMensagem" TextMode="MultiLine" Rows="3" Width="350" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Local de Origem: </span><span class="">
            <asp:TextBox ID="tbxLocalOrigem" Width="280" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">
            <asp:LinkButton ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Enviar" />
        </span>
    </p>
    </div>
</asp:Content>
