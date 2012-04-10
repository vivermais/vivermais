﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormNewsLetter.aspx.cs" Inherits="ViverMais.View.SiteViverMais.FormNewsLetter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href ="estilos.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
    function FxApagar(campo){
        if (campo.value == "Digite o seu nome")
        {
            campo.style.backgroundColor ="#f8feff";
            campo.value="";
        }


        if (campo.value == "Digite o seu e-mail")
        {
            campo.style.backgroundColor ="#f8feff";
            campo.value="";
        }
    }

    function FxPreencher(campo, texto){
        if (campo.value == "")
        {
            campo.style.backgroundColor = "#e2f9ff";
            campo.value="Digite o seu "+texto;
        }
    }


</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="formesp"><asp:TextBox ID="tbxNome" CssClass="forms" runat="server" Text="Digite o seu nome" Width="200px"></asp:TextBox>       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="Digite o seu nome"            ControlToValidate="tbxNome" ErrorMessage="Informe seu nome"></asp:RequiredFieldValidator>--%>        </div>
        <div class="formesp">
       <asp:TextBox ID="tbxEmail" CssClass="forms" runat="server" Text="Digite o seu e-mail" Width="200px"></asp:TextBox>       <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="Digite o seu e-mail"            ControlToValidate="tbxEmail" ErrorMessage="Informe seu e-mail"></asp:RequiredFieldValidator>--%>          </div>
        <asp:Button CssClass="botao" ID="btnConfirmar" runat="server" Text="Confirmar"             onclick="btnConfirmar_Click" />
          
    
    
    </form>
</body>
</html>
