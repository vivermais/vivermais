﻿<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="FormExibeSubElencoMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormExibeSubElencoMedicamento"
    Title="ViverMais - Sub-Elencos de Medicamento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            &nbsp;Sub-Elenco de Medicamentos</h2>
        <fieldset class="formulario">
            <legend>Dados dos Sub-Elencos de Medicamentos</legend>
            <p style="margin-bottom:25px">
                <span>
                    <asp:LinkButton ID="btnNovo" runat="server" PostBackUrl="~/Farmacia/FormSubElencoMedicamento.aspx">
                  <img id="imgnsubelenco" alt="Salvar" src="img/btn/novosubelenco1.png"
                  onmouseover="imgnsubelenco.src='img/btn/novosubelenco2.png';"
                  onmouseout="imgnsubelenco.src='img/btn/novosubelenco1.png';" /></asp:LinkButton>
                </span>

            </p>
            <p>
                <span class="rotulo">Nome</span> <span>
                    <asp:TextBox ID="TextBox_NomeSubElenco" runat="server" CssClass="campo" MaxLength="100" Width="300px"></asp:TextBox>
                </span>
            </p>
        
                     <br/>    
                <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_Pesquisa" ValidationGroup="ValidationGroup_Pesquisa">
                     <img id="imgpesquisar" alt="Pesquisar" src="img/btn/pesquisar1.png"
                onmouseover="imgpesquisar.src='img/btn/pesquisar2.png';"
                onmouseout="imgpesquisar.src='img/btn/pesquisar1.png';" />
                    </asp:LinkButton>
                    </div>
                    
                    <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_ListarTodos">
                     <img id="imglistartodos" alt="Listar Todos" src="img/btn/listartodos1.png"
                onmouseover="imglistartodos.src='img/btn/listartodos2.png';"
                onmouseout="imglistartodos.src='img/btn/listartodos1.png';" />
                    </asp:LinkButton>
                   </div>                                 
                   
             
           
            <p>
                <span style="margin-left: 5px;">
                    <asp:GridView ID="GridView_SubElenco" runat="server" DataKeyNames="Codigo" AllowPaging="true" Font-Size="X-Small"
                        OnRowDeleting="OnRowDeleting_SubElenco" PageSize="20" PagerSettings-Mode="Numeric"
                        OnPageIndexChanging="OnPageIndexChanging_SubElenco" AutoGenerateColumns="false" Width="100% ">
                        <Columns>
                            <asp:HyperLinkField HeaderText="Nome" DataTextField="Nome" DataNavigateUrlFields="Codigo"
                                DataNavigateUrlFormatString="FormSubElencoMedicamento.aspx?codigo={0}" />
                            <asp:TemplateField HeaderText="Excluir?">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Delete" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este sub-elenco ?');">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%-- <asp:CommandField ButtonType="Image" CausesValidation="true" ShowDeleteButton="true"
                             HeaderText="Excluir ?" />--%>
                        </Columns>
                         <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" Text="Nenhum sub-elenco encontrado."></asp:Label>
                        </EmptyDataTemplate>
                    </asp:GridView>
                    <%--<asp:TreeView ID="TreeViewElenco" runat="server" 
                    onselectednodechanged="TreeViewElenco_SelectedNodeChanged"  ForeColor="#363636"  ExpandImageUrl="~/Farmacia/img/setinha.png" CollapseImageUrl="" LeafNodeStyle-ImageUrl="" HoverNodeStyle-ForeColor="Green" >
                    </asp:TreeView>--%>
                </span>
            </p>
            <%--<p>
                <span style="margin-left: 5px;">
                    <asp:TreeView ID="TreeViewSubElenco" runat="server" 
                    onselectednodechanged="TreeViewSubElenco_SelectedNodeChanged">
                    </asp:TreeView>
                </span>
            </p>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é Obrigatório."
                Display="None" ControlToValidate="TextBox_NomeSubElenco" ValidationGroup="ValidationGroup_Pesquisa"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do sub-elenco."
                Display="None" ControlToValidate="TextBox_NomeSubElenco" ValidationGroup="ValidationGroup_Pesquisa"
                ValidationExpression="^\S{2}[\W-\w]*$">
            </asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisa" />
        </fieldset>
    </div>
</asp:Content>
