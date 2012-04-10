<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true" CodeBehind="FormAutorizarRM.aspx.cs" Inherits="ViverMais.View.Farmacia.FormAutorizarRM" Title="ViverMais - Autorização de RM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1"%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="Upd1" runat="server">
  <ContentTemplate>
   <div id="top">
        <h2>Formulário de Autorização de Requisição de Medicamentos</h2>
        <fieldset class="formulario">
            <legend>Dados da Requisição de Medicamentos</legend>
            <p>
                <span class="rotulo">Nº da Requisição:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblNumeroRequisicao" runat="server" Text="" CssClass="label"></asp:Label>
                </span>            
            </p>            
            <p>
                <span class="rotulo">Farmácia:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblFarmacia" runat="server" Text="" CssClass="label"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Envio ao Distrito:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblDataEnvio" runat="server" Text="" CssClass="label"></asp:Label>
                </span>
            </p> 
            <p>
                <span class="rotulo">Data de Criação:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblDataCriacao" runat="server" Text="" CssClass="label"></asp:Label>
                </span>            
            </p>         
        </fieldset>       
        <fieldset class="formulario">
            <legend>Itens da Requisição</legend>
                <asp:GridView ID="gridItens" runat="server" AutoGenerateColumns="False" Width="700px"
                  onrowediting="gridItens_RowEditing" onrowupdating="gridItens_RowUpdating"
                  OnRowCancelingEdit="gridItens_RowCancelingEdit"
                  BorderColor="Black" onrowcommand="gridItens_RowCommand">
                  <RowStyle CssClass="rowGridView" />
                  <Columns>
                        <asp:BoundField DataField="Codigo" HeaderStyle-CssClass="colunaEscondida"
                           ItemStyle-CssClass="colunaEscondida" FooterStyle-CssClass="colunaEscondida" />
                        <asp:BoundField HeaderText="Medicamento" DataField="Medicamento" ReadOnly="true"
                           ItemStyle-Width="380px" ItemStyle-VerticalAlign="Bottom" ItemStyle-BorderColor="Silver" />
                        <asp:BoundField HeaderText="Elenco" DataField="Elenco" ReadOnly="true" 
                           ItemStyle-Width="200px" ItemStyle-VerticalAlign="Bottom" ItemStyle-BorderColor="Silver"/>
                        <asp:BoundField HeaderText="Qtd Solicitada" DataField="QtdPedida" ReadOnly="true" 
                          ItemStyle-Width="50px" ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Right"
                          ItemStyle-BorderColor="Silver"/>
                        <asp:TemplateField HeaderText="Qtd.">
                            <ItemTemplate>
                                <asp:Label ID="lblQtdPedidaDistrito" runat="server" Text='<%#bind("QtdPedidaDistrito") %>' ValidationGroup="GridItem"></asp:Label>
                            </ItemTemplate>
                            <edititemtemplate>
                               <asp:TextBox ID="TextBox_Quantidade" CssClass="campo" runat="server" Text='<%#bind("QtdPedidaDistrito") %>' Width="35px" MaxLength="6"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantidade é Obrigatório!" ControlToValidate="TextBox_Quantidade" Display="None" ValidationGroup="GridItem"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento." ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None" ValidationGroup="GridItem">
                               </asp:RegularExpressionValidator>
                            </edititemtemplate>
                            <ItemStyle VerticalAlign="Bottom" HorizontalAlign="Right" Width="45px" BorderColor="Silver"/>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="true" ShowCancelButton="true"
                            InsertVisible="false" ValidationGroup="GridItem"
                            CancelText="<img src='img/bt_del.png' border='0' alt='Cancelar'>"
                            EditText="<img src='img/bt_edit.png' border='0' alt='Editar'>"
                            UpdateText="<img src='img/alterar.png' border='0' alt='Alterar'>">
                            <ItemStyle VerticalAlign="Bottom" BorderColor="Silver"/>
                        </asp:CommandField>
                        <asp:TemplateField>
                             <ItemTemplate>
                                    <asp:LinkButton ID="btnDeletarItem" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                         runat="server" Width="10px" CausesValidation="false" Text="<img src='img/bt_del.png' border='0' alt='Excluir'>"
                                         Font-Size="X-Small" OnClientClick="javascript : return confirm('Tem certeza que deseja EXCLUIR o item?');"
                                         CommandName="DeletarItem">
                                    </asp:LinkButton>
                             </ItemTemplate>
                             <ItemStyle Width="10px" VerticalAlign="Bottom" BorderColor="Silver" />
                         </asp:TemplateField>                         
                  </Columns>
                  <HeaderStyle CssClass="hearderGridView" />
                </asp:GridView>
            <p><span></span></p>
            <p>
                <span>
                    <asp:LinkButton ID="btnEnviar" runat="server" Text="Enviar ao Almoxarifado" onclick="btnEnviar_Click"
                      Font-Size="X-Small" OnClientClick="javascript : return confirm('Tem certeza que deseja ENVIAR a Requisição?');">
                    </asp:LinkButton>
                    &nbsp;&nbsp;<asp:LinkButton ID="btnCancelar" runat="server" Text="Cancelar Requisição"
                      Font-Size="X-Small" onclick="btnCancelar_Click" OnClientClick="javascript : return confirm('Tem certeza que deseja CANCELAR a Requisição?');">
                    </asp:LinkButton>                    
                    &nbsp;&nbsp;<asp:LinkButton ID="btnAdicionarMedicamento" runat="server" Text="Adicionar Medicamento"
                      Font-Size="X-Small" onclick="btnAdicionarMedicamento_Click">
                    </asp:LinkButton>
                </span>
            </p>
        </fieldset>
        <asp:Panel ID="PanelListaMedicamento" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Lista de Medicamentos</legend>
                <p>
                  <span>
                      <asp:Label ID="lblMensagemAddMedicamento" runat="server" Text="Clique no medicamento para adicionar"
                       Font-Size="X-Small" ForeColor="#006a6a" Font-Bold="true">
                      </asp:Label>
                  </span>
                  <span style="margin-left: 650px;">
                      <asp:LinkButton ID="btnFechar" runat="server" 
                        Text="<img src='img/close24.png' border='0' alt='Fechar'>" 
                        onclick="btnFechar_Click">  
                      </asp:LinkButton> 
                  </span>
                </p>
                <p>
                    <span>
                        <asp:TreeView ID="TreeViewMedicamento" runat="server" OnSelectedNodeChanged="TreeViewMedicamento_SelectedNodeChanged">
                        </asp:TreeView>
                    </span>
                </p>
            </fieldset></asp:Panel>
        <p></p>
  </div>
 </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
