﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterMain.Master"
    CodeBehind="Home.aspx.cs" Inherits="ViverMais.View.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

    <link media="screen" type="text/css" rel="stylesheet" href="colorbox.css" />

    <script type="text/javascript" src="JavaScript/jquery.colorbox.js"></script>

    <script type="text/javascript">
                    
                jQuery.noConflict();
  
			    setTimeout(function(){
                jQuery.colorbox({ href:"#inline_content", inline:true,  width:"80%" });
                },20);
         
    </script>

    <div id="geral">
        &nbsp;&nbsp;&nbsp;
        <object id="FlashID" classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" width="940"
            height="347">
            <param name="movie" value="swf/bem-vindo.swf" />
            <param name="quality" value="high" />
            <param name="wmode" value="opaque" />
            <param name="swfversion" value="8.0.35.0" />
            <!-- This param tag prompts users with Flash Player 6.0 r65 and higher to download the latest version of Flash Player. Delete it if you don’t want users to see the prompt. -->
            <param name="expressinstall" value="Scripts/expressInstall.swf" />
            <!-- Next object tag is for non-IE browsers. So hide it from IE using IECC. -->
            <!--[if !IE]>-->
            <object type="application/x-shockwave-flash" data="swf/bem-vindo.swf" width="940"
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
        <!-- This contains the hidden content for inline calls -->
        <div style='display: none'>
            <div id='inline_content' style="padding: 14px; background: #fff; font-family: Arial;
                font-size: 12px !important; text-align: justify;" class="inline">
                <p style="font-size: 17px; font-weight: bold; margin-bottom: 15px;">
                    Caros colaboradores,</p>
                <p>
                    Em 2012 o Ministério da Saúde está adotando um novo modelo de Cartão SUS, seguindo
                    a Portaria Ministério da Saúde N° 940/2011, este passará a ser disponibilizado pelas
                    Unidades de Saúde do município de Salvador. Vale lembrar que este novo modelo segue
                    todas as premissas e diretrizes do Cartão Nacional de Saúde. Importante: A numeração
                    do Cartão Nacional de Saúde é única e aceita em todo o território brasileiro, salientamos
                    que os modelos anteriores continuam sendo válidos, basta ter somente um deles, conforme
                    os exemplos abaixo.</p>
                <p style="font-size: 14px; font-weight: bold; padding: 10px 0px 10px 0px">
                    ATENÇÃO:</p>
                <p>
                    <b>Como proceder, caso o usuário não saiba ou não possua o número do Cartão SUS:</b></p>
                <p>
                    • Caso o usuário das ações e serviços de saúde não disponha da informação do número
                    do seu CNS, o mesmo poderá consultar e/ou imprimir o Cartão Nacional de Saúde através
                    do site ViverMais+, www.saude.salvador.ba.gov.br/ViverMais ou dirigindo-se a qualquer unidade
                    de saúde da rede municipal;</p>
                <p>
                    • Caso o usuário não possua Cartão SUS, o mesmo deverá comparecer a unidade de saúde
                    da rede municipal mais próxima, tendo em mãos o RG ou Certidão de Nascimento (para
                    menores de 16 anos) e comprovante de residência (com CEP válido) para confeccionar
                    o seu Cartão SUS.</p>
                <p style="font-size: 14px; font-weight: bold; padding: 10px 0px 10px 0px">
                    OBSERVAÇÕES:</p>
                <p>
                    <b>1</b> - Usuários de outros municípios podem obter o Cartão SUS através das Secretarias
                    Municipais de Saúde dos seus respectivos Municípios ou através das Unidades de Saúde
                    de Salvador, sendo que, será mantido o município de residência.</p>
                <p>
                    <b>2</b> – O Estabelecimento de Saúde não pode negar o atendimento ao usuário, uma
                    vez que ele tenha em mãos, qualquer um dos Cartões SUS apresentados abaixo.
                </p>
                <p>
                    Mais informações referentes ao Cartão Nacional de Saúde, entre em contato com o
                    Núcleo de Gestão da Informação – NGI através do telefone (71) 3186-1264 / 1265 /
                    1189.</p>
                <p>
                    Exemplos de modelos de Cartão SUS confeccionados e válidos para atendimento na rede
                    SUS (Estabelecimentos Assistenciais de Saúde da rede própria e conveniada):</p>
                <p style="position: relative; margin: 10px 0px 10px 0px;">
                    <img src="img/cartoes-validos.jpg" alt="cartões Válidos" /></p>
                <p>
                    Pedimos a colaboração de todos.<br />
                    <b>Equipe NGI</b>
                </p>
            </div>
        </div>
        <div style="margin-left: 0px">
            <table align="center" border="0" cellpadding="0" cellspacing="0" style="width: 870px;
                margin: 0 0 0 0;">
                <tr>
                    <td height="127" valign="top" width="870">
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkAtendimento" PostBackUrl="Atendimento/Default.aspx" runat="server">
                    <img id="img_atendimento" alt="" src="img/bts/bt_atendimento_off.png"
                    onmouseover="img_atendimento.src='img/bts/bt_atendimento_on.png';" 
                    onmouseout="img_atendimento.src='img/bts/bt_atendimento_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkSus" PostBackUrl="Paciente/Default.aspx" runat="server">
                    <img id="img_sus" alt="" src="img/bts/bt_sus_off.png"
                    onmouseover="img_sus.src='img/bts/bt_sus_on.png';" 
                    onmouseout="img_sus.src='img/bts/bt_sus_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkAgendamento" runat="server" PostBackUrl="~/Agendamento/Default.aspx">                
            <img id="img_agendamento" alt="" src="img/bts/bt_agend_off.png" 
                onmouseover="img_agendamento.src='img/bts/bt_agend_1.png';" 
                onmouseout="img_agendamento.src='img/bts/bt_agend_off.png';"  />
                            </asp:LinkButton></div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkUrgencia" PostBackUrl="Urgencia/Default.aspx" runat="server">
                    <img id="img_urgencia" alt="" src="img/bts/bt_urgenc_off.png"
                    onmouseover="img_urgencia.src='img/bts/bt_urgenc_1.png';"
                    onmouseout="img_urgencia.src='img/bts/bt_urgenc_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkFarmacia" runat="server" PostBackUrl="~/Farmacia/Default.aspx">
                <img id="img_farmacia" alt="" src="img/bts/bt_farmac_off.png" 
                onmouseover="img_farmacia.src='img/bts/bt_farmac_1.png';" 
                onmouseout="img_farmacia.src='img/bts/bt_farmac_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkVacina" PostBackUrl="~/Vacina/Default.aspx" runat="server">
                        <img id="img_Vacina" alt="" src="img/bts/bt_vacina_off.png"
                        onmouseover="img_Vacina.src='img/bts/bt_vacina_1.png';"
                        onmouseout="img_Vacina.src='img/bts/bt_vacina_off.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkSeguranca" PostBackUrl="Seguranca/Default.aspx" runat="server">
                    <img id="img_seguranca" alt="" src="img/bts/bt_seg_off.png"
                    onmouseover="img_seguranca.src='img/bts/bt_seg_1.png';"
                    onmouseout="img_seguranca.src='img/bts/bt_seg_off.png';"  />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkRelatorio" PostBackUrl="~/Relatorio/Home.aspx" runat="server">
                <img id="img_relatorio" alt="" src="img/bts/bt_rel_off.png" 
                onmouseover="img_relatorio.src='img/bts/bt_rel_1.png';" 
                onmouseout="img_relatorio.src='img/bts/bt_rel_off.png';"  style="text-decoration:none" /> 
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkBPA" PostBackUrl="~/EnvioBPA/Default.aspx" runat="server">
                        <img id="img_envioBpa" alt="" src="img/bts/bt_envio_bpa_off.png"
                        onmouseover="img_envioBpa.src='img/bts/bt_envio_bpa_1.png';" 
                        onmouseout="img_envioBpa.src='img/bts/bt_envio_bpa_off.png';" />
                            </asp:LinkButton>
                        </div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkExportCNS" runat="server" Text="Exportar CNS" PostBackUrl="~/Paciente/FormExportarCNS.aspx">
                           <img id="img_expcns" alt="" src="img/bts/bt_exp-cns_1.png" 
                onmouseover="img_expcns.src='img/bts/bt_exp-cns_on.png';" 
                onmouseout="img_expcns.src='img/bts/bt_exp-cns_1.png';"  />
                            </asp:LinkButton></div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="lnkOuvidoria" runat="server">
                <img id="img_ouvidoria" alt="" src="img/bts/bt_ouvid_off.png" 
                onmouseover="img_ouvidoria.src='img/bts/bt_ouvid_1.png';" 
                onmouseout="img_ouvidoria.src='img/bts/bt_ouvid_off.png';"  />
                            </asp:LinkButton></div>
                        <div class="botoesIndex">
                            <asp:LinkButton ID="LinkCatalogo" runat="server" PostBackUrl="~/GuiaProcedimentos/Default.aspx">
                <img id="img_guia" alt="teste" src="img/bts/bt_guia_off.png" 
                onmouseover="img_guia.src='img/bts/bt_guia_on.png';" 
                onmouseout="img_guia.src='img/bts/bt_guia_off.png';"  />
                            </asp:LinkButton></div>
                            <div class="botoesIndex">
                             <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/GuiaProcedimentos/Default.aspx">
                <img id="imgSenhador" alt="Senhador" src="img/bts/bt_senhador_off.png" 
                onmouseover="imgSenhador.src='img/bts/bt_senhador_on.png';" 
                onmouseout="imgSenhador.src='img/bts/bt_senhador_off.png';"  />
                            </asp:LinkButton></div>
                    </td>
                </tr>
                <%-- <tr align="center" >
                <td><asp:Image ID="rodape_ViverMais" runat="server" ImageUrl="~/img/rodape_ViverMais.png"  /></td>
                </tr>--%>
            </table>
        </div>
    </div>
</asp:Content>
