﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAcolhimento.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormAcolhimento" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formulário de Atendimento Inicial</h2>
        <fieldset class="formulario">
            <legend>Dados do Atendimento</legend>
            <p>
                <span style="color: Red; font-family: Verdana; font-size: xx-small; float: right;">*
                    campos obrigatórios</span>
            </p>
            <p>
                &nbsp</p>
            <p>
                <span class="rotulo">Nº:</span> <span>
                    <asp:Label ID="lblNumero" runat="server" Font-Bold="True" ForeColor="Maroon" Font-Size="Smaller"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data:</span> <span>
                    <asp:Label ID="lblData" runat="server" Font-Bold="True" ForeColor="Maroon" Font-Size="Smaller"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Paciente:</span> <span>
                    <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" ForeColor="Maroon" Font-Size="Smaller"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                    Tensão Arterial:</span> <span>
                        <asp:TextBox ID="tbxTensaoArterialInicio" runat="server" CssClass="campo" Height="16px"
                            MaxLength="3" Width="60px"></asp:TextBox>
                        <asp:Label ID="lbQuantificadorTensaoArterial" runat="server" Text="X"></asp:Label>
                        <asp:TextBox ID="tbxTensaoArterialFim" runat="server" CssClass="campo" Height="16px"
                            MaxLength="3" Width="60px"></asp:TextBox>
                        mmHg
                        <asp:ImageButton ID="Button_DuViverMaisPadroes" runat="server" Width="16px" Height="18px"
                            OnClientClick="return false;" ImageUrl="~/Urgencia/img/help.png" Style="position: absolute;
                            margin-left: 20px;" />
                    </span>
            </p>
            <div id="Div_Flout">
            </div>
            <div id="Div_InfoPadroes" style="display: none; width: 250px; z-index: 2; opacity: 0;
                filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0); font-size: 12px;
                border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                <div id="Div2" style="float: left; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                    <asp:LinkButton ID="btnClosePadroes" runat="server" OnClientClick="return false;"
                        Text="X" ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                        font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                </div>
                Caso a tensão arterial, freqüência cardíaca, freqüência respiratória, temperatura,
                htg ou peso contenha parte fracionária utilize a vírgula para indicá-la. Exemplo:
                100,30 ou 100,3.
            </div>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                    Freqüência Cardíaca:</span> <span>
                        <asp:TextBox ID="tbxFreqCardiaca" CssClass="campo" runat="server" MaxLength="3" Width="60px"
                            Height="16px"></asp:TextBox>
                        bpm </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label3" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                    Freqüência Respiratória:</span> <span>
                        <asp:TextBox ID="tbxFreqRespitatoria" CssClass="campo" runat="server" MaxLength="3"
                            Width="60px" Height="16px"></asp:TextBox>
                        i.m. </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                    Temperatura: </span><span>
                        <asp:TextBox ID="tbxTemperatura" CssClass="campo" runat="server" MaxLength="3" Width="60px"
                            Height="16px" AutoPostBack="true" OnTextChanged="OnTextChanged_Temperatua">
                        </asp:TextBox>
                        ºC </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>HGT:
                </span><span>
                    <asp:TextBox ID="tbxHgt" runat="server" CssClass="campo" Height="16px" MaxLength="3"
                        Width="60px"></asp:TextBox>
                    m/mol </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="Label9" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>Peso</span>
                <span>
                    <asp:TextBox ID="TextBox_Peso" runat="server" CssClass="campo" Width="60px" MaxLength="5"></asp:TextBox>Kg</span>
            </p>
            <p class="camporadio">
                <asp:CheckBox ID="ckbAcidente" runat="server" CssClass="camporadio" Width="70px"
                    Text="Acidente" />
                <asp:CheckBox ID="ckbDorIntensa" runat="server" CssClass="camporadio" Width="100px"
                    Text="Dor Intensa" />
                <asp:CheckBox ID="ckbFratura" runat="server" CssClass="camporadio" Width="70px" Text="Fratura" />
                <asp:CheckBox ID="ckbConvulsao" runat="server" CssClass="camporadio" Width="100px"
                    Text="Convulsão" />
                <asp:CheckBox ID="CheckBoxDorToraxica" runat="server" CssClass="camporadio" Width="100px"
                    Text="Dor Toráxica" />
                <asp:CheckBox ID="ckbAlergia" runat="server" CssClass="camporadio" Width="70px" Text="Alergia" />
            </p>
            <%--<br />--%>
            <p class="camporadio">
                <asp:CheckBox ID="ckbAsma" runat="server" CssClass="camporadio" Width="70px" Text="Asma" />
                <asp:CheckBox ID="ckbDiarreia" runat="server" CssClass="camporadio" Width="70px"
                    Text="Diarréia" />
                <asp:CheckBox ID="CheckBoxSaturacaoOxigenio" runat="server" CssClass="camporadio"
                    Width="130px" Text="Saturação de Oxigênio" />
            </p>
            <asp:UpdatePanel ID="UpdatePanel_Acolhimento" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tbxTemperatura" EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Doença Crônica</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_DoencaCronica" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_DoencaCronica" CssClass="camporadio"
                                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Ausente" Value="Ausente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Presente" Value="Presente"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--<asp:RadioButton ID="RadioButton_DoencaCronicaAusente" CssClass="camporadio" Checked="true"
                                CausesValidation="true" runat="server" GroupName="GroupName_DoencaCronica" Width="70px"
                                OnCheckedChanged="OnCheckedChanged_DoencaCronica" AutoPostBack="true" Text="Ausente" />
                            <asp:RadioButton ID="RadioButton_DoencaCronicaPresente" CssClass="camporadio" runat="server"
                                GroupName="GroupName_DoencaCronica" CausesValidation="true" Width="70px" OnCheckedChanged="OnCheckedChanged_DoencaCronica"
                                AutoPostBack="true" Text="Presente" />--%></span>
                    </p>
                    <p>
                        <span class="rotulo">Queixa Atual</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Queixa" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_QueixaAtual" CssClass="camporadio"
                                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Não caracteriza urgência" Value="SemRisco" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Potencial risco urgência" Value="ComRisco"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_QueixaAtualNaoUrgencia" Checked="true" CssClass="camporadio"
                                runat="server" GroupName="GroupName_QueixaAtual" Width="160px" OnCheckedChanged="OnCheckedChanged_QueixaAtual"
                                AutoPostBack="true" Text="Não caracteriza urgência" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_QueixaAtualUrgencia" runat="server" CssClass="camporadio"
                                GroupName="GroupName_QueixaAtual" Width="150px" OnCheckedChanged="OnCheckedChanged_QueixaAtual"
                                AutoPostBack="true" CausesValidation="true" Text="Potencial risco urgência" />--%></span>
                    </p>
                    <p>
                        <span class="rotulo">Internação anterior</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_InternacaoAnterior" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Internacao" CssClass="camporadio"
                                RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Não" Value="Nao" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Sim. Pronto Socorro até um mês." Value="ProntoSocorro"></asp:ListItem>
                                <asp:ListItem Text="Sim. UTI até 1 ano." Value="UTI"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_InternacaoNão" Checked="true" CssClass="camporadio"
                                runat="server" GroupName="GroupName_Internacao" Width="70px" OnCheckedChanged="OnCheckedChanged_Internacao"
                                AutoPostBack="true" Text="Não" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_InternacaoProntoSocorro" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Internacao" Width="180px" OnCheckedChanged="OnCheckedChanged_Internacao"
                                AutoPostBack="true" Text="SIM. Pronto socorro até 1 mês" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_InternacaoUTI" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Internacao" Width="150px" OnCheckedChanged="OnCheckedChanged_Internacao"
                                AutoPostBack="true" Text="SIM. UTI até 1 ano" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Febre</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Febre" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Febre" CssClass="camporadio" RepeatColumns="3"
                                RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Ausente" Value="Ausente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="< 39°" Value="Menor39"></asp:ListItem>
                                <asp:ListItem Text=">= 39°" Value="IgualMaior39"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_FebreAusente" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Febre" Width="70px" OnCheckedChanged="OnCheckedChanged_Febre"
                                AutoPostBack="true" Text="Ausente" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_FebreMenor" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Febre" Width="70px" OnCheckedChanged="OnCheckedChanged_Febre"
                                AutoPostBack="true" Text="< 39º" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_FebreMaior" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Febre" Width="70px" OnCheckedChanged="OnCheckedChanged_Febre"
                                AutoPostBack="true" Text=">= 39º" CausesValidation="true" />--%>
                            <asp:CustomValidator ID="CustomValidatorFebre" runat="server" ErrorMessage="Confira o valor indicado para a temperatura de acordo com o campo Febre."
                                Display="Dynamic" ValidationGroup="group_acolhimento" Text="Confira o valor indicado para a temperatura de acordo com o campo Febre."
                                OnServerValidate="OnServerValidate_Febre"></asp:CustomValidator>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nível de consciência</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Consciencia" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Consciencia" CssClass="camporadio"
                                RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Consciente" Value="Consciente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Agitado ou irritado" Value="Agitado"></asp:ListItem>
                                <asp:ListItem Text="Sonolento ou torporoso" Value="Sonolento"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_NivelConsciencia" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Consciencia" Width="100px" OnCheckedChanged="OnCheckedChanged_Consciencia"
                                AutoPostBack="true" Text="Consciente" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_NivelConscienciaAgitado" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Consciencia" Width="150px" OnCheckedChanged="OnCheckedChanged_Consciencia"
                                AutoPostBack="true" Text="Agitado ou irritado" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_NivelConscienciaSonolento" CssClass="camporadio"
                                runat="server" GroupName="GroupName_Consciencia" Width="150px" OnCheckedChanged="OnCheckedChanged_Consciencia"
                                AutoPostBack="true" Text="Sonolento ou Torporoso" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Cor da pele</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_CorPelo" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Pele" CssClass="camporadio" RepeatColumns="3"
                                RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Corado" Value="Corado" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Pálida" Value="Palido"></asp:ListItem>
                                <asp:ListItem Text="Cianótico" Value="Cianotico"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_PeleCorada" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Pele" Width="20px" OnCheckedChanged="OnCheckedChanged_Pele"
                                AutoPostBack="true" CausesValidation="true" />Corado
                            <asp:RadioButton ID="RadioButton_PelePalida" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Pele" Width="20px" OnCheckedChanged="OnCheckedChanged_Pele"
                                AutoPostBack="true" CausesValidation="true" />Pálida
                            <asp:RadioButton ID="RadioButton_PeleCianotico" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Pele" Width="20px" OnCheckedChanged="OnCheckedChanged_Pele"
                                AutoPostBack="true" CausesValidation="true" />Cianótico </span>--%>
                    </p>
                    <p>
                        <span class="rotulo">Hidratação</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Hidatracao" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Hidratacao" CssClass="camporadio"
                                RepeatColumns="3" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Hidratado" Value="Hidratado" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Algum grau de desidratação" Value="AlgumaDesidratacao"></asp:ListItem>
                                <asp:ListItem Text="Desidratado grave" Value="DesidratacaoGrave"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_HidratacaoHidratado" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Hidratacao" Width="120px" OnCheckedChanged="OnCheckedChanged_Hidratacao"
                                AutoPostBack="true" Text="Hidratado" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_HidratacaoGrauDesidratacao" CssClass="camporadio"
                                runat="server" GroupName="GroupName_Hidratacao" Width="180px" OnCheckedChanged="OnCheckedChanged_Hidratacao"
                                AutoPostBack="true" Text="Algum grau de desidratação" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_HidratacaoDesitrado" runat="server" GroupName="GroupName_Hidratacao"
                                Width="150px" OnCheckedChanged="OnCheckedChanged_Hidratacao" AutoPostBack="true"
                                Text="Desidratado grave" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Hemorragia</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Hemorragia" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Hemorragia" CssClass="camporadio"
                                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table">
                                <asp:ListItem Text="Ausente" Value="Ausente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Presente" Value="Presente"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_HemorragiaAusente" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Hemorragia" Width="100px" OnCheckedChanged="OnCheckedChanged_Hemorragia"
                                AutoPostBack="true" Text="Ausente" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_HemorragiaPresente" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Hemorragia" Width="100px" OnCheckedChanged="OnCheckedChanged_Hemorragia"
                                AutoPostBack="true" Text="Presente" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Freqüência respiratória</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_FrequenciaRespiratoria" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Frequencia" RepeatColumns="3"
                                RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="camporadio">
                                <asp:ListItem Text="Normal" Value="Normal" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Aumentada" Value="Aumentada"></asp:ListItem>
                                <asp:ListItem Text="Diminuída" Value="Diminuida"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_FrequenciaNormal" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Frequencia" Width="100px" OnCheckedChanged="OnCheckedChanged_Frequencia"
                                AutoPostBack="true" Text="Normal" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_FrequenciaAumentada" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Frequencia" Width="100px" OnCheckedChanged="OnCheckedChanged_Frequencia"
                                AutoPostBack="true" Text="Aumentada" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_FrequenciaDiminuida" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Frequencia" Width="100px" OnCheckedChanged="OnCheckedChanged_Frequencia"
                                AutoPostBack="true" Text="Diminuída" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Desconforto respiratório</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_DesconfortoRespiratorio" runat="server"
                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Frequencia"
                                RepeatColumns="2" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="camporadio">
                                <asp:ListItem Text="Ausente" Value="Ausente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Presente" Value="Presente"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_DesconfortoAusente" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Desconforto" Width="100px" OnCheckedChanged="OnCheckedChanged_Desconforto"
                                AutoPostBack="true" Text="Ausente" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_DesconfortoPresente" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Desconforto" Width="120px" OnCheckedChanged="OnCheckedChanged_Desconforto"
                                AutoPostBack="true" Text="Presente" CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Dor</span> <span class="camporadio">
                            <asp:RadioButtonList ID="RadioButtonList_Dor" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_Dor" RepeatColumns="4" RepeatDirection="Horizontal"
                                RepeatLayout="Table" CssClass="camporadio">
                                <asp:ListItem Text="Ausente" Value="Ausente" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Leve" Value="Leve"></asp:ListItem>
                                <asp:ListItem Text="Moderada" Value="Moderada"></asp:ListItem>
                                <asp:ListItem Text="Grave" Value="Grave"></asp:ListItem>
                            </asp:RadioButtonList>
                            <%--                            <asp:RadioButton ID="RadioButton_DorAusente" CssClass="camporadio" Checked="true"
                                runat="server" GroupName="GroupName_Dor" Width="100px" OnCheckedChanged="OnCheckedChanged_Dor"
                                AutoPostBack="true" Text="Ausente" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_DorLeve" CssClass="camporadio" runat="server" GroupName="GroupName_Dor"
                                Width="100px" OnCheckedChanged="OnCheckedChanged_Dor" AutoPostBack="true" Text="Leve"
                                CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_DorModerada" CssClass="camporadio" runat="server"
                                GroupName="GroupName_Dor" Width="100px" OnCheckedChanged="OnCheckedChanged_Dor"
                                AutoPostBack="true" Text="Moderada" CausesValidation="true" />
                            <asp:RadioButton ID="RadioButton_DorGrave" CssClass="camporadio" runat="server" GroupName="GroupName_Dor"
                                Width="100px" OnCheckedChanged="OnCheckedChanged_Dor" AutoPostBack="true" Text="Grave"
                                CausesValidation="true" />--%>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Queixa</span> <span>
                            <asp:TextBox ID="TextBox_Queixa" runat="server" Width="620px" Height="120px" TextMode="MultiLine"
                                Rows="20" Columns="5" MaxLength="1000" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">
                            <asp:Label ID="Label6" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                            Situação:</span> <span>
                                <asp:DropDownList ID="ddlSituacao" runat="server" Width="265px" CausesValidation="true"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_Situacao" CssClass="drop" AutoPostBack="true"
                                    Height="23px">
                                </asp:DropDownList>
                            </span>
                    </p>
                    <asp:Panel ID="PanelEspecialidade" runat="server">
                        <p>
                            <span class="rotulo">
                                <asp:Label ID="Label8" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                                Especialidade Atendimento: </span><span>
                                    <asp:DropDownList ID="DropDownListEspecialidade" runat="server" Width="265px" CssClass="drop"
                                        DataTextField="NomeEspecialidade" DataValueField="CodigoEspecialidade">
                                    </asp:DropDownList>
                                </span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="Panel_ClassificacaoRisco" runat="server">
                        <p>
                            <span class="rotulo">
                                <asp:Label ID="Label7" runat="server" Text="*" ForeColor="White" Font-Size="15px"></asp:Label>
                                Classificação de Risco: </span><span>
                                    <asp:DropDownList ID="ddlClassificacaoRisco" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ModificaClassificacao"
                                        runat="server" Width="265px" Height="23px" CssClass="drop" CausesValidation="true">
                                    </asp:DropDownList>
                                    &nbsp
                                    <asp:Image ID="Imagem_Classificacao" Width="16px" Height="18px" runat="server" ImageUrl="~/Urgencia/img/classificacao.png"
                                        Style="position: absolute; margin-top: 3px;" />
                                    &nbsp&nbsp
                                    <asp:ImageButton ID="Button_MsgClassificacao" runat="server" Width="16px" Height="18px"
                                        OnClientClick="return false;" ImageUrl="~/Urgencia/img/help.png" Style="position: absolute;
                                        margin-top: 3px; margin-left: 7px;" />
                                </span>
                            <div id="flyout">
                            </div>
                            <div id="info" style="display: none; width: 250px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                <div id="btnCloseParent" style="float: left; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                                    <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                        ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                        font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                </div>
                                Usuário, a classificação de risco apresentada é apenas "sugerida" pelo sistema baseada
                                em escore de graViverMaisde.
                                <br />
                                Fica a seu critério a mudança deste campo para ajuste à sua conveniência.
                            </div>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="Panel_BotaoSalvar" runat="server">
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" ValidationGroup="group_acolhimento"
                                OnClientClick="javascript:if (Page_ClientValidate('group_acolhimento')) return confirm('Tem certeza que deseja salvar os dados ?'); return false;">
                            <img id="imginiciarat" alt="Salvar" src="img/bts/btn_salvar1.png"
                onmouseover="this.src='img/bts/btn_salvar2.png';"
                onmouseout="this.src='img/bts/btn_salvar1.png';" />
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_BotaoReimprimirSenhaAtendimento" runat="server" Visible="false">
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_ReimprimirSenhaAtendimento" runat="server" OnClick="OnClick_ReimprimirSenhaAtendimento">
                            <img id="img1" alt="Reimpressão de Senha" src="img/bts/btn-reimprimir-senha1.png"
                onmouseover="this.src='img/bts/btn-reimprimir-senha2.png';"
                onmouseout="this.src='img/bts/btn-reimprimir-senha1.png';" />
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_BotaoFichaAcolhimento" runat="server" Visible="false">
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_ImprimirFichaAcolhimento" runat="server" OnClick="OnClick_ImprimirFichaAcolhimento">
                                <img id="img2" alt="Imprimir Ficha de Acolhimento" src="img/bts/btn-imprimir-ficha-acolh1.png"
                                    onmouseover="this.src='img/bts/btn-imprimir-ficha-acolh2.png';"
                                    onmouseout="this.src='img/bts/btn-imprimir-ficha-acolh1.png';" />
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Urgencia/ExibeFilaAcompanhamento.aspx">
                            <img id="imgVoltar" alt="Voltar" src="img/bts/btn-voltar1.png"
                                onmouseover="this.src='img/bts/btn-voltar2.png';" onmouseout="this.src='img/bts/btn-voltar1.png';" />
                        </asp:LinkButton>
                    </div>
                    <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TensaoMaxima" runat="server"
                            ErrorMessage="Máxima da Tensão Arterial é Obrigatório!" ControlToValidate="tbxTensaoArterialInicio"
                            ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                            ToolTip="Formato" ControlToValidate="tbxTensaoArterialInicio" ValidationGroup="group_acolhimento"
                            Display="None" ErrorMessage="Máxima da Tensão Arterial com formato inválido.">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_TensaoMinima" runat="server"
                            ErrorMessage="Mínima da Tensão Arterial é Obrigatório!" ControlToValidate="tbxTensaoArterialFim"
                            ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                            ToolTip="Formato" ControlToValidate="tbxTensaoArterialFim" ValidationGroup="group_acolhimento"
                            Display="None" ErrorMessage="Mínima da Tensão Arterial com formato inválido.">
                        </asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_FreqCard" runat="server" ErrorMessage="Freqüência Cardíaca é Obrigatório!"
                            ControlToValidate="tbxFreqCardiaca" ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Frequência Cardíaca com formato inválido!"
                            ControlToValidate="tbxFreqCardiaca" ValidationGroup="group_acolhimento" Display="None"
                            ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_FreqResp" runat="server" ErrorMessage="Freqüência Respiratória é Obrigatório!"
                            ControlToValidate="tbxFreqRespitatoria" ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Frequência Respiratória com formato inválido!"
                            ControlToValidate="tbxFreqRespitatoria" ValidationGroup="group_acolhimento" Display="None"
                            ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Temperatura" runat="server"
                            ErrorMessage="Temperatura é Obrigatório!" ControlToValidate="tbxTemperatura"
                            ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Temperatura com formato inválido!"
                            ControlToValidate="tbxTemperatura" ValidationGroup="group_acolhimento" Display="None"
                            ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_HGT" runat="server" ErrorMessage="HGT é Obrigatório!"
                            ControlToValidate="tbxHgt" ValidationGroup="group_acolhimento" Display="None"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Peso" runat="server" ErrorMessage="Peso é Obrigatório!"
                            ControlToValidate="TextBox_Peso" Display="None" ValidationGroup="group_acolhimento">
                        </asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator_Peso" runat="server"
                            ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$" ToolTip="Formato" ControlToValidate="TextBox_Peso"
                            ValidationGroup="group_acolhimento" Display="None" ErrorMessage="Peso com formato inválido.">
                        </asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator_Situacao" runat="server" ControlToValidate="ddlSituacao"
                            Display="None" ErrorMessage="Situação é Obrigatório!" Font-Size="XX-Small" Operator="GreaterThan"
                            ValidationGroup="group_acolhimento" ValueToCompare="-1"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidatorEspecialidadeAtendimento" runat="server"
                            ControlToValidate="DropDownListEspecialidade" Display="None" ErrorMessage="Especialidade de Atendimento é Obrigatório!"
                            Operator="NotEqual" ValidationGroup="group_acolhimento" ValueToCompare="-1"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator_Classificacao" runat="server" ControlToValidate="ddlClassificacaoRisco"
                            Display="None" ErrorMessage="Classificação de Risco é Obrigatório!" Font-Size="XX-Small" Type="Integer"
                            Operator="GreaterThan" ValidationGroup="group_acolhimento" ValueToCompare="-1"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="group_acolhimento" />
                        <cc1:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="Button_MsgClassificacao">
                            <Animations>
                                            <OnClick>
                                               <Sequence>
                                               <%-- Disable the button so it can't be clicked again --%>
                                               <EnableAction Enabled="false" />
                                               <%-- Position the wire frame and show it --%>
                                               <ScriptAction Script="Cover($get('Button_MsgClassificacao'), $get('flyout'));" />
                                               <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                                               <%-- Move the wire frame from the button's bounds to the info panel's bounds --%>
            <%--                                   <Parallel AnimationTarget="flyout" Duration=".3" Fps="25">
                                                   <Move Horizontal="150" Vertical="-50" />
                                                   <Resize Width="260" Height="280" />
                                                   <Color PropertyKey="backgroundColor" StartValue="#AAAAAA" EndValue="#FFFFFF" />
                                               </Parallel>--%>
                                               <%-- Move the  panel on top of the wire frame, fade it in, and hide the frame --%>
                                               <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                                               <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                                               <FadeIn AnimationTarget="info" Duration=".2"/>
                                               <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                                               </Sequence>
                                            </OnClick>
                            </Animations>
                        </cc1:AnimationExtender>
                        <cc1:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                            <Animations>
                                            <OnClick>
                                               <Sequence AnimationTarget="info">
                                               <%--  Shrink the panel out of view --%>
                                               <StyleAction Attribute="overflow" Value="hidden"/>
                                               <Parallel Duration=".3" Fps="15">
                                                  <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                  <FadeOut />
                                               </Parallel>
                                               <%--  Reset the target --%>
                                               <StyleAction Attribute="display" Value="none"/>
                                               <StyleAction Attribute="width" Value="250px"/>
                                               <StyleAction Attribute="height" Value=""/>
                                               <StyleAction Attribute="fontSize" Value="12px"/>
                                               <%--  Enable the button --%>
                                               <EnableAction AnimationTarget="Button_MsgClassificacao" Enabled="true" />
                                               </Sequence>
                                            </OnClick>
                            </Animations>
                        </cc1:AnimationExtender>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="Button_DuViverMaisPadroes">
                <Animations>
                                            <OnClick>
                                               <Sequence>
                                               
                                               <EnableAction Enabled="false" />
                                               
                                               <ScriptAction Script="Cover($get('Button_DuViverMaisPadroes'), $get('Div_Flout'));" />
                                               <StyleAction AnimationTarget="Div_Flout" Attribute="display" Value="block"/>
                                               
                                               <ScriptAction Script="Cover($get('Div_Flout'), $get('Div_InfoPadroes'), true);" />
                                               <StyleAction AnimationTarget="Div_InfoPadroes" Attribute="display" Value="block"/>
                                               <FadeIn AnimationTarget="Div_InfoPadroes" Duration=".2"/>
                                               <StyleAction AnimationTarget="Div_Flout" Attribute="display" Value="none"/>
                                               </Sequence>
                                            </OnClick>
                </Animations>
            </cc1:AnimationExtender>
            <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="btnClosePadroes">
                <Animations>
                                            <OnClick>
                                               <Sequence AnimationTarget="Div_InfoPadroes">
                                               
                                               <StyleAction Attribute="overflow" Value="hidden"/>
                                               <Parallel Duration=".3" Fps="15">
                                                  <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                                  <FadeOut />
                                               </Parallel>
                                               
                                               <StyleAction Attribute="display" Value="none"/>
                                               <StyleAction Attribute="width" Value="250px"/>
                                               <StyleAction Attribute="height" Value=""/>
                                               <StyleAction Attribute="fontSize" Value="12px"/>
                                               
                                               <EnableAction AnimationTarget="Button_DuViverMaisPadroes" Enabled="true" />
                                               </Sequence>
                                            </OnClick>
                </Animations>
            </cc1:AnimationExtender>
        </fieldset>
    </div>
    <%--    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>