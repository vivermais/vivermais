﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormIndisponivelUrgencia.aspx.cs"
    Inherits="ViverMais.View.FormIndisponivelUrgencia" MasterPageFile="~/MasterMain.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div style="height: 350px; position: relative; top: 100px;">
        <div style="margin: 0 auto; font-family: Arial; font-size: 11px; font-weight: bold;
            color: #4f4f4f; width: 611px; height: 114px; background-color: #f8eff0; border: solid 2px #970f10">
            <div style="margin: 15px; float: left">
                <img src="../img/exclamacao.png" alt="" /></div>
            <div style="margin-top: 45px; margin-left: 10px; width: 450px;">
                <span style="font-weight: bold; float: none; clear: both">Usuário, o Módulo Urgência
                    ainda não está disponível para sua unidade! </span>
                <br />
                <span style="font-weight: normal">Por favor, procure a administração.</span>
            </div>
        </div>
    </div>
</asp:Content>
