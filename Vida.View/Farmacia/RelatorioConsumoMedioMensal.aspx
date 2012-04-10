<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master" AutoEventWireup="true" CodeBehind="RelatorioConsumoMedioMensal.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioConsumoMedioMensal" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
    Relatório de Consolidado Mensal
    </h2>
    <p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
    <Columns>
            <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" />
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
        </Columns>
    </asp:GridView>
    </p>
</asp:Content>
