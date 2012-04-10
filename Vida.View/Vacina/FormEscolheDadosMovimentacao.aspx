<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEscolheDadosMovimentacao.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormEscolheDadosMovimentacao" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

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
            <legend>Informações</legend>
            <%--            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>--%>
            <%--<asp:Panel ID="Panel_Farmacia" runat="server" Visible="false">--%>
            <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:DropDownList ID="DropDownList_Sala" runat="server" CssClass="drop" AutoPostBack="true"
                        Width="350px" CausesValidation="false" OnSelectedIndexChanged="OnSelectedIndexChanged_DesabilitaSituacao"
                        DataTextField="Nome" DataValueField="Codigo">
                        <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                    </asp:DropDownList>
                </span>
            </p>
            <%--</asp:Panel>--%>
            <%--                </ContentTemplate>
            </asp:UpdatePanel>--%>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Sala" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Tipo Movimentação</span> <span>
                            <asp:DropDownList ID="DropDownList_TipoMovimentacao" runat="server" Width="200px"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaSituacao" AutoPostBack="true"
                                CausesValidation="true" CssClass="drop">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_TipoMovimentacao" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Sala" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_Situacao" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Situação</span> <span>
                                <asp:DropDownList ID="DropDownList_Situacao" runat="server" Width="200px" CssClass="drop"
                                    DataTextField="Nome" DataValueField="Codigo">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="botoesroll">
                <asp:LinkButton ID="Button_Ok" runat="server" OnClick="OnClick_Continuar" ValidationGroup="ValidationGroup_Movimentacao">
                  <img id="imgcontinuar" alt="Continuar" src="img/continuar1.png"
                  onmouseover="imgcontinuar.src='img/continuar2.png';"
                  onmouseout="imgcontinuar.src='img/continuar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="Button1" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Escolha uma sala de vacina."
                ValidationGroup="ValidationGroup_Movimentacao" Display="None" Operator="GreaterThan"
                ValueToCompare="-1" ControlToValidate="DropDownList_Sala"></asp:CompareValidator>
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
