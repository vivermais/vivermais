﻿<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="FormExame.aspx.cs" Inherits="ViverMais.View.Urgencia.FormExame" Title="ViverMais - Cadastro de Exames" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>Formulário de Cadastro de Exames Internos</h2>
    <fieldset class="formulario">
        <legend>Dados do Exame</legend>
        <p>
            <span class="rotulo">Descrição:</span> 
            <span style="margin-left: 5px;">
                <asp:TextBox ID="tbxNome" CssClass="campo" runat="server" Width="400px" MaxLength="100"></asp:TextBox>
            </span>
        </p>
        <p>
            <span class="rotulo">Status</span>
            <span class="camporadio">
                <asp:RadioButton ID="RadioButton_Ativo" runat="server" CssClass="camporadio" Width="20px" GroupName="GroupName_Situacao" Checked="true" />Ativo
                <asp:RadioButton ID="RadioButton_Inativo" runat="server" CssClass="camporadio" Width="20px" GroupName="GroupName_Situacao"/>Inativo
            </span>
        </p>
        <p align="center">
            <span>
                <asp:ImageButton ID="btnSalvar" runat="server" ValidationGroup="ValidationGroup_Exame"
                onclick="btnSalvar_Click" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png" Width="134px" Height="38px" />
                <asp:ImageButton ID="Button_Cancelar" runat="server" PostBackUrl="~/Urgencia/FormExibeExame.aspx"
                 ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png" Width="134px" Height="38px" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Descrição é Obrigatório!" ControlToValidate="tbxNome" Display="None" ValidationGroup="ValidationGroup_Exame"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Exame"/>
            <p>
            </span>
        </p>
    </fieldset>
    </div>
<%--    </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
