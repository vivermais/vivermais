<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeExameEletivo.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormExibeExameEletivo" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>Lista de Exames</h2>
        <fieldset class="formulario">
            <legend>Relação</legend>
            <p>
                <asp:LinkButton ID="LinkButton_NovoRegistro" runat="server" PostBackUrl="~/Urgencia/FormExameEletivo.aspx">
            <img id="imgnovo" alt="Novo Registro" src="img/novo-registro1.png"
                  onmouseover="this.src='img/novo-registro2.png';"
                  onmouseout="this.src='img/novo-registro1.png';" />
                </asp:LinkButton>
            </p>
            <p>
                <span>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="gridExames" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                                Width="100%" DataKeyNames="Codigo">
                                <Columns>
                                    <asp:BoundField DataField="Descricao" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="StatusToString" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                    <asp:HyperLinkField ItemStyle-HorizontalAlign="Center" DataNavigateUrlFields="Codigo"
                                        Text="Selecionar" DataNavigateUrlFormatString="~/Urgencia/FormExameEletivo.aspx?co_exame={0}" />
                                    <%--                        <asp:CommandField SelectText="Selecionar" ShowSelectButton="True" ItemStyle-HorizontalAlign="Center">
                        </asp:CommandField>--%>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow_left" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
