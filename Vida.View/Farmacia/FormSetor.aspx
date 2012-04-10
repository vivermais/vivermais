﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormSetor.aspx.cs" Inherits="ViverMais.View.Farmacia.FormSetor" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top">
            <h2>Formulário para Cadastro de Setor</h2>
            <fieldset class="formulario">
                <legend>Dados do Setor</legend>
                <p>
                    <span class="rotulo">Setor</span>
                    <span style="margin-left: 5px;">
                    <span>
                        <asp:TextBox ID="TextBox_Setor" CssClass="campo" Width="300px" runat="server"></asp:TextBox>
                    </span>
                </p>
                <br />
                <div class="botoesroll" style="margin-top:20px">
                <asp:LinkButton ID="Button_Salvar" runat="server" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_cadSetor">
                                        <img id="imgsalvar" runat="server"/>
                    </asp:LinkButton>
                    </div>
                    <div class="botoesroll" style="margin-top:20px">
                    <span><asp:LinkButton ID="Button_Cancelar" runat="server" PostBackUrl="~/Farmacia/Default.aspx" >
                     <img id="imgcancelar" alt="Salvar" src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                    </asp:LinkButton>
                  </div>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server" ErrorMessage="Setor é Obrigatório!" ControlToValidate="TextBox_Setor" Display="None" ValidationGroup="ValidationGroup_cadSetor"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_Setor"
                        Display="None" ErrorMessage="Há caracters inválidos no Nome do Setor." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadSetor"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadSetor"/>
                </p>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>  