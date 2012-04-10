<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBuscaLoteMedicamento.aspx.cs" Inherits="Vida.View.Farmacia.FormBuscaLoteMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<%--            <fieldset>
                <legend>Lote de Medicamentos</legend>
                <p>
                    <span>Medicamento</span>
                    <span>
                        <asp:DropDownList ID="DropDownList_Medicamento" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span>Fabricante</span>
                    <span>
                        <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span>Lote</span>
                    <span>
                        <asp:TextBox ID="TextBox_Lote" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span>Data de Validade</span>
                     <span>
                        <asp:TextBox ID="TextBox_Validade" runat="server"></asp:TextBox>
                         <cc1:MaskedEditExtender ID="MaskedEditExtender1" InputDirection="LeftToRight"
                            MaskType="Date" Mask="99/99/9999" TargetControlID="TextBox_Validade" runat="server">
                         </cc1:MaskedEditExtender>
                         <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_Validade"
                            Format="dd/MM/yyyy" runat="server">
                         </cc1:CalendarExtender>
                    </span>
                </p>
                
                <p>
                    <asp:Button ID="Button_Pesquisar" runat="server" Text="Pesquisar" CommandArgument="alguns" OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_Pesquisa" />
                    <asp:Button ID="Button_ListarTodos" runat="server" Text="Listar Todos" CommandArgument="todos" OnClick="OnClick_Pesquisar" />
                    <asp:Button ID="Button_Novo" runat="server" Text="Novo Registro" PostBackUrl="~/Farmacia/FormLoteMedicamento.aspx" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Lote deve iniciar com pelo menos três caracteres."
                     ValidationGroup="ValidationGroup_Pesquisa" ValidationExpression="^[\S]{3}$" Display="None" ControlToValidate="TextBox_Lote"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Validade com formato inválido."
                        ControlToValidate="TextBox_Validade" Type="Date" Operator="DataTypeCheck" 
                        ValidationGroup="ValidationGroup_Pesquisa" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900."
                        ValidationGroup="ValidationGroup_Pesquisa" ControlToValidate="TextBox_Validade" 
                        Type="Date" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" Display="None"></asp:CompareValidator>
                    <asp:CustomValidator ID="CustomValidator_Pesquisa" runat="server" ErrorMessage="CustomValidator" 
                        Display="None" OnServerValidate="OnServerValidate_ValidaPesquisa" 
                        ValidationGroup="ValidationGroup_Pesquisa"></asp:CustomValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="ValidationGroup_Pesquisa" />
                </p>
            </fieldset>--%>

            <%--<asp:Panel ID="Panel_Resultado" runat="server" Visible="false">--%>
                <fieldset>
                    <legend>Lotes de Medicamento</legend>
                        <p>
                             <span>
                                Pressione o botão ao lado para cadastrar um novo lote de medicamento.
                                <asp:Button ID="Button_Novo" runat="server" Text="Novo Registro" PostBackUrl="~/Farmacia/FormLoteMedicamento.aspx"/>
                             </span>
                        </p>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Lotes" runat="server" AutoGenerateColumns="false"
                             AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Lote" DataNavigateUrlFields="Codigo" 
                                        DataNavigateUrlFormatString="~/Farmacia/FormLoteMedicamento.aspx?co_lote={0}" DataTextField="Lote" />
                                    <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" />
                                    <asp:BoundField HeaderText="Fabricante"  DataField="NomeFabricante" />
                                    <asp:BoundField HeaderText="Data de Validade" DataFormatString="{0:dd/MM/yyyy}" DataField="Validade" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            <%--</asp:Panel>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>