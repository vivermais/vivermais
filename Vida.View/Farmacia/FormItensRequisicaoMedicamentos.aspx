<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormItensRequisicaoMedicamentos.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormItensRequisicaoMedicamentos" EnableEventValidation="false"
    MasterPageFile="~/Farmacia/MasterFarmacia.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .modalBackground
        {
            background-color: #CCCCFF;
            filter: alpha(opacity=40);
            opacity: 0.5;
        }
        .ModalWindow
        {
            border: none;
            background:none; 
            padding: 30px 20px 10px 50px;
            position: absolute;
            top: -1000px;
            font-family: Verdana, Arial;
            color: #ffffff;
            
            
            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <span>
        <h2>Requisição de Medicamentos<span style=" padding-left:300px">Passo 2 de 2</span></h2>
        </span>
    
        
        <fieldset>
            <legend>Dados da Requisição</legend>
            <p>
                <span>Farmácia</span> <span>
                    <asp:Label ID="Label_Farmacia" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span>Nº da Requisição</span> <span>
                    <asp:Label ID="Label_NumeroRequisicao" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span>Data de Abertura</span> <span>
                    <asp:Label ID="Label_DataAbertura" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span>Data de Envio ao Distrito</span> <span>
                    <asp:Label ID="Label_DataEnvioDistrito" runat="server" Text=""></asp:Label>
                </span>
            </p>
            <p>
                <span>Status</span> <span>
                    <asp:Label ID="Label_Status" runat="server" Text=""></asp:Label>
                </span>
            </p>
         
                <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton_EnviarRequisicao" runat="server" Visible="false" OnClick="OnClick_EnviarDistrito">
                     <img id="imgenviar" alt="Enviar" src="img/btn/enviar1.png"
                  onmouseover="imgenviar.src='img/btn/enviar2.png';"
                  onmouseout="imgenviar.src='img/btn/enviar1.png';" />
                    </asp:LinkButton>
                    </div>
                    <div class="botoesroll">
                    <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Farmacia/FormBuscaRM.aspx">
                     <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                    </asp:LinkButton>
                    </div>
               
        </fieldset>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView_MedicamentosSolicitados" EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <fieldset>
                    <legend>Elencos da Farmácia</legend>
                    <p>
                        <span>
                            <asp:TreeView ID="TreeView_Medicamentos" runat="server" Visible="false" OnSelectedNodeChanged="OnSelectedNodeChanged_IncluirMedicamento">
                            </asp:TreeView>
                            <asp:Label ID="LabelElencosFarmacias" runat="server" Text="Não há elencos disponíveis para esta farmácia." Visible="false"></asp:Label>
                        </span>
                    </p>
                    <cc1:ModalPopupExtender ID="ModalPopupExtender_IncluirMedicamento" runat="server"
                        PopupControlID="Panel_IncluirMedicamento" RepositionMode="None" BackgroundCssClass="modalBackground"
                        TargetControlID="HiddenField_IncluirMedicamento">
                    </cc1:ModalPopupExtender>
                    <asp:HiddenField ID="HiddenField_IncluirMedicamento" runat="server" />
                  <%--  /fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff--%>
                    <asp:Panel ID="Panel_IncluirMedicamento" runat="server" Visible="true" CssClass="ModalWindow" BackImageUrl="~/Farmacia/img/bg-efect.png" Height="280px" Width="549px" background-repeat="repeat"> 
                        <br />
                        <p>
                            <span class="rotulo_effect">Medicamento</span> <span>
                                <asp:Label ID="Label_MedicamentoSelecionado" runat="server" Text="" Font-Bold="true" Font-Size="13px"></asp:Label></span>
                        </p>
                        <p>
                            <span class="rotulo_effect">Posição Estoque</span> <span>
                                <asp:Label ID="Label_PosicaoAtualEstoqueMedicamento" runat="server" Text="" Font-Bold="true" Font-Size="13px"></asp:Label></span>
                        </p>
                        <p>
                            <span class="rotulo_effect">Quantidade Solicitada</span> <span>
                                <asp:TextBox ID="TextBox_QuantidadeSolicitadaMedicamento" MaxLength="4" runat="server" Width="42px" Font-Size="13px" Font-Bold="true" CssClass="campo"></asp:TextBox>
                            </span>
                        </p>
                          <br />
                          <br />
                          
           <p></p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_ConfirmarInclusao" runat="server" OnClick="OnClick_IncluirNovoMedicamento"
                                ValidationGroup="ValidationGroup_IncluirMedicamento">
                                     <img id="imgincluir" alt="Incluir" src="img/btn/incluir1.png"
                  onmouseover="imgincluir.src='img/btn/incluir2.png';"
                  onmouseout="imgincluir.src='img/btn/incluir1.png';" />
                                </asp:LinkButton>
                                </div>
                                
                              <div class="botoesroll">  
                            <asp:LinkButton ID="LinkButton_CancelarInclusao" runat="server" OnClick="OnClick_CancelarInclusaoMedicamento">
                            <img id="imgfechar" alt="Fechar" src="img/btn/cancelar1.png"
                  onmouseover="imgfechar.src='img/btn/cancelar2.png';"
                  onmouseout="imgfechar.src='img/btn/cancelar1.png';" />
                            </asp:LinkButton>
                   </div>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Quantidade Solicitada é Obrigatório."
                                ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento" Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Digite somente números em Quantidade Solicitada."
                                ValidationExpression="^\d*$" ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento"
                                Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="O valor em Quantidade Solicitada deve ser maior que zero."
                                Operator="GreaterThan" ValueToCompare="0" ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento"
                                Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ValidationGroup="ValidationGroup_IncluirMedicamento"
                                ShowMessageBox="true" ShowSummary="false" />
                        </p>
                    </asp:Panel>
                         <%--  /fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff--%>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="LinkButton_ConfirmarInclusao" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <fieldset>
                    <legend>Medicamentos da Requisição</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_MedicamentosSolicitados" runat="server" AllowPaging="true"
                                OnPageIndexChanging="OnPageIndexChanging_Medicamentos" PageSize="20" PagerSettings-Mode="Numeric" BorderColor="Silver"
                                    Font-Size="Small" Width="100%"
                                OnRowDataBound="OnRowDataBound_ConfigurarGridMedicamentos" AutoGenerateColumns="false"
                                OnRowEditing="OnRowEditing_ItemRequisicao" OnRowCancelingEdit="OnRowCancelingEdit_ItemRequisicao"
                                OnRowUpdating="OnRowUpdating_ItemRequisicao" DataKeyNames="Codigo" OnRowCommand="OnRowCommand_ExcluirItemRequisicao">
                                <Columns>
                                    <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" ReadOnly="true"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Qtd Solicitada" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_QtdContada" runat="server" Text='<%#bind("QtdPedida") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox_QtdSolicitada" Width="30px" MaxLength="4" CssClass="campo"
                                                runat="server" Text='<%#bind("QtdPedida") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Qtd Solicitada é Obrigatório!"
                                                Display="None" ControlToValidate="TextBox_QtdSolicitada"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Qtd Contada."
                                                Display="None" ValidationExpression="^\d*$" ControlToValidate="TextBox_QtdSolicitada"></asp:RegularExpressionValidator>
                                            <asp:CompareValidator ID="CompareValidator_1" runat="server" ErrorMessage="Qtd Solicitada deve ser maior que zero."
                                                ControlToValidate="TextBox_QtdSolicitada" Display="None" ValueToCompare="0" Operator="GreaterThan"></asp:CompareValidator>
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                ShowSummary="false" />
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="QtdPedida" HeaderText="Quantidade Solicitada" ItemStyle-HorizontalAlign="Center" />--%>
                                    <%--<asp:BoundField DataField="SaldoAtual" HeaderText="" ReadOnly="true"
                            ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:TemplateField HeaderText="Posição Estoque" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_QtdEstoque" runat="server" Text='<%#bind("SaldoAtual") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="Label_QtdEstoqueAlterar" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ButtonType="Link" CancelText="Cancelar" UpdateText="Alterar" EditText="Editar"
                                        InsertVisible="false" ShowEditButton="true" />
                                    <asp:ButtonField ButtonType="Link" CausesValidation="true" CommandName="CommandName_Excluir"
                                        Text="Excluir" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Não há item algum para esta requisição."></asp:Label>
                                </EmptyDataTemplate>
                                 <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#006666" ForeColor="White" HorizontalAlign="Center" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--        <asp:UpdatePanel ID="UpdatePanel89" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TreeView_Medicamentos" EventName="SelectedNodeChanged" />
            </Triggers>
            <ContentTemplate>
                <fieldset>
                    <legend>Novos medicamentos</legend>
                    <p>
                    </p>
                </fieldset>
                <fieldset>
                    <legend>Medicamentos solicitados</legend>
                    <p>
                        <asp:GridView ID="GridView_MedicamentosSolicitados" runat="server" AllowPaging="true"
                            OnPageIndexChanging="OnPageIndexChanging_Medicamentos" PageSize="20" PagerSettings-Mode="Numeric"
                            OnRowDataBound="OnRowDataBound_ConfigurarGridMedicamentos" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="QtdPedida" HeaderText="Quantidade Solicitada" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SaldoAtual" HeaderText="Posição Estoque" ItemStyle-HorizontalAlign="Center" />
                            </Columns>
                        </asp:GridView>
                    </p>
                </fieldset>
                <asp:Panel ID="PanelAdicionarNovoMedicamento" runat="server" Visible="false">
                    <div id="cinza" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                        height: 130%; z-index: 100; min-height: 100%; background-color: #999; filter: alpha(opacity=40);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; top: 100px; left: 25%;
                        width: 600px; z-index: 102; background-color: #541010; border-right: #ffffff  5px solid;
                        padding-right: 10px; border-top: #ffffff  5px solid; padding-left: 10px; padding-bottom: 10px;
                        border-left: #ffffff  5px solid; color: #000000; padding-top: 10px; border-bottom: #ffffff 5px solid;
                        text-align: justify; font-family: Verdana;">
                        <div style="padding-left: 50px;">
                            <br>
                                <br></br>
                                <br></br>
                                <asp:Label ID="NovoMedicamento" runat="server" Text="Incluir Novo Medicamento"></asp:Label>
                                <br />
                                <p>
                                    <span>Medicamento</span> <span>
                                        <asp:Label ID="Label_MedicamentoSelecionado" runat="server" Text=""></asp:Label></span>
                                </p>
                                <p>
                                    <span>Posição Estoque</span> <span>
                                        <asp:Label ID="Label_PosicaoAtualEstoqueMedicamento" runat="server" Text=""></asp:Label></span>
                                </p>
                                <p>
                                    <span>Quantidade Solicitada</span> <span>
                                        <asp:TextBox ID="TextBox_QuantidadeSolicitadaMedicamento" MaxLength="5" runat="server"></asp:TextBox>
                                    </span>
                                </p>
                                <p>
                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_IncluirNovoMedicamento"
                                        ValidationGroup="ValidationGroup_IncluirMedicamento">Incluir</asp:LinkButton>
                                </p>
                                <p>
                                    <p style="padding: 20px 10px 50px 0">
                                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_CancelarInclusaoMedicamento">Fechar</asp:LinkButton>
                                    </p>
                                    <p>
                                    </p>
                                    <p>
                                    </p>
                                    <br></br>
                                </p>
                                <p>
                                    <asp:RequiredFieldValidator ID="RequiredValidator1" runat="server" ErrorMessage="Quantidade Solicitada é Obrigatório."
                                        ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento" Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números em Quantidade Solicitada."
                                        ValidationExpression="^\d*$" ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento"
                                        Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:RegularExpressionValidator>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="O valor em Quantidade Solicitada deve ser maior que zero."
                                        Operator="GreaterThan" ValueToCompare="0" ControlToValidate="TextBox_QuantidadeSolicitadaMedicamento"
                                        Display="None" ValidationGroup="ValidationGroup_IncluirMedicamento"></asp:CompareValidator>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_IncluirMedicamento"
                                        ShowMessageBox="true" ShowSummary="false" />
                                </p>
                            </br>
                        </div>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>
    </div>
</asp:Content>
