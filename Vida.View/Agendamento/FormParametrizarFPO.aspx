<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormParametrizarFPO.aspx.cs" Inherits="ViverMais.View.Agendamento.FormParametrizacaoFPO"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Incluir" />
            <asp:PostBackTrigger ControlID="btnIncluir1" />
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Formulário de Parametrização</h2>
                <fieldset class="formulario">
                    <legend>Parametrização do FPO</legend>
                    <p>
                        <asp:RadioButtonList ID="rbtnTipo" runat="server" RepeatDirection="Horizontal" TextAlign="Left"
                            CellPadding="0" CellSpacing="0" CssClass="camporadio" Height="27px" OnSelectedIndexChanged="rbtnTipo_SelectedIndexChanged"
                            AutoPostBack="true" Width="504px">
                            <asp:ListItem Value="1">Por CNES</asp:ListItem>
                            <asp:ListItem Value="2">Por Procedimento</asp:ListItem>
                        </asp:RadioButtonList>
                    </p>
                    <br />
                    <asp:Panel ID="PanelCnes" runat="server">
                        <p>
                            <span class="rotulo">CNES:</span> <span>
                                <asp:TextBox ID="tbxcnes" CssClass="campo" runat="server" AutoPostBack="True" OnTextChanged="tbxcnes_TextChanged"
                                    MaxLength="7" Width="100px"></asp:TextBox></span> <span style="position: absolute;">
                                        <asp:Label ID="lblcnes" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                    </span>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxcnes" Display="Dynamic" ErrorMessage="* Informe a unidade"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxcnes" Display="Dynamic" ErrorMessage="* Somente Números"
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></p>
                        <p>
                            <span class="rotulo">Percentual Prestador:</span> <span>
                                <asp:TextBox ID="tbxPrestador" CssClass="campo" runat="server" Width="50px" AutoPostBack="True"
                                    MaxLength="3" OnTextChanged="tbxPrestador_TextChanged"></asp:TextBox></span>
                        </p>
                        <p>
                            <span class="rotulo">Percentual Solicitante:</span> <span>
                                <asp:TextBox ID="tbxSolicitante" CssClass="campo" runat="server" Width="50px" Enabled="False"></asp:TextBox></span></p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador" Display="Dynamic" ErrorMessage="* Informe o Percentual do Prestador"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador" Display="Dynamic" ErrorMessage="* Somente Números"
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador" Display="Dynamic" ErrorMessage="* Percentual maior que 100%"
                                Operator="LessThanEqual" Type="Integer" ValueToCompare="100"></asp:CompareValidator>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnIncluir1" runat="server" OnClick="Incluir1_Click">
                <img id="imgincluir1" alt="Incluir" src="img/incluir_1.png"
                onmouseover="imgincluir1.src='img/incluir_2.png';"
                onmouseout="imgincluir1.src='img/incluir_1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnVoltar2" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                <img id="imgvoltar1" alt="Voltar" src="img/voltar_1.png"
                onmouseover="imgvoltar1.src='img/voltar_2.png';"
                onmouseout="imgvoltar1.src='img/voltar_1.png';" /></asp:LinkButton>
                        </div>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="PanelProcedimento" runat="server">
                        <p>
                            <span class="rotulo">CNES:</span> <span>
                                <asp:TextBox ID="tbxcnes1" CssClass="campo" runat="server" AutoPostBack="True" OnTextChanged="tbxcnes1_TextChanged"
                                    MaxLength="7" Width="100px"></asp:TextBox></span> <span>
                                        <asp:Label ID="lblcnes1" runat="server" Font-Bold="true"></asp:Label></span>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxcnes1" Display="Dynamic" ErrorMessage="* Informe a unidade"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator4" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxcnes1" Display="Dynamic" ErrorMessage="* Somente Números"
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></p>
                        <p>
                            <span class="rotulo">Grupo:</span> <span>
                                <asp:DropDownList ID="ddlGrupo" runat="server" CssClass="drop" Height="21px" OnSelectedIndexChanged="ddlGrupo_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlGrupo" Display="Dynamic" ErrorMessage="* Selecione o Grupo do Procedimento"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Sub-Grupo:</span> <span>
                                <asp:DropDownList ID="ddlSubGrupo" CssClass="drop" Height="21px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlSubGrupo_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlSubGrupo" Display="Dynamic" ErrorMessage="* Selecione o Sub-Grupo do Procedimento"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Forma de Organização:</span> <span>
                                <asp:DropDownList ID="ddlForma" CssClass="drop" Height="21px" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlForma_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlForma" Display="Dynamic" ErrorMessage="* Selecione a Forma de Organização do Procedimento"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Procedimento:</span> <span>
                                <asp:DropDownList ID="ddlProcedimento" CssClass="drop" Height="21px" runat="server"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" Font-Size="XX-Small" runat="server"
                                    ControlToValidate="ddlProcedimento" Display="Dynamic" ErrorMessage="* Selecione o Procedimento"
                                    InitialValue="0"></asp:RequiredFieldValidator></span>
                        </p>
                        <p>
                            <span class="rotulo">Percentual Prestador:</span> <span>
                                <asp:TextBox ID="tbxPrestador1" CssClass="campo" runat="server" Width="50px" AutoPostBack="True"
                                    MaxLength="3" OnTextChanged="tbxPrestador1_TextChanged"></asp:TextBox></span>
                            <span class="rotulomin">Percentual Solicitante:</span> <span>
                                <asp:TextBox ID="tbxSolicitante1" CssClass="campo" runat="server" Width="50px" Style="margin-right: 0px"
                                    Enabled="False"></asp:TextBox></span>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador1" Display="Dynamic" ErrorMessage="* Informe o Percentual do Prestador"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador1" Display="Dynamic" ErrorMessage="* Somente Números"
                                Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator6" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxPrestador1" Display="Dynamic" ErrorMessage="* Percentual maior que 100%"
                                Operator="LessThanEqual" Type="Integer" ValueToCompare="100"></asp:CompareValidator>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="Incluir" runat="server" OnClick="Incluir_Click">
                <img id="imgincluir" alt="Incluir" src="img/incluir_1.png"
                onmouseover="imgincluir.src='img/incluir_2.png';"
                onmouseout="imgincluir.src='img/incluir_1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="Voltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx">
                <img id="imgvoltar" alt="Voltar" src="img/voltar_1.png"
                onmouseover="imgvoltar.src='img/voltar_2.png';"
                onmouseout="imgvoltar.src='img/voltar_1.png';" /></asp:LinkButton>
                        </div>
                    </asp:Panel>
                </fieldset>
                <asp:Panel ID="PanelListaFPO" runat="server">
                    <fieldset class="formulario">
                        <legend>Parametrização do FPO Cadastrado</legend>
                        <br />
                        <p>
                            <asp:GridView ID="GridViewListaFPO" runat="server" AutoGenerateColumns="False" BackColor="White"
                                BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                GridLines="Vertical" onrowediting="GridViewListaFPO_RowEditing" 
                                Width="529px" onrowcancelingedit="GridViewListaFPO_RowCancelingEdit" 
                                onrowupdating="GridViewListaFPO_RowUpdating">
                              <FooterStyle BackColor="#477ba5" ForeColor="Black" />
                                    <RowStyle BackColor="#a6c5de" ForeColor="Black" Font-Size="11px" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="EstabelecimentoSaude" HeaderText="EAS"  ReadOnly="true"/>
                                    <asp:TemplateField HeaderText="% Prestador" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbxPercentualPrestador" runat="server" CssClass="campo" Width="70px" 
                                                ReadOnly="true" Text='<%#bind("ValorPrestador") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbxPercentualPrestador" runat="server" CssClass="campo" Width="70px"
                                                Text='<%#bind("ValorPrestador") %>'>
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldTbxPercentualPrestador" ControlToValidate="tbxPercentualPrestador"
                                                runat="server" ErrorMessage="Preencha o Percentual" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="% Solicitante" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbxPercentualSolicitante" runat="server" CssClass="campo" Width="70px"
                                                ReadOnly="true" Text='<%#bind("ValorSolicitante") %>'>
                                            </asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbxPercentualSolicitante" runat="server" CssClass="campo" Width="70px"
                                                Text='<%#bind("ValorSolicitante") %>'>
                                            </asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldTbxPercentualSolicitante" ControlToValidate="tbxPercentualSolicitante"
                                                runat="server" ErrorMessage="Preencha o Percentual" InitialValue="0">
                                            </asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <%--<asp:BoundField DataField="ValorPrestador" HeaderText="% Prestador" ItemStyle-HorizontalAlign="Center" />--%>
                                    <%--<asp:BoundField DataField="ValorSolicitante" HeaderText="% Solicitante" ItemStyle-HorizontalAlign="Center" />--%>
                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" 
                                        ItemStyle-HorizontalAlign="Center"  ReadOnly="true" >
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditarPacto" runat="server" CommandName="Edit" CausesValidation="false">
                                                <asp:Image ID="imgEdit" ImageUrl="~/Agendamento/img/bt_edit.png" runat="server" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('Validation_ConfirmarPacto')) return confirm('Confirma as informações dessa Parametrização?');">
                                                <asp:Image ID="imgFinalizar" ImageUrl="~/Agendamento/img/confirma.png" AlternateText="Confirmar"
                                                    runat="server" />
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                CausesValidation="false">
                                                <asp:Image ID="imgCancelar" ImageUrl="~/Agendamento/img/cancela.png" runat="server"
                                                    AlternateText="Cancelar" />
                                            </asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:TemplateField>
                                </Columns>
                              <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana" Font-Size="11px" Height="22px" />
                                    <AlternatingRowStyle BackColor="#c2dcf2" />
                            </asp:GridView>
                        </p>
                    </fieldset></asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
