﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormSolicitacoesPorPaciente.aspx.cs" Inherits="ViverMais.View.Agendamento.FormSolicitacoesPorPaciente"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Solicitações Por Paciente</h2>
        <fieldset class="formulario">
            <legend>Dados do Paciente</legend>
            <p>
                <span class="rotulo">Nome:</span>
                <asp:Label ID="lblNome" runat="server" Font-Bold="true" Font-Size="13px" Font-Names="Verdana">-</asp:Label>
            </p>
            <p>
                <span class="rotulo">CNS:</span>
                <asp:Label ID="lblCNS" runat="server" Font-Bold="true" Font-Size="13px" Font-Names="Verdana">-</asp:Label>
            </p>
            <p>
                <span class="rotulo">Sexo:</span>
                <asp:Label ID="lblSexo" runat="server" Font-Bold="true" Font-Size="13px" Font-Names="Verdana">-</asp:Label>
            </p>
            <p>
                <span class="rotulo">Data de Nascimento:</span>
                <asp:Label ID="lblDataNascimento" runat="server" Font-Bold="true" Font-Size="13px"
                    Font-Names="Verdana">-</asp:Label>
            </p>
            <p>
                <span class="rotulo">Telefone:</span>
                <asp:Label ID="lblTelefone" runat="server" Font-Bold="true" Font-Size="13px" Font-Names="Verdana">-</asp:Label>
            </p>
            <p>
                <span class="rotulo">Município:</span>
                <asp:Label ID="lblMunicipio" runat="server" Font-Bold="true" Font-Size="13px" Font-Names="Verdana">-</asp:Label>
            </p>
        </fieldset>
        <p>
            &nbsp;
        </p>
        <fieldset class="formulario">
            <legend>Lista de Solicitações</legend>
            <p>
                <asp:GridView ID="GridViewSolicitacoes" AllowPaging="true" runat="server" AutoGenerateColumns="False"
                    OnPageIndexChanging="GridViewSolicitacoes_PageIndexChanging" OnRowDataBound="GridViewSolicitacoes_OnRowDataBound"
                    OnRowCommand="GridViewSolicitacoes_OnRowCommand"
                    BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical" Width="100%" DataKeyNames="Codigo">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelecionar" Enabled="true" runat="server" CommandName="Selecionar" CommandArgument='<%# Eval("Codigo") %>'>
                                    <asp:Image ID="imgSelecionar" AlternateText="Selecionar" ImageUrl="~/img/bts/select.png" runat="server" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NomePrioridade" HeaderText="Prioridade">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Data_Solicitacao" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data">
                        </asp:BoundField>
                        <%--<asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormAutoriza.aspx?id_solicitacao={0}"
                            DataTextField="Data_Solicitacao" DataTextFormatString="{0:dd/MM/yyyy}" HeaderText="Data"
                            Text="Data" Target="_self">
                            <ItemStyle Width="70px" />
                        </asp:HyperLinkField>--%>
                        <%--<asp:CommandField ShowSelectButton="True" ButtonType="Link" SelectText="<img src='../img/bts/select.png' alt=''/>"
                            ControlStyle-Height="23" ControlStyle-Width="22" HeaderText="Selecionar"></asp:CommandField>--%>
                        <asp:BoundField DataField="Procedimento" HeaderText="Procedimento"></asp:BoundField>
                        <asp:BoundField DataField="NomeSituacao" HeaderText="Status"></asp:BoundField>
                        <asp:BoundField DataField="DataAgenda" HeaderText="Data AGENDA" />
                        <asp:BoundField DataField="NomeUnidadeExecutante" HeaderText="EAS" />
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                        Font-Size="11px" Height="22px" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </p>
        </fieldset>
        <p>
            &nbsp;</p>
        <div class="botoesroll">
            <asp:LinkButton ID="btnConcluir" runat="server" CausesValidation="False" OnClick="btnConcluir_Click">
      	            <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png" onmouseover="imgsalvar.src='img/salvar_2.png';"
      	                onmouseout="imgsalvar.src='img/salvar_1.png';" />
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
