<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True" CodeBehind="RelatorioHistoricoMes.aspx.cs" Inherits="ViverMais.View.EnvioBPA.RelatorioHistoricoMes" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="top">
<h2>
Relatório de Envio por Mês
</h2>
<fieldset>
<%--<p>
    Mês: 
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
    Ano: 
    <asp:DropDownList ID="ddlAno" runat="server">
        <asp:ListItem>2010</asp:ListItem>
    </asp:DropDownList>
</p>--%>
<p><span class="rotulo">
    Competências: </span> 
    <span>
   
    <asp:DropDownList ID="ddlCompetencias" runat="server">
    </asp:DropDownList>
    </span>
</p>
<p>
<div class="botoesroll">
    <asp:LinkButton ID="imbBtnPesquisar" runat="server" 
        onclick="imbBtnPesquisar_Click">
               <img id="imgpesquisar" alt="" src="img/pesquisar_1.png"
                onmouseover="imgpesquisar.src='img/pesquisar_2.png';"
                onmouseout="imgpesquisar.src='img/pesquisar_1.png';"/> 
        </asp:LinkButton>
        </div>
        
        <div class="botoesroll">
    <asp:LinkButton ID="imgBtnVoltar" PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx" runat="server">
            <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
    </asp:LinkButton>
    </div>
</p>
<p>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="EstabelecimentoSaude" 
                HeaderText="Estabelecimento de Saúde" />
            <asp:BoundField DataField="Arquivo" HeaderText="Nome do Arquivo" />
            <asp:BoundField DataField="Usuario" HeaderText="Responsável pelo Envio" />
            <asp:BoundField DataField="DataEnvio" 
                DataFormatString="{0: dd/MM/yyyy HH:mm:ss}" HeaderText="Data do Envio" />
        </Columns>
    </asp:GridView>
</p>
<p>

</p>
</fieldset>
</div>
</asp:Content>
