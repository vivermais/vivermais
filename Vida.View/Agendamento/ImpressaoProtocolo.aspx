﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImpressaoProtocolo.aspx.cs"
    Inherits="ViverMais.View.Agendamento.ImpressaoProtocolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <fieldset style="width: 210px; font-family:Arial; border:none;">
            <legend style="font-size:13px; font-weight:bold;">Protocolo de Solicitação</legend>
            <br />
            <div align="center">
                <asp:Label ID="lblViverMais" runat="server" Font-Bold="True" Font-Size="12px"
                    Text="ViverMais - Sistema Integrado de Saúde"></asp:Label>
                <br />
            <asp:Label ID="lblPrefeitura" runat="server" Font-Bold="True" Text="Prefeitura do Salvador"
                Font-Size="12px"></asp:Label>
            <br />
                <asp:Label ID="lblSecretaria" runat="server" Font-Bold="True" Text="Secretaria Municial da Saúde"
                    Font-Size="12px"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lblProtocolo" runat="server" Font-Bold="True" Text="Protocolo:" Font-Size="12px"></asp:Label>            <br />
                <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Font-Size="22px" Font-Names="Verdana"></asp:Label>
            </div>
            <br />
            <div style="font-size:12px;">
            Nome:<br />
            <asp:Label ID="lblPaciente" runat="server" Font-Bold="True"></asp:Label>
            <br />
            Cartão Sus:<br />
            <asp:Label ID="lblCartaoSus" runat="server" Font-Bold="true" ></asp:Label>
            <br /><br /><br />
            Data:<asp:Label ID="lblData" runat="server" Font-Bold="True"></asp:Label>
            <br /><br /><br />
            Procedimento:<br />
            <asp:Label ID="lblProcedimento" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <p style="font-size:10px; font-weight:bold; text-align: center;">Acesse:<br /> www.saude.salvador.ba.gov.br/ViverMais</p><p style="font-size:9px; font-weight:bold; text-align: center;">ou<br /> Compareça a unidade de saúde mais próxima para acompanhamento da sua solicitação.</p>
            </div>
        </fieldset>
    </div>
    </form>
</body>
</html>
