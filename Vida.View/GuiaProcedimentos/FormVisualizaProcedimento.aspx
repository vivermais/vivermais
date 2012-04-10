<%@ Page Language="C#" MasterPageFile="~/GuiaProcedimentos/MasterCatalogo.Master"
    AutoEventWireup="true" CodeBehind="FormVisualizaProcedimento.aspx.cs" Inherits="Vida.View.GuiaProcedimentos.FormVisualizaProcedimento "
    Title="Informação de Procedimento" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Informação de Procedimento
    </h1>
    <p>
        <span class="">Código do Procedimento: </span><span class="">
            <asp:Label ID="lblCodigoProcedimento"
                Width="100px" AutoPostBack="true" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Nome do Procedimento: </span><span class="">
            <asp:Label ID="lblNomeProcedimento" Enabled="false" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Conceito: </span><span class="">
            <asp:Label ID="lblConceito" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Aplicação: </span><span class="">
            <asp:Label ID="lblAplicacao" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Preparo: </span><span class="">
            <asp:Label ID="lblPreparo" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Dicas: </span><span class="">
            <asp:Label ID="lblDicas" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">Observação: </span><span class="">
            <asp:Label ID="lblObservacao" Width="400px" runat="server"></asp:Label>
        </span>
    </p>
    <p>
        <span class="">
            <asp:LinkButton ID="btnVoltar" runat="server" OnClick="btnVoltar_Click" Text="Voltar" />
        </span>
    </p>
</asp:Content>
