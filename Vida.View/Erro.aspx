﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Erro.aspx.cs" Inherits="ViverMais.View.Erro"
    Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<style type="text/css">
<!--
.texto {
	font-family: Verdana, Geneva, sans-serif;
	font-size: 12px;
	color: #858585;
}
.texto2 {
	font-family: Verdana, Geneva, sans-serif;
	font-size: 18px;
	color: #afafaf;
}
-->
</style>
    <title></title>
</head>

<body style="background-color:#f3f3f3">
<table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="top"><p>&nbsp;</p>
      <table width="656" border="0" align="center" cellpadding="0" cellspacing="0">
      <tr>
        <td width="610"><img src="img/tela-erro.jpg" alt="Ops, Ocorreu um erro." width="656" height="337" /></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td><p class="texto2" style="margin-left:25px">Verique e reporte a mensagem de erro abaixo ao Suporte Técnico.</p></td>
      </tr>
      <tr>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td><table width="614" border="0"  cellpadding="0" cellspacing="0" style="margin-left:25px">
          <tr>
            <td><form id="form1" runat="server">
              <p><img src="img/pagina.png" width="614" height="19" alt="Página" /></p>
              <asp:label ID="Label_Pagina" runat="server" text="" CssClass="texto"></asp:label>
              <br />
              <p><img src="img/erro.png" width="614" height="16" alt="Página" /></p>
              <asp:label ID="Label_Erro" runat="server" text="" CssClass="texto"></asp:label>
              <span class="texto"></span><br />
              <p><img src="img/rastreamento.png" width="614" height="17" alt="Página" /></p>
              <asp:label ID="Label_StackTrace" runat="server" text="" CssClass="texto" Width="614px"></asp:label>
              <br />
            </form></td>
          </tr>
          <tr>
            <td>&nbsp;</td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  </tr>
</table>
</body>
</html>
<%--<asp:content id="Content1" contentplaceholderid="head" runat="server">
</asp:content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
<div>
<h2>Ooops, Ocorreu um erro...</h2>
<h3>Infelizmente ocorreu um erro na execução da operação</h3>
<p>
Por favor, Reporte ao administrador do sistema o erro ocorrido.
</p>
<p>--%>
<%--<asp:Label ID="lblMensagem" runat="server" Text=""></asp:Label>--%>
<%--</p>
</div>
</asp:content>--%>