<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_SuspeitaDiagnosticaPrescricao.ascx.cs" Inherits="ViverMais.View.Urgencia.Inc_SuspeitaDiagnosticaPrescricao" %>
<%@ OutputCache Duration="1" VaryByParam="none" %>

<fieldset class="formulario2">
    <legend>Suspeita Diagnóstica</legend>
    <p>
        <span class="rotulo">Código CID:</span> <span style="margin-left: 5px;">
            <asp:TextBox ID="TextBox_CID" CssClass="campo" runat="server"></asp:TextBox>
            <asp:Button ID="Button_BuscaCID" runat="server" Text="Buscar" CausesValidation="true" OnClick="OnClick_BuscarCID"
                ValidationGroup="ValidationGroup_BuscaCID" />
    </p>
    <p>
        <span class="rotulo">Grupo CID:</span> <span style="margin-left: 5px;">
            <asp:DropDownList ID="ddlGrupoCid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnSelectedIndexChanged_BuscarCids">
            </asp:DropDownList>
        </span>
    </p>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button_BuscaCID" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlGrupoCid" EventName="SelectedIndexChanged" />
        </Triggers>
        <ContentTemplate>
            <p>
                <span class="rotulo">CID:</span>
                <span style="margin-left: 5px;">
                    <asp:DropDownList ID="ddlCid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaProcedimentos" DataTextField="Nome" DataValueField="Codigo"
                        Width="395px">
                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCid" runat="server" ErrorMessage="" ValidationGroup="ValidationGroup_cadCid" InitialValue="0"></asp:RequiredFieldValidator>
                    <asp:Button ID="btnAdicionarCid" runat="server" OnClick="btnAdicionarCid_Click" Text="+" CausesValidation="true"
                        ValidationGroup="ValidationGroup_cadCid" Height="19px" Width="28px" />
                </span>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdicionarCid" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <p>
            <span>
                <asp:GridView ID="gridCid" DataKeyNames="Codigo" OnRowDeleting="gridCid_RowDeleting"
                    runat="server" Width="600px" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Codigo" HeaderText="Codigo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Nome" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center"/>
                        <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DeleteText="Excluir"/>                                        </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow_left" />
                </asp:GridView>
            </span>
        </p>
    </ContentTemplate>
    </asp:UpdatePanel>
</fieldset>

<div>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
ErrorMessage="Código CID é Obrigatório!" ControlToValidate="TextBox_CID" Display="None"
ValidationGroup="ValidationGroup_BuscaCID"></asp:RequiredFieldValidator>
<asp:ValidationSummary ID="ValidationSummary4" Font-Size="XX-Small" runat="server"
ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_BuscaCID" />
<asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
ErrorMessage="Selecione um CID!" Display="None" ControlToValidate="ddlCid" ValueToCompare="0"
Operator="GreaterThan" ValidationGroup="ValidationGroup_cadCid"></asp:CompareValidator>
<asp:ValidationSummary ID="ValidationSummary3" Font-Size="XX-Small" runat="server"
ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadCid" />
</div>

<asp:Panel ID="Panel_Prescricao" runat="server">
<fieldset class="formulario2">
<legend>Prescrição</legend>
<fieldset class="formulario3">
<legend>Procedimentos (SIGTAP)</legend>
 <asp:UpdatePanel ID="UpdatePanel_Quatro" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="UpdatePanel2$ddlCid" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimento1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
       <p>
            <span class="rotulo">Procedimento</span>
            <span style="margin-left: 5px;">
                <asp:DropDownList ID="DropDownList_Procedimento" runat="server" Width="395px">
                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </p>
        <p>
            <span class="rotulo">Intervalo</span>
            <span style="margin-left: 5px;">
                <asp:TextBox ID="TextBox_IntervaloProcedimento" CssClass="campo" runat="server"></asp:TextBox>
            </span>
        </p>
        <p>
            <span>
                <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um procedimento!" ControlToValidate="DropDownList_Procedimento" Display="None" Operator="GreaterThan" ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimento"></asp:CompareValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Intervalo é Obrigatório!" Display="None" ValidationGroup="ValidationGroup_cadProcedimento" ControlToValidate="TextBox_IntervaloProcedimento"></asp:RequiredFieldValidator>
                <asp:ImageButton ID="Button_AdicionarProcedimento1" runat="server" CausesValidation="true" Width="134px" Height="38px"
                    OnClick="OnClick_AdicionarProcedimento" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                    ValidationGroup="ValidationGroup_cadProcedimento"/>
                <asp:ValidationSummary ID="ValidationSummary_procedimento" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadProcedimento" />
            </span>
        </p>
        </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_Cinco" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimento1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <p>
            <span>
                <asp:GridView ID="GridView_Procedimento" OnRowDeleting="OnRowDeleting_DeletarProcedimento"
                    runat="server" Width="600px" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
                        <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" DeleteText="Excluir"/>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                    </EmptyDataTemplate>
                    <EmptyDataRowStyle HorizontalAlign="Center" />
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow_left" />
                </asp:GridView>
            </span>
        </p>
    </ContentTemplate>
