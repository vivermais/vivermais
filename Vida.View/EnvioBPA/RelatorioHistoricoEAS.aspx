<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True" CodeBehind="RelatorioHistoricoEAS.aspx.cs" Inherits="ViverMais.View.EnvioBPA.RelatorioHistoricoEAS" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="top">
<h2>
Relatório de Histórico de Envio de BPA por Unidade e Ano
</h2>
<fieldset>
<p>
<span class="rotulo">
    CNES: 
     </span>
     
    <asp:TextBox ID="tbxCNES" runat="server" CssClass="campo"></asp:TextBox>
</p>
<p>
<span class="rotulo">
    Ano:
    </span>
    <asp:DropDownList ID="ddlAno" runat="server">
        <asp:ListItem>2010</asp:ListItem>
        <asp:ListItem>2009</asp:ListItem>
    </asp:DropDownList>
</p>
<p>
<div class="botoesroll">
    <asp:LinkButton ID="imgEnviar" runat="server" onclick="imgEnviar_Click">
    <img id="img_enviar" alt="" src="img/enviar1.png"
                onmouseover="img_enviar.src='img/enviar2.png';"
                onmouseout="img_enviar.src='img/enviar1.png';"/>
    </asp:LinkButton>
</div>
<div class="botoesroll">
    <asp:LinkButton ID="imgBtnVoltar" PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx" runat="server">
            <img id="imgpesquisar" alt="" src="img/voltar_1.png"
                onmouseover="imgpesquisar.src='img/voltar_2.png';"
                onmouseout="imgpesquisar.src='img/voltar_1.png';"/>
    </asp:LinkButton>
    </div>
</p>
<p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField HeaderText="Mês" DataField="CompetenciaDate" 
                DataFormatString="{0:MMM}" />
            <asp:BoundField DataField="Arquivo" HeaderText="Nome do Arquivo" />
            <asp:BoundField DataField="Usuario" HeaderText="Resp. pelo Envio" />
            <asp:BoundField DataField="DataEnvio" 
                DataFormatString="{0: dd/MM/yyyy HH:mm:ss}" HeaderText="Data do Envio" />
        </Columns>
    </asp:GridView>
</p>

</fieldset>
</div>
</asp:Content>
