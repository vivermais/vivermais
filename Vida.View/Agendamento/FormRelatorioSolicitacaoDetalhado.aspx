<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="True"
    CodeBehind="FormRelatorioSolicitacaoDetalhado.aspx.cs" Inherits="ViverMais.View.Agendamento.FormRelatorioSolicitacaoDetalhado"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="WUC_PesquisarEstabelecimento"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Relatórios</h2>
                <fieldset class="formulario4">
                    <legend>Relatório Solicitações Detalhadas</legend>
                    <p>
                        <span class="rotulo">Selecione e tipo de pesquisa</span> <span>
                            <asp:RadioButtonList ID="rbtnTipoPesquisa" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                CssClass="radio1" TextAlign="Right" OnSelectedIndexChanged="rbtnTipoPesquisa_SelectedIndexChanged">
                                <asp:ListItem Value="1">Unidade</asp:ListItem>
                                <asp:ListItem Value="2">Paciente</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </p>
                    <asp:Panel ID="PanelPaciente" runat="server" Visible="false">
                        <fieldset>
                            <legend>Paciente</legend>
                            <p>
                                <span class="rotulo">Cartão SUS</span> <span>
                                    <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" MaxLength="15" runat="server">
                                    </asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="O Cartão SUS deve conter 15 dígitos."
                                        Display="Dynamic" ControlToValidate="tbxCartaoSUS" ValidationExpression="^\d{15}$"></asp:RegularExpressionValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                        ControlToValidate="tbxCartaoSUS" Display="Dynamic" ErrorMessage="Preencha o campo Cartão SUS"></asp:RequiredFieldValidator></span>
                                </span>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelUnidade" runat="server" Visible="false">
                        <fieldset class="formulario3">
                            <legend>Unidade</legend>
                            <p>
                                <span class="rotulo">Tipo de Unidade:</span> <span>
                                    <asp:RadioButtonList ID="rbtTipoUnidade" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="S">Solicitante</asp:ListItem>
                                        <asp:ListItem Value="E">Executante</asp:ListItem>
                                    </asp:RadioButtonList>
                                </span>
                            </p>
                            <p>
                                <uc1:WUC_PesquisarEstabelecimento ID="EAS" runat="server" />
                                <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                                    RenderMode="Inline">
                                    <ContentTemplate>
                                        <p>
                                            <span class="rotulo">Unidade</span> <span>
                                                <asp:DropDownList ID="ddlUnidade" CssClass="campo" Height="21px" runat="server" DataTextField="NomeFantasia"
                                                    DataValueField="CNES" Width="380px">
                                                    <asp:ListItem Text="Selecione.." Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </p>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <%--                                <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                                    RenderMode="Inline" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:Panel ID="Panel_Unidade" runat="server" Visible="false">
                                            <p>
                                                <asp:GridView ID="grid_EstabelecimentoSaude" runat="server" AllowPaging="True" PageSize="10"
                                                    OnPageIndexChanging="onPageEstabelecimento" AutoGenerateColumns="False" DataKeyNames="CNES"
                                                    OnRowCommand="OnRowCommand_VerificarAcao" BackColor="White" BorderColor="#477ba5"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                                                    <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                                    <RowStyle BackColor="#b7cfe1" ForeColor="Black" Font-Size="11px" />
                                                    <PagerStyle BackColor="#e9e1d3" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#ffffff" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                                        Font-Size="11px" Height="30px" />
                                                    <AlternatingRowStyle BackColor="#caddeb" />
                                                    <Columns>
                                                        <asp:BoundField HeaderText="CNES" DataField="CNES" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:ButtonField ItemStyle-Width="500px" ButtonType="Link" DataTextField="NomeFantasia"
                                                            HeaderText="Nome Fantasia" CommandName="cn_visualizarEstabelecimento">
                                                            <ItemStyle Width="500px" />
                                                        </asp:ButtonField>
                                                        <asp:BoundField DataField="DescricaoStatusEstabelecimento" HeaderText="Status" ItemStyle-HorizontalAlign="Center">
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <FooterStyle BackColor="#ffffff" ForeColor="Black" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum estabelecimento encontrado."
                                                            Font-Bold="true"></asp:Label>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </p>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
--%>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelMunicipio" runat="server" Visible="false">
                        <fieldset class="formulario3">
                            <legend>Município</legend>
                            <p>
                                <span style="float: left;">
                                    <asp:RadioButtonList ID="rbtnTipoMunicipio" runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rbtnTipoMunicipio_SelectedIndexChanged">
                                        <asp:ListItem Value="1">Salvador</asp:ListItem>
                                        <asp:ListItem Value="2">Outro Município</asp:ListItem>
                                    </asp:RadioButtonList>
                                </span><span style="float: left; margin-top: 5px; margin-left: 4px">
                                    <asp:DropDownList ID="ddlMunicipios" runat="server" CssClass="drop" AutoPostBack="true"
                                        Visible="false">
                                    </asp:DropDownList>
                                </span>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelPeriodo" runat="server" Visible="false">
                        <fieldset class="formulario3">
                            <legend>Período</legend>
                            <p>
                                <span class="rotulo">Data:</span> <span>
                                    <asp:TextBox ID="tbxData_Inicial" CssClass="campo" runat="server" MaxLength="10"
                                        Width="70px">
                                    </asp:TextBox>
                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxData_Inicial"
                                        Animated="true">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                        TargetControlID="tbxData_Inicial" Mask="99/99/9999" MaskType="Date">
                                    </cc1:MaskedEditExtender>
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                        ControlToValidate="tbxData_Inicial" ErrorMessage="Campo Obrigatório">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:CompareValidator ID="compareData" runat="server" ControlToValidate="tbxData_Inicial"
                                        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data Inicial é inválida"
                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
                                    a
                                    <asp:TextBox ID="tbxData_Final" CssClass="campo" runat="server" MaxLength="10" Width="70px">
                                    </asp:TextBox>
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                        ControlToValidate="tbxData_Final" ErrorMessage="Campo Obrigatório">
                                    </asp:RequiredFieldValidator>
