﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormReceberRemanejamento.aspx.cs"
    EnableEventValidation="false" Inherits="ViverMais.View.Vacina.FormReceberRemanejamento"
    MasterPageFile="~/Vacina/MasterVacina.Master" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Receber Remanejamento</h2>
        <fieldset class="formulario">
            <legend>Pesquisar</legend><span class="rotulo">Sala </span><span>
                <asp:DropDownList ID="DropDownList_Sala" runat="server" AutoPostBack="true" DataTextField="Nome"
                    Width="350px" CssClass="drop" DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_PesquisarRemanejamento">
                </asp:DropDownList>
            </span>
            <div class="botoesroll">
                <asp:LinkButton ID="Lnk_Cancelar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_Sala" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_RemanejamentosPesquisados" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Remanejamentos Pesquisados</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_Remanejamentos" runat="server" AutoGenerateColumns="false"
                                    Width="100%" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Remanejamentos"
                                    BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                    <Columns>
                                        <asp:BoundField HeaderText="Sala de Origem" DataField="SalaOrigem" />
                                        <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="~/Vacina/FormConfirmarRecebimentoRemanejamento.aspx?co_remanejamento={0}"
                                            HeaderText="Data" DataTextField="Data" DataTextFormatString="{0:dd/MM/yyy HH:mm}" />
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum registrado encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>