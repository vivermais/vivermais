﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormGerarAtestadoReceita.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormGerarAtestadoReceita" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master"
    EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div id="top">
                <h2>
                    Emitir
                    <asp:Label ID="Label_Conteudo" runat="server" Text=""></asp:Label></h2>
                <fieldset class="formulario">
                    <legend style="background: #962424; color: #FFFFFF;">Formulário</legend>
                    <p>
                        <span class="rotulo">Nome:</span> <span>
                            <asp:Label ID="LabelPacienteCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">CNS:</span> <span>
                            <asp:Label ID="LabelCNSCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">RG:</span> <span>
                            <asp:Label ID="LabelRGCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Unidade:</span> <span>
                            <asp:Label ID="LabelUnidadeCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">CNES:</span> <span>
                            <asp:Label ID="LabelCNESCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Número de Atendimento:</span> <span>
                            <asp:Label ID="LabelNumeroAtendimentoCabecalho" runat="server" Font-Bold="true" Text=""></asp:Label>
                        </span>
                    </p>
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Panel ID="PanelAtestadoMedico" runat="server" Visible="false">
                                <%--<fieldset class="formulario">
                                <legend>Conteúdo</legend>--%>
                                <p style="font-family: Verdana; font-size: 12px; text-align: justify; margin-top: 20px;
                                    margin-left: 15px; margin-right: 15px; margin-bottom: 20px;">
                                    Atesto para devidos fins, que o(a) Sr(a)
                                    <asp:Label ID="LabelPacienteAtestadoMedico" runat="server" Font-Size="13px" Font-Bold="true"
                                        ForeColor="#013042" Text=""></asp:Label>
                                    compareceu nesta unidade de saúde no dia
                                    <asp:Label ID="LabelHorarioAtestadoMedico" runat="server" Font-Size="13px" Font-Bold="true"
                                        ForeColor="#013042" Text=""></asp:Label>, apresentando
                                    <asp:Label ID="LabelCIDAtestadoMedico" runat="server" Font-Size="13px" Font-Bold="true"
                                        ForeColor="#013042" Text="CID"></asp:Label>:
                                    <asp:Literal ID="CIDSAtestadoMedico" runat="server"></asp:Literal>
                                    <%--                                    <div id="CIDSAtestadoMedico" runat="server">
                                    </div>--%>
                                    . Necessita ficar afastado de suas atiViverMaisdes por um período de
                                    <asp:TextBox ID="TextBoxPeriodoAfastamentoAtestadoMedico" Width="25px" CssClass="lote"
                                        runat="server"></asp:TextBox>
                                    dia(s) a partir desta data.
                                </p>
                                <br />
                                <br />
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelDataAtestadoMedico" runat="server" CssClass="assinatura" Text=""></asp:Label>
                                </p>
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelMedicoAtestado" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <p style="text-align: center; font-size: 11px; font-family: Verdana; font-weight: bold;">
                                    CRM-BA:
                                    <asp:Label ID="LabelCRMAtestado" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <div style="margin-left: 200px">
                                    <span></br>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Gerar" runat="server" OnClick="OnClick_Gerar" CommandArgument="atestadomedico"
                                            OnClientClick="javascript:if (Page_ClientValidate('ValidationGroupGerarAtestadoMedico')) return confirm('Todos os dados do atestado estão corretos?');">
                    <img id="emitir1" alt="Emitir" src="img/btn-emitir-1.png"
                        onmouseover="emitir1.src='img/btn-emitir-2.png';"
                        onmouseout="emitir1.src='img/btn-emitir-1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Cancelar" runat="server" OnClientClick="javascript:parent.parent.GB_hide();">
                        <img id="cancelar1" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                        onmouseover="cancelar1.src='img/bts/btn_cancelar2.png';"
                        onmouseout="cancelar1.src='img/bts/btn_cancelar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o período de afastamento do paciente."
                                            Display="None" ControlToValidate="TextBoxPeriodoAfastamentoAtestadoMedico" ValidationGroup="ValidationGroupGerarAtestadoMedico"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator" runat="server" ControlToValidate="TextBoxPeriodoAfastamentoAtestadoMedico"
                                            ValidationExpression="^\d*$" ValidationGroup="ValidationGroupGerarAtestadoMedico"
                                            Display="None" ErrorMessage="Digite somente números no período."></asp:RegularExpressionValidator>
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroupGerarAtestadoMedico" />
                                    </span>
                                </div>
                                <%--</fieldset>--%>
                            </asp:Panel>
 <%--                       </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>--%>
                            <asp:Panel ID="PanelAtestadoComparecimento" runat="server" Visible="false">
                                <%--<fieldset class="formulario">
                                <legend>Conteúdo</legend>--%>
                                <p style="font-family: Verdana; font-size: 11px;">
                                    Declaro que o(a) Sr(a)
                                    <asp:Label ID="LabelPacienteAtestadoComparecimento" runat="server" Text="" Font-Size="13px"
                                        Font-Bold="true" ForeColor="#013042"></asp:Label>
                                    compareceu nesta unidade de saúde no dia
                                    <asp:Label ID="LabelPeriodoAtestadoComparecimento" runat="server" Text=""></asp:Label>
                                    para
                                    <FCKeditorV2:FCKeditor ID="EditorInformacaoAtestadoComparecimento" runat="server" BasePath="~/Urgencia/FCKEditor/"
                                    LinkBrowserURL="~/Urgencia/FCKEditor/" Height="300px" ImageBrowserURL="~/Urgencia/FCKEditor/">
                                </FCKeditorV2:FCKeditor>
                                    <%--<asp:TextBox ID="TextBoxInformacaoAtestadoComparecimento" CssClass="campo" runat="server"
                                        Width="690px" Height="200px" Rows="5" TextMode="MultiLine"></asp:TextBox>--%>
                                </p>
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelDataAtestadoComparecimento" runat="server" CssClass="assinatura"
                                        Text=""></asp:Label>
                                </p>
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelMedicoComparecimento" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <p style="text-align: center; font-size: 11px; font-family: Verdana; font-weight: bold;">
                                    CRM-BA:
                                    <asp:Label ID="LabelCRMComparecimento" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <div style="margin-left: 200px">
                                    <span></br>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Gerar2" runat="server" OnClick="OnClick_Gerar" CommandArgument="atestadocomparecimento"
                                             OnClientClick="javascript:return confirm('Todos os dados do atestado de comparecimento estão corretos?');">
                    <img id="imgemitir2" alt="Emitir" src="img/btn-emitir-1.png"
                        onmouseover="imgemitir2.src='img/btn-emitir-2.png';"
                        onmouseout="imgemitir2.src='img/btn-emitir-1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Cancelar2" runat="server" OnClientClick="javascript:parent.parent.GB_hide();">
                        <img id="imgcancelar2" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                        onmouseover="imgcancelar2.src='img/bts/btn_cancelar2.png';"
                        onmouseout="imgcancelar2.src='img/bts/btn_cancelar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o conteúdo para o atestado de comparecimento."
                                            Display="None" ControlToValidate="TextBoxInformacaoAtestadoComparecimento" ValidationGroup="ValidationGroupGerarAtestadoComparecimento"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroupGerarAtestadoComparecimento" />--%>
                                    </span>
                                </div>
                                <%-- <p align="center">
                                    <span>
                                        <asp:Button ID="Button1" runat="server" Text="Emitir" OnClick="OnClick_Gerar" CommandArgument="atestadocomparecimento"
                                            ValidationGroup="ValidationGroupGerarAtestadoComparecimento" />
                                        <asp:Button ID="Button2" runat="server" Text="Cancelar" OnClientClick="javascript:parent.parent.GB_hide();" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o conteúdo para o atestado de comparecimento."
                                            Display="None" ControlToValidate="TextBoxInformacaoAtestadoComparecimento" ValidationGroup="ValidationGroupGerarAtestadoComparecimento"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroupGerarAtestadoComparecimento" />
                                    </span>
                                </p>--%>
                                <%-- </fieldset>--%>
                            </asp:Panel>
