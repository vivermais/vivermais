<%@ Page Language="C#" MasterPageFile="~/Profissional/MasterProfissional.Master"
    AutoEventWireup="True" CodeBehind="BuscaProfissional.aspx.cs" Inherits="ViverMais.View.Profissional.BuscaProfissional"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Profissionais de Saúde</h2>
        <br />
        <h4>
            Para cadastrar um novo Profissional de Saúde, utilize o botão abaixo.
        </h4>
        <p>
            <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Profissional/FormProfissional.aspx">
                       <img id="img_Profissional" alt="" src="img/cadastrar_1.png"
                onmouseover="img_Profissional.src='img/cadastrar_2.png';"
                onmouseout="img_Profissional.src='img/cadastrar_1.png';" />
            </asp:LinkButton>
        </p>
        <h2>
            Pesquisa de Profissionais de Saúde</h2>
        <br />
        
        <fieldset class="formulario">
            <legend>Selecione o modo de Pesquisa</legend>
            <p>
            <span class="rotulo"> <asp:Label ID="lblCategoria" runat="server" Text="Categoria"></asp:Label></span>
               
                <span>
                    <asp:DropDownList ID="ddlCategoria" runat="server" ValidationGroup="Pesquisa">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlCategoria"
                        ErrorMessage="*" ForeColor="Red" Font-Bold="true" InitialValue="0"></asp:RequiredFieldValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lblnomeProfissionalPesquisa" runat="server" Text="Nome"></asp:Label></span>
                <span><asp:TextBox ID="tbxNomeProfissionalPesquisa" runat="server" CssClass="campo" ValidationGroup="Pesquisa" MaxLength="60"></asp:TextBox></span>
                
            </p>
            <p>
                <span class="rotulo">
                    <asp:Label ID="lblNumConselho" runat="server" Text="Número Conselho"></asp:Label></span>
                <span><asp:TextBox ID="tbxNumConselho" runat="server" CssClass="campo" ValidationGroup="Pesquisa" MaxLength="10"></asp:TextBox></span>
                    
                <asp:Label ID="lblSemPesquisa" runat="server" Text="Selecione um método para Pesquisa"
                    Font-Bold="true" ForeColor="Red" Font-Size="X-Small" ></asp:Label>
            </p>
            <p>
                <asp:LinkButton ID="btnBuscarProfissional" runat="server" OnClick="btnPesquisar_Click" 
                    CausesValidation="true" ValidationGroup="Pesquisa">
                    <img id="imgpesquisar1" alt="Pesquisar" src="img/pesquisar_1.png"
                        onmouseover="imgpesquisar1.src='img/pesquisar_2.png';"
                        onmouseout="imgpesquisar1.src='img/pesquisar_1.png';" />
                </asp:LinkButton>
            </p>
            <p>
                <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                <asp:GridView ID="GridViewListaProfissionais" runat="server" AutoGenerateColumns="false"
                    EnableSortingAndPagingCallbacks="true" AllowPaging="true" PageSize="20" 
                    AlternatingRowStyle-BackColor="LightBlue" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
CellPadding="3" GridLines="Vertical" Width="100%">
<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
<RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="formprofissional.aspx?codigo={0}"
                            DataTextField="Nome" ItemStyle-HorizontalAlign="Center" HeaderText="Nome do Profissional" ItemStyle-ForeColor="#ffffff" />
                        <asp:BoundField DataField="OrgaoEmissorRegistro" HeaderText="Ocupacão" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="NumeroRegistro" HeaderText="Nº Conselho" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle BackColor="#714494" ForeColor="Black" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#714494" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#714494" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
<AlternatingRowStyle BackColor="#efe9f4" />
                </asp:GridView>
            </p>
        </fieldset>
    </div>
</asp:Content>