--%>
                                    <cc1:CalendarExtender runat="server" ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="tbxData_Final"
                                        Animated="true" >
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                                        TargetControlID="tbxData_Final" Mask="99/99/9999" MaskType="Date">
                                    </cc1:MaskedEditExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="tbxData_Final"
                                        Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data final é inválida"
                                        Operator="DataTypeCheck" Type="Date"></asp:CompareValidator></span>                                                        <p style="font-weight:bold">ou</p>                                                           <span class="rotulo">Competência (AAAAMM)</span> <span>                                    <asp:TextBox ID="tbxCompetencia" CssClass="campo" runat="server" MaxLength="10" Width="70px">
                                    </asp:TextBox>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="999999" MaskType="Number"
                                        TargetControlID="tbxCompetencia" ClearMaskOnLostFocus="true" AutoComplete="false">
                                    </cc1:MaskedEditExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small"
                                        runat="server" ErrorMessage="Formato Invalido (AAAA/MM)" ControlToValidate="tbxCompetencia"
                                        Display="Dynamic" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9]"></asp:RegularExpressionValidator>
                                </span>
                                                           </p>                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelProcedimento" runat="server" Visible="false">
                        <fieldset class="formulario3">
                            <legend>Procedimento</legend>
                            <p>
                                <asp:RadioButtonList ID="rbtnTipoProcedimento" runat="server" AutoPostBack="True"
                                    RepeatDirection="Horizontal" CssClass="radio1" TextAlign="Right" OnSelectedIndexChanged="rbtnTipoProcedimento_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Regulado</asp:ListItem>
                                    <asp:ListItem Value="2">Autorizado</asp:ListItem>
                                    <asp:ListItem Value="3">Agendado</asp:ListItem>
                                    <asp:ListItem Value="4">Atendimento Básico</asp:ListItem>
                                </asp:RadioButtonList>
                            </p>
                            <p>
                                <span class="rotulo">Procedimento</span> <span>                                    <asp:DropDownList ID="ddlProcedimento" CssClass="drop" runat="server" AutoPostBack="True" Font-Size="X-Small" Width="550px" OnMouseOver="showTooltip(this);"                                        OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged">                                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlProcedimento"
                                        Display="Dynamic" ErrorMessage="Selecione o Procedimento" Font-Size="XX-Small"
                                        Text="* Selecione o Procedimento" InitialValue="0"></asp:RequiredFieldValidator>
 --%>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Especialidade</span>
                                <span>
                                    <asp:DropDownList ID="ddlEspecialidade" CssClass="drop" Font-Size="XX-Small" runat="server"
                                        AutoPostBack="True">
                                        <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                    <%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEspecialidade"
                                        Display="Dynamic" ErrorMessage="Selecione o Procedimento" Font-Size="XX-Small"
                                        Text="* Selecione a Especialidade" InitialValue="0"></asp:RequiredFieldValidator>
--%>
                                </span>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelStatus" runat="server" Visible="false">
                        <fieldset class="formulario3">
                            <legend>Status</legend>
                            <p>
                                <asp:RadioButtonList ID="rbtStatus" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                                    CssClass="radio1" TextAlign="Right">
                                    <asp:ListItem Value="1">Pendente</asp:ListItem>
                                    <asp:ListItem Value="2">Autorizada</asp:ListItem>
                                    <asp:ListItem Value="4">Indeferida</asp:ListItem>
                                    <asp:ListItem Value="5">Confirmada</asp:ListItem>
                                    <asp:ListItem Value="6">Desmarcada</asp:ListItem>
                                </asp:RadioButtonList>
                            </p>
                        </fieldset>
                    </asp:Panel>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnGeraRelatorio" Text="Imprimir" runat="server" CausesValidation="true"
                            OnClick="btnGeraRelatorio_Click"> 
                        <img id="imgGerar" alt="Imprimir" src="img/imprimir_1.png" 
                        onmouseover="imgGerar.src='img/imprimir_2.png';"
                        onmouseout="imgGerar.src='img/imprimir_1.png';" />
                        </asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnLimpar" Text="Imprimir" runat="server" CausesValidation="true"
                            OnClick="btnLimpar_Click"> 
                        <img id="imgLimpar" alt="Limpar" src="img/btn-limpar1.png" 
                        onmouseover="imgLimpar.src='img/btn-limpar2.png';"
                        onmouseout="imgLimpar.src='img/btn-limpar1.png';" />
                        </asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                        <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                            CausesValidation="False">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                    </div>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
