<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormVinculoServicoUnidade.aspx.cs"
    Inherits="ViverMais.View.ServicoSaude.FormVinculoServicoUnidade" MasterPageFile="~/ServicoSaude/ServicoSaude.Master" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="ContentPlaceHolder2">
    <link rel="stylesheet" href="style_form_servico2.css" type="text/css" media="screen" />
  </asp:Content>
  <asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder3">

 
      <div id="top" style="margin-left:50px">
        <fieldset class="formulario">
            <legend>Cadastrar Serviço</legend>
             <br />
            <p>
                <span class="rotulo">Serviço</span> <span>
                    <asp:DropDownList ID="ddlServico" runat="server" CssClass="drop" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlServico_SelectedIndexChanged" >
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Selecione um Serviço"
                        ControlToValidate="ddlServico" InitialValue="-1" ValidationGroup="Saude" Font-Size="Small"></asp:RequiredFieldValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Unidade:</span> <span>                    <asp:DropDownList ID="ddlUnidade" runat="server">                    </asp:DropDownList>                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione uma Unidade"                        ControlToValidate="ddlUnidade" InitialValue="-1" ValidationGroup="Saude" Font-Size="Small"></asp:RequiredFieldValidator>                </span>            </p>            <p>
                <asp:ImageButton ID="ImgVincular" runat="server" OnClick="lknVincular_Click" ImageUrl="~/ServicoSaude/img/btn-vincular.png"
                    Style="right: 855px" Width="111px" Height="39px"></asp:ImageButton></p>
            <p>
            </p>
            <span>
                <asp:GridView ID="GridViewUnidades" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    Width="100%" OnRowCommand="GridViewUnidades_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="CNES" DataField="CNES" />
                        <asp:BoundField HeaderText="Unidade" DataField="NomeFantasia" />
                        <asp:TemplateField HeaderText="Excluir" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:LinkButton ID="cmdDelete" runat="server">
                                        <img alt="Excluir" style="border:0px;" src="../Agendamento/img/excluirr.png" />
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
        </fieldset>
    </div>
</asp:Content>