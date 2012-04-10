﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RelatorioSolicitacao.aspx.cs"
    Inherits="ViverMais.View.Agendamento.RelatorioSolicitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:180px">
        <div style="width: 170px; font-family:Arial; border:1px dashed #333333; font-weight:bold; padding:3px">
            Marcação de Consulta
            </div>
            <br />
            <div style="font-family:Verdana"> 
            <div align="center">
                <asp:Label ID="lblViverMais" runat="server" Font-Bold="False" Font-Size="9px"
                    Text="ViverMais - Sistema Integrado de Saúde"></asp:Label><br /><br />
                    </div>
                    <div style="text-align:center; font-size:7px;">
            <asp:Label ID="lblPrefeitura" runat="server" Font-Bold="True" Text="Prefeitura do Salvador"
                Font-Size="10px" ></asp:Label><br />
            <asp:Label ID="lblSecretaria" runat="server" Font-Bold="True" Text="Secretaria Municial da Saúde"
                Font-Size="10px" ></asp:Label>
                </div>
         
          
            <br />
            <div style="text-align:center">
            <asp:Label ID="lblChave" runat="server" Font-Bold="True" Text="Chave:"
                Font-Size="12px" Font-Names="Verdana"></asp:Label>
                </div>
                
                <asp:Label ID="lblCodigo" runat="server" Font-Bold="True" Font-Size="18px" 
                    Font-Names="Verdana"></asp:Label>
             
             <div style="font-size:11px;">
            <br />
            <span style="font-weight:bold">Nome:</span><br /><asp:Label ID="lblPaciente" runat="server" 
                Font-Bold="True"></asp:Label>
            <br />
            <br />
            Cartão Sus:<br /><asp:Label ID="lblCartaoSus" runat="server" 
                Font-Bold="true"></asp:Label>
            <br /><br /><br />
            Unidade:<br /><asp:Label ID="lblEas" runat="server" Font-Bold="True" ></asp:Label>
            <br />
            Endereço:<br /><asp:Label ID="lblEnderecoUnidade" runat="server" Font-Bold="true"></asp:Label>
            <br />
            Telefone:<asp:Label ID="lblTelefone" runat="server" ></asp:Label>
            <br />
            Profissional:<br /><asp:Label ID="lblProfissional" runat="server" Font-Bold="true"></asp:Label>
            <br /><br /><br />
            Data:<asp:Label ID="lblData" runat="server" Font-Bold="True"></asp:Label>
            <br />
            Turno:<asp:Label ID="lblTurno" runat="server" Font-Size="Smaller" Font-Names="Verdana"
                Font-Bold="True"></asp:Label> (<asp:Label ID="lblHoraIni" Font-Bold="True" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>
                - <asp:Label ID="lblHoraFim" runat="server" Font-Bold="True" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>)
            <br /><br /><br />
            Procedimento:<asp:Label ID="lblProcedimento" runat="server" Font-Size="Smaller" Font-Names="Verdana"
                Font-Bold="True"></asp:Label>
            <br />
            Recomendação:<br /><span style="font-weight:bold;"><asp:Literal ID="lblRecomendacoes" runat="server"></asp:Literal></span>
            <p style="font-size:9px; font-weight:bold; text-align: center;">Acesse:<br /> www.saude.salvador.ba.gov.br/ViverMais</p>
            <div style="height:200px;"><br /><br /><br /><br /><br /><br /><br />.</div>
            <%--<asp:Label ID="lblRecomendac
            ao" runat="server" Font-Size="Smaller" Font-Names="Verdana"></asp:Label>--%>
            </div>
        </div>
  
    </form>
</body>
</html>
