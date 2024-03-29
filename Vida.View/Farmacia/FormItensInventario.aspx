﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensInventario.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormItensInventario" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript" language="javascript">
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
    </script>

    <style type="text/css">
        .formulario2
        {
            width: 640px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 10px 10px 20px 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Lista de Medicamentos do Inventário</h2>
        <fieldset class="formulario">
            <legend>Inventário</legend>
            <p>
                <span class="rotulo">Farmácia</span> <span style="margin-left: 5px;">
                    <asp:Label ID="Label_Farmacia" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span> <span style="margin-left: 5px;">
                    <asp:Label ID="Label_DataInventario" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span>
                    <cc1:TabContainer ID="TabContainer_ItensInventario" runat="server">
                        <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Medicamentos">
                            <ContentTemplate>
                                <fieldset class="formulario2">
                                    <legend>Medicamentos</legend>
                                    <%--                    <p>
                                    <span>
                                        <asp:Button ID="Button_AtualizarLista" runat="server" Text="Atualizar Lista Abaixo" OnClick="OnClick_AtualizarItens" />
                                        <asp:Button ID="Button_CadastrarMedicamento" runat="server" Text="Cadastrar Medicamento" />
                                    </span>
                                </p>--%>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="TabPanel_Dois$Button_CadastrarLote" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <span>
                                                    <asp:GridView ID="GridView_Itens" runat="server" AutoGenerateColumns="False" DataKeyNames="CodigoLote"
                                                        AllowPaging="true" Width="600px" OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicao"
                                                        OnRowEditing="OnRowEditing_EditarRegistro" OnRowUpdating="OnRowUpdating_AlterarRegistro"
                                                        OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Medicamento" DataField="Medicamento" ReadOnly="true" />
                                                            <%--                                <asp:TemplateField HeaderText="Medicamento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" Text='<%#bind("Medicamento") %>' ></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                            <asp:BoundField DataField="UnidadeMedicamento" HeaderText="Unidade Medida" ReadOnly="true"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" ReadOnly="true" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="Lote" HeaderText="Lote" ReadOnly="true" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QtdEstoque" HeaderText="Qtd Estoque" ReadOnly="true" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <%--                                <asp:BoundField DataField="QtdContada" HeaderText="Qtd Contada"
                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>--%>
                                                            <asp:TemplateField HeaderText="Qtd Contada">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_QtdContada" runat="server" Text='<%#bind("QtdContada") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_QtdContada" Width="30px" CssClass="campo" runat="server"
                                                                        Text='<%#bind("QtdContada") %>' MaxLength="6"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Qtd Contada é Obrigatório!"
                                                                        Display="None" ControlToValidate="TextBox_QtdContada"></asp:RequiredFieldValidator>
                                                                    <asp:CompareValidator ID="CompareValidator_1" runat="server" ErrorMessage="Qtd Contada deve ser igual ou maior que zero."
                                                                        ControlToValidate="TextBox_QtdContada" Display="None" ValueToCompare="0" Operator="GreaterThanEqual"></asp:CompareValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Qtd Contada."
                                                                        Display="None" ValidationExpression="^\d*$" ControlToValidate="TextBox_QtdContada"></asp:RegularExpressionValidator>
                                                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                                        ShowSummary="false" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Link" CancelText="Cancelar" UpdateText="Alterar" EditText="Editar"
                                                                InsertVisible="false" ShowEditButton="true" />
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum medicamento encontrado."></asp:Label>
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
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Incluir Medicamentos">
                            <ContentTemplate>
                                <fieldset class="formulario2">
                                    <legend>Lote de Medicamento</legend>
                                    <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Button_CadastrarLote" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <span class="rotulo">Medicamento</span> <span style="margin-left: 5px;">
                                                    <asp:DropDownList ID="DropDownList_Medicamento" runat="server" Width="300px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaLoteMedicamento">
                                                    </asp:DropDownList>
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="DropDownList_Medicamento" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="Button_CadastrarLote" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <span class="rotulo">Lote</span> <span style="margin-left: 5px;">
                                                    <asp:DropDownList ID="DropDownList_Lote" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_InformacoesLote">
                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Um$DropDownList_Lote" EventName="SelectedIndexChanged" />
                                            <asp:AsyncPostBackTrigger ControlID="Button_CadastrarLote" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <p>
                                                <span>
                                                    <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                        ContentCssClass="accordionContent">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ContentTemplate>
                                                        </ContentTemplate>
                                                        <Panes>
                                                            <cc1:AccordionPane ID="AccordionPane_1" runat="server">
                                                                <Header>
                                                                    Informações do Lote Selecionado</Header>
                                                                <Content>
                                                                    <asp:DetailsView ID="DetailsView_InformacaoLote" AutoGenerateRows="false" runat="server"
                                                                        Height="50px" Width="400px">
                                                                        <Fields>
                                                                            <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField HeaderText="Lote" DataField="Lote" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center" />
                                                                            <asp:BoundField HeaderText="Validade" DataField="Validade" DataFormatString="{0:dd/MM/yyyy}"
                                                                                ItemStyle-HorizontalAlign="Center" />
                                                                        </Fields>
                                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                        <EmptyDataTemplate>
                                                                            <asp:Label ID="Label1" runat="server" Text="Nenhum lote selecionado."></asp:Label>
                                                                        </EmptyDataTemplate>
                                                                        <HeaderStyle CssClass="tab" />
                                                                        <RowStyle CssClass="tabrow" />
                                                                    </asp:DetailsView>
                                                                </Content>
                                                            </cc1:AccordionPane>
                                                        </Panes>
                                                    </cc1:Accordion>
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Button_CadastrarLote" EventName="Click" />
                                            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                                        </Triggers>
                                        <ContentTemplate>
                                           <%-- <p>
                                                <span class="rotulo">Quantidade Estoque</span> <span style="margin-left: 5px;">
                                                    <asp:TextBox ID="TextBox_QtdEstoque" CssClass="campo" runat="server"></asp:TextBox>
                                                </span>
                                            </p>--%>
                                            <p>
                                                <span class="rotulo">Quantidade Contada</span> <span style="margin-left: 5px;">
                                                    <asp:TextBox ID="TextBox_QtdContada" CssClass="campo" runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <p align="center">
                                        <span>
                                            <asp:Button ID="Button_CadastrarLote" runat="server" Text="Incluir" OnClick="OnClick_SalvarItem"
                                                ValidationGroup="group_cadMedicamento" />
                                            <asp:Button ID="Button1" runat="server" Text="Cancelar" OnClick="OnClick_CancelarCadastro" />
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um Lote."
                                                ControlToValidate="DropDownList_Lote" ValueToCompare="-1" Operator="GreaterThan"
                                                Display="None" ValidationGroup="group_cadMedicamento"></asp:CompareValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade Estoque é Obrigatório."
                                                ControlToValidate="TextBox_QtdEstoque" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Contada é Obrigatório."
                                                ControlToValidate="TextBox_QtdContada" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Quantidade Estoque deve conter somente números."
                                                ControlToValidate="TextBox_QtdEstoque" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento"
                                                Display="None"></asp:RegularExpressionValidator>--%>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Quantidade Contada deve conter somente números."
                                                ControlToValidate="TextBox_QtdContada" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento"
                                                Display="None"></asp:RegularExpressionValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantidade Contada deve ser maior que zero."
                                                ControlToValidate="TextBox_QtdContada" ValueToCompare="0" Operator="GreaterThan"
                                                Display="None" ValidationGroup="group_cadMedicamento"></asp:CompareValidator>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_cadMedicamento"
                                                ShowMessageBox="true" ShowSummary="false" />
                                        </span>
                                    </p>
                                </fieldset>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </span>
            </p>
            <p align="center">
                <span>
                    <asp:Button ID="Button_Cancelar" runat="server" Text="Voltar" PostBackUrl="~/Farmacia/Default.aspx" />
                    <asp:Button ID="Button_FecharInventario" runat="server" Text="Fechar Inventário"
                        OnClick="OnClick_FecharInventario" />
                </span>
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
