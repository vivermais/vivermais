﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirMedicamentosAprazados.aspx.cs" Inherits="ViverMais.View.Urgencia.FormImprimirMedicamentosAprazados"
 EnableEventValidation="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CrystalReportViewer_MedicamentosAprazados" runat="server"
            AutoDataBind="true" DisplayGroupTree="False" />
    </div>
    </form>
</body>
</html>
