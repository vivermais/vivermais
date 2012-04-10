﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPesquisarMovimentacao.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormPesquisarMovimentacao" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/WUC_MensagemIE.ascx" TagName="IExplorer" TagPrefix="IE" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Movimentações Registradas
        </h2>
        <fieldset class="formulario">
            <legend>Pesquisar</legend>
            <p>
                <span class="rotulo">Número</span> <span>
                    <asp:TextBox ID="TextBox_Numero" runat="server" CssClass="campo" Width="100px" MaxLength="9"></asp:TextBox>
                    <asp:LinkButton ID="LinkButtonPesquisarNumero" runat="server" ValidationGroup="ValidationGroup_PesquisarNumero"
                        OnClick="OnClick_PesquisarPorNumero">
                        <img src='img/procurar.png' alt='Pesquisar' />
                    </asp:LinkButton>
                </span>
            </p>
            <p>
                <span class="rotulo">Sala de Vacina *</span><span>
                    <asp:DropDownList ID="DropDownList_Sala" runat="server" Width="350" CssClass="drop"
                        DataTextField="Nome" DataValueField="Codigo" AutoPostBack="true"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaSalasDestino">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Tipo de Movimentação *</span><span>
                    <asp:DropDownList ID="DropDownList_TipoMovimento" runat="server" CssClass="drop"
                        Width="250px" DataTextField="Nome" DataValueField="Codigo" AutoPostBack="true"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaSalasDestino">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel_PesquisaSalaDestino" runat="server" UpdateMode="Conditional"
                RenderMode="Inline" ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_TipoMovimento" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Sala" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_SalasDestino" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Sala de Destino</span> <span>
                                <asp:DropDownList ID="DropDownList_SalaDestino" Width="350" CssClass="drop" runat="server"
                                    DataTextField="Nome" DataValueField="Codigo">
                                    <asp:ListItem Text="SELECIONE..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Data de Início</span> <span>
                    <asp:TextBox ID="TextBox_DataInicio" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Data Final</span> <span>
                    <asp:TextBox ID="TextBox_DataFim" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Pesquisar" runat="server" OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_PesquisarMovimentacao">
                  <img id="imgpesquisar" alt="Pesquisar" src="img/btn_pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn_pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn_pesquisar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Cancelar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel_Movimentos" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Lnk_Pesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButtonPesquisarNumero" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_ResultadoPesquisa" runat="server" Visible="false">
                    <%--<IE:IExplorer ID="IExplorer" runat="server" />--%>
                    <fieldset class="formulario">
                        <legend>Resultado da Pesquisa</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_ResultadoPesquisa" runat="server" AutoGenerateColumns="false"
                                    OnSelectedIndexChanging="OnSelectedIndexChanging" Width="100%" AllowPaging="true"
                                    BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" PageSize="10" PagerSettings-Mode="Numeric"
                                    DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Movimentos" OnRowDataBound="OnRowDataBound_Movimento">
                                    <Columns>
                                        <asp:BoundField DataField="Numero" HeaderText="Número" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Sala de Destino">
                                            <ItemTemplate>
                                                <asp:Label ID="LabelSalaDestino" runat="server" Text='<%#bind("NomeSalaDestino") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
<%--                                        <asp:BoundField HeaderText="Sala de Destino"
                                          DataField="NomeSalaDestino" />--%>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Alterar">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkAlterarQuantidade" runat="server">
                                                    <img src="img/btn_editar.png" border="0" alt="Alterar Movimento?" />
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:HyperLinkField
                                          Text="Alterar" />--%>
                                        <%--<asp:TemplateField HeaderText="Alterar" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Alterar" runat="server"
                                                 OnClick="">Alterar</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Imprimir" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Ver" runat="server" CommandName="Select" CausesValidation="true">
                                                <img src='img/visualizar-p.png' alt='Imprimir' />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:ButtonField ButtonType="Link" CommandName="Select" Text="" ItemStyle-HorizontalAlign="Center" />--%>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Sala de Vacina é Obrigatório."
            Display="None" ControlToValidate="DropDownList_Sala" ValueToCompare="-1" Operator="GreaterThan"
            ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Tipo de Movimentação é Obrigatório."
            Display="None" ControlToValidate="DropDownList_TipoMovimento" ValueToCompare="-1"
            Operator="GreaterThan" ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Início inválida."
            ControlToValidate="TextBox_DataInicio" Display="None" Operator="DataTypeCheck"
            Type="Date" ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Data de Início deve ser igual ou maior que 01/01/1900."
            ControlToValidate="TextBox_DataInicio" Display="None" Operator="GreaterThanEqual"
            ValueToCompare="01/01/1900" Type="Date" ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data Final inválida."
            ControlToValidate="TextBox_DataFim" Display="None" Operator="DataTypeCheck" Type="Date"
            ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
            ControlToValidate="TextBox_DataFim" Display="None" Operator="GreaterThanEqual"
            ValueToCompare="01/01/1900" Type="Date" ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data de Início."
            ControlToValidate="TextBox_DataFim" ControlToCompare="TextBox_DataInicio" Display="None"
            Operator="GreaterThanEqual" Type="Date" ValidationGroup="ValidationGroup_PesquisarMovimentacao"></asp:CompareValidator>
        <asp:CustomValidator ID="CustomValidatorPeriodoMovimentacao" runat="server" ErrorMessage="CustomValidator"
            Display="None" ValidationGroup="ValidationGroup_PesquisarMovimentacao" OnServerValidate="OnServerValidate_CompararDatas"></asp:CustomValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_PesquisarMovimentacao"
            ShowMessageBox="true" ShowSummary="false" />
        <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="TextBox_DataInicio"
            Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataInicio"
            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" InputDirection="LeftToRight">
        </cc1:MaskedEditExtender>
        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_DataFim" Format="dd/MM/yyyy"
            PopupButtonID="calendar_icon.png" runat="server">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataFim"
            Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear" InputDirection="LeftToRight">
        </cc1:MaskedEditExtender>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Número é Obrigatório."
            ControlToValidate="TextBox_Numero" Display="None" ValidationGroup="ValidationGroup_PesquisarNumero"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="ValidationGroup_PesquisarNumero"
            ControlToValidate="TextBox_Numero" Display="None" ValidationExpression="^\d{9}$"
            ErrorMessage="O número da movimentação deve conter 9 dígitos."></asp:RegularExpressionValidator>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarNumero" />
    </div>
</asp:Content>
