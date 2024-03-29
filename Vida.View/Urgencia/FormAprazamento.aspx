﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormAprazamento.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormAprazamento" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 750px;
            height: auto;
            margin-left: 17px;
            margin-right: 0px;
            padding: 10px 10px 20px 10px;
        }
        .formulario3
        {
            width: 680px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 20px 20px 20px 20px;
            margin: 10px;
        }
        .formulario4
        {
            width: 680px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 5px 5px 5px 5px;
            margin: 10px;
        }
        /* Estilo do Accordeon*/.accordionHeaderEv
        {
            border: 1px solid #d78686;
            color: #ffffff;
            background-color: #d78686; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 650px;
        }
        .accordionHeaderSelectedEv
        {
            border: 1px solid #7c1616;
            color: white;
            background-color: #7c1616; /*font-weight: bold;*/
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
    </style>
    <%--    <script type="text/javascript" language="javascript">
        function showTooltip(obj) {
            if (obj.options[obj.selectedIndex].title == "") {
                obj.title = obj.options[obj.selectedIndex].text;
                obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
                for (i = 0; i < obj.options.length; i++) {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
        }
    </script>--%>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender1"
        TargetControlID="allwaysOnMessage" VerticalSide="Middle" VerticalOffset="10"
        EnableViewState="false" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <asp:Panel ID="allwaysOnMessage" runat="server">
        <div style="position: fixed; margin-left:50%; left:290px; top: 310px; width:185px;">
            <p>
                <asp:Image ID="avisoUreg" runat="server" ImageUrl="~/Urgencia/img/legenda-status.png" />
            </p>
        </div>
    </asp:Panel>
    <div id="top">
        <h2>
            Aprazamento</h2>
        <fieldset class="formulario">
            <legend>Dados da Prescrição</legend>
            <p>
                <span class="rotulo">Data</span> <span>
                    <asp:Label ID="Label_Data" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Status</span> <span>
                    <asp:Label ID="Label_Status" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Aprazar até</span> <span>
                    <asp:Label ID="Label_UltimaDataValida" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
                </span>
            </p>
            <%--<p>
                <span>--%>
            <br />
            <div class="botoesroll">
                <asp:LinkButton ID="LinkButton_Voltar" runat="server" OnClick="OnClick_VoltarPagina">
                  <img id="imgvoltar1" alt="Salvar" src="img/bts/btn-voltar1.png"
                  onmouseover="imgvoltar1.src='img/bts/btn-voltar2.png';"
                  onmouseout="imgvoltar1.src='img/bts/btn-voltar1.png';" /></asp:LinkButton>
            </div>
            <%--</span>
            </p>--%>
        </fieldset>
        <%--<fieldset class="formulario">
            <legend>Itens da Prescrição</legend>--%>
        <%--            <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                HeaderCssClass="accordionHeaderEv" HeaderSelectedCssClass="accordionHeaderSelectedEv"
                ContentCssClass="accordionContentEv">
                <Panes>
                    <cc1:AccordionPane ID="AccordionPane_Medicamento" runat="server">
                        <Header>
                            Procedimentos</Header>
                        <Content>--%>
        <br />
        <br />
        <cc1:TabContainer ID="TabContainer1" runat="server" Width="720px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Procedimentos">
                <ContentTemplate>
                    <fieldset class="formulario4">
                        <legend>Novos Aprazamentos</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_ProcedimentoAprazar" DataKeyNames="CodigoProcedimento"
                                    runat="server" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanging="OnSelectedIndexChanging_Procedimentos"
                                    OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoAprazar">
                                    <Columns>
                                        <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Intervalo" ItemStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SuspensoToString" HeaderText="Status" ItemStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Aprazar"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:ButtonField ButtonType="Link" CommandName="Select" Text="Aprazar"
                                                    ItemStyle-HorizontalAlign="Center" />--%>
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
                        <asp:UpdatePanel ID="UpdatePanel_ProcedimentosAprazar" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="true" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView_ProcedimentoAprazar" EventName="SelectedIndexChanging" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_AprazarProcedimento" runat="server">
                                    <p>
                                        <span class="rotulo">Procedimento</span> <span style="margin-left: 5px;">
                                            <asp:DropDownList ID="DropDownList_ProcedimentoAprazar" runat="server" Width="400px">
                                            </asp:DropDownList>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data</span> <span style="margin-left: 5px;">
                                            <asp:TextBox ID="TextBox_DataAprazarProcedimento" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Hora</span> <span style="margin-left: 5px;">
                                            <asp:TextBox ID="TextBox_HoraAprazarProcedimento" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Aprazar automaticamente?</span> <span>
                                            <asp:CheckBox ID="CheckBox_AprazarProcedimentoAutomaticamente" runat="server" />
                                        </span>
                                    </p>
                                    <p align="center">
                                        <span>
                                            <asp:ImageButton ID="ImageButton_AdicionarProcedimentoAprazar" runat="server" Width="134px"
                                                Height="38px" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" OnClick="OnClick_AdicionarProcedimentoAprazamento"
                                                ValidationGroup="ValidationGroup_AprazarProcedimento" />
                                            <asp:ImageButton ID="ImageButton3" runat="server" Width="134px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                                CausesValidation="false" OnClick="OnClick_CancelarProcedimentoAprazamento" />
                                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Selecione um Procedimento."
                                                Operator="GreaterThan" ValueToCompare="-1" Display="None" ValidationGroup="ValidationGroup_AprazarProcedimento"
                                                ControlToValidate="DropDownList_ProcedimentoAprazar"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Data é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimento" ControlToValidate="TextBox_DataAprazarProcedimento"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Data com formato inválido."
                                                Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_AprazarProcedimento"
                                                ControlToValidate="TextBox_DataAprazarProcedimento"></asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                ValidationGroup="ValidationGroup_AprazarProcedimento" ControlToValidate="TextBox_DataAprazarProcedimento"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Hora é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimento" ControlToValidate="TextBox_HoraAprazarProcedimento"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Hora com formato inválido."
                                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraAprazarProcedimento"
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimento"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ValidationGroup_AprazarProcedimento" />
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataAprazarProcedimento">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="TextBox_DataAprazarProcedimento"
                                                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                            </cc1:MaskedEditExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" TargetControlID="TextBox_HoraAprazarProcedimento"
                                                InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                            </cc1:MaskedEditExtender>
                                        </span>
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="formulario4">
                        <legend>Aprazamentos Registrados</legend>
                        <asp:UpdatePanel ID="UpdatePanel_ProcedimentosAprazados" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="true" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ImageButton_AdicionarProcedimentoAprazar" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_ProcedimentosAprazados" AutoGenerateColumns="false" runat="server"
                                            Width="100%" OnRowDeleting="OnRowDeleting_ProcedimentoAprazado" OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoAprazado"
                                            OnRowCancelingEdit="OnRowCancelingEdit_CancelarAprazamentoProcedimento" OnRowUpdating="OnRowUpdating_ConfirmarAprazamentoProcedimento"
                                            OnRowEditing="OnRowEditing_EditarAprazamentoProcedimento" AllowPaging="true"
                                            PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_ProcedimentosAprazados"
                                            DataKeyNames="CodigoProcedimento,Horario">
                                            <Columns>
                                                <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Horário de Aplicação" DataField="Horario" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Solicitante" DataField="NomeProfissionalSolicitante"
                                                    ReadOnly="true" />
                                                <asp:BoundField DataField="NomeProfissionalExecutor" HeaderText="Executor" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Data de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_DataExecucao" runat="server" Text='<%#bind("DataExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_DataExecucao" runat="server" CssClass="campo" Width="70px"
                                                            Text='<%#bind("DataExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hora de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_HoraExecucao" runat="server" Text='<%#bind("HoraExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_HoraExecucao" runat="server" CssClass="campo" Width="50px"
                                                            Text='<%#bind("HoraExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit"
                                                            CausesValidation="false">Executar</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                            CausesValidation="false" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ConfirmarAprazarProcedimento')) return confirm('Tem certeza que deseja confirmar a execução deste procedimento ?');"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento">Confirmar</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                            CausesValidation="false">Cancelar</asp:LinkButton>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data com formato inválido."
                                                            Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento" ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento"
                                                            ControlToValidate="TextBox_HoraExecucao"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido."
                                                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraExecucao"
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento"></asp:RegularExpressionValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento" />
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataExecucao">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataExecucao"
                                                            InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraExecucao"
                                                            InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                                        </cc1:MaskedEditExtender>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_Excluir" runat="server" CausesValidation="false" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este horário de aprazamento ?');"
                                                            CommandName="Delete">Excluir</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Não foi encontrado registro de aprazamento para os procedimentos disponíveis."></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="tab" />
                                            <RowStyle CssClass="tabrow" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <%--                        </Content>
                    </cc1:AccordionPane>
                    <cc1:AccordionPane ID="AccordionPane1" runat="server">
                        <Header>
                            Procedimentos Não-Faturáveis</Header>
                        <Content>--%>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Procedimentos Não-Faturáveis">
                <ContentTemplate>
                    <fieldset class="formulario4">
                        <legend>Novos Aprazamentos</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_ProcedimentoNaoFaturavelAprazar" DataKeyNames="CodigoProcedimento"
                                    runat="server" Width="100%" AutoGenerateColumns="False" OnSelectedIndexChanging="OnSelectedIndexChanging_ProcedimentosNaoFaturaveis"
                                    OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAprazar">
                                    <Columns>
                                        <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Intervalo" ItemStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SuspensoToString" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Aprazar"
                                            ItemStyle-HorizontalAlign="Center" />
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
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                            RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView_ProcedimentoNaoFaturavelAprazar" EventName="SelectedIndexChanging" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_RegistrarAprazamentoProcedimentoNaoFaturavel" runat="server">
                                    <p>
                                        <span class="rotulo">Procedimento</span> <span style="margin-left: 5px;">
                                            <asp:DropDownList ID="DropDownList_ProcedimentoNaoFaturavelAprazar" runat="server"
                                                Width="400px">
                                            </asp:DropDownList>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data</span> <span style="margin-left: 5px;">
                                            <asp:TextBox ID="TextBox_DataAprazarProcedimentoNaoFaturavel" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Hora</span> <span style="margin-left: 5px;">
                                            <asp:TextBox ID="TextBox_HoraAprazarProcedimentoNaoFaturavel" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Aprazar automaticamente?</span> <span>
                                            <asp:CheckBox ID="CheckBox_AprazarProcedimentoNaoFaturavelAutomaticamente" runat="server" />
                                        </span>
                                    </p>
                                    <p align="center">
                                        <span>
                                            <asp:ImageButton ID="ImageButton_AdcionarPNF" runat="server" Width="134px" Height="38px"
                                                ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" OnClick="OnClick_AdicionarProcedimentoNaoFaturavelAprazamento"
                                                ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel" />
                                            <asp:ImageButton ID="ImageButton2" runat="server" Width="134px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                                CausesValidation="false" OnClick="OnClick_CancelarProcedimentoNaoFaturavelAprazamento" />
                                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione um Procedimento."
                                                Operator="GreaterThan" ValueToCompare="-1" Display="None" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel"
                                                ControlToValidate="DropDownList_ProcedimentoNaoFaturavelAprazar"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Data é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel"
                                                ControlToValidate="TextBox_DataAprazarProcedimentoNaoFaturavel"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Data com formato inválido."
                                                Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel"
                                                ControlToValidate="TextBox_DataAprazarProcedimentoNaoFaturavel"></asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel" ControlToValidate="TextBox_DataAprazarProcedimentoNaoFaturavel"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Hora é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel"
                                                ControlToValidate="TextBox_HoraAprazarProcedimentoNaoFaturavel"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Hora com formato inválido."
                                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraAprazarProcedimentoNaoFaturavel"
                                                Display="None" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ValidationGroup_AprazarProcedimentoNaoFaturavel" />
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataAprazarProcedimentoNaoFaturavel">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="TextBox_DataAprazarProcedimentoNaoFaturavel"
                                                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                            </cc1:MaskedEditExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="TextBox_HoraAprazarProcedimentoNaoFaturavel"
                                                InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                            </cc1:MaskedEditExtender>
                                        </span>
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="formulario4">
                        <legend>Aprazamentos Registrados</legend>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                            RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ImageButton_AdcionarPNF" EventName="Click" />
                                <%--<asp:AsyncPostBackTrigger ControlID="Button_CancelarAprazamentoMedicamento1" EventName="Click" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_ProcedimentoNaoFaturavelAprazado" AutoGenerateColumns="false"
                                            runat="server" Width="100%" OnRowDeleting="OnRowDeleting_ProcedimentoNaoFaturavelAprazado"
                                            OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAprazado"
                                            OnRowCancelingEdit="OnRowCancelingEdit_CancelarAprazamentoProcedimentoNaoFaturavel"
                                            OnRowUpdating="OnRowUpdating_ConfirmarAprazamentoProcedimentoNaoFaturavel" OnRowEditing="OnRowEditing_EditarAprazamentoProcedimentoNaoFaturavel"
                                            AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_ProcedimentosNaoFaturaveisAprazados"
                                            DataKeyNames="CodigoProcedimento,Horario">
                                            <Columns>
                                                <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Horário de Aplicação" DataField="Horario" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Solicitante" DataField="NomeProfissionalSolicitante"
                                                    ReadOnly="true" />
                                                <asp:BoundField DataField="NomeProfissionalExecutor" HeaderText="Executor" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Data de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_DataExecucao" runat="server" Text='<%#bind("DataExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_DataExecucao" runat="server" CssClass="campo" Width="70px"
                                                            Text='<%#bind("DataExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hora de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_HoraExecucao" runat="server" Text='<%#bind("HoraExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_HoraExecucao" runat="server" CssClass="campo" Width="50px"
                                                            Text='<%#bind("HoraExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit"
                                                            CausesValidation="false">Executar</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                            CausesValidation="false" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel')) return confirm('Tem certeza que deseja confirmar a execução deste procedimento ?');"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel">Confirmar</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                            CausesValidation="false">Cancelar</asp:LinkButton>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data com formato inválido."
                                                            Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel" ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel"
                                                            ControlToValidate="TextBox_HoraExecucao"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido."
                                                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraExecucao"
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel"></asp:RegularExpressionValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimentoNaoFaturavel" />
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataExecucao">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataExecucao"
                                                            InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraExecucao"
                                                            InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                                        </cc1:MaskedEditExtender>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_Excluir" runat="server" CausesValidation="false" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este horário de aprazamento ?');"
                                                            CommandName="Delete">Excluir</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Não foi encontrado registro de aprazamento para os procedimentos disponíveis."></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="tab" />
                                            <RowStyle CssClass="tabrow" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <%--                        </Content>
                    </cc1:AccordionPane>
                    <cc1:AccordionPane ID="AccordionPane2" runat="server">
                        <Header>
                            Medicamentos</Header>
                        <Content>--%>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Medicamentos/Prescrição">
                <ContentTemplate>
                    <fieldset class="formulario4">
                        <legend>Novos Aprazamentos</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_MedicamentoAprazar" DataKeyNames="Medicamento" OnSelectedIndexChanging="OnSelectedIndexChanging_Medicamentos"
                                    OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentoAprazar" runat="server"
                                    Width="100%" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Medicamento/Prescrição">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_Medicamento" runat="server" Text='<%#bind("NomeMedicamento") %>' ToolTip="Este(a) medicamento/prescrição está incluso em um kit?"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:HyperLinkField HeaderText="Medicamento" ControlStyle-ForeColor="Blue" DataTextField="NomeMedicamento" ItemStyle-HorizontalAlign="Center" />--%>
                                        <%--<asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                                        <%--<asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração"
                                                    ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField DataField="DescricaoObservacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SuspensoToString" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Aprazar"
                                            ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum(a) medicamento/prescrição encontrado(a) para registrar um aprazamento."></asp:Label>
                                    </EmptyDataTemplate>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <HeaderStyle CssClass="tab" />
                                    <RowStyle CssClass="tabrow" />
                                </asp:GridView>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="Update_PanelMedicamentoRegistrarAprazamento" runat="server"
                            UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView_MedicamentoAprazar" EventName="SelectedIndexChanging" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_RegistrarAprazamentoMedicamento" runat="server">
                                    <p>
                                        <span class="rotulo">Medicamento/Prescrição</span> <span>
                                            <asp:DropDownList ID="DropDownList_MedicamentoAprazar" runat="server" Width="400px">
                                            </asp:DropDownList>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data</span> <span>
                                            <asp:TextBox ID="TextBox_DataAprazarMedicamento" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Hora</span> <span>
                                            <asp:TextBox ID="TextBox_HoraAprazarMedicamento" CssClass="campo" runat="server"></asp:TextBox>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Aprazar automaticamente?</span> <span>
                                            <asp:CheckBox ID="CheckBox_AprazarMedicamentoAutomaticamente" runat="server" />
                                        </span>
                                    </p>
                                    <p align="center">
                                        <span>
                                            <asp:ImageButton ID="Button_AprazarMedicamento1" runat="server" Width="134px" Height="38px"
                                                ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" OnClick="OnClick_AdicionarMedicamentoAprazamento"
                                                ValidationGroup="ValidationGroup_AprazarMedicamento" />
                                            <asp:ImageButton ID="Button_CancelarAprazamentoMedicamento1" runat="server" Width="134px"
                                                Height="38px" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png" CausesValidation="false"
                                                OnClick="OnClick_CancelarMedicamentoAprazamento" />
                                            <asp:CompareValidator ID="CompareValidator40" runat="server" ErrorMessage="Selecione um(a) medicamento/prescrição."
                                                Operator="GreaterThan" ValueToCompare="-1" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento"
                                                ControlToValidate="DropDownList_MedicamentoAprazar"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_DataAprazarMedicamento"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data com formato inválido."
                                                Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento"
                                                ControlToValidate="TextBox_DataAprazarMedicamento"></asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_DataAprazarMedicamento"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Hora é Obrigatório."
                                                Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_HoraAprazarMedicamento"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido."
                                                ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraAprazarMedicamento"
                                                Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" ValidationGroup="ValidationGroup_AprazarMedicamento" />
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataAprazarMedicamento">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataAprazarMedicamento"
                                                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                            </cc1:MaskedEditExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraAprazarMedicamento"
                                                InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                            </cc1:MaskedEditExtender>
                                        </span>
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <fieldset class="formulario4">
                        <legend>Aprazamentos Registrados</legend>
                        <asp:UpdatePanel ID="UpdatePanel_MedicamentoAprazamentoRegistrados" runat="server"
                            UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button_AprazarMedicamento1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton_ConfirmarRecusaMedicamento" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_MedicamentoAprazado" AutoGenerateColumns="false" runat="server"
                                            Width="100%" OnRowDeleting="OnRowDeleting_MedicamentoAprazado" OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentoAprazado"
                                            OnRowCancelingEdit="OnRowCancelingEdit_CancelarAprazamentoMedicamento" OnRowUpdating="OnRowUpdating_ConfirmarAprazamentoMedicamento"
                                            OnRowEditing="OnRowEditing_EditarAprazamentoMedicamento" AllowPaging="true" PageSize="10"
                                            PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_MedicamentosAprazados"
                                            DataKeyNames="CodigoMedicamento,Horario" OnRowCommand="OnRowCommand_Medicamentos">
                                            <Columns>
                                                <asp:BoundField HeaderText="Medicamento/Prescrição" DataField="NomeMedicamento" ReadOnly="true" />
                                                <asp:BoundField HeaderText="Horário de Aplicação" DataField="Horario" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Solicitante" DataField="NomeProfissionalSolicitante"
                                                    ReadOnly="true" />
                                                <asp:BoundField DataField="NomeProfissionalExecutor" HeaderText="Executor" ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Data de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_DataExecucao" runat="server" Text='<%#bind("DataExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_DataExecucao" runat="server" CssClass="campo" Width="70px"
                                                            Text='<%#bind("DataExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hora de Excecução">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_HoraExecucao" runat="server" Text='<%#bind("HoraExecucao") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_HoraExecucao" runat="server" CssClass="campo" Width="50px"
                                                            Text='<%#bind("HoraExecucao") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit"
                                                            CausesValidation="false">Executar</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                            CausesValidation="false" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ConfirmarAprazarMedicamento')) return confirm('Tem certeza que deseja confirmar a execução deste procedimento ?');"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento">Confirmar</asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                            CausesValidation="false">Cancelar</asp:LinkButton>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data com formato inválido."
                                                            Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"
                                                            ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None"
                                                            ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora é Obrigatório."
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"
                                                            ControlToValidate="TextBox_HoraExecucao"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido."
                                                            ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraExecucao"
                                                            Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"></asp:RegularExpressionValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" />
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataExecucao">
                                                        </cc1:CalendarExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataExecucao"
                                                            InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                                        </cc1:MaskedEditExtender>
                                                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraExecucao"
                                                            InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                                        </cc1:MaskedEditExtender>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_Excluir" runat="server" CausesValidation="false" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este horário de aprazamento ?');"
                                                            CommandName="Delete">Excluir</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton_RecusarMedicamento" runat="server">Recusar Medicamento</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:ButtonField ButtonType="Link" CausesValidation="false" CommandName="Recusar"
                                                    Text="Recusar Medicamento/Prescrição" />
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Não foi encontrado registro de aprazamento para os medicamentos/prescrições disponíveis."></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="tab" />
                                            <RowStyle CssClass="tabrow" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel_RecusaMedicamento" runat="server" UpdateMode="Conditional"
                            ChildrenAsTriggers="true" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GridView_MedicamentoAprazado" EventName="RowCommand" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="Panel_RecusarMedicamento" runat="server" Visible="false">
                                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                                        height: 130%; z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=75);
                                        moz-opacity: 0.3; opacity: 0.8">
                                    </div>
                                    <div id="mensagem" style="position: fixed; top: 100px; left: 50%; margin-left: -370px;
                                        width: 700px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 15px; padding-bottom: 10px;
                                        border-left: #ffffff  5px solid; color: #000000; padding-top: 10px; border-bottom: #ffffff 5px solid;
                                        text-align: justify; font-family: Verdana;">
                                        <div style="padding-left: 0px;">
                                            <asp:Label ID="Label" runat="server" CssClass="titulo" Font-Bold="true" Text="Motivo da Recusa do(a) Medicamento/Prescrição"></asp:Label>
                                            <br />
                                            <p>
                                                <span class="rotulo">Medicamento/Prescrição</span> <span>
                                                    <asp:Label ID="Label_MedicamentoRecusa" runat="server" Text="" ForeColor="White"></asp:Label>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Horário de Aplicação</span> <span>
                                                    <asp:Label ID="Label_HorarioMedicamentoRecusa" runat="server" Text="" ForeColor="White"></asp:Label>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Justificativa</span> <span>
                                                    <asp:TextBox ID="TextBox_MotivoRecusaMedicamento" runat="server" CssClass="campo"
                                                        TextMode="MultiLine" Width="100%" Height="300px"></asp:TextBox>
                                                </span>
                                            </p>
                                            <br />
                                            <asp:Panel ID="Panel_ConfirmarCancelarRecusaMedicamento" runat="server">
                                                <div id="botoesroll">
                                                    <asp:LinkButton ID="LinkButton_ConfirmarRecusaMedicamento" runat="server" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_RecusarMedicamento'))
                                             return confirm('Tem certeza que deseja recusar este(a) medicamento/prescrição?'); return false;" OnClick="OnClick_ConfirmarRecusaMedicamento">
                                             <img alt="Confirmar" src="img/bts/btn-confirmar1.png"
                                                    onmouseover="this.src='img/bts/btn-confirmar2.png';"
                                                    onmouseout="this.src='img/bts/btn-confirmar1.png';" />
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton_CancelarRecusaMedicamento" runat="server" OnClick="OnClick_CancelarRecusaMedicamento">
                                                <img alt="Cancelar" src="img/bts/btn_cancelar1.png"
                                                    onmouseover="this.src='img/bts/btn_cancelar2.png';"
                                                    onmouseout="this.src='img/bts/btn_cancelar1.png';" /></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel_FecharMotivoRecusaMedicamento" runat="server">
                                                <p style="padding: 20px 10px 50px 0">
                                                    <asp:ImageButton ID="ImageButton_FecharMovitoRecusaMedicamento" runat="server" CausesValidation="false"
                                                        Height="39px" ImageAlign="Left" ImageUrl="~/Urgencia/img/fechar-btn.png" OnClick="OnClick_CancelarRecusaMedicamento"
                                                        Width="100px" />
                                                </p>
                                            </asp:Panel>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator_MotivoRecusaMedicamento" runat="server"
                                                ErrorMessage="Justificativa é Obrigatória." ValidationGroup="ValidationGroup_RecusarMedicamento"
                                                Display="None" ControlToValidate="TextBox_MotivoRecusaMedicamento"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator_MotivoRecusaMedicamento"
                                                runat="server" ErrorMessage="O tamanho máximo para o campo Justificativa é de 300 caracteres."
                                                Display="None" ValidationExpression="^[\w\W]{1,300}$" ValidationGroup="ValidationGroup_RecusarMedicamento"
                                                ControlToValidate="TextBox_MotivoRecusaMedicamento"></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ValidationGroup_RecusarMedicamento"
                                                ShowMessageBox="true" ShowSummary="false" />
                                        </div>
                                    </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <%--                        </Content>
                    </cc1:AccordionPane>
                </Panes>
            </cc1:Accordion>--%>
        <%--</fieldset>--%>
    </div>
</asp:Content>
