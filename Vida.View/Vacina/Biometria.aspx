﻿<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="Biometria.aspx.cs" Inherits="ViverMais.View.Vacina.Biometria" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="JavaScript" type="text/javascript">

	function get_url_param(name)
	{  
		name = name.replace(/[\[]/,"\\\[").replace(/[\]]/,"\\\]");  
		var regexS = "[\\?&]"+name+"=([^&#]*)";  
		var regex = new RegExp( regexS );  
		var results = regex.exec( window.location.href ); 
		if( results == null )    return "";  
		else return results[1];
	} 
	function writeAppletTag() {
		document.writeln('<applet width="224" height="250">"');
        document.writeln('<param name="cache_archive" value="../BiometriaVacinaApplet.jar,axis.jar,../SignedFingerprintSDKJavaAppletSample.jar" />');
		document.writeln(buildParamTag('id',get_url_param('codigo')));
		document.writeln(buildParamTag('url','http://www.ViverMais.saude.salvador.ba.gov.br/Vacina/FormPaciente.aspx?codigo='));
		document.writeln('<param name="jnlp_href" value="../Vacina/launch.jnlp" />');
		document.writeln('</APPLET>');
	}

	function buildParamTag(name, value) {
		return '<PARAM NAME="' + name + '" VALUE="' + value + '">';
	}

    </script>

    <div>
        <fieldset>
            <legend>Identificação Biométrica</legend>
            <script language="JavaScript" type="text/javascript">
                        writeAppletTag();
            </script>
        </fieldset>
    </div>
</asp:Content>
