﻿<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormVincularEstrategiaImuno.aspx.cs" Inherits="ViverMais.View.Vacina.FormVincularEstrategiaImuno"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Vincular Estratégia à Imunobiológico
        </h2>
        <fieldset class="formulario">
            <legend>Relação</legend>
            <p>
                <span class="rotulo">Estratégias</span> <span>
                    <asp:DropDownList ID="ddlEstrategias" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlEstrategias_SelectedIndexChanged"
                        DataTextField="Descricao" DataValueField="Codigo" AutoPostBack="true">
                    </asp:DropDownList>
                </span>
            </p>
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEstrategias" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Imunobiológicos</span> <span>
                            <asp:DropDownList ID="ddlImunobiologicos" runat="server" CssClass="drop">
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                            </asp:DropDownList></span><span style="position:absolute;"><asp:LinkButton ID="btnAddImuno" runat="server" CausesValidation="true" ValidationGroup="AddImuno"
                                OnClick="btnAddImuno_Click" Height="19px" Width="19px">
                                      <img id="imgadd" alt="Vadascrar" src="img/add.png"
                                      onmouseover="imgadd.src='img/add.png';"
                                      onmouseout="imgadd.src='img/add.png';" /></asp:LinkButton></span>
                    </p>
                    <p>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione uma estratégia."
                                ValidationGroup="AddImuno" Operator="GreaterThan" ValueToCompare="0" Display="None" ControlToValidate="ddlEstrategias">
                            </asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione um imunobiológico."
                                ValidationGroup="AddImuno" Operator="GreaterThan" ValueToCompare="0" Display="None" ControlToValidate="ddlImunobiologicos">
                            </asp:CompareValidator>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlImunobiologicos"
                                ErrorMessage="Selecione um imunobiológico!" Display="None" ForeColor="Red" Font-Bold="true"
                                ValidationGroup="AddImuno">
                            </asp:RequiredFieldValidator>--%>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="AddImuno"
                                ShowMessageBox="true" ShowSummary="false" /></p>
                    <br />
                    <asp:Panel ID="Panel_Estrategias_Imuno" runat="server" Visible="false">
                        <asp:GridView ID="Gridview_Imunos" runat="server" AutoGenerateColumns="false" Width="690px"
                            GridLines="Horizontal" Font-Names="Verdana" BorderStyle="None"
                            BorderWidth="1px" CellPadding="3" OnRowCommand="Gridview_Imunos_RowCommand">
                            <Columns>
                                <asp:BoundField HeaderText="Codigo" DataField="Codigo">
                                    <ItemStyle CssClass="colunaEscondida" />
                                    <HeaderStyle CssClass="colunaEscondida" />
                                    <FooterStyle CssClass="colunaEscondida" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Nome" DataField="Nome" />
                                <asp:BoundField HeaderText="Unidade Medida" DataField="UnidadeMedida" />
                                <asp:TemplateField HeaderText="Excluir">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnExcluiRegistro" runat="server" CommandArgument='<%#Eval("Codigo")%>'
                                            CommandName="Excluir" Text="Excluir" OnClientClick="javascript: return confirm('Confirma a exclusão deste Registro?');">
                                                          <img id="imgexcluir" alt="Vadascrar" src="img/excluir_gridview.png"
                                                          onmouseover="imgexcluir.src='img/excluir_gridview.png';"
                                                          onmouseout="imgexcluir.src='img/excluir_gridview.png';" />
                                                                                </asp:LinkButton>
                                    </ItemTemplate> 
                                </asp:TemplateField>                            </Columns>
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                Height="24px" Font-Size="13px" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado." Font-Names="Verdana"
                                    Font-Bold="true" Font-Size="11px"></asp:Label>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                        </asp:GridView>
                        <br />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <div class="botoesroll">
            <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Vacina/Default.aspx"
                CausesValidation="false">
                <img id="img_voltar" alt="Voltar" src="img/btn_voltar1.png"
                onmouseover="img_voltar.src='img/btn_voltar2.png';"
                onmouseout="img_voltar.src='img/btn_voltar1.png';" />
            </asp:LinkButton>
        </div>
    </div>
</asp:Content>
