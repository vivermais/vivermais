<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExcluirIdentificacaoPaciente.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormExcluirIdentificacaoPaciente" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Paciente/WUCPesquisarPaciente.ascx" TagName="TagName_PesquisarPaciente"
    TagPrefix="TagPrefix_PesquisarPaciente" %>
<%@ Register Src="~/Paciente/WUCExibirPaciente.ascx" TagName="TagName_ExibirPaciente"
    TagPrefix="TagPrefix_ExibirPaciente" %>
<%@ Register Src="~/Urgencia/WUCExibirAtendimento.ascx" TagName="TagName_ExibirAtendimento"
    TagPrefix="TagPrefix_ExibirAtendimento" %>
<%@ Register Src="~/Urgencia/WUCPesquisarAtendimento.ascx" TagName="TagName_PesquisarAtendimento"
    TagPrefix="TagPrefix_PesquisarAtendimento" %>
<asp:Content ContentPlaceHolderID="head" ID="c_head" runat="server">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2 style="margin-bottom:40px;">
            Excluir Identificação de Paciente</h2>
            
        <cc1:TabContainer ID="TabContainer_ExcluirIdentificacao" runat="server">
            <cc1:TabPanel ID="TabAtendimento" runat="server" HeaderText="Atendimentos">
                <ContentTemplate>
                    <TagPrefix_PesquisarAtendimento:TagName_PesquisarAtendimento ID="WUC_PesquisarAtendimento"
                        runat="server"></TagPrefix_PesquisarAtendimento:TagName_PesquisarAtendimento>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel HeaderText="Pesquisar por Paciente" ID="TabPaciente" runat="server">
                <ContentTemplate>
                    <TagPrefix_PesquisarPaciente:TagName_PesquisarPaciente ID="WUC_PesquisarPaciente"
                        runat="server" />
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <br />
        <asp:UpdatePanel ID="UpdatePanelAtendimentos" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="true" RenderMode="Block">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton_ExcluirIdentificacao" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton_Cancelar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <TagPrefix_ExibirAtendimento:TagName_ExibirAtendimento ID="WUC_ExibirAtendimento"
                    runat="server" Visible="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <TagPrefix_ExibirPaciente:TagName_ExibirPaciente ID="WUC_ExibirPacienteAtendimento"
            runat="server" />
        <asp:UpdatePanel ID="UpdatePanel_BotoesAcao" UpdateMode="Conditional" runat="server"
            ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Panel ID="Panel_Acoes" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Ações</legend>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_ExcluirIdentificacao" runat="server" OnClick="OnClickExcluirIdentificacao"
                                OnClientClick="javascript:return confirm('Tem certeza que deseja excluir a identificação deste paciente?');">
                                <img id="imgexcluir" alt="Excluir Identificação" src="img/bts/excluir-identificacao1.png"
                  onmouseover="imgexcluir.src='img/bts/excluir-identificacao2.png';"
                  onmouseout="imgexcluir.src='img/bts/excluir-identificacao1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_Cancelar" runat="server" OnClick="OnClick_CancelarExclusao">
                        <img id="imgcancelarexclusao" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                  onmouseover="imgcancelarexclusao.src='img/bts/btn_cancelar2.png';"
                  onmouseout="imgcancelarexclusao.src='img/bts/btn_cancelar1.png';" /></asp:LinkButton>
                        </div>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
