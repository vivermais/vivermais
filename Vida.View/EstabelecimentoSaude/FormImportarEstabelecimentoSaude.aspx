<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImportarEstabelecimentoSaude.aspx.cs"
    Inherits="ViverMais.View.EstabelecimentoSaude.FormImportarEstabelecimentoSaude" MasterPageFile="~/EstabelecimentoSaude/MasterEstabelecimento.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder3">
    <div id="top">
        <h2>
            Importar Estabelecimento de Saúde (CNES)</h2>
        <fieldset>
            <legend>Arquivo</legend>
<%--            <asp:UpdatePanel ID="UpdatePanel" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="ButtonUpload" />
                </Triggers>
                <ContentTemplate>--%>
                    <p>
                        <span class="rotulo">Arquivo</span> <span>
                            <asp:FileUpload ID="FileUpload_Estabelecimento" runat="server" Width="400px" CssClass="drop" />
                        </span>
                    </p>
                    <div style="font-family:Arial; font-size:11px; font-weight:bold; color:#bc0d0d; margin-bottom:12px;" >* Tamanho máximo do arquivo: 50 MB. Formato compatível: XML.</div>
                    <div class="botoesroll">
                  <asp:LinkButton ID="ButtonUpload" runat="server" OnClick="OnClick_Importar" >
                  <img id="imgimport" alt="Importar" src="img/importar1.png"
                  onmouseover="imgimport.src='img/importar2.png';"
                  onmouseout="imgimport.src='img/importar1.png';" /></asp:LinkButton>
                        </div>
                    <div class="botoesroll">
                  <asp:LinkButton ID="ButtonVoltar" runat="server" Text="Voltar" PostBackUrl="~/EstabelecimentoSaude/FormImportacoesRealizadas.aspx" >
                  <img id="imgvoltar" alt="Voltar" src="img/voltar1.png"
                  onmouseover="imgvoltar.src='img/voltar2.png';"
                  onmouseout="imgvoltar.src='img/voltar1.png';" /></asp:LinkButton>
                        </div><br />
<%--                </ContentTemplate>
            </asp:UpdatePanel>--%>
        </fieldset>
    </div>
</asp:Content>
