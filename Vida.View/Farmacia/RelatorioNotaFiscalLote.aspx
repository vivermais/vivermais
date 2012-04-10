﻿<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master"
    AutoEventWireup="true" CodeBehind="RelatorioNotaFiscalLote.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatorioNotaFiscalLote"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>
        Relatório de Nota Fiscal por Lote
    </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="NumeroNotaFiscal" HeaderText="NumeroNotaFiscal" />
                <asp:BoundField DataField="DataRecebimento" HeaderText="Data de Recebimento" />
                <asp:BoundField DataField="RazaoSocial" HeaderText="Razão Social" />
                <asp:BoundField DataField="Empenho" HeaderText="Empenho" />
            </Columns>
        </asp:GridView>
    </p>
</asp:Content>
