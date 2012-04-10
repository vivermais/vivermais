<%@ Page Language="C#" MasterPageFile="~/Profissional/MasterProfissional.Master"
    AutoEventWireup="True" CodeBehind="FormVinculoProfissionalEstabelecimento.aspx.cs"
    Inherits="ViverMais.View.Profissional.FormVinculoProfissionalEstabelecimento" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="Up1" runat="server">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="btnBuscarProfissional" />
            <asp:PostBackTrigger ControlID="btnAddEstabelecimento" />
            <asp:PostBackTrigger ControlID="GridviewEstabelecimentos"/>--%>
            <asp:PostBackTrigger ControlID="Wizard1" />
            <%--<asp:AsyncPostBackTrigger ControlID="Wizard1" />--%>
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Vínculo Profissional de Saúde</h2>
                <fieldset style="width: auto; height: auto; margin-left: 0; margin-right: 0; padding: 10px 10px 20px 10px;">
                    <legend>Vínculo</legend>
                    <asp:Panel runat="server">
                        <asp:Wizard ID="Wizard1" DisplaySideBar="false" runat="server" ActiveStepIndex="1"
                            BackColor="#ffffff" BorderColor="#ffffff" BorderWidth="1px" Font-Names="Verdana"
                            Font-Size="0.8em" Width="740px" CancelButtonText="Cancelar" FinishCompleteButtonText="Salvar"
                            OnFinishButtonClick="Wizard1_FinishButtonClick" StartNextButtonStyle-CssClass="colunaEscondida"
                            StepNextButtonText="Próximo" StepPreviousButtonText="Anterior" OnPreviousButtonClick="Wizard1_PreviousButtonClick">
                            <StepStyle BorderStyle="Solid" BorderWidth="2px" Font-Size="0.8em" ForeColor="#333333"
                                VerticalAlign="Bottom" Width="100%" />
                            <StartNextButtonStyle CssClass="colunaEscondida" />
                            <WizardSteps>
                                <asp:WizardStep ID="WizardStep1" runat="server" Title="Busca de Profissional" StepType="Auto">
                                    <fieldset style="width: 500px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
                                        <legend>Pesquisa de Profissional</legend>
                                        <p>
                                            <span class="rotulo">
                                                <asp:Label ID="lblCPFPesquisa" runat="server" Text="CPF:"></asp:Label>
                                            </span><span>
                                                <asp:TextBox ID="tbxCPFPesquisa" runat="server" CssClass="campo" ValidationGroup="Pesquisa"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender8" runat="server" TargetControlID="tbxCPFPesquisa"
                                                    Mask="999,999,999-99" MaskType="None" InputDirection="LeftToRight" ClearMaskOnLostFocus="false">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorPesquisa" runat="server" ControlToValidate="tbxCPFPesquisa" 
                                                    ValidationGroup="Pesquisa" ErrorMessage="Campo Obrigatório" InitialValue="___.___.___-__">
                                                </asp:RequiredFieldValidator>
                                            </span><span></span>
                                        </p>
                                        <p>
                                            <span>
                                                <asp:LinkButton ID="btnBuscarProfissional" runat="server" OnClick="btnBuscarProfissional_Click"
                                                    CausesValidation="true" ValidationGroup="Pesquisa">
                        <img id="imgpesquisar" alt="Pesquisar" src="img/pesquisar_1.png"
                            onmouseover="imgpesquisar.src='img/pesquisar_2.png';"
                            onmouseout="imgpesquisar.src='img/pesquisar_1.png';" />
                                                </asp:LinkButton></span> </span>
                                        </p>
                                    </fieldset>
                                    <asp:Panel ID="panelConfirmacao" runat="server" Visible="false">
                                        <div id="cinza" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                                            height: 200%; z-index: 100; min-height: 100%; background-color: #999; filter: alpha(opacity=45);
                                            moz-opacity: 0.3; opacity: 0.3">
                                        </div>
                                        <div id="mensagem" visible="false" style="position: fixed; top: 250px; left: 30%;
                                            width: 300px; z-index: 102; background-color: #FFFFFF; border-right: #336699 2px solid;
                                            padding-right: 10px; border-top: #336699 2px solid; padding-left: 10px; padding-bottom: 10px;
                                            border-left: #336699 2px solid; color: #000000; padding-top: 10px; border-bottom: #336699 2px solid;
                                            text-align: justify; font-family: Verdana;">
                                            <asp:Label ID="lblConfirmacao" runat="server"></asp:Label>
                                            <p>
                                                &nbsp;</p>
                                            <span style="margin-left: 90px;">
                                                <asp:Button ID="btnSim" runat="server" Text="SIM" Width="50px" OnClick="btnSim_Click" />
                                            </span><span style="margin-left: 15px;">
                                                <asp:Button ID="btnNao" runat="server" Text="NÃO" Width="50px" OnClick="btnNao_Click" />
                                            </span>
                                        </div>
                                    </asp:Panel>
                                    <p>
                                        <asp:Label ID="lblSemRegistro" runat="server" Font-Bold="true" ForeColor="Red" Text="Nenhum Registro Encontrado"></asp:Label>
                                        <asp:Panel ID="PanelGridviewListaProfissionais" runat="server">
                                            <fieldset class="formulario">
                                                <legend>Lista de Profissionais</legend>
                                                <p>
                                                <asp:GridView ID="GridViewListaProfissionais" runat="server" AutoGenerateColumns="false"
                                                    EnableSortingAndPagingCallbacks="true" AllowPaging="true" PageSize="20" AlternatingRowStyle-BackColor="LightBlue"
                                                    OnRowCommand="GridViewListaProfissionais_RowCommand" BackColor="White" BorderColor="#cfc8d4"
                                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" Width="100%">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Selecionar" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="cmdSelect" CommandArgument='<%#Eval("Codigo")%>' CommandName="Select"
                                                                    runat="server" CausesValidation="false">
                                                                    <asp:Image ID="imgSelect" ImageUrl="~/Profissional/img/select.png" runat="server" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Codigo" HeaderText="CPF" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" ControlStyle-CssClass="colunaEscondida" />
                                                        <asp:BoundField DataField="Nome" HeaderText="Nome Profissional" ItemStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="Categoria" HeaderText="Categoria" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="RegistroConselho" HeaderText="Nº Conselho" ItemStyle-HorizontalAlign="Center" />
                                                    </Columns>
                                                    <PagerStyle BackColor="#efe9f4" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#efe9f4" Font-Bold="True" ForeColor="black" />
                                                    <HeaderStyle BackColor="#714494" Font-Bold="True" ForeColor="White" Font-Names="Arial"
                                                        Font-Size="11px" Height="22px" />
                                                    <AlternatingRowStyle BackColor="#efe9f4" />
                                                </asp:GridView>
                                            </p>
                                            </fieldset>
                                        </asp:Panel>
                                </asp:WizardStep>
                                <asp:WizardStep ID="WizardStep2" runat="server" Title="Dados de Identificação">
                                    <fieldset class="formulario">
                                        <legend>Dados de Identificação</legend>
                                        <p>
                                        <span><asp:Label ID="lblCadastramento" runat="server" Text="Cadastramento" CssClass="rotulo"></asp:Label></span>
                                        <span><asp:RadioButtonList ID="rbtCadastramento" runat="server" RepeatDirection="Horizontal"
                                                TextAlign="Right" CellPadding="0" CellSpacing="0" CssClass="camporadio" RepeatColumns="2" 
                                               AutoPostBack="true" OnSelectedIndexChanged="rbtCadastramento_SelectedIndexChanged">
                                                <asp:ListItem Text="SUS" Value="S"></asp:ListItem>
                                                <asp:ListItem Text="Não SUS" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" ControlToValidate="rbtCadastramento"
                                                ErrorMessage="Informe o Tipo de Cadastro" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator></span>
                                            
                                        </p>
                                        <p>
                                            <span class="rotulo">
                                                <asp:Label ID="lblCartaoSUS" runat="server" Text="Cartão SUS"></asp:Label>
                                            </span><span >
                                                <asp:TextBox ID="tbxCartaoSUS" runat="server" CssClass="campo" MaxLength="15" ValidationGroup="ValidationGroup_Busca_Paciente_ViverMais"></asp:TextBox>
                                                <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                                                    ControlToValidate="tbxCartaoSUS" Display="Dynamic" ErrorMessage="O campo Cartão SUS deve conter apenas Números"
                                                    Operator="DataTypeCheck" Type="Double">
                                                </asp:CompareValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ControlToValidate="tbxCartaoSUS"
                                                    ErrorMessage="O Cartão SUS é Obrigatório" ForeColor="Red" Font-Bold="true" ValidationGroup="ValidationGroup_Busca_Paciente_ViverMais">
                                                </asp:RequiredFieldValidator>
                                            </span>
                                            <asp:LinkButton ID="btnBuscarPacienteViverMais" runat="server" Text="Buscar Paciente ViverMais"
                                                OnClick="btnBuscarPacienteViverMais_OnClick" ValidationGroup="ValidationGroup_Busca_Paciente_ViverMais"
                                                CausesValidation="true">
                                            </asp:LinkButton>
                                        </p>
                                        <p>
                                            <span class="rotulo">
                                                <asp:Label ID="lblNomeProfissional" runat="server" Text="Nome do Profissional"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxNomeProfissional" MaxLength="60" runat="server" CssClass="campo"
                                                    Width="250px"></asp:TextBox></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxNomeProfissional"
                                                ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                            <span>
                                                <asp:Label ID="lblCPF" runat="server" Text="CPF" CssClass="rotulo"></asp:Label></span>
                                                <span >
                                                    <asp:TextBox ID="tbxCPF" runat="server" CssClass="campo"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                                                        ControlToValidate="tbxCPF" InitialValue="___.___.___-__" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="999,999,999-99"
                                                        MaskType="Number" TargetControlID="tbxCPF" ClearMaskOnLostFocus="false" InputDirection="LeftToRight">
                                                    </cc1:MaskedEditExtender>
                                                </span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblSexo" runat="server" Text="Sexo" CssClass="rotulo"></asp:Label></span>
                                            
                                            <span>
                                                <asp:RadioButtonList ID="rbtSexo" runat="server" TextAlign="Right" CellPadding="0" RepeatColumns="2"
                                                    CellSpacing="0" CssClass="camporadio" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="Feminino" Value="F"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="rbtSexo"
                                                Font-Bold="true" ForeColor="Red" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span> <asp:Label ID="lblPisPASEP" runat="server" CssClass="rotulo" Text="PIS/PASEP"></asp:Label></span>
                                           <span>
                                                <asp:TextBox ID="tbxPisPASEP" runat="server" MaxLength="11" CssClass="campo"></asp:TextBox></span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblNomeMae" runat="server" CssClass="rotulo" Text="Nome da Mãe"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxNomeMae" runat="server" MaxLength="60" CssClass="campo" Width="250px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                                                    ControlToValidate="tbxNomeMae" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblNomePai" runat="server" CssClass="rotulo" Text="Nome do Pai"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxNomePai" runat="server" MaxLength="60" CssClass="campo" Width="250px"></asp:TextBox>
                                            </span>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                               ControlToValidate="tbxNomePai" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblDataNascimento" runat="server" CssClass="rotulo" Text="Data de Nascimento"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxDataNascimento" runat="server" CssClass="campo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                                                    ControlToValidate="tbxDataNascimento" Font-Bold="true" InitialValue="__/__/____"
                                                    ForeColor="Red"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender MaskType="Date" ID="MaskedEditExtender2" runat="server" TargetControlID="tbxDataNascimento"
                                                    Mask="99/99/9999" ClearMaskOnLostFocus="false">
                                                </cc1:MaskedEditExtender>
                                            </span>
                                            <span><asp:Label ID="lblRacaCor" runat="server" Text="Raça/Cor" CssClass="rotulomin"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlRacaCor" runat="server" CssClass="drop">
                                                </asp:DropDownList></span>
                                                
                                        </p>
                                        <p>
                                            <span><asp:Label ID="lblRG" runat="server" Text="RG" CssClass="rotulo"></asp:Label></span>
                                                <span>
                                                    <asp:TextBox ID="tbxRG" runat="server" MaxLength="15" CssClass="campo" Width="90px"></asp:TextBox></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*"
                                                    ControlToValidate="tbxRG" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender15" runat="server" Mask="999999999999999"
                                                    MaskType="Number" TargetControlID="tbxRG" ClearMaskOnLostFocus="true" AutoComplete="false"
                                                    InputDirection="LeftToRight">
                                                </cc1:MaskedEditExtender>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblOrgaoEmissorRg" runat="server" CssClass="rotulo" Text="Orgão Emissor RG"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlOrgaoEmissorRG" runat="server" CssClass="drop">
                                            </asp:DropDownList></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                                                ControlToValidate="ddlOrgaoEmissorRG" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblDataEmissaoRG" runat="server" CssClass="rotulo" Text="Data Emissão"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxDataEmissaoRG" runat="server" CssClass="campo"></asp:TextBox></span>
                                            
                                            <cc1:MaskedEditExtender MaskType="Date" ID="MaskedEditExtender7" runat="server" TargetControlID="tbxDataEmissaoRG"
                                                Mask="99/99/9999">
                                            </cc1:MaskedEditExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" InitialValue="__/__/____"
                                                ErrorMessage="*" ControlToValidate="tbxDataEmissaoRG" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span>
                                            <asp:Label ID="lblTituloEleitor" runat="server" Text="Título de Eleitor" CssClass="rotulo"></asp:Label></span>
                                            <span >
                                                <asp:TextBox ID="tbxTituloEleitor" runat="server" MaxLength="13" CssClass="campo"></asp:TextBox>
                                            </span><span>
                                                <asp:Label ID="lblZonaEleitoral" runat="server" CssClass="rotulomin" Text="Zona"></asp:Label></span>
                                                <span><asp:TextBox ID="tbxZonaEleitoral" runat="server" CssClass="campo" MaxLength="4"
                                                    Width="50px"></asp:TextBox></span>
                                                
                                            <span>
                                                <asp:Label ID="lblSecaoEleitoral" runat="server" CssClass="rotulomin" Text="Seção"></asp:Label> </span>
                                                <span><asp:TextBox ID="tbxSecaoEleitoral" runat="server" MaxLength="4" CssClass="campo"
                                                    Width="50px"></asp:TextBox></span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblCTPS" runat="server" CssClass="rotulo" Text="CTPS Número"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxCTPS" runat="server" MaxLength="7" CssClass="campo"></asp:TextBox></span>
                                            <span>
                                                <asp:Label ID="lblSerieCTPS" runat="server" CssClass="rotulomin" Text="Série"></asp:Label></span>
                                                <span><asp:TextBox ID="tbxSerieCTPS" runat="server" MaxLength="5" CssClass="campo" Width="50px"></asp:TextBox></span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblNacionalidade" runat="server" CssClass="rotulo" Text="Nacionalidade"></asp:Label></span>
                                            <span>
                                                <asp:RadioButtonList ID="rbtNacionalidade" runat="server" RepeatDirection="Horizontal" RepeatColumns="2"
                                                    TextAlign="Right" CellPadding="0" CellSpacing="0" CssClass="camporadio" OnSelectedIndexChanged="rbtNacionalidade_SelectedIndexChanged"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="Brasileira" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Estrangeira" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ErrorMessage="*"
                                                    ControlToValidate="rbtNacionalidade" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </span>
                                        </p>
                                        <asp:Panel ID="PanelEstrangeiroSelecionado" runat="server">
                                            <p>
                                            <span><asp:Label ID="lblDataEntradaPais" runat="server" CssClass="rotulo" Text="Dt Entrada no País"></asp:Label></span>
                                                
                                                <span><asp:TextBox ID="tbxDataEntradaPais" runat="server" CssClass="campo" Width="60px"></asp:TextBox></span>
                                                
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" TargetControlID="tbxDataEntradaPais"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="tbxDataEntradaPais"
                                                    runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                                <span><asp:Label ID="lblDataNaturalizacao" runat="server" CssClass="rotulomin" Text="Dt. Naturalização"></asp:Label></span>
                                                <span><asp:TextBox ID="tbxDataNaturalizacao" runat="server" CssClass="campo" Width="60px"></asp:TextBox></span>
                                                
                                                <cc1:MaskedEditExtender ID="MaskedEditExtender13" runat="server" TargetControlID="tbxDataNaturalizacao"
                                                    MaskType="Date" Mask="99/99/9999">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="tbxDataNaturalizacao"
                                                    runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                                <span><asp:Label ID="lblNumeroPortaria" runat="server" CssClass="rotulomin" Text="Nº Portaria"></asp:Label></span>
                                                <span><asp:TextBox ID="tbxNumeroPortaria" runat="server" CssClass="campo" Width="45px"></asp:TextBox></span>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="tbxNumeroPortaria"
                                                    runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true"></asp:RequiredFieldValidator>
                                            </p>
                                        </asp:Panel>
                                        <p><span><asp:Label ID="lblStatusProfissional" runat="server" CssClass="rotulo" Text="Status do profissional"></asp:Label></span>
                                            
                                            <span >
                                                <asp:DropDownList ID="ddlStatusProfissional" runat="server" CssClass="drop">
                                                    <asp:ListItem Text="Ativo" Value="A"></asp:ListItem>
                                                    <asp:ListItem Text="Inativo" Value="I"></asp:ListItem>
                                                </asp:DropDownList>
                                            </span>
                                        </p>
                                        <p>
                                        <span> <asp:Label ID="lblDataAtualizacao" runat="server" CssClass="rotulo" Text="Data Última Atualização">
                                            </asp:Label></span>
                                           <span >
                                                <asp:TextBox ID="tbxDataAtualizacao" runat="server" CssClass="campo" Enabled="false"></asp:TextBox></span>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" TargetControlID="tbxDataAtualizacao"
                                                Mask="99/99/9999" MaskType="Date">
                                            </cc1:MaskedEditExtender>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblUltimoUsuario" runat="server" CssClass="rotulo" Text="Último usuário a Atualizar o Registro"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxUltimoUsuario" runat="server" Enabled="false" Width="170px" CssClass="campo"></asp:TextBox></span>
                                            
                                        </p>
                                        <%--<p>
                                            <asp:Label ID="lblCodPacienteViverMais" runat="server" CssClass="rotulo" Text="Código Paciente ViverMais"></asp:Label>
                                            <span style="margin-left: 5px;"></span>
                                            <asp:TextBox ID="tbxCodigoPacienteViverMais" runat="server" CssClass="campo" Enabled="false"></asp:TextBox>
                                        </p>--%>
                                    </fieldset>
                                </asp:WizardStep>
                                <asp:WizardStep ID="WizardStep3" runat="server" Title="Dados Residenciais e Bancários">
                                    <fieldset class="formulario">
                                        <legend>Dados Residenciais e Bancários</legend>
                                        <h4>
                                            Residência</h4>
                                        <p>
                                        <span><asp:Label ID="lblTipoLogradouro" CssClass="rotulo" runat="server" Text="Tipo de Logradouro"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlTipoLogradouro" runat="server" CssClass="drop">
                                            </asp:DropDownList></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ErrorMessage="*"
                                                ForeColor="Red" Font-Bold="true" ControlToValidate="ddlTipoLogradouro"></asp:RequiredFieldValidator>
                                            <span><asp:Label ID="lblLogradouro" runat="server" CssClass="rotulomin" Text="Logradouro"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxLogradouro" runat="server" CssClass="campo" MaxLength="60" Width="170px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="*"
                                                    ForeColor="Red" Font-Bold="true" ControlToValidate="tbxLogradouro"></asp:RequiredFieldValidator>
                                            </span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblNumeroEndereco" runat="server" CssClass="rotulo" Text="Número"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxNumeroEndereco" MaxLength="10" runat="server" CssClass="campo"></asp:TextBox></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="*"
                                                ForeColor="Red" Font-Bold="true" ControlToValidate="tbxNumeroEndereco"></asp:RequiredFieldValidator>
                                                <span><asp:Label ID="lblCEP" runat="server" CssClass="rotulomin" Text="CEP"></asp:Label></span>
                                            
                                            <span><asp:TextBox ID="tbxCEP" runat="server" CssClass="campo"></asp:TextBox></span>
                                            
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender11" runat="server" Mask="99,999-999"
                                                MaskType="Number" TargetControlID="tbxCEP" InputDirection="LeftToRight" ClearMaskOnLostFocus="false">
                                            </cc1:MaskedEditExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="*"
                                                ForeColor="Red" Font-Bold="true" ControlToValidate="tbxCEP"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblComplemento" runat="server" CssClass="rotulo" Text="Complemento"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxComplemento" MaxLength="60" runat="server" CssClass="campo" Width="190px"></asp:TextBox></span>
                                            <span><asp:Label ID="lblBairro" runat="server" CssClass="rotulomin" Text="Bairro"></asp:Label></span> 
                                            <span>
                                                    <asp:DropDownList ID="ddlBairro" runat="server" CssClass="drop">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ErrorMessage="*"
                                                        ForeColor="Red" Font-Bold="true" ControlToValidate="ddlBairro"></asp:RequiredFieldValidator>
                                                </span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblCodIBGE" runat="server" CssClass="rotulo" Text="Código IBGE" Width="75px"></asp:Label></span>
                                            <span>
                                                <asp:TextBox ID="tbxCodIBGE" runat="server" MaxLength="6" CssClass="campo" Width="60px"
                                                    OnTextChanged="tbxCodIBGE_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="*"
                                                    ControlToValidate="tbxCodIBGE" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            </span>
                                            <span>
                                                <asp:Label ID="lblMunicipio" runat="server" CssClass="rotulomin" Text="Município"></asp:Label></span>
                                                <span><asp:TextBox ID="tbxMunicipio" runat="server" CssClass="campo"></asp:TextBox></span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ErrorMessage="*"
                                                    ControlToValidate="tbxMunicipio" Font-Bold="true" ForeColor="Red"></asp:RequiredFieldValidator>
                                            <span>
                                                <asp:Label ID="lblUF" runat="server" CssClass="rotulomin" Text="UF"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlUFProf" runat="server" CssClass="drop">
                                                </asp:DropDownList></span>
                                                
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*"
                                                    Font-Bold="true" ForeColor="Red" ControlToValidate="ddlUFProf" InitialValue="0"></asp:RequiredFieldValidator>
                                            
                                        </p>
                                        <span><asp:Label ID="lblTelefone" runat="server" CssClass="rotulo" Text="Telefone"></asp:Label></span>
                                        <span><asp:TextBox ID="tbxTelefone" runat="server" CssClass="campo"></asp:TextBox></span>
                                        
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender12" runat="server" TargetControlID="tbxTelefone"
                                            Mask="(99)9999-9999" MaskType="Number" ClearMaskOnLostFocus="false" InputDirection="LeftToRight">
                                        </cc1:MaskedEditExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*"
                                            Font-Bold="true" ForeColor="Red" ControlToValidate="tbxTelefone"></asp:RequiredFieldValidator>
                                        <p>
                                            &nbsp
                                        </p>
                                        <h4>
                                            Dados Bancários</h4>
                                        <p>
                                        <span><asp:Label ID="lblBanco" runat="server" CssClass="rotulo" Text="Banco"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlBanco" runat="server" CssClass="drop">
                                            </asp:DropDownList></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ErrorMessage="*"
                                                Font-Bold="true" ForeColor="Red" ControlToValidate="ddlBanco"></asp:RequiredFieldValidator>
                                            <span>
                                                <asp:Label ID="lblAgencia" runat="server" Text="Agência" CssClass="rotulomin"></asp:Label></span>
                                                <span>
                                                    <asp:TextBox ID="tbxAgencia" runat="server" MaxLength="5" CssClass="campo"></asp:TextBox><asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator23" runat="server" ErrorMessage="*" Font-Bold="true"
                                                        ForeColor="Red" ControlToValidate="tbxAgencia"></asp:RequiredFieldValidator>
                                                </span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblContaCorrente" runat="server" CssClass="rotulo" Text="Conta-Corrente"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxContaCorrente" runat="server" MaxLength="14" CssClass="campo"></asp:TextBox><asp:RequiredFieldValidator
                                                ID="RequiredFieldValidator24" runat="server" ErrorMessage="*" Font-Bold="true"
                                                ForeColor="Red" ControlToValidate="tbxContaCorrente"></asp:RequiredFieldValidator></span>
                                            
                                        </p>
                                    </fieldset>
                                </asp:WizardStep>
                                <asp:WizardStep ID="WizardStep4" runat="server" StepType="Finish" Title="Vínculos">
                                    <fieldset style="width: auto; height: auto; margin-left: 0; margin-right: 0; padding: 10px 10px 20px 10px;">
                                        <legend>Vínculos</legend>
                                        <p>
                                        <span><asp:Label ID="lblCNES" runat="server" Text="CNES" CssClass="rotulo"></asp:Label></span>
                                        <span><asp:TextBox ID="tbxCNES" runat="server" CssClass="campo" OnTextChanged="tbxCNES_TextChanged"
                                                    AutoPostBack="true" ValidationGroup="InsereVinculo"></asp:TextBox></span>
                                        <span><asp:TextBox ID="tbxNomeEstabelecimento" runat="server" ValidationGroup="InsereVinculo"
                                                CssClass="campo" Width="270px"></asp:TextBox></span>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender14" runat="server" Mask="9999999" InputDirection="LeftToRight"
                                                    AutoComplete="false" TargetControlID="tbxCNES" MaskType="Number">
                                                </cc1:MaskedEditExtender>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server" ControlToValidate="tbxCNES"
                                                    Font-Size="XX-Small" ErrorMessage="*" SetFocusOnError="True" Display="Dynamic"
                                                    ValidationGroup="InsereVinculo"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblRegistroConselho" runat="server" CssClass="rotulo" Text="Registro no Conselho"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxRegistroConselho" runat="server" CssClass="campo"></asp:TextBox></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" ControlToValidate="tbxRegistroConselho"
                                                runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true" ValidationGroup="InsereVinculo"></asp:RequiredFieldValidator>
                                         </p>
                                        <p>
                                        <span><asp:Label ID="lblOrgaoEmissor" runat="server" CssClass="rotulo" Text="Orgão Emissor Conselho"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlOrgaoEmissorRegistroConselho" runat="server" CssClass="drop">
                                            </asp:DropDownList></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator26" ControlToValidate="ddlOrgaoEmissorRegistroConselho"
                                                runat="server" ErrorMessage="*" ForeColor="Red" Font-Bold="true" ValidationGroup="InsereVinculo"></asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblVinculacao" runat="server" CssClass="rotulo" Text="Vinculação"></asp:Label></span>
                                            <span><asp:DropDownList ID="ddlVinculacao" runat="server" OnSelectedIndexChanged="ddlVinculacao_SelectedIndexChanged"
                                                AutoPostBack="true" ValidationGroup="InsereVinculo" CssClass="drop">
                                            </asp:DropDownList></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" ControlToValidate="ddlVinculacao"
                                                Font-Size="XX-Small" ErrorMessage="*" SetFocusOnError="True" InitialValue="0"
                                                Display="Dynamic" ValidationGroup="InsereVinculo">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblTipoVinculacao" runat="server" CssClass="rotulo" Text="Tipo"></asp:Label></span>
                                            <span ><asp:DropDownList ID="ddlTipoVinculacao" ValidationGroup="InsereVinculo" runat="server"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlTipoVinculacao_SelectedIndexChanged" CssClass="drop">
                                            </asp:DropDownList></span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" ControlToValidate="ddlTipoVinculacao"
                                                Font-Size="XX-Small" ErrorMessage="*" SetFocusOnError="True" InitialValue="0"
                                                Display="Dynamic" ValidationGroup="InsereVinculo">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblSubTipoVinculacao" runat="server" CssClass="rotulo" Text="SubTipo"></asp:Label></span>
                                            <span ><asp:DropDownList ID="ddlSubTipoVinculacao" runat="server" AutoPostBack="true" CssClass="drop" ValidationGroup="InsereVinculo">
                                            </asp:DropDownList></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" ControlToValidate="ddlSubTipoVinculacao"
                                                Font-Size="XX-Small" ErrorMessage="*" SetFocusOnError="True" InitialValue="0"
                                                Display="Dynamic" ValidationGroup="InsereVinculo">
                                            </asp:RequiredFieldValidator>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblCBO" runat="server" Text="CBO" CssClass="rotulo"></asp:Label></span>
                                            <span><asp:TextBox ID="tbxCBO" runat="server" CssClass="campo" OnTextChanged="tbxCBO_TextChanged"
                                                AutoPostBack="true" ValidationGroup="InsereVinculo"></asp:TextBox> <asp:TextBox ID="tbxNomeCBO" runat="server" CssClass="campo" ValidationGroup="InsereVinculo"></asp:TextBox></span>
                                            
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ControlToValidate="tbxCBO"
                                                Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ValidationGroup="InsereVinculo">
                                            </asp:RequiredFieldValidator>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender10" runat="server" Mask="999999" MaskType="Number"
                                                TargetControlID="tbxCBO">
                                            </cc1:MaskedEditExtender>

                                            
                                        </p>
                                        <p>
                                            <h4>
                                                Carga Horária Semanal</h4>
                                        </p>
                                        
                                        <p>
                                        <span><asp:Label ID="lblCargaAmbulatorial" runat="server" Text="Ambulatorial" CssClass="rotulo"></asp:Label></span>
                                        <span><asp:TextBox ID="tbxCargaAmbulatorial" runat="server" CssClass="campo" Width="50px"
                                            ValidationGroup="InsereVinculo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="tbxCargaAmbulatorial"
                                            Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ValidationGroup="InsereVinculo">
                                        </asp:RequiredFieldValidator>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99" MaskType="Number"
                                            TargetControlID="tbxCargaAmbulatorial" AutoComplete="false">
                                        </cc1:MaskedEditExtender></span>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblCargaHospitalar" runat="server" CssClass="rotulo" Text="Hospital"></asp:Label></span>
                                        <span ><asp:TextBox ID="tbxCargaHospitalar" runat="server" CssClass="campo" Width="50px"
                                            ValidationGroup="InsereVinculo"></asp:TextBox></span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" ControlToValidate="tbxCargaHospitalar"
                                            Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ValidationGroup="InsereVinculo">
                                        </asp:RequiredFieldValidator>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99" MaskType="Number"
                                            TargetControlID="tbxCargaHospitalar" AutoComplete="false">
                                        </cc1:MaskedEditExtender>
                                        </p>
                                        <p>
                                        <span><asp:Label ID="lblCargaOutros" runat="server" CssClass="rotulo" Text="Outros"></asp:Label></span>
                                        <span><asp:TextBox ID="tbxCargaOutros" runat="server" CssClass="campo" Width="50px" ValidationGroup="InsereVinculo"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" ControlToValidate="tbxCargaOutros"
                                            Font-Size="XX-Small" ErrorMessage="*" Display="Dynamic" ValidationGroup="InsereVinculo">
                                        </asp:RequiredFieldValidator>
                                        <cc1:MaskedEditExtender ID="MaskedEditExtender9" runat="server" Mask="99" MaskType="Number"
                                            TargetControlID="tbxCargaOutros" AutoComplete="false">
                                        </cc1:MaskedEditExtender></span>
                                        </p>
                                        <p>
                                            &nbsp
                                        </p>
                                        <p>
                                            <asp:LinkButton ID="btnAddEstabelecimento" runat="server" OnClick="btnAddVinculo_Click"
                                                ValidationGroup="InsereVinculo">
                    <img id="addvinculo" alt="Pesquisar" src="img/incluir-vinculo1.png"
                        onmouseover="addvinculo.src='img/incluir-vinculo2.png';"
                        onmouseout="addvinculo.src='img/incluir-vinculo1.png';" />
                                            </asp:LinkButton>
                                        </p>
                                        <p>
                                            <h4>
                                                Vínculos Ativos</h4>
                                        </p>
                                        <p>
                                            <asp:Label ID="lblSemVinculos" runat="server" Text="Sem Vínculos Ativos" Font-Bold="true"
                                                ForeColor="Red"></asp:Label>
                                        </p>
                                        <asp:Panel ID="PanelVinculosAtivos" runat="server">
                                            <p>
                                                <asp:GridView ID="GridviewVinculosAtivos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                    EnableSortingAndPagingCallbacks="True" OnRowEditing="GridviewVinculosAtivos_RowEditing"
                                                    OnRowCancelingEdit="GridviewVinculosAtivos_RowCancelingEdit" OnRowUpdating="GridviewVinculosAtivos_RowUpdating"
                                                    BackColor="White" BorderColor="#cfc8d4" BorderStyle="None" BorderWidth="1px"
                                                    CellPadding="3" GridLines="Vertical" Width="650px">
                                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="10px" />
                                                    <Columns>
                                                        <asp:BoundField DataField="CodigoEstabelecimento" HeaderText="Codigo Estabelecimento" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Nome Fantasia" HeaderText="Nome Fantasia" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CodigoCBO" HeaderText="Codigo CBO" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CBO" HeaderText="CBO" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CodigoVinculacao" HeaderText="Codigo Vinculação" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Vinculacao" HeaderText="Vinculação" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CodigoTipoVinculacao" HeaderText="Codigo Tipo Vinculação" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Tipo" HeaderText="Tipo" ReadOnly="true" ItemStyle-HorizontalAlign="Center"/>
                                                        <asp:BoundField DataField="CodigoSubTipoVinculacao" HeaderText="Codigo SubTipo Vinculação" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SubTipo" HeaderText="SubTipo" ReadOnly="true" ItemStyle-HorizontalAlign="Center"/>
                                                        <asp:BoundField DataField="SUS" HeaderText="SUS" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CargaHorariaAmbulatorial" HeaderText="CHS Ambulatorial"
                                                            ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CargaHorariaHospitalar" HeaderText="CHS Hospitalar" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CargaHorariaOutros" HeaderText="CHS Outros" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="NumeroRegistro" HeaderText="NumeroRegistro" ReadOnly="true" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="CodigoOrgaoEmissor" HeaderText="Codigo OrgaoEmissor" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderStyle CssClass="colunaEscondida" />
                                                            <ItemStyle CssClass="colunaEscondida" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OrgaoEmissor" HeaderText="Orgão Emissor" ReadOnly="true" />
                                                        <asp:TemplateField HeaderText="Ativo?" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkBoxStatus" Enabled="false" Checked='<%#bind("Status") %>' runat="server" />
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:CheckBox ID="chkBoxStatus" runat="server" Checked='<%#bind("Status") %>' />
                                                            </EditItemTemplate>
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Editar">
                                                            <ItemStyle HorizontalAlign="Center" />
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit"
                                                                    CausesValidation="false">
                                                                    <asp:Image runat="server" ID="editarimg" ImageUrl="~/Profissional/img/btn-editar2x.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                                    CausesValidation="false" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ConfirmarAprazarProcedimento')) return confirm('Confirma as informações para este Vínculo?');"
                                                                    ValidationGroup="ValidationGroup_ConfirmarAprazarProcedimento">
                                                                    <asp:Image runat="server" ID="finalizarimg" ImageUrl="~/Profissional/img/btn-finalizar2x.png" AlternateText="Finalizar" /></asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                                    CausesValidation="false">
                                                                    <asp:Image runat="server" ID="cancelarimg" ImageUrl="~/Profissional/img/btn-cancel2x.png" AlternateText="Cancelar" />
                                                                    </asp:LinkButton>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--
                                                        <asp:CommandField ButtonType="Button" DeleteText="Excluir" ShowDeleteButton="True">
                                                            <ControlStyle Height="20px" Width="43px" />
                                                        </asp:CommandField>--%>
                                                    </Columns>
                                                    <PagerStyle BackColor="#f4f0f8" ForeColor="Black" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#f4f0f8" Font-Bold="True" ForeColor="White" />
                                                    <HeaderStyle BackColor="#714494" ForeColor="White" Font-Names="Arial"
                                                        Font-Size="9px" Height="22px" />
                                                    <AlternatingRowStyle BackColor="#f2eef5" />
                                                </asp:GridView>
                                            </p>
                                        </asp:Panel>
                                    </fieldset>
                                </asp:WizardStep>
                            </WizardSteps>
                            <SideBarButtonStyle BackColor="#507CD1" Font-Names="Arial" ForeColor="White" />
                            <NavigationButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="10px" ForeColor="#284E98" />
                            <SideBarStyle BackColor="#507CD1" Font-Size="0.9em" VerticalAlign="Top" Width="23%" />
                            <HeaderStyle BackColor="#284E98" BorderColor="#EFF3FB" BorderStyle="Solid" BorderWidth="2px"
                                Font-Bold="True" Font-Size="10px" ForeColor="White" HorizontalAlign="Center" />
                        </asp:Wizard>
                    </asp:Panel>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
