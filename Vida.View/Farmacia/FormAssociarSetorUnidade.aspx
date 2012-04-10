<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAssociarSetorUnidade.aspx.cs" Inherits="Vida.View.Farmacia.FormAssociarSetorUnidade" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top">
            <fieldset class="formulario">
                <legend>Setores da Unidade</legend>
                    <span><h4>Selecione uma unidade abaixo para ver os setores cadastrados na mesma.</h4></span>
                    <p>
                        <span>
                        <asp:GridView ID="GridView_Unidade" runat="server" AllowPaging="true" 
                            AutoGenerateColumns="false" DataKeyNames="Codigo" 
                            OnPageIndexChanging="OnPageIndexChanging_Paginacao" 
                            OnRowCommand="OnRowCommand_Acao" PageSize="20" Width="305px">
                            <Columns>
                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_Editar" 
                                    DataTextField="NomeFantasia" HeaderText="Unidade" />
                            </Columns>
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                        </span>
                    </p>
                    <p>
                        &nbsp;
                        <asp:Panel ID="Panel_SetoresUnidade" runat="server" Visible="false">
                            <asp:Table ID="Table_Setores" runat="server">
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        Associar
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">&nbsp;</asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        Desassociar
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:ListBox ID="ListBox_SetoresDisponiveis" runat="server" 
                                            SelectionMode="Multiple" Width="150px" Height="150px" CssClass="listbox"></asp:ListBox>
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:ImageButton ID="ImageButton_Adicionar" runat="server" 
                                            ImageUrl="~/Farmacia/img/direita.png" width="32" height="32" OnClick="OnClick_AdicionaSetor" 
                                            ValidationGroup="ValidationGroup_AdicionaSetor" />
                                        <asp:ImageButton ID="ImageButton_Retirar" runat="server" 
                                            ImageUrl="~/Farmacia/img/esquerda.png" width="32" height="32" OnClick="OnClick_RetiraSetor" 
                                            ValidationGroup="ValidationGroup_RetiraSetor" />
                                    </asp:TableCell>
                                    <asp:TableCell HorizontalAlign="Center">
                                        <asp:ListBox ID="ListBox_SetoresAlocados" runat="server" 
                                            SelectionMode="Multiple" Width="150" Height="150" CssClass="listbox"></asp:ListBox>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                                        <asp:Button ID="Button_Salvar" runat="server" OnClick="OnClick_Salvar" 
                                            Text="Salvar" ValidationGroup="ValidationGroup_Salvar" />
                                        <asp:Button ID="Button_Cancelar" runat="server" OnClick="OnClick_Cancelar" 
                                            Text="Cancelar" />
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                            
                            <p></p>
                            <asp:CustomValidator ID="CustomValidator_Adicionar" runat="server" 
                                Display="None" ErrorMessage="CustomValidator" Font-Size="XX-Small" 
                                OnServerValidate="OnServerValidate_ValidaAdicaoSetor" 
                                ValidationGroup="ValidationGroup_AdicionaSetor"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator_Retirar" runat="server" Display="None" 
                                ErrorMessage="CustomValidator" Font-Size="XX-Small" 
                                OnServerValidate="OnServerValidate_ValidaRemocaoSetor" 
                                ValidationGroup="ValidationGroup_RetiraSetor"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator_Associar" runat="server" 
                                Display="None" ErrorMessage="CustomValidator" Font-Size="XX-Small" 
                                OnServerValidate="OnServerValidate_ValidaAssociacao" 
                                ValidationGroup="ValidationGroup_Salvar"></asp:CustomValidator>
                        </asp:Panel>
                    </p>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>