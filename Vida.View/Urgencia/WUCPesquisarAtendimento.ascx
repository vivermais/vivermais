<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPesquisarAtendimento.ascx.cs"
    Inherits="ViverMais.View.Urgencia.WUCPesquisarAtendimento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset class="formulario">
    <legend>Pesquisar Atendimentos</legend>
    <cc1:Accordion ID="Accordion1" runat="server" RequireOpenedPane="false" HeaderCssClass="accordionHeader"
        HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent"
        SelectedIndex="1">
        <HeaderTemplate>
        </HeaderTemplate>
        <ContentTemplate>
        </ContentTemplate>
        <Panes>
            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                <Header>
                    Número
                </Header>
                <Content>
                    <p>
                        <span class="rotulo">Número de Atendimento</span> <span>
                            <asp:TextBox ID="TextBox_NumeroAtendimento" runat="server" CssClass="campo" Width="90px"
                                MaxLength="10"></asp:TextBox>
                        </span>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Font-Size="XX-Small"
                            runat="server" ErrorMessage="Digite somente números para o Número de Atendimento."
                            ValidationGroup="ValidationGroup_PesquisarAtendimento" Display="None" ValidationExpression="\d*"
                            ControlToValidate="TextBox_NumeroAtendimento"></asp:RegularExpressionValidator>
                    </p>
                </Content>
            </cc1:AccordionPane>
            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                <Header>
                    Período de Atendimento
                </Header>
                <Content>
                    <p>
                        <span class="rotulo">Data Inicial</span> <span>
                            <asp:TextBox ID="TextBox_DataInicialAtendimento" runat="server" CssClass="campo"
                                Width="100px"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data Final</span> <span>
                            <asp:TextBox ID="TextBox_DataFinalAtendimento" runat="server" CssClass="campo" Width="100px"></asp:TextBox>
                        </span>
                    </p>
                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_DataInicialAtendimento"
                        Format="dd/MM/yyyy" runat="server">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataInicialAtendimento"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                        Display="None" ErrorMessage="Data Inicial Inválida!" ControlToValidate="TextBox_DataInicialAtendimento"
                        Operator="DataTypeCheck" Type="Date" ValidationGroup="ValidationGroup_PesquisarAtendimento"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                        Display="None" ErrorMessage="Data Inicial deve ser igual ou maior que 01/01/1900!"
                        ControlToValidate="TextBox_DataInicialAtendimento" Type="Date" Operator="GreaterThanEqual"
                        ValueToCompare="01/01/1900" ValidationGroup="ValidationGroup_PesquisarAtendimento"></asp:CompareValidator>
                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_DataFinalAtendimento"
                        Format="dd/MM/yyyy" runat="server">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataFinalAtendimento"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" UserDateFormat="DayMonthYear">
                    </cc1:MaskedEditExtender>
                    <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
                        Display="None" ErrorMessage="Data Final Inválida!" ControlToValidate="TextBox_DataFinalAtendimento"
                        Operator="DataTypeCheck" Type="Date" ValidationGroup="ValidationGroup_PesquisarAtendimento"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ValidationGroup="ValidationGroup_PesquisarAtendimento"
                        Display="None" ControlToValidate="TextBox_DataFinalAtendimento" ControlToCompare="TextBox_DataInicialAtendimento"
                        Type="Date" Operator="GreaterThanEqual" ErrorMessage="Data Final deve ser igual ou maior que Data Inicial."></asp:CompareValidator>
                </Content>
            </cc1:AccordionPane>
        </Panes>
    </cc1:Accordion>
    <br />
    <div class="botoesroll">
        <asp:LinkButton ID="LinkButton_Pesquisar" runat="server" ValidationGroup="ValidationGroup_PesquisarAtendimento">
        <img id="imgpesquisar" alt="Buscar" src="img/bts/btn_buscar1.png"
                  onmouseover="imgpesquisar.src='img/bts/btn_buscar2.png';"
                  onmouseout="imgpesquisar.src='img/bts/btn_buscar1.png';" />
        </asp:LinkButton>
    </div>
    <%--<br />--%>
    <div class="botoesroll">
        <asp:LinkButton ID="LinkButton_ListarTodos" runat="server">
            <img id="imglistartodos" alt="Buscar" src="img/bts/listar-todos1.png"
                  onmouseover="imglistartodos.src='img/bts/listar-todos2.png';"
                  onmouseout="imglistartodos.src='img/bts/listar-todos1.png';" />
        </asp:LinkButton>
    </div>
    <asp:CustomValidator ID="CustomValidator_PesquisarAtendimento" runat="server" ErrorMessage="Informe o Número ou Período de Atendimento."
        ValidationGroup="ValidationGroup_PesquisarAtendimento" Display="None" OnServerValidate="OnServerValidate_PesquisarAtendimento"></asp:CustomValidator>
    <asp:ValidationSummary ID="ValidationSummary_PesquisarAtendimento" runat="server"
        ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarAtendimento" />
    <asp:UpdatePanel ID="UpdatePanel_BuscaRegistro" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="true">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LinkButton_Pesquisar" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="LinkButton_ListarTodos" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel_ResultadoPesquisa" runat="server" Visible="false">
                <br />
                <br />
                <br />
                <p>
                    <asp:GridView ID="GridView_PacienteUrgence" runat="server" AutoGenerateColumns="false"
                        AllowPaging="true" Width="100%" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_PacienteUrgence"
                        DataKeyNames="Codigo">
                        <Columns>
                            <asp:CommandField HeaderText="Selecionar" ButtonType="Link" ShowSelectButton="true"
                                SelectText="<img src='../img/bts/select.png' alt='' />" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Número" DataField="NumeroToString" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Paciente" DataField="NomePacienteToString" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Data de Atendimento" DataField="Data" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <HeaderStyle CssClass="tab" />
                        <RowStyle CssClass="tabrow" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Nenhum atendimento encontrado."></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </p>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
