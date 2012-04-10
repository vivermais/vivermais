﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormExibeCameraIP.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormExibeCameraIP" EnableEventValidation="false"
    MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .accordionHeaderC
        {
            border: 1px solid #142126;
            color: #142126;
            background-color: #eee; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 400px;
            text-align: center;
        }
        .accordionHeaderSelectedC
        {
            border: 1px solid #142126;
            color: white;
            background-color: #142126; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 400px;
            text-align: center;
        }
        .accordionContentC
        {
            background-color: #fff;
            border: 1px solid #142126;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 400px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <fieldset class="formulario">
            <legend>Câmeras Cadastradas</legend>
            <p>
                <asp:LinkButton ID="LinkButton_NovoRegistro" runat="server" PostBackUrl="~/Urgencia/FormCameraIP.aspx">
            <img id="imgnovo" alt="Novo Registro" src="img/novo-registro1.png"
                  onmouseover="this.src='img/novo-registro2.png';"
                  onmouseout="this.src='img/novo-registro1.png';" />
                </asp:LinkButton>
            </p>
            <p>
                <span>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:GridView ID="GridView_Unidades" runat="server" AutoGenerateColumns="false" DataKeyNames="CNES"
                                OnRowDataBound="OnRowDataBound_ConfigurarGridViewUnidade" Width="690">
                                <Columns>
                                    <asp:BoundField DataField="NomeFantasia" HeaderText="Unidade" ItemStyle-Width="400px" />
                                    <asp:TemplateField HeaderText="Câmeras">
                                        <ItemTemplate>
                                            <cc1:Accordion ID="Accordion_Cameras" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                                HeaderCssClass="accordionHeaderC" HeaderSelectedCssClass="accordionHeaderSelectedC"
                                                ContentCssClass="accordionContentC">
                                                <Panes>
                                                    <cc1:AccordionPane ID="AcccordionPane" runat="server">
                                                        <Header>
                                                            Visualizar</Header>
                                                        <Content>
                                                            <asp:GridView ID="GridView_Cameras" runat="server" AutoGenerateColumns="false" Width="390px">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="Local" DataField="Local" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:BoundField HeaderText="Endereço" DataField="Endereco" ItemStyle-HorizontalAlign="Center" />
                                                                    <asp:HyperLinkField ItemStyle-HorizontalAlign="Center" HeaderText="Editar" DataNavigateUrlFields="Codigo"
                                                                        Text="Selecionar" DataNavigateUrlFormatString="~/Urgencia/FormCameraIP.aspx?co_camera={0}" />
                                                                    <%--<asp:TemplateField HeaderText="Editar">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton0" runat="server" Text="Editar" CommandArgument='<%#bind("Codigo") %>'
                                                                                OnClick="OnClick_EditarCamera">
                                                                                 <img id="imgureditar2" alt="Editar" src="img/btn-editar2x.png" />
                                                                                </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>--%>
                                                                    <asp:TemplateField HeaderText="Excluir">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Text="Excluir" CommandArgument='<%#bind("Codigo") %>'
                                                                                OnClientClick="return confirm('Tem certeza que deseja excluir esta câmera ?');"
                                                                                OnClick="OnClick_ExcluirCamera">
                                                                                <img id="imgurexcluir2" alt="Excluir" src="img/btn-excluir2x.png" />
                                                                            </asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="tab" />
                                                                <RowStyle CssClass="tabrow" />
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                                                <EmptyDataTemplate>
                                                                    <asp:Label ID="Label_Empty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                                </EmptyDataTemplate>
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
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
