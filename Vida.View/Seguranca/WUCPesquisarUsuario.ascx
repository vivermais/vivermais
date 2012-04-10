<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="WUCPesquisarUsuario.ascx.cs"
    Inherits="ViverMais.View.Seguranca.WUCPesquisarUsuario" %>
<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="TagName_Estabelecimento"
    TagPrefix="TagPrefix_Estabelecimento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset>
    <legend>Pesquisar</legend>
    <asp:Panel ID="Panel_Estabelecimento" runat="server">
        <TagPrefix_Estabelecimento:TagName_Estabelecimento ID="EAS" runat="server"></TagPrefix_Estabelecimento:TagName_Estabelecimento>
        <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
            RenderMode="Block" ChildrenAsTriggers="false">
            <ContentTemplate>
                <p>
                    <span class="rotulo">Unidade</span> <span>
                        <asp:DropDownList ID="DropDownList_Estabelecimento" runat="server" Height="20px"
                            CssClass="campo" Width="380px" DataTextField="NomeFantasia" DataValueField="CNES">
                            <asp:ListItem Text="SELECIONE..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="Panel_Municipio" runat="server">
        <p>
            <span class="rotulo">Munícipio</span> <span>
                <asp:DropDownList ID="DropDownList_Municipio" runat="server" DataTextField="NomeSemUF"
                    DataValueField="Codigo" Width="380px" CssClass="campo" Height="20px">
                    <asp:ListItem Text="SELECIONE..." Value="0"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </p>
    </asp:Panel>
    <p>
        <span class="rotulo">Cartão SUS</span> <span>
            <asp:TextBox ID="TextBox_CartaoSUS" runat="server" MaxLength="15" CssClass="campo"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="rotulo">Nome</span> <span>
            <asp:TextBox ID="TextBox_NomeUsuario" runat="server" CssClass="campo" Width="300"></asp:TextBox>
        </span>
    </p>
    <p>
        <span class="rotulo">Data de Nascimento</span> <span>
            <asp:TextBox ID="TextBox_DataNascimento" runat="server" CssClass="campo"></asp:TextBox>
        </span>
    </p>
    <%--<p>--%>
    <%--<span>--%>
    <div class="botoesroll" style="margin-top: 15px">
        <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="ValidationGroup_PesquisarUsuario" OnClick="OnClick_PesquisarUsuario">
            <img id="imgfiltrar" alt="Filtrar" runat="server" />
        </asp:LinkButton>
    </div>
    <%--    <div class="botoesroll" style="margin-top: 15px">
        <asp:LinkButton ID="LinkButton2" runat="server" ValidationGroup="ValidationGroup_ListarTodosUsuarios">
            <img id="imglistartodos" alt="Listar Todos" runat="server" />
        </asp:LinkButton>
    </div>--%>
    <div class="botoesroll" style="margin-top: 15px">
        <asp:LinkButton ID="LinkButton3" runat="server">
            <img id="imgVoltar" alt="Voltar" runat="server" />
        </asp:LinkButton>
    </div>
    <%--</span>--%>
    <%--    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Selecione um Estabelecimento de Saúde."
        Display="None" ValidationGroup="ValidationGroup_PesquisarUsuario" ControlToValidate="DropDownList_Estabelecimento"
        Operator="GreaterThan" ValueToCompare="-1" SetFocusOnError="true"></asp:CompareValidator>
    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione um Estabelecimento de Saúde."
        Display="None" ValidationGroup="ValidationGroup_ListarTodosUsuarios" ControlToValidate="DropDownList_Estabelecimento"
        Operator="GreaterThan" ValueToCompare="-1" SetFocusOnError="true"></asp:CompareValidator>
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="ValidationGroup_ListarTodosUsuarios" />--%>
    <%--</p>--%>
    <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
        RenderMode="Inline">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="DropDownList_Estabelecimento" EventName="SelectedIndexChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
            <%--<asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />--%>
        </Triggers>
        <ContentTemplate>
            <p>
                <span>
                    <asp:GridView ID="GridView_Usuarios" runat="server" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="Codigo" PageSize="10" OnPageIndexChanging="OnPageIndexChanging_Usuarios"
                        Width="100%" Font-Size="x-Small" PagerSettings-Mode="Numeric">
                        <Columns>
                            <asp:ButtonField ButtonType="Link" DataTextField="Nome" HeaderText="Nome" />
                            <asp:BoundField HeaderText="Cartão SUS" DataField="CartaoSUS" />
                            <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Unidade" DataField="UnidadeToString" />
                            <asp:BoundField HeaderText="Status" DataField="Status" />
                        </Columns>
                        <HeaderStyle Font-Bold="True" Height="20px" HorizontalAlign="Center" />
                        <RowStyle Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                        <PagerStyle HorizontalAlign="Center" />
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                    </asp:GridView>
                </span>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_CartaoSUS" runat="server"
    ErrorMessage="O cartão SUS deve conter 15 dígitos." Display="None" ControlToValidate="TextBox_CartaoSUS"
    ValidationGroup="ValidationGroup_PesquisarUsuario" ValidationExpression="^\d{15}$">
</asp:RegularExpressionValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_NomeUsuario" runat="server"
    ErrorMessage="Informe pelo menos os três primeiros caracteres do nome do usuário."
    Display="None" ControlToValidate="TextBox_NomeUsuario" ValidationGroup="ValidationGroup_PesquisarUsuario"
    ValidationExpression="^\S{3}[\W-\w]*$">
</asp:RegularExpressionValidator>
<asp:CompareValidator ID="CompareValidator_DataNascimento" runat="server" ErrorMessage="Data de Nascimento inválida."
    Display="None" Type="Date" Operator="DataTypeCheck" ControlToValidate="TextBox_DataNascimento"
    ValidationGroup="ValidationGroup_PesquisarUsuario"></asp:CompareValidator>
<asp:CompareValidator ID="CompareValidator_DataNascimento2" runat="server" ErrorMessage="Data de Nascimento deve ser igual ou maior que 01/01/1900."
    ControlToValidate="TextBox_DataNascimento" Operator="GreaterThanEqual" ValueToCompare="01/01/1900"
    Type="Date" Display="None" ValidationGroup="ValidationGroup_PesquisarUsuario"></asp:CompareValidator>
<asp:CustomValidator ID="CustomValidator_PesquisarUsuario" runat="server" ErrorMessage="Usuário, informe pelo menos um dos seguintes campos: \\n\\n(1) Unidade, \\n(2) Município, \\n(3) Cartão SUS, \\n(4) Nome, \\n(5) Data de Nascimento."
    ValidationGroup="ValidationGroup_PesquisarUsuario" OnServerValidate="OnServerValidate_PesquisarUsuario"></asp:CustomValidator>
<asp:ValidationSummary ID="ValidationSummary_PesquisaUsuario" runat="server" ShowMessageBox="true"
    ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarUsuario" />
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataNascimento">
</cc1:CalendarExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataNascimento"
    InputDirection="LeftToRight" MaskType="Date" Mask="99/99/9999">
</cc1:MaskedEditExtender>
