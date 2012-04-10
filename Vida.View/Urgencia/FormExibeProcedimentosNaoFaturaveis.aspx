﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeProcedimentosNaoFaturaveis.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormExibeProcedimentosNaoFaturaveis" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 660px;
            height: auto;
            margin-left: 20px;
            margin-right: 0;
            padding: 5px 5px 10px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Procedimentos não-faturáveis</h2>
        <fieldset class="formulario">
            <legend>Relação</legend>
            <p>
                <asp:LinkButton ID="LinkButton_NovoRegistro" runat="server" PostBackUrl="~/Urgencia/FormProcedimentosNaoFaturaveis.aspx">
            <img id="imgnovo" alt="Novo Registro" src="img/novo-registro1.png"
                  onmouseover="this.src='img/novo-registro2.png';"
                  onmouseout="this.src='img/novo-registro1.png';" />
                </asp:LinkButton>
            </p>
            <p>
                <span>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:GridView ID="GridView_Procedimentos" AutoGenerateColumns="false" runat="server"
                                DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Paginacao" AllowPaging="true"
                                PageSize="10" PagerSettings-Mode="Numeric" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                    <asp:BoundField HeaderText="Status" DataField="DescricaoStatus" />
<%--                                    <asp:TemplateField HeaderText="Procedimento">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%#bind("Nome") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
<%--                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%#bind("DescricaoStatus") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:HyperLinkField ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Codigo"
                                        Text="Selecionar" DataNavigateUrlFormatString="~/Urgencia/FormProcedimentosNaoFaturaveis.aspx?co_procedimento={0}" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
