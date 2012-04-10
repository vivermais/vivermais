<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormRelatorioAgendaPrestador.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioAgendaPrestador"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top">
                        <h2>
                    Relatórios</h2>
            <fieldset class="formulario">
                <legend>Relatório Agenda Prestador</legend>
                <p>
                    <span class="rotulo">Data Inicial:</span> <span>
                        <asp:TextBox ID="tbxData_Inicial" CssClass="campo" runat="server" MaxLength="10"
                            Width="70px" ValidationGroup="VisualizarAgenda" CausesValidation="True">
                        </asp:TextBox>
                        <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxData_Inicial"
                            Animated="true">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                            TargetControlID="tbxData_Inicial" Mask="99/99/9999" MaskType="Date">
                        </cc1:MaskedEditExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxData_Inicial" ErrorMessage="Campo Obrigatório" ValidationGroup="Validation_VisualizaRelatorio">
                        </asp:RequiredFieldValidator>
                    </span>
                </p>
                <p>
                        <span class="rotulo">Data Final:</span> <span>
                        <asp:TextBox ID="tbxData_Final" CssClass="campo" runat="server" MaxLength="10" Width="70px"
                            ValidationGroup="Validation_VisualizaRelatorio" CausesValidation="True">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxData_Final" ErrorMessage="Campo Obrigatório" ValidationGroup="Validation_VisualizaRelatorio">
                        </asp:RequiredFieldValidator>
                        <cc1:CalendarExtender runat="server" ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="tbxData_Final"
                            Animated="true">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                            TargetControlID="tbxData_Final" Mask="99/99/9999" MaskType="Date">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Unidade Executante:</span> <span>
                        <asp:DropDownList ID="ddlUnidadeExecutante" runat="server" CssClass="drop" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlUnidadeExecutante_OnSelectedIndexChanged">
                        </asp:DropDownList>
                    </span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlUnidadeExecutante"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small"
                        ValidationGroup="Validation_VisualizaRelatorio">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <span class="rotulo">Procedimento:</span> <span>
                        <asp:DropDownList ID="ddlProcedimento" CssClass="drop" runat="server" Width="507px"
                            DataTextField="Nome" DataValueField="Codigo" AutoPostBack="True" OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged">
                        </asp:DropDownList>
                    </span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProcedimento"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small"
                        ValidationGroup="Validation_VisualizaRelatorio">
                    </asp:RequiredFieldValidator>
                </p>
                <p>
                    <span class="rotulo">Profissional:</span> <span>
                        <asp:DropDownList ID="ddlProfissional" CssClass="drop" runat="server" Width="507px"
                            DataTextField="Nome" DataValueField="Codigo">
                        </asp:DropDownList>
                    </span>
                </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="btnGeraRelatorio" Text="Imprimir" runat="server" ValidationGroup="Validation_VisualizaRelatorio"
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
