<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    CodeBehind="FormRequisicaoMedicamentos.aspx.cs" Inherits="ViverMais.View.Farmacia.FormRequisicaoMedicamentos" EnableEventValidation="false" %>
<%@ Register Src="~/Farmacia/WUCPesquisarRequisicao.ascx" TagName="WUCPesquisarRequisicao" TagPrefix="WUCPesquisarRequisicao" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
    <h2>Requisição de Medicamentos<span style=" padding-left:300px">Passo 1 de 2</span></h2>
    <fieldset class="formulario">
        <legend>Formulário de Abertura</legend>
        <p>
            <span class="rotulo">Farmácia</span><span>
                <asp:DropDownList ID="DropDownList_Farmacia" runat="server" DataValueField="Codigo" CssClass="campo" Height="22px"
                    DataTextField="Nome">
                </asp:DropDownList>
            </span>
        </p>
        <p>
            <span class="rotulo">Data de Abertura </span>
            <span style="font-weight:bold">
                <asp:Label ID="Label_DataAbertura" runat="server" Text=""></asp:Label>
            </span>
        </p>

            <div class="botoesroll">
                <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="ValidationGroup_AbrirRequisicao"
                 OnClick="OnClick_AbrirRequisicao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_AbrirRequisicao')) return confirm('Tem certeza que deseja abrir uma nova requisição de medicamentos ?'); return false;">                 <img id="solicitar" alt="Voltar" src="img/btn/avancar1.png"                  onmouseover="solicitar.src='img/btn/avancar2.png';"                  onmouseout="solicitar.src='img/btn/avancar1.png';" />                 </asp:LinkButton>                 </div>                  <div class="botoesroll">                <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Farmacia/FormBuscaRM.aspx">                 <img id="volta1" alt="Voltar" src="img/btn/voltar1.png"                  onmouseover="volta1.src='img/btn/voltar2.png';"                  onmouseout="volta1.src='img/btn/voltar1.png';" />                </asp:LinkButton>            </div>    </fieldset>
    </div>
   <%-- <WUCPesquisarRequisicao:WUCPesquisarRequisicao ID="WUCPesquisarRequisicao" runat="server" />--%>
    
    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma farmácia." Display="None" ControlToValidate="DropDownList_Farmacia" ValueToCompare="-1" Operator="GreaterThan" ValidationGroup="ValidationGroup_AbrirRequisicao"></asp:CompareValidator>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_AbrirRequisicao" />
</asp:Content>
