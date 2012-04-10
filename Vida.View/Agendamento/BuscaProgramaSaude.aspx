<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="BuscaProgramaSaude.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaProgramaSaude"
    Title="Módulo Regulação - Programas de Saúde" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Programas de Saúde</h2>
        <br />
        <div class="botoesroll">
            <span>Clique no botão abaixo para cadastrar um novo Programa de Saúde</span>
            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Agendamento/FormCadastrarProgramaSaude.aspx">
                       <img id="img_Programa" alt="" src="img/btn-cad-prog-saude1.png"
                onmouseover="img_Programa.src='img/btn-cad-prog-saude2.png';"
                onmouseout="img_Programa.src='img/btn-cad-prog-saude1.png';" />
            </asp:LinkButton>
        </div>
        <br />
        <fieldset class="formulario">
            <legend>Lista dos Programas de Saúde Cadastrados</legend>
            <p>&nbsp;</p>
                <asp:Panel runat="server" ID="PanelPactosAtivosInativos" Visible="true">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabelPanel_Um" runat="server" HeaderText="Programas Ativos">
                            <ContentTemplate>            
                                <p>
                                    <asp:GridView ID="GridViewListaProgramasAtivos" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Codigo"
                                        OnRowCommand="GridViewListaProgramas_RowCommand" OnPageIndexChanging="GridViewListaProgramasAtivos_PageIndexChanging"
                                        BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                                        <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                        <RowStyle BackColor="#a6c5de" ForeColor="Black" Font-Size="11px" />
                                        <Columns>
                                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormCadastrarProgramaSaude.aspx?codigo={0}"
                                                DataTextField="Nome" HeaderText="Programas" ItemStyle-CssClass="" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:HyperLinkField>
                                            <asp:BoundField DataField="MultiDisciplinarToString" HeaderText="Multi-Disciplinar" />
                                            <asp:TemplateField HeaderText="Inativar" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                        CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja inativar este Programa?');"
                                                        Text="">
                                                        <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblSemRegistro2" runat="server" Text="Nenhum Registro Encontrado" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </EmptyDataTemplate>
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                        <AlternatingRowStyle BackColor="#c2dcf2" />
                                    </asp:GridView>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Programas Inativos">
                            <ContentTemplate>
                                <p>
                                    <asp:GridView ID="GridViewListaProgramasInativos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        OnPageIndexChanging="GridViewListaProgramasInativos_PageIndexChanging" BackColor="White" BorderColor="#477ba5" 
                                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%" DataKeyNames="Codigo">
                                        <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                        <RowStyle BackColor="#a6c5de" ForeColor="Black" Font-Size="11px" />
                                        <Columns>
                                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                                            <asp:BoundField DataField="MultiDisciplinarToString" HeaderText="Multi-Disciplinar" />
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblSemRegistro" runat="server" Text="Nenhum Registro Encontrado" Font-Size="Small" ForeColor="Red"></asp:Label>
                                        </EmptyDataTemplate>
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                        <AlternatingRowStyle BackColor="#c2dcf2" />
                                    </asp:GridView>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
