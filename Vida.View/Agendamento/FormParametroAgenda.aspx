<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormParametroAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.FormParametroAgenda"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="WUC_PesquisarEstabelecimento"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Parâmetros Agenda Ambulatorial</h2>
        <fieldset class="formulario" style="width: 900px;">
            <legend>Formulário</legend>
            <p>
                <span class="rotulo">Tipo de Configuração:</span>
                <asp:RadioButtonList ID="rbtTipo" runat="server" RepeatDirection="Horizontal" TextAlign="Right"
                    CellPadding="0" CellSpacing="0" CssClass="radio" OnSelectedIndexChanged="rbtTipo_SelectedIndexChanged"
                    AutoPostBack="true">
                    <asp:ListItem Value="U">Unidade</asp:ListItem>
                    <asp:ListItem Value="P"> Procedimento</asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorTipo" Font-Size="XX-Small"
                    runat="server" ControlToValidate="rbtTipo" ErrorMessage="*Selecione o Tipo.">
                </asp:RequiredFieldValidator>
            </p>
            <fieldset>
                <legend>Informe o Estabelecimento</legend>
                <p>
                    <uc1:WUC_PesquisarEstabelecimento ID="WUC_PesquisarEstabelecimento1" runat="server" />
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
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                        ControlToValidate="ddlUnidade" ErrorMessage="Selecione a unidade." InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <%--<span class="rotulo">CNES:</span>--%>
                    <%--<span>
                    <asp:TextBox ID="tbxCnes" CssClass="campo" runat="server" MaxLength="7" Width="50px"></asp:TextBox>
                </span><span style="position: absolute; margin-top: 3px; margin-left: 75px;">
                    <asp:LinkButton ID="btnPesquisar" runat="server" CausesValidation="true" OnClick="btnPesquisar_OnClick"
                        ValidationGroup="ValidationPesquisaParametro">
                                <img id="imgPesquisar" src="img/procurar.png" alt="Pesquisar Parametros" />
                    </asp:LinkButton></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                    ControlToValidate="tbxCnes" ErrorMessage="Informe o CNES" ValidationGroup="ValidationPesquisaParametro">
                </asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidatorCNES" Font-Size="XX-Small" runat="server"
                    ControlToValidate="tbxCnes" Display="Dynamic" ErrorMessage="O campo CNES deve conter apenas Números"
                    Operator="DataTypeCheck" Type="Double">
                </asp:CompareValidator>
                <span>
                    <asp:Label ID="lblCnes" runat="server" Font-Bold="true"></asp:Label></span>--%>
                </p>
            </fieldset>
            <asp:Panel ID="PanelPesquisaProcedimento" runat="server" Visible="false">
                <fieldset class="formularioMedio">
                    <legend>Pesquisar Procedimento</legend>
                    <p>
                        <span class="rotulo">Por Código</span><span>
                            <asp:TextBox ID="tbxCodigoProcedimento" MaxLength="10" runat="server" CssClass="campo"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidatorProcedimento" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxCodigoProcedimento" Display="Dynamic" ErrorMessage="O campo Código deve conter apenas Números"
                                Operator="DataTypeCheck" Type="Double">
                            </asp:CompareValidator>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Por Nome</span> <span>
                            <asp:TextBox ID="tbxNomeProcedimento" runat="server" CssClass="campo"></asp:TextBox></span>
                    </p>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="btnPesquisarProcedimento_Click"
                        CausesValidation="false">
                                        <img id="img_pesquisar" alt="" src="img/pesquisar_1.png"
                                        onmouseover="img_pesquisar.src='img/pesquisar_2.png';"
                                        onmouseout="img_pesquisar.src='img/pesquisar_1.png';" />
                    </asp:LinkButton>
                    <p>
                        <asp:Label ID="lblSemPesquisa" runat="server" Text="Selecione um método para Pesquisa"
                            ForeColor="Red" Visible="false"></asp:Label>
                    </p>
                    <p>
                        <asp:GridView ID="GridViewListaProcedimento" runat="server" AllowPaging="true" AlternatingRowStyle-BackColor="LightBlue"
                            AutoGenerateColumns="false" OnPageIndexChanging="GridViewListaProcedimento_OnPageIndexChanged"
                            OnRowCommand="GridViewListaProcedimento_RowCommand" PageSize="10" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Selecionar" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                            CommandName="Select">
                                            <asp:Image ID="imgSelect" runat="server" ImageUrl="~/img/bts/select.png" />
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                                <asp:BoundField DataField="Nome" HeaderText="Nome Procedimento" ItemStyle-Font-Bold="true" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                            </EmptyDataTemplate>
                            <AlternatingRowStyle BackColor="LightBlue" />
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow_left" HorizontalAlign="Center" />
                        </asp:GridView>
                    </p>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="PanelProcedimento" runat="server" Visible="false">
                <p>
                    <span class="rotulo">Procedimento:</span> <span>
                        <asp:DropDownList CssClass="drop" ID="ddlProcedimento" runat="server" Width="445px">
                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span><span>Para modificar o Procedimento,
                        <asp:LinkButton ID="lnkBtnModificarProcedimento" Text="clique aqui!" runat="server"
                            CausesValidation="false" OnClick="lnkBtnModificarProcedimento_Click" />
                    </span>
                </p>
                <p>
                    <span class="rotulo">Especialidade:</span> <span>
                        <asp:DropDownList CssClass="drop" ID="ddlCBO" runat="server" Width="445px" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlCBO_SelectedIndexChanged">
                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Sub-Grupo:</span> <span>
                        <asp:DropDownList CssClass="drop" ID="ddlSubGrupo" runat="server" Width="200px" DataTextField="NomeSubGrupo"
                            DataValueField="Codigo">
                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorProcedimento" runat="server"
                        ControlToValidate="ddlProcedimento" Display="Dynamic" ErrorMessage="Informe o procedimento"
                        SetFocusOnError="True" Font-Size="XX-Small" InitialValue="0">
                    </asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCBO" runat="server" ControlToValidate="ddlCBO"
                        Display="Dynamic" ErrorMessage="Informe o CBO" SetFocusOnError="True" Font-Size="XX-Small"
                        InitialValue="0">
                    </asp:RequiredFieldValidator>
                </p>
            </asp:Panel>
            <p>
                <span class="rotulo">% Local:</span> <span>
                    <asp:TextBox ID="tbxPct_Local" CssClass="campo" runat="server" MaxLength="3" Width="20px"></asp:TextBox></span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="tbxPct_Local"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" SetFocusOnError="True" Font-Size="XX-Small"></asp:RequiredFieldValidator></p>
            <p>
                <span class="rotulo">% Distrital:</span> <span>
                    <asp:TextBox ID="tbxPct_Distrital" CssClass="campo" runat="server" MaxLength="3"
                        Width="20px"></asp:TextBox></span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxPct_Distrital"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" SetFocusOnError="True" Font-Size="XX-Small"></asp:RequiredFieldValidator>
            </p>
            <p>
                <span class="rotulo">% Rede:</span> <span>
                    <asp:TextBox ID="tbxPct_Rede" CssClass="campo" runat="server" MaxLength="3" Width="20px"></asp:TextBox></span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxPct_Rede"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" SetFocusOnError="True" Font-Size="XX-Small"></asp:RequiredFieldValidator>
            </p>
            <p>
                <span class="rotulo">% Reserva Técnica:</span> <span>
                    <asp:TextBox ID="tbxPct_Reserva" CssClass="campo" runat="server" MaxLength="3" Width="20px"></asp:TextBox>
                </span>
            </p>
            <p>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxPct_Reserva"
                    Display="Dynamic" ErrorMessage="Campo Obrigatório" SetFocusOnError="True" Font-Size="XX-Small"></asp:RequiredFieldValidator>
            </p>
            <asp:Panel ID="PanelVisualizarTodasAgendas" runat="server" Visible="false">
                <p>
                    <span class="rotulo">Visualizar todas as Agendas</span> <span>
                        <asp:RadioButtonList ID="rbtnVisualizaOutrasAgendas" runat="server" CssClass="radio"
                            RepeatDirection="Horizontal">
                            <asp:ListItem Text="Sim" Value="S" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Não" Value="N"></asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </p>
            </asp:Panel>
            <div class="botoesroll">
                <asp:LinkButton ID="lknSalvar" runat="server" OnClick="Salvar_Click" CausesValidation="true">
                  <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  onmouseover="imgsalvar.src='img/salvar_2.png';"
                  onmouseout="imgsalvar.src='img/salvar_1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="btnPesquisar" runat="server" CausesValidation="true" ValidationGroup="ValidationPesquisaParametro"
                    OnClick="btnPesquisar_OnClick">
                    <img id="img1" alt="Pesquisar Parametros" src="img/pesquisar_1.png"
                onmouseover="img1.src='img/pesquisar_2.png';"
                onmouseout="img1.src='img/pesquisar_1.png';" />
                    <%--<img id="imgPesquisar" src="img/procurar.png" alt="Pesquisar Parametros" />--%>
                </asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                    CausesValidation="False">
                  <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  onmouseover="imgvoltar.src='img/voltar_2.png';"
                  onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
            </div>
            <br />
            <br />
            <br />
            <br />
            <asp:Panel ID="PanelListaParametros" runat="server" Visible="false">
                <fieldset>
                    <legend>Lista de Parâmetros</legend>
                    <asp:GridView ID="GridViewParametros" runat="server" AutoGenerateColumns="False"
                        Width="100%" OnSorting="GridViewParametros_Sorting" BackColor="White" BorderColor="#477ba5"
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical">
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" HorizontalAlign="Center" />
                        <Columns>
                            <asp:BoundField DataField="CNES" HeaderText="CNES" />
                            <asp:BoundField DataField="Tipo" HeaderText="Tipo" ItemStyle-Width="100px" />
                            <%--<asp:BoundField DataField="Procedimento" HeaderText="Procedimento">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>--%>
                            <asp:BoundField DataField="Percentual" HeaderText="Percentual" ItemStyle-Width="60px">
                            </asp:BoundField>
                            <%-- <asp:BoundField DataField="Pct_Distrital" HeaderText="% Distrital">
                            <ItemStyle Width="60px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Pct_Especifica" HeaderText="% Especifica" />
                        <asp:BoundField DataField="Pct_Rede" HeaderText="% Rede" /--%>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                        </EmptyDataTemplate>
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                            Font-Size="11px" Height="22px" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                    </asp:GridView>
                </fieldset>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
