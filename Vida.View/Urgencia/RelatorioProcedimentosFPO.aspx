﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioProcedimentosFPO.aspx.cs" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" Inherits="ViverMais.View.Urgencia.RelatorioProcedimentosFPO" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer_Procedimento" runat="server" AutoDataBind="true"
     DisplayGroupTree="False" />
</asp:Content>