<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTermometroAtendimento.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormTermometroAtendimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="refresh" CONTENT="2" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView_TermometroAtendimento" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:ImageField HeaderText="Risco" DataImageUrlField="Imagem"
                 DataImageUrlFormatString="~/Urgencia/img/{0}" ItemStyle-HorizontalAlign="Center"></asp:ImageField>
                <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <HeaderStyle CssClass="tab" />
            <RowStyle CssClass="tabrow" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
