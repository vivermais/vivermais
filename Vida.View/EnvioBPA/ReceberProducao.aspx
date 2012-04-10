<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="ReceberProducao.aspx.cs" Inherits="ViverMais.View.EnvioBPA.ReceberProducao"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="top">
    <h2>
        Recebimento de Produção
    </h2>
    <fieldset>
    <p>
        <span class="rotulo">Usuário:</span>
        <span>
        <asp:Label ID="lblUsuario" runat="server" Text=""></asp:Label>
        </span>
    </p>
    <p>
   <span class="rotulo">
        Competência:
        </span>
        <span>
        <asp:DropDownList ID="ddlCompetencias" runat="server">
        </asp:DropDownList>
        </span>
        <div class="botoesroll">
        <asp:LinkButton ID="imgBtnEnviar" runat="server" OnClick="imgBtnEnviar_Click" Style="height: 16px">
        <img id="img_enviar" alt="" src="img/enviar1.png"
                onmouseover="img_enviar.src='img/enviar2.png';"
                onmouseout="img_enviar.src='img/enviar1.png';"/>
        </asp:LinkButton>
        </div>
            <div class="botoesroll">
        <asp:LinkButton ID="lnkVoltar" runat="server" PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx">
          <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
        </asp:LinkButton>
        </div>
    </p>
        
        
        <%--        <asp:DropDownList ID="ddlMes" runat="server">
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
--%>
    </p>
    <p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            onrowcommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="EstabelecimentoSaude" HeaderText="Estabelecimento de Saúde" />
                <asp:BoundField DataField="Usuario" HeaderText="Responsável pelo Envio" />
                <asp:BoundField DataField="DataEnvio" DataFormatString="{0: dd/MM/yyyy HH:mm:ss}"
                    HeaderText="Data do Envio" />
                <asp:HyperLinkField DataNavigateUrlFields="CaminhoArquivo" DataNavigateUrlFormatString="/EnvioBPA/upload/{0}"
                    DataTextField="Arquivo" HeaderText="Download" />
                <asp:ButtonField Text="Excluir" />
            </Columns>
        </asp:GridView>
    </p>
</fieldset>
    </div>
</asp:Content>
