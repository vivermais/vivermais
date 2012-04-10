<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true" CodeBehind="FormRegistrarRMAutorizacao.aspx.cs" Inherits="Vida.View.Farmacia.FormRegistrarRMAutorizacao" Title="Vida - Requisição de Medicamento" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="top">
        <h2>
            Formulário de Autorização de Requisição de Medicamentos</h2>
        <fieldset class="formulario">
            <legend>Dados do Elenco de Medicamentos</legend>
            <p>
                <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                    obrigatórios</span>
            </p>
            <p>
                <span class="rotulo">Farmácia:</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="ddlFarmacia" runat="server">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Nº da Requisição:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblNumeroRequisicao" runat="server" Text="-"></asp:Label>
                </span>
            </p>
            <p>
                <span class="rotulo">Data:</span> <span style="margin-left: 5px;">
                    <asp:Label ID="lblDataCriacao" runat="server" Text=""></asp:Label>
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Items da Requisição</legend>
            <p>
                <span style="margin-left: 5px;">
                    <asp:GridView ID="gridItens" runat="server" AutoGenerateColumns="False" Width="600px"
                        OnSelectedIndexChanged="gridItens_SelectedIndexChanged" 
                    onrowediting="gridItens_RowEditing" onrowupdating="gridItens_RowUpdating" 
                    OnRowDeleting="gridItens_RowDeleting" 
                    OnRowCancelingEdit="gridItens_RowCancelingEdit" 
                    onrowdatabound="gridItens_RowDataBound">
                        <Columns>
                            <asp:BoundField HeaderText="Medicamento" DataField="Medicamento" ReadOnly="true" ControlStyle-Width="450px" />
                            <asp:BoundField HeaderText="Elenco" DataField="Elenco" ReadOnly="true" ControlStyle-Width="250px" />
                            <asp:TemplateField HeaderText="Qtd.">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantidade" runat="server" Text='<%#bind("QtdPedida") %>'>' ValidationGroup="GridItem"></asp:Label>
                            </ItemTemplate>
                            <edititemtemplate>
                               <asp:TextBox ID="TextBox_Quantidade" runat="server" Width="50" MaxLength="6"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantidade é Obrigatório!" ControlToValidate="TextBox_Quantidade" Display="None" ValidationGroup="GridItem"></asp:RequiredFieldValidator>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento." ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None" ValidationGroup="GridItem" ></asp:RegularExpressionValidator>
                            </edititemtemplate>
                             </asp:TemplateField>
                            <asp:CommandField CancelText="Cancelar" DeleteText="Excluir" EditText="Editar" UpdateText="Atualizar" ShowEditButton="true" ShowCancelButton="true" ShowDeleteButton="true" ControlStyle-Width="75px" InsertVisible="false" ValidationGroup="GridItem" />
                        </Columns>
                    </asp:GridView>
                </span>
            </p>
                        <p>
                <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                <asp:Button ID="btnEnviar" runat="server" Text="Enviar ao Distrito" 
                                onclick="btnEnviar_Click" CausesValidation="false" />
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Lista de Medicamentos</legend>
            <p>
                <span>
                    <asp:TreeView ID="TreeViewMedicamento" runat="server" OnSelectedNodeChanged="TreeViewMedicamento_SelectedNodeChanged">
                    </asp:TreeView>
                </span>
            </p>
        </fieldset>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
