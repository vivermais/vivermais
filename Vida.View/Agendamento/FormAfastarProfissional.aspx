﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormAfastarProfissional.aspx.cs" Inherits="ViverMais.View.Agendamento.FormAfastarProfissional"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Always">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSalvar" />
            <asp:PostBackTrigger ControlID="imgPesquisar" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Afastamento de Profissional</h2>
                <asp:Panel ID="PanelBuscaProfissional" runat="server">
                    <fieldset class="formulario">
                        <legend>Localize o Profissional</legend>
                        <p>
                            <span><asp:Label ID="lblCategoria" runat="server" Text="Categoria" CssClass="rotulo"></asp:Label></span>
                            <span>
                                <asp:DropDownList ID="ddlCategoria" runat="server" ValidationGroup="Pesquisa" CssClass="drop">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="ddlCategoria"
                                    ErrorMessage="*" ForeColor="Red" Font-Bold="true" InitialValue="0"></asp:RequiredFieldValidator>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nº Conselho:</span> 
                            <span>
                                <asp:TextBox ID="tbxConselho" CssClass="campo" runat="server"
                                    MaxLength="8" Width="90px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="tbxConselho"
                                    runat="server" ErrorMessage="Campo Obrigatório para Pesquisa!" ValidationGroup="Pesquisa"></asp:RequiredFieldValidator>
                                    
                            </span>
                            <asp:CompareValidator ID="CompareValidator5" Font-Size="XX-Small" runat="server"
                                        ControlToValidate="tbxConselho" Display="Dynamic" ErrorMessage="O campo Número Conselho deve conter apenas Números"
                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="Pesquisa"></asp:CompareValidator>
                        </p>
                        <p>
                            <span class="rotulo">CNES:</span> 
                            <span><asp:TextBox ID="tbxCNES" runat="server" CssClass="campo" MaxLength="7"></asp:TextBox>
                            <asp:Label ID="lblNomeEstabelecimento" runat="server"></asp:Label></span>
                            <asp:CompareValidator ID="CompareValidator4" Font-Size="XX-Small" runat="server"
                                        ControlToValidate="tbxCNES" Display="Dynamic" ErrorMessage="O campo CNES deve conter apenas Números"
                                        Operator="DataTypeCheck" Type="Double" ValidationGroup="Pesquisa"></asp:CompareValidator>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="imgPesquisar" ValidationGroup="Pesquisa" runat="server" CausesValidation="true"
                                OnClick="imgPesquisar_Click1">
                                <img id="imgpesquisar" alt="Buscar Profissional" src="img/pesquisar_1.png"
                                onmouseover="imgpesquisar.src='img/pesquisar_2.png';"
                                onmouseout="imgpesquisar.src='img/pesquisar_1.png';" />
                            </asp:LinkButton>
                            <p>
                                <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                                <asp:Panel ID="PanelGridviewListaProfissionais" runat="server">
                                    <fieldset class="formularioMenor">
                                        <legend>Lista de Profissionais</legend>
                                        <asp:GridView ID="GridViewListaProfissionais" runat="server" AllowPaging="true" AlternatingRowStyle-BackColor="LightBlue"
                                            AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="true" OnRowCommand="GridViewListaProfissionais_RowCommand"
                                            PageSize="20" BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" Width="100%">
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                                            <Columns>
                                                <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                                                <asp:BoundField DataField="Nome" HeaderText="Nome Profissional" ItemStyle-Font-Bold="true" />
                                                <asp:BoundField DataField="Categoria" HeaderText="Categoria" ItemStyle-HorizontalAlign="Center"  />
                                                <asp:BoundField DataField="RegistroConselho" HeaderText="Nº Conselho" ItemStyle-HorizontalAlign="Center" />
                                                <asp:TemplateField>
                                                <ItemStyle HorizontalAlign="Center" />
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                                            CommandName="Select">
                                                            <asp:Image ID="imgSelect" runat="server" ImageUrl="~/img/bts/bt_edit.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />     
                                        </asp:GridView>
                                    </fieldset>
                                </asp:Panel>
                        </div>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="PanelExibeDadosProfissional" runat="server">
                    <fieldset class="formularioMenor">
                        <legend>Dados do Profissional</legend>
                        <p>
                            <span class="rotulo">Nome:</span>
                            <asp:Label ID="lblNomeProfissional" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label>
                        </p>
                        <p>
                            <span class="rotulo">Categoria:</span>
                            <asp:Label ID="lblConselho" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label>
                        </p>
                        <p>
                            <span class="rotulo">Nº Conselho:</span>
                            <asp:Label ID="lblNumeroConselho" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label>
                        </p>
                        <p>
                            <span class="rotulo">CNES da Unidade:</span>
                            <asp:Label ID="lblCnes" runat="server" Font-Size="12px" Font-Bold="true"></asp:Label>
                        </p>
                    </fieldset>
                </asp:Panel>
                <asp:Panel ID="PanelVerificaExistenciaSolicitacao" runat="server">
                    <fieldset class="formulario">
                        <legend>Busca Agendas Para o Profissional</legend>
                        <p>
                            <asp:RadioButtonList ID="rblTipoAfastamento" runat="server" RepeatDirection="Vertical"
                                AutoPostBack="True" Width="100%" OnSelectedIndexChanged="rblTipoAfastamento_SelectedIndexChanged" Font-Bold="true" CssClass="camporadio">
                                <asp:ListItem Value="D">Afastamento Determinado</asp:ListItem>
                                <asp:ListItem Value="I">Afastamento Indeterminado</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                ControlToValidate="rblTipoAfastamento" Display="Dynamic" ErrorMessage="* Informe o Tipo de Afastamento" Text="Informe o Tipo de Afastamento"
                                SetFocusOnError="True">
                            </asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <span class="rotulo">Profissional:</span> <span></span>
                            <asp:DropDownList ID="ddlProfissional" runat="server" Width="300px" DataTextField="Nome"
                                DataValueField="Codigo">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlProfissional" Display="Dynamic" ErrorMessage="* Informe o Profissional" Text="Informe o Profissional"
                                InitialValue="0" SetFocusOnError="True" ValidationGroup="PesquisaAfastamento"></asp:RequiredFieldValidator>
                            <asp:LinkButton ID="btnAlteraProfissional" runat="server" Font-Size="X-Small" CausesValidation="false" OnClick="btnAlteraProfissional_Click">
                            <asp:Image ID="imgAltprofi" AlternateText="Alterar Profissional" ImageUrl="~/Agendamento/img/btn-altprofi.png" runat="server" />
                            </asp:LinkButton>
                        </p>
                        <p>
                            <span class="rotulo">Data Inicial:</span> 
                            <span><asp:TextBox ID="tbxData_Inicial" CssClass="campo" runat="server" MaxLength="10"
                                Width="70px" ValidationGroup="VisualizarAgenda" CausesValidation="True"></asp:TextBox></span>
                            <cc1:CalendarExtender runat="server" ID="CalendarExtender4" Format="dd/MM/yyyy" TargetControlID="tbxData_Inicial"
                                Animated="true">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                TargetControlID="tbxData_Inicial" Mask="99/99/9999" MaskType="Date">
                            </cc1:MaskedEditExtender>
                            <asp:CompareValidator ID="CompareValidator2" Type="Date" runat="server" Font-Size="XX-Small" Operator="DataTypeCheck" ErrorMessage="Data Inicial Inválida" ControlToValidate="tbxData_Inicial" Text="Data Inicial Inválida"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server" Text="Informe a Data Inicial"
                                ControlToValidate="tbxData_Inicial" ErrorMessage="* Informe a Data Inicial" ValidationGroup="VisualizarAgenda"></asp:RequiredFieldValidator>

                        </p>
                        <p>
                            <span class="rotulo">Data de Retorno:</span> <span></span>
                            <asp:TextBox ID="tbxData_Final" CssClass="campo" runat="server" MaxLength="10" Width="70px" ValidationGroup="VisualizarAgenda" CausesValidation="True"></asp:TextBox>
                            <cc1:CalendarExtender runat="server" ID="CalendarExtender5" Format="dd/MM/yyyy" TargetControlID="tbxData_Final"
                                Animated="true">
                            </cc1:CalendarExtender>
                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                                TargetControlID="tbxData_Final" Mask="99/99/9999" MaskType="Date">
                            </cc1:MaskedEditExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxData_Final" ErrorMessage="* Informe a Data Final" ValidationGroup="VisualizarAgenda" Text="Informe a Data Final">
                            </asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" Type="Date" runat="server" Font-Size="XX-Small" Operator="DataTypeCheck" ErrorMessage="Data Final Inválida" ControlToValidate="tbxData_Final" Text="Data Final Inválida"></asp:CompareValidator>
                        </p>
                        <p>
                            <span class="rotulo">Turno:</span> <span></span>
                            <asp:DropDownList ID="ddlTurno" runat="server" CssClass="drop">
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Manhã" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Tarde" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Noite" Value="N"></asp:ListItem>
                            </asp:DropDownList>
                        </p>
                        <div class="botoesroll"><asp:LinkButton ID="btnVisualizarAgendas" runat="server" CausesValidation="true" OnClick="btnPesquisarAgendas_Click" ValidationGroup="VisualizarAgenda">
                        <img id="imgvizualizar" alt="Vizualizar Agendas" src="img/btn-vizualizaragendas1.png"
                  onmouseover="imgvizualizar.src='img/btn-vizualizaragendas2.png';"
                  onmouseout="imgvizualizar.src='img/btn-vizualizaragendas1.png';" />
                    </asp:LinkButton></div><br /><br />
                        <asp:UpdatePanel ID="UpdatePanelAgendas" runat="server" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnVisualizarAgendas" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelExibeAgenda" runat="server" Visible="false">
                                    <fieldset style="width: 690px; height: auto; margin-right: 0; padding: 10px 10px 5px 10px;">
                                        <legend>Agendas</legend>
                                            <div class="botoesroll">
                                              <%--<asp:LinkButton ID="btnBloquearTodasAgendas" CausesValidation="false" runat="server" OnClick="btnBloquearTodasAgendas_Click" OnClientClick="javascript : return confirm('Tem certeza que deseja bloquear todas as Agendas?');" >
                                              <img id="imgbloqtodas" alt="Bloquear todas as Agendas" src="img/btn-bloq-todas1.png"
                                              onmouseover="imgbloqtodas.src='img/btn-bloq-todas2.png';"
                                              onmouseout="imgbloqtodas.src='img/btn-bloq-todas1.png';" /></asp:LinkButton>
                                            </div>
                                            <br /><br />--%>
                                        <p>
                                            <span class="legenda">Q.O. - Qtde Ofertada &nbsp;/</span> <span class="legenda">Q.A.
                                                - Qtde Agendada &nbsp;/</span> <span style="background-color: #922929">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                            <span class="legenda">Agenda Bloqueada &nbsp;/</span>
                                        </p>
                                        <asp:GridView ID="GridviewAgendas" runat="server" RowStyle-HorizontalAlign="Center"
                                            Font-Size="X-Small" Font-Names="Verdana" AutoGenerateColumns="false" AllowPaging="true"
                                            PageSize="10" Width="690px" OnRowCommand="GridViewAgendas_RowCommand" OnPageIndexChanging="GridViewAgendas_PageIndexChanging">
                                            <Columns>
                                                <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                                    <ItemStyle CssClass="colunaEscondida" />
                                                    <HeaderStyle CssClass="colunaEscondida" />
                                                    <FooterStyle CssClass="colunaEscondida" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                                <asp:BoundField DataField="Cbo" HeaderText="Especialidade" />
                                                <asp:BoundField DataField="NomeProfissional" HeaderText="Profissional" />
                                                <asp:BoundField DataField="Data" HeaderText="Data" />
                                                <asp:BoundField DataField="Turno" HeaderText="Turno" />
                                                <asp:BoundField DataField="Horario" HeaderText="Horário" />
                                                <asp:BoundField DataField="Quantidade" HeaderText="Q.O" />
                                                <asp:BoundField DataField="QuantidadeAgendada" HeaderText="Q.A" />
                                                <asp:BoundField DataField="Bloqueada" Visible="false">
                                                    <ItemStyle CssClass="colunaEscondida" />
                                                    <HeaderStyle CssClass="colunaEscondida" />
                                                    <FooterStyle CssClass="colunaEscondida" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdSelect" CommandArgument='<%#Eval("Codigo")%>' CommandName="Select"
                                                            runat="server" CausesValidation="false">
                                                            <asp:Image ID="imgSelect" AlternateText="Selecionar" ImageUrl="~/img/bts/bt_edit.png"
                                                                runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnBloqueioAgenda" CausesValidation="false" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                            CommandName="Bloqueio" OnClientClick="javascript: return confirm('Confirma o Bloqueio Desta Agenda?');">
                                                        <asp:Image ID="imgBloq" AlternateText="Bloquear" ImageUrl="~/img/bts/bt_block.png"
                                                                runat="server" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhuma Agenda Encontrada!" Font-Size="X-Small" ForeColor="Red"></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle Height="22px" ForeColor="White" BackColor="#325e8b" />
                                            <RowStyle Height="21px" VerticalAlign="Bottom" BackColor="#77a4d1" ForeColor="#ffffff" />
                                            <PagerStyle Height="22px" ForeColor="White" BackColor="#477ba5" HorizontalAlign="Center"
                                                Font-Bold="true" />
                                        </asp:GridView>
                                        <br />
                                    </fieldset>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        <p>
                            <span class="rotulo">Motivo:</span> <span>
                                <asp:TextBox ID="tbxMotivo" CssClass="campo" runat="server" MaxLength="150"
                                    Width="300px" Rows="3" Height="50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="tbxMotivo" Display="Dynamic" ErrorMessage="* Informe o Motivo" Text="Informe o Motivo"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Observações:</span> <span>
                                <asp:TextBox ID="tbxObs" CssClass="campo" runat="server" AutoPostBack="True" MaxLength="150"
                                    Width="300px" Rows="1" Height="50px"></asp:TextBox></span>
                        </p>
                </asp:Panel>
                <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server"
                            ShowMessageBox="true" ShowSummary="false" />
                <asp:Panel ID="PanelDadosDaAgenda" runat="server" Visible="false">
                    <div id="cinza" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;
                       z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                       moz-opacity: 0.3; opacity: 0.3" visible="false";>
                   </div>
                   <div id="mensagem" style="position: absolute; top: 150px; left: 50%; margin-left:-350px; width: 700px;
                       z-index: 102; background-color: #0d2639; border: #ffffff 5px solid; padding-right: 20px;
                        padding-left: 20px; padding-bottom: 20px; color: #c5d4df; padding-top: 10px;  text-align: justify;
                       font-family: Verdana;" visible="false">
                       <p style="height: 10px;">
                           <span style="float:right">
                               <asp:LinkButton ID="btnFechar" runat="server" CausesValidation="false" OnClick="btnFechar_Click">
                                   <img id="Img1" src="~/Agendamento/img/fechar-agendamento.png" alt="Fechar" runat="server" />
                               </asp:LinkButton>
                            </span>
                        </p>
                        
                        <fieldset class="formularioMedio">
                            <legend>Agenda Selecionada</legend>
                            <p>
                            <span class="rotulo">Estabelecimento</span>
                            <span><asp:Label ID="lblNomeEstabelecimentoAgenda" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p>
                            <p>
                                <asp:TextBox ID="tbxCodigoAgenda" runat="server" Visible="false" CssClass="campo"></asp:TextBox>
                                <span class="rotulo">Procedimento</span>
                                <span><asp:Label ID="lblProcedimentoAgenda" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p>
                            <p>
                                <span class="rotulo">Profissional</span>
                                <span><asp:DropDownList ID="ddlProfissionalAgenda" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlProfissionalAgenda" Display="Dynamic" ErrorMessage="* Informe o Profissional" Text="Informe o Profissional"
                                InitialValue="-1" ValidationGroup="ValidationAgenda">
                                </asp:RequiredFieldValidator>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data</span> 
                                <span><asp:TextBox ID="tbxDataAgenda" runat="server" CssClass="campo"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Animated="true" Format="dd/MM/yyyy"
                                    TargetControlID="tbxDataAgenda">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" InputDirection="LeftToRight"
                                    TargetControlID="tbxDataAgenda" Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                                <asp:CompareValidator ID="CompareValidator1" Type="Date" runat="server" Font-Size="XX-Small" ValidationGroup="ValidationAgenda" Operator="DataTypeCheck" ErrorMessage="Data da Agenda Inválida" ControlToValidate="tbxDataAgenda"></asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Font-Size="XX-Small" runat="server" ValidationGroup="ValidationAgenda"
                                    ControlToValidate="tbxDataAgenda" Display="Dynamic" ErrorMessage="* Informe a Data da Agenda" Text="Informe a Data " SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                                </span>
                                
                                <%--<asp:Label ID="lblTurnoAgenda" runat="server" Font-Size="X-Small"></asp:Label>--%>
                            </p>
                            <p>
                            <span class="rotulo">Turno</span>
                            <span>
                            <asp:DropDownList ID="ddlTurnoAgenda" runat="server" >
                                <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Manhã" Value="M"></asp:ListItem>
                                <asp:ListItem Text="Tarde" Value="T"></asp:ListItem>
                                <asp:ListItem Text="Noite" Value="N"></asp:ListItem>
                            </asp:DropDownList></span>
                            </p>
                            <p>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlTurnoAgenda" Display="Dynamic" ErrorMessage="* Informe o Turno"
                                InitialValue="0" SetFocusOnError="True" ValidationGroup="ValidationAgenda"></asp:RequiredFieldValidator>
                            </p>
                            <p>
                                <span class="rotulo">Qtd. Vagas</span>
                                <span><asp:Label ID="lblQtdAgenda" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p>
                            <p>
                            <span class="rotulo">Qtd. Agendada</span>
                            <span><asp:Label ID="lblQtdAgendadaAgenda" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p><br />
                            <p>
                                <div class="botoesroll">
                                    <asp:LinkButton ID="btnSalvaAgenda" runat="server" OnClick="btnSalvaAgenda_Click" CausesValidation="true" ValidationGroup="ValidationAgenda">
                                                <img id="img2" alt="Salvar" onmouseout="img2.src='img/salvar_1.png';" 
                                                    onmouseover="img2.src='img/salvar_2.png';" src="img/salvar_1.png" />
                                    </asp:LinkButton>
                                </div>
                            </p>
                            <p>
                                &nbsp;
                            </p>
                            <div style="width: 750px; height: 300px; overflow: auto;">
                            <fieldset class="formularioMedio">
                                <legend>Solicitações para Agenda</legend>
				                <div class="botoesroll">
                                    <asp:LinkButton ID="btnImprimeSolicitacoes" runat="server" Font-Size="X-Small" OnClick="btnImprimeSolicitacoes_Click" CausesValidation="false">
                                        <img id="imgprinttodas" alt="Voltar" src="img/btn-printlista1.png"
                  		                    onmouseover="imgprinttodas.src='img/btn-printlista2.png';"
                  		                    onmouseout="imgprinttodas.src='img/btn-printlista1.png';" />
				                    </asp:LinkButton>
				                </div>
                                <p>
                                    <asp:Label ID="lblSemSolicitacoes" runat="server" Visible="false" Text="Não Existem Solicitações para esta Agenda"></asp:Label>
                                    <asp:GridView ID="GridviewSolicitacoesAgenda" runat="server" AllowPaging="true" Font-Names="Verdana" Font-Size="11px" 
                                        AutoGenerateColumns="false" EnableSortingAndPagingCallbacks="true" OnRowCommand="GridViewSolicitacoesAgenda_RowCommand"
                                        PageSize="20" RowStyle-HorizontalAlign="Center" Width="650px">
                                        <Columns>
                                            <asp:BoundField DataField="Codigo" HeaderText="Codigo" Visible="false" />
                                            <asp:BoundField DataField="Paciente" HeaderText="Paciente" />
                                            <asp:BoundField DataField="CartaoSUS" HeaderText="CNS" />
                                            <asp:BoundField DataField="DataNascimento" HeaderText="Dt. Nascimento" />
                                            <asp:BoundField DataField="DataSolicitacao" HeaderText="DataS Solicitação" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                                        CommandName="Select" OnClientClick="javascript : return confirm('Tem Certeza Que Deseja Desmarcar Esta Solicitação?');">
                                                        <asp:Image ID="imgSelect" runat="server" ImageUrl="~/img/bts/bt_edit.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                            <HeaderStyle Height="22px" ForeColor="White" BackColor="#477ba5" />
                                            <RowStyle Height="21px" VerticalAlign="Bottom" />
                                            <PagerStyle Height="22px" ForeColor="White" BackColor="#477ba5" HorizontalAlign="Center"
                                                Font-Bold="true" />
                                    </asp:GridView>
                                </p>
                            </fieldset></div>
                        </fieldset>
                    </div>
                </asp:Panel>
                </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click1" CausesValidation="true">
                <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                onmouseover="imgsalvar.src='img/salvar_2.png';"
                onmouseout="imgsalvar.src='img/salvar_1.png';" />
                    </asp:LinkButton></div>
                <div class="botoesroll">
                    <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                        CausesValidation="false">
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
                    </asp:LinkButton></div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
