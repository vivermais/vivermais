﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensRemanejamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormItensRemanejamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
        <div id="top">
            <h2>Informações do Remanejamento</h2>
            <fieldset class="formulario">
                <legend>Movimento de Origem</legend>
                <p>
                    <span class="rotulo">Data</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_DataMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Farmácia de Origem</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_FarmaciaMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Envio</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_DataEnvioMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Recebimento</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_DataRecebMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Envio</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_RespEnvMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Receb.</span>
                    <span style="margin-left:5px;">
                        <asp:Label ID="Label_RespRecebMovimento" runat="server" Text=""></asp:Label>
                    </span>
                </p>
            </fieldset>
            
            <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
            <fieldset class="formulario">
                <legend>Lista de Medicamentos</legend>
                <p>
                    <span>
                        <asp:GridView ID="GridView_ItensRemanejamento" runat="server" OnRowDataBound="OnRowDataBound_FormataGridView"
                            OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicaoItem" DataKeyNames="CodigoLote"
                            OnRowEditing="OnRowEditing_EditarItem" OnRowUpdating="OnRowUpdating_AlterarItem"
                            AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric"
                            OnPageIndexChanging="OnPageIndexChanging_Paginacao" Width="660px"
                            AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ReadOnly="true"/>
                                <asp:BoundField HeaderText="Lote" DataField="NomeLote" ReadOnly="true" />
                                <asp:BoundField HeaderText="Quantidade Registrada" DataField="QuantidadeRegistrada" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Quantidade Recebida">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#bind("QuantidadeRecebida") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox_QuantidadeRecebida" runat="server" CssClass="campo" Text='<%#bind("QuantidadeRecebida") %>' Width="30px"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receber Item ?">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" CommandName="Edit" runat="server">Confirmar</asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" CommandName="Update" runat="server" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_Finalizar')) return confirm('Tem certeza que deseja confirmar o recebimento deste item ?'); return false;" ValidationGroup="ValidationGroup_Finalizar">Finalizar</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton2" CommandName="Cancel" runat="server">Cancelar</asp:LinkButton>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Recebida é Obrigatório." Display="None" ControlToValidate="TextBox_QuantidadeRecebida" ValidationGroup="ValidationGroup_Finalizar" ></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Quantidade Recebida." ValidationExpression="^\d*$" Display="None" ControlToValidate="TextBox_QuantidadeRecebida" ValidationGroup="ValidationGroup_Finalizar" ></asp:RegularExpressionValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Quantidade Recebida deve ser igual ou maior que zero." Display="None" ControlToValidate="TextBox_QuantidadeRecebida" ValueToCompare="0" Operator="GreaterThanEqual" ValidationGroup="ValidationGroup_Finalizar" ></asp:CompareValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Finalizar" />
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Image ID="ImageStatus" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:CommandField HeaderText="Receber Item ?" ButtonType="Link" ShowEditButton="true" CancelText="Cancelar" UpdateText="Finalizar"
                                 InsertVisible="false" EditText="Confirmar" />--%>
                            </Columns>
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow" />
                        </asp:GridView>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="Button_Finalizar" runat="server" Text="Finalizar" OnClick="OnClick_FinalizarRecebimento"/>
                        <asp:Button ID="Button1" runat="server" Text="Voltar" PostBackUrl="~/Farmacia/Default.aspx"/>
                    </span>
                </p>
            </fieldset>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
</asp:Content>
