<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCExibirPaciente.ascx.cs"
    Inherits="ViverMais.View.Paciente.WUCExibirPaciente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel_ExibirPaciente" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="false">
    <ContentTemplate>
        <asp:Panel ID="Panel_DadosPacientes" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend runat="server">Dados do Paciente</legend>
                <p>
                    <span class="rotulo">Nome:</span> <span>
                        <asp:Label ID="lblNomeWUC" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Nome da Mãe:</span> <span>
                        <asp:Label ID="lblNomeMaeWUC" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Data do Nascimento:</span> <span>
                        <asp:Label ID="lblDataNascimentoWUC" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Cartão do SUS:</span> <span>
                        <asp:Label ID="lblCartaoSUSWUC" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
