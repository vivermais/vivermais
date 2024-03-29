﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormQuadroEntregaExame.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormQuadroEntregaExame" EnableEventValidation="false" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="vertical-align:bottom">
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Timer ID="Timer_VerificarEntregaExame" runat="server" Interval="100000"
            OnTick="OnTick_VerificarEntregaExame"
            EnableViewState="false">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer_VerificarEntregaExame" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server"  OnClientClick="javascript:window.open('FormConfirmarBaixaExame.aspx');">
                    <asp:Image ID="Image_BotaoExames" runat="server" Width="187px" Height="45px" ImageUrl="~/Urgencia/img/resultado-exame-estatico.gif"   />
                </asp:LinkButton>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
