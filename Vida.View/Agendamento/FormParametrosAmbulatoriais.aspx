﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormParametrosAmbulatoriais.aspx.cs" Inherits="ViverMais.View.Agendamento.FormParametrosAmbulatoriais"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Parâmetros Ambulatóriais</h2>
        <fieldset class="formulario">
            <legend>Parâmetros da Agenda</legend>
            <p>
                <span>&nbsp;</span>
            </p>
            <p>
                <span class="rotulo">Mínimo Dias Abertura Agenda</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxMin_Dias" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxMin_Dias" ClearMaskOnLostFocus="true" InputDirection="LeftToRight" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxMin_Dias"
                    Display="Dynamic" ErrorMessage="RequiredFieldValidator1" Font-Size="XX-Small">Campo Obrigatório</asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">Máximo Dias Abertura Agenda</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxMax_Dias" runat="server" InputDirection="LeftToRight" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxMax_Dias" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxMax_Dias"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">Mínimo Dias Fila Espera</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxMin_Dias_Espera" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxMin_Dias_Espera" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxMin_Dias_Espera"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator>
            </p>
            <p>
                <span class="rotulo">Percentual Vagas Fila</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxPct_Vagas_Espera" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxPct_Vagas_Espera" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="tbxPct_Vagas_Espera"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">Mín Dias Cancelamento</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxMin_Dias_Cancela" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxMin_Dias_Cancela" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="tbxMin_Dias_Cancela"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">Mín Dias Reaproveitamento</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxMin_Dias_Reaproveita" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxMin_Dias_Reaproveita" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxMin_Dias_Reaproveita"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">Val. Código Controle (dias)</span> <span>
                    <asp:TextBox CssClass="campo" ID="tbxValidade_Codigo" runat="server" Width="100px"></asp:TextBox></span>
            </p>
            <p>
                <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="999" MaskType="Number"
                    TargetControlID="tbxValidade_Codigo" InputDirection="LeftToRight" ClearMaskOnLostFocus="true" AutoComplete="false">
                </cc1:MaskedEditExtender>
            </p>
            <br />
            <div class="botoesroll">
                <asp:LinkButton ID="lknSalvar" runat="server" OnClick="btnSalvar_Click">
                  <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  onmouseover="imgsalvar.src='img/salvar_2.png';"
                  onmouseout="imgsalvar.src='img/salvar_1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                    CausesValidation="False">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
    </div>
</asp:Content>