</asp:UpdatePanel>
</fieldset>
<fieldset class="formulario3">
<legend>Procedimentos Não-Faturáveis</legend>
<asp:UpdatePanel ID="UpdatePanel_Quinze" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavel1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
    <p>
        <span class="rotulo">Procedimento</span>
        <span style="margin-left: 5px;">
            <asp:DropDownList ID="DropDownList_ProcedimentosNaoFaturaveis" runat="server">
                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
            </asp:DropDownList>
        </span>
    </p>
    <p>
        <span class="rotulo">Intervalo</span>
        <span style="margin-left: 5px;">
            <asp:TextBox ID="TextBox_IntervaloProcedimentoNaoFaturavel" CssClass="campo" runat="server"></asp:TextBox>
        </span>
    </p>
    <p>
        <span>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione um procedimento." ControlToValidate="DropDownList_ProcedimentosNaoFaturaveis" Display="None" Operator="GreaterThan" ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Intervalo é Obrigatório!" Display="None" ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel" ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavel"></asp:RequiredFieldValidator>
            <asp:ImageButton ID="Button_AdicionarprocedimentoNaoFaturavel1" CausesValidation="true" runat="server" Width="134px" Height="38px"
                OnClick="OnClick_AdicionarProcedimentoNaoFaturavel" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel"/>
        <asp:ValidationSummary ID="ValidationSummary5" ShowMessageBox="true" ShowSummary="false"  ValidationGroup="ValidationGroup_cadProcedimentoNaoFaturavel" runat="server" />
        </span>
    </p>
 </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel_Quatorze" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarprocedimentoNaoFaturavel1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
           <p>
                <span>
                    <asp:GridView ID="GridView_ProcedimentosNaoFaturavel" AutoGenerateColumns="false"
                         runat="server" Width="600px" OnRowDeleting="OnRowDeleting_ExcluirProcedimentoNaoFaturavel">
                         <Columns>
                            <asp:BoundField HeaderText="Procedimento" DataField="NomeProcedimento" />
                            <asp:BoundField HeaderText="Intervalo" DataField="Intervalo" />
                            <asp:CommandField ShowDeleteButton="true" ButtonType="Link" ItemStyle-HorizontalAlign="Center" DeleteText="Excluir" />
                         </Columns>
                         <HeaderStyle CssClass="tab" />
                         <RowStyle CssClass="tabrow" />
                         <EmptyDataRowStyle HorizontalAlign="Center" />
                         <EmptyDataTemplate>
                             <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                         </EmptyDataTemplate>
                    </asp:GridView>
                </span>
            </p>
    </ContentTemplate>
</asp:UpdatePanel>
</fieldset>
<fieldset class="formulario3">
<legend>Medicamentos</legend>
<asp:UpdatePanel ID="UpdatePanel_Onze" runat="server">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
       <p>
            <span class="rotulo">Medicamento</span> <span style="margin-left: 5px;">
                <asp:DropDownList ID="ddlMedicamentos" runat="server" Width="395px">
                </asp:DropDownList>
            </span>
        </p>
        <p>
            <span class="rotulo">Intervalo</span> <span style="margin-left: 5px;">
                <asp:TextBox ID="tbxIntervaloMedicamento" CssClass="campo" runat="server"></asp:TextBox>
            </span>
        </p>
        <p>
            <span class="rotulo">Via de Administração</span>
            <span style="margin-left: 5px;">
                <asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamento" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaFormaAdministracaoMedicamento">
                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </p>
    </ContentTemplate>
</asp:UpdatePanel>
    <p>
        <span class="rotulo">Forma de Administração</span>
        <asp:UpdatePanel ID="UpdatePanel_FormaAdministracao" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="UpdatePanel_Onze$DropDownList_ViaAdministracaoMedicamento" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
            <span style="margin-left: 5px;">
                <asp:DropDownList ID="DropDownList_FormaAdministracaoMedicamento" runat="server">
                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </p>
    <asp:UpdatePanel ID="UpdatePanel_Dezesseis" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
        </Triggers>
        <ContentTemplate>
           <p>
                <span class="rotulo">Observação</span>
                <span style="margin-left: 5px;">
                    <asp:TextBox ID="TextBox_ObservacaoPrescricaoMedicamento" Width="620px" Height="110px" Rows="20" Columns="5"
                     TextMode="MultiLine" runat="server"></asp:TextBox>
                </span>
            </p>
        </ContentTemplate>
    </asp:UpdatePanel>
<p>
    <span>
        <asp:ImageButton ID="btnAdicionarMedicamento1" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png" CausesValidation="true"
        onclick="btnAdicionarMedicamento_Click" ValidationGroup="ValidationGroup_PrescricaoMedicamento"
        Width="134px" Height="38px" />
        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione um medicamento!" ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="ddlMedicamentos" ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:CompareValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Intervalo é Obrigatório!" Display="None" ControlToValidate="tbxIntervaloMedicamento" ValidationGroup="ValidationGroup_PrescricaoMedicamento"></asp:RequiredFieldValidator>
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PrescricaoMedicamento" />
    </span>
</p>
 <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
<p>
    <span>
        <asp:GridView ID="gridMedicamentos" runat="server" Width="600px"
         AutoGenerateColumns="False" onrowdeleting="gridMedicamentos_RowDeleting">
            <Columns>
                <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="Intervalo" HeaderText="Intervalo" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="NomeFormaAdministracao" HeaderText="Forma Administração" ItemStyle-HorizontalAlign="Center"/>
                <asp:BoundField DataField="DescricaoObservacao" HeaderText="Observacao" ItemStyle-HorizontalAlign="Center"/>
                <asp:CommandField ShowDeleteButton="True" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
            </EmptyDataTemplate>
            <EmptyDataRowStyle HorizontalAlign="Center" />
            <HeaderStyle CssClass="tab" />
            <RowStyle CssClass="tabrow_left" />
        </asp:GridView>
    </span>
</p>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>