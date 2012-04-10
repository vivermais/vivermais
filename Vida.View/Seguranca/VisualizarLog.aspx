﻿<%@ Page Language="C#" MasterPageFile="~/Seguranca/MasterSeguranca.Master" AutoEventWireup="true"
    CodeBehind="VisualizarLog.aspx.cs" Inherits="ViverMais.View.Seguranca.VisualizarLog"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Visualizar LOG</h2>
        <fieldset class="formulario">
            <legend>LOG</legend>
            <p>
                <span class="rotulo">Data Inicial</span> <span>
                    <asp:TextBox ID="tbxDataInicial" CssClass="campo" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="img/calendar_icon.png"
                        TargetControlID="tbxDataInicial">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxDataInicial"
                        Display="Dynamic" ErrorMessage="Preencha a Data Inicial" Font-Size="XX-Small"></asp:RequiredFieldValidator></span>
            </p>
            <p>
                <span class="rotulo">Data Final</span> <span>
                    <asp:TextBox ID="tbxDataFinal" runat="server" CssClass="campo"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupButtonID="img/calendar_icon.png"
                        TargetControlID="tbxDataFinal">
                    </cc1:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxDataFinal"
                        Display="Dynamic" ErrorMessage="Preencha a Data Final" Font-Size="XX-Small"></asp:RequiredFieldValidator></span>
            </p>
            <p>
                <span class="rotulo">Evento</span> <span>
                    <asp:DropDownList ID="ddlEvento" CssClass="campo" Height="21px" runat="server">
                    </asp:DropDownList>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="ImgBtnEnviar1" runat="server" OnClick="ImgBtnEnviar1_Click">
      <img id="imgenviar" alt="Salvar" src="img/btn_enviar_dados1.png"
      onmouseover="imgenviar.src='img/btn_enviar_dados2.png';"
      onmouseout="imgenviar.src='img/btn_enviar_dados1.png';" /></asp:LinkButton>
            </div>
            <br />
            <br />
            <p>
            <span class="rotulo">Evento</span>
            <span><asp:Label ID="lblEvento" runat="server" CssClass="campo" Text="-"></asp:Label></span>
                 
            </p>
            <p>
                <asp:GridView ID="GridViewLog" runat="server" AutoGenerateColumns="False" Width="100%"
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                    CellPadding="3" GridLines="Vertical">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle CssClass="tabrow_left" BackColor="#EEEEEE" ForeColor="Black" Height="23px"
                        Font-Names="Verdana" />
                    <Columns>
                        <asp:BoundField DataField="Data" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" HeaderText="Data" />
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <asp:BoundField DataField="Valor" HeaderText="Valor" />
                    </Columns>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle CssClass="tab" BackColor="#5b5b5b" Font-Bold="True" ForeColor="White"
                        Font-Size="13px" Font-Names="Verdana" Height="27px" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                </asp:GridView>
                <!--asp:BoundField DataField="Evento" HeaderText="Evento" ItemStyle-HorizontalAlign="Center" /-->
            </p>
        </fieldset>
    </div>
</asp:Content>