<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormReceberRemanejamento.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormReceberRemanejamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Medicamentos Remanejados</h2>
                <fieldset class="formulario">
                    <legend>Itens Registrados</legend>
                    <p>
                        <span class="rotulo">Farmácia</span> <span>
                            <asp:DropDownList ID="DropDownList_Farmacia" runat="server" CausesValidation="true"
                             AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaRemanejamento"
                             DataTextField="Nome" DataValueField="Codigo">
                                <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_ItensRemanejamento" runat="server" AutoGenerateColumns="false"
                                AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" Width="660px" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                <Columns>
                                    <asp:BoundField HeaderText="Data de Abertura" DataField="DataAbertura" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                    <asp:BoundField HeaderText="Farmácia de Origem" DataField="FarmaciaOrigem" />
                                    <%--<asp:BoundField HeaderText="Status" DataField="DescricaoStatus" />--%>
                                    <asp:HyperLinkField DataNavigateUrlFields="Codigo" Text="Ver Itens" DataNavigateUrlFormatString="~/Farmacia/FormItensRemanejamento.aspx?co_remanejamento={0}" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
