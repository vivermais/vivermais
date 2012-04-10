<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormProcedimentosNaoFaturaveis.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormProcedimentosNaoFaturaveis" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>Procedimento Não-Faturável</h2>
<%--        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView_Procedimentos" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>--%>
                <fieldset class="formulario">
                    <legend>Formulário de Cadastro</legend>
                    <p>
                        <span class="rotulo">Nome</span> <span style="margin-left: 5px;">
                            <asp:TextBox ID="TextBox_NomeProcedimento" Width="400px" CssClass="campo" runat="server"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Status</span> <span class="camporadio">
                            <asp:RadioButton ID="RadioButton_Ativo" CssClass="camporadio" Width="20px" runat="server"
                                GroupName="GroupName_Status" Checked="true" />Ativo
                            <asp:RadioButton ID="RadioButton_Inativo" CssClass="camporadio" Width="20px" GroupName="GroupName_Status"
                                runat="server" />Inativo </span>
                    </p>
                    <p align="center">
                        <span>
                            <asp:ImageButton ID="Button_Salvar" runat="server" ValidationGroup="ValidationGroup_Procedimento"
                                ImageUrl="~/Urgencia/img/bts/btn_salvar1.png" Width="134px" Height="38px" OnClick="OnClick_SalvarProcedimento" />
                            <asp:ImageButton ID="Button_Cancelar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png"
                                Width="134px" Height="38px" PostBackUrl="~/Urgencia/FormExibeProcedimentosNaoFaturaveis.aspx" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é Obrigatório!"
                                ControlToValidate="TextBox_NomeProcedimento" ValidationGroup="ValidationGroup_Procedimento"
                                Display="None">
                            </asp:RequiredFieldValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                runat="server" ValidationGroup="ValidationGroup_Procedimento" />
                        </span>
                    </p>
                </fieldset>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
