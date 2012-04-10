<%@ Page Language="C#" MasterPageFile="~/Relatorio/RelatorioMaster.Master" AutoEventWireup="true"
    CodeBehind="RelatorioCNS.aspx.cs" Inherits="Vida.View.Relatorio.RelatorioCNS"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <fieldset>
            <legend>Opções do Relatório</legend>
            <p>
                <span class="rotulo">Unidade: </span>
                <asp:DropDownList ID="ddlUnidades" runat="server" DataTextField="NomeFantasia" 
                    DataValueField="CNES" class="campo" AutoPostBack="True" 
                    onselectedindexchanged="ddlUnidades_SelectedIndexChanged">
                </asp:DropDownList>
            </p>
            <br />
            <p>
                <span class="rotulo">Usuário: </span>
                <asp:DropDownList ID="ddlUsuario" runat="server" DataValueField="Codigo" DataTextField="Nome"  class="campo">
                </asp:DropDownList>
            </p>
            <br />
            <p>
                <span class="rotulo">Data Inicio: </span>
                <asp:TextBox ID="txtDataInicio" runat="server" MaxLength="10" class="campo"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditDataInicio" runat="server" ClearMaskOnLostFocus="true"
                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataInicio" >
                </cc1:MaskedEditExtender>
             </p>
             <br />
             <p>
                <span class="rotulo">Data Fim: </span>
                <asp:TextBox ID="txtDataFim" runat="server" MaxLength="10" class="campo"></asp:TextBox>
                <cc1:MaskedEditExtender ID="MaskedEditDataFim" runat="server" ClearMaskOnLostFocus="true"
                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="txtDataFim">
                </cc1:MaskedEditExtender>
            </p>
            <br />
            <p>
                <span class="rotulo">Quantidade de cartões: </span> 
                <asp:Label ID="lblQuantidadeCartoes" runat="server" Text=""></asp:Label>
            </p>
            <br />
            <p>
                <asp:Button ID="Button1" runat="server" Text="Contabilizar" onclick="Button1_Click" /> 
            </p>
    </fieldset>
    </div>
</asp:Content>
