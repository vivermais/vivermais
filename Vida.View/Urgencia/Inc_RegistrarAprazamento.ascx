﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_RegistrarAprazamento.ascx.cs" Inherits="ViverMais.View.Urgencia.Inc_RegistrarAprazamento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ OutputCache Duration="1" VaryByParam="none" VaryByCustom="browser" %>

<fieldset class="formulario">
    <h2>Confirmação de Aprazamento</h2>
    <legend>Dados da Prescrição</legend>
    <p>
        <span class="rotulo">Data</span>
        <span style="margin-left: 5px;">
            <asp:Label ID="Label_DataPrescricao" runat="server" Text=""></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Status</span>
        <span style="margin-left: 5px;">
            <asp:Label ID="Label_Status" runat="server" Text=""></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Válida Até</span>
        <span style="margin-left: 5px;">
            <asp:Label ID="Label_ValidadePrescricao" runat="server" Text=""></asp:Label>
        </span>
    </p>
</fieldset>
<fieldset class="formulario">
    <legend>Aprazamentos a serem confirmados</legend>
        <p>
        <span>
            <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                HeaderCssClass="accordionHeaderAp" HeaderSelectedCssClass="accordionHeaderSelectedAp"
                ContentCssClass="accordionContentAp">
                <Panes>
                    <cc1:AccordionPane ID="AccordionPane_Medicamentos" runat="server">
                        <Header>Medicamentos</Header>
                        <Content>
                            <asp:UpdatePanel ID="UpdatePanel_ConfirmarAprazamentoMedicamento" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:GridView ID="GridView_MedicamentosAprazar" runat="server"
                                        DataKeyNames="CodigoItem" OnRowCancelingEdit="OnRowCancelingEdit_CancelarAprazamentoMedicamento"
                                        OnRowEditing="OnRowEditing_EditarAprazamentoMedicamento"
                                        OnRowUpdating="OnRowUpdating_ConfirmarAprazamentoMedicamento"
                                        OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentos"
                                        AutoGenerateColumns="false" Width="600px">
                                    <Columns>
                                        <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ReadOnly="true" />
                                        <asp:BoundField DataField="DescricaoStatus" HeaderText="Status" ReadOnly="true" />
                                        <asp:BoundField DataField="Horario" HeaderText="Horário de Aplicação" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ReadOnly="true" />
                                        <asp:BoundField DataField="NomeProfissionalExecutor" HeaderText="Profissional Executor" ReadOnly="true" />
                                        <asp:TemplateField HeaderText="Data de Excecução">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_DataExecucao" runat="server" Text='<%#bind("DataExecucao") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox_DataExecucao" runat="server" CssClass="campo" Width="70px" Text='<%#bind("DataExecucao") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Hora de Excecução">
                                            <ItemTemplate>
                                                <asp:Label ID="Label_HoraExecucao" runat="server" Text='<%#bind("HoraExecucao") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBox_HoraExecucao" runat="server" CssClass="campo" Width="50px" Text='<%#bind("HoraExecucao") %>'></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit" CausesValidation="false">Confirmar Excecução</asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update" CausesValidation="false" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ConfirmarAprazarMedicamento')) return confirm('Tem certeza que deseja confirmar a execução deste procedimento ?');" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento">Finalizar</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel" CausesValidation="false">Cancelar</asp:LinkButton>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório." Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" ControlToValidate="TextBox_DataExecucao"></asp:RequiredFieldValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data com formato inválido." Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900." Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" ControlToValidate="TextBox_DataExecucao"></asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora é Obrigatório." Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento" ControlToValidate="TextBox_HoraExecucao"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido." ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraExecucao" Display="None" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"></asp:RegularExpressionValidator>
                                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_ConfirmarAprazarMedicamento"/>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                 TargetControlID="TextBox_DataExecucao">
                                                </cc1:CalendarExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataExecucao"
                                                 InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                                </cc1:MaskedEditExtender>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraExecucao"
                                                    InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                                </cc1:MaskedEditExtender>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum medicamento foi aprazado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="tab" />
                                    <RowStyle CssClass="tabrow" />
                                </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </Content>
                    </cc1:AccordionPane>
                </Panes>
            </cc1:Accordion>
        </span>
    </p>
</fieldset>