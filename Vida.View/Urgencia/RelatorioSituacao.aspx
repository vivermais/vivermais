﻿<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioSituacao.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioSituacao"
    Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer_Situacao" runat="server" AutoDataBind="true"
     DisplayGroupTree="False">
    </CR:CrystalReportViewer>
    <%--<h3>
    Relatório por Situação
    </h3>
    <p>
        Situação:
        <asp:Label ID="lblSituacao" runat="server" Text="[Situação]"></asp:Label>
    </p>
    <p>
        Distrito:
        <asp:Label ID="lblDistrito" runat="server" Text="[Distrito]"></asp:Label>
    </p>
    <p>
        Unidade:
        <asp:Label ID="lblUnidade" runat="server" Text="[Unidade]"></asp:Label>
    </p>
    <p>
        Data:
        <asp:Label ID="lblData" runat="server" Text="[Data]"></asp:Label>
    </p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        Width="100%">
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Atendimento" HeaderText="Número de Atendimento" />
            <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data" />
            <asp:BoundField DataField="Dias" HeaderText="Dias" />
        </Columns>
    </asp:GridView>--%>
</asp:Content>
