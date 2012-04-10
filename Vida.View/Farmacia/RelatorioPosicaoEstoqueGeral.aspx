<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioPosicaoEstoqueGeral.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioPosicaoEstoqueGeral"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Relatório de Posição Geral de Estoque</h2>
    <p>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
    </p>
</asp:Content>
