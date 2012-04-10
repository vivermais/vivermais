<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true" CodeBehind="FormExibeVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormExibeVacina" Title="ViverMais - Vacina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Lista de Imunobiológicos Cadastrados</h2>
                <fieldset class="formulario">
                    <legend>Imunobiológicos</legend>
                    
                    <div class="botoesroll">
                  <asp:LinkButton ID="Lnk_Novo" runat="server" PostBackUrl="~/Vacina/FormVacina.aspx" >
                  <img id="imgnovoregistro" alt="Adicionar" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                        </div>
                    <br /><br>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Vacina" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                             OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric" BackColor="White" BorderColor="#f9e5a9" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal" Font-Names="Verdana">
                                <Columns>
                                    <asp:BoundField HeaderText="Código" DataField="CodigoCMADI" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" />
                                    <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo" ItemStyle-HorizontalAlign="Center"
                                     DataNavigateUrlFormatString="~/Vacina/FormVacina.aspx?co_vacina={0}"
                                     DataTextField="Nome"/>
                                </Columns>
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" 
                                    ForeColor="#383838" Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
