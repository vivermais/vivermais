<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormBuscaNotaFiscal.aspx.cs" Inherits="ViverMais.View.Farmacia.FormBuscaNotaFiscal" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
            <div id="top">
                <h2>Pesquisa de Nota Fiscal</h2>
            
            <fieldset class="formulario">
                <legend>Nota Fiscal</legend>
                <p>
                    
                        <span>
                            Pressione o botão abaixo para cadastrar uma nova nota fiscal.
                            </span> </p>
                            
                            <div class="botoesroll">
                            <asp:LinkButton ID="Button_Novo" runat="server"   PostBackUrl="~/Farmacia/FormNotaFiscal.aspx" >
                  <img id="imgnregistro" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgnregistro.src='img/btn/novoregistro2.png';"
                  onmouseout="imgnregistro.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                  </div>
                         <br /><br />
                    
               
                <p>
                    <span class="rotulo">Número da Nota</span>
                    <span>
                        <asp:TextBox ID="TextBox_Numero" CssClass="campo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Fornecedor</span>
                    <span>
                        <asp:DropDownList ID="DropDownList_Fornecedor" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                
                    <div class="botoesroll">
                        <asp:LinkButton ID="Button_Pesquisar" runat="server"  OnClick="OnClick_Pesquisar" ValidationGroup="ValidationGroup_Pesquisa">
                  <img id="imgpesquisa" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                  </div>
                  <div class="botoesroll">
                       <asp:LinkButton ID="Button_Cancelar" runat="server"  PostBackUrl="~/Farmacia/Default.aspx">
                  <img id="imgcancela" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancela.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancela.src='img/btn/cancelar1.png';" /></asp:LinkButton>

                    
                
                <p>
                    <span>
                        <asp:CustomValidator ID="CustomValidator_Pesquisa" runat="server" ErrorMessage="CustomValidator" 
                        OnServerValidate="OnServerValidate_ValidarPesquisa" Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:CustomValidator>
                    </span>
                </p>
            </fieldset>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Button_Pesquisar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
            <asp:Panel ID="Panel_Resultado" runat="server" Visible="false">
                <fieldset class="formulario">
                    <legend>Resultado da Busca</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_NotaFiscal" runat="server" AutoGenerateColumns="false" Width="600px"
                                OnRowDataBound="OnRowDataBound_FormataGridView" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                                AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric">
                                <Columns>
                                    <asp:HyperLinkField HeaderText="Número" DataNavigateUrlFields="Codigo" 
                                        DataNavigateUrlFormatString="~/Farmacia/FormNotaFiscal.aspx?co_nota={0}"
                                        DataTextField="NumeroNota" />
                                    <asp:BoundField HeaderText="Fornecedor" DataField="NomeFornecedor" />
                                    <asp:BoundField HeaderText="Responsável" DataField="NomeResponsavel" />
                                    <asp:BoundField HeaderText="Status" DataField="Status" />
                                    <asp:HyperLinkField Text="Itens" DataNavigateUrlFields="Codigo"
                                        DataNavigateUrlFormatString="~/Farmacia/FormItensNotaFiscal.aspx?co_nota={0}" />
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>