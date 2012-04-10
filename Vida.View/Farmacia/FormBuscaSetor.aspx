<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormBuscaSetor.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormBuscaSetor" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/Farmacia/Inc_AssociarSetorUnidade.ascx" TagName="IncAssociarSetorUnidade"
    TagPrefix="IASU" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 670px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 2px 2px 2px 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
    <div id="top">
        <h2>
            Setores
        </h2>
        <%--    <p>
                <span class="rotulo">Setor</span>
                <span style="margin-left:5px;">
                    <asp:TextBox ID="TextBox_Setor" CssClass="campo" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span>
                    <asp:Button ID="Button_Pesquisar" runat="server" Text="Pesquisar" CommandArgument="alguns" OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_Pesquisa"/>
                    <asp:Button ID="Button_ListarTodos" runat="server" Text="Listar Todos" CommandArgument="todos" OnClick="OnClick_Pesquisar" />
                </span>
            </p>
            <p>
                <span>
                    <asp:Button ID="Button_AssociarSetor" runat="server" Text="Setores da Unidade" PostBackUrl="~/Farmacia/FormAssociarSetorUnidade.aspx" />
                    <asp:Button ID="Button_Novo" runat="server" Text="Novo Setor" PostBackUrl="~/Farmacia/FormSetor.aspx" />
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="xx-small" runat="server" ErrorMessage="Setor é Obrigatório!" ControlToValidate="TextBox_Setor" Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="xx-small" runat="server" ErrorMessage="Setor deve iniciar com pelo menos três caracteres!" ControlToValidate="TextBox_Setor" ValidationExpression="^[\S]{3}[\w\W]*$" Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummary1" Font-Size="xx-small" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="ValidationGroup_Pesquisa" />
            </p>
        </fieldset>--%>
        <%--<asp:Panel ID="Panel_Resultado" runat="server" Visible="false">--%>
        <fieldset class="formulario">
            <legend>Setores/Unidades</legend>
            <p>
                <span>
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1">
                        <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Cadastrados">
                            <ContentTemplate>
                                <fieldset class="formulario2">
                                    <legend>Relação</legend>
                                    <p>
                                        <p><h4>Pressione no botão abaixo para cadastrar um novo setor.</h4></p>
                                        
                                            <asp:LinkButton ID="Button_Novo" runat="server" Text="Novo Registro" PostBackUrl="~/Farmacia/FormSetor.aspx" >
                                             <img id="imgnovoreg" alt="Novo Registro" src="img/btn/novoregistro1.png"
                                                               onmouseover="imgnovoreg.src='img/btn/novoregistro2.png';"
                                                               onmouseout="imgnovoreg.src='img/btn/novoregistro1.png';" />
                                            </asp:LinkButton>
                                        
                                    </p>
                                    <p>
                                        <span>
                                            <asp:GridView ID="GridView_Setor" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                                                OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" Width="100%" Font-Size="X-Small">
                                                <Columns>
                                                    <asp:HyperLinkField HeaderText="Setor" DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Farmacia/FormSetor.aspx?co_setor={0}"
                                                        DataTextField="Nome" />
                                                </Columns>
                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                </EmptyDataTemplate>
                                                 <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                            </asp:GridView>
                                        </span>
                                    </p>
                                </fieldset>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Unidade">
                            <ContentTemplate>
                                <fieldset class="formulario2">
                                    <legend>Relação </legend>
                                    <IASU:IncAssociarSetorUnidade ID="inc_associar" runat="server" />
                                </fieldset>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </span>
            </p>
        </fieldset>
        <%--</asp:Panel>--%>
    </div>
    <%--            </ContentTemplate>
</asp:UpdatePanel>--%>
</asp:Content>
