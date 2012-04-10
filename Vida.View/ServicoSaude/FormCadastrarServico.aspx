<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormCadastrarServico.aspx.cs"
    Title="Cadastro Serviço Saúde" Inherits="ViverMais.View.ServicoSaude.FormCadastrarServico"
    MasterPageFile="~/ServicoSaude/ServicoSaude.Master" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <link rel="stylesheet" href="style_form_servico2.css" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder3">
    <div id="top" style="margin-left:50px">
        <fieldset class="formulario">
            <legend>Cadastrar Serviço</legend>
            <br />
            <p>
                <span class="rotulo">Nome:</span> <span>
                    <asp:TextBox ID="tbxNomeServico" runat="server" AutoPostBack="True" CssClass="campo"></asp:TextBox><asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNomeServico"
                        Display="Dynamic" ErrorMessage="* Digite o Servico" Font-Size="X-Small"></asp:RequiredFieldValidator>
                </span>
            </p>
            <div class="botoesroll">
                <asp:ImageButton ID="imgSalvar" runat="server" OnClick="lknSalvar_Click" ImageUrl="~/ServicoSaude/img/btn-salvar.png"
                    Width="100px" Height="39px"></asp:ImageButton>
            </div>
            <br />
            <p>
                <span>
                    <asp:GridView ID="GridViewServico" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Width="100%" OnPageIndexChanging="GridViewServico_PageIndexChanging" OnRowCommand="GridViewServico_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="Codigo" HeaderText="Codigo"></asp:BoundField>
                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" HeaderText="Servico" DataTextField="Nome"
                                DataNavigateUrlFormatString="FormCadastrarServico.aspx?id_servico={0}">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:HyperLinkField>
                            <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="cmdDelete" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                        CommandName="Excluir" OnClientClick="javascript : return confirm('Tem certeza que deseja excluir este Serviço?');"
                                        Text="">
                                        <asp:Image ID="Excluir" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="tab" BackColor="#28718e" Font-Bold="True" ForeColor="#ffffff"
                            Height="16px" Font-Size="12px" />
                        <FooterStyle BackColor="#72b4cf" ForeColor="#ffffff" />
                        <RowStyle CssClass="tabrow" BackColor="#72b4cf" ForeColor="#ffffff" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <PagerStyle BackColor="#72b4cf" ForeColor="#ffffff" HorizontalAlign="Right" />
                        <AlternatingRowStyle BackColor="#72b4cf" />
                    </asp:GridView>
                </span>
            </p>
        </fieldset>
    </div>
</asp:Content>
