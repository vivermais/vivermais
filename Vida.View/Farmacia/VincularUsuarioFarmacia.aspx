<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="VincularUsuarioFarmacia.aspx.cs" Inherits="ViverMais.View.Farmacia.VincularUsuarioFarmacia"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Seguranca/WUCPesquisarUsuario.ascx" TagName="TagPesquisarUsuario"
    TagPrefix="WUCUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Vincular Usuário a Farmácia</h2>
        <%--   <cc1:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="740px"            ActiveTabIndex="0">            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Informações do Vínculo" ScrollBars="Horizontal">                <ContentTemplate>--%>
        <WUCUsuario:TagPesquisarUsuario ID="WUC_PesquisarUsuario" runat="server"></WUCUsuario:TagPesquisarUsuario>
        <asp:UpdatePanel ID="UpdatePanel_VinculoUsuarioFarmacia" runat="server" UpdateMode="Always"
            ChildrenAsTriggers="true" RenderMode="Block">
            <ContentTemplate>
                <asp:Panel ID="Panel_UsuarioFarmacia" runat="server" Visible="false">
                    <fieldset>
                        <legend>Vincular Usuário a Farmácia</legend>
                        <p>
                            <span class="rotulo">Unidade</span><span>
                                <asp:Label ID="Label_Unidade" runat="server" Text="" CssClass="campo-dados"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Usuário</span><span class="campo-dados">
                                <asp:Label ID="Label_Usuario" runat="server" Text=""></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Cartão SUS</span><span>
                                <asp:Label ID="Label_CartaoSUSUsuario" runat="server" Text="" CssClass="campo-dados"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Nascimento</span><span>
                                <asp:Label ID="Label_DataNascimentoUsuario" runat="server" Text="" CssClass="campo-dados"></asp:Label>
                            </span>
                            <br />
                        </p>
                        <p>
                            <span style="float: left">
                                <p style="font-family: Arial; font-size: 13px; font-weight: bolder">
                                    Farmácias Disponíveis</p>
                                <asp:ListBox ID="ListBox_FarmaciasDisponiveis" SelectionMode="Multiple" DataTextField="Nome"
                                    DataValueField="Codigo" runat="server" Width="250px"></asp:ListBox>
                            </span><span style="float: left; height: 140px">
                                <div class="botoesroll" style="padding-top: 45px; margin-left: 16px">
                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="OnClick_InserirFarmacia"
                                        ValidationGroup="ValidationGroup_InserirFarmacia">
                                        <img id="imgadd" alt="Salvar" src="img/btn/dir1.png" onmouseover="imgadd.src='img/btn/dir2.png';" onmouseout="imgadd.src='img/btn/dir1.png';" />
                                    </asp:LinkButton>
                                </div>
                                <div class="botoesroll" style="padding-top: 45px">
                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="OnClick_RetirarFarmacia"
                                        ValidationGroup="ValidationGroup_RetirarFarmacia">                         <img id="imgexcl" alt="Retirar" src="img/btn/esq1.png"                onmouseover="imgexcl.src='img/btn/esq2.png';"                onmouseout="imgexcl.src='img/btn/esq1.png';" />                                                </asp:LinkButton>
                                </div>
                            </span><span style="float: left">
                                <p style="font-family: Arial; font-size: 13px; font-weight: bolder">
                                    Farmácias Vínculadas</p>
                                <asp:ListBox ID="ListBox_FarmaciasVinculadas" SelectionMode="Multiple" DataTextField="Nome"
                                    DataValueField="Codigo" runat="server" Width="250px"></asp:ListBox>
                            </span>
                        </p>
                        <br />
                        <p>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="OnClick_SalvarVinculo">
                                                     <img id="imgsalvar1" alt="Salvar" src="img/btn/salvar1.png" onmouseover="imgsalvar1.src='img/btn/salvar2.png';" onmouseout="imgsalvar1.src='img/btn/salvar1.png';" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="OnClick_CancelarVinculo">
                                                     <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png" onmouseover="imgcancelar.src='img/btn/cancelar2.png';"                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                                </asp:LinkButton>
                            </div>
                        </p>
                        <p>
                            <asp:CustomValidator ID="CustomValidator_InserirFarmacia" runat="server" Display="None"
                                ErrorMessage="Escolha pelo menos uma farmácia para vinculá-la ao usuário." Font-Size="XX-Small"
                                OnServerValidate="OnServerValidate_InserirFarmacia" ValidationGroup="ValidationGroup_InserirFarmacia"></asp:CustomValidator>
                            <asp:CustomValidator ID="CustomValidator_RetirarFarmacia" runat="server" Display="None"
                                ErrorMessage="Escolha pelo menos uma farmácia para desvinculá-la do usuário."
                                Font-Size="XX-Small" OnServerValidate="OnServerValidate_RetirarFarmacia" ValidationGroup="ValidationGroup_RetirarFarmacia"></asp:CustomValidator>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <%--      
        </ContentTemplate>
                    </cc1:TabPanel>
                            </cc1:TabContainer>--%>
    </div>
</asp:Content>
