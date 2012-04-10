﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormInventario.aspx.cs"
    Inherits="ViverMais.View.FormInventario" MasterPageFile="~/Farmacia/MasterFarmacia.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="head" ID="c_head" runat="server">
</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Always">--%>
    <%--<ContentTemplate>--%>
    <div id="top">
            <asp:UpdatePanel ID="UpdatePanel_InventariosCadastrados" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_Farmacia" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
        <h2>
            Formulário para Cadastro de Inventário</h2>
        <fieldset class="formulario">
            <legend>Inventário</legend>
            <p>
                <span class="rotulo">Farmácia</span> <span>
                    <asp:DropDownList ID="DropDownList_Farmacia" runat="server" CausesValidation="true"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaInventarios"
                        AutoPostBack="true" DataTextField="Nome" DataValueField="Codigo">
                        <%--<asp:ListItem Text="Selecione..." Value="-1">
                        </asp:ListItem>--%>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span> <span>
                    <asp:TextBox ID="TextBox_DataAbertura" CssClass="campo" runat="server" ReadOnly="true"></asp:TextBox>
                </span>
            </p>
            <p>
                <span>
                    <asp:LinkButton ID="btn_Salvar_Click" runat="server"  OnClick="OnClick_Salvar" CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja abrir o inventário ?'); return false;" ValidationGroup="group_cadInventario">
                  <img id="imgsalvar1" alt="Salvar" src="img/btn/salvar1.png"
                  onmouseover="imgsalvar1.src='img/btn/salvar2.png';"
                  onmouseout="imgsalvar1.src='img/btn/salvar1.png';" /></asp:LinkButton>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Farmácia deve ser escolhida!"
                        ControlToValidate="DropDownList_Farmacia" ValueToCompare="-1" Operator="GreaterThan"
                        ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Abertura é Obrigatório!"
                        ControlToValidate="TextBox_DataAbertura" Display="None" ValidationGroup="group_cadInventario"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Abertura com formato inválido!"
                        ControlToValidate="TextBox_DataAbertura" Operator="DataTypeCheck" Type="Date"
                        ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Abertura deve ser maior que 01/01/1900!"
                        ControlToValidate="TextBox_DataAbertura" Operator="GreaterThan" ValueToCompare="01/01/1900"
                        Type="Date" ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>                   
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="group_cadInventario" />
                <p>
                </span>
            </p>
        </fieldset>
            
            
                <asp:Panel ID="Panel_InventariosFarmacia" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Inventários da Farmácia</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_Inventario" runat="server" AllowPaging="True" Width="100%" Font-Size="x-small"
                                    PageSize="10" OnPageIndexChanging="OnPageIndexChanging_Paginacao" AutoGenerateColumns="False"
                                    PagerSettings-Mode="Numeric" OnRowDataBound="OnRowDataBound_FormatarGridView">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Data do Inventário" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                                    Text='<%#bind("DataInventario") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Situação" DataField="Situacao" HeaderStyle-HorizontalAlign="Center"
                                            ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                    </Columns>
                                   <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset></asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
