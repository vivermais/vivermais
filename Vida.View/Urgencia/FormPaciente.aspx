<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPaciente.aspx.cs" Inherits="ViverMais.View.Urgencia.FormPaciente"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Paciente/WUCPesquisarPaciente.ascx" TagName="TagName_PesquisarPaciente"
    TagPrefix="TagPrefix_PesquisarPaciente" %>
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
    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Atendimento de Paciente</h2>
        <br />
        <%--<fieldset class="formulario">
            <legend>Formulário para iniciar atendimento</legend>--%>
        <%--            <asp:LinkButton ID="lnkBiometria" runat="server" OnClick="lnkBiometria_Click">
                    <img id="img_newbiometria" alt="Identificação Biométrica" src="img/bts/id_biometrica1.png"
                        onmouseover="img_newbiometria.src='img/bts/id_biometrica2.png';"
                        onmouseout="img_newbiometria.src='img/bts/id_biometrica1.png';" />
            </asp:LinkButton>--%>
        <asp:Panel ID="Panel_PacienteNormal" runat="server" Visible="false">
            <%--                <fieldset class="formulario2">
                    <legend>Vínculo SUS</legend>
                    <p>
                        <span class="rotulo">Cartão SUS</span> <span>
                            <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" runat="server" MaxLength="15"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nome</span> <span>
                            <asp:TextBox ID="tbxNomePaciente" CssClass="campo" runat="server" MaxLength="100"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nome da Mãe</span> <span>
                            <asp:TextBox ID="tbxNomeMae" CssClass="campo" runat="server" MaxLength="100"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data de Nascimento</span> <span>
                            <asp:TextBox ID="tbxDataNascimento" CssClass="campo" runat="server"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbxDataNascimento"
                                Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                            </cc1:CalendarExtender>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnBuscarPaciente" runat="server" OnClick="OnClick_BuscarPaciente"
                            CausesValidation="true" ValidationGroup="group_pesquisaPaciente">
                <img id="imgbuscar" alt="Validar" src="img/bts/btn_buscar1.png"
                onmouseover="imgbuscar.src='img/bts/btn_buscar2.png';"
                onmouseout="imgbuscar.src='img/bts/btn_buscar1.png';" /></asp:LinkButton>
                    </div>
                </fieldset>--%>
            <%--Colocar como False e depois Ativar--%>
            <TagPrefix_PesquisarPaciente:TagName_PesquisarPaciente ID="WUC_PesquisarPaciente"
                runat="server" />
            <asp:UpdatePanel ID="UpdatePanel_PacienteSUS" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <%--                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnBuscarPaciente" EventName="Click" />
                    </Triggers>--%>
                <ContentTemplate>
                    <asp:Panel ID="PanelResultado" Visible="false" runat="server">
                        <fieldset class="formulario">
                            <legend>Resultado da Busca</legend>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_ResultadoPesquisa" runat="server" AutoGenerateColumns="false"
                                        AllowPaging="true" PageSize="20" DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                                        OnRowDataBound="OnRowDataBound_CriandoGridView" PagerSettings-Mode="Numeric"
                                        Width="100%">
                                        <Columns>
                                            <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Mãe" DataField="NomeMae" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnIniciarAtendimento" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                                        OnClick="OnClick_IniciarAtendimento">
                <img id="imginiciarat" alt="Iniciar Atendimento" src="img/bts/btn_iniciarat1.png"
                onmouseover="this.src='img/bts/btn_iniciarat2.png';"
                onmouseout="this.src='img/bts/btn_iniciarat1.png';" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" Width="600px" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado! Para cadastrar um novo paciente "></asp:Label>
                                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Paciente/FormPaciente.aspx">Clique Aqui.</asp:HyperLink>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="tab" />
                                        <RowStyle CssClass="tabrow" />
                                    </asp:GridView>
                                </span>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="Panel_AcoesPacienteSUS" runat="server" Visible="false" Width="450px">
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
                                <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="window.location='FormPaciente.aspx?tipo_paciente=pacientesus';">
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
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
        <%--            <div>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" MaskType="Date" Mask="99/99/9999"
                    TargetControlID="tbxDataNascimento" runat="server">
                </cc1:MaskedEditExtender>
                <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server"
                    ValidationGroup="group_pesquisaPaciente" ShowMessageBox="true" ShowSummary="false" />
                <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                    ErrorMessage="Data Nascimento deve ser maior que 01/01/1900!" ControlToValidate="tbxDataNascimento"
                    ValueToCompare="01/01/1900" Type="Date" Operator="GreaterThanEqual" Display="None"
                    ValidationGroup="group_pesquisaPaciente"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                    ErrorMessage="Data Nascimento inválida!" ControlToValidate="tbxDataNascimento"
                    Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="group_pesquisaPaciente"></asp:CompareValidator>
            </div>--%>
        <asp:Panel ID="Panel_PacienteSimplificado" runat="server" Visible="false">
            <fieldset class="formulario2">
                <legend>Simplificado</legend>
                <p>
                    <asp:LinkButton ID="lnkBiometria" runat="server" OnClick="lnkBiometria_Click">
                    <img id="img_newbiometria" alt="Identificação Biométrica" src="img/bts/id_biometrica1.png"
                        onmouseover="img_newbiometria.src='img/bts/id_biometrica2.png';"
                        onmouseout="img_newbiometria.src='img/bts/id_biometrica1.png';" />
                    </asp:LinkButton>
                </p>
                <asp:UpdatePanel ID="UpdatePanel_SituacaoPaciente" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <ContentTemplate>
                        <p>
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
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel_PacienteSimplificado" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButton_Desacordado" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="RadioButton_Desorientado" EventName="CheckedChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel_IdentificacaoPaciente" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Nome</span> <span>
                                    <asp:Label ID="Label_NomePaciente" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data de Nascimento</span> <span>
                                    <asp:Label ID="Label_DataNascimentoPaciente" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Medium"></asp:Label>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Nome da Mãe</span> <span>
                                    <asp:Label ID="Label_NomeMaePaciente" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
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
                                <asp:LinkButton ID="btSalvarPaciente" runat="server" OnClick="OnClick_SalvarPaciente"
                                    ValidationGroup="ValidationGroup_Simplificado">
                <img id="imgsalvar" alt="Validar" src="img/bts/btn-iniciar-atendimento1.png"
                onmouseover="this.src='img/bts/btn-iniciar-atendimento2.png';"
                onmouseout="this.src='img/bts/btn-iniciar-atendimento1.png';" /></asp:LinkButton>
                            </div>
                        </asp:Panel>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
	<img id="Img4" alt="Sair" src="img/btn-sair-1.png"
                onmouseover="this.src='img/btn-sair-2.png';"
                onmouseout="this.src='img/btn-sair-1.png';" />
                            </asp:LinkButton>
                        </div>
                        <asp:Panel ID="Panel_AcoesPacienteSimplificado" runat="server" Visible="false">
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClientClick="window.location='FormPaciente.aspx?tipo_paciente=pacientesimplificado';">
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
        <%--        </fieldset>--%>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
