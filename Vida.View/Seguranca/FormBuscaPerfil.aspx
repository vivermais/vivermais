﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormBuscaPerfil.aspx.cs"
    Inherits="ViverMais.View.Seguranca.FormBuscaPerfil" MasterPageFile="~/Seguranca/MasterSeguranca.Master"
    EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" ID="c_head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder3" ID="c_body" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Lista de Perfis Cadastrados</h2>
                <fieldset class="formulario">
                    <legend>Perfis</legend>
                    <p>
                    <span class="rotulo">Módulo</span>
                    <span>
                    <asp:DropDownList ID="DropDownList_Sistema" runat="server" CssClass="drop" Height="21px" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Pesquisar">
                            <asp:ListItem Selected="True" Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList></span>
                        <span><asp:ImageButton ID="ImgButtonCadastrar" runat="server" 
                            PostBackUrl="~/Seguranca/FormPerfil.aspx" 
                            ImageUrl="~/Seguranca/img/novo_registro.png" Height="20px" Width="138px" 
                            style="position:absolute; z-index:-1; float:left;"/></span>
                    </p>
                    <br />
                </fieldset>
                <asp:Panel ID="Panel_ResultadoPesquisa" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Resultados da Busca</legend>
                        <asp:GridView ID="GridView_ResultadoPesquisa" Width="100%" AutoGenerateColumns="False" runat="server"
                            OnPageIndexChanging="OnPageIndexChanging_Paginacao" AllowPaging="True" PageSize="20"
                            PagerSettings-Mode="Numeric" BackColor="White" BorderColor="#999999" 
                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Font-Size="12px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Codigo" ItemStyle-HorizontalAlign="Center" DataNavigateUrlFormatString="FormPerfil.aspx?co_perfil={0}"
                                    DataTextField="Nome" HeaderText="Perfil"></asp:HyperLinkField>
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5b5b5b" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
