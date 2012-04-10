<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpressaoEtiqueta.aspx.cs"
    Inherits="ViverMais.View.Paciente.ImpressaoEtiqueta" Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <div style="margin-left:40px" matg>
    <form id="form1" runat="server">
    <br />
    <asp:Label ID="lblCartao" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
        Font-Size="Small" Width="100%"></asp:Label><br />
    <asp:Label ID="lblNome" runat="server" Text="Label" Font-Bold="True" Font-Names="Arial"
        Font-Size="12px" Width="100%"></asp:Label><br />
    <asp:Label ID="lblData" runat="server" Text="Label" Font-Names="Arial" Font-Size="11px" Font-Bold="True"></asp:Label>
    <br />
    <asp:Image ID="imgBarCode" runat="server" Width="250px" Height="29px" />
    </form>
    </div>
</body>
</html>
