﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true" CodeBehind="BuscaAfastamentoEAS.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaAfastamentoEAS" Title="Módulo Regulação - Busca Afastamento de Estabelecimento de Saúde" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
    <h2>Busca Afastamento de Estabelecimento de Saúde</h2>
    <fieldset class="formulario">
        <legend>Formulário Pesquisa</legend>
		<div>
               <asp:LinkButton ID="btnNovoAfastamento" runat="server" 
                 Font-Size="X-Small" PostBackUrl="~/Agendamento/FormAfastarEAS.aspx">
		        <img id="imgnovoafast" alt="Cancelar" src="img/btn-cad-novo-afast1.png"
                  onmouseover="imgnovoafast.src='img/btn-cad-novo-afast2.png';"
                  onmouseout="imgnovoafast.src='img/btn-cad-novo-afast1.png';" />
		</asp:LinkButton>
			</div>
			<br /><br />
        <asp:UpdatePanel ID="UpdatePanelPequisaEstabalecimento" runat="server" ChildrenAsTriggers="true">
            <Triggers>
               <asp:AsyncPostBackTrigger ControlID="btnPesquisarEstabelecimento" EventName="Click" />
            </Triggers>
            <ContentTemplate>    
                <fieldset class="formularioMenor">
                     <legend>Pesquisar Estabelecimento de Saúde</legend>
                     <p>
                       <span class="rotulo">CNES</span>
                       <span>
                           <asp:TextBox ID="tbxCNES" runat="server" CssClass="campo"></asp:TextBox>
                       </span>
                       <span>
                           <asp:CompareValidator ID="CompareValidator1" runat="server" 
                             ErrorMessage="Digite somente números no CNES" Font-Size="X-Small" 
                             ControlToValidate="tbxCNES" Type="Integer" Display="Dynamic" Operator="DataTypeCheck"></asp:CompareValidator>                           
                       </span>
                     </p>
                     <p>
                       <span class="rotulo">Descrição</span>
                       <span>
                           <asp:TextBox ID="tbxDescricao" runat="server" CssClass="campo" Width="302px"></asp:TextBox>
                       </span>
                     </p>
                     <p>
                         <asp:Label ID="lblResultado" runat="server" Text="" Font-Names="Verdana" Font-Size="X-Small"
                          ForeColor="Red"></asp:Label>
                     </p>
                     <p>
                           <div class="botoesroll">
                               <span>
                                   <asp:LinkButton ID="btnPesquisarEstabelecimento" runat="server" CausesValidation="false"
                                     onclick="btnPesquisarEstabelecimento_Click">
                                        <img id="img_pesquisar1" alt="" src="img/pesquisar_1.png"
                                        onmouseover="img_pesquisar1.src='img/pesquisar_2.png';"
                                        onmouseout="img_pesquisar1.src='img/pesquisar_1.png';" />                           
                                   </asp:LinkButton>
                               </span>
                           </div>  
                           <p>
                           </p>
                     </p>
                </fieldset>          
                <p>&nbsp;</p>
                <p>
                   <span class="rotulo">Estabelecimento de Saúde</span>
                   <span>
                       <asp:DropDownList ID="ddlEstabelecimentoSaude" runat="server" CssClass="drop" Width="300px">
                       </asp:DropDownList>
                   </span>
                </p>
                <p>
                  <span>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o estabelecimento de saúde" InitialValue="0"
                        Display="Dynamic" Font-Size="X-Small" SetFocusOnError="true" ControlToValidate="ddlEstabelecimentoSaude">
                      </asp:RequiredFieldValidator>
                  </span>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>

               <div class="botoesroll">
                      <asp:LinkButton ID="btnPesquisarAfastamento" runat="server" 
                       CausesValidation="false" onclick="btnPesquisarAfastamento_Click">
                            <img id="img_pesquisar2" alt="" src="img/pesquisar_1.png"
                            onmouseover="img_pesquisar2.src='img/pesquisar_2.png';"
                            onmouseout="img_pesquisar2.src='img/pesquisar_1.png';" />                           
                       </asp:LinkButton>
               </div>            

        <br />
        <asp:Panel ID="PanelAgendamento" runat="server" Visible="false">
        <fieldset class="formularioMedio">
            <legend>Afastamentos</legend>
            <p>
               <span class="rotulo">CNES:</span>
               <span>
                    <asp:Label ID="lbCNES" runat="server" CssClass="label" Text=""></asp:Label>
               </span>
            </p>
            <p>
               <span class="rotulo">Estabelecimento Saúde:</span>
               <span>
                    <asp:Label ID="lbEstabelecimentoSaude" runat="server" CssClass="label" Text=""></asp:Label>
               </span>
            </p>            
            <asp:GridView ID="GridViewAfastamentos" runat="server" Font-Size="X-Small" Font-Names="Verdana"
               AutoGenerateColumns="False" onrowcommand="GridViewAfastamentos_RowCommand" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" Width="100%">
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
               <Columns>
                 <asp:BoundField DataField="Codigo">
                     <ItemStyle CssClass="colunaEscondida" />
                     <HeaderStyle CssClass="colunaEscondida" />
                     <FooterStyle CssClass="colunaEscondida" />
                 </asp:BoundField>                           
                 <asp:ButtonField DataTextField="DataInicialToString" HeaderText="Data Inicial"
                      CommandName="EditarAfastamento">
                     <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Bottom" />
                 </asp:ButtonField>
                 <asp:BoundField DataField="DataReativacaoToString" HeaderText="Data de Reativacao">
                     <ItemStyle Width="70px" HorizontalAlign="Center" VerticalAlign="Bottom" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Motivo" HeaderText="Motivo">
                     <ItemStyle Width="258px" HorizontalAlign="Left" VerticalAlign="Bottom" />
                 </asp:BoundField>
                 <asp:BoundField DataField="Obs" HeaderText="Observação">
                     <ItemStyle Width="257px" HorizontalAlign="Left" VerticalAlign="Bottom" />
                 </asp:BoundField>
               </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />            
            </asp:GridView>
        </fieldset>
        </asp:Panel>
    </fieldset>
  </div>    
</asp:Content>
