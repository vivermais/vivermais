﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDadosInventario.aspx.cs" Inherits="ViverMais.View.FormDadosInventario" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div id="top">
        <h2>Dados do Inventário</h2>
        <fieldset>
            <legend>Informações</legend>
            <p>
                <span class="rotulo">Farmácia</span>
                <span style="margin-left:5px;">
                    <asp:Label ID="Label_Farmacia" runat="server" Text="" />
                </span>
            </p>
            <p>
                <span class="rotulo">Situação</span>
                <span style="margin-left:5px;">
                    <asp:Label ID="Label_Situacao" runat="server" Text="" />
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span>
                <span style="margin-left:5px;">
                    <asp:Label ID="Label_DataAbertura"  runat="server" Text="" />
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Fechamento</span>
                <span style="margin-left:5px;">
                    <asp:TextBox ID="TextBox_DataFechamento" ReadOnly="true" CssClass="campo" runat="server"></asp:TextBox>
                    <asp:Label ID="Label_DataFechamento" runat="server" Text="" Visible="false"/>
                </span>
            </p>
            <p>
                <span>
                    <asp:Button ID="Button_FecharInventario" runat="server" Text="Fechar Inventário" OnClientClick="javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja fechar este inventário ?'); return false;" OnClick="OnClick_FecharInventario" ValidationGroup="group_altInventario" />
                    <asp:Button ID="Button_MedicamentosInventario" runat="server" Text="Medicamentos do Inventário" OnClick="OnClick_ItensInventario" />
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                       TargetControlID="TextBox_DataFechamento" Mask="99/99/9999" MaskType="Date" >
                    </cc1:MaskedEditExtender>
<%--                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                     TargetControlID="TextBox_DataFechamento">
                    </cc1:CalendarExtender>--%>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Fechamento é Obrigatório!" ControlToValidate="TextBox_DataFechamento" Display="None" ValidationGroup="group_altInventario"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Fechamento com formato inválido!" ControlToValidate="TextBox_DataFechamento" Operator="DataTypeCheck" Type="Date" ValidationGroup="group_altInventario" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Fechamento deve ser maior que 01/01/1900!" ControlToValidate="TextBox_DataFechamento" Operator="GreaterThan" ValueToCompare="01/01/1900" Type="Date" ValidationGroup="group_altInventario" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_altInventario" ShowMessageBox="true" ShowSummary="false" />
                </span>
            </p>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <ContentTemplate>
                <fieldset>
                    <legend>Relatórios</legend>
                    <p>
                        <span class="rotulo">Conferência</span>
                        <span class="camporadio">
                            <asp:RadioButton ID="RadioButton_Conferencia" CssClass="camporadio" Width="20px" Checked="true" runat="server" GroupName="group_inventario" ValidationGroup="group_relInventario" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Final</span>
                        <span class="camporadio">
                            <asp:RadioButton ID="RadioButton_Final" CssClass="camporadio" Width="20px" runat="server" GroupName="group_inventario"  ValidationGroup="group_relInventario" />
                        </span>
                    </p>
                    <p>
                        <span>
                            <asp:Button ID="Button_GerarRelatorio" runat="server" CausesValidation="true"
                             Text="Gerar Relatório" OnClick="OnClick_GerarRelatorio" />
                        </span>
                    </p>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
   
</asp:Content>
