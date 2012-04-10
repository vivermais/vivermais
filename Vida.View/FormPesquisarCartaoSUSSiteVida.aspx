<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPesquisarCartaoSUSSiteVida.aspx.cs"
    Inherits="Vida.View.FormPesquisarCartaoSUSSiteVida" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="style_popup.css" type="text/css" media="screen" />
    <script type="text/javascript" language="javascript" src="JavaScript/MascarasGerais.js"></script>
    <%--<link rel="stylesheet" href="style_vida.css" type="text/css" media="screen" />--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender2"
        TargetControlID="UpDateProgressLembrar" VerticalSide="Middle" VerticalOffset="10"
        HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <asp:UpdateProgress ID="UpDateProgressLembrar" runat="server" DisplayAfter="1" DynamicLayout="false">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <div id="bgloader">
                    <img src="img/ajax-loadernn.gif" style="margin-left: 68px; margin-top: 45px;" alt="" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div>
        <fieldset class="formulario">
            <legend class="img-consulta-nome"></legend>
            <p>
                <span class="rotulo">CPF</span> <span>
                    <asp:TextBox ID="TextBox_CPF" runat="server" CssClass="campo"></asp:TextBox></span>
            </p>
            <p>
                <span class="rotulo">Nome completo</span> <span>
                    <asp:TextBox ID="TextBox_Nome" runat="server" CssClass="campo"></asp:TextBox></span>
            </p>
            <p>
                <span class="rotulo">Nome da Mãe Completo</span> <span>
                    <asp:TextBox ID="TextBox_NomeMae" runat="server" CssClass="campo"></asp:TextBox></span>
            </p>            
            <p>
                <span class="rotulo">Data de Nascimento</span> <span>
                    <asp:TextBox ID="TextBox_DataNascimento" MaxLength="10" runat="server" CssClass="campo" OnKeyPress="return(formataData(this,event));"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" TargetControlID="TextBox_DataNascimento"
                        Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                    </cc1:CalendarExtender>
                </span>
            </p>
            <br />
            <p style="padding-left: 51px;">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_PesquisarUsuario"
                    ValidationGroup="ValidationGroup_PesquisarUsuario">
                    <asp:Image ID="Pesquisar" runat="server" ImageUrl="~/images/btn-pesquisar.png" AlternateText="Pesquisar" />
                </asp:LinkButton>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="parent.parent.GB_hide();"
                    CausesValidation="false">
                    <asp:Image ID="Fechar" runat="server" ImageUrl="~/images/btn-fechar.png" AlternateText="Fechar" />
                </asp:LinkButton>
            </p>
            <br />
            <br />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome e Sobrenome é Obrigatório."
                ValidationGroup="ValidationGroup_PesquisarUsuario" Display="None" ControlToValidate="TextBox_Nome"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Data de Nascimento é Obrigatório."
                ValidationGroup="ValidationGroup_PesquisarUsuario" Display="None" ControlToValidate="TextBox_DataNascimento"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Nascimento inválida."
                ControlToValidate="TextBox_DataNascimento" Display="None" ValidationGroup="ValidationGroup_PesquisarUsuario"
                Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Nascimento deve ser igual ou maior que 01/01/1900."
                ControlToValidate="TextBox_DataNascimento" Display="None" ValidationGroup="ValidationGroup_PesquisarUsuario"
                Operator="GreaterThanEqual" ValueToCompare="01/01/1900" Type="Date"></asp:CompareValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarUsuario" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_ResultadoBusca" runat="server" Visible="false">
                        <asp:GridView ID="GridView_Usuarios" runat="server" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                            PageSize="20" PagerSettings-Mode="Numeric" AutoGenerateColumns="False" DataKeyNames="Codigo"
                            CellPadding="4" ForeColor="#ffffff" GridLines="None" Width="700px">
                            <RowStyle BackColor="#5f7d8f" Font-Size="10px" />
                            <Columns>
                                <asp:BoundField DataField="Nome" HeaderText="Nome" ItemStyle-HorizontalAlign="Left"
                                    ItemStyle-Font-Bold="true" />
                                <asp:BoundField DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-HorizontalAlign="Center"
                                    ItemStyle-VerticalAlign="Middle" HeaderText="Data de Nascimento" ItemStyle-Width="70px" />
                                <asp:BoundField DataField="CartaoSUS" HeaderText="Cartão SUS" ItemStyle-HorizontalAlign="Center" />
                                <asp:TemplateField HeaderText="Unidade" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="link" runat="server" Text='<%#bind("UnidadeToString") %>' CommandArgument='<%#bind("Codigo") %>'
                                            OnClick="OnClick_RetornaPesquisa" CausesValidation="true"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:HyperLinkField DataTextField="UnidadeToString" ControlStyle-ForeColor="Blue" ItemStyle-HorizontalAlign="Center"
DataNavigateUrlFields="Codigo" HeaderText="Unidade"
DataNavigateUrlFormatString="~/Index.aspx?co_usuario={0}" />--%>
                            </Columns>
                            <FooterStyle BackColor="#0f2d3f" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Não foi encontrado usuário algum com os dados informados."></asp:Label>
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#0f2d3f" Font-Bold="True" ForeColor="White" Font-Size="11px" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="#446a80" />
                        </asp:GridView>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    </form>
</body>
</html>
