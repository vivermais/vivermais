﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormVinculoSubGrupoProcedimento.aspx.cs" Inherits="ViverMais.View.Agendamento.FormVinculoSubGrupoProcedimento"
    Title="Untitled Page" %>

<%@ Register Src="~/Agendamento/WUCPesquisaProcedimento.ascx" TagName="WUCPesquisaProcedimento"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Vínculo de Sub-Grupo e Procedimentos</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Informe o Sub-Grupo</span><span>
                    <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="drop">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:Panel ID="PanelPesquisaProcedimento" runat="server">
                <uc2:WUCPesquisaProcedimento ID="WUCPesquisaProcedimento1" runat="server" />
            </asp:Panel>
            <p>
                <span class="rotulo">Procedimento</span> <span>
                    <asp:DropDownList ID="ddlProcedimento" runat="server" CssClass="drop">
                    </asp:DropDownList>
                </span><span>Para modificar o Procedimento,
                    <asp:LinkButton ID="lnkBtnModificarProcedimento" Text="clique aqui!" runat="server"
                        CausesValidation="false" OnClick="lnkBtnModificarProcedimento_Click" />
                </span>
            </p>
            <p>
                <span class="rotulo">Especialidade</span> <span>
                    <asp:DropDownList ID="ddlCBO" runat="server" CssClass="drop">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span>                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSubGrupo" runat="server" ErrorMessage="SubGrupo é Obrigatório."
                        Display="Dynamic" ControlToValidate="ddlSubGrupo" InitialValue="0" ValidationGroup="Salvar"></asp:RequiredFieldValidator><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProcedimento" runat="server" ErrorMessage="Procedimento é Obrigatório."
                        Display="Dynamic" ControlToValidate="ddlProcedimento" InitialValue="0" ValidationGroup="Salvar"></asp:RequiredFieldValidator><br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCBO" runat="server" ErrorMessage="Especialidade é Obrigatório."
                        Display="Dynamic" ControlToValidate="ddlCBO" InitialValue="0" ValidationGroup="Salvar"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="Salvar" />
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" CausesValidation="true" ValidationGroup="Salvar">
                <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                onmouseover="imgsalvar.src='img/salvar_2.png';"
                onmouseout="imgsalvar.src='img/salvar_1.png';" />
                </asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                    CausesValidation="false">
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
                </asp:LinkButton></div>
        </fieldset>
        <br />
        <br />
        <fieldset class="formulario">
            <legend>Vínculos Cadastrados</legend>
            <cc1:TabContainer ID="TabContainerVinculos" runat="server" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabelPanel_Ativos" runat="server" HeaderText="Vínculos Ativos">
                    <ContentTemplate>
                        <p>
                            <asp:GridView ID="GridViewVinculosAtivos" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Vertical" Width="100%" OnRowCommand="GridViewVinculosAtivos_RowCommand"
                                OnPageIndexChanging="GridViewVinculosAtivos_PageIndexChanging" HorizontalAlign="Center">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubGrupo" HeaderText="Sub-Grupo" ReadOnly="true" />
                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                    <asp:BoundField DataField="CBO" HeaderText="CBO" ReadOnly="true" />
                                    <asp:TemplateField HeaderText="Inativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja INATIVAR este Vínculo?');"
                                                Text="">
                                                <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum Vínculo Ativo." Font-Bold="true"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel_Inativos" runat="server" HeaderText="Vínculos Inativos">
                    <ContentTemplate>
                        <p>
                            <asp:GridView ID="GridViewVinculosInativos" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Vertical" Width="100%" OnPageIndexChanging="GridViewVinculosInativos_PageIndexChanging"
                                HorizontalAlign="Center">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubGrupo" HeaderText="Sub-Grupo" ReadOnly="true" />
                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                    <asp:BoundField DataField="CBO" HeaderText="CBO" ReadOnly="true" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum Vínculo Inativo." Font-Bold="true"></asp:Label>
                                </EmptyDataTemplate>
                                <RowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </fieldset>
    </div>
</asp:Content>
