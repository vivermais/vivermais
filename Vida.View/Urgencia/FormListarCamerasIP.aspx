﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormListarCamerasIP.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormListarCamerasIP" MasterPageFile="~/Urgencia/MasterRelatorioUrgencia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <link href="GreyBox/gb_styles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <% 
        string url = Request.Url.ToString();
        url = url.Substring(0, url.LastIndexOf(char.Parse("/")));
    %>

    <script type="text/javascript">
        var GB_ROOT_DIR = '<%=url %>/GreyBox/';
    </script>

    <script type="text/javascript" src="GreyBox/AJS.js"></script>

    <script type="text/javascript" src="GreyBox/AJS_fx.js"></script>

    <script type="text/javascript" src="GreyBox/gb_scripts.js"></script>
    
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    CÂMERAS IP</h2>
                <fieldset>
                    <legend>Relação</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Unidades" AutoGenerateColumns="false" Width="750px" DataKeyNames="CNES"
                                runat="server" OnRowDataBound="OnRowDataBound_FormataGridViewUnidades">
                                <Columns>
                                    <asp:BoundField HeaderText="Unidade" DataField="NomeFantasia" />
                                    <asp:TemplateField HeaderText="Câmeras">
                                        <ItemTemplate>
                                            <cc1:Accordion ID="Accordion_Cameras" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                                HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                                                ContentCssClass="accordionContent">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AccordionPane_Um" runat="server">
                                                        <Header>
                                                            Visualizar
                                                        </Header>
                                                        <Content>
                                                            <asp:GridView ID="GridView_Cameras" Width="350px" runat="server" OnRowDataBound="OnRowDataBound_FormataGridViewCameras"
                                                                AutoGenerateColumns="false">
                                                                <Columns>
                                                                    <asp:BoundField DataField="Local" HeaderText="Local" ItemStyle-Width="300px" />
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton_Ver" CausesValidation="true" runat="server" CommandArgument='<%#bind("Endereco") %>'>
                                                                                <asp:Image ID="Imgcam" runat="server" ImageUrl="~/Urgencia/img/btn-cam.png" Width="32px" Height="32px" />
                                                                                </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="tab" />
                                                                <RowStyle CssClass="tabrow" />
                                                            </asp:GridView>
                                                        </Content>
                                                    </cc1:AccordionPane>
                                                </Panes>
                                            </cc1:Accordion>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
