<%@ Page Language="C#" MasterPageFile="~/Seguranca/MasterSeguranca.Master" AutoEventWireup="true"
    CodeBehind="FormUsuario.aspx.cs" Inherits="ViverMais.View.Seguranca.FormUsuario" Title="Untitled Page"
    EnableEventValidation="false" %>

<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="WUC_PesquisarEstabelecimento"
    TagPrefix="WUC_EAS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Usuário
        </h2>
        <cc1:TabContainer ID="TabContainer2" runat="server" Width="740px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Dados do Usuário">
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Informações Gerais</legend>
                        <p>
                            <span class="rotulo">Cartão SUS</span> <span>
                                <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" MaxLength="15" runat="server"></asp:TextBox>
                            </span><span>
                                <asp:ImageButton ID="imgBtnPesquisarCartao" runat="server" OnClick="imgBtnPesquisarCartao_Click"
                                    ImageUrl="~/Seguranca/img/procurar.png" Width="16px" Height="16px" Style="position: absolute;
                                    margin-top: 3px;" ValidationGroup="ValidationGroup_pesquisaUsuario" />
                            </span>
                        </p>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgBtnPesquisarCartao" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Nome Completo</span> <span>
                                        <asp:Label ID="lblNome" runat="server" Text="-" Font-Bold="true"></asp:Label>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <span class="rotulo">Senha</span> <span>
                                <asp:TextBox ID="tbxSenha" MaxLength="15" CssClass="campo" TextMode="Password" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Confirmar Senha</span> <span>
                                <asp:TextBox ID="tbxConfirmaSenha" CssClass="campo" MaxLength="15" TextMode="Password"
                                    runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Ativo</span> <span class="camporadio">
                                <asp:RadioButton ID="RadioButton_Ativo" runat="server" GroupName="GroupName_Ativo"
                                    Checked="true" />Sim
                                <asp:RadioButton ID="RadioButton_Inativo" runat="server" GroupName="GroupName_Ativo" />Não
                            </span>
                        </p>
                        <%--<fieldset class="formulario" style="width:;">--%>
                        <%--<legend>Pesquisar Unidade</legend>--%>
                        <WUC_EAS:WUC_PesquisarEstabelecimento ID="EAS" runat="server"></WUC_EAS:WUC_PesquisarEstabelecimento>
                        <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                            RenderMode="Inline" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Unidade</span> <span>
                                        <asp:DropDownList ID="DropDownList_Unidade" CssClass="campo" Height="21px" runat="server"
                                            DataTextField="NomeFantasia" DataValueField="CNES" Width="380px"
                                            AutoPostBack="true" OnSelectedIndexChanged="DropDownList_Unidade_OnSelectedIndexChanged">
                                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            &nbsp;</p>
                        <asp:UpdatePanel ID="UpdatePanel_Profissional" runat="server" ChildrenAsTriggers="true"
                            RenderMode="Inline" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DropDownList_Unidade" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelProfissionalSaude" runat="server" Visible="false">
                                    <span style="font-size: x-small;">Para Listar os Profissionais, Informe a Unidade acima.</span>
                                    <span class="rotulo">Profissionais</span>
                                    <p>
                                        <asp:DropDownList ID="ddlProfissionais" runat="server" CssClass="drop" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlProfissionais_SelectedIndexChanged" DataValueField="CPF"
                                            DataTextField="Nome">
                                            <asp:ListItem Value="0" Text="Selecione..."></asp:ListItem>
                                        </asp:DropDownList>
                                    </p>
                                    <asp:RequiredFieldValidator ID="RequiredFieldProfissional" runat="server" Display="None"
                                        ErrorMessage="Selecione um Profissional" InitialValue="0" ControlToValidate="ddlProfissionais"
                                        ValidationGroup="ValidationGroup_cadUsuario"></asp:RequiredFieldValidator>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanelDadosProfissional" runat="server" UpdateMode="Conditional"
                            RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlProfissionais" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelDadosProfissional" runat="server" Visible="false">
                                    <p>
                                        <span class="rotulo">Nome do Profissional</span> <span>
                                            <asp:Label ID="lblNomeProfissional" runat="server" Font-Bold="true"></asp:Label></span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data Nascimento</span><span><asp:Label ID="lblDataNascimento"
                                            runat="server" Font-Bold="true"></asp:Label></span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Especialidades</span><span><asp:Label ID="lblEspecialidades"
                                            runat="server" Font-Bold="true"></asp:Label></span>
                                    </p>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </fieldset>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCartaoSUS"
                        Display="None" ErrorMessage="O Número do Cartão SUS é Obrigatório." ValidationGroup="ValidationGroup_pesquisaUsuario"
                        Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="tbxCartaoSUS"
                        Display="None" ErrorMessage="O Cartão SUS deve conter 15 dígitos." Font-Size="XX-Small"
                        ValidationExpression="^\d{15}$" ValidationGroup="ValidationGroup_pesquisaUsuario"></asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_pesquisaUsuario" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxCartaoSUS"
                        Display="None" ErrorMessage="O Número do Cartão SUS é Obrigatório." ValidationGroup="ValidationGroup_cadUsuario"
                        Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="tbxCartaoSUS"
                        Display="None" ErrorMessage="O Cartão SUS deve conter 15 dígitos." Font-Size="XX-Small"
                        ValidationExpression="^\d{15}$" ValidationGroup="ValidationGroup_cadUsuario"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="tbxSenha"
                        Display="None" ErrorMessage="A Senha é obrigatória." ValidationGroup="ValidationGroup_cadUsuario"
                        Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tbxSenha"
                        Display="None" ErrorMessage="A Senha deve possuir no mínimo 6 caraceteres e no máximo 15, podendo conter letras, números e caracteres especiais sem espaços."
                        ValidationExpression="^\S{6,15}$" ValidationGroup="ValidationGroup_cadUsuario"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxConfirmaSenha"
                        Display="None" ErrorMessage="Confirme a senha!" ValidationGroup="ValidationGroup_cadUsuario"
                        Font-Size="X-Small"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="DropDownList_Unidade"
                        Display="None" ErrorMessage="Selecione uma unidade!" Operator="GreaterThan" ValidationGroup="ValidationGroup_cadUsuario"
                        ValueToCompare="0" Font-Size="X-Small"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="tbxConfirmaSenha"
                        ControlToValidate="tbxSenha" Display="None" ErrorMessage="A senha não confere!"
                        Operator="Equal" ValidationGroup="ValidationGroup_cadUsuario" Font-Size="X-Small"></asp:CompareValidator>
                </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Perfis">
                    <contenttemplate>
                    <fieldset class="formulario">
                        <legend>Perfis do Usuário</legend>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button_Salvar1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="GridView_Perfil" EventName="RowDeleting" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Sistema</span> <span>
                                        <asp:DropDownList ID="DropDownList_Sistema" runat="server" CssClass="drop" Width="250px"
                                            OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaPerfis"
                                             DataValueField="Codigo" DataTextField="Nome"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
