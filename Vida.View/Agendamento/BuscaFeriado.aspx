<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="BuscaFeriado.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaFeriado"
    Title="Módulo Regulação - Cadastro de Feriados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Cadastro de Feriados</h2>
        <br />
        <div>
        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Agendamento/FormFeriado.aspx">
                       <img id="img_Feriado" alt="Cadastrar Novo Feriado" src="img/btn-cad-novo-feriad1.png"
                onmouseover="img_Feriado.src='img/btn-cad-novo-feriad2.png';"
                onmouseout="img_Feriado.src='img/btn-cad-novo-feriad1.png';" />
            </asp:LinkButton></div>

        <br />
        
        <asp:Panel ID="PanelListaFeriados" runat="server">
            <fieldset class="formulario">
                <legend>Lista dos Feriados Cadastrados</legend>
                <asp:GridView ID="GridViewListaFeriados" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnRowCommand="GridViewListaFeriados_RowCommand" OnPageIndexChanging="GridViewListaFeriados_PageIndexChanging" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" Width="100%">
                                    <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                    <RowStyle BackColor="#b3cfe6" ForeColor="Black" Font-Size="11px" />
                    <Columns>
                        <asp:BoundField DataField="Codigo">
                            <HeaderStyle CssClass="colunaEscondida" />
                            <ItemStyle CssClass="colunaEscondida" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormFeriado.aspx?codigo={0}"
                            DataTextField="Descricao" HeaderText="Feriados" ItemStyle-CssClass="" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="DataFormatada" HeaderText="Data" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Nome" HeaderText="Tipo" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="cmdDelete" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                    CommandName="Excluir" OnClientClick="javascript : return confirm('Tem certeza que deseja excluir este Feriado?');"
                                    Text="">
                                    <asp:Image ID="Excluir" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label runat="server" Text="Nenhum Registro Encontrado!" Font-Size="X-Small"></asp:Label>
                    </EmptyDataTemplate>
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#c2dcf2" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
