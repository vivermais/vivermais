﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirMovimentoRemanejamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormImprimirMovimentoRemanejamento" MasterPageFile="~/Farmacia/MasterRelatorioFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <CR:CrystalReportViewer ID="CrystalReportViewer_Relatorio" runat="server" AutoDataBind="true"
     DisplayGroupTree="False" />
</asp:Content>