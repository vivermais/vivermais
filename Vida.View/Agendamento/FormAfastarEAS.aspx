<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormAfastarEAS.aspx.cs" Inherits="ViverMais.View.Agendamento.FormAfastarEAS"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Afastar Estabelecimento de Saúde</h2>
        <fieldset class="formulario">
            <legend>Formulário</legend>
            <asp:UpdatePanel ID="UpdatePanelPequisaEstabalecimento" runat="server" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisarEstabelecimento" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="PanelEstabelecimentoSaude" runat="server">
                        <fieldset class="formularioMenor">
                            <legend>Pesquisar Estabelecimento de Saúde</legend>
                            <p>
                                <span class="rotulo">CNES</span> <span>
                                    <asp:TextBox ID="tbxCNES" runat="server" CssClass="campo"></asp:TextBox>
                                </span><span>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Digite somente números no CNES"
                                        Font-Size="X-Small" ControlToValidate="tbxCNES" Type="Integer" Display="Dynamic"
                                        Operator="DataTypeCheck"></asp:CompareValidator>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Descrição</span> <span>
                                    <asp:TextBox ID="tbxDescricao" runat="server" CssClass="campo" Width="302px"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <asp:Label ID="lblResultado" runat="server" Text="" Font-Names="Verdana" Font-Size="X-Small"
                                    ForeColor="Red"></asp:Label>
                            </p>
                            <p>
                                <div class="botoesroll">
                                    <span style="margin-left: 350px;">
                                        <asp:LinkButton ID="btnPesquisarEstabelecimento" runat="server" CausesValidation="false"
                                            OnClick="btnPesquisarEstabelecimento_Click">
                                                <img id="img_pesquisar1" alt="" src="img/pesquisar_1.png"
                                                onmouseover="img_pesquisar1.src='img/pesquisar_2.png';"
                                                onmouseout="img_pesquisar1.src='img/pesquisar_1.png';" />                           
                                        </asp:LinkButton>
                                    </span>
                                </div>
                                <p>
                                </p>
                            </p>
                        </fieldset>
                        <p>
                            &nbsp;</p>
                    </asp:Panel>
                    <p>
                        <span class="rotulo">Estabelecimento de Saúde</span> <span>
                            <asp:DropDownList ID="ddlEstabelecimentoSaude" runat="server" CssClass="drop" Width="300px">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o estabelecimento de saúde"
                                InitialValue="0" Display="Dynamic" Font-Size="X-Small" SetFocusOnError="true"
                                ControlToValidate="ddlEstabelecimentoSaude">
                            </asp:RequiredFieldValidator>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<p>
                <span class="rotulo">CNES:</span> <span>
                    <asp:TextBox ID="tbxCnes" CssClass="campo" runat="server" AutoPostBack="True" MaxLength="7"
                        Width="50px" OnTextChanged="tbxCnes_TextChanged"></asp:TextBox>
                    <asp:Label ID="lblCnes" runat="server"></asp:Label></span>
            </p>--%>
            <p>
                <asp:RadioButtonList ID="rblTipoAfastamento" runat="server" RepeatDirection="Vertical" TextAlign="Right"
                    CssClass="radio1" AutoPostBack="True" OnSelectedIndexChanged="rblTipoAfastamento_SelectedIndexChanged">
                    <asp:ListItem Value="S">Afastamento Determinado</asp:ListItem>
                    <asp:ListItem Value="N">Afastamento Indeterminado</asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <p>
                <span class="rotulo">Data Inicial:</span> <span>
                    <asp:TextBox ID="tbxData_Inicial" CssClass="campo" runat="server" MaxLength="10"
                        Width="70px"></asp:TextBox><cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxData_Inicial"
                        Animated="true"></cc1:CalendarExtender></span></p>
                    
                    <p>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Font-Size="XX-Small" runat="server"
                        ControlToValidate="tbxData_Inicial" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                        SetFocusOnError="True" ValidationGroup="PesquisaAgenda">
                    </asp:RequiredFieldValidator>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                        TargetControlID="tbxData_Inicial" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender></p>
            
            <p>
                <span class="rotulo">Data de Reativação:</span> <span>
                    <asp:TextBox ID="tbxData_Reativacao" CssClass="campo" runat="server" MaxLength="10"
                        Width="70px" Enabled="False"></asp:TextBox>
                    <cc1:CalendarExtender runat="server" ID="CalendarExtender3" Format="dd/MM/yyyy" TargetControlID="tbxData_Reativacao"
                        Animated="true">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                        TargetControlID="tbxData_Reativacao" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                </span>
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
            <div class="botoesroll"><asp:LinkButton ID="btnVisualizarAgendas" runat="server"
                        CausesValidation="True" OnClick="btnVisualizarAgendas_Click" ValidationGroup="PesquisaAgenda">
                        <img id="imgvizualizar" alt="Vizualizar Agendas" src="img/btn-vizualizaragendas1.png"
                  onmouseover="imgvizualizar.src='img/btn-vizualizaragendas2.png';"
                  onmouseout="imgvizualizar.src='img/btn-vizualizaragendas1.png';" />
                    </asp:LinkButton></div>
                    <br />
                    <br />
            <asp:UpdatePanel ID="UpdatePanelAgendas" runat="server" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnVisualizarAgendas" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Panel ID="PanelAgendas" runat="server" Visible="false">
                        <fieldset class="formularioMedio">
                            <legend>Agendas</legend>
 
				<div class="botoesroll">
                                    <asp:LinkButton ID="btnBloquearTodasAgendas" runat="server"
                                        CausesValidation="false" Font-Size="X-Small" OnClientClick="javascript : return confirm('Tem certeza que deseja bloquear todas as Agendas?');"
                                        OnClick="btnBloquearTodasAgendas_Click">
					<img id="imgbloqueartodas" alt="Imprimir" src="img/btn-bloq-todas1.png"
                  			onmouseover="imgbloqueartodas.src='img/btn-bloq-todas2.png';"
                  			onmouseout="imgbloqueartodas.src='img/btn-bloq-todas1.png';" />
                  			</asp:LinkButton>
				</div><br />
                            <p>
                                <span class="legenda">Q.O. - Quantidade Ofertada &nbsp;/</span> <span class="legenda">
                                    Q.A. - Quantidade Agendada &nbsp;/</span> <span style="background-color: #696969;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                <span class="legenda">Agenda Bloqueada</span>
                            </p>
                            <asp:Label ID="lblSemRegistro" runat="server" Text="Nenhuma Agenda Encontrada!" Visible="false" ForeColor="Red"></asp:Label>
                            <asp:GridView ID="GridViewAgendas" runat="server" Width="675px" Font-Size="X-Small"
                                Font-Names="Verdana" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                                OnPageIndexChanging="GridViewAgendas_PageIndexChanging" OnRowCommand="GridViewAgendas_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Codigo">
                                        <ItemStyle CssClass="colunaEscondida" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <FooterStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento">
                                        <ItemStyle Width="475px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Data" HeaderText="Data">
                                        <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QtdVagas" HeaderText="Q.O.">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="QtdAgendada" HeaderText="Q.A.">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Bloqueada">
                                        <ItemStyle CssClass="colunaEscondida" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <FooterStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Visualizar Agenda">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnSelecionarAgenda" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                runat="server" OnClick="SelecionarAgenda" CausesValidation="false">
                                                <asp:Image ID="imgSelect" ImageUrl="~/img/bts/bt_edit.png" runat="server" AlternateText="Visualizar Agenda" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Bloquear Agenda">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnBloquearAgenda" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                runat="server" CausesValidation="false" Font-Size="X-Small"
                                                OnClientClick="javascript : return confirm('Tem certeza que deseja bloquear esta Agenda?');"
                                                CommandName="BloquearAgenda">
                                                <asp:Image ID="imgBlock" ImageUrl="~/Agendamento/img/bloquear.png" runat="server" AlternateText="Bloquear Agenda" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="40px" HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                <HeaderStyle Height="22px" ForeColor="White" BackColor="#477ba5" />
                                <RowStyle Height="21px" VerticalAlign="Bottom" />
                                <PagerStyle Height="22px" ForeColor="White" BackColor="#477ba5" HorizontalAlign="Center"
                                    Font-Bold="true" />
                            </asp:GridView>
                        </fieldset></asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
            </p>
            <p>
                <span class="rotulo">Motivo</span> <span>
                    <asp:TextBox ID="tbxMotivo" CssClass="campo" runat="server" MaxLength="150" Width="300px"
                        Rows="1" Height="50px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                        ControlToValidate="tbxMotivo" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                        SetFocusOnError="True"></asp:RequiredFieldValidator></span>
            </p>
            <p>
                <span class="rotulo">Observações:</span> <span>
                    <asp:TextBox ID="tbxObs" CssClass="campo" runat="server" MaxLength="150"
                        Width="300px" Height="50px" Rows="1"></asp:TextBox></span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="btnSalvar" runat="server" OnClick="Salvar_Click">
                    <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                    onmouseover="imgsalvar.src='img/salvar_2.png';"
                    onmouseout="imgsalvar.src='img/salvar_1.png';" />
                </asp:LinkButton>
            </div>
            <%--<div class="botoesroll">
                <asp:LinkButton ID="btnpesquisar" runat="server" OnClick="btnPesquisar_Click" CausesValidation="False">
                    <img id="img_pesquisar" alt="" src="img/pesquisar_1.png"
                    onmouseover="img_pesquisar.src='img/pesquisar_2.png';"
                    onmouseout="img_pesquisar.src='img/pesquisar_1.png';" />
                </asp:LinkButton>
            </div>--%>
            <div class="botoesroll">
                <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                    CausesValidation="false">
                        <img id="img_voltar" alt="" src="img/voltar_1.png"
                        onmouseover="img_voltar.src='img/voltar_2.png';"
                        onmouseout="img_voltar.src='img/voltar_1.png';" />
                </asp:LinkButton></div>
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanelVisualizaAgenda" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div id="cinza" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 150%;
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
                        <fieldset class="formulario">
                            <legend>Agenda Selecionada</legend>
                            <p>
                                <span class="rotulo">Procedimento</span> <span>
                                    <asp:Label ID="lbProcedimento" runat="server" CssClass="label"></asp:Label></span>
                            </p>
                            <p>
                                <span class="rotulo">Data</span> <span>
                                    <asp:Label ID="lbData" runat="server" CssClass="label"></asp:Label></span>
                            </p>
                            <p>
                                <span class="rotulo">Profisional</span> <span>
                                    <asp:Label ID="lbProfissional" runat="server" CssClass="label"></asp:Label></span>
                            </p>
                            <p>
                                <span class="rotulo">Turno</span> <span>
                                    <asp:Label ID="lbTurno" runat="server" CssClass="label"></asp:Label></span>
                            </p>
                            <fieldset class="formularioMedio">
                                <legend>Solicitações</legend>
                                <div class="botoesroll">
                                    <asp:LinkButton ID="btnDesmarcarTodasSolicitacoes" runat="server" Font-Size="X-Small"
                                        CausesValidation="false" OnClientClick="javascript : return confirm('Tem certeza que deseja desmarcar TODAS as solicitações?');"
                                        OnClick="btnDesmarcarTodasSolicitacoes_Click">
				                <img id="imgdesmarcar" alt="Desmarcar" src="img/desmarcar_1.png"
                  			onmouseover="imgdesmarcar.src='img/desmarcar_2.png';"
                  			onmouseout="imgdesmarcar.src='img/desmarcar_1.png';" />
                                    </asp:LinkButton>
                                </div>
                                <div class="botoesroll">
                                    <asp:LinkButton ID="btnImprimirSolicitacoes" runat="server" Font-Size="X-Small" CausesValidation="false"
                                        OnClientClick="" OnClick="btnImprimirSolicitacoes_Click">
					<img id="imgimprimir" alt="Imprimir" src="img/imprimir_1.png"
                  			onmouseover="imgimprimir.src='img/imprimir_2.png';"
                  			onmouseout="imgimprimir.src='img/imprimir_1.png';" />
                                    </asp:LinkButton>
                                </div>
                                <br />
                                <p>
                                    <asp:Label ID="lbMensagemSolicitacoes" runat="server" Text="Não há solicitações para esta agenda"
                                        Visible="false" Font-Size="X-Small" ForeColor="#771e00"></asp:Label>
                                </p>
                                <br />
                                <asp:GridView ID="GridViewSolicitacoes" runat="server" Width="670px" Font-Size="X-Small"
                                    Font-Names="Verdana" AutoGenerateColumns="False" AllowPaging="True" PageSize="10"
                                    AlternatingRowStyle-BackColor="LightBlue" OnRowCommand="GridViewSolicitacoes_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo">
                                            <ItemStyle CssClass="colunaEscondida" />
                                            <HeaderStyle CssClass="colunaEscondida" />
                                            <FooterStyle CssClass="colunaEscondida" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CartaoSus" HeaderText="Cartão SUS">
                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Paciente" HeaderText="Paciente">
                                            <ItemStyle Width="400px" HorizontalAlign="Left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DataNascimento" HeaderText="Nascimento">
                                            <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="50px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDesmarcarSolicitacao" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" Width="50px" CausesValidation="false" Text="Desmarcar" Font-Size="X-Small"
                                                    OnClientClick="javascript : return confirm('Tem certeza que deseja desmarcar esta solicitação?');"
                                                    CommandName="Desmarcar">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle Height="22px" ForeColor="White" BackColor="#477ba5" />
                                    <RowStyle Height="21px" VerticalAlign="Bottom" />
                                    <PagerStyle Height="22px" ForeColor="White" BackColor="#477ba5" HorizontalAlign="Center"
                                        Font-Bold="true" />
                                </asp:GridView>
                                <p>
                                </p>
                            </fieldset>
                        </fieldset>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
