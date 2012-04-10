<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true" CodeBehind="FormExibeUnidadeMedidaVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormExibeUnidadeMedidaVacina" Title="ViverMais - Unidade de Medida de Vacina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Lista de Unidades de Medida para Vacina</h2>                <fieldset class="formulario">
                    <legend>Relação</legend>
                    <div class="botoesroll">
                  <asp:LinkButton ID="Lnk_Novo" runat="server" PostBackUrl="~/Vacina/FormUnidadeMedidaVacina.aspx" >
                  <img id="imgnovo" alt="Novo Registro" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovo.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovo.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                        </div><br /><br />
                    <p>
                            <asp:GridView ID="GridView_UnidadeMedidaVacina" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                             OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric" Width="690px" BackColor="White" BorderColor="#E7E7FF" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo"
                                     DataNavigateUrlFormatString="~/Vacina/FormUnidadeMedidaVacina.aspx?co_unidademedida={0}"
                                     DataTextField="Nome" ItemStyle-Width="300px"/>
                                     <asp:BoundField DataField="Sigla" HeaderText="Sigla" ItemStyle-Width="100px" />
                                     <asp:TemplateField HeaderText="Excluir">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_ExcluirUnidadeMedida" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir esta unidade de medida ?');return false;" >
                                          <img id="imgexcluir" alt="Excluir" src="img/excluir_gridview.png"
                                          onmouseover="imgexcluir.src='img/excluir_gridview.png';"
                                          onmouseout="imgexcluir.src='img/excluir_gridview.png';" /></asp:LinkButton>
                                        </ItemTemplate>
                                     </asp:TemplateField>
                                </Columns>
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" 
                                    ForeColor="#383838" Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#4A3C8C" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
