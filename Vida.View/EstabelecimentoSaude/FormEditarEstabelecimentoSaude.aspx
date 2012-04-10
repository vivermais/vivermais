﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEditarEstabelecimentoSaude.aspx.cs"
    Inherits="ViverMais.View.EstabelecimentoSaude.FormEditarEstabelecimentoSaude" MasterPageFile="~/EstabelecimentoSaude/MasterEstabelecimento.Master"
    EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder3" ID="c_body" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Editar Estabelecimento de Saúde</h2>
        <fieldset>
            <legend>Estabelecimento de Saúde</legend>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lbRazaoSocial" runat="server" Text="Razão Social"></asp:Label></span>
                <span>
                    <asp:TextBox ID="tbxRazaoSocial" runat="server" Width="400px" CssClass="campo"></asp:TextBox></span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lbNomeFantasia" runat="server" Text="Nome Fantasia"></asp:Label></span>
                <asp:TextBox ID="tbxNomeFantasia" runat="server" Width="400px" CssClass="campo"></asp:TextBox>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lbCNES" runat="server" Text="CNES"></asp:Label></span>
                <asp:Label ID="Label_CNES" runat="server" Text="-" Font-Bold="true" Font-Size="11px"
                    CssClass="campo"></asp:Label>
                <%--<asp:TextBox ID="tbxCNES" runat="server" Enabled="false"></asp:TextBox>--%>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lbTipoEstabelecimento" runat="server" Text="Tipo de Estabelecimento"></asp:Label></span>
                <asp:DropDownList ID="ddltipoestabelecimento" runat="server" CssClass="drop"
                 DataTextField="Descricao" DataValueField="Codigo" Width="350px">
                </asp:DropDownList>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lblSiglaEstabelecimento" runat="server" Text="Sigla Estabelecimento"></asp:Label></span>
                <asp:TextBox ID="tbxSiglaEstabelecimento" runat="server" MaxLength="6" CssClass="campo"></asp:TextBox>
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_OnClick" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">
                            <asp:Label ID="lbLogradouro" runat="server" Text="Logradouro"></asp:Label></span>
                        <asp:TextBox ID="tbxLogradouro" runat="server" Width="450px" CssClass="campo"></asp:TextBox>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_OnClick" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">
                            <asp:Label ID="lbMunicipio" runat="server" Text="Município Gestor"></asp:Label></span>
                        <asp:DropDownList ID="ddlMunicipioGestor" Width="350px" DataTextField="NomeSemUF"
                         DataValueField="Codigo"
                         runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaDistritos"
                            CssClass="drop">
                        </asp:DropDownList>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_OnClick" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipioGestor" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">
                            <asp:Label ID="lbDistrito" runat="server" Text="Distrito"></asp:Label></span>
                        <asp:DropDownList ID="ddlDistrito" CssClass="drop" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaBairro" Width="350px" 
                            DataTextField="Nome" DataValueField="Codigo">
                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="false">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_OnClick" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipioGestor" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlDistrito" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">
                            <asp:Label ID="lbBairro" runat="server" Text="Bairro"></asp:Label></span>
                        <asp:DropDownList ID="DropDownList_Bairro" CssClass="drop" 
                        DataTextField="Nome" DataValueField="Codigo"
                        Width="350px" runat="server">
                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                        <%--<asp:TextBox ID="tbxBairro" runat="server" Width="450px"></asp:TextBox>--%>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lbCEP" runat="server" Text="CEP"></asp:Label></span>
                <asp:TextBox ID="tbxCEP" runat="server" MaxLength="8" CssClass="campo"></asp:TextBox>
                <asp:Button ID="Button_OnClick" runat="server" Text="Buscar" OnClick="OnClick_BuscarEndereco"
                    ValidationGroup="ValidationGroup_BuscarCEP" CssClass="drop" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="CEP é Obrigatório."
                    ControlToValidate="tbxCEP" Display="None" ValidationGroup="ValidationGroup_BuscarCEP"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O CEP deve conter 8 dígitos."
                    ValidationExpression="^\d{8}$" Display="None" ControlToValidate="tbxCEP" ValidationGroup="ValidationGroup_BuscarCEP"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="ValidationGroup_BuscarCEP" />
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="lkn_atualizar" runat="server" OnClick="btn_atualizar_Click" ValidationGroup="group_cadUnidade">
                  <img id="imgatualizar" alt="Atualizar" src="img/atualizar1.png"
                  onmouseover="imgatualizar.src='img/atualizar2.png';"
                  onmouseout="imgatualizar.src='img/atualizar1.png';" /></asp:LinkButton>
            </div>
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" RenderMode="Inline"
                ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Panel ID="Panel_Desativar" runat="server">
                        <div class="botoesroll">
                            <asp:LinkButton ID="lkn_excluir" runat="server" OnClick="btn_ativar_excluir_Click"
                                CommandArgument="excluir" OnClientClick="javascript:return confirm('Tem certeza que deseja bloquear a undidade ?');">
                  <img id="imgbloquear" alt="Bloquear" src="img/bloquear1.png"
                  onmouseover="imgbloquear.src='img/bloquear2.png';"
                  onmouseout="imgbloquear.src='img/bloquear1.png';" /></asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="Panel_Ativar" runat="server" Visible="false">
                        <div class="botoesroll">
                            <asp:LinkButton ID="lkn_ativar" runat="server" OnClick="btn_ativar_excluir_Click"
                                CommandArgument="ativar">
                  <img id="imgdesbloquear" alt="Desbloquear" src="img/desbloquear1.png"
                  onmouseover="imgdesbloquear.src='img/desbloquear2.png';"
                  onmouseout="imgdesbloquear.src='img/desbloquear1.png';" /></asp:LinkButton>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="botoesroll">
                <asp:LinkButton ID="lkn_voltar" runat="server" PostBackUrl="~/EstabelecimentoSaude/FormEstabelecimentoDeSaude.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar1.png"
                  onmouseover="imgvoltar.src='img/voltar2.png';"
                  onmouseout="imgvoltar.src='img/voltar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <div>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Razão Social é Obrigatório!"
                ValidationGroup="group_cadUnidade" ControlToValidate="tbxRazaoSocial" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nome Fantasia é Obrigatório!"
                ValidationGroup="group_cadUnidade" ControlToValidate="tbxNomeFantasia" Display="None"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Logradouro é Obrigatório!"
                ValidationGroup="group_cadUnidade" ControlToValidate="tbxLogradouro" Display="None"></asp:RequiredFieldValidator>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Bairro é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxBairro" Display="None"></asp:RequiredFieldValidator>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="CEP é Obrigatório!"
                ValidationGroup="group_cadUnidade" ControlToValidate="tbxCEP" Display="None"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="O CEP deve conter 8 dígitos."
                ValidationExpression="^\d{8}$" Display="None" ControlToValidate="tbxCEP" ValidationGroup="group_cadUnidade"></asp:RegularExpressionValidator>
            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="CNES é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="tbxCNES" Display="None"></asp:RequiredFieldValidator>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="A Sigla é Obrigatória"
                ValidationGroup="group_cadUnidade" ControlToValidate="tbxSiglaEstabelecimento"
                Display="None"></asp:RequiredFieldValidator>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Município Gestor é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="ddlMunicipioGestor" Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>--%>
            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Bairro é Obrigatório!"
                        ValidationGroup="group_cadUnidade" ControlToValidate="DropDownList_Bairro" Display="None"
                        ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>--%>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Tipo de Estabelecimento é Obrigatório!"
                ValidationGroup="group_cadUnidade" ControlToValidate="ddltipoestabelecimento"
                Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
            <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Distrito é Obrigatório!" ValidationGroup="group_cadUnidade" ControlToValidate="ddlDistrito" Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_cadUnidade"
                ShowMessageBox="true" ShowSummary="false" />
        </div>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
