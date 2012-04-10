<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConfirmarBaixaExame.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormConfirmarBaixaExame" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Confirmar Entrega de Exame
        </h2>
        <fieldset class="formulario1200">
            <legend>Relação</legend>
            <asp:GridView ID="GridView_Pacientes" DataKeyNames="CodigoProntuario" runat="server" Width="900px"
                AutoGenerateColumns="false" OnRowDataBound="OnRowDataBound_FormataGridView" PageSize="20"
                PagerSettings-Mode="Numeric" OnPageIndexChanging="Paginacao_Exames">
                <Columns>
                    <asp:BoundField DataField="NumeroAtendimento" HeaderText="Nº Atendimento" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PacienteNome" HeaderText="Paciente" ItemStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="PacienteDescricao" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center" />
                    <asp:TemplateField HeaderText="Exames Solicitados" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <cc1:Accordion ID="Accordion_Exames" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane_Exames" runat="server" Width="600px" HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                ContentCssClass="accordionContent">
                                        <Header>
                                            Visualizar
                                        </Header>
                                        <Content>
                                            <asp:GridView ID="GridView_Exames" runat="server" AutoGenerateColumns="false"   HeaderStyle-CssClass="accordionHeader" >
                                                <Columns>
                                                    <asp:BoundField DataField="DataSolicitacao" HeaderText="Data de Solicitação" />
                                                    <asp:BoundField DataField="Exame" HeaderText="Exame" />
                                                    <asp:BoundField DataField="Resultado" HeaderText="Resultado" ItemStyle-Width="250px" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_ConfirmarEntregaExame"  
                                                                CommandArgument='<%#bind("CodigoExame") %>' OnClientClick="javascript:return confirm('Deseja realmente confirmar a entrega deste exame ?');">Confirmar </asp:LinkButton>
                                                        
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                 <HeaderStyle CssClass="tab" />
                 <RowStyle CssClass="tabrow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="LabelEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView>
        </fieldset>
    </div>
</asp:Content>
