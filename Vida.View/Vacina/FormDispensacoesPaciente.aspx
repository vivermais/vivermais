<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormDispensacoesPaciente.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormDispensacoesPaciente" MasterPageFile="~/Vacina/MasterVacina.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>
<%@ Register Src="~/Paciente/WUCExibirPaciente.ascx" TagName="TagName_PesquisaPaciente"
    TagPrefix="TagPrefix_PesquisaPaciente" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Dispensações realizadas
            <%--<asp:Label ID="idl" runat="server" Text="Dispensações realizadas" Font-Bold="true"
         Font-Size="Larger"></asp:Label>
             <br /> <br />--%>
        </h2>
        <cc2:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="800px">
            <cc2:TabPanel ID="TabPanel1" runat="server" HeaderText="Informações do Paciente"
                ScrollBars="Horizontal">
                <ContentTemplate>
                    <TagPrefix_PesquisaPaciente:TagName_PesquisaPaciente ID="WUC_ExibirPaciente" runat="server" />
                </ContentTemplate>
            </cc2:TabPanel>
            <cc2:TabPanel ID="TabPanel2" runat="server" HeaderText="Dispensações" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset class="formulario">
                        <legend>Filtrar Dispensações</legend>
                        <p>
                            <span class="rotulo">Data</span> <span>
                                <asp:TextBox ID="TextBox_DataAtendimento" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="ValidationGroup_PesquisarDispensacao"
                                    OnClick="OnClick_FiltrarDispensacoes"><img id="imgpesquisar" alt="Pesquisar" src="img/btn_pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn_pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn_pesquisar1.png';" /></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_ListarTodos"><img id="imglistar" alt="Listar Todos" src="img/btn_listar_todos1.png"
                  onmouseover="imglistar.src='img/btn_listar_todos2.png';"
                  onmouseout="imglistar.src='img/btn_listar_todos1.png';" /></asp:LinkButton>
                            </span>
                        </p>
                        <%--            <p>
                <span>--%>
                        <%--<asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_ListarTodos">Listar Todos</asp:LinkButton>--%>
                        <%--                </span>
            </p>--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                            RenderMode="Inline">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="LinkButton5" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton6" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridView_Dispensacoes" runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="Codigo" Width="100%" OnRowCommand="OnRowCommand_Dispensacao" AllowPaging="true"
                                            PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Dispensacoes"
                                            BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                            <Columns>
                                                <asp:BoundField HeaderText="Sala" DataField="NomeSala" />
                                                <asp:BoundField HeaderText="Data" ItemStyle-Width="200px" DataField="Data" DataFormatString="{0:dd/MM/yyyy}" />
                                                <%--<asp:ButtonField ButtonType="Link" Text="Imprimir Recibo" CommandName="CommandName_ImprimirReciboDispensacao" />--%>
                                                <asp:ButtonField ButtonType="Link" Text="<img src='img/info.png' alt='Informações'/>"
                                                    CommandName="CommandName_VerInformacoes" />
                                            </Columns>
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" Font-Size="Smaller" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhuma dispensação encontrada na(s) sala(s) de vacina desta unidade.."></asp:Label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <p>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data é Obrigatório."
                                ValidationGroup="ValidationGroup_PesquisarDispensacao" Display="None" ControlToValidate="TextBox_DataAtendimento"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data com formato inválido."
                                ControlToValidate="TextBox_DataAtendimento" ValidationGroup="ValidationGroup_PesquisarDispensacao"
                                Display="None" Type="Date" Operator="DataTypeCheck" Font-Size="XX-Small"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data deve ser igual ou maior que 01/01/1900."
                                ControlToValidate="TextBox_DataAtendimento" ValidationGroup="ValidationGroup_PesquisarDispensacao"
                                Display="None" Type="Date" ValueToCompare="01/01/1900" Operator="GreaterThanEqual"
                                Font-Size="XX-Small"></asp:CompareValidator>
                            <cc2:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBox_DataAtendimento"
                                Format="dd/MM/yyyy">
                            </cc2:CalendarExtender>
                            <cc2:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TextBox_DataAtendimento"
                                Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight">
                            </cc2:MaskedEditExtender>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="ValidationGroup_PesquisarDispensacao"
                                ShowMessageBox="true" ShowSummary="false" />
                        </p>
                    </fieldset>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional"
                        RenderMode="Inline">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="LinkButton5" />
                            <asp:AsyncPostBackTrigger ControlID="GridView_Dispensacoes" EventName="RowCommand" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel_Dispensacao" runat="server" Visible="false">
                                <fieldset class="formulario">
                                    <legend>Informações da Dispensação</legend>
                                    <p>
                                        <span class="rotulo">Sala de Vacina</span> <span>
                                            <asp:Label ID="Label_SalaInfo" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Data</span> <span>
                                            <asp:Label ID="Label_DataInfo" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                        </span>
                                    </p>
                                    <p>
                                        <span class="rotulo">Grupo de Atendimento</span> <span>
                                            <asp:Label ID="Label_GrupoInfo" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                        </span>
                                    </p>
                                    <p>
                                        <br />
                                        <span>
                                            <asp:GridView ID="GridView_ItensDispensacao" runat="server" AutoGenerateColumns="false"
                                                OnRowDataBound="OnRowDataBound_ItensDispensacao" Width="100%" DataKeyNames="Codigo"
                                                BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Vacina" DataField="Vacina" ItemStyle-Width="200px" />
                                                    <asp:BoundField HeaderText="Lote" DataField="DescricaoLote" ItemStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="Fabricante" DataField="VacinaFabricante" ItemStyle-Width="100px" />
                                                    <asp:BoundField HeaderText="Validade" DataField="ValidadeLote" DataFormatString="{0:dd/MM/yyyy}"
                                                        ItemStyle-Width="70px" />
                                                    <asp:BoundField HeaderText="Dose" DataField="DescricaoDose" />
                                                    <asp:BoundField HeaderText="Abertura de Ampola" DataField="DescricaoAberturaAmpola"
                                                        ItemStyle-Width="100px" />
                                                    <asp:TemplateField HeaderText="Excluir">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox_Excluir" runat="server" CssClass="radio1" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                    Height="24px" Font-Size="13px" />
                                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                                <EmptyDataRowStyle HorizontalAlign="Center" Font-Size="Smaller" />
                                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            </asp:GridView>
                                        </span>
                                    </p>
                                    <p>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="LinkButton6" runat="server" OnClick="OnClick_ExcluirItens" OnClientClick="javascript:return confirm('Tem certeza que deseja excluir os itens selecionados?');">
                                            <img id="imgexcluiritens" alt="Excluir Itens" src="img/excluir-itens1.png"
                  onmouseover="imgexcluiritens.src='img/excluir-itens2.png';"
                  onmouseout="imgexcluiritens.src='img/excluir-itens1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="LinkButton4" runat="server" OnClick="OnClick_Cancelar">
                                <img id="imgcancelar" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn_cancelar1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                        <div class="botoesroll">
                                            <asp:LinkButton ID="LinkButton5" runat="server" OnClick="OnClick_ImprimirRecibo">
                                            <img id="imgimprimirrecibo" alt="Imprimir Recibo" src="img/imprimir-recibo1.png"
                  onmouseover="imgimprimirrecibo.src='img/imprimir-recibo2.png';"
                  onmouseout="imgimprimirrecibo.src='img/imprimir-recibo1.png';" />
                                            </asp:LinkButton>
                                        </div>
                                    </p>
                                </fieldset>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </ContentTemplate>
            </cc2:TabPanel>
        </cc2:TabContainer>
        <br />
        <asp:LinkButton ID="LinkButton3" runat="server" PostBackUrl="~/Vacina/FormPesquisaPaciente.aspx?tipo=pesquisardispensacao">
                    <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" />
        </asp:LinkButton>
    </div>
</asp:Content>
