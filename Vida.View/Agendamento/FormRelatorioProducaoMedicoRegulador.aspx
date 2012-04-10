﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" CodeBehind="FormRelatorioProducaoMedicoRegulador.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioProducaoMedicoRegulador" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top"><h2>Relatórios</h2>
        <fieldset class="formulario">
                <legend>Relatório Produção Médico Regulador</legend>
                <p>
                    <span class="rotulo">Data Inicial:</span> <span>
                        <asp:TextBox ID="tbxData_Inicial" CssClass="campo" runat="server" MaxLength="10"
                            Width="70px" ValidationGroup="VisualizarAgenda" CausesValidation="True">
                        </asp:TextBox>
                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxData_Inicial"
                            Animated="true">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                            TargetControlID="tbxData_Inicial" Mask="99/99/9999" MaskType="Date">
                        </cc1:MaskedEditExtender>
                        <asp:CompareValidator ID="CompareValidator1" Type="Date" runat="server" Font-Size="XX-Small" Operator="DataTypeCheck" ErrorMessage="Data Inválida" ControlToValidate="tbxData_Inicial"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxData_Inicial" ErrorMessage="Campo Obrigatório" ValidationGroup="Validation_VisualizaRelatorio">
                        </asp:RequiredFieldValidator>
                    </span>
                </p>
                <p>
                        <span class="rotulo">Data Final:</span> <span>
                        <asp:TextBox ID="tbxData_Final" CssClass="campo" runat="server" MaxLength="10" Width="70px"
                            ValidationGroup="Validation_VisualizaRelatorio" CausesValidation="True">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxData_Final" ErrorMessage="Campo Obrigatório" ValidationGroup="Validation_VisualizaRelatorio">
                        </asp:RequiredFieldValidator>
                        <cc1:CalendarExtender runat="server" ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="tbxData_Final"
                            Animated="true">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                            TargetControlID="tbxData_Final" Mask="99/99/9999" MaskType="Date">
                        </cc1:MaskedEditExtender>
                        <asp:CompareValidator ID="CompareValidator2" Type="Date" runat="server" Font-Size="XX-Small" Operator="DataTypeCheck" ErrorMessage="Data Inválida" ControlToValidate="tbxData_Final"></asp:CompareValidator>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Selecione Perfil:</span> <span>
                        <asp:DropDownList ID="ddlPerfis" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlPerfis_OnSelectedIndexChanged" AutoPostBack="true"
                            DataValueField= "Codigo" DataTextField="Nome">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                            ControlToValidate="ddlPerfis" InitialValue="0" ErrorMessage="Campo Obrigatório" ValidationGroup="Validation_VisualizaRelatorio">
                        </asp:RequiredFieldValidator>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Médico Regulador:</span> <span>
                        <asp:DropDownList ID="ddlMedicoRegulador" runat="server" CssClass="drop">
                        </asp:DropDownList>
                    </span>
                </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="btnGeraRelatorio" Text="Imprimir" runat="server" ValidationGroup="Validation_VisualizaRelatorio"
                        CausesValidation="true" OnClick="btnGeraRelatorio_Click">
                        <img id="imgGerar" alt="Imprimir" src="img/imprimir_1.png" 
                        onmouseover="imgGerar.src='img/imprimir_2.png';"
                        onmouseout="imgGerar.src='img/imprimir_1.png';" />
                    </asp:LinkButton>
                </div>
                <div class="botoesroll">
                    <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                </div>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>