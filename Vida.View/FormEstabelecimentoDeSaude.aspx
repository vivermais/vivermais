<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEstabelecimentoDeSaude.aspx.cs" Inherits="Vida.View.FormEstabelecimentoDeSaude" MasterPageFile="~/MasterMain.Master" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    <asp:GridView ID="grid_EstabelecimentoSaude" runat="server" 
        OnPageIndexChanging="onPageEstabelecimento"
        DataKeyNames="Codigo" OnRowCommand="onRowCommand_verificarAcao">
        <Columns>
            <asp:ButtonField ButtonType="Link" DataTextField="RazaoSocial" HeaderText="Razão Social" CommandName="cn_visualizarEstabelecimento" />
            <asp:BoundField DataField="NomeFantasia" HeaderText="Nome Fantasia" />
            <asp:BoundField DataField="Status" HeaderText="Nome Fantasia" />
        </Columns>
    </asp:GridView>
</asp:Content>
