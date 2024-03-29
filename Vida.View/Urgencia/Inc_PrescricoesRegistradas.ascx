﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_PrescricoesRegistradas.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_PrescricoesRegistradas" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Urgencia/Inc_PesquisarCid.ascx" TagName="TagNamePesquisarCID"
    TagPrefix="TagPrefixPesquisarCID" %>
<fieldset class="formulario2">
    <legend>Prescrições Registradas</legend>
    <p>
        <span>
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <asp:GridView ID="GridView_PrescricaoAlterar" DataKeyNames="Codigo" runat="server"
                        AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="5" OnPageIndexChanging="OnPageIndexChanging_PaginacaoPrescricaoAlterar"
                        OnRowDataBound="OnRowDataBound_GridView_PrescricaoAlterar">
                        <Columns>
                            <asp:BoundField DataField="DataCompleta" HeaderText="Data de Registro" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <asp:BoundField DataField="DescricaoStatus" HeaderText="Status" />
                            <asp:BoundField DataField="UltimaDataValida" HeaderText="Aprazar Até" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_VerItens" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                        OnClick="OnClick_VerItensPrescricao">Itens</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Aprazar" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                        OnClick="OnClick_AprazarPrescricao">Aprazamento</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Exluir" runat="server" CommandArgument='<%#bind("Codigo") %>'
                                        OnClick="OnClick_ExcluirPrescricao" OnClientClick="javascript:return confirm('Usuário, tem certeza que deseja excluir esta prescrição ?');">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Aprazados">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Aprazados" runat="server" OnClick="OnClick_VerMedicamentosAprazados"
                                        CommandArgument='<%#bind("Codigo") %>'>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Urgencia/img/bts/pill.png"
                                            Width="16px" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle HorizontalAlign="Center" />
                        <EmptyDataTemplate>
                            <asp:Label ID="lbEmpty" runat="server" Text="Nenhuma prescrição encontrada."></asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="tab" />
                        <RowStyle CssClass="tabrow" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </span>
    </p>
</fieldset>

<asp:UpdatePanel ID="UpdatePanel_DescricaoPrescricao" runat="server" UpdateMode="Conditional" RenderMode="Block">
    <ContentTemplate>
        <asp:Panel ID="Panel_Prescricao" runat="server" Visible="false">
            <fieldset class="formulario2">
                <legend>Prescrição</legend>
                <p>
                    <span class="rotulo">Data</span>
                    <span>
                        <asp:Label ID="Label_DataPrescricao" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="12px"></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Status</span>
                    <span>
                        <asp:Label ID="Label_StatusPrescricao" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="12px"></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Aprazar até</span>
                    <span>
                        <asp:Label ID="Label_AprazarPrescricao" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="12px"></asp:Label>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Profissional - CRM</span>
                    <span>
                        <asp:Label ID="Label_ProfissionalCRM" runat="server" Text="" Font-Bold="true" ForeColor="Black" Font-Size="12px"></asp:Label>
                    </span>
                </p>
            </fieldset>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

<asp:UpdatePanel ID="UpdatePanel_CadastrarProcedimento" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true" RenderMode="Block">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoAlterar" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_CadastrarProcedimento" runat="server" Visible="false">
             <br /><br />
             <div class="titulos-field">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Novo Procedimento</span>
            </div>
            
            <p>
                <span class="rotulo">Buscar Procedimento</span> <span>
                    <asp:TextBox ID="TextBox_BuscarProcedimentoSIGTAPAlterar" runat="server" CssClass="campo"
                        Width="250"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton4" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimentoALterar"
                        OnClick="OnClick_BuscarProcedimentoSIGTAP" ImageUrl="~/Urgencia/img/procurar.png"
                        Width="16px" Height="16px" Style="vertical-align: bottom;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Informe o nome do procedimento."
                        Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAPAlterar" ValidationGroup="ValidationGroup_BuscarProcedimentoALterar"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                        ErrorMessage="Informe pelo menos os três primeiros caracteres do procedimento."
                        Display="None" ControlToValidate="TextBox_BuscarProcedimentoSIGTAPAlterar" ValidationGroup="ValidationGroup_BuscarProcedimentoALterar"
                        ValidationExpression="^(\W{3,})|(\w{3,})$">
                    </asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary9" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimentoALterar" />
                </span>
            </p>
            <p>
                <span class="rotulo">Procedimento *</span> <span>
                    <asp:DropDownList ID="DropDownList_ProcedimentoAlterar" CssClass="drop" runat="server"
                        AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_RetiraCids"
                        Width="395px">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Freqüência *</span> <span>
                    <asp:TextBox ID="TextBox_IntervaloProcedimentoAlterar" Width="25px" CssClass="campo"
                        MaxLength="4" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimentoAlterar" CssClass="drop"
                        runat="server" AutoPostBack="true" CausesValidation="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimento">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Selecione um procedimento!"
                ControlToValidate="DropDownList_ProcedimentoAlterar" Display="None" Operator="GreaterThan"
                ValueToCompare="-1" ValidationGroup="ValidationGroup_cadProcedimentoAlterar"></asp:CompareValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator_PrescricaoFrequenciaProcedimento"
                runat="server" ErrorMessage="Freqüência é Obrigatório!" Display="None" ValidationGroup="ValidationGroup_cadProcedimentoAlterar"
                ControlToValidate="TextBox_IntervaloProcedimentoAlterar"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator_PrescricaoFrequenciaProcedimento"
                runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                ControlToValidate="TextBox_IntervaloProcedimentoAlterar" ValidationGroup="ValidationGroup_cadProcedimentoAlterar"
                ValidationExpression="^\d*$">
            </asp:RegularExpressionValidator>
            <asp:CompareValidator ID="CompareValidator_PrescricaoFrequenciaProcedimento" runat="server"
                ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimentoAlterar"
                Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ValidationGroup_cadProcedimentoAlterar"></asp:CompareValidator>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<TagPrefixPesquisarCID:TagNamePesquisarCID ID="WUC_PrescricaoProcedimentoCID" runat="server" />
