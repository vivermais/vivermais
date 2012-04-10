﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" CodeBehind="FormImportarFPO.aspx.cs"
    Inherits="ViverMais.View.Agendamento.FormImportarFPO" Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnFazerUpload" />
        </Triggers>
        <ContentTemplate>
        <div id="top">
        <h2>FPO - Ficha de Programação Orçamentária</h2>
            <fieldset class="formulario">
                <legend>Importar FPO</legend>
                <p>
                    <asp:FileUpload ID="FileUpload1" runat="server" CssClass="campo" Height="21px" 
                        Width="288px" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="FileUpload1" Font-Size="XX-Small"  
                        ErrorMessage="* É Necessário selecionar um arquivo"></asp:RequiredFieldValidator>
                </p>
                <div class="botoesroll"><asp:LinkButton ID="btnFazerUpload" runat="server" 
                        onclick="btnFazerUpload_Click" >
                <img id="imgimportar" alt="Salvar" src="img/importar_1.png"
                onmouseover="imgimportar.src='img/importar_2.png';"
                onmouseout="imgimportar.src='img/importar_1.png';"  />
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
