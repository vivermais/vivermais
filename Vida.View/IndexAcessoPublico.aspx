﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndexAcessoPublico.aspx.cs" Inherits="ViverMais.View.IndexAcessoPublico" MasterPageFile="~/MasterAcessoPublico.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
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
        <div id="container_menu">
            <div class="divrow_menu">
            </div>
            <div class="divrow_menu">
            </div>
        </div>
    </div>
</asp:Content>
