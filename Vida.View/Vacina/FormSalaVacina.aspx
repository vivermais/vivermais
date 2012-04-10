﻿<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="True"
    CodeBehind="FormSalaVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormSalaVacina"
    Title="ViverMais - Sala de Vacina" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Seguranca/WUCPesquisarUsuario.ascx" TagName="TagName_PesquisarUsuario"
    TagPrefix="TagPrefix_PesquisarUsuario" %>
<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="TagName_Estabelecimento"
    TagPrefix="TagPrefix_Estabelecimento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário para Cadastro de Sala de Vacina</h2>
        <br />
        <br />
        <cc1:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="740px"
            ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Dados Gerais" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Sala de Vacina</legend>
                        <TagPrefix_Estabelecimento:TagName_Estabelecimento ID="EAS" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Unidade</span> <span>
                                        <asp:DropDownList ID="DropDown_EAS" CssClass="drop" runat="server" DataTextField="NomeFantasia"
                                            DataValueField="CNES" AutoPostBack="True" Width="380px" OnSelectedIndexChanged="DropDown_EAS_SelectedIndexChanged">
                                            <asp:ListItem Text="SELECIONE..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:TextBox ID="TextBox_Nome" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Bloqueado</span> <span class="camporadio">
                                <asp:RadioButton ID="RadioButton_Bloqueado" CssClass="camporadio" Width="20px" GroupName="GroupName_Bloqueado"
                                    runat="server" />Sim
                                <asp:RadioButton ID="RadioButton_NaoBloqueado" CssClass="camporadio" Width="20px"
                                    Checked="true" GroupName="GroupName_Bloqueado" runat="server" />Não </span>
                        </p>
                        <p>
                            <span class="rotulo">Situação</span> <span class="camporadio">
                                <asp:RadioButton ID="RadioButton_Ativo" CssClass="camporadio" Width="20px" Checked="true"
                                    GroupName="GroupName_Situacao" runat="server" />Ativo
                                <asp:RadioButton ID="RadioButton_Inativo" CssClass="camporadio" Width="20px" GroupName="GroupName_Situacao"
                                    runat="server" />Inativo </span>
                        </p>
                        <p>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Unidade é Obrigatório."
                                Display="None" ControlToValidate="DropDown_EAS" ValueToCompare="-1" Operator="GreaterThan"
                                ValidationGroup="ValidationGroup_cadSalaVacina"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é obrigatório!"
                                Display="None" ControlToValidate="TextBox_Nome" ValidationGroup="ValidationGroup_cadSalaVacina"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="ValidationGroup_cadSalaVacina" runat="server" />
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione uma Unidade."
                     Display="None" ValueToCompare="-1" Operator="GreaterThan" ControlToValidate="DropDown_EAS"
                      ValidationGroup="ValidationGroup_PesquisarUsuariosEstabelecimento"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarUsuariosEstabelecimento" />
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Usuários" ScrollBars="Horizontal">
                <ContentTemplate>
                    <TagPrefix_PesquisarUsuario:TagName_PesquisarUsuario ID="WUC_PesquisarUsuario" runat="server" />
                    <asp:UpdatePanel ID="UpdatePanel_UsuariosSala" runat="server" UpdateMode="Conditional"
                        ChildrenAsTriggers="true">
                        <%-- <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel1$DropDown_EAS" EventName="SelectedIndexChanged" />
                        </Triggers>--%>
                        <ContentTemplate>
                            <fieldset class="formulario">
                                <legend>Usuários da Sala</legend>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_UsuariosSala" runat="server" DataKeyNames="CodigoUsuario"
                                            AutoGenerateColumns="false" AllowPaging="true" OnRowDataBound="OnRowDataBound_UsuarioSala"
                                            PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_UsuarioSala"
                                            OnSelectedIndexChanging="OnSelectedIndexChanging_UsuarioSala"
                                            Width="100%" EnableModelValidation="true"
                                            OnRowDeleting="OnRowDeleting_UsuarioSala">
                                            <Columns>
                                                <asp:BoundField DataField="NomeUsuario" HeaderText="Nome" />
                                                <asp:BoundField DataField="CartaoSUSUsuario" HeaderText="Cartão SUS" />
                                                <asp:BoundField DataField="DataNascimentoUsuario" HeaderText="Data de Nascimento"
                                                    DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:ButtonField ButtonType="Link" CausesValidation="true" HeaderText="Responsável?"
                                                    CommandName="Select" />
                                                <asp:ButtonField ButtonType="Link" CausesValidation="true" HeaderText="Excluir" CommandName="Delete"
                                                    Text="<img src='img/excluir_gridview.png' border=0 title='Deseja desvincular o usuário com esta sala de vacina?'/>" />
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Não existe nenhum usuário vínculado nesta sala."></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle HorizontalAlign="Center" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" HorizontalAlign="Center" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" HorizontalAlign="Center" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </fieldset>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <div class="botoesroll" style="margin-left: 200px;">
            <asp:LinkButton ID="Lkn_Salvar" runat="server" OnClick="OnClick_Salvar" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadSalaVacina')) return confirm('Todos os dados da sala de vacina estão corretos ?'); return false;">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="Lkn_Cancelar" runat="server" CausesValidation="false" PostBackUrl="~/Vacina/FormExibeSalaVacina.aspx">
                  <img id="imgvoltar" alt="Cancelar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
        </div>
    </div>
</asp:Content>