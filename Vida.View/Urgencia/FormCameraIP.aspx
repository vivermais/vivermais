﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCameraIP.aspx.cs" Inherits="ViverMais.View.Urgencia.FormCameraIP"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Câmera IP
        </h2>
        <%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>--%>
        <fieldset class="formulario">
            <legend>Formulário de Cadastro</legend>
            <p>
                <span class="rotulo">Unidade</span> <span>
                    <asp:DropDownList ID="DropDownList_Unidade" runat="server" CssClass="drop" Width="300px"
                        DataTextField="NomeFantasia" DataValueField="CNES">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Local</span> <span>
                    <asp:TextBox ID="TextBox_Local" runat="server" CssClass="campo" Width="300"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Endereço IP</span> <span>
                    <asp:TextBox ID="TextBox_Endereco" runat="server" CssClass="campo" Width="150"></asp:TextBox>
                </span>
            </p>
            <p align="center">
                <span>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LinkButton_Salvar" runat="server" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_CadastrarCamera">
                             <img id="imgursalvar" alt="Salvar" src="img/bts/btn_salvar1.png"
                onmouseover="imgursalvar.src='img/bts/btn_salvar2.png';"
                onmouseout="imgursalvar.src='img/bts/btn_salvar1.png';" />
                        </asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LinkButton_Cancelar" runat="server" PostBackUrl="~/Urgencia/FormExibeCameraIP.aspx">
                             <img id="imgurcancelar" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                onmouseover="imgurcancelar.src='img/bts/btn_cancelar2.png';"
                onmouseout="imgurcancelar.src='img/bts/btn_cancelar1.png';" />
                        </asp:LinkButton>
                    </div>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Unidade é Obrigatório."
                        ControlToValidate="DropDownList_Unidade" ValueToCompare="-1" Operator="GreaterThan"
                        Display="None" ValidationGroup="ValidationGroup_CadastrarCamera"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Local é Obrigatório."
                        ControlToValidate="TextBox_Local" Display="None" ValidationGroup="ValidationGroup_CadastrarCamera"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Endereço IP é Obrigatório."
                        ControlToValidate="TextBox_Endereco" Display="None" ValidationGroup="ValidationGroup_CadastrarCamera"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_CadastrarCamera" />
                    <p>
                </span>
            </p>
        </fieldset>
        <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
