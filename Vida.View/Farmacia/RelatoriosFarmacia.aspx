<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true"
    CodeBehind="RelatoriosFarmacia.aspx.cs" Inherits="ViverMais.View.Farmacia.RelatoriosFarmacia"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:Accordion ID="AccordionRelatorio" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
        ContentCssClass="accordionContent">
        <HeaderTemplate>
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    Relatório de Movimentação Diária</Header>
                <Content>
                    <p>
                        <span class="rotulo">Farmacia</span> <span>
                            <asp:DropDownList ID="ddlFarmaciaMovimentacao" runat="server">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data</span> <span>
                            <asp:TextBox ID="tbxDataMovimentacao" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbxDataMovimentacao"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                <Header>
                    Relatório de Posição de Estoque por Lote</Header>
                <Content>
                    <p>
                        <span class="rotulo">Farmacia</span> <span>
                            <asp:DropDownList ID="ddlFarmaciaPosicaoEstoqueLote" runat="server">
                            </asp:DropDownList>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane3" runat="server">
                <Header>
                    Relatório de Lotes de Medicamentos a Vencer</Header>
                <Content>
                    <p>
                        <span class="rotulo">Farmacia</span> <span>
                            <asp:DropDownList ID="ddlFarmaciaLoteAVencer" runat="server">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data</span> <span>
                            <asp:TextBox ID="tbxDataValidadeAVencer" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbxDataValidadeAVencer"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane4" runat="server">
                <Header>
                    Relatório de Consumo Médio Mensal</Header>
                <Content>
                    <p>
                        <span class="rotulo">Farmacia</span> <span>
                            <asp:DropDownList ID="ddlFarmaciaConsumoMedioMensal" runat="server">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data Inicial</span> <span>
                            <asp:TextBox ID="tbxDataInicialConsumoMedioMensal" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="tbxDataInicialConsumoMedioMensal"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data Final</span> <span>
                            <asp:TextBox ID="tbxDataFinalConsumoMedioMensal" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender4" TargetControlID="tbxDataFinalConsumoMedioMensal"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane5" runat="server">
                <Header>
                    Relatório de Produção por Usuário</Header>
                <Content>
                    <p>
                        <span class="rotulo">Farmacia</span> <span>
                            <asp:DropDownList ID="ddlFarmaciaProducaoUsuario" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Usuário</span>
                        
                        <span>
                            <asp:DropDownList ID="ddlUsuarioProducaoUsuario" runat="server">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data</span> 
                        <span>
                            <asp:TextBox ID="tbxDataProducaoUsuario" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender6" TargetControlID="tbxDataProducaoUsuario"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane6" runat="server">
                <Header>Relatório de Consolidado de RM</Header>
                <Content>
                    <p>
                        <span class="rotulo">Distrito</span> <span>
                            <asp:DropDownList ID="ddlDistritoConsolidadoRM" runat="server" AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Mês</span> <span>
                            <asp:TextBox ID="tbxMesConsolidadoRM" MaxLength="2" runat="server" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Ano</span> <span>
                            <asp:TextBox ID="tbxAnoConsolidadoRM" MaxLength="4" runat="server" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane7" runat="server">
                <Header>
                    Relatório de Nota Fiscal por Lote</Header>
                <Content>
                    <p>
                        <span class="rotulo">Número do Lote</span> <span>
                            <asp:TextBox ID="tbxNumeroLote" runat="server" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane8" runat="server">
                <Header>
                    Relatório de Valor Unitário por Medicamento</Header>
                <Content>
                    <p>
                        <span class="rotulo">Data Inicial</span> <span>
                            <asp:TextBox ID="tbxDataInicialValorUnitMedicamento" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender7" TargetControlID="tbxDataInicialValorUnitMedicamento"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data Final</span> <span>
                            <asp:TextBox ID="tbxDataFinalValorUnitMedicamento" runat="server" CssClass="campo"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="tbxDataFinalValorUnitMedicamento"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                </Content>
            </cc1:AccordionPane>
        </Panes>
    </cc1:Accordion>
    <asp:ImageButton ID="btnpesquisar" runat="server" OnClick="btnpesquisar_Click" />
</asp:Content>
