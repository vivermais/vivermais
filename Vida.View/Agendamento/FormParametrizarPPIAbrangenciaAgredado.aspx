﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="True"
    CodeBehind="FormParametrizarPPIAbrangenciaAgredado.aspx.cs" Inherits="ViverMais.View.Agendamento.FormParametrizarPPIAbrangenciaAgredado"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <%--<asp:PostBackTrigger ControlID="img_btnAddAgregado" />--%>
            <%--<asp:PostBackTrigger ControlID="btnSalvar" />--%>
        </Triggers>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Parametrizar PPI Por Abrangência</h2>
                <fieldset style="width: 880px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
                    <legend>Programação Pactuada e Integrada Por Abrangência</legend>
                    <%--<asp:UpdatePanel ID="Up1" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="img_btnAddAgregado" />
                        </Triggers>--%>
                    <%--<ContentTemplate>--%>
                    <p>
                        &nbsp
                    </p>
                    <p>
                        <asp:Label ID="Grupos" runat="server" CssClass="rotulo" Text="Grupos"></asp:Label>
                        <span>
                            <asp:DropDownList ID="ddlGrupos" runat="server" CssClass="drop" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlGrupos_SelectedIndexChanged">
                            </asp:DropDownList>
                        </span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o Grupo"
                            ControlToValidate="ddlGrupos" InitialValue="0" Font-Size="XX-Small" ValidationGroup="ValidationPacto"></asp:RequiredFieldValidator>
                    </p>
                    <%--<p>
                                <asp:Label ID="lblGrupoAgregados" runat="server" CssClass="rotulo" Text="Grupo de Agregados"></asp:Label>
                                <span>
                                    <asp:DropDownList ID="ddlGrupoAgregados" runat="server" CssClass="drop" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlGrupoAgregados_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <asp:Label ID="lblSubGrupoAgregado" runat="server" CssClass="rotulo" Text="SubGrupo de Agregados"></asp:Label>
                                <span>
                                    <asp:DropDownList ID="ddlSubGrupoAgregado" runat="server" CssClass="drop" AutoPostBack="true"
                                        OnSelectedIndexChanged="ddlSubGrupoAgregado_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </span>
                            </p>
                            <p>
                                <asp:Label ID="lblAgregados" runat="server" CssClass="rotulo" Text="Agregados"></asp:Label>
                                <span>
                                    <asp:DropDownList ID="ddlAgregados" runat="server" CssClass="drop">
                                        <asp:ListItem Value="0" Text="Selecione..."></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldAgregado" runat="server" ErrorMessage="Selecione o Agregado"
                                        ControlToValidate="ddlAgregados" Font-Size="XX-Small" InitialValue="0" ValidationGroup="ValidationPacto">
                                    </asp:RequiredFieldValidator>
                                </span>
                            </p>
                            <p>
                                <span class="rotulo">Informe o tipo de Pacto</span> <span>
                                    <asp:RadioButtonList ID="rbtlTipoPacto" runat="server" CssClass="camporadio" RepeatDirection="Horizontal"
                                        AutoPostBack="true" OnSelectedIndexChanged="rbtlTipoPacto_OnSelectedIndexChanged">
                                        <asp:ListItem Text="Por Agregado" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="Por Procedimento" Value="P" Enabled="false"></asp:ListItem>
                                        <asp:ListItem Text="Por CBO" Value="C" Enabled="false"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Selecione o Tipo"
                                        ControlToValidate="rbtlTipoPacto" Font-Size="XX-Small" ValidationGroup="ValidationPacto">
                                    </asp:RequiredFieldValidator>
                                </span>
                            </p>--%>
                    <%--                            <asp:Panel ID="PanelProcedimento" runat="server" Visible="false">
                                <p>
                                    <span class="rotulo">Informe o Procedimento</span>
                                    <asp:DropDownList ID="ddlProcedimento" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlProcedimento_OnSelectedIndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="ddlProcedimento"
                                        runat="server" Font-Size="XX-Small" ErrorMessage="Selecione o Procedimento" InitialValue="0"
                                        ValidationGroup="ValidationPacto">
                                    </asp:RequiredFieldValidator>
                                </p>
                            </asp:Panel>--%>
                    <%--                            <asp:Panel ID="PanelCBO" runat="server" Visible="false">
                                <p>
                                    <span class="rotulo">Informe o CBO</span>
                                    <asp:DropDownList ID="ddlCBO" runat="server" CssClass="drop">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="ddlCBO"
                                        runat="server" ErrorMessage="Selecione o CBO" Font-Size="XX-Small" InitialValue="0"
                                        ValidationGroup="ValidationPacto">
                                    </asp:RequiredFieldValidator>
                                </p>
                            </asp:Panel>--%>
                    <%--<p>
                                <span class="rotulo">Informe o Valor do Pacto</span>
                                <asp:TextBox ID="tbxValorPacto" runat="server" CssClass="campo" OnKeyPress="return(MascaraMoeda(this,'.',',',event));"></asp:TextBox>
                                <span style="position: absolute;">
                                    <asp:ImageButton ID="img_btnAddAgregado" runat="server" OnClick="btnAddAgregado_Click"
                                        Width="19px" Height="19px" ImageUrl="~/Agendamento/img/add.png" ValidationGroup="ValidationPacto" />
                                </span>
                                <br />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="tbxValorPacto"
                                    runat="server" Font-Size="XX-Small" ErrorMessage="Preencha o Valor Para Este Pacto"
                                    ValidationGroup="ValidationPacto">
                                </asp:RequiredFieldValidator>
                            </p>--%>
                    <br />
                    <%--<p>
                        <asp:Label ID="lblSemAgregado" runat="server" Text="Sem Agregados Selecionados" Font-Bold="true"
                            ForeColor="Red"></asp:Label>
                    </p>--%>
                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </fieldset>
                <p>
                    &nbsp;
                </p>
                <fieldset style="width: 880px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
                    <legend>Pactos do Grupo Selecionado</legend>
                    <p>
                        <asp:Panel runat="server" ID="PanelPactosAtivosInativos" Visible="true">
                            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                                <cc1:TabPanel ID="TabelPanel_Um" runat="server" HeaderText="Pactos Ativos">
                                    <ContentTemplate>
                                        <p>
                                            <asp:Label ID="lblSemRegistroAtivo" runat="server" Text="Nenhum Pacto Ativo" Visible="false"
                                                Font-Size="X-Small"></asp:Label>
                                        </p>
                                        <p>
                                            <asp:GridView ID="GridViewPactosAtivos" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                                BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" OnRowCommand="GridViewPactosAtivos_RowCommand"
                                                OnRowUpdating="GridViewPactosAtivos_RowUpdating" OnRowEditing="GridViewPactosAtivos_RowEditing"
                                                OnRowCancelingEdit="GridViewPactosAtivos_RowCancelingEdit" OnPageIndexChanging="GridViewPactosAtivos_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo" ReadOnly="true">
                                                        <HeaderStyle CssClass="colunaEscondida" />
                                                        <ItemStyle CssClass="colunaEscondida" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Ano" HeaderText="Ano" ReadOnly="true" />
                                                    <asp:BoundField DataField="TipoPacto" HeaderText="Tipo Pacto" ReadOnly="true" />
                                                    <asp:BoundField DataField="CodigoAgregado" ReadOnly="true">
                                                        <HeaderStyle CssClass="colunaEscondida" />
                                                        <ItemStyle CssClass="colunaEscondida" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="NomeAgregado" HeaderText="Agregado" ReadOnly="true" />
                                                    <asp:BoundField DataField="CodigoProcedimento" HeaderText="Procedimento Selecionado"
                                                        ReadOnly="true">
                                                        <HeaderStyle CssClass="colunaEscondida" />
                                                        <ItemStyle CssClass="colunaEscondida" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" ReadOnly="true" />
                                                    <asp:BoundField DataField="CodigoCBO" ReadOnly="true">
                                                        <HeaderStyle CssClass="colunaEscondida" />
                                                        <ItemStyle CssClass="colunaEscondida" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CBO" HeaderText="CBO" ReadOnly="true" />
                                                    <asp:BoundField DataField="ValorPactuado" HeaderText="Valor Pacto" ReadOnly="true" />
                                                    <%--<asp:TemplateField HeaderText="Editar" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEditarPacto" runat="server" CommandName="Edit" CausesValidation="false">
                                                                <asp:Image ID="imgEdit" ImageUrl="~/Agendamento/img/bt_edit.png" runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                                CausesValidation="true" OnClientClick="javascript:if (Page_ClientValidate('Validation_ConfirmarPacto')) return confirm('Confirma as informações para este Pacto?');"
                                                                ValidationGroup="Validation_ConfirmarPacto">
                                                                <asp:Image ID="imgFinalizar" ImageUrl="~/Agendamento/img/confirma.png" AlternateText="Confirmar"
                                                                    runat="server" />
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="LinkButton_CancelarConfirmacao" runat="server" CommandName="Cancel"
                                                                CausesValidation="false">
                                                                <asp:Image ID="imgCancelar" ImageUrl="~/Agendamento/img/cancela.png" runat="server"
                                                                    AlternateText="Cancelar" />
                                                            </asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Inativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                                CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja INATIVAR este Pacto?');"
                                                                Text="">
                                                                <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Parametrizar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-VerticalAlign="Middle">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="cmdParametrizar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                                CommandName="Parametrizar" Text="Parametrizar" ><img src="img/edit-peq.png" alt="Parametrizar" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                                    Font-Size="11px" Height="22px" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </p>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                                <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Pactos Inativos">
                                    <ContentTemplate>
                                        <p>
                                            <asp:Label ID="lblSemRegistroInativo" runat="server" Text="Nenhum Pacto Inativo"
                                                Visible="false" Font-Size="X-Small"></asp:Label>
                                        </p>
                                        <p>
                                            <asp:GridView ID="GridViewPactosInativos" runat="server" AllowPaging="true" AutoGenerateColumns="false"
                                                BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                                                OnPageIndexChanging="GridViewPactosInativos_PageIndexChanging" CellPadding="3"
                                                GridLines="Vertical" Width="100%">
                                                <Columns>
                                                    <asp:BoundField DataField="Ano" HeaderText="Ano" ReadOnly="true" />
                                                    <asp:BoundField DataField="TipoPacto" HeaderText="Tipo Pacto" />
                                                    <asp:BoundField DataField="Agregado" HeaderText="Agregado" />
                                                    <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                                    <asp:BoundField DataField="CBO" HeaderText="CBO" />
                                                    <asp:BoundField DataField="ValorPactuado" HeaderText="Valor Pacto" />
                                                </Columns>
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                                    Font-Size="11px" Height="22px" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                        </p>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                        </asp:Panel>
                    </p>
                </fieldset>
                <asp:Panel ID="PanelDadosDoPacto" runat="server" Visible="false">
                   <div id="cinza" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 150%;
                       z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                       moz-opacity: 0.3; opacity: 0.3" visible="false";>
                   </div>
                   <div id="mensagem" style="position: fixed; top: 150px; left: 50%; margin-left:-350px; width: 700px;
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
                            <legend>Pacto Abrangência</legend>
                            <p>
                                <span class="rotulo-panel">Grupo Abrangência</span> <span>
                                    <asp:Label ID="lblGrupoAbrangência" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p>
                            <p>
                                <span class="rotulo-panel">Valor do Pacto</span> <span>
                                    <asp:Label ID="lblTotalValorPactuado" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label></span>
                            </p>
                           <%-- <asp:UpdatePanel ID="UpdatePanelCotaFinanceira" runat="server">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlMunicipios" EventName="SelectedIndexChanged" />
                                </Triggers>
                                <ContentTemplate>--%>
                                    <p>
                                        <span class="rotulo-panel">Município</span><span>
                                            <asp:DropDownList ID="ddlMunicipios" runat="server" DataTextField="NomeSemUF" DataValueField="Codigo"                                                
                                                CssClass="drop">
                                                <asp:ListItem Text="Selecione" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </span>
                                    </p>
                                    <%--<p>
                                        <span class="rotulo-panel">Cota Financeira do Município</span> <span>
                                            <asp:TextBox ID="tbxCotaFinanceira" onkeypress="javascript:return MascaraMoeda(this,'.',',',event);"
                                                runat="server" Enabled="false" CssClass="campo"></asp:TextBox>
                                        </span>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_Cota" runat="server" ControlToValidate="tbxCotaFinanceira"
                                            Display="Dynamic" ErrorMessage="* Preencha a Cota para o Município"></asp:RequiredFieldValidator>
                                    </p>
                                    <p>
                                        &nbsp</p>
                                    <p>
                                        <asp:LinkButton ID="btnSalvarCota" runat="server" CausesValidation="true" OnClick="btnSalvarCota_OnClick">
                                            <img id="img_incluir" alt="Salvar" src="img/salvar_1.png"
                                                onmouseover="img_incluir.src='img/salvar_2.png';"
                                                onmouseout="img_incluir.src='img/salvar_1.png';" />
                                        </asp:LinkButton>
                                    </p>--%>
                                <%--</ContentTemplate>
                            </asp:UpdatePanel>--%>
                            <%--<asp:Panel ID="PanelMunicipios" runat="server">
                            </asp:Panel>--%>
                            </fieldset>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
