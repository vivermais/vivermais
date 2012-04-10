<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormDoenca.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormDoenca" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Formulário para Cadastro de Doença</h2>
        <fieldset>
            <legend>Doença</legend>
            <p>
                <span class="rotulo">Nome</span> <span>
                    <asp:TextBox ID="tbxNome" CssClass="campo" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é obrigatório!"
                    Display="None" ControlToValidate="tbxNome" ValidationGroup="ValidationGroup_cadDoenca"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                    ValidationGroup="ValidationGroup_cadDoenca" runat="server" />
            </p>
                    <div class="botoesroll">
                  <asp:LinkButton ID="lkn_salvar" runat="server" OnClick="btnSalvar_Click" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadDoenca')) return confirm('O nome da doença informado está correto ?'); return false;"
                    ValidationGroup="ValidationGroup_cadDoenca" >
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
                        </div>
                  <div class="botoesroll">
                  <asp:LinkButton ID="LinkButtonVoltar" runat="server" PostBackUrl="~/Vacina/FormExibeDoenca.aspx" >
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
                        </div>
                    <br /><br />
        </fieldset>
    </div>
</asp:Content>
