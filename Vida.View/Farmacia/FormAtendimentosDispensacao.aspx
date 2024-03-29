﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAtendimentosDispensacao.aspx.cs" Inherits="ViverMais.View.Farmacia.FormAtendimentosDispensacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="top">
    <fieldset>
	    <legend>Atendimentos realizados para esta receita</legend>
	    <table>
	        <tr>
	            <td>
	                <span class="rotulo">N º da Receita</span>
	                <asp:Label ID="lbReceita" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
	            </td>
	            <td>
	                <span class="rotulo">Data da Receita</span>
	                <asp:Label ID="lbDataReceita" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
	            </td>
	        </tr>
	        <tr>
	            <td>
	                <span class="rotulo">Paciente</span>
	                <asp:Label ID="lbPaciente" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
	            </td>
	            <td>
	                <span class="rotulo">Cartão SUS</span>
	                <asp:Label ID="lbCartaoSUS" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
	            </td>
	        </tr>
	        <tr>
	            <td>
	                <span class="rotulo">Profissional</span>
	                <asp:Label ID="lbProfissional" runat="server" Font-Bold="True" 
                        ForeColor="Black"></asp:Label>
	            </td>
	        </tr>
	        <tr>
	            <td>
	                <span class="rotulo">Distrito Origem</span>
	                <asp:Label ID="lbDistritoOrigem" runat="server" Font-Bold="True" 
                        ForeColor="Black"></asp:Label>
	            </td>
	        </tr>
	        <tr>
	            <td>
	                <span class="rotulo">Munícipio Origem</span>
	                <asp:Label ID="lbMunicipioOrigem" runat="server" Font-Bold="True" 
                        ForeColor="Black"></asp:Label>
	            </td>
	        </tr>
	    </table>
	    <br />
	    <table>
	        <tr>
	            <td>
	                <asp:GridView ID="GridView1" runat="server" OnRowCommand="verificar_acao"
	                 AutoGenerateColumns="False" Width="736px" CellPadding="4" ForeColor="#333333" 
                     DataKeyNames="CodigoFarmacia" GridLines="None">
	                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
	                    <Columns>
	                        <asp:BoundField DataField="Farmacia" HeaderText="Farmácia" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
	                        <asp:BoundField DataField="DataAtendimento" HeaderText="Data de Atendimento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
	                        <asp:ButtonField ButtonType="Link" CommandName="c_visualizar" Text="Visualizar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
	                    </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>    
	            </td>
	        </tr>
	    </table>
	</fieldset>
	</div>
</asp:Content>
