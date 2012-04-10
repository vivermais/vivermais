﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_Aprazamento.ascx.cs" Inherits="ViverMais.View.Urgencia.Inc_Aprazamento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>

<style type="text/css">
.formulario2
{
  width:750px;
  height:auto;
  margin-left: 5px;
  margin-right:0px;
  padding: 5px 5px 10px 5px;
}

.formulario3
{
  width:670px;
  height:auto;
  margin-left: 5px;
  margin-right:0px;
  padding: 5px 5px 10px 5px;
}

.formulario4
{
  width:610px;
  height:auto;
  margin-left: 2px;
  margin-right:0px;
  padding: 2px 2px 4px 2px;
}

.accordionHeaderAp
{
    border: 1px solid #142126;
    color: #142126;
    background-color: #eee;
    /*font-weight: bold;*/
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
    width: 650px;
    text-align:center;
}

.accordionHeaderSelectedAp
{
    border: 1px solid #142126;
    color: white;
    background-color: #142126;
    /*font-weight: bold;*/
    padding: 5px;
    margin-top: 5px;
    cursor: pointer;
    width: 650px;
    text-align:center;
}

.accordionContentAp
{
    background-color: #fff;
    border: 1px solid #142126;
    border-top: none;
    padding: 5px;
    padding-top: 10px;
    width: 650px;
} 
</style>

<%--<asp:UpdatePanel ID="UpdatePanel_IncAprazar" runat="server" UpdateMode="Conditional">
    <ContentTemplate>--%>
    <fieldset class="formulario2">
    <legend>Prescrição em Vigência para Aprazamento</legend>
    <p>
        <span>
            <asp:GridView ID="GridView_PrescricoesRegistradas" DataKeyNames="Codigo" runat="server"
             AutoGenerateColumns="false"
             Width="600px" OnRowCommand="OnRowCommand_SelecionarPrescricao">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data de Registro" />
                    <asp:BoundField DataField="DescricaoStatus" HeaderText="Status" />
                    <asp:BoundField DataField="UltimaDataValida" HeaderText="Aprazar até" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Selecionar" Text="Selecionar" />
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
            </asp:GridView>
        </span>
    </p>
    </fieldset>

<%--<asp:UpdatePanel ID="UpdatePanel_ItensPrescricao" runat="server" UpdateMode="Always">
    <ContentTemplate>
    <asp:Panel ID="Panel_Um" runat="server" Visible="false">--%>
        <fieldset class="formulario2">
            <legend>Itens da Prescrição</legend>
            
           <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                        HeaderCssClass="accordionHeaderAp" HeaderSelectedCssClass="accordionHeaderSelectedAp"
                        ContentCssClass="accordionContentAp" >
            <Panes>
                <cc1:AccordionPane ID="AccordionPane_Medicamento" runat="server">
                    <Header>Procedimentos</Header>
                    <Content>
<%--                <fieldset class="formulario3">
                <legend>Procedimentos</legend>--%>
                <p>
                    <span>
                        <asp:GridView ID="GridView_Procedimento" DataKeyNames="CodigoProcedimento"
                            OnRowCommand="OnRowCommand_AprazarProcedimento"
                            runat="server" Width="600px" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                <%--<asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração" ItemStyle-HorizontalAlign="Center"/>--%>
                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_Aprazar" Text="Aprazar" ItemStyle-HorizontalAlign="Center" />
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
<%--            </fieldset>--%>
            </Content>
            </cc1:AccordionPane>
            
            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                    <Header>Procedimentos Não-Faturáveis</Header>
                    <Content>
<%--            <fieldset class="formulario3">
                <legend>Procedimentos Não-Faturáveis</legend>--%>
                <p>
                    <span>
                        <asp:GridView ID="GridView_ProcedimentoNaoFaturavel" DataKeyNames="CodigoProcedimento"
                            OnRowCommand="OnRowCommand_AprazarProcedimentoNaoFaturavel"
                            runat="server" Width="600px" AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_Aprazar" Text="Aprazar" ItemStyle-HorizontalAlign="Center" />
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
<%--            </fieldset>--%>
            </Content>
            </cc1:AccordionPane>
