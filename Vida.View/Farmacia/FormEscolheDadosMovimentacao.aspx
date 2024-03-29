﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormEscolheDadosMovimentacao.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormEscolheDadosMovimentacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>--%>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Dados da Movimentação</h2>
        <fieldset class="formulario">
            <%--            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <%--<asp:Panel ID="Panel_Farmacia" runat="server" Visible="false">--%>
            <p>
                <span class="rotulo">Farmácia</span> <span>
                    <asp:DropDownList ID="DropDownList_Farmacia" runat="server" AutoPostBack="true" CausesValidation="false"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_DesabilitaSituacao" DataTextField="Nome" DataValueField="Codigo">
                        <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                    </asp:DropDownList>
                </span>
            </p>
            <%--</asp:Panel>--%>
            <%--                </ContentTemplate>
            </asp:UpdatePanel>--%>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Farmacia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Tipo Movimentação</span> <span>
                            <asp:DropDownList ID="DropDownList_TipoMovimentacao" runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaSituacao"
                                AutoPostBack="true" CausesValidation="true">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_TipoMovimentacao" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Farmacia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_Situacao" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Situação</span> <span>
                                <asp:DropDownList ID="DropDownList_Situacao" runat="server">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            
                
                <div class="botoesroll">
                <asp:LinkButton ID="Button_Ok" runat="server"   OnClick="OnClick_Continuar"  ValidationGroup="ValidationGroup_Movimentacao">
                  <img id="imgcontinua" alt="Continuar" src="img/btn/continuar1.png"
                  onmouseover="imgcontinua.src='img/btn/continuar2.png';"
                  onmouseout="imgcontinua.src='img/btn/continuar1.png';" /></asp:LinkButton>
                </div>
                 <div class="botoesroll">
                   <asp:LinkButton ID="Button1" runat="server"   PostBackUrl="~/Farmacia/Default.aspx" >
                  <img id="imgcancela" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancela.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancela.src='img/btn/cancelar1.png';" /></asp:LinkButton>
                </div>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Escolha uma farmácia."
                        ValidationGroup="ValidationGroup_Movimentacao" Display="None" Operator="GreaterThan"
                        ValueToCompare="-1" ControlToValidate="DropDownList_Farmacia"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Escolha um tipo de movimentação."
                        ValidationGroup="ValidationGroup_Movimentacao" Display="None" Operator="GreaterThan"
                        ValueToCompare="-1" ControlToValidate="DropDownList_TipoMovimentacao"></asp:CompareValidator>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList_TipoMovimentacao" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:CompareValidator ID="CompareValidator_Situacao" runat="server" ErrorMessage="Escolha uma Situação."
                                Display="None" Enabled="false" ControlToValidate="DropDownList_Situacao" ValueToCompare="-1"
                                Operator="GreaterThan" ValidationGroup="ValidationGroup_Movimentacao"></asp:CompareValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_Movimentacao"
                        ShowMessageBox="true" ShowSummary="false" />
                
           
        </fieldset>
    </div>
</asp:Content>
