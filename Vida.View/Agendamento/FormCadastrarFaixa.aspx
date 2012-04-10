﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="True" CodeBehind="FormCadastrarFaixa.aspx.cs"
 Inherits="ViverMais.View.Agendamento.FormCadastrarFaixa" Title="Módulo Regulação - Formulário de Cadastro de Faixas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
    <h2>Formulário de Cadastro de Faixas</h2>
	<fieldset class="formulario">
					<legend>Cadastrar</legend>
				<p>
				<span class="rotulo">Faixa Inicial:</span>
                            <span><asp:TextBox ID="tbxFaixaInicial"  CssClass="campo" 
                        runat="server" MaxLength="7"></asp:TextBox>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxFaixaInicial" Display="Dynamic" 
                        ErrorMessage="* Informe a Faixa Inicial"></asp:RequiredFieldValidator>
				    <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxFaixaInicial" Display="Dynamic" 
                        ErrorMessage="* Somente Números" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
				</span>
                    </p>
                    
				<p>
				<span class="rotulo">Faixa Final:</span>
				<span>
                    <asp:TextBox ID="tbxFaixaFinal" CssClass="campo" name="faixafinal" runat="server" 
                        MaxLength="7" AutoPostBack="True" 
                        ontextchanged="tbxFaixaFinal_TextChanged"></asp:TextBox>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxFaixaFinal" Display="Dynamic" 
                        ErrorMessage="* Informe a Faixa Final"></asp:RequiredFieldValidator>
				    <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxFaixaFinal" Display="Dynamic" 
                        ErrorMessage="* Somente Números" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></span>
				</p>
				<p>
				<span class="rotulo">Quantitativo da Faixa</span>
                    <asp:Label ID="lblQtd" runat="server" Text="-" Font-Bold="true" Font-Size="13px"></asp:Label>
				</p>
				<p>
				<span class="rotulo">Tipo:</span>
				<span><asp:DropDownList ID="ddlTipo" CssClass="campo" Height="21px" runat="server">
                        <asp:ListItem Value="0">Selecione...</asp:ListItem>
                        <asp:ListItem Value="A">APAC</asp:ListItem>
                    </asp:DropDownList>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="ddlTipo" Display="Dynamic" ErrorMessage="* Selecione o Tipo" 
                        InitialValue="0"></asp:RequiredFieldValidator></span>
				</p>
				<p>
				<span class="rotulo">Ano de Vigencia:</span>
                            <span><asp:TextBox ID="tbxAnoVigencia"  CssClass="campo" 
                        runat="server" MaxLength="4"></asp:TextBox>
				    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxAnoVigencia" Display="Dynamic" 
                        ErrorMessage="* Informe o Ano de Vigencia"></asp:RequiredFieldValidator>
				    <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="tbxAnoVigencia" Display="Dynamic" 
                        ErrorMessage="* Somente Números" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
				</span>
                    </p>
				
				<div class="botoesroll"><asp:LinkButton ID="btnCadastrar" runat="server" onclick="Incluir_Click">
                <img id="imgcadastra" alt="" src="img/cadastrar_1.png"
                onmouseover="imgcadastra.src='img/cadastrar_2.png';"
                onmouseout="imgcadastra.src='img/cadastrar_1.png';" />
            </asp:LinkButton>
           </div>
                 <div class="botoesroll">  
              <asp:LinkButton ID="btnVoltar" runat="server" onclick="Incluir_Click" PostBackUrl="~/Agendamento/Default.aspx" 
                        CausesValidation="False">
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton>
				</div> 
				</fieldset>
        <asp:Panel ID="PanelListaFeriados" runat="server">
            <fieldset class="formulario">
                <legend>Lista de Faixas Cadastradas</legend>
                <asp:GridView ID="GridViewListaFaixas" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    OnPageIndexChanging="GridViewListaFaixas_PageIndexChanging" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" Width="100%" >
                                    <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                    <RowStyle BackColor="#a6c5de" ForeColor="Black" Font-Size="11px" />
                    <Columns>
                        <asp:BoundField DataField="Codigo">
                            <HeaderStyle CssClass="colunaEscondida" />
                            <ItemStyle CssClass="colunaEscondida" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="FaixaInicial" HeaderText="Faixa Inicial" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="FaixaFinal" HeaderText="Faixa Final" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Quantitativo" HeaderText="Quantidade de Faixa" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="QuantidadeDisponivel" HeaderText="Quantidade Disponível" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                        <asp:BoundField DataField="Ano_vigencia" HeaderText="Ano de Vigência" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="Label1" runat="server" Text="Nenhum Registro Encontrado!" Font-Size="X-Small"></asp:Label>
                    </EmptyDataTemplate>
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#c2dcf2" />
                </asp:GridView>
            </fieldset>
        </asp:Panel>
			</div>
			
</asp:Content>
