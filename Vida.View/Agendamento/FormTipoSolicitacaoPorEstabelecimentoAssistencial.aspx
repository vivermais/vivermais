﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormTipoSolicitacaoPorEstabelecimentoAssistencial.aspx.cs" Inherits="ViverMais.View.Agendamento.FormTipoSolicitacaoPorEstabelecimentoAssistencial"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <%--<asp:UpdatePanel ID="Up1" runat="server">
        <Triggers>
        </Triggers>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lknSalvar" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Tipo de Solicitações por Estabelecimento Assistencial de Saúde</h2>
                <fieldset class="formulario">
                    <legend>Tipo de Solicitação</legend>
                        <p>
                        <span><asp:Label ID="lblCNES" runat="server" CssClass="rotulo" Text="Informe o CNES"></asp:Label></span>
                            <span><asp:TextBox ID="tbxCNES" runat="server" CssClass="campo" OnTextChanged="tbxCNES_TextChanged"
                                AutoPostBack="true"></asp:TextBox></span>
                            <span style="position:absolute;"><asp:Label ID="lblNomeFantasia" runat="server" Font-Bold="true"></asp:Label></span>
                        </p>
                        <p>
                        <span><asp:Label ID="lblTipoSolicitacao" runat="server" CssClass="rotulo" Text="Tipo de Solicitações"></asp:Label></span>
                            
                            <span><asp:CheckBoxList ID="chkbxTipoSolicitacao" runat="server" 
                                RepeatDirection="Vertical" CssClass="radio1" CellPadding="0" CellSpacing="0" TextAlign="Right">
                                <asp:ListItem Text="Regulado" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Autorizado" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Agendado" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Agendado Básico" Value="4"></asp:ListItem>
                            </asp:CheckBoxList></span>
                            
                        </p>


<div class="botoesroll">
                  <asp:LinkButton ID="lknSalvar" runat="server" OnClick="btnSalvar_Click" >
                  <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  onmouseover="imgsalvar.src='img/salvar_2.png';"
                  onmouseout="imgsalvar.src='img/salvar_1.png';" /></asp:LinkButton>
                        </div>
                    <div class="botoesroll">
                  <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx" >
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                        </div>
                        <br />
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
