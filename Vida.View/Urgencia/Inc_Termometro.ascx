<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_Termometro.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_Termometro" %>
<script type="text/javascript" src="FusionCharts/FusionCharts.js">
</script>
<%--<asp:Timer ID="Timer" runat="server" OnTick="OnTick_AtualizaQuadro" Interval="5000">
</asp:Timer>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Timer" EventName="Tick" />
    </Triggers>
    <ContentTemplate>--%>
        <asp:Literal ID="Literal_Quadro" runat="server"></asp:Literal>
<%--        <asp:GridView ID="GridView_TermometroAtendimento" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:ImageField HeaderText="Risco" DataImageUrlField="Imagem" DataImageUrlFormatString="~/Urgencia/img/{0}"
                    ItemStyle-HorizontalAlign="Center">
                </asp:ImageField>
                <asp:BoundField HeaderText="Quantidade" DataField="Quantidade" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <HeaderStyle CssClass="tab" />
            <RowStyle CssClass="tabrow" />
        </asp:GridView>--%>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
