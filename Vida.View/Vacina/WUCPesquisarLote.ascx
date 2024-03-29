﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="WUCPesquisarLote.ascx.cs"
    Inherits="ViverMais.View.Vacina.WUCPesquisarLote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc1" %>--%>
<fieldset class="formulario">
    <legend>Pesquisar Lote</legend>
    <p>
        <span class="rotulo">Imunobiológico</span> <span>
            <asp:DropDownList ID="DropDownList_Vacina" CssClass="drop" runat="server" Width="300px"
                DataTextField="Nome" DataValueField="Codigo">
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="rotulo">Fabricante</span> <span>
            <asp:DropDownList ID="DropDownList_Fabricante" runat="server" CssClass="drop" Width="300px"
                DataTextField="Nome" DataValueField="Codigo">
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="rotulo">Nº Aplicações</span> <span>
            <asp:TextBox ID="TextBox_Aplicacoes" MaxLength="2" CssClass="campo" Width="25px"
                runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="rotulo">Lote</span> <span>
            <asp:TextBox ID="TextBox_Lote" runat="server" CssClass="campo"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="rotulo">Data de Validade</span> <span>
            <asp:TextBox ID="TextBox_Validade" runat="server" CssClass="campo"></asp:TextBox>
        </span>
    </p>
    <div class="botoesroll">
        <asp:LinkButton ID="LnkPesquisar" runat="server" OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_PesquisarLote">
                  <img id="imgpesquisar" alt="Pesquisar" src="img/btn_pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn_pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn_pesquisar1.png';" /></asp:LinkButton>
    </div>
    <div class="botoesroll">
        <asp:LinkButton ID="LnkListarTodos" runat="server">
                  <img id="imglistar" alt="Listar Todos" src="img/btn_listar_todos1.png"
                  onmouseover="imglistar.src='img/btn_listar_todos2.png';"
                  onmouseout="imglistar.src='img/btn_listar_todos1.png';" /></asp:LinkButton>
    </div>
    <p>
        <span>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números no campo Nº Aplicações."
                ControlToValidate="TextBox_Aplicacoes" ValidationExpression="^\d*$" ValidationGroup="ValidationGroup_PesquisarLote"
                Display="None">
            </asp:RegularExpressionValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Validade com formato inválido."
                ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_PesquisarLote"
                Display="None" Type="Date" Operator="DataTypeCheck" Font-Size="XX-Small"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900."
                ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_PesquisarLote"
                Display="None" Type="Date" ValueToCompare="01/01/1900" Operator="GreaterThanEqual"
                Font-Size="XX-Small"></asp:CompareValidator>
            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_Validade"
                Format="dd/MM/yyyy">
            </cc2:CalendarExtender>
            <%--<cc1:maskededitextender id="MaskedEditExtender2" runat="server" masktype="Number"
                mask="99" targetcontrolid="TextBox_Aplicacoes" inputdirection="LeftToRight" clearmaskonlostfocus="true">
                    </cc1:maskededitextender>--%>
            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_Validade"
                Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
            </cc2:MaskedEditExtender>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarLote" />
            <asp:CustomValidator ID="CustomValidator_PesquisarLote" runat="server" ErrorMessage="CustomValidator"
                OnServerValidate="OnServerValidate_Pesquisa" ValidationGroup="ValidationGroup_PesquisarLote"></asp:CustomValidator>
        </span>
    </p>
</fieldset>
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="LnkPesquisar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="LnkListarTodos" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_LotesPesquisados" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Lotes Pesquisados</legend>
                <p>
                    <asp:GridView ID="GridView_Lote" runat="server" AutoGenerateColumns="False" Width="100%"
                        PageSize="10" AllowPaging="True" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                        BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Horizontal">
                        <Columns>
                            <asp:BoundField HeaderText="Imunobiológico" DataField="NomeVacina" ItemStyle-Width="200px"
                                ReadOnly="true" />
                            <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-Width="200px"
                                ReadOnly="true" />
                            <asp:BoundField HeaderText="Aplicação" DataField="AplicacaoVacina" ItemStyle-Width="130px"
                                ReadOnly="true" />
                            <asp:HyperLinkField HeaderText="Lote" DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Vacina/FormLoteImunobiologico.aspx?co_lote={0}"
                                DataTextField="Identificacao" ItemStyle-Width="100px"></asp:HyperLinkField>
                            <asp:BoundField HeaderText="Validade" DataField="DataValidade" DataFormatString="{0:dd/MM/yyy}"
                                ReadOnly="true" />
                        </Columns>
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                            Height="24px" Font-Size="13px" />
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                        <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                        </EmptyDataTemplate>
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                    </asp:GridView>
                </p>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
