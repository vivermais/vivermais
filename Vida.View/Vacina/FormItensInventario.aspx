﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormItensInventario.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormItensInventario" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Vacina/WUCPesquisarLote.ascx" TagName="TagName_PesquisarLote"
    TagPrefix="TagPrefix_PesquisarLote" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <%-- <script type="text/javascript" language="javascript">
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
    <div id="top">
        <h2>
            Lista de Vacinas do Inventário</h2>
        <fieldset class="formulario" style="width: 800px">
            <legend>Inventário</legend>
            <%-- <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Unidade</span> <span>
                    <asp:Label ID="Label_UnidadeSalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span> <span>
                    <asp:Label ID="Label_DataInventario" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                </span>
            </p>--%>
            <%--            <br />--%>
            <p>
                <span>
                    <cc1:TabContainer ID="TabContainer_ItensInventario" runat="server" Width="750px">
                        <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Informações">
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Sala de Vacina</span> <span>
                                        <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Unidade</span> <span>
                                        <asp:Label ID="Label_UnidadeSalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Data de Abertura</span> <span>
                                        <asp:Label ID="Label_DataInventario" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Imunobiológicos">
                            <ContentTemplate>
                                <TagPrefix_PesquisarLote:TagName_PesquisarLote ID="WUC_PesquisarLote" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel_LotesPesquisados" runat="server" ChildrenAsTriggers="true"
                                    UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel_ResultadoPesquisa" runat="server" Visible="false">
                                            <fieldset class="formulario">
                                                <legend>Lotes Pesquisados</legend>
                                                <p>
                                                    <span>
                                                        <asp:GridView ID="GridView_LotesPesquisados" Width="100%" runat="server" AutoGenerateColumns="false"
                                                            OnRowCancelingEdit="OnRowCancelingEdit_LotesPesquisados" OnRowEditing="OnRowEditing_LotesPesquisados"
                                                            OnRowUpdating="OnRowUpdating_LotesPesquisados" OnPageIndexChanging="OnPageIndexChanging_LotesPesquisados"
                                                            AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric" BackColor="White"
                                                            DataKeyNames="Codigo" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                                                            CellPadding="3" GridLines="Horizontal">
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Imunobiológico" DataField="NomeVacina" ItemStyle-Width="200px"
                                                                    ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-Width="200px"
                                                                    ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Aplicação" DataField="AplicacaoVacina" ItemStyle-Width="130px"
                                                                    ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Lote" DataField="Identificacao" ItemStyle-Width="100px"
                                                                    ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Validade" DataField="DataValidade" DataFormatString="{0:dd/MM/yyy}"
                                                                    ReadOnly="true" />
                                                                <asp:TemplateField HeaderText="Qtd Estoque">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label_QtdEstoque" runat="server" Text="0"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox_QtdEstoque" runat="server" Text="0" Width="30px" MaxLength="6"
                                                                            CssClass="campo"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Qtd Contada">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label_QtdContada" runat="server" Text="0"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox_QtdContada" runat="server" Text="0" Width="30px" MaxLength="6"
                                                                            CssClass="campo"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade Estoque é Obrigatório."
                                                                            ControlToValidate="TextBox_QtdEstoque" Display="None" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Contada é Obrigatório."
                                                                            ControlToValidate="TextBox_QtdContada" Display="None" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Quantidade Estoque deve conter somente números."
                                                                            ControlToValidate="TextBox_QtdEstoque" ValidationExpression="\d*" Display="None"
                                                                            ValidationGroup="ValidationGroup_cadVacina"></asp:RegularExpressionValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Quantidade Contada deve conter somente números."
                                                                            ControlToValidate="TextBox_QtdContada" ValidationExpression="\d*" Display="None"
                                                                            ValidationGroup="ValidationGroup_cadVacina"></asp:RegularExpressionValidator>
                                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantidade Contada deve ser maior que zero."
                                                                            ControlToValidate="TextBox_QtdContada" ValueToCompare="0" Operator="GreaterThan"
                                                                            Display="None" ValidationGroup="ValidationGroup_cadVacina"></asp:CompareValidator>
                                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                                            ShowSummary="false" ValidationGroup="ValidationGroup_cadVacina" />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="150px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton_Editar" runat="server" CausesValidation="true" CommandName="Edit"
                                                                            Text="<img src='img/add-vac.png' border=0 title='Incluir'/>"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton_Confirmar" runat="server" CausesValidation="true"
                                                                            OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadVacina')) return confirm('Tem certeza que deseja incluir este lote no inventário ?');return false;"
                                                                            CommandName="Update" Text="<img src='img/btn_confirmar.png' border=0 title='Confirmar'/>"></asp:LinkButton>
                                                                        <asp:LinkButton ID="LinkButton_Cancelar" runat="server" CausesValidation="false"
                                                                            CommandName="Cancel" Text="<img src='img/btn_cancelar.png' border=0 title='Cancelar'/>"></asp:LinkButton>
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                                Height="24px" Font-Size="13px" />
                                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                            </EmptyDataTemplate>
                                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                                        </asp:GridView>
                                                    </span>
                                                </p>
                                            </fieldset>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_LotesPesquisados" EventName="RowUpdating" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <fieldset class="formulario">
                                            <legend>Vacinas do inventário</legend>
                                            <p>
                                                <span>
                                                    <asp:GridView ID="GridView_Itens" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo"
                                                        AllowPaging="true" Width="100%" OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicao"
                                                        OnRowEditing="OnRowEditing_EditarRegistro" OnRowUpdating="OnRowUpdating_AlterarRegistro"
                                                        OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="10" PagerSettings-Mode="Numeric"
                                                        BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                                        CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Vacina" DataField="NomeVacina" ReadOnly="true" />
                                                            <asp:BoundField DataField="UnidadeMedidaVacina" HeaderText="Unidade Medida" ReadOnly="true"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="NomeFabricanteVacina" HeaderText="Fabricante" ReadOnly="true"
                                                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="Aplicação" DataField="AplicacaoVacina" ReadOnly="true" />
                                                            <asp:BoundField DataField="IdentificacaoLote" HeaderText="Lote" ReadOnly="true" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="ValidadeLote" HeaderText="Validade" DataFormatString="{0:dd/MM/yyyy}"
                                                                ReadOnly="true" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="QtdEstoque" HeaderText="Qtd Estoque" ReadOnly="true" HeaderStyle-HorizontalAlign="Center"
                                                                ItemStyle-HorizontalAlign="Center">
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            </asp:BoundField>
                                                            <asp:TemplateField HeaderText="Qtd Contada">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_QtdContada" runat="server" Text='<%#bind("QtdContada") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_QtdContada" Width="30px" CssClass="campo" runat="server"
                                                                        MaxLength="6" Text='<%#bind("QtdContada") %>'></asp:TextBox>
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
                                                            <asp:CommandField ButtonType="Link" CancelText="<img src='img/btn_cancelar.png' border=0 title='Cancelar'/>"
                                                                UpdateText="<img src='img/btn_confirmar.png' border=0 title='Confirmar'/>" EditText="<img src='img/btn_editar.png' border=0 title='Alterar'/>"
                                                                InsertVisible="false" ShowEditButton="true" />
                                                        </Columns>
                                                        <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                            Height="24px" Font-Size="13px" />
                                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                                        <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </span>
                                            </p>
                                        </fieldset>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--<asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LknButton_CadastrarLote" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton1" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <p>
                                            <span class="rotulo">Imunobiológico</span> <span>
                                                <asp:DropDownList ID="DropDownList_Vacina" runat="server" Width="300px" AutoPostBack="true"
                                                    OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaLoteVacina">
                                                </asp:DropDownList>
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="DropDownList_Vacina" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton_CadastrarLote" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton1" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <p>
                                            <span class="rotulo">Lote</span> <span>
                                                <asp:DropDownList ID="DropDownList_Lote" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_InformacoesLote">
                                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LknButton_CadastrarLote" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton1" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <p>
                                            <span class="rotulo">Quantidade Estoque</span> <span>
                                                <asp:TextBox ID="TextBox_QtdEstoque" CssClass="campo" runat="server"></asp:TextBox>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Quantidade Contada</span> <span>
                                                <asp:TextBox ID="TextBox_QtdContada" CssClass="campo" runat="server"></asp:TextBox>
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Um$DropDownList_Lote" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton_CadastrarLote" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="LknButton1" EventName="Click" />
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
                                                                    Height="50px" Width="490px">
                                                                    <Fields>
                                                                        <asp:BoundField HeaderText="Vacina" DataField="NomeVacina" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="Lote" DataField="Identificacao" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="Aplicação" DataField="AplicacaoVacina" ItemStyle-HorizontalAlign="Center" />
                                                                        <asp:BoundField HeaderText="Validade" DataField="DataValidade" DataFormatString="{0:dd/MM/yyyy}"
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
                                <asp:LinkButton ID="LknButton_CadastrarLote" runat="server" OnClick="OnClick_SalvarItem"
                                    ValidationGroup="group_cadMedicamento">
                  <img id="imgincluir" alt="Adicionar" src="img/btn_adicionar1.png"
                  onmouseover="imgincluir.src='img/btn_adicionar2.png';"
                  onmouseout="imgincluir.src='img/btn_adicionar1.png';" /></asp:LinkButton>
                                <asp:LinkButton ID="LknButton1" runat="server" OnClick="OnClick_CancelarCadastro">
                  <img id="imgcancelar2" alt="Voltar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar2.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar2.src='img/btn_cancelar1.png';" /></asp:LinkButton>
                                <p align="center">
                                    <span>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um Lote."
                                            ControlToValidate="DropDownList_Lote" ValueToCompare="-1" Operator="GreaterThan"
                                            Display="None" ValidationGroup="group_cadMedicamento"></asp:CompareValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade Estoque é Obrigatório."
                                            ControlToValidate="TextBox_QtdEstoque" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Contada é Obrigatório."
                                            ControlToValidate="TextBox_QtdContada" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Quantidade Estoque deve conter somente números."
                                            ControlToValidate="TextBox_QtdEstoque" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento"
                                            Display="None"></asp:RegularExpressionValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Quantidade Contada deve conter somente números."
                                            ControlToValidate="TextBox_QtdContada" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento"
                                            Display="None"></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_cadMedicamento"
                                            ShowMessageBox="true" ShowSummary="false" />
                                    </span>
                                </p>--%>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="LknButton_Cancelar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgcancelar" alt="Voltar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn_cancelar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="LknButton_FecharInventario" runat="server" OnClick="OnClick_FecharInventario">
                  <img id="imgfecharinvent" alt="Fechar Inventário" src="img/btn_close_invent1.png"
                  onmouseover="imgfecharinvent.src='img/btn_close_invent2.png';"
                  onmouseout="imgfecharinvent.src='img/btn_close_invent1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
    </div>
</asp:Content>
