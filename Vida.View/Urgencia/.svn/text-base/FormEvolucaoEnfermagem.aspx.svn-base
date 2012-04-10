<%@ Page Title="" Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    AutoEventWireup="true" CodeBehind="FormEvolucaoEnfermagem.aspx.cs" Inherits="Urgence.View.FormEvolucaoEnfermagem" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Urgencia/Inc_MenuHistorico.ascx" TagName="Inc_MenuHistorico"
    TagPrefix="IMH" %>
<%@ Register Src="~/Urgencia/Inc_PrescricoesRegistradas.ascx" TagName="Inc_Prescricoes"
    TagPrefix="IPR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formulario2
        {
            width: 690px;
            height: auto;
            margin-left: 5px;
            margin-right: 0px;
            padding: 10px 10px 20px 10px;
        }
        .formulario3
        {
            width: 680px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 20px 20px 20px 20px;
            margin: 10px;
        }
        .formulario4
        {
            width: 600px;
            height: auto;
            margin-left: 0px;
            margin-right: 0px;
            padding: 5px 5px 5px 5px;
            margin: 10px;
        }
        /* Estilo do Accordeon*/.accordionHeaderEv
        {
            border: 1px solid #142126;
            color: #142126;
            background-color: #eee; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 650px;
        }
        .accordionHeaderSelectedEv
        {
            border: 1px solid #142126;
            color: white;
            background-color: #142126; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 650px;
        }
        .accordionContentEv
        {
            background-color: #fff;
            border: 1px solid #142126;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 650px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Evolução de Enfermagem"></asp:Label>
        </h2>
        <br />
        <br />
        <cc1:TabContainer runat="server" ScrollBars="Horizontal" Width="740px"
            ActiveTabIndex="1">
            <cc1:TabPanel runat="server" HeaderText="Dados de Atendimento/Histórico">
                <ContentTemplate>
                    <fieldset class="formulario2">
                        <legend>Dados de Atendimento</legend>
                        <p>
                            <span class="rotulo">N°:</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data:</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblData" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label></span></p>
                        <p>
                            <span class="rotulo">Paciente:</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" Text="-" ForeColor="Maroon"></asp:Label>
                            </span>
                        </p>
                    </fieldset>
                    <fieldset class="formulario2">
                        <legend>Histórico</legend>
                        <p>
                            <span>
                                <IMH:Inc_MenuHistorico ID="Inc_MenuHistorico" runat="server"></IMH:Inc_MenuHistorico>
                            </span>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Evolução de Enfermagem">
                <ContentTemplate>
                    <fieldset class="formulario2">
                        <legend>Evolução de Enfermagem</legend>
                        <p>
                            <span>
                                <asp:LinkButton ID="LinkButton_HistoricoEnfermagem" CausesValidation="true" runat="server" OnClick="OnClick_HistoricoEnfermagem"><img alt="Histórico" src="img/bts/urg-historico1.png"
                                                    onmouseover="this.src='img/bts/urg-historico2.png';"
                                                    onmouseout="this.src='img/bts/urg-historico1.png';" /></asp:LinkButton>
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSalvarEvolucaoEnfermagem" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Registro:</span> <span style="margin-left: 5px;">
                                        <asp:TextBox ID="TextBox_ObservacaoEvolucaoEnfermagem" CssClass="campo" runat="server"
                                            TextMode="MultiLine" Width="670px" Height="110px" Rows="20" Columns="5" ></asp:TextBox>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p align="center">
                            <span>
                                <asp:ImageButton ID="btnSalvarEvolucaoEnfermagem" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png"
                                    Width="134px" Height="38px" runat="server" CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadEvolucaoEnfermagem')) return confirm('Todos os dados informados estão corretos ?'); return false;"
                                    OnClick="OnClick_SalvarEvolucaoEnfermagem" ValidationGroup="ValidationGroup_cadEvolucaoEnfermagem" />
                            </span><span>
                                <asp:ImageButton ID="ImageButton2" runat="server" Width="134px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                    CausesValidation="false" PostBackUrl="~/Urgencia/Default.aspx" />
                            </span>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                ControlToValidate="TextBox_ObservacaoEvolucaoEnfermagem" ErrorMessage="Registro de Evolução é Obrigatório!"
                                ValidationGroup="ValidationGroup_cadEvolucaoEnfermagem" Display="None"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummaryEvolucao" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ValidationGroup_cadEvolucaoEnfermagem" />
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Prescrições" ScrollBars="Horizontal">
                <ContentTemplate>
                    <IPR:Inc_Prescricoes ID="Inc_PrescricoesRegistradas" runat="server"></IPR:Inc_Prescricoes>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <asp:UpdatePanel ID="UpdatePanel_HistoricoEvolucao" runat="server" ChildrenAsTriggers="true"
            UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Panel ID="Panel_Evolucoes" runat="server" Visible="false">
                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; top: 100px; left: 50%; margin-left:-350px;
                        width: 700px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 50px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 0px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        <p style="padding: 0px 10px 30px 0">
                        </p>
                        <p style="color: White; font-size: medium; font-family: Arial; font-weight: bold;">
                            Histórico de Evoluções
                        </p>
                        <div id="conteudo" style="width: 650px; height: 300px; overflow: auto;">
                            <asp:DataList ID="DataList_HistoricoEnfermagem" runat="server">
                                <ItemTemplate>
                                                                        <p>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold"> PROFISSIONAL:</div>
                                    <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:480px;  margin-right:15px"><asp:Label ID="Label_Profissional" runat="server" Text='<%#bind("Profissional") %>'></asp:Label></div>
                                    </p>

                                    <p>
                                     <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">CBO:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:20px; float:left; margin-bottom:10px;"> <asp:Label ID="Label_CBO" runat="server" Text='<%#bind("CBO") %>'></asp:Label></div>
                                    <div style="background-color:#2a0606; padding:5px; color:#fff; font-size:11px; width:80px; float:left; margin-right:1px; font-weight:bold">DATA:</div>
                                   <div style="background-color:#3d0909; padding:5px; color:#fff; font-size:11px; width:180px; margin-right:1px;  float:left; margin-bottom:10px;"> <asp:Label ID="Label_Data" runat="server" Text='<%#bind("Data") %>'></asp:Label></div>
                                   </p>
                                   
                                    <asp:TextBox ID="TextBox_Conteudo" runat="server" Text='<%#bind("Conteudo") %>' TextMode="MultiLine" CssClass="campo"
                                     ReadOnly="true" Rows="20" Columns="20" Width="620px" Height="200px"></asp:TextBox>
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <div style="margin-top:15px; margin-bottom:10px;"><img src="img/div-separador-hitorico.png" alt"" /></div>
                                </SeparatorTemplate>
                                <FooterStyle HorizontalAlign="Center" />
                                <FooterTemplate>
                                    <asp:Label ID="Label15" runat="server" Text="Nenhum registro encontrado." Font-Bold="true" ForeColor="White" Font-Size="13px"
                                        Visible='<%#bool.Parse((DataList_HistoricoEnfermagem.Items.Count == 0).ToString()) %>'></asp:Label>
                                </FooterTemplate>
                            </asp:DataList>
                        </div>
                        <div class="botoesroll" style="margin: 15px 0px 5px 0px">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_FecharHistoricoEvolucoes">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Urgencia/img/fechar-btn.png" Width="100px"
                                    Height="39px" />
                            </asp:LinkButton>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
