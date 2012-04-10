<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExibeFilaAcolhimento.aspx.cs"
    Inherits="ViverMais.View.Atendimento.ExibeFilaAcolhimento" MasterPageFile="~/Atendimento/MasterAtendimento.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <asp:Timer ID="Timer_Temporizador" runat="server" Interval="10000" OnTick="OnTick_Temporizador">
        </asp:Timer>
        <h2>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Acolhimento"></asp:Label></h2>
        <asp:UpdatePanel ID="UpdatePanel_Acolhimento" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Timer_Temporizador" EventName="Tick" />
            </Triggers>
            <ContentTemplate>
                <div style="margin: 0 auto; width: 200px; margin-top: 30px;">
                    <asp:DataList ID="DataList_TipoAcolhimento" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow" RepeatColumns="6" DataKeyField="Codigo">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton_EscolherFilaAcolhimento" runat="server" OnClick="OnClick_Acolhimento"
                                CommandArgument='<%#bind("Codigo") %>' Text='<%#bind("Nome") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:DataList>
                </div>
                <fieldset class="formulario" style="width: 850px;float:left;">
                    <legend>Relação</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridFila" runat="server" AutoGenerateColumns="False" Width="100%"
                                DataKeyNames="Codigo" OnInit="OnInit_gridFila">
                                <Columns>
                                    <asp:BoundField HeaderText="Horário de Entrada" DataField="Data" ItemStyle-Width="150px"
                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                    <asp:HyperLinkField DataNavigateUrlFields="Codigo" Target="_parent" DataNavigateUrlFormatString="FormAcolhimento.aspx?codigo={0}"
                                        HeaderStyle-HorizontalAlign="Center" DataTextField="NumeroToString" HeaderText="Identificador"
                                        ItemStyle-HorizontalAlign="center">
                                        <ControlStyle Width="100px" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="NomePacienteToString" HeaderText="Paciente" ItemStyle-Height="25px"
                                        ItemStyle-Width="250px" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PacienteDescricao" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="400px" />
                                    <asp:TemplateField HeaderText="Reimpressão de Senha" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton_ReimprimirSenha" runat="server" OnClientClick='<%# Eval("Codigo", "window.open(\"FormImprimirSenhaAcolhimentoAtendimento.aspx?co_prontuario={0}&tipo_impressao=acolhimento\",\"Impressão\",\"height = 270, width = 250\");") %>'>
                                                <img id="imgvoltar" alt="Reimpressão de Senha" src="img/bts/imprimir1.png"
                                                    onmouseover="this.src='img/bts/imprimir2.png';"
                                                    onmouseout="this.src='img/bts/imprimir1.png';" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
