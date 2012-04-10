<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="FormBuscaMedicamento.aspx.cs" Inherits="ViverMais.View.Urgencia.WebForm2"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="Upd1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Busca de Medicamentos/Prescrição</h2>
                <fieldset class="formulario">
                    <legend>Informações</legend>
                    <p>
                        <span class="rotulo">Nome:</span> <span>
                            <asp:TextBox ID="tbxMedicamento" CssClass="campo" runat="server" Width="200px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                ControlToValidate="tbxMedicamento">
                            </asp:RequiredFieldValidator>
                        </span>
                    </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click" >
                <img id="imgpesquisar" alt="Salvar Código" src="img/bts/btn_buscar1.png"
                onmouseover="imgpesquisar.src='img/bts/btn_buscar2.png';"
                onmouseout="imgpesquisar.src='img/bts/btn_buscar1.png';" /></asp:LinkButton>
                        </div>

                    <div class="botoesroll">
                        <asp:LinkButton ID="btnNovo" runat="server" OnClick="btnNovo_Click" CausesValidation="false" >
                <img id="imgnovomedic" alt="Cancelar" src="img/bts/btn_novomedicamento1.png"
                onmouseover="imgnovomedic.src='img/bts/btn_novomedicamento2.png';"
                onmouseout="imgnovomedic.src='img/bts/btn_novomedicamento1.png';" /></asp:LinkButton>
                        </div>
                </fieldset>
                <asp:Panel ID="Panel_ResultadoBusca" runat="server" Visible="false">
                    <fieldset id="itens">
                        <legend>Medicamentos/Prescrição</legend>
                        <p>
                            <span>
                                <asp:GridView ID="gridMedicamento" runat="server" AutoGenerateColumns="False" Width="780px"
                                    DataKeyNames="Codigo" OnRowDataBound="gridMedicamento_RowDataBound" OnRowCommand="GridMedicamento_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Nome" HeaderText="Medicamento/Prescrição" />
                                        <asp:BoundField HeaderText="Pertence a Rede ?" DataField="PertenceARedeToString" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:CommandField SelectText="Alterar" ShowSelectButton="True"></asp:CommandField>
                                    </Columns>
                                    <HeaderStyle CssClass="tab" />
                                    <RowStyle CssClass="tabrow" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
