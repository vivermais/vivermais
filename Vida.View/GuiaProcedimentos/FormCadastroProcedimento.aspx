<%@ Page Language="C#" MasterPageFile="~/GuiaProcedimentos/MasterCatalogo.Master"
    AutoEventWireup="true" CodeBehind="FormCadastroProcedimento.aspx.cs" Inherits="ViverMais.View.GuiaProcedimentos.FormCadastroProcedimento "
    Title="Formulário de Cadastro de Informações de Procedimento" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Cadastro de Informação de Procedimento
    </h1>
    <p>
        <span class="">Código do Procedimento: </span><span class="">
            <asp:TextBox ID="tbxCodigoProcedimento" OnTextChanged="tbxCodigoProcedimento_TextChanged"
                Width="100px" AutoPostBack="true" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Nome do Procedimento: </span><span class="">
            <asp:TextBox ID="tbxNomeProcedimento" Enabled="false" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Conceito: </span><span class="">
            <asp:TextBox ID="tbxConceito" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Aplicação: </span><span class="">
            <FCKeditorV2:FCKeditor ID="editorInformacaoAplicacao" runat="server" BasePath="~/Urgencia/FCKEditor/"
                LinkBrowserURL="~/Urgencia/FCKEditor/" Height="300px" ImageBrowserURL="~/Urgencia/FCKEditor/">
            </FCKeditorV2:FCKeditor>
        </span>
    </p>
    <p>
        <span class="">Preparo: </span><span class="">
            <asp:TextBox ID="tbxPreparo" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Dicas: </span><span class="">
            <asp:TextBox ID="tbxDicas" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Observação: </span><span class="">
            <asp:TextBox ID="tbxObservacao" TextMode="MultiLine" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">
            <asp:LinkButton ID="btnCadastrar" runat="server" OnClick="btnCadastrar_Click" Text="Cadastrar" />
        </span>
    </p>
</asp:Content>
