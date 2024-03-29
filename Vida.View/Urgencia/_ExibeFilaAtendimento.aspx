﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="_ExibeFilaAtendimento.aspx.cs"
    Inherits="ViverMais.View.Urgencia._ExibeFilaAtendimento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="style_form_urgencia.css" type="text/css" media="screen" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="Scritpt1" runat="server" ScriptMode="Debug">
    </asp:ScriptManager>
    <%--<asp:LinkButton ID="linkteste" runat="server" OnClientClick="parent.location='FormProntuario.aspx';">Teste</asp:LinkButton>--%>
    <div id="top">
        <asp:Timer ID="Timer_Temporizador" runat="server" Interval="10000" OnTick="OnTick_Temporizador">
        </asp:Timer>
        <h2>
            <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="Atendimento"></asp:Label></h2>
        <fieldset class="formulario" style="width: 850px;">
            <legend>Relação</legend>
            <p>
                <span>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Timer_Temporizador" EventName="Tick" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:GridView ID="gridFila" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound_FormataGrid"
                                OnSelectedIndexChanging="OnSelectedIndexChanging_PrimeiraConsultaMedica" Width="100%"
                                DataKeyNames="CodigoProntuario">
                                <Columns>
                                    <asp:BoundField DataField="NumeroProntuario" HeaderText="Identificador" ItemStyle-HorizontalAlign="Center" />
                                    <asp:ButtonField DataTextField="NomePaciente" ItemStyle-Width="280px" ButtonType="Link"
                                        CommandName="Select" HeaderText="Paciente" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="PacienteDescricao" HeaderText="Descrição" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="400px" />
                                    <asp:ImageField HeaderText="Classificação de Risco" DataImageUrlFormatString="~/Urgencia/img/{0}"
                                        HeaderStyle-HorizontalAlign="Center" DataImageUrlField="ImagemClassificacaoRisco"
                                        ItemStyle-HorizontalAlign="Center">
                                    </asp:ImageField>
                                    <asp:TemplateField HeaderText="Espera" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label_TempoEspera" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow_left" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </span>
            </p>
        </fieldset>
    </div>
    </form>
</body>
</html>
