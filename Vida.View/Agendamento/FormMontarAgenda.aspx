<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormMontarAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.FormMontarAgenda"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <script type="text/javascript">
        function ValidaQuantidade(obj)
        {
            if(document.getElementById(obj).value == "" || parseInt(document.getElementById(obj).value) > 60)
            {
                alert('Quantidade Inválida!');
                return false;
            }
            else
                return true;
            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
<%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js"></script>

    <link media="screen" type="text/css" rel="stylesheet" href="../colorbox.css" />

    <script type="text/javascript" defer="defer" src="../JavaScript/jquery.colorbox.js"></script>--%>

    <div id="top">
        <h2>
            Agenda</h2>
        <fieldset class="formulario">
            <legend>Montar Agenda</legend>
            <p>
                <span class="rotulo">Competência (AAAAMM):</span> <span>
                    <asp:TextBox ID="tbxCompetencia" runat="server" CssClass="campo" AutoPostBack="True"
                        MaxLength="7" Width="50px" OnTextChanged="tbxCompetencia_TextChanged"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="999999" MaskType="Number"
                        TargetControlID="tbxCompetencia" ClearMaskOnLostFocus="true" AutoComplete="false">
                    </cc1:MaskedEditExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small"
                        runat="server" ErrorMessage="Formato Invalido (AAAA/MM)" ControlToValidate="tbxCompetencia"
                        Display="Dynamic" ValidationExpression="[0-9][0-9][0-9][0-9][0-9][0-9]" ValidationGroup="Validacao"></asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                        ControlToValidate="tbxCompetencia" Display="Dynamic" ErrorMessage="Preencha o campo Competência" ValidationGroup="Validacao"></asp:RequiredFieldValidator></span>
            </p>
            <asp:UpdatePanel runat="server" ID="UpdatePanelProcedimento" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="tbxCompetencia" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbkPublicar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Procedimento:</span> <span>
                            <asp:DropDownList ID="ddlProcedimento" CssClass="drop" runat="server" Width="507px"
                                DataTextField="Nome" DataValueField="Codigo" AutoPostBack="True" OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlProcedimento"
                                Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small" ValidationGroup="Validacao"></asp:RequiredFieldValidator></span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="UpdatePanelEspecialidade" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlProcedimento" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbkPublicar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Especialidade:</span> <span>
                            <asp:DropDownList ID="ddlCBO" CssClass="drop" runat="server" Width="507px" DataTextField="Nome"
                                DataValueField="Codigo" AutoPostBack="true" OnSelectedIndexChanged="ddlCBO_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCBO"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small" InitialValue="0" ValidationGroup="Validacao"></asp:RequiredFieldValidator>
                            </span></span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCBO" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbkPublicar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Sub-Grupo:</span> <span>
                            <asp:DropDownList ID="ddlSubGrupo" CssClass="drop" runat="server" Width="507px">
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel runat="server" ID="UpdatePanelProfissional" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCBO" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="btnSalvar" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="lbkPublicar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <p>
                            <span class="rotulo">Profissional:</span> <span>
                                <asp:DropDownList ID="ddlProfissional" CssClass="drop" runat="server" Width="507px"
                                    DataTextField="Nome" DataValueField="Codigo">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlProfissional"
                                    Display="Dynamic" ErrorMessage="Campo Obrigatório" InitialValue="0" Font-Size="XX-Small" ValidationGroup="Validacao"></asp:RequiredFieldValidator></span>
                        </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Turno:</span> <span>
                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="drop">
                        <asp:ListItem Text="Selecione" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Manhã" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Tarde" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Noite" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                        ControlToValidate="ddlTurno" Display="Dynamic" ErrorMessage="Campo Obrigatório"
                        SetFocusOnError="True" InitialValue="0" ValidationGroup="Validacao"></asp:RequiredFieldValidator>--%></span>
                <span>
                    <asp:Image ImageUrl="~/Agendamento/img/iimg-horario.png" ID="Image1" runat="server" /></span>
            </p>
            <p>
                <span class="rotulo">Hora Inicial - Final</span> <span>
                    <asp:TextBox ID="tbxHora_Inicial" CssClass="campo" runat="server" MaxLength="5" Width="35px"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="tbxHora_Inicial"
                        MaskType="Time" Mask="99:99" AutoComplete="false" InputDirection="LeftToRight"
                        ClearMaskOnLostFocus="false">
                    </cc1:MaskedEditExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="XX-Small"
                        Display="Dynamic" runat="server" ErrorMessage="Hora Inicial Inválida" ControlToValidate="tbxHora_Inicial"
                        ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">
                    </asp:RegularExpressionValidator>
                </span>a &nbsp &nbsp <span>
                    <asp:TextBox ID="tbxHora_Final" CssClass="campo" runat="server" MaxLength="5" Width="35px"></asp:TextBox>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="tbxHora_Final"
                        Mask="99:99" MaskType="Time" InputDirection="LeftToRight" AutoComplete="false"
                        ClearMaskOnLostFocus="false">
                    </cc1:MaskedEditExtender>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Font-Size="XX-Small"
                        Display="Dynamic" runat="server" ErrorMessage="Hora Final Inválida" ControlToValidate="tbxHora_Final"
                        ValidationExpression="^([0-1][0-9]|[2][0-3]):[0-5][0-9]$">
                    </asp:RegularExpressionValidator>
                </span>
            </p>
            <p>
                <span class="rotulo">Quantidade Vagas:</span> <span>
                    <asp:TextBox ID="tbxQuantidade" CssClass="campo" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="tbxQuantidade"
                        Display="Dynamic" ErrorMessage="Campo Obrigatório" Font-Size="XX-Small">
                    </asp:RequiredFieldValidator>
                </span>
            </p>
            <br />
            <asp:UpdatePanel runat="server" ID="UpdatePanelCalendar" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Calendar1" EventName="SelectionChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
                            BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest"
                            Font-Names="Verdana" Font-Size="8pt" ForeColor="#477BA5" Height="200px" Width="220px">
                            <SelectedDayStyle BackColor="#65A0CD" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <WeekendDayStyle BackColor="#C5D9F1" />
                            <TodayDayStyle BackColor="#65A0CD" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <DayHeaderStyle BackColor="#65A0CD" ForeColor="#EBF0F5" Height="1px" />
                            <TitleStyle BackColor="#477ba5" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True"
                                Font-Size="10pt" ForeColor="#FFFFFF" Height="25px" />
                        </asp:Calendar>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
        </fieldset>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnpesquisar" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="lbkPublicar" EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="PanelAgendas" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Agendas</legend>
                        <p align="left">
                            <asp:GridView ID="gridAgenda" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gridAgenda_OnPageIndexChanging"
                                OnRowDataBound="gridAgenda_DataBound" BackColor="White" BorderColor="#477BA5"
                                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataKeyNames="Codigo"
                                OnRowCommand="gridAgenda_RowCommand" OnRowCancelingEdit="gridAgenda_RowCancelingEdit"
                                OnRowEditing="gridAgenda_RowEditing" OnRowUpdating="gridAgenda_RowUpdating">
                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" Font-Size="11px" HorizontalAlign="Center" />
                                <Columns>
                                    <asp:BoundField DataField="Codigo">
                                        <HeaderStyle CssClass="colunaEscondida" />
                                        <ItemStyle CssClass="colunaEscondida" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Competencia" HeaderText="Comp" ReadOnly="true">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Data">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbxData" runat="server" Enabled="false" CssClass="campo" Width="70px"
                                                Text='<%#bind("DataAgendaFormatada") %>' AutoPostBack="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbxData" runat="server" CssClass="campo" Width="70px" Text='<%#bind("DataAgendaFormatada") %>'></asp:TextBox>
                                            <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                                                TargetControlID="tbxData" Mask="99/99/9999" MaskType="Date">
                                            </cc1:MaskedEditExtender>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="ID_Profissional" HeaderText="Profissional" ReadOnly="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="SubGrupo" HeaderText="SubGrupo" ReadOnly="true">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="250px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TurnoToString" HeaderText="Turno" ReadOnly="true">
                                        <ItemStyle Width="60px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Quantidade">
                                        <ItemTemplate>
                                            <asp:TextBox ID="tbxQuantidade" runat="server" Enabled="false" CssClass="campo" Width="70px"
                                                Text='<%#bind("Quantidade") %>' AutoPostBack="false"></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="tbxQuantidade" runat="server" CssClass="campo" Width="70px" Text='<%#bind("Quantidade") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StatusToString" HeaderText="STATUS" ReadOnly="true">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="BloqueadaToString" HeaderText="Bloqueada" ReadOnly="true">
                                        <ItemStyle Width="90px" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Editar" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton_ConfirmarExcecucao" runat="server" CommandName="Edit"
                                                CausesValidation="false">
                                                <asp:Image ID="imgEdit" ImageUrl="~/Agendamento/img/bt_edit.png" runat="server" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton_ProsseguirConfirmacao" runat="server" CommandName="Update"
                                                CausesValidation="false">
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
                                    <asp:TemplateField HeaderText="Excluir" ItemStyle-Width="60px" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="cmdDelete" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                CommandName="Excluir" OnClientClick="javascript : return confirm('Tem certeza que deseja excluir esse dia da Agenda?');"
                                                Text="" CausesValidation="false">
                                                <asp:Image ID="Excluir" runat="server" ImageUrl="~/Agendamento/img/excluirr.png" />
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Nenhum Registro Encontrado!" Font-Size="X-Small" ForeColor="Red"></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#477ba5" Font-Bold="True" ForeColor="White" Font-Names="Verdana"
                                    Font-Size="11px" Height="22px" />
                                <AlternatingRowStyle BackColor="#DCDCDC" />
                            </asp:GridView>
                            <br />
                            <div class="botoesroll">
                                <asp:LinkButton ID="lbkPublicar" runat="server" CausesValidation="false" OnClick="lbkPublicar_Click"
                                    Visible="False">
                         <img id="imgpublic" alt="Salvar" src="img/btn-public1.png"
                      onmouseover="imgpublic.src='img/btn-public2.png';"
                      onmouseout="imgpublic.src='img/btn-public1.png';" />   
                                </asp:LinkButton>
                                </div>
                        </p>
                    </fieldset>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="botoesroll">
            <asp:LinkButton ID="btnSalvar" runat="server" OnClick="Salvar_Click" CausesValidation="true" ValidationGroup="Validacao">
                <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                onmouseover="imgsalvar.src='img/salvar_2.png';"
                onmouseout="imgsalvar.src='img/salvar_1.png';" />
            </asp:LinkButton></div>
        <div class="botoesroll">
            <asp:LinkButton ID="btnpesquisar" runat="server" OnClick="btnPesquisar_Click" ValidationGroup="Validacao">
                <img id="img_pesquisar" alt="" src="img/pesquisar_1.png"
                onmouseover="img_pesquisar.src='img/pesquisar_2.png';"
                onmouseout="img_pesquisar.src='img/pesquisar_1.png';" />
            </asp:LinkButton></div>
        <div class="botoesroll">
            <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                CausesValidation="false">
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
            </asp:LinkButton></div>
    </div>
    <%-- <div style='display: none'>
                    <div id='inline_content' style="padding: 14px; background: #fff; font-family: Arial;
                        font-size: 12px !important; text-align: justify;" class="inline">
                        <p style="font-size: 13px; margin-bottom: 15px;">
                            “A Secretaria Municipal da Saúde lança chamamento público para credenciar prestadores
                            de serviços em diversas especialidades.<br /><br /> Os editais estão disponibilizados no site:
                           <b>www.saude.salvador.ba.gov.br.”</b> 
                        </p>
                    </div>
                </div>--%>
    <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
        <ContentTemplate>
            <asp:Panel runat="server" ID="PanelMensagem" Visible="false">
                <div id="cinza" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;
                    z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                    moz-opacity: 0.3; opacity: 0.3" visible="false">
                </div>
                <div id="mensagem" style="position: fixed; top: 50%; margin-top: -200px; left: 50%;
                    margin-left: -300px; width: 600px; z-index: 102; background-color: #0d2639; border: #ffffff 5px solid;
                    padding-right: 20px; padding-left: 20px; padding-bottom: 20px; color: #c5d4df;
                    padding-top: 10px; text-align: justify; font-family: Verdana;" visible="false">
                    <p style="height: 10px;">
                        <span style="float: right">
                            <asp:LinkButton ID="btnFechar" runat="server" Height="39px" Width="100%" OnClick="OnClick_btnFechar"
                                CausesValidation="false">
                                <img src="img/fechar-agendamento.png" id="imgInforme" alt=""/>
                            </asp:LinkButton>
                        </span>
                    </p>
                    <p style="font-size: 18px; font-family: Arial; font-weight: bold; color: #fff;">
                        <b>ATENÇÃO</b></p>
                    <p>
                    </p>
                    <p style="font-size: 12px; font-family: Arial; color: #fff; margin-top: 25px;">
                        <br />
                        <p>A quantidade de vagas publicadas excede a FPO.<br />
                            FPO: <asp:Label ID="lblQtdFPO" runat="server"></asp:Label>.<br />
                            Quantidade excedida: <asp:Label ID="lblQtdExcedida" runat="server"></asp:Label>
                            <p>
                                Informamos que só deverá ser autorizada a quantidade máxima do Teto Orçamentário apresentado na FPO da competência vigente.
                                Deseja publicar?
                            </p>
                        </p>
                        <div class="botoesroll">
                         <asp:LinkButton ID="btnSim" runat="server"  
                            CausesValidation="false" Font-Size="X-Small" onclick="btnSim_Click">
                         <img src="img/btn-sim.png" alt="Sim" />
                         </asp:LinkButton>
                         </div><div class="botoesroll">
                         <asp:LinkButton ID="btnNao" runat="server" Text="NÃO" 
                            CausesValidation="false" Font-Size="X-Small" onclick="btnNao_Click">
                         <img src="img/btn-nao.png" alt="Não" />
                         </asp:LinkButton>
                      </div>
                    </p>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
           <%-- <script defer="defer" type="text/javascript">
                    
                jQuery.noConflict();
  
			    setTimeout(function(){
                jQuery.colorbox({ href:"#inline_content", inline:true,  width:"40%" });
              //  jQuery(".inline").colorbox({inline:true, href:"#inline_content"});
                },20);
         
    </script>--%>
</asp:Content>
