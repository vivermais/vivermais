<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDadosInventario.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormDadosInventario" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Dados do Inventário</h2>
        <fieldset>
            <legend>Informações</legend>
            <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" />
                </span>
            </p>
            <p>
                <span class="rotulo">Unidade</span> <span>
                    <asp:Label ID="Label_Unidade" runat="server" Text="" Font-Bold="true" />
                </span>
            </p>
            <p>
                <span class="rotulo">Situação</span> <span>
                    <asp:Label ID="Label_Situacao" runat="server" Text="" Font-Bold="true" />
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span> <span>
                    <asp:Label ID="Label_DataAbertura" runat="server" Text="" Font-Bold="true" />
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Fechamento</span> <span>
                    <asp:TextBox ID="TextBox_DataFechamento" CssClass="campo" runat="server"></asp:TextBox>
                    <asp:Label ID="Label_DataFechamento" runat="server" Text="" Visible="false" Font-Bold="true" />
                </span>
            </p>
            <br />
            <div class="botoesroll">
                <asp:LinkButton ID="LknVoltar" runat="server" PostBackUrl="~/Vacina/FormInventario.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
            <asp:Panel ID="Panel_FecharInventario" runat="server">
                <div class="botoesroll">
                    <asp:LinkButton ID="LknButton_FecharInventario" runat="server" OnClientClick="javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja fechar este inventário ?'); return false;"
                        OnClick="OnClick_FecharInventario" ValidationGroup="group_altInventario">
                  <img id="imgfecharinvent" alt="Fechar Inventário" src="img/btn_close_invent1.png"
                  onmouseover="imgfecharinvent.src='img/btn_close_invent2.png';"
                  onmouseout="imgfecharinvent.src='img/btn_close_invent1.png';" /></asp:LinkButton>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel_VacinasInventario" runat="server">
                <div class="botoesroll">
                    <asp:LinkButton ID="LknButton_VacinasInventario" runat="server" OnClick="OnClick_ItensInventario">
                  <img id="imgvacivent" alt="Vacinas do Inventário" src="img/btn_vac_invent1.png"
                  onmouseover="imgvacivent.src='img/btn_vac_invent2.png';"
                  onmouseout="imgvacivent.src='img/btn_vac_invent1.png';" /></asp:LinkButton>
                </div>
            </asp:Panel>
            
            <p>
                <span>
                    <%--<asp:Button ID="ButtonVoltar" runat="server" Text="Voltar" PostBackUrl="~/Vacina/FormInventario.aspx" />--%>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                        TargetControlID="TextBox_DataFechamento" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataFechamento">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Fechamento é Obrigatório!"
                        ControlToValidate="TextBox_DataFechamento" Display="None" ValidationGroup="group_altInventario"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Fechamento com formato inválido!"
                        ControlToValidate="TextBox_DataFechamento" Operator="DataTypeCheck" Type="Date"
                        ValidationGroup="group_altInventario" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Fechamento deve ser maior que 01/01/1900!"
                        ControlToValidate="TextBox_DataFechamento" Operator="GreaterThan" ValueToCompare="01/01/1900"
                        Type="Date" ValidationGroup="group_altInventario" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_altInventario"
                        ShowMessageBox="true" ShowSummary="false" />
                </span>
            </p>
        </fieldset>
<%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
            <Triggers>
                <asp:PostBackTrigger ControlID="Lkn_GerarRelatorio" />
            </Triggers>
            <ContentTemplate>--%>
                <fieldset>
                    <legend>Relatórios</legend>
                    <p>
                        <span class="rotulo">Conferência</span> <span class="camporadio">
                            <asp:RadioButton ID="RadioButton_Conferencia" CssClass="camporadio" Width="20px"
                                Checked="true" runat="server" GroupName="group_inventario" ValidationGroup="group_relInventario" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Final</span> <span class="camporadio">
                            <asp:RadioButton ID="RadioButton_Final" CssClass="camporadio" Width="20px" runat="server"
                                GroupName="group_inventario" ValidationGroup="group_relInventario" />
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="Lkn_GerarRelatorio" runat="server" CausesValidation="false" OnClick="OnClick_GerarRelatorio">
                  <img id="imggerarrel" alt="Gerar Relatório" src="img/btn_gerarrel1.png"
                  onmouseover="imggerarrel.src='img/btn_gerarrel2.png';"
                  onmouseout="imggerarrel.src='img/btn_gerarrel1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                </fieldset>
<%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
