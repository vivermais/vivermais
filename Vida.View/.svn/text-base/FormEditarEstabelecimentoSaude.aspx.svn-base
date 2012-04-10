<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEditarEstabelecimentoSaude.aspx.cs" Inherits="Vida.View.FormEditarEstabelecimentoSaude" MasterPageFile="~/MasterMain.Master" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    <asp:Label ID="lbRazaoSocial" runat="server" Text="Razão Social"></asp:Label>
    <asp:TextBox ID="tbxRazaoSocial" runat="server"></asp:TextBox>
    
    <asp:Label ID="lbNomeFantasia" runat="server" Text="Nome Fantasia"></asp:Label>
    <asp:TextBox ID="tbxNomeFantasia" runat="server"></asp:TextBox>
    
    <asp:Label ID="lbLogradouro" runat="server" Text="Logradouro"></asp:Label>
    <asp:TextBox ID="tbxLogradouro" runat="server"></asp:TextBox>
    
    <asp:Label ID="lbMunicipio" runat="server" Text="Munícipio Gestor"></asp:Label>
    <asp:DropDownList ID="ddlMunicipioGestor" runat="server">
    </asp:DropDownList>
    
    <asp:Label ID="lbBairro" runat="server" Text="Bairro"></asp:Label>
    <asp:TextBox ID="tbxBairro" runat="server"></asp:TextBox>
    
    <asp:Label ID="lbCEP" runat="server" Text="CEP"></asp:Label>
    <asp:TextBox ID="tbxCEP" runat="server"></asp:TextBox>
    
    <asp:Button ID="btn_atualizar" runat="server" Text="Atualizar" OnClick="btn_atualizar_Click" ValidationGroup="group_cadUnidade" />
    <asp:Button ID="btn_excluir"   runat="server" Text="Excluir"   OnClick="btn_excluir_Click" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir a undidade ?');" />
    <asp:Button ID="btn_ativar"    runat="server" Text="Ativar"    OnClick="btn_ativar_Click" Visible="false" />
    <asp:Button ID="btn_voltar"    runat="server" Text="Voltar"    PostBackUrl="~/FormEstabelecimentoDeSaude.aspx" />
    
    <div>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Razão Social é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxRazaoSocial" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nome Fantasia é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxNomeFantasia" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Logradouro é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxLogradouro" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Bairro é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxBairro" Display="None"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="CEP é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxCEP" Display="None"></asp:RequiredFieldValidator>
        
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Município Gestor é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="ddlMuncipioGestor" Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_cadUnidade" ShowMessageBox="true" ShowSummary="false" />
    </div>
</asp:Content>
