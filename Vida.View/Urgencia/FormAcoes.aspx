﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAcoes.aspx.cs" Inherits="ViverMais.View.Urgencia.FormAcoes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:GridView ID="GridView_Medicos" runat="server" AutoGenerateColumns="false" Width="100%" >
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Especialidade" HeaderText="Especialidade" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="GridView_Enfermeiros" runat="server" AutoGenerateColumns="false" Width="100%" >
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Especialidade" HeaderText="Especialidade" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:GridView ID="GridView_Tecnicos" runat="server" AutoGenerateColumns="false" Width="100%" >
        <Columns>
            <asp:BoundField DataField="Nome" HeaderText="Nome" />
            <asp:BoundField DataField="Especialidade" HeaderText="Especialidade" />
        </Columns>
    </asp:GridView>
    </form>
</body>
</html>