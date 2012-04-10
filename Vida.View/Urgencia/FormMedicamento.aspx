<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="FormMedicamento.aspx.cs" Inherits="ViverMais.View.Urgencia.WebForm1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div id="top">
        <h2>Cadastro de Medicamentos/Prescrição</h2>
    
    <fieldset class="formulario">
        <legend>Informações</legend>
        <p>
            <span class="rotulo">Nome:</span> 
            <span>
                <asp:TextBox ID="tbxMedicamento" CssClass="campo" runat="server" 
                Width="400px" MaxLength="120" ></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="tbxMedicamento">
                </asp:RequiredFieldValidator>
            </span>
        </p>
        <p>
            <span class="rotulo">É medicamento?</span>
            <span>
                <asp:CheckBox ID="chckMedicamento" runat="server" />
            </span>
        </p>
        <p align="center">
            <span>
                <asp:ImageButton ID="btnSalvar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png" Width="134px" Height="38px" onclick="btnSalvar_Click" />
            </span>
            <span>
                <asp:ImageButton ID="btnCancelar" runat="server"  CausesValidation="false" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png" Width="134px" Height="38px" PostBackUrl="~/Urgencia/FormBuscaMedicamento.aspx" />
            </span>
        </p>
    </fieldset>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
