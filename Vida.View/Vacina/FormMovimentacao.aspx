﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMovimentacao.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormMovimentacao" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Vacina/WUCPesquisarLote.ascx" TagPrefix="WUCPrefix_PesquisarLote"
    TagName="WUCName_PesquisarLote" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Movimentação</h2>
        <fieldset class="formulario" style="width: 750px;">
            <legend>Informações</legend>
            <cc1:TabContainer ID="TabContainer1" runat="server">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Dados Gerais">
                    <ContentTemplate>
                        <p>
                            <span class="rotulo">Sala de Vacina </span><span>
                                <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Tipo de Movimentação </span><span>
                                <asp:Label ID="Label_TipoMovimentacao" Font-Bold="true" Font-Size="Smaller" runat="server"
                                    Text=""></asp:Label>
                            </span>
                        </p>
                        <asp:Panel ID="Panel_SituacaoMovimento" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Situação</span><span>
                                    <asp:Label ID="Label_SituacaoMovimento" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_MotivoMovimento" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Motivo</span> <span>
                                    <asp:DropDownList ID="DropDownList_Motivo" runat="server" CssClass="drop" Width="200px"
                                        DataTextField="Nome" DataValueField="Codigo">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_EstabelecimentoSaude" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Estabelecimento de Saúde</span> <span>
                                    <asp:DropDownList ID="DropDownList_EAS" runat="server" CssClass="drop" Width="350px"
                                        DataTextField="NomeFantasia" DataValueField="CNES">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_SalaVacinaDestino" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Sala de Destino</span> <span>
                                    <asp:Label ID="LabelSalaDestino" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"
                                        Visible="false"></asp:Label>
                                    <asp:DropDownList ID="DropDownList_SalaDestino" runat="server" CssClass="drop" Width="350px"
                                        DataTextField="Nome" DataValueField="Codigo">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_ResponsavelEnvio" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Responsável Envio</span> <span>
                                    <asp:Label ID="LabelResponsavelEnvio" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"
                                        Visible="false"></asp:Label>
                                    <asp:TextBox ID="TextBox_ResponsavelEnvio" runat="server" Width="350px" MaxLength="300"
                                        CssClass="campo"></asp:TextBox>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_DataEnvio" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Data Envio</span> <span>
                                    <asp:Label ID="LabelDataEnvio" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"
                                        Visible="false"></asp:Label>
                                    <asp:TextBox ID="TextBox_DataEnvio" Width="100px" runat="server" CssClass="campo"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataEnvio"
                                        InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear">
                                    </cc1:MaskedEditExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBox_DataEnvio"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_Responsavel" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Responsável</span> <span>
                                    <asp:TextBox ID="TextBox_Responsavel" Width="350px" runat="server" MaxLength="300"
                                        CssClass="campo"></asp:TextBox>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_ResponsavelRecebimento" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Responsável Recebimento</span> <span>
                                    <asp:Label ID="LabelResponsavelRecebimento" runat="server" Text="" Font-Bold="true"
                                        Font-Size="Smaller" Visible="false"></asp:Label>
                                    <asp:TextBox ID="TextBox_ResponsavelRecebimento" Width="350px" runat="server" MaxLength="300"
                                        CssClass="campo"></asp:TextBox>
                                </span>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="Panel_DataRecebimento" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Data Recebimento</span> <span>
                                    <asp:Label ID="LabelDataRecebimento" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"
                                        Visible="false"></asp:Label>
                                    <asp:TextBox ID="TextBox_DataRecebimento" Width="100px" runat="server" CssClass="campo"></asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataRecebimento"
                                        InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" UserDateFormat="DayMonthYear">
                                    </cc1:MaskedEditExtender>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_DataRecebimento"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>
                                </span>
                            </p>
                        </asp:Panel>
                        <p>
                            <span class="rotulo">Observação</span>
                        </p>
                        <br />
                        <asp:TextBox ID="TextBox_Observacao" runat="server" CssClass="campo" TextMode="MultiLine"
                            Width="100%" Height="100px" MaxLength="500"></asp:TextBox>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Imunobiológicos">
                    <ContentTemplate>
                        <WUCPrefix_PesquisarLote:WUCName_PesquisarLote ID="WUC_PesquisarLote" runat="server">
                        </WUCPrefix_PesquisarLote:WUCName_PesquisarLote>
                        <asp:UpdatePanel ID="UpdatePanel_LotesPesquisados" runat="server" ChildrenAsTriggers="true"
                            UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel_LotesPesquisados" runat="server" Visible="false">
                                    <fieldset class="formulario">
                                        <legend>Lotes Pesquisados</legend>
                                        <asp:GridView ID="GridView_LotesPesquisados" Width="100%" runat="server" AutoGenerateColumns="false"
                                            OnRowCancelingEdit="OnRowCancelingEdit_Lotes" OnRowEditing="OnRowEditing_Lotes"
                                            DataKeyNames="Codigo" OnRowUpdating="OnRowUpdating_Lotes" AllowPaging="true"
                                            PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Lotes"
                                            OnRowDataBound="OnRowDataBound_Lotes" BackColor="White" BorderColor="#f9e5a9"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
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
                                                <asp:TemplateField HeaderText="Estoque">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label_QuantidadeEstoque" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantidade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text="0" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_Quantidade" Width="20" Text="0" runat="server" CssClass="campo"
                                                            MaxLength="5" ValidationGroup="ValidationGroup_IncluirLote"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade é Obrigatório."
                                                            Display="None" ControlToValidate="TextBox_Quantidade" ValidationGroup="ValidationGroup_IncluirLote"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números positivos em quantidade."
                                                            ValidationExpression="^\d*$" Display="None" ControlToValidate="TextBox_Quantidade"
                                                            ValidationGroup="ValidationGroup_IncluirLote"></asp:RegularExpressionValidator>
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBox_Quantidade"
                                                            ErrorMessage="Quantidade deve ser maior que zero." ValueToCompare="0" Operator="GreaterThan"
                                                            Display="None" ValidationGroup="ValidationGroup_IncluirLote"></asp:CompareValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="ValidationGroup_IncluirLote" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="150px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Text="<img src='img/add-vac.png' border=0 title='Incluir'/>"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Update" ValidationGroup="ValidationGroup_IncluirLote"
                                                            Text="<img src='img/btn_confirmar.png' border=0 title='Confirmar'/>"></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Cancel" Text="<img src='img/btn_cancelar.png' border=0 title='Cancelar'/>"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Nenhum lote encontrado." Font-Bold="true"
                                                    Font-Size="Smaller"></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                        </asp:GridView>
                                    </fieldset>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="LinkButtonAlterarQuantidadeItem" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButtonCancelarAlteracaoItem" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GridView_LotesPesquisados" EventName="RowUpdating" />
                            </Triggers>
                            <ContentTemplate>
                                <fieldset class="formulario">
                                    <legend>Imunobiológicos da Movimentação</legend>
                                    <asp:GridView ID="GridView_Imunos" Width="100%" runat="server" AutoGenerateColumns="false"
                                        DataKeyNames="CodigoLote" OnRowEditing="OnRowEditing_Imunos" OnRowCancelingEdit="OnRowCancelingEdit_Imunos"
                                        OnRowUpdating="OnRowUpdating_Imunos" OnRowDeleting="OnRowDeleting_Imunos" AllowPaging="true"
                                        PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Imunos"
                                        BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" OnRowDataBound="OnRowDataBound_Imunos"
                                        OnSelectedIndexChanging="OnSelectedIndexChanging_Imunos">
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
                                            <asp:TemplateField HeaderText="Quantidade">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label6" runat="server" Text='<%#bind("Quantidade") %>' Font-Bold="true"
                                                        Font-Size="Medium"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox_Quantidade" Width="20" runat="server" Text='<%#bind("Quantidade") %>'
                                                        CssClass="campo" MaxLength="5" ValidationGroup="ValidationGroup_EditarQuantidade"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade é Obrigatório."
                                                        Display="None" ControlToValidate="TextBox_Quantidade" ValidationGroup="ValidationGroup_EditarQuantidade"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números positivos em quantidade."
                                                        ValidationExpression="^\d*$" Display="None" ControlToValidate="TextBox_Quantidade"
                                                        ValidationGroup="ValidationGroup_EditarQuantidade"></asp:RegularExpressionValidator>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_EditarQuantidade" />
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="150px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Editar" runat="server" CommandName="Edit" Text="<img src='img/btn_editar.png' border=0 title='Alterar'/>"></asp:LinkButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="LinkButton_Confirmar" runat="server" CommandName="Update" ValidationGroup="ValidationGroup_EditarQuantidade"
                                                        Text="<img src='img/btn_confirmar.png' border=0 title='Confirmar'/>"></asp:LinkButton>
                                                    <asp:LinkButton ID="LinkButton_Cancelar" runat="server" CommandName="Cancel" Text="<img src='img/btn_cancelar.png' border=0 title='Cancelar'/>"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:ButtonField ButtonType="Link" ItemStyle-Width="80px" CommandName="Delete" Text="<img src='img/excluir_gridview.png' border=0 title='Excluir'/>" />
                                            <asp:ButtonField ButtonType="Link" ItemStyle-Width="80px" CommandName="Select" HeaderText="Editar" Text="<img src='img/editar.png' border=0 alt='Alterar Quantidade?' />" />
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum lote encontrado." Font-Bold="true"
                                                Font-Size="Smaller"></asp:Label>
                                        </EmptyDataTemplate>
                                        <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                        <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                        <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                    </asp:GridView>
                                </fieldset>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Salvar" runat="server" OnClick="OnClick_Salvar" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarMovimentacao')) return confirm('Todos os dados da movimentação estão corretos ?'); return false;">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
            </div>
            <%-- <asp:Panel ID="Panel_ImprimirMovimento" runat="server" Visible="false">--%>
            <div class="botoesroll">
                <asp:LinkButton ID="LinkButton_ImprimirMovimento" Visible="false" runat="server"
                    OnClick="OnClick_ImprimirMovimento">
                        <img id="imgimprimirmovimento" alt="Imprimir Movimento" src="img/imprimir-movimento1.png"
                  onmouseover="imgimprimirmovimento.src='img/imprimir-movimento2.png';"
                  onmouseout="imgimprimirmovimento.src='img/imprimir-movimento1.png';" />
                </asp:LinkButton>
            </div>
            <%--</asp:Panel>--%>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Cancelar" runat="server" PostBackUrl="~/Vacina/FormEscolheDadosMovimentacao.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <asp:CompareValidator ID="CompareValidator_Motivo" runat="server" ErrorMessage="Motivo é Obrigatório."
            Enabled="false" ControlToValidate="TabContainer1$TabPanel1$DropDownList_Motivo"
            Display="None" ValueToCompare="-1" Operator="GreaterThan" ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Observacao" runat="server"
            ErrorMessage="Observação é Obrigatório." Enabled="false" Display="None" ControlToValidate="TabContainer1$TabPanel1$TextBox_Observacao"
            ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator_EstabelecimentoSaude" runat="server" ErrorMessage="Estabelecimento de Saúde é Obrigatório."
            Display="None" ValueToCompare="-1" Operator="GreaterThan" Enabled="false" ControlToValidate="TabContainer1$TabPanel1$DropDownList_EAS"
            ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator_SalaDestino" runat="server" ErrorMessage="Sala de Destino é Obrigatório."
            Display="None" ValueToCompare="-1" Operator="GreaterThan" Enabled="false" ControlToValidate="TabContainer1$TabPanel1$DropDownList_SalaDestino"
            ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Responsavel" runat="server"
            ErrorMessage="Responsável é Obrigatório." Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_Responsavel" Enabled="false"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_ResponsavelEnvio" runat="server"
            ErrorMessage="Responsável Envio é Obrigatório." Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_ResponsavelEnvio" Enabled="false"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataEnvio" runat="server"
            ErrorMessage="Data Envio é Obrigatório." Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_DataEnvio" Enabled="false"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator_DataEnvioCheck" runat="server" ErrorMessage="Data Envio com formato inválido."
            Type="Date" ControlToValidate="TabContainer1$TabPanel1$TextBox_DataEnvio" Enabled="false"
            Operator="DataTypeCheck" Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator_DataEnvioCheck2" runat="server" ErrorMessage="Data Envio deve ser igual ou maior que 01/01/1900."
            Display="None" ControlToValidate="TabContainer1$TabPanel1$TextBox_DataEnvio"
            Enabled="false" Operator="GreaterThan" Type="Date" ValueToCompare="01/01/1900"
            ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_ResponsavelRecebimento" runat="server"
            ErrorMessage="Responsável Recebimento é Obrigatório." Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_ResponsavelRecebimento" Enabled="false"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator_DataRecebimento" runat="server"
            ErrorMessage="Data Recebimento é Obrigatório." Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_DataRecebimento" Enabled="false"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator_DataRecebimentoCheck" runat="server" ErrorMessage="Data Recebimento com formato inválido."
            Type="Date" ControlToValidate="TabContainer1$TabPanel1$TextBox_DataRecebimento"
            Enabled="false" Operator="DataTypeCheck" Display="None" ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator_DataRecebimentoCheck2" runat="server"
            ErrorMessage="Data Recebimento deve ser igual ou maior que 01/01/1900." Display="None"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_DataRecebimento" Enabled="false"
            Operator="GreaterThan" Type="Date" ValueToCompare="01/01/1900" ValidationGroup="ValidationGroup_SalvarMovimentacao"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator_CompararDataEnvioRecebimento" runat="server"
            ErrorMessage="Data envio não pode ser maior que data recebimento." ValidationGroup="ValidationGroup_SalvarMovimentacao"
            Display="None" Type="Date" ControlToCompare="TabContainer1$TabPanel1$TextBox_DataRecebimento"
            ControlToValidate="TabContainer1$TabPanel1$TextBox_DataEnvio" Operator="LessThanEqual"
            Enabled="false">
        </asp:CompareValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
            ShowMessageBox="true" ValidationGroup="ValidationGroup_SalvarMovimentacao" />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
            RenderMode="Inline">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel2$GridView_Imunos" EventName="SelectedIndexChanging" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_AlterarQuantidadeItemMovimento" runat="server" Visible="false">
                    <div id="Div1" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="Div2" visible="false" style="position: fixed; background-color: #f5efcc;
                        background-position: center; background-repeat: no-repeat; top: 100px; left: 25%;
                        width: 500px; height: auto; z-index: 102; background-image: url('img/fundo_mensagem.png');
                        border-right: #e6b626 4px solid; padding-right: 20px; border-top: #e6b626 4px solid;
                        padding-left: 20px; padding-bottom: 30px; border-left: #e6b626 4px solid; color: #000000;
                        padding-top: 20px; border-bottom: #e6b626 4px solid; text-align: justify; font-family: Verdana;">
                        <div style="width: 500px; height: 10px; margin-left: 20px; margin-top: 10px;">
                        </div>
                        <h6>
                            Alterar Quantidade do Imunobiológico
                        </h6>
                        <p class="atencao">
                            Alteração
                            <asp:Label ID="LabelQtdAlteracoes" runat="server" Text=""></asp:Label>
                        </p>
                        
                        <p>
                            <span class="rotulo">Imunobiológico</span> <span>
                                <asp:Label ID="LabelImunoAlteracao" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Fabricante</span> <span>
                                <asp:Label ID="LabelFabricanteAlteracao" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Aplicação</span> <span>
                                <asp:Label ID="LabelAplicacaoAlteracao" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Lote</span> <span>
                                <asp:Label ID="LabelLoteAlteracao" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Validade</span> <span>
                                <asp:Label ID="LabelValidadeAlteracao" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Quantidade</span> <span>
                                <asp:TextBox ID="TextBox_Quantidade" Width="20" runat="server" CssClass="campo" MaxLength="5"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Motivo</span>
                        </p>
                        <br />
