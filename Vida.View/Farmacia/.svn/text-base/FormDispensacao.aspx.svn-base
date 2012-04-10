<%@ Page Title="" Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true" CodeBehind="FormDispensacao.aspx.cs" Inherits="Vida.View.Farmacia.FormDispensacao" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 739px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
	<fieldset>
	    <legend>Cadastro de Receita da Dispensação</legend>
	    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
	    <Triggers>
	        <asp:PostBackTrigger ControlID="ImageButton1" />
	        <asp:PostBackTrigger ControlID="ImageButton2" />
	    </Triggers>
	    <ContentTemplate>
	        <asp:Panel ID="painel_um" runat="server">
	            <p>
                    <span class="rotulo">Cartão SUS:</span>
		            <asp:TextBox CssClass="campo" ID="tbxCartaoSUS" runat="server" Height="21px" 
                      MaxLength="15"  Width="120px"></asp:TextBox>
                    
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageAlign="Top" ImageUrl="~/img/bt_pesq.png" OnClick="btn_Pequisar_Click" CommandArgument="paciente" ValidationGroup="group_pesqPaciente" />
                </p>		
		        <p>
		            <span class="rotulo">Nome do Paciente:</span>
                    <asp:TextBox CssClass="campo" ID="tbxPaciente" runat="server" Height="21px" ReadOnly="true" 
                      MaxLength="40"  Width="303px"></asp:TextBox>
                </p>
                
                <p>
                    <span class="rotulo">Data da Receita:</span>
                    <asp:TextBox ID="tbxDataReceita" CssClass="campo" runat="server" Height="21px" Width="120px" ></asp:TextBox>
                </p>
                
		        <p>
		            <span class="rotulo">Nº Conselho:</span>
                    <asp:TextBox CssClass="campo" ID="tbxNumeroConselho" runat="server" Height="21px" MaxLength="7"  Width="60px">
                    </asp:TextBox>
                    &nbsp;
                    <asp:DropDownList ID="ddlCategoria" runat="server"
                        DataTextField="Nome" DataValueField="Codigo">
                    </asp:DropDownList>
                   
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageAlign="Top" OnClick="btn_Pequisar_Click" CommandArgument="profissional" ValidationGroup="group_pesqProfissional"
                        ImageUrl="~/img/bt_pesq.png"  />
                   
                </p>        
                <p>
                    <span class="rotulo">Profissional:</span>
                    <asp:TextBox CssClass="campo" ID="tbxProfissional" runat="server" Height="21px" ReadOnly="true" 
                      MaxLength="40"  Width="303px"></asp:TextBox>
		        </p>
                <p>
                    <span class="rotulo">Distrito Origem:</span>
                    <asp:DropDownList ID="ddlDistrito" runat="server" Width="200" Height="180" CssClass="campo"
                        DataTextField="Nome" DataValueField="Codigo">
                    </asp:DropDownList>
		        </p>
                <p>
                    <span class="rotulo">Município Origem:</span>
                    <asp:DropDownList ID="ddlMunicipio" runat="server" Width="200" Height="180" CssClass="campo"
                        DataTextField="Nome" DataValueField="Codigo">
                    </asp:DropDownList>
		        </p>
    		    	
                <div id="botao">
                    <p>
                        <asp:Button CssClass="bts" ID="btnSalvar" runat="server" Text="Salvar" ValidationGroup="group_cadDispensacao"
                            onclick="btn_Salvar_Click" />
                        <asp:Button CssClass="bts" ID="bt_voltar" runat="server" Text="Voltar" 
                            PostBackUrl="~/Default.aspx" />
                    </p>
                </div>
	        </asp:Panel>
	        
            <br /><br /><br /><br />
            <asp:Panel ID="painel_dois" runat="server" Visible="false">
                <table>
                    <tr valign="top">
                        <td align="center" valign="top" class="style1">
                            <asp:Label ID="lbMensagem" runat="server" 
                                Text="A Data da Receita está superior a um período de 6 meses. Deseja continuar o cadastro da receita ?" 
                                Font-Bold="True" ForeColor="Black"></asp:Label>
                        </td>
                    </tr>
                    <tr valign="top">
                        <td align="center" valign="top" class="style1">
                            <asp:Button ID="btn_confirmar_liberacao" runat="server" Text="Confirmar Cadastro" CssClass="bts" OnClick="btn_confirmar_liberacao_Click" />
                            <asp:Button ID="btn_cancelar_liberacao" runat="server" Text="Cancelar" CssClass="bts" OnClick="btn_cancelar_liberacao_Click" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            
            <div>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                 TargetControlID="tbxDataReceita" Mask="99/99/9999" MaskType="Date">
                </cc1:MaskedEditExtender>
                
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="É preciso informar o Cartão SUS do paciente!" ControlToValidate="tbxCartaoSUS" ValidationGroup="group_pesqPaciente" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="É preciso informar o Cartão SUS do paciente!" ControlToValidate="tbxCartaoSUS" ValidationGroup="group_cadDispensacao" Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="É preciso informar a Data da Receita!" ControlToValidate="tbxDataReceita" Display="None" ValidationGroup="group_cadDispensacao"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Formato da Data da Receita inválido!" Operator="DataTypeCheck" ControlToValidate="tbxDataReceita" Display="None" ValidationGroup="group_cadDispensacao" Type="Date"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data da Receita deve ser maior que 01/01/1900!" Operator="GreaterThan" ValueToCompare="01/01/1900" ControlToValidate="tbxDataReceita" Display="None" ValidationGroup="group_cadDispensacao" Type="Date"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="É preciso informar o Nº Conselho do profissional!" ControlToValidate="tbxNumeroConselho" ValidationGroup="group_pesqProfissional" Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="É preciso escolher uma Categoria para o profissional!" ControlToValidate="ddlCategoria" ValidationGroup="group_pesqProfissional" ValueToCompare="0" Operator="GreaterThan" Display="None"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="É preciso informar o Nº Conselho do profissional!" ControlToValidate="tbxNumeroConselho" ValidationGroup="group_cadDispensacao" Display="None"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="É preciso escolher uma Categoria para o profissional!" ControlToValidate="ddlCategoria" ValidationGroup="group_cadDispensacao" ValueToCompare="0" Operator="GreaterThan" Display="None"></asp:CompareValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Campo Município de Origem Obrigatório!" ValueToCompare="0" Operator="GreaterThan" ValidationGroup="group_cadDispensacao" ControlToValidate="ddlMunicipio" Display="None"></asp:CompareValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O Cartão SUS deve conter 15 dígitos!" Display="None" ValidationGroup="group_cadDispensacao" ValidationExpression="\d{15}" ControlToValidate="tbxCartaoSUS" ></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="O Cartão SUS deve conter 15 dígitos!" Display="None" ValidationGroup="group_pesqPaciente" ValidationExpression="\d{15}" ControlToValidate="tbxCartaoSUS" ></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Nº Conselho deve conter somente números!" Display="None" ValidationGroup="group_pesqProfissional"  ValidationExpression="\d*" ControlToValidate="tbxNumeroConselho"></asp:RegularExpressionValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Nº Conselho deve conter somente números!" Display="None" ValidationGroup="group_cadDispensacao"  ValidationExpression="\d*" ControlToValidate="tbxNumeroConselho"></asp:RegularExpressionValidator>

                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="group_pesqPaciente" ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary3" runat="server" ValidationGroup="group_cadDispensacao" ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="group_pesqProfissional" ShowMessageBox="true" ShowSummary="false" />
            </div>
            
        </ContentTemplate>
        </asp:UpdatePanel>        
	</fieldset>
</asp:Content>
