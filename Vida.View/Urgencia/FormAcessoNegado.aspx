﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAcessoNegado.aspx.cs"
 MasterPageFile="~/Urgencia/MasterUrgencia.Master" EnableEventValidation="false" Inherits="ViverMais.View.Urgencia.FormAcessoNegado" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="contentAcesso">
    <div id="avisoespecial">
        <div id="avisoespecial-conteudo">
            <asp:Literal ID="LiteralMensagem" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>
