<%@ Page Language="C#" MasterPageFile="~/MasterMain.Master" AutoEventWireup="true"
    CodeBehind="FaleConosco.aspx.cs" Inherits="ViverMais.View.FaleConosco" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top-faleconosco">
    <fieldset class="fieldsetfaleconosco">
    <legend>Contato</legend>
    <p style="font-family:arial; font-size:11px; margin-bottom:15px;">
    Utilize o formulário abaixo para entrar em contato conosco.
    </p>
    <p>
        <span class="rotulofaleconosco">Nome</span>
        <span">
        <asp:TextBox ID="tbxNome" runat="server" CssClass="campofaleconosco"></asp:TextBox>
        <asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" runat="server" 
            ErrorMessage="Preencha o campo Nome" ControlToValidate="tbxNome" 
            Display="Dynamic"></asp:RequiredFieldValidator></span>
    </p>
    <p>
    </p>
    <p>
        <span class="rotulofaleconosco">Email</span>
        <span">
        <span><asp:TextBox ID="tbxEmail" runat="server" CssClass="campofaleconosco"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
            ControlToValidate="tbxEmail" Display="Dynamic" 
            ErrorMessage="Preencha o campo Email"></asp:RequiredFieldValidator></span>
        
    </p>
    <p>
        <span class="rotulofaleconosco">Assunto</span>
        <span>
        <asp:TextBox ID="tbxAssunto" runat="server" CssClass="campofaleconosco"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
            ControlToValidate="tbxAssunto" Display="Dynamic" 
            ErrorMessage="Preencha o campo Assunto"></asp:RequiredFieldValidator> </span>
    </p>
    <p>
        <span class="rotulofaleconosco">Mensagem</span>
        <span>
        <asp:TextBox ID="tbxMensagem" runat="server" CssClass="campomult" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
            ControlToValidate="tbxMensagem" Display="Dynamic" 
            ErrorMessage="Digite a mensagem"></asp:RequiredFieldValidator> </span>
    </p>
    <p>
        <asp:LinkButton ID="lnkEnviar" runat="server" CssClass="" 
            onclick="lnkEnviar_Click">
         <img id="img_envia" alt="" src="img/enviar1.png"
                    onmouseover="img_envia.src='img/enviar2.png';"
                    onmouseout="img_envia.src='img/enviar1.png';" style="margin-left:0px; margin-bottom:20px; margin-top:10px;"  />   
            </asp:LinkButton>
    </p>
       
    </fieldset>
    </div>
</asp:Content>
