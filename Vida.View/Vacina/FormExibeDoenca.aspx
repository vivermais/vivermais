<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeDoenca.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormExibeDoenca" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Lista de Doenças</h2>
                <fieldset class="formulario">
                    <legend>Doenças</legend>
                    <div class="botoesroll">
                        <asp:LinkButton ID="Lnk_Novo" runat="server" PostBackUrl="~/Vacina/FormDoenca.aspx">
                  <img id="imgnovoregistro" alt="Adicionar" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                    <br>
                    <p>
                        <asp:GridView ID="GridView_Doenca" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                            OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric"
                            BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" Width="100%">
                            <Columns>
                                <asp:HyperLinkField HeaderText="Nome" DataNavigateUrlFields="Codigo"
                                    DataNavigateUrlFormatString="~/Vacina/FormDoenca.aspx?co_doenca={0}"
                                    DataTextField="Nome" >
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:HyperLinkField>
                            </Columns>
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                Height="24px" Font-Size="13px" />
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
