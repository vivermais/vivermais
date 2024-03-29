﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormImportarAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.FormImportarAgenda"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnEnviarArquivo" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
            <h2>Agenda</h2>
                <fieldset class="formulario">
                    <legend>Importar Agenda</legend>
                    <p>
                        <asp:FileUpload ID="FileUpload1"  CssClass="campo" runat="server" Width="288px" Height="21px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FileUpload1"
                            Font-Size="XX-Small" ErrorMessage="* É Necessário selecionar um arquivo">
                        </asp:RequiredFieldValidator>
                    </p>
                    <div class="botoesroll"><asp:LinkButton ID="btnEnviarArquivo" runat="server" OnClick="btnEnviarArquivo_Click" >
                <img id="imgimportar" alt="Importar Agenda" src="img/importar_1.png"
                onmouseover="imgimportar.src='img/importar_2.png';"
                onmouseout="imgimportar.src='img/importar_1.png';" />
            </asp:LinkButton></div>
                    <div class="botoesroll"><asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx" >
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton></div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
