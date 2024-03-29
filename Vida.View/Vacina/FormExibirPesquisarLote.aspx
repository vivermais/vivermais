﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibirPesquisarLote.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormExibirPesquisarLote" EnableEventValidation="false"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Vacina/WUCPesquisarLote.ascx" TagName="TagName_PesquisarLote"
    TagPrefix="TagPrefix_PesquisarLote" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Lote de Imunobiológico
        </h2>
        <div class="botoesroll">
            <asp:LinkButton ID="Lnk_NovoLote" runat="server" PostBackUrl="~/Vacina/FormLoteImunobiologico.aspx">
                  <img id="imgnovoregistro" alt="Novo Lote" src="img/btn_novo_lote1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_lote2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_lote1.png';" /></asp:LinkButton>
        </div>
        <br />
        <br />
        <TagPrefix_PesquisarLote:TagName_PesquisarLote ID="WUCPesquisarLote" runat="server">
        </TagPrefix_PesquisarLote:TagName_PesquisarLote>
    </div>
</asp:Content>
