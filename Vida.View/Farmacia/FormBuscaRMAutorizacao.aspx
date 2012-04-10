<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True" CodeBehind="FormBuscaRMAutorizacao.aspx.cs" Inherits="ViverMais.View.Farmacia.FormBuscaRMAutorizacao" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário de Busca de Requisição de Medicamento</h2>
        <fieldset class="formulario">
            <legend>Dados da busca</legend>
            <p>
                <span>Pressione o botão ao lado para cadastrar uma nova requisição de medicamento.
                    <asp:LinkButton ID="Button_Novo" runat="server"  PostBackUrl="~/Farmacia/FormRegistrarRM.aspx" CausesValidation="false">
                  <img id="registrarrm" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="registrarrm.src='img/btn/novoregistro2.png';"
                  onmouseout="registrarrm.src='img/btn/novoregistro1.png';" /></asp:LinkButton>
                </span>
            </p>
            <p>
                <span class="rotulo">Nº da Requisição:</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="tbxRequisicao" runat="server" CssClass="campo"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        ControlToValidate="tbxRequisicao"></asp:RequiredFieldValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Farmácia:</span>
                <span style="margin-left: 5px;">
                    <asp:DropDownList ID="ddlFarmacia" runat="server" CssClass="comboBox" Width="300px"></asp:DropDownList>
                </span>
            </p>
            <p>
                <span>
                  <asp:LinkButton ID="btnPesquisar" runat="server"  OnClick="btnPesquisar_Click" >
                  <img id="imgpesq" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesq.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesq.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                   <asp:LinkButton ID="btnListarTodos" runat="server"  OnClick="btnListarTodos_Click" CausesValidation="false" >
                  <img id="imglistar" alt="Listar Todos" src="img/btn/listartodos1.png"
                  onmouseover="imglistar.src='img/btn/listartodos2.png';"
                  onmouseout="imglistar.src='img/btn/listartodos1.png';" /></asp:LinkButton>

                </span>
            </p>
        </fieldset>
        <asp:Panel ID="Panel_ResultadoBusca" runat="server" Visible="false">
        <fieldset class="formulario">
            <legend>Resultado da Busca</legend>
            <p>
                <span>
                    <asp:GridView ID="gridRM" runat="server" AutoGenerateColumns="False"
                        Width="700px" BorderColor="Black">
                        <Columns>
                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataTextField="Codigo" HeaderText="Número"
                               DataNavigateUrlFormatString="~/Farmacia/FormAutorizarRM.aspx?codigo={0}" ItemStyle-Width="60px"
                               ItemStyle-VerticalAlign="Bottom" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="Silver" />
                            <asp:BoundField DataField="NomeFarmacia" HeaderText="Farmácia" ItemStyle-Width="300px"
                               ItemStyle-VerticalAlign="Bottom" ItemStyle-BorderColor="Silver" />
                            <asp:BoundField DataField="DataCriacao" HeaderText="Data da Criação" ItemStyle-Width="120px"
                               ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Bottom" ItemStyle-BorderColor="Silver" />
                            <asp:BoundField DataField="DataEnvioToString" HeaderText="Data do Envio" ItemStyle-Width="120px"
                               ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Bottom" ItemStyle-BorderColor="Silver" />
                            <asp:BoundField DataField="Status" ItemStyle-VerticalAlign="Bottom" HeaderText="Status"
                             ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" ItemStyle-BorderColor="Silver" />
                        </Columns>
                        <HeaderStyle CssClass="hearderGridView" />
                        <RowStyle CssClass="rowGridView" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </span>
            </p>
        </fieldset>
        </asp:Panel>
    </div>
</asp:Content>
