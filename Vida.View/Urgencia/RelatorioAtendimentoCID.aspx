﻿<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" AutoEventWireup="true" CodeBehind="RelatorioAtendimentoCID.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioAtendimentoCID" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<CR:CrystalReportViewer ID="CrystalReportViewer_AtendimentoCID" runat="server"
 DisplayGroupTree="False" AutoDataBind="true"></CR:CrystalReportViewer>
<%--<h3>
    Relatório de Atendimento por CID
</h3>
<p>
Unidade: <asp:Label ID="lblUnidade" runat="server" Text=""></asp:Label>
</p>
<p>
CID: <asp:Label ID="lblCID" runat="server" Text=""></asp:Label>
</p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
        </Columns>
    </asp:GridView>--%>
</asp:Content>
