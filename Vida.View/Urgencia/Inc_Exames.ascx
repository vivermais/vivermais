<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_Exames.ascx.cs" Inherits="ViverMais.View.Urgencia.Inc_Exames" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>

<fieldset class="formulario2">
    <legend>Exames</legend>
    <p>
        <asp:UpdatePanel ID="UpdatePanel_Treze" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames" EventName="Click" />
            </Triggers>
            <ContentTemplate>
             <span class="rotulo">Exames:</span>
                <span style="margin-left: 5px;">
                <asp:DropDownList ID="ddlExames" runat="server">
                </asp:DropDownList>
                
                <asp:Button ID="btnAdicionarExames" runat="server" Text="+" CausesValidation="true" OnClick="btnAdicionarExames_Click"
           Height="19px" Width="28px" ValidationGroup="Exames" />
           </span>
        <asp:CompareValidator ID="CompareValidator_Exames" runat="server" Display="None" ControlToValidate="ddlExames" ErrorMessage="Selecione um Exame!"
            ValidationGroup="Exames" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
        <asp:ValidationSummary ID="ValidationSummary_Um" runat="server" ValidationGroup="Exames" ShowMessageBox="true" ShowSummary="false" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdicionarExames" EventName="Click" />
        </Triggers>
        <ContentTemplate>
        <p>
            <span>
                <asp:GridView ID="gridExames" runat="server" Width="600px" AutoGenerateColumns="False"
                    OnRowDeleting="gridExames_RowDeleting">
                    <Columns>
                        <asp:BoundField DataField="Exame" HeaderText="Exame" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                        <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow_left" />
                </asp:GridView>
            </span>
        </p>
        </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>