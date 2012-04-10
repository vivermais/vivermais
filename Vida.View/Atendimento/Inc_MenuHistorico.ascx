<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_MenuHistorico.ascx.cs" Inherits="ViverMais.View.Atendimento.Inc_MenuHistorico" %>
<asp:BulletedList ID="BulletedList_Menu" runat="server" DisplayMode="LinkButton" BulletStyle="Numbered"
 OnClick="OnClick_RedirecionaJanela">
    <%--<asp:ListItem Text="" Value=""></asp:ListItem>--%>
</asp:BulletedList>
<%--<ul>
    <li><asp:LinkButton ID="LinkButton_Acolhimento" runat="server">Acolhimento</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_AtestadosReceitas" runat="server">Atestados/Receitas</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_ConsultaMedica" runat="server">Consulta Médica</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_EvolucoesEnfermagem" runat="server">Evoluções Enfermagem</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_EvolucoesMedica" runat="server">Evoluções Médica</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_ExamesEletivos" runat="server">Exames Eletivos</asp:LinkButton></li>
    <li><asp:LinkButton ID="LinkButton_Exames" runat="server">Exames Internos</asp:LinkButton></li>
    <asp:Panel ID="Panel_FichaAtendimento" runat="server" Visible="false">
        <li><asp:LinkButton ID="LinkButton_FichaAtendimento" runat="server">Ficha Atendimento</asp:LinkButton></li>
    </asp:Panel>
    <li><asp:LinkButton ID="LinkButton_Prescricoes" runat="server">Prescrições</asp:LinkButton></li>
</ul>--%>