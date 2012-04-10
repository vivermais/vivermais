<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormParametrizarVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormParametrizarVacina"
    Title="ViverMais - Parametrização de Vacinas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true">
        <ContentTemplate>
            <div id="top">
                <h2>
                    Parametrizar Vacina</h2>
                <fieldset class="formulario">
                    <legend>Critérios</legend>
                    <p>
                        <span class="rotulo">Vacina</span> <span>
                            <asp:DropDownList ID="DropDownList_Vacina" CssClass="drop" runat="server" AutoPostBack="True"
                                Width="270px" OnSelectedIndexChanged="OnSelectedIndexChanged_Vacina" DataTextField="Nome"
                                DataValueField="Codigo">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Dose</span> <span>
                            <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList_Vacina" EventName="SelectedIndexChanged" />
                        </Triggers>
                        <ContentTemplate>--%>
                            <asp:DropDownList ID="DropDownList_Dose" CssClass="drop" runat="server" AutoPostBack="True"
                                Width="270px" DataValueField="Codigo" DataTextField="NomeDose" OnSelectedIndexChanged="OnSelectedIndexChanged_Dose">
                                <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                            <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                        </span>
                    </p>
                    <cc1:TabContainer ID="TabContainer_Paramerizacao" runat="server" ScrollBars="None"
                        Width="700px">
                        <cc1:TabPanel ID="TabPanel_FaixaEtaria" runat="server" HeaderText="Por faixa etária"
                            ScrollBars="Horizontal" TabIndex="0">
                            <ContentTemplate>
                                <%--  <asp:UpdatePanel ID="UpdatePanel4" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <p>
                                    <span class="rotulo">Faixa Etária Inicial:</span> <span>
                                        <asp:TextBox ID="TextBox_FaixaEtariaInicial" runat="server" CssClass="campo" MaxLength="3"
                                            Width="30px"></asp:TextBox>
                                        <asp:DropDownList ID="DropDown_UnidadeTempoInicial" Width="120px" CssClass="drop"
                                            runat="server">
                                        </asp:DropDownList>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Faixa Etária Final:</span> <span>
                                        <asp:TextBox ID="TextBox_FaixaEtariaFinal" runat="server" CssClass="campo" MaxLength="3"
                                            Width="30px"></asp:TextBox>
                                        <asp:DropDownList ID="DropDown_UnidadeTempoFinal" Width="120px" CssClass="drop" runat="server">
                                        </asp:DropDownList>
                                    </span>
                                </p>
                                <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Evento" runat="server" HeaderText="Por evento" ScrollBars="Horizontal"
                            TabIndex="1">
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Propriedade:</span> <span>
                                        <asp:DropDownList ID="DropDownList_Propriedade" CssClass="drop" runat="server" DataTextField="Nome"
                                            DataValueField="Codigo" Width="120px">
                                        </asp:DropDownList>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Tempo:</span> <span>
                                        <asp:TextBox ID="TextBox_Tempo" Width="30px" MaxLength="3" CssClass="campo" runat="server"></asp:TextBox></span>
                                    <asp:DropDownList ID="DropDown_UnidadeTempo" CssClass="drop" runat="server" Width="75px">
                                    </asp:DropDownList>
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Próxima Dose</span> <span>
                                        <asp:DropDownList ID="DropDown_DosePrevista" CssClass="drop" runat="server" DataValueField="Codigo"
                                            DataTextField="NomeDose" Width="120px">
                                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                    <p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_AddParametrizacao" runat="server" OnClick="OnClick_AdicionarParametrizacao"
                                ValidationGroup="ValidationGroup_PorFaixa">
                  <img id="imgadicionar" alt="Adicionar" src="img/btn_adicionar1.png"
                  onmouseover="imgadicionar.src='img/btn_adicionar2.png';"
                  onmouseout="imgadicionar.src='img/btn_adicionar1.png';" /></asp:LinkButton>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Vacina é Obrigatório."
                                ControlToValidate="DropDownList_Vacina" ValueToCompare="-1" Operator="GreaterThan"
                                Display="None" ValidationGroup="ValidationGroup_PorFaixa"></asp:CompareValidator>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Dose é Obrigatório."
                                ControlToValidate="DropDownList_Dose" ValueToCompare="-1" Operator="GreaterThan"
                                Display="None" ValidationGroup="ValidationGroup_PorFaixa"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowSummary="false"
                                ShowMessageBox="true" ValidationGroup="ValidationGroup_PorFaixa" />
                        </div>
                    </p>
                </fieldset>
                <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="DropDownList_Vacina" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="DropDownList_Dose" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="LinkButton_AddParametrizacao" EventName="Click" />
            </Triggers>
            <ContentTemplate>--%>
                <fieldset class="formulario">
                    <legend>Parâmetros</legend>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Parametrizacoes" runat="server" DataKeyNames="Codigo"
                                Width="100%" CellPadding="3" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric"
                                OnPageIndexChanging="OnPageIndexChanging_Parametrizacao" AutoGenerateColumns="false"
                                OnRowDeleting="OnRowDeleting_Parametrizacao" OnRowDataBound="OnRowDataBound_Parametrizacao">
                                <Columns>
                                    <asp:BoundField HeaderText="Descrição" DataField="DescricaoParametrizacao" ItemStyle-Width="580px"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:CommandField ButtonType="Link" ItemStyle-HorizontalAlign="Center" CausesValidation="true"
                                        ShowDeleteButton="true" HeaderText="Excluir" DeleteText="<img src='img/excluir_gridview.png' border=0 />" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
                <%--            </ContentTemplate>
        </asp:UpdatePanel>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
