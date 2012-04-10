<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormVincularProgramaUnidade.aspx.cs" Inherits="ViverMais.View.Agendamento.FormVincularProgramaUnidade"
    Title="Untitled Page" %>

<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="TagName_Estabelecimento"
    TagPrefix="TagPrefix_Estabelecimento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Vínculo de Programas de Saúde e Unidades</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Programa de Saúde</span> <span>
                    <asp:DropDownList ID="ddlProgramaDeSaude" runat="server" CssClass="drop" AutoPostBack="true"
                        Width="300px" DataValueField="Codigo" DataTextField="Nome" OnSelectedIndexChanged="ddlProgramaDeSaude_SelectedIndexChanged">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPrograma" runat="server" ErrorMessage="Selecione o programa de saúde"
                    InitialValue="0" Display="Dynamic" Font-Size="X-Small" SetFocusOnError="true"
                    ControlToValidate="ddlProgramaDeSaude">
                </asp:RequiredFieldValidator>
            </p>
            <asp:Panel ID="Panel_Estabelecimento" runat="server">
                <fieldset>
                    <legend>Estabelecimento</legend>
                    <TagPrefix_Estabelecimento:TagName_Estabelecimento ID="EAS" runat="server"></TagPrefix_Estabelecimento:TagName_Estabelecimento>
                    <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                        RenderMode="Block" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <p>
                                <span class="rotulo">Unidade</span> <span>
                                    <asp:DropDownList ID="DropDownList_Estabelecimento" runat="server" Height="20px"
                                        OnSelectedIndexChanged="DropDownList_Estabelecimento_SelectedIndexChanged" AutoPostBack="true"
                                        CssClass="campo" Width="380px" DataTextField="NomeFantasia" DataValueField="CNES">
                                        <asp:ListItem Text="SELECIONE..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUnidade" Font-Size="Small"
                                    runat="server" ControlToValidate="DropDownList_Estabelecimento" InitialValue="0"
                                    ForeColor="Red" ErrorMessage="O Estabelecimento é Obrigatório" Text="*">
                                </asp:RequiredFieldValidator>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </asp:Panel>
            <asp:UpdatePanel ID="UpdatePanelTipoUnidade" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Tipo de Unidade</span> <span>
                            <asp:CheckBoxList ID="chkBoxTipoUnidade" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Solicitante" Value="S"></asp:ListItem>
                                <asp:ListItem Text="Executante" Value="E"></asp:ListItem>
                            </asp:CheckBoxList>
                        </span><span>
                            <asp:CustomValidator ID="CustomValidatorTipoUnidade" runat="server" ControlToValidate=""
                                ErrorMessage="O Tipo é obrigatório" OnServerValidate="CustomValidatorTipoUnidade_ServerValidate">
                            </asp:CustomValidator>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:ImageButton ID="btnSalvar" runat="server" OnClick="btnAddVinculo_Click" CausesValidation="true"
                            ImageUrl="~/Agendamento/img/salvar_1.png" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server"
                ShowMessageBox="true" ShowSummary="false" />
        </fieldset>
        <p>
            &nbsp;
        </p>
        <fieldset style="width: 880px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
            <legend>Vínculos</legend>
            <p>
                <asp:UpdatePanel ID="UpdatePanelVinculos" runat="server" ChildrenAsTriggers="true"
                    UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlProgramaDeSaude" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
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
                                                        <asp:BoundField DataField="Estabelecimento" HeaderText="Unidade" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoSolicitanteToString" HeaderText="Solicitante" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoExecutanteToString" HeaderText="Executante" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Inativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                                    CausesValidation="false" CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja INATIVAR este Vínculo?');"
                                                                    Text="">
                                                                    <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="LabelSemRegistro" runat="server" Text="Nenhum Registro Encontrado!"
                                                            Font-Size="X-Small" ForeColor="Red"></asp:Label>
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
                                                        <asp:BoundField DataField="Estabelecimento" HeaderText="Unidade" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoSolicitanteToString" HeaderText="Solicitante" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TipoExecutanteToString" HeaderText="Executante" ReadOnly="true">
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Reativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                            ItemStyle-VerticalAlign="Middle">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="cmdReativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                                    CausesValidation="false" CommandName="Reativar" OnClientClick="javascript : return confirm('Tem certeza que deseja REATIVAR este Vínculo?');"
                                                                    Text="">
                                                                    <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="LabelSemRegistro1" runat="server" Text="Nenhum Registro Encontrado!"
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
        </fieldset>
    </div>
</asp:Content>
