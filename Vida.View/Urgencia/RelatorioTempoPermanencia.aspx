<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master" AutoEventWireup="true" CodeBehind="RelatorioTempoPermanencia.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatorioTempoPermanencia" Title="Untitled Page" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <CR:CrystalReportViewer ID="CrystalReportViewer_TempoPermanencia" runat="server"
     DisplayGroupTree="False" AutoDataBind="true" />
<%--    <asp:GridView ID="GridView_TempoPermanencia" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="Prontuario" HeaderText="Número de Atendimento" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Paciente" HeaderText="Paciente" ItemStyle-HorizontalAlign="Center"/>
            <asp:BoundField DataField="Permanencia" HeaderText="Tempo de Permanência (Dias e Horas)" ItemStyle-HorizontalAlign="Center"/>
        </Columns>
    </asp:GridView>--%>
</asp:Content>
