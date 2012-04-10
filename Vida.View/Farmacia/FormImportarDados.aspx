﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImportarDados.aspx.cs" Inherits="ViverMais.View.Farmacia.FormImportarDados" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<asp:LinkButton ID="LinkButton4" runat="server" OnClick="OnClick_Teste">Teste</asp:LinkButton>--%>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_ImportarUnidadeMedida">Importar Unidade Medida</asp:LinkButton> <br />
        <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_ImportarFabricante">Importar Fabricante</asp:LinkButton> <br />--%>
        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="OnClick_ImportarMedicamento">Importar Medicamento</asp:LinkButton> <br />
        <%--
        <asp:LinkButton ID="LinkButton6" runat="server"  OnClick="OnClick_ImportarElenco">Importar Elenco Medicamento</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton7" runat="server"  OnClick="OnClick_ImportarSubElenco">Importar Sub-Elenco Medicamento</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton8" runat="server"  OnClick="OnClick_ImportarElencoSubElenco">Importar Relação Elenco e Sub-Elenco</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton10" runat="server" OnClick="OnClick_ImportarElencoMedicamento">Importar Relação Elenco e Medicamento</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton9" runat="server"  OnClick="OnClick_ImportarSubElencoMedicamento">Importar Relação Sub-Elenco e Medicamento</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton12" runat="server" OnClick="OnClick_ImportarLoteMedicamento">Importar Lote Medicamento</asp:LinkButton><br />
        
        <p>
            <asp:DropDownList ID="DropDownList_Farmacia" DataTextField="nomefarmacia" DataValueField="id_farmacia"
             runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaCNES">
            </asp:DropDownList>
            <asp:TextBox ID="TextBox_CNES" runat="server" MaxLength="7"></asp:TextBox><asp:Button ID="Button" runat="server" Text="ok" OnClick="OnClick_VerificarCNES" />
            Unidade no Sisfarma 2.0:
            <asp:Label ID="Label_Unidade2" runat="server" Text="Nenhum"></asp:Label> <br />
            
            Unidade no ViverMais:
            <asp:Label ID="Label_Unidade" runat="server" Text="Nenhum"></asp:Label>
            <asp:Button ID="Button1" runat="server" Text="Adicionar Farmácia" OnClick="OnClick_AdicionarFarmacia" />
        </p>
        
        <p>
            <asp:GridView ID="GridView_Farmacia" runat="server" AutoGenerateColumns="false"
                OnRowDeleting="OnRowDeleting_DeletarFarmacia">
                <Columns>
                    <asp:BoundField HeaderText="Farmacia" DataField="Farmacia" />
                    <asp:BoundField HeaderText="Unidade" DataField="Unidade" />
                    <asp:CommandField ShowDeleteButton="True" DeleteText="Excluir" />
                </Columns>
            </asp:GridView>
        </p>
        
        <asp:LinkButton ID="LinkButton5" runat="server" OnClick="OnClick_ImportarFarmacia">Importar Farmacia</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton11" runat="server" OnClick="OnClick_ImportarElencoFarmacia">Importar Relação Elenco e Farmácia</asp:LinkButton> <br />
        <asp:LinkButton ID="LinkButton13" runat="server" OnClick="OnClick_ImportarEstoqueFarmacia">Importar Estoque Farmacia</asp:LinkButton><br />
        
        <br />
        Unidade de Saúde
        <asp:GridView ID="GridView_Unidade" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="NomeFantasia" HeaderText="Unidade" />
                <asp:BoundField DataField="CNES" HeaderText="CNES" />
            </Columns>
        </asp:GridView>--%>
    </div>
    </form>
</body>
</html>