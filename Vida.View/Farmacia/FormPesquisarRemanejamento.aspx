﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormPesquisarRemanejamento.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormPesquisarRemanejamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Pesquisar Remanejamentos Concluídos</h2>
        <fieldset class="formulario">
            <legend>Informações</legend>
            <p>
                <span class="rotulo">Farmácia</span> <span>
                    <asp:DropDownList ID="DropDownList_Farmacia" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFarmaciasOrigem" DataTextField="Nome" DataValueField="Codigo">
                        <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Data</span> <span>
                    <asp:TextBox ID="TextBox_Data" runat="server" CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Farmacia" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Farmácia de Origem</span><span>
                            <asp:DropDownList ID="DropDownList_FarmaciaOrigem" runat="server">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            
                <span>
                   <asp:LinkButton ID="Button_Pesquisar" runat="server"   OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_Pesquisar">
                  <img id="imgpesquisa" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                    <asp:LinkButton ID="Button_Cancelar" runat="server"  PostBackUrl="~/Farmacia/Default.aspx">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn/cancelar1.png';" /></asp:LinkButton>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma Farmácia!"
                        ControlToValidate="DropDownList_Farmacia" Operator="GreaterThan" ValueToCompare="-1"
                        Display="None" ValidationGroup="ValidationGroup_Pesquisar">
                    </asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data com formato inválido."
                        ControlToValidate="TextBox_Data" Display="None" Type="Date" Operator="DataTypeCheck" ValidationGroup="ValidationGroup_Pesquisar" />
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                        ControlToValidate="TextBox_Data" Display="None" Type="Date" ValueToCompare="01/01/1900"
                         Operator="GreaterThanEqual" ValidationGroup="ValidationGroup_Pesquisar">
                    </asp:CompareValidator>
                    
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_Data"
                     InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                        TargetControlID="TextBox_Data">
                    </cc1:CalendarExtender>
                    <asp:CustomValidator ID="CustomValidator_Pesquisa" runat="server" ErrorMessage="CustomValidator"
                        Display="None" ValidationGroup="ValidationGroup_Pesquisar" OnServerValidate="OnServerValidate_ValidaPesquisa"></asp:CustomValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisar" />
                </span>
                </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button_Pesquisar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_Pesquisa" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Resultado</legend>
                        <p>
                            <asp:GridView ID="GridView_ItensRemanejamento" runat="server" AutoGenerateColumns="false" OnRowCommand="OnRowCommand_VerificarAcao"
                                AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" DataKeyNames="Codigo"
                                Width="660px" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                <Columns>
                                    <asp:BoundField HeaderText="Data de Abertura" DataField="DataAbertura" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                    <asp:BoundField HeaderText="Farmácia de Origem" DataField="FarmaciaOrigem" />
                                    <%--<asp:BoundField HeaderText="Status" DataField="DescricaoStatus" />--%>
                                    <asp:HyperLinkField DataNavigateUrlFields="Codigo" Text="Ver Itens" DataNavigateUrlFormatString="~/Farmacia/FormItensRemanejamento.aspx?co_remanejamento={0}" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Imprimir" Text="Imprimir" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
