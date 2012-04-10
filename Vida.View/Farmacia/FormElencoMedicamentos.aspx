<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="true"
    CodeBehind="FormElencoMedicamentos.aspx.cs" Inherits="ViverMais.View.Farmacia.FormElencoMedicamentos"
    Title="ViverMais - Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function showTooltip(obj) {
            if (obj.options[obj.selectedIndex].title == "") {
                obj.title = obj.options[obj.selectedIndex].text;
                obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
                for (i = 0; i < obj.options.length; i++) {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel runat="server" ID="up">
        <ContentTemplate>--%>
            <div id="top">
                <h2>
                    Formulário de Elenco de Medicamentos</h2>
                <fieldset class="formulario">
                    <legend>Dados do Novo Elenco</legend>
                    <p>
                        <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                            obrigatórios</span>
                    </p>
                    <p>
                        <span class="rotulo">Descrição:</span> <span style="margin-left: 5px;">
                            <asp:TextBox ID="tbxElenco" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxElenco" ErrorMessage="RequiredFieldValidator" InitialValue=""
                                SetFocusOnError="true">*</asp:RequiredFieldValidator>
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Medicamentos</legend>
                    <p>
                        <span class="rotulo">Medicamentos:</span> 
                        <span>
                            <asp:DropDownList ID="ddlMedicamento" runat="server" Width="395px" CssClass="drop">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                                InitialValue="0" ValidationGroup="Medicamento" ControlToValidate="ddlMedicamento"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click"
                                ValidationGroup="Medicamento" />
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Lista de Medicamentos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridMedicamentos" runat="server" AutoGenerateColumns="False" Width="660px"
                                OnRowDeleting="gridMedicamentos_RowDeleting" DataKeyNames="Codigo">
                                <Columns>
                                    <asp:BoundField DataField="CodMedicamento" HeaderText="Código" />
                                    <asp:BoundField DataField="Nome" HeaderText="Medicamento" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label_Empty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Vincular Sub-Elencos</legend>
                    <p>
                        <span class="rotulo">Sub-Elenco:</span> <span style="margin-left: 5px;">
                            <asp:DropDownList ID="ddlSubElenco" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                InitialValue="0" ValidationGroup="SubElenco" ControlToValidate="ddlSubElenco"></asp:RequiredFieldValidator>
                            <asp:Button ID="btnAdicionar2" runat="server" Text="Adicionar" OnClick="btnAdicionar2_Click"
                                ValidationGroup="SubElenco" />
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Lista de Sub-Elencos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridSubElenco" runat="server" AutoGenerateColumns="False" Width="660px"
                                OnRowDeleting="gridSubElenco_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                    <asp:BoundField DataField="Nome" HeaderText="Sub-elenco" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label_Empty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <p align="center">
                        <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" />
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                            PostBackUrl="~/Farmacia/FormExibeElenco.aspx" />
                        <%--<asp:LinkButton ID="LinkButton_Excluir" runat="server" Visible="false"
                         OnClick="OnClick_ExcluirElenco" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este elenco ?');">Excluir</asp:LinkButton>--%>
                    </p>
                </fieldset>
            </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
