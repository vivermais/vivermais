<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="ViverMais.View.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/Seguranca/WUCAlterarSenha.ascx" TagName="TagAlterarSenha" TagPrefix="WUCTrocarSenha" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta name="description" content="description" />
    <meta name="keywords" content="keywords" />
    <meta name="author" content="author" />
    <title>ViverMais - Sistema Integrado em Saúde Pública</title>
    <link rel="stylesheet" href="style_ViverMais.css" type="text/css" media="screen" />
    <link href="GreyBox/gb_styles.css" rel="stylesheet" type="text/css" />

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

</head>
<body>
    <form id="form1" runat="server">
    <% 
        string url = Request.Url.ToString();
        url = url.Substring(0, url.LastIndexOf("Index.aspx"));
    %>

    <script type="text/javascript">
        var GB_ROOT_DIR = '<%=url %>GreyBox/';
    </script>

    <script type="text/javascript" src="GreyBox/AJS.js"></script>

    <script type="text/javascript" src="GreyBox/AJS_fx.js"></script>

    <script type="text/javascript" src="GreyBox/gb_scripts.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true"
        EnableScriptGlobalization="true" LoadScriptsBeforeUI="false" ScriptMode="Release">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="imgBtnLogin" />
            <asp:PostBackTrigger ControlID="imgBtnCartao" />
        </Triggers>
        <ContentTemplate>
           
                <div id="container">
                    <div class="divrow">
                        <div class="divcell">
                            <img src="images/urgence_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img01_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img09_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img12_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/nutrinet_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img10_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img06_o.jpg" alt="" />
                        </div>
                    </div>
                    <div class="divrow">
                        <div class="divcell">
                            <img src="images/img13_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/sisfarma_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img07_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/notifique_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img05_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/cygnus_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img14_o.jpg" alt="" />
                        </div>
                    </div>
                   <div class="divrow">
                      <!--   <div class="homologa2">
                        </div> -->
                        <div class="divlogin" style="background: url('images/bkg_log.jpg') no-repeat; width: 1001px;">
                            <%--<asp:Login ID="LoginViverMais" runat="server" DisplayRememberMe="False" FailureText="Usuário ou senha inválidos."
                            LoginButtonText="Entrar" OnAuthenticate="LoginViverMais_Authenticate" PasswordLabelText="Senha:"
                            TitleText="acesse aqui" UserNameLabelText="Usuário:" VisibleWhenLoggedIn="False"
                            BorderPadding="10" Font-Size="Small" ForeColor="#00164D" LoginButtonImageUrl="~/img/bts/bt_entrar.png" LoginButtonStyle-Width="81px" LoginButtonStyle-Height="30px"
                            LoginButtonType="Image" Font-Names="Arial" Width="241px">
                            <TextBoxStyle BackColor="White" BorderStyle="None" Width="150px" />
                            <LoginButtonStyle Font-Size="Small" />
                            <FailureTextStyle Font-Size="XX-Small" />
                            <TitleTextStyle Font-Names="Verdana" Font-Size="24px" ForeColor="#02223E" HorizontalAlign="Right"
                                Wrap="True" />
                        </asp:Login>--%>
                        </div>
                    </div>
                    <div class="divrow">
                        <div class="divcell">
                            <img src="images/sms_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img08_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/siaps_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img04_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/sacs_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/img11_o.jpg" alt="" />
                        </div>
                        <div class="divcell">
                            <img src="images/ngi_o.jpg" alt="" />
                        </div>
                    </div>
                    <div class="diventer" style="background: url('images/bkg_enter.png') no-repeat; width: 340px;">
                        <div class="logon">
                            <asp:Panel ID="Panel1" DefaultButton="imgBtnLogin" runat="server">
                                <p>
                                    <span class="rotulolog">Cartão SUS</span> <span style="margin-left: 5px;">
                                        <asp:TextBox ID="tbxCartaoSUS" runat="server" CssClass="campolog" MaxLength="15"></asp:TextBox></span>
                                    <asp:ImageButton ID="imgBtnCartao" runat="server" OnClick="imgBtnCartao_Click" Width="49px"
                                        ValidationGroup="ValidationGroup_BuscarUsuarioCartao" Height="21px" ImageUrl="~/images/btn-buscar1.png"
                                        Style="position: absolute;" TabIndex="2" />
                                </p>
                                <p>
                                    <span class="rotulolog">CNES</span> <span style="margin-left: 5px;">
                                        <%--<asp:TextBox ID="tbxCNES" runat="server" AutoPostBack="True" CssClass="campolog"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlCNES" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCNES_SelectedIndexChanged"
                                            BackColor="#0F2D3F" Width="148px" ForeColor="#ffffff" Font-Size="12px" Style="margin-right: 5px;">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<asp:ImageButton ID="imgBtnCNES" CausesValidation="false" runat="server" OnClick="imgBtnCNES_Click" Width="21px"
                                            Height="21px" ImageUrl="~/images/search.gif" style="position:absolute;" TabIndex="3" />--%>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulolog">Usuário</span> <span style="margin-left: 5px;">
                                        <asp:DropDownList ID="ddlUsuario" runat="server" BackColor="#0F2D3F" AutoPostBack="true"
                                            OnSelectedIndexChanged="OnSelectedIndexChanged_SelecionaCNES" Width="148px" ForeColor="#ffffff"
                                            Font-Size="12px">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulolog">Senha</span> <span style="margin-left: 5px;">
                                        <asp:TextBox ID="tbxSenha" runat="server" MaxLength="20" CssClass="campolog" TextMode="Password"></asp:TextBox>
                                    </span>
                                </p>
                                <p style="margin-left: 86px;">
                                    <asp:ImageButton ID="imgBtnLogin" runat="server" OnClick="imgBtnLogin_Click" ImageUrl="~/images/btn-entrar1.png"
                                        Width="74px" Height="19px" ValidationGroup="ValidationGroup_LogarSistema" />
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_LimparCampos">
                                        <asp:Image ID="Limpar" runat="server" ImageUrl="~/images/btn-limpar1.png" />
                                    </asp:LinkButton><br />
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="javascript:GB_show('Pesquisar Cartão SUS','../FormLembrarCartaoSUS.aspx',500,900);"
                                        CausesValidation="false">
                                        <asp:Image ID="Esqueci" runat="server" ImageUrl="~/images/btn-esqueci1.png" />
                                    </asp:LinkButton>
                                    <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="javascript:window.open('FormLembrarCartaoSUS.aspx');"
                                        CausesValidation="false">
                                        <asp:Image ID="Esqueci" runat="server" ImageUrl="~/images/btn-esqueci1.png" />
                                    </asp:LinkButton>--%>
                                    <p style="margin-left: 96px;">
                                        <%--                                    <asp:ImageButton ID="imgBtnLogin" runat="server" OnClick="imgBtnLogin_Click" ImageUrl="~/images/enter.gif"
                                        Width="77px" Height="25px" ValidationGroup="ValidationGroup_LogarSistema" />--%>
                                        <%--<asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_LimparCampos">Limpar</asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="LinkButton_PesquisarCartao" runat="server" OnClientClick="javascript:window.open('FormLembrarCartaoSUS.aspx');" CausesValidation="false">Cartão SUS</asp:LinkButton>--%>
                                        <%--<asp:LinkButton ID="LinkButton_PesquisarCartao" runat="server" OnClick="OnClick_PesquisarCartao">Cartão SUS</asp:LinkButton>--%>
                                    </p>
                            </asp:Panel>
                        </div>
                    </div>
                    <div class="divfoot">
                    </div>
                </div>
            
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxCartaoSUS"
                ValidationGroup="ValidationGroup_BuscarUsuarioCartao" Display="None" ErrorMessage="Digite o número do Cartão SUS."
                Font-Size="XX-Small" Style="margin-left: 95px;"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O Cartão SUS deve conter 15 dígitos."
                ValidationGroup="ValidationGroup_BuscarUsuarioCartao" Display="None" ControlToValidate="tbxCartaoSUS"
                ValidationExpression="^\d{15}$"></asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ValidationGroup="ValidationGroup_BuscarUsuarioCartao" ShowSummary="false" />
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Escolha um usuário!"
                Display="None" ControlToValidate="ddlUsuario" Operator="GreaterThan" ValueToCompare="-1"
                ValidationGroup="ValidationGroup_LogarSistema"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxSenha"
                ValidationGroup="ValidationGroup_LogarSistema" Display="None" ErrorMessage="Senha é Obrigatório!"
                Font-Size="XX-Small" Style="margin-left: 95px;"></asp:RequiredFieldValidator>
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                ValidationGroup="ValidationGroup_LogarSistema" ShowSummary="false" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
     RenderMode="Block">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelMensagem" Visible="false">
                <div id="cinza" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;
                    z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                    moz-opacity: 0.3; opacity: 0.3" visible="false">
                </div>
                <div id="mensagem" style="position: fixed; top: 50%; margin-top: -271px; left: 50%; top:50%;
                    margin-left: -312px; width: 623px; height:542px; z-index: 102;  background: url(   'img/msg-ViverMais-carnaval.png' ) no-repeat left; 
                    padding-right: 20px; padding-left: 20px; padding-bottom: 20px; color: #c5d4df;
                    padding-top: 10px; text-align: justify; font-family: Verdana;" visible="false">
                    <p style="height: 10px;">
                        <span style="float: right;margin-top:66px; margin-right:85px";>
                            <asp:ImageButton ID="btnFechar" runat="server" Height="42px" ImageAlign="Left" ImageUrl="img/fechar-carna.png"
                                Width="42px" OnClick="OnClick_btnFechar" />
                        </span>
                    </p>
                    
                 
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>

    <script language="javascript" type="text/javascript">

        function disposeTree(sender, args) {
            var elements = args.get_panelsUpdating();
            for (var i = elements.length - 1; i >= 0; i--) {
                var element = elements[i];
                var allnodes = element.getElementsByTagName('*'),
                length = allnodes.length;
                var nodes = new Array(length)
                for (var k = 0; k < length; k++) {
                    nodes[k] = allnodes[k];
                }
                for (var j = 0, l = nodes.length; j < l; j++) {
                    var node = nodes[j];
                    if (node.nodeType === 1) {
                        if (node.dispose && typeof (node.dispose) === "function") {
                            node.dispose();
                        }
                        else if (node.control && typeof (node.control.dispose) === "function") {
                            node.control.dispose();
                        }

                        var behaviors = node._behaviors;
                        if (behaviors) {
                            behaviors = Array.apply(null, behaviors);
                            for (var k = behaviors.length - 1; k >= 0; k--) {
                                behaviors[k].dispose();
                            }
                        }
                    }
                }
                element.innerHTML = "";
            }
        }


        Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(disposeTree); 

    </script>

</body>
</html>
