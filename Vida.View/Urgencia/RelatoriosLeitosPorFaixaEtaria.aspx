<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master"
    AutoEventWireup="true" CodeBehind="RelatoriosLeitosPorFaixaEtaria.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatoriosLeitosPorFaixaEtaria"
    Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <h3>
        Relatório de Leitos Por Faixa Etária
    </h3>
    <p>
        Unidade<span style="margin-left: 5px;">
            <asp:Label ID="lblUnidade" runat="server"></asp:Label>
        </span><span style="margin-left: 300px;">Data:
            <asp:Label runat="server" ID="lblData"></asp:Label>
        </span>
    </p>
    <p>
        <asp:GridView ID="GridViewLeitosFaixaEtaria" runat="server" ShowHeader="false" Width="300px">
        
        </asp:GridView>
    </p>--%>
    <CR:CrystalReportViewer ID="CrystalReportViewer_LeitosFaixa" runat="server" AutoDataBind="true"
     DisplayGroupTree="False" />
</asp:Content>
