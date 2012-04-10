<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPesquisaEstoque.aspx.cs" Inherits="ViverMais.View.Farmacia.FormPesquisaEstoque" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>
<%@ Register Src="~/Farmacia/IncPesquisarEstoque.ascx" TagName="IncPesquisarEstoque" TagPrefix="IPE" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <%--<div id="top">
            <h2>Pesquisar Estoque</h2>--%>
            <%--<fieldset class="formulario">
                <legend>Dados da Pesquisa</legend>
                <p>
                    <span>--%>
                        <IPE:IncPesquisarEstoque ID="inc_pesquisa" runat="server" />
           <%--         </span>
                </p>
            </fieldset>--%>
        <%--</div>--%>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>