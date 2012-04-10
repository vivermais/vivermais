<%@ Page Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="FormVagas.aspx.cs" Inherits="ViverMais.View.Urgencia.FormVagas" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Vagas de Atendimento</h2>
                <fieldset class="formulario">
                    <legend>Vagas Cadastradas</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Vagas" runat="server" OnRowCommand="OnRowCommand_Acao"
                                AutoGenerateColumns="false" DataKeyNames="Unidade" Width="690px">
                                <Columns>
                                    <asp:BoundField HeaderText="Vagas Infantil" DataField="VagaInfantil" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Vagas Masculina" DataField="VagaMasculina" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Vagas Feminina" DataField="VagaFeminina" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_editar" Text="Editar"
                                        ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Selecione uma Unidade."></asp:Label>
                                </EmptyDataTemplate>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
                <asp:Panel ID="Panel_Vagas" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Dados da Vagas de Atendimento</legend>
                        <p>
                            <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                                obrigatórios</span>
                        </p>
                        <%--            <p>
                <span class="rotulo">Unidade:</span>
                <span style="margin-left: 5px;">
                    <asp:DropDownList ID="DropDownList_Unidade" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaVagas">
                        <asp:ListItem Text="Selecione..." Value="0" Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server" ControlToValidate="DropDownList_Unidade"
                         ErrorMessage="RequiredFieldValidator" InitialValue="0" SetFocusOnError="true" ValidationGroup="cadConsultorio">*</asp:RequiredFieldValidator>
                </span>
            </p>--%>
                        <p>
                            <span class="rotulo">Vagas Infantil</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_VagasInfantil" CssClass="campo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="TextBox_VagasInfantil" ErrorMessage="RequiredFieldValidator"
                                    SetFocusOnError="true" ValidationGroup="cadConsultorio">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Font-Size="XX-Small"
                                    runat="server" ErrorMessage="Error" SetFocusOnError="true" ValidationExpression="\d*"
                                    ControlToValidate="TextBox_VagasInfantil" ValidationGroup="cadConsultorio">Digite somente números!</asp:RegularExpressionValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Vagas Masculina</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_VagasMasculina" CssClass="campo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="TextBox_VagasMasculina" ErrorMessage="RequiredFieldValidator"
                                    SetFocusOnError="true" ValidationGroup="cadConsultorio">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small"
                                    runat="server" ErrorMessage="Error" SetFocusOnError="true" ValidationExpression="\d*"
                                    ControlToValidate="TextBox_VagasMasculina" ValidationGroup="cadConsultorio">Digite somente números!</asp:RegularExpressionValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Vagas Feminina</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="TextBox_VagasFeminina" CssClass="campo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="TextBox_VagasFeminina" ErrorMessage="RequiredFieldValidator"
                                    SetFocusOnError="true" ValidationGroup="cadConsultorio">*</asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="XX-Small"
                                    runat="server" ErrorMessage="Error" SetFocusOnError="true" ValidationExpression="\d*"
                                    ControlToValidate="TextBox_VagasFeminina" ValidationGroup="cadConsultorio">Digite somente números!</asp:RegularExpressionValidator></span>
                        </p>
                        <p align="center">
                            <asp:ImageButton ID="ButtonSalvarVaga" runat="server" ImageUrl="~/Urgencia/img/bts/btn_salvar1.png" Width="134px" Height="38px" CommandArgument="alterar"
                                OnClick="OnClick_SalvarVagas" ValidationGroup="cadConsultorio" />
                            <asp:ImageButton ID="ButtonCancelar" runat="server" ImageUrl="~/Urgencia/img/bts/btn_cancelar1.png" Width="134px" Height="38px" OnClick="OnClick_Cancelar" />
                        </p>
                    </fieldset>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
