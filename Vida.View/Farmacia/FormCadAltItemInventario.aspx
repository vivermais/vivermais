﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCadAltItemInventario.aspx.cs" Inherits="ViverMais.View.Farmacia.FormCadAltItemInventario" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
<script type="text/javascript" language="javascript">
    function showTooltip(obj)
    {
           if(obj.options[obj.selectedIndex].title == "")
           {
            obj.title = obj.options[obj.selectedIndex].text;
            obj.options[obj.selectedIndex].title =  obj.options[obj.selectedIndex].text;
                for(i =0;i<obj.options.length;i++)
                {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
    }
</script>
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="top">
            <h2>Formulário de Cadastro Medicamento - Inventário</h2>
                <fieldset>
                <legend>Medicamento</legend>
                        <asp:Panel ID="Panel_Alterar" runat="server" Visible="true">
                        <p>
                            <span>
                            <asp:DetailsView ID="DetailsView_ItemInventario"
                                runat="server" Height="100px" Width="125px" AutoGenerateRows="false"
                                OnItemUpdating="OnItemUpdating_Alteracao" 
                                OnModeChanging="OnModeChanging_Modo">
                                <Fields>
                                    <asp:CommandField ButtonType="Link" CancelText="Cancelar" EditText="Editar"
                                        UpdateText="Atualizar" ShowCancelButton="true" ShowEditButton="true" />
                                    <asp:BoundField HeaderText="Medicamento" DataField="Medicamento" ItemStyle-HorizontalAlign="Center" ReadOnly="true" />
                                    <asp:BoundField HeaderText="Fabricante" DataField="Fabricante" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Lote" DataField="Lote" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Validade" DataField="ValidadeLote" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                                    <asp:BoundField HeaderText="Quantidade Estoque" DataField="QtdEstoque" ItemStyle-HorizontalAlign="Center" ReadOnly="true"/>
                                    <asp:TemplateField HeaderText="Quantidade Contada" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Medicamento" runat="server" Text='<%#bind("QtdContada") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_QuantidadeContada" runat="server" Text='<%#bind("QtdContada") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantidade Contada é Obrigatório!" ControlToValidate="TextBox_QuantidadeContada" Display="None" ></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Digite somente números em Quantidade Contada." ControlToValidate="TextBox_QuantidadeContada" Display="None" ValidationExpression="^\d*$" ></asp:RegularExpressionValidator>
                                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:Button ID="Button_FecharJanela" runat="server" Text="Fechar Janela" OnClientClick="parent.parent.GB_hide();" />
                            </span>
                        </p>
                        </asp:Panel>
                        
                        <asp:Panel ID="Panel_Cadastrar" runat="server" Visible="false">
                            <p>
                                <span class="rotulo">Medicamento</span>
                                <span style="margin-left:5px;">
                                    <asp:DropDownList ID="DropDownList_Medicamento" runat="server" Width="300px"
                                        AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaLoteMedicamento">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Lote</span>
                                <span style="margin-left:5px;">
                                    <asp:DropDownList ID="DropDownList_Lote" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_InformacoesLote">
                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <span>
                                    <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent">
                                        <HeaderTemplate>
                                        </HeaderTemplate>
                                        <ContentTemplate></ContentTemplate>
                                        <Panes>
                                            <cc1:AccordionPane ID="AccordionPane_1" runat="server">
                                                <Header>Informações do Lote Selecionado</Header>
                                                <Content>
                                                    <asp:DetailsView ID="DetailsView_InformacaoLote" AutoGenerateRows="false" runat="server" Height="50px" Width="400px">
                                                        <Fields>
                                                            <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ItemStyle-HorizontalAlign="Center"/>
                                                            <asp:BoundField HeaderText="Lote" DataField="Lote" ItemStyle-HorizontalAlign="Center"/>
                                                            <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center"/>
                                                            <asp:BoundField HeaderText="Validade" DataField="Validade" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"/>
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
                            <p>
                                <span class="rotulo">Quantidade Estoque</span>
                                <span style="margin-left:5px;">
                                    <asp:TextBox ID="TextBox_QtdEstoque" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Quantidade Contada</span>
                                <span style="margin-left:5px;">
                                    <asp:TextBox ID="TextBox_QtdContada" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span>
                                    <asp:Button ID="Button_CadastrarLote" runat="server" Text="Salvar" OnClick="OnClick_SalvarItem" ValidationGroup="group_cadMedicamento"/>
                                    <asp:Button ID="Button1" runat="server" Text="Fechar Janela" OnClientClick="parent.parent.GB_hide();" />
                                    
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um Lote." ControlToValidate="DropDownList_Lote" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="group_cadMedicamento"></asp:CompareValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade Estoque é Obrigatório." ControlToValidate="TextBox_QtdEstoque" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Contada é Obrigatório." ControlToValidate="TextBox_QtdContada" Display="None" ValidationGroup="group_cadMedicamento"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Quantidade Estoque deve conter somente números." ControlToValidate="TextBox_QtdEstoque" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento" Display="None"></asp:RegularExpressionValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Quantidade Contada deve conter somente números." ControlToValidate="TextBox_QtdContada" ValidationExpression="\d*" ValidationGroup="group_cadMedicamento" Display="None"></asp:RegularExpressionValidator>

                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_cadMedicamento" ShowMessageBox="true" ShowSummary="false" />
                                </span>
                            </p>
                        </asp:Panel>
                        
    <%--                    <p>
                            <span>
                                <asp:Button ID="Button_FecharJanela" runat="server" Text="Fechar Janela" OnClientClick="parent.parent.GB_hide();" />
                            </span>
                        </p>--%>
            </fieldset>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>