<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormGerarBPA.aspx.cs" Inherits="ViverMais.View.Agendamento.FormGerarBPA"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Always">
        <Triggers>

            <%--<asp:PostBackTrigger ControlID="btnBaixarArquivo" />--%>
<%--            <asp:PostBackTrigger ControlID="btnImprimeRelatorio" />
--%>        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Gerar BPA / APAC</h2>
                <fieldset class="formulario">
                    <legend>Formulário</legend>
                    <p>
                        <span>
                            <asp:Label ID="lblCompetencia" runat="server" CssClass="rotulo" Text="Competência (AAAAMM)"></asp:Label></span>
                        <span>
                            <asp:TextBox CssClass="campo" ID="tbxCompetencia" runat="server" MaxLength="6" Width="60px"></asp:TextBox></span>
                        <%--<span class="rotulo">Competência (AAAAMM):</span>--%>
                        <%--<cc1:MaskedEditExtender MaskType="Date" ID="MaskedEditExtender2" runat="server" TargetControlID="tbxCompetencia"
                    Mask="9999/99" ClearMaskOnLostFocus="true">
                </cc1:MaskedEditExtender>--%>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxCompetencia" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                        <%--<asp:Label ID="lblAlertaCompetencia" runat="server" CssClass="rotulo" Text="(AAAA/MM)"></asp:Label>--%>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small"
                            runat="server" ErrorMessage="Formato Invalido (AAAA/MM)" ControlToValidate="tbxCompetencia"
                            Display="Dynamic" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator></p>
                    <%--<asp:ImageButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" ImageUrl="~/img/bts/bt_pesq.png"
                Height="43px" Width="44px" />--%>
                    <p>
                        <span class="rotulo">Tipo de processamento</span> <span>
                            <asp:RadioButtonList ID="rbtTipo" runat="server" RepeatDirection="Vertical" TextAlign="Right"
                                CellPadding="0" CellSpacing="0" CssClass="radio1">
                                <asp:ListItem Value="C">BPC</asp:ListItem>
                                <asp:ListItem Value="I">BPI</asp:ListItem>
                                <asp:ListItem Value="A">APAC</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxCompetencia" ErrorMessage="*" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator></span>
                        <%--<asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" 
                onclick="btnPesquisar_Click" />--%>
                    </p>
                    <p>
                        <span class="rotulo">Modo de processamento:</span> <span>
                            <asp:RadioButtonList ID="rbtModo" runat="server" RepeatDirection="Vertical" TextAlign="Right"
                                CellPadding="0" CellSpacing="0" CssClass="radio1">
                                <asp:ListItem Value="P">Prévia</asp:ListItem>
                                <asp:ListItem Value="F">Faturamento</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </p>
                    <br />
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click">
                <img id="imggerar" alt="Gerar" src="img/gerar_1.png"
                onmouseover="imggerar.src='img/gerar_2.png';"
                onmouseout="imggerar.src='img/gerar_1.png';" />
                        </asp:LinkButton>
                        <cc1:ConfirmButtonExtender ID="cbe" runat="server" TargetControlID="btnSalvar" ConfirmText="Confirma a Execução da rotina ?" />
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';" />
                        </asp:LinkButton></div>
                    <p>
                        <asp:Label ID="lblSemRegistros" runat="server" Text="Não Existem Registros para esta Competência"
                            Font-Bold="true" ForeColor="Red">
                        </asp:Label>
                    </p>
                </fieldset>
        <asp:UpdatePanel ID="UpdatePanel89" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_ConfirmarFechamentoBPA" runat="server" Visible="false">
                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 130%; z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.8">
                    </div>
                    <div id="mensagem" style="position: fixed; top: 100px; left: 25%; width: 600px; z-index: 102;
                        background-color: #541010; border-right: #ffffff  5px solid; padding-right: 10px;
                        border-top: #ffffff  5px solid; padding-left: 10px; padding-bottom: 10px; border-left: #ffffff  5px solid;
                        color: #000000; padding-top: 10px; border-bottom: #ffffff 5px solid; text-align: justify;
                        font-family: Verdana;">
                        <div style="padding-left: 50px;">
                            <br />
                            <p>
                                <span>
                                    <asp:Label ID="Label_InfoConfirmarFatura" runat="server" Text="" Font-Bold="true"
                                        ForeColor="White" Font-Size="Medium"></asp:Label>
                                </span>
                                <div class="botoesroll">
                                    <asp:LinkButton ID="LinkButton_ConfirmarFatura" runat="server" OnClick="OnClick_ConfirmarFatura">
                                        <img id="imgsim" alt="Sim" src="img/bts/sim1.png"
                onmouseover="imgsim.src='img/bts/sim2.png';"
                onmouseout="imgsim.src='img/bts/sim1.png';" />
                                    </asp:LinkButton>
                                </div>
                                <div class="botoesroll">
                                    <asp:LinkButton ID="LinkButton_NaoConfirmarFatura" runat="server" OnClick="OnClick_CancelarFatura">
                                    <img id="imgnao" alt="Não" src="img/bts/nao1.png"
                onmouseover="imgnao.src='img/bts/nao2.png';"
                onmouseout="imgnao.src='img/bts/nao1.png';" />
                                    </asp:LinkButton>
                                </div>
                            </p>
                        </div>
                    </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="LinkButton_ConfirmarFatura" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnBaixarArquivo" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel_Fatura" runat="server" Visible="false">
                            <fieldset class="formulario">
                                <legend>Informações sobre a fatura solicitada</legend>
                                <p>
                                    <span class="rotulo">Competência</span><span>
                                        <asp:Label ID="Label_Competencia" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Data de Fechamento</span><span>
                                        <asp:Label ID="Label_DataFechamento" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Tipo</span><span>
                                        <asp:Label ID="Label_Tipo" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Unidade</span><span>
                                        <asp:Label ID="Label_Unidade" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                    </span>
                                </p>
                                <br />
                                <div class="botoesroll">
                                    <asp:LinkButton ID="btnBaixarArquivo" runat="server" OnClick="btnBaixarArquivo_Click"
                                CausesValidation="false">
                            <img id="imgdownload" alt="Download Arquivos" src="img/btn-downloadarquivos1.png"
                onmouseover="imgdownload.src='img/btn-downloadarquivos2.png';"
                onmouseout="imgdownload.src='img/btn-downloadarquivos1.png';" />
                                    </asp:LinkButton>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
