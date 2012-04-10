<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirSenhaRecepcao.aspx.cs"
    Inherits="ViverMais.View.Atendimento.FormImprimirSenhaRecepcao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impressão de Senha</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center; width: 210px;">
        <asp:Literal ID="Literal_Senha" runat="server"></asp:Literal>
    </div>

    <script type="text/javascript">
        window.print();
    </script>
    </form>
</body>
</html>
