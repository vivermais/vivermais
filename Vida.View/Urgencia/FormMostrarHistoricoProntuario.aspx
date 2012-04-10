﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMostrarHistoricoProntuario.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormMostrarHistoricoProntuario" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/Urgencia/Inc_MenuHistorico.ascx" TagName="Inc_MenuHistorico"
    TagPrefix="IMH" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Informações do Atendimento</h2>
        <fieldset class="formulario">
            <legend>Dados do Atendimento</legend>
            <p>
                <span class="rotulo">N°:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblData" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label></span></p>
            <p>
                <span class="rotulo">Paciente:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Histórico</legend>
            <p>
                <span>
                    <IMH:Inc_MenuHistorico ID="Inc_MenuHistorico" runat="server"></IMH:Inc_MenuHistorico>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
