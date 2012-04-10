<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true"
    CodeBehind="FormRegistrarRM.aspx.cs" Inherits="Vida.View.Farmacia.FormRegistrarRM"
    Title="VIDA - Formulário de Requisição de Medicamento" %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="top">
        <h2>
            Formulário de Registro de Requisição de Medicamentos
        </h2>
        <fieldset class="formulario">
            <legend>Dados da Requisição de Medicamentos</legend>
            <p>
                <span class="rotulo">Farmácia:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblFarmacia" runat="server" Text="" CssClass="label"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Nº da Requisição:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblNumeroRequisicao" runat="server" Text="-" CssClass="label"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data:</span>
                <span style="margin-left: 5px;">
                    <asp:Label ID="lblDataCriacao" runat="server" Text="" CssClass="label"></asp:Label>
                </span>
            </p>
        </fieldset>
        <asp:Panel ID="Panel_ListaMedicamento" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Lista de Medicamentos</legend>
                <asp:TreeView ID="TreeViewMedicamento" runat="server" OnSelectedNodeChanged="TreeViewMedicamento_SelectedNodeChanged">
                </asp:TreeView>
            </fieldset></asp:Panel>        
        <fieldset class="formulario">
            <legend>Itens da Requisição</legend>
             <asp:GridView ID="gridItens" runat="server" AutoGenerateColumns="False" Width="700px"
                OnSelectedIndexChanged="gridItens_SelectedIndexChanged" 
                onrowediting="gridItens_RowEditing" onrowupdating="gridItens_RowUpdating" 
                OnRowDeleting="gridItens_RowDeleting" OnRowCancelingEdit="gridItens_RowCancelingEdit" 
                onrowdatabound="gridItens_RowDataBound">
                <RowStyle CssClass="rowGridView" />
                <Columns>
                    <asp:BoundField HeaderText="Medicamento" DataField="Medicamento" ReadOnly="true">
                        <ItemStyle Width="450px" VerticalAlign="Bottom" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Elenco" DataField="Elenco" ReadOnly="true">
                        <ItemStyle Width="200px" VerticalAlign="Bottom" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Qtd.">
                        <ItemTemplate>
                            <asp:Label ID="lblQuantidade" runat="server" Text='<%#bind("QtdPedida") %>' ValidationGroup="GridItem"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                           <asp:TextBox ID="TextBox_Quantidade" CssClass="campo" runat="server" Text='<%#bind("QtdPedida") %>' Width="50" MaxLength="6"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantidade é Obrigatório!" ControlToValidate="TextBox_Quantidade" Display="None" ValidationGroup="GridItem"></asp:RequiredFieldValidator>
                           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento." ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None" ValidationGroup="GridItem" ></asp:RegularExpressionValidator>
                        </edititemtemplate>
                        <ItemStyle VerticalAlign="Bottom" HorizontalAlign="Right" Width="50px"/>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="true" ShowCancelButton="true"
                     ShowDeleteButton="true" InsertVisible="false" ValidationGroup="GridItem"
                     CancelText="<img src='../img/bts/bt_del.png' border='0' alt='Cancelar'>"
                     EditText="<img src='../img/bts/bt_edit.png' border='0' alt='Editar'>"
                     UpdateText="<img src='../img/bts/alterar.png' border='0' alt='Alterar'>"
                     DeleteText="<img src='../img/bts/bt_del.png' border='0' alt='Cancelar'>">
                        <ItemStyle VerticalAlign="Bottom" />
                    </asp:CommandField>
                </Columns>
                <HeaderStyle CssClass="hearderGridView" />
             </asp:GridView>
             <p></p>
             <p>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar ao Distrito" 
                                onclick="btnEnviar_Click" CausesValidation="false" />
             </p>
        </fieldset>
   </div>
   <p></p>
  </ContentTemplate>
  </asp:UpdatePanel>
</asp:Content>
