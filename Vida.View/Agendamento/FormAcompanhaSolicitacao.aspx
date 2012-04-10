﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormAcompanhaSolicitacao.aspx.cs"
    Inherits="ViverMais.View.Agendamento.FormAcompanhaSolicitacao" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="style_form_agendamento-siteViverMais.css" type="text/css"
        media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnPesquisarSolicitacao" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <span>Preencha os campos abaixo para fazer a sua consulta.</span><p>
                </p>
                <fieldset class="formulario">
                    <legend>Formulário</legend>
                    <p>
                        <asp:Label ID="lblCNS" runat="server" Text="Cartão SUS" CssClass="rotulo"></asp:Label>
                        <span>
                            <asp:TextBox ID="tbxCNES" runat="server" CssClass="campo" MaxLength="15"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxCNES" Display="Dynamic" ErrorMessage="O campo Cartão SUS deve conter apenas Números"
                                Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                ErrorMessage="Campo Obrigatório" ControlToValidate="tbxCNES" Display="Dynamic"></asp:RequiredFieldValidator>
                            <%--<cc1:MaskedEditExtender ID="MKEditExtender1" MaskType="Number" Mask="999999999999999"
                                AutoComplete="false" TargetControlID="tbxCNES" ClearMaskOnLostFocus="true" runat="server">
                            </cc1:MaskedEditExtender>--%>
                        </span>
                    </p>
                    <p>
                        <asp:Label ID="lblProtocolo" runat="server" Text="Nº Protocolo" CssClass="rotulo"></asp:Label>
                        <span>
                            <asp:TextBox ID="tbxNumProtocolo" runat="server" CssClass="campo" MaxLength="13"></asp:TextBox>
                            <%--<cc1:MaskedEditExtender ID="MaskedEditExtender1" MaskType="Number" Mask="9999999999999" AutoComplete="false" TargetControlID="tbxNumProtocolo" ClearMaskOnLostFocus="true" runat="server"></cc1:MaskedEditExtender>--%>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnPesquisarSolicitacao" runat="server" OnClick="btnPesquisarSolicitacao_Click">
                <img id="imgpesquisar" alt="Buscar Solicitação" src="../img/btn-pesquisar.png" style="border:none;"
                onmouseover="imgpesquisar.src='../img/btn-pesquisar2.png';"
                onmouseout="imgpesquisar.src='../img/btn-pesquisar.png';" />
                        </asp:LinkButton></div>
                    <asp:Panel ID="PanelDadosPaciente" runat="server">
                        <p>
                            &nbsp
                        </p>
                        <p>
                            &nbsp
                        </p>
                        <p>
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
                        </p>
                        <p>
                            &nbsp
                        </p>
                        <p style="font-family: Verdana; font-size: 11px;">
                            <b>Lista das Solicitações</b>
                        </p>
                        <p>
                            <asp:Label ID="lblSemRegistros" runat="server" Text="Não Existem Solicitações para o Paciente Informado."
                                Font-Bold="true" ForeColor="Red"></asp:Label>
                        </p>
                        <asp:Panel ID="PanelExibeSolicitacoes" runat="server">
                            <p>
                                <asp:GridView ID="GridViewSolicitacosConfirmadas" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True" OnRowCommand="GridViewSolicitacosConfirmadas_RowCommand"
                                    OnPageIndexChanging="GridViewSolicitacosConfirmadas_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo">
                                            <HeaderStyle CssClass="colunaEscondida" />
                                            <ItemStyle CssClass="colunaEscondida" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomePaciente" HeaderText="Paciente" />
                                        <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                        <asp:BoundField DataField="DataSolicitacao" HeaderText="Data Solicitação" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Unidade" HeaderText="Unidade de Saúde" />
                                        <asp:TemplateField HeaderText="Imprimir">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="cmdImprimeLaudo" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                    CommandName="ImprimeLaudo" OnClientClick="javascript : return confirm('Visualizar o Laudo?');">
                                                    <asp:Image ID="Imprime_laudo" runat="server" ImageUrl="~/Agendamento/img/btn-laudo.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imprimir">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="cmdImprimeSolicitacao" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                    CommandName="ImprimeSolicitacao" OnClientClick="javascript : return confirm('Confirma a Impressão da Autorização?');">
                                                    <asp:Image ID="Autoriza" runat="server" ImageUrl="~/Agendamento/img/btn-autoriza.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Imprimir">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="cmdImprimeJustificativa" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                    CommandName="ImprimeJustificativa" OnClientClick="javascript : return confirm('Visualizar a Justificativa?');">
                                                    <asp:Image ID="Indeferimento" runat="server" ImageUrl="~/Agendamento/img/btn-indeferimento.png" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:CommandField ShowSelectButton="true" ButtonType="Image" HeaderText="Imprimir"
                                            SelectImageUrl="~/Agendamento/img/btn-rel.png"></asp:CommandField>--%>
                                    </Columns>
                                    <HeaderStyle CssClass="tab" BackColor="#28718e" Font-Bold="True" ForeColor="#ffffff"
                                        Height="16px" Font-Size="11px" />
                                    <FooterStyle BackColor="#72b4cf" ForeColor="#ffffff" />
                                    <RowStyle CssClass="tabrow" BackColor="#72b4cf" ForeColor="#ffffff" Font-Size="10px" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#72b4cf" ForeColor="#ffffff" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#72b4cf" />
                                </asp:GridView>
                            </p>
                        </asp:Panel>
                        <asp:Panel ID="PanelVisualizaJustificativa" runat="server" Visible="false">
                            <div id="cinza" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 300%;
                                z-index: 100; min-height: 100%; background-color: #999;" visible="false">
                            </div>
                            <div id="mensagem" style="position: absolute; top: 190px; left: 10%; width: 700px;
                                z-index: 102; background-color: #FFFFFF; border-right: #336699 2px solid; padding-right: 20px;
                                border-top: #336699 2px solid; padding-left: 20px; padding-bottom: 10px; border-left: #336699 2px solid;
                                color: #000000; padding-top: 10px; border-bottom: #336699 2px solid; text-align: justify;
                                font-family: Verdana;" visible="false">
                                <p style="height: 10px;">
                                    <span style="margin-left: 96%;">
                                        <asp:LinkButton ID="btnFechar" runat="server" CausesValidation="false" OnClick="btnFechar_Click">
                                            <img id="Img1" src="~/Agendamento/img/close24.png" alt="Fechar" runat="server" />
                                        </asp:LinkButton>
                                    </span>
                                </p>
                                <p>&nbsp;</p>
                                <p>
                                    <asp:Label ID="lblCabecalho" runat="server" Text="JUSTIFICATICA PARA O INDEFERIMENTO" Height="16px" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                    <asp:Label ID="lblJustificativa" runat="server" Font-Italic="true"></asp:Label>
                                </p>
                            </div>
                        </asp:Panel>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
