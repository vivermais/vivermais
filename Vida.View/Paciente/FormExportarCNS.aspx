<%@ Page Language="C#" MasterPageFile="~/Paciente/MasterPaciente.Master" AutoEventWireup="true" CodeBehind="FormExportarCNS.aspx.cs" Inherits="ViverMais.View.Paciente.FormExportarCNS" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <fieldset class="fieldset2">
    <legend>Exportar CNS</legend>
    <p>
        <span class="rotulo">Data Inicial:</span><span><asp:TextBox ID="tbxDataInicial" runat="server" CssClass="campo"></asp:TextBox></span>
        </p>
        <p>
        <span class="rotulo">Data Final:</span><span><asp:TextBox ID="tbxDataFinal" runat="server" CssClass="campo"></asp:TextBox></span>
    </p>
    <span>
    <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" TargetControlID="tbxDataInicial"
        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
    </cc1:MaskedEditExtender>
    <asp:CompareValidator ID="compareData" runat="server" ControlToValidate="tbxDataInicial"
        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data Inicial é  inválida"
        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></span>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbxDataFinal"
        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
    </cc1:MaskedEditExtender>
    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbxDataFinal"
        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data Final é  inválida"
        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
    <p>
    <br />
    <asp:LinkButton ID="lnkBtnDownload" runat="server" 
        onclick="lnkBtnDownload_Click">
        <img id="imgbaixar" alt="Baixar" src="img/btn-baixar-1.png"
                onmouseover="imgbaixar.src='img/btn-baixar-2.png';"
                onmouseout="imgbaixar.src='img/btn-baixar-1.png';" />

        </asp:LinkButton>
        </p>
        </fieldset>
        </div>
</asp:Content>