<%--            <fieldset class="formulario3">
                <legend>Kits de Medicamentos</legend>
                <p>
                    <span>
                        <asp:GridView ID="gridKit" runat="server" Width="600px" DataKeyNames="CodigoKit"
                         OnRowCommand="OnRowCommand_AprazarKit" OnRowDataBound="OnRowDataBound_FormataGridViewKit"
                         AutoGenerateColumns="False" >
                            <Columns>
                                <asp:BoundField DataField="NomeKitPA" HeaderText="Kit" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração" ItemStyle-HorizontalAlign="Center"/>
                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_Aprazar" Text="Aprazar" />
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
            </fieldset>--%>
            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                    <Header>Medicamentos</Header>
                    <Content>
            <fieldset class="formulario4">
                <legend>Novos Aprazamentos</legend>
                <p>
                    <span>
                        <asp:GridView ID="gridMedicamentos" DataKeyNames="Medicamento"
                         OnRowCommand="OnRowCommand_AprazarMedicamento" OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentos"
                         runat="server" Width="600px"
                         AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="DescricaoObservacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="SuspensoToString" HeaderText="Status" ItemStyle-HorizontalAlign="Center"/>
                                <asp:ButtonField ButtonType="Link" CommandName="CommandName_Aprazar" Text="Aprazar" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum medicamento encontrado para registrar um aprazamento."></asp:Label>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow" />
                        </asp:GridView>
                    </span>
                </p>
                
<%--                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>--%>
                    <%--<asp:Panel ID="Panel_AprazamentoMedicamento" runat="server" Visible="false">--%>
                        <%--<fieldset>
                            <legend>Aprazar</legend>--%>
                            <p>
                                <span class="rotulo">Medicamento</span>
                                <span style="margin-left: 5px;">
                                    <asp:DropDownList ID="DropDownList_MedicamentoAprazar" runat="server" Width="400px">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data</span>
                                <span style="margin-left: 5px;">
                                    <asp:TextBox ID="TextBox_DataAprazarMedicamento" CssClass="campo" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Hora</span>
                                <span style="margin-left: 5px;">
                                    <asp:TextBox ID="TextBox_HoraAprazarMedicamento" CssClass="campo" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p align="center">
                                <span>
                                    <asp:ImageButton ID="Button_AprazarMedicamento1" runat="server" Width="134px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                     OnClick="OnClick_AdicionarMedicamentoAprazamento" ValidationGroup="ValidationGroup_AprazarMedicamento"/>
                                    <asp:ImageButton ID="Button_CancelarAprazamentoMedicamento1" runat="server" Width="134px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                     CausesValidation="false" OnClick="OnClick_CancelarMedicamentoAprazamento" />
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Selecione um medicamento." Operator="GreaterThan" ValueToCompare="-1" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="DropDownList_MedicamentoAprazar"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório." Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_DataAprazarMedicamento"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data com formato inválido." Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_DataAprazarMedicamento"></asp:CompareValidator>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900." Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_DataAprazarMedicamento"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora é Obrigatório." Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento" ControlToValidate="TextBox_HoraAprazarMedicamento"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora com formato inválido." ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraAprazarMedicamento" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamento"></asp:RegularExpressionValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_AprazarMedicamento"/>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                     TargetControlID="TextBox_DataAprazarMedicamento">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataAprazarMedicamento"
                                     InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                    </cc1:MaskedEditExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraAprazarMedicamento"
                                        InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                    </cc1:MaskedEditExtender>
                                </span>
                            </p>
                       <%-- </fieldset>--%>
                    <%--</asp:Panel>--%>
<%--                    <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                        ContentCssClass="accordionContent" >
                        <Panes>
                            <cc1:AccordionPane ID="AccordionPane_Medicamento" runat="server">
                                <Header>Medicamentos Aprazados</Header>
                                <Content>--%>
                                    <%--<asp:GridView ID="GridView_MedicamentosAprazados" runat="server" Width="600px"
                                     OnRowDeleting="OnRowDeleting_DeletarMedicamentoAprazado"
                                     AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ReadOnly="true" />--%>
                                            <%--<asp:BoundField HeaderText="Status" DataField="DescricaoStatus" ReadOnly="true" />--%>
<%--                                            <asp:TemplateField HeaderText="Data de Aplicação">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_DataAplicacao" runat="server" Text='<%#bind("DataAplicacao") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox_DataAplicacao" runat="server" Width="80px"  CssClass="campo" Text='<%#bind("DataAplicacao") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hora de Aplicação">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label_HoraAplicacao" runat="server" Text='<%#bind("HoraAplicacao") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox_HoraAplicacao" runat="server" Width="40px" CssClass="campo" Text='<%#bind("HoraAplicacao") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>--%>
<%--                                            <asp:BoundField HeaderText="Data de Aplicação" DataField="Horario" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" />
                                            <asp:BoundField HeaderText="Hora de Aplicação" DataField="Horario" DataFormatString="{0:HH:mm}" ReadOnly="true" />--%>
