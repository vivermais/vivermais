﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormCadastrarProgramaSaude.aspx.cs" Inherits="ViverMais.View.Agendamento.FormCadastrarProgramaSaude"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Cadastro de Programas de Saúde</h2>
        <asp:Panel ID="PanelListaFeriados" runat="server">
            <fieldset class="formulario">
                <legend>Cadastrado De Programa de Saúde</legend>
                <p>
                    <span class="rotulo">Nome do Programa</span> <span>
                        <asp:TextBox ID="tbxNomePrograma" runat="server" CssClass="campoMaiusculo" MaxLength="50"></asp:TextBox></span>
                </p>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNomePrograma"
                        Display="Dynamic" ErrorMessage="Digite o Nome do Programa" Font-Size="XX-Small"></asp:RequiredFieldValidator>
                </p>
                <p>
                    <span class="rotulo">Multi-Disciplinar?</span>
                    <span>
                        <asp:CheckBox ID="chkBoxMulti" runat="server" Text="Sim">
                        </asp:CheckBox>
                    </span>
                </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="Incluir" runat="server" OnClick="Incluir_Click" CausesValidation="true">
                       <img id="img_incluir" alt="Salvar" src="img/salvar_1.png"
                        onmouseover="img_incluir.src='img/salvar_2.png';"
                        onmouseout="img_incluir.src='img/salvar_1.png';" />
                    </asp:LinkButton></div>
                <div class="botoesroll">
                    <asp:LinkButton ID="Voltar" runat="server" PostBackUrl="~/Agendamento/BuscaProgramaSaude.aspx"
                        CausesValidation="false">
                       <img id="img_voltar" alt="Voltar" src="img/voltar_1.png"
                            onmouseover="img_voltar.src='img/voltar_2.png';"
                            onmouseout="img_voltar.src='img/voltar_1.png';" />
                    </asp:LinkButton></div>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
