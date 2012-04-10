<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeCampanha.aspx.cs" Inherits="ViverMais.View.Vacina.FormExibeCampanha" MasterPageFile="~/Vacina/MasterVacina.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Lista de Campanhas</h2>
                <fieldset class="formulario">
                    <legend>Campanhas</legend>
                     <div class="botoesroll">
                  <asp:LinkButton ID="Lnk_Novo" runat="server" PostBackUrl="~/Vacina/FormCampanha.aspx" >
                  <img id="imgnovoregistro" alt="Adicionar" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                        </div>
                    <br /><br>
                    <p>
                            <asp:GridView ID="GridView_Campanha" runat="server" AutoGenerateColumns="false" AllowPaging="true" Width="690px"
                             OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric" BackColor="White" BorderColor="#f9e5a9" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo" ItemStyle-HorizontalAlign="Center"
                                     DataNavigateUrlFormatString="~/Vacina/FormCampanha.aspx?co_campanha={0}"
                                     DataTextField="Nome" ItemStyle-Width="690px"/>
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
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>