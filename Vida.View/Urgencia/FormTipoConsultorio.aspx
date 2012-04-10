<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="FormTipoConsultorio.aspx.cs" Inherits="ViverMais.View.Urgencia.WebForm1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <fieldset>
        <legend>Tipo Consultório</legend>
        <p>
            <span>Descrição:</span>
            <asp:TextBox ID="TextBox_Descricao" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" SetFocusOnError="true"
              ControlToValidate="TextBox_Descricao" ErrorMessage="RequiredFieldValidator" ValidationGroup="cadTipoConsultorio">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <span>Cor:</span>
            <asp:TextBox ID="TextBox_Cor" runat="server"></asp:TextBox>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" SetFocusOnError="true"
              ControlToValidate="TextBox_Cor" ErrorMessage="RequiredFieldValidator" ValidationGroup="cadTipoConsultorio">*</asp:RequiredFieldValidator>
        </p>
        
        <p>
            <asp:Button ID="ButtonSalvarTipo" runat="server" Text="Salvar" CommandArgument="salvar" OnClick="OnClick_SalvarTipoConsultorio" ValidationGroup="cadTipoConsultorio" />
            <asp:Button ID="ButtonCancelar"   runat="server" Text="Cancelar" OnClick="OnClick_Cancelar" />
        </p>
    </fieldset>
    
    <fieldset>
        <legend>Tipos Cadastrados</legend>
        <p>
            <asp:GridView ID="GridView_TipoConsultorio" DataKeyNames="Codigo" OnRowCommand="OnRowCommand_Acao"
             AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Editar" HeaderText="Descricao" DataTextField="Descricao" />
                    <asp:BoundField HeaderText="Cor" DataField="Cor" />
                </Columns>
                
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado!"></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </p>
    </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
