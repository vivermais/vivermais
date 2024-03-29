﻿<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True" CodeBehind="FormVincularUsuarioDistrito.aspx.cs" Inherits="ViverMais.View.Farmacia.WebForm1" Title="Untitled Page" %>
<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>Vincular Usuário a Distrito Sanitário
    </h2>
    <fieldset class="formulario">
    <p>
        <asp:GridView ID="GridView_Usuarios" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            PageSize="20" OnPageIndexChanging="OnPageIndexChanging_Paginacao" PagerSettings-Mode="Numeric"
            OnSelectedIndexChanged="GridView_Usuarios_SelectedIndexChanged">
            <Columns>
                <%--<asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFormatString="FormUsuario.aspx?co_usuario={0}"
             DataNavigateUrlFields="Codigo" DataTextField="Nome" />--%>
                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                <asp:BoundField HeaderText="Login" DataField="Login" />
                <asp:BoundField HeaderText="Unidade" DataField="UnidadeToString" />
                <asp:CommandField ShowSelectButton="True" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator"
            OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
    </p>
    <p>
        Usuário:
        <asp:Label ID="lblNome" runat="server"></asp:Label>
    </p>
    <p>
        Distrito Vinculado:
        <asp:Label ID="lblVinculo" runat="server"></asp:Label>
    </p>
    <p>
        Caso deseje alterar o distrito vinculado, selecione um abaixo e clique em Vincular
    </p>
    <p>
        <asp:DropDownList ID="ddlDistrito" runat="server">
        </asp:DropDownList>
    </p>
    <p>
            <asp:LinkButton ID="btnVincular" runat="server" OnClick="btnVincular_Click">
                  <img id="imgvincular" alt="Vincular" src="img/btn/vincular1.png"
                  onmouseover="imgvincular.src='img/btn/vincular2.png';"
                  onmouseout="imgvincular.src='img/btn/vincular1.png';" /></asp:LinkButton>
    </p>
    <p>
    Consulta de Usuários por Distrito
    </p>
    
        <asp:DropDownList ID="ddlConsultaDistrito" runat="server">
        </asp:DropDownList>
        <br /><br />
        <div class="botoesroll">
        <asp:LinkButton ID="btnPesquisar" runat="server"  onclick="btnPesquisar_Click" CausesValidation="false">
                  <img id="imgpesquisar" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                </div>
    <p>
        <asp:GridView ID="GridViewUsuariosDistrito" runat="server" 
            AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Nome" HeaderText="Nome" />
            </Columns>
        </asp:GridView>
    </p>
    </fieldset>
    </div>
</asp:Content>
