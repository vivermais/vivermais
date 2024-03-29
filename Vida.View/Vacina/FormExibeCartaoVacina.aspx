﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeCartaoVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormExibeCartaoVacina" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ViverMais - Cartão de Vacina On-Line</title>
</head>
<body>
    <form runat="server" id="formulario">
    <asp:UpdatePanel ID="upd1" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
            </asp:ScriptManager>
            <div id="top">
                <br />
                <cc1:TabContainer ID="TabContainer1" BackColor="#bee4f8" runat="server" ScrollBars="None"
                    Width="800px">
                    <cc1:TabPanel BackColor="#bee4f8" ID="TabPanel1" runat="server" HeaderText="Vacinas da Criança"
                        ScrollBars="Horizontal">
                        <ContentTemplate>
                            <br />
                            <br />
<%--                            <div class="botoesroll">
                                <asp:ImageButton Enabled="false" ID="LinkButton2" runat="server" OnClick="btnCartaoVacina_Click"
                                    ImageUrl="~/Vacina/img/btn-imprimecartao-crianca.png" Height="39" Width="228" />
                            </div>--%>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_CartaoVacina" runat="server" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="White" BorderColor="#f9e5a9"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="true" PageSize="10"
                                        PagerSettings-Mode="Numeric" GridLines="Horizontal" Font-Names="Verdana" OnPageIndexChanging="OnPageIndexChanging_CartaoCrianca"
                                        OnRowCommand="OnRowCommand_GridView_CartaoVacina">
                                        <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina">
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                                HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" ItemStyle-BorderWidth="0px" ItemStyle-BorderStyle="None" ItemStyle-BorderColor="Aqua" />
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#5c9ec1" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </span>
                                <%-- <asp:GridView ID="GridView_CartaoVacina" runat="server" AutoGenerateColumns="False"
                                    OnRowCommand="OnRowCommand_GridView_CartaoVacina" DataKeyNames="CodigoVacina,CodigoDoseVacina"
                                    BackColor="Transparent" BorderColor="#ffffff" BorderStyle="None" BorderWidth="1px"
                                    Width="720px" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                    <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Font-Size="10px">
                                            <ItemStyle Width="200px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Font-Size="10px">
                                            <ItemStyle Width="40px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data" ItemStyle-Font-Size="10px">
                                            <ItemStyle Width="50px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Font-Size="7px"
                                            ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" HeaderText="Próxima Dose" ItemStyle-Font-Size="7px"
                                            ItemStyle-Width="54px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade" ItemStyle-Font-Size="7px"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                    <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#5c9ec1" />
                                </asp:GridView>--%>
                            </p>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Vacinas do Adolescente" ScrollBars="Horizontal">
                        <ContentTemplate>
                            <br />
                            <br />
                            <%--<div class="botoesroll">
                                <asp:ImageButton Enabled="false" ID="ImageButton2" runat="server" OnClick="btnCartaoVacina_Click"
                                    ImageUrl="~/Vacina/img/btn-imprimecartao-adolescente.png" Width="248" Height="39" />
                            </div>--%>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_CartaoVacinaAdolescente" runat="server" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="White" BorderColor="#f9e5a9"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="true" PageSize="10"
                                        PagerSettings-Mode="Numeric" GridLines="Horizontal" Font-Names="Verdana" OnPageIndexChanging="OnPageIndexChanging_CartaoAdolescente"
                                        OnRowCommand="OnRowCommand_GridView_CartaoVacinaAdolescente">
                                        <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina">
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                                HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#5c9ec1" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <%--<asp:GridView ID="GridView_CartaoVacinaAdolescente" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="Transparent" BorderColor="#ffffff"
                                        BorderStyle="None" BorderWidth="1px" Width="720px" CellPadding="3" GridLines="Horizontal"
                                        Font-Names="Verdana">
                                        <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="200px"
                                                ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="40px"
                                                ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data"
                                                ItemStyle-Width="40px" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Font-Size="10px"
                                                ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" HeaderText="Próxima Dose"
                                                ItemStyle-Font-Size="10px" ItemStyle-Width="54px" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade" ItemStyle-Font-Size="9px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                                ControlStyle-Height="16px" ControlStyle-Width="20px" HeaderText="Info" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#5c9ec1" />
                                    </asp:GridView>--%>
                                </span>
                            </p>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Vacinas do Adulto" ScrollBars="Horizontal">
                        <ContentTemplate>
                            <br />
                            <br />
                            <%--<div class="botoesroll">
                                <asp:ImageButton Enabled="false" ID="ImageButton1" runat="server" OnClick="btnCartaoVacina_Click"
                                    ImageUrl="~/Vacina/img/btn-imprimecartao-adulto.png" Width="218" Height="39" />
                                    </div>--%>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_CartaoVacinaAdulto" runat="server" AutoGenerateColumns="False"
                                            Width="100%" DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="White" BorderColor="#f9e5a9"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="true" PageSize="10"
                                            PagerSettings-Mode="Numeric" GridLines="Horizontal" Font-Names="Verdana" OnPageIndexChanging="OnPageIndexChanging_CartaoAdulto"
                                            OnRowCommand="OnRowCommand_GridView_CartaoVacinaAdulto">
                                            <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina">
                                                    <ItemStyle Width="300px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                                    <ItemStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                                    HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                    ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                            </Columns>
                                            <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                            <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#5c9ec1" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                        <%--<asp:GridView ID="GridView_CartaoVacinaAdulto" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="Transparent" BorderColor="#ffffff"
                                            BorderStyle="None" BorderWidth="1px" Width="720px" CellPadding="3" GridLines="Horizontal"
                                            Font-Names="Verdana">
                                            <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="200px"
                                                    ItemStyle-Font-Size="10px" />
                                                <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="50px"
                                                    ItemStyle-Font-Size="10px" />
                                                <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data"
                                                    ItemStyle-Width="40px" ItemStyle-Font-Size="10px" />
                                                <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Font-Size="10px"
                                                    ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" HeaderText="Próxima Dose"
                                                    ItemStyle-Font-Size="10px" ItemStyle-Width="54px" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade" ItemStyle-Font-Size="9px"
                                                    ItemStyle-HorizontalAlign="Center" />
                                                <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                                    ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" />
                                            </Columns>
                                            <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                            <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#5c9ec1" />
                                        </asp:GridView>--%>
                                    </span>
                                </p>
                            
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Histórico de Vacinação" ScrollBars="Horizontal">
                        <ContentTemplate>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_HistoricoVacinacao" runat="server" AutoGenerateColumns="False"
                                        Width="100%" DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="White" BorderColor="#f9e5a9"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" AllowPaging="true" PageSize="10"
                                        PagerSettings-Mode="Numeric" GridLines="Horizontal" Font-Names="Verdana" OnPageIndexChanging="OnPageIndexChanging_Historico"
                                        OnRowCommand="OnRowCommand_GridView_HistoricoVacinacao">
                                        <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina">
                                                <ItemStyle Width="300px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                                <ItemStyle Width="100px" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                                HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" ItemStyle-BorderWidth="0px" />
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#5c9ec1" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                    <%--<asp:GridView ID="GridView_HistoricoVacinacao" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="CodigoVacina,CodigoDoseVacina" BackColor="Transparent" BorderColor="#ffffff"
                                        BorderStyle="None" BorderWidth="1px" Width="720px" CellPadding="3" GridLines="Horizontal"
                                        Font-Names="Verdana">
                                        <RowStyle BackColor="#5c9ec1" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="200px"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="40px"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data de Aplicação"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="50px" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                                HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Size="10px" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Width="200px" ItemStyle-Font-Size="9px" />
                                            <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" ItemStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#1c5878" Font-Bold="True" ForeColor="#ffffff"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <RowStyle CssClass="tabrow" BackColor="#4a8bae" ForeColor="#ffffff" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <PagerStyle BackColor="#5c9ec1" ForeColor="#ffffff" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#5c9ec1" />
                                    </asp:GridView>--%>
                                </span>
                            </p>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel1$GridView_CartaoVacina"
                            EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel2$GridView_CartaoVacinaAdolescente"
                            EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel3$GridView_CartaoVacinaAdulto"
                            EventName="RowCommand" />
                        <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel4$GridView_HistoricoVacinacao"
                            EventName="RowCommand" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel_Info" runat="server" Visible="false">
                            <div id="Div1" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                                height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                                moz-opacity: 0.3; opacity: 0.3">
                            </div>
                            <div id="Div2" visible="false" style="position: fixed; background-color: #092b3e;
                                background-position: center; background-repeat: no-repeat; top: 100px; left: 25%;
                                width: 500px; height: auto; z-index: 102; background-image: url('img/fundo_mensagem.png');
                                border-right: #10546f 5px solid; padding-right: 20px; border-top: #10546f 5px solid;
                                padding-left: 20px; padding-bottom: 30px; border-left: #10546f 5px solid; color: #ffffff;
                                padding-top: 20px; border-bottom: #10546f 5px solid; text-align: justify; font-family: Verdana;">
                                <div style="width: 500px; height: 10px; margin-left: 20px; margin-top: 10px;">
                                </div>
                                <br />
                                <h6>
                                    Informação sobre o imunobiológico</h6>
                                <br />
                                <br />
                                <p>
                                    <span class="rotulo">Vacina </span><span>
                                        <asp:Label ID="Label_InfoVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Dose </span><span>
                                        <asp:Label ID="Label_InfoDose" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                    </span>
                                </p>
                                <br />
                                <%--<p>
                            <span>--%>
                                <%-- <table>
                                    <tr>
                                        <th>Doenças Evitadas</th>
                                        <td>--%>
                                <fieldset style="margin: 0px; padding: 10px; border-style:dashed; border-width:1px;border-color:#367d98;">
                                    <legend style="margin: 8px; margin-left: 0px; background-color:#367d98; color:#ffffff; padding:7px; margin-left:0px; font-size:13px; font-weight:bold">Doenças Evitadas</legend><span style="font-weight: bold;
                                        font-size: x-small">
                                        <asp:Literal ID="Literal_InfoDoencas" runat="server"></asp:Literal>
                                    </span>
                                </fieldset>
                                <fieldset style="margin: 0px; padding: 10px; border-style:dashed; border-width:1px;border-color:#367d98;">
                                    <legend  style="margin: 8px; margin-left: 0px; background-color:#367d98; color:#ffffff; padding:7px; margin-left:0px; font-size:13px; font-weight:bold">Paramêtros</legend><span style="font-weight: bold;
                                        font-size: x-small">
                                        <asp:Literal ID="Literal_InfoParametros" runat="server"></asp:Literal>
                                    </span>
                                </fieldset>
                                <%--   </td>
                                    </tr>
                                </table>--%>
                                <%--<cc1:Accordion ID="Accordion_Cameras" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                    HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane_Um" runat="server">
                                            <Header>
                                                Visualizar
                                            </Header>
                                            <Content>
                                                <asp:GridView ID="GridView_InfoDoencas" runat="server" AutoGenerateColumns="False"
                                                    BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                                    Width="100%" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                    <Columns>
                                                        <asp:BoundField DataField="Nome" HeaderText="Doenças Evitadas" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                        Height="24px" Font-Size="13px" />
                                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="Label1" runat="server" Text="Nenhuma doença encontrada."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                                </asp:GridView>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>--%>
                                <%--  </span>
                        </p>--%>
                                <%-- <p>
                            <span>--%>
                                <%-- <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent">
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                    <Header>
                                        Visualizar
                                    </Header>
                                    <Content>
                                <asp:GridView ID="GridView_InfoParametros" runat="server" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                    Width="100%" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundField DataField="DescricaoParametrizacao" HeaderText="Paramêtros" />
                                    </Columns>
                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum paramêtro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                </asp:GridView>
                                     </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>--%>
                                <%--  </span>
                        </p>--%>
                                <%--<div class="botoesroll">--%>
                                <br />
                                <asp:ImageButton ID="ImageButton3" runat="server" OnClick="OnClick_FecharInformacoes"
                                    ImageUrl="~/Vacina/img/btn-fechar.png" />
                                <%-- </div>--%>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%--                <asp:Panel ID="Panel_Info" runat="server" Visible="false">
                    <div id="Div1" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                        height: 200%; z-index: 100; min-height: 100%; background-color: #999; filter: alpha(opacity=25);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="Div2" visible="false" style="position: fixed; background-color: #092b3e;
                        background-position: center; background-repeat: no-repeat; top: 125px; left: 15%;
                        width: 500px; min-height: 340px; z-index: 102; background-image: url('img/fundo_mensagem.png');
                        border-right: #10546f 5px solid; padding-right: 10px; border-top: #10546f 5px solid;
                        padding-left: 25px; padding-bottom: 25px; border-left: #10546f 5px solid; color: #ffffff;
                        padding-top: 10px; border-bottom: #10546f 5px solid; text-align: justify; font-family: Verdana;">
                        <div style="width: 500px; height: 10px; margin-left: 20px; margin-top: 10px;">
                        </div>
                        <br />
                        <h2>
                            Informação sobre o imunobiológico</h2>
                        <br />
                        <p>
                            <span class="literalgrid2">
                                <asp:Literal ID="Literal_Info" runat="server"></asp:Literal>
                            </span>
                        </p>
                        <div class="botoesroll">
                            <asp:ImageButton ID="ImageButton3" runat="server" OnClick="OnClick_Fechar" ImageUrl="~/Vacina/img/btn-fechar.png" />
                        </div>
                    </div>
                </asp:Panel>--%>
                <div class="botoesroll">
                    <br />
                     <asp:ImageButton ID="LknImprimir" runat="server" OnClick="btnCartaoVacina_Click"
                        ImageUrl="~/Vacina/img/btn-imprimecartao-padrao.png" Width="157" Height="39" />
                    <asp:ImageButton ID="LnkVoltar" runat="server" PostBackUrl="~/Vacina/FormPesquisaCartaoVacina.aspx"
                        ImageUrl="~/Vacina/img/btn-voltar.png" Width="98" Height="39" />
                </div>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
