<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormExibeSalaVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormExibeSalaVacina"
    Title="ViverMais - Sala de Vacina" %>

<%@ Register Src="~/Vacina/WUC_PesquisarSalaVacina.ascx" TagName="PesquisarSala"
    TagPrefix="WSV" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Lista de Salas de Vacina Cadastradas</h2>
        <div class="botoesroll">
            <asp:LinkButton ID="Lkn_Novo" runat="server" PostBackUrl="~/Vacina/FormSalaVacina.aspx">
                  <img id="imgnovoregistro" alt="Novo Registro" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
        </div>
        <fieldset class="formulario">
            <legend>Pesquisar</legend>
            <WSV:PesquisarSala ID="WUC_PesquisarSala" runat="server"></WSV:PesquisarSala>
            <asp:UpdatePanel ID="UpdatePanelPesquisaSalaVacina" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="true" RenderMode="Inline">
                <ContentTemplate>
                    <asp:Panel ID="Panel_ResultadoPequisa" runat="server" Visible="false">
                        <p>
                            <span>
                                <asp:GridView ID="GridView_SalaVacina" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="10"
                                    PagerSettings-Mode="Numeric" Width="100%" BackColor="White" BorderColor="#f9e5a9"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                    <Columns>
                                        <asp:BoundField DataField="NomeUnidade" HeaderText="Unidade" ItemStyle-Width="400px" />
                                        <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Vacina/FormSalaVacina.aspx?co_salavacina={0}"
                                            DataTextField="Nome" ItemStyle-Width="300px" />
                                        <asp:BoundField HeaderText="Ativo" DataField="AtivoToString" ItemStyle-Width="50px"
                                            ItemStyle-Font-Bold="true" />
                                        <asp:BoundField HeaderText="Bloqueado" DataField="BloqueadoToString" ItemStyle-Width="100px"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Font-Bold="true" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                </asp:GridView>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
