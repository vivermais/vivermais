<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormTermometro.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormTermometro" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:Timer ID="Timer" runat="server" OnTick="OnTick_AtualizaQuadro" Interval="30">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:GridView ID="GridView_TermometroAtendimento" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:ImageField HeaderText="Risco" DataImageUrlField="Imagem" DataImageUrlFormatString="~/Urgencia/img/{0}"
                            ItemStyle-HorizontalAlign="Center">
                        </asp:ImageField>
                        <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
