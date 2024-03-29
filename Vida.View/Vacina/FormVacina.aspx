﻿<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormVacina"
    MasterPageFile="~/Vacina/MasterVacina.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">

    <script type="text/javascript" language="javascript">
        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = '340 px';
            top.style.left = '480 px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

</asp:Content>
<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div id="top">
        <h2>
            Formulário para Cadastro de Imunobiológicos</h2>
        <br />
        <br />
        <cc1:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="740px"
            ActiveTabIndex="0">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Imunobiológico" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset>
                        <legend>Imunobiológico</legend>
                        <p>
                            <span class="rotulo">Código</span> <span>
                                <asp:TextBox ID="TextBox_Codigo" MaxLength="7" CssClass="campo" runat="server" Width="70px">
                                </asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome</span> <span>
                                <asp:TextBox ID="TextBox_Nome" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                            </span>
                        </p>
                        <%--<p>
                                    <span class="rotulo">Abreviação Nome</span> <span>
                                        <asp:TextBox ID="TextBox_AbreviacaoNome" CssClass="campo" MaxLength="11" runat="server"
                                            Width="300px"></asp:TextBox>
                                    </span>
                                </p>--%>
                        <p>
                            <span class="rotulo">Unidade de Medida</span> <span>
                                <asp:DropDownList ID="DropDown_UnidadeMedida" runat="server" Width="150px" DataTextField="Nome"
                                    CssClass="drop" DataValueField="Codigo" />
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Pertence ao Calendário</span> <span>
                                <asp:CheckBox ID="CheckBox_PertenceAoCalendario" runat="server" />
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Ativa</span> <span>
                                <asp:CheckBox ID="CheckBox_Ativo" runat="server" Checked="true" />
                            </span>
                        </p>
                    </fieldset>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Código é obrigatório!"
                        Display="None" ControlToValidate="TextBox_Codigo" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="O código da vacina deve possuir 7 dígitos."
                        ControlToValidate="TextBox_Codigo" Display="None" ValidationGroup="ValidationGroup_cadVacina"
                        ValidationExpression="^\d{7}$">
                    </asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Nome é obrigatório!"
                        Display="None" ControlToValidate="TextBox_Nome" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>
                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Abreviação Nome é obrigatório!"
                                Display="None" ControlToValidate="TextBox_AbreviacaoNome" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Unidade de medida é obrigatório!"
                        Display="None" ControlToValidate="DropDown_UnidadeMedida" InitialValue="0" ValidationGroup="ValidationGroup_cadVacina"></asp:RequiredFieldValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                        ValidationGroup="ValidationGroup_cadVacina" runat="server" />
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="9999999" TargetControlID="TextBox_Codigo"
                        InputDirection="LeftToRight" ClearMaskOnLostFocus="false" MaskType="None">
                    </cc1:MaskedEditExtender>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Doses" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset>
                        <legend>Doses de Imunobiológico</legend>
                        <p>
                            <span class="rotulo">Doses</span> <span style="float: left">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_Dose" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="Button_Adicionar" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDown_Dose" runat="server" Width="300px" DataTextField="Descricao"
                                            DataValueField="Codigo" CssClass="drop" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span><span>
                                <asp:LinkButton ID="Button_Adicionar" runat="server" OnClick="Button_Adicionar_OnClick"
                                    ValidationGroup="AddDoseVacina">
                                            <img id="img1" alt="Adicionar" src="img/add-vac.png" />
                                </asp:LinkButton>
                            </span>
                            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Selecione uma dose."
                                Display="None" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="DropDown_Dose"
                                ValidationGroup="AddDoseVacina"></asp:CompareValidator>
                            <asp:ValidationSummary ID="ValidationSummary3" Font-Size="XX-Small" runat="server"
                                ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddDoseVacina" />
                        </p>
                        <br />
                        <p>
                            <span>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="Button_Adicionar" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="GridView_Dose" AutoGenerateColumns="False" OnRowCommand="OnRowCommand_ExcluirDose"
                                            BackColor="White" BorderColor="#f9e5a9" Width="100%" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" OnRowDataBound="OnRowDataBound_Dose">
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Nome" DataField="Descricao" ItemStyle-Width="550px" />
                                                <asp:ButtonField ButtonType="Image" CommandName="CommandName_Excluir" HeaderText="Excluir"
                                                    CausesValidation="true" ImageUrl="~/Vacina/img/excluir_gridview.png" />
                                                <%--<asp:CommandField ButtonType="Image" HeaderText="Excluir" ShowDeleteButton="true" DeleteImageUrl="~/Vacina/img/excluir_gridview.png" ControlStyle-Width="15px" ControlStyle-Height="15px"  />--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhuma dose registrada."></asp:Label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span>
                        </p>
                        <br />
                    </fieldset>
                    <%--<fieldset>
                                <legend>Doses já cadastradas</legend>
                                <p>
                                    <span>Selecione as doses que deseja excluir
                                        <asp:CheckBoxList ID="CheckBoxList_DOSE" runat="server" AutoPostBack="true" OnSelectedIndexChanged="OnSelectedIndexChanged_CheckBoxList_DOSE"
                                            DataTextField="NomeDose" DataValueField="Codigo" Width="200px">
                                        </asp:CheckBoxList>
                                    </span>
                                </p>
                            </fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Doenças Evitadas" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset>
                        <legend>Doenças Evitadas</legend>
                        <p>
                            <span class="rotulo">Doença</span> <span style="float: left">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_Doenca" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_addDoenca" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDown_Doenca" runat="server" Width="300px" CssClass="drop"
                                            DataTextField="Nome" DataValueField="Codigo">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span><span>
                                <asp:LinkButton ID="btn_addDoenca" runat="server" OnClick="Button_AdicionarDoenca_OnClick"
                                    ValidationGroup="AddDoenca">
                                            <img id="img2" alt="Adicionar" src="img/add-vac.png" />
                                </asp:LinkButton>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_addDoenca" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="GridView_Doenca" AutoGenerateColumns="false" OnRowCommand="OnRowCommand_ExcluirDoenca"
                                            BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                            CellPadding="3" Width="100%" GridLines="Horizontal" Font-Names="Verdana">
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Nome" DataField="Nome" ItemStyle-Width="550px" />
                                                <asp:ButtonField ButtonType="Image" CommandName="CommandName_Excluir" CausesValidation="true"
                                                    HeaderText="Excluir" ImageUrl="/Vacina/img/excluir_gridview.png" />
                                                <%--<asp:CommandField ButtonType="Image" HeaderText="Excluir" ShowDeleteButton="true" CausesValidation="true" DeleteImageUrl="~/Vacina/img/excluir_gridview.png" ControlStyle-Width="15px" ControlStyle-Height="15px"  />--%>
                                            </Columns>
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhuma doença registrada."></asp:Label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span>
                        </p>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma doença."
                            Display="None" ValueToCompare="0" Operator="GreaterThan" ControlToValidate="DropDown_Doenca"
                            ValidationGroup="AddDoenca"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary2" Font-Size="XX-Small" runat="server"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="AddDoenca" />
                    </fieldset>
                    <%--<fieldset>
                                <legend>Doenças já cadastradas</legend>--%>
                    <%--</fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Estratégias" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset>
                        <legend>Estratégias Vínculadas</legend>
                        <p>
                            <span class="rotulo">Estratégia</span> <span style="float: left">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GridView_Estrategia" EventName="RowCommand" />
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton_AdicionarEstrategia" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:DropDownList ID="DropDownList_Estrategia" runat="server" Width="300px" CssClass="drop"
                                            DataTextField="Descricao" DataValueField="Codigo">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span><span>
                                <asp:LinkButton ID="LinkButton_AdicionarEstrategia" runat="server" OnClick="OnClick_AdicionarEstrategia"
                                    ValidationGroup="ValidationGroup_Estrategia">
                                            <img id="img3" alt="Adicionar" src="img/add-vac.png" />
                                </asp:LinkButton>
                            </span>
                        </p>
                        <p>
                            <span>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton_AdicionarEstrategia" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView runat="server" ID="GridView_Estrategia" AutoGenerateColumns="false"
                                            OnRowCommand="OnRowCommand_EstrategiaVacina" BackColor="White" BorderColor="#f9e5a9"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="100%" GridLines="Horizontal"
                                            Font-Names="Verdana">
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Nome" DataField="Descricao" ItemStyle-Width="550px" />
                                                <asp:ButtonField ButtonType="Image" CommandName="CommandName_Excluir" CausesValidation="true"
                                                    HeaderText="Excluir" ImageUrl="/Vacina/img/excluir_gridview.png" />
                                            </Columns>
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhuma estratégia registrada."></asp:Label>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span>
                        </p>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Selecione uma estratégia."
                            Display="None" ValueToCompare="-1" Operator="GreaterThan" ControlToValidate="DropDownList_Estrategia"
                            ValidationGroup="ValidationGroup_Estrategia"></asp:CompareValidator>
                        <asp:ValidationSummary ID="ValidationSummary4" Font-Size="XX-Small" runat="server"
                            ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Estrategia" />
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="Fabricantes" ScrollBars="Horizontal">
                <ContentTemplate>
                    <fieldset>
                        <legend>Fabricantes Vínculados</legend>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="LinkButton_AdicionarItemVacina" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="LinkButton_CancelarItemVacina" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <p>
                                    <span class="rotulo">Fabricante</span> <span>
                                        <asp:DropDownList ID="DropDown_Fabricante" runat="server" CssClass="drop" DataTextField="Nome"
                                            DataValueField="Codigo" />
                                    </span>
                                </p>
                                <p>
                                    <span class="rotulo">Aplicações</span> <span>
                                        <asp:TextBox ID="TextBox_Aplicacao" CssClass="campo" runat="server" Width="20px"
                                            MaxLength="4"></asp:TextBox>
                                        <asp:ImageButton ID="ImgButton_DuViverMaisAplicacao" runat="server" Width="16px" Height="18px"
                                            OnClientClick="return false;" ImageUrl="~/Vacina/img/help.png" Style="position: absolute;" />
                                </p>
                                <div id="flyout" class="wireFrame">
                                </div>
                                <!-- Info panel to be displayed as a flyout when the button is clicked -->
                                <div id="info" style="display: none; width: 300px; z-index: 2; opacity: 0; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=0);
                                    font-size: 12px; border: solid 1px #CCCCCC; background-color: #FFFFFF; padding: 5px;">
                                    <div id="btnCloseParent" style="float: right; opacity: 100; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=100);">
                                        <asp:LinkButton ID="btnClose" runat="server" OnClientClick="return false;" Text="X"
                                            ToolTip="Fechar" Style="background-color: #666666; color: #FFFFFF; text-align: center;
                                            font-weight: bold; text-decoration: none; border: outset thin #FFFFFF; padding: 5px;" />
                                    </div>
                                    <br />
                                    Indica quantas aplicações de vacina podem ser administradas utilizando um item desta
                                    vacina.
                                    <br />
                                    Exemplo: Uma ampola da vacina BCG da FUNDAÇÃO ATAULPHO possibilita, no máximo, dez
                                    aplicações.
                                </div>
                                <asp:CompareValidator ID="CompareValidator4" runat="server" Display="None" ErrorMessage="Selecione um Fabricante!"
                                    ValidationGroup="ValidationGroup_AdicionarItemVacina" ControlToValidate="DropDown_Fabricante"
                                    ValueToCompare="-1" Operator="GreaterThan">
                                </asp:CompareValidator>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" Display="None" runat="server"
                                    ControlToValidate="TextBox_Aplicacao" ErrorMessage="Número de aplicações é obrigatório!"
                                    ValidationGroup="ValidationGroup_AdicionarItemVacina"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="None" runat="server"
                                    ControlToValidate="TextBox_Aplicacao" ErrorMessage="O campo Aplicações deve conter somente números!"
                                    ValidationGroup="ValidationGroup_AdicionarItemVacina" ValidationExpression="\d*"></asp:RegularExpressionValidator>
                                <asp:CompareValidator ID="CompareValidator5" runat="server" Display="None" ErrorMessage="A quantidade de aplicações deve ser maior que 0."
                                    ValidationGroup="ValidationGroup_AdicionarItemVacina" ControlToValidate="TextBox_Aplicacao"
                                    ValueToCompare="0" Operator="GreaterThan">
                                </asp:CompareValidator>
                                <asp:ValidationSummary ID="ValidationSummary5" ShowMessageBox="true" ShowSummary="false"
                                    ValidationGroup="ValidationGroup_AdicionarItemVacina" runat="server" />
                                <cc1:AnimationExtender ID="OpenAnimation" runat="server" TargetControlID="ImgButton_DuViverMaisAplicacao">
                                    <Animations>
                        <OnClick>
                        <Sequence>
                         <%-- Disable the button so it can't be clicked again --%>
                         <EnableAction Enabled="false" />
                         <%-- Position the wire frame and show it --%>
                         <ScriptAction Script="Cover($get('ImgButton_DuViverMaisAplicacao'), $get('flyout'));" />
                         <StyleAction AnimationTarget="flyout" Attribute="display" Value="block"/>
                         <ScriptAction Script="Cover($get('flyout'), $get('info'), true);" />
                         <StyleAction AnimationTarget="info" Attribute="display" Value="block"/>
                         <FadeIn AnimationTarget="info" Duration=".2"/>
                         <StyleAction AnimationTarget="flyout" Attribute="display" Value="none"/>
                         </Sequence>
                         </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                <cc1:AnimationExtender ID="CloseAnimation" runat="server" TargetControlID="btnClose">
                                    <Animations>
                                    <OnClick>
                                    <Sequence AnimationTarget="info">
                                    <%--  Shrink the panel out of view --%>
                                    <StyleAction Attribute="overflow" Value="hidden"/>
                                    <Parallel Duration=".3" Fps="15">
                                    <Scale ScaleFactor="0.05" Center="true" ScaleFont="true" FontUnit="px" />
                                    <FadeOut />
                                    </Parallel>
                                    <%--  Reset the target --%>
                                    <StyleAction Attribute="display" Value="none"/>
                                    <StyleAction Attribute="width" Value="250px"/>
                                    <StyleAction Attribute="height" Value=""/>
                                    <StyleAction Attribute="fontSize" Value="12px"/>
                                    <%--  Enable the button --%>
                                    <EnableAction AnimationTarget="ImgButton_DuViverMaisAplicacao" Enabled="true" />
                                    </Sequence>
                                    </OnClick>
                                    </Animations>
                                </cc1:AnimationExtender>
                                </span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_AdicionarItemVacina" runat="server" ValidationGroup="ValidationGroup_AdicionarItemVacina"
                                OnClick="OnClick_AdicionarItemVacina">
                  <img id="imgincluiritem" alt="Incluir" src="img/btn_adicionar1.png"
                  onmouseover="imgincluiritem.src='img/btn_adicionar2.png';"
                  onmouseout="imgincluiritem.src='img/btn_adicionar1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="LinkButton_CancelarItemVacina" runat="server" CausesValidation="false"
                                OnClick="OnClick_CancelarInclusaoItemVacina">
                            <img id="imgcancelarinclusaoitem" alt="Cancelar"
                                onmouseout="imgcancelarinclusaoitem.src='img/btn_cancelar1.png';" 
                                onmouseover="imgcancelarinclusaoitem.src='img/btn_cancelar2.png';" 
                                src="img/btn_cancelar1.png" /></asp:LinkButton>
                        </div>
                        <br />
                        <br />
                        <p>
                            <span>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="LinkButton_AdicionarItemVacina" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView_ItemVacina" Width="100%" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="OnRowCommand_ItemVacina" DataKeyNames="Codigo" BackColor="White"
                                            BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                                            Font-Names="Verdana">
                                            <Columns>
                                                <asp:BoundField DataField="NomeFabricante" HeaderText="Fabricante" ItemStyle-Width="400px" />
                                                <asp:BoundField DataField="Aplicacao" HeaderText="Aplicações" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-Width="100px" />
                                                <asp:ButtonField ButtonType="Image" CausesValidation="true" CommandName="CommandName_Excluir"
                                                    ImageUrl="~/Vacina/img/excluir_gridview.png" ControlStyle-Width="15px" HeaderText="Excluir"
                                                    ControlStyle-Height="15px" />
                                            </Columns>
                                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#191919" />
                                            <EmptyDataRowStyle HorizontalAlign="Center" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label2" runat="server" Text="Nenhum fabricante vínculado."></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                            <AlternatingRowStyle BackColor="#F7F7F7" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </span>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        <div class="botoesroll" style="margin-left: 200px;">
            <asp:LinkButton ID="Lkn_Salvar" runat="server" OnClick="OnClick_Salvar" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_cadVacina')) return confirm('Todos os dados da vacina estão corretos ?'); return false;">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="Lkn_Cancelar" runat="server" PostBackUrl="~/Vacina/FormExibeVacina.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
        </div>
        <br />
        <br />
        <p>
        </p>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
