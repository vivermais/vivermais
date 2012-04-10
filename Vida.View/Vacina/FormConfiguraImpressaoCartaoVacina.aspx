<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormConfiguraImpressaoCartaoVacina.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormConfiguraImpressaoCartaoVacina" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>ViverMais - Impressão do Cartão de Vacina</title>
    <link rel="stylesheet" href="style_form_vacina.css" type="text/css" media="screen" />
    <%-- <link href="GreyBox/gb_styles.css" rel="stylesheet" type="text/css" />
<%
string url = Request.Url.ToString();
url = url.Substring(0, url.LastIndexOf(char.Parse("/")));
%>

<script type="text/javascript">
var GB_ROOT_DIR = '<%=url %>/GreyBox/';
</script>

<script type="text/javascript" src="GreyBox/AJS.js"></script>

<script type="text/javascript" src="GreyBox/AJS_fx.js"></script>

<script type="text/javascript" src="GreyBox/gb_scripts.js"></script>--%>
    <style type="text/css">
        .Animation2
        {
            display: none;
            position: absolute;
            width: 1px;
            height: 1px;
            left: 280px;
            top: 20px;
            padding: 3px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 482px;">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender2"
        TargetControlID="UpDateProgressVacina" VerticalSide="Middle" VerticalOffset="10"
        HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <div>
    <asp:UpdateProgress ID="UpDateProgressVacina" runat="server" DisplayAfter="1" DynamicLayout="false">
        <ProgressTemplate>
            <div id="progressBackgroundFilter">
            </div>
            <div id="processMessage">
                <div id="bgloader">
                    <img alt="" src="img/ajax-loadernn.gif" style="margin-left: 68px; margin-top: 45px;" />
                </div>
            </div>
            <%-- <div id="bgloader">
                <img src="img/ajax-loadernn.gif" style="margin-left: 68px; margin-top: 45px;" />
            </div>--%>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <%--</div>--%>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" RenderMode="Inline">
        <Triggers>
            <asp:PostBackTrigger ControlID="LnkImprimir" />
        </Triggers>
       <%-- <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LinkButton_EscolherTema" EventName="Click" />
        </Triggers>--%>
        <ContentTemplate>
            <div>
                <asp:Panel ID="Panel_TemaCartao" runat="server" Visible="false">
                    <div visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 0; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="layouts">
                        <div class="titulothumbs">
                            <div style="margin-left: 70px; margin-top: 16px">
                                Selecione o seu Tema
                            </div>
                        </div>
                        <br />
                        <asp:DataList ID="DataList_Thumb" runat="server" RepeatColumns="3">
                            <ItemTemplate>
                                <div style="padding-bottom: 30px; margin-bottom: 13px">
                                    <asp:ImageButton ID="imgpadrao" ImageUrl='<%#bind("ImagemThumb") %>' runat="server"
                                        CssClass="thumbscard" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_VisualizarImagemPrincipal" />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                        <br />
                        <div>
                            <img src="img/thumbs/line.png" /></div>
                        <br />
                        <div id="view">
                            <asp:Image ID="Image_ImagemPrincipalAvatar" runat="server" CssClass="ImagemPrincipalCartaoVacina"
                                AlternateText="" />
                        </div>
                        <br />
                        <div>
                            <img alt="" src="img/thumbs/line.png" /></div>
                        <br />
                        <div class="botoesroll" style="margin-left: 180px">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_SelecionaAvatar"
                                BorderStyle="None">
<img id="imgsalvar" alt="Salvar" src="img/aplicar1.png"
onmouseover="imgsalvar.src='img/aplicar2.png';"
onmouseout="imgsalvar.src='img/aplicar1.png';" />
                            </asp:LinkButton></div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_CancelarEscolhaTema"
                                BorderStyle="None">
<img id="imgcancelar" alt="Cancelar" src="img/cancelar1.png"
onmouseover="imgcancelar.src='img/cancelar2.png';"
onmouseout="imgcancelar.src='img/cancelar1.png';" />
                            </asp:LinkButton></div>
                    </div>
                </asp:Panel>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="LinkButton_EscolherTema" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="DropDownCartaoVacina" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>--%>
            <asp:Table ID="ImpressaoCartao_Vacina" runat="server" Height="778px" Width="389px"
                HorizontalAlign="Left" CellPadding="0" CellSpacing="0">
                <asp:TableRow VerticalAlign="Top">
                    <asp:TableCell>
                        <asp:Table ID="Cartao_Vacina" runat="server" Height="" Width="389px" CellPadding="0"
                            CellSpacing="0">
                            <asp:TableRow VerticalAlign="Top" HorizontalAlign="Center" Width="389px" Height="22px"
                                Style="background-repeat: no-repeat;">
                                <asp:TableCell VerticalAlign="Top" HorizontalAlign="Left">
                                    <asp:Image ID="ImagemTopo" runat="server" />
                                    <%--<asp:Image ID="Image_TipoCartao" runat="server" />--%>
                                </asp:TableCell>
                                <%-- <asp:TableCell Width="10px">
</asp:TableCell>--%>
                            </asp:TableRow>
                            <asp:TableHeaderRow>
                                <asp:TableCell VerticalAlign="Top" Height="33px">
                                    <asp:Table ID="Table_Cabecalho" Width="482px" Height="33px" runat="server" HorizontalAlign="Center"
                                        Font-Names="Verdana" Font-Size="11px" CellPadding="0" CellSpacing="0">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Table ID="nonono" runat="server" Height="33px" Width="482px" CellPadding="0"
                                                    CellSpacing="0">
                                                    <asp:TableRow Width="482px" BorderColor="#000000" BorderWidth="1px" BorderStyle="Solid">
                                                        <asp:TableCell Height="33px" Width="53px" BackColor="#000000" BorderStyle="Solid"
                                                            BorderColor="#000000" BorderWidth="1px">
                                                            <asp:Image ID="nomecartao" runat="server" ImageUrl="~/Vacina/img/CartaoVacina/Temas/nome_cartao_vacina.png" />
                                                        </asp:TableCell>
                                                        <asp:TableCell BackColor="#ffffff" BorderColor="#000000" BorderStyle="Solid" BorderWidth="1px">
                                                            <p style="padding-left: 10px; font-weight: bold;">
                                                                <asp:Label ID="Label_Paciente" runat="server" Text=""></asp:Label></p>
                                                        </asp:TableCell>
                                                    </asp:TableRow>
                                                </asp:Table>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                            <asp:TableHeaderRow VerticalAlign="Top">
                                <asp:TableCell VerticalAlign="Top">
                                    <asp:Table ID="dadosvacina" Width="389px" Height="33px" runat="server" HorizontalAlign="Center"
                                        Font-Names="Verdana" Font-Size="11px" CellPadding="0" CellSpacing="0">
                                        <asp:TableRow HorizontalAlign="Center" Width="389px" Height="34px" Style="background-repeat: no-repeat;"
                                            CellPadding="0" CellSpacing="0">
                                            <asp:TableCell Height="33px" Width="389px" BackColor="#000000">
                                                <asp:Image ID="img_dadosvacina" runat="server" />
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:GridView ID="GridView_CartaoVacina" runat="server" AutoGenerateColumns="false"
                                                    Width="482px" ShowHeader="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="VacinaImpressaoCartao" ItemStyle-Font-Size="7px" ItemStyle-Width="78px"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="DoseVacinaImpressaoCartao" ItemStyle-Font-Size="7px" ItemStyle-Width="39px"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" ItemStyle-Font-Size="7px"
                                                            ItemStyle-Width="54px" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Font-Size="7px"
                                                            ItemStyle-Width="54px" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" ItemStyle-Font-Size="7px" ItemStyle-Width="45px"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ValidadeLoteImpressaoCartao" ItemStyle-Font-Size="7px"
                                                            ItemStyle-Width="53px" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CNESImpressaoCartao" ItemStyle-Font-Size="7px" ItemStyle-Width="37px"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" ItemStyle-Font-Size="7px"
                                                            ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </asp:TableCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:Table ID="selecao" runat="server" CellPadding="10">
        <asp:TableRow>
            <asp:TableCell>
                <asp:LinkButton ID="LinkButton_EscolherTema" runat="server" OnClick="OnClick_EscolherTema">
<img alt="Escolher Tema do Cartão de Vacina" src="img/escolha-tema.gif" width="216px" height="47px" style="border: 0px" />
                </asp:LinkButton>
                <%--<a href="#" onclick="return GB_showImageSet(image_set, 1,SelecionaImagem=function(){location = 'FormConfiguraImpressaoCartaoVacina.aspx?index='+GB_CURRENT.current_index;})">
<img src="img/escolha-tema.gif" width="216px" height="47px" style="border: 0px" /></a>--%>
                <fieldset>
                    <legend>Tipo de Cartão de Vacina</legend>
                    <p>
                        <span>
                            <asp:DropDownList ID="DropDownCartaoVacina" AutoPostBack="true" CssClass="drop" Width="200px"
                                runat="server" OnSelectedIndexChanged="OnSelectedIndexChanged_CartaoVacina">
                                <%-- <asp:ListItem Text="Cartão de Vacina da Criança" Value="1"></asp:ListItem>
<asp:ListItem Text="Cartão de Vacina do Adolescente" Value="3"></asp:ListItem>
<asp:ListItem Text="Cartão de Vacina do Adulto" Value="2"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <asp:ImageButton ID="LnkImprimir" runat="server" OnClick="OnClick_LnkImprimir" ImageUrl="~/Vacina/img/imprimir-cartao.png"
                            Width="178" Height="39px"></asp:ImageButton>
                    </p>
                </fieldset>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
    <%--            <cc1:AnimationExtender ID="AnimationExtender1" runat="server" TargetControlID="LinkButton_EscolherTema">
                <Animations>
<OnClick>
<Sequence AnimationTarget="Panel_TemaCartao">
<EnableAction AnimationTarget="LinkButton_EscolherTema" Enabled="false" />
<StyleAction Attribute="display" Value="Block" />
</Sequence>
</OnClick>
                </Animations>
            </cc1:AnimationExtender>--%>
    <%--           <cc1:AnimationExtender ID="AnimationExtender2" runat="server" TargetControlID="LinkButton1">
                <Animations>
<OnClick>
<Sequence AnimationTarget="Panel_TemaCartao">
<EnableAction AnimationTarget="LinkButton_EscolherTema" Enabled="true" />
<StyleAction Attribute="display" Value="None" />
</Sequence>
</OnClick>
                </Animations>
            </cc1:AnimationExtender>--%>
               </ContentTemplate>
    </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
