<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormRelatorioVagasAgendadasBasico.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioVagasAgendadasBasico"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Relatório de Vagas Para Procedimentos Agendados e Básico</h2>
        <fieldset class="formulario">
            <legend>Relatório de Vagas</legend>
            <p>
                <span class="rotulo">Tipo de Procedimento</span>
                <asp:RadioButtonList ID="rbtnTipoProcedimento" runat="server" CssClass="radio">
                    <%--<asp:ListItem Value="3">Agendado</asp:ListItem>
                    <asp:ListItem Value="4">Básico</asp:ListItem>--%>
                </asp:RadioButtonList>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredValidarTipoProcedimento" runat="server" ControlToValidate="rbtnTipoProcedimento"
                    Display="Dynamic" ErrorMessage="* Tipo de Procedimento Obrigatório" Font-Size="XX-Small"></asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:LinkButton ID="btnImprimir" runat="server" OnClick="btnImprimir_OnClick" CausesValidation="true">
                    <img src="img/imprimir_1.png" alt="Imprimir o Relatório" />
                </asp:LinkButton>
            </p>
        </fieldset>
    </div>
</asp:Content>
