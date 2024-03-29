﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImportacoesRealizadas.aspx.cs"
    Inherits="ViverMais.View.EstabelecimentoSaude.FormImportacoesRealizadas" MasterPageFile="~/EstabelecimentoSaude/MasterEstabelecimento.Master"
    EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="c_head" runat="server" ID="c_head">
    <meta http-equiv="refresh" content="15" />
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder3">
    <div id="top">
        <h2>
            Importações realizadas</h2>
        <fieldset>
            <legend>Relação</legend>
            <div style="font-family: Arial; font-size: 17px; font-weight: bold; color: #bc0d0d;
                margin-bottom: 12px;">
                Página atualizada automaticamente de 15 em 15 segundos.</div>
            <div class="botoesroll">
                <asp:LinkButton ID="lkn_ButtonNovaImportacao" runat="server" PostBackUrl="~/EstabelecimentoSaude/FormImportarEstabelecimentoSaude.aspx">
                  <img id="imgnovaimport" alt="Nova Importação" src="img/nova-importacao1.png"
                  onmouseover="imgnovaimport.src='img/nova-importacao2.png';"
                  onmouseout="imgnovaimport.src='img/nova-importacao1.png';" /></asp:LinkButton>
            </div>
            <br />
            <asp:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Conditional" RenderMode="Block"
                ChildrenAsTriggers="true" Visible>
                <ContentTemplate>
                    <p>
                        <asp:GridView ID="GridView_Importacao" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                            BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Vertical" Width="100%">
                            <FooterStyle BackColor="#e9e1d3" ForeColor="Black" />
                            <RowStyle BackColor="#e9e1d3" ForeColor="Black" Font-Size="9px" />
                            <PagerStyle BackColor="#e9e1d3" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#dfd3bf" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#987840" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                Font-Size="10px" Height="30px" />
                            <AlternatingRowStyle BackColor="#dfd3bf" />
                            <Columns>
                                <asp:BoundField HeaderText="Nº" DataField="Codigo" HeaderStyle-Width="20px" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField HeaderText="Usuário" DataField="NomeUsuario"></asp:BoundField>
                                <asp:BoundField HeaderText="Arquivo" DataField="Arquivo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Tamanho Arquivo(Bytes)" DataField="TamanhoArquivo" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Horário Início" DataField="HorarioInicio" DataFormatString="{0:dd/MM/yyyy HH:mm}"
                                    ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Horário Término" DataField="HorarioTerminoToString" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Status" DataField="StatusToString" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField HeaderText="Erro" DataField="Erro" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
