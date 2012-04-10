<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="FormExibeElenco.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeElenco"
    EnableEventValidation="false" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Elenco de Medicamentos</h2>
        <fieldset class="formulario">
            <legend>Dados dos Elencos de Medicamentos</legend>
            <p>
                <span>
                    <asp:LinkButton ID="btnNovo" runat="server" PostBackUrl="~/Farmacia/FormElencoMedicamentos.aspx">
                  <img id="imgexelenco" alt="Novo Elenco" src="img/btn/novoelenco1.png"
                  onmouseover="imgexelenco.src='img/btn/novoelenco2.png';"
                  onmouseout="imgexelenco.src='img/btn/novoelenco1.png';" /></asp:LinkButton>
                </span>
            </p>
            <p>
                <span class="rotulo">Nome</span> <span>
                    <asp:TextBox ID="TextBox_NomeElenco" runat="server" CssClass="campo" MaxLength="100"></asp:TextBox>
                </span>
            </p>
            <br />

              
                <div  class="botoesroll" style="margin-top:20px">
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_Pesquisa" ValidationGroup="ValidationGroup_Pesquisa">
             <img id="imgpesquisa" alt="Pesquisar" src="img/btn/pesquisar1.png"
                onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" />
                    </asp:LinkButton>
                    </div>
                    <div class="botoesroll" style="margin-top:20px">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_ListarTodos">
                     <img id="imglistatodos" alt="Listar Todos" src="img/btn/listartodos1.png"
                onmouseover="imglistatodos.src='img/btn/listartodos2.png';"
                onmouseout="imglistatodos.src='img/btn/listartodos1.png';" />
                    </asp:LinkButton>
                    </div>
              

            <br /><br /><br />
            <p>
                <span>
                    <asp:GridView ID="GridView_Elenco" runat="server" DataKeyNames="Codigo" AllowPaging="true"
                        OnRowDeleting="OnRowDeleting_Elenco" PageSize="20" PagerSettings-Mode="Numeric" Font-Size="X-Small" Width="100%"
                        OnPageIndexChanging="OnPageIndexChanging_Elenco" AutoGenerateColumns="false">
                        <Columns>
                            <asp:HyperLinkField HeaderText="Nome" DataTextField="Nome" DataNavigateUrlFields="Codigo"
                                DataNavigateUrlFormatString="FormElencoMedicamentos.aspx?codigo={0}" />
                            <asp:TemplateField HeaderText="Excluir">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este elenco ?');">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:CommandField ButtonType="Image" CausesValidation="true" ShowDeleteButton="true"
                             HeaderText="Excluir ?" />--%>
                        </Columns>
                        <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Nenhum elenco encontrado."></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <%--<asp:TreeView ID="TreeViewElenco" runat="server" 
                    onselectednodechanged="TreeViewElenco_SelectedNodeChanged"  ForeColor="#363636"  ExpandImageUrl="~/Farmacia/img/setinha.png" CollapseImageUrl="" LeafNodeStyle-ImageUrl="" HoverNodeStyle-ForeColor="Green" >
                    </asp:TreeView>--%>
                </span>
            </p>
        </fieldset>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é Obrigatório."
            Display="None" ControlToValidate="TextBox_NomeElenco" ValidationGroup="ValidationGroup_Pesquisa"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do elenco."
            Display="None" ControlToValidate="TextBox_NomeElenco" ValidationGroup="ValidationGroup_Pesquisa"
            ValidationExpression="^\S{3}[\W-\w]*$">
        </asp:RegularExpressionValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisa" />
    </div>
</asp:Content>
