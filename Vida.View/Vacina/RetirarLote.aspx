﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RetirarLote.aspx.cs" Inherits="ViverMais.View.Vacina.RetirarLote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <p>
            <span>Vacina </span><span>
                <asp:DropDownList ID="DropDownList_Vacina" DataTextField="Nome" AutoPostBack="true"
                 OnSelectedIndexChanged="OnSelectedIndexChanged"
                    DataValueField="Codigo" runat="server">
                </asp:DropDownList>
                Código da Vacina<asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </span>
        </p>
        <p>
            <span>Fabricante </span><span>
                <asp:DropDownList ID="DropDownList_Fabricante" DataTextField="Nome"
                    DataValueField="Codigo" runat="server">
                </asp:DropDownList>
            </span>
        </p>
        <p>
            <span>Validade </span><span>
                <asp:TextBox ID="TextBox_Validade" runat="server"></asp:TextBox>
            </span>
        </p>
        <p>
            <span>Lote</span><span>
                <asp:TextBox ID="TextBox_Lote" runat="server"></asp:TextBox>
            </span>
        </p>
        <p>
            <span>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_Incluir">Incluir na Pesquisa</asp:LinkButton>
            </span>
        </p>
    </div>
    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
        MaskType="Date" InputDirection="LeftToRight" TargetControlID="TextBox_Validade">
    </cc1:MaskedEditExtender>
    <div>
        <p>
            <span>
                <asp:GridView ID="GridView_Pesquisa" runat="server" AutoGenerateColumns="false" Width="600px"
                    OnRowDeleting="OnRowDeleting_Excluir">
                    <Columns>
                        <asp:BoundField HeaderText="Vacina" DataField="NomeVacina" />
                        <asp:BoundField HeaderText="Validade" DataField="DataValidade" />
                        <asp:BoundField HeaderText="Lote" DataField="Lote" />
                        <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" />
                        <asp:CommandField ButtonType="Link" DeleteText="Delete" ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </span>
        </p>
        <p>
            <span>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_GerarArquivos">Gerar Arquivos</asp:LinkButton>
            </span>
        </p>
    </div>
    </form>
</body>
</html>
