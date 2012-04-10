<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCAlterarSenha.ascx.cs"
    Inherits="ViverMais.View.Seguranca.WUCAlterarSenha" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<link rel="stylesheet" href="style_form_seguranca.css" type="text/css" media="screen" />--%>

<script type="text/javascript" src="js/jquery.js"></script>

<script type="text/javascript" src="js/mocha.js"></script>

        <p>
            <span class="rotulo">Senha Atual</span> <span>
                <asp:TextBox ID="tbxSenhaAtual" CssClass="campo" TextMode="Password" runat="server"
                    MaxLength="15"></asp:TextBox>
            </span>
        </p>
        <p>
            <span class="rotulo">Senha Nova</span> <span>
                <asp:TextBox ID="tbxSenhaNova" CssClass="campo" TextMode="Password" runat="server"
                    MaxLength="15"></asp:TextBox>
                <%--               <cc1:PasswordStrength ID="PS" runat="server" TargetControlID="tbxSenhaNova" DisplayPosition="RightSide"
                    StrengthIndicatorType="Text" PreferredPasswordLength="10" PrefixText="Força: "
                    MinimumNumericCharacters="1" MinimumSymbolCharacters="6"
                    RequiresUpperAndLowerCaseCharacters="false" TextStrengthDescriptions="Muito ruim; Fraco; Média; Forte; Excelente"
                    TextStrengthDescriptionStyles="Forca1Senha;Forca2Senha;Forca3Senha;Forca4Senha;Forca5Senha"
                    CalculationWeightings="5;15;40;40" />--%>
            <asp:Label ID="complexity" runat="server" CssClass="defaultForce"></asp:Label>
<%--            <div id="complexity" class="defaultForce">
                </div>--%></span>
            <span>
            
            </span>
        </p>
        <p>
            <span class="rotulo">Confirme a Senha Nova</span> <span>
                <asp:TextBox ID="tbxConfirmSenhaNova" CssClass="campo" TextMode="Password" runat="server"
                    MaxLength="15"></asp:TextBox>
            </span>
        </p>
        <br />
        <div class="botoesroll">
            <asp:LinkButton ID="LnkbtnSalvar" runat="server" ValidationGroup="ValidationGroup_AlterarSenha">
      <img id="imgsalvar" alt="Salvar" runat="server" /></asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="LinkButtonVoltar" runat="server">
            <img id="imgvoltar" alt="Voltar" runat="server" />
            </asp:LinkButton>
        </div>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="ValidationGroup_AlterarSenha" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxSenhaAtual"
            Display="None" ValidationGroup="ValidationGroup_AlterarSenha" ErrorMessage="O campo Senha Atual é Obrigatório!"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxSenhaNova"
            Display="None" ValidationGroup="ValidationGroup_AlterarSenha" ErrorMessage="O campo Senha Nova é Obrigatório!"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxConfirmSenhaNova"
            Display="None" ErrorMessage="A Confirmação da Senha Nova é Obrigatória!" ValidationGroup="ValidationGroup_AlterarSenha"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Os campos de senha nova devem ser iguais!"
            ControlToCompare="tbxSenhaNova" ControlToValidate="tbxConfirmSenhaNova" Display="None"
            ValidationGroup="ValidationGroup_AlterarSenha"></asp:CompareValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxSenhaNova"
            Display="None" ErrorMessage="Sua Senha Nova deve possuir no mínimo 6 caraceteres e no máximo 15, podendo conter letras, números e caracteres especiais sem espaços."
            ValidationExpression="^\S{6,15}$" ValidationGroup="ValidationGroup_AlterarSenha"></asp:RegularExpressionValidator>
        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Senha Nova inválida! Por favor, informe outra senha."
            ControlToValidate="tbxSenhaNova" ValueToCompare="123456" Operator="NotEqual"
            Display="None" ValidationGroup="ValidationGroup_AlterarSenha"></asp:CompareValidator>

