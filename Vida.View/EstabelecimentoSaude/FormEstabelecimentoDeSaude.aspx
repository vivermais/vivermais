<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormEstabelecimentoDeSaude.aspx.cs"
    Inherits="ViverMais.View.EstabelecimentoSaude.FormEstabelecimentoDeSaude" MasterPageFile="~/EstabelecimentoSaude/MasterEstabelecimento.Master"
    EnableEventValidation="false" %>

<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="WUC_PesquisarUnidade"
    TagPrefix="WUC_Unidade" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder3" ID="c_body" runat="server">
    <div id="top">
        <h2>
            Estabelecimentos de Sa�de
        </h2>
        <fieldset class="formulario">
            <legend>Pesquisar</legend>
            <WUC_Unidade:WUC_PesquisarUnidade ID="EAS" runat="server" />
            <%--            <p>
                <span class="rotulo">Nome Fantasia</span>
                <span>
                    <asp:TextBox ID="TextBox_BuscarEstabelecimento" runat="server" Width="300" CssClass="campo"></asp:TextBox>
                </span>
            </p>
          <br />--%>
            <%--            <div class="botoesroll">
                  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_BuscarEstabelecimento"
                        ValidationGroup="ValidationGroup_BuscarEstabelecimento" >
                  <img id="imgbuscar" alt="Buscar" src="img/buscar1.png"
                  onmouseover="imgbuscar.src='img/buscar2.png';"
                  onmouseout="imgbuscar.src='img/buscar1.png';" /></asp:LinkButton>
                        </div>--%>
            <%--                    <div class="botoesroll">
                 <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_ListarTodosEstabelecimentos" >
                  <img id="imglistar" alt="Listar Todos" src="img/listartodos1.png"
                  onmouseover="imglistar.src='img/listartodos2.png';"
                  onmouseout="imglistar.src='img/listartodos1.png';" /></asp:LinkButton>
                        </div>--%>
            <%--                        <p>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Digite um nome para a busca." Display="None"
                        ControlToValidate="TextBox_BuscarEstabelecimento"
                        ValidationGroup="ValidationGroup_BuscarEstabelecimento"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="ValidationGroup_BuscarEstabelecimento" />
                        </p>--%>
            <%--            <br /><br />--%>
            <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                RenderMode="Inline" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:Panel ID="Panel_Unidade" runat="server" Visible="false">
                        <p>
                            <asp:GridView ID="grid_EstabelecimentoSaude" runat="server" AllowPaging="True" PageSize="10"
                                OnPageIndexChanging="onPageEstabelecimento" AutoGenerateColumns="False"
                                DataKeyNames="CNES" OnRowCommand="OnRowCommand_VerificarAcao" BackColor="White"
                                BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="100%">
                                <FooterStyle BackColor="#e9e1d3" ForeColor="Black" />
                                <RowStyle BackColor="#e9e1d3" ForeColor="Black" Font-Size="11px" />
                                <PagerStyle BackColor="#e9e1d3" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#dfd3bf" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#987840" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="30px" />
                                <AlternatingRowStyle BackColor="#dfd3bf" />
                                <Columns>
                                    <asp:BoundField HeaderText="CNES" DataField="CNES" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField ItemStyle-Width="500px" ButtonType="Link" DataTextField="NomeFantasia"
                                        HeaderText="Nome Fantasia" CommandName="cn_visualizarEstabelecimento">
                                        <ItemStyle Width="500px" />
                                    </asp:ButtonField>
                                    <asp:BoundField DataField="DescricaoStatusEstabelecimento" HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <FooterStyle BackColor="#dfd3bf" ForeColor="Black" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum estabelecimento encontrado." Font-Bold="true"></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </p>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
    </div>
</asp:Content>
