﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormBuscaMedicamento.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormBuscaMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Lista de Medicamentos Cadastrados</h2>
        <%--<fieldset>
            <legend>Pesquisa de Medicamentos</legend>
            <p>
                <span>Unidade de Medida</span>
                <span>
                    <asp:DropDownList ID="DropDownList_UnidadeMedida" runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span>Medicamento</span>
                <span>
                    <asp:TextBox ID="TextBox_Nome" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span><asp:Button ID="Button_Pesquisar" runat="server" Text="Pesquisar" OnClick="OnClick_Pesquisar" CommandArgument="alguns" ValidationGroup="ValidationGroup_Pesquisa"/></span>
                <span><asp:Button ID="Button_ListarTodos" runat="server" Text="Listar Todos" OnClick="OnClick_Pesquisar" CommandArgument="todos"/></span>
                <span><asp:Button ID="Button_Novo" runat="server" Text="Novo Medicamento" PostBackUrl="~/Farmacia/FormMedicamento.aspx"/></span>
            </p>
            
            <p>
                <asp:CustomValidator ID="CustomValidator_Pesquisa" runat="server" ErrorMessage="CustomValidator" Display="None" ValidationGroup="ValidationGroup_Pesquisa" OnServerValidate="OnServerValidate_ValidaPesquisa"></asp:CustomValidator>
            </p>
        </fieldset>--%>
        <%--<asp:Panel ID="Panel_Resultado" runat="server" Visible="false">--%>
        <fieldset class="formulario">
            <legend>Medicamentos</legend>
            <p>
                <h4>
                    Pressione o botão abaixo para cadastrar um novo medicamento.</h4>
                <p>
                    <div>
                        <asp:LinkButton ID="Button_Novo" runat="server" PostBackUrl="~/Farmacia/FormMedicamento.aspx">
                  <img id="imgsalvar" alt="Novo Registro" src="img/btn/novoregistro1.png"
                  onmouseover="imgsalvar.src='img/btn/novoregistro2.png';"
                  onmouseout="imgsalvar.src='img/btn/novoregistro1.png';" /></asp:LinkButton></div>
                </p>
                <p>
                    <span class="rotulo">Código:</span> <span>
                        <asp:TextBox ID="txtCodigoMedicamento" runat="server" CssClass="campo"></asp:TextBox></span>
                </p>
                <p>
                    <span class="rotulo">Medicamento:</span> <span>
                        <asp:TextBox ID="txtNomeMedicamento" runat="server" CssClass="campo" Width="180px"></asp:TextBox></span>
                </p>
                <div class="botoesroll" style="margin-top: 20px">
                    <asp:LinkButton ID="Button1" runat="server" Text="Buscar" OnClick="Button1_Click">                                   <img id="imgbuscar" alt="Buscar" src="img/btn/pesquisar1.png"                onmouseover="imgbuscar.src='img/btn/pesquisar2.png';"                onmouseout="imgbuscar.src='img/btn/pesquisar1.png';" />                </asp:LinkButton>
                </div>
                <div class="botoesroll" style="margin-bottom: 15px; margin-top: 20px">
                    <asp:LinkButton ID="Button2" runat="server" Text="Listar todos" OnClick="Button2_Click">                                  <img id="imglistatodos" alt="Listar Todos" src="img/btn/listartodos1.png"                onmouseover="imglistatodos.src='img/btn/listartodos2.png';"                onmouseout="imglistatodos.src='img/btn/listartodos1.png';" />                </asp:LinkButton>
                </div>
            </p>
            <br />
            <p>
                <span>
                    <asp:GridView ID="GridView_Medicamento" runat="server" AllowPaging="true" Font-Size="X-Small"
                        AutoGenerateColumns="false" DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_Paginacao"
                        OnRowDataBound="OnRowDataBound_FormataGridView" Width="680px" PagerSettings-Mode="Numeric"
                        PageSize="20">
                        <Columns>
                            <asp:BoundField DataField="CodMedicamento" HeaderText="Codigo" ItemStyle-HorizontalAlign="Center" />
                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Farmacia/FormMedicamento.aspx?co_medicamento={0}"
                                DataTextField="Nome" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="UnidadeMedidaToString" HeaderText="Unidade de Medida"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Ind_Antibio" HeaderText="É Antibiótico ?" ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                        <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px"
                            HorizontalAlign="Center" />
                        <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                        <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </span>
            </p>
            <p>
            </p>
            
            <p>
            </p>
            <p>
            </p>
            
        </fieldset>
        <%-- </asp:Panel>--%>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
