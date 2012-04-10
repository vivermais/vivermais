﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormEstadosProntuarios.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormEstadosProntuarios" EnableEventValidation="false"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
<%--    <script language="javascript">
        function validateNumber(o, args)
        {
            args.isValid = (args.Value != ' ');
        }
    </script>--%>
</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Block">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Pacientes
                    <asp:Label ID="Label_Chamada" runat="server" Text=""></asp:Label></h2>
                <fieldset class="formulario">
                    <legend>Relação</legend>
                    <p>
                        <asp:GridView ID="GridView_Prontuarios" runat="server"
                            AutoGenerateColumns="false" Width="100%">
                            <Columns>
                                <asp:BoundField HeaderText="Nº de Atendimento" DataField="NumeroToString" />
                                <asp:BoundField HeaderText="Paciente" DataField="NomePacienteToString">
                                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Descrição" DataField="PacienteDescricao" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="300px">
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ImageField HeaderText="Classificação de Risco" DataImageUrlFormatString="~/Urgencia/img/{0}"
                                    HeaderStyle-HorizontalAlign="Center" DataImageUrlField="ImagemClassificacaoRisco"
                                    ItemStyle-HorizontalAlign="Center">
                                </asp:ImageField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                            OnClick="OnClick_EvolucaoEnfermagem">
                <img id="imgevenf" alt="Evolução de Enfermagem" src="img/evenf_1.png"
                onmouseover="this.src='img/evenf_2.png';"
                onmouseout="this.src='img/evenf_1.png';" /></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server"
                                             OnClick="OnClick_EvolucaoMedica" CommandArgument='<%#bind("Codigo") %>'>
                <img id="imgevmedica" alt="Evolução Médica" src="img/evmedic_1.png"
                onmouseover="this.src='img/evmedic_2.png';"
                onmouseout="this.src='img/evmedic_1.png';" /></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow" />
                        </asp:GridView>
                    </p>
                </fieldset>
                <asp:Panel ID="PanelIdentificarProfissional" runat="server" Visible="false" 
                    DefaultButton="Button_ValidarProfissional">
                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; top: 100px; left: 25%;
                        width: 300px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 50px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 0px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        <p style="padding: 0px 10px 30px 0">
                        </p>
                        <p style="color: White">
                            Identificação de Profissional
                        </p>
                        <br />
                        <p>
                            <span class="rotulo2">Código</span> <span>
                                <asp:TextBox ID="TextBox_CodigoIdentificacao" runat="server" CssClass="campo" TextMode="Password"></asp:TextBox>
                            </span>
                        </p>
                        <div class="botoesroll">
                            <asp:ImageButton ID="Button_ValidarProfissional" runat="server" ValidationGroup="ValidationGroup_IdentificarProfissional"
                                OnClick="OnClick_ValidarIdentificacao" ImageUrl="~/Urgencia/img/confirmar-btn.png"
                                Width="125px" Height="39px" CausesValidation="true"></asp:ImageButton>
                        </div>
                        <div class="botoesroll">
                            <asp:ImageButton ID="Button_CancelarValidacaoProfissional" runat="server" OnClick="OnClick_FecharJanelaIdentificacaoProfissional"
                                ImageUrl="~/Urgencia/img/fechar-btn.png" Width="100px" Height="39px"></asp:ImageButton>
                        </div>
                        <p>
<%--                            <asp:CustomValidator id="CustomValidator1" 
             runat="server" ControlToValidate="TextBox_CodigoIdentificacao" 
             ErrorMessage="Number must be divisible by 5" Display="None"
             ClientValidationFunction="validateNumber" ValidationGroup="ValidationGroup_IdentificarProfissional">
            </asp:CustomValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Código é Obrigatório!"
                                ValidationGroup="ValidationGroup_IdentificarProfissional" Display="None" EnableClientScript="true"
                                ControlToValidate="TextBox_CodigoIdentificacao" SetFocusOnError="true"></asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ValidationGroup_IdentificarProfissional" />
                        </p>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>