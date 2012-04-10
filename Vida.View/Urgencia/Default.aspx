<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs"
 Inherits="ViverMais.View.Urgencia.Default" Title="ViverMais - Módulo Urgência" EnableViewState="false" %>
<%--<%@ Register Src="~/Urgencia/Inc_GraficoExamesSolicitados.ascx" TagName="Inc_Teste" TagPrefix="IT" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div>
<div id="geral">
<object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="910" height="425">
  <param name="movie" value="home-urgencia.swf" />
  <param name="quality" value="high" />
  <param name="wmode" value="transparent" />
  <param name="swfversion" value="7.0.70.0" />
  <!-- This param tag prompts users with Flash Player 6.0 r65 and higher to download the latest version of Flash Player. Delete it if you don’t want users to see the prompt. -->
  <param name="expressinstall" value="Scripts/expressInstall.swf" />
  <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
  <!--[if !IE]>-->
  <object type="application/x-shockwave-flash" data="home-urgencia.swf" width="910" height="425">
    <!--<![endif]-->
    <param name="quality" value="high" />
    <param name="wmode" value="transparent" />
    <param name="swfversion" value="7.0.70.0" />
    <param name="expressinstall" value="Scripts/expressInstall.swf" />
    <!-- The browser displays the following alternative content for users with Flash Player 6.0 and older. -->
    <div>
      <h4>Content on this page requires a newer version of Adobe Flash Player.</h4>
      <p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" width="112" height="33" /></a></p>
    </div>
    <!--[if !IE]>-->
  </object>
  <!--<![endif]-->
</object>



    </div>
</div>
<%--<div>
<div id="top">


<object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="910" height="517">
  <param name="movie" value="home.swf" />
  <param name="quality" value="high" />
  <param name="wmode" value="transparent" />
  <param name="swfversion" value="7.0.70.0" />
  <!-- This param tag prompts users with Flash Player 6.0 r65 and higher to download the latest version of Flash Player. Delete it if you don’t want users to see the prompt. -->
  <param name="expressinstall" value="Scripts/expressInstall.swf" />
  <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
  <!--[if !IE]>-->
  <object type="application/x-shockwave-flash" data="home.swf" width="910" height="517">
    <!--<![endif]-->
    <param name="quality" value="high" />
    <param name="wmode" value="transparent" />
    <param name="swfversion" value="7.0.70.0" />
    <param name="expressinstall" value="Scripts/expressInstall.swf" />
    <!-- The browser displays the following alternative content for users with Flash Player 6.0 and older. -->
    <div>
      <h4>Content on this page requires a newer version of Adobe Flash Player.</h4>
      <p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" width="112" height="33" /></a></p>
    </div>
    <!--[if !IE]>-->
  </object>
  <!--<![endif]-->
</object>
<script type="text/javascript">
</script>


    </div>
</div>--%>
    <%--<div id="geral" style="background:url('/img/mod_urgence.jpg') no-repeat";>
    <div id="container_menu">
    <div class="divrow_menu">
    <div class="divcell_menu"><asp:LinkButton ID="lnkAcolhimento" OnClientClick="javascript:window.open('RelatorioFilaAcolhimentoUnidades.aspx','Acolhimento');" runat="server">
                <img id="img_fila_acol" alt="" src="/urgencia/img/bt_ur_fila_acol_2.png" 
                onmouseover="img_fila_acol.src='/urgencia/img/bt_ur_fila_acol_1.png';" 
                onmouseout="img_fila_acol.src='/urgencia/img/bt_ur_fila_acol_2.png';"  />
            </asp:LinkButton></div>
    <div class="divcell_menu"><asp:LinkButton ID="lnkAtendimento" runat="server" OnClientClick="javascript:window.open('RelatorioClassificacaoRiscoUnidades.aspx','Atendimento');">
                <img id="img_fila_atend" alt="" src="/urgencia/img/bt_ur_fila_atend_2.png" 
                onmouseover="img_fila_atend.src='/urgencia/img/bt_ur_fila_atend_1.png';" 
                onmouseout="img_fila_atend.src='/urgencia/img/bt_ur_fila_atend_2.png';"  />
            </asp:LinkButton></div>
    <div class="divcell_menu"><asp:LinkButton ID="lnkLeitos" runat="server" OnClientClick="javascript:window.open('RelatorioVagasUnidades.aspx','Leitos');" >
                <img id="img_n_leito" alt="" src="/urgencia/img/bt_ur_num_leit_2.png" 
                onmouseover="img_n_leito.src='/urgencia/img/bt_ur_num_leit_1.png';" 
                onmouseout="img_n_leito.src='/urgencia/img/bt_ur_num_leit_2.png';"  />
            </asp:LinkButton></div>
    </div>
    </div>

</div>--%>

<%--<IT:Inc_Teste ID="Teste" runat="server">
</IT:Inc_Teste>--%>

</asp:Content>
