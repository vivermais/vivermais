﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Apagarduplicatas.aspx.cs" Inherits="ViverMais.View.Vacina.Apagarduplicatas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=1">Retirar Duplicatas na mesma dispensação</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=2">Retirar Duplicatas em dispensações diferentes</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=3">Apagar dispensações sem itens</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton4" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=4">Atualizar Estrátegia com Item Dispensado</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton5" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=5">Inserindo Cartão de Vacina</asp:LinkButton><br />
        <asp:LinkButton ID="LinkButton6" runat="server" PostBackUrl="~/Vacina/Apagarduplicatas.aspx?opcao=6">Associando Usuários e Responsáveis pelas Salas de Vacinas</asp:LinkButton><br />
    </div>
    </form>
</body>
</html>
