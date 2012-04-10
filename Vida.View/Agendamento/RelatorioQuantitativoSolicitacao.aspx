<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioQuantitativoSolicitacao.aspx.cs"
    Inherits="ViverMais.View.Agendamento.RelatorioQuantitativoSolicitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width: 1085px; height:133px;  background-image: url( 'img/topo-relatorio.jpg' ); background-repeat:no-repeat;">
        <div style="width: 1085px; height:60px; font-size:26px; color:#333333; font-family: Arial; border: none; font-weight: bold; padding: 50px 3px 3px 20px; ">
            Relatorio Quantitativo de Solicitação
        </div>
        <br />
        <div style="background-color:#526383; color:White; width:100%; margin-bottom:1px; height:20px; padding-top:2px">
        <span style="float:left; margin-left:7px;">
            <asp:Label ID="lblPeriodo" runat="server" Font-Bold="True" Text="Período:" Font-Size="12px"
                Font-Names="Verdana"></asp:Label>
            <asp:Label ID="lblPeriodo1" runat="server" Font-Bold="True" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        </span>
        <span style="float:right; margin-right:7px;">
            <asp:Label ID="lblDataGeracao" runat="server" Font-Bold="True" Text="Data da Geração:"
                Font-Size="12px" Font-Names="Verdana"></asp:Label>
            <asp:Label ID="lblData" runat="server" Font-Bold="True" Font-Size="12px" Font-Names="Verdana"></asp:Label>
        </span>
        </div>
        <asp:GridView ID="GridViewQuantitativo" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#7589b0" BorderStyle="None" BorderWidth="1px"
            CellPadding="3" GridLines="Vertical" Width="100%">
             <FooterStyle BackColor="#78b9cbd" ForeColor="Black" Font-Names="Arial" />
 <RowStyle BackColor="#afc1e2" ForeColor="Black" Font-Size="9px" Font-Names="Arial" HorizontalAlign="Center" />
 <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" Font-Names="Arial" />
 <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" Font-Names="Arial" />
 <HeaderStyle BackColor="#78b9cbd" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
 <AlternatingRowStyle BackColor="#cfdaef" />
            <Columns>
                <asp:BoundField HeaderText="Unidade Solicitante" DataField="UnidadeSolicitante" />
                <asp:BoundField HeaderText="Município" DataField="Municipio" />
                <asp:BoundField HeaderText="Procedimento" DataField="Procedimento" />
                <asp:BoundField HeaderText="Especialidade" DataField="Especialidade" />
                <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" />
            </Columns>
        </asp:GridView>
        <br />
    </div>
    </form>
</body>
</html>
