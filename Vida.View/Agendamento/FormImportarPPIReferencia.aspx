﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormImportarPPIReferencia.aspx.cs" Inherits="ViverMais.View.Agendamento.FormImportarPPIReferencia"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            PPI - Parametrização Pactuada e Integrada</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:PostBackTrigger ControlID="btnUpLoad" />
            </Triggers>
            <ContentTemplate>
                <fieldset class="formulario">
                    <legend>Importar PPI</legend>
                    <p>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="campo" Height="21px" Width="288px" />
                    </p>
                    <p>&nbsp;</p>
                    <p>
                        <asp:LinkButton ID="btnUpLoad" runat="server" OnClick="btnUpLoad_OnClick">
                            <img id="imgimportar" alt="Salvar" src="img/importar_1.png"
                                onmouseover="imgimportar.src='img/importar_2.png';"
                                onmouseout="imgimportar.src='img/importar_1.png';"  />
                        </asp:LinkButton>
                    </p>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <%--<Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
            </Triggers>--%>
            <ContentTemplate>
                <p>
                    <asp:HiddenField ID="hiddenRegistroAtual" runat="server" />
                    <asp:HiddenField ID="hiddenTotalRegistro" runat="server" />
                    <asp:Label runat="server" CssClass="rotulo">Registro Atual: </asp:Label>
                    <asp:Label ID="lblRegistroAtual" runat="server" Font-Size="X-Small"></asp:Label>
                </p>
                <p>
                    &nbsp;</p>
                <p>
                    &nbsp;</p>
                <p>
                    <asp:Label runat="server" CssClass="rotulo">Total de Registros: </asp:Label>
                    <asp:Label ID="lblTotalRegistros" runat="server" Font-Size="X-Small"></asp:Label>
                </p>
                <%--<asp:Timer ID="timer1" runat="server">
                </asp:Timer>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
