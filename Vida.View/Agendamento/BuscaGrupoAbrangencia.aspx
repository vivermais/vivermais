﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="BuscaGrupoAbrangencia.aspx.cs" Inherits="ViverMais.View.Agendamento.BuscaGrupoAbrangencia"
    Title="Módulo Regulação - Cadastro de Grupo de Abrangência PPI" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
<script type="text/javascript" language="javascript">
function showTooltip(obj) {
if (obj.options[obj.selectedIndex].title == "") {
obj.title = obj.options[obj.selectedIndex].text;
obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
for (i = 0; i < obj.options.length; i++) {
obj.options[i].title = obj.options[i].text;
}
}
else
obj.title = obj.options[obj.selectedIndex].text;
}
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Cadastro de Grupo de Abrangência PPI</h2>
        <br />
        <div>
            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Agendamento/FormCadastroGrupoAbrangencia.aspx">
            <img id="img_Grupo" alt="Cadastrar Grupo Abrangência" src="img/novo-grupo-abrangencia1.png"
            onmouseover="img_Grupo.src='img/novo-grupo-abrangencia2.png';"
            onmouseout="img_Grupo.src='img/novo-grupo-abrangencia1.png';" />
            </asp:LinkButton></div>
        <br />
        <asp:Panel ID="PanelListaGrupoAbrangencia" runat="server">
            <asp:UpdatePanel ID="UpdatePanelVisualizaVinculo" runat="server" ChildrenAsTriggers="true">
                <%--<Triggers>
                       <asp:AsyncPostBackTrigger ControlID="btnSim" EventName="Click" />
                       <asp:AsyncPostBackTrigger ControlID="btnNao" EventName="Click" />
                   </Triggers>--%>
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Vincular Município ao Grupo</legend>
                        <p>
                            <span class="rotulo">Selecione o Grupo</span>
                            <asp:DropDownList ID="ddlGrupoAbrangencia" runat="server" OnSelectedIndexChanged="ddlGrupoAbrangencia_OnSelectedIndexChanged" AutoPostBack="true" Font-Size="X-Small" Width="520px">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Selecione o Grupo"
                                InitialValue="0" Display="Dynamic" Font-Size="X-Small" SetFocusOnError="true"
                                ControlToValidate="ddlGrupoAbrangencia" ValidationGroup="AddMunicipio">
                            </asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <span class="rotulo">Selecione o Município</span>
                            <%--<asp:ListBox ID="lbxMunicipios" runat="server">
                            <asp:ListItem Value="0" Text="Selecione..."></asp:ListItem>
                        </asp:ListBox>
                        <span></span><span></span><span></span>
                        <asp:ListBox ID="lbxMunicipiosSelecionados" runat="server">k
                        </asp:ListBox>--%>
                            <asp:DropDownList ID="ddlMunicipios" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Selecione o Município"
                                InitialValue="0" Display="Dynamic" Font-Size="X-Small" SetFocusOnError="true"
                                ControlToValidate="ddlMunicipios" ValidationGroup="AddMunicipio">
                            </asp:RequiredFieldValidator>
                            <span style="position: absolute;">
                                <asp:ImageButton ID="img_btnAddGrupo" runat="server" OnClick="btnAddVinculo_Click" AlternateText="Clique para Incluir Vínculo"
                                    Width="19px" Height="19px" ImageUrl="~/Agendamento/img/add.png" ValidationGroup="AddMunicipio" />
                            </span>
                           <br />   <br />
                            <p>
                                <asp:Label runat="server" Text="Caso queira incluir TODOS os municípios ao grupo, clique no botão abaixo:" Font-Size="Small" Font-Bold="true"></asp:Label>
                                </p>
                                <p>
                                <span>
                                <br />
                                    <asp:LinkButton ID="btnAddTodosMunicipios" Text="clique aqui" runat="server" CausesValidation="true"
                                        OnClick="btnAddTodosMunicipios_Click">
                                        <img id="img_incluir1" alt="Incluir" src="img/incluir_1.png"
            onmouseover="img_incluir1.src='img/incluir_2.png';"
            onmouseout="img_incluir1.src='img/incluir_1.png';" />
                                        </asp:LinkButton>
                                </span>
                            </p>
                        </p>
                    </fieldset>
                    <asp:Panel ID="PanelVinculos" runat="server">
                        <fieldset class="formulario">
                            <legend>Vínculos</legend>
                            <asp:Label ID="lblSemRegistro" runat="server" Text="Nenhum Grupo Cadastrado" Visible="false"
                                Font-Size="X-Small"></asp:Label>
                            <asp:TreeView ID="TreeViewVinculoGrupoMunicipio" runat="server" OnSelectedNodeChanged="TreeViewVinculoGrupoMunicipio_SelectedNodeChanged">
                            </asp:TreeView>
                        </fieldset>
                    </asp:Panel>
                    <asp:Panel ID="PanelExcluirVinculo" runat="server" Visible="false">
                        <div id="cinza" style="position: absolute; top: 0px; left: 0px; width: 100%; height: 150%;
                           z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                           moz-opacity: 0.3; opacity: 0.3" visible="false";>
                       </div>
                        <div id="mensagem" style="position: fixed; top: 150px; left: 50%; margin-left:-350px; width: 700px;
                       z-index: 102; background-color: #0d2639; border: #ffffff 5px solid; padding-right: 20px;
                        padding-left: 20px; padding-bottom: 20px; color: #c5d4df; padding-top: 10px;  text-align: justify;
                       font-family: Verdana;" visible="false">
                            <p>
                                <span style="margin-left: 60px;">
                                    <asp:Label ID="lblConfirmacao" runat="server" Font-Size="9pt"
                                        Font-Bold="True"></asp:Label>
                                </span>
                            </p>
                            <div style="position:relative; left:50%; margin-left:-110px; width:220px; text-decoration:none; margin-top: 10px;">
                                <asp:LinkButton ID="btnSim" runat="server" OnClick="btnSim_Click" CausesValidation="false"
                                    Font-Size="X-Small">
                                    <img src="img/btn-sim.png" alt="Sim" />
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnNao" runat="server" Text="NÃO" OnClick="btnNao_Click" CausesValidation="false"
                                    Font-Size="X-Small">
                                    <img src="img/btn-nao.png" alt="Não" />
                                </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                                
                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