<%--                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>--%>
                            <asp:Panel ID="PanelReceitaMedica" runat="server" Visible="false">
                                <%--<fieldset class="formulario">
                                <legend>Conteúdo</legend>--%>
                                <FCKeditorV2:FCKeditor ID="EditorMedicamentosReceita" runat="server" BasePath="~/Urgencia/FCKEditor/"
                                    LinkBrowserURL="~/Urgencia/FCKEditor/" Height="300px" ImageBrowserURL="~/Urgencia/FCKEditor/">
                                </FCKeditorV2:FCKeditor>
                                <%--<p>
                                    <asp:TextBox ID="TextBoxMedicamentosReceita" CssClass="campo" runat="server" Width="690px"
                                        Height="200px" Rows="5" TextMode="MultiLine" Columns="10"></asp:TextBox>
                                </p>--%>
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelDataReceitaMedica" runat="server" CssClass="assinatura" Text=""></asp:Label>
                                </p>
                                <p style="text-align: center;">
                                    <asp:Label ID="LabelMedicoReceita" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <p style="text-align: center; font-size: 11px; font-family: Verdana; font-weight: bold;">
                                    CRM-BA:
                                    <asp:Label ID="LabelCRMReceita" runat="server" CssClass="assinatura" Font-Bold="true"
                                        Text=""></asp:Label>
                                </p>
                                <div style="margin-left: 200px">
                                    <span></br>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Gerar3" runat="server" OnClick="OnClick_Gerar" CommandArgument="receitamedica"
                                            OnClientClick="javascript:return confirm('Todos os dados da receita estão corretos?');" >
                    <img id="imgemitir3" alt="Emitir" src="img/btn-emitir-1.png"
                        onmouseover="imgemitir3.src='img/btn-emitir-2.png';"
                        onmouseout="imgemitir3.src='img/btn-emitir-1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="Button_Cancelar3" runat="server" OnClientClick="javascript:parent.parent.GB_hide();">
                        <img id="imgcancelar3" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                        onmouseover="imgcancelar3.src='img/bts/btn_cancelar2.png';"
                        onmouseout="imgcancelar3.src='img/bts/btn_cancelar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o conteúdo da receita."
                                            Display="None" ControlToValidate="TextBoxMedicamentosReceita" ValidationGroup="ValidationGroupGerarReceitaMedica"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroupGerarReceitaMedica" />--%>
                                    </span>
                                </div>
                                <%--<p align="center">
                                    <span>
                                        <asp:Button ID="Button3" runat="server" Text="Emitir" OnClick="OnClick_Gerar" CommandArgument="receitamedica"
                                            ValidationGroup="   " />
                                        <asp:Button ID="Button4" runat="server" Text="Cancelar" OnClientClick="javascript:parent.parent.GB_hide();" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o conteúdo da receita."
                                            Display="None" ControlToValidate="TextBoxMedicamentosReceita" ValidationGroup="ValidationGroupGerarReceitaMedica"></asp:RequiredFieldValidator>
                                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                            ShowSummary="false" ValidationGroup="ValidationGroupGerarReceitaMedica" />
                                    </span>
                                </p>--%>
                                <%--</fieldset>--%>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </div>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
