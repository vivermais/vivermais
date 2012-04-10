﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeFarmacia.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormExibeFarmacia" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">--%>
    <%--<ContentTemplate>--%>
    <div id="top">
        <h2>
            Lista de Farmácias Cadastradas</h2>
        <fieldset class="formulario">
            <legend>Relação<%--Unidade:<asp:Label ID="Label_Unidade" runat="server" Text=""></asp:Label>--%>
            </legend>
            <p>
                <h4>
                        Pressione o botão abaixo para cadastrar uma nova farmácia.</h4><span>
                    
                    <asp:LinkButton ID="Button_Novo" runat="server" PostBackUrl="~/Farmacia/FormFarmacia.aspx" >
                  <img id="imgsalvar" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgsalvar.src='img/btn/novoregistro2.png';"
                  onmouseout="imgsalvar.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                </span>
            </p>
            <p>
                <span class="rotulo">Estabelecimento de Saúde</span> <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_Unidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFarmacias" CssClass="campo" Height="23px">
                        <asp:ListItem Text="Selecione..." Value="-1" ></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Unidade" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="Panel_Farmacia" runat="server" Visible="false">
                        <p>
                            <span>
                                <asp:GridView ID="GridView_Farmacia" runat="server" AutoGenerateColumns="false" Width="100%" Font-Size="x-Small">
                                    <Columns>
                                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Farmacia/FormFarmacia.aspx?co_farmacia={0}"
                                            DataTextField="Nome" HeaderText="Farmácia" />
                                    </Columns>
                                    <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Left"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Left" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataRowStyle HorizontalAlign="Left" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </span>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
