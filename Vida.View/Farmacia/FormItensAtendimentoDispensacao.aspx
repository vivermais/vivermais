<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensAtendimentoDispensacao.aspx.cs" Inherits="Vida.View.Farmacia.FormItensAtendimentoDispensacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <fieldset>
	    <legend>Medicamentos Dispensados</legend>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                        OnRowCommand="verificar_acao" DataKeyNames="CodigoLoteMedicamento" 
                        Width="739px" CellPadding="4" ForeColor="#333333" GridLines="None" >
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="NomeLoteMedicamento" HeaderText="Lote" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QtdDias" HeaderText="Qtd Dias" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QtdPrescrita" HeaderText="Qtd Prescrita" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="QtdDispensada" HeaderText="Qtd Dispensada" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Fabricante" HeaderText="Fabricante" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:ButtonField ButtonType="Link" CommandName="c_alterar" Text="Alterar" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#bind("CodigoLoteMedicamento") %>' OnClientClick="return confirm('Tem certeza que deseja exluir este medicamento ?');" OnClick="e_excluir_medicamento">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                    
                    <br />
                    <table id="table_Medicamento" runat="server" visible="false">
                        <tr valign="top" >
                            <td valign="top" align="left">
                                <asp:Label ID="lbMedicamento" runat="server" Font-Bold="True" 
                                    ForeColor="Black" Text="Medicamento" ></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                 <asp:Label ID="lbNomeMedicamento" runat="server" Font-Bold="True" 
                                    ForeColor="Black" Font-Italic="True" ></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top" >
                            <td valign="top" align="left">
                                <asp:Label ID="lbQuantidadeDias" runat="server" Text="Qtd Dias" 
                                    Font-Bold="True" ForeColor="Black"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="tbxQuantidadeDias" CssClass="campo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top" >
                            <td valign="top" align="left">
                                <asp:Label ID="lbQuantidadePrescrita" runat="server" Text="Qtd Prescrita" 
                                    Font-Bold="True" ForeColor="Black"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="tbxQuantidadePrescrita" ReadOnly="true" CssClass="campo" runat="server">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top" >
                            <td valign="top" align="left">
                                <asp:Label ID="lbQuantidadeDispensada" runat="server" Text="Qtd Dispensada" 
                                    Font-Bold="True" ForeColor="Black"></asp:Label>
                            </td>
                            <td valign="top" align="left">
                                <asp:TextBox ID="tbxQuantidadeDispensada" CssClass="campo" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr valign="top"  id="linha_label_observacao" runat="server" visible="false">
                            <td colspan="2" valign="top" align="left">
                                <asp:Label ID="lbObservacao" runat="server" Font-Bold="True" ForeColor="Black" Text="Observação de Adiantamento de Entrega"></asp:Label>
                            </td>
                        </tr>
                        <tr valign="top" id="linha_textbox_observacao" runat="server" visible="false">
                            <td colspan="2" valign="top" align="left">
                                <asp:TextBox ID="tbxObservacao" runat="server" TextMode="MultiLine" Rows="5" Columns="2"></asp:TextBox>   
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                
                <asp:Panel ID="Panel2" runat="server" Visible="false">
                    <asp:Label ID="Label1" runat="server" 
                        Text="A quantidade dispensada para o medicamento é superior ou igual a 300 unidades. Tem certeza que deseja realizar a alteração ?" 
                        Font-Bold="True" ForeColor="Black"></asp:Label>
                </asp:Panel>
                
                <table id="table_Botoes_Atualizar" runat="server" visible="false">
                    <tr valign="top" >
                        <td valign="top" align="center">
                            <asp:Button ID="btn_Salvar" CssClass="bts" runat="server" ValidationGroup="group_validaDispensacao" CommandArgument="c_inserir" OnClick="btn_Salvar_Click" Text="Atualizar" />
                            <asp:Button ID="btn_Cancelar"  CssClass="bts" runat="server" OnClick="btn_Cancelar_Click" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
                
                <table id="table_Botoes_Confirmar" runat="server" visible="false">
                    <tr valign="top">
                        <td valign="top" align="center">
                            <asp:Button ID="btn_Confirmar" CssClass="bts" runat="server" ValidationGroup="group_validaDispensacao" CommandArgument="c_confirmar" OnClick="btn_Salvar_Click" Text="Confirmar" />
                            <asp:Button ID="btn_Cancelar_Confirmacao"  CssClass="bts" runat="server" OnClick="btn_Cancelar_Confirmacao_Click" Text="Cancelar" />
                        </td>
                    </tr>
                </table>
                
                <div>
                    
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
                            
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                        ControlToValidate="tbxObservacao" Display="None"
                        ErrorMessage="Campo Observação é Obrigatório!" 
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
                      ValidationGroup="group_validaDispensacao" ShowMessageBox="true" ShowSummary="false" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
	</fieldset>
</asp:Content>
