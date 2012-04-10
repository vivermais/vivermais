<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TesteUtil.aspx.cs" Inherits="Vida.View.TesteUtil"
    MasterPageFile="~/MasterMain.Master" EnableEventValidation="false" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        $(function() {
            $("#tabs").tabs();
        });
    </script>

    <script type="text/javascript">	$(function() {		$( "#tabs" ).tabs();	});	</script>

    <div style="width: 500px; margin: 0 auto; margin-top: 50px">
        <div id="tabs">
            <ul>
                <li><a href="#tabs-1">A mãe de Danísio</a></li>
                <li><a href="#tabs-2">A de Paschoal</a></li>
                <li><a href="#tabs-3">E a de Blade</a></li>
            </ul>
            <div id="tabs-1">
                Calma, isso é apenas para testar a funcionalidade desse novo componente... e pelo
                visto está servindo bastante, né?</div>
            <div id="tabs-2">
                Phasellus mattis tincidunt nibh. Cras orci urna, blandit id, pretium vel, aliquet
                ornare, felis. Maecenas scelerisque sem non nisl. Fusce sed lorem in enim dictum
                bibendum.</div>
            <div id="tabs-3">
                Nam dui erat, auctor a, dignissim quis, sollicitudin eu, felis. Pellentesque nisi
                urna, interdum eget, sagittis et, consequat vestibulum, lacus. Mauris porttitor
                ullamcorper augue.</div>
        </div>
    </div>
</asp:Content>
