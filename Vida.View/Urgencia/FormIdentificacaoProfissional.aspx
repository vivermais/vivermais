<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="FormIdentificacaoProfissional.aspx.cs" Inherits="ViverMais.View.Urgencia.FormIdentificacaoProfissional"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
        <p>
            <span>O código de identificação serve para identificação rápida dos profissionais<br />
                responsáveis por realizar os registros de evolução de pacientes. </span>
        </p>
    </fieldset>
    <fieldset>
        <legend>Identificação</legend>
        <p>
            <span>Usuário</span>
            <asp:TextBox ID="TextBox_Usuario" runat="server"></asp:TextBox>
        </p>
        <p>
            <span>Senha</span>
            <asp:TextBox ID="TextBox_Senha" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button_Identificar" runat="server" Text="Confirmar Identificação"
                OnClick="Button_Identificar_Click" />
        </p>
        <p>
            <span>
                <asp:Label ID="Label_Resultado" runat="server" ForeColor="Red"></asp:Label>
            </span>
        </p>
    </fieldset>
    <fieldset>
        <legend>Dados do profissional</legend>
        <p>
            <span>Profissional</span> <span>
                <asp:Label ID="Label_ExibeIdentificacao" runat="server" Text="" Visible="false"></asp:Label>
            </span>
            <asp:Label ID="Label_Profissional" runat="server" Text=""></asp:Label>
        </p>
        <p>
            <span>Código de Identificação</span>         
        </p>
        <p>
            <span>
            <asp:Button ID="Button_ExibirCodigo" runat="server" Text="Exibir Código de Identificação"
                Width="196px" OnClick="Button_ExibirCodigo_Click" />
            </span>
        </p>
    </fieldset>
    <fieldset>
        <legend>Modificar Código de Identificação</legend>
        <p>
            <span>Código de Identificação</span>
            <asp:TextBox ID="TextBox_Identificacao" runat="server" TextMode="Password"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
        </p>
    </fieldset>
</asp:Content>
