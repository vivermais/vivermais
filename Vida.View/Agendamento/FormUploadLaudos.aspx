<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormUploadLaudos.aspx.cs"
    EnableEventValidation="false" Inherits="ViverMais.View.Agendamento.FormUploadLaudos" %>

<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v10.2, Version=10.2.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../style_popup.css" type="text/css" media="screen" />

    <script type="text/javascript" language="javascript" src="../JavaScript/MascarasGerais.js"></script>

    <script type="text/javascript" language="javascript" src="js/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" language="javascript" src="js/jquery.uploadify.v2.1.3.min.js"></script>

    <script type="text/javascript" language="javascript" src="js/swfobject.js"></script>

    <link type="text/css" rel="Stylesheet" href="uploadify.css" />

    <script type="text/javascript">
    // <![CDATA[
        var fieldSeparator = "|";
        function FileUploaded(s, e) 
        {
            if(e.isValid) 
            {
                var indexSeparator = e.callbackData.indexOf(fieldSeparator)
                var fileName = e.callbackData.substring(0, indexSeparator);
//                if(check(fileName) == true)
//                {
                    var listbox = document.getElementById("listBox1");
                    if(listbox != null)
                    {
                        var exite = false;
                        for(i=listbox.options.length-1;i>=0;i--)
                        { 
                            if(listbox.options[i].value == fileName)
                            {
                                exite = true;
                                break;
                                //listbox.remove(i);
                            }
                        }
                        if(exite == false)
                            listbox.add(new Option(fileName,fileName));
                        else
                            alert('Arquivo já exite na lista!');
                    }
//                }
//                else
//                {
//                    alert('Arquivo com nome inválido: '+ fileName +';');
//                }
            }
        }
        
        function check( filename )
        {
           if( filename.match( /^[\w- ]+\..*$/ ) )
             return true;
           else
             return false;
         }

        
        function RemoveItem(listbox)
        {
            //var listbox = document.getElementById("listBox1");
            //var len = listbox.options.length;
            var i;
            
            var selecionouArquivo = false;
            var nomeArquivo;

            for(i=listbox.options.length-1;i>=0;i--)
            { 
                if(listbox.options[i].selected)
                {
                    document.getElementById("HiddenNomeAnexo").value = listbox.options[i].value;
                    selecionouArquivo = true;
                    listbox.remove(i);
                    break;
                }
            }
            if(selecionouArquivo == false)
                alert('Selecione um anexo para excluir!');
                
        }
        
        
    // ]]>
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true">
    </asp:ScriptManager>
    <cc1:AlwaysVisibleControlExtender runat="server" ID="AlwaysVisibleControlExtender2"
        TargetControlID="UpDateProgressUpload" VerticalSide="Middle" VerticalOffset="10"
        HorizontalSide="Center" HorizontalOffset="10" ScrollEffectDuration=".1">
    </cc1:AlwaysVisibleControlExtender>
    <br />
    <asp:UpdateProgress ID="UpDateProgressUpload" runat="server" DisplayAfter="1" DynamicLayout="false">
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
            <legend class="img-anexar-laudo"></legend>
            <p>
                <span style="font-family: Verdana; font-size: 12px; color: White; font-weight: bold;">
                    Selecione os arquivos de até 1MB</span>
            </p>
            <p>
                <dx:ASPxUploadControl ID="ASPxUploadControl1" runat="server" OnFileUploadComplete="UploadControl_FileUploadComplete"
                    AddButton-Image-AlternateText="Adiciona um Laudo" AddButton-Text="" ShowAddRemoveButtons="true"
                    ClientInstanceName="UploadControl" CancelButton-Text="Cancelar" UploadButton-Text="Enviar"
                    RemoveButton-Text="" RemoveButton-Image-AlternateText="Remove um Laudo" RemoveButton-Image-Url="~/Agendamento/img/excluirr.png"
                    AddButton-Image-Url="~/Agendamento/img/btn-adicionar-campo.png" ShowProgressPanel="True" ShowUploadButton="True"
                    Size="50" ProgressBarSettings-DisplayMode="Percentage">
                    <ValidationSettings NotAllowedFileExtensionErrorText="Extensão do arquivo não permitida"
                        MaxFileSizeErrorText="Tamanho do arquivo Inválido. " AllowedFileExtensions=".jpg,.jpeg"
                        MaxFileSize="1048576">
                    </ValidationSettings>
                    <ClientSideEvents FileUploadComplete="function(s, e) { FileUploaded(s, e) }" />
                    <UploadButton Text="" Image-Url="img/btn-anexar-laudo.png">
                    </UploadButton>
                </dx:ASPxUploadControl>
                <br />
                <br />
            </p>
            <span style="font-family: Verdana; font-size: 12px; color: White; font-weight: bold;">
                Arquivos Anexados</span>
            <p>
                <asp:ListBox ID="listBox1" runat="server" Rows="15" Width="300px"></asp:ListBox>
               
            </p>
            <asp:UpdatePanel ID="UpdatePanel_CamposEscondidos" runat="server" UpdateMode="Conditional"
                ChildrenAsTriggers="false" RenderMode="Inline">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnRemoverArquivo" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenNomeAnexo" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<a href="#destination">
            <%--<img src="img/btn-excluir1.png" alt="Remover Laudo da lista de Anexos" onclick="RemoveItem(listBox1)" /></a>--%>
            <asp:LinkButton ID="btnRemoverArquivo" CausesValidation="false" runat="server" OnClick="btnRemoverArquivo_OnClick"
                    OnClientClick="javascript:RemoveItem(listBox1);">
                    <img alt="Remover Laudo da lista de Anexos" src="img/btn-excluir-item.png" />
                </asp:LinkButton>
           
            <asp:ImageButton ID="btnFinalizaSolicitacao" Text="Finalizar" runat="server" OnClientClick="parent.parent.GB_hide();"
                CausesValidation="false" ImageUrl="~/images/finalizar-upload.png"></asp:ImageButton>
        </fieldset>
    </div>
    </form>
</body>
</html>
