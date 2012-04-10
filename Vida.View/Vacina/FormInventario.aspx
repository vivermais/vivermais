<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormInventario.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormInventario" EnableEventValidation="false" MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Formulário para Cadastro de Inventário</h2>
        <fieldset class="formulario">
            <legend>Inventário</legend>
            <%--            <p>
                <span class="rotulo">Unidade de Saúde</span> <span>
                    <asp:Label ID="LabelUnidadeSaude" runat="server" Text="" Font-Bold="true"></asp:Label>
                </span>
            </p>--%>
            <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:DropDownList ID="DropDownList_SalaVacina" CssClass="drop" runat="server" CausesValidation="true"
                        Width="300px" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaInventarios"
                        AutoPostBack="true" DataTextField="Nome" DataValueField="Codigo">
                        <%--<asp:ListItem Text="Selecione..." Value="-1">
                        </asp:ListItem>--%>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Data de Abertura</span> <span>
                    <asp:TextBox ID="TextBox_DataAbertura" CssClass="campo" ReadOnly="true" runat="server"></asp:TextBox>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="lnk_Salvar_Click" OnClick="OnClick_Salvar" CausesValidation="true"
                    runat="server" OnClientClick="javascript:if (Page_ClientValidate()) return confirm('Tem certeza que deseja abrir o inventário ?'); return false;"
                    ValidationGroup="group_cadInventario">
                  <img id="imgSalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgSalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgSalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="LknButton_Cancelar" runat="server" PostBackUrl="~/Vacina/Default.aspx">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn_cancelar1.png';" /></asp:LinkButton>
            </div>
            <p>
                <span>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Sala de Vacina deve ser escolhida!"
                        ControlToValidate="DropDownList_SalaVacina" ValueToCompare="-1" Operator="GreaterThan"
                        ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                        TargetControlID="TextBox_DataAbertura" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataAbertura">
                    </cc1:CalendarExtender>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Abertura é Obrigatório!"
                        ControlToValidate="TextBox_DataAbertura" Display="None" ValidationGroup="group_cadInventario"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Abertura com formato inválido!"
                        ControlToValidate="TextBox_DataAbertura" Operator="DataTypeCheck" Type="Date"
                        ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Abertura deve ser maior que 01/01/1900!"
                        ControlToValidate="TextBox_DataAbertura" Operator="GreaterThan" ValueToCompare="01/01/1900"
                        Type="Date" ValidationGroup="group_cadInventario" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="group_cadInventario" />
                </span>
            </p>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel_InventariosCadastrados" runat="server" UpdateMode="Conditional"
            ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_SalaVacina" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_InventariosSalaVacina" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Inventários da Sala Vacina</legend>
                        <p>
                            <span class="rotulo">Sala de Vacina </span><span>
                                <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Unidade </span><span>
                                <asp:Label ID="Label_Unidade" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <br />
                            <asp:GridView ID="GridView_Inventario" runat="server" AllowPaging="True" Width="100%"
                                PageSize="10" OnPageIndexChanging="OnPageIndexChanging_Paginacao" AutoGenerateColumns="False"
                                PagerSettings-Mode="Numeric" OnRowDataBound="OnRowDataBound_FormatarGridView"
                                BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                                CellPadding="3" GridLines="Horizontal">
                                <Columns>
                                    <asp:TemplateField HeaderText="Data do Inventário" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                                Text='<%#bind("DataInventario") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Situação" DataField="Situacao" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                    </asp:BoundField>
                                </Columns>
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#4A3C8C" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum inventário encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