<%--                <asp:Panel ID="PanelGrid" runat="server">
                    <fieldset class="formulario">
                        <legend>Dados da Exportação</legend>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnBaixarArquivo" runat="server" OnClick="btnBaixarArquivo_Click"
                                CausesValidation="false">
                                            <img id="imgbaixar" alt="Salvar" src="img/btn-baixar-1.png"
                                            onmouseover="imgbaixar.src='img/btn-baixar-2.png';"
                                            onmouseout="imgbaixar.src='img/btn-baixar-1.png';" />
                            </asp:LinkButton></div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnImprimeRelatorio" runat="server" OnClick="btnImprimeRelatorio_Click"
                                CausesValidation="false">
                                            <img id="imgimprimirrelatorio" alt="Salvar" src="img/btn-imprimirrelatorioexportacao-1.png"
                                            onmouseover="imgimprimirrelatorio.src='img/btn-imprimirrelatorioexportacao-2.png';"
                                            onmouseout="imgimprimirrelatorio.src='img/btn-imprimirrelatorioexportacao-1.png';" />
                            </asp:LinkButton></div>
                    </fieldset>
                </asp:Panel>
--%>                <%--<asp:Panel ID="PanelGrid" runat="server">
                <fieldset class="formulario">
                <legend>Faturas Pesquisadas</legend>
                    <p>
                        <asp:GridView ID="gridFatura" runat="server" AutoGenerateColumns="False" Width="462px">
                            <Columns>
                                <asp:BoundField DataField="Competencia" HeaderText="Comp">
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                </asp:BoundField>
                                <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormGerarBPA.aspx?id_fatura={0}"
                                    DataTextField="Data_Situacao" HeaderText="Data" Text="Data">
                                    <ItemStyle HorizontalAlign="Center" Width="90px" />
                                </asp:HyperLinkField>
                                <asp:BoundField DataField="Situacao" HeaderText="Situação">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Tipo_Processamento" HeaderText="Tipo Processamento">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                            </Columns>
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow_left" />
                        </asp:GridView>
                    </p>
                </fieldset>
            </asp:Panel>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
