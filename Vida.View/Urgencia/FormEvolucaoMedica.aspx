<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="True"
    CodeBehind="FormEvolucaoMedica.aspx.cs" Inherits="ViverMais.View.Urgencia.FormEvolucaoMedica"
    EnableEventValidation="false" Title="ViverMais - Formulário de Evolução Médica" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Urgencia/Inc_PesquisarCid.ascx" TagName="TagNamePesquisarCID"
    TagPrefix="TagPrefixPesquisarCID" %>
<%@ Register Src="~/Urgencia/Inc_MenuHistorico.ascx" TagName="Inc_MenuHistorico"
    TagPrefix="IMH" %>
<%@ Register Src="~/Urgencia/Inc_PrescricoesRegistradas.ascx" TagName="Inc_PrescricoesRegistradas"
    TagPrefix="IPR" %>
<%--<%@ Register Src="~/Urgencia/Inc_Termometro.ascx" TagName="Inc_Termometro" TagPrefix="IT" %>--%>
<%@ Register Src="~/Urgencia/Inc_AtestadoReceita.ascx" TagName="Inc_AtestadoReceita"
    TagPrefix="IAR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formulario2
        {
            width: 690px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 10px 10px 20px 10px;
        }
        .formulario3
        {
            width: 640px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 10px 10px 20px 10px;
            margin: 1px;
        }
        .formulario4
        {
            width: 600px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 5px 5px 5px 5px;
            margin: 10px;
        }
        .accordionHeaderEv
        {
            border: 1px solid #142126;
            color: #142126;
            background-color: #eee;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 650px;
        }
        .accordionHeaderSelectedEv
        {
            border: 1px solid #142126;
            color: white;
            background-color: #142126;
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 650px;
        }
        .accordionContentEv
        {
            background-color: #fff;
            border: 1px solid #142126;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 650px;
        }
        #frameexame
        {
            height: 82px;
        }
        #I1
        {
            height: 98px;
        }
        #I2
        {
            height: 64px;
        }
        .style1
        {
            width: 298px;
        }
    </style>

    <script type="text/javascript" src="FusionCharts/FusionCharts.js">
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender1"
        TargetControlID="allwaysOnMessage" VerticalSide="Middle" VerticalOffset="10"
        HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <asp:Panel ID="allwaysOnMessage" runat="server">
        <div style="position: absolute; margin-left: 50%; left: 315px; top: 0px;">
            <p>
                <asp:Image ID="avisoUreg" runat="server" ImageUrl="~/Urgencia/img/aviso_urg.png" />
            </p>
        </div>
    </asp:Panel>
    <div id="top">
        <h2>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Evolução Médica"></asp:Label>
        </h2>
        <br />
        <br />
        <iframe frameborder="0" scrolling="no" id="I1" scrolling="no" src="FormTemporizador.aspx"
            name="I1"></iframe>
        <br />
        <table width="714" border="0" cellspacing="0" cellpadding="0" style="height: 22px">
            <tr>
                <td valign="bottom" class="style1">
                    <asp:LinkButton ID="btnGrafAtendimento" runat="server" OnClick="OnClick_MostrarQuadroAtendimento">
	<img id="grafatendimentos" alt="Quadro de Vagas" src="img/btn_graficoatendimento1.png"
                onmouseover="grafatendimentos.src='img/btn_graficoatendimento2.png';"
                onmouseout="grafatendimentos.src='img/btn_graficoatendimento1.png';" />
                    </asp:LinkButton>
                </td>
                <td valign="top">
                    &nbsp;<iframe frameborder="0" scrolling="no" id="I2" src="FormQuadroEntregaExame.aspx"
                        name="I2"></iframe>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel99" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrafAtendimento" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_QuadroAtendimento" runat="server" Visible="false">
                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 130%; z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; top: 100px; left: 25%;
                        width: 600px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 10px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 10px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        </p>
                        <asp:Literal ID="Literal_GraficoAtendimento" runat="server"></asp:Literal>
                        <p style="padding: 20px 10px 50px 0">
                            <asp:ImageButton ID="ImageButton3" runat="server" OnClick="OnClick_FecharQuadroAtendimento"
                                CausesValidation="false" Height="39px" ImageAlign="Left" ImageUrl="~/Urgencia/img/fechar-btn.png"
                                Width="100px" />
                        </p>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <cc1:TabContainer runat="server" ScrollBars="Horizontal" Width="740px"
            ActiveTabIndex="1">
            <cc1:TabPanel runat="server" HeaderText="Dados de Atendimento/Histórico">
                <ContentTemplate>
                    <fieldset class="formulario2">
                        <legend>Dados de Atendimento</legend>
                        <p>
                            <span class="rotulo">N°:</span> <span>
                                <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data:</span> <span>
                                <asp:Label ID="lblData" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label></span></p>
                        <p>
                            <span class="rotulo">Paciente:</span> <span>
                                <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                            </span>
                        </p>
                    </fieldset>
                    <fieldset class="formulario2">
                        <legend>Histórico</legend>
                        <p>
                            <span>
                                <IMH:Inc_MenuHistorico ID="Inc_MenuHistorico" runat="server"></IMH:Inc_MenuHistorico>
                            </span>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Registro de Evolução Médica">
                <ContentTemplate>
                    <fieldset class="formulario2">
                        <legend>Informações</legend>
                        <p>
                            <cc1:TabContainer ID="TabContainer1" runat="server" Width="685px">
                                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Evolução/Suspeita Diagnóstica">
                                    <ContentTemplate>
                                        <fieldset class="formulario3">
                                            <legend>Evolução</legend>
                                            <p>
                                                <span>
                                                    <asp:LinkButton ID="LinkButton_HistoricoMedico" runat="server" CausesValidation="true" OnClick="OnClick_HistoricoMedico">
                                                    <img alt="Histórico" src="img/bts/urg-historico1.png"
                                                    onmouseover="this.src='img/bts/urg-historico2.png';"
                                                    onmouseout="this.src='img/bts/urg-historico1.png';" />
                                                    </asp:LinkButton>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Registro de Evolução</span><span>
                                                    <asp:TextBox ID="TextBox_ObservacaoEvolucaoMedica" CssClass="campo" runat="server"
                                                        TextMode="MultiLine" Width="620px" Height="110px" Rows="20" Columns="5" ></asp:TextBox>
                                                </span>
                                            </p>
                                            <p style="height: auto;">
                                                <span class="rotulo">Classificação de Risco</span> <span>
                                                    <asp:DropDownList ID="DropDownList_ClassificacaoRisco" runat="server" AutoPostBack="true"
                                                        CssClass="drop" OnSelectedIndexChanged="OnSelectedIndexChanged_ClassificacaoRisco"
                                                        Width="265px"  DataTextField="Descricao" DataValueField="Codigo">
                                                    </asp:DropDownList>
                                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="DropDownList_ClassificacaoRisco" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:Image ID="Image_ClassificacaoRisco" runat="server" Width="16px" Height="18px"
                                                                Style="position:relative; margin-top:0px; left: 0px;" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Situação</span> <span>
                                                    <asp:DropDownList ID="DropDownList_SituacaoEvolucaoMedica" CssClass="drop" Width="265px"
                                                        AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_SituacaoEvolucaoMedica"
                                                        runat="server" CausesValidation="true">
                                                    </asp:DropDownList>
                                                </span>
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel_UnidadeTransferencia" runat="server" UpdateMode="Conditional"
                                                RenderMode="Block">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList_SituacaoEvolucaoMedica" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <asp:Panel ID="Panel_UnidadeTransferencia" runat="server" Visible="false">
                                                        <%--<fieldset>
                                                        <legend>Transferência</legend>--%>
                                                        <p>
                                                            <span class="rotulo">Unidade de Transferência</span> <span>
                                                                <asp:DropDownList ID="DropDownList_UnidadeTransferencia" runat="server" DataTextField="Nome"
                                                                    DataValueField="Codigo" CssClass="drop" Width="350px">
                                                                </asp:DropDownList>
                                                            </span>
                                                        </p>
                                                        <%--</fieldset>--%>
                                                        <asp:CompareValidator ID="CompareValidator_UnidadeTransferencia" runat="server" ErrorMessage="Unidade de Transferência é Obrigatório."
                                                            ControlToValidate="DropDownList_UnidadeTransferencia" ValueToCompare="-1" Operator="GreaterThan"
                                                            Display="None" ValidationGroup="ValidationGroup_cadEvolucaoMedica"></asp:CompareValidator>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                        <fieldset class="formulario3">
                                            <legend>Suspeita Diagnóstica</legend>
                                            <p>
                                                <span>
                                                    <asp:LinkButton ID="LinkButton_HistoricoSuspeitaDiagnostica" runat="server" CausesValidation="true" OnClick="OnClick_HistoricoSuspeitaDiagnostica">
                                                    <img alt="Histórico" src="img/bts/urg-historico1.png"
                                                    onmouseover="this.src='img/bts/urg-historico2.png';"
                                                    onmouseout="this.src='img/bts/urg-historico1.png';" />
                                                    </asp:LinkButton>
                                                </span>
                                            </p>
                                            <TagPrefixPesquisarCID:TagNamePesquisarCID ID="WUC_SuspeitaDiagnostica" runat="server" />
                                            <asp:UpdatePanel ID="UpdatePanel_SuspeitaDiagnostica" runat="server" ChildrenAsTriggers="true"
                                                UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <%--<p>--%>
                                                    <span class="rotulo">CID</span> <span>
                                                        <asp:DropDownList ID="DropDownList_CidEvolucaoMedica" CssClass="drop" CausesValidation="true"
                                                            runat="server" DataTextField="DescricaoCodigoNome" DataValueField="Codigo" Width="395px">
                                                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:ImageButton ID="btnAdicionarCid1" runat="server" OnClick="OnClick_AdicionarCidEvolucaoMedica"
                                                            CausesValidation="true" ValidationGroup="ValidationGroup_CidEvolucaoMedica" Height="19px"
                                                            Width="19px" ImageUrl="~/Urgencia/img/add.png" Style="position: absolute;" />
                                                    </span>
                                                    <%--</p>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarCid1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_CidEvolucaoMedica" OnRowDeleting="OnRowDeleting_CidEvolucaoMedica"
                                                                runat="server" Width="625px" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Codigo" ReadOnly="true" HeaderText="Código" ItemStyle-Width="50px"
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Nome" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                        DeleteText="Excluir" />
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <HeaderStyle CssClass="tab" />
                                                                <RowStyle CssClass="tabrow_left" />
                                                            </asp:GridView>
                                                        </span>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                        <asp:UpdatePanel ID="UpdatePanel_EvolucaoMedica" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="DropDownList_SituacaoEvolucaoMedica" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Panel ID="Panel_SumarioAlta" runat="server" Visible="false">
                                                    <fieldset>
                                                        <legend>Sumário de Alta</legend>
                                                        <p>
                                                            <span class="rotulo">Registro de Alta</span> <span>
                                                                <asp:TextBox ID="TextBox_SumarioAltaEvolucaoMedica" CssClass="campo" runat="server"
                                                                    TextMode="MultiLine" Width="620px" Height="110px" Rows="20" Columns="5"></asp:TextBox>
                                                            </span>
                                                        </p>
                                                    </fieldset>
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <p align="center">
                                            <span>
                                                <asp:CompareValidator ID="CompareValidator4" Font-Size="XX-Small" runat="server"
                                                    ErrorMessage="Selecione um CID!" Display="None" ControlToValidate="DropDownList_CidEvolucaoMedica"
                                                    ValueToCompare="0" Operator="GreaterThan" ValidationGroup="ValidationGroup_CidEvolucaoMedica"></asp:CompareValidator>
                                                <asp:ValidationSummary ID="ValidationSummary3" Font-Size="XX-Small" runat="server"
                                                    ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_CidEvolucaoMedica" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                                    ControlToValidate="TextBox_ObservacaoEvolucaoMedica" ErrorMessage="Registro de Evolução é Obrigatório!"
                                                    ValidationGroup="ValidationGroup_cadEvolucaoMedica" Display="None"></asp:RequiredFieldValidator>
                                                <asp:UpdatePanel ID="UpdatePanel_SumarioAlta" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DropDownList_SituacaoEvolucaoMedica" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_SumarioAlta" Enabled="false"
                                                            Font-Size="XX-Small" runat="server" ControlToValidate="TextBox_SumarioAltaEvolucaoMedica"
                                                            ErrorMessage="Sumário de Alta é Obrigatório!" ValidationGroup="ValidationGroup_cadEvolucaoMedica"
                                                            Display="None"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma Situação."
                                                    ControlToValidate="DropDownList_SituacaoEvolucaoMedica" ValidationGroup="ValidationGroup_cadEvolucaoMedica"
                                                    ValueToCompare="-1" Operator="GreaterThan" Display="None">
                                                </asp:CompareValidator>
                                                <asp:CustomValidator ID="CustomValidator_ConsultaMedica" Display="None" OnServerValidate="OnServerValidate_ConsultaMedica"
                                                    runat="server" ErrorMessage="CustomValidator" ValidationGroup="ValidationGroup_cadEvolucaoMedica">
                                                </asp:CustomValidator>
                                                <asp:ValidationSummary ID="ValidationSummaryEvolucao" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="ValidationGroup_cadEvolucaoMedica" />
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanelR1" runat="server" HeaderText="Prescrição">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel_Prescricao" runat="server">
                                            <fieldset class="formulario3">
                                                <legend>Prescrição</legend>
                                                <p>
                                                    <span class="campo">Para copiar os itens da última prescrição
                                                        <asp:LinkButton ID="LinkButton_CopiarItensUltimaPrescricao" runat="server" OnClick="OnClick_CopiarItensUltimaPrescricao"
                                                            CausesValidation="true">Clique Aqui.</asp:LinkButton>
                                                    </span>
                                                </p>
                                                <p class="campo2">
                                                    Esta prescrição entra em vigência imediatamente<asp:CheckBox ID="CheckBox_Vigencia"
                                                        runat="server" />
                                                </p>
                                                <fieldset class="formulario4">
                                                    <legend>Procedimentos (SIGTAP)</legend>
                                                    <asp:UpdatePanel ID="UpdatePanel_Quatro" runat="server" UpdateMode="Conditional"
                                                        ChildrenAsTriggers="true" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoEvolucaoMedica"
                                                                EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span class="rotulo">Buscar Procedimento</span> <span>
                                                                    <asp:TextBox ID="TextBox_BuscarProcedimentoSIGTAP" runat="server" CssClass="campo"
                                                                        Width="250"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton4" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimento"
                                                                        OnClick="OnClick_BuscarProcedimentoSIGTAP" ImageUrl="~/Urgencia/img/procurar.png"
                                                                        Width="16px" Height="16px" Style="vertical-align: bottom;" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o nome do procedimento."
                                                                        Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAP" ValidationGroup="ValidationGroup_BuscarProcedimento"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                        ErrorMessage="Informe pelo menos os três primeiros caracteres do procedimento."
                                                                        Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAP" ValidationGroup="ValidationGroup_BuscarProcedimento"
                                                                        ValidationExpression="^(\W{3,})|(\w{3,})$">
                                                                    </asp:RegularExpressionValidator>
                                                                    <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true"
                                                                        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimento" />
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Procedimento *</span> <span>
                                                                    <asp:DropDownList ID="DropDownList_ProcedimentoEvolucaoMedica" CssClass="drop" runat="server"
                                                                        Width="395px" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_RetiraCids">
                                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Freqüência *</span> <span>
                                                                    <asp:TextBox ID="TextBox_IntervaloProcedimentoEvolucaoMedica" Width="25px" CssClass="campo"
                                                                        MaxLength="4" runat="server"></asp:TextBox>
                                                                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimento" CssClass="drop"
                                                                        CausesValidation="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimento">
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um procedimento!"
                                                                ControlToValidate="DropDownList_ProcedimentoEvolucaoMedica" Display="None" Operator="GreaterThan"
                                                                ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica"></asp:CompareValidator>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaProcedimento" runat="server"
                                                                ErrorMessage="Freqüência é Obrigatório!" Display="None" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica"
                                                                ControlToValidate="TextBox_IntervaloProcedimentoEvolucaoMedica"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaProcedimento"
                                                                runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                                                                ControlToValidate="TextBox_IntervaloProcedimentoEvolucaoMedica" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica"
                                                                ValidationExpression="^\d*$">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:CompareValidator ID="CompareValidator_FrequenciaProcedimento" runat="server"
                                                                ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimentoEvolucaoMedica"
                                                                Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica"></asp:CompareValidator>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <TagPrefixPesquisarCID:TagNamePesquisarCID ID="WUC_ProcedimentoCid" runat="server" />
                                                    <asp:UpdatePanel ID="UpdatePanel_ProcedimentoCID" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoEvolucaoMedica"
                                                                EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="DropDownList_ProcedimentoEvolucaoMedica" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span class="rotulo">CID</span><span>
                                                                    <asp:DropDownList ID="DropDownList_ProcedimentoCID" runat="server" CssClass="drop"
                                                                        DataTextField="DescricaoCodigoNome" DataValueField="Codigo" Width="395px">
                                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <p>
                                                        <span>
                                                            <asp:ImageButton ID="Button_AdicionarProcedimentoEvolucaoMedica" runat="server" CausesValidation="true"
                                                                Width="134px" Height="38px" OnClick="OnClick_AdicionarProcedimentoEvolucaoMedica"
                                                                ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica" />
                                                            <asp:ValidationSummary ID="ValidationSummary_procedimento" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_cadProcedimentoEvolucaoMedica" />
                                                        </span>
                                                    </p>
                                                    <asp:UpdatePanel ID="UpdatePanel_Cinco" runat="server" RenderMode="Inline" UpdateMode="Conditional"
                                                        ChildrenAsTriggers="true">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton_CopiarItensUltimaPrescricao" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoEvolucaoMedica"
                                                                EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span>
                                                                    <asp:GridView ID="GridView_ProcedimentoEvolucaoMedica" OnRowDeleting="OnRowDeleting_ProcedimentoEvolucaoMedica"
                                                                        runat="server" Width="600px" AutoGenerateColumns="False">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freqüência" ItemStyle-Width="50px"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="DescricaoCIDVinculado" HeaderText="CID" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                                                                DeleteText="Excluir" />
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle CssClass="tab" />
                                                                        <RowStyle CssClass="tabrow_left" />
                                                                    </asp:GridView>
                                                                </span>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </fieldset>
                                                <fieldset class="formulario4">
                                                    <legend>Procedimentos Não-Faturáveis</legend>
                                                    <asp:UpdatePanel ID="UpdatePanel_Quinze" runat="server" UpdateMode="Conditional"
                                                        ChildrenAsTriggers="true" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavelEvolucaoMedica"
                                                                EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span class="rotulo">Buscar Procedimento</span> <span>
                                                                    <asp:TextBox ID="TextBox_BuscarProcedimento" runat="server" CssClass="campo" Width="250px"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton1" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                                        OnClick="OnClick_BuscarProcedimentoNaoFaturavel" ImageUrl="~/Urgencia/img/procurar.png"
                                                                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Procedimento *</span> <span>
                                                                    <asp:DropDownList ID="DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica" CssClass="drop"
                                                                        Width="395px" runat="server">
                                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Freqüência *</span> <span>
                                                                    <asp:TextBox ID="TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica" Width="25px"
                                                                        CssClass="campo" MaxLength="4" runat="server"></asp:TextBox>
                                                                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel"
                                                                        CausesValidation="true" CssClass="drop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel">
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Selecione um procedimento."
                                                                    ControlToValidate="DropDownList_ProcedimentosNaoFaturaveisEvolucaoMedica" Display="None"
                                                                    Operator="GreaterThan" ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel"
                                                                    runat="server" ErrorMessage="Freqüência é Obrigatório!" Display="None" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica"
                                                                    ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel"
                                                                    runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                                                                    ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica"
                                                                    ValidationExpression="^\d*$">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ID="CompareValidator_FrequenciaProcedimentoNaoFaturavel" runat="server"
                                                                    ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelEvolucaoMedica"
                                                                    Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox_BuscarProcedimento"
                                                                    Display="None" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                                    ErrorMessage="Informe o nome do procedimento."></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do Procedimento."
                                                                    Display="None" ControlToValidate="TextBox_BuscarProcedimento" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                                    ValidationExpression="^(\W{3,})|(\w{3,})$">
                                                                </asp:RegularExpressionValidator>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <p>
                                                        <span>
                                                            <asp:ImageButton ID="Button_AdicionarprocedimentoNaoFaturavelEvolucaoMedica" CausesValidation="true"
                                                                runat="server" Width="134px" Height="38px" OnClick="OnClick_AdicionarProcedimentoNaoFaturavelEvolucaoMedica"
                                                                ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica" />
                                                            <asp:ValidationSummary ID="ValidationSummary5" ShowMessageBox="true" ShowSummary="false"
                                                                ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavelEvolucaoMedica" runat="server" />
                                                            <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel" />
                                                        </span>
                                                    </p>
                                                    <asp:UpdatePanel ID="UpdatePanel_Quatorze" runat="server" UpdateMode="Conditional"
                                                        ChildrenAsTriggers="true" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton_CopiarItensUltimaPrescricao" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavelEvolucaoMedica"
                                                                EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span>
                                                                    <asp:GridView ID="GridView_ProcedimentosNaoFaturavelEvolucaoMedica" AutoGenerateColumns="false"
                                                                        runat="server" Width="600px" OnRowDeleting="OnRowDeleting_ProcedimentoNaoFaturavelEvolucaoMedica">
                                                                        <Columns>
                                                                            <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" />
                                                                            <asp:BoundField HeaderText="Freqüência" DataField="DescricaoIntervalo" />
                                                                            <asp:CommandField ShowDeleteButton="true" ButtonType="Link" ItemStyle-HorizontalAlign="Center"
                                                                                DeleteText="Excluir" />
                                                                        </Columns>
                                                                        <HeaderStyle CssClass="tab" />
                                                                        <RowStyle CssClass="tabrow" />
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        <EmptyDataTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </span>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </fieldset>
                                                <fieldset class="formulario4">
                                                    <legend>Medicamentos/Prescrição</legend>
                                                    <asp:UpdatePanel ID="UpdatePanel_Onze" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamentoEvolucaoMedica" EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span class="rotulo">Buscar Medicamento/Prescrição</span> <span>
                                                                    <asp:TextBox ID="TextBox_BuscarMedicamento" runat="server" CssClass="campo" Width="250"></asp:TextBox>
                                                                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="OnClick_BuscarMedicamento"
                                                                        ValidationGroup="ValidationGroup_BuscarMedicamento" ImageUrl="~/Urgencia/img/procurar.png"
                                                                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Medicamento/Prescrição *</span> <span>
                                                                    <asp:DropDownList ID="DropDownList_MedicamentoEvolucaoMedica" CssClass="drop" runat="server"
                                                                        Width="395px">
                                                                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span><span>
                                                                    <asp:ImageButton ID="ImageButton_Bulario" runat="server" OnClick="OnClick_VerBulario"
                                                                        ValidationGroup="ValidationGroup_VerBulario" ImageUrl="~/Urgencia/img/bula.png"
                                                                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Freqüência *</span> <span>
                                                                    <asp:TextBox ID="TextBox_IntervaloMedicamentoEvolucaoMedica" Width="25px" CssClass="campo"
                                                                        MaxLength="4" runat="server"></asp:TextBox>
                                                                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaMedicamento" CssClass="drop"
                                                                        CausesValidation="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaMedicamento">
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <span class="rotulo">Via de Administração</span> <span>
                                                                    <asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamentoEvolucaoMedica" CssClass="drop"
                                                                        runat="server" CausesValidation="true">
                                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </p>
                                                            <p>
                                                                <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Selecione um(a) medicamento/prescrição!"
                                                                    ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="DropDownList_MedicamentoEvolucaoMedica"
                                                                    ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaMedicamento" runat="server"
                                                                    ErrorMessage="Freqüência é Obrigatório!" Display="None" ControlToValidate="TextBox_IntervaloMedicamentoEvolucaoMedica"
                                                                    ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaMedicamento"
                                                                    runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                                                                    ControlToValidate="TextBox_IntervaloMedicamentoEvolucaoMedica" ValidationGroup="ValidationGroup_PrescricaoMedicamento"
                                                                    ValidationExpression="^\d*$">
                                                                </asp:RegularExpressionValidator>
                                                                <asp:CompareValidator ID="CompareValidator_FrequenciaMedicamento" runat="server"
                                                                    ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloMedicamentoEvolucaoMedica"
                                                                    Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:CompareValidator>
                                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um(a) medicamento/prescrição!"
                                                                    ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="DropDownList_MedicamentoEvolucaoMedica"
                                                                    ValidationGroup="ValidationGroup_VerBulario"></asp:CompareValidator>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_BuscarMedicamento"
                                                                    Display="None" ValidationGroup="ValidationGroup_BuscarMedicamento" ErrorMessage="Informe o nome do(a) medicamento/prescrição."></asp:RequiredFieldValidator>
                                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do(a) Medicamento/Prescrição."
                                                                    Display="None" ControlToValidate="TextBox_BuscarMedicamento" ValidationGroup="ValidationGroup_BuscarMedicamento"
                                                                    ValidationExpression="^(\W{3,})|(\w{3,})$">
                                                                </asp:RegularExpressionValidator>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <asp:UpdatePanel ID="UpdatePanel_Dezesseis" RenderMode="Inline" runat="server" UpdateMode="Conditional">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamentoEvolucaoMedica" EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span class="rotulo">Observação</span> <span>
                                                                    <asp:TextBox ID="TextBox_ObservacaoMedicamentoEvolucaoMedica" CssClass="campo" Width="590px"
                                                                        Height="110px" Rows="20" Columns="5" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                                </span>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                    <p>
                                                        <span>
                                                            <asp:ImageButton ID="btnAdicionarMedicamentoEvolucaoMedica" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                                                CausesValidation="true" OnClick="OnClick_AdicionarMedicamentoEvolucaoMedica"
                                                                ValidationGroup="ValidationGroup_PrescricaoMedicamento" Width="134px" Height="38px" />
                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_PrescricaoMedicamento" />
                                                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_VerBulario" />
                                                            <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_BuscarMedicamento" />
                                                        </span>
                                                    </p>
                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                                                        ChildrenAsTriggers="true">
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="LinkButton_CopiarItensUltimaPrescricao" EventName="Click" />
                                                            <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamentoEvolucaoMedica" EventName="Click" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <p>
                                                                <span>
                                                                    <asp:GridView ID="GridView_MedicamentoEvolucaoMedica" runat="server" Width="600px"
                                                                        AutoGenerateColumns="False" OnRowDeleting="OnRowDeleting_MedicamentoEvolucaoMedica">
                                                                        <Columns>
                                                                            <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento/Prescrição" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freqüência" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                                                                            <%--<asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração"
                                                                                ItemStyle-HorizontalAlign="Center" />--%>
                                                                            <asp:BoundField DataField="DescricaoObservacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        <HeaderStyle CssClass="tab" />
                                                                        <RowStyle CssClass="tabrow_left" />
                                                                    </asp:GridView>
                                                                </span>
                                                            </p>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </fieldset>
                                            </fieldset>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanelR2" runat="server" HeaderText="Exames">
                                    <ContentTemplate>
                                        <fieldset class="formulario3">
                                            <legend>Exames Internos</legend>
                                            <p>
                                                <asp:UpdatePanel ID="UpdatePanel_Treze" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                    <%--                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames1" EventName="Click" />
                                                    </Triggers>--%>
                                                    <ContentTemplate>
                                                        <span class="rotulo">Exame:</span> <span>
                                                            <asp:DropDownList ID="DropDownList_ExamesEvolucaoMedica" CssClass="drop" runat="server"
                                                                Width="300px">
                                                            </asp:DropDownList>
                                                            <asp:ImageButton ID="btnAdicionarExames1" runat="server" CausesValidation="true"
                                                                OnClick="OnClick_AdicionarExameEvolucaoMedica" ValidationGroup="ValidationGroup_ExameEvolucaoMedica"
                                                                Height="19px" Width="19px" ImageUrl="~/Urgencia/img/add.png" Style="position: absolute;" />
                                                        </span>
                                                        <asp:CompareValidator ID="CompareValidator_Exames" runat="server" Display="None"
                                                            ControlToValidate="DropDownList_ExamesEvolucaoMedica" ErrorMessage="Selecione um Exame!"
                                                            ValidationGroup="ValidationGroup_ExameEvolucaoMedica" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary_Um" runat="server" ValidationGroup="ValidationGroup_ExameEvolucaoMedica"
                                                            ShowMessageBox="true" ShowSummary="false" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ExameEvolucaoMedica" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                OnRowDeleting="OnRowDeleting_ExameEvolucaoMedica">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Exame" HeaderText="Exame" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center"
                                                                        DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                                                    <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="tab" />
                                                                <RowStyle CssClass="tabrow_left" />
                                                            </asp:GridView>
                                                        </span>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                        <fieldset class="formulario3">
                                            <legend>Exames Eletivos</legend>
                                            <p>
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                    <%--                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames" EventName="Click" />
                                                    </Triggers>--%>
                                                    <ContentTemplate>
                                                        <span class="rotulo">Exame:</span> <span style="margin-left: 5px;">
                                                            <asp:DropDownList ID="DropDownList_ExameEletivo" CssClass="drop" runat="server" Width="300px">
                                                            </asp:DropDownList>
                                                            <asp:ImageButton ID="ButtonAdicionarExameEletivo" runat="server" CausesValidation="true"
                                                                OnClick="OnClick_AdicionarExameEletivo" Height="19px" Width="19px" ImageUrl="~/Urgencia/img/add.png"
                                                                Style="position: static;" ValidationGroup="ExamesEletivos" />
                                                        </span>
                                                        <asp:CompareValidator ID="CompareValidator7" runat="server" Display="None" ControlToValidate="DropDownList_ExameEletivo"
                                                            ErrorMessage="Selecione um Exame!" ValidationGroup="ExamesEletivos" ValueToCompare="0"
                                                            Operator="GreaterThan"></asp:CompareValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary8" runat="server" ValidationGroup="ExamesEletivos"
                                                            ShowMessageBox="true" ShowSummary="false" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ButtonAdicionarExameEletivo" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ExamesEletivos" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                OnRowDeleting="OnRowDeleting_ExameEletivo">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeExame" HeaderText="Exame" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center"
                                                                        DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                                                    <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" DeleteText="Excluir"
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                </Columns>
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="tab" />
                                                                <RowStyle CssClass="tabrow_left" />
                                                            </asp:GridView>
                                                        </span>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </p>
                        <div class="botoesroll" style="margin-left: 25px;">
                            <asp:LinkButton ID="Button_GerarReceita1" runat="server" CommandArgument="receita"
                                OnClick="OnClick_GerarReceitaAtestado" Visible="false">
                <img id="imgreceita" alt="Gerar Receita" src="img/bts/btn-emitirreceita.png"
                onmouseover="imgreceita.src='img/bts/btn-emitirreceita2.png';"
                onmouseout="imgreceita.src='img/bts/btn-emitirreceita.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="Button_GerarAtestado1" runat="server" CommandArgument="receita"
                                OnClick="OnClick_GerarReceitaAtestado" Visible="false">
                <img id="imgatestado" alt="Confirmar" src="img/bts/btn-emitiratestado.png"
                onmouseover="imgatestado.src='img/bts/btn-emitiratestado2.png';"
                onmouseout="imgatestado.src='img/bts/btn-emitiratestado.png';" /></asp:LinkButton>
                        </div>
                        <div style="width: 500px;" id="centeralign">
                            <div class="botoesroll" wi>
                                <asp:LinkButton ID="btnSalvar1" runat="server" CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadEvolucaoMedica')) return confirm('Todos os dados informados para esta evolução estão corretos ?'); return false;"
                                    OnClick="OnClick_SalvarEvolucaoMedica" ValidationGroup="ValidationGroup_cadEvolucaoMedica">
                <img id="imgsalvar1" alt="Gerar Receita" src="img/bts/btn_salvar1.png"
                onmouseover="imgsalvar1.src='img/bts/btn_salvar2.png';"
                onmouseout="imgsalvar1.src='img/bts/btn_salvar1.png';" /></asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="Button_Cancelar" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
                <img id="imgcancelar" alt="Confirmar" src="img/bts/btn_cancelar1.png"
                onmouseover="imgcancelar.src='img/bts/btn_cancelar2.png';"
                onmouseout="imgcancelar.src='img/bts/btn_cancelar1.png';" /></asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_IncPrescricao" runat="server" HeaderText="Prescrições Registradas"
                >
                <ContentTemplate>
                    <IPR:Inc_PrescricoesRegistradas ID="Inc_PrescricoesRegistradas" runat="server"></IPR:Inc_PrescricoesRegistradas>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Atestados e Receitas" >
                <ContentTemplate>
                    <IAR:Inc_AtestadoReceita ID="Inc_AtestadoReceita" runat="server"></IAR:Inc_AtestadoReceita>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <asp:UpdatePanel ID="UpdatePanel_HistoricoMedico" runat="server" ChildrenAsTriggers="true"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel_HistoricoMedico" runat="server" Visible="false">
                    <div id="Div1" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="Div2" visible="false" style="position: fixed; top: 100px; left: 50%; margin-left: -350px;
                        width: 700px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 50px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 0px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        <p style="padding: 0px 10px 30px 0">
                        </p>
                        <p style="color: White; font-size: medium; font-family: Arial; font-weight: bold;">
                            <asp:Label ID="Label_TituloHistorico" runat="server" Text=""></asp:Label>
                        </p>
                        <div id="conteudo" style="width: 650px; height: 300px; overflow: auto;">
                            <asp:DataList ID="DataList_HistoricoMedico" runat="server">
                                <ItemTemplate>
                                                                      <p>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold"> PROFISSIONAL:</div>
                                    <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:480px;  margin-right:15px"><asp:Label ID="Label_Profissional" runat="server" Text='<%#bind("Profissional") %>'></asp:Label></div>
                                    </p>

                                    <p>
                                     <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">CBO:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:20px; float:left; margin-bottom:10px;"> <asp:Label ID="Label_CBO" runat="server" Text='<%#bind("CBO") %>'></asp:Label></div>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">DATA:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:1px;  float:left; margin-bottom:10px;"> <asp:Label ID="Label_Data" runat="server" Text='<%#bind("Data") %>'></asp:Label></div>
                                   </p>
                                   
                                    <asp:TextBox ID="TextBox_Conteudo" runat="server" Text='<%#bind("Conteudo") %>' TextMode="MultiLine"
                                        CssClass="campo" ReadOnly="true" Rows="20" Columns="20" Width="620px" Height="200px"></asp:TextBox>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                <div style="margin-top:15px; margin-bottom:10px;"><img src="img/div-separador-hitorico.png" alt"" /></div>
                                </SeparatorTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <FooterTemplate>
                                    <asp:Label ID="Label15" runat="server" Text="Nenhum registro encontrado." Font-Bold="true" ForeColor="White" Font-Size="13px" Visible='<%#bool.Parse((DataList_HistoricoMedico.Items.Count == 0).ToString()) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:DataList>
                        </div>
                        <div class="botoesroll" style="margin: 15px 0px 5px 0px">
                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="OnClick_FecharHistoricoMedico">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Urgencia/img/fechar-btn.png" Width="100px"
                                    Height="39px" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <%--<asp:UpdatePanel ID="UpdatePanel_HistoricoSuspeitaDiagnostica" runat="server" ChildrenAsTriggers="true"
            UpdateMode="Conditional" RenderMode="Block">
            <ContentTemplate>
                <asp:Panel ID="Panel_HistoricoSuspeitaDiagnostica" runat="server" Visible="false">
                    <div id="Div3" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="Div4" visible="false" style="position: fixed; top: 100px; left: 50%; margin-left: -350px;
                        width: 700px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 50px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 0px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        <p style="padding: 0px 10px 30px 0">
                        </p>
                        <p style="color: White; font-size: medium; font-family: Arial; font-weight: bold;">
                            Histórico de Suspeita Diagnóstica
                        </p>
                        <div id="conteudo" style="width: 650px; height: 300px; overflow: auto;">
                            <asp:DataList ID="DataList_HistoricoSuspeitaDiagnostica" runat="server">
                                <ItemTemplate>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold"> PROFISSIONAL:</div>
                                    <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; float:left; margin-right:15px"><asp:Label ID="Label_Profissional" runat="server" Text='<%#bind("Profissional") %>'></asp:Label></div>
                                     <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">CBO:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:1px;  margin-bottom:10px;"> <asp:Label ID="Label_CBO" runat="server" Text='<%#bind("CBO") %>'></asp:Label></div>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">DATA:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:1px;  margin-bottom:10px;"> <asp:Label ID="Label_Data" runat="server" Text='<%#bind("Data") %>'></asp:Label></div>
                                    <asp:TextBox ID="TextBox_Conteudo" runat="server" Text='<%#bind("Cids") %>' TextMode="MultiLine"
                                        CssClass="campo" ReadOnly="true" Rows="20" Columns="20" Width="620px" Height="200px"></asp:TextBox>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <div style="margin-top:15px; margin-bottom:10px;"><img src="img/div-separador-hitorico.png" alt"" /></div>
                                </SeparatorTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <FooterTemplate>
                                    <asp:Label ID="Label15" runat="server" Text="Nenhuma suspeita encontrada." Font-Bold="true" ForeColor="White" Font-Size="13px" Visible='<%#bool.Parse((DataList_HistoricoSuspeitaDiagnostica.Items.Count == 0).ToString()) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:DataList>
                        </div>
                        <div class="botoesroll" style="margin: 15px 0px 5px 0px">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_FecharHistoricoSuspeitaDiagnostica">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Urgencia/img/fechar-btn.png" Width="100px"
                                    Height="39px" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
