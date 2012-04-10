<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormGerarBPA.aspx.cs" Inherits="ViverMais.View.Urgencia.FormGerarBPA"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButtonSalvar" />
        </Triggers>
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Gerar BPA</h2>
        <fieldset class="formulario">
            <legend>Informações</legend>
            <p>
                <span class="rotulo">Mês</span> <span>
                    <asp:DropDownList ID="DropDownList_Mes" runat="server" CssClass="drop" Width="100px">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Ano</span> <span>
                    <asp:TextBox ID="TextBox_Ano" runat="server" CssClass="campo" Width="50px" MaxLength="4"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Tipo</span> <span>
                    <asp:DropDownList ID="DropDownList_Tipo" runat="server" CssClass="drop" Width="200px">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="RadioButtonList_Modo" EventName="SelectedIndexChanged" />
                </Triggers>--%>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Modo</span> <span>
                            <asp:RadioButtonList ID="RadioButtonList_Modo" runat="server" RepeatDirection="Horizontal"
                                CssClass="radio" RepeatColumns="2" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_ConfirmarFatura">
                            </asp:RadioButtonList>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LinkButtonSalvar" runat="server" Text="Gerar" OnClick="OnClick_GerarBPA"
                            ValidationGroup="ValidationGroup_GerarBPA">
                <img id="imggerarbpa" alt="Gerar BPA" src="img/bts/gerar-bpa1.png"
                onmouseover="imggerarbpa.src='img/bts/gerar-bpa2.png';"
                onmouseout="imggerarbpa.src='img/bts/gerar-bpa1.png';" />
                        </asp:LinkButton>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="botoesroll">
                <asp:LinkButton ID="LinkButtonCancelar" runat="server" PostBackUrl="~/Urgencia/Default.aspx">
                <img id="imgvoltar" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                onmouseover="imgvoltar.src='img/bts/btn_cancelar2.png';"
                onmouseout="imgvoltar.src='img/bts/btn_cancelar1.png';" />
                </asp:LinkButton></div>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Mês é Obrigatório."
                ControlToValidate="DropDownList_Mes" ValueToCompare="-1" Operator="GreaterThan"
                Display="None" ValidationGroup="ValidationGroup_GerarBPA"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ano é Obrigatório."
                Display="None" ControlToValidate="TextBox_Ano" ValidationGroup="ValidationGroup_GerarBPA"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Ano deve ser maior que 1899."
                ValueToCompare="1899" Operator="GreaterThan" Display="None" ControlToValidate="TextBox_Ano"
                ValidationGroup="ValidationGroup_GerarBPA"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Tipo é Obrigatório."
                ControlToValidate="DropDownList_Tipo" ValueToCompare="-1" Operator="NotEqual"
                Display="None" ValidationGroup="ValidationGroup_GerarBPA"></asp:CompareValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_GerarBPA" />
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel89" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButtonSalvar" EventName="Click" />
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton_ConfirmarFatura" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButtonSalvar" EventName="Click" />
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
                        <p>
                            <span class="rotulo">Usuário Responsável</span><span>
                                <asp:Label ID="Label_UsuarioResponsavel" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Medium"></asp:Label>
                            </span>
                        </p>
                        <br />
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_DownloadFatura">
                            <img id="imgdownload" alt="Download Arquivos" src="img/bts/download-1.png"
                onmouseover="imgdownload.src='img/bts/download-2.png';"
                onmouseout="imgdownload.src='img/bts/download-1.png';" />
                            </asp:LinkButton>
                        </div>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
