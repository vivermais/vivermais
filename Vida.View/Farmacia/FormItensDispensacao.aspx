<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensDispensacao.aspx.cs" Inherits="Vida.View.Farmacia.FormItensDispensacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ContentPlaceHolderID="head" ID="c_head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 759px;
        }
        .style3
        {
            width: 765px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    
    <fieldset>
        <legend>Cadastro de Medicamentos na Dispensação</legend>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
	        <ContentTemplate>
	            <div>
	                <p>
	                    <span class="rotulo">Nº da Receita</span>
                        <asp:TextBox ID="tbxNumeroReceita" runat="server" ReadOnly="true"></asp:TextBox>
	                </p>
	                <p>
                        <span class="rotulo">Farmácia</span>
                        <asp:DropDownList ID="ddlFarmacia" runat="server">
                        </asp:DropDownList>
                    </p>
                    
                    <p>
                        <span class="rotulo">Cartão SUS</span>
                        <asp:TextBox ID="tbxCartaoSUS" runat="server" ReadOnly="true"></asp:TextBox>
                    </p>
                    
                    <p>
                        <span class="rotulo">Paciente</span>
                        <asp:TextBox ID="tbxPaciente" runat="server" ReadOnly="true"></asp:TextBox>
                    </p>
                    
                    <p>
                        <span class="rotulo">Data de Atendimento:</span>
                        <asp:TextBox ID="tbxDataAtendimento" runat="server" Height="21px" Width="120px" ReadOnly="true"></asp:TextBox>
                    </p>
	            </div>
	            
	            <asp:Panel ID="Panel_um" runat="server" >
	                <div>
                      <br />
                        <span>Selecione uma letra</span>
                        
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="A" Text="A" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="B" Text="B" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="C" Text="C" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="D" Text="D" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton5" runat="server" CommandArgument="E" Text="E" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton6" runat="server" CommandArgument="F" Text="F" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton7" runat="server" CommandArgument="G" Text="G" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton8" runat="server" CommandArgument="H" Text="H" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton9" runat="server" CommandArgument="I" Text="I" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton10" runat="server" CommandArgument="J" Text="J" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton24" runat="server" CommandArgument="K" Text="K" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton11" runat="server" CommandArgument="L" Text="L" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton12" runat="server" CommandArgument="M" Text="M" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton13" runat="server" CommandArgument="N" Text="N" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton14" runat="server" CommandArgument="O" Text="O" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton15" runat="server" CommandArgument="P" Text="P" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton16" runat="server" CommandArgument="Q" Text="Q" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton17" runat="server" CommandArgument="R" Text="R" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton18" runat="server" CommandArgument="S" Text="S" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton19" runat="server" CommandArgument="T" Text="T" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton20" runat="server" CommandArgument="U" Text="U" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton21" runat="server" CommandArgument="V" Text="V" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton25" runat="server" CommandArgument="W" Text="W" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton22" runat="server" CommandArgument="X" Text="X" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton26" runat="server" CommandArgument="Y" Text="Y" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        <asp:LinkButton ID="LinkButton23" runat="server" CommandArgument="Z" Text="Z" OnClick="e_buscar_medicamento_letra"></asp:LinkButton>
                        
                        <br />
                        
                        <span>Ou informe o Nome do Medicamento</span>
                        <asp:TextBox ID="tbxNomeMedicamento" runat="server"></asp:TextBox>
                        <asp:Button ID="Button2" CssClass="bts" runat="server" Text="Buscar" OnClick="e_bucsar_medicamento_nome" ValidationGroup="group_BuscaMedicamentoNome" />                      
                        <p>
                            <br />
                        </p>
                    </div>
                    
                    <div>
                         <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                          AutoGenerateColumns="False" OnPageIndexChanging="e_paginacao" 
                          OnRowCommand="e_verificar_acao" PagerSettings-Mode="Numeric" 
                          PagerSettings-Visible="true" PageSize="4" Width="739px" CellPadding="4" 
                             ForeColor="#333333" GridLines="None">
                             <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeLote" HeaderText="Lote" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeFabricante" HeaderText="Fabricante" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataValidadeLote" HeaderText="Validade" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QuantidadeEstoque" HeaderText="Qtd Disponível" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Link" CommandName="c_incluir" Text="Selecione" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                            </Columns>
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                             <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                             <EditRowStyle BackColor="#2461BF" />
                             <AlternatingRowStyle BackColor="White" />
                         </asp:GridView>
                    </div>
                    
                    <div>
                        <br />
                        <table>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Medicamento" Font-Bold="True" 
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td valign="top">
                                    <asp:Label ID="lbMedicamentoEscolhido" runat="server" Text="Nenhum" 
                                        Font-Bold="True" Font-Italic="true" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Qtd de Dias" Font-Bold="True" 
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxQuantidadeDias" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Qtd Prescrita" Font-Bold="True" 
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxQuantidadePrescrita" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Qtd Dispensada" Font-Bold="True" 
                                        ForeColor="Black"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="tbxQuantidadeDispensada" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        
                        <table runat="server" id="tabelaObservacao" visible="false">
                            <tr valign="top">
                                <td align="justify" valign="top">
                                    <asp:Label ID="lbConteudo" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td align="justify">
                                    <asp:TextBox ID="tbxObservacao" runat="server" TextMode="MultiLine" Columns="7" 
                                        Rows="5" Width="238px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        
                        <table>
                            <tr>
                                <td align="center" valign="top" class="style1">
                                    
                                    <asp:Button ID="btn_Salvar_Medicamento" CssClass="bts" runat="server" Height="20px" 
                                        OnClick="btn_Salvar_Medicamento_click" CommandArgument="c_inserir" Text="Adicionar" Width="118px" 
                                        ValidationGroup="group_validaDispensacao" />
                                          
                                    <asp:Button ID="btn_Confirmar_Medicamento" CssClass="bts" runat="server" Height="20px" Visible="false" 
                                        OnClick="btn_Salvar_Medicamento_click"  CommandArgument="c_confirmar_observacao" Text="Adicionar" Width="118px" 
                                        ValidationGroup="group_validaDispensacao" />
                                            
                                    <asp:Button ID="btn_Alterar_Medicamento" CssClass="bts" runat="server" Height="20px" 
                                        OnClick="btn_Salvar_Medicamento_click" CommandArgument="c_atualizar" Text="Atualizar" Width="118px" 
                                        ValidationGroup="group_validaDispensacao" Visible="false" />
                                        
                                    <asp:Button ID="btn_Cancelar_Medicamento" CssClass="bts" runat="server" Height="20px" 
                                        OnClick="btn_Cancelar_Medicamento_click" Text="Cancelar" Width="118px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    
                    <div>
                        <br/>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            OnRowCommand="e_atualizar_itens_escolhidos" Width="756px" CellPadding="4" 
                            ForeColor="#333333" GridLines="None">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="LoteMedicamento" HeaderText="Lote" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QtdDias" HeaderText="Qtd de Dias" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QtdPrescrita" HeaderText="Qtd Prescrita" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QtdDispensada" HeaderText="Qtd Dispensada" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:BoundField>
                                <asp:ButtonField ButtonType="Link" CommandName="c_alterar" Text="Alterar" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                                <asp:ButtonField ButtonType="Link" CommandName="c_excluir" Text="Excluir" 
                                    ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" >
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:ButtonField>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        
                        <table>
                            <tr>
                                <td align="center" valign="top" class="style1">
                                    <asp:Button ID="Button1" runat="server" CssClass="bts"
                                    OnClick="btn_Salvar_click" Text="Concluir Dispensação" />
                                    
                                    <asp:Button ID="Button3" runat="server" CssClass="bts"
                                    OnClick="btn_Cancelar_click" Text="Cancelar Dispensação" />
                                </td>
                            </tr>
                        </table>
                    </div>
	            </asp:Panel>
	            
	            <asp:Panel ID="Panel_dois" runat="server" Visible="false">
	                <table>
	                    <tr>
	                        <td valign="top" align="center">
                                <asp:Label ID="lb_msg" runat="server" 
                                    Text="A quantidade dispensada para o medicamento é superior ou igual a 300 unidades. Tem certeza que deseja realizar a dispensação ?" 
                                    Font-Bold="True" ForeColor="Black"></asp:Label>
	                        </td>
	                    </tr>
	                    <tr>
	                        <td valign="top" align="center" class="style3">
	                             <asp:Button ID="btn_Confirmar_Quantidade_Medicamento" CssClass="bts" runat="server" Height="20px" 
                                        OnClick="btn_Confirmar_Quantidade_Medicamento_click" Text="Confirmar" Width="118px" />
                                 <asp:Button ID="btn_Cancelar_Quantidade_Medicamento" CssClass="bts" runat="server" Height="20px" 
                                        OnClick="btn_Cancelar_Quantidade_Medicamento_click"  Text="Cancelar" Width="118px" />
	                        </td>
	                    </tr>
	                </table>
	            </asp:Panel>
                    
                <div>
                    <p>&nbsp;</p>
                    
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToValidate="tbxQuantidadeDias" ValidationGroup="group_validaDispensacao"
                        Display="None" ValueToCompare="0" Operator="GreaterThan"
                        ErrorMessage="A quantidade de dias deve ser maior que zero."></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToValidate="tbxQuantidadePrescrita" ValidationGroup="group_validaDispensacao"
                        Display="None" ValueToCompare="0" Operator="GreaterThan"
                        ErrorMessage="A quantidade prescrita deve ser maior que zero."></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToValidate="tbxQuantidadeDispensada" ValidationGroup="group_validaDispensacao"
                        Display="None" ValueToCompare="0" Operator="GreaterThan"
                        ErrorMessage="A quantidade dispensada dever ser maior que zero."></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                        ControlToValidate="tbxObservacao" Display="None"
                        ErrorMessage="Campo Observação é Obrigatório!"
                        ValidationGroup="group_validaDispensacao"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="tbxQuantidadeDias" Display="None" 
                        ErrorMessage="Campo Quantidade de Dias é Obrigatório!" 
                        ValidationGroup="group_validaDispensacao"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="tbxQuantidadePrescrita" Display="None" 
                        ErrorMessage="Campo Quantidade Prescrita é Obrigatório!" 
                        ValidationGroup="group_validaDispensacao"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                        ControlToValidate="tbxQuantidadeDispensada" Display="None" 
                        ErrorMessage="Campo Quantidade Dispensada é Obrigatório!" 
                        ValidationGroup="group_validaDispensacao"></asp:RequiredFieldValidator>
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                     ErrorMessage="Campo Quantidade de Dias deve conter somente números!" Display="None"
                     ValidationGroup="group_validaDispensacao"  ValidationExpression="\d*"
                     ControlToValidate="tbxQuantidadeDias"></asp:RegularExpressionValidator>
                    
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                     ErrorMessage="Campo Quantidade Prescrita deve conter somente números!" Display="None"
                     ValidationGroup="group_validaDispensacao"  ValidationExpression="\d*"
                     ControlToValidate="tbxQuantidadePrescrita"></asp:RegularExpressionValidator>
                     
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                     ErrorMessage="Campo Quantidade Dispensada deve conter somente números!" Display="None"
                     ValidationGroup="group_validaDispensacao"  ValidationExpression="\d*"
                     ControlToValidate="tbxQuantidadeDispensada"></asp:RegularExpressionValidator>

                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                        ShowMessageBox="true" ShowSummary="false" 
                        ValidationGroup="group_validaDispensacao" />
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" 
                        ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="group_BuscaMedicamentoNome" />    
                    <p>&nbsp;</p>
                </div>
                    
	        </ContentTemplate>
	    </asp:UpdatePanel>
    
    </fieldset>
    
</asp:Content>