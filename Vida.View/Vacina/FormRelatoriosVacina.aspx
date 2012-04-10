<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormRelatoriosVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormRelatoriosVacina" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="TagNameEstabelecimento"
    TagPrefix="TagPrefixEstabelecimento" %>
<asp:Content ContentPlaceHolderID="head" runat="server" ID="c_head">
    <style type="text/css">
        /* Estilo do Accordeon*/.accordionHeader2
        {
            border: 1px solid #c6a033;
            color: #142126;
            background-color: #c6a033; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 600px;
            font-weight: bold;
        }
        .accordionHeaderSelected2
        {
            border: 1px solid #c6a033;
            color: white;
            background-color: #c6a033; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 600px;
            font-weight: bold;
        }
        .accordionContent2
        {
            background-color: #fff;
            border: 1px solid #c6a033;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 600px;
        }
    </style>
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Relatórios de Vacina</h2>
        <%--<fieldset class="formulario">
            <legend>Pesquisar</legend>--%>
        <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
            HeaderCssClass="accordionHeader2" HeaderSelectedCssClass="accordionHeaderSelected2"
            ContentCssClass="accordionContent2">
            <HeaderTemplate>
            </HeaderTemplate>
            <ContentTemplate>
            </ContentTemplate>
            <Panes>
                <cc1:AccordionPane ID="RELATORIO_PRODUCAO_DIARIA_VACINA" runat="server">
                    <Header>
                        Relação de Produção: Estabelecimento X Período</Header>
                    <Content>
                        <TagPrefixEstabelecimento:TagNameEstabelecimento ID="EAS" runat="server" />
                        <p>
                            <span class="rotulo">Distrito</span> <span>
                                <asp:DropDownList ID="DropDownList_Distrito" CssClass="drop" Width="350px"
                                 AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaUnidades" runat="server" DataTextField="Nome"
                                    DataValueField="Codigo">
                                </asp:DropDownList>
                            </span>
                        </p>
                        <%--<p>--%>
                        <%--<span class="rotulo">Estabelecimento </span><span>--%>
                        <%--<asp:DropDownList ID="DropDownList_Distrito_Producao_Diaria" DataTextField="Nome"
                                    DataValueField="Codigo" runat="server" CssClass="drop" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaUnidadesProducaoDiaria">
                                </asp:DropDownList>--%>
                        </span>
                        <%--</p>--%>
                        <asp:UpdatePanel ID="UpdatePanel_ProducaoDiaria" runat="server" ChildrenAsTriggers="true"
                            UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DropDownList_Distrito" EventName="SelectedIndexChanged" />
                                <asp:PostBackTrigger ControlID="LinkButton_GerarRelatorio" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_Unidades" runat="server" Visible="false">
                                    <p>
                                        <span class="rotulo">Data de Início</span> <span>
                                            <asp:TextBox ID="TextBox_DataInicioProducao" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data de Término</span> <span>
                                            <asp:TextBox ID="TextBox_DataTerminoProducao" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <asp:Table ID="TabelaProducaoDiaria" runat="server" Width="100%">
                                            <asp:TableRow>
                                                <asp:TableCell>
                                                    <asp:CheckBox ID="CheckBox_SelecionaTodasUnidadesProducaoDiaria" runat="server" Checked="false"
                                                        AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_SelecionaTodasUnidadesProducaoDiaria" />
                                                </asp:TableCell>
                                                <asp:TableCell>
                                                    <p style="font-weight:bolder;">SELECIONAR TODAS UNIDADES</p>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow Width="100%">
                                                <asp:TableCell ColumnSpan="2" Width="100%">
                                                    <asp:CheckBoxList ID="CheckBoxList_UnidadesProducaoDiaria" runat="server" CssClass="camporadio"
                                                        Width="100%" TextAlign="Right" RepeatLayout="Table"
                                                         DataValueField="CNES" DataTextField="NomeFantasia"
                                                        RepeatColumns="2" RepeatDirection="Horizontal">
                                                    </asp:CheckBoxList>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </p>
                                    <div class="botoesroll">
                                        <asp:LinkButton ID="LinkButton_GerarRelatorio" runat="server" OnClick="OnClick_GerarRelatorioProducaoDiaria"
                                            ValidationGroup="ValidationGroup_ProducaoDiaria">
                                            <img id="imggerar" alt="Gerar Relatório" src="img/btn_gerarrel1.png"
                                              onmouseover="imggerar.src='img/btn_gerarrel2.png';"
                                              onmouseout="imggerar.src='img/btn_gerarrel1.png';" />    
                                        </asp:LinkButton></div>
                                    <div class="botoesroll">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_CancelarProducaoDiaria">
                                            <img id="imgcancelar" alt="Cancelar" src="img/btn_cancelar1.png"
                                          onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                                          onmouseout="imgcancelar.src='img/btn_cancelar1.png';" />
                                        </asp:LinkButton></div>
                                    <p>
                                        <cc1:CalendarExtender ID="CalendarExtender5" TargetControlID="TextBox_DataInicioProducao"
                                            Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataInicioProducao"
                                            Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                        </cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Início é Obrigatório!"
                                            ControlToValidate="TextBox_DataInicioProducao" Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Data de Início com formato Inválido!"
                                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataInicioProducao"
                                            Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Data de Início deve ser igual ou maior que 01/01/1900."
                                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataInicioProducao"
                                            Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:CompareValidator>
                                        <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_DataTerminoProducao"
                                            Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                        </cc1:CalendarExtender>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataTerminoProducao"
                                            Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                                        </cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data de Término é Obrigatório!"
                                            ControlToValidate="TextBox_DataTerminoProducao" Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Término com formato Inválido!"
                                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataTerminoProducao"
                                            Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Término deve ser igual ou maior que 01/01/1900."
                                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataTerminoProducao"
                                            Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Término deve ser igual ou maior que a Data de Início."
                                            Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataTerminoProducao"
                                            ControlToCompare="TextBox_DataInicioProducao" Display="None" ValidationGroup="ValidationGroup_ProducaoDiaria"></asp:CompareValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroup_ProducaoDiaria" />
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
        <%--</fieldset>--%>
    </div>
</asp:Content>
