<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ViverMais.View.EnvioBPA.Default" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>
        Envio de BPA
    </h2>
    
    <fieldset>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkEnviarArquivo" NavigateUrl="~/EnvioBPA/FormEnviarBPA.aspx"
            runat="server">Enviar Arquivo de BPA</asp:HyperLink>
            </div>
    </p>
    <br />
    <p>
     <div class="botao-bpa">
        <asp:HyperLink ID="lnkRelatorioEnvioArquivo" NavigateUrl="~/EnvioBPA/RelatorioEnvioBPA.aspx"
            runat="server">Relatório Envio BPA</asp:HyperLink>
            </div>
    </p>
    <br />
    <p>
     <div class="botao-bpa">
        <asp:HyperLink ID="lnkRelatoriosAdministrativos" runat="server" NavigateUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx">Relatórios Administrativos</asp:HyperLink>
        </div>
    </p>
    </fieldset>
    </div>
</asp:Content>
