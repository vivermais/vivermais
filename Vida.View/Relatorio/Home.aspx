﻿<%@ Page Language="C#" MasterPageFile="~/Relatorio/RelatorioMaster.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ViverMais.View.Relatorio.Home" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<p>
    <asp:LinkButton ID="lnkBtnRelatorioGenerico" runat="server" PostBackUrl="~/Relatorio/RelatorioGenerico.aspx">Relatório Genérico</asp:LinkButton>
</p>
<p>
    <asp:LinkButton ID="lnkBtnRelatorioProducaoCNS" runat="server" PostBackUrl="~/Relatorio/RelatorioCNS.aspx">Relatório Produção CNS</asp:LinkButton>
</p>
</asp:Content>