<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibePesquisarLote.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormExibePesquisarLote" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Pesquisar Lote de Imunobiológico
        </h2>
        <fieldset class="formulario">
            <legend>Relação</legend>
            <p>
                <span>Para cadastrar um novo lote pressione o botão ao lado.
                    <asp:Button ID="Button_NovoLote" runat="server" Text="Novo Lote" PostBackUrl="~/Vacina/FormLoteImunobiologico.aspx" />
                </span>
            </p>
            <p>
                <span class="rotulo">Imunobiológico</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_Vacina" runat="server" Width="300px" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Fabricante</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_Fabricante" runat="server" Width="300px" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Nº Aplicações</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Aplicacoes" CssClass="campo" Width="45px" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Lote</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Lote" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Validade</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_Validade" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <p align="center">
                <span>
                    <asp:Button ID="ButtonPesquisar" runat="server" Text="Pesquisar" OnClick="OnClick_PesquisarLote"
                        ValidationGroup="ValidationGroup_PesquisarLote" />
                    <asp:Button ID="ButtonListarTodos" runat="server" Text="Listar Todos" OnClick="OnClick_ListarTodosLotes" />
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Validade com formato inválido."
                        ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_PesquisarLote"
                        Display="None" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900."
                        ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_PesquisarLote"
                        Display="None" Type="Date" ValueToCompare="01/01/1900" Operator="GreaterThanEqual"></asp:CompareValidator>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_Validade"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_Validade"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                    </cc1:MaskedEditExtender>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarLote" />
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Lotes Pesquisados</legend>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ButtonPesquisar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ButtonListarTodos" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Lote" runat="server" AutoGenerateColumns="false" Width="690px" PageSize="20"
                                AllowPaging="true" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                <Columns>
                                    <asp:BoundField HeaderText="Imunobiológico" DataField="NomeVacina" />
                                    <asp:HyperLinkField HeaderText="Lote" DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Vacina/FormLoteImunobiologico.aspx?co_lote={0}"
                                        DataTextField="Identificacao" ItemStyle-Width="300px" />
                                    <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" />
                                    <asp:BoundField HeaderText="Data de Validade" DataField="DataValidade" DataFormatString="{0:dd/MM/yyy}" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
