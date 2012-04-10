<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormReImpressaoGuia.aspx.cs" Inherits="ViverMais.View.Agendamento.FormReImpressaoGuia"
    Title="Untitled Page" %>

<%@ Register Src="../Paciente/WUCPesquisarPaciente.ascx" TagName="WUCPesquisarPaciente"
    TagPrefix="uc1" %>
<%@ Register Src="../Paciente/WUCExibirPaciente.ascx" TagName="WUCExibirPaciente"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Reimpressão de Guia</h2>
        <uc1:WUCPesquisarPaciente ID="WUCPesquisarPaciente1" runat="server" />
        <%--<uc2:WUCExibirPaciente ID="WUCExibirPaciente1" runat="server" />--%>
        <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Panel ID="PanelExibeSolicitacoes" runat="server" Visible="false">
                    <p>
                        &nbsp</p>
                    <p>
                        &nbsp</p>
                    <%--<p>
                        <asp:Label ID="Label1" runat="server" CssClass="rotulo" Text="Nome Paciente"></asp:Label>
                        <span>
                            <asp:Label ID="lblNomePaciente" runat="server" Font-Bold="true"></asp:Label></span>
                    </p>
                    <p>
                        <asp:Label ID="Label2" runat="server" CssClass="rotulo" Text="Nome Mãe"></asp:Label>
                        <span>
                            <asp:Label ID="lblNomeMae" runat="server" Font-Bold="true"></asp:Label></span>
                    </p>
                    <p>
                        <asp:Label ID="Label3" runat="server" CssClass="rotulo" Text="Data Nascimento"></asp:Label>
                        <span>
                            <asp:Label ID="lblDataNascimento" runat="server" Font-Bold="true"></asp:Label></span>
                    </p>--%>
                    
                    <p style="font-family: Verdana; font-size: 11px; margin-top:30px;">
                        <b>Lista das Solicitações</b>
                    </p>
<%--                    <p>
                        <asp:Label ID="lblSemRegistros" runat="server" Text="Não Existem Solicitações para o Paciente Informado."
                            Font-Bold="true" ForeColor="Red"></asp:Label>
                    </p>--%>
                    <p>
                        <asp:GridView ID="GridViewSolicitacosPendentesAutorizadas" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCommand="GridViewSolicitacosPendentesAutorizadas_RowCommand"
                            OnPageIndexChanging="GridViewSolicitacosPendentesAutorizadas_OnPageIndexChanging"
                            BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Vertical" Width="100%">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                            <Columns>
                                <asp:BoundField DataField="Codigo">
                                    <HeaderStyle CssClass="colunaEscondida" />
                                    <ItemStyle CssClass="colunaEscondida" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomePaciente" HeaderText="Paciente" />
                                <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" ItemStyle-Font-Bold="true"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="DataSolicitacao" HeaderText="Data Solicitação" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Font-Bold="true" />
                                <asp:BoundField DataField="Unidade" HeaderText="Unidade de Saúde" />
                                <asp:TemplateField HeaderText="Imprimir Laudo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdImprimeLaudo" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                            CommandName="ImprimeLaudo" OnClientClick="javascript : return confirm('Confirma a Impressão do Laudo?');">
                                            <asp:Image ID="Imprime_laudo" runat="server" ImageUrl="~/Agendamento/img/btn-laudo.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprimir Autorização" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdImprimeAutorizacao" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                            CommandName="ImprimeAutorizacao" OnClientClick="javascript : return confirm('Confirma a Impressão da Autorização?');">
                                            <asp:Image ID="Autoriza" runat="server" ImageUrl="~/Agendamento/img/btn-autoriza.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprimir Protocolo" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdImprimeProtocolo" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                            CommandName="ImprimeProtocolo" OnClientClick="javascript : return confirm('Confirma a Impressão do Protocolo de Atendimento?');">
                                            <asp:Image ID="Imprime_Protocolo" runat="server" ImageUrl="~/Agendamento/img/btn-protocolo.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Imprimir Justificativa">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdImprimeJustificativa" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                            CommandName="ImprimeJustificativa" OnClientClick="javascript : return confirm('Visualizar a Justificativa?');">
                                            <asp:Image ID="Indeferimento" runat="server" ImageUrl="~/Agendamento/img/btn-indeferimento.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblSemRegistros" runat="server" Text="Não Existem Solicitações para este paciente ou nenhuma solicitação foi realizada por esta unidade para o paciente informado."
                                Font-Bold="true" ForeColor="Red"></asp:Label>
                            </EmptyDataTemplate>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="11px" Height="22px" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
                        </asp:GridView>
                    </p>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
