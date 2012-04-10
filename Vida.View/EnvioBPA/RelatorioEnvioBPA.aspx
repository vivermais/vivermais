﻿<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="RelatorioEnvioBPA.aspx.cs" Inherits="ViverMais.View.EnvioBPA.RelatorioEnvioBPA"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>
    Relatório de Envio de BPA por Ano
    </h2>
    
    <fieldset>
    <p>
    <span class="rotulo">
        CNES: 
    </span>
    <span>
        <asp:Label ID="lblCNES" runat="server" Text="Label"></asp:Label>
        </span>
    </p>
    <p>
<%--    <span class="rotulo">
        Login: 
    </span>--%>
    <span>
      <%--  <asp:Label ID="lblLogin" runat="server" Text="Label"></asp:Label>--%>
        </span>
    </p>

    <p>
        <span class="rotulo">Ano:</span>
        <span><asp:DropDownList ID="ddlAno" runat="server" >
            <asp:ListItem>2010</asp:ListItem>
            <asp:ListItem>2009</asp:ListItem>
        </asp:DropDownList>
        </span>
        </p>
    
        
           
    
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Protocolo" />
                <asp:BoundField DataField="Competencia" HeaderText="Competência" />
                <asp:BoundField DataField="Arquivo" HeaderText="Nome do Arquivo" />
                <asp:BoundField DataField="TamanhoArquivo" HeaderText="Tamanho do Arquivo" />
                <asp:BoundField DataField="NumeroControle" HeaderText="Nº Controle" />
                <asp:BoundField DataField="Usuario" HeaderText="Resp. pelo Envio" />
                <asp:BoundField DataField="DataEnvio" 
                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Data" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="empty" runat="server" Text="Nenhum dado encontrado." />
            </EmptyDataTemplate>
        </asp:GridView>
    </p>
    <p>
    
     <div class="botoesroll">
        <asp:LinkBUtton ID="imgBtnEnviar" runat="server" 
            onclick="imgBtnEnviar_Click">
            <img id="img_enviar" alt="" src="img/enviar1.png"
                onmouseover="img_enviar.src='img/enviar2.png';"
                onmouseout="img_enviar.src='img/enviar1.png';"/>
            </asp:LinkBUtton>
            </div>
        
    <div class="botoesroll">
        <asp:LinkButton ID="imgBtnVoltar" runat="server" PostBackUrl="~/EnvioBPA/Default.aspx">
        <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
        </asp:LinkButton>
        </div>
       
    </p>
    </fieldset>
    </div>
</asp:Content>
