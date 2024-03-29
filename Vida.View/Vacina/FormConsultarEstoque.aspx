﻿<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormConsultarEstoque.aspx.cs" Inherits="ViverMais.View.Vacina.FormConsultarEstoque"
    Title="Untitled Page" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Consultar Estoque</h2>
        <fieldset class="formulario">
            <legend>Formulário</legend>
            <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:DropDownList ID="ddlSalaVacina" runat="server" Width="300px" CssClass="drop"
                        DataTextField="Nome" DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Fabricante</span> <span>
                    <asp:DropDownList ID="ddlFabricante" runat="server" Width="300px" CssClass="drop"
                        DataTextField="Nome" DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Imunobiológico</span> <span>
                    <asp:DropDownList ID="ddlVacina" runat="server" Width="300px" CssClass="drop" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Qtd Aplicacão</span> <span>
                    <asp:TextBox ID="TextBox_QtdAplicacao" runat="server" Width="25px" MaxLength="3"
                        CssClass="campo"></asp:TextBox>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="lnkConsultar" runat="server" OnClick="lnkConsultar_Click" ValidationGroup="ValidationGroup_ConsultarEstoque">
                  <img id="imgconsultar" alt="Pesquisar" src="img/btn_consultar1.png"
                  onmouseover="imgconsultar.src='img/btn_consultar2.png';"
                  onmouseout="imgconsultar.src='img/btn_consultar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="LinkButtonVoltar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
            </div>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" RenderMode="Block"
          >
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="lnkConsultar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_estoque" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Estoque</legend>
                        <p>
                            <asp:GridView ID="GridViewEstoque" runat="server" AutoGenerateColumns="False" Width="100%"
                                BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" AllowPaging="true"
                                PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Estoque">
                                <Columns>
                                    <asp:BoundField DataField="NomeVacina" HeaderText="Vacina" />
                                    <asp:BoundField DataField="NomeFabricanteVacina" HeaderText="Fabricante" />
                                    <asp:BoundField DataField="IdentificacaoLote" HeaderText="Lote" />
                                    <asp:BoundField DataField="ValidadeLote" HeaderText="Validade" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="QtdAplicacaoVacina" HeaderText="Qtd Aplicação" />
                                    <asp:BoundField DataField="QuantidadeEstoque" HeaderText="Quantidade" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label_ConsultarEstoque" runat="server" Text="Nenhum Registro Localizado com os Parâmetros Especificados."></asp:Label>
                                </EmptyDataTemplate>
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma Sala de Vacina."
                ControlToValidate="ddlSalaVacina" ValueToCompare="-1" Operator="GreaterThan"
                Display="None" ValidationGroup="ValidationGroup_ConsultarEstoque"></asp:CompareValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Qtd Aplicação."
                Display="None" ControlToValidate="TextBox_QtdAplicacao" ValidationGroup="ValidationGroup_ConsultarEstoque"
                ValidationExpression="^\d*$">
            </asp:RegularExpressionValidator>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_ConsultarEstoque" />
        </p>
    </div>
</asp:Content>