<%--                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="Button_Salvar1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DropDownList_Sistema" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>--%>
                                <p>
                                    <span class="rotulo">Perfil</span> <span>
                                        <asp:DropDownList ID="DropDownList_Perfil" CssClass="drop" runat="server" Width="250px">
                                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                </asp:UpdatePanel>
                <div class="botoesroll">
                    <asp:LinkButton ID="Button_Salvar1" runat="server" OnClick="OnClick_SalvarPerfil"
                        ValidationGroup="ValidationGroup_cadPerfil">
                                                      <img id="imgadicionar" alt="Adicionar" src="img/btn_adicionar1.png"
                                                      onmouseover="imgadicionar.src='img/btn_adicionar2.png';"
                                                      onmouseout="imgadicionar.src='img/btn_adicionar1.png';" /></asp:LinkButton>
                </div>
                <br />
                <p>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um perfil!"
                        ControlToValidate="DropDownList_Perfil" Operator="GreaterThan" ValueToCompare="0"
                        ValidationGroup="ValidationGroup_cadPerfil" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_cadPerfil" />
                </p>
                <br />
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                    RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button_Salvar1" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            <asp:GridView ID="GridView_Perfil" AutoGenerateColumns="false" DataKeyNames="Codigo"
                                OnRowDeleting="OnRowDeleting_Perfil" runat="server" GridLines="Vertical" BackColor="White"
                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" Width="100%">
                                <Columns>
                                    <asp:BoundField HeaderText="Módulo" DataField="Modulo" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Perfil" DataField="Nome" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField ButtonType="Link" Text="<img src='img/excluir_gridview.png' alt='Excluir Perfil?' />"
                                        CausesValidation="true" ControlStyle-Height="15px" ControlStyle-Width="15px"
                                        CommandName="Delete" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum perfil incluído." Font-Bold="true"></asp:Label>
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <HeaderStyle CssClass="tab" BackColor="#5b5b5b" Font-Bold="True" ForeColor="White"
                                    Font-Size="13px" Font-Names="Verdana" Height="27px" />
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle CssClass="tabrow_left" BackColor="#EEEEEE" ForeColor="Black" Height="23px"
                                    Font-Names="Verdana" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </fieldset> </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <br />
        <div class="botoesroll">
            <asp:LinkButton ID="lnkSalvar" runat="server" CausesValidation="true" OnClick="lnkSalvar_Click"
                ValidationGroup="ValidationGroup_cadUsuario">
                <img id="imgsalvar" alt="Salvar" src="img/btn_salvar_1.png"
                onmouseover="imgsalvar.src='img/btn_salvar_2.png';"
                onmouseout="imgsalvar.src='img/btn_salvar_1.png';" /></asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="LinkButtonVoltar" runat="server">
                            <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                            onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                onmouseout="imgvoltar.src='img/btn_voltar1.png';"  />
            </asp:LinkButton>
        </div>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="ValidationGroup_cadUsuario" />
    </div>
</asp:Content>
