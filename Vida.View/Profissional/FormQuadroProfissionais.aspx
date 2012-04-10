<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormQuadroProfissionais.aspx.cs" Inherits="ViverMais.View.Profissional.FormQuadroProfissionais" MasterPageFile="~/MasterMain.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head"></asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Nome" DataField="Nome" />
        </Columns>
    </asp:GridView>
</asp:Content>
