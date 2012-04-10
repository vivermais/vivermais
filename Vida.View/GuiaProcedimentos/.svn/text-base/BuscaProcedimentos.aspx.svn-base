<%@ Page Language="C#" MasterPageFile="~/GuiaProcedimentos/MasterCatalogo.Master"
    AutoEventWireup="true" CodeBehind="BuscaProcedimentos.aspx.cs" Inherits="Vida.View.GuiaProcedimentos.BuscaProcedimentos"
    Title="Lista de Procedimentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Cadastro de Informação de Procedimento
    </h1>
    <p>
        <span class="">Código do Procedimento: </span><span class="">
            <asp:TextBox ID="tbxCodigoProcedimento" Width="100px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">Nome do Procedimento: </span><span class="">
            <asp:TextBox ID="tbxNomeProcedimento" Width="400px" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="">
            <asp:LinkButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" Text="Pesquisar" />
        </span>
    </p>
    <asp:Panel ID="PanelProcedimentos" runat="server" Visible="false">
        <fieldset class="formulario">
            <legend>Procedimentos</legend>
            <asp:GridView ID="GridViewProcedimentos" runat="server" AutoGenerateColumns="false"
                Width="600px" PagerSettings-Mode="Numeric">
                <Columns>
                    <asp:HyperLinkField HeaderText="Código do Procedimento" DataNavigateUrlFields="Codigo"
                        DataNavigateUrlFormatString="FormVisualizaProcedimento.aspx?co_infoProcedimento={0}" DataTextField="CodigoProcedimento" />
                    <asp:BoundField HeaderText="Nome do Procedimento" DataField="NomeProcedimento" />
                    <asp:BoundField HeaderText="Aplicação" DataField="Aplicacao" />
                    <asp:BoundField HeaderText="Conceito" DataField="Conceito" />
                    <asp:BoundField HeaderText="Dicas" DataField="Dicas" />
                    <asp:BoundField HeaderText="Observação" DataField="Observacao" />
                    <asp:BoundField HeaderText="Preparo" DataField="Preparo" />
                </Columns>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </fieldset>
    </asp:Panel>
</asp:Content>
