<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormRegistrarReceita.aspx.cs" Inherits="ViverMais.View.Vacina.FormRegistrarReceita"
    Title="Untitled Page" %>

<%@ Register Src="../Paciente/WUCPesquisarPaciente.ascx" TagName="WUCPesquisarPaciente"
    TagPrefix="uc1" %>

<%@ Register Src="../Paciente/WUCExibirPaciente.ascx" TagName="WUCExibirPaciente"
    TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:WUCPesquisarPaciente ID="WUCPesquisarPaciente1" runat="server" />
    <uc3:WUCExibirPaciente ID="WUCExibirPaciente1" runat="server" />
    
    <p>
        Vacina:<asp:DropDownList ID="ddlVacina" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        Dose:<asp:DropDownList ID="ddlDose" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        Data:
        <asp:TextBox ID="tbxData" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:ImageButton ID="imgBtnSalvar" runat="server" 
            onclick="imgBtnSalvar_Click" />
    </p>
</asp:Content>
