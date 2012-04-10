<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="BuscaAfastamentoProfissional.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaAfastamentoProfissional"
    Title="Módulo Regulação - Afastamento de Profissionais" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Afastamento de Profissionais</h2>
        <br />
        <div>
            Clique no botão abaixo para efetuar um afastamento de Profissional<p>
                <asp:LinkButton ID="btnCadastrarAfastamento" runat="server" CausesValidation="false"
                    PostBackUrl="~/Agendamento/FormAfastarProfissional.aspx">
                       <img id="img_Afastamento" alt="Cadastrar Novo Afastamento de Profissional" src="img/btn-cad-novo-prof1.png"
                onmouseover="img_Afastamento.src='img/btn-cad-novo-prof2.png';"
                onmouseout="img_Afastamento.src='img/btn-cad-novo-prof1.png';" />
                </asp:LinkButton></p>
        </div>
        <br />
        <asp:Panel ID="PanelBuscaAfastamento" runat="server">
            <fieldset class="formulario">
                <legend>Busca de Afastamentos</legend>
                <p>
                    <span class="rotulo">CNES da Unidade</span> <span>
                        <asp:TextBox ID="tbxcnes" runat="server" CssClass="campo"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="tbxcnes"
                            ErrorMessage="*" ForeColor="Red" Font-Bold="true" Font-Size="XX-Small"></asp:RequiredFieldValidator></span>
                </p>
                <p>
                    <span class="rotulo">Nº Conselho</span> <span>
                        <asp:TextBox ID="tbxNumConselho" runat="server" CssClass="campo"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="tbxNumConselho"
                            ErrorMessage="*" ForeColor="Red" Font-Bold="true" Font-Size="XX-Small"></asp:RequiredFieldValidator></span>
                </p>
                <p>
                    <asp:Label ID="lblCategoria" runat="server" Text="Categoria" CssClass="rotulo"></asp:Label>
                    <span>
                        <asp:DropDownList ID="ddlCategoria" runat="server" ValidationGroup="Pesquisa" CssClass="drop"
                            DataTextField="Nome" DataValueField="Codigo">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCategoria"
                            ErrorMessage="*" ForeColor="Red" Font-Bold="true" InitialValue="0" Font-Size="XX-Small"></asp:RequiredFieldValidator>
                    </span>
                </p>
                <div class="botoesroll">
                    <asp:LinkButton ID="imgPesquisar" runat="server" CausesValidation="true" OnClick="imgPesquisar_Click">
                            <img id="imgpesquisar" alt="Buscar Profissional" src="img/pesquisar_1.png"
                            onmouseover="imgpesquisar.src='img/pesquisar_2.png';"
                            onmouseout="imgpesquisar.src='img/pesquisar_1.png';" />
                    </asp:LinkButton>
                </div>
            </fieldset>
            <asp:Panel ID="PanelListaAfastamentos" runat="server">
                <fieldset class="formulario">
                    <legend>Lista os Afastamentos Cadastrados</legend>
                    <asp:GridView ID="GridViewListaAfastamentos" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Width="100%" OnPageIndexChanging="GridViewListaAfastamentos_PageIndexChanging"
                        DataKeyNames="Codigo" OnRowCommand="GridViewListaAfastamentos_RowCommand">
                        <Columns>
                            <%--<asp:BoundField DataField="Codigo">
                                <HeaderStyle CssClass="colunaEscondida" />
                                <ItemStyle CssClass="colunaEscondida" />
                                <FooterStyle CssClass="colunaEscondida" />
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Unidade" HeaderText="Unidade" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="Profissional" HeaderText="Profissional" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="Data_Inicial" HeaderText="Data Inicial" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd/MM/yyyy}"></asp:BoundField>
                            <asp:TemplateField HeaderText="Data Retorno">
                                <ItemTemplate>
                                    <asp:TextBox ID="tbxDataRetorno" runat="server" CssClass="campo" Width="85px" ReadOnly="true"
                                        Text='<%#bind("Data_Final") %>'>
                                    </asp:TextBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxDataRetorno" runat="server" CssClass="campo" Width="85px" Text='<%#bind("Data_Final") %>'>
                                    </asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldDataRetorno" runat="server" ControlToValidate="tbxDataRetorno"
                                        Display="None" ErrorMessage="A data de Retorno é obrigatória">
                                    </asp:RequiredFieldValidator>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="DataRetorno" HeaderText="Data Retorno" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>--%>
                            <asp:BoundField DataField="Motivo" HeaderText="Motivo" ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:BoundField DataField="Obs" HeaderText="Observação." ItemStyle-HorizontalAlign="Center">
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="cmdSelect" runat="server" CausesValidation="false" CommandArgument='<%#Eval("Codigo")%>'
                                        CommandName="Select">
                                        <asp:Image ID="imgSelect" runat="server" ImageUrl="~/img/bts/bt_edit.png" />
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                        CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroupSolicitacao')) return confirm('Confirma as informações para esta Solicitação?');return false;">
                                        <asp:Image ID="imgFinalizar" ImageUrl="~/Agendamento/img/confirma.png" AlternateText="Confirmar"
                                            runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                        CausesValidation="false">
                                        <asp:Image ID="imgCancelar" ImageUrl="~/Agendamento/img/cancela.png" runat="server"
                                            AlternateText="Cancelar" />
                                    </asp:LinkButton>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                        ShowSummary="false" ValidationGroup="ValidationGroupSolicitacao" />
                                </EditItemTemplate>--%>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <asp:Label ID="lblSemRegistros" runat="server" Text="Nenhum Registro Localizado"
                                Font-Size="X-Small">
                            </asp:Label>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="tab" />
                        <RowStyle CssClass="tabrow_left" />
                    </asp:GridView>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="PanelDadosAfastamento" runat="server" Visible="false">
                <div id="cinza" class="backgroundMensagem" visible="false">
                </div>
                <div id="mensagem" visible="false" class="divMensagem">
                    <p>
                        <span style="float: right;">
                            <asp:LinkButton ID="btnFechar" runat="server" CausesValidation="false" OnClick="btnFechar_Click">
                                <img src="img/fechar-agendamento.png" id="imgInforme" alt=""/>
                            </asp:LinkButton>
                        </span>
                    </p>
                    <div style="clear: right;">
                    </div>
                    <fieldset>
                        <legend>Afastamento Selecionado</legend>
                        <p>
                            <span class="rotulo">Estabelecimento:</span> <span>
                                <asp:TextBox ID="tbxCodigoAfastamento" runat="server" Visible="false" CssClass="campo"></asp:TextBox></span>
                            <asp:Label ID="lblEstabelecimento" runat="server"></asp:Label>
                        </p>
                        <br />
                        <p>
                            <span class="rotulo">Profissional:</span> <span></span>
                            <asp:Label ID="lblProfissional" runat="server"></asp:Label>
                        </p>
                        <p>
                            <span class="rotulo">Data Inicial:</span> <span>
                                <asp:Label ID="lblDataInicial" runat="server"></asp:Label></span>
                        </p>
                        <p>
                            <span class="rotulo">Data Retorno:</span> <span>
                                <asp:TextBox ID="tbxDataRetorno" runat="server" CssClass="campo"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender6" runat="server" Animated="true" Format="dd/MM/yyyy"
                                    TargetControlID="tbxDataRetorno">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                    TargetControlID="tbxDataRetorno" Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="tbxDataRetorno" ErrorMessage="Campo Obrigatório"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Motivo:</span> <span>
                                <asp:TextBox ID="tbxMotivo" CssClass="campo" runat="server" MaxLength="150" Width="300px"
                                    Rows="3" Height="50px" ReadOnly="true"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="tbxMotivo" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Observação:</span> <span>
                                <asp:TextBox ID="tbxObs" CssClass="campo" runat="server" MaxLength="150" Width="300px"
                                    Rows="1" Height="50px">
                                </asp:TextBox></span>
                        </p>
                        <p>
                            &nbsp;</p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnSalvaAfastamento" runat="server" OnClick="btnSalvaAfastamento_Click">
                    <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  onmouseover="imgsalvar.src='img/salvar_2.png';"
                  onmouseout="imgsalvar.src='img/salvar_1.png';" />
                            </asp:LinkButton>
                        </div>
                    </fieldset>
                </div>
            </asp:Panel>
        </asp:Panel>
    </div>
</asp:Content>
