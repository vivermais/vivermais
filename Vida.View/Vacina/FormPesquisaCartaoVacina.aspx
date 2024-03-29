﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormPesquisaCartaoVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormPesquisaCartaoVacina" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="style_form_vacina2.css" type="text/css" media="screen" />
    <title>Formulário para impressão do Cartão de Vacina</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <div>
        <fieldset class="formulario">
            <legend>Cartão SUS</legend>
            <p>
                <span class="rotulo">Cartão SUS</span> <span>
                    <asp:TextBox ID="TextBox_CartaoSUS" runat="server" MaxLength="15" Width="130" CssClass="campo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Informe o número do Cartão SUS"
                        ValidationGroup="Pesquisa" ControlToValidate="TextBox_CartaoSUS" Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" Font-Size="X-Small" ControlToValidate="TextBox_CartaoSUS"
                        Display="Dynamic" ErrorMessage="O campo Cartão SUS deve conter apenas Números"
                        Operator="DataTypeCheck" Type="Double" ValidationGroup="Pesquisa"></asp:CompareValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Nascimento</span> <span>
                    <asp:TextBox ID="TextBox_DataNascimento" runat="server" Width="70" CssClass="campo"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" TargetControlID="TextBox_DataNascimento"
                        Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="CalendarDataNascimento" runat="server" TargetControlID="TextBox_DataNascimento" >
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe a data de nascimento"
                        ValidationGroup="Pesquisa" ControlToValidate="TextBox_DataNascimento" Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareData" runat="server" ControlToValidate="TextBox_DataNascimento"
                        Display="Dynamic" Font-Size="X-Small" ErrorMessage="A data de Nascimento informada é inválida"
                        Operator="DataTypeCheck" Type="Date" ValidationGroup="Pesquisa"></asp:CompareValidator>
                </span>
            </p>
            <p>
                <span>
                    <asp:ImageButton ID="LnkPesquisar" runat="server" OnClick="LnkPesquisarClick" ValidationGroup="Pesquisa"
                        Width="121" Height="39" ImageUrl="~/Vacina/img/btn-pesquisar.png" /></span>
            </p>
            <p>
                <span>
                    <asp:GridView ID="GridViewPacientes" runat="server" AutoGenerateColumns="False" Width="100%" 
                        OnSelectedIndexChanged="GridViewPacientes_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" SelectText="Selecionar" ButtonType="Button">
                            </asp:CommandField>
                            <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            <asp:BoundField DataField="NomeMae" HeaderText="Nome da Mãe" />
                            <asp:BoundField DataField="DataNascimento" ItemStyle-HorizontalAlign="Center" HeaderText="Data de Nascimento"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                Nenhum paciente foi localizado com os dados fornecidos.</p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="tab" BackColor="#28718e" Font-Bold="True" ForeColor="#ffffff"
                                        Height="16px" Font-Size="12px" />
                                    <FooterStyle BackColor="#72b4cf" ForeColor="#ffffff" />
                                    <RowStyle CssClass="tabrow" BackColor="#72b4cf" ForeColor="#ffffff" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#72b4cf" ForeColor="#ffffff" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#72b4cf" />
                    </asp:GridView>
                    <%--<asp:Label ID="lblMensagem" Font-Size="X-Small" runat="server" ForeColor="Red" Text="Nenhum paciente foi localizado com os dados fornecidos"></asp:Label>--%>
                </span>
            </p>
        </fieldset>
        <asp:Panel ID="PanelImprimeCartao" runat="server" Visible="false">
            <fieldset class="formulario">
                <legend>Dados do Paciente</legend>
                <p>
                    <span class="rotulo">Nome:</span> <span>
                        <asp:Label ID="lblNome" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Nome da Mãe:</span> <span>
                        <asp:Label ID="lblNomeMae" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Data do Nascimento:</span> <span>
                        <asp:Label ID="lblDataNascimento" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
                <p>
                    <span class="rotulo">Cartão do SUS:</span> <span>
                        <asp:Label ID="lblCartaoSUS" runat="server" Font-Size="12px" Font-Bold="true" /></span>
                </p>
            </fieldset>
            <h4>
                Clique no botão abaixo para imprimir o Cartão de Vacina</h4>
            <div class="botoesroll">
                <asp:ImageButton ID="lknCartaoVacina" runat="server" OnClick="btnCartaoVacina_Click"
                    ImageUrl="~/Vacina/img/btn-visualizarcartao.png" Width="168" Height="39" />
            </div>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
