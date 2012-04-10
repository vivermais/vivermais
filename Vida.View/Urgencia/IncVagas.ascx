﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="IncVagas.ascx.cs" Inherits="ViverMais.View.Urgencia.IncVagas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<link rel="stylesheet" href="style_form_urgencia.css" type="text/css" media="screen" />
<div id="Div1" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                        height: 200%; z-index: 100; min-height: 100%; background-color: #999; filter: alpha(opacity=45);
                        moz-opacity: 0.3; opacity: 0.3">
                        
                        <div id="Div2" visible="false" style="position: fixed; background-color: #FFFFFF;
                        background-position: center; background-repeat: no-repeat; top: 100px; left: 25%;
                        width: 500px; height: 400px; z-index: 102; background-image: url('img/fundo_mensagem.png');
                        border-right: #336699 2px solid; padding-right: 10px; border-top: #336699 2px solid;
                        padding-left: 10px; padding-bottom: 10px; border-left: #336699 2px solid; color: #000000;
                        padding-top: 10px; border-bottom: #336699 2px solid; text-align: justify; font-family: Verdana;">
                        <div style="width: 500px; height: 10px; margin-left: 20px; margin-top: 10px;">
                        
    <asp:Label ID="lbQuadroVagas" runat="server" Text=""></asp:Label>
        <br />
        <p>
            <span>
                <asp:GridView ID="GridView_Vagas" AutoGenerateColumns="false" runat="server">
                    <Columns>
                        <asp:BoundField HeaderText="Tipo" DataField="TipoVaga" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Número total de leitos" DataField="Vaga" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="Número de leitos ocupados" DataField="VagaOcupada" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow" />
                </asp:GridView>
            </span>
        </p>
    </div>
</div>
