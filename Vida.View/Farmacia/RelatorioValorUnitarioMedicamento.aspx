<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master" AutoEventWireup="true" CodeBehind="RelatorioValorUnitarioMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioValorUnitarioMedicamento" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" />
            <asp:BoundField DataField="ValorUnitario" HeaderText="Valor Unitário" />
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
        </Columns>
    </asp:GridView>
</asp:Content>
