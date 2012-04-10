<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormVincularUsuarioSalaVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormVincularUsuarioSalaVacina" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Seguranca/WUCPesquisarUsuario.ascx" TagName="TagWUC_PesquisarUsuario"
    TagPrefix="WUCUsuario" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Vincular Usuário a Sala de Vacina
        </h2>
        <cc1:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="740px"
            ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Informações do Vínculo" ScrollBars="Horizontal">
                <ContentTemplate>
                    <WUCUsuario:TagWUC_PesquisarUsuario ID="WUC_PesquisarUsuario" runat="server"></WUCUsuario:TagWUC_PesquisarUsuario>
                    <asp:UpdatePanel ID="UpdatePanel_VinculoUsuarioSala" runat="server" UpdateMode="Always"
                        ChildrenAsTriggers="true" RenderMode="Block">
                        <ContentTemplate>
                            <asp:Panel ID="Panel_UsuarioSala" runat="server" Visible="false">
                                <fieldset>
                                    <legend>Vincular Usuário a Sala de Vacina</legend>
                                    <p>
                                        <span class="rotulo">Unidade</span><span>
                                            <asp:Label ID="Label_Unidade" runat="server" Text="" CssClass="campo"></asp:Label>
                                        </span>
                                        <br />
                                    </p>
                                    <p>
                                        <span class="rotulo">Usuário</span><span>
                                            <asp:Label ID="Label_Usuario" runat="server" Text="" CssClass="campo"></asp:Label>
                                        </span>
                                        <br />
                                    </p>
                                    <p>
                                        <span class="rotulo">Cartão SUS</span><span>
                                            <asp:Label ID="Label_CartaoSUSUsuario" runat="server" Text="" CssClass="campo"></asp:Label>
                                        </span>
                                        <br />
                                    </p>
                                    <p>
                                        <span class="rotulo">Data de Nascimento</span><span>
                                            <asp:Label ID="Label_DataNascimentoUsuario" runat="server" Text="" CssClass="campo"></asp:Label>
                                        </span>
                                        <br />
                                    </p>
                                    <br />
                                    <p>
                                        <span style="float: left">
                                            <p style="font-family: Arial; font-size: 13px; font-weight: bolder">
                                                Salas Disponíveis</p>
                                            <asp:ListBox ID="ListBox_SalasDisponiveis" SelectionMode="Multiple"
                                                runat="server" Width="250px" >
                                                </asp:ListBox>
                                        </span><span style="float: left; height: 140px">
                                            <div class="botoesroll" style="padding-top: 45px; margin-left: 16px">
                                                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="OnClick_InserirSala"
                                                    ValidationGroup="ValidationGroup_InserirSala">
                         <img id="imgadd" alt="Salvar" src="img/set_dir.png"
                onmouseover="imgadd.src='img/set_dir-on.png';"
                onmouseout="imgadd.src='img/set_dir.png';" />
                                                </asp:LinkButton>
                                            </div>
                                            <div class="botoesroll" style="padding-top: 45px">
                                                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="OnClick_RetirarSala"
                                                    ValidationGroup="ValidationGroup_RetirarSala">
                         <img id="imgexcl" alt="Retirar" src="img/set_esq.png"
                onmouseover="imgexcl.src='img/set_esq-on.png';"
                onmouseout="imgexcl.src='img/set_esq.png';" />
                                                </asp:LinkButton>
                                            </div>
                                        </span><span style="float: left">
                                            <p style="font-family: Arial; font-size: 13px; font-weight: bolder">
                                                Salas Vínculadas</p>
                                            <asp:ListBox ID="ListBox_SalasVinculadas"  SelectionMode="Multiple" runat="server" Width="250px"></asp:ListBox>
                                        </span>
                                    </p>
                                    <br />
                                    <p>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="LinkButton3" runat="server" OnClick="OnClick_SalvarVinculo">
                     <img id="imgsalvar1" alt="Salvar" src="img/btn_salvar1.png"
                onmouseover="imgsalvar1.src='img/btn_salvar2.png';"
                onmouseout="imgsalvar1.src='img/btn_salvar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="OnClick_CancelarVinculo">
                     <img id="imgcancelar" alt="Cancelar" src="img/btn_cancelar1.png"
                onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn_cancelar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                    </p>
                                    <p>
                                        <asp:CustomValidator ID="CustomValidator_InserirSala" runat="server" Display="None"
                                            ErrorMessage="Escolha pelo menos uma sala de vacina para vinculá-la ao usuário." Font-Size="XX-Small"
                                            OnServerValidate="OnServerValidate_InserirSala" ValidationGroup="ValidationGroup_InserirSala"></asp:CustomValidator>
                                        <asp:CustomValidator ID="CustomValidator_RetirarSala" runat="server" Display="None"
                                            ErrorMessage="Escolha pelo menos uma sala de vacina para desvinculá-la do usuário."
                                            Font-Size="XX-Small" OnServerValidate="OnServerValidate_RetirarSala" ValidationGroup="ValidationGroup_RetirarSala"></asp:CustomValidator>
                                    </p>
                                </fieldset>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
    </div>
</asp:Content>
