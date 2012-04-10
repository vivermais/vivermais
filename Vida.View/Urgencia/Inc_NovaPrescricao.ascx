<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_NovaPrescricao.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_NovaPrescricao" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>

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
<fieldset class="formulario2">
    <legend>Itens da Prescrição Anterior</legend>
    <p>
        <span>
            <asp:GridView ID="gridPrescricaAnterior" DataKeyNames="Medicamento" 
                runat="server" Width="600px" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração"
                        ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
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
<asp:UpdatePanel ID="UpdatePanel_ItensPrescricao" runat="server" UpdateMode="Always">
    <ContentTemplate>
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
                    <legend>Kits de Medicamentos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridKit" runat="server" Width="600px" DataKeyNames="CodigoKit"
                                AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField DataField="NomeKitPA" HeaderText="Kit" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center" />
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
                    <legend>Medicamentos</legend>
                    <asp:Panel ID="Panel_IncluiMedicamento" runat="server">
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
                            <span>
                                <asp:ImageButton ID="btnAdicionarMedicamento" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                                    Width="134px" Height="38px" Text="add" OnClick="btnAdicionarMedicamentoPrescricao_Click"
                                    ValidationGroup="MedicamentosPrescricao" />
                            </span>
                        </p>
                    </asp:Panel>
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
                                    <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Suspender" Text="Suspender" />
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
                <p>
                    <span>
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar Prescrição" OnClick="btnSalvarPrescricao_Click"
                            CausesValidation="false" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
