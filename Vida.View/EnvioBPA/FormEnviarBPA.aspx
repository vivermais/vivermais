<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="FormEnviarBPA.aspx.cs" Inherits="ViverMais.View.EnvioBPA.FormEnviarBPA"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <h2>
        Formulário de Envio de BPA
    </h2>
    <fieldset>
    <p>
    <span class="rotulo">
        CNES:
    </span>
    <span>
        <asp:Label ID="lblCNES" runat="server" Text="[CNES]"></asp:Label>
        </span>
    </p>
    <p>
    <span class="rotulo">
        Usuário:
        </span>
        <span>
        <asp:Label ID="lblUsuario" runat="server" Text="[Usuario]"></asp:Label>
        </span>
    </p>
    <p>
    <span>
        Competência(AAAA.MM): 
        </span>
        <asp:DropDownList ID="ddlCompetencia" runat="server">
        </asp:DropDownList>
        </p>
    <p>
    <span class="rotulo">
        Localizar Arquivo:
        </span>
         <asp:FileUpload ID="FileUpload1" runat="server" Width="300px" />
    </p>
    <p>
    <span class="rotulo">
        Número de Controle: 
        </span>
        <span>
        <asp:TextBox ID="tbxNumeroControle" runat="server"  CssClass="campo"
            MaxLength="4" Width="60px"></asp:TextBox>
            </span>
    </p>
    <p>
    <div class="botoesroll">
        <asp:LinkButton ID="imgbtnEnviar" runat="server"  onclick="imgbtnEnviar_Click">
                    <img id="img_enviar" alt="" src="img/enviar1.png"
                onmouseover="img_enviar.src='img/enviar2.png';"
                onmouseout="img_enviar.src='img/enviar1.png';"/>
        </asp:LinkButton>
        </div>
   <div class="botoesroll">
        <asp:LinkButton ID="imgbtnVoltar" runat="server" PostBackUrl="~/EnvioBPA/Default.aspx"> 
                <img id="imgvoltar" alt="" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';"/>
        </asp:LinkButton>
        </div>
    </p>
    </fieldset>
    </div>
</asp:Content>
