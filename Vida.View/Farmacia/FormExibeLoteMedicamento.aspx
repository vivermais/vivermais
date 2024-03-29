﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeLoteMedicamento.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormExibeLoteMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/Farmacia/IncPesquisarLoteMedicamento.ascx" TagName="IncPesquisarLoteMedicamento"
    TagPrefix="IPLM" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .formulario2
        {
            width: 670px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 2px 2px 2px 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Lista de Lotes Cadastrados</h2>
        <fieldset class="formulario">
            <legend>Lotes de Medicamento</legend>
            <p>
                <span>
                    <cc1:TabContainer ID="TabContainer1" runat="server">
                        <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Lotes Cadastrados">
                            <ContentTemplate>
                                <fieldset class="formulario2">
                                    <legend>Relação</legend>
                                    <p>
                                        <span>Pressione o botão abaixo para cadastrar um novo lote de medicamento.
                                        <br /> <br />
                                            <asp:LinkButton ID="Button_Novo" runat="server"  PostBackUrl="~/Farmacia/FormLoteMedicamento.aspx">
                  <img id="imgnovoregistro" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgnovoregistro.src='img/btn/novoregistro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                                        </span>
                                    </p>
                                    <p>
                                        <span>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <asp:GridView ID="GridView_Lotes" runat="server" AutoGenerateColumns="false" Width="600px"
                                                        AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                                        <Columns>
                                                            <asp:HyperLinkField HeaderText="Lote" DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Farmacia/FormLoteMedicamento.aspx?co_lote={0}"
                                                                DataTextField="Lote" />
                                                            <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" />
                                                            <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" />
                                                            <asp:BoundField HeaderText="Data de Validade" DataFormatString="{0:dd/MM/yyyy}" DataField="Validade" />
                                                        </Columns>
                                                        <EmptyDataRowStyle HorizontalAlign="Center" />
                                                        <EmptyDataTemplate>
                                                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle CssClass="tab" />
                                                        <RowStyle CssClass="tabrow" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </span>
                                    </p>
                                </fieldset>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Pesquisa de Lote">
                            <ContentTemplate>
                                <IPLM:IncPesquisarLoteMedicamento ID="inc_pesquisa" runat="server"></IPLM:IncPesquisarLoteMedicamento>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </span>
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
