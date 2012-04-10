<%@ Page Language="C#" MasterPageFile="~/Seguranca/MasterSeguranca.Master" AutoEventWireup="true"
    CodeBehind="AlterarSenha.aspx.cs" Inherits="ViverMais.View.Seguranca.AlterarSenha"
    Title="Untitled Page" %>
<%@ Register Src="~/Seguranca/WUCAlterarSenha.ascx" TagName="TagAlterarSenha" TagPrefix="WUCAlterarSenha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<asp:UpdatePanel ID="Up" runat="server" >
    <ContentTemplate>
    <div id="top">
    <h2>
        Alterar Senha
    </h2><br />
    <fieldset class="formulario">
        <legend>Informações</legend>
        <WUCAlterarSenha:TagAlterarSenha ID="ccAlterarSenha" runat="server"></WUCAlterarSenha:TagAlterarSenha>
    </fieldset>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>

<%--    <div id="top">
        <h2>
            Alterar Senha
        </h2>
    
    <fieldset class="formulario">
    <legend>Formulário</legend>
        <p>
            <span class="rotulo">Senha Atual</span> 
            <span>
                <asp:TextBox ID="tbxSenhaAtual" CssClass="campo" TextMode="Password" runat="server" 
                MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                ControlToValidate="tbxSenhaAtual" Display="Dynamic" 
                ErrorMessage="O campo Senha Atual é Obrigatório"></asp:RequiredFieldValidator>
            </span>
        </p>
        <p>
            <span class="rotulo">Senha Nova</span>
            <span>
                <asp:TextBox ID="tbxSenhaNova" CssClass="campo" TextMode="Password" runat="server" 
                MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                ControlToValidate="tbxSenhaNova" Display="Dynamic" 
                ErrorMessage="O campo Senha Nova é Obrigatório"></asp:RequiredFieldValidator>
            </span>
        </p>
        <p>
            <span class="rotulo">Confirme a Senha Nova</span> 
            <span>
                <asp:TextBox ID="tbxConfirmSenhaNova" CssClass="campo" TextMode="Password" runat="server" 
                MaxLength="10"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                ControlToValidate="tbxConfirmSenhaNova" Display="None" 
                ErrorMessage="A Confirmação da Senha Nova é Obrigatória" ValidationGroup="ValidationGroup_AlterarSenha"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                ErrorMessage="Os campos de senha nova devem ser iguais" 
                ControlToCompare="tbxSenhaNova" ControlToValidate="tbxConfirmSenhaNova" 
                Display="None" ValidationGroup="ValidationGroup_AlterarSenha"></asp:CompareValidator>
            </span>
        </p>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="A Senha Nova não deve possuir valor igual a '123456'! Por favor, informe outra senha."
         ControlToValidate="tbxSenhaNova" ValueToCompare="123456" Operator="NotEqual" Display="None" ValidationGroup="ValidationGroup_AlterarSenha"></asp:CompareValidator>
        
        
  <div class="botoesroll">
      <asp:LinkButton ID="LnkbtnSalvar" runat="server" onclick="btnSalvar_Click" ValidationGroup="ValidationGroup_AlterarSenha">
      <img id="imgsalvar" alt="Salvar" src="img/btn_salvar_1.png"
      onmouseover="imgsalvar.src='img/btn_salvar_2.png';"
      onmouseout="imgsalvar.src='img/btn_salvar_1.png';" /></asp:LinkButton>
                        </div>
                        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_AlterarSenha"/>
    </fieldset>
    </div>--%>
</asp:Content>
