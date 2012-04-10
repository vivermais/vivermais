<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormKitPA2.aspx.cs" Inherits="ViverMais.View.Urgencia.FormKitPA2"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 660px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 5px 5px 5px 5px;
        }
        .formulario3
        {
            width: 640px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 5px 5px 5px 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formulário de Cadastro de Kits do PA</h2>
        <fieldset class="formulario">
            <legend>Kit PA</legend>
            <p>
                <cc1:TabContainer ID="TabContainer_Kit" runat="server">
                    <cc1:TabPanel ID="TabPanel_DadosKit" runat="server" HeaderText="Informações">
                        <ContentTemplate>
                            <fieldset class="formulario2">
                                <legend>Informações</legend>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button_Salvar" EventName="Click" />
                                        <%--<asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />--%>
                                        <%--<asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                    </Triggers>
                                    <ContentTemplate>
                                        <p>
                                            <span class="rotulo">Nome</span> <span>
                                                <asp:TextBox ID="TextBox_Nome" runat="server" CssClass="campo" Width="300px"></asp:TextBox>
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <p align="center">
                                    <span>
                                        <asp:ImageButton ID="Button_Salvar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png" Width="134px" Height="38px"
                                         OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_cadKit" />
                                        <asp:ImageButton ID="Button_Cancelar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png" Width="134px" Height="38px" PostBackUrl="~/Urgencia/FormExibeKit.aspx" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é Obrigatório!"
                                            ControlToValidate="TextBox_Nome" Display="None" ValidationGroup="ValidationGroup_cadKit"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="CustomValidator_ItensKit" runat="server" ErrorMessage="CustomValidator"
                                            OnServerValidate="OnServerValidate_ValidaItensKit" Display="None" ValidationGroup="ValidationGroup_cadKit"></asp:CustomValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroup_cadKit" />
                                    </span>
                                </p>
                            </fieldset>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel_ItensKit" runat="server" HeaderText="Itens">
                        <ContentTemplate>
                            <fieldset class="formulario2">
                                <legend>Itens</legend>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Incluídos</legend>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />--%>
                                                    <%--<asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ItensIncluidos" DataKeyNames="CodigoItem" OnRowDataBound="OnRowDataBound_FormataGridViewItensIncluidos"
                                                                runat="server" AutoGenerateColumns="false" Width="600px">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeItem" HeaderText="Nome" />
                                                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                                                    <asp:TemplateField HeaderText="Retirar ?">
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox_Retirar" runat="server" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Disponíveis</legend>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_ProximosItens" EventName="RowDeleting" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ItensDisponiveis" runat="server" AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging_PaginacaoItensDisponiveis"
                                                                PageSize="20" PagerSettings-Mode="Numeric" DataKeyNames="Codigo" Width="600px"
                                                                OnRowCancelingEdit="OnRowCancelingEdit_CancelarInsercaoItem" OnRowEditing="OnRowEditing_InserirItem"
                                                                OnRowUpdating="OnRowUpdating_SalvarItem" AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbQuantidade" runat="server" Text="0"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox_Quantidade" runat="server" Text="0" Width="30px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade é Obrigatório!"
                                                                                ValidationGroup="ValidationGroup_ItemKit" ControlToValidate="TextBox_Quantidade"
                                                                                Display="None"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Quantidade."
                                                                                ValidationGroup="ValidationGroup_ItemKit" ValidationExpression="^\d*$" ControlToValidate="TextBox_Quantidade"
                                                                                Display="None"></asp:RegularExpressionValidator>
                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                                                                                ValidationGroup="ValidationGroup_ItemKit" ControlToValidate="TextBox_Quantidade"
                                                                                Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="ValidationGroup_ItemKit" />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ButtonType="Link" CancelText="Cancelar" EditText="Inserir" ValidationGroup="ValidationGroup_ItemKit"
                                                                        InsertVisible="false" UpdateText="Confirmar" ShowCancelButton="true" ShowEditButton="true" />
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Próximas inclusões</legend>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
<%--                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_ItensDisponiveis" EventName="RowUpdating" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ProximosItens" runat="server" OnRowDataBound="OnRowDataBound_FormataGridViewProximosItens"
                                                                AutoGenerateColumns="false" AllowPaging="true" PageSize="20" Width="600px" PagerSettings-Mode="Numeric"
                                                                OnPageIndexChanging="OnPageIndexChanging_PaginacaoProximosItens" OnRowDeleting="OnRowDeleting_DeletarItemKitProximo">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeItem" HeaderText="Nome" />
                                                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                                                    <asp:CommandField ButtonType="Link" DeleteText="Retirar" ShowDeleteButton="true"
                                                                        InsertVisible="false" />
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                            </fieldset>
                        </ContentTemplate>
                    </cc1:TabPanel>
                    <cc1:TabPanel ID="TabPanel_MedicamentosKit" runat="server" HeaderText="Medicamentos">
                        <ContentTemplate>
                            <fieldset class="formulario2">
                                <legend>Medicamentos</legend>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Incluídos</legend>
                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <asp:GridView ID="GridView_MedicamentosIncluidos" DataKeyNames="CodigoMedicamento"
                                                            OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentosIncluidos" Width="600px"
                                                            runat="server" AutoGenerateColumns="false">
                                                            <Columns>
                                                                <asp:BoundField DataField="NomeMedicamento" HeaderText="Nome" />
                                                                <asp:BoundField DataField="DescricaoMedicamentoPrincipal" HeaderText="Medicamento Principal" />
                                                                <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                                                <asp:TemplateField HeaderText="Retirar ?">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="CheckBox_Retirar" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <HeaderStyle CssClass="tab" />
                                                            <RowStyle CssClass="tabrow" />
                                                        </asp:GridView>
                                                    </p>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Disponíveis</legend>
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_ProximosMedicamentos" EventName="RowDeleting" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_MedicamentosDisponiveis" runat="server" AllowPaging="true"
                                                                OnPageIndexChanging="OnPageIndexChanging_PaginacaoMedicamentosDisponiveis" PageSize="20"
                                                                PagerSettings-Mode="Numeric" DataKeyNames="Codigo" Width="600px" OnRowCancelingEdit="OnRowCancelingEdit_CancelarInsercaoMedicamento"
                                                                OnRowEditing="OnRowEditing_InserirMedicamento" OnRowUpdating="OnRowUpdating_SalvarMedicamento"
                                                                AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Nome" HeaderText="Nome" ReadOnly="true" />
                                                                    <asp:TemplateField HeaderText="Medicamento Principal ?">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbMedicamentoPrincipal" runat="server" Text="Não"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:CheckBox ID="CheckBox_MedicamentoPrincipal" runat="server" />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lbQuantidade" runat="server" Text="0"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <EditItemTemplate>
                                                                            <asp:TextBox ID="TextBox_Quantidade" runat="server" Text="0" Width="30px"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade é Obrigatório!"
                                                                                ValidationGroup="ValidationGroup_MedicamentoKit" ControlToValidate="TextBox_Quantidade"
                                                                                Display="None"></asp:RequiredFieldValidator>
                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Quantidade."
                                                                                ValidationGroup="ValidationGroup_MedicamentoKit" ValidationExpression="^\d*$"
                                                                                ControlToValidate="TextBox_Quantidade" Display="None"></asp:RegularExpressionValidator>
                                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                                                                                ControlToValidate="TextBox_Quantidade" ValidationGroup="ValidationGroup_MedicamentoKit"
                                                                                Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                                                ShowSummary="false" ValidationGroup="ValidationGroup_MedicamentoKit" />
                                                                        </EditItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:CommandField ButtonType="Link" CancelText="Cancelar" EditText="Inserir" ValidationGroup="ValidationGroup_MedicamentoKit"
                                                                        InsertVisible="false" UpdateText="Confirmar" ShowCancelButton="true" ShowEditButton="true" />
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                                <p>
                                    <span>
                                        <fieldset class="formulario3">
                                            <legend>Próximas inclusões</legend>
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Salvar"
                                                        EventName="Click" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="TabContainer_Kit$TabPanel_DadosKit$Button_Cancelar"
                                                        EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_Kits" EventName="SelectedIndexChanged" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="GridView_MedicamentosDisponiveis" EventName="RowUpdating" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <p>
                                                        <span>
                                                            <asp:GridView ID="GridView_ProximosMedicamentos" runat="server" OnRowDataBound="OnRowDataBound_FormataGridViewProximosMedicamentos"
                                                                AutoGenerateColumns="false" AllowPaging="true" PageSize="20" Width="600px" PagerSettings-Mode="Numeric"
                                                                OnPageIndexChanging="OnPageIndexChanging_PaginacaoProximosMedicamentos" OnRowDeleting="OnRowDeleting_DeletarMedicamentoKitProximo">
                                                                <Columns>
                                                                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Nome" />
                                                                    <asp:BoundField DataField="DescricaoMedicamentoPrincipal" HeaderText="Medicamento Principal ?" />
                                                                    <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
                                                                    <asp:CommandField ButtonType="Link" DeleteText="Retirar" ShowDeleteButton="true"
                                                                        InsertVisible="false" />
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
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </fieldset>
                                    </span>
                                </p>
                            </fieldset>
                        </ContentTemplate>
                    </cc1:TabPanel>
                </cc1:TabContainer>
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
