<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" EnableEventValidation = "false"
    CodeBehind="FormRelatorioAgendaMontadaPublicada.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioAgendaMontadaPublicada"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Relatório de Agendas Montadas X Publicadas</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Competência (AAAAMM):</span> <span>
                    <asp:TextBox ID="tbxCompetencia" runat="server" CssClass="campo"
                        MaxLength="7" Width="50px"></asp:TextBox>
                    <asp:LinkButton ID="btnCompetencia" CausesValidation="true" runat="server" OnClick="btnCompetencia_OnClick">
                        <img src="img/procurar.png" alt="Pesquisar" id="imgProcurar" runat="server" />
                    </asp:LinkButton>
                    <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                        ControlToValidate="tbxCompetencia" Display="Dynamic" ErrorMessage="O campo Competência deve conter apenas Números"
                        Operator="DataTypeCheck" Type="Double">
                    </asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                        ControlToValidate="tbxCompetencia" Display="Dynamic" ErrorMessage="Preencha o campo Competência"></asp:RequiredFieldValidator></span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnCompetencia" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Estabelecimento de Saúde:</span> <span>
                            <asp:DropDownList ID="ddlEstabelecimentoSaude" CssClass="drop" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEstabelecimentoSaude_OnSelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlEstabelecimentoSaude"
                                Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small"
                                ValidationGroup="Validacao">
                            </asp:RequiredFieldValidator>--%>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdateProcedimento" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEstabelecimentoSaude" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnCompetencia" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Procedimento:</span> <span>
                            <asp:DropDownList ID="ddlProcedimento" CssClass="drop" runat="server" Width="507px"
                                DataTextField="Nome" DataValueField="Codigo" AutoPostBack="True" OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProcedimento"
                                Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small"
                                ValidationGroup="Validacao">
                            </asp:RequiredFieldValidator>--%>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdateEspecialidade" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEstabelecimentoSaude" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnCompetencia" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlProcedimento" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Especialidade:</span> <span>
                            <asp:DropDownList ID="ddlCBO" CssClass="drop" runat="server" Width="507px" DataTextField="Nome"
                                DataValueField="Codigo" AutoPostBack="true" OnSelectedIndexChanged="ddlCBO_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCBO"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small" InitialValue="0"
                                    ValidationGroup="Validacao"></asp:RequiredFieldValidator>
                            </span>--%>
                            </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdateProfissional" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEstabelecimentoSaude" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnCompetencia" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlProcedimento" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCBO" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Profissional:</span> <span>
                            <asp:DropDownList ID="ddlProfissional" CssClass="drop" runat="server" Width="507px"
                                DataTextField="Nome" DataValueField="Codigo">
                            </asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProfissional"
                                Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small"
                                ValidationGroup="Validacao"></asp:RequiredFieldValidator>--%>
                            </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="botoesroll">
                    <asp:LinkButton ID="btnGeraRelatorio" Text="Imprimir" runat="server"
                        CausesValidation="true" OnClick="btnGeraRelatorio_Click">
                        <img id="imgGerar" alt="Imprimir" src="img/imprimir_1.png" 
                        onmouseover="imgGerar.src='img/imprimir_2.png';"
                        onmouseout="imgGerar.src='img/imprimir_1.png';" />
                    </asp:LinkButton>
                </div>
                <div class="botoesroll">
                    <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                </div>
        </fieldset>
    </div>
</asp:Content>
