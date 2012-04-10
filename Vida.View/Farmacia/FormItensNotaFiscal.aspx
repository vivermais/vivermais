<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensNotaFiscal.aspx.cs" Inherits="ViverMais.View.Farmacia.FormItensNotaFiscal" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.formulario2
{
  width:670px;
  height:auto;
  margin-left: 5px;
  margin-right:5px;
  padding: 10px 10px 20px 10px;
}
</style>
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
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <Triggers>
    <asp:PostBackTrigger ControlID="Button_IncluirLoteMedicamento" />
    </Triggers>
        <ContentTemplate>--%>
            <div id="top">
                <h2>Nota Fiscal</h2>
                <%--<fieldset class="formulario">
                    <legend>Dados e Itens</legend>
                    <p>
                        <span>--%>
                    <fieldset class="formulario">
                    <legend>Informações</legend>
                    <p>
                        <span class="rotulo">Número da Nota</span>
                        <span style="margin-left:5px;"><asp:Label ID="Label_NumeroNota" runat="server" Text=""></asp:Label></span>
                    </p>
                    <p>
                        <span class="rotulo">Fornecedor</span>
                        <span style="margin-left:5px;"><asp:Label ID="Label_Fornecedor" runat="server" Text=""></asp:Label></span>
                    </p>
                    <p>
                        <span><asp:Button  ID="Button_EncerrarNotaFiscal" runat="server" Text="Encerrar Nota" OnClick="OnClick_EncerrarNotaFiscal" OnClientClick="javascript:return confirm('Tem certeza que deseja encerrar esta nota fiscal ?');" /></span>
                        <span><asp:Button ID="Button_CancelarEncerramento" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" /></span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Itens</legend>
                    <asp:Panel ID="Panel_ItensNotaFiscal" runat="server">
                        <p>
                            <span>
                                Pressione o botão ao lado para listar todos os medicamentos <asp:Button ID="Button_ListarMedicamentos" runat="server" Text="Medicamentos" OnClick="OnClick_ListarMedicamentos" />
                            </span>
                            <br />
                            <span class="rotulo">Código do Medicamento</span>
                            <span style="margin-left:5px;">
                                <asp:TextBox ID="TextBox_CodigoMedicamento" CssClass="campo" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Farmacia/img/lupa-search.jpg" Height="30px" Width="30px" runat="server" OnClick="OnClick_PesquisaMedicamento" ValidationGroup="ValidationGroup_Pesquisa" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Código do Medicamento é Obrigatório!" ControlToValidate="TextBox_CodigoMedicamento" Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:RequiredFieldValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisa" />
                            </span>
                        </p>
                        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Seis$Button_IncluirLoteMedicamento" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="Button_ListarMedicamentos" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Seis$Button_CancelarInclusaoLoteMedicamento" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                            <p>
                                <span class="rotulo">
                                    Medicamento
                                </span>
                                <span style="margin-left:5px;">
                                    <asp:DropDownList ID="DropDownList_Medicamento" runat="server" AutoPostBack="true" Width="300px"
                                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaInformacaoMedicamento">
                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel1$DropDownList_Medicamento" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Seis$Button_IncluirLoteMedicamento" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Seis$Button_CancelarInclusaoLoteMedicamento" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                        <p>
                            <span>
                                <cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                        ContentCssClass="accordionContent">
                                    <HeaderTemplate></HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                    <Panes>
                                        <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                            <Header>Informações sobre o medicamento selecionado</Header>
                                            <Content>
                                                <asp:DetailsView ID="DetailsView_Medicamento" AutoGenerateRows="false" 
                                                    runat="server" Height="50px" Width="300px">
                                                    <Fields>
                                                        <asp:BoundField HeaderText="Medicamento" DataField="Nome" ItemStyle-HorizontalAlign="Center"/>
                                                        <asp:BoundField HeaderText="Unidade de Medida" DataField="UnidadeMedidaToString" ItemStyle-HorizontalAlign="Center"/>
                                                    </Fields>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum medicamento selecionado."></asp:Label>
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
                        
                        <asp:UpdatePanel ID="UpdatePanel_Seis" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>
                             <p>
                                <span class="rotulo">Fabricante</span>
                                <span style="margin-left:5px;">
                                    <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Lote</span>
                                <span style="margin-left:5px;">
                                    <asp:TextBox ID="TextBox_Lote" CssClass="campo" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                            <span class="rotulo">Validade</span>
                            <span style="margin-left:5px;">
                                <asp:TextBox ID="TextBox_Validade" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Quantidade</span>
                            <span style="margin-left:5px;">
                                <asp:TextBox ID="TextBox_Quantidade" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Valor Unitário</span>
                            <span style="margin-left:5px;">
                                <asp:TextBox ID="TextBox_ValorUnitario" CssClass="campo" runat="server" OnKeyPress="return(MascaraMoeda(this,'.',',',event));"></asp:TextBox>
                            </span>
                        </p>
                        
                        <p>
                            <span>
                                <asp:Button ID="Button_IncluirLoteMedicamento" runat="server" Text="Incluir" OnClick="OnClick_IncluirLoteMedicamento" ValidationGroup="ValidationGroup_cadLoteMedicamento"/>
                                <asp:Button ID="Button_CancelarInclusaoLoteMedicamento" runat="server" Text="Cancelar" OnClick="OnClick_CancelarLoteMedicamento" />
                            </span>
                        </p>
                        
                        <p>
                            <span>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Medicamento é Obrigatório!" ControlToValidate="DropDownList_Medicamento" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Fabricante é Obrigatório!" ControlToValidate="DropDownList_Fabricante" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Lote é Obrigatório!" Display="None" ControlToValidate="TextBox_Lote" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Validade é Obrigatório!" Display="None" ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Formato inválido para Validade!" Operator="DataTypeCheck" Type="Date" Display="None" ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900." Type="Date" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" Display="None" ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantidade é Obrigatório!" Display="None" ControlToValidate="TextBox_Quantidade" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Quantidade deve ser maior que 0." Display="None" ControlToValidate="TextBox_Quantidade" ValueToCompare="0" Operator="GreaterThan" Type="Integer" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente número em Quantidade." Display="None" ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Valor Unitário é Obrigatório!" Display="None" ControlToValidate="TextBox_ValorUnitario" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Valor Unitário deve ser maior que 0." Display="None" Type="Currency" Operator="GreaterThan" ValueToCompare="0" ControlToValidate="TextBox_ValorUnitario" ValidationGroup="ValidationGroup_cadLoteMedicamento"></asp:CompareValidator>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadLoteMedicamento" />
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_Validade" 
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                    TargetControlID="TextBox_Validade" Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                    
                    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Seis$Button_IncluirLoteMedicamento" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                        <p>
                        <span>
                            <asp:GridView ID="GridView_LotesMedicamentoNotaFiscal" runat="server" 
                                OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicao" 
                                OnRowEditing="OnRowEditing_EditarRegistro"
                                OnRowUpdating="OnRowUpdating_AlterarRegistro"
                                OnRowDataBound="OnRowDataBound_FormataGridView"
                                OnRowCommand="OnRowCommand_VerificarAcao"
                                AutoGenerateColumns="false" DataKeyNames="Codigo">
                                <Columns>
                                    <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                    <asp:BoundField HeaderText="Unidade de Medida" DataField="UnidadeMedidaMedicamento" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                    <asp:BoundField HeaderText="Lote" DataField="NomeLote" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                              <%--      <asp:TemplateField HeaderText="Lote" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Lote" runat="server" Text='<%#bind("NomeLote") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_Lote" runat="server" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Lote é Obrigatório!" ControlToValidate="TextBox_Lote" Display="None" ></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="Fabricante" DataField="FabricanteMedicamento" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                    <%--<asp:TemplateField HeaderText="Fabricante" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Fabricante" runat="server" Text='<%#bind("FabricanteMedicamento") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownList_Fabricante" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um fabricante!" ValueToCompare="-1" Operator="GreaterThan" ControlToValidate="DropDownList_Fabricante" Display="None"></asp:CompareValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="Validade" DataField="ValidadeMedicamento" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="true" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"/>
                                    <%--<asp:TemplateField HeaderText="Validade" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Validade" runat="server" Text='<%#bind("ValidadeMedicamento","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_Validade" runat="server" Enabled="false" ></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Validade é Obrigatório!" ControlToValidate="TextBox_Validade" Display="None" ></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Formato inválido para Validade!" Operator="DataTypeCheck" Type="Date" Display="None" ControlToValidate="TextBox_Validade"></asp:CompareValidator>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900." Type="Date" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" Display="None" ControlToValidate="TextBox_Validade"></asp:CompareValidator>
                                            <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="TextBox_Validade"
                                                runat="server" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                                                TargetControlID="TextBox_Validade" Mask="99/99/9999" MaskType="Date"
                                                InputDirection="LeftToRight">
                                            </cc1:MaskedEditExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_Quantidade" runat="server" Text='<%#bind("Quantidade") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_Quantidade" CssClass="campo" Width="30px" runat="server" Text='<%#bind("Quantidade") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Quantidade é Obrigatório!" ControlToValidate="TextBox_Quantidade" Display="None" ></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Quantidade deve ser maior que 0." Display="None" ControlToValidate="TextBox_Quantidade" ValueToCompare="0" Operator="GreaterThan" Type="Integer"></asp:CompareValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Digite somente números em Quantidade." ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None"></asp:RegularExpressionValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Valor Unitário" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_ValorUnitario" runat="server" Text='<%#bind("ValorUnitario") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_ValorUnitario" CssClass="campo" Width="30px" runat="server" Text='<%#bind("ValorUnitario") %>' OnKeyPress="return(MascaraMoeda(this,'.',',',event));"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Valor Unitário é Obrigatório!" ControlToValidate="TextBox_ValorUnitario" Display="None" ></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Valor Unitário deve ser maior que 0." Display="None" Type="Currency" Operator="GreaterThan" ValueToCompare="0" ControlToValidate="TextBox_ValorUnitario"></asp:CompareValidator>
                                            <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" CancelText="Cancelar" EditText="Editar"  UpdateText="Alterar"
                                        InsertVisible="false" ShowEditButton="true" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Excluir" Text="Excluir" />
    <%--                                <asp:CommandField ButtonType="Link" DeleteText="Excluir"
                                        InsertVisible="false" ShowDeleteButton="true" />--%>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </span>
                    </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
<%--                        </span>
                    </p>
                </fieldset>--%>
            </div>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>