﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirCartaoVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormImprimirCartaoVacina" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViverMais - Impressão do Cartão de Vacina</title>
    <link rel="stylesheet" href="style_form_vacina.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server" style="height: 778px; width: 389px;">
    <asp:Table ID="ImpressaoCartao_Vacina" runat="server" Height="778px" Width="389px"
        CellPadding="0">
        <asp:TableRow>
            <asp:TableCell>
                <asp:Table ID="Cartao_Vacina" runat="server" Height="778px" Width="389px" CellPadding="0"
                    CellSpacing="0">
                    <asp:TableRow HorizontalAlign="Center" Width="389px" Height="163px" Style="background-repeat: no-repeat;">
                        <asp:TableCell>
                            <asp:Image ID="ImagemTopo" runat="server" ImageUrl="~/Vacina/img/topo_cartao_vacina1.png" />
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableHeaderRow>
                        <asp:TableCell VerticalAlign="Top" BackColor="#000000">
                            <asp:Table ID="Table_Cabecalho" Width="389px" runat="server" HorizontalAlign="Center"
                                Font-Names="Verdana" Font-Size="11px" CellPadding="0" CellSpacing="0">
                                <asp:TableRow Width="389px" BorderColor="#000000" BorderWidth="1px" BorderStyle="Solid">
                                    <asp:TableCell Height="33px" Width="53px" BackColor="#000000" BorderStyle="Solid"
                                        BorderColor="#000000" BorderWidth="1px">
                                        <asp:Image ID="nomecartao" runat="server" ImageUrl="~/Vacina/img/nome_cartao_vacina.png" />
                                    </asp:TableCell>
                                    <asp:TableCell BackColor="#ffffff" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px">
                                        <p style="padding-left: 10px; font-weight: bold;">
                                            <asp:Label ID="Label_Paciente" runat="server" Text=""></asp:Label></p>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableHeaderRow>
                    <asp:TableHeaderRow VerticalAlign="Top">
                        <asp:TableCell VerticalAlign="Top">
                            <asp:Table ID="dadosvacina" Width="389px" Height="34px" runat="server" HorizontalAlign="Center"
                                Font-Names="Verdana" Font-Size="11px" CellPadding="0" CellSpacing="0">
                                <asp:TableRow HorizontalAlign="Center" Width="389px" Height="34px" Style="background-repeat: no-repeat;">
                                    <asp:TableCell Height="34px" Width="389px" BackColor="#000000">
                                        <asp:Image ID="img_dadosvacina" runat="server" ImageUrl="~/Vacina/img/topo_dados_vacina.png" />
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:GridView ID="GridView_CartaoVacina" runat="server" AutoGenerateColumns="false"
                                            Width="389px" Height="700px" ShowHeader="false">
                                            <Columns>
                                                <asp:BoundField DataField="AbreviacaoVacina" ItemStyle-Font-Size="7px" ItemStyle-Width="78px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DoseAbreviada" ItemStyle-Font-Size="7px" ItemStyle-Width="39px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="DataAplicacaoToString" ItemStyle-Font-Size="7px" ItemStyle-Width="54px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProximaDose" ItemStyle-Font-Size="7px" ItemStyle-Width="54px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="LoteToString" ItemStyle-Font-Size="7px" ItemStyle-Width="45px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="SiglaEstabelecimento" ItemStyle-Font-Size="7px" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </asp:TableCell>
            <asp:TableCell>
                <p>
                    <span>
                        <asp:DataList ID="DataList_Avatares" runat="server" DataKeyField="Codigo" 
                        OnItemDataBound="OnItemDataBound_DataList_Avatares" RepeatColumns="3" RepeatDirection="Horizontal">
                            <ItemTemplate>
                            
                                <asp:Panel ID="Panel_Item" runat="server">
                                <p>
                                    <span>
                                        <asp:ImageButton runat="server" ID="IMG_Avatar"  CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_IMG_Avatar" />
                                    </span>
                                </p>
                                <p>
                                <span>
                                        <asp:Label ID="Label_Avatar" runat="server"></asp:Label>
                                </span>
                                </p>
                                </asp:Panel>
                            </ItemTemplate>
                        </asp:DataList>
                    </span>
                </p>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    </form>
</body>
</html>
