<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true" CodeBehind="FormClassificacaoRisco.aspx.cs" Inherits="ViverMais.View.Urgencia.FormClassificacaoRisco" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
    <ContentTemplate>
    <div id="top">
    <h2>Lista de Classificação de Risco</h2>
    
    <fieldset class="formulario">
        <legend>Relação</legend>
        <p>
        <span><asp:GridView ID="GridView_TipoConsultorio" DataKeyNames="Codigo" Width="690px"
             AutoGenerateColumns="false" runat="server" OnRowDataBound="OnRowDataBound_FormataGridView">
                <Columns>
                    <asp:BoundField HeaderText="Descrição" DataField="Descricao" ItemStyle-HorizontalAlign="Center"/>
                    <%--<asp:BoundField HeaderText="Cor" DataField="Cor" ItemStyle-HorizontalAlign="Center"/>--%>
                    <asp:TemplateField HeaderText="Cor">
                        <ItemTemplate>
                            <asp:Image ID="Imagem_Cor" runat="server" ImageUrl='<%#bind("Imagem") %>' Width="32px" Height="32px"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
            </asp:GridView></span>
        </p>
    </fieldset>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
