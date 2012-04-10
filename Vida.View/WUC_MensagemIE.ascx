<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_MensagemIE.ascx.cs"
    Inherits="ViverMais.View.WUC_MensagemIE" %>
    
<asp:Panel ID="PanelMensagem" runat="server" Visible="false">
    <div id="mensagem-ie">
        <div id="img-mensagem-ie">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/img/img-interroga-ie.png" />
            <%--<img src="/img/img-interroga-ie.png" alt=" " />--%>
        </div>
        <div id="texto-mensagem-ie">
            <p>
                <strong>Prezado usuário,</strong><br />
                Caso esteja utilizando o Internet Explorer com versão igual ou superior a 7.0, você
                provavelmente não conseguirá fazer o download dos arquivos desta página. Nesse caso,
                siga as seguintes instruções:
            </p>
            <p>
                No Internet Explorer, ir em:<br />
                <strong>Ferramentas > Opções de Internet > Segurança > Internet > Nível Personalizado
                    > Downloads</strong>
            </p>
            <p>
                Na opção<strong> Aviso automático para downloads de arquivos</strong> marcar a opção
                <strong>Habilitar</strong>.
            </p>
        </div>
    </div>
</asp:Panel>
