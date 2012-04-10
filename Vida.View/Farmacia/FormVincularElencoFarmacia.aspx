<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="FormVincularElencoFarmacia.aspx.cs" Inherits="ViverMais.View.Farmacia.FormVincularElencoFarmacia"
    Title="ViverMais - Vínculo de Elencos e Farmácias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário de Vincúlo de Elencos e Farmácias</h2>
        <fieldset class="formulario">
            <legend>Vinculos</legend>
            <p>
                <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                    obrigatórios</span>
            </p>
            <p>
                <span class="rotulo">Farmácia:</span> <span>
                    <asp:DropDownList ID="ddlFarmacia" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                        ControlToValidate="ddlFarmacia" ErrorMessage="RequiredFieldValidator" InitialValue="0"
                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Elenco:</span> <span>
                    <asp:DropDownList ID="ddlElenco" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server"
                        ControlToValidate="ddlElenco" ErrorMessage="RequiredFieldValidator" InitialValue="0"
                        SetFocusOnError="true">*</asp:RequiredFieldValidator>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click">
                <img id="imgvincular" alt="Vincular" src="img/btn/vincular1.png"
                onmouseover="imgvincular.src='img/btn/vincular2.png';"
                onmouseout="imgvincular.src='img/btn/vincular1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="btnExcluir" runat="server" OnClick="btnExcluir_Click">
                <img id="imgdesvincular" alt="Desvincular" src="img/btn/desvincular1.png"
                onmouseover="imgdesvincular.src='img/btn/desvincular2.png';"
                onmouseout="imgdesvincular.src='img/btn/desvincular1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="btnCancelar" runat="server" OnClick="btnCancelar_Click">
                <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <fieldset class="formulario">
            <legend>Farmácias/Elencos Vinculados</legend>
            <p>
                <span style="margin-left: 5px;">
                    <asp:TreeView ID="TreeViewFarmacia" runat="server" OnSelectedNodeChanged="TreeViewFarmacia_SelectedNodeChanged">
                    </asp:TreeView>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
