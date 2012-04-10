<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLoteImunobiologico.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormLoteImunobiologico" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <div id="top">
                <h2>
                    Formulário para cadastro de lote imunobiológico
                </h2>
                <fieldset class="formulario">
                    <legend>Informações</legend>
                    <p>
                        <span class="rotulo">Imunobiológico</span> <span>
                            <asp:DropDownList ID="DropDownList_Vacina" CssClass="drop" runat="server" Width="300px"
                                DataTextField="Nome" DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFabricantes"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList_Vacina" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                <span class="rotulo">Fabricante</span> <span>
                                    <asp:DropDownList ID="DropDownList_Fabricante" CssClass="drop" runat="server" Width="300px" AutoPostBack="true"
                                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaAplicacao" DataTextField="Nome"
                                        DataValueField="Codigo">
                                        <%-- <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList_Fabricante" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="DropDownList_Vacina" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                <span class="rotulo">Nº Aplicações</span> <span>
                                    <asp:DropDownList ID="DropDownList_Aplicacao" CssClass="drop" runat="server" Width="100px"
                                        DataTextField="Aplicacao" DataValueField="Codigo">
                                        <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <p>
                        <span class="rotulo">Lote</span> <span>
                            <asp:TextBox ID="TextBox_Lote" runat="server" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Data de Validade</span> <span>
                            <asp:TextBox ID="TextBox_Validade" runat="server" CssClass="campo"></asp:TextBox>
                        </span>
                    </p>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LnkButton1" runat="server" OnClick="OnClick_SalvarLote"
                        OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_SalvarLote')) return confirm('Todos os dados do lote estão corretos ?'); return false;" >
                  <img id="imgSalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgSalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgSalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LnkButton2" runat="server" PostBackUrl="~/Vacina/FormExibirPesquisarLote.aspx">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn_cancelar1.png';" /></asp:LinkButton>
                    </div>
                    <p>
                        <span>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Imunobiológico é Obrigatório."
                                ControlToValidate="DropDownList_Vacina" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None" ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Fabricante é Obrigatório."
                                ControlToValidate="DropDownList_Fabricante" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None" ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Nº Aplicações é Obrigatório."
                                ControlToValidate="DropDownList_Aplicacao" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None" ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Lote é Obrigatório."
                                ControlToValidate="TextBox_Lote" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data de Validade é Obrigatório."
                                ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Validade com formato inválido."
                                ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None" Type="Date" Operator="DataTypeCheck"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900."
                                ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_SalvarLote"
                                Display="None" Type="Date" ValueToCompare="01/01/1900" Operator="GreaterThanEqual"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_SalvarLote"
                                ShowMessageBox="true" ShowSummary="false" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_Validade"
                                Format="dd/MM/yyyy">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_Validade"
                                Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                            </cc1:MaskedEditExtender>
                        </span>
                    </p>
                </fieldset>
            </div>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
