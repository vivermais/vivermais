<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioProcedencia.aspx.cs"
 Inherits="ViverMais.View.Urgencia.RelatorioProcedencia" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer_Procedencia" runat="server" AutoDataBind="true"
     DisplayGroupTree="False" />
</asp:Content>