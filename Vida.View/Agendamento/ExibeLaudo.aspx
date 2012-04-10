﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExibeLaudo.aspx.cs" Inherits="ViverMais.View.Agendamento.ExibeLaudo" %>

<%@ Register Assembly="SpiceLogicBLOB" Namespace="SpiceLogic.BLOBControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Módulo Regulação - Exibe Laudos</title>
    <link rel="stylesheet" href="style_form_agendamento-site.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="top">
        <h2>
            Exibe Laudo</h2>
        <fieldset class="formulario">
            <legend>Laudos da Solicitação</legend>
            <br />
            <div class="botoesroll">
                <%--<asp:LinkButton ID="btnImprimir" runat="server" Enabled="true" CausesValidation="False"
                    OnClick="lknImprimir_Click">
                <img id="imgimprimir" alt="Imprimir Laudo" src="img/btn-print-laudo1.png" 
                onmouseover="imgimprimir.src='img/btn-print-laudo2.png';" 
                onmouseout="imgimprimir.src='img/btn-print-laudo1.png';"  />
                </asp:LinkButton>--%>
                <%--<asp:LinkButton ID="btnImprimir" runat="server" PostBackUrl="~/Agendamento/ImprimeLaudo.aspx">
                        <img id="imgimprimir" alt="imgimprimir" src="../img/bts/id_imprimeetiqueta.png"
                onmouseover="imgimprimir.src='img/bts/id_imprimeetiqueta2.png';"
                onmouseout="imgimprimir.src='img/bts/id_imprimeetiqueta.png';"  />
                        </asp:LinkButton>--%>
            </div>
            <br />
            <p>
                <asp:Label ID="lblSemLaudo" runat="server" Text="Não Existe Laudo Para o Procedimento"
                    Font-Bold="true"></asp:Label>
                <cc1:BlobImage ID="BlobImage1" runat="server" Width="100%" Height="58%" ThumbnailDisplay-KeepAspectRatio="true">
                </cc1:BlobImage>
                <asp:Image ID="Image1" runat="server" Visible="true" Width="100%" Height="58%" />
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="lknAnterior" runat="server" CausesValidation="False" OnClick="lknAnterior_Click"
                    BorderStyle="None">
                  <img id="imganterior" alt="Anterior" src="img/btn-anterior1.png"
                  onmouseover="imganterior.src='img/btn-anterior2.png';"
                  onmouseout="imganterior.src='img/btn-anterior1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="lknProximo" runat="server" CausesValidation="False" OnClick="lknProximo_Click">
                  <img id="imgproximo" alt="Próximo" src="img/btn-proximo1.png"
                  onmouseover="imgproximo.src='img/btn-proximo2.png';"
                  onmouseout="imgproximo.src='img/btn-proximo1.png';" /></asp:LinkButton>
            </div>
            <br />
        </fieldset>
    </div>
    </form>
</body>
</html>
