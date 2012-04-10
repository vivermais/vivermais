﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPesquisarProfissionalSolicitante.ascx.cs"
    Inherits="ViverMais.View.Profissional.WUCPesquisarProfissionalSolicitante" %>
<fieldset class="formulario">
    <legend>Pesquisa de Profissional de Saúde</legend>
    <asp:Panel ID="PanelDados" runat="server">
        <p>
            <asp:Label ID="lblCategoria" runat="server" Text="Categoria" CssClass="rotulo"></asp:Label>
            <span>
                <asp:DropDownList ID="ddlCategoria" runat="server" ValidationGroup="Pesquisa" CssClass="drop">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlCategoria"
                    ErrorMessage="*" ForeColor="Red" Font-Bold="true" InitialValue="0"></asp:RequiredFieldValidator>
            </span>
        </p>
        <p>
            <span class="rotulo">
                <asp:Label ID="lblNumConselho" runat="server" Text="Número Conselho"></asp:Label></span>
            <span>
                <asp:TextBox ID="tbxNumConselho" runat="server" CssClass="campo" ValidationGroup="Pesquisa"></asp:TextBox></span>&nbsp
            <span style="position: absolute; margin-top: 3px;">
                    <asp:ImageButton ID="imgBuscarProfissional" runat="server" CausesValidation="true"
                        ValidationGroup="Pesquisa" OnClick="btnBuscarProfissional_Click" ImageUrl="~/Agendamento/img/procurar.png"
                        Width="16px" Height="16px" />
                </span>
        </p>
        <p>
            <span class="rotulo">
                <asp:Label ID="lblnomeProfissionalPesquisa" runat="server" Text="Nome"></asp:Label></span>
            <span>
                <asp:TextBox ID="tbxNomeProfissionalPesquisa" runat="server" CssClass="campo" ValidationGroup="Pesquisa"></asp:TextBox></span>&nbsp
            <span style="position: absolute; margin-top: 3px;">
                    <asp:ImageButton ID="imgBuscarProfissionalPorNome" runat="server" CausesValidation="true"
                        ValidationGroup="Pesquisa" OnClick="btnBuscarProfissional_Click" ImageUrl="~/Agendamento/img/procurar.png"
                        Width="16px" Height="16px" />
                </span>
        </p>
        <p>
            <asp:Label ID="lblSemPesquisa" runat="server" Text="Selecione um método para Pesquisa"
                ForeColor="Red"></asp:Label></p>
        <p>
            
            <%--<p>
            <asp:LinkButton ID="lnkBtnPesquisar" runat="server" OnClick="lnkBtnPesquisar_Click">Pesquisar</asp:LinkButton>
        </p>
        <p>--%>
            <asp:GridView ID="GridViewListaProfissionais" runat="server" AutoGenerateColumns="false"
                AllowPaging="true" PageSize="6" Width="100%" OnPageIndexChanging="GridViewListaProfissionais_PageIndexChanging"
                AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="GridViewListaProfissionais_RowCommand">
                <%--OnSelectedIndexChanged="GridViewListaProfissionais_SelectedIndexChanged">--%>
                <Columns>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selecionar">
                        <ItemTemplate>
                            <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                CommandName="Select">
                                <asp:Image ID="imgSelect" ImageUrl="~/img/bts/select.png" runat="server" />
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                    <asp:BoundField DataField="Nome" HeaderText="Nome Profissional" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="OrgaoEmissorRegistro" HeaderText="Ocupacão" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="NumeroRegistro" HeaderText="Nº Conselho" ItemStyle-HorizontalAlign="Center" />

                    <%--<asp:CommandField ButtonType="Image" ItemStyle-HorizontalAlign="Center" SelectImageUrl="~/img/bts/bt_edit.png"
                    SelectText="Selecionar" ShowSelectButton="True" ItemStyle-Width="21px" >
                    <ItemStyle 
                    </asp:CommandField>--%>
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                </EmptyDataTemplate>
                <AlternatingRowStyle BackColor="LightBlue" />
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow_left" />
            </asp:GridView>
        </p>
    </asp:Panel>
    <asp:Panel ID="PanelExibeDados" runat="server" Visible="false">
        <p>
            <span class="rotulo">Nome</span> <span>
                <asp:Label ID="lblNome" runat="server" Text="" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="13px" Style="display: block;"></asp:Label>
            </span>
            <br />
        </p>
        <p>
            <span class="rotulo">Orgão Emissor</span> <span>
                <asp:Label ID="lblOrgaoEmissor" runat="server" Text="" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="13px" Style="display: block;"></asp:Label>
            </span>
            <br />
        </p>
        <p>
            <span class="rotulo">Nº Conselho</span> <span>
                <asp:Label ID="lblNumeroConselho" runat="server" Text="" Font-Bold="true" Font-Names="Verdana"
                    Font-Size="13px" Style="display: block;"></asp:Label>
            </span>
            <br />
        </p>
        <p>
            <span>Para modificar o Profissional de Saúde,
                <asp:LinkButton ID="lnkBtnModificarProfissional" Text="clique aqui!" runat="server"
                    CausesValidation="false" OnClick="lnkBtnModificarProfissional_Click" /></span>
        </p>
    </asp:Panel>
</fieldset>
