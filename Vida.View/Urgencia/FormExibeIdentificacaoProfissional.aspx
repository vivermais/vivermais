<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeIdentificacaoProfissional.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormExibeIdentificacaoProfissional" EnableEventValidation="false"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Lista de Profissionais Identificados</h2>
        <fieldset class="formulario">
            <legend><asp:Label ID="Label_Unidade" runat="server" Text=""></asp:Label></legend>
            <p>
                <asp:LinkButton ID="LinkButton_NovoRegistro" runat="server" PostBackUrl="~/Urgencia/FormAssociarUsuarioProfissional.aspx">
            <img id="imgnovo" alt="Novo Registro" src="img/novo-registro1.png"
                  onmouseover="this.src='img/novo-registro2.png';"
                  onmouseout="this.src='img/novo-registro1.png';" />
                </asp:LinkButton>
            </p>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <p>
                        <asp:GridView ID="GridView_UsuariosProfissionais" runat="server" AutoGenerateColumns="false"
                            AllowPaging="true" DataKeyNames="CodigoUsuario" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                            PageSize="10" PagerSettings-Mode="Numeric" Width="100%">
                            <Columns>
                                <asp:BoundField DataField="NomeUsuario" HeaderText="Usuário" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="NomeProfissional" HeaderText="Profissional" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Ocupacao" HeaderText="Ocupação" ItemStyle-HorizontalAlign="Center" />
                                <asp:HyperLinkField ItemStyle-HorizontalAlign="Center" HeaderText="Editar" DataNavigateUrlFields="CodigoUsuario"
                                    Text="Selecionar" DataNavigateUrlFormatString="~/Urgencia/FormAssociarUsuarioProfissional.aspx?co_usuario={0}" />
                                <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Editar" runat="server" CommandArgument='<%#bind("CodigoUsuario") %>'
                                        OnClick="OnClick_EditarUsuarioProfissional">Editar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
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
        </fieldset>
    </div>
</asp:Content>
