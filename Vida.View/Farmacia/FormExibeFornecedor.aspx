﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeFornecedor.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeFornecedor" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Lista de Fornecedores Cadastrados</h2>
                <fieldset class="formulario">
                    <legend>Fornecedores</legend>
                    <p>
                         <span>
                            Pressione o botão abaixo para cadastrar um novo fornecedor. </span></p>
                            <div class="botoesroll">
                            <asp:LinkButton ID="Button_Novo" runat="server"  PostBackUrl="~/Farmacia/FormFornecedor.aspx">
                  <img id="imgnregistro" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgnregistro.src='img/btn/novoregistro1.png';"
                  onmouseout="imgnregistro.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                            </div>
                        
                    
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Fornecedor" runat="server" AutoGenerateColumns="false" AllowPaging="true" Width="100%" Font-Size="x-small"
                             OnPageIndexChanging="OnPageIndexChanging_Paginacao" PageSize="20" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <asp:BoundField HeaderText="CNPJ" DataField="Cnpj" />
                                    <asp:HyperLinkField HeaderText="Razão Social" DataNavigateUrlFields="Codigo"
                                     DataNavigateUrlFormatString="~/Farmacia/FormFornecedor.aspx?co_fornecedor={0}"
                                     DataTextField="RazaoSocial"/>
                                     <asp:BoundField HeaderText="Nome Fantasia" DataField="NomeFantasia" />
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
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>