<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormCadastroGrupoAbrangencia.aspx.cs" Inherits="ViverMais.View.Agendamento.FormCadastroGrupoAbrangencia"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<div id="top">
    <fieldset class="formulario">
        <legend>Cadastro de Grupos Abrangência</legend>
        <p>
            <span class="rotulo">Nome do grupo</span>
            <asp:TextBox ID="tbxNomeGrupo" runat="server" CssClass="campo" MaxLength="100"></asp:TextBox>
            <span style="position: absolute;">
                <asp:ImageButton ID="btnAddGrupo" runat="server" OnClick="btnAddGrupo_Click"
                    Width="19px" Height="19px" ImageUrl="~/Agendamento/img/add.png" />
            </span>
        </p>
        <div class="botoesroll">
            <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click">
                <img id="imgSalvar" alt="" src="img/salvar_1.png"
                onmouseover="imgSalvar.src='img/salvar_2.png';"
                onmouseout="imgSalvar.src='img/salvar_1.png';" />
            </asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/BuscaGrupoAbrangencia.aspx"
                CausesValidation="False">
            <img id="img_voltar" alt="" src="img/voltar_1.png"
            onmouseover="img_voltar.src='img/voltar_2.png';"
            onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton>
        </div>
        <p>
            &nbsp</p>
        <p>
            &nbsp</p>
        <p>
            &nbsp</p>
        <asp:Panel runat="server" ID="PanelAutorizacao" Visible="true">
            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Grupos Ativos">
                    <ContentTemplate>
                        <p>
                            <asp:Label ID="lblSemRegistroAtivo" runat="server" Text="Nenhum Grupo Ativo" Visible="false"
                                Font-Size="X-Small"></asp:Label>
                        </p>
                        <p>
                            <asp:GridView ID="GridViewGruposAbrangencia" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                DataKeyNames="Codigo" BackColor="White" BorderColor="#477ba5" BorderStyle="None"
                                BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="50%" OnRowCommand="GridViewGruposAbrangencia_RowCommand"
                                OnPageIndexChanging="GridViewGruposAbrangencia_PageIndexChanging">
                                <FooterStyle BackColor="#cfdbe7" ForeColor="Black" />
                                <RowStyle BackColor="#e2e7ec" ForeColor="Black" Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormCadastroGrupoAbrangencia.aspx?codigo={0}"
                                        DataTextField="NomeGrupo" HeaderText="Grupos" ItemStyle-CssClass="" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:HyperLinkField>
                                    <asp:TemplateField HeaderText="Inativar" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="cmdDelete" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja inativar este Grupo?');"
                                                Text="">
                                                <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png"
                                                    Visible="true" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                 <SelectedRowStyle BackColor="#cfdbe7" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5d8bb0" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                            <AlternatingRowStyle BackColor="#cfdbe7" />
                            </asp:GridView>
                        </p>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Grupos Inativos">
                    <ContentTemplate>
                        <fieldset>
                            <legend>Grupos de Abrangência Inativos</legend>
                            <p>
                                <asp:Label ID="lblSemRegistroInativo" runat="server" Text="Nenhum Grupo Inativo"
                                    Visible="false" Font-Size="X-Small"></asp:Label>
                            </p>
                            <p>
                                <asp:GridView ID="GridViewGruposAbrangenciaInativo" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                    DataKeyNames="Codigo" BackColor="White" BorderColor="#477ba5" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="50%" OnPageIndexChanging="GridViewGruposAbrangenciaInativo_PageIndexChanging"><%--  OnRowCommand="GridViewGruposAbrangencia_RowCommand"--%>
                                    <FooterStyle BackColor="#e2e7ec" ForeColor="Black" />
                                    <RowStyle BackColor="#e2e7ec" ForeColor="Black" Font-Size="11px" />
                                    <Columns>
                                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true" ItemStyle-HorizontalAlign="Center">
                                            <HeaderStyle CssClass="colunaEscondida" />
                                            <ItemStyle CssClass="colunaEscondida" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="NomeGrupo" HeaderText="Grupos Inativos" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <SelectedRowStyle BackColor="#cfdbe7" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5d8bb0" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                            <AlternatingRowStyle BackColor="#cfdbe7" />
                                </asp:GridView>
                            </p>
                        </fieldset>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </asp:Panel>
    </fieldset>
    </div>
</asp:Content>
