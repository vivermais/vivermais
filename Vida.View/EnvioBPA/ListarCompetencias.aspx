﻿<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="ListarCompetencias.aspx.cs" Inherits="ViverMais.View.EnvioBPA.ListarCompetencias"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="top">
    <h2>
        Competências
    </h2>
    <fieldset>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%">
            <Columns>
            
                <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormCompetencia.aspx?id_competencia={0}"
                    DataTextField="Mes" HeaderText="Mês"  ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Ano" HeaderText="Ano" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="DataInicial" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Inicial" ItemStyle-HorizontalAlign="Center"  />
                <asp:BoundField DataField="DataFinal" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Final"  ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
             <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
             <HeaderStyle BackColor="#6a96b6" ForeColor="White" />
               <EmptyDataTemplate>
                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum Registro foi Encontrado."></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </p>
    <p>
        <asp:LinkButton ID="imgBtnVoltar" PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx" runat="server">
                    <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
            </asp:LinkButton>
    </p>
    </fieldset>
    </div>
</asp:Content>
