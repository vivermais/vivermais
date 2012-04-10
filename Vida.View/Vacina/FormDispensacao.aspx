<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormDispensacao.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormDispensacao" MasterPageFile="~/Vacina/MasterVacina.Master"
    EnableEventValidation="false" %>
<%@ Register Src="~/WUC_MensagemIE.ascx" TagName="IExplorer" TagPrefix="IE" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Paciente/WUCExibirPaciente.ascx" TagName="WUCExibirPaciente"
    TagPrefix="uc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Formul�rio de Dispensa��o</h2>
        <%--<IE:IExplorer ID="IExplorer" runat="server" />--%>
        <p>
            <span>
                <uc1:WUCExibirPaciente ID="WUCExibirPaciente1" runat="server" />
            </span>
        </p>
        <fieldset class="formulario">
            <legend>Dados da Dispensa��o</legend>
            <p>
                <span class="rotulo">Sala de Vacina</span> <span>
                    <asp:Label ID="Label_SalaVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                    <asp:DropDownList ID="DropDownList_SalaVacina" runat="server" AutoPostBack="true"
                        CssClass="drop" Width="400px" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaItensDispensacao"
                        DataValueField="Codigo" DataTextField="Nome">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Grupo de Atendimento</span> <span>
                    <asp:DropDownList ID="ddlGrupoAtendimento" CssClass="drop" runat="server" Width="400px"
                        DataTextField="Descricao" DataValueField="Codigo" />
                </span>
            </p>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:PostBackTrigger ControlID="lknsalvar" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_SalaVacina" EventName="SelectedIndexChanged" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_DadosDispensacao" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Itens da Dispensa��o</legend>
                        <p>
                            <span class="rotulo">Estrat�gias</span> <span>
                                <asp:DropDownList ID="ddlEstrategias" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlEstrategias_SelectedIndexChanged"
                                    DataTextField="Descricao" DataValueField="Codigo" AutoPostBack="true">
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Imunobiol�gico</span> <span>
                                <asp:DropDownList ID="ddlImunobiologico" CssClass="drop" runat="server" AutoPostBack="true"
                                    Width="400px" DataTextField="Nome" DataValueField="Codigo" OnSelectedIndexChanged="ddlImunobiologico_SelectedIndexChanged">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Fabricante</span> <span>
                                <asp:DropDownList ID="DropDownList_Fabricante" CssClass="drop" runat="server" AutoPostBack="true"
                                    Width="400px" DataTextField="Nome" DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_Lote">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Lote/Validade/Aplica��o</span> <span>
                                <asp:DropDownList ID="ddlLote" CssClass="drop" runat="server" Width="400px" DataTextField="DescricaoAgregada"
                                    DataValueField="Codigo">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Dose</span> <span>
                                <asp:DropDownList ID="ddlDose" CssClass="drop" DataTextField="Descricao" DataValueField="Codigo"
                                    runat="server" Width="250px">
                                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Abertura de Ampola</span> <span>
                                <asp:CheckBox ID="chkAberturaAmpola" runat="server" /></span>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknAddImunobiologico" runat="server" OnClick="btnAddImunobiologico_Click"
                                ValidationGroup="GroupAddImunobiologico">
                  <img id="imgadicionar" alt="Adicionar" src="img/btn_adicionar1.png"
                  onmouseover="imgadicionar.src='img/btn_adicionar2.png';"
                  onmouseout="imgadicionar.src='img/btn_adicionar1.png';" /></asp:LinkButton>
                        </div>
                        <p>
                            <asp:GridView ID="GridViewItensDispensacao" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="GridViewItensDispensacao_RowDeleting" DataKeyNames="Codigo" BackColor="White"
                                BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                Font-Names="Verdana" Width="100%">
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundField HeaderText="Vacina" DataField="Vacina" ItemStyle-Width="200px" />
                                    <asp:BoundField HeaderText="Lote" DataField="DescricaoLote" ItemStyle-Width="70px" />
                                    <asp:BoundField HeaderText="Fabricante" DataField="VacinaFabricante" ItemStyle-Width="100px" />
                                    <asp:BoundField HeaderText="Validade" DataField="ValidadeLote" DataFormatString="{0:dd/MM/yyyy}" ItemStyle-Width="70px" />
                                    <asp:BoundField HeaderText="Dose" DataField="DescricaoDose" />
                                    <asp:BoundField HeaderText="Abertura de Ampola" DataField="DescricaoAberturaAmpola"
                                        ItemStyle-Width="100px" />
                                    <asp:TemplateField>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Delete"
                                                CommandArgument="<%# Container.DataItemIndex %>" Height="15px" Width="15px" ImageUrl="~/Vacina/img/excluir_gridview.png"
                                                ToolTip="Excluir" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" Font-Size="Smaller" Font-Bold="true" />
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </p>
                        <p>
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Estrat�gia � Obrigat�rio."
                                ValidationGroup="GroupAddImunobiologico" Display="None" ControlToValidate="ddlEstrategias"
                                ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Imunobiol�gico � Obrigat�rio."
                                ValidationGroup="GroupAddImunobiologico" Display="None" ControlToValidate="ddlImunobiologico"
                                ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Fabricante � Obrigat�rio."
                                ValidationGroup="GroupAddImunobiologico" Display="None" ControlToValidate="DropDownList_Fabricante"
                                ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Lote � Obrigat�rio."
                                ValidationGroup="GroupAddImunobiologico" Display="None" ControlToValidate="ddlLote"
                                ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Dose � Obrigat�rio."
                                ValidationGroup="GroupAddImunobiologico" Display="None" ControlToValidate="ddlDose"
                                ValueToCompare="-1" Operator="GreaterThan"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                                ShowSummary="false" ValidationGroup="GroupAddImunobiologico" />
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="botoesroll">
            <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Vacina/FormPesquisaPaciente.aspx?tipo=dispensacao"
                CausesValidation="false">
			        <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  	onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  	onmouseout="imgvoltar.src='img/btn_voltar1.png';" />	
            </asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="lknsalvar" runat="server" OnClick="btnSalvar_click" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_CadDispensacao')) return confirm('Todos os dados da dispensa��o est�o corretos ?');return false;">
                  <img id="imgsalvar" alt="Vadascrar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="lknCartaoVacina" runat="server" PostBackUrl="~/Vacina/FormCartaoVacina.aspx"
                CausesValidation="false">
                  <img id="imgcartaovacina" alt="Voltar" src="img/btn_cartao_vacina1.png"
                  onmouseover="imgcartaovacina.src='img/btn_cartao_vacina2.png';"
                  onmouseout="imgcartaovacina.src='img/btn_cartao_vacina1.png';" /></asp:LinkButton>
        </div>
        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Sala de Vacina � Obrigat�rio."
            ControlToValidate="DropDownList_SalaVacina" ValueToCompare="-1" Operator="GreaterThan"
            ValidationGroup="ValidationGroup_CadDispensacao" Display="None"></asp:CompareValidator>
        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Grupo de Atendimento � Obrigat�rio."
            ControlToValidate="ddlGrupoAtendimento" ValueToCompare="-1" Operator="GreaterThan"
            ValidationGroup="ValidationGroup_CadDispensacao" Display="None"></asp:CompareValidator>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
            ShowSummary="false" ValidationGroup="ValidationGroup_CadDispensacao" />
    </div>
</asp:Content>
