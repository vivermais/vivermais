<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="Inc_AssociarSetorUnidade.ascx.cs"
    Inherits="ViverMais.View.Farmacia.Inc_AssociarSetorUnidade" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<h4>
    Selecione uma unidade abaixo para ver os setores cadastrados na mesma.</h4>
<asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <p>
            <span>
                <asp:GridView ID="GridView_Unidade" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                    Font-Size="X-Small" DataKeyNames="CNES" Width="100%" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                    OnRowCommand="OnRowCommand_Acao" PageSize="20">
                    <Columns>
                        <asp:ButtonField ButtonType="Link" CommandName="CommandName_Editar" DataTextField="NomeFantasia"
                            HeaderText="Unidade" />
                    </Columns>
                    <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px"
                        HorizontalAlign="Center" />
                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </span>
        </p>
    </ContentTemplate>
</asp:UpdatePanel>
<p align="center">
    &nbsp;
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="GridView_Unidade" EventName="RowCommand" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel_SetoresUnidade" runat="server" Visible="false">
                <p>
                    <span style="margin-left: 20px" class="rotulo">UNIDADE:</span><span class="campo">
                        <asp:Label ID="lblUnidade" runat="server" Text=""></asp:Label>
                    </span>
                </p>
                <asp:Table ID="Table_Setores" runat="server" HorizontalAlign="Center">
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left"><br />
                    <span style="font-family:Arial; font-size:13px; font-weight:bolder;">Disponíveis</span>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">&nbsp;</asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                    <span style="font-family:Arial; font-size:13px; font-weight:bolder; margin-top:20px">Incluídos</span>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:ListBox ID="ListBox_SetoresDisponiveis" runat="server" SelectionMode="Multiple" DataTextField="Nome"
                                DataValueField="Codigo"
                                Width="250px" Height="150px"></asp:ListBox>
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:ImageButton ID="ImageButton_Adicionar" runat="server" ImageUrl="~/Farmacia/img/btn/dir1.png"
                                CssClass="espimg" Width="27" Height="27" OnClick="OnClick_AdicionaSetor" ValidationGroup="ValidationGroup_AdicionaSetor" />
                            <asp:ImageButton ID="ImageButton_Retirar" runat="server" ImageUrl="~/Farmacia/img/btn/esq1.png"
                                CssClass="espimg" Width="27px" Height="27" OnClick="OnClick_RetiraSetor" ValidationGroup="ValidationGroup_RetiraSetor" />
                        </asp:TableCell>
                        <asp:TableCell HorizontalAlign="Left">
                            <asp:ListBox ID="ListBox_SetoresAlocados" runat="server" SelectionMode="Multiple" DataTextField="Nome"
                                DataValueField="Codigo"
                                Width="250" Height="150"></asp:ListBox>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell ColumnSpan="3" HorizontalAlign="Center">
                            <div class="botoesroll">
                                <asp:LinkButton ID="Button_Salvar" runat="server" OnClick="OnClick_Salvar"
                                    ValidationGroup="ValidationGroup_Salvar">
                                 <img id="imgsalvar" runat="server" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                <asp:LinkButton ID="Button_Cancelar" runat="server" OnClick="OnClick_Cancelar" Text="Cancelar">
                            <img id="imgcancelar" alt="Salvar" src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                                </asp:LinkButton>
                            </div>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <p>
                </p>
                <asp:CustomValidator ID="CustomValidator_Adicionar" runat="server" Display="None"
                    ErrorMessage="CustomValidator" Font-Size="XX-Small" OnServerValidate="OnServerValidate_ValidaAdicaoSetor"
                    ValidationGroup="ValidationGroup_AdicionaSetor"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator_Retirar" runat="server" Display="None" ErrorMessage="CustomValidator"
                    Font-Size="XX-Small" OnServerValidate="OnServerValidate_ValidaRemocaoSetor" ValidationGroup="ValidationGroup_RetiraSetor"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator_Associar" runat="server" Display="None"
                    ErrorMessage="CustomValidator" Font-Size="XX-Small" OnServerValidate="OnServerValidate_ValidaAssociacao"
                    ValidationGroup="ValidationGroup_Salvar"></asp:CustomValidator>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</p>
