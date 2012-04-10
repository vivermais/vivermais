<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncPesquisaLoteMedicamento.ascx.cs" Inherits="Vida.View.Farmacia.IncPesquisaLoteMedicamento" %>
<fieldset>
    <legend>Pesquisa de Lotes de Medicamento</legend>
    <p>
        <span>Medicamento</span>
        <span>
            <asp:DropDownList ID="DropDownList_Medicamento" runat="server">
                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span>Fabricante</span>
        <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
        </asp:DropDownList>
    </p>
    <p>
        <span>Lote</span>
        <span>
            <asp:TextBox ID="TextBox_Lote" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span>
            <asp:Button ID="Button_Pesquisar" runat="server" Text="Pesquisar" OnClick="OnClick_Pesquisar"/>
        </span>
    </p>
</fieldset>