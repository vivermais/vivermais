﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormFeriado.aspx.cs" Inherits="ViverMais.View.Agendamento.FormFeriado"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Feriado</h2>
        <fieldset class="formulario">
            <legend>Cadastrar Feriado</legend>
            <p>
                <span class="rotulo">Data:</span> <span>
                    <asp:TextBox ID="tbxData" CssClass="campo" runat="server"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                        TargetControlID="tbxData" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender runat="server" ID="data" Format="dd/MM/yyyy" TargetControlID="tbxData"
                        Animated="true">
                    </cc1:CalendarExtender>
                    <asp:CompareValidator Type="Date" runat="server" Font-Size="XX-Small" Operator="DataTypeCheck" ErrorMessage="Data Inválida" ControlToValidate="tbxData"></asp:CompareValidator>
                    <asp:Label ID="lblCriticaData" runat="server" Text="Campo Obrigatório" Font-Size="X-Small"
                        ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server" 
                ControlToValidate="tbxData" ErrorMessage="Campo Obrigatório" 
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </p>
            <p>
                <span class="rotulo">Tipo de Feriado:</span> <span>
                    <asp:DropDownList ID="ddlFeriado" runat="server" CssClass="drop">
                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                        <asp:ListItem Value="E">Estadual</asp:ListItem>
                        <asp:ListItem Value="M">Municipal</asp:ListItem>
                        <asp:ListItem Value="N">Nacional</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="lblCriticaTipo" runat="server" Text="Campo Obrigatório" Font-Size="XX-Small"
                        ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server" 
                ControlToValidate="ddlFeriado" ErrorMessage="Campo Obrigatório" 
                SetFocusOnError="True" Display="Dynamic" InitialValue="0"></asp:RequiredFieldValidator></span>
            </p>
            <p>
                <span class="rotulo">Descrição:</span> <span>
                    <asp:TextBox ID="tbxDescricao" CssClass="campo" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                    <asp:Label ID="lblCriticaDescrição" runat="server" Text="Campo Obrigatório" Font-Size="X-Small"
                        ForeColor="Red"></asp:Label>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="tbxDescricao" Font-Size="XX-Small" ErrorMessage="Campo Obrigatório" 
                SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator></span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="Incluir" runat="server" OnClick="Incluir_Click" CausesValidation="true">
                       <img id="img_incluir" alt="" src="img/salvar_1.png"
                onmouseover="img_incluir.src='img/salvar_2.png';"
                onmouseout="img_incluir.src='img/salvar_1.png';" />
                </asp:LinkButton></div>
            <div class="botoesroll">
                <asp:LinkButton ID="Voltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx" CausesValidation="false">
                       <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
                </asp:LinkButton></div>
            
        </fieldset>
        </div>
</asp:Content>