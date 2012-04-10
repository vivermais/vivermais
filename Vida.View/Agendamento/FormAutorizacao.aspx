<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormAutorizacao.aspx.cs" Inherits="ViverMais.View.Agendamento.FormAutorizacao"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Autorização Ambulatorial</h2>
        <asp:Panel ID="PanelDadosPesquisa" runat="server">
            <fieldset class="formulario" style="width: 770px;">
                <legend>Consulta Solicitações Ambulatoriais</legend>
                <div style="position: relative; top: -12px; left: 50%; margin-left: 150px;">
                    <asp:LinkButton ID="lnkVagas" runat="server" OnClick="OnClick_VagasDisponiveis" CausesValidation="false">
               <img id="imgqdvagas" alt="Quadro de Vagas" src="img/qd-vagas1.png"
                  	onmouseover="imgqdvagas.src='img/qd-vagas2.png';"
                  	onmouseout="imgqdvagas.src='img/qd-vagas1.png';" />
                    </asp:LinkButton>
                </div>
                <p>
                    <span class="rotulo">Cartão Sus:</span> <span>
                        <asp:TextBox ID="tbxCartaoSUS" runat="server" MaxLength="15" CssClass="campo"></asp:TextBox>
                    </span>
                </p>
                <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rbtnEspecialidade" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <p>
                            <span class="rotulo">Tipo de Processamento:</span> <span>
                                <asp:RadioButtonList ID="rbtnEspecialidade" runat="server" RepeatDirection="Horizontal"
                                    TextAlign="Right" AutoPostBack="true" CellPadding="0" CellSpacing="0" CssClass="camporadio"
                                    OnSelectedIndexChanged="rbtnEspecialidade_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="Regulado"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Autorizado"></asp:ListItem>
                                </asp:RadioButtonList>
                            </span>
                        </p>
                        <asp:Panel ID="PanelExibeProcedimento" runat="server">
                            <p>
                                <span class="rotulo">Procedimento:</span> <span>
                                    <asp:DropDownList ID="ddlProcedimento" runat="server" Width="450px" DataTextField="Nome"
                                        DataValueField="Codigo" CssClass="drop">
                                    </asp:DropDownList>
                                </span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator_Procedimento" runat="server"
                                    ControlToValidate="ddlProcedimento" InitialValue="0" Display="Static" ErrorMessage="*"
                                    ForeColor="Red" Font-Bold="true" Font-Size="Large">
                                </asp:RequiredFieldValidator>
                            </p>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <p>
                    <span class="rotulo">Prioridade:</span> <span>
                        <asp:RadioButtonList ID="rbtPrioridade" runat="server" RepeatDirection="Vertical"
                            CssClass="radio" TextAlign="Right" CellPadding="0" CellSpacing="0" RepeatColumns="0">
                            <asp:ListItem Value="0" Selected="True"><img id="vermelho" src="img/leg-verm.png" alt="Prioridade zero" /></asp:ListItem>
                            <asp:ListItem Value="1"><img id="amarelo" src="img/leg-amar.png" alt="Prioridade 1" /></asp:ListItem>
                            <asp:ListItem Value="2"><img id="verde" src="img/leg-verd.png" alt="Prioridade 2" /></asp:ListItem>
                            <asp:ListItem Value="3"><img id="azul" src="img/leg-azul.png" alt="Prioridade 3" /></asp:ListItem>
                            <asp:ListItem Value="4"><img id="branco" src="img/leg-bran.png" alt="Prioridade 4" /></asp:ListItem>
                        </asp:RadioButtonList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Filtrar Por Município:</span> <span>
                        <asp:RadioButtonList ID="rbtMunicipio" runat="server" CellPadding="0" CellSpacing="0"
                            CssClass="camporadio" OnSelectedIndexChanged="rbtMunicipio_SelectedIndexChanged"
                            RepeatDirection="Vertical" AutoPostBack="true" TextAlign="Right">
                           <%-- <asp:ListItem Value="0" Text="Salvador"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Interior"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Muncípio Específico"></asp:ListItem>--%>
                        </asp:RadioButtonList>
                    </span>
                </p>
                <asp:UpdatePanel ID="UpdatePanelMunicipio" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rbtMunicipio" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="PanelExibeMunicipio" runat="server">
                            <span class="rotulo">Selecione o Município:</span> <span>
                                <asp:DropDownList ID="ddlMunicipios" runat="server" CssClass="drop">
                                </asp:DropDownList>
                            </span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorMunicipio" runat="server"
                                ControlToValidate="ddlMunicipios" InitialValue="0" Display="Static" ErrorMessage="Selecione o Município"
                                ForeColor="Red" Font-Bold="true" Font-Size="Large">
                            </asp:RequiredFieldValidator>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="botoesroll">
                    <asp:LinkButton ID="lnkPesquisar" runat="server" OnClick="btnPesquisar_Click" CausesValidation="True">
                  	    <img id="imgpesquisar" alt="Salvar" src="img/pesquisar_1.png"
                  	    onmouseover="imgpesquisar.src='img/pesquisar_2.png';"
                  	    onmouseout="imgpesquisar.src='img/pesquisar_1.png';" />
                    </asp:LinkButton>
                </div>
                <div class="botoesroll">
                    <asp:LinkButton ID="lknVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                        CausesValidation="False">
			<img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                  	onmouseover="imgvoltar.src='img/voltar_2.png';"
                  	onmouseout="imgvoltar.src='img/voltar_1.png';" />	
                    </asp:LinkButton>
                </div>
            </fieldset>
        </asp:Panel>
        <br />
        <asp:Panel ID="PanelExibeSolicitacao" runat="server">
            <asp:UpdatePanel ID="UpdatePanelSolicitacoes" runat="server" ChildrenAsTriggers="true"
                UpdateMode="Conditional">
                <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lknAnterior" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lknProximo" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="ddlPaginas" EventName="SelectedIndexChanged" />
                    
                </Triggers>--%>
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Solicitações</legend>
                        <p>
                            <asp:Label ID="lblQtd" runat="server" Font-Bold="true"></asp:Label>
                            <asp:GridView ID="gridSolicitacao" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                Width="100%" PageSize="10">
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="Prioridade" HeaderText="Prioridade">
                                        <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data_Solicitacao" HeaderText="Data"></asp:BoundField>
                                    <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormSolicitacoesPorPaciente.aspx?Codigo={0}"
                                        DataTextField="CNS" HeaderText="CNS" Text="Data" Target="_blank">
                                        <ItemStyle HorizontalAlign="Center" Width="70px" />
                                    </asp:HyperLinkField>
                                    <asp:BoundField DataField="Nome" HeaderText="Nome"></asp:BoundField>
                                    <asp:BoundField DataField="Idade" HeaderText="Idade">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CID" HeaderText="CID"></asp:BoundField>
                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento"></asp:BoundField>
                                    <asp:BoundField DataField="Municipio" HeaderText="Município" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado!"></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknAnterior" runat="server" CausesValidation="False" OnClick="btnAnterior_Click">
                  	<img id="imganterior" alt="Página Anterior" src="img/btn-anterior1.png"/></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknProximo" runat="server" CausesValidation="False" OnClick="btnProximo_Click">
			                    <img id="img1" alt="Próxima Página" src="img/btn-proximo1.png" />	
                            </asp:LinkButton>
                        </div>
                        <div style="float: right; width: auto; padding: 0px 20px 0 0; text-decoration: none;
                            margin-top: 10px;">
                            <div class="rotulo">Ir para a página</div> 
                            <div class="campo">
                                <asp:DropDownList ID="ddlPaginas" runat="server" CssClass="drop" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlPaginas_OnSelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </fieldset>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
