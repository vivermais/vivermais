<%@ Page Language="C#" MasterPageFile="~/Paciente/MasterPaciente.Master" AutoEventWireup="true"
    CodeBehind="FormPaciente.aspx.cs" Inherits="ViverMais.View.Paciente.FormPaciente"
    EnableEventValidation="false" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
        function calendarShown(sender, e) {
            sender._switchMode("years");
        }
        
        function ValidarData(sender, args)
        {
            args.IsValid = VerificaData(args.Value);
        }
         
        function VerificaData(valor) {
            var date=valor;
            var ardt=new Array;
            var ExpReg= new RegExp("(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/[12][0-9]{3}");
            ardt=date.split("/");
            erro=false;
            
            if ( date.search(ExpReg)==-1)
                    erro = true;
            
            else if (((ardt[1]==4)||(ardt[1]==6)||(ardt[1]==9)||(ardt[1]==11))&&(ardt[0]>30))
                    erro = true;
            else if ( ardt[1]==2) {
                    if ((ardt[0]>28)&&((ardt[2]%4)!=0))
                            erro = true;
                    if ((ardt[0]>29)&&((ardt[2]%4)==0))
                            erro = true;
            }
            return !erro;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSalvar" />
            <asp:PostBackTrigger ControlID="lnkCartaoSus" />
            <asp:PostBackTrigger ControlID="lnkEtiqueta" />
            <asp:PostBackTrigger ControlID="lnkBtnEtiquetaHTML" />
            <asp:AsyncPostBackTrigger ControlID="imgBuscarCEP" />
            <asp:AsyncPostBackTrigger ControlID="ddlRacaCor" />
        </Triggers>
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formul�rio de Paciente
        </h2>
        <br />
        <fieldset class="formulario" style="font-size: 12px; background-color: #f0f0f0">
            <br />
            Para altera��o dos campos em <span style="color: #000;"><b>(Cinza)</b></span> favor
            entrar em contato com o NGI - N�cleo de Gest�o da Informa��o.
            <br />
            <br />
            <b>Contatos: </b><b>Tel.:</b> (71) 3186-1180 / 1188 / <b>E-mail:</b> ngi.saude@salvador.ba.gov.br
        </fieldset>
        <fieldset class="formulario">
            <%--            <asp:Panel ID="panelInforma��o" runat="server" Visible="true">
                <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                    height: 200%; z-index: 100; min-height: 100%; background-color: #999;">
                </div>
                <div id="mensagem" visible="false" style="position: fixed; top: 10px; left: 30%; background-image: url('../img/msg-ViverMais-cartaosus.png');
                    width: 623px; height: 542px; z-index: 102; ">
                    <p >
                        <span style="margin-left: 568px; margin-top:25px; top:25px">
                            <asp:LinkButton ID="btnFechar" runat="server" CausesValidation="false" Text="<img src='../img/fechar-carna.png'>"
                                Font-Size="X-Small" OnClick="btnFechar_Click">
                            </asp:LinkButton>
                        </span>
                        
                    </p>
                </div>
            </asp:Panel>
--%>
            <legend>Dados Pessoais</legend>
            <%--<p>--%>
            <asp:UpdatePanel ID="UpdatePanel_CartaoSUS" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Cart�o SUS</span>&nbsp;<span>
                            <asp:Label ID="lblCNS" runat="server" Font-Bold="True" Font-Size="18px" Text=""></asp:Label>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Via do Cart�o</span> <span>
                    <asp:Label ID="lblViaCartao" runat="server" Font-Bold="True" Text="1�"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Biometria</span> <span>
                    <asp:Label ID="lblBiometria" runat="server" Font-Bold="True" Text="N�o Cadastrada"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Motivo do Cadastramento*</span> <span>
                    <asp:DropDownList ID="ddlMotivo" CssClass="campo" Height="21px" runat="server" Width="300px"
                        DataTextField="Motivo" DataValueField="Codigo" TabIndex="1">
                    </asp:DropDownList>
                </span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorMotivo" runat="server" ControlToValidate="ddlMotivo"
                    Display="Dynamic" ErrorMessage="Selecione um Motivo" Font-Size="XX-Small" InitialValue="-1"
                    ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
            </p>
            <p>
                <span class="rotulo" style="background-color: #9e9e9e; border: solid 1px #9e9e9e">Nome
                    do Paciente*</span> <span>
                        <asp:TextBox ID="tbxNomePaciente" runat="server" CssClass="campo" MaxLength="70"
                            Width="400px" TabIndex="1"></asp:TextBox>
                    </span>
            </p>
            <asp:RequiredFieldValidator ID="RequiredFieldNomePaciente" runat="server" ControlToValidate="tbxNomePaciente"
                Display="Dynamic" ErrorMessage="Digite o nome do Paciente" Font-Size="XX-Small"
                ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxNomePaciente"
                Display="Dynamic" ErrorMessage="H� caracters inv�lidos no Nome do Paciente" Font-Size="XX-Small"
                ValidationExpression="^[a-zA-Z\s]{1,70}$" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
            <asp:UpdatePanel ID="UpdatePanelNomeMae" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <ContentTemplate>
                    <p>
                        <span class="rotulo" style="background-color: #9e9e9e; border: solid 1px #9e9e9e">Nome
                            da M�e*</span> <span>
                                <asp:TextBox ID="tbxNomeMae" runat="server" CssClass="campo" MaxLength="70" Width="400px"
                                    TabIndex="2"></asp:TextBox></span> <span>
                                        <asp:CheckBox ID="chkMaeIgnorada" Text="IGNORADA" runat="server" Checked="false"
                                            AutoPostBack="true" OnCheckedChanged="chkMaeIgnorada_CheckedChanged" />
                                    </span>
                    </p>
                    <asp:RequiredFieldValidator ID="RequiredFieldNomeMae" runat="server" ControlToValidate="tbxNomeMae"
                        Display="Dynamic" ErrorMessage="Digite o Nome da M�e do Paciente" Font-Size="XX-Small"
                        ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxNomeMae"
                        Display="Dynamic" ErrorMessage="H� caracters inv�lidos no Nome da M�e do Paciente"
                        Font-Size="XX-Small" ValidationExpression="^[a-zA-Z\s]{1,70}$" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelNomePai" runat="server" ChildrenAsTriggers="true"
                RenderMode="Inline" UpdateMode="Conditional">
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Nome do Pai</span> <span>
                            <asp:TextBox ID="tbxNomePai" runat="server" CssClass="campo" MaxLength="70" Width="400px"
                                TabIndex="3"></asp:TextBox>
                            <span><span>
                                <asp:CheckBox ID="chkPaiIgnorado" Text="IGNORADO" runat="server" Checked="false"
                                    AutoPostBack="true" OnCheckedChanged="chkPaiIgnorado_CheckedChanged" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxNomePai"
                                    Display="Dynamic" ErrorMessage="H� caracters inchkPaiIgnoradov�lidos no Nome do Pai do Paciente"
                                    Font-Size="XX-Small" ValidationExpression="^[a-zA-Z\s]{1,70}$" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
                            </span></span></span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo" style="background-color: #9e9e9e; border: solid 1px #9e9e9e">Data
                    de Nascimento*</span> <span>
                        <asp:TextBox ID="tbxDataNascimento" runat="server" CssClass="campo" Width="80px"
                            MaxLength="10"></asp:TextBox>
                        <cc1:CalendarExtender ID="calTitleLength" runat="server" TargetControlID="tbxDataNascimento"
                            OnClientShown="calendarShown" Format="dd/MM/yyyy" />
                        <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" ClearMaskOnLostFocus="true"
                            InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="tbxDataNascimento">
                        </cc1:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="tbxDataNascimento"
                            Display="Dynamic" ErrorMessage="Preencha a Data de Nascimento" Font-Size="XX-Small"
                            ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator_DataNascimento" runat="server" ClientValidationFunction="ValidarData"
                            ControlToValidate="tbxDataNascimento" ErrorMessage="A data de Nascimento n�o parece ser v�lida."
                            ValidateEmptyText="true" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                        <%--<asp:CompareValidator ID="compareData" runat="server" ControlToValidate="tbxDataNascimento"
                        Display="Dynamic" ErrorMessage="A data de Nascimento n�o parece ser v�lida" Font-Size="XX-Small"
                        Operator="DataTypeCheck" Type="Date" CultureInvariantValues="false" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>--%>
                        <%--</span><span>--%>
                        <asp:CustomValidator ID="CustomValidatorDataNascimento" runat="server" Display="Dynamic"
                            ErrorMessage="A Data de Nascimento deve ser menor ou igual a Data de Hoje" Font-Size="XX-Small"
                            OnServerValidate="CustomValidatorDataNascimento_ServerValidate" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                    </span>
            </p>
            <p>
                <span class="rotulo">Sexo*</span> <span>
                    <asp:RadioButtonList ID="rbtnListSexo" runat="server" CellPadding="0" CellSpacing="0"
                        CssClass="radio1" TextAlign="Right" RepeatDirection="Horizontal" TabIndex="5">
                        <asp:ListItem Value="M">Masculino</asp:ListItem>
                        <asp:ListItem Value="F">Feminino</asp:ListItem>
                    </asp:RadioButtonList>
                </span>
            </p>
            <asp:RequiredFieldValidator ID="RequiredFieldSexo" runat="server" ControlToValidate="rbtnListSexo"
                Display="Dynamic" ErrorMessage="Selecione o Sexo" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
            <p>
                <span class="rotulo">Ra�a/Cor*</span> <span>
                    <asp:DropDownList ID="ddlRacaCor" CssClass="campo" Height="21px" runat="server" Width="200px"
                        DataTextField="Descricao" DataValueField="Codigo" CausesValidation="true" TabIndex="6"
                        OnSelectedIndexChanged="ddlRacaCor_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:RequiredFieldValidator ID="RequiredFieldRacaCor" runat="server" ControlToValidate="ddlRacaCor"
                Display="Dynamic" ErrorMessage="Selecione a Ra�a/Cor do Paciente" Font-Size="XX-Small"
                InitialValue="-1" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
            <asp:UpdatePanel ID="UpdatePanelEtnia" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlRacaCor" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="ViewEtnia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Etnia*</span> <span>
                                <asp:DropDownList ID="ddlEtnia" CssClass="campo" Height="21px" runat="server" Width="200px"
                                    DataTextField="Descricao" DataValueField="Codigo" TabIndex="6">
                                </asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Frequenta Escola*</span> <span>
                    <asp:RadioButtonList ID="rbtnListFreqEscola" runat="server" CellPadding="0" CellSpacing="0"
                        CssClass="radio1" RepeatDirection="Horizontal" TextAlign="Right" TabIndex="7">
                        <asp:ListItem Value="S">Sim</asp:ListItem>
                        <asp:ListItem Value="N">N�o</asp:ListItem>
                    </asp:RadioButtonList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanelNacionalidade" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Nacionalidade*</span> <span>
                            <asp:RadioButtonList ID="rbtnListNacionalidade" runat="server" AutoPostBack="True"
                                CellPadding="0" CellSpacing="0" CssClass="radio1" OnSelectedIndexChanged="rbtnListNacionalidade_SelectedIndexChanged"
                                RepeatDirection="Vertical" TextAlign="Right">
                                <asp:ListItem Value="B" Selected="True">Brasileira</asp:ListItem>
                                <asp:ListItem Value="E">Estrangeira</asp:ListItem>
                                <asp:ListItem Value="N">Naturalizado</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="rbtnListNacionalidade"
                            Display="Dynamic" ErrorMessage="Selecione uma Nacionalidade" Font-Size="XX-Small"
                            ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="rbtnListFreqEscola"
                Display="Dynamic" ErrorMessage="Selecione uma op��o para Frequenta Escola" ValidationGroup="ValidationGroupCartaoSUS"
                Font-Size="XX-Small"></asp:RequiredFieldValidator>
            <asp:UpdatePanel ID="UpdatePanelMultiView" runat="server" UpdateMode="Conditional"
                RenderMode="Inline" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbtnListNacionalidade" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="ViewBrasileiro" runat="server">
                            <p>
                                <span class="rotulo">UF de Nascimento*</span> <span>
                                    <asp:DropDownList ID="ddlUFNascimento" CssClass="campo" Height="21px" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlUFNascimento_SelectedIndexChanged"
                                        Width="200px" DataTextField="Sigla" DataValueField="Sigla" TabIndex="9">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidatorUFNasc" runat="server" Display="Dynamic"
                                        ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha o UF de Nascimento"
                                        Font-Size="XX-Small" OnServerValidate="CustomValidatorUFNasc_ServerValidate"></asp:CustomValidator>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Munic�pio de Nascimento*</span> <span>
                                    <asp:DropDownList ID="ddlMunicipioNascimento" CssClass="campo" Height="21px" runat="server"
                                        Width="200px" DataTextField="NomeSemUF" DataValueField="Codigo" TabIndex="10">
                                    </asp:DropDownList>
                                    <asp:CustomValidator ID="CustomValidatorMunicipioNasc" runat="server" Display="Dynamic"
                                        ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha o Munic�pio de Nascimento"
                                        Font-Size="XX-Small" OnServerValidate="CustomValidatorMunicipioNasc_ServerValidate"></asp:CustomValidator>
                                </span>
                            </p>
                        </asp:View>
                        <asp:View ID="ViewEstrangeiro" runat="server">
                            <p>
                                <span class="rotulo">Pa�s*</span> <span>
                                    <asp:DropDownList ID="ddlPaisOrigem" CssClass="campo" Height="21px" runat="server"
                                        Width="200px" DataTextField="Nome" DataValueField="Codigo" TabIndex="11">
                                    </asp:DropDownList>
                                </span>
                                <asp:CustomValidator ID="CustomValidatorPaisNasc" runat="server" Display="Dynamic"
                                    ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha o Pa�s de Origem"
                                    Font-Size="XX-Small" OnServerValidate="CustomValidatorPaisNasc_ServerValidate"></asp:CustomValidator>
                            </p>
                            <p>
                                <span class="rotulo">Data de Entrada no Brasil*</span> <span>
                                    <asp:TextBox ID="tbxDataEntradaBrasil" CssClass="campo" runat="server" TabIndex="12"></asp:TextBox></span>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="tbxDataEntradaBrasil"
                                    OnClientShown="calendarShown" />
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" ClearMaskOnLostFocus="true"
                                    InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="tbxDataEntradaBrasil">
                                </cc1:MaskedEditExtender>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="img/calendar_icon.png"
                                    TargetControlID="tbxDataEntradaBrasil">
                                </cc1:CalendarExtender>
                                <asp:CustomValidator ID="CustomValidatorDataEntradaBrasil" runat="server" Display="Dynamic"
                                    ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha a Data de Entrada no Brasil"
                                    Font-Size="XX-Small" OnServerValidate="CustomValidatorDataEntradaBrasil_ServerValidate"></asp:CustomValidator>
                            </p>
                        </asp:View>
                        <asp:View ID="ViewNaturalizado" runat="server">
                            <p>
                                <span class="rotulo">Data da Naturaliza��o*</span> <span>
                                    <asp:TextBox ID="tbxDataNaturalizacao" CssClass="campo" runat="server" TabIndex="13"></asp:TextBox></span>
                                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="tbxDataNaturalizacao"
                                    OnClientShown="calendarShown" />
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="true"
                                    InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="tbxDataNaturalizacao">
                                </cc1:MaskedEditExtender>
                                <asp:CustomValidator ID="CustomValidatorDataNaturalizacao" runat="server" Display="Dynamic"
                                    ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha a Data de Naturaliza��o"
                                    Font-Size="XX-Small" OnServerValidate="CustomValidatorDataNaturalizacao_ServerValidate"></asp:CustomValidator>
                            </p>
                            <p>
                                <span class="rotulo">N�mero da Portaria*</span> <span>
                                    <asp:TextBox ID="tbxNaturalizacaoPortaria" CssClass="campo" runat="server" MaxLength="16"
                                        TabIndex="14"></asp:TextBox></span>
                                <asp:CustomValidator ID="CustomValidatorNumPortaria" runat="server" Display="Dynamic"
                                    ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Digite o N�mero da Portaria"
                                    Font-Size="XX-Small" OnServerValidate="CustomValidatorNumPortaria_ServerValidate"></asp:CustomValidator>
                            </p>
                        </asp:View>
                    </asp:MultiView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelPossuiDeficiencia" runat="server" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Possui Defici�ncia*</span> <span>
                            <asp:RadioButtonList ID="rbDeficiencia" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Deficiencia"
                                CssClass="camporadio" BorderStyle="None" ForeColor="Black" Font-Bold="true" Font-Size="11px"
                                RepeatColumns="2" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_Deficiencia" runat="server"
                                Font-Size="XX-Small" ErrorMessage="Informe se o Paciente Possui Defic�ncia" ControlToValidate="rbDeficiencia"
                                Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_DeficienciaParte1" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbDeficiencia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_TipoDeficiencia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Tipo de Defici�ncia*</span> <span>
                                <div style="float: left; width: 405px;">
                                    <asp:CheckBoxList ID="chckTipoDeficiencia" runat="server" CssClass="camporadio" RepeatColumns="6"
                                        RepeatDirection="Horizontal" DataValueField="Codigo" AutoPostBack="true" BorderStyle="None"
                                        CausesValidation="true" ForeColor="Black" Font-Bold="true" Font-Size="11px" OnSelectedIndexChanged="OnSelectedIndexChanged_TipoDeficiencia"
                                        DataTextField="Nome">
                                    </asp:CheckBoxList>
                                </div>
                                <div style="margin-top: 13px; width: 16px; height: 18px; position: relative;">
                                    <asp:ImageButton ID="ImageButtonInformacaoTipoDeficiencia" runat="server" Width="16px"
                                        ToolTip="O que � Estomia?" Height="18px" OnClientClick="return false;" ImageUrl="~/Paciente/img/help.png" />
                                </div>
                                <div id="flyout" class="wireFrame">
                                </div>
                                <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                    <div id="btnCloseParent" style="float: left; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                                        <asp:LinkButton ID="LinkButtonFecharInformativoDeficiencia" runat="server" OnClientClick="return false;"
                                            Text="X" ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                    </div>
                                    Estomia � um procedimento cir�rgico que consiste na exterioriza��o do sistema (digestivo,
                                    respirat�rio e urin�rio), criando um orif�cio externo.
                                </div>
                                <br />
                                <asp:CustomValidator ID="CustomValidator_TipoDeficiencia" runat="server" OnServerValidate="OnServerValidate_TipoDeficiencia"
                                    ErrorMessage="Selecione Pelo Menos um Tipo de Defici�ncia" Display="Dynamic"
                                    Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS" Text="Selecione Pelo Menos um Tipo de Defici�ncia">
                                </asp:CustomValidator>
                                <cc1:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="ImageButtonInformacaoTipoDeficiencia">
                                    <Animations>
                                            <OnClick>
                                               <Sequence>
                                               <EnableAction Enabled="false" />
                                               
                                               <ScriptAction Script="Cover($get('ImageButtonInformacaoTipoDeficiencia'), $get('flyout'));" />
                                               <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                               <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                               <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                               <FadeIn AnimationTarget="info" Duration=".2"/>
                                               <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                               </Sequence>
                                            </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="LinkButtonFecharInformativoDeficiencia">
                                    <Animations>
                                            <OnClick>
                                               <Sequence AnimationTarget="info">
                                               
                                               <StyleAction Attribute="overflow" Value="hidden"/>
                                               <Parallel Duration=".3" Fps="15">
                                                  <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                  <FadeOut />
                                               </Parallel>
                                               
                                               <StyleAction Attribute="display" Value="none"/>
                                               <StyleAction Attribute="width" Value="250px"/>
                                               <StyleAction Attribute="height" Value=""/>
                                               <StyleAction Attribute="fontSize" Value="12px"/>
                                               
                                               <EnableAction AnimationTarget="ImageButtonInformacaoTipoDeficiencia" Enabled="true" />
                                               </Sequence>
                                            </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                            </span>
                        </p>
                        <%--                        <p>
                            <span class="rotulo">Origem da Defici�ncia*</span> <span>
                                <asp:CheckBoxList ID="chckOrigemDeficiencia" runat="server" CssClass="camporadio"
                                    RepeatColumns="5" RepeatDirection="Horizontal" DataValueField="Codigo" DataTextField="Nome"
                                    BorderStyle="None" ForeColor="Black" Font-Bold="true" Font-Size="11px">
                                </asp:CheckBoxList>
                                <asp:CustomValidator ID="CustomValidator_OrigemDeficiencia" runat="server" ErrorMessage="Selecione Pelo Menos uma Origem da(s) Defici�ncia(s)"
                                    Display="Dynamic" OnServerValidate="OnServerValidate_OrigemDeficiencia" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                            </span>
                        </p>--%></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelDeficienciaParte4" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbDeficiencia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_OrigemDeficiencia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Origem da Defici�ncia*</span> <span>
                                <asp:CheckBoxList ID="chckOrigemDeficiencia" runat="server" CssClass="camporadio"
                                    RepeatColumns="5" RepeatDirection="Horizontal" DataValueField="Codigo" DataTextField="Nome"
                                    BorderStyle="None" ForeColor="Black" Font-Bold="true" Font-Size="11px">
                                </asp:CheckBoxList>
                                <asp:CustomValidator ID="CustomValidator_OrigemDeficiencia" runat="server" ErrorMessage="Selecione Pelo Menos uma Origem da(s) Defici�ncia(s)"
                                    Font-Size="XX-Small" Display="Dynamic" OnServerValidate="OnServerValidate_OrigemDeficiencia"
                                    ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_DeficienciaParte3" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbDeficiencia" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chckTipoDeficiencia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_MeioLocomocaoDeficiencia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Meios de Locomo��o*</span><span>
                                <asp:CheckBoxList ID="chckLocomocaoDeficiencia" runat="server" CssClass="camporadio"
                                    RepeatColumns="6" DataValueField="Codigo" DataTextField="Nome" RepeatDirection="Horizontal"
                                    BorderStyle="None" ForeColor="Black" Font-Bold="true" Font-Size="11px" AutoPostBack="true"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_Locomocao" CausesValidation="true" />
                                <asp:CustomValidator Font-Size="XX-Small" ID="CustomValidator_LocomocaoDeficiencia"
                                    runat="server" ErrorMessage="Selecione Pelo Menos um Meio de Locomo��o para a(s) Defici�ncia(s) do Paciente"
                                    Display="Dynamic" OnServerValidate="OnServerValidate_LocomocaoDeficiencia" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="Panel_MeioComunicacaoDeficiencia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Meios de Comunica��o*</span><span>
                                <asp:CheckBoxList ID="chckComunicacaoDeficiencia" runat="server" CssClass="camporadio"
                                    RepeatColumns="5" DataValueField="Codigo" DataTextField="Nome" RepeatDirection="Horizontal"
                                    BorderStyle="None" ForeColor="Black" Font-Bold="true" Font-Size="11px" AutoPostBack="true"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_Comunicacao" CausesValidation="true" />
                                <asp:CustomValidator Font-Size="XX-Small" ID="CustomValidator_ComunicacaoDeficiencia"
                                    runat="server" ErrorMessage="Selecione Pelo Menos um Meio de Comunica��o para a(s) Defici�ncia(s) do Paciente"
                                    Display="Dynamic" OnServerValidate="OnServerValidate_ComunicacaoDeficiencia"
                                    ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false"
                RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbDeficiencia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_OrteseDeficiencia" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Usa Ortese*</span> <span>
                                <asp:RadioButtonList ID="rbOrtese" runat="server" CssClass="camporadio" RepeatColumns="2"
                                    RepeatDirection="Horizontal" BorderStyle="None" ForeColor="Black" Font-Bold="true"
                                    Font-Size="11px">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Ortese" Font-Size="XX-Small"
                                    runat="server" ErrorMessage="Informe se o Paciente usa �rtese" ControlToValidate="rbOrtese"
                                    Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_DeficienciaParte2" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="rbDeficiencia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_ProteseDeficiencia" runat="server" Visible="false">
                        <%--                        <p>
                            <span class="rotulo">Usa Ortese*</span> <span>
                                <asp:RadioButtonList ID="rbOrtese" runat="server" CssClass="camporadio" RepeatColumns="2"
                                    RepeatDirection="Horizontal" BorderStyle="None" ForeColor="Black" Font-Bold="true"
                                    Font-Size="11px">
                                </asp:RadioButtonList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Ortese" runat="server" ErrorMessage="Informe se o Paciente usa �rtese"
                                    ControlToValidate="rbOrtese" Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                            </span>
                        </p>--%>
                        <p>
                            <span class="rotulo">Pr�tese*</span> <span>
                                <asp:CheckBoxList ID="chckProtese" runat="server" CssClass="camporadio" RepeatColumns="5"
                                    RepeatDirection="Horizontal" DataValueField="Codigo" BorderStyle="None" ForeColor="Black"
                                    Font-Bold="true" Font-Size="11px" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Protese"
                                    DataTextField="Nome" CausesValidation="true">
                                </asp:CheckBoxList>
                                <asp:CustomValidator Font-Size="XX-Small" ID="CustomValidator_Protese" runat="server"
                                    OnServerValidate="OnServerValidate_Protese" ErrorMessage="Informe se o Paciente Usa Alguma Pr�tese"
                                    Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Email</span> <span>
                    <asp:TextBox ID="tbxEmail" runat="server" CssClass="campo" MaxLength="100" Width="300px"
                        TabIndex="15"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="tbxEmail"
                        Display="Dynamic" ErrorMessage="O Email n�o parece ser v�lido" Font-Size="XX-Small"
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Vivo</span> <span>
                    <asp:CheckBox ID="cbxVivo" CssClass="campo" runat="server" Checked="true" Width="25px"
                        TabIndex="16" />
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Documentos</legend>
            <p>
                <span class="rotulo">CPF</span> <span>
                    <asp:TextBox ID="tbxCPF" CssClass="campo" runat="server" MaxLength="11" TabIndex="17"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidatorCPF" runat="server" ControlToValidate="tbxCPF"
                        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="O CPF deve conter apenas N�meros"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator></span>
            </p>
            <p>
                <span class="rotulo">Identidade</span> <span>
                    <asp:TextBox ID="tbxIdentidade" CssClass="campo" runat="server" MaxLength="10" TabIndex="18"></asp:TextBox>
                    <asp:UpdatePanel ID="UpdatePanel_IdentidadeParte1" runat="server" ChildrenAsTriggers="false"
                        RenderMode="Inline" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSalvar" />
                            <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                            <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnListNacionalidade" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:CompareValidator ID="CompareValidatorIdentidade" runat="server" ControlToValidate="tbxIdentidade"
                                Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A Identidade deve Conter apenas N�meros"
                                Operator="DataTypeCheck" Type="Double" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Existem Caracteres inv�lidos no campo Identidade"
                                ControlToValidate="tbxIdentidade" ValidationExpression="^[a-zA-Z0-9\s]{1,10}$"
                                Font-Size="XX-Small" Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS">
                            </asp:RegularExpressionValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    -&nbsp;<asp:TextBox ID="tbxComplementoIdentidade" CssClass="campo" runat="server"
                        MaxLength="4" Width="60px" TabIndex="19"></asp:TextBox>
                    <asp:UpdatePanel ID="UpdatePanel_IdentidadeParte2" runat="server" ChildrenAsTriggers="false"
                        RenderMode="Inline" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSalvar" />
                            <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                            <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                            <asp:AsyncPostBackTrigger ControlID="rbtnListNacionalidade" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbxComplementoIdentidade"
                                Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A Identidade deve Conter apenas N�meros"
                                Operator="DataTypeCheck" Type="Double" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Existem Caracteres inv�lidos no campo Identidade"
                                ControlToValidate="tbxComplementoIdentidade" ValidationExpression="^[a-zA-Z0-9\s]{1,10}$"
                                Font-Size="XX-Small" Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS">
                            </asp:RegularExpressionValidator>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span><span class="rotulomin">Data da Emiss�o</span> <span>
                    <asp:TextBox ID="tbxDataEmissaoIdentidade" CssClass="campo" runat="server" Width="80px"
                        TabIndex="20"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="tbxDataEmissaoIdentidade"
                        OnClientShown="calendarShown" />
                    <cc1:MaskedEditExtender ID="MaskedEditEmissaoIdentidade" runat="server" TargetControlID="tbxDataEmissaoIdentidade"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                    </cc1:MaskedEditExtender>
                    <asp:CompareValidator ID="CompareValidatorEmissaoIdentidade" runat="server" ControlToValidate="tbxDataEmissaoIdentidade"
                        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A Data da Emiss�o inv�lida"
                        Operator="DataTypeCheck" Type="Date" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                    <asp:CustomValidator ID="CVDataEmissaoIdentidade" runat="server" ErrorMessage="A Data de Emiss�o da Identidade deve ser maior que a Data de Nascimento e igual ou menor que a Data de Hoje"
                        OnServerValidate="CVDataEmissaoIdentidade_ServerValidate" Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"
                        Font-Size="XX-Small"></asp:CustomValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">�rg�o Expedidor</span> <span>
                    <asp:DropDownList ID="ddlOrgaoExpedidor" CssClass="campo" Height="21px" runat="server"
                        Width="350px" DataValueField="Codigo" DataTextField="Nome" TabIndex="21">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidatorOrgaoEmissor" runat="server" ControlToValidate="ddlOrgaoExpedidor"
                        Display="Dynamic" ErrorMessage="Selecione um �rg�o Emissor" Font-Size="XX-Small"
                        OnServerValidate="CustomValidatorOrgaoEmissor_ServerValidate" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">UF</span> <span>
                    <asp:DropDownList ID="ddlUFIdentidade" CssClass="campo" Height="21px" runat="server"
                        Width="100px" DataTextField="Sigla" DataValueField="Sigla" TabIndex="22">
                    </asp:DropDownList>
                    <asp:CustomValidator ID="CustomValidatorUFIdentidade" runat="server" Display="Dynamic"
                        ErrorMessage="Selecione O UF emissor da Identidade" ValidationGroup="ValidationGroupCartaoSUS"
                        Font-Size="XX-Small" OnServerValidate="CustomValidatorUFIdentidade_ServerValidate"></asp:CustomValidator></span>
            </p>
            <p>
                <span class="rotulo">Certid�o</span> <span>
                    <asp:DropDownList ID="ddlTipoCertidao" CssClass="campo" Height="21px" runat="server"
                        TabIndex="23">
                        <asp:ListItem Value="-1">Selecione...</asp:ListItem>
                        <asp:ListItem Value="91">Certid�o de Nascimento</asp:ListItem>
                        <asp:ListItem Value="92">Certid�o de Casamento</asp:ListItem>
                        <asp:ListItem Value="93">Certid�o de Separa��o ou Div�rcio</asp:ListItem>
                        <asp:ListItem Value="95">Certid�o Administrativa - �ndio</asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Nome do Cart�rio</span> <span>
                    <asp:TextBox ID="tbxNomeCartorio" CssClass="campo" runat="server" Width="250px" MaxLength="20"
                        TabIndex="24"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidatorCartorio" runat="server" Display="Dynamic"
                        Font-Size="XX-Small" ErrorMessage="Digite o Nome do Cart�rio" ValidationGroup="ValidationGroupCartaoSUS"
                        OnServerValidate="CustomValidatorCartorio_ServerValidate"></asp:CustomValidator></span>
                <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Existem Caracteres inv�lidos no Nome do Cart�rio"
                            ControlToValidate="tbxNomeCartorio" ValidationExpression="^[a-zA-Z0-9\s]{1,40}$"
                            Font-Size="XX-Small"></asp:RegularExpressionValidator>--%>
            </p>
            <p>
                <span class="rotulo">Certid�o do Modelo Novo</span> <span>
                    <asp:TextBox ID="tbxNovaCertidao" CssClass="campo" runat="server" AutoPostBack="true"
                        OnTextChanged="tbxNovaCertidao_TextChanged" CausesValidation="true" Width="300px"
                        TabIndex="25"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtenderNovaCertidao" runat="server" TargetControlID="tbxNovaCertidao"
                        Mask="999999,99,99,9999,9,99999,999,9999999-99" MaskType="None" InputDirection="LeftToRight"
                        ClearMaskOnLostFocus="false">
                    </cc1:MaskedEditExtender>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanelDocumentos_Parte1" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="tbxNovaCertidao" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Livro</span> <span>
                            <asp:TextBox ID="tbxLivro" CssClass="campo" runat="server" MaxLength="8" Width="70px"
                                TabIndex="26"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidatorLivro" runat="server" Display="Dynamic" Font-Size="XX-Small"
                                ErrorMessage="Preencha o Campo Livro" ValidationGroup="ValidationGroupCartaoSUS"
                                OnServerValidate="CustomValidatorLivro_ServerValidate"></asp:CustomValidator></span>
                        <span class="rotulomin">Folhas</span> <span>
                            <asp:TextBox ID="tbxFolhas" CssClass="campo" runat="server" MaxLength="4" Width="70px"
                                TabIndex="27"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidatorFolhas" runat="server" Display="Dynamic"
                                ValidationGroup="ValidationGroupCartaoSUS" Font-Size="XX-Small" ErrorMessage="Preencha o Campo Folhas"
                                OnServerValidate="CustomValidatorFolhas_ServerValidate"></asp:CustomValidator></span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <asp:UpdatePanel ID="UpdatePanelDocumentos_Parte2" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="false" RenderMode="Inline">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSalvar" />
                        <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                        <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                        <asp:AsyncPostBackTrigger ControlID="tbxNovaCertidao" EventName="TextChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <%--<div style="position:relative; width:300px; float:left">--%>
                        <span class="rotulo">Termo</span> <span>
                            <asp:TextBox ID="tbxTermo" CssClass="campo" runat="server" MaxLength="8" Width="70px"
                                TabIndex="28"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidatorTermo" runat="server" Display="Dynamic" ErrorMessage="Preencha a Campo Termo"
                                OnServerValidate="CustomValidatorTermo_ServerValidate" ValidationGroup="ValidationGroupCartaoSUS"
                                Font-Size="XX-Small"></asp:CustomValidator>
                        </span>
                        <%--</div>--%>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div style="position: relative; width: 300px; float: left">
                    <span class="rotulomin">Data da Emiss�o</span> <span>
                        <asp:TextBox ID="tbxDataEmissaoCertidao" CssClass="campo" runat="server" Width="80px"
                            TabIndex="29"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="tbxDataEmissaoCertidao"
                            OnClientShown="calendarShown" />
                        <cc1:MaskedEditExtender ID="MaskedEditEmissaoCertidao" runat="server" TargetControlID="tbxDataEmissaoCertidao"
                            Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                        </cc1:MaskedEditExtender>
                        <asp:CompareValidator ID="CompareValidatorEmissaoCertidao" runat="server" ControlToValidate="tbxDataEmissaoCertidao"
                            Display="Dynamic" ErrorMessage="Data da Emiss�o da Certid�o inv�lida" Operator="DataTypeCheck"
                            Type="Date" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                        <asp:CustomValidator ID="CustomValidatorDataCertidao" runat="server" Display="Dynamic"
                            ErrorMessage="Voc� n�o preencheu a Data da Certid�o ou a Data � inv�lida" OnServerValidate="CustomValidatorDataCertidao_ServerValidate"
                            Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator></span>
                </div>
                <div style="clear: left;">
                </div>
            </p>
            <p>
                <span class="rotulo">CTPS</span> <span>
                    <asp:TextBox ID="tbxCTPS" CssClass="campo" runat="server" MaxLength="10" TabIndex="30"></asp:TextBox></span>
                <span class="rotulomin">S�rie</span> <span>
                    <asp:TextBox ID="tbxSerieCTPS" CssClass="campo" runat="server" MaxLength="5" TabIndex="31"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidatorSerieCTPS" runat="server" Display="Dynamic"
                        ErrorMessage="Preencha a S�ria da CTPS" OnServerValidate="CustomValidatorSerieCTPS_ServerValidate"
                        Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">UF</span> <span>
                    <asp:DropDownList ID="ddlUFCTPS" CssClass="campo" Height="21px" runat="server" Width="100px"
                        DataTextField="Sigla" DataValueField="Sigla" TabIndex="32">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <asp:CustomValidator ID="CustomValidatorUFCTPS" runat="server" Display="Dynamic"
                    ErrorMessage="Selecione a UF da CTPS" OnServerValidate="CustomValidatorUFCTPS_ServerValidate"
                    Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator></p>
            <p>
                <span class="rotulo">Data de Emiss�o</span> <span>
                    <asp:TextBox ID="tbxDataEmissaoCTPS" CssClass="campo" runat="server" TabIndex="33"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="tbxDataEmissaoCTPS"
                        OnClientShown="calendarShown" />
                    <cc1:MaskedEditExtender ID="MaskedEditEmissaoCTPS" runat="server" TargetControlID="tbxDataEmissaoCTPS"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                    </cc1:MaskedEditExtender>
                    <asp:CompareValidator ID="CompareValidatorEmissaoCTPS" runat="server" ControlToValidate="tbxDataEmissaoCTPS"
                        Display="Dynamic" ErrorMessage="A data da Emiss�o da CTPS n�o � v�lida" Operator="DataTypeCheck"
                        Type="Date" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                    <asp:CustomValidator ID="CustomValidatorDataCTPS" runat="server" Display="Dynamic"
                        ErrorMessage="Preencha a Data de Emiss�o da CTPS" OnServerValidate="CustomValidatorDataCTPS_ServerValidate"
                        Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">T�tulo de Eleitor</span> <span>
                    <asp:TextBox ID="tbxTituloEleitor" CssClass="campo" runat="server" MaxLength="15"
                        TabIndex="34"></asp:TextBox></span> <span class="rotulomin">Zona</span>
                <span>
                    <asp:TextBox ID="tbxZonaEleitoral" CssClass="campo" runat="server" MaxLength="4"
                        TabIndex="35"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidatorZonaEleitoral" runat="server" Display="Dynamic"
                        ErrorMessage="Preencha a Zona Eleitoral" OnServerValidate="CustomValidatorZonaEleitoral_ServerValidate"
                        Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Se��o</span> <span>
                    <asp:TextBox ID="tbxSecaoEleitoral" CssClass="campo" runat="server" MaxLength="4"
                        TabIndex="36"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidatorSecaoEleitoral" runat="server" Display="Dynamic"
                        ErrorMessage="Preencha a Se��o Eleitoral" OnServerValidate="CustomValidatorSecaoEleitoral_ServerValidate"
                        Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Dados Residenciais</legend>
            <p>
                <span class="rotulo">CEP*</span> <span>
                    <asp:TextBox ID="tbxCEP" CssClass="campo" runat="server" MaxLength="8" TabIndex="37"></asp:TextBox></span>
                <asp:ImageButton ID="imgBuscarCEP" runat="server" ImageUrl="~/Paciente/img/bts/buscarcep.gif"
                    OnClick="imgBuscarCEP_Click" CausesValidation="false" Width="71px" Height="20px"
                    Style="position: absolute;" />
                <span>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="O CEP 40000000 n�o � v�lido"
                        ControlToValidate="tbxCEP" ValueToCompare="40000000" Operator="NotEqual" Font-Size="XX-Small"
                        Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxCEP"
                        Display="Dynamic" ErrorMessage="Preencha o CEP" ValidationGroup="ValidationGroupCartaoSUS"
                        Font-Size="XX-Small" Style="padding-left: 120px;"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="tbxCEP"
                        ErrorMessage="O CEP deve conter 8 d�gitos" ValidationGroup="ValidationGroupCartaoSUS"
                        Font-Size="XX-Small" Display="Dynamic" ValidationExpression="^\d{8}$"></asp:RegularExpressionValidator>
                    <%--<asp:CompareValidator ID="CompareValidatorCEP" runat="server" ControlToValidate="tbxCEP"
                        Font-Size="XX-Small" Display="Dynamic" ErrorMessage="O CEP deve conter apeanas N�meros"
                        Operator="DataTypeCheck" Type="Integer" ValidationGroup="ValidationGroupCartaoSUS"></asp:CompareValidator>--%>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel_Endereco" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="imgBuscarCEP" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Munic�pio de Resid�ncia*</span> <span>
                            <asp:DropDownList ID="ddlMunicipios" CssClass="campo" Height="21px" runat="server"
                                Width="300px" DataTextField="NomeSemUF" DataValueField="Codigo" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlMunicipios_SelectedIndexChanged" TabIndex="38">
                            </asp:DropDownList>
                        </span>
                        <asp:Label ID="lblMunicipioResidencia" runat="server" Visible="false"></asp:Label>
                    </p>
                    <p>
                        <span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlMunicipios"
                                Display="Dynamic" ErrorMessage="Selecione o Munic�pio de Resid�ncia" Font-Size="XX-Small"
                                InitialValue="-1" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_EnderecoParte2" runat="server" RenderMode="Inline"
                ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="imgBuscarCEP" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipios" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Tipo de Logradouro*</span> <span>
                            <asp:DropDownList ID="ddlTipoLogradouro" CssClass="campo" Height="21px" runat="server"
                                Width="200px" DataValueField="Codigo" DataTextField="Descricao" TabIndex="39">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipoLogradouro" runat="server"
                                ControlToValidate="ddlTipoLogradouro" Display="Dynamic" ErrorMessage="Selecione o Tipo de Logradouro"
                                InitialValue="-1" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nome do Logradouro*</span> <span>
                            <asp:TextBox ID="tbxNomeLogradouro" CssClass="campo" runat="server" Width="300px"
                                MaxLength="50" TabIndex="40"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton_DuViverMaisEndereco" runat="server" ToolTip="D�ViverMais sobre como preencher o Endere�o?"
                                Width="16px" Height="18px" OnClientClick="return false;" ImageUrl="~/Paciente/img/help.png" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxNomeLogradouro"
                                Display="Dynamic" ValidationGroup="ValidationGroupCartaoSUS" ErrorMessage="Preencha o Nome do Logradouro"
                                Font-Size="XX-Small"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="Dynamic"
                                ErrorMessage="Existem Caracteres inv�lidos no Nome do Logradouro" ControlToValidate="tbxNomeLogradouro"
                                ValidationExpression="^[a-zA-Z0-9\s]{1,40}$" Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
                        </span>
                    </p>
                    <div id="flyoutEndereco" class="wireFrame">
                    </div>
                    <div id="infoEndereco" style="display: none; width: 250px; z-index: 2; opacity: 0;
                        filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px;
                        border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                        <div id="Div3" style="float: left; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                            <asp:LinkButton ID="LinkButton_FecharDuViverMaisEndereco" runat="server" OnClientClick="return false;"
                                Text="X" ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                        </div>
                        Se o endere�o informado �: Rua das Margaridas, 13A, bloco C, Itapoan
                        <br />
                        <strong>Tipo de Logradouro:</strong> Rua
                        <br />
                        <strong>Nome do Logradouro:</strong> das Margaridas<br />
                        <strong>N�mero:</strong> 13<br />
                        <strong>Complemento:</strong> A, Bloco C<br />
                        <strong>Bairro:</strong> Itapoan
                    </div>
                    <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="ImageButton_DuViverMaisEndereco">
                        <Animations>
                                            <OnClick>
                                               <Sequence>
                                               <EnableAction Enabled="false" />
                                               
                                               <ScriptAction Script="Cover($get('ImageButton_DuViverMaisEndereco'), $get('flyoutEndereco'));" />
                                               <StyleAction AnimationTarget="flyoutEndereco" Attribute="display" Value="block"/>
                                               <ScriptAction Script="Cover($get('flyoutEndereco'), $get('infoEndereco'), true);" />
                                               <StyleAction AnimationTarget="infoEndereco" Attribute="display" Value="block"/>
                                               <FadeIn AnimationTarget="infoEndereco" Duration=".2"/>
                                               <StyleAction AnimationTarget="flyoutEndereco" Attribute="display" Value="none"/>
                                               </Sequence>
                                            </OnClick>
                        </Animations>
                    </cc1:AnimationExtender>
                    <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="LinkButton_FecharDuViverMaisEndereco">
                        <Animations>
                                            <OnClick>
                                               <Sequence AnimationTarget="infoEndereco">
                                               
                                               <StyleAction Attribute="overflow" Value="hidden"/>
                                               <Parallel Duration=".3" Fps="15">
                                                  <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                  <FadeOut />
                                               </Parallel>
                                               
                                               <StyleAction Attribute="display" Value="none"/>
                                               <StyleAction Attribute="width" Value="250px"/>
                                               <StyleAction Attribute="height" Value=""/>
                                               <StyleAction Attribute="fontSize" Value="12px"/>
                                               
                                               <EnableAction AnimationTarget="ImageButton_DuViverMaisEndereco" Enabled="true" />
                                               </Sequence>
                                            </OnClick>
                        </Animations>
                    </cc1:AnimationExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelNumeroEndereco" runat="server" RenderMode="Inline" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                    <p>
                        <span class="rotulo">N�mero*</span> <span>
                            <asp:TextBox ID="tbxNumero" CssClass="campo" runat="server" Width="40px" MaxLength="7"
                                TabIndex="41"></asp:TextBox>
                            <asp:CheckBox ID="chkNumero" Text="S/N" runat="server" Checked="false" AutoPostBack="true"
                                OnCheckedChanged="chkNumero_CheckedChanged" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNumero"
                                Display="Dynamic" ErrorMessage="Preencha o campo N�mero" Font-Size="XX-Small"
                                ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                                Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS" Display="Dynamic"
                                ControlToValidate="tbxNumero" ErrorMessage="Preencha o N�mero do Endere�o com S/N ou Informe um N�mero"
                                ValidationExpression="^([Ss]\/[Nn])|(\d*)$"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="tbxNumero"
                                ValidationGroup="ValidationGroupCartaoSUS" Display="Dynamic" ErrorMessage="O campo N�mero do Endere�o n�o pode ser Zero. Utilize S/N."
                                Font-Size="XX-Small" InitialValue="0"></asp:RequiredFieldValidator>
                        </span>
                    </p>
            </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_EnderecoParte2Continuacao" runat="server" RenderMode="Inline"
                ChildrenAsTriggers="false" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                    <asp:PostBackTrigger ControlID="lnkCartaoSus" />
                    <asp:PostBackTrigger ControlID="lnkEtiqueta" />
                    <asp:AsyncPostBackTrigger ControlID="imgBuscarCEP" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipios" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Complemento</span> <span>
                            <asp:TextBox ID="tbxComplemento" CssClass="campo" runat="server" MaxLength="15" TabIndex="42"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Existem Caracteres inv�lidos no Nome do Complemento do Logradouro"
                                ControlToValidate="tbxComplemento" ValidationExpression="^[a-zA-Z0-9\s]{1,40}$"
                                Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:RegularExpressionValidator>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Bairro*</span> <span>
                            <asp:DropDownList ID="ddlBairro" CssClass="campo" Height="21px" runat="server" Width="250px"
                                DataTextField="Nome" DataValueField="Codigo" TabIndex="43">
                            </asp:DropDownList>
                        </span>
                        <asp:Label ID="lblBairro" runat="server" Visible="false"></asp:Label>
                    </p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBairro"
                        Display="Dynamic" ErrorMessage="Selecione o Bairro de Resid�ncia" InitialValue="-1"
                        Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:RequiredFieldValidator>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Telefone</span> <span>
                    <asp:TextBox ID="tbxDDD" CssClass="campo" runat="server" MaxLength="2" Width="25px"></asp:TextBox>
                    -
                    <asp:TextBox ID="tbxTelefone" CssClass="campo" runat="server" MaxLength="8" Width="70px"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator_Telefone" runat="server" ErrorMessage="Preencha o DDD com 2 d�gitos e o N�mero do Telefone com 8 d�gitos."
                        Display="Dynamic" Font-Size="XX-Small" OnServerValidate="OnServerValidate_Telefone"
                        ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
                </span>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="true"
                    Mask="99" MaskType="Number" AutoComplete="false" InputDirection="LeftToRight"
                    TargetControlID="tbxDDD">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" ClearMaskOnLostFocus="true"
                    Mask="99999999" MaskType="Number" AutoComplete="false" InputDirection="LeftToRight"
                    TargetControlID="tbxTelefone">
                </cc1:MaskedEditExtender>
            </p>
            <asp:CustomValidator ID="CustomValidatorIdentidadeCertidao" runat="server" Display="Dynamic"
                ErrorMessage="Preencha a Identidade ou a Certid�o de Nascimento" OnServerValidate="CustomValidatorIdentidadeCertidao_ServerValidate"
                Font-Size="XX-Small" ValidationGroup="ValidationGroupCartaoSUS"></asp:CustomValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroupCartaoSUS" />
            <p style="text-align: left;">
                <asp:ImageButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" ValidationGroup="ValidationGroupCartaoSUS"
                    ImageUrl="~/Paciente/img/bts/btn_salvar1.png" Height="38px" Width="134px" />
                &nbsp;
                <asp:ImageButton ID="btnVoltar" runat="server" PostBackUrl="~/Paciente/Default.aspx"
                    CausesValidation="False" ImageUrl="~/Paciente/img/bts/voltar_1.png" Height="38px"
                    Width="134px" /></p>
            <asp:UpdatePanel ID="UpdatePanel_CamposEscondidos" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSalvar" />
                </Triggers>
                <ContentTemplate>
                    <%--                    <p>--%>
                    <asp:HiddenField ID="HiddenCodigoPaciente" runat="server" />
                    <asp:HiddenField ID="HiddenNomePaciente" runat="server" />
                    <asp:HiddenField ID="HiddenDataNascimento" runat="server" />
                    <asp:HiddenField ID="HiddenMunicipio" runat="server" />
                    <asp:HiddenField ID="HiddenNumeroCartao" runat="server" />
                    <%--                    </p>--%>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="botoesroll">
                <asp:LinkButton ID="lnkCartaoSus" CssClass="sep_buttons" OnClick="lnkCartaoSus_Click"
                    runat="server" ValidationGroup="ValidationGroupCartaoSUS">
                <img id="img_newcardsus" alt="" src="../img/bts/id_imprimecartaosus.png" 
                onmouseover="img_newcardsus.src='../img/bts/id_imprimecartaosus2.png';" 
                onmouseout="img_newcardsus.src='../img/bts/id_imprimecartaosus.png';"  />
                </asp:LinkButton></div>
            <!-- Impress�o em PDF -->
            <div class="botoesroll">
                <asp:LinkButton ID="lnkEtiqueta" runat="server" CssClass="sep_buttons" OnClick="lnkEtiqueta_Click"
                    ValidationGroup="ValidationGroupCartaoSUS">
                <img id="img_etiquetapdf" alt="Impress�o de Etiqueta em PDF" src="../img/bts/btn_print_pdf1.png" 
                onmouseover="img_etiquetapdf.src='../img/bts/btn_print_pdf2.png';" 
                onmouseout="img_etiquetapdf.src='../img/bts/btn_print_pdf1.png';"  />
                </asp:LinkButton></div>
            <!-- Impress�o em PDF -->
            <!-- Impress�o em HTML -->
            <!--div class="botoesroll">
                        <asp:LinkButton ID="lnkBtnEtiquetaHTML" runat="server" CssClass="sep_buttons" CausesValidation="False"
                            OnClick="lnkBtnEtiquetaHTML_Click">
                <img id="img_etiquetahtml" alt="Impress�o de Etiqueta em HTML" src="../img/bts/btn_print_html1.png" 
                onmouseover="img_etiquetahtml.src='../img/bts/btn_print_html2.png';" 
                onmouseout="img_etiquetahtml.src='../img/bts/btn_print_html1.png';"  />
                        </asp:LinkButton></div-->
            <!--Impress�o em HTML-->
            <div class="botoesroll">
                <asp:LinkButton ID="btnBiometria" CausesValidation="false" runat="server" OnClick="btnBiometria_Click">
                <img id="img_newbiometria" alt="" src="../img/bts/id_biometrica.png" 
                onmouseover="img_newbiometria.src='../img/bts/id_biometrica2.png';" 
                onmouseout="img_newbiometria.src='../img/bts/id_biometrica.png';"  />
                </asp:LinkButton>
            </div>
            <p>
                &nbsp;</p>
        </fieldset>
        <p>
            &nbsp;</p>
    </div>
    <%--    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="O CEP 40000000 n�o � v�lido"
        ControlToValidate="tbxCEP" ValueToCompare="40000000" Operator="NotEqual" Display="None"
        ValidationGroup="ValidationGroup_BuscarCEP"></asp:CompareValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="tbxCEP"
        Display="None" ErrorMessage="Preencha o CEP" ValidationGroup="ValidationGroup_BuscarCEP"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="tbxCEP"
        Display="None" ErrorMessage="O CEP deve conter apeanas N�meros" Operator="DataTypeCheck"
        Type="Integer" ValidationGroup="ValidationGroup_BuscarCEP"></asp:CompareValidator>
    <asp:ValidationSummary ID="ValidationSummary_BuscaCEP" runat="server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarCEP"
         />--%>
</asp:Content>