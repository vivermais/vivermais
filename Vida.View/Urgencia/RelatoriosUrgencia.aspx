<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="RelatoriosUrgencia.aspx.cs" Inherits="ViverMais.View.Urgencia.RelatoriosUrgencia"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Relatórios de Urgência</h2>
        <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
            ContentCssClass="accordionContent">
            <HeaderTemplate>
            </HeaderTemplate>
            <ContentTemplate>
            </ContentTemplate>
            <Panes>
                <cc1:AccordionPane ID="GERAR_RELATORIO_SITUACAO" runat="server">
                    <Header>
                        Relação de Pacientes Por Situação</Header>
                    <Content>
                        <p>
                            <span class="rotulo">Distrito</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="ddlDistritoSituacao" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlDistritoSituacao_SelectedIndexChanged"
                                    Width="300px" runat="server">
                                </asp:DropDownList>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel_Situacao" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlDistritoSituacao" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="ddlUnidadeSituacao" runat="server" Width="300px">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Situação</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="ddlSituacao" runat="server">
                                </asp:DropDownList>
                            </span>
                            <%--<asp:CustomValidator ID="CustomValidatorSituacao" Font-Size="XX-Small" runat="server"
                                ErrorMessage="Selecione um Distrito e uma Unidade" Display="Dynamic" OnServerValidate="CustomValidatorSituacao_ServerValidate"></asp:CustomValidator>--%>
                        </p>
                        <p>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                OnClick="OnClick_RelSituacao" Width="44px" Height="43px" ValidationGroup="ValidationGroup_Situacao" />
                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Selecione uma Unidade."
                                ControlToValidate="ddlUnidadeSituacao" Operator="GreaterThan" ValueToCompare="-1"
                                Display="None" ValidationGroup="ValidationGroup_Situacao"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Selecione uma Situação."
                                ControlToValidate="ddlSituacao" Operator="GreaterThan" ValueToCompare="-1" Display="None"
                                ValidationGroup="ValidationGroup_Situacao"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ValidationGroup_Situacao" />
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_ATENDIMENTO_CID" runat="server">
                    <Header>
                        Relação de Atendimentos Por CID</Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_UnidadeCID" runat="server" Width="300px">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p style="height: auto;">
                            <span class="rotulo">Grupo CID</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="ddlGrupoCid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGrupoCid_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp; </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel_Cid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlGrupoCid" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">CID</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="ddlCid" runat="server" DataTextField="Nome" DataValueField="Codigo"
                                            Height="20px" Width="300px">
                                            <asp:ListItem Text="Selecione..." Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Todos</span> <span style="margin-left: 5px;">
                                <asp:CheckBox ID="CheckBox_TodosOsCids" runat="server" />
                            </span>
                        </p>
                        <p>
                            <p>
                                <span class="rotulo">Data Inicial</span> <span style="margin-left: 5px;">
                                    <asp:TextBox ID="tbxDataInicialCid" CssClass="campo" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="tbxDataInicialCid"
                                        Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbxDataInicialCid"
                                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                    </cc1:MaskedEditExtender>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data Final</span> <span style="margin-left: 5px;">
                                    <asp:TextBox ID="tbxDataFinalCid" CssClass="campo" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender6" TargetControlID="tbxDataFinalCid" Format="dd/MM/yyyy"
                                        PopupButtonID="calendar_icon.png" runat="server">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbxDataFinalCid"
                                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                    </cc1:MaskedEditExtender>
                                </span>
                                <%--<asp:CustomValidator ID="CustomValidatorAtendCIDDataIni" runat="server" ErrorMessage="Preencha a Data Inicial e a Data Final"
                                    Display="Dynamic" Font-Size="XX-Small" OnServerValidate="CustomValidatorAtendCIDDataIni_ServerValidate"></asp:CustomValidator>--%>
                            </p>
                            <p>
                                <span>
                                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                        OnClick="OnClick_RelCid" Width="44px" Height="43px" ValidationGroup="ValidationGroup_Cid" />
                                    <asp:CompareValidator ID="CompareValidator37" runat="server" ErrorMessage="Selecione uma Unidade."
                                        ControlToValidate="DropDownList_UnidadeCID" Display="None" ValueToCompare="-1"
                                        Operator="GreaterThan" ValidationGroup="ValidationGroup_Cid">
                                    </asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data Inicial é Obrigatório!"
                                        ControlToValidate="tbxDataInicialCid" Display="None" ValidationGroup="ValidationGroup_Cid"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Data Inicial com formato Inválido!"
                                        Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataInicialCid" Display="None"
                                        ValidationGroup="ValidationGroup_Cid"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900."
                                        Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataInicialCid"
                                        Display="None" ValidationGroup="ValidationGroup_Cid"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data Final é Obrigatório!"
                                        ControlToValidate="tbxDataFinalCid" Display="None" ValidationGroup="ValidationGroup_Cid"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator11" runat="server" ErrorMessage="Data Final com formato Inválido!"
                                        Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataFinalCid" Display="None"
                                        ValidationGroup="ValidationGroup_Cid"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
                                        Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataFinalCid"
                                        Display="None" ValidationGroup="ValidationGroup_Cid"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."
                                        Operator="GreaterThanEqual" Type="Date" ControlToCompare="tbxDataInicialCid"
                                        ControlToValidate="tbxDataFinalCid" Display="None" ValidationGroup="ValidationGroup_Cid"></asp:CompareValidator>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="ValidationGroup_Cid" />
                                    <asp:CustomValidator ID="CustomValidator_RelCid" runat="server" ErrorMessage="CustomValidator"
                                        OnServerValidate="OnServerValidate_RelCid" Display="None" ValidationGroup="ValidationGroup_Cid"></asp:CustomValidator>
                                </span>
                            </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_EVASAO" runat="server">
                    <Header>
                        Relação de Evasão
                    </Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_UnidadeEvasao" runat="server" Width="300px">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Inicial</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxPeriodoInicial" CssClass="campo" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="tbxPeriodoInicial"
                                    Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="tbxPeriodoInicial"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                                <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="A Data Inicial não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxPeriodoInicial" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Final</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxPeriodoFinal" CssClass="campo" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbxPeriodoFinal" Format="dd/MM/yyyy"
                                    PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="tbxPeriodoFinal"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                                <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="A Data Final não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxPeriodoFinal" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                            </span>
                            <%--<asp:CustomValidator ID="CustomValidatorAbsData" runat="server" Display="Dynamic"
                                ErrorMessage="Preencha a Data Inicial e a Data Final" Font-Size="XX-Small" OnServerValidate="CustomValidatorAbsData_ServerValidate"></asp:CustomValidator>--%>
                        </p>
                        <p>
                            <span>
                                <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                    OnClick="OnClick_RelEvasao" Width="44px" Height="43px" ValidationGroup="ValidationGroup_Evasao" />
                                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_Evasao" />
                                <asp:CompareValidator ID="CompareValidator36" runat="server" ErrorMessage="Selecione uma Unidade."
                                    ControlToValidate="DropDownList_UnidadeEvasao" Display="None" ValueToCompare="-1"
                                    Operator="GreaterThan" ValidationGroup="ValidationGroup_Evasao">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Data Inicial é Obrigatório!"
                                    ControlToValidate="tbxPeriodoInicial" Display="None" ValidationGroup="ValidationGroup_Evasao"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data Inicial com formato Inválido!"
                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxPeriodoInicial" Display="None"
                                    ValidationGroup="ValidationGroup_Evasao"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900."
                                    Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxPeriodoInicial"
                                    Display="None" ValidationGroup="ValidationGroup_Evasao"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Data Final é Obrigatório!"
                                    ControlToValidate="tbxPeriodoFinal" Display="None" ValidationGroup="ValidationGroup_Evasao"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Data Final com formato Inválido!"
                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxPeriodoFinal" Display="None"
                                    ValidationGroup="ValidationGroup_Evasao"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator14" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
                                    Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxPeriodoFinal"
                                    Display="None" ValidationGroup="ValidationGroup_Evasao"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."
                                    Operator="GreaterThanEqual" Type="Date" ControlToCompare="tbxPeriodoInicial"
                                    ControlToValidate="tbxPeriodoFinal" Display="None" ValidationGroup="ValidationGroup_Evasao"></asp:CompareValidator>
                            </span>
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_ATENDIMENTO_FAIXA_ETARIA" runat="server">
                    <Header>
                        Relação de Atendimentos Por Faixa Etária
                    </Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_UnidadeFaixaEtaria" runat="server" Width="300px">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Inicial</span><span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxDataInicial" CssClass="campo" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender3" TargetControlID="tbxDataInicial" Format="dd/MM/yyyy"
                                    PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="tbxDataInicial"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                                <%-- <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="A Data Inicial não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxDataInicial" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Final</span><span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxDataFinal" CssClass="campo" runat="server"></asp:TextBox>
                                <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="A Data Final não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxDataFinal" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                                <cc1:CalendarExtender ID="CalendarExtender4" TargetControlID="tbxDataFinal" Format="dd/MM/yyyy"
                                    PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" TargetControlID="tbxDataFinal"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Sexo</span> <span class="camporadio" style="margin-left: 5px;">
                                <asp:RadioButtonList runat="server" ID="rbtSexo" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="M" Selected="True">Masculino</asp:ListItem>
                                    <asp:ListItem Value="F">Feminino</asp:ListItem>
                                    <asp:ListItem>Ambos</asp:ListItem>
                                </asp:RadioButtonList>
                            </span>
                            <%--<asp:CustomValidator ID="CustomValidatorAtendFaixaEtaria" runat="server" Display="Dynamic"
                                ErrorMessage="Preencha a Data Inicial e a Data Final" Font-Size="XX-Small" OnServerValidate="CustomValidatorAtendFaixaEtaria_ServerValidate"></asp:CustomValidator>--%>
                        </p>
                        <p>
                            <span>
                                <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                    OnClick="OnClick_RelAtendimentoFaixa" Width="44px" Height="43px" ValidationGroup="ValidationGroup_AtendimentoFaixa" />
                                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_AtendimentoFaixa" />
                                <asp:CompareValidator ID="CompareValidator35" runat="server" ErrorMessage="Selecione uma Unidade."
                                    ControlToValidate="DropDownList_UnidadeFaixaEtaria" Display="None" ValueToCompare="-1"
                                    Operator="GreaterThan" ValidationGroup="ValidationGroup_AtendimentoFaixa">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Data Inicial é Obrigatório!"
                                    ControlToValidate="tbxDataInicial" Display="None" ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data Inicial com formato Inválido!"
                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataInicial" Display="None"
                                    ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900."
                                    Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataInicial"
                                    Display="None" ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Data Final é Obrigatório!"
                                    ControlToValidate="tbxDataFinal" Display="None" ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator15" runat="server" ErrorMessage="Data Final com formato Inválido!"
                                    Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataFinal" Display="None"
                                    ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator16" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
                                    Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataFinal"
                                    Display="None" ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator24" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."
                                    Operator="GreaterThanEqual" Type="Date" ControlToCompare="tbxDataInicial" ControlToValidate="tbxDataFinal"
                                    Display="None" ValidationGroup="ValidationGroup_AtendimentoFaixa"></asp:CompareValidator>
                            </span>
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_LEITOS_FAIXA_ETARIA" runat="server">
                    <Header>
                        Relatório de Leitos Por Faixa Etária
                    </Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="ddlUnidadeLeitos" runat="server" Width="300px">
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                    OnClick="OnClick_RelLeitosFaixa" Width="44px" Height="43px" ValidationGroup="ValidationGroup_RelFaixa" />
                                <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_RelFaixa" />
                                <asp:CompareValidator ID="CompareValidator17" runat="server" ErrorMessage="Selecione uma Unidade."
                                    Operator="GreaterThan" ValueToCompare="-1" ControlToValidate="ddlUnidadeLeitos"
                                    Display="None" ValidationGroup="ValidationGroup_RelFaixa"></asp:CompareValidator>
                            </span>
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_TEMPO_PERMANENCIA" runat="server">
                    <Header>
                        Relatório por Tempo de Permanência
                    </Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="ddlUnidadePermanecia" runat="server" Width="300px">
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Inicial</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxDataInicioPermanencia" CssClass="campo" runat="server"></asp:TextBox>
                                <%--<asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="A Data Inicial não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxDataInicioPermanencia" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                                <cc1:CalendarExtender ID="CalendarExtender7" TargetControlID="tbxDataInicioPermanencia"
                                    Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="tbxDataInicioPermanencia"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Final</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxDataFinalPermanencia" CssClass="campo" runat="server"></asp:TextBox>
                                <%--<asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="A Data Inicial não parece válida"
                                    Display="Dynamic" Type="Date" ControlToValidate="tbxDataFinalPermanencia" Font-Size="XX-Small"
                                    Operator="DataTypeCheck"></asp:CompareValidator>--%>
                                <cc1:CalendarExtender ID="CalendarExtender8" TargetControlID="tbxDataFinalPermanencia"
                                    Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" TargetControlID="tbxDataFinalPermanencia"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                        <p>
                            <asp:ImageButton ID="ImageButton6" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                OnClick="OnClick_RelPermanencia" Width="44px" Height="43px" ValidationGroup="ValidationGroup_RelPermanencia" />
                            <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ValidationGroup_RelPermanencia" />
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione uma Unidade."
                                Operator="GreaterThan" ValueToCompare="0" ControlToValidate="ddlUnidadePermanecia"
                                Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Data Inicial é Obrigatório!"
                                ControlToValidate="tbxDataInicioPermanencia" Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Data Inicial com formato Inválido!"
                                Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataInicioPermanencia"
                                Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator18" runat="server" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900."
                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataInicioPermanencia"
                                Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Data Final é Obrigatório!"
                                ControlToValidate="tbxDataFinalPermanencia" Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator19" runat="server" ErrorMessage="Data Final com formato Inválido!"
                                Operator="DataTypeCheck" Type="Date" ControlToValidate="tbxDataFinalPermanencia"
                                Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="tbxDataFinalPermanencia"
                                Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."
                                Operator="GreaterThanEqual" Type="Date" ControlToCompare="tbxDataInicioPermanencia"
                                ControlToValidate="tbxDataFinalPermanencia" Display="None" ValidationGroup="ValidationGroup_RelPermanencia"></asp:CompareValidator>
                        </p>
                        <%--<p>
                            <asp:CustomValidator ID="CustomValidator_Permanencia" Font-Size="XX-Small" runat="server"
                            ErrorMessage="Selecione uma Unidade e informe a data inicial e final" Display="Dynamic" OnServerValidate="CustomValidatorPermanencia_ServerValidate"></asp:CustomValidator>
                        </p>--%>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_PROCEDIMENTO" runat="server">
                    <Header>
                        Relatório de Procedimento</Header>
                    <Content>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_EstabelecimentoProcedimentoFPO" runat="server" Width="300px">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Mês</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_MesProcedimento" runat="server">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Ano</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_AnoProcedimento" runat="server" CssClass="campo"></asp:TextBox>
                            </span>
                        </p>
                        <%--                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Procedimentos</span> <span class="camporadio" style="margin-left: 5px;">
                                        <asp:RadioButtonList ID="RadioButtonList_ProcedimentosFPO" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaProcedimentos"
                                            runat="server">
                                            <asp:ListItem Value="T" Selected="True">Todos</asp:ListItem>
                                            <asp:ListItem Value="S">SIGTAP</asp:ListItem>
                                            <asp:ListItem Value="N">Não-Faturáveis</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <%--                        <asp:UpdatePanel ID="UpdatePanel_ProcedimentoFPO" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="RadioButtonList_ProcedimentosFPO" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_ProcedimentosSIGTAP" runat="server" Visible="false">--%>
                        <p>
                            <span class="rotulo">Pesquisar Procedimento</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_BuscarSIGTAP" runat="server" CssClass="campo" Width="250px"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton9" runat="server" OnClick="OnClick_PesquisarProcedimentoSIGTAP"
                                    ValidationGroup="ValidationGroup_PesquisarProcedimentoSIGTAP" ImageUrl="~/Urgencia/img/procurar.png" Width="16px" Height="16px"/>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ImageButton9" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">SIGTAP</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="DropDownList_ProcedimentoSIGTAPFPO" Height="20px" Width="300px"
                                            runat="server">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--                                </asp:Panel>
                                <asp:Panel ID="Panel_ProcedimentosNaoFaturaveis" runat="server" Visible="false">--%>
                        <p>
                            <span class="rotulo">Pesquisar Procedimento</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_BuscarNaoFaturavel" runat="server" CssClass="campo" Width="250"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton10" runat="server" OnClick="OnClick_PesquisarProcedimentoNaoFaturavel"
                                    ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel" ImageUrl="~/Urgencia/img/procurar.png" Width="16px" Height="16px"/>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ImageButton10" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Não-Faturável</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="DropDownList_ProcedimentoNaoFaturavelFPO" Height="20px" Width="300px"
                                            runat="server">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="TODOS" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%--                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                        <p>
                            <span>
                                <asp:ImageButton ID="ImageButton7" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                    OnClick="OnClick_RelProcedimentoFPO" Width="44px" Height="43px" ValidationGroup="ValidationGroup_RelProcedimentoFPO" />
                                <asp:CompareValidator ID="CompareValidator27" runat="server" ErrorMessage="Selecione uma Unidade."
                                    ControlToValidate="DropDownList_EstabelecimentoProcedimentoFPO" Display="None"
                                    Operator="GreaterThan" ValueToCompare="-1" ValidationGroup="ValidationGroup_RelProcedimentoFPO"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator25" runat="server" ErrorMessage="Selecione um mês."
                                    Display="None" ControlToValidate="DropDownList_MesProcedimento" Operator="GreaterThan"
                                    ValueToCompare="-1" ValidationGroup="ValidationGroup_RelProcedimentoFPO"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Ano é Obrigatório."
                                    ControlToValidate="TextBox_AnoProcedimento" Display="None" ValidationGroup="ValidationGroup_RelProcedimentoFPO"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator26" runat="server" ErrorMessage="Ano deve ser igual ou maior que 1900."
                                    Operator="GreaterThanEqual" Display="None" ValueToCompare="1900" ValidationGroup="ValidationGroup_RelProcedimentoFPO"
                                    ControlToValidate="TextBox_AnoProcedimento"></asp:CompareValidator>
                                <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_RelProcedimentoFPO" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator40" runat="server" ControlToValidate="TextBox_BuscarSIGTAP"
                                    Display="None" ValidationGroup="ValidationGroup_PesquisarProcedimentoSIGTAP"
                                    ErrorMessage="Informe o nome do procedimento."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do Procedimento."
                                    Display="None" ControlToValidate="TextBox_BuscarSIGTAP" ValidationGroup="ValidationGroup_PesquisarProcedimentoSIGTAP"
                                    ValidationExpression="^(\W{3,})|(\w{3,})$">
                                </asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarProcedimentoSIGTAP" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox_BuscarNaoFaturavel"
                                    Display="None" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                    ErrorMessage="Informe o nome do procedimento."></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do Procedimento."
                                    Display="None" ControlToValidate="TextBox_BuscarNaoFaturavel" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel"
                                    ValidationExpression="^(\W{3,})|(\w{3,})$">
                                </asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavel" />
                            </span>
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="GERAR_RELATORIO_PROCEDENCIA" runat="server">
                    <Header>
                        Relatório de Procedência
                    </Header>
                    <Content>
                        <p>
                            <span class="rotulo">Grupo CID</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_GrupoCIDProcedencia" CausesValidation="false"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlGrupoCid_SelectedIndexChanged"
                                    runat="server">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Unidade</span> <span style="margin-left: 5px;">
                                <asp:DropDownList ID="DropDownList_UnidadeCIDProcedencia" Height="20px" Width="300px"
                                    CausesValidation="false" runat="server">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel42" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DropDownList_GrupoCIDProcedencia" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">CID</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="DropDownList_CIDProcedencia" Height="20px" Width="300px" CausesValidation="false"
                                            runat="server">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Data Inicial</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_DataInicialCIDProcedencia" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Final</span><span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_DataFinalCIDProcedencia" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:ImageButton ID="ImageButton8" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                                    OnClick="OnClick_RelCIDProcedencia" Width="44px" Height="43px" ValidationGroup="ValidationGroup_RelCIDProcedencia" />
                                <asp:CompareValidator ID="CompareValidator29" runat="server" ErrorMessage="Unidade é Obrigatório."
                                    ControlToValidate="DropDownList_UnidadeCIDProcedencia" ValueToCompare="-1" Operator="GreaterThan"
                                    Display="None" ValidationGroup="ValidationGroup_RelCIDProcedencia"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator28" runat="server" ErrorMessage="CID é Obrigatório."
                                    ControlToValidate="DropDownList_CIDProcedencia" ValueToCompare="-1" Operator="GreaterThan"
                                    Display="None" ValidationGroup="ValidationGroup_RelCIDProcedencia"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Data Inicial é Obrigatório."
                                    Display="None" ValidationGroup="ValidationGroup_RelCIDProcedencia" ControlToValidate="TextBox_DataInicialCIDProcedencia"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator30" runat="server" ErrorMessage="Data Inicial inválida."
                                    Type="Date" Operator="DataTypeCheck" ValidationGroup="ValidationGroup_RelCIDProcedencia"
                                    Display="None" ControlToValidate="TextBox_DataInicialCIDProcedencia"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator31" runat="server" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900."
                                    Type="Date" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" ValidationGroup="ValidationGroup_RelCIDProcedencia"
                                    Display="None" ControlToValidate="TextBox_DataInicialCIDProcedencia"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Data Final é Obrigatório."
                                    Display="None" ValidationGroup="ValidationGroup_RelCIDProcedencia" ControlToValidate="TextBox_DataFinalCIDProcedencia"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator32" runat="server" ErrorMessage="Data Final inválida."
                                    Type="Date" Operator="DataTypeCheck" ValidationGroup="ValidationGroup_RelCIDProcedencia"
                                    Display="None" ControlToValidate="TextBox_DataFinalCIDProcedencia"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator33" runat="server" ErrorMessage="Data Final deve ser igual ou maior que 01/01/1900."
                                    Type="Date" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" ValidationGroup="ValidationGroup_RelCIDProcedencia"
                                    Display="None" ControlToValidate="TextBox_DataFinalCIDProcedencia"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator34" runat="server" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."
                                    Type="Date" Operator="LessThanEqual" ControlToCompare="TextBox_DataFinalCIDProcedencia"
                                    ValidationGroup="ValidationGroup_RelCIDProcedencia" Display="None" ControlToValidate="TextBox_DataInicialCIDProcedencia"></asp:CompareValidator>
                                <asp:ValidationSummary ID="ValidationSummary8" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="ValidationGroup_RelCIDProcedencia" />
                                <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="TextBox_DataInicialCIDProcedencia"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                                <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataInicialCIDProcedencia">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" TargetControlID="TextBox_DataFinalCIDProcedencia"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                </cc1:MaskedEditExtender>
                                <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
                                    TargetControlID="TextBox_DataFinalCIDProcedencia">
                                </cc1:CalendarExtender>
                            </span>
                        </p>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
        <%--        <p>
            <asp:ImageButton ID="btnPesquisar" runat="server" ImageUrl="~/img/bts/bt_pesq.png"
                OnClick="btnPesquisar_Click" Width="44px" Height="43px" />
        </p>--%>
    </div>
    <br />
    <br />
    <br />
    <br />
</asp:Content>
