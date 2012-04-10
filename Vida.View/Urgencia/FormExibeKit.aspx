﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeKit.aspx.cs" Inherits="ViverMais.View.Urgencia.FormExibeKit"
    EnableEventValidation="false" MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Lista de Kits</h2>
        <fieldset class="formulario">
            <legend>Lista de Kits</legend>
            <p>
                <asp:LinkButton ID="LinkButton_NovoRegistro" runat="server" PostBackUrl="~/Urgencia/FormKitPA.aspx">
            <img id="imgnovo" alt="Novo Registro" src="img/novo-registro1.png"
                  onmouseover="this.src='img/novo-registro2.png';"
                  onmouseout="this.src='img/novo-registro1.png';" />
                </asp:LinkButton>
            </p>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Kits" runat="server" DataKeyNames="Codigo" AutoGenerateColumns="false"
                                AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging_PaginacaoKits" PageSize="10"
                                PagerSettings-Mode="Numeric"
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                    <asp:HyperLinkField ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Codigo"
                                        Text="Editar" DataNavigateUrlFormatString="~/Urgencia/FormKitPA.aspx?co_kit={0}" />
                                        
                                    <%--<asp:CommandField SelectText="Selecionar" ShowSelectButton="True"></asp:CommandField>--%>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
