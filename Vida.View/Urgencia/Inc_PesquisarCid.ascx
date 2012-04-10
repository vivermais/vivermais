<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_PesquisarCid.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_PesquisarCid" %>
<asp:UpdatePanel ID="UpdatePanel_PesquisarCID" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true" RenderMode="Inline">
    <ContentTemplate>
        <asp:Panel ID="Panel_PesquisarCID" runat="server">
            <p>
                <span class="rotulo">CID</span><span>
                    <asp:TextBox ID="TextBox_CID" CssClass="campo" runat="server" Width="400px"></asp:TextBox></span>
                <span style="position: absolute; padding-top: 4px;">
                    <asp:ImageButton ID="ImageButton_BuscarNomeCID" runat="server" ImageUrl="~/Urgencia/img/procurar.png"
                        Width="16px" Height="16px" CausesValidation="true" /></span>
            </p>
            <p>
                <span class="rotulo">Código do CID</span> <span>
                    <asp:TextBox ID="TextBox_CodigoCID" CssClass="campo" runat="server"></asp:TextBox></span>
                <span style="position: absolute; padding-top: 4px;">
                    <asp:ImageButton ID="ImageButton_BuscarCodigoCID" runat="server" ImageUrl="~/Urgencia/img/procurar.png"
                        Width="16px" Height="16px" CausesValidation="true" /></span>
            </p>
            <p>
                <span class="rotulo">Grupo do CID</span> <span>
                    <asp:DropDownList ID="DropDownList_GrupoCID" runat="server" CssClass="drop" AutoPostBack="True">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_BuscarCodigoCid" runat="server"
                    EnableClientScript="true" ErrorMessage="Código CID é Obrigatório!" ControlToValidate="TextBox_CodigoCID"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator_BuscarNomeCid" runat="server"
                    EnableClientScript="true" ErrorMessage="CID é Obrigatório!" ControlToValidate="TextBox_CID"
                    Display="None"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator_BuscarNomeCid" runat="server"
                    ErrorMessage="Informe pelo menos os três primeiros caracteres do CID." Display="None"
                    ControlToValidate="TextBox_CID" ValidationExpression="^\S{3}[\W-\w]*$">
                </asp:RegularExpressionValidator>
                <asp:ValidationSummary ID="ValidationSummary_BuscarCodigoCid" runat="server" EnableClientScript="true"
                    ShowMessageBox="true" ShowSummary="false" />
                <asp:ValidationSummary ID="ValidationSummary_BuscarNomeCid" runat="server" EnableClientScript="true"
                    ShowMessageBox="true" ShowSummary="false" />
            </p>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
