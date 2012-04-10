﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormExibeFabricante.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeFabricante" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="top">
            <h2>Lista de Fabricantes Cadastrados</h2>
            <fieldset class="formulario">
                <legend>Fabricantes</legend>
                <p>
                     <span>
                        <h4>Pressione o botão abaixo para cadastrar um novo fabricante.</h4>
                        <asp:LinkButton ID="Button_Novo" runat="server" PostBackUrl="~/Farmacia/FormFabricante.aspx" >
                  <img id="imgnfabricante" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgnfabricante.src='img/btn/novoregistro2.png';"
                  onmouseout="imgnfabricante.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                     </span>
                     <p>
                     </p>
                     <p>
                         <span>
                         <asp:GridView ID="GridView_Fabricante" runat="server" AllowPaging="true" Width="100%"
                             AutoGenerateColumns="false" OnPageIndexChanging="OnPageIndexChanging_Paginacao" 
                             PagerSettings-Mode="Numeric" PageSize="20" Font-Size="X-Small">
                             <Columns>
                                 <asp:HyperLinkField DataNavigateUrlFields="Codigo" 
                                     DataNavigateUrlFormatString="~/Farmacia/FormFabricante.aspx?co_fabricante={0}" 
                                     DataTextField="Nome" HeaderText="Fabricante" />
                             </Columns>
                             <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                             
                             <EmptyDataRowStyle HorizontalAlign="Center" />
                             <EmptyDataTemplate>
                                 <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                             </EmptyDataTemplate>
                         </asp:GridView>
                         </span>
                     </p>
                </p>
            </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
