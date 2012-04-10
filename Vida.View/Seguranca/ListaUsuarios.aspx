<%@ Page Language="C#" MasterPageFile="~/Seguranca/MasterSeguranca.Master" AutoEventWireup="true"
    CodeBehind="ListaUsuarios.aspx.cs" Inherits="ViverMais.View.Seguranca.ListaUsuarios"
    Title="Untitled Page" EnableEventValidation="false" %>

<%@ Register Src="~/Seguranca/WUCPesquisarUsuario.ascx" TagName="TagPesquisarUsuario" TagPrefix="WUC_Usuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Lista de Usuários</h2>
            <%--<asp:Panel ID="Panel_WUC" runat="server"></asp:Panel>--%>
        <%--<fieldset class="formulario">
            <legend>Relação</legend>--%>
            <WUC_Usuario:TagPesquisarUsuario ID="WUC_PesquisarUsuario" runat="server" />
            <%--<asp:Panel ID="PanelPesquisa" runat="server" DefaultButton="lnkBtnPesquisar">
                <p>
                    <span class="rotulo">Nome</span><span><asp:TextBox ID="tbxNome" runat="server" CssClass="campo"></asp:TextBox></span>
                    <span>
                        <asp:LinkButton ID="lnkBtnPesquisar" runat="server" OnClick="lnkBtnPesquisar_Click">
                    <img id="imgpesquisar" alt="Pesquisar" src="img/procurar.png"
                  onmouseover="imgpesquisar.src='img/procurar.png';"
                  onmouseout="imgpesquisar.src='img/procurar.png';" />
                        </asp:LinkButton></span>
                </p>
            </asp:Panel>
            <p>
                <asp:GridView ID="GridView_Usuarios" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="20" OnPageIndexChanging="OnPageIndexChanging_Paginacao" PagerSettings-Mode="Numeric"
                    CellPadding="3" GridLines="Vertical" BackColor="White" BorderColor="#999999"
                    BorderStyle="None" BorderWidth="1px" Width="666px">
                    <Columns>
                        <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFormatString="FormUsuario.aspx?co_usuario={0}"
                            DataNavigateUrlFields="Codigo" DataTextField="Nome" HeaderStyle-Width="350px">
                        </asp:HyperLinkField>
                        <asp:BoundField HeaderText="Cartão SUS" DataField="CartaoSUS" HeaderStyle-Width="170px"
                            ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                        <asp:BoundField HeaderText="Unidade" DataField="UnidadeToString" HeaderStyle-Width="380px">
                            <HeaderStyle Width="700px"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="tab" BackColor="#5b5b5b" Font-Bold="True" ForeColor="White"
                        Font-Size="13px" Font-Names="Verdana" Height="27px" />
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle CssClass="tabrow_left" BackColor="#EEEEEE" ForeColor="Black" Height="23px"
                        Font-Names="Verdana" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                </asp:GridView>
            </p>--%>
       <%-- </fieldset>--%>
    </div>
</asp:Content>
