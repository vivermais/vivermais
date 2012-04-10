<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormPreparoProcedimento.aspx.cs" Inherits="ViverMais.View.Agendamento.FormPreparoProcedimento"
    Title="Untitled Page" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="Up1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSalvar" />
        </Triggers>
        <ContentTemplate>
        <div id="top">
        <h2>Cadastrar Novo Preparo</h2>
            <fieldset class="formulario">
                <legend>Preparos de Procedimentos </legend>
                <h4>Utilize o campo abaixo para fazer a descrição do Preparo de
                    Procedimentos.</h4>

                <div style="padding-left: 90px">
                    <p>
                        <span><textarea id="txtPreparo" runat="server" name="txtPreparo" cols="50" rows="14"></textarea></span>
                        <%--&nbsp;<cc2:Editor ID="tbxPreparo" runat="server" AutoFocus="false" Width="500px" />--%>
                    </p>
                </div>
                <div class="botoesroll"><asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" >
                <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                onmouseover="imgsalvar.src='img/salvar_2.png';"
                onmouseout="imgsalvar.src='img/salvar_1.png';" />
            </asp:LinkButton></div>
            <div class="botoesroll"><asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/BuscaPreparoProcedimento.aspx" >
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton></div>
                
                
            </fieldset>
            </div>
            <%--<asp:Panel ID="PanelDescricao" runat="server" Visible="false">
                <div id="cinza" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                    height: 200%; z-index: 100; min-height: 100%; background-color: #999;">
                </div>
                <div id="mensagem" visible="false" style="position: fixed; top: 70px; left: 250px;
                    width: 500px; z-index: 102; border-right: #336699 2px solid; padding-right: 10px; border-top: #336699 2px solid;
                    padding-left: 10px; padding-bottom: 10px; border-left: #336699 2px solid; color: #fff;
                    padding-top: 10px; border-bottom: #336699 2px solid; text-align: justify; font-family: Verdana; ">                   
                    <p>
                        <span style="margin-left: 480px;">
                            <asp:ImageButton ID="btnFechar" runat="server" ImageUrl="~/img/close24.png" OnClick="btnFechar_Click"
                                CausesValidation="false" />
                        </span>
                    </p>
                </div>
            </asp:Panel>--%>
          
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
