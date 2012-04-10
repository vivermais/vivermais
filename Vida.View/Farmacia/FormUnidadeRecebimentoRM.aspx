<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormUnidadeRecebimentoRM.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormUnidadeRecebimentoRM" MasterPageFile="~/Farmacia/MasterFarmacia.Master"
    EnableEventValidation="false" Title="Unidades Responsáveis pelo recebimento das RM's" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
        <%--<Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList_Unidade" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="GridView_UnidadeDistrito" EventName="RowDeleting" />
                </Triggers>--%>
        <ContentTemplate>
            <div id="top">
                <h2>
                    Unidades Responsáveis pelo recebimento das RM's</h2>
                <fieldset>
                    <legend>Vincular</legend>
                    <p>
                        <span class="rotulo">Distrito</span> <span>
                            <asp:DropDownList ID="DropDownList_Distrito" runat="server" DataTextField="Nome"
                                CssClass="campo" Height="27px" DataValueField="Codigo" OnSelectedIndexChanged="OnSelectedIndexChanged_DropDownList_Distrito"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <%-- <p>
                        <span class="rotulo">Unidade</span> <span>
                            <asp:DropDownList ID="DropDownList_Unidade"  CssClass="campo" Height="27px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_Distrito"
                                DataTextField="NomeFantasia" DataValueField="CNES">
                            </asp:DropDownList>
                        </span>
                    </p>--%>
                    <%--  <p>
                        <span>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_VincularUnidadeDistrito"
                                ValidationGroup="ValidationGroup_VincularUnidadeDistrito">
                                 <img id="imgvinc" alt="Vincular" src="img/btn/vincular1.png"
                onmouseover="imgvinc.src='img/btn/vincular1.png';"
                onmouseout="imgvinc.src='img/btn/vincular1.png';" />
                                </asp:LinkButton>
                                </div>
                                <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl="~/Farmacia/Default.aspx">
                             <img id="imgvoltar" alt="Voltar" src="img/btn/voltar1.png"
                onmouseover="imgvoltar.src='img/btn/voltar1.png';"
                onmouseout="imgvoltar.src='img/btn/voltar1.png';" />
                            </asp:LinkButton>
                            </div>
                        </span>
                    </p>--%>
                </fieldset>
                <asp:Panel ID="Panel_Resultado" runat="server" Visible="false">
                    <fieldset>
                        <legend>Unidades relacionadas</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_UnidadeDistrito" runat="server" AutoGenerateColumns="false"
                                    Font-Size="X-Small" Width="100%">
                                    <Columns>
                                        <asp:BoundField HeaderText="Unidade" DataField="NomeFantasia" />
                                        <%--<asp:BoundField HeaderText="Responsável pelo Distrito ?" DataField="NomeDistrito" />--%>
                                    </Columns>
                                    <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px"
                                        HorizontalAlign="Center" />
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset>
                </asp:Panel>
                <p>
                    <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma unidade."
                        ControlToValidate="DropDownList_Unidade" ValueToCompare="-1" Operator="GreaterThan"
                        Display="None" ValidationGroup="ValidationGroup_VincularUnidadeDistrito"></asp:CompareValidator>--%>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione um distrito."
                        ControlToValidate="DropDownList_Distrito" ValueToCompare="-1" Operator="GreaterThan"
                        Display="None" ValidationGroup="ValidationGroup_VincularUnidadeDistrito"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_VincularUnidadeDistrito" />
                </p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
