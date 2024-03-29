﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormDefinirFaixaProcedimento.aspx.cs" Inherits="ViverMais.View.Agendamento.FormDefinirFaixaProcedimento"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lknCadastrar" />
            <asp:PostBackTrigger ControlID="lknVoltar" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Faixa do Procedimento</h2>
                <fieldset class="formulario">
                    <legend>Definir Faixa do Procedimento</legend>
                    <p>
                        <span class="rotulo">CNES:</span> <span>
                            <asp:TextBox ID="tbxCnes" CssClass="campo" runat="server" AutoPostBack="True" OnTextChanged="tbxCnes_TextChanged"
                                Width="60px"></asp:TextBox>
                            <asp:TextBox ID="tbxUnidade" runat="server" CssClass="campo" Width="300px"></asp:TextBox>
                            <cc1:MaskedEditExtender MaskType="Number" ID="maskededitextender3" runat="server"
                                TargetControlID="tbxCnes" Mask="9999999" ClearMaskOnLostFocus="true">
                            </cc1:MaskedEditExtender>
                            <asp:ImageButton ID="imgUnidade" runat="server" ImageUrl="~/Agendamento/img/procurar.png"
                                Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" OnClick="ImageButton2_Click" />
                        </span>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxCnes" Display="Dynamic" ErrorMessage="* Informe o CNES"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxUnidade" Display="Dynamic" ErrorMessage="* Informe a Unidade"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxCnes" Display="Dynamic" ErrorMessage="* Somente Números"
                            Operator="DataTypeCheck" Type="Integer" ValidationGroup="codProcedimento"></asp:CompareValidator></p>
                    <p>
                        <asp:ListBox ID="lbxUnidade" runat="server" OnSelectedIndexChanged="lbxUnidade_SelectedIndexChanged"
                            AutoPostBack="True"></asp:ListBox>
                    </p>
                    <p>
                        <span class="rotulo">Procedimento:</span> <span>
                            <asp:TextBox ID="tbxCodigo" runat="server" CssClass="campo" OnTextChanged="tbxCodigo_TextChanged"
                                AutoPostBack="True" Width="75px"></asp:TextBox>
                            <cc1:MaskedEditExtender MaskType="Number" ID="maskededitextender4" runat="server"
                                TargetControlID="tbxCodigo" Mask="9999999999" ClearMaskOnLostFocus="true">
                            </cc1:MaskedEditExtender>
                            <asp:TextBox ID="tbxProcedimento" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                            <asp:ImageButton ID="imgProcedimento" runat="server" ImageUrl="~/Agendamento/img/procurar.png"
                                Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" OnClick="ImageButton1_Click" />
                        </span>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxCodigo" Display="Dynamic" ErrorMessage="* Informe o Código do Procedimento"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxProcedimento" Display="Dynamic" ErrorMessage="* Informe o  Procedimento"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxCodigo" Display="Dynamic" ErrorMessage="* Somente Números"
                            Operator="DataTypeCheck" Type="Integer" ValidationGroup="codProcedimento"></asp:CompareValidator></p>
                    <p>
                        <asp:ListBox ID="lbxProcedimento" runat="server" OnSelectedIndexChanged="ListBox1_SelectedIndexChanged"
                            AutoPostBack="True"></asp:ListBox>
                    </p>
                    <p>
                        <span class="rotulo">Faixa:</span> <span>
                            <asp:DropDownList ID="ddlFaixa" runat="server" CssClass="drop">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlFaixa" Display="Dynamic" ErrorMessage="* Selecione a Faixa"
                                InitialValue="0" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator></span>
                    </p>
                    <p>
                        <span class="rotulo">Vigência:</span> <span>
                            <asp:TextBox ID="tbxDataInicial" CssClass="campo" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="ano1" Format="dd/MM/yyyy" TargetControlID="tbxDataInicial"
                                Animated="true">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                TargetControlID="tbxDataInicial" Mask="99/99/9999" MaskType="Date">
                            </cc1:MaskedEditExtender>
                            <b style="font-family: Verdana; font-size: 11px;">a</b>
                            <asp:TextBox ID="tbxDataFinal" CssClass="campo" runat="server" Style="margin-left: 5px"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxDataFinal"
                                Animated="true">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                                TargetControlID="tbxDataFinal" Mask="99/99/9999" MaskType="Date">
                            </cc1:MaskedEditExtender>
                        </span>
                    </p>
                    <p>
                        <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                            ControlToCompare="tbxDataInicial" ControlToValidate="tbxDataFinal" Display="Dynamic"
                            ErrorMessage="* Data Inicial tem que ser menor que a Final" Operator="GreaterThanEqual"
                            Type="Date" ValidationGroup="codProcedimento"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxDataInicial" Display="Dynamic" ErrorMessage="* Informe a Data Inicial"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxDataFinal" Display="Dynamic" ErrorMessage="* Informe a Data Final"
                            SetFocusOnError="True" ValidationGroup="codProcedimento"></asp:RequiredFieldValidator>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknCadastrar" runat="server" OnClick="Incluir_Click" ValidationGroup="codProcedimento">
                  <img id="imgcadastrar" alt="Vadascrar" src="img/cadastrar_1.png"
                  onmouseover="imgcadastrar.src='img/cadastrar_2.png';"
                  onmouseout="imgcadastrar.src='img/cadastrar_1.png';" /></asp:LinkButton>
                    </div>
                    <cc1:ConfirmButtonExtender ID="cfbLknCadastrar" runat="server" TargetControlID="lknCadastrar"
                        ConfirmText="Confirma a Definição da Faixa de Procedimento?" />
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                            CausesValidation="False">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                </fieldset>
                <fieldset class="formulario">
                    <legend>Faixas Cadastradas</legend>
                    <asp:Label ID="lblSemRegistro" runat="server" Text="Nenhum Registro Encontrado" Visible="false"></asp:Label>
                    <asp:GridView ID="GridViewFaixaProcedimento" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        EnableSortingAndPagingCallbacks="True" Width="100%" OnRowCommand="GridViewFaixaProcedimento_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle CssClass="colunaEscondida" />
                                <ItemStyle CssClass="colunaEscondida" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Unidade" HeaderText="Unidade" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Validade_Inicial" HeaderText="Validade Inicial" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Validade_Final" HeaderText="Validade Final" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Faixa" HeaderText="Faixa" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="cmdDelete" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                        CommandName="Excluir" OnClientClick="javascript : return confirm('Tem certeza que deseja excluir ?');"
                                        Text="">
                                        <asp:Image ID="Excluir" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="tab" />
                        <RowStyle CssClass="tabrow_left" />
                    </asp:GridView>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
