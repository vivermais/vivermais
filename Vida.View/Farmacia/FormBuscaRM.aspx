<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="FormBuscaRM.aspx.cs" Inherits="ViverMais.View.Farmacia.FormBuscaRM" Title="ViverMais - Busca de Requisição de Medicamentos" %>

<%@ Register Src="~/Farmacia/WUCPesquisarRequisicao.ascx" TagName="WUCPesquisarRequisicao"
    TagPrefix="WUCPesquisarRequisicao" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário de Busca de Requisição de Medicamento</h2>
        <br />
        <p>
            <asp:LinkButton ID="Button_Novo" runat="server" PostBackUrl="~/Farmacia/FormRequisicaoMedicamentos.aspx"
                CausesValidation="false">
                  <img id="imgnovarm" alt="Nova RM" src="img/btn/novarm1.png"
                  onmouseover="imgnovarm.src='img/btn/novarm2.png';"
                  onmouseout="imgnovarm.src='img/btn/novarm1.png';" /></asp:LinkButton>
        </p>
       <%-- <fieldset class="formulario">
            <legend>Dados da busca</legend>
            <p>--%>
                <WUCPesquisarRequisicao:WUCPesquisarRequisicao ID="WUCPesquisarRequisicao" runat="server" />
            <%--</p>--%>
            <%--            <p>
                <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                    obrigatórios</span>
            </p>
            <p>
                <span class="rotulo">Nº da Requisição:</span>
                <span style="margin-left: 5px;">
                    <asp:TextBox ID="tbxRequisicao" runat="server" CssClass="campo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="tbxRequisicao"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                    ErrorMessage="Digite Somente Números" ControlToValidate="tbxRequisicao" 
                    font-family="Verdana" font-size="11px" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                </span>
            </p>
            <p>
                <span>
                    <asp:LinkButton ID="btnPesquisar" runat="server"   OnClick="btnPesquisar_Click">
                  <img id="imgpesquisa" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" /></asp:LinkButton>

                  <asp:LinkButton ID="btnListarTodos" runat="server"  OnClick="btnListarTodos_Click"
                        CausesValidation="false">
                  <img id="imglistar" alt="Listar Todos" src="img/btn/listartodos1.png"
                  onmouseover="imglistar.src='img/btn/listartodos2.png';"
                  onmouseout="imglistar.src='img/btn/listartodos1.png';" /></asp:LinkButton>
                </span>
            </p>
        </fieldset>
        <fieldset class="formulario">
            <legend>Resultado da Busca</legend>
                <asp:GridView ID="gridRM" runat="server" AutoGenerateColumns="False" 
                Width="700px" BorderColor="Silver" Font-Size="X-Small">
                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Height="20px" />
                    <Columns>
                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataTextField="Codigo" HeaderText="Número"
                            DataNavigateUrlFormatString="~/Farmacia/FormRegistrarRM.aspx?codigo={0}">
                                <ItemStyle HorizontalAlign="Center" Width="50px" BorderColor="Silver" VerticalAlign="Bottom"></ItemStyle>
                        </asp:HyperLinkField>
                        <asp:BoundField DataField="DataCriacao" HeaderText="Data da Criação">
                            <ItemStyle HorizontalAlign="Center" Width="225px" BorderColor="Silver" VerticalAlign="Bottom"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="DataEnvioToString" HeaderText="Data do Envio">
                            <ItemStyle HorizontalAlign="Center" Width="225px" BorderColor="Silver" VerticalAlign="Bottom"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Status" HeaderText="Status">
                            <ItemStyle HorizontalAlign="Center" Width="150px" BorderColor="Silver" VerticalAlign="Bottom"></ItemStyle>
                        </asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle BackColor="#538065" Font-Size="X-Small" ForeColor="White" />
                </asp:GridView>
        </fieldset>--%>
    </div>
</asp:Content>
