<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormRecepcaoPaciente.aspx.cs"
    Inherits="ViverMais.View.Atendimento.FormRecepcaoPaciente" MasterPageFile="~/Atendimento/MasterAtendimento.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Paciente/WUCPesquisarPaciente.ascx" TagName="WUCPesquisarPaciente"
    TagPrefix="uc1" %>
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
        <asp:Panel ID="Panel_Paciente" runat="server">
            <asp:UpdatePanel ID="UpdatePanel_PacienteSUS" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <uc1:WUCPesquisarPaciente ID="WUC_PesquisarPaciente" runat="server" />
                    <asp:Panel ID="Panel_PacientesPesquisados" Visible="false" runat="server">
                        <fieldset class="formulario">
                            <legend>Resultado da Busca</legend>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_ResultadoPesquisa" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="20" DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                                        OnRowDataBound="OnRowDataBound_CriandoGridView" PagerSettings-Mode="Numeric"
                                        Width="100%" OnSelectedIndexChanging="OnSelectedIndexChanging_Atendimento">
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
                    <asp:Panel ID="Panel_Atendimento" runat="server" Visible="false">
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
                                <span class="rotulo">Atendimento Prioritário</span>
                                <asp:CheckBoxList ID="chkboxPrioridades" runat="server">
                                    <asp:ListItem Text="GráViverMais" Value="G"></asp:ListItem>
                                    <asp:ListItem Text="Criança de Colo" Value="C"></asp:ListItem>
                                </asp:CheckBoxList>
                            </p>
                            <p>
                                <span class="rotulo">Serviço</span><span>
                                    <asp:DropDownList ID="ddlServicoSenhador" CssClass="drop" AutoPostBack="true" OnSelectedIndexChanged="ddlServicoSenhador_SelectedIndexChanged"
                                        runat="server">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <asp:Panel ID="PanelSolicitacoes" Visible="false" runat="server">
                                <fieldset class="formulario">
                                    <legend>Solicitações Agendadas</legend>
                                    <p>
                                        <span>
                                            <asp:GridView ID="GridViewSolicitacoes" runat="server" AutoGenerateColumns="false"
                                                AllowPaging="true" PageSize="20" DataKeyNames="Codigo" OnPageIndexChanging="GridViewSolicitacoes_OnPageIndexChanging"
                                                OnRowDataBound="OnRowDataBound_CriandoGridView" PagerSettings-Mode="Numeric"
                                                Width="100%" OnSelectedIndexChanging="GridViewSolicitacoes_OnSelectedIndexChanging">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Situação" DataField="NomeSituacao" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Data de Agenda" DataField="DataAgenda" DataFormatString="{0:dd/MM/yyyy}"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Profissional" DataField="NomeProfissionalExecutante"
                                                        ItemStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" ItemStyle-HorizontalAlign="Center" />
                                                    <asp:CommandField SelectText="Selecionar" ShowSelectButton="true" />
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" Width="600px" />
                                                <HeaderStyle CssClass="tab" />
                                                <RowStyle CssClass="tabrow" />
                                            </asp:GridView>
                                        </span>
                                    </p>
                                </fieldset>
                            </asp:Panel>
                            <asp:Panel ID="PanelProfissional" runat="server" Visible="false">
                                <p>
                                    <span class="rotulo">
                                        <asp:Label ID="lblProfissional" runat="server" CssClass="label_itens" />
                                    </span>
                                </p>
                            </asp:Panel>
                            <asp:Panel ID="Panel_LinkButtonIniciarAtendimento" runat="server">
                                <div class="botoesroll">
                                    <asp:LinkButton ID="LinkButton_IniciarAtendimento" runat="server" OnClick="OnClick_IniciarAtendimento">
                                <img id="img6" alt="Iniciar Atendimento" src="img/bts/btn-iniciar-atendimento1.png"
                                    onmouseover="this.src='img/bts/btn-iniciar-atendimento2.png';"
                                    onmouseout="this.src='img/bts/btn-iniciar-atendimento1.png';" />
                                    </asp:LinkButton>
                                </div>
                            </asp:Panel>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="Panel_AcoesPosAtendimento" runat="server" Visible="false" Width="450px">
                        <fieldset class="formulario">
                            <legend>Ações</legend>
                            <div class="botoesroll">
                                <asp:LinkButton ID="ButtonSair" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
	                            <img id="btnsair" alt="Sair" src="img/bts/btn-sair-1.png"
                                            onmouseover="this.src='img/bts/btn-sair-2.png';"
                                            onmouseout="this.src='img/bts/btn-sair-1.png';" />
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
                                <asp:LinkButton ID="LinkButton_ReimprimirSenha" runat="server" OnClick="OnClick_ReimprimirSenha">
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
    </div>
</asp:Content>