<%--                        <p>--%>
                            <asp:TextBox ID="TextBox_Motivo" runat="server" TextMode="MultiLine" CssClass="campo"
                                MaxLength="500" Height="100px" Width="100%"></asp:TextBox>
<%--                        </p>--%>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButtonAlterarQuantidadeItem" runat="server" OnClick="OnClick_AlterarQuantidadeItem"
                                OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_AlterarQuantidadeItem'))
                        return confirm('Usuário, tem certeza que deseja alterar a quantidade do imunobiológico para esta movimentação?'); return false;"><img id="img1" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="this.src='img/btn_salvar2.png';"
                  onmouseout="this.src='img/btn_salvar1.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButtonCancelarAlteracaoItem" runat="server" OnClick="OnClick_CancelarAlteracaoItem">
                    <img id="img2" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="this.src='img/btn_cancelar2.png';"
                  onmouseout="this.src='img/btn_cancelar1.png';" />
                            </asp:LinkButton>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade é Obrigatório."
                            Display="None" ControlToValidate="TextBox_Quantidade" ValidationGroup="ValidationGroup_AlterarQuantidadeItem"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números positivos em quantidade."
                            ValidationExpression="^\d*$" Display="None" ControlToValidate="TextBox_Quantidade"
                            ValidationGroup="ValidationGroup_AlterarQuantidadeItem"></asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                            ValidationGroup="ValidationGroup_AlterarQuantidadeItem" Display="None" ControlToValidate="TextBox_Quantidade"
                            ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Motivo é Obrigatório."
                            Display="None" ControlToValidate="TextBox_Motivo" ValidationGroup="ValidationGroup_AlterarQuantidadeItem"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_AlterarQuantidadeItem" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