<asp:UpdatePanel ID="UpdatePanel_ProcedimentoCIDPrescricao" runat="server" UpdateMode="Conditional"
    RenderMode="Block">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoAlterar" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="DropDownList_ProcedimentoAlterar" EventName="SelectedIndexChanged" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_CidProcedimentoPrescricao" runat="server" Visible="false">
            <p>
                <span class="rotulo">CID</span> <span>
                    <asp:DropDownList ID="DropDownList_CidAlterarProcedimento" CssClass="drop" CausesValidation="true"
                        runat="server" DataTextField="DescricaoCodigoNome" DataValueField="Codigo" Width="395px">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_BotaoAdicionarProcimentoPrescricao" runat="server"
    UpdateMode="Conditional" RenderMode="Block">
    <ContentTemplate>
        <asp:Panel ID="Panel_BotaoAdicionarProcecimentoPrescricao" runat="server" Visible="false">
            <asp:ImageButton ID="Button_AdicionarProcedimentoAlterar" runat="server" CausesValidation="true"
                Width="134px" Height="38px" OnClick="OnClick_AdicionarProcedimentoAlterar" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadProcedimentoAlterar')) return confirm('Usuário, todos os dados do procedimento estão corretos?'); return false;"
                
                 />
            <asp:ValidationSummary ID="ValidationSummary_procedimento" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_cadProcedimentoAlterar" />
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_ProcedimentoPrescricao" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="Button_AdicionarProcedimentoAlterar" EventName="Click" />
    </Triggers>
    <ContentTemplate>
    
        <asp:Panel ID="Panel_ProcedimentosPrescricao" runat="server" Visible="false">
           <br /><br />
                     <div class="titulos-field" style="top:15px;">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Procedimentos Cadastrados</span>
            </div>
            
            <p>
                <span>
                    <asp:GridView ID="GridView_ProcedimentoAlterar" DataKeyNames="CodigoProcedimento"
                        OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoAlterar" runat="server"
                        Width="100%" AutoGenerateColumns="False"
                        >
                        <Columns>
                            <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freqüência" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DescricaoCIDVinculado" HeaderText="CID" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Suspender" runat="server" CommandArgument='<%#bind("CodigoProcedimento") %>'
                                        OnClick="OnClick_SuspenderAtivarProcedimento"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Excluir" runat="server"
                                    CommandArgument='<%#bind("CodigoProcedimento") %>' OnClientClick="javascript:return confirm('Usuário, ao excluir este procedimento, os aprazamentos que ainda não foram confirmados também serão excluídos. Deseja realmente continuar?');"
                                        OnClick="OnClick_ExcluirProcedimento">Excluir</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_CadastrarProcedimentoNaoFaturavel" runat="server"
    UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Block">
    <%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView_PrescricaoAlterar" EventName="RowCommand" />
                </Triggers>--%>
    <ContentTemplate>
        <asp:Panel ID="Panel_ProcedimentoNaoFaturavel" runat="server" Visible="false">
            <div class="titulos-field">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Novo Procedimento Não-Faturável</span>
            </div>
            <p>
                <span class="rotulo">Buscar Procedimento</span> <span>
                    <asp:TextBox ID="TextBox_BuscarProcedimentoAlterar" runat="server" CssClass="campo"
                        Width="250px"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton2" runat="server" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavelAlterar"
                        OnClick="OnClick_BuscarProcedimentoNaoFaturavelAlterar" ImageUrl="~/Urgencia/img/procurar.png"
                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                </span>
            </p>
            <p>
                <span class="rotulo">Procedimento *</span> <span>
                    <asp:DropDownList ID="DropDownList_ProcedimentoNaoFaturavelCadastrar" CssClass="drop"
                        Width="395px" runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Freqüência *</span> <span>
                    <asp:TextBox ID="TextBox_IntervaloProcedimentoNaoFaturavelAlterar" Width="25px" CssClass="campo"
                        MaxLength="4" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaProcedimentoNaoFaturavelAlterar"
                        CssClass="drop" runat="server" CausesValidation="true" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaProcedimentoNaoFaturavel">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span>
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                        Width="134px" Height="38px" Text="add" OnClick="OnClick_AdicionarProcedimentoNaoFaturavelAlterar"
                        OnClientClick="javascript:if (Page_ClientValidate('ProcedimentosNaoFaturavelPrescricaoAlterar')) return confirm('Usuário, todos os dados do procedimento estão corretos?'); return false;" />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione um procedimento!"
                        ControlToValidate="DropDownList_ProcedimentoNaoFaturavelCadastrar" Display="None"
                        Operator="GreaterThan" ValueToCompare="-1" ValidationGroup="ProcedimentosNaoFaturavelPrescricaoAlterar"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel"
                        runat="server" ErrorMessage="Freqüência é Obrigatório!" ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelAlterar"
                        Display="None" ValidationGroup="ProcedimentosNaoFaturavelPrescricaoAlterar" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel"
                        runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                        ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelAlterar" ValidationGroup="ProcedimentosNaoFaturavelPrescricaoAlterar"
                        ValidationExpression="^\d*$">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator_PrescricaoFrequenciaProcedimentoNaoFaturavel"
                        runat="server" ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloProcedimentoNaoFaturavelAlterar"
                        Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="ProcedimentosNaoFaturavelPrescricaoAlterar"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ProcedimentosNaoFaturavelPrescricaoAlterar" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TextBox_BuscarProcedimentoAlterar"
                        Display="None" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavelAlterar"
                        ErrorMessage="Informe o nome do procedimento."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do Procedimento."
                        Display="None" ControlToValidate="TextBox_BuscarProcedimentoAlterar" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavelAlterar"
                        ValidationExpression="^(\W{3,})|(\w{3,})$">
                    </asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary10" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarProcedimentoNaoFaturavelAlterar" />
                </span>
            </p>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_ProcedimentoNaoFaturavelPrescricao" runat="server"
    UpdateMode="Conditional" ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ImageButton1" EventName="Click" />
        <%--<asp:AsyncPostBackTrigger ControlID="GridView_PrescricaoAlterar" EventName="RowCommand" />--%>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_ProcedimentosNaoFaturaveisPrescricao" runat="server" Visible="false">
            <div class="titulos-field">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Procedimentos Não-Faturáveis Cadastrados</span>
            </div>
            
            <p>
                <span>
                    <asp:GridView ID="GridView_ProcedimentoNaoFaturavelAlterar" runat="server" Width="100%"
                        DataKeyNames="CodigoProcedimento" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound_FormataGridViewProcedimentoNaoFaturavelAlterar"
                        >
                        <Columns>
                            <asp:BoundField DataField="NomeProcedimento" HeaderText="Procedimento" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freqüência" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Suspender" runat="server" CommandArgument='<%#bind("CodigoProcedimento") %>'
                                        OnClick="OnClick_SuspenderAtivarProcedimentoNaoFaturavel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Excluir" runat="server" CommandArgument='<%#bind("CodigoProcedimento") %>'
                                        OnClick="OnClick_ExcluirProcedimentoNaoFaturavel" OnClientClick="javascript:return confirm('Usuário, ao excluir este procedimento, os aprazamentos que ainda não foram confirmados também serão excluídos. Deseja realmente continuar?');">
                                        Excluir
                                        </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_CadastrarMedicamento" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true" RenderMode="Block">
    <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView_PrescricaoAlterar" EventName="RowCommand" />
                </Triggers>--%>
    <ContentTemplate>
        <asp:Panel ID="Panel_IncluiMedicamento" runat="server" Visible="false">
             <div class="titulos-field">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Novo(a) Medicamento/Prescrição</span>
            </div> 
            
            <p>
                <span class="rotulo">Buscar Medicamento/Prescrição</span> <span>
                    <asp:TextBox ID="TextBox_BuscarMedicamentoAlterar" runat="server" CssClass="campo"
                        Width="250"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton3" runat="server" OnClick="OnClick_BuscarMedicamentoAlterar"
                        ValidationGroup="ValidationGroup_BuscarMedicamentoAlterar" ImageUrl="~/Urgencia/img/procurar.png"
                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                </span>
            </p>
            <p>
                <span class="rotulo">Medicamento/Prescrição *</span> <span>
                    <asp:DropDownList ID="DropDownList_MedicamentoAlterar" CssClass="drop" runat="server"
                        Width="395px">
                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </span><span>
                    <asp:ImageButton ID="ImageButton_Bulario" runat="server" OnClick="OnClick_VerBulario"
                        ValidationGroup="ValidationGroup_VerBularioAlterar" ImageUrl="~/Urgencia/img/bula.png"
                        Width="16px" Height="16px" Style="position: absolute; margin-top: 3px;" />
                </span>
            </p>
            <p>
                <span class="rotulo">Freqüência *</span> <span>
                    <asp:TextBox ID="TextBox_IntervaloMedicamentoAlterar" Width="25px" CssClass="campo"
                        MaxLength="4" runat="server"></asp:TextBox>
                    <asp:DropDownList ID="DropDownList_UnidadeTempoFrequenciaMedicamentoAlterar" CssClass="drop" CausesValidation="true"
                        runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_FrequenciaMedicamento">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Via de Administração</span> <span>
                    <asp:DropDownList ID="DropDownList_ViaAdministracaoMedicamentoAlterar" CssClass="drop"
                        runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <span class="rotulo">Observação</span> <span>
                <asp:TextBox ID="TextBox_ObservacaoMedicamentoAlterar" Width="620px" CssClass="campo"
                    Height="110px" Rows="20" Columns="5" TextMode="MultiLine" runat="server"></asp:TextBox>
            </span></p>
            <p>
                <span>
                    <asp:ImageButton ID="btnAdicionarMedicamento" runat="server" ImageUrl="~/Urgencia/img/bts/btn-incluir1.png"
                        Width="134px" Height="38px" Text="add" OnClick="OnClick_AdicionarMedicamentoAlterar"
                        OnClientClick="javascript:if (Page_ClientValidate('MedicamentosPrescricao')) return confirm('Usuário, todos os dados do(a) medicamento/prescrição estão corretos?'); return false;" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um(a) medicamento/prescrição!"
                        ControlToValidate="DropDownList_MedicamentoAlterar" Display="None" Operator="GreaterThan"
                        ValueToCompare="0" ValidationGroup="MedicamentosPrescricao"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator_PrescricaoFrequenciaMedicamento"
                        runat="server" ErrorMessage="Freqüência é Obrigatório!" ControlToValidate="TextBox_IntervaloMedicamentoAlterar"
                        ValidationGroup="MedicamentosPrescricao" Display="None"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_PrescricaoFrequenciaMedicamento"
                        runat="server" ErrorMessage="Digite somente números na freqüência." Display="None"
                        ControlToValidate="TextBox_IntervaloMedicamentoAlterar" ValidationGroup="MedicamentosPrescricao"
                        ValidationExpression="^\d*$">
                    </asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator_PrescricaoFrequenciaMedicamento" runat="server"
                        ErrorMessage="Freqüência deve ser maior que zero." ControlToValidate="TextBox_IntervaloMedicamentoAlterar"
                        Display="None" Operator="GreaterThan" ValueToCompare="0" ValidationGroup="MedicamentosPrescricao"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="MedicamentosPrescricao" />
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Selecione um(a) medicamento/prescrição!"
                        ValueToCompare="0" Operator="GreaterThan" Display="None" ControlToValidate="DropDownList_MedicamentoAlterar"
                        ValidationGroup="ValidationGroup_VerBularioAlterar"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary6" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_VerBularioAlterar" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBox_BuscarMedicamentoAlterar"
                        Display="None" ValidationGroup="ValidationGroup_BuscarMedicamentoAlterar" ErrorMessage="Informe o nome do(a) medicamento/prescrição."></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Informe pelo menos os três primeiros caracteres do(a) Medicamento/Prescrição."
                        Display="None" ControlToValidate="TextBox_BuscarMedicamentoAlterar" ValidationGroup="ValidationGroup_BuscarMedicamentoAlterar"
                        ValidationExpression="^(\W{3,})|(\w{3,})$">
                    </asp:RegularExpressionValidator>
                    <asp:ValidationSummary ID="ValidationSummary7" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_BuscarMedicamentoAlterar" />
                </span>
            </p>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="UpdatePanel_MedicamentosPrescricao" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnAdicionarMedicamento" EventName="Click" />
        <%--<asp:AsyncPostBackTrigger ControlID="GridView_PrescricaoAlterar" EventName="RowCommand" />--%>
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_MedicamentosPrescricao" runat="server" Visible="false">
                 <div class="titulos-field">
            <div style="float:left"><img src="img/seta.png" alt="" /></div>
            
            <span class="titulo-nome">Medicamentos/Prescrições Cadastrados(as)</span>
            </div>
            <p>
                <span>
                    <asp:GridView ID="GridView_MedicamentoAlterar" DataKeyNames="Medicamento" runat="server"
                        Width="100%" AutoGenerateColumns="False"
                        OnRowDataBound="OnRowDataBound_FormataGridViewMedicamentoAlterar">
                        <Columns>
                            <asp:BoundField DataField="NomeMedicamento" HeaderText="Medicamento/Prescrição" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DescricaoIntervalo" HeaderText="Freqüência" ItemStyle-Width="100px"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="NomeViaAdministracao" HeaderText="Via Administração" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Observacao" HeaderText="Observação" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="SuspensoToString" HeaderText="Situação" ItemStyle-HorizontalAlign="Center" />
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Suspender" runat="server" CommandArgument='<%#bind("Medicamento") %>'
                                        OnClick="OnClick_SuspenderAtivarMedicamento"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton_Excluir" runat="server" CommandArgument='<%#bind("Medicamento") %>'
                                        OnClick="OnClick_ExcluirMedicamento" OnClientClick="javascript:return confirm('Usuário, ao excluir este(a) medicamento/prescrição, os aprazamentos que ainda não foram confirmados também serão excluídos. Deseja realmente continuar?');">
                                        Excluir
                                        </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
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
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
<%--</asp:Panel>--%>
<asp:UpdatePanel ID="UpdatePanel_VerAprazados" runat="server" UpdateMode="Conditional"
    ChildrenAsTriggers="true">
    <ContentTemplate>
        <asp:Panel ID="Panel_VerTabelaAprazamento" runat="server" Visible="false">
            <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                moz-opacity: 0.3; opacity: 0.3">
            </div>
            <div id="mensagem" visible="false" style="position: fixed; top: 100px; left: 25%;
                width: 300px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 50px; padding-bottom: 10px;
                border-left: #ffffff  5px solid; color: #000000; padding-top: 0px; border-bottom: #ffffff 5px solid;
                text-align: justify; font-family: Verdana;">
                <p style="padding: 0px 10px 30px 0">
                </p>
                <p style="color: White; font-size: medium; font-family: Arial; font-weight: bold;">
                    Pesquisar Aprazamento
                </p>
                <br />
                <p>
                    <span class="rotulo2">Data</span><span>
                        <asp:TextBox ID="TextBox_DataPesquisaAprazamento" runat="server" CssClass="campo"
                            Width="100px"></asp:TextBox>
                    </span>
                </p>
                <%--<p>--%>
                <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton_PesquisarAprazamento" runat="server" OnClick="OnClick_PesquisarAprazamento"
                        ValidationGroup="ValidationGroup_PesquisarAprazamento">
                        <asp:Image ID="Image3" runat="server" ImageUrl="~/Urgencia/img/buscar-botao.png"
                            Width="100px" Height="39px" />
                    </asp:LinkButton>
                </div>
                <%--<br />--%>
                <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_FecharPesquisaAprazamento">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Urgencia/img/fechar-btn.png" Width="100px"
                            Height="39px" />
                    </asp:LinkButton>
                </div>
                <%--</p>--%>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Data é Obrigatório."
                        ControlToValidate="TextBox_DataPesquisaAprazamento" Display="None" ValidationGroup="ValidationGroup_PesquisarAprazamento"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator9" runat="server" ErrorMessage="Data com formato inválido."
                        Operator="DataTypeCheck" Type="Date" ControlToValidate="TextBox_DataPesquisaAprazamento"
                        Display="None" ValidationGroup="ValidationGroup_PesquisarAprazamento"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                        ValueToCompare="01/01/1900" Operator="GreaterThanEqual" Type="Date" ControlToValidate="TextBox_DataPesquisaAprazamento"
                        Display="None" ValidationGroup="ValidationGroup_PesquisarAprazamento"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarAprazamento" />
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_DataPesquisaAprazamento"
                        Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="TextBox_DataPesquisaAprazamento" UserDateFormat="DayMonthYear"
                        InputDirection="LeftToRight">
                    </cc1:MaskedEditExtender>
                </p>
            </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
