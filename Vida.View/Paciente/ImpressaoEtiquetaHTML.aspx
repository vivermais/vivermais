<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpressaoEtiquetaHTML.aspx.cs"
    Inherits="ViverMais.View.Paciente.ImpressaoEtiquetaHTML" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
        .style1
        {
            height: 479px;
        }
    </style>
</head>
<body>
    <%--<div style="margin-left:0px">--%>
    <form id="form1" runat="server">
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                <asp:Label ID="lblCartao" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
                    Font-Size="Small" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblNome" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12px" Width="100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblData" runat="server" Text="Label" Font-Names="Arial" Font-Size="11px"
                    Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="imgBarCode" runat="server" />
            </td>
        </tr>
    </table>
    </form>
    <%--</div>--%>
</body>
</html>
