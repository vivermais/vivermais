﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioListaSolicitacoesAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.RelatorioListaSolicitacoesAgenda" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer_ListaSolicitacoesAgenda" runat="server" AutoDataBind="true"
            Height="50px" Width="350px" DisplayGroupTree="False" />
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
            <Report FileName="RelatoriosCrystal\CrystalReportViewer_ListaSolicitacoesAgenda.rpt">
            </Report>
        </CR:CrystalReportSource>
    </div>
    </form>
</body>
</html>
