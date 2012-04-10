﻿<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="FormCompetencia.aspx.cs" Inherits="ViverMais.View.EnvioBPA.FormCompetencia"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>
        Formulário de Competência
    </h2>
   <fieldset>
    <p>
    <span class="rotulo">
        Ano:
     </span>
     <span>
        <asp:TextBox ID="tbxAno" CssClass="campo" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
    <span class="rotulo">
        Mês: </span>
        
            <asp:DropDownList ID="ddlMes" runat="server">
            <asp:ListItem Value="01">Janeiro</asp:ListItem>
            <asp:ListItem Value="02">Fevereiro</asp:ListItem>
            <asp:ListItem Value="03">Março</asp:ListItem>
            <asp:ListItem Value="04">Abril</asp:ListItem>
            <asp:ListItem Value="05">Maio</asp:ListItem>
            <asp:ListItem Value="06">Junho</asp:ListItem>
            <asp:ListItem Value="07">Julho</asp:ListItem>
            <asp:ListItem Value="08">Agosto</asp:ListItem>
            <asp:ListItem Value="09">Setembro</asp:ListItem>
            <asp:ListItem Value="10">Outubro</asp:ListItem>
            <asp:ListItem Value="11">Novembro</asp:ListItem>
            <asp:ListItem Value="12">Dezembro</asp:ListItem>
        </asp:DropDownList>
       
    </p>
    <p>
    <span class="rotulo">
        Data Inicio:
        </span>
        <span>
        <asp:TextBox ID="tbxDataInicio" CssClass="campo" runat="server"></asp:TextBox>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataInicio">
        </cc1:CalendarExtender></span>
    </p>
    <p>
        <span class="rotulo">
        Data Fim:
        </span>
        <span>
        <asp:TextBox ID="tbxDataFim" runat="server" CssClass="campo"></asp:TextBox>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="tbxDataFim">
        </cc1:CalendarExtender>
        </span>
    </p>
    <p>
    <div class="botoesroll">
        <asp:LinkButton ID="imgBtnSalvar" runat="server" OnClick="imgBtnSalvar_Click">
                    <img id="img_salvar" alt="" src="img/salvar_1.png"
                onmouseover="img_salvar.src='img/salvar_2.png';"
                onmouseout="img_salvar.src='img/salvar_1.png';"/>
        </asp:LinkButton>
        </div>
     <div class="botoesroll">
        <asp:LinkButton ID="imgBtnVoltar" PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx"
            runat="server">            
                    <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
            </asp:LinkButton>
            </div>
    </p>
    </fieldset>
    </div>
</asp:Content>
