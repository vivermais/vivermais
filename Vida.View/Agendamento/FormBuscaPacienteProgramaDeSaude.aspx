<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" CodeBehind="FormBuscaPacienteProgramaDeSaude.aspx.cs" Inherits="ViverMais.View.Agendamento.FormBuscaPacienteProgramaDeSaude" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Busca de Pacientes no Programa de Saúde</h2>
            <br />
        <div>
            Clique no botão abaixo para víncular pacientes nos Programas de Saúde<p>
                <asp:LinkButton ID="btnVinculoPaciente" runat="server" CausesValidation="false"
                    PostBackUrl="~/Agendamento/FormVinculoPacienteProgramaDeSaude.aspx">
                       <img id="img_Afastamento" alt="Cadastrar Novo Afastamento de Profissional" src="img/btn-cad-novo-prof1.png"
                onmouseover="img_Afastamento.src='img/btn-cad-novo-prof2.png';"
                onmouseout="img_Afastamento.src='img/btn-cad-novo-prof1.png';" />
                </asp:LinkButton></p>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Programa de Saúde</span> <span>
                    <asp:DropDownList ID="ddlProgramaDeSaude" runat="server" CssClass="drop" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProgramaDeSaude_SelectedIndexChanged" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <asp:CompareValidator ID="CompareValidator_Programa" runat="server" ControlToValidate="ddlProgramaDeSaude"
                    Display="None" ErrorMessage="Programa é Obrigatório!" Font-Size="XX-Small" Operator="GreaterThan"
                    ValueToCompare="0">
                </asp:CompareValidator>
            </p>
        </fieldset>
        <p>
            &nbsp;
        </p>
        <fieldset style="width: 880px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
            <legend>Pacientes</legend>
            <p>
                <asp:Panel runat="server" ID="PanelVinculosAtivosInativos" Visible="true">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabelPanel_Um" runat="server" HeaderText="Ativos">
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridViewVinculosAtivos" runat="server" AutoGenerateColumns="False"
                                            AllowSorting="True" CssClass="gridview" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewVinculosAtivos_OnPageIndexChanging"
                                            OnRowCommand="GridViewVinculosAtivos_RowCommand" GridLines="Vertical" DataKeyNames="Codigo">
                                            <Columns>
                                                <asp:BoundField DataField="Nome" HeaderText="Paciente"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Inativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                            CausesValidation="false" CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja INATIVAR este paciente do Programa?');"
                                                            Text="">
                                                            <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LabelSemRegistro" runat="server" Text="Nenhum Registro Encontrado!"
                                                    Font-Size="X-Small" ForeColor="Blue"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle CssClass="GridviewPager" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Inativos">
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridViewVinculosInativos" runat="server" AutoGenerateColumns="False"
                                            AllowSorting="True" CssClass="gridview" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewVinculosInativos_OnPageIndexChanging"
                                            OnRowCommand="GridViewVinculosInativos_RowCommand" GridLines="Vertical" DataKeyNames="Codigo">
                                            <Columns>
                                                <asp:BoundField DataField="Nome" HeaderText="Paciente" ReadOnly="true"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Reativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdReativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                            CausesValidation="false" CommandName="Reativar" OnClientClick="javascript : return confirm('Tem certeza que deseja REATIVAR este paciente no Prgrama?');"
                                                            Text="">
                                                            <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LabelSemRegistro1" runat="server" Text="Nenhum Registro Encontrado"
                                                    Font-Size="X-Small" ForeColor="Red"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle CssClass="GridviewPager" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </asp:Panel>
            </p>
        </fieldset>
    </div>
</asp:Content>
