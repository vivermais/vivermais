﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterMain2.Master"
    CodeBehind="home2.aspx.cs" Inherits="ViverMais.View.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
 
  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-20556264-11']);
  _gaq.push(['_trackPageview']);
 
  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();
 
    </script>

    <br />
    <div id="geral">
        <object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="940"
            height="347">
            <param name="movie" value="/swf/bem-vindo.swf" />
            <param name="quality" value="high" />
            <param name="wmode" value="opaque" />
            <param name="swfversion" value="8.0.35.0" />
            <!-- This param tag prompts users with Flash Player 6.0 r65 and higher to download the latest version of Flash Player. Delete it if you don’t want users to see the prompt. -->
            <param name="expressinstall" value="Scripts/expressInstall.swf" />
            <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
            <!--[if !IE]>-->
            <object type="application/x-shockwave-flash" data="/swf/bem-vindo.swf" width="940"
                height="347">
                <!--<![endif]-->
                <param name="quality" value="high" />
                <param name="wmode" value="opaque" />
                <param name="swfversion" value="8.0.35.0" />
                <param name="expressinstall" value="Scripts/expressInstall.swf" />
                <!-- The browser displays the following alternative content for users with Flash Player 6.0 and older. -->
                <div>
                    <h4>
                        Content on this page requires a newer version of Adobe Flash Player.</h4>
                    <p>
                        <a href="http://www.adobe.com/go/getflashplayer">
                            <img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif"
                                alt="Get Adobe Flash player" width="112" height="33" /></a></p>
                </div>
                <!--[if !IE]>-->
            </object>
            <!--<![endif]-->
        </object>
        <%--<div id="container_menu">
            <div class="divrow_menu">
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkAgendamento" runat="server" PostBackUrl="~/Agendamento/Default.aspx">                <img id="img_agendamento" alt="" src="/img/bts/bt_agend_off.png" 
                onmouseover="img_agendamento.src='img/bts/bt_agend_1.png';" 
                onmouseout="img_agendamento.src='img/bts/bt_agend_off.png';"  
                />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkOuvidoria" runat="server">
                <img id="img_ouvidoria" alt="" src="/img/bts/bt_ouvid_off.png" 
                onmouseover="img_ouvidoria.src='img/bts/bt_ouvid_1.png';" 
                onmouseout="img_ouvidoria.src='img/bts/bt_ouvid_off.png';"  />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkFarmacia" runat="server" PostBackUrl="~/Farmacia/Default.aspx">
                <img id="img_farmacia" alt="" src="/img/bts/bt_farmac_off.png" 
                onmouseover="img_farmacia.src='img/bts/bt_farmac_1.png';" 
                onmouseout="img_farmacia.src='img/bts/bt_farmac_off.png';"  />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkSus" PostBackUrl="Paciente/Default.aspx" runat="server">
                    <img id="img_sus" alt="" src="/img/bts/bt_sus_off.png"
                    onmouseover="img_sus.src='img/bts/bt_sus_on.png';" 
                    onmouseout="img_sus.src='img/bts/bt_sus_off.png';"  />
                    </asp:LinkButton></div>
            </div>
            <div class="divrow_menu">
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkSeguranca" PostBackUrl="Seguranca/Default.aspx" runat="server">
                    <img id="img_seguranca" alt="" src="/img/bts/bt_seg_off.png"
                    onmouseover="img_seguranca.src='img/bts/bt_seg_1.png';"
                    onmouseout="img_seguranca.src='img/bts/bt_seg_off.png';"  />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkRelatorio" PostBackUrl="~/Relatorio/RelatorioGenerico.aspx" runat="server" >
                <img id="img_relatorio" alt="" src="img/bts/bt_rel_off.png" 
                onmouseover="img_relatorio.src='img/bts/bt_rel_1.png';" 
                onmouseout="img_relatorio.src='img/bts/bt_rel_off.png';"  />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkUrgencia" PostBackUrl="Urgencia/Default.aspx" runat="server">
                    <img id="img_urgencia" alt="" src="img/bts/bt_urgenc_off.png"
                    onmouseover="img_urgencia.src='img/bts/bt_urgenc_1.png';"
                    onmouseout="img_urgencia.src='img/bts/bt_urgenc_off.png';"  />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lnkCadastro" PostBackUrl="Paciente/FormPaciente.aspx" runat="server">                
                    <img id="img_cadastro" alt="" src="img/bts/bt_cad_off.png" onmouseover="img_cadastro.src='img/bts/bt_cad_on.png';" onmouseout="img_cadastro.src='img/bts/bt_cad_off.png';" />
                    </asp:LinkButton></div>
                <div class="divcell_menu">
                    <asp:LinkButton ID="lblBPA" PostBackUrl="~/EnvioBPA/Default.aspx" runat="server">
                        <img id="img1" alt="" src="" onmouseover="" onmouseout="" />
                    </asp:LinkButton></div>
                <br />
                <br />
                <table align="center" border="0" cellpadding="0" cellspacing="0" width="865">
                    <tr>
                        <td height="127" valign="top" width="764">
                            
                            
                            
                            
                            
                            
                            
                            
                            </td>
                    </tr>
                </table>
                <br />
                <br />
                <br />
            </div>
            <div class="divcell_menu">
                    <asp:LinkButton ID="lblVacina" PostBackUrl="~/Vacina/Default.aspx" runat="server">
                        <img id="img1" alt="Vacina" src="" onmouseover="" onmouseout="" />
                    </asp:LinkButton></div>
            </div>--%>
        <div style="margin-left: 40px">
            <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 870px;
                margin: 0 0 0 0;">
                <tr>
                    <td height="127" valign="top" width="870">
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkSus" PostBackUrl="Paciente/Default.aspx" runat="server">
                    <img id="img_sus" alt="" src="img/bts/bt_sus_off.png"
                    onmouseover="img_sus.src='img/bts/bt_sus_on.png';" 
                    onmouseout="img_sus.src='img/bts/bt_sus_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkCadastro" PostBackUrl="Paciente/FormPaciente.aspx" runat="server">                
                    <img id="img_cadastro" alt="" src="img/bts/bt_cad_off.png"
                     onmouseover="img_cadastro.src='img/bts/bt_cad_on.png';"
                      onmouseout="img_cadastro.src='img/bts/bt_cad_off.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkAgendamento" runat="server" PostBackUrl="~/Agendamento/Default.aspx">                
            <img id="img_agendamento" alt="" src="img/bts/bt_agend_off.png" 
                onmouseover="img_agendamento.src='img/bts/bt_agend_1.png';" 
                onmouseout="img_agendamento.src='img/bts/bt_agend_off.png';"  />
                            </asp:LinkButton></div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkUrgencia" PostBackUrl="Urgencia/Default.aspx" runat="server">
                    <img id="img_urgencia" alt="" src="img/bts/bt_urgenc_off.png"
                    onmouseover="img_urgencia.src='img/bts/bt_urgenc_1.png';"
                    onmouseout="img_urgencia.src='img/bts/bt_urgenc_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkFarmacia" runat="server" PostBackUrl="~/Farmacia/Default.aspx">
                <img id="img_farmacia" alt="" src="img/bts/bt_farmac_off.png" 
                onmouseover="img_farmacia.src='img/bts/bt_farmac_1.png';" 
                onmouseout="img_farmacia.src='img/bts/bt_farmac_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkVacina" PostBackUrl="~/Vacina/Default.aspx" runat="server">
                        <img id="img_Vacina" alt="" src="img/bts/bt_vacina_off.png"
                        onmouseover="img_Vacina.src='img/bts/bt_vacina_1.png';"
                        onmouseout="img_Vacina.src='img/bts/bt_vacina_off.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkSeguranca" PostBackUrl="Seguranca/Default.aspx" runat="server">
                    <img id="img_seguranca" alt="" src="img/bts/bt_seg_off.png"
                    onmouseover="img_seguranca.src='img/bts/bt_seg_1.png';"
                    onmouseout="img_seguranca.src='img/bts/bt_seg_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkRelatorio" PostBackUrl="~/Relatorio/Home.aspx" runat="server">
                <img id="img_relatorio" alt="" src="img/bts/bt_rel_off.png" 
                onmouseover="img_relatorio.src='img/bts/bt_rel_1.png';" 
                onmouseout="img_relatorio.src='img/bts/bt_rel_off.png';"  style="text-decoration:none" /> 
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkBPA" PostBackUrl="~/EnvioBPA/Default.aspx" runat="server">
                        <img id="img_envioBpa" alt="" src="img/bts/bt_envio_bpa_off.png"
                        onmouseover="img_envioBpa.src='img/bts/bt_envio_bpa_1.png';" 
                        onmouseout="img_envioBpa.src='img/bts/bt_envio_bpa_off.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkExportCNS" runat="server" Text="Exportar CNS" PostBackUrl="~/Paciente/FormExportarCNS.aspx">
                           <img id="img_expcns" alt="" src="img/bts/bt_exp-cns_1.png" 
                onmouseover="img_expcns.src='img/bts/bt_exp-cns_on.png';" 
                onmouseout="img_expcns.src='img/bts/bt_exp-cns_1.png';"  />
                            </asp:LinkButton></div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lnkOuvidoria" runat="server">
                <img id="img_ouvidoria" alt="" src="img/bts/bt_ouvid_off.png" 
                onmouseover="img_ouvidoria.src='img/bts/bt_ouvid_1.png';" 
                onmouseout="img_ouvidoria.src='img/bts/bt_ouvid_off.png';"  />
                            </asp:LinkButton></div>
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Image ID="rodape_ViverMais" runat="server" ImageUrl="~/img/rodape_ViverMais.png" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
