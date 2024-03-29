﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Inc_AtestadoReceita.ascx.cs"
    Inherits="ViverMais.View.Urgencia.Inc_AtestadoReceita" %>
<fieldset class="formulario2">
    <legend>Consulta Médica</legend>
    <p>
        <span>
            <asp:GridView ID="GridView_AtestadoReceitaConsultaMedica" runat="server" AutoGenerateColumns="false"
                DataKeyNames="Codigo" Width="690px">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" Width="154px" Height="38px" ImageUrl="~/Urgencia/img/bts/btn-emitiratestado.png"
                                CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarAtestadoEvolucao" style="margin-top:7px;" />
                            <%--<asp:LinkButton ID="LinkButton_Atestado" runat="server"
                             CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarAtestadoConsulta">Atestado</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Urgencia/img/bts/btn-emitirreceita.png"
                                Width="154px" Height="38px" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarReceitaEvolucao" style="margin-top:7px;" />
                            <%--<asp:LinkButton ID="LinkButton_Receita" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarReceitaConsulta">Receita</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" Width="210px" Height="38px" CommandArgument='<%#bind("Codigo") %>'
                                OnClick="OnClick_GerarComparecimentoEvolucao" ImageUrl="~/Urgencia/img/bts/bt_gerarcomparecimento1.png" style="margin-top:7px;"  />
                            <%--<asp:LinkButton ID="LinkButton_Comparecimento" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarComparecimentoConsulta">Comparecimento</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
            </asp:GridView>
        </span>
    </p>
</fieldset>
<fieldset class="formulario2">
    <legend>Evoluções Médicas</legend>
    <p>
        <span>
            <asp:GridView ID="GridView_AtestadoReceitaEvolucaoMedica" runat="server" Width="690px"
                AutoGenerateColumns="false" DataKeyNames="Codigo" AllowPaging="true" PageSize="20"
                PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_PaginacaoAtestadoReceitaEvolucaoesMedica">
                <Columns>
                    <asp:BoundField DataField="Data" HeaderText="Data" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Urgencia/img/bts/btn-emitiratestado.png"
                                Width="154px" Height="38px" style="margin-top:7px;" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarAtestadoEvolucao" />
                            <%--<asp:LinkButton ID="LinkButton_Atestado" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarAtestadoEvolucao">Atestado</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Urgencia/img/bts/btn-emitirreceita.png"
                                Width="154px" Height="38px" style="margin-top:7px;" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarReceitaEvolucao" />
                            <%--<asp:LinkButton ID="LinkButton_Receita" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarReceitaEvolucao">Receita</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" Width="210px" Height="38px" CommandArgument='<%#bind("Codigo") %>'
                                OnClick="OnClick_GerarComparecimentoEvolucao" ImageUrl="~/Urgencia/img/bts/bt_gerarcomparecimento1.png" style="margin-top:7px;" />
                            <%--<asp:LinkButton ID="LinkButton_Comparecimento" runat="server" CommandArgument='<%#bind("Codigo") %>' OnClick="OnClick_GerarComparecimentoEvolucao">Comparecimento</asp:LinkButton>--%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>
                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle CssClass="tab" />
                <RowStyle CssClass="tabrow" />
            </asp:GridView>
        </span>
    </p>
</fieldset>
