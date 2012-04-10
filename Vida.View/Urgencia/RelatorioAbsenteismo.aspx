<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioAbsenteismo.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioAbsenteismo"
    Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
<%--    <h2>
        <asp:Label Text="Relatório de Evasão" runat="server" ID="lblRelatorioAbsenteismo">
        </asp:Label>
    </h2>--%>
<%--    <fieldset class="formulario">
    <legend>Relatório</legend>
    <p>
        Unidade: <span style="margin-left: 5px;">
            <asp:Label ID="lblUnidade" runat="server"></asp:Label>
        </span>
        <span style="margin-left: 300px;">
            <asp:Label runat="server" id="lblDatahora"></asp:Label>
        </span>
    </p>
    <p>
        Período: <span style="margin-left: 5px;">
            <asp:Label ID="lblPeriodo" runat="server"></asp:Label>
        </span>
    </p>
    <asp:GridView ID="GridViewAbsenteismo" runat="server" ShowHeader="false" Width="300px">
        
    </asp:GridView>
    </fieldset>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer_Evasao" runat="server" AutoDataBind="true"
        DisplayGroupTree="False" />
    </div>
</asp:Content>
