<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImprimeLaudo.aspx.cs" Inherits="ViverMais.View.Agendamento.ImprimeLaudo" %>

<%@ Register Assembly="SpiceLogicBLOB" Namespace="SpiceLogic.BLOBControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>

<script language='javascript' type="text/javascript"> 

    function CloseWindow()
    {
        ww = window.open(window.location, "_self");ww.close();
    }
    
    function fechaJanela()
    {
        //fecha janela depois 5 segundos
        setTimeout("window.close()",5000);
    }
</script>

<body onload="fechaJanela()">
    <%--<cc1:BlobImage ID="BlobImage1" runat="server" Width="100%" Height="58%" ThumbnailDisplay-KeepAspectRatio="true">
    </cc1:BlobImage>
    <asp:Image ID="Image1" runat="server" Visible="true" Width="100%" Height="58%" />--%>
</body>
</html>
