<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormVacinacaoCampanha.aspx.cs" Inherits="ViverMais.View.Vacina.FormVacinacaoCampanha"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário de Vacinação de Campanha</h2>
        <fieldset class="formulario">
            <legend>Pesquisa de Campanha</legend>
            <p>
                <span class="rotulo">Data de Início</span><span>
                    <asp:TextBox ID="TextBox_DataInicioCampanha" runat="server" CssClass="campo" Width="100px"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Término</span><span>
                    <asp:TextBox ID="TextBox_DataTerminoCampanha" runat="server" CssClass="campo" Width="100px"></asp:TextBox>
                </span>
            </p>
            <div class="botoesroll"><asp:LinkButton ID="LinkButton_Pesquisar" runat="server" OnClick="OnClick_PesquisarCampanha"
                        ValidationGroup="ValidationGroup_PesquisarCampanha">
			<img id="imgpesquisar" alt="Pesquisar" src="img/btn_pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn_pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn_pesquisar1.png';" />
		    </asp:LinkButton></div>
            <div class="botoesroll"><asp:LinkButton ID="LinkButton_ListarTodasCampanhas" runat="server" OnClick="OnClick_ListarTodasCampanhas"
                        CausesValidation="false">
			<img id="imglistar" alt="Listar Todos" src="img/btn_listar_todos1.png"
                  onmouseover="imglistar.src='img/btn_listar_todos2.png';"
                  onmouseout="imglistar.src='img/btn_listar_todos1.png';" />
		    </asp:LinkButton></div>
            <div class="botoesroll"><asp:LinkButton ID="LinkButton_Voltar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
			<img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" />
			</asp:LinkButton></div>
            <p>
                <span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data de Início é Obrigatório."
                        Display="None" ControlToValidate="TextBox_DataInicioCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Início inválida."
                        Display="None" ControlToValidate="TextBox_DataInicioCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"
                        Operator="DataTypeCheck" Type="Date">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Início deve ser igual ou maior que 01/01/1900."
                        Display="None" ControlToValidate="TextBox_DataInicioCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"
                        ValueToCompare="01/01/1900" Operator="GreaterThanEqual" Type="Date">
                    </asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Data de Término é Obrigatório."
                        Display="None" ValidationGroup="ValidationGroup_PesquisarCampanha" ControlToValidate="TextBox_DataTerminoCampanha"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Início inválida."
                        Display="None" ControlToValidate="TextBox_DataTerminoCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"
                        Operator="DataTypeCheck" Type="Date">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Data de Início deve ser igual ou maior que 01/01/1900."
                        Display="None" ControlToValidate="TextBox_DataTerminoCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"
                        ValueToCompare="01/01/1900" Operator="GreaterThanEqual" Type="Date">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Data de Término deve ser igual ou maior que Data de Início."
                        Display="None" ControlToValidate="TextBox_DataTerminoCampanha" ValidationGroup="ValidationGroup_PesquisarCampanha"
                        ControlToCompare="TextBox_DataInicioCampanha" Operator="GreaterThanEqual" Type="Date">
                    </asp:CompareValidator>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataInicioCampanha"
                        InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_DataInicioCampanha"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataTerminoCampanha"
                        InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox_DataTerminoCampanha"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarCampanha" />
                <p>
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Resultado da Pesquisa</legend>
            <p>

                    &nbsp;<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="LinkButton_Pesquisar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="LinkButton_ListarTodasCampanhas" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="GridView_Campanhas" runat="server" DataKeyNames="Codigo" AutoGenerateColumns="False"
                                OnRowCommand="OnRowCommand_VerificarAcao" Width="690px" AllowPaging="True" 
                                PageSize="20" PagerSettings-Mode="Numeric" 
                                OnPageIndexChanging="OnPageIndexChanging_PaginacaoCampanhas" BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundField HeaderText="Nome" DataField="Nome" 
                                        ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Data de Início" DataField="DataInicio" DataFormatString="{0:dd/MM/yyyy}"
                                        ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Data de Término" DataField="DataFim" DataFormatString="{0:dd/MM/yyyy}"
                                        ItemStyle-HorizontalAlign="Center" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:ButtonField ButtonType="Image" CommandName="CommandName_Selecionar" ImageUrl="~/Vacina/img/select.png" ControlStyle-Height="19px" ControlStyle-Width="19px"
                                        ItemStyle-HorizontalAlign="Center" HeaderText="Selecione aqui" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:ButtonField>
                                </Columns>
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro de campanha encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>

            </p>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton_Pesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton_ListarTodasCampanhas" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GridView_Campanhas" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_InformacoesCampanha" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Informações da Campanha</legend>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:Label ID="Label_NomeCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Início</span> <span>
                                <asp:Label ID="Label_DataInicioCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Término</span> <span>
                                <asp:Label ID="Label_DataTerminoCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Faixa Etária</span> <span>
                                <asp:Label ID="Label_FaixaEtariaCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Sexo</span> <span>
                                <asp:Label ID="Label_SexoCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Meta</span> <span>
                                <asp:Label ID="Label_MetaCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Status</span> <span>
                                <asp:Label ID="Label_StatusCampanha" runat="server" Text="" CssClass="label_itens"></asp:Label>
                            </span>
                        </p>
                    </fieldset></asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton_Pesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton_ListarTodasCampanhas" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="GridView_Campanhas" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_VacinasCampanha" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Vacinas da Campanha</legend>
                        <p>
                            <span>
                                <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                    HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                    ContentCssClass="accordionContent">
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                            <Header>
                                                Novas Vacinas
                                            </Header>
                                            <Content>
                                                <p>
                                                    <span class="rotulo">Vacina</span> <span>
                                                        <asp:DropDownList ID="DropDownList_VacinaCampanha" runat="server" Width="250px" AutoPostBack="true"
                                                            OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFabricantesVacina" DataTextField="Nome"
                                                            DataValueField="Codigo">
                                                            <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </span>
                                                </p>
                                                <p>
                                                    <span class="rotulo">Fabricante</span> <span>
                                                        <asp:DropDownList ID="DropDownList_FabricanteVacinaCampanha" runat="server" Width="250px">
                                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </span>
                                                </p>
                                                <p>
                                                    <span class="rotulo">Quantidade Vacinações</span> <span>
                                                        <asp:TextBox ID="TextBox_QuantidadeAplicacoes" runat="server" CssClass="campo" Width="50px"></asp:TextBox>
                                                    </span>
                                                </p>
                                                <div class="botoesroll"><asp:LinkButton ID="LinkButton_AdicionarVacina" runat="server" OnClick="OnClick_AdicionarVacina" ValidationGroup="ValidationGroup_cadVacinas">
                                                <img id="imgadicionar1" alt="Cancelar" src="img/btn_adicionar1.png"
                                                                  onmouseover="imgadicionar1.src='img/btn_adicionar2.png';"
                                                                  onmouseout="imgadicionar1.src='img/btn_adicionar1.png';" />
                                                </asp:LinkButton></div>
                                                <div class="botoesroll"><asp:LinkButton ID="LinkButton_Cancelar" runat="server" OnClick="OnClick_CancelarAdicaoVacina">
                                                <img id="imgcancelar1" alt="Cancelar" src="img/btn_cancelar1.png"
                                                                  onmouseover="imgcancelar1.src='img/btn_cancelar2.png';"
                                                                  onmouseout="imgcancelar1.src='img/btn_cancelar1.png';" />
                                                </asp:LinkButton></div>
                                                <p>
                                                    <span>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Vacina é Obrigatório."
                                                            Display="None" ControlToValidate="DropDownList_VacinaCampanha" ValueToCompare="-1"
                                                            Operator="GreaterThan" ValidationGroup="ValidationGroup_cadVacinas"></asp:CompareValidator>
                                                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Fabricante é Obrigatório."
                                                            Display="None" ControlToValidate="DropDownList_FabricanteVacinaCampanha" ValueToCompare="-1"
                                                            Operator="GreaterThan" ValidationGroup="ValidationGroup_cadVacinas"></asp:CompareValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Vacinações é Obrigatório."
                                                            ControlToValidate="TextBox_QuantidadeAplicacoes" Display="None" ValidationGroup="ValidationGroup_cadVacinas"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Quantidade Vacinações deve conter somente números."
                                                            ValidationGroup="ValidationGroup_cadVacinas" ControlToValidate="TextBox_QuantidadeAplicacoes"
                                                            Display="None" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
                                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Quantidade Vacinações deve ser maior que zero."
                                                            ControlToValidate="TextBox_QuantidadeAplicacoes" ValueToCompare="0" Operator="GreaterThan"
                                                            ValidationGroup="ValidationGroup_cadVacinas" Display="None"></asp:CompareValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ValidationGroup_cadVacinas"
                                                            ShowMessageBox="true" ShowSummary="false" />
                                                    </span>
                                                </p>
                                            </Content>
                                        </cc1:AccordionPane>
                                        <cc1:AccordionPane ID="AccordionPane3" runat="server">
                                            <Header>
                                                Vacinas Cadastradas
                                            </Header>
                                            <Content>
                                            <br />
                                                <p>
                                                    <span>
                                                        <asp:GridView ID="GridView_VacinasCampanha" runat="server" AutoGenerateColumns="false"
                                                            OnRowDataBound="OnRowDataBound_FormataGridViewVacinas" EnableViewState="true"
                                                            DataKeyNames="Codigo" AllowPaging="true" PageSize="20" OnRowCommand="OnRowCommand_VerificarAcaoItemCampanha"
                                                            PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_PaginacaoVacinas"
                                                            Width="510px" OnRowEditing="OnRowEditing_EditarItemCampanha" OnRowUpdating="OnRowUpdating_AlterarItemCampanha"
                                                            OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicaoItemCampanha" BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                                            <Columns>
                                                                <asp:BoundField HeaderText="Vacina" DataField="NomeVacina" ReadOnly="true" />
                                                                <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ReadOnly="true" />
                                                                <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="Label_Quantidade" runat="server" Text='<%#bind("Quantidade") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="TextBox_Quantidade" Width="30" CssClass="campo" runat="server" Text='<%#bind("Quantidade") %>'></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantidade é Obrigatório!"
                                                                            ControlToValidate="TextBox_Quantidade" Display="None"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento."
                                                                            ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None"></asp:RegularExpressionValidator>
                                                                        <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                                                                            ControlToValidate="TextBox_Quantidade" ValueToCompare="0" Operator="GreaterThan"
                                                                            Display="None"></asp:CompareValidator>
                                                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                                            ShowSummary="false" />
                                                                    </EditItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:CommandField HeaderText="Editar" ButtonType="Image" CancelText="Cancelar" UpdateText="Atualizar"                                                                    InsertVisible="false" ShowCancelButton="true" ShowEditButton="true" EditImageUrl="~/Vacina/img/editar.png" ControlStyle-Height="16px" ControlStyle-Width="16px" />                                                                <asp:ButtonField HeaderText="Excluir" ButtonType="Image" CausesValidation="true" CommandName="CommandName_Excluir" ImageUrl="~/Vacina/img/excluir_gridview.png" ControlStyle-Height="19px" ControlStyle-Width="19px" />                                                            </Columns>
                                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                                            <EmptyDataTemplate>
                                                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado." Font-Bold="true" Font-Names="Verdana"></asp:Label>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </span>
                                                </p>
                                            </Content>
                                        </cc1:AccordionPane>
                                    </Panes>
                                </cc1:Accordion>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
