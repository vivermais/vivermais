<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormCartaoVacina.aspx.cs" Inherits="ViverMais.View.Vacina.FormCartaoVacina"
    Title="ViverMais - Cartão de Vacina" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Formulário de Atualização de Cartão de Vacina</h2>
        <br />
        <br />
        <cc1:TabContainer ID="TabContainer1" runat="server" ScrollBars="None" Width="800px">
            <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Vacinas da Criança" ScrollBars="Horizontal">
                <ContentTemplate>
                    <%--<fieldset class="formulario">
                        <legend>Vacinas da Criança</legend>--%>
                    <div class="botoesroll">
                        <asp:LinkButton ID="Lnk_Novo" runat="server" OnClick="OnClick_NovoRegistro">
                  <img id="imgnovoregistro" alt="Adicionar" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lknSalvar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="TabPanel4$GridView_HistoricoVacinacao" EventName="RowDeleting" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                <asp:GridView ID="GridView_CartaoVacina" runat="server" AutoGenerateColumns="False"
                                    OnRowEditing="OnRowEditing_CartaoVacina" Width="100%" DataKeyNames="CodigoVacina,CodigoDoseVacina"
                                    BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric"
                                    OnPageIndexChanging="OnPageIndexChanging_CartaoCrianca" GridLines="Horizontal"
                                    Font-Names="Verdana" OnRowCommand="OnRowCommand_GridView_CartaoVacina" OnRowDataBound="OnRowDataBound_CartaoCrianca">
                                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina">
                                            <ItemStyle Width="300px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                            HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                            ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                        <asp:ButtonField ButtonType="Link" CommandName="Edit" Text="<img src='img/refresh.png' alt='Atualizar'/>"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                        <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                        <%-- <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Image ID="Image_Informacoes" runat="server" ImageUrl="~/Vacina/img/info.png" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <%--<asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                        ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" />--%>
                                    </Columns>
                                    <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                        Height="24px" Font-Size="13px" />
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                    <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                    <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                    <AlternatingRowStyle BackColor="#F7F7F7" />
                                </asp:GridView>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--  </fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="Vacinas do Adolescente" ScrollBars="Horizontal">
                <ContentTemplate>
                    <%-- <fieldset class="formulario">
                        <legend>Vacinas do Adolescente</legend>--%>
                    <div class="botoesroll">
                        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_NovoRegistro">
                            <img id="imgnovoregistro2" alt="Adicionar" src="img/btn_novo_registro1.png"
                             onmouseover="imgnovoregistro2.src='img/btn_novo_registro2.png';"
                             onmouseout="imgnovoregistro2.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                     <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lknSalvar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="TabPanel4$GridView_HistoricoVacinacao" EventName="RowDeleting" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_CartaoVacinaAdolescente" runat="server" AutoGenerateColumns="False"
                                        OnRowEditing="OnRowEditing_CartaoAdolescente" DataKeyNames="CodigoVacina,CodigoDoseVacina"
                                        BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                        Width="100%" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_CartaoAdolescente"
                                        CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" OnRowDataBound="OnRowDataBound_CartaoAdolescente"
                                        OnRowCommand="OnRowCommand_GridView_CartaoVacinaAdolescente">
                                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="300px" />
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="100px" />
                                           <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                            HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="Edit" Text="<img src='img/refresh.png' alt='Atualizar'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                            <%--                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="Image_Informacoes" runat="server" ImageUrl="~/Vacina/img/info.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" />--%>
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                        <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                        </EmptyDataTemplate>
                                        <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                    </asp:GridView>
                                </span>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%--</fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="Vacinas do Adulto" ScrollBars="Horizontal">
                <ContentTemplate>
                    <%-- <fieldset class="formulario">
                        <legend>Vacinas do Adulto</legend>--%>
                    <div class="botoesroll">
                        <asp:LinkButton ID="btnNovoRegistroAdulto" runat="server" OnClick="OnClick_NovoRegistro">
                  <img id="imgnovoregistro3" alt="Adicionar" src="img/btn_novo_registro1.png"
                  onmouseover="imgnovoregistro3.src='img/btn_novo_registro2.png';"
                  onmouseout="imgnovoregistro3.src='img/btn_novo_registro1.png';" /></asp:LinkButton>
                    </div>
                    <br />
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                     <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lknSalvar" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="TabPanel4$GridView_HistoricoVacinacao" EventName="RowDeleting" />
                        </Triggers>
                        <ContentTemplate>
                            <p>
                                <span>
                                    <asp:GridView ID="GridView_CartaoVacinaAdulto" runat="server" AutoGenerateColumns="False"
                                        OnRowEditing="OnRowEditing_CartaoAdulto" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric"
                                        OnPageIndexChanging="OnPageIndexChanging_CartaoAdulto" DataKeyNames="CodigoVacina,CodigoDoseVacina"
                                        BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                        Width="100%" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" OnRowDataBound="OnRowDataBound_CartaoAdulto"
                                        OnRowCommand="OnRowCommand_GridView_CartaoVacinaAdulto">
                                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                        <Columns>
                                            <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="300px" />
                                            <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="100px" />
                                           <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                            HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                                ItemStyle-HorizontalAlign="Center" ItemStyle-Width="200px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="Edit" Text="<img src='img/refresh.png' alt='Atualizar'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                            <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                                ControlStyle-Height="16px" ControlStyle-Width="16px" />
                                            <%--                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Image ID="Image_Informacoes" runat="server" ImageUrl="~/Vacina/img/info.png" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            <%--<asp:ButtonField ButtonType="Link" CommandName="CommandName_Informacoes" Text="<img src='img/info.png' alt='Informações' />"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" />--%>
                                            <%--<asp:CommandField ButtonType="Link" EditImageUrl="~/Vacina/img/refresh.png" 
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Atualizar" />--%>
                                            <%--<asp:CommandField ButtonType="Link" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" />--%>
                                        </Columns>
                                        <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                            Height="24px" Font-Size="13px" />
                                        <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                        <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                        <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                        <EmptyDataTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                        </EmptyDataTemplate>
                                        <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                        <AlternatingRowStyle BackColor="#F7F7F7" />
                                    </asp:GridView>
                                </span>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- </fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="Histórico de Vacinação" ScrollBars="Horizontal">
                <ContentTemplate>
                    <%--<fieldset class="formulario2">
                        <legend>Histórico de Vacinação</legend>--%>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                     <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="lknSalvar" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_HistoricoVacinacao" runat="server" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                                Width="100%" AllowPaging="true" PageSize="10" PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Historico"
                                CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" DataKeyNames="Codigo"
                                OnRowCommand="OnRowCommand_GridView_HistoricoVacinacao" OnRowDeleting="OnRowDeleting_HistoricoVacinacao">
                                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                <Columns>
                                    <asp:BoundField DataField="VacinaImpressaoCartao" HeaderText="Vacina" ItemStyle-Width="200px"
                                        ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DoseVacinaImpressaoCartao" HeaderText="Dose" ItemStyle-Width="100px" />
                                    <asp:BoundField DataField="DataAplicacaoVacinaImpressaoCartao" HeaderText="Data Aplicada">
                                            <ItemStyle Width="100px" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ProximaDoseVacinaImpressaoCartao" ItemStyle-Width="54px"
                                            HeaderText="Próxima Dose" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="LoteVacinaImpressaoCartao" HeaderText="Lote" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                         <asp:BoundField DataField="ValidadeLoteImpressaoCartao" HeaderText="Validade" ItemStyle-Width="50px"
                                            ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="UnidadeVacinaImpressaoCartao" HeaderText="Unidade/Local"
                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="270px" />
                                    <asp:ButtonField ButtonType="Link" CommandName="CommandName_VerInformacoes" Text="<img src='img/info.png' alt='Informações'/>"
                                        ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderStyle-HorizontalAlign="Right" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Delete"
                                                OnClientClick="javascript:return confirm('Tem certeza que deseja excluir a vacinação do histórico deste paciente?');return false;">
                                                <img src='img/excluir_gridview.png' alt='Excluir Vacinação?'/>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:CommandField ButtonType="Link" DeleteText="<img src='img/info.png' alt='Informações'/>"
                                        ShowDeleteButton="true" ShowInsertButton="false" />--%>
                                    
                                    <%--                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Image ID="Image_Informacoes" runat="server" ToolTip='<%#bind("InformcaoesCartao") %>'
                                                ImageUrl="~/Vacina/img/info.png" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <%-- <asp:CommandField ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/Vacina/img/info.png"
                                            ControlStyle-Height="16px" ControlStyle-Width="16px" HeaderText="Info" ItemStyle-HorizontalAlign="Center" />--%>
                                </Columns>
                                <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                    Height="24px" Font-Size="13px" />
                                <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
                                <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                        </span>
                    </p>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <%-- </fieldset>--%>
                </ContentTemplate>
            </cc1:TabPanel>
        </cc1:TabContainer>
        
        
        
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel1$GridView_CartaoVacina"
                    EventName="RowEditing" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel1$Lnk_Novo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel2$GridView_CartaoVacinaAdolescente"
                    EventName="RowEditing" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel2$LinkButton1" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel3$GridView_CartaoVacinaAdulto"
                    EventName="RowEditing" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel3$btnNovoRegistroAdulto"
                    EventName="Click" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_AtualizarCartao" runat="server" Visible="false" >
                    <div id="cinza" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="mensagem" visible="false" style="position: fixed; background-color: #f5efcc;
                        background-position: center; background-repeat: no-repeat; top: 150px; left: 25%;
                        width: 600px; height: auto; z-index: 102; background-image: url('img/fundo_mensagem.png');
                        border-right: #e6b626 4px solid; padding-right: 20px; border-top: #e6b626 4px solid;
                        padding-left: 20px; padding-bottom: 20px; border-left: #e6b626 4px solid; color: #000000;
                        padding-top: 20px; border-bottom: #e6b626 4px solid; text-align: justify; font-family: Verdana;">
                        <br />
                        <h6>
                            Atualização do Cartão de Vacina</h6>
                        <br />
                        <p>
                            <span class="rotulo">Imunobiológico</span> <span>
                                <asp:DropDownList ID="DropDown_Vacina" runat="server" CssClass="drop" DataTextField="VacinaFabricante"
                                    DataValueField="Codigo">
                                </asp:DropDownList>
                                <asp:Label ID="Label_Imunobiologico" runat="server" Text="" Visible="false" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                                * </span>
                        </p>
                        <p>
                            <span class="rotulo">Dose</span> <span>
                                <asp:DropDownList ID="DropDown_Dose" runat="server" CssClass="drop" DataTextField="Descricao"
                                    DataValueField="Codigo">
                                </asp:DropDownList>
                                <asp:Label ID="Label_DoseVacina" runat="server" Text="" Visible="false" Font-Bold="true"
                                    Font-Size="Smaller"></asp:Label>
                                * </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Aplicação</span> <span>
                                <asp:TextBox ID="TextBox_Data" runat="server" CssClass="campo"></asp:TextBox>*
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_Data">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" Mask="99/99/9999" MaskType="Date"
                                    TargetControlID="TextBox_Data" InputDirection="LeftToRight" runat="server">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Lote</span> <span>
                                <asp:TextBox ID="TextBox_Lote" runat="server" CssClass="campo"></asp:TextBox>*
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Validade Lote</span> <span>
                                <asp:TextBox ID="TextBox_ValidadeLote" runat="server" CssClass="campo"></asp:TextBox>*
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_ValidadeLote">
                                </cc1:CalendarExtender>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" Mask="99/99/9999" MaskType="Date"
                                    TargetControlID="TextBox_ValidadeLote" InputDirection="LeftToRight" runat="server">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Local</span> <span>
                                <asp:TextBox ID="TextBox_Local" runat="server" CssClass="campo" MaxLength="40"></asp:TextBox>*
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Motivo</span> <span>
                                <asp:TextBox ID="TextBox_Motivo" runat="server" CssClass="campo" MaxLength="40"></asp:TextBox>*
                            </span>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknSalvar" runat="server" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_AtualizarDadosCartao">
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="lknCancelar" runat="server" OnClick="OnClick_Cancelar">
                  <img id="imgfinalizar" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="imgfinalizar.src='img/btn_cancelar2.png';"
                  onmouseout="imgfinalizar.src='img/btn_cancelar1.png';" /></asp:LinkButton>
                        </div>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Imunobiológico é Obrigatório."
                            Display="None" ControlToValidate="DropDown_Vacina" ValueToCompare="-1" Operator="GreaterThan"
                            ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Dose é Obrigatório."
                            Display="None" ControlToValidate="DropDown_Dose" ValueToCompare="-1" Operator="GreaterThan"
                            ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Data de Aplicação é Obrigatório."
                            ControlToValidate="TextBox_Data" Display="None" ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Aplicação inválida."
                            ControlToValidate="TextBox_Data" Display="None" Operator="DataTypeCheck" Type="Date"
                            ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Data de Aplicação deve ser igual ou maior que 01/01/1900."
                            ControlToValidate="TextBox_Data" Display="None" Operator="GreaterThanEqual" ValueToCompare="01/01/1900"
                            Type="Date" ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Lote é Obrigatório."
                            Display="None" ValidationGroup="ValidationGroup_AtualizarDadosCartao" ControlToValidate="TextBox_Lote">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Validade Lote é Obrigatório."
                            ControlToValidate="TextBox_ValidadeLote" Display="None" ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ErrorMessage="Validade Lote inválida."
                            ControlToValidate="TextBox_ValidadeLote" Display="None" Operator="DataTypeCheck"
                            Type="Date" ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:CompareValidator ID="CompareValidator6" runat="server" ErrorMessage="Validade Lote deve ser igual ou maior que 01/01/1900."
                            ControlToValidate="TextBox_ValidadeLote" Display="None" Operator="GreaterThanEqual"
                            ValueToCompare="01/01/1900" Type="Date" ValidationGroup="ValidationGroup_AtualizarDadosCartao"></asp:CompareValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Local é Obrigatório."
                            Display="None" ValidationGroup="ValidationGroup_AtualizarDadosCartao" ControlToValidate="TextBox_Local">
                        </asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Motivo é Obrigatório."
                            Display="None" ValidationGroup="ValidationGroup_AtualizarDadosCartao" ControlToValidate="TextBox_Motivo">
                        </asp:RequiredFieldValidator>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                            ShowSummary="false" ValidationGroup="ValidationGroup_AtualizarDadosCartao" />
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel1$GridView_CartaoVacina"
                    EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel2$GridView_CartaoVacinaAdolescente"
                    EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel3$GridView_CartaoVacinaAdulto"
                    EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="TabContainer1$TabPanel4$GridView_HistoricoVacinacao"
                    EventName="RowCommand" />
            </Triggers>
            <ContentTemplate>
                <asp:Panel ID="Panel_Info" runat="server" Visible="false">
                    <div id="Div1" visible="false" style="position: fixed; top: 0px; left: 0px; width: 100%;
                        height: 100%; z-index: 100; min-height: 100%; background-color: #000000; filter: alpha(opacity=75);
                        moz-opacity: 0.3; opacity: 0.3">
                    </div>
                    <div id="Div2" visible="false" style="position: fixed; background-color: #f5efcc;
                        background-position: center; background-repeat: no-repeat; top: 100px; left: 25%;
                        width: 500px; height: auto; z-index: 102; background-image: url('img/fundo_mensagem.png');
                        border-right: #e6b626 4px solid; padding-right: 20px; border-top: #e6b626 4px solid;
                        padding-left: 20px; padding-bottom: 30px; border-left: #e6b626 4px solid; color: #000000;
                        padding-top: 20px; border-bottom: #e6b626 4px solid; text-align: justify; font-family: Verdana;">
                        <div style="width: 500px; height: 10px; margin-left: 20px; margin-top: 10px;">
                        </div>
                        <br />
                        <h6>
                            Informação sobre o imunobiológico</h6>
                        <br />
                        <br />
                        <p>
                            <span class="rotulo">Vacina </span><span>
                                <asp:Label ID="Label_InfoVacina" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Dose </span><span>
                                <asp:Label ID="Label_InfoDose" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <%--<p>
                            <span>--%>
                        <%-- <table>
                                    <tr>
                                        <th>Doenças Evitadas</th>
                                        <td>--%>
                                        <br />
                        <fieldset style="margin: 0px; padding: 10px;">
                            <legend style="margin: 8px; margin-left: 0px;">Doenças Evitadas</legend><span style="font-weight: bold;
                                font-size: x-small">
                                <asp:Literal ID="Literal_InfoDoencas" runat="server"></asp:Literal>
                            </span>
                        </fieldset>
                        <fieldset style="margin: 0px; padding: 10px;">
                            <legend style="margin: 8px; margin-left: 0px;">Paramêtros</legend><span style="font-weight: bold;
                                font-size: x-small">
                                <asp:Literal ID="Literal_InfoParametros" runat="server"></asp:Literal>
                            </span>
                        </fieldset>
                        <br />
                        <asp:LinkButton ID="lkn_Fechar" runat="server" OnClick="OnClick_FecharInformacoes">
			        <img id="imgvoltar" alt="Fechar" src="img/fechar-pop.png"
                  	onmouseover="imgvoltar.src='img/fechar-pop.png';"
                  	onmouseout="imgvoltar.src='img/fechar-pop.png';" />	
                        </asp:LinkButton>
                        <%-- </div>--%>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="botoesroll">
            <asp:LinkButton ID="lknCartaoVacina" runat="server" OnClick="btnCartaoVacina_Click">
                  <img id="imgcartaovacina" alt="Imprimir Cartão" src="img/imprimir-cartao.png"
                  onmouseover="imgcartaovacina.src='img/imprimir-cartao2.png';"
                  onmouseout="imgcartaovacina.src='img/imprimir-cartao.png';" />
            </asp:LinkButton>
        </div>
        <div class="botoesroll">
            <asp:LinkButton ID="LnkVoltar" runat="server" PostBackUrl="~/Vacina/FormPesquisaPaciente.aspx?tipo=cartao">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn_voltar1.png"
                  onmouseover="imgcancelar.src='img/btn_voltar2.png';"
                  onmouseout="imgcancelar.src='img/btn_voltar1.png';" /></asp:LinkButton>
        </div>
        <br />
        <br />
    </div>
</asp:Content>
