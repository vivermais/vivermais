<%@ Page Title="" Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    MaintainScrollPositionOnPostback="true" AutoEventWireup="True" CodeBehind="FormProntuario.aspx.cs"
    Inherits="Urgence.View.FormProntuario" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Urgencia/MasterUrgencia.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Urgencia/Inc_MenuHistorico.ascx" TagName="Inc_MenuHistorico"
    TagPrefix="IMH" %>
<%@ Register Src="~/Urgencia/Inc_PesquisarCid.ascx" TagName="TagNamePesquisarCid"
    TagPrefix="TagPrefixPesquisarCid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formulario2
        {
            width: 660px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 5px 5px 10px 5px;
        }
        .formulario3
        {
            width: 640px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 5px 5px 10px 5px;
        }
        .formulario1
        {
            width: 721px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 5px 5px 10px 5px;
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

    <script type="text/javascript" language="javascript">

        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            //top.style.position = 'absolute';
            top.style.top = location.y + ' px';
            top.style.left = location.x + ' px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
       
    </script>

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
            Primeira Consulta M�dica</h2>
        <br />
        <br />
        <iframe id="frametempo" frameborder="0" scrolling="no" src="FormTemporizador.aspx">
        </iframe>
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
        <asp:UpdatePanel ID="UpdatePanel99" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnGrafAtendimento" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_QuadroAtendimento" runat="server" Visible="false" CssClass="white_content">
                    <div id="cinza" visible="false" style="position: fixed; top: 0; left: 0; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=70);
                        moz-opacity: 0.3; opacity: 0.3; .">
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
        <fieldset class="formulario1">
            <legend>Informa��es</legend>
            <%-- <p>--%>
            <cc1:TabContainer ID="TabContainer2" runat="server" Width="720px">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Primeira Consulta M�dica">
                    <ContentTemplate>
                        <cc1:TabContainer ID="TabContainer1" runat="server">
                            <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Dados Registro Eletr�nico">
                                <ContentTemplate>
                                    <fieldset class="formulario2">
                                        <legend>Registro de Atendimento</legend>
                                        <p>
                                            <span class="rotulo">N�:</span> <span class="label">
                                                <asp:Label ID="lblNumero" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Data:</span> <span>
                                                <asp:Label ID="lblData" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Hor�rio da Classifica��o Risco:</span> <span>
                                                <asp:Label ID="Label_HorarioClassificacaoRisco" runat="server" Text="" Font-Bold="True"
                                                    ForeColor="Maroon"></asp:Label>
                                            </span>
                                        </p>
                                        <asp:UpdatePanel ID="UpdatePanel_IdentificacaoPaciente" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="false">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlSituacao" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <p>
                                                    <span class="rotulo">Paciente:</span> <span>
                                                        <asp:TextBox ID="tbxNomePaciente" runat="server" CssClass="campo" Visible="false"></asp:TextBox>
                                                        <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" ForeColor="Maroon" Visible="false"></asp:Label>
                                                    </span>
                                                </p>
                                                <p>
                                                    <span class="rotulo">Idade:</span> <span>
                                                        <asp:TextBox ID="tbxIdade" CssClass="campo" runat="server" MaxLength="3"></asp:TextBox>
                                                    </span>
                                                </p>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_NomePaciente" Font-Size="XX-Small"
                                                    runat="server" ErrorMessage="Nome do Paciente � Obrigat�rio" ControlToValidate="tbxNomePaciente"
                                                    Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Idade" Font-Size="XX-Small"
                                                    runat="server" ErrorMessage="Idade � Obrigat�rio!" ControlToValidate="tbxIdade"
                                                    Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender_Idade" runat="server" MaskType="None"
                                                    Mask="999" InputDirection="LeftToRight" TargetControlID="tbxIdade" AcceptNegative="None">
                                                </cc1:MaskedEditExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <p>
                                            <span class="rotulo">Sexo:</span> <span class="camporadio">
                                                <asp:RadioButton ID="RadioButton_SexoM" CssClass="camporadio" runat="server" Checked="true"
                                                    GroupName="GroupName_Sexo" Width="20px" />Masculino
                                                <asp:RadioButton ID="RadioButton_SexoF" CssClass="camporadio" runat="server" GroupName="GroupName_Sexo"
                                                    Width="20px" />Feminino </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Situa��o Atual:</span> <span>
                                                <asp:Label ID="lblSituacao" runat="server" Font-Bold="true"></asp:Label>
                                            </span>
                                        </p>
<%--                                        <p>
                                            <span class="rotulo">Peso</span> <span>
                                                <asp:TextBox ID="TextBox_Peso" runat="server" CssClass="campo" Width="60px" MaxLength="4"></asp:TextBox></span>
                                            Kg <span style="position: absolute; padding-top: 3px;">
                                                <asp:ImageButton ID="img_Button_DuViverMaisPeso" runat="server" Width="16px" Height="18px"
                                                    OnClientClick="return false;" ImageUrl="~/Urgencia/img/help.png" /></span>
                                            <div id="flyout">
                                            </div>
                                            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                                font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                                <div id="btnCloseParent" style="float: left; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                                        ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                                        font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                                </div>
                                                Caso o peso do paciente, freq��ncia card�aca, freq��ncia respirat�ria, tens�o arterial,
                                                temperatura, hgt contenha parte fracion�ria utilize a v�rgula para indic�-la. Exemplo:
                                                100,30 ou 100,3.
                                            </div>
                                        </p>--%>
                                        <p>
                                            <span class="rotulo">Freq. Card�aca:</span> <span>
                                                <asp:TextBox ID="tbxFreqCardiaca" CssClass="campo" runat="server" MaxLength="3" Width="60px"
                                                    Height="16px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblFreqCardiaca" runat="server" Font-Bold="true"></asp:Label>
                                                bpm </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Freq. Respirat�ria:</span> <span>
                                                <asp:TextBox ID="tbxFreqRespitatoria" CssClass="campo" runat="server" MaxLength="3"
                                                    Width="60px" Height="16px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblFreqRespiratoria" runat="server" Font-Bold="true"></asp:Label>
                                                i.m. </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Tens�o Arterial:</span> <span>
                                                <asp:TextBox ID="tbxTensaoArterialInicio" runat="server" CssClass="campo" Height="16px"
                                                    MaxLength="3" Width="60px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lbQuantificadorTensaoArterial" runat="server" Visible="false" Text="X"></asp:Label>
                                                <asp:TextBox ID="tbxTensaoArterialFim" runat="server" CssClass="campo" Height="16px"
                                                    MaxLength="3" Width="60px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblTensaoArterial" runat="server" Font-Bold="true"></asp:Label>
                                                mmHg </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Temperatura:</span> <span>
                                                <asp:TextBox ID="tbxTemperatura" CssClass="campo" runat="server" MaxLength="3" Width="60px"
                                                    Height="16px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblTemperatura" runat="server" Font-Bold="true"></asp:Label>
                                                �C </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">HGT:</span> <span>
                                                <asp:TextBox ID="tbxHgt" runat="server" CssClass="campo" Height="16px" MaxLength="3"
                                                    Width="60px" Visible="false"></asp:TextBox>
                                                <asp:Label ID="lblHgt" runat="server" Font-Bold="true"></asp:Label>
                                                m/mol </span>
                                        </p>
                                        &nbsp
                                        <p class="camporadio">
                                            <asp:CheckBox ID="ckbConvulsao" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Convuls�o
                                            <asp:CheckBox ID="ckbAcidente" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Acidente
                                            <asp:CheckBox ID="ckbDorIntensa" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Dor Intensa
                                        </p>
                                        <p class="camporadio">
                                            <asp:CheckBox ID="ckbAlergia" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Alergia
                                            <asp:CheckBox ID="ckbAsma" CssClass="camporadio" runat="server" Enabled="False" Width="20px" />Asma
                                            <asp:CheckBox ID="ckbDiarreia" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Diarr�ia
                                        </p>
                                        <p class="camporadio">
                                            <asp:CheckBox ID="ckbFratura" CssClass="camporadio" runat="server" Enabled="False"
                                                Width="20px" />Fratura
                                            <asp:CheckBox ID="CheckBoxDorToraxica" Enabled="false" runat="server" CssClass="camporadio"
                                                Width="20px" />Dor Tor�xica
                                            <asp:CheckBox ID="CheckBoxSaturacaoOxigenio" Enabled="false" runat="server" CssClass="camporadio"
                                                Width="20px" />Satura��o de Oxig�nio
                                        </p>
                                        <p>
                                            <span class="rotulo">Queixa:</span> <span>
                                                <asp:TextBox ID="TextBox_Queixa" runat="server" Width="100%" Height="120px" CssClass="campo"
                                                    TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                                                <%--<asp:Label ID="lblQueixa" runat="server" Font-Bold="true"></asp:Label>--%>
                                            </span>
                                        </p>
                                    </fieldset>
                                    <fieldset class="formulario2">
                                        <legend>Suspeita Diagn�stica</legend>
                                        <TagPrefixPesquisarCid:TagNamePesquisarCid ID="WUC_SuspeitaDiagnostica" runat="server" />
                                        <asp:UpdatePanel ID="UpdatePanel_SuspeitaDiagnostica" runat="server" UpdateMode="Conditional"
                                            ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <%--<p>--%>
                                                <span class="rotulo">CID:</span> <span>
                                                    <asp:DropDownList ID="ddlCid" runat="server" CssClass="drop" DataTextField="DescricaoCodigoNome"
                                                        DataValueField="Codigo" Width="395px" CausesValidation="true">
                                                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCid"
                                                        runat="server" ErrorMessage="" ValidationGroup="ValidationGroup_cadCid" InitialValue="0"></asp:RequiredFieldValidator></span>
                                                <span style="position: absolute;">
                                                    <asp:ImageButton ID="img_btnAdicionarCid" runat="server" OnClick="btnAdicionarCid_Click"
                                                        CausesValidation="true" ValidationGroup="ValidationGroup_cadCid" Height="19px"
                                                        Width="19px" ImageUrl="~/Urgencia/img/add.png" /></span>
                                                <%--</p>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <%--<asp:PostBackTrigger ControlID="img_btnAdicionarCid" />--%>
                                                <asp:AsyncPostBackTrigger ControlID="img_btnAdicionarCid" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <p>
                                                    <span>
                                                        <asp:GridView ID="gridCid" OnRowDeleting="gridCid_RowDeleting" runat="server" Width="630px"
                                                            AutoGenerateColumns="False">
                                                            <Columns>
                                                                <asp:BoundField DataField="Codigo" HeaderText="C�digo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Nome" HeaderText="Descri��o" ItemStyle-HorizontalAlign="Center" />
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
                                    <div>
                                        <%--                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                                ErrorMessage="C�digo CID � Obrigat�rio!" ControlToValidate="TextBox_CID" Display="None"
                                                ValidationGroup="ValidationGroup_BuscaCID"></asp:RequiredFieldValidator>
                                            <asp:ValidationSummary ID="ValidationSummary4" Font-Size="XX-Small" runat="server"
                                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_BuscaCID" />--%>
                                        <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                                            ErrorMessage="Selecione um CID!" Display="None" ControlToValidate="ddlCid" ValueToCompare="0"
                                            Operator="GreaterThan" ValidationGroup="ValidationGroup_cadCid"></asp:CompareValidator>
                                        <asp:ValidationSummary ID="ValidationSummary3" Font-Size="XX-Small" runat="server"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadCid" />
                                    </div>
                                    <fieldset class="formulario2">
                                        <legend>Anamnese/Exame F�sico</legend>
                                        <p>
                                            <span>
                                                <asp:TextBox CssClass="campo" ID="tbxAvaliacaoMedica" runat="server" Height="100px"
                                                    Rows="20" TextMode="MultiLine" MaxLength="200" Width="620px"></asp:TextBox>
                                            </span>
                                        </p>
                                    </fieldset>
                                    <asp:UpdatePanel ID="UpdatePanel_SumarioAlta" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlSituacao" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:Panel ID="Panel_SumarioAlta" runat="server" Visible="false">
                                                <fieldset class="formulario2">
                                                    <legend>Sum�rio de Alta</legend>
                                                    <p>
                                                        <span>
                                                            <asp:TextBox ID="TextBox_SumarioAlta" CssClass="campo" runat="server" TextMode="MultiLine"
                                                                Rows="20" Columns="5" Height="110px" Width="620px"></asp:TextBox>
                                                        </span>
                                                    </p>
                                                </fieldset>
                                            </asp:Panel>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <fieldset class="formulario2">
                                        <p style="height: auto;">
                                            <span class="rotulo">Classifica��o de Risco:</span> <span>
                                                <asp:DropDownList ID="DropDownList_ClassificacaoRisco" runat="server" AutoPostBack="true"
                                                    CssClass="drop" OnSelectedIndexChanged="OnSelectedIndexChanged_ClassificacaoRisco"
                                                    Width="265px" DataTextField="Descricao" DataValueField="Codigo">
                                                </asp:DropDownList>
                                                <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DropDownList_ClassificacaoRisco" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                    <ContentTemplate>
                                                        <asp:Image ID="Image_ClassificacaoRisco" runat="server" Width="16px" Height="18px"
                                                            Style="position: relative; margin-top: 0px; left: 0px; float: left" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </span>
                                        </p>
                                        <p style="height: auto;">
                                            <span class="rotulo">Situa��o:</span> <span>
                                                <asp:DropDownList ID="ddlSituacao" runat="server" Width="265px" AutoPostBack="true"
                                                    CssClass="drop" CausesValidation="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Situacao">
                                                </asp:DropDownList>
                                            </span>
                                        </p>
                                    </fieldset>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel_CompareValidator_Idade" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlSituacao" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:CompareValidator ID="CompareValidator_Idade" runat="server" ErrorMessage="Idade deve ser maior que zero."
                                                    Operator="GreaterThan" ValueToCompare="0" ControlToValidate="tbxIdade" Display="None"
                                                    ValidationGroup="ValidationGroup_cadProntuarioMedico">
                                                </asp:CompareValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" Font-Size="XX-Small"
                                            runat="server" ErrorMessage="Idade deve conter somente n�meros!" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            ValidationExpression="\d*" Display="None" ControlToValidate="tbxIdade"></asp:RegularExpressionValidator>
<%--                                        <asp:UpdatePanel ID="UpdatePanel_RequiredFieldValidator_Peso" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlSituacao" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Peso" runat="server" ErrorMessage="Peso � Obrigat�rio!"
                                                    ControlToValidate="TextBox_Peso" Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico">
                                                </asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>
<%--                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="TextBox_Peso" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="Peso com formato inv�lido.">
                                        </asp:RegularExpressionValidator>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxFreqCardiaca" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="Freq��ncia Card�aca com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxFreqRespitatoria" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="Freq��ncia Respirat�ria com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <%--                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="" EventName="" />
                                                </Triggers>
                                                <ContentTemplate>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TensaoInicio" runat="server" ErrorMessage="M�xima da Tens�o Arterial � Obrigat�rio!"
                                                ControlToValidate="tbxTensaoArterialInicio" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                                Display="None"></asp:RequiredFieldValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxTensaoArterialInicio" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="M�xima da Tens�o Arterial com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <%--                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="" EventName="" />
                                                </Triggers>
                                                <ContentTemplate>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_TensaoFim" runat="server" ErrorMessage="M�nima da Tens�o Arterial � Obrigat�rio!"
                                                ControlToValidate="tbxTensaoArterialFim" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                                Display="None"></asp:RequiredFieldValidator>
                                             </ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxTensaoArterialFim" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="M�nima da Tens�o Arterial com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxTemperatura" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="Temperatura com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                            ToolTip="Formato" ControlToValidate="tbxHgt" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                            Display="None" ErrorMessage="HGT com formato inv�lido.">
                                        </asp:RegularExpressionValidator>
                                        <asp:UpdatePanel ID="UpdatePanel_Grupo" runat="server" UpdateMode="Conditional">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="ddlSituacao" EventName="SelectedIndexChanged" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Anamnese" Font-Size="XX-Small"
                                                    runat="server" ErrorMessage="Anamnese/Exame F�sico � Obrigat�rio!" ControlToValidate="tbxAvaliacaoMedica"
                                                    Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator_ConsultaMedica" runat="server" ErrorMessage="ErrorMessageValidator"
                                                    Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico" OnServerValidate="OnServerValidate_ConsultaMedica"></asp:CustomValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_SumarioAlta" runat="server"
                                                    ErrorMessage="Sum�rio de Alta � Obrigat�rio!" Display="None" ValidationGroup="ValidationGroup_cadProntuarioMedico"
                                                    ControlToValidate="TextBox_SumarioAlta" Enabled="false"></asp:RequiredFieldValidator>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
                                            ErrorMessage="Situa��o � Obrigat�rio!" ControlToValidate="ddlSituacao" Display="None"
                                            ValidationGroup="ValidationGroup_cadProntuarioMedico" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server"
                                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadProntuarioMedico" />
                                    </div>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Prescri��o">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel_Prescricao" runat="server">
                                        <%--                                            <fieldset class="formulario2">
                                                <legend>Prescri��o</legend>--%>
                                        <fieldset class="formulario3">
                                            <legend>Procedimentos (SIGTAP)</legend>
                                            <asp:UpdatePanel ID="UpdatePanel_Quatro" runat="server" UpdateMode="Conditional"
                                                ChildrenAsTriggers="true" RenderMode="Inline">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimento1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span class="rotulo">Buscar Procedimento</span> <span>
                                                            <asp:TextBox ID="TextBox_BuscarProcedimentoSIGTAP" runat="server" CssClass="campo"
                                                                Width="250"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton4" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimento"
                                                                OnClick="OnClick_BuscarProcedimentoSIGTAP" ImageUrl="~/Urgencia/img/procurar.png"
                                                                Width="16px" Height="16px" Style="vertical-align: bottom;" />
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Informe o nome do procedimento."
                                                                Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAP" ValidationGroup="ValidationGroup_BuscarProcedimento"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                                                ErrorMessage="Informe pelo menos os tr�s primeiros caracteres do procedimento."
                                                                Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAP" ValidationGroup="ValidationGroup_BuscarProcedimento"
                                                                ValidationExpression="^\S{3}[\W-\w]*$">
                                                            </asp:RegularExpressionValidator>
                                                            <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true"
                                                                ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimento" />
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Procedimento *</span> <span>
                                                            <asp:DropDownList ID="DropDownList_Procedimento" runat="server" AutoPostBack="true"
                                                                OnSelectedIndexChanged="OnSelectedIndexChanged_RetiraCids" Width="395px" CssClass="drop">
                                                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Freq��ncia *</span> <span>
                                                            <asp:TextBox ID="TextBox_IntervaloProcedimento" Width="25px" CssClass="campo" MaxLength="4"
                                                                runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimento" CssClass="drop"
                                                                CausesValidation="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimento">
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Executar agora?</span> <span>
                                                            <asp:CheckBox ID="CheckBox_ExecutarProcimentoAgora" runat="server" />
                                                        </span>
                                                    </p>
                                                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um procedimento!"
                                                        ControlToValidate="DropDownList_Procedimento" Display="None" Operator="GreaterThan"
                                                        ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimento"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaProcedimento" runat="server"
                                                        ErrorMessage="Freq��ncia � Obrigat�rio!" Display="None" ValidationGroup="ValidationGroup_cadProcedimento"
                                                        ControlToValidate="TextBox_IntervaloProcedimento"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaProcedimento"
                                                        runat="server" ErrorMessage="Digite somente n�meros na freq��ncia." Display="None"
                                                        ControlToValidate="TextBox_IntervaloProcedimento" ValidationGroup="ValidationGroup_cadProcedimento"
                                                        ValidationExpression="^\d*$">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator_FrequenciaProcedimento" runat="server"
                                                        ErrorMessage="Freq��ncia deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimento"
                                                        Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_cadProcedimento"></asp:CompareValidator>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <TagPrefixPesquisarCid:TagNamePesquisarCid ID="WUC_ProcedimentoCid" runat="server" />
                                            <asp:UpdatePanel ID="UpdatePanel_ProcedimentoCID" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimento1" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Procedimento" EventName="SelectedIndexChanged" />
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
                                                    <asp:ImageButton ID="Button_AdicionarProcedimento1" runat="server" CausesValidation="true"
                                                        Width="134px" Height="38px" OnClick="OnClick_AdicionarProcedimento" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                                        ValidationGroup="ValidationGroup_cadProcedimento" />
                                                    <asp:ValidationSummary ID="ValidationSummary_procedimento" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_cadProcedimento" />
                                                </span>
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel_Cinco" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimento1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_Procedimento" OnRowDeleting="OnRowDeleting_DeletarProcedimento"
                                                                runat="server" Width="100%" AutoGenerateColumns="False">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freq��ncia" ItemStyle-Width="100px"
                                                                        ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="DescricaoCIDVinculado" HeaderText="CID" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="DescricaoExecutarPrimeiroMomento" HeaderText="Executar Agora?"
                                                                        ItemStyle-HorizontalAlign="Center" />
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
                                        <fieldset class="formulario3">
                                            <legend>Procedimentos N�o-Fatur�veis</legend>
                                            <asp:UpdatePanel ID="UpdatePanel_Quinze" runat="server" UpdateMode="Conditional"
                                                ChildrenAsTriggers="true" RenderMode="Inline">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavel1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span class="rotulo">Buscar Procedimento</span> <span>
                                                            <asp:TextBox ID="TextBox_BuscarProcedimento" runat="server" CssClass="campo" Width="250px"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton1" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                                OnClick="OnClick_BuscarProcedimentoNaoFaturavel" ImageUrl="~/Urgencia/img/procurar.png"
                                                                Width="16px" Height="16px" Style="vertical-align: bottom;" />
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Procedimento *</span> <span>
                                                            <asp:DropDownList ID="DropDownList_ProcedimentosNaoFaturaveis" CssClass="drop" DataValueField="Codigo"
                                                                Width="395px" runat="server">
                                                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Freq��ncia *</span> <span>
                                                            <asp:TextBox ID="TextBox_IntervaloProcedimentoNaoFaturavel" Width="25px" CssClass="campo"
                                                                MaxLength="4" runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavel"
                                                                CausesValidation="true" CssClass="drop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel">
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Executar agora?</span> <span>
                                                            <asp:CheckBox ID="CheckBox_ExecutarProcedimentoNaoFaturavelAgora" runat="server" />
                                                        </span>
                                                    </p>
                                                    <%--<p>
                                                        <span>--%>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione um procedimento."
                                                        ControlToValidate="DropDownList_ProcedimentosNaoFaturaveis" Display="None" Operator="GreaterThan"
                                                        ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaProcedimentoNaoFaturavel"
                                                        runat="server" ErrorMessage="Freq��ncia � Obrigat�rio!" Display="None" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"
                                                        ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavel"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaProcedimentoNaoFaturavel"
                                                        runat="server" ErrorMessage="Digite somente n�meros na freq��ncia." Display="None"
                                                        ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavel" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"
                                                        ValidationExpression="^\d*$">
                                                    </asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator_FrequenciaProcedimentoNaoFaturavel" runat="server"
                                                        ErrorMessage="Freq��ncia deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavel"
                                                        Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox_BuscarProcedimento"
                                                        Display="None" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                        ErrorMessage="Informe o nome do procedimento."></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os tr�s primeiros caracteres do Procedimento."
                                                        Display="None" ControlToValidate="TextBox_BuscarProcedimento" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                                        ValidationExpression="^\S{3}[\W-\w]*$">
                                                    </asp:RegularExpressionValidator>
                                                    <%-- </span>
                                                    </p>--%>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <p>
                                                <asp:ImageButton ID="Button_AdicionarprocedimentoNaoFaturavel1" CausesValidation="true"
                                                    runat="server" Width="134px" Height="38px" OnClick="OnClick_AdicionarProcedimentoNaoFaturavel"
                                                    ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel" />
                                                <asp:ValidationSummary ID="ValidationSummary5" ShowMessageBox="true" ShowSummary="false"
                                                    ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel" runat="server" />
                                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true"
                                                    ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel" />
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel_Quatorze" runat="server" UpdateMode="Conditional"
                                                ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavel1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ProcedimentosNaoFaturavel" AutoGenerateColumns="false"
                                                                runat="server" Width="100%" OnRowDeleting="OnRowDeleting_ExcluirProcedimentoNaoFaturavel">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" />
                                                                    <asp:BoundField HeaderText="Freq��ncia" DataField="DescricaoIntervalo" />
                                                                    <asp:BoundField DataField="DescricaoExecutarPrimeiroMomento" HeaderText="Executar Agora?"
                                                                        ItemStyle-HorizontalAlign="Center" />
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
                                        <fieldset class="formulario3">
                                            <legend>Medicamentos/Prescri��o</legend>
                                            <asp:UpdatePanel ID="UpdatePanel_Onze" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                                                ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span class="rotulo">Buscar Medicamento/Prescri��o</span> <span>
                                                            <asp:TextBox ID="TextBox_BuscarMedicamento" runat="server" CssClass="campo" Width="250"></asp:TextBox>
                                                            <asp:ImageButton ID="ImageButton2" runat="server" OnClick="OnClick_BuscarMedicamento"
                                                                ValidationGroup="ValidationGroup_BuscarMedicamento" ImageUrl="~/Urgencia/img/procurar.png"
                                                                Width="16px" Height="16px" Style="vertical-align: bottom;" />
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Medicamento/Prescri��o *</span> <span>
                                                            <asp:DropDownList ID="ddlMedicamentos" runat="server" Width="395px" CssClass="drop">
                                                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span><span>
                                                            <asp:ImageButton ID="ImageButton_Bulario" runat="server" OnClick="OnClick_VerBulario"
                                                                ValidationGroup="ValidationGroup_VerBulario" ImageUrl="~/Urgencia/img/bula.png"
                                                                Width="16px" Height="16px" Style="vertical-align: bottom;" />
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Freq��ncia *</span> <span>
                                                            <asp:TextBox ID="tbxIntervaloMedicamento" Width="25px" CssClass="campo" MaxLength="4"
                                                                runat="server"></asp:TextBox>
                                                            <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaMedicamento" CssClass="drop"
                                                                CausesValidation="true" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_MedicamentoNaoFaturavel">
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Via de Administra��o</span> <span>
                                                            <%--<asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamento" runat="server" CssClass="drop" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento">
                                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                            <asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamento" runat="server" CssClass="drop">
                                                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <span class="rotulo">Executar agora?</span> <span>
                                                            <asp:CheckBox ID="CheckBox_ExecutarMedicamentoAgora" runat="server" />
                                                        </span>
                                                    </p>
                                                    <p>
                                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione um(a) medicamento/prescri��o!"
                                                            ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="ddlMedicamentos"
                                                            ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_FrequenciaMedicamento" runat="server"
                                                            ErrorMessage="Freq��ncia � Obrigat�rio!" Display="None" ControlToValidate="tbxIntervaloMedicamento"
                                                            ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_FrequenciaMedicamento"
                                                            runat="server" ErrorMessage="Digite somente n�meros na freq��ncia." Display="None"
                                                            ControlToValidate="tbxIntervaloMedicamento" ValidationGroup="ValidationGroup_PrescricaoMedicamento"
                                                            ValidationExpression="^\d*$">
                                                        </asp:RegularExpressionValidator>
                                                        <asp:CompareValidator ID="CompareValidator_FrequenciaMedicamento" runat="server"
                                                            ErrorMessage="Freq��ncia deve ser maior que zero." ControlToValidate="tbxIntervaloMedicamento"
                                                            Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Selecione um(a) medicamento/prescri��o!"
                                                            ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="ddlMedicamentos"
                                                            ValidationGroup="ValidationGroup_VerBulario"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_BuscarMedicamento"
                                                            Display="None" ValidationGroup="ValidationGroup_BuscarMedicamento" ErrorMessage="Informe o nome do(a) medicamento/prescri��o."></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Informe pelo menos os tr�s primeiros caracteres do(a) Medicamento/Prescri��o."
                                                            Display="None" ControlToValidate="TextBox_BuscarMedicamento" ValidationGroup="ValidationGroup_BuscarMedicamento"
                                                            ValidationExpression="^\S{3}[\W-\w]*$">
                                                        </asp:RegularExpressionValidator>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <%--                                                    <p>
                                                        <span class="rotulo">Forma de Administra��o</span>
                                                        <asp:UpdatePanel ID="UpdatePanel_FormaAdministracao" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
                                                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Onze$DropDownList_ViaAdministracaoMedicamento"
                                                                    EventName="SelectedIndexChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>
                                                                <span>
                                                                    <asp:DropDownList ID="DropDownList_FormaAdministracaoMedicamento" runat="server">
                                                                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </span>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </p>--%>
                                            <asp:UpdatePanel ID="UpdatePanel_Dezesseis" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span class="rotulo">Observa��o</span> <span>
                                                            <asp:TextBox ID="TextBox_ObservacaoPrescricaoMedicamento" Width="620px" CssClass="campo"
                                                                Height="110px" Rows="20" Columns="5" TextMode="MultiLine" runat="server"></asp:TextBox>
                                                        </span>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <p>
                                                <span>
                                                    <asp:ImageButton ID="btnAdicionarMedicamento1" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                                        CausesValidation="true" OnClick="btnAdicionarMedicamento_Click" ValidationGroup="ValidationGroup_PrescricaoMedicamento"
                                                        Width="134px" Height="38px" />
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_PrescricaoMedicamento" />
                                                    <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_VerBulario" />
                                                    <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarMedicamento" />
                                                </span>
                                            </p>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="gridMedicamentos" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                OnRowDeleting="gridMedicamentos_RowDeleting">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento/Prescri��o" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freq��ncia" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administra��o" ItemStyle-HorizontalAlign="Center" />
                                                                    <%--<asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administra��o"
                                                                                ItemStyle-HorizontalAlign="Center" />--%>
                                                                    <asp:BoundField DataField="DescricaoObservacao" HeaderText="Observa��o" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField DataField="DescricaoExecutarPrimeiroMomento" HeaderText="Executar Agora?"
                                                                        ItemStyle-HorizontalAlign="Center" />
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
                                    </asp:Panel>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel_Tres" runat="server" HeaderText="Solicita��o de Exames"
                                Enabled="true">
                                <ContentTemplate>
                                    <fieldset class="formulario2">
                                        <legend>Exames Internos</legend>
                                        <p>
                                            <asp:UpdatePanel ID="UpdatePanel_Treze" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <%-- <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames" EventName="Click" />
                                                    </Triggers>--%>
                                                <ContentTemplate>
                                                    <span class="rotulo">Exame:</span> <span>
                                                        <asp:DropDownList ID="ddlExames" runat="server" Width="300px" CssClass="drop">
                                                        </asp:DropDownList>
                                                    </span><span style="position: absolute;">
                                                        <asp:ImageButton ID="img_btnAdicionarExames" runat="server" CausesValidation="true"
                                                            OnClick="btnAdicionarExames_Click" ValidationGroup="Exames" Height="19px" Width="19px"
                                                            ImageUrl="~/Urgencia/img/add.png" /></span>
                                                    <asp:CompareValidator ID="CompareValidator_Exames" runat="server" Display="None"
                                                        ControlToValidate="ddlExames" ErrorMessage="Selecione um Exame!" ValidationGroup="Exames"
                                                        ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                                    <asp:ValidationSummary ID="ValidationSummary_Um" runat="server" ValidationGroup="Exames"
                                                        ShowMessageBox="true" ShowSummary="false" />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </p>
                                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="img_btnAdicionarExames" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <p>
                                                    <span>
                                                        <asp:GridView ID="gridExames" runat="server" Width="100%" AutoGenerateColumns="False"
                                                            OnRowDeleting="gridExames_RowDeleting">
                                                            <Columns>
                                                                <asp:BoundField DataField="Exame" HeaderText="Exame" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center"
                                                                    DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                                                <asp:CommandField ShowDeleteButton="True" DeleteText="Excluir" ItemStyle-Width="50px"
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
                                    <fieldset class="formulario2">
                                        <legend>Exames Eletivos</legend>
                                        <p>
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <%--                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames" EventName="Click" />
                                                    </Triggers>--%>
                                                <ContentTemplate>
                                                    <span class="rotulo">Exame:</span> <span>
                                                        <asp:DropDownList ID="DropDownList_ExameEletivo" runat="server" Width="300px" CssClass="drop">
                                                        </asp:DropDownList>
                                                    </span><span style="position: static;">
                                                        <asp:ImageButton ID="img_ButtonAdicionarExameEletivo" runat="server" CausesValidation="true"
                                                            OnClick="OnClick_AdicionarExameEletivo" ValidationGroup="ExamesEletivos" Height="19px"
                                                            Width="19px" ImageUrl="~/Urgencia/img/add.png" /></span>
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
                                                <asp:AsyncPostBackTrigger ControlID="img_ButtonAdicionarExameEletivo" EventName="Click" />
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSalvar1" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <%-- <br />
                                <p align="center">
                                    <span>--%>
                                <asp:Panel ID="Panel_Acoes" runat="server" Visible="false">
                                    <div id="Div1" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                                        height: 130%; z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=75);
                                        moz-opacity: 0.3; opacity: 0.8">
                                    </div>
                                    <div id="Div2" style="position: fixed; top: 100px; left: 25%; width: 600px; z-index: 102;
                                        background-color: #541010; border-right: #ffffff  5px solid; padding-right: 10px;
                                        border-top: #ffffff  5px solid; padding-left: 10px; padding-bottom: 10px; border-left: #ffffff  5px solid;
                                        color: #000000; padding-top: 10px; border-bottom: #ffffff 5px solid; text-align: justify;
                                        font-family: Verdana;">
                                        <div style="padding-left: 0px;">
                                            <p>
                                                <span>
                                                    <asp:Label ID="Label_InfoConfirmarFatura" runat="server" Text="A��es" Font-Bold="true"
                                                        ForeColor="White" Font-Size="Medium"></asp:Label>
                                                </span>
                                                <div class="botoesroll">
                                                    <asp:ImageButton ID="Button_GerarReceita1" ImageUrl="~/Urgencia/img/bts/btn-emitirreceita.png"
                                                        runat="server" Width="154px" Height="38px" CommandArgument="receita" OnClick="OnClick_GerarReceitaAtestado" />
                                                </div>
                                                <div class="botoesroll">
                                                    <asp:ImageButton ID="Button_GerarAtestado1" ImageUrl="~/Urgencia/img/bts/btn-emitiratestado.png"
                                                        runat="server" Width="154px" Height="38px" CommandArgument="atestado" OnClick="OnClick_GerarReceitaAtestado" />
                                                </div>
                                                <div class="botoesroll">
                                                    <asp:ImageButton ID="Button_GerarComparecimento" ImageUrl="~/Urgencia/img/bts/bt_gerarcomparecimento1.png"
                                                        runat="server" CommandArgument="comparecimento" OnClick="OnClick_GerarReceitaAtestado"
                                                        Width="210px" Height="38px" />
                                                </div>
                                                <div class="botoesroll">
                                                    <asp:LinkButton ID="ButtonSair" runat="server" PostBackUrl="~/Urgencia/ExibeFilaAtendimento.aspx">
	<img id="btnsair" alt="Sair" src="img/btn-sair-1.png"
                onmouseover="btnsair.src='img/btn-sair-2.png';"
                onmouseout="btnsair.src='img/btn-sair-1.png';" />
                                                    </asp:LinkButton>
                                                </div>
                                            </p>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <%-- </span>
                                </p>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <p align="center">
                                    <span>
                                        <asp:ImageButton ID="btnSalvar1" runat="server" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png"
                                            Width="134px" Height="38px" CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadProntuarioMedico')) return confirm('Todos os dados informados para este atendimento est�o corretos ?'); return false;"
                                            OnClick="btnSalvar_Click" ValidationGroup="ValidationGroup_cadProntuarioMedico" />
                                        <asp:ImageButton ID="Button_Cancelar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                            Text="Cancelar" PostBackUrl="~/Urgencia/Default.aspx" Width="134px" Height="38px" />
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabHistorico" runat="server" HeaderText="Hist�rico">
                    <ContentTemplate>
                        <fieldset class="formulario2">
                            <legend>Informa��es Dispon�veis</legend>
                            <p>
                                <span>
                                    <IMH:Inc_MenuHistorico ID="Inc_MenuHistorico" runat="server"></IMH:Inc_MenuHistorico>
                                </span>
                            </p>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </fieldset>
    </div>
    <%--    <script type="text/javascript">
        alert(document.body.offsetHeight);
    </script>--%>
</asp:Content>