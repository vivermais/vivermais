﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormMovimentacao.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormMovimentacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 640px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 10px 10px 20px 10px;
        }
    </style>

    <script type="text/javascript" language="javascript">
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

        function LoadAllowBlock() {
            __doPostBack('ctl00_BodyContentMid_TabContainer_Movimentacao', '');
        } 
    </script>

</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" >
    <ContentTemplate>--%>
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender1"
        TargetControlID="allwaysOnMessage" VerticalSide="Bottom" VerticalOffset="100"
        HorizontalSide="Left" HorizontalOffset="150" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <asp:Panel ID="allwaysOnMessage" runat="server" Height="160px" Width="140px" BackColor="White"
        ForeColor="Black" BorderWidth="2" BorderStyle="Solid" BorderColor="Black" Style="margin-left: 780px;">
        <div style="vertical-align: middle; text-align: center;">
            <p>
                ATENÇÃO</p>
            Usuário, antes de finalizar a movimentação escolhida, confirme todos os dados informados.
            <br />
        </div>
    </asp:Panel>
    <div id="top">
        <h2>
            Movimentação de Medicamento:
            <asp:Label ID="Label_Farmacia" runat="server" Text=""></asp:Label></h2>
        <%--            <fieldset class="formulario">
            <legend>Farmácia: <asp:Label ID="Label_Farmacia" runat="server" Text=""></asp:Label></legend>
            <p>
                <span>--%>
        <asp:Panel ID="Panel_DevolucaoPaciente" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Devolução do Paciente</legend>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ObservacaoDevolucaoPaciente" runat="server"
                            TextMode="MultiLine" Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarDevolucaoPaciente" runat="server" Text="Salvar" CommandArgument="CommandArgument_DevolucaoPaciente"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarDevolucaoPaciente')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button2" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Observação é Obrigatório!"
                            ControlToValidate="TextBox_ObservacaoDevolucaoPaciente" Display="None" ValidationGroup="ValidationGroup_SalvarDevolucaoPaciente"></asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarDevolucaoPaciente" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_Doacao" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Doação</legend>
                <%--<asp:Panel ID="Panel_SituacaoDoacao" runat="server" Visible="false">--%>
                <p>
                    <span class="rotulo">Situação</span> <span style="margin-left: 5px;">
                        <%--<asp:Label ID="Label_SituacaoDoacao" runat="server" Text=""></asp:Label>--%>
                        <%--<asp:DropDownList ID="DropDownList_SituacaoDoacao" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_SituacaoEmprestimoDoacao">
                                            </asp:DropDownList>--%>
                        <asp:DropDownList ID="DropDownList_SituacaoDoacao" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <%--</asp:Panel>--%>
                <p>
                    <span class="rotulo">Motivo</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_MotivoDoacao" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">EAS</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_EstabelecimentoAssistencialDoacao" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ResponsavelEnvioDoacao" CssClass="campo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Receb.</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ResponsavelRecebimentoDoacao" CssClass="campo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_DataEnvioDoacao" CssClass="campo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataEnvioDoacao">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataEnvioDoacao" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Recebimento</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_DataRecebimentoDoacao" CssClass="campo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataRecebimentoDoacao">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataRecebimentoDoacao" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ObservacaoDoacao" CssClass="campo" runat="server" TextMode="MultiLine"
                            Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarDoacao" runat="server" Text="Salvar" CommandArgument="CommandArgument_Doacao"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarDoacao')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button3" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <%--<asp:CompareValidator ID="CompareValidator_SituacaoDoacao" runat="server" ErrorMessage="Selecione uma Situação!" ControlToValidate="DropDownList_SituacaoDoacao" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao" Enabled="false"></asp:CompareValidator>--%>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione um Motivo!"
                            ControlToValidate="DropDownList_MotivoDoacao" ValueToCompare="-1" Operator="GreaterThan"
                            Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <%--<asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um Estabelecimento Assistencial de Saúde!" ControlToValidate="DropDownList_EstabelecimentoAssistencialDoacao" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="Responsável pelo Envio é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelEnvioDoacao" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox_ResponsavelEnvioDoacao"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Envio."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="Responsável pelo Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelRecebimentoDoacao" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox_ResponsavelRecebimentoDoacao"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Recebimento."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="Data de Envio é Obrigatório!"
                            ControlToValidate="TextBox_DataEnvioDoacao" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator44" runat="server" ErrorMessage="Formato inválido para Data de Envio."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataEnvioDoacao"
                            Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator31" runat="server" ErrorMessage="Data de Envio deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataEnvioDoacao"
                            Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ErrorMessage="Data de Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_DataRecebimentoDoacao" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator32" runat="server" ErrorMessage="Formato inválido para Data de Recebimento."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataRecebimentoDoacao"
                            Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator33" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataRecebimentoDoacao"
                            Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator34" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que Data de Envio."
                            Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataRecebimentoDoacao"
                            ControlToCompare="TextBox_DataEnvioDoacao" Display="None" ValidationGroup="ValidationGroup_SalvarDoacao"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarDoacao" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_Emprestimo" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Empréstimo</legend>
                <p>
                    <span class="rotulo">Situação</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_SituacaoEmprestimo" runat="server">
                        </asp:DropDownList>
                        <%--<asp:DropDownList ID="DropDownList_SituacaoEmprestimo" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="OnSelectedIndexChanged_SituacaoEmprestimoDoacao">
                                            </asp:DropDownList>--%>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Motivo</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_MotivoEmprestimo" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">EAS</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_EstabelecimentoAssistencialEmprestimo" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelEnvioEmprestimo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Receb.</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelRecebimentoEmprestimo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataEnvioEmprestimo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender9" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataEnvioEmprestimo">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataEnvioEmprestimo" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Recebimento</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataRecebimentoEmprestimo" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender10" runat="server" Format="dd/MM/yyyy"
                            TargetControlID="TextBox_DataRecebimentoEmprestimo">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataRecebimentoEmprestimo" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ObservacaoEmprestimo" runat="server" TextMode="MultiLine"
                            Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarEmprestimo" runat="server" Text="Salvar" CommandArgument="CommandArgument_Emprestimo"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarEmprestimo')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button4" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Selecione um Motivo!"
                            ControlToValidate="DropDownList_MotivoEmprestimo" ValueToCompare="-1" Operator="GreaterThan"
                            Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <%--<asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Selecione uma Situação!" ControlToValidate="DropDownList_SituacaoEmprestimo" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>--%>
                        <%--<asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Selecione um Estabelecimento Assistencial de Saúde!" ControlToValidate="DropDownList_EstabelecimentoAssistencialEmprestimo" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Responsável pelo Envio é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelEnvioEmprestimo" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_ResponsavelEnvioEmprestimo"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Envio."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Responsável pelo Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelRecebimentoEmprestimo" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="TextBox_ResponsavelRecebimentoEmprestimo"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Recebimento."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Data de Envio é Obrigatório!"
                            ControlToValidate="TextBox_DataEnvioEmprestimo" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Formato inválido para Data de Envio."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataEnvioEmprestimo"
                            Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Data de Envio deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataEnvioEmprestimo"
                            Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Data de Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_DataRecebimentoEmprestimo" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator10" runat="server" ErrorMessage="Formato inválido para Data de Recebimento."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataRecebimentoEmprestimo"
                            Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator35" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataRecebimentoEmprestimo"
                            Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator36" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que Data de Envio."
                            Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataRecebimentoEmprestimo"
                            ControlToCompare="TextBox_DataEnvioEmprestimo" Display="None" ValidationGroup="ValidationGroup_SalvarEmprestimo"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarEmprestimo" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_Perda" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Perda</legend>
                <p>
                    <span class="rotulo">Motivo</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_MotivoPerda" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ObservacaoPerda" runat="server" TextMode="MultiLine"
                            Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarPerda" runat="server" Text="Salvar" CommandArgument="CommandArgument_Perda"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarPerda')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button5" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:CompareValidator ID="CompareValidator20" runat="server" ErrorMessage="Selecione um Motivo!"
                            ControlToValidate="DropDownList_MotivoPerda" ValueToCompare="-1" Operator="GreaterThan"
                            Display="None" ValidationGroup="ValidationGroup_SalvarPerda"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarPerda" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_Remanejamento" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Remanejamento</legend>
                <p>
                    <span class="rotulo">Farmácia de Destino</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_FarmaciaDestinoRemanejamento" runat="server">
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelEnvioRemanejamento" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Receb.</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelRecebimentoRemanejamento" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataEnvioRemanejamento" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataEnvioRemanejamento">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataEnvioRemanejamento" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Recebimento</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataRecebimentoRemanejamento" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataRecebimentoRemanejamento">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataRecebimentoRemanejamento" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ObservacaoRemanejamento" runat="server"
                            TextMode="MultiLine" Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarRemanejamento" runat="server" Text="Salvar" CommandArgument="CommandArgument_Remanejamento"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarRemanejamento')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button6" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione uma farmácia de destino."
                            ValueToCompare="-1" Operator="GreaterThan" ControlToValidate="DropDownList_FarmaciaDestinoRemanejamento"
                            Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="Responsável pelo Envio é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelEnvioRemanejamento" Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="TextBox_ResponsavelEnvioRemanejamento"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Envio."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="Responsável pelo Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelRecebimentoRemanejamento" Display="None"
                            ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="TextBox_ResponsavelRecebimentoRemanejamento"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Recebimento."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ErrorMessage="Data de Envio é Obrigatório!"
                            ControlToValidate="TextBox_DataEnvioRemanejamento" Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator21" runat="server" ErrorMessage="Formato inválido para Data de Envio."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataEnvioRemanejamento"
                            Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator22" runat="server" ErrorMessage="Data de Envio deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataEnvioRemanejamento"
                            Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="Data de Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_DataRecebimentoRemanejamento" Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator23" runat="server" ErrorMessage="Formato inválido para Data de Recebimento."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataRecebimentoRemanejamento"
                            Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator24" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataRecebimentoRemanejamento"
                            Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator25" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que Data de Envio."
                            Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataRecebimentoRemanejamento"
                            ControlToCompare="TextBox_DataEnvioRemanejamento" Display="None" ValidationGroup="ValidationGroup_SalvarRemanejamento"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarRemanejamento" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_TransferenciaInterna" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Transferência Interna</legend>
                <p>
                    <span class="rotulo">Setor de Destino</span> <span style="margin-left: 5px;">
                        <asp:DropDownList ID="DropDownList_SetorDestinoTransferenciaInterna" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelEnvioTransferenciaInterna" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável pelo Receb.</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_ResponsavelRecebimentoTransferenciaInterna"
                            runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Envio</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataEnvioTransferenciaInterna" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender7" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataEnvioTransferenciaInterna">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataEnvioTransferenciaInterna" InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Recebimento</span> <span style="margin-left: 5px;">
                        <asp:TextBox CssClass="campo" ID="TextBox_DataRecebimentoTransferenciaInterna" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender8" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataRecebimentoTransferenciaInterna">
                        </cc1:CalendarExtender>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" Mask="99/99/9999"
                            MaskType="Date" TargetControlID="TextBox_DataRecebimentoTransferenciaInterna"
                            InputDirection="LeftToRight">
                        </cc1:MaskedEditExtender>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ObservacaoTransferenciaInterna" CssClass="campo" runat="server"
                            TextMode="MultiLine" Width="630px" Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarTransferenciaInterna" runat="server" Text="Salvar" CommandArgument="CommandArgument_TransferenciaInterna"
                            OnClick="OnClick_SalvarMovimentacao" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarTransferenciaInterna')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button7" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:CompareValidator ID="CompareValidator13" runat="server" ErrorMessage="Selecione um Setor de Destino."
                            ValueToCompare="-1" Operator="GreaterThan" ControlToValidate="DropDownList_SetorDestinoTransferenciaInterna"
                            Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="Responsável pelo Envio é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelEnvioTransferenciaInterna" Display="None"
                            ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="TextBox_ResponsavelEnvioTransferenciaInterna"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Envio."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Responsável pelo Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_ResponsavelRecebimentoTransferenciaInterna" Display="None"
                            ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="TextBox_ResponsavelRecebimentoTransferenciaInterna"
                            Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável pelo Recebimento."
                            Font-Size="XX-Small" ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Data de Envio é Obrigatório!"
                            ControlToValidate="TextBox_DataEnvioTransferenciaInterna" Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator26" runat="server" ErrorMessage="Formato inválido para Data de Envio."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataEnvioTransferenciaInterna"
                            Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator27" runat="server" ErrorMessage="Data de Envio deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataEnvioTransferenciaInterna"
                            Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Data de Recebimento é Obrigatório!"
                            ControlToValidate="TextBox_DataRecebimentoTransferenciaInterna" Display="None"
                            ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator28" runat="server" ErrorMessage="Formato inválido para Data de Recebimento."
                            Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataRecebimentoTransferenciaInterna"
                            Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator29" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que 01/01/1900."
                            Operator="GreaterThanEqual" Type="Date" ValueToCompare="01/01/1900" ControlToValidate="TextBox_DataRecebimentoTransferenciaInterna"
                            Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator30" runat="server" ErrorMessage="Data de Recebimento deve ser igual ou maior que Data de Envio."
                            Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataRecebimentoTransferenciaInterna"
                            ControlToCompare="TextBox_DataEnvioTransferenciaInterna" Display="None" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary8" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_SalvarTransferenciaInterna" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <asp:Panel ID="Panel_AcertoBalanco" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Acerto de Balanço</legend>
                <p>
                    <span class="rotulo">Data</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_DataAcertoBalanco" runat="server" ReadOnly="true" CssClass="campo">
                        </asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Responsável</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ResponsavelAcertoBalanco" runat="server" CssClass="campo"
                            Width="300px">
                        </asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Observação</span> <span style="margin-left: 5px;">
                        <asp:TextBox ID="TextBox_ObservacaoAcertoBalanco" CssClass="campo" runat="server" TextMode="MultiLine" Width="630px"
                            Height="200px" Columns="20" Rows="6"></asp:TextBox>
                    </span>
                </p>
                <p align="center">
                    <span>
                        <asp:Button ID="ButtonSalvarAcertoBalanco" runat="server" Text="Salvar" ValidationGroup="ValidationGroup_AcertoBalanco"
                            OnClick="OnClick_SalvarMovimentacao" CommandArgument="CommandArgument_AcertoBalanco"
                            OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_AcertoBalanco')) return confirm('Tem certeza que deseja finalizar esta movimentação ?'); return false;" />
                        <asp:Button ID="Button8" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Responsável é Obrigatório!"
                            ValidationGroup="ValidationGroup_AcertoBalanco" Display="None" ControlToValidate="TextBox_ResponsavelAcertoBalanco">
                        </asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary5" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_AcertoBalanco" />
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
        <%--                </span>
            </p>
           
        </fieldset>--%>
        <%--        <asp:UpdatePanel ID="UpdatePanel_Oito" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_SituacaoDoacao" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>--%>
        <fieldset class="formulario">
            <legend>Medicamentos da Movimentação</legend>
            <p>
                <span>
                    <cc1:Accordion ID="Accordion_IncluirMedicamento" Enabled="true" runat="server" RequireOpenedPane="false"
                        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                        ContentCssClass="accordionContent" SelectedIndex="-1">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ContentTemplate>
                        </ContentTemplate>
                        <Panes>
                            <cc1:AccordionPane ID="AccordionPane_IncluirMedicamentos" runat="server">
                                <Header>
                                    Incluir Medicamentos</Header>
                                <Content>
                                    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <ContentTemplate>
                                            <p>
                                                <span class="rotulo">Medicamento</span> <span style="margin-left: 5px;">
                                                    <asp:DropDownList ID="DropDownList_MedicamentoMovimentacao" Width="300px" runat="server"
                                                        AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFabricante"
                                                        CausesValidation="true" DataTextField="Nome" DataValueField="Codigo">                                                        
                                                    </asp:DropDownList>                                                    
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Fabricante</span> <span style="margin-left: 5px;">
                                                    <asp:DropDownList ID="DropDownList_FabricanteMovimentacao" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaLote">
                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Lote</span> <span style="margin-left: 5px;">
                                                    <asp:DropDownList ID="DropDownList_LoteMovimentacao" runat="server">
                                                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </span>
                                            </p>
                                            <p>
                                                <span class="rotulo">Quantidade</span> <span style="margin-left: 5px;">
                                                    <asp:TextBox CssClass="campo" ID="TextBox_QuantidadeMedicamentoMovimentacao" MaxLength="6" Width="30px"
                                                        runat="server"></asp:TextBox>
                                                </span>
                                            </p>
                                            <p align="center">
                                                <span>
                                                    <asp:Button ID="Button_IncluirMedicamento" runat="server" Text="Incluir" OnClick="OnClick_IncluirMedicamento"
                                                        ValidationGroup="ValidationGroup_IncluirMedicamento" />
                                                    <asp:Button ID="Button_CancelarInclusao" runat="server" Text="Limpar" OnClick="OnClick_CancelarInclusao" />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Medicamento é Obrigatório!"
                                                        ControlToValidate="DropDownList_MedicamentoMovimentacao" ValueToCompare="-1"
                                                        Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Fabricante é Obrigatório!"
                                                        ControlToValidate="DropDownList_FabricanteMovimentacao" ValueToCompare="-1" Operator="GreaterThan"
                                                        Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                                                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Lote é Obrigatório!"
                                                        ControlToValidate="DropDownList_LoteMovimentacao" ValueToCompare="-1" Operator="GreaterThan"
                                                        Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Quantidade é Obrigatório!"
                                                        ControlToValidate="TextBox_QuantidadeMedicamentoMovimentacao" Display="None"
                                                        ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento."
                                                        ControlToValidate="TextBox_QuantidadeMedicamentoMovimentacao" ValidationExpression="^\d*$"
                                                        Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RegularExpressionValidator>
                                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                                                        ControlToValidate="TextBox_QuantidadeMedicamentoMovimentacao" ValueToCompare="0"
                                                        Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                                        ShowSummary="false" ValidationGroup="ValidationGroup_IncluirMedicamento" />
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordionPane_MecicamentosCadastrados" runat="server">
                                <Header>
                                    Medicamentos Incluídos</Header>
                                <Content>
                                    <asp:UpdatePanel ID="UpdatePanel_Dois" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                                        <%--<Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="Button_IncluirMedicamento" EventName="Click" />
                                        </Triggers>--%>
                                        <ContentTemplate>
                                            <p>
                                                <span>
                                                    <asp:GridView ID="GridView_MedicamentosMovimentacao" Width="400px" OnRowDataBound="OnRowDataBound_FormataGridView"
                                                        OnRowDeleting="OnRowDeleting_Deletar" DataKeyNames="CodigoLote" OnRowEditing="OnRowEditing_Editar"
                                                        OnRowUpdating="OnRowUpdating_Alterar" OnRowCancelingEdit="OnRowCancelingEdit_CancelarEdicao"
                                                        OnPageIndexChanging="OnPageIndexChanging_Paginacao" AllowPaging="true" PageSize="20"
                                                        PagerSettings-Mode="Numeric" runat="server" AutoGenerateColumns="false">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ItemStyle-HorizontalAlign="Center"
                                                                ReadOnly="true" />
                                                            <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center"
                                                                ReadOnly="true" />
                                                            <asp:BoundField HeaderText="Lote" DataField="NomeLote" ItemStyle-HorizontalAlign="Center"
                                                                ReadOnly="true" />
                                                            <asp:TemplateField HeaderText="Quantidade" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label_Quantidade" runat="server" Text='<%#bind("Quantidade") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="TextBox_Quantidade" Width="30" CssClass="campo" MaxLength="6" runat="server" Text='<%#bind("Quantidade") %>'></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Quantidade é Obrigatório!"
                                                                        ControlToValidate="TextBox_Quantidade" Display="None"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números na quantidade do medicamento."
                                                                        ControlToValidate="TextBox_Quantidade" ValidationExpression="^\d*$" Display="None"></asp:RegularExpressionValidator>
                                                                    <asp:CompareValidator ID="CompareValidator12" runat="server" ErrorMessage="Quantidade deve ser maior que zero."
                                                                        ControlToValidate="TextBox_Quantidade" ValueToCompare="0" Operator="GreaterThan"
                                                                        Display="None"></asp:CompareValidator>
                                                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                                                                        ShowSummary="false" />
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Link" CancelText="Cancelar" DeleteText="Excluir" EditText="Editar"
                                                                UpdateText="Atualizar" InsertVisible="false" ShowCancelButton="true" ShowDeleteButton="true"
                                                                ShowEditButton="true" />
                                                        </Columns>
                                                        <HeaderStyle CssClass="tab" />
                                                        <RowStyle CssClass="tabrow" />
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhnum registro encontrado."></asp:Label>
                                                        </EmptyDataTemplate>
                                                    </asp:GridView>
                                                </span>
                                            </p>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>
                </span>
            </p>
        </fieldset>
        <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
    <%--    </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
