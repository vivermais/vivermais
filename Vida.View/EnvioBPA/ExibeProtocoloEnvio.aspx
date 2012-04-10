<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="true"
    CodeBehind="ExibeProtocoloEnvio.aspx.cs" Inherits="ViverMais.View.EnvioBPA.ExibeProtocoloEnvio"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
    Protocolo de Envio de BPA
    </h1>
    <p>
        CNES: 
        <asp:Label ID="lblCNES" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Login: 
        <asp:Label ID="lblLogin" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Data de Envio: <asp:Label ID="lblDataEnvio" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Competência: <asp:Label ID="lblCompetencia" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Arquivo Enviado: <asp:Label ID="lblArquivoEnviado" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Tamanho do Arquivo: <asp:Label ID="lblTamanhoArquivo" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Número de Controle: <asp:Label ID="lblNumeroControle" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        Numero do Protocolo: <asp:Label ID="lblNumeroProtocolo" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:ImageButton ID="imgbtnVoltar" PostBackUrl="~/EnvioBPA/Default.aspx" runat="server" />
    </p>
</asp:Content>
