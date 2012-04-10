﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioVagasUnidades.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioVagasUnidades" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div id="top">
    <h2>NÚMERO DE LEITOS</h2>
    <fieldset>
        <legend>Relação</legend>
        <p>
            <span>
                <asp:GridView ID="GridView_QuadroVagas" runat="server" AutoGenerateColumns="false" 
                    DataKeyNames="CNES" OnRowDataBound="OnRowDataBound_FormataGridView" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="NomeFantasia" HeaderText="Unidade" />
                        <asp:TemplateField HeaderText="Quadro de Vagas">
                            <ItemTemplate>
                                <cc1:Accordion ID="Accordion_Vagas" runat="server" SelectedIndex="-1"
                                    RequireOpenedPane="false" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent" Width="100%">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane_Um" runat="server">
                                            <Header>Visualizar</Header>
                                            <Content>
                                                <asp:GridView ID="GridView_Vagas" AutoGenerateColumns="false" runat="server" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Tipo" DataField="TipoVaga" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Número total de leitos" DataField="Vaga" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Número de leitos ocupados"  DataField="VagaOcupada"  ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField HeaderText="Número de leitos livres" DataField="VagaLivre" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <HeaderStyle CssClass="tab" />
                                                    <RowStyle CssClass="tabrow" />
                                                </asp:GridView>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow" />
                </asp:GridView>
                
<%--                <asp:DataList ID="DataList_Vagas" runat="server" DataKeyField="Codigo"
                OnItemDataBound="OnItemDataBound_FormataDataList">
                    <ItemTemplate>
                        <asp:Label ID="lbQuadroVagas" runat="server" Text='<%#bind("NomeFantasia") %>'></asp:Label>
                        <asp:GridView ID="GridView_Vagas" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:BoundField HeaderText="Tipo" DataField="TipoVaga" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Número de leitos disponíveis" DataField="Vaga" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Número de leitos ocupados"  DataField="VagaOcupada"  ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:DataList>--%>
            </span>
        </p>
    </fieldset>
</div>
</asp:Content>