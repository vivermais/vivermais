<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="RelatoriosAdministrativos.aspx.cs" Inherits="ViverMais.View.EnvioBPA.RelatoriosAdministrativos"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>
        Ferramentas Administrativas
    </h2>
    <fieldset>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkCadastrarCompetencia" NavigateUrl="~/EnvioBPA/FormCompetencia.aspx"
            runat="server">Cadastrar Competência</asp:HyperLink>
            </div>
    </p>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkEditarCompetencia" runat="server" NavigateUrl="~/EnvioBPA/ListarCompetencias.aspx">Editar Competência</asp:HyperLink>
    </div>
    </p>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkHistoricoMes" runat="server" NavigateUrl="~/EnvioBPA/RelatorioHistoricoMes.aspx">Histórico Mês</asp:HyperLink>
    </div>
    </p>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkHistoricoEAS" runat="server" NavigateUrl="~/EnvioBPA/RelatorioHistoricoEAS.aspx">Histórico do Estabelecimento de Saúde</asp:HyperLink>
    </div>
    </p>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkReceberProducao" runat="server" NavigateUrl="~/EnvioBPA/ReceberProducao.aspx">Receber Produção</asp:HyperLink>
    </div>
    </p>
    <br />
    <p>
    <div class="botao-bpa">
        <asp:HyperLink ID="lnkConsultaProtocolo" runat="server" NavigateUrl="~/EnvioBPA/ConsultaProtocolo.aspx">Consultar Protocolo</asp:HyperLink>
    </div>
    </p>
    <br />
    <p>
        <asp:LinkButton ID="imgBtnVoltar" runat="server" PostBackUrl="~/EnvioBPA/Default.aspx">
                <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
        </asp:LinkButton>
    </p>
    </fieldset>
    </div>
</asp:Content>
