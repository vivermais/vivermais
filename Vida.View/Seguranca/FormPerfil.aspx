<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormPerfil.aspx.cs" Inherits="ViverMais.View.Seguranca.FormPerfil"
    MasterPageFile="~/Seguranca/MasterSeguranca.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder2" ID="c_head" runat="server">
    <style type="text/css">
        .accordionHeader2
        {
            border: 1px solid #142126;
            color: #142126;
            background-color: #eee; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 98%;
            text-align: center;
        }
        .accordionHeaderSelected2
        {
            border: 1px solid #142126;
            color: white;
            background-color: #142126; /*font-weight: bold;*/
            padding: 5px;
            margin-top: 5px;
            cursor: pointer;
            width: 98%;
            text-align: center;
        }
        .accordionContent2
        {
            background-color: #fff;
            border: 1px solid #142126;
            border-top: none;
            padding: 5px;
            padding-top: 10px;
            width: 98%;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPlaceHolder3" ID="c_body" runat="server">
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_Salvar1" />
        </Triggers>
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formulário para Cadastro de Perfil
        </h2>
        <fieldset class="formulario">
            <legend>Informações</legend>
            <cc1:TabContainer ID="TabContainer_Perfil" runat="server">
                <cc1:TabPanel ID="Perfis" runat="server" HeaderText="Geral">
                    <ContentTemplate>
                        <p>
                            <span class="rotulo">Módulo</span> <span>
                                <asp:DropDownList ID="DropDownList_Sistema" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CarregaOperacoes"
                                    runat="server" DataTextField="Nome" DataValueField="Codigo" Width="311px">
                                    <%--<asp:ListItem Selected="True" Text="Selecione..." Value="0"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:TextBox ID="TextBox_Nome" runat="server" CssClass="campo" Width="300px"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <asp:CheckBox ID="ckbxProfissionalSaude" runat="server" Text="Este perfil é para profissionais de Saúde"/>
                        </p>
                        <p>
                            <cc1:Accordion ID="Accordion_Operacoes" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                                HeaderCssClass="accordionHeader2" HeaderSelectedCssClass="accordionHeaderSelected2"
                                ContentCssClass="accordionContent2">
                                <Panes>
                                    <cc1:AccordionPane ID="AccordionPane_Operacoes" runat="server">
                                        <Header>
                                            <%--<p align="center">--%>
                                            <asp:Label ID="TextoCabecalho" runat="server" Text="Operações" Font-Bold="true" Font-Size="Medium"></asp:Label>
                                            <%-- </p>--%>
                                        </Header>
                                        <Content>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Sistema" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                                <ContentTemplate>
                                                    <div style="margin-left:4px;">
                                                    <asp:Panel ID="SelecionarTodasOperacoes" runat="server" Visible="false">
                                                        <asp:CheckBox ID="CheckBox_SelecionarTodos" runat="server" AutoPostBack="true"
                                                         TextAlign="Right" CssClass="check_grid" Font-Size="13px" Font-Underline="true"
                                                        OnCheckedChanged="OnCheckedChanged_Operacoes" Text="SELECIONAR TODAS?" /> 
                                                    </asp:Panel>
                                                           </div>
                                                        <br />
                                                    <asp:CheckBoxList ID="CheckBoxList_Operacoes" runat="server" RepeatDirection="Horizontal"
                                                        RepeatColumns="2" DataTextField="Nome" DataValueField="Codigo" Width="100%" CssClass="check_grid"
                                                        CellPadding="2" RepeatLayout="Table" CellSpacing="2" TextAlign="Right">
                                                    </asp:CheckBoxList>
                                                    <asp:Panel ID="EmptyOperacoes" runat="server" Visible="false">
                                                        <p align="center">
                                                            <asp:Label ID="LabelEmpty" runat="server" Font-Size="15px" Font-Bold="true" ForeColor="Black"
                                                                Text="Nenhuma registro encontrado para perfil selecionado."></asp:Label>
                                                        </p>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </Content>
                                    </cc1:AccordionPane>
                                </Panes>
                            </cc1:Accordion>
                        </p>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é Obrigatório!"
                                ControlToValidate="TextBox_Nome" Display="None" ValidationGroup="ValidationGroup_cadPerfil"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Há caracteres inválidos no Nome do Perfil"
                                ControlToValidate="TextBox_Nome" Display="None" ValidationExpression="^[0-9a-zA-Z\s]{1,30}$"
                                ValidationGroup="ValidationGroup_cadPerfil"></asp:RegularExpressionValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Módulo é Obrigatório!"
                                ControlToValidate="DropDownList_Sistema" ValueToCompare="0" Operator="GreaterThan"
                                Display="None" ValidationGroup="ValidationGroup_cadPerfil"></asp:CompareValidator>
                        </p>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="Horarios" runat="server" HeaderText="Horários">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <table width="335">
                                    <tr>
                                        <th width="110" height="29" bgcolor="#333333" style="color:#fff; font-size:11px"><strong>
                                         <span style="font-size:10px;"> Dia</span>  
                                      </strong></th>
                                        <th width="78" bgcolor="#333333" style="color:#fff; font-size:11px"><strong>
                                          Hora Inicial
                                    </strong></th>
                                        <th width="68" bgcolor="#333333" style="color:#fff; font-size:11px; font-size:10px"><strong>
                                          Hora Final
                                    </strong></th>
                                        <th width="66" bgcolor="#333333" style="color:#fff; font-size:11px"><strong>
                                          Bloquear Dia?
                                      </strong></th>
    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_1" runat="server" Text="" Font-Bold="true"  Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_1" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_1" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_1" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_2" runat="server" Text=""  Font-Bold="true"  Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_2" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_2" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_2" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_3" runat="server" Text=""  Font-Bold="true"  Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_3" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_3" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_3" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_4" runat="server" Text=""  Font-Bold="true"  Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_4" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_4" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_4" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_5" runat="server" Text=""  Font-Bold="true" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_5" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_5" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_5" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_6" runat="server" Text=""  Font-Bold="true" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_6" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_6" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_6" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
                                    <tr bgcolor="#f2f2f2">
                                        <td bgcolor="#E4E4E4">
                                            <asp:Label ID="Label_Dia_0" runat="server" Text=""  Font-Bold="true" Font-Size="12px"></asp:Label>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraInicial_0" runat="server" CssClass="drop"
                                                AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_HorarioFinal">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:DropDownList ID="DropDownList_HoraFinal_0" runat="server" CssClass="drop">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="center">
                                            <asp:CheckBox ID="CheckBox_Bloqueado_0" runat="server" AutoPostBack="true" OnCheckedChanged="OnCheckedChanged_Bloqueado" />
                                        </td>
                                    </tr>
</table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
            <br />
            <div class="botoesroll">
                <asp:LinkButton ID="Button_Salvar1" runat="server" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_cadPerfil">
      <img id="imgsalvar" alt="Salvar" src="img/btn_salvar_1.png"
      onmouseover="imgsalvar.src='img/btn_salvar_2.png';"
      onmouseout="imgsalvar.src='img/btn_salvar_1.png';" /></asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="LinkButtonVoltar" runat="server">
                            <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                            onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                onmouseout="imgvoltar.src='img/btn_voltar1.png';"  />
                </asp:LinkButton>
            </div>
            <p>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                    ShowSummary="false" ValidationGroup="ValidationGroup_cadPerfil" />
            </p>
        </fieldset>
    </div>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
