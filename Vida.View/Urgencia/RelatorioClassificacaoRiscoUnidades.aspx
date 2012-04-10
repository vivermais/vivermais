<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioClassificacaoRiscoUnidades.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioClassificacaoRiscoUnidades" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<div id="top">
    <h2>ATENDIMENTO</h2>
    <fieldset>
        <legend>Relação</legend>
        <p>
            <span>
                <asp:GridView ID="GridView_QuadroClassificacao" runat="server" DataKeyNames="CNES"
                    OnRowDataBound="OnRowDataBound_FormataGridViewClassificacao" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField HeaderText="Unidade" DataField="NomeFantasia" />
                        <asp:TemplateField HeaderText="Quadro de Classificação">
                            <ItemTemplate>
                                <cc1:Accordion ID="Accordion_Classificacao" runat="server" SelectedIndex="-1"
                                 RequireOpenedPane="false" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                 ContentCssClass="accordionContent">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane_Um" runat="server">
                                            <Header>
                                                Visualizar
                                            </Header>
                                            <Content>
                                                <asp:GridView ID="GridView_ClassificacaoRisco" Width="250px" runat="server"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Risco">
                                                            <ItemTemplate>
                                                                <asp:Image ID="Image_Classificacao" runat="server"
                                                                ImageUrl='<%#Eval("ClassificacaoRisco","~/Urgencia/img/{0}") %>' Width="32px" Height="32px"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="QtdPaciente" HeaderText="Quantidade de Pacientes" />
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
    <%--            <asp:DataList ID="DataList_ClassificacaoRisco" runat="server" OnItemDataBound="OnItemDataBound_FormataDataList"
                 DataKeyField="Codigo">
                    <ItemTemplate>
                        <asp:Label ID="lbUnidade" runat="server" Text='<%#bind("NomeFantasia") %>'></asp:Label>
                        <asp:GridView ID="GridView_ClassificacaoRisco" runat="server" OnRowDataBound="OnRowDataBound_FormataGridView"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Classificação de Risco">
                                    <ItemTemplate>
                                        <asp:Image ID="Image_Classificacao" runat="server" ImageUrl='<%#bind("ClassificacaoRisco") %>' Width="32px" Height="32px"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="QtdPaciente" HeaderText="Quantidade de Pacientes" />
                            </Columns>
                        </asp:GridView>
                    </ItemTemplate>
                </asp:DataList>--%>
            </span>
        </p>
    </fieldset>
</div>
</asp:Content>
