﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioIndeferimentoSolicitacao.aspx.cs" Inherits="ViverMais.View.Agendamento.RelatorioIndeferimentoSolicitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
      <form id="form1" runat="server">
    <div>
        <fieldset style="width: 208px; font-family:Arial; border:none;">
            <legend style="font-size:13px; font-weight:bold;">Consulta Indeferida</legend>
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
            <asp:Label ID="lblChave" runat="server" Font-Bold="True" Text="Chave:"
                Font-Size="14px" Font-Names="Verdana"></asp:Label>
                <br /><br />
                <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Font-Size="25px" 
                    Font-Names="Verdana"></asp:Label>
           </div>
            <br />
            <div style="font-size:12px;">
            Nome:<br />
            <asp:Label ID="lblPaciente" runat="server" 
                Font-Bold="True"></asp:Label>
            <br />
            Cartão Sus:<br /><asp:Label ID="lblCartaoSus" runat="server" 
                Font-Bold="true"></asp:Label>
            <%--<br />
            Unidade:<asp:Label ID="lblEas" runat="server" Font-Bold="True" Font-Names="Verdana"
                Font-Size="Smaller"></asp:Label>
            <br />
            Endereço:<asp:Label ID="lblEnderecoUnidade" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>
            <br />
            Telefone:<asp:Label ID="lblTelefone" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>
            <br />
            Profissional:<asp:Label ID="lblProfissional" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>--%>
            <br /><br /><br />
            Data Solicitação:<br /><asp:Label ID="lblData" runat="server" Font-Bold="True" ></asp:Label>
            <%--<br />
            Turno:<asp:Label ID="lblTurno" runat="server" Font-Size="Smaller" Font-Names="Verdana"
                Font-Bold="True"></asp:Label> (<asp:Label ID="lblHoraIni" Font-Bold="True" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>
                - <asp:Label ID="lblHoraFim" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>)--%>
            <br />
            Procedimento:<br /><asp:Label ID="lblProcedimento" runat="server" Font-Bold="True"></asp:Label>
            <br />
            Situação:<br /><asp:Label ID="lblSituacao" runat="server" Font-Bold="True"></asp:Label>
            <br /><br /><br />
            Data Indeferimento:<br /><asp:Label ID="lblDataIndeferimento" runat="server" Font-Bold="True"></asp:Label>
            <br />
            <%--CREMEB:<asp:Label ID="lblCRM" runat="server" Font-Size="Smaller" Font-Names="Verdana" Font-Bold="True"></asp:Label>
            <br />--%>
            JUSTIFICATIVA:<br /><span style="font-weight:bold;"><asp:Literal ID="lblJustificativa" runat="server" ></asp:Literal></span>
            
            <p style="font-size:10px; font-weight:bold; text-align: center;">Acesse:<br /> www.saude.salvador.ba.gov.br/ViverMais</p>
        </div>
        </fieldset>
    </div>
    </form>
</body>
</html>