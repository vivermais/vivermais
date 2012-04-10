﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeUnidadeMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeUnidadeMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_heade" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<asp:UpdatePanel ID="UpdatePanel_Body" runat="server">
    <ContentTemplate>
    <div id="top">
        <h2>Lista de Unidades Cadastradas</h2>
        <fieldset class="formulario">
            <legend>Forma Farmacêutica</legend>
            <p>
                <span>
                    <h4>Pressione o botão abaixo para cadastrar uma nova unidade de medida.</h4>
                     <asp:LinkButton ID="Button_NovaUnidade" runat="server" PostBackUrl="~/Farmacia/FormUnidadeMedidaMedicamento.aspx" >
                  <img id="imgsalvar" alt="" src="img/btn/novoregistro1.png"
                  onmouseover="imgsalvar.src='img/btn/novoregistro2.png';"
                  onmouseout="imgsalvar.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                </span>
                <p>
                </p>
                <p>
                    <span>
                    <asp:GridView ID="GridView_UnidadeMedida" runat="server" AllowPaging="true" 
                        AutoGenerateColumns="false" DataKeyNames="Codigo" Width="100%"
                        OnPageIndexChanging="OnPageIndexChanging_Paginacao" BorderColor="Silver"
                        OnRowCommand="OnRowCommand_Acao" PagerSettings-Mode="Numeric" PageSize="20" Font-Size="x-Small" Font-Bold="false" >
                        <Columns>
                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_Editar" 
                                DataTextField="Sigla" HeaderText="Sigla" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Nome" HeaderText="Nome" 
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Excluir" runat="server" 
                                        CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_ExcluirUnidadeMedida" 
                                        OnClientClick="javascript:return confirm('Tem certeza que deseja excluir esta unidade de medida ?');">                                        <img src="img/excluirr.png" alt="Excluir" />                                        </asp:LinkButton>                                </ItemTemplate>                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                   
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    </span>
                </p>
                <p>
                </p>
                <p>
                </p>
                <p>
                </p>
            </p>
        </fieldset>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>