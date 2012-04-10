﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeResponsavelAtesto.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeResponsavelAtesto" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="top">
            <h2>Lista de Responsáveis Cadastrados</h2>
            <fieldset>
                <legend>Responsável Atesto - Nota Fiscal</legend>
                <p>
                    <span>
                        Pressione o botão ao lado para cadastrar um novo responsável
                        <asp:Button ID="Button_Novo" runat="server" Text="Novo Registro" PostBackUrl="~/Farmacia/FormResponsavelAtestoNotaFiscal.aspx" />
                    </span>
                </p>
                <p>
                    <span>
                        <asp:GridView ID="GridView_Responsavel" runat="server" AutoGenerateColumns="false"
                         AllowPaging="true" PageSize="20" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                         PagerSettings-Mode="Numeric">
                            <Columns>
                                <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo"
                                    DataNavigateUrlFormatString="~/Farmacia/FormResponsavelAtestoNotaFiscal.aspx?co_responsavel={0}"
                                    DataTextField="Nome" />
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
                <p>
                    <span>
                        <%--<asp:Button ID="Button_Cancelar" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />--%>
                        <%--<asp:Button ID="Button_FecharJanela" runat="server" Text="Fechar Janela" OnClientClick="parent.parent.GB_hide();" />--%>
                    </span>
                </p>
            </fieldset>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
