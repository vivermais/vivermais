﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" CodeBehind="FormTempRegistraExecucao.aspx.cs" Inherits="ViverMais.View.Agendamento.FormTempRegistraExecucao" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="lknSalvar" />
            <asp:PostBackTrigger ControlID="imgPesquisar" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Confirmação de Agenda</h2>
                <fieldset class="formulario">
                    <legend>Registrar Execução </legend>
                    <p>
                        <span class="rotulo">Identificador:</span> <span>
                            <asp:TextBox ID="tbxIdentificador" CssClass="campo" runat="server" MaxLength="13"
                                Width="90px">
                            </asp:TextBox></span> <span style="position: absolute;">
                                <asp:ImageButton ID="ImgPesquisar" runat="server" CausesValidation="True"
                                    OnClick="btnPesquisar_Click" ImageUrl="~/Agendamento/img/procurar.png" Width="16px"
                                    Height="16px" /></span>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxIdentificador" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                            SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:Panel ID="PanelSelecionaPaciente" runat="server" Visible="true">
                        <p>
                            <span class="rotulo">Paciente:</span> <span>
                                <asp:DropDownList ID="ddlPaciente" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaciente_OnSelectedIndexChanged"></asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>                    
                    <asp:Panel ID="PanelExibeDados" runat="server">
                       <%-- <p>
                            <span class="rotulo">Nome do Paciente:</span> <span>
                                <asp:Label ID="lblPaciente" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>--%>
                        <p>
                            <span class="rotulo">Procedimento:</span> <span>
                                <asp:Label ID="lblProcedimento" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Profissional:</span> <span>
                                <asp:Label ID="lblProfissional" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data do Atendimento:</span> <span>
                                <asp:Label ID="lblDataAtendimento" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Turno:</span> <span>
                                <asp:Label ID="lblTurno" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="PanelBuscaCID" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Código CID:</span> <span>
                                <asp:TextBox ID="tbxCID" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                            <asp:ImageButton ID="Button_BuscaCID" runat="server" ImageUrl="~/Agendamento/img/procurar.png"
                                Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" CausesValidation="true"
                                ValidationGroup="ValidationGroup_BuscaCID" OnClick="OnClick_BuscarCID" />
                        </p>
                        <p>
                            <span class="rotulo">Grupo CID:</span> <span>
                                <asp:DropDownList ID="ddlGrupoCID" CssClass="drop" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="OnSelectedIndexChanged_BuscarCids">
                                </asp:DropDownList>
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="PanelExibeCID" runat="server">
                        <span class="rotulo">CID:</span> <span>
                            <asp:DropDownList ID="ddlCID" runat="server" CssClass="drop">
                            </asp:DropDownList>
                        </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obrigatório"
                            ControlToValidate="ddlCID" InitialValue="0" Font-Size="XX-Small"></asp:RequiredFieldValidator>
                    </asp:Panel>
                    <asp:Panel ID="PanelExibeMedicoRegulador" runat="server">
                        <p>
                            <span class="rotulo">Médico Autorizador:</span> <span>
                                <asp:Label ID="lblMedicoAutorizador" runat="server" Font-Bold="true"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">CNS:</span> <span>
                                <asp:Label ID="lblCNSAutorizador" runat="server" Font-Bold="true"></asp:Label></span>
                        </p>
                        <p>
                            <span class="rotulo">Validade</span> <span>
                                <asp:Label ID="lblValidadeInicial" runat="server" Font-Bold="true"></asp:Label></span>
                            até
                            <asp:Label ID="lblValidadeFinal" runat="server" Font-Bold="true"></asp:Label></span>
                        </p>
                    </asp:Panel>
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknSalvar" runat="server" CausesValidation="true" OnClick="Salvar_Click" Enabled="False">
                  <img id="imgconfirmar" alt="Confirmar" src="img/btn-confirmar1.png"
                  onmouseover="imgconfirmar.src='img/btn-confirmar2.png';"
                  onmouseout="imgconfirmar.src='img/btn-confirmar1.png';" /></asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                            CausesValidation="False">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
