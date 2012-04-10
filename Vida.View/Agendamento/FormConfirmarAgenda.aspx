<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="True"
    CodeBehind="FormConfirmarAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.FormConfirmarAgenda"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

   <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

    <link media="screen" type="text/css" rel="stylesheet" href="../colorbox.css" />

    <script type="text/javascript" defer="defer" src="../JavaScript/jquery.colorbox.js"></script>--%>





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
                                <asp:ImageButton ID="ImgPesquisar" runat="server" CausesValidation="True" OnClick="btnPesquisar_Click"
                                    ImageUrl="~/Agendamento/img/procurar.png" Width="16px" Height="16px" />
                        </span>
                    </p>
                    <p>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxIdentificador" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                            SetFocusOnError="True">
                        </asp:RequiredFieldValidator>
                    </p>
                    <asp:Panel ID="PanelExibeDados" runat="server">
                        <p>
                            <span class="rotulo">Nome do Paciente:</span> <span>
                                <asp:Label ID="lblPaciente" runat="server" Font-Bold="true" Text="-"></asp:Label>
                            </span>
                        </p>
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
                        <p>
                            <span class="rotulo">CID:</span> <span>
                                <asp:DropDownList ID="ddlCID" runat="server" CssClass="drop">
                                </asp:DropDownList>
                            </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Campo Obrigatório"
                                ControlToValidate="ddlCID" InitialValue="0" Font-Size="XX-Small"></asp:RequiredFieldValidator>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="PanelProntuario" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">Nº Prontuário</span> <span>
                                <asp:TextBox ID="tbxProntuario" runat="server" CssClass="campo" MaxLength="10"></asp:TextBox>
                            </span>
                            <asp:CompareValidator ID="CompareValidatorProntuario" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxProntuario" Display="Dynamic" ErrorMessage="O campo Prontuário deve conter apenas Números"
                                Operator="DataTypeCheck" Type="Double">
                            </asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorProntuario" runat="server"
                                ErrorMessage="Campo Obrigatório" ControlToValidate="tbxProntuario" Font-Size="XX-Small">
                            </asp:RequiredFieldValidator>
                        </p>
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
                        <asp:LinkButton ID="lknSalvar" runat="server" OnClick="Salvar_Click" Enabled="False">
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
                <%--<div style='display: none'>
                    <div id='inline_content' style="padding: 14px; background: #fff; font-family: Arial;
                        font-size: 12px !important; text-align: justify;" class="inline">
                        <p style="font-size: 13px; margin-bottom: 15px;">
                            “A Secretaria Municipal da Saúde lança chamamento público para credenciar prestadores
                            de serviços em diversas especialidades.<br /><br /> Os editais estão disponibilizados no site:
                           <b>www.saude.salvador.ba.gov.br.”</b> 
                        </p>
                    </div>
                </div>--%>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
     <%--   <script defer="defer" type="text/javascript">
                    
                jQuery.noConflict();
  
			    setTimeout(function(){
                jQuery.colorbox({ href:"#inline_content", inline:true,  width:"560px" });
              //  jQuery(".inline").colorbox({inline:true, href:"#inline_content"});
                },20);
         
    </script>--%>
</asp:Content>
