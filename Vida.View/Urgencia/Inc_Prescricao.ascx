<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_Prescricao.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_Prescricao" %>
<%--<%@ OutputCache Duration="1" VaryByParam="none" VaryByCustom="browser" %>--%>

<style type="text/css">
    .formulario2
    {
        width: 690px;
        height: auto;
        margin-left: 5px;
        margin-right: 0px;
        padding: 5px 5px 10px 5px;
    }
    .formulario3
    {
        width: 670px;
        height: auto;
        margin-left: 5px;
        margin-right: 0px;
        padding: 5px 5px 10px 5px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel_ItensPrescricao" runat="server" UpdateMode="Always">
    <ContentTemplate>--%>
        <fieldset class="formulario2">
            <legend>Prescrições Registradas</legend>
            <p>
                <span>
                    <asp:GridView ID="GridView_PrescricoesRegistradas" DataKeyNames="Codigo" runat="server"
                        AutoGenerateColumns="false" Width="600px" AllowPaging="true" PageSize="20" OnPageIndexChanging="OnPageIndexChanging_PaginacaoPrescricao"
                        OnRowCommand="OnRowCommand_SelecionarPrescricao">
                        <Columns>
                            <asp:BoundField DataField="Data" HeaderText="Data de Registro" />
                            <asp:BoundField DataField="DescricaoStatus" HeaderText="Status" />
                            <asp:BoundField DataField="UltimaDataValida" HeaderText="Aprazar até" DataFormatString="{0:dd/MM/yyyy hh:MM}" />
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
        <asp:Panel ID="Panel_Um" runat="server" Visible="true">
            <fieldset class="formulario2">
                <legend>Itens da Prescrição</legend>
                <fieldset class="formulario3">
                    <legend>Procedimentos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Procedimento" DataKeyNames="CodigoProcedimento" runat="server"
                                Width="600px" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-Width="50px"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração"
                                        ItemStyle-HorizontalAlign="Center" />
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
                </fieldset>
                <fieldset class="formulario3">
                    <legend>Procedimentos Não Faturáveis</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridProcedimentoNaoFaturavel" runat="server" Width="600px" DataKeyNames="Codigo"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NomeProcedimento" HeaderText="Kit" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center" />
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
                </fieldset>
                <fieldset class="formulario3">
                    <legend>Medicamentos</legend>
                    <asp:Panel ID="Panel_IncluiMedicamento" runat="server">
                        <asp:UpdatePanel ID="UpdatePanel_Onze" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Medicamentos:</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="ddlMedicamentos" runat="server">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                            ControlToValidate="ddlMedicamentos" ValidationGroup="MedicamentosPrescricao"
                                            InitialValue="0"></asp:RequiredFieldValidator>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Intervalo:</span> <span style="margin-left: 5px;">
                                        <asp:TextBox ID="tbxIntervaloMedicamento" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                            ControlToValidate="tbxIntervaloMedicamento" ValidationGroup="MedicamentosPrescricao"></asp:RequiredFieldValidator>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Via de Administração</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamento" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Forma de Administração</span>
                            <asp:UpdatePanel ID="UpdatePanel_FormaAdministracao" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Onze$DropDownList_ViaAdministracaoMedicamento"
                                        EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>
                                    <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="DropDownList_FormaAdministracaoMedicamento" runat="server">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel_Dezesseis" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                                        <asp:TextBox ID="TextBox_ObservacaoPrescricaoMedicamento" Width="620px" Height="110px"
                                            Rows="20" Columns="5" TextMode="MultiLine" runat="server"></asp:TextBox>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span>
                                <asp:ImageButton ID="btnAdicionarMedicamento" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                    Width="134px" Height="38px" Text="add" OnClick="btnAdicionarMedicamentoPrescricao_Click"
                                    ValidationGroup="MedicamentosPrescricao" />
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:UpdatePanel ID="UpdatePanel_Dezoito" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                        <ContentTemplate>
                                            <p>
                        <span>
                            <asp:GridView ID="gridMedicamentos" DataKeyNames="Medicamento" OnRowCommand="OnRowCommand_SuspenderMedicamento"
                                runat="server" Width="600px" AutoGenerateColumns="False" OnRowDataBound="gridMedicamentos_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Observacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Suspender" Text="Suspender"
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </fieldset>
        </asp:Panel>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
