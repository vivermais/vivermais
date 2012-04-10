<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioLoteAVencer.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioLoteAVencer"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Relatório de Lotes a Vencer
    </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" />
                <asp:BoundField DataField="Sigla" HeaderText="Sigla" />
                <asp:BoundField DataField="Lote" HeaderText="Lote" />
                <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" />
                <asp:BoundField DataField="QuantidadeEstoque" HeaderText="QuantidadeEstoque" />
                <asp:BoundField DataField="Validade" HeaderText="Validade" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
