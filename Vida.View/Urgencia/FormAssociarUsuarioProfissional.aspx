<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormAssociarUsuarioProfissional.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormAssociarUsuarioProfissional" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="head" ID="c_head" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    <div id="top">
        <h2>
            Identificação de Profissional:
            <asp:Label ID="Label_UnidadeSaude" runat="server" Text=""></asp:Label></h2>
        <fieldset class="formulario">
            <legend>Filtro de Profissionais</legend>
            <asp:UpdatePanel ID="UpdatePane_FiltroCBO" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Ocupação do Profissional</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="ddlFiltroCBO" runat="server" AutoPostBack="true" DataTextField="Nome"
                                DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaProfissionais">
                                <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_FiltroProfissional" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlFiltroCBO" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Profissional</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="ddlProfissionais" Enabled="false" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="OnSelectedIndexChanged_MostrarDadosProfissional" DataTextField="NomeProfissional"
                                DataValueField="CPFProfissional">
                                <asp:ListItem Text="Selecione..." Value="0" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel_DadosProfissional" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlProfissionais" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="panel_dados_profissional" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Dados do Profissional</legend>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:Label ID="lbNomeProfissional" runat="server" Font-Bold="True"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Nascimento</span> <span>
                                <asp:Label ID="lbDataNascimentoProfissional" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">CPF</span> <span>
                                <asp:Label ID="lbCPFProfissional" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <fieldset class="formulario">
            <legend>Filtro de Usuários</legend>
            <asp:UpdatePanel ID="UpdatePanel_FiltroUsuario" runat="server" ChildrenAsTriggers="true"
                UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Usuário</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="ddlFiltroUsuarios" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_MostrarDadosUsuario">
                                <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel_DadosUsuario" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button_Cancelar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="ddlFiltroUsuarios" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="panel_dados_usuario" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Dados do Usuário</legend>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:Label ID="lbNomeUsuario" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Cartão SUS</span> <span>
                                <asp:Label ID="lbCartaoSUSUsuario" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Nascimento</span> <span>
                                <asp:Label ID="lbDataNascimentoUsuario" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome da Mãe</span> <span>
                                <asp:Label ID="lbNomeMaeUsuario" runat="server" Text="" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <fieldset class="formulario">
            <p align="center">
                <asp:ImageButton ID="btnAssociarUsuarioProfissional" runat="server" CssClass="sep_buttons"
                    ImageUrl="~/Urgencia/img/bts/btn-associar1.png" Text="Associar" OnClick="OnClick_AssociarUsuarioProfissional"
                    ValidationGroup="group_cadAssociar" Width="134px" Height="38px" />
                <asp:ImageButton ID="Button_Cancelar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                   PostBackUrl="~/Urgencia/FormExibeIdentificacaoProfissional.aspx" Width="134px" Height="38px" />
            </p>
        </fieldset>
<%--        <fieldset class="formulario">
            <legend>Usuários/Profissionais - Urgência</legend>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAssociarUsuarioProfissional" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <asp:GridView ID="GridView_UsuariosProfissionais" runat="server" AutoGenerateColumns="false"
                            AllowPaging="true" DataKeyNames="CodigoUsuario" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                            PageSize="10" PagerSettings-Mode="Numeric" Width="100%" OnRowDataBound="OnRowDataBound_GridView_UsuariosProfissionais">
                            <Columns>
                                <asp:BoundField DataField="NomeUsuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="NomeProfissional" HeaderText="Profissional" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Ocupacao" HeaderText="Ocupação" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton_Editar" runat="server" CommandArgument='<%#bind("CodigoUsuario") %>'
                                            OnClick="OnClick_EditarUsuarioProfissional">Editar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton_Excluir" runat="server" CommandArgument='<%#bind("CodigoUsuario") %>'
                                            OnClick="OnClick_ExcluirUsuarioProfissional" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir a identificação deste usuário ?');">Excluir Identificação</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                            </EmptyDataTemplate>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow_left" />
                        </asp:GridView>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>--%>
    </div>
    <div>
        <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
            ErrorMessage="Ocupação do Profissional é Obrigatório!" ValidationGroup="group_cadAssociar"
            Display="None" ControlToValidate="ddlFiltroCBO" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
            ErrorMessage="Profissional é Obrigatório!" ValidationGroup="group_cadAssociar"
            Display="None" ControlToValidate="ddlProfissionais" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
            ErrorMessage="Usuário é Obrigatório!" ValidationGroup="group_cadAssociar" Display="None"
            ControlToValidate="ddlFiltroUsuarios" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server"
            ShowMessageBox="true" ShowSummary="false" ValidationGroup="group_cadAssociar" />
    </div>
</asp:Content>
