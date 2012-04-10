﻿<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="BuscaProntuario.aspx.cs" Inherits="ViverMais.View.Urgencia.BuscaProntuario"
    Title="ViverMais - Módulo Urgência - Busca de Prontuários" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Paciente/WUCPesquisarPaciente.ascx" TagName="TagNamePesquisarPaciente"
    TagPrefix="TagPrefixPesquisarPaciente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formulario3
        {
            width: 500px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 5px 5px 10px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário para Busca de Registro Eletrônico</h2>
        <br />
        <cc1:TabContainer ID="TabContainer1" runat="server">
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Por Paciente">
                <ContentTemplate>
                    <TagPrefixPesquisarPaciente:TagNamePesquisarPaciente ID="WUC_PesquisarPaciente" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Panel ID="Panel_ResultadoPaciente" runat="server" Visible="false">
                                <fieldset class="formulario">
                                    <legend>Resultado da Busca de Paciente</legend>
                                    <p>
                                        <span>
                                            <asp:GridView ID="GridView_ResultadoPesquisaPaciente" DataKeyNames="Codigo" Width="700px"
                                                AutoGenerateColumns="false" runat="server" OnRowCommand="OnRowCommand_VerProntuarios">
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Link" HeaderText="Nome" CommandName="CommandName_VerProntuarios"
                                                        DataTextField="Nome" />
                                                    <asp:BoundField HeaderText="Nome da Mãe" DataField="NomeMae" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                </Columns>
                                                <HeaderStyle CssClass="tab" />
                                                <RowStyle CssClass="tabrow" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </span>
                                    </p>
                                </fieldset>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Outras opções">
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Outras opções</legend>
                        <p>
                            <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent">
                                <HeaderTemplate>
                                </HeaderTemplate>
                                <ContentTemplate>
                                </ContentTemplate>
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                        <Header>
                                            Por Número de Atendimento</Header>
                                        <Content>
                                            <p>
                                                <span class="rotulo">Número:</span> <span>
                                                    <asp:TextBox ID="tbxNumero" CssClass="campo" runat="server" Height="16px" Width="90px"
                                                        MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                                        ControlToValidate="tbxNumero" ErrorMessage="Número é Obrigatório." Display="None"
                                                        ValidationGroup="Numero"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Font-Size="XX-Small"
                                                        runat="server" ErrorMessage="Digite somente números." ValidationGroup="Numero"
                                                        Display="None" ValidationExpression="\d*" ControlToValidate="tbxNumero"></asp:RegularExpressionValidator>
                                                </span>
                                            </p>
                                            <p>
                                                <span>
                                                    <asp:ImageButton ID="btnPesquisar" runat="server" OnClick="BtnPesquisar_Click" ImageUrl="~/Urgencia/img/bts/btn_buscar1.png"
                                                        Width="134px" Height="38px" ValidationGroup="Numero" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Numero" />
                                                </span>
                                            </p>
                                        </Content>
                                    </cc1:AccordionPane>
                                    <cc1:AccordionPane ID="AccordionPane2" runat="server">
                                        <Header>
                                            Data e Situação
                                        </Header>
                                        <Content>
                                            <p>
                                                <span class="rotulo">Data:</span> <span>
                                                    <asp:TextBox ID="tbxData" CssClass="campo" runat="server" Height="18px" Width="70px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbxData" Format="dd/MM/yyyy"
                                                        PopupButtonID="calendar_icon.png" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbxData"
                                                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                                    </cc1:MaskedEditExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxData"
                                                        ErrorMessage="Data é Obrigatório!" Display="None" ValidationGroup="Situacao">*</asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                                                        Display="None" ErrorMessage="Data Inválida!" ControlToValidate="tbxData" Operator="DataTypeCheck"
                                                        Type="Date" ValidationGroup="Situacao"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                                                        Display="None" ErrorMessage="Data deve ser maior que 01/01/1900!" ControlToValidate="tbxData"
                                                        Type="Date" Operator="GreaterThan" ValueToCompare="01/01/1900" ValidationGroup="Situacao"></asp:CompareValidator>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Situação:</span> <span>
                                                    <asp:DropDownList ID="ddlSituacao" CssClass="drop" runat="server" Height="22px" Width="229px">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator6" Display="None" Font-Size="XX-Small"
                                                        runat="server" ErrorMessage="Selecione uma situação." ControlToValidate="ddlSituacao"
                                                        Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Situacao"></asp:CompareValidator>
                                                </span>
                                            </p>
                                            <p>
                                                <span>
                                                    <asp:ImageButton ID="btnPesquisar2" runat="server" OnClick="btnPesquisar2_Click"
                                                        ImageUrl="~/Urgencia/img/bts/btn_buscar1.png" Width="134px" Height="38px" ValidationGroup="Situacao" />
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Situacao" />
                                                </span>
                                            </p>
                                        </Content>
                                    </cc1:AccordionPane>
                                    <cc1:AccordionPane ID="AccordionPane3" runat="server">
                                        <Header>
                                            Data e Classificação de Risco
                                        </Header>
                                        <Content>
                                            <p>
                                                <span class="rotulo">Data:</span> <span>
                                                    <asp:TextBox ID="tbxData2" CssClass="campo" runat="server" Height="18px" Width="70px"></asp:TextBox>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbxData2"
                                                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbxData2" Format="dd/MM/yyyy"
                                                        PopupButtonID="calendar_icon.png" runat="server">
                                                    </cc1:CalendarExtender>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Display="None" Font-Size="XX-Small"
                                                        runat="server" ControlToValidate="tbxData2" ErrorMessage="Data é Obrigatório!"
                                                        ValidationGroup="Consultorio"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
                                                        Display="None" ErrorMessage="Data Inválida!" ControlToValidate="tbxData2" Operator="DataTypeCheck"
                                                        Type="Date" ValidationGroup="Consultorio"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator4" Font-Size="XX-Small" runat="server"
                                                        Display="None" ErrorMessage="Data deve ser maior que 01/01/1900!" ControlToValidate="tbxData2"
                                                        Type="Date" Operator="GreaterThan" ValueToCompare="01/01/1900" ValidationGroup="Consultorio"></asp:CompareValidator>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Classificação de Risco:</span> <span>
                                                    <asp:DropDownList ID="ddlSituacaoRisco" runat="server" CssClass="drop">
                                                    </asp:DropDownList>
                                                    <asp:CompareValidator ID="CompareValidator5" Display="None" Font-Size="XX-Small"
                                                        runat="server" ErrorMessage="Selecione uma classificação de risco." ControlToValidate="ddlSituacaoRisco"
                                                        Operator="GreaterThan" ValueToCompare="0" ValidationGroup="Consultorio"></asp:CompareValidator>
                                                </span>
                                            </p>
                                            <p>
                                                <span>
                                                    <asp:ImageButton ID="btnPesquisar3" runat="server" OnClick="BtnPesquisar3_Click"
                                                        ImageUrl="~/Urgencia/img/bts/btn_buscar1.png" Width="134px" Height="38px" ValidationGroup="Consultorio" />
                                                    <asp:ValidationSummary ID="ValidationSummary_Dois" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="Consultorio" />
                                                </span>
                                            </p>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <asp:UpdatePanel ID="UpdatePanel_Prontuarios" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Panel ID="Panel_ResultadoBusca" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Registros Eletrônicos Pesquisados</legend>
                        <p>
                            <span>
                                <asp:GridView ID="gridProntuario" AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric"
                                    OnPageIndexChanging="OnPageIndexChanging_PaginacaoGridViewProntuarios" runat="server" DataKeyNames="Codigo"
                                    AutoGenerateColumns="False" Width="100%">
                                    <Columns>
                                        <%--<asp:BoundField DataField="Codigo" HeaderText="Identificador" Visible="false" />--%>
                                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" Target="_blank" DataNavigateUrlFormatString="FormMostrarHistoricoProntuario.aspx?codigo={0}"
                                            DataTextField="NumeroToString" HeaderText="Número" ItemStyle-Width="100px">
                                            <ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>
                                        </asp:HyperLinkField>
                                        <asp:BoundField DataField="NomePacienteToString" HeaderText="Paciente" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="PacienteDescricao" HeaderText="Descrição" ItemStyle-Width="400px" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Situacao" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="tab" />
                                    <RowStyle CssClass="tabrow_left" />
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
