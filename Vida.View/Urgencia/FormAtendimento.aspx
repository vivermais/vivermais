﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormAtendimento.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormAtendimento" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Paciente/WUCPesquisarPaciente.ascx" TagName="WUCPesquisarPaciente"
    TagPrefix="IncPesquisarPaciente" %>
<asp:Content ContentPlaceHolderID="head" ID="c_head" runat="server">
    <style type="text/css">
        .formulario2
        {
            width: 690px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 5px 5px 10px 5px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    <div id="top">
        <h2>
            Atendimento de Paciente</h2>
        <br />
        <asp:Panel ID="Panel_PacienteNormal" runat="server" Visible="false">
            <IncPesquisarPaciente:WUCPesquisarPaciente ID="WUC_PesquisarPaciente" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel_PacienteSUS" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Panel ID="Panel_PacientesPesquisados" Visible="false" runat="server">
                        <fieldset class="formulario">
                            <legend>Resultado da Busca</legend>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_ResultadoPesquisa" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="20" DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                                        OnRowDataBound="OnRowDataBound_CriandoGridView" PagerSettings-Mode="Numeric"
                                        Width="100%" OnSelectedIndexChanging="OnSelectedIndexChanging_AtendimentoNormal">
                                        <Columns>
                                            <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Mãe" DataField="NomeMae" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:CommandField SelectText="Selecionar" ShowSelectButton="true" />
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" Width="600px" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado! Para cadastrar um novo paciente "></asp:Label>
                                            <asp:HyperLink ID="HyperLink_NovoPaciente" runat="server">Clique Aqui.</asp:HyperLink>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="tab" />
                                        <RowStyle CssClass="tabrow" />
                                    </asp:GridView>
                                </span>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="Panel_AtendimentoNormal" runat="server" Visible="false">
                        <fieldset class="formulario">
                            <legend>Atendimento</legend>
                            <p>
                                <span class="rotulo">Nome</span> <span>
                                    <asp:Label ID="Label_NomePacienteNormal" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data de Nascimento</span> <span>
                                    <asp:Label ID="Label_DataNascimentoPacienteNormal" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Nome da Mãe</span> <span>
                                    <asp:Label ID="Label_MaePacienteNormal" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Acolhimento</span> <span>
                                    <asp:RadioButtonList ID="RadioButtonList_TipoAcolhimentoNormal" runat="server" RepeatColumns="2"
                                        RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="camporadio_1">
                                    </asp:RadioButtonList>
                                </span>
                            </p>
                            <asp:Panel ID="Panel_LinkButtonIniciarAtendimentoNormal" runat="server">
                                <div class="botoesroll">
                                    <asp:LinkButton ID="LinkButton_IniciarAtendimentoNormal" runat="server" OnClick="OnClick_IniciarAtendimento">
                                <img id="img6" alt="Iniciar Atendimento" src="img/bts/btn-iniciar-atendimento1.png"
                                    onmouseover="this.src='img/bts/btn-iniciar-atendimento2.png';"
                                    onmouseout="this.src='img/bts/btn-iniciar-atendimento1.png';" />
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="Panel_AcoesPosAtendimentoNormal" runat="server" Visible="false" Width="450px">
                        <fieldset class="formulario">
                            <legend>Ações</legend>
                            <div class="botoesroll">
                                <asp:LinkButton ID="ButtonSair" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
	<img id="btnsair" alt="Sair" src="img/btn-sair-1.png"
                onmouseover="this.src='img/btn-sair-2.png';"
                onmouseout="this.src='img/btn-sair-1.png';" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_NovoAtendimento" runat="server" OnClientClick="window.location='FormAtendimento.aspx?tipo_atendimento=normal';">
	<img id="Img3" alt="Novo Atendimento" src="img/bts/btn-novo-atendimento1.png"
                onmouseover="this.src='img/bts/btn-novo-atendimento2.png';"
                onmouseout="this.src='img/bts/btn-novo-atendimento1.png';" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_ImprimirFichaAtendimento" runat="server" OnClick="OnClick_ImprimirFichaAtendimentoPaciente">
	<img id="ImprimirFichaAtendimento" alt="Imprimir Ficha de Atendimento" src="img/btn-imprimir-ficha-aten1.png"
                onmouseover="this.src='img/btn-imprimir-ficha-aten2.png';"
                onmouseout="this.src='img/btn-imprimir-ficha-aten1.png';" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_ReimprimirSenhaAcolhimento" runat="server" OnClick="OnClick_ReimprimirSenhaAcolhimento">
                                                             <img id="img1" alt="Reimpressão de Senha" src="img/bts/btn-reimprimir-senha1.png"
                onmouseover="this.src='img/bts/btn-reimprimir-senha2.png';"
                onmouseout="this.src='img/bts/btn-reimprimir-senha1.png';" />
                                </asp:LinkButton>
                            </div>
                        </fieldset>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <asp:Panel ID="Panel_PacienteSimplificado" runat="server" Visible="false">
            <fieldset class="formulario2">
                <legend>Atendimento Simplificado</legend>
                <p>
                    <asp:LinkButton ID="lnkBiometria" runat="server" PostBackUrl="~/Urgencia/FormAtendimentoBiometriaPaciente.aspx?tipo_atendimento=simplificado">
                    <img id="img_newbiometria" alt="Identificação Biométrica" src="img/bts/id_biometrica1.png"
                        onmouseover="this.src='img/bts/id_biometrica2.png';"
                        onmouseout="this.src='img/bts/id_biometrica1.png';" />
                    </asp:LinkButton>
                </p>
                <asp:UpdatePanel ID="UpdatePanel_SituacaoPaciente" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <p>
                            <span class="rotulo">Situação</span> <span>
                                <asp:RadioButtonList ID="RadioButtonList_SituacaoSimplificado" runat="server" RepeatColumns="2"
                                    RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="camporadio_1" AutoPostBack="true"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_VerificaProntuarioAberto">
                                    <asp:ListItem Text="Desacordado" Value="Desacordado" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Desorientado" Value="Desorientado"></asp:ListItem>
                                </asp:RadioButtonList>
                            </span>
                        </p>
                        <%--                        <p>
                            <span class="rotulo">Desacordado</span> <span>
                                <asp:RadioButton ID="RadioButton_Desacordado" Checked="true" CssClass="camporadio_1"
                                    CausesValidation="true" runat="server" GroupName="GroupName_TipoPaciente" OnCheckedChanged="OnCheckedChanged_VerificaProntuarioAberto"
                                    AutoPostBack="true" />
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Desorientado</span> <span>
                                <asp:RadioButton ID="RadioButton_Desorientado" CausesValidation="true" CssClass="camporadio_1"
                                    runat="server" GroupName="GroupName_TipoPaciente" OnCheckedChanged="OnCheckedChanged_VerificaProntuarioAberto"
                                    AutoPostBack="true" />
                            </span>
                        </p>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel_PacienteSimplificado" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList_SituacaoSimplificado" EventName="SelectedIndexChanged" />
                        <%--                        <asp:AsyncPostBackTrigger ControlID="RadioButton_Desacordado" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="RadioButton_Desorientado" EventName="CheckedChanged" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel_IdentificacaoPacienteSimplificado" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Nome</span> <span>
                                    <asp:Label ID="Label_NomePacienteSimplificado" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data de Nascimento</span> <span>
                                    <asp:Label ID="Label_DataNascimentoPacienteSimplificado" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Nome da Mãe</span> <span>
                                    <asp:Label ID="Label_MaePacienteSimplificado" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_TipoAcolhimentoSimplificado" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Acolhimento</span> <span>
                                    <asp:RadioButtonList ID="RadioButtonList_TipoAcolhimentoSimplificado" runat="server"
                                        RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="camporadio_1">
                                    </asp:RadioButtonList>
                                </span>
                            </p>
                        </asp:Panel>
                        <p>
                            <span class="rotulo">Descrição:</span> <span>
                                <asp:TextBox ID="tbxDescricao" CssClass="campo" runat="server" Height="110px" Rows="3"
                                    TextMode="MultiLine" Width="620px"></asp:TextBox>
                            </span>
                        </p>
                        <asp:Panel ID="Panel_SalvarPacienteSimplificado" runat="server">
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_IniciarAtendimentoSimplificado" runat="server" OnClick="OnClick_IniciarAtendimentoSimplificado"
                                    ValidationGroup="ValidationGroup_Simplificado">
                <img id="imgsalvar" alt="Iniciar Atendimento" src="img/bts/btn-iniciar-atendimento1.png"
                onmouseover="this.src='img/bts/btn-iniciar-atendimento2.png';"
                onmouseout="this.src='img/bts/btn-iniciar-atendimento1.png';" /></asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_SairSimplificado" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
	                            <img id="Img4" alt="Sair" src="img/btn-sair-1.png"
                                    onmouseover="this.src='img/btn-sair-2.png';"
                                    onmouseout="this.src='img/btn-sair-1.png';" />
                            </asp:LinkButton>
                        </div>
                        <asp:Panel ID="Panel_AcoesPacienteSimplificado" runat="server" Visible="false">
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_NovoAtendimentoSimplificado" runat="server" OnClientClick="window.location='FormAtendimento.aspx?tipo_atendimento=simplificado';">
	<img id="Img5" alt="Novo Atendimento" src="img/bts/btn-novo-atendimento1.png"
                onmouseover="this.src='img/bts/btn-novo-atendimento2.png';"
                onmouseout="this.src='img/bts/btn-novo-atendimento1.png';" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton_ReimprimirSenhaAtendimento" runat="server" OnClick="OnClick_ReimprimirSenhaAtendimento">
                                    <img id="img2" alt="Reimpressão de Senha" src="img/bts/btn-reimprimir-senha1.png"
                                        onmouseover="this.src='img/bts/btn-reimprimir-senha2.png';"
                                        onmouseout="this.src='img/bts/btn-reimprimir-senha1.png';" />
                                </asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Descricao" Font-Size="XX-Small"
                                runat="server" ErrorMessage="Descrição é obrigatório!" ControlToValidate="tbxDescricao"
                                Display="None" ValidationGroup="ValidationGroup_Simplificado"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary2" Font-Size="XX-Small" runat="server"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Simplificado" />
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
