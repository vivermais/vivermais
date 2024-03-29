﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormParametrizarProcedimento.aspx.cs" Inherits="ViverMais.View.Agendamento.FormParametrizarProcedimento"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="lknIncluir" />
            <asp:PostBackTrigger ControlID="tbxCodigo" />
            <asp:PostBackTrigger ControlID="tbxProcedimento" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Parametrização de Procedimentos</h2>
                <fieldset class="formulario">
                    <legend>Parametrização de Procedimentos</legend>
                    <br />
                    <p>
                        <asp:RadioButtonList ID="rbtnTipo" runat="server" RepeatDirection="Horizontal" CellPadding="3"
                            CellSpacing="0" CssClass="radio" TextAlign="Right" AutoPostBack="true" OnSelectedIndexChanged="rbtnTipo_SelectedIndexChanged">
                            <asp:ListItem Value="1">Por Forma de Organização</asp:ListItem>
                            <asp:ListItem Value="2">Por Código</asp:ListItem>
                            <asp:ListItem Value="3">Por Procedimento</asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Font-Size="XX-Small" runat="server"
                            ControlToValidate="rbtnTipo" Display="Dynamic" ErrorMessage="* Selecione o Tipo da Parametrização"></asp:RequiredFieldValidator>
                    </p>
                    <br />
                    <asp:Panel ID="PanelForma" runat="server">
                        <p>
                            <span class="rotulo">Grupo:</span> <span>
                                <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlGrupo" Display="Dynamic" ErrorMessage="* Selecione o Grupo"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Sub-Grupo:</span> <span>
                                <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="drop" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSubGrupo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlSubGrupo" Display="Dynamic" ErrorMessage="* Selecione o Sub-Grupo"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Forma de Organização:</span> <span>
                                <asp:DropDownList ID="ddlForma" runat="server" CssClass="drop" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlForma_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlForma" Display="Dynamic" ErrorMessage="* Selecione a Forma de Organização"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="PanelCodigo" runat="server">
                        <p>
                            <span class="rotulo">Codigo:</span> <span>
                                <asp:TextBox ID="tbxCodigo" runat="server" CssClass="campo" AutoPostBack="True" OnTextChanged="codigo_TextChanged"
                                    MaxLength="10"></asp:TextBox>
                                <p>
                                    <asp:Label ID="lblRotuloProcedimento" runat="server" Text="Procedimento:" CssClass="rotulo"
                                        Visible="false"></asp:Label>
                                    <asp:Label ID="lblProcedimento" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                                </p>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="tbxCodigo" Display="Dynamic" ErrorMessage="* Informe o Código do Procedimento"></asp:RequiredFieldValidator></span>
                        </p>
                    </asp:Panel>
                    <p>
                        <asp:Label ID="lblSemRegistros" runat="server" Font-Bold="true"></asp:Label></p>
                    <asp:Panel ID="PanelProcedimento" runat="server">
                        <p>
                            <span class="rotulo">Procedimento:</span> <span>
                                <asp:TextBox ID="tbxProcedimento" runat="server" CssClass="campo" AutoPostBack="True"
                                    OnTextChanged="procedimento_TextChanged" Width="400px"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxProcedimento" Display="Dynamic" ErrorMessage="* Informe o Procedimento"></asp:RequiredFieldValidator></p>
                    </asp:Panel>
                    <p>
                        <asp:Label ID="lblRotuloProced" runat="server" Text="Procedimento: " CssClass="rotulo"
                            Visible="false"></asp:Label>
                        <asp:Label ID="lblNomeProcedimento" runat="server" Font-Bold="true" Visible="false"></asp:Label>
                    </p>
                    <p>
                        <asp:GridView ID="GridViewProcedimento" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                            EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="GridViewProcedimento_PageIndexChanging"
                            PageSize="20" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Vertical" Width="100%">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormParametrizarProcedimento.aspx?id_procedimento={0}"
                                    DataTextField="Codigo" HeaderText="Codigo" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="100px" />
                                <asp:BoundField DataField="Nome" HeaderText="Procedimento" />
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="11px" Height="22px" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </p>
                    <asp:Panel ID="PanelPreparos" runat="server">
                        <p>
                            &nbsp
                        </p>
                        <p>
                            <asp:GridView ID="GridViewPreparo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="GridViewPreparo_PageIndexChanging"
                                OnRowCommand="GridViewPreparo_RowCommand" BackColor="White" BorderColor="#477ba5"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Selecione os Preparos" />
                                    <asp:TemplateField HeaderText="Selecionar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                                CommandName="Select">
                                                <asp:Image ID="imgSelect" runat="server" ImageUrl="~/Agendamento/img/bt_edit.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblPreparos" runat="server" Text="Nenhum Preparo Cadastrado!" ForeColor="Red"
                                        Font-Size="X-Small"></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="PanelPreparosSelecionados" runat="server">
                        <p>
                            &nbsp
                        </p>
                        <p>
                            <asp:GridView ID="GridViewPreparosSelecionados" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True" OnPageIndexChanging="GridViewPreparosSelecionados_PageIndexChanging"
                                OnRowCommand="GridViewPreparoSelecionados_RowCommand" BackColor="White" BorderColor="#477ba5"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Descricao" HeaderText="Lista dos preparos Selecionados" />
                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="delete" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                                CommandName="Excluir">
                                                <asp:Image ID="imgDelete" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:CommandField DeleteImageUrl="~/Agendamento/img/excluirr.png" 
                                        ShowDeleteButton="true" ButtonType="Image" HeaderText="Excluir"  ItemStyle-HorizontalAlign="Center" ControlStyle-Width="20px" ControlStyle-Height="20px">
                                    </asp:CommandField>                                    --%>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblPreparosSelecionados" runat="server" Text="Nenhum Preparo Selecionado!" ForeColor="Red"
                                        Font-Size="X-Small"></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                    </asp:Panel>
                    <p>
                        <span class="rotulo">Tipo:</span></p>
                    <asp:RadioButtonList ID="rbtnTipoProcedimento" runat="server" RepeatDirection="Vertical"
                        CellPadding="1" CellSpacing="0" CssClass="radio" TextAlign="Right" RepeatColumns="1">
                        <asp:ListItem Value="1">Regulado</asp:ListItem>
                        <asp:ListItem Value="2">Autorizado</asp:ListItem>
                        <asp:ListItem Value="3">Agendado</asp:ListItem>
                        <asp:ListItem Value="4">Atendimento Básico</asp:ListItem>
                    </asp:RadioButtonList>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                            ControlToValidate="rbtnTipoProcedimento" Display="Dynamic" ValidationGroup="ValidationSalvar"
                            ErrorMessage="* Selecione o Tipo"></asp:RequiredFieldValidator></p>
                    <br />
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknIncluir" runat="server" CausesValidation="true" OnClick="Incluir_Click"
                            ValidationGroup="ValidationSalvar">
                  <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  onmouseover="imgsalvar.src='img/salvar_2.png';"
                  onmouseout="imgsalvar.src='img/salvar_1.png';" /></asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                            CausesValidation="false">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
