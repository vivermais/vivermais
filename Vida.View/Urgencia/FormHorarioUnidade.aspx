﻿<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="FormHorarioUnidade.aspx.cs" Inherits="ViverMais.View.Urgencia.FormHorarioUnidade" Title="ViverMais - Horário de Funcionamento de Unidade" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div id="top">
        <h2>Horário de Vigência da Prescrição</h2>
    
    <fieldset class="formulario">
        <legend>Informações</legend>
        <p>
            <span class="rotulo">Unidade:</span> <span style="margin-left: 5px;">
                <asp:Label ID="lblUnidade" runat="server" Font-Bold="True" 
                ForeColor="Maroon"></asp:Label>
            </span>
        </p>
        <p>
            <span class="rotulo">Horário Atual:</span>
            <span style="margin-left: 5px;">
                <asp:Label ID="lblHorarioAtual" runat="server" Text="" Font-Bold="true"></asp:Label>
                </span>
        </p>
        <p>
            <span class="rotulo">Horário:</span> 
            <span style="margin-left: 5px;">
                <asp:TextBox ID="tbxHorario" CssClass="campo" runat="server" Width="50px"></asp:TextBox>
                 <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbxHorario"
                    Mask="99:99" MaskType="Time" InputDirection="LeftToRight"></cc1:MaskedEditExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbxHorario"
                    ValidationGroup="ValidationGroup_HorarioUnidade" SetFocusOnError="true">
                </asp:RequiredFieldValidator>
                </span>
        </p>
        <p>
            <span>
                <asp:ImageButton ID="btnSalvar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png"
                    Width="134px" Height="38px" ValidationGroup="ValidationGroup_HorarioUnidade"
                    onclick="btnSalvar_Click" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                    ValidationGroup="ValidationGroup_HorarioUnidade" ShowSummary="false" />
            </span>
        </p>
    </fieldset>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
