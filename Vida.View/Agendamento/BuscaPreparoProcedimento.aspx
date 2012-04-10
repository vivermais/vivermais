﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="True"
    CodeBehind="BuscaPreparoProcedimento.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaPreparoProcedimento"
    Title="Módulo Regulação - Cadastro de Preparos de Procedimentos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="cc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="Up1" runat="server">
        <ContentTemplate>
            <div id="top">
            <h2>Cadastro de Preparos de Procedimentos</h2>
            <br />
            <div class="botoesroll">
               <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Agendamento/FormPreparoProcedimento.aspx">
                       <img id="img_npreparo" alt="Cadastrar um Novo Preparo de Procedimento" src="img/cadastrar_n_preparo_1.png"
                onmouseover="img_npreparo.src='img/cadastrar_n_preparo_2.png';"
                onmouseout="img_npreparo.src='img/cadastrar_n_preparo_1.png';" />
            </asp:LinkButton>
            </div>
            <br />
            <fieldset class="formulario">
                <legend>Lista de Preparos de Procedimentos </legend>
                    <p>
                    &nbsp
                    <asp:GridView ID="GridViewPreparo" runat="server" AllowPaging="True" 
                        AutoGenerateColumns="False" EnableSortingAndPagingCallbacks="True" OnRowCommand="GridViewPreparo_RowCommand"
                        OnPageIndexChanging="GridViewPreparo_PageIndexChanging" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                        CellPadding="3" GridLines="Vertical" Width="100%">
                  <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                    <RowStyle BackColor="#a6c5de" ForeColor="Black" Font-Size="11px" />
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" 
                                DataNavigateUrlFormatString="FormPreparoProcedimento.aspx?codigo={0}" 
                                DataTextField="Descricao" HeaderText="Descrição" ItemStyle-CssClass="" 
                                ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="cmdDelete" runat="server" 
                                        CommandArgument='<%# Eval("Codigo") %>' CommandName="Excluir" 
                                        OnClientClick="javascript : return confirm('Tem certeza que deseja excluir este Preparo?');" 
                                        Text="">
                                    <asp:Image ID="Excluir" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label runat="server" Text="Nenhum Registro Encontrado." Font-Size="X-Small"></asp:Label>
                        </EmptyDataTemplate>
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#c2dcf2" />
                    </asp:GridView>
                </p>
                <br />

                    <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                       <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton>
              
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
