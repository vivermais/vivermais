<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" AutoEventWireup="true" CodeBehind="ImpressaoProntuario.aspx.cs" Inherits="ViverMais.View.Urgencia.ImpressaoProntuario" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 149px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="top"></div>
    <table style="width:100%;">
        <tr>
            <td class="style1">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label2" runat="server" Text="Nº do Prontuário"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Prontuario" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label3" runat="server" Text="Data do Prontuário"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_DataProntuario" runat="server" Text="HGT"></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Text="Nome"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Nome" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label4" runat="server" Text="Data de Nascimento"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_DataNascimento" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label5" runat="server" Text="Situação Atual"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_SituacaoAtual" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label6" runat="server" Text="Idade"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Idade" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label7" runat="server" Text="Sexo"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_Sexo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label8" runat="server" Text="Freq. Cardíaca"></asp:Label>
            </td>
            <td>
                <asp:Label ID="Label_FreqCardiaca" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label9" runat="server" Text="Freq. Respiratória"></asp:Label>
                </td>
            <td>
                <asp:Label ID="Label_FreqRespiratoria" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label10" runat="server" Text="Tensão Arterial"></asp:Label>
                </td>
            <td>
                <asp:Label ID="Label_TensaoArterial" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label11" runat="server" Text="Temperatura"></asp:Label></td>
            <td>
                <asp:Label ID="Label_Temperatura" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label12" runat="server" Text="HGT"></asp:Label></td>
            <td>
                <asp:Label ID="Label_HGT" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="style1">
                <asp:Label ID="Label13" runat="server" Text="Outras informações"></asp:Label></td>
            <td>
                <asp:Label ID="Label_OutrasInfo" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Panel ID="Panel_ProntuarioMedico" runat="server">
                </asp:Panel>
                </td>
        </tr>
    </table>
</asp:Content>
