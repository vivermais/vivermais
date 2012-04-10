<%@ Page Language="C#" MasterPageFile="~/Laboratorio/MasterLaboratorio.Master" AutoEventWireup="True"
    CodeBehind="FormPesquisaLaudo.aspx.cs" Inherits="ViverMais.View.Laboratorio.FormPesquisaLaudo"
    Title="Laboratório - Pesquisa de Laudos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Pesquisa de Laudos</h2>
        <fieldset class="formulario">
            <legend>Cartão SUS</legend>
            <p>
                <span class="rotulo">Nº Cartão SUS</span> <span>
                    <asp:TextBox ID="tbxCartaoSus" CssClass="campo" runat="server" />
                </span>
            </p>
            <p>
                <span class="rotulo">Novo Campo</span> <span>
                    <asp:TextBox ID="TextBox1" CssClass="campo" runat="server" />
                </span>
            </p>
            <p>
                <span>
                    <asp:LinkButton ID="btnPesquisar" Text="Pesquisar" OnClick="btnPesquisar_Click" runat="server">
                     <img id="imgpsquisa" alt="" src="img/pesquisar1.png"
                onmouseover="imgpsquisa.src='img/pesquisar2.png';"
                onmouseout="imgpsquisa.src='img/pesquisar1.png';" />
                    </asp:LinkButton>
                </span>
            </p>
        </fieldset>
        <asp:Panel ID="PanelGridView" runat="server" Visible="false">
        <fieldset class="formulario">
            <legend>Lista de Laudos</legend>
            <p>
                <span>
                    <asp:GridView ID="GridView_Laudos" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="NomeArquivo" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="NomeArquivo" HeaderText="Nome do Arquivo" />
                            <asp:ButtonField ButtonType="Image" CausesValidation="false" HeaderText="Visualizar"
                                CommandName="CommandName_Responsavel" ImageUrl="~/Laboratorio/img/lupa.png"  />
                        </Columns>
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#0b9496" Font-Bold="True" 
                                    ForeColor="#ffffff" Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#0b9496" ForeColor="#ffffff" />
                                <RowStyle CssClass="tabrow" BackColor="#e4f6f5" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                    </asp:GridView>
                </span>
            </p>
            <p>
                <span>
                    <asp:Label ID="lblNoRegisters" runat="server" Visible="false" Text="Nenhum Registro Encontrado"
                        ForeColor="Red" Font-Bold="true" />
                </span>
            </p>
        </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
