<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormGerarReceitaDispensacao.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormGerarReceitaDispensacao" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
    <style type="text/css">
        .formulario2
        {
            width: 660px;
            height: auto;
            margin-left: 5px;
            margin-right: 5px;
            padding: 5px 5px 5px 5px;
        }
        .accordionHeader2
        {
            border: 1px solid #142126;
            color: #142126;
            background-color: #eee; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 720px;
        }
        .accordionHeaderSelected2
        {
            border: 1px solid #142126;
            color: white;
            background-color: #142126; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 720px;
        }
        .accordionContent2
        {
            background-color: #fff;
            border: 1px solid #142126;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 720px;
        }
    </style>
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2><asp:Literal ID="litTituloPagina" Text="Cadastro de Receita" runat="server"></asp:Literal></h2>
        <cc1:Accordion ID="AccordionReceita" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
            HeaderCssClass="accordionHeader2" HeaderSelectedCssClass="accordionHeaderSelected2"
            ContentCssClass="accordionContent2">
            <HeaderTemplate>
            </HeaderTemplate>
            <ContentTemplate>
            </ContentTemplate>
            <Panes>
                <cc1:AccordionPane ID="AccordionPane1" runat="server">
                    <Header>
                        Pesquisar Paciente</Header>
                    <Content>
                        <fieldset class="formulario2">
                            <legend>Informações</legend>
                            <p>
                                <span>
                                    <asp:LinkButton ID="lnkBiometria" runat="server" PostBackUrl="~/Farmacia/FormBiometriaGerarReceita.aspx">
                    <img id="img_newbiometria" alt="" src="../img/bts/bts_ident_bio.png"
                        onmouseover="img_newbiometria.src='../img/bts/bts_ident_bio_b.png';"
                        onmouseout="img_newbiometria.src='../img/bts/bts_ident_bio.png';" />
                                    </asp:LinkButton>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Cartão SUS</span> <span>
                                    <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" runat="server" MaxLength="15"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Nome</span> <span>
                                    <asp:TextBox ID="tbxNomePaciente" CssClass="campo" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Nome da Mãe</span> <span>
                                    <asp:TextBox ID="tbxNomeMae" CssClass="campo" runat="server"></asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Data de Nascimento</span> <span>
                                    <asp:TextBox ID="tbxDataNascimento" CssClass="campo" runat="server"></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" TargetControlID="tbxDataNascimento"
                                        Format="dd/MM/yyyy" PopupButtonID="calendar_icon.png" runat="server">
                                    </cc1:CalendarExtender>
                                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                        TargetControlID="tbxDataNascimento" Mask="99/99/9999" MaskType="Date">
                                    </cc1:MaskedEditExtender>
                                </span>
                            </p>
                            
                                <asp:LinkButton ID="btnBuscarPaciente" runat="server"  ValidationGroup="group_pesquisaPaciente" OnClick="OnClick_BuscarPaciente">
                  <img id="imgbuscapaciente" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgbuscapaciente.src='img/btn/pesquisar2.png';"
                  onmouseout="imgbuscapaciente.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Nascimento com formato inválido!"
                                    ControlToValidate="tbxDataNascimento" Operator="DataTypeCheck" Type="Date" ValidationGroup="group_pesquisaPaciente"
                                    Display="None"></asp:CompareValidator>
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Nascimento deve ser igual ou maior que 01/01/1900!"
                                    ControlToValidate="tbxDataNascimento" Operator="GreaterThanEqual" ValueToCompare="01/01/1900"
                                    Type="Date" ValidationGroup="group_pesquisaPaciente" Display="None"></asp:CompareValidator>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                    ShowSummary="false" ValidationGroup="group_pesquisaPaciente" />
                            
                        </fieldset>
                        <asp:UpdatePanel ID="UpdatePanel_Um" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnBuscarPaciente" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="PanelResultado" runat="server" Visible="false">
                                    <fieldset class="formulario2">
                                        <legend>Resultado</legend>
                                        <p>
                                            <span>
                                                <asp:GridView ID="GridView_ResultadoPesquisaPaciente" runat="server" AutoGenerateColumns="false"
                                                    AllowPaging="true" PageSize="20" OnRowCommand="OnRowCommand_SelecionarPaciente"
                                                    DataKeyNames="Codigo" OnPageIndexChanging="OnPageIndexChanging_PaginacaoPaciente"
                                                    PagerSettings-Mode="Numeric" Width="650px" BorderColor="Silver" Font-Size="X-Small">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Nome" DataField="Nome">
                                                          <ItemStyle Width="300px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Mãe" DataField="NomeMae">
                                                          <ItemStyle Width="250px" HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}">
                                                          <ItemStyle Width="90px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:ButtonField ButtonType="Link" CommandName="CommandName_Selecionar" Text="<img src='../img/bts/bt_edit.png' border='0' alt='Editar'>"
                                                         ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10px" />
                                                    </Columns>
                                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Height="20px" />
                                                </asp:GridView>
                                            </span>
                                        </p>
                                    </fieldset></asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="AccordionPane2" runat="server">
                    <Header>
                        Pesquisar Profissional</Header>
                    <Content>
                        <fieldset class="formulario2">
                            <legend>Informações</legend>
                            <p>
                                <span class="rotulo">Nº Conselho:</span> <span>
                                    <asp:TextBox CssClass="campo" ID="tbxNumeroConselhoProfissional" runat="server"
                                        MaxLength="7" Width="60px">
                                    </asp:TextBox>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Categoria do Profissional </span><span>
                                    <asp:DropDownList ID="ddlCategoriaProfissional" runat="server" DataTextField="Nome"
                                        DataValueField="Codigo" Width="300px">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p align="left">
                                <span>
                                    <asp:LinkButton ID="ImageButton2" runat="server"  OnClick="OnClick_PesquisarProfissional"
                                        ValidationGroup="group_pesqProfissional">
                  <img id="imgvincular" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgvincular.src='img/btn/pesquisar2.png';"
                  onmouseout="imgvincular.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None"
                                        ControlToValidate="tbxNumeroConselhoProfissional" ErrorMessage="Nº Conselho é Obrigatório."
                                        ValidationGroup="group_pesqProfissional"></asp:RequiredFieldValidator>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" Display="None" ControlToValidate="ddlCategoriaProfissional"
                                        ValueToCompare="-1" Operator="GreaterThan" ErrorMessage="Selecione uma Categoria do Profissional."
                                        ValidationGroup="group_pesqProfissional"></asp:CompareValidator>
                                    <asp:ValidationSummary ID="ValidationSummary2" ShowMessageBox="true" ShowSummary="false"
                                        runat="server" ValidationGroup="group_pesqProfissional" />
                                </span>
                            </p>
                        </fieldset>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
        <fieldset class="formulario">
            <legend>Informações da Receita    </legend>
            <asp:UpdatePanel ID="UpdatePanel_Tres" runat="server" UpdateMode="Conditional">
                <%--                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="GridView_ResultadoPesquisaPaciente"
                        EventName="RowCommand"/>
                </Triggers>--%>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Paciente</span> <span>
                            <asp:DropDownList ID="DropDownList_Paciente" runat="server" Width="300px">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel_Quatro" runat="server" UpdateMode="Conditional">
             <%--   <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ImageButton2" EventName="Click" />
                </Triggers>--%>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Profissional</span> <span>
                            <asp:DropDownList ID="DropDownList_Profissional" runat="server" Width="300px">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Data da Receita</span> <span>
                    <asp:TextBox ID="TextBox_DataReceita" CssClass="campo" runat="server"></asp:TextBox>
                </span>
            </p>
            <p>
                <span class="rotulo">Município Origem</span> <span">
                    <asp:DropDownList ID="ddlMunicipio" runat="server" Width="300px" AutoPostBack="true"
                        OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaDistrito">
                    </asp:DropDownList>
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipio" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Distrito Origem</span> <span>
                            <asp:DropDownList ID="ddlDistrito" runat="server" Width="300px">
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Fora da Rede</span> <span style="margin-left: 5px;">
                    <asp:TextBox ID="TxtMedicamentosFora" CssClass="campo" runat="server" Text="0"></asp:TextBox>
                </span>
            </p>
        </fieldset>
        
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button_IncluirMedicamento"  EventName="Click" />
            </Triggers>
            <ContentTemplate>                  
                <asp:Panel ID="Panel_ConteudoDispensacao" runat="server" Visible="true">
                <fieldset class="formulario">
                    <legend>Medicamentos</legend>    
                    <p>
                        <span class="rotulo">Buscar Medicamento</span> <span>
                            <asp:TextBox ID="TextBox_BuscarMedicamento" runat="server" CssClass="campo" Width="350px"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton1" runat="server" OnClick="OnClick_PesquisarMedicamento"
                                ValidationGroup="ValidationGroup_ProcurarMedicamento" ImageUrl="~/Farmacia/img/procurar.png"
                                Width="16px" Height="16px" Style="vertical-align: bottom;"/>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Medicamento</span> <span>
                            <asp:DropDownList ID="DropDownList_Medicamento" runat="server" AutoPostBack="true"
                                Width="350px" DataValueField="Codigo" DataTextField="Nome">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Quantidade Prescrita</span> <span>
                            <asp:TextBox ID="TextBox_QuantidadePrescrita" runat="server" CssClass="campo" Width="75px"></asp:TextBox>
                        </span>
                    </p>                    
                    <p align="center">
                        <span>
                        <div class="botoesroll">
                            <asp:LinkButton ID="Button_IncluirMedicamento" runat="server" Text="Inculir" OnClick="OnClick_IncluirMedicamento"
                                ValidationGroup="ValidationGroup_DispensarMedicamento">
                                 <img id="imgincluir" alt="Incluir" src="img/btn/incluir1.png"
                onmouseover="imgincluir.src='img/btn/incluir2.png';"
                onmouseout="imgincluir.src='img/btn/incluir1.png';" />
                                </asp:LinkButton>
                                </div>
                                
                                <div class="botoesroll">
                                <asp:LinkButton ID="Button_CancelarInclusao" runat="server" Text="Cancelar" OnClick="OnClick_CancelarInclusao">
                             <img id="imgurcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                onmouseover="imgurcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgurcancelar.src='img/btn/cancelar1.png';" />
                            </asp:LinkButton>
                            </div>
                        </span>
                    </p>
                </fieldset></asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="ID1" runat="server" UpdateMode="Conditional">
            <Triggers>
               
            </Triggers>
            <ContentTemplate>
                <p>
                    <span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Informe o nome do medicamento."
                            ControlToValidate="TextBox_BuscarMedicamento" ValidationGroup="ValidationGroup_ProcurarMedicamento"
                            Display="None"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Informe pelos menos as três primeiras letras do medicamento."
                            ControlToValidate="TextBox_BuscarMedicamento" ValidationExpression="^(\W{3,})|(\w{3,})$"
                            Display="None" ValidationGroup="ValidationGroup_ProcurarMedicamento">
                        </asp:RegularExpressionValidator>
                        <asp:ValidationSummary ID="ValidationSummary4" runat="server" ValidationGroup="ValidationGroup_ProcurarMedicamento"
                            ShowMessageBox="true" ShowSummary="false" />
                        <p>
                            <asp:CompareValidator ID="CompareValidator8" runat="server" 
                                ControlToValidate="DropDownList_Medicamento" Display="None" 
                                ErrorMessage="Selecione um medicamento." Operator="GreaterThan" 
                                ValidationGroup="ValidationGroup_DispensarMedicamento" ValueToCompare="-1"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ControlToValidate="TextBox_QuantidadePrescrita" Display="None" 
                                ErrorMessage="Quantidade Prescrita é Obrigatório." 
                                ValidationGroup="ValidationGroup_DispensarMedicamento"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                                ControlToValidate="TextBox_QuantidadePrescrita" Display="None" 
                                ErrorMessage="Digite somente números em Quantidade Prescrita." 
                                ValidationExpression="^\d*$" 
                                ValidationGroup="ValidationGroup_DispensarMedicamento">
                        </asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator9" runat="server" 
                                ControlToValidate="TextBox_QuantidadePrescrita" Display="None" 
                                ErrorMessage="Quantidade Prescrita deve ser maior que zero." 
                                Operator="GreaterThan" ValidationGroup="ValidationGroup_DispensarMedicamento" 
                                ValueToCompare="0"></asp:CompareValidator>
 
 
 
                            <asp:ValidationSummary ID="ValidationSummary5" runat="server" 
                                ShowMessageBox="true" ShowSummary="false" 
                                ValidationGroup="ValidationGroup_DispensarMedicamento" />
                            <p>
                            </p>
                    </p>
                    </span>
               </p>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button_IncluirMedicamento" EventName="Click" />
            
            </Triggers>
            <ContentTemplate>
                  <asp:Panel ID="Panel_MedicamentosDispensados" runat="server" Visible="false">
                        <fieldset class="formulario">
                            <legend>Medicamentos incluídos</legend>                               
                             <!-- <asp:GridView ID="GridView_Medicamentos11" runat="server" 
                                AllowPaging="true" AutoGenerateColumns="false" BorderColor="Silver" 
                                DataKeyNames="CodMedicamento" Font-Size="X-Small" 
                                OnPageIndexChanging="OnPageIndexChanging_Paginacao" 
                                onrowcancelingedit="GridView_MedicamentosDispensar_RowCancelingEdit" 
                                OnRowCommand="OnRowCommand_VerificarAcao" 
                                onrowediting="GridView_MedicamentosDispensar_RowEditing" 
                                onrowupdating="GridView_MedicamentosDispensar_RowUpdating" 
                                PagerSettings-Mode="Numeric" PageSize="10" Width="700px">
                                <Columns>
                                    <asp:BoundField DataField="CodMedicamento" HeaderText="Codigo" 
                                        ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Bottom" Width="50px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Nome" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="410px"  />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="QD">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_QtdPrescrita" runat="server" Text='<%#bind("QtdPrescrita") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_QtdPrescrita" runat="server" CssClass="campo" 
                                                Text='<%#bind("QtdPrescrita") %>' Width="25px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="30px" />
                                    </asp:TemplateField>
                                    
                                    <%--  
                                    <asp:BoundField DataField="QtdPrescrita" HeaderText="QP" ReadOnly="true">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="30px" />
                                    </asp:BoundField>
                                    
                                    <asp:TemplateField HeaderText="QD">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_QtdDi" runat="server" Text='<%#bind("QtdDias") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_QtdDi" runat="server" CssClass="campo" 
                                                Text='<%#bind("QtdDias") %>' Width="25px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="30px" />
                                    </asp:TemplateField>
                                    --%>
                                    
                                    <%--<asp:ButtonField ButtonType="Link" CommandName="CommandName_Editar" Text="<img src='img/bt_edit.png' border='0' alt='Editar'>" ItemStyle-Width="10px" />--%>
                                    <asp:CommandField ButtonType="Link" 
                                        CancelText="&lt;img src='img/bt_del.png' border='0' alt='Cancelar'&gt;" 
                                        CausesValidation="false" 
                                        EditText="&lt;img src='img/bt_edit.png' border='0' alt='Editar'&gt;" 
                                        InsertVisible="false" ShowEditButton="true" 
                                        UpdateText="&lt;img src='img/alterar.png' border='0' alt='Alterar'&gt;" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_Excluir" 
                                        ItemStyle-Width="10px" 
                                        Text="&lt;img src='img/bt_del.png' border='0' alt='Cancelar'&gt;" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum medicamento incluído."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#f0f0f0" ForeColor="Black" Height="20px" />
                                <PagerStyle BackColor="#006666" Font-Bold="True" ForeColor="White" 
                                    HorizontalAlign="Center" />
                            </asp:GridView>
                              -->
                            <asp:GridView ID="GridView_Medicamentos" runat="server" 
                                AutoGenerateColumns="False" DataKeyNames="CodMedicamento,Codigo" 
                                onrowediting="GridView_Medicamentos_RowEditing" 
                                onrowdeleting="GridView_Medicamentos_RowDeleting" 
                                onrowupdating="GridView_Medicamentos_RowUpdating">
                                <Columns>
                                    <asp:BoundField DataField="CodMedicamento" HeaderText="Código" 
                                        ReadOnly="True" />
                                    <asp:BoundField DataField="NomeMedicamento" HeaderText="Nome" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="Quantidade">
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("QtdPrescrita") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TxtQuantidadePrescritaEdit" runat="server" 
                                                Text='<%# Bind("QtdPrescrita") %>'></asp:TextBox>                                            
                                            <br />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                                ControlToValidate="TxtQuantidadePrescritaEdit" Display="Dynamic" 
                                                ErrorMessage="Informe a quantidade." SetFocusOnError="True">Informe a 
                                            quantidade.</asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                        </fieldset></asp:Panel>
             </ContentTemplate>
        </asp:UpdatePanel>
        
    <span>                    
        <asp:LinkButton ID="Button_SalvarReceita" runat="server"  OnClick="OnClick_SalvarReceita"
                        ValidationGroup="ValidationGroup_RegistrarReceita" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_RegistrarReceita')) return confirm('Todos os dados da receita estão corretos ?'); return false;">
                  <img id="imgsalvar2" alt="Salvar" src="img/btn/salvar1.png"
                  onmouseover="imgsalvar2.src='img/btn/salvar2.png';"
                  onmouseout="imgsalvar2.src='img/btn/salvar1.png';" /></asp:LinkButton>
                    <asp:LinkButton ID="Button1" runat="server"  PostBackUrl="~/Farmacia/Default.aspx">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn/cancelar1.png';" /></asp:LinkButton>
                    <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Selecione um Paciente."
                        Display="None" ControlToValidate="DropDownList_Paciente" Operator="NotEqual"
                        ValueToCompare="-1" ValidationGroup="ValidationGroup_RegistrarReceita"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator7" runat="server" ErrorMessage="Selecione um Profissional."
                        Display="None" ControlToValidate="DropDownList_Profissional" Operator="NotEqual"
                        ValueToCompare="-1" ValidationGroup="ValidationGroup_RegistrarReceita"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_DataReceita"
                        Display="None" ValidationGroup="ValidationGroup_RegistrarReceita" ErrorMessage="Data da Receita é Obrigatório."></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data da Receita com formato inválido."
                        Type="Date" Operator="DataTypeCheck" Display="None" ControlToValidate="TextBox_DataReceita"
                        ValidationGroup="ValidationGroup_RegistrarReceita"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Data da Receita deve ser igual ou maior que 01/01/1900."
                        ValueToCompare="01/01/1900" Operator="GreaterThanEqual" Type="Date" Display="None"
                        ControlToValidate="TextBox_DataReceita" ValidationGroup="ValidationGroup_RegistrarReceita"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_RegistrarReceita" />
                    
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataReceita">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" InputDirection="LeftToRight" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="TextBox_DataReceita" runat="server">
                    </cc1:MaskedEditExtender>
                    <asp:CustomValidator ID="CustomValidator_RegistroReceita" runat="server" ErrorMessage="CustomValidator"
                        ValidationGroup="ValidationGroup_RegistrarReceita" Display="None" OnServerValidate="OnServerValidate_RegistroReceita">
                    </asp:CustomValidator>
                </span>
    </div>
    <p></p>
</asp:Content>
