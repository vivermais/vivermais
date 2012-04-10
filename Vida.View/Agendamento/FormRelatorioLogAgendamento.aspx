﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormRelatorioLogAgendamento.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioLogAgendamento"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <p>
        <span>Data Inicial</span>
        <asp:TextBox ID="tbxDataInicial" runat="server" CssClass="campo"></asp:TextBox>
    </p>
    <p>
        <span>Data Final</span>
        <asp:TextBox ID="tbxDataFinal" runat="server" CssClass="campo"></asp:TextBox>
    </p>
    <p>
        <span>Cartão SUS</span>
        <asp:TextBox ID="tbxCartaoSUS" runat="server" CssClass="campo"></asp:TextBox>
    </p>
    <asp:LinkButton ID="btnImprimir" runat="server" Text="Imprimir" OnClick="btnImprimir_OnClick"></asp:LinkButton>
</asp:Content>