<%--                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Editar" CausesValidation="false" runat="server" CommandName="Edit">Editar</asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Alterar" CausesValidation="false" runat="server" CommandName="Update" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_AprazarMedicamentoEdicao')) return confirm('Tem certeza que deseja alterar os dados deste horário aprazamento ?'); return false;" ValidationGroup="ValidationGroup_AprazarMedicamento">Alterar</asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton_Cancelar" CausesValidation="false" runat="server" CommandName="Cancel">Cancelar</asp:LinkButton>
                                                    
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Aplicação é Obrigatório." Display="None" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao" ControlToValidate="TextBox_DataAplicacao"></asp:RequiredFieldValidator>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Aplicação com formato inválido." Operator="DataTypeCheck" Type="Date" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao" ControlToValidate="TextBox_DataAplicacao"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Aplicação deve ser igual ou maior que 01/01/1900." Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao" ControlToValidate="TextBox_DataAplicacao"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Hora de Aplicação é Obrigatório." Display="None" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao" ControlToValidate="TextBox_HoraAplicacao"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Hora de Aplicação com formato inválido." ValidationExpression="^([0-1][0-9]|[2][0-3]):([0-5][0-9])$" ControlToValidate="TextBox_HoraAplicacao" Display="None" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao"></asp:RegularExpressionValidator>
                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_AprazarMedicamentoEdicao"/>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                     TargetControlID="TextBox_DataAplicacao">
                                                    </cc1:CalendarExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataAplicacao"
                                                     InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                                                    </cc1:MaskedEditExtender>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_HoraAplicacao"
                                                        InputDirection="LeftToRight" MaskType="Time" Mask="99:99">
                                                    </cc1:MaskedEditExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>--%>
<%--                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Excluir" runat="server" CommandName="Delete" CausesValidation="false" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este horário de aprazamento ?');">Excluir</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:CommandField ButtonType="Link" ShowDeleteButton="true" DeleteText="Excluir" />--%>
<%--                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Ainda não foi realizado novos aprazamentos para os medicamentos disponíveis."></asp:Label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="tab" />
                                        <RowStyle CssClass="tabrow" />
                                    </asp:GridView>--%>
<%--                                    <p align="center">
                                        <span>
                                            <asp:Button ID="Button_SalvarItensAprazados" runat="server" Text="Salvar Itens Aprazados" CausesValidation="false" OnClick="OnClick_SalvarItensAprazados" />
                                        </span>
                                    </p>--%>
                                    </fieldset>
                                    <fieldset class="formulario4">
                                        <legend>Aprazamentos Registrados</legend>
                                        <p>
                                            <span>
                                                <asp:GridView ID="GridView_AprazamentoMedicamentoRegistrado" AutoGenerateColumns="false"
                                                  runat="server" Width="600px"
                                                   OnRowDeleting="OnRowDeleting_DeletarMedicamentoAprazamentoRegistrado" 
                                                   OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentoJaRegistrados"
                                                   DataKeyNames="CodigoItem">
                                                  <Columns>
                                                    <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ReadOnly="true" />
                                                    <asp:BoundField HeaderText="Status" DataField="DescricaoStatus" ReadOnly="true" />
                                                    <asp:BoundField HeaderText="Horário de Aplicação" DataField="Horario" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ReadOnly="true" />
                                                    <asp:BoundField HeaderText="Profissional Solicitante" DataField="NomeProfissionalSolicitante" ReadOnly="true" />
                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton_Excluir" runat="server" CausesValidation="false" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este horário de aprazamento ?');" CommandName="Delete">Excluir</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  </Columns>
                                                  <EmptyDataRowStyle HorizontalAlign="Center" />
                                                  <EmptyDataTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text="Não foi encontrado registro de aprazamento algum para os medicamentos disponíveis."></asp:Label>
                                                  </EmptyDataTemplate>
                                                  <HeaderStyle CssClass="tab" />
                                                  <RowStyle CssClass="tabrow" />
                                                </asp:GridView>
                                            </span>
                                        </p>
                                    </fieldset>
<%--                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>--%>
<%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
           <%-- </fieldset>--%>
            </Content>
            </cc1:AccordionPane>
            </Panes>
            </cc1:Accordion>
        </fieldset>
<%--        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>--%>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
