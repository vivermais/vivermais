﻿<%@ Page Language="C#" MasterPageFile="~/Relatorio/RelatorioMaster.Master" AutoEventWireup="true"
    CodeBehind="RelatorioGenerico.aspx.cs" Inherits="ViverMais.View.Relatorio.RelatorioGenerico"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>
        Relatório Genérico
    </h1>
    <p>
        Selecione os critérios do relatório abaixo</p>
    <p>
        Entidade:
        <asp:DropDownList ID="ddlEntidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEntidade_SelectedIndexChanged"
            Width="300px">
        </asp:DropDownList>
    </p>
    <p>
        Propriedade:<asp:DropDownList ID="ddlPropriedade" runat="server" AutoPostBack="true"
            OnSelectedIndexChanged="ddlPropriedade_SelectedIndexChanged" Width="300px">
        </asp:DropDownList>
    </p>
    <p>
        Expressão:<asp:DropDownList ID="ddlExpressao" runat="server">
            <asp:ListItem Value="0">Igual</asp:ListItem>
            <asp:ListItem Value="1">Diferente</asp:ListItem>
            <asp:ListItem Value="2">Maior</asp:ListItem>
            <asp:ListItem Value="3">Maior Igual</asp:ListItem>
            <asp:ListItem Value="4">Menor</asp:ListItem>
            <asp:ListItem Value="5">Menor Igual</asp:ListItem>
            <asp:ListItem Value="6">Contém</asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        Tipo:
        <asp:Label ID="lblTipo" runat="server" Text="-"></asp:Label>
        <%--<asp:DropDownList ID="ddlTipo" runat="server">
            <asp:ListItem Value="0">Número</asp:ListItem>
            <asp:ListItem Value="1">Texto</asp:ListItem>
            <asp:ListItem Value="2">Data</asp:ListItem>
            <asp:ListItem Value="3">Booleano</asp:ListItem>
        </asp:DropDownList>--%>
    </p>
    <p>
        Valor:
        <asp:TextBox ID="tbxValor" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:LinkButton ID="lnkAddExpressao" runat="server" OnClick="lnkAddExpressao_Click">Add Expressão</asp:LinkButton>
        &nbsp&nbsp
        <asp:LinkButton ID="lnkDelExpressao" runat="server" OnClick="lnkDelExpressao_Click">Del Expressão</asp:LinkButton>
    </p>
    <p>
        <asp:ListBox ID="lbxExpressoes" runat="server" Width="300px"></asp:ListBox>
    </p>
    <p>
        <asp:ImageButton ID="imgBtnEnviar" runat="server" OnClick="imgBtnEnviar_Click" />
    </p>
    <p>
        Total de Resultados:
        <asp:Label ID="lblTotalResultados" runat="server" Text="0"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridViewRelatorio" runat="server" Width="100%">
            <EmptyDataTemplate>
                <p>
                    Nenhum Registro Encontrado
                    </p>
            </EmptyDataTemplate>
        </asp:GridView>
    </p>
</asp:Content>
