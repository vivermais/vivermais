﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCodigoIdentificacao.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormCodigoIdentificacao" MasterPageFile="~/Urgencia/MasterUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">

<%--    <script type="text/javascript" language="javascript">
        function showTooltip(obj) {
            if (obj.options[obj.selectedIndex].title == "") {
                obj.title = obj.options[obj.selectedIndex].text;
                obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
                for (i = 0; i < obj.options.length; i++) {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
        }
    </script>--%>

</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Código de Identificação</h2>
                <fieldset class="formulario">
                    <legend>Identificação de Usuário</legend>
                    <p>
                        <span class="rotulo">Cartão SUS</span> <span>
                            <asp:TextBox ID="tbxCartaoSUS" runat="server" CssClass="campo" MaxLength="15"></asp:TextBox></span>
                        <asp:ImageButton ID="imgBtnCartao" runat="server" OnClick="imgBtnCartao_Click" ImageUrl="~/Urgencia/img/procurar.png"
                            Width="16px" Height="16px" style="position:absolute; margin-top:3px;" ValidationGroup="ValidationGroup_PesquisaCartaoSUS" />
                    </p>
                    <%--                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCartaoSUS"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small" Style="margin-left: 95px;"></asp:RequiredFieldValidator>--%>
                    <p>
                        <span class="rotulo">CNES</span> <span>
                            <asp:TextBox ID="tbxCNES" runat="server" CssClass="campo"></asp:TextBox>
                            <asp:DropDownList ID="ddlCNES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCNES_SelectedIndexChanged"
                                Visible="False" Width="180px" Font-Size="12px">
                            </asp:DropDownList>
                            <asp:ImageButton ID="imgBtnCNES" CausesValidation="false" runat="server" OnClick="imgBtnCNES_Click"
                                ImageUrl="~/Urgencia/img/procurar.png" Width="16px" Height="16px" style="position:absolute; margin-top:3px;" />                        </span>                    </p>
                    <p>
                        <span class="rotulo">Usuário</span> <span>
                            <asp:DropDownList ID="ddlUsuario" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUsuario_SelectedIndexChanged"
                                Width="180px" Font-Size="12px" CssClass="drop">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Senha</span> <span>
                            <asp:TextBox ID="tbxSenha" runat="server" CssClass="campo" TextMode="Password"></asp:TextBox>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="ButtonValidarCodigo" runat="server" OnClick="OnClick_Validar" ValidationGroup="ValidationGroup_ValidarUsuario" >
                <img id="imgvalidar" alt="Validar" src="img/bts/btn-validar1.png"
                onmouseover="imgvalidar.src='img/bts/btn-validar2.png';"
                onmouseout="imgvalidar.src='img/bts/btn-validar1.png';" /></asp:LinkButton>
                        </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="Button2" runat="server" PostBackUrl="~/Urgencia/Default.aspx" >
                <img id="imgcancelar" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                onmouseover="imgcancelar.src='img/bts/btn_cancelar2.png';"
                onmouseout="imgcancelar.src='img/bts/btn_cancelar1.png';" /></asp:LinkButton>
                        </div>
                        
                            <p align="center">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cartão SUS é Obrigatório!"
                            ControlToValidate="tbxCartaoSUS" ValidationGroup="ValidationGroup_PesquisaCartaoSUS"
                            Display="None" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"
                            ErrorMessage="O Cartão SUS deve conter 15 dígitos." ControlToValidate="tbxCartaoSUS"
                            ValidationExpression="^\d{15}$" ValidationGroup="ValidationGroup_PesquisaCartaoSUS">
                        </asp:RegularExpressionValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_PesquisaCartaoSUS" />
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Usuário é Obrigatório!"
                            ControlToValidate="ddlUsuario" Display="None" ValueToCompare="-1" Operator="GreaterThan"
                            ValidationGroup="ValidationGroup_ValidarUsuario"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Senha é Obrigatório!"
                            ControlToValidate="tbxSenha" ValidationGroup="ValidationGroup_ValidarUsuario"
                            Display="None" />
                        <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_ValidarUsuario" />
                        <%--                        <asp:ImageButton ID="imgBtnLogin" runat="server"
                            OnClick="OnClick_Validar" ImageUrl="~/images/enter.gif"
                            Width="77px" Height="25px" />--%>
                    </p>
                    <%--            <p>
                <span class="rotulo">Cartão SUS</span>
                <span style="margin-left:5px;">
                    <asp:TextBox ID="TextBox_CartaoSUS" runat="server" MaxLength="15" CssClass="campo"></asp:TextBox>
                </span>
                <asp:Button ID="ButtonBuscarCartaoSUS" runat="server" Text="Pesquisar" ValidationGroup="ValidationGroup_PesquisarCartaoSUS"/>
            </p>
            <p>
                <span class="rotulo">CNES</span>
                <span style="margin-left:5px;">
                    <asp:TextBox ID="TextBox_CNES" runat="server" CssClass="campo"></asp:TextBox>
                </span>
                 <asp:Button ID="ButtonBuscarCNES" runat="server" Text="Pesquisar" ValidationGroup="ValidationGroup_PesquisarCNES" />
            </p>
            <p>
                <span class="rotulo">Usuário</span>
                <span style="margin-left:5px;">
                    <asp:DropDownList ID="DropDownList_Usuario" runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            
            <p>
                <span class="rotulo">Senha</span>
                <span style="margin-left:5px;"><asp:TextBox ID="TextBox_Senha" CssClass="campo" runat="server" TextMode="Password"></asp:TextBox></span>
            </p>
            <p align="center">
                <asp:Button ID="Button_Login" runat="server" Text="Validar" OnClick="OnClick_Validar" ValidationGroup="ValidationGroup_Validar"/>
                <asp:Button ID="Button_Voltar" runat="server" Text="Voltar" PostBackUrl="~/Urgencia/Default.aspx" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um usuário." Display="None" ControlToValidate="DropDownList_Usuario" ValidationGroup="ValidationGroup_Validar" Operator="GreaterThan" ValueToCompare="-1"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server" ErrorMessage="Senha é Obrigatório!" ControlToValidate="TextBox_Senha" Display="None" ValidationGroup="ValidationGroup_Validar"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Validar" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Cartão SUS é Obrigatório." ControlToValidate="TextBox_CartaoSUS" Display="None" ValidationGroup="ValidationGroup_PesquisarCartaoSUS"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="O Cartão SUS deve conter 15 dígitos." ControlToValidate="TextBox_CartaoSUS" Display="None" ValidationExpression="^\d{15}$" ValidationGroup="ValidationGroup_PesquisarCartaoSUS"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarCartaoSUS" />
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="CNES é Obrigatório!" ControlToValidate="TextBox_CNES" Display="None" ValidationGroup="ValidationGroup_PesquisarCNES"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarCNES"/>
            </p>--%>
                </fieldset>
                <asp:Panel ID="Panel_CodigoIdentificacao" runat="server" Visible="false"
                 DefaultButton="ButtonSalvarCodigo">
                    <fieldset class="formulario">
                        <legend>Código de Identificação</legend>
                        <p>
                            <span class="rotulo">Seu Código</span> <span>
                                <asp:Label ID="lbCodigo" runat="server" Text=""></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Novo Código</span> <span>
                                <asp:TextBox ID="TextBox_Codigo" CssClass="campo" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Confirmar Código</span> <span>
                                <asp:TextBox ID="TextBox_ConfirmaCodigo" CssClass="campo" runat="server" TextMode="Password" MaxLength="20"></asp:TextBox>
                            </span>
                        </p>
                        <div class="botoesroll">
                    <asp:LinkButton ID="ButtonSalvarCodigo" runat="server" OnClick="OnClick_SalvarCodigo"
                     ValidationGroup="ValidationGroup_Codigo" CausesValidation="true" >
                <img id="imgsalvar" alt="Salvar Código" src="img/bts/btn_salvar1.png"
                onmouseover="imgsalvar.src='img/bts/btn_salvar2.png';"
                onmouseout="imgsalvar.src='img/bts/btn_salvar1.png';" /></asp:LinkButton>
                        </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="ButtonCancelar_Confirmacao" runat="server" OnClick="OnClick_CancelarCodigo" >
                <img id="imgcancelar_confirm" alt="Cancelar" src="img/bts/btn_cancelar1.png"
                onmouseover="imgcancelar_confirm.src='img/bts/btn_cancelar2.png';"
                onmouseout="imgcancelar_confirm.src='img/bts/btn_cancelar1.png';" /></asp:LinkButton>
                        </div>
                        <p align="center">
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Código é Obrigatório!"
                                    Display="None" ControlToValidate="TextBox_Codigo" ValidationGroup="ValidationGroup_Codigo"></asp:RequiredFieldValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Confirmar Código é Obrigatório!"
                                    Display="None" ControlToValidate="TextBox_ConfirmaCodigo" ValidationGroup="ValidationGroup_Codigo"></asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="O código não confere com sua confirmação!"
                                    ControlToValidate="TextBox_Codigo" ControlToCompare="TextBox_ConfirmaCodigo"
                                    Operator="Equal" Display="None" ValidationGroup="ValidationGroup_Codigo"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O código de identificação deve conter pelo menos 8 caracteres entre números e letras e sem caracteres especiais."
                                    ValidationExpression="^(?=.{8}).*([A-Za-z][0-9]|[0-9][A-Za-z]).*$" ControlToValidate="TextBox_Codigo"
                                    ValidationGroup="ValidationGroup_Codigo" Display="None"></asp:RegularExpressionValidator>
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ValidationGroup_Codigo"
                                    ShowMessageBox="true" ShowSummary="false" />
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
