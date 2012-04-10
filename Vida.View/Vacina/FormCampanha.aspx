<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCampanha.aspx.cs" Inherits="ViverMais.View.Vacina.FormCampanha"
    MasterPageFile="~/Vacina/MasterVacina.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:PostBackTrigger ControlID="LinkButtonSalvar" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Campanha</h2>
                <fieldset class="formulario">
                    <legend>Dados da Campanha</legend>
                    <p>
                        <span class="rotulo">Nome da Campanha</span> <span>
                            <asp:TextBox ID="tbxNome" runat="server" CssClass="campo" Width="300px" MaxLength="150" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data de Início</span> <span>
                            <asp:TextBox ID="TextBox_DataInicio" runat="server" CssClass="campo" Width="100px" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data de Término</span> <span>
                            <asp:TextBox ID="TextBox_DataFim" runat="server" CssClass="campo" Width="100px" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Faixa Etária</span> <span>
                            <asp:TextBox ID="tbxFaixaInicial" runat="server" CssClass="campo" Width="50px" />
                            <asp:DropDownList ID="DropDownList_UnidadeFaixaInicial" CssClass="drop" runat="server"
                                Width="70px">
                                <asp:ListItem Text="Meses" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Anos" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;a&nbsp;
                            <asp:TextBox ID="tbxFaixaFinal" runat="server" CssClass="campo" Width="50px" />
                            <asp:DropDownList ID="DropDownList_UnidadeFaixaFinal" CssClass="drop" runat="server"
                                Width="70px">
                                <asp:ListItem Text="Meses" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Anos" Value="A"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Sexo </span><span>
                            <asp:DropDownList ID="ddlSexo" runat="server" CssClass="drop" Width="100px">
                                <asp:ListItem Selected="True" Value="0">Selecione...</asp:ListItem>
                                <asp:ListItem Text="Masculino" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Feminino" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Ambos" Value="3"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Meta</span> <span>
                            <asp:TextBox ID="TextBox_Meta" runat="server" CssClass="campo" Width="50px" MaxLength="9" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Status</span> <span>
                            <asp:Label ID="Label_StatusCampanha" runat="server" Text=""></asp:Label>
                        </span>
                    </p>
                    <asp:Panel ID="PanelSalvar" runat="server">
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButtonSalvar" runat="server" OnClick="btnSalvar_Click" ValidationGroup="ValidationGroup_cadCampanha">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelFinalizar" runat="server" Visible="false">
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_FinalizarCampanha" runat="server" OnClick="OnClick_FinalizarCampanha"
                                OnClientClick="javascript:return confirm('Tem certeza que deseja finalizar esta campanha ?');">
			            <img id="imgfinalizar" alt="Cancelar" src="img/btn_finalizar1.png"
                  	onmouseover="imgfinalizar.src='img/btn_finalizar2.png';"
                  	onmouseout="imgfinalizar.src='img/btn_finalizar1.png';" />	
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LinkButtonVoltar" runat="server" PostBackUrl="~/Vacina/FormExibeCampanha.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
                    </div>
                    <p>
                        <span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Nome é Obrigatório."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="tbxNome">
                            </asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data de Início é Obrigatório."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataInicio">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Início com formato inválido."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataInicio"
                                Operator="DataTypeCheck" Type="Date">
                            </asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Início deve ser igual ou maior que 01/01/1900."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataInicio"
                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900">
                            </asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Término é Obrigatório."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataFim">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Término com formato inválido."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataFim"
                                Operator="DataTypeCheck" Type="Date">
                            </asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Data de Término deve ser igual ou maior que 01/01/1900."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataFim"
                                Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900">
                            </asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Data de Término deve ser igual ou maior que Data de Início."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_DataFim"
                                Operator="GreaterThanEqual" Type="Date" ControlToCompare="TextBox_DataInicio">
                            </asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="É preciso informar o Início da Faixa Etária."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="tbxFaixaInicial">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                ToolTip="Formato" ControlToValidate="tbxFaixaInicial" ValidationGroup="ValidationGroup_cadCampanha"
                                Display="None" ErrorMessage="Início da Faixa Etária deve conter somente números.">
                            </asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="É preciso informar o Fim da Faixa Etária."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="tbxFaixaFinal">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationExpression="^(\d{1,}\,\d{1,})|(\d*)$"
                                ToolTip="Formato" ControlToValidate="tbxFaixaFinal" ValidationGroup="ValidationGroup_cadCampanha"
                                Display="None" ErrorMessage="Fim da Faixa Etária deve conter somente números.">
                            </asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Sexo é Obrigatório."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="ddlSexo"
                                ValueToCompare="0" Operator="GreaterThan">
                            </asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Meta é Obrigatório."
                                ValidationGroup="ValidationGroup_cadCampanha" Display="None" ControlToValidate="TextBox_Meta">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationExpression="^\d*$"
                                ToolTip="Formato" ControlToValidate="TextBox_Meta" ValidationGroup="ValidationGroup_cadCampanha"
                                Display="None" ErrorMessage="Meta deve conter somente números inteiros positivos.">
                            </asp:RegularExpressionValidator>
                            <asp:CustomValidator ID="CustomValidator_FaixaEtaria" runat="server" ErrorMessage="ErrorMessage"
                                Display="None" OnServerValidate="OnServerValidate_ValidarFaixaEtaria" ValidationGroup="ValidationGroup_cadCampanha">
                            </asp:CustomValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="ValidationGroup_cadCampanha" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataInicio">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataInicio"
                                InputDirection="LeftToRight" MaskType="Date" Mask="99/99/9999">
                            </cc1:MaskedEditExtender>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataFim">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TextBox_DataFim"
                                InputDirection="LeftToRight" MaskType="Date" Mask="99/99/9999">
                            </cc1:MaskedEditExtender>
                        </span>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
