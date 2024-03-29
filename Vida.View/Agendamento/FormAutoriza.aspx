﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAutoriza.aspx.cs" Inherits="ViverMais.View.Agendamento.FormAutoriza" %>

<%@ Register Assembly="SpiceLogicBLOB" Namespace="SpiceLogic.BLOBControl" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" href="~/style_ViverMais.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="style_form_agendamento.css" type="text/css" media="screen" />

    <script type="text/javascript">
        function close() 
        {    
            parent.parent.location.reload();            
            parent.parent.GB_hide();        
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="true"
            EnablePartialRendering="true" ScriptMode="Release" LoadScriptsBeforeUI="false">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="up2" UpdateMode="Always">
            <Triggers>
                <asp:PostBackTrigger ControlID="lknSalvar" />
            </Triggers>
            <ContentTemplate>
                <div id="top">
                    <h2>
                        Autorização de Procedimentos</h2>
                    <br />
                    <%--<asp:Panel ID="panelConfirmacao" runat="server" Visible="false">
                    <div id="cinza" visible="false" style="position: absolute; top: 0px; left: 0px; width: 100%;
                        height: 200%; z-index: 100; min-height: 100%; background-color: #999;">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; top: 200px; left: 30%;
                        width: 480px; z-index: 102; background-color: #FFFFFF; border-right: #336699 2px solid;
                        padding-right: 10px; border-top: #336699 2px solid; padding-left: 10px; padding-bottom: 10px;
                        border-left: #336699 2px solid; color: #000000; padding-top: 10px; border-bottom: #336699 2px solid;
                        text-align: justify; font-family: Verdana;">
                        <p style="height: 10px;">
                            <span style="margin-left: 400px;">
                                <asp:LinkButton ID="btnFechar" runat="server" CausesValidation="false" Text="<img src='img/fechar.png'>"
                                    Font-Size="X-Small" OnClick="btnFechar_Click">
                                </asp:LinkButton>
                            </span>
                        </p>
                        <p>
                            &nbsp</p>
                        <p>
                            <asp:Label ID="lblConfirmacao" runat="server"></asp:Label></p>
                        <p>
                            <span class="rotulo">Justificativa</span>
                            <asp:TextBox ID="tbxJustificativaCotaPPI" runat="server" TextMode="MultiLine" CssClass="campo"
                                Width="400px" Height="47px"></asp:TextBox>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="tbxJustificativaCotaPPI"
                                runat="server" ErrorMessage="Informe a justificativa!" Font-Size="XX-Small"></asp:RequiredFieldValidator>
                        </p>
                        <span style="margin-left: 90px;">
                            <asp:Button ID="btnSim" runat="server" Text="SIM" Width="50px" OnClick="btnSim_Click"
                                CausesValidation="true" />
                        </span><span style="margin-left: 15px;">
                            <asp:Button ID="btnNao" runat="server" Text="NÃO" Width="50px" CausesValidation="false"
                                OnClick="btnNao_Click" />
                        </span>
                    </div>
                </asp:Panel>--%>
                    <asp:Panel runat="server" ID="PanelAutorizacao" Visible="true">
                        <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                            <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Laudo">
                                <ContentTemplate>
                                    <fieldset class="formulario">
                                        <legend>Laudos da Solicitação </legend>
                                        <br />
                                        <p>
                                            <cc2:BlobImage ID="BlobImage1" runat="server" Width="100%" Height="58%" ThumbnailDisplay-KeepAspectRatio="true">
                                            </cc2:BlobImage>
                                            <asp:Image ID="Image1" runat="server" Visible="true" Width="100%" Height="58%" />
                                        </p>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="lknAnterior" runat="server" CausesValidation="False" OnClick="btnAnterior_Click">
                  	<img id="imganterior" alt="Salvar" src="img/btn-anterior1.png"
                  	onmouseover="imganterior.src='img/btn-anterior2.png';"
                  	onmouseout="imganterior.src='img/btn-anterior1.png';" /></asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="lknProximo" runat="server" CausesValidation="False" OnClick="btnProximo_Click">
			<img id="imgvoltar" alt="Voltar" src="img/btn-proximo1.png"
                  	onmouseover="imgvoltar.src='img/btn-proximo2.png';"
                  	onmouseout="imgvoltar.src='img/btn-proximo1.png';" />	
                                            </asp:LinkButton>
                                        </div>
                                        
                                    </fieldset>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Dados da Solicitação">
                                <ContentTemplate>
                                    <fieldset class="formulario">
                                        <legend>Dados da Solicitação </legend>
                                        <p>
                                            <span class="rotulo">Nome do Paciente:</span> <span>
                                                <asp:Label ID="lblNomePaciente" runat="server" Font-Bold="True"></asp:Label>
                                            </span>
                                        </p>
                                        <br />
                                        <p>
                                            <span class="rotulo">Cartão SUS:</span> <span>
                                                <asp:Label ID="lblCNS" runat="server" Font-Bold="True"></asp:Label>
                                            </span>
                                        </p>
                                        <br />
                                        <p>
                                            <span class="rotulo">Procedimento:</span> <span>
                                                <asp:Label ID="lblProcedimento" runat="server" Font-Bold="True"></asp:Label>
                                            </span>
                                        </p>
                                        <br />
                                        <p>
                                            <span class="rotulo">Data da Solicitação:</span> <span>
                                                <asp:Label ID="lblDataSolicitacao" runat="server" Font-Bold="True"></asp:Label>
                                            </span><span class="rotulomin">Município:</span> <span>
                                                <asp:Label ID="lblMunicipio" runat="server" Font-Bold="True"></asp:Label>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Prioridade:</span><br />
                                            <br />
                                            <span>
                                                <asp:RadioButtonList ID="rbtPrioridade" runat="server" TextAlign="Right" CellPadding="0"
                                                    CellSpacing="0" RepeatDirection="Vertical" RepeatColumns="0">
                                                    <asp:ListItem Value="0"><img id="vermelho" src="img/leg-verm.png" alt="Vermelhor" /></asp:ListItem>
                                                    <asp:ListItem Value="1"><img id="amarelo" src="img/leg-amar.png" alt="Amarelo" /></asp:ListItem>
                                                    <asp:ListItem Value="2"><img id="verde" src="img/leg-verd.png" alt="Verde" /></asp:ListItem>
                                                    <asp:ListItem Value="3"><img id="azul" src="img/leg-azul.png" alt="Azul" /></asp:ListItem>
                                                    <asp:ListItem Value="4"><img id="branco" src="img/leg-bran.png" alt="Branco" /></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Situação:</span> <span>
                                                <asp:RadioButtonList ID="rbtSituacao" runat="server" TextAlign="Right" CellPadding="0"
                                                    CellSpacing="0" CssClass="camporadio" Height="16px" Width="164px">
                                                    <asp:ListItem Value="1">Pendente</asp:ListItem>
                                                    <asp:ListItem Value="2">Autorizado</asp:ListItem>
                                                    <asp:ListItem Value="3" Enabled="false">Ag Automático</asp:ListItem>
                                                    <asp:ListItem Value="4">Indeferido</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Observação:</span>
                                        </p>
                                        <p>
                                            <asp:Label ID="lblObservacao" runat="server" Text="-" Font-Bold="True"></asp:Label>
                                        </p>
                                        <p>
                                            <span class="rotulo">Justificativa:</span>
                                        </p>
                                        <p>
                                            <asp:TextBox ID="tbxJustificativa" CssClass="campo" runat="server" Width="400px"
                                                Height="47px"></asp:TextBox>
                                        </p>
                                    </fieldset>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel_Tres" runat="server" HeaderText="Especialidade">
                                <ContentTemplate>
                                    <fieldset class="formulario">
                                        <legend>Especialidade</legend>
                                        <p style="height: auto">
                                            <asp:Label ID="lblSemEspecialidade" runat="server" Text="Não Existem Especialidades"></asp:Label>
                                            <asp:RadioButtonList ID="rbtnEspecialidade" runat="server" AutoPostBack="True" RepeatColumns="5"
                                                ForeColor="#8B0402" OnSelectedIndexChanged="rbtnEspecialidade_SelectedIndexChanged">
                                            </asp:RadioButtonList>
                                        </p>
                                    </fieldset>
                                    <br />
                                    <br />
                                    <fieldset class=" formulario3">
                                        <legend>Sub-Grupo (Opcional)</legend>
                                        <br />
                                        <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="drop" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlSubGrupo_SelectedIndexChanged">
                                            <asp:ListItem Text="Selecione..." Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%--<p>
                                        <asp:RadioButtonList ID="rbtnSubGrupo" runat="server" CssClass="camporadio_list" AutoPostBack="true" RepeatColumns="2" ForeColor="#8B0402"
                                            OnSelectedIndexChanged="rbtnSubGrupo_SelectedIndexChanged"></asp:RadioButtonList>
                                    </p>--%>
                                    </fieldset>
                                </ContentTemplate>
                            </cc1:TabPanel>
                            <cc1:TabPanel ID="TabPanel_Quatro" runat="server" HeaderText="Agenda">
                                <ContentTemplate>
                                    <asp:Panel ID="PanelExibeAgenda" runat="server">
                                        <fieldset class="formulario" style="width: 95%">
                                            <legend>Disponibilidade </legend>
                                            <asp:GridView ID="GridViewAgenda" runat="server" AutoGenerateColumns="False" OnRowCommand="GridViewAgenda_OnRowCommand"
                                                BackColor="White" BorderColor="#477ba5" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Vertical" Width="100%" OnPageIndexChanging="GridViewAgenda_PageIndexChanging"
                                                PageSize="10" AllowPaging="true">
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" HorizontalAlign="Center" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Agendar">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSelecionar" runat="server" CommandName="Selecionar" CommandArgument='<%# Eval("Codigo") %>'>
                                                                <asp:Image ID="imgSelecionar" AlternateText="Selecionar" ImageUrl="~/Agendamento/img/agendar.png"
                                                                    runat="server" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Unidade" DataField="Estabelecimento" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Profissional" DataField="NomeProfissional" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Bairro" DataField="Bairro" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Procedimento" DataField="Procedimento" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Data" DataField="DataAgendaFormatada" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Turno" DataField="Turno" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Qtd" DataField="QuantidadeDisponivel" ItemStyle-Font-Size="10px" />
                                                    <asp:BoundField HeaderText="Horário" DataField="Horario" ItemStyle-Font-Size="10px"
                                                        ItemStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="Dia" DataField="Dia" ItemStyle-Font-Size="10px" ItemStyle-Width="70px" />
                                                    <asp:BoundField DataField="Codigo" HeaderText="Codigo">
                                                        <HeaderStyle CssClass="colunaEscondida" />
                                                        <ItemStyle CssClass="colunaEscondida" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <asp:Label ID="lblMensagem" runat="server" Font-Bold="True" Font-Names="Comic Sans MS"
                                                        Font-Size="Medium" ForeColor="Red" Text="Não existe agenda para esse procedimento e especialidade!"></asp:Label>
                                                </EmptyDataTemplate>
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                                    Font-Size="11px" Height="22px" />
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                            </asp:GridView>
                                            <p>
                                            </p>
                                        </fieldset>
                                    </asp:Panel>
                                </ContentTemplate>
                            </cc1:TabPanel>
                        </cc1:TabContainer>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknSalvar" runat="server" CausesValidation="False" OnClick="btnSalvar_Click">
                  	<img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                  	onmouseover="imgsalvar.src='img/salvar_2.png';"
                  	onmouseout="imgsalvar.src='img/salvar_1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknVoltar" runat="server" OnClick="btnVoltar_Click" CausesValidation="False">
			<img id="imgvoltar1" alt="Voltar" src="img/voltar_1.png"
                  	onmouseover="imgvoltar1.src='img/voltar_2.png';"
                  	onmouseout="imgvoltar1.src='img/voltar_1.png';" />	
                            </asp:LinkButton>
                        </div>
                    </asp:Panel>
                    <%--                <p>
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" Style="margin-left: 82px"
                        CausesValidation="False" OnClick="btnSalvar_Click" />
                    <asp:Button ID="btnVoltar" runat="server" Text="Voltar" OnClick="btnVoltar_Click"
                        Style="margin-left: 26px" CausesValidation="False" />
                </p>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
