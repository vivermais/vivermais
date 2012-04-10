<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioConsolidadoRM.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioConsolidadoRM"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Relatório de Consolidado de RM
    </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" />
                <asp:BoundField DataField="Unidade" HeaderText="Unidade" />
                <asp:BoundField DataField="QuantidadeFornecida" HeaderText="QuantidadeFornecida" />
                <asp:BoundField DataField="QuantidadePedida" HeaderText="QuantidadePedida" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
