﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncPesquisarLoteMedicamento.ascx.cs" Inherits="ViverMais.View.Farmacia.IncPesquisarLoteMedicamento" %>
<style type="text/css">
.formulario2
{
  width:670px;
  height:auto;
  margin-left: 5px;
  margin-right:5px;
  padding: 2px 2px 2px 2px;
}
</style>
<script type="text/javascript" language="javascript">
    function showTooltip(obj) {
        if (obj.options[obj.selectedIndex].title == "") {
            obj.title = obj.options[obj.selectedIndex].text;
            obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
            for (i = 0; i < obj.options.length; i++) {
                obj.options[i].title = obj.options[i].text;
            }
        }
        else
            obj.title = obj.options[obj.selectedIndex].text;
    }
</script>
<%--<div id="top">--%>
<fieldset class="formulario2">
    <legend>Pesquisa de Lotes de Medicamento</legend>
    <p>
        <span class="rotulo">Medicamento</span>
        <span>
            <asp:DropDownList ID="DropDownList_Medicamento" runat="server" Width="300px">
                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="rotulo">Fabricante</span>
        <span>
        <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
        </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="rotulo">Lote</span>
        <span>
            <asp:TextBox ID="TextBox_Lote" CssClass="campo" runat="server"></asp:TextBox>
        </span>
    </p>
    <p align="center">
        <span>
            <asp:Button ID="Button_Pesquisar" runat="server" Text="Pesquisar" OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_Pesquisa"/>
            <asp:Button ID="Button1" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx"/>
            <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server" ErrorMessage="Medicamento é Obrigatório!" ControlToValidate="DropDownList_Medicamento" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small" runat="server" ErrorMessage="Lote deve iniciar com pelo menos três caracteres!" ValidationExpression="^[\S]{3}[\w\W]*$" Display="None" ControlToValidate="TextBox_Lote" ValidationGroup="ValidationGroup_Pesquisa"></asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisa" />
        <p>
        </span>
    </p>
</fieldset>

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_Pesquisar" EventName="Click" />
    </Triggers>
    <ContentTemplate>
<asp:Panel ID="Panel_Resultado" runat="server" Visible="false">
<fieldset class="formulario2">
    <legend>Resultado da Pesquisa</legend>
    <p>
        <span>
            <asp:GridView ID="GridView_Lotes" runat="server" AutoGenerateColumns="false" AllowPaging="true" Width="600px"
             OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric">
                <Columns>
                    <asp:BoundField HeaderText="Lote" DataField="Lote" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center"/>
                    <asp:BoundField HeaderText="Validade" DataField="Validade" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"/>
                </Columns>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </span>
    </p>
</fieldset>
<%--</div>--%>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>