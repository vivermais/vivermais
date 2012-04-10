﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPesquisaProcedimento.ascx.cs"
    Inherits="ViverMais.View.Agendamento.WUCPesquisaProcedimento" %>
<fieldset class="formularioMedio">
    <legend>Pesquisar Procedimento</legend>
    <p>
        <span class="rotulo">Por Código</span><span>
            <asp:TextBox ID="tbxCodigoProcedimento" MaxLength="10" runat="server" CssClass="campo"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidatorProcedimento" Font-Size="XX-Small" runat="server"
                ControlToValidate="tbxCodigoProcedimento" Display="Dynamic" ErrorMessage="O campo Código deve conter apenas Números"
                Operator="DataTypeCheck" Type="Double">
            </asp:CompareValidator>
        </span>
    </p>
    <p>
        <span class="rotulo">Por Nome</span> <span>
            <asp:TextBox ID="tbxNomeProcedimento" runat="server" CssClass="campo"></asp:TextBox></span>
    </p>
    <asp:LinkButton ID="btnPesquisarProcedimento" runat="server" OnClick="btnPesquisarProcedimento_Click"
        CausesValidation="false">
                    <img id="img_pesquisar" alt="" src="img/pesquisar_1.png"
                    onmouseover="img_pesquisar.src='img/pesquisar_2.png';"
                    onmouseout="img_pesquisar.src='img/pesquisar_1.png';" />
    </asp:LinkButton>
    <p>
        <asp:Label ID="lblSemPesquisa" runat="server" Text="Selecione um método para Pesquisa"
            ForeColor="Red" Visible="false"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GridViewListaProcedimento" runat="server" AutoGenerateColumns="false"
            AllowPaging="true" PageSize="6" Width="100%" AlternatingRowStyle-BackColor="LightBlue"
            OnRowCommand="GridViewListaProcedimento_RowCommand" OnPageIndexChanging="GridViewListaProcedimento_OnPageIndexChanged">
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Selecionar">
                    <ItemTemplate>
                        <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                            CommandName="Select">
                            <asp:Image ID="imgSelect" ImageUrl="~/img/bts/select.png" runat="server" />
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                <asp:BoundField DataField="Nome" HeaderText="Nome Procedimento" ItemStyle-Font-Bold="true" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
            </EmptyDataTemplate>
            <AlternatingRowStyle BackColor="LightBlue" />
            <HeaderStyle CssClass="tab" />
            <RowStyle CssClass="tabrow_left" HorizontalAlign="Center" />
        </asp:GridView>
    </p>
</fieldset>
