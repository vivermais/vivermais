﻿<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true" CodeBehind="FormParametrizacaoVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormParametrizacaoVacina" Title="ViverMais - Parametrização de Vacina" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Parametrização de Vacina</h2>
                <fieldset class="formulario">
                    <legend>Parametrização</legend>
                    <p>
                         <span class="rotulo">Vacina</span> <span>
                            <asp:DropDownList ID="DropDown_Vacina" CssClass="drop" runat="server" AutoPostBack="true" DataTextField="Nome" DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_DropDownParametrizacao" />
                         </span>
                    </p>
                    <p>
                        <span class="rotulo">Faixa Etária Inicial</span> <span>
                            <asp:TextBox ID="TextBox_FaixaEtariaInicial" CssClass="campo" runat="server"></asp:TextBox>
                         </span>
                    </p>
                    <p>
                         <span class="rotulo">Faixa Etária Final</span> <span>
                             <asp:TextBox ID="TextBox_FaixaEtariaFinal" CssClass="campo" runat="server"></asp:TextBox>
                         </span>
                    </p>
                    <p>
                         <span class="rotulo">Dose</span> <span>
                             <asp:DropDownList ID="DropDown_Dose" CssClass="drop" runat="server" DataTextField="Descricao" DataValueField="Codigo"></asp:DropDownList>
                         </span>
                    </p>
                    <%--<p>
                         <span class="rotulo">Sexo</span> <span style="margin-left: 5px;">
                             <asp:RadioButton ID="RadioButton_Masculino" CssClass="camporadio" Width="20px" Checked="true"
                        GroupName="GroupName_Sexo" runat="server" />Masculino
                    <asp:RadioButton ID="RadioButton_Feminino" CssClass="camporadio" Width="20px" GroupName="GroupName_Sexo"
                        runat="server" />Feminino
                        <asp:RadioButton ID="RadioButton_Ambos" CssClass="camporadio" Width="20px" GroupName="GroupName_Sexo"
                        runat="server" />Âmbos
                         </span>
                         </span>
                    </p>--%>
                    <p>
                        <asp:Button ID="Button_Adicionar" runat="server" Text="Adicionar" OnClick="OnClick_Adicionar" ValidationGroup="ValidationGroup_Param" />
                    </p>
                    </fieldset>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Selecione uma vacina!"
                    Display="None" ControlToValidate="DropDown_Vacina" InitialValue="0"  ValidationGroup="ValidationGroup_Param"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe faixa etária inicial!"
                    Display="None" ControlToValidate="TextBox_FaixaEtariaInicial" ValidationGroup="ValidationGroup_Param"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Informe faixa etária final!"
                    Display="None" ControlToValidate="TextBox_FaixaEtariaFinal" ValidationGroup="ValidationGroup_Param"></asp:RequiredFieldValidator>
                     <asp:CompareValidator ID="CompareValidator1" Display="None"  runat="server"  
                     ControlToCompare="TextBox_FaixaEtariaFinal"  ControlToValidate="TextBox_FaixaEtariaInicial" 
                     Operator="LessThanEqual" Type="Double" ValidationGroup="ValidationGroup_Param"
                     ErrorMessage="A faixa etária inicial deve ser menor ou igual a faixa etária final"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="ValidationGroup_Param" runat="server" />
                    <fieldset class="formulario">
                    <legend>Registros de Parametrização</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Parametrizacao" runat="server" AutoGenerateColumns="false" OnRowCommand="OnRowCommand_GridViewParametrizacao" DataKeyNames="Codigo"
                            OnRowDeleting="OnRowDeleting_GridViewParametrizacao">
                                <Columns>
                                     <asp:BoundField HeaderText="Faixa Etária" DataField="FaixaEtariaToString" ItemStyle-Width="150px" />
                                     <asp:BoundField HeaderText="Dose" DataField="DoseVacina" ItemStyle-Width="100px" />
                                <asp:CommandField  ButtonType="Button" DeleteText="Excluir" ShowDeleteButton="true"/>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
