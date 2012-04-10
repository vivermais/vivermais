<%@ Page Language="C#" MasterPageFile="~/Vacina/MasterVacina.Master" AutoEventWireup="true"
    CodeBehind="FormVincularVacinaFabricante.aspx.cs" Inherits="ViverMais.View.Vacina.FormVincularVacinaFabricante"
    Title="ViverMais - Vínculo de Imunobiológicos e Fabricantes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function showTooltip(obj) {
            if (obj.options[obj.selectedIndex].title == "") {
                obj.title = obj.options[obj.selectedIndex].text;
                obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
                for (i = 0; i < obj.options.length; i++) {
                    obj.options[i].title = obj.options[i].text;
                }
            }
            else
                obj.title = obj.options[obj.selectedIndex].text;
        }

        function Cover(bottom, top, ignoreSize) {
            var location = Sys.UI.DomElement.getLocation(bottom);
            top.style.position = 'absolute';
            top.style.top = location.y + ' px';
            top.style.left = 5 * location.x + ' px';
            if (!ignoreSize) {
                top.style.height = bottom.offsetHeight + 'px';
                top.style.width = bottom.offsetWidth + 'px';
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UPD1" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
    <div id="top">
                <h2>
                    Fomulário para Vínculo de Imunobiológicos e Fabricantes</h2>
                <fieldset>
                    <legend>Itens do Vínculo</legend>
                    <p>
                        <span class="rotulo">Imunobiológico</span> <span>
                            <asp:DropDownList ID="DropDown_Vacina" runat="server" CssClass="drop" DataTextField="Nome" DataValueField="Codigo"
                                AutoPostBack="true" OnSelectedIndexChanged="DropDown_Vacina_OnSelectedIndexChanged" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Fabricante</span> <span>
                            <asp:DropDownList ID="DropDown_Fabricante" runat="server" CssClass="drop" DataTextField="Nome" DataValueField="Codigo" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Aplicações</span> <span>
                            <asp:TextBox ID="TextBox_Aplicacao" CssClass="campo" runat="server" Width="20px"
                                MaxLength="4"></asp:TextBox>
                            <asp:ImageButton ID="ImgButton_DuViverMaisAplicacao" runat="server" Width="16px" Height="18px" OnClientClick="return false;" ImageUrl="~/Vacina/img/help.png" style="position:absolute;" />
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
                                Indica quantas aplicações de vacina podem ser administradas utilizando um item de
                                vacina.
                                <br />
                                Exemplo: Uma ampola da vacina H1N1 possibilita, no máximo, dez aplicações.
                            </div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                ControlToValidate="DropDown_Vacina" ErrorMessage="Selecione um Imunobiológico!"
                                InitialValue="0" ValidationGroup="ValidationGroup_Adicionar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="None" runat="server" InitialValue="0"
                                ControlToValidate="DropDown_Fabricante" ErrorMessage="Selecione um Fabricante!"
                                ValidationGroup="ValidationGroup_Adicionar"></asp:RequiredFieldValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" runat="server"
                                ControlToValidate="TextBox_Aplicacao" ErrorMessage="Número de aplicações é obrigatório!"
                                ValidationGroup="ValidationGroup_Adicionar"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Display="None" runat="server"
                                ControlToValidate="TextBox_Aplicacao" ErrorMessage="O campo aplicação deve conter somente números!"
                                ValidationGroup="ValidationGroup_Adicionar" ValidationExpression="\d*"></asp:RegularExpressionValidator>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false"
                                ValidationGroup="ValidationGroup_Adicionar" runat="server" />
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
                        <p>
                        </p>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnAdicionar" runat="server" OnClick="OnClick_btnAdicionar" 
                                ValidationGroup="ValidationGroup_Adicionar">
                            <img id="imgadicionar" alt="Adicionar" 
                                onmouseout="imgadicionar.src='img/btn_adicionar1.png';" 
                                onmouseover="imgadicionar.src='img/btn_adicionar2.png';" 
                                src="img/btn_adicionar1.png" /></asp:LinkButton>
                        </div>
                        <div class="botoesroll">
                            <asp:LinkButton ID="btnCancelar" runat="server" CausesValidation="false" 
                                OnClick="OnClick_btnCancelar">
                            <img id="imgcancelar" alt="Voltar" 
                                onmouseout="imgcancelar.src='img/btn_cancelar1.png';" 
                                onmouseover="imgcancelar.src='img/btn_cancelar2.png';" 
                                src="img/btn_cancelar1.png" /></asp:LinkButton>
                        </div>
                    </p>
                </fieldset>
                <asp:Panel ID="Panel_FabricantesVinculados" runat="server" Visible="false">
                <fieldset>
                    <legend>Fabricantes vinculados</legend>
                    <p>
                            <asp:GridView ID="GridView_Fabricante" runat="server" AutoGenerateColumns="False"
                                OnRowDeleting="OnRowDeleting_GridView_Fabricante" DataKeyNames="Codigo"
                                OnRowEditing="OnRowEditing_GridView_Fabricante" PageSize="20" PagerSettings-Mode="Numeric"
                            BackColor="White" BorderColor="#f9e5a9" BorderStyle="None" BorderWidth="1px"
                            CellPadding="3" GridLines="Horizontal" Font-Names="Verdana" >
                                <Columns>
                                    <asp:BoundField DataField="NomeFabricante" HeaderText="Fabricante" ItemStyle-Width="300px" />
                                    <asp:BoundField DataField="Aplicacao" HeaderText="Aplicações" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="100px" />
                                    <asp:CommandField ButtonType="Image" HeaderText="Editar" ShowEditButton="true" EditImageUrl="~/Vacina/img/editar.png" ControlStyle-Width="16px" ControlStyle-Height="16px" />
                                    <asp:CommandField ButtonType="Image" HeaderText="Excluir" ShowDeleteButton="true" DeleteImageUrl="~/Vacina/img/excluir_gridview.png" ControlStyle-Width="15px" ControlStyle-Height="15px" />
                                </Columns>
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                Height="24px" Font-Size="13px" />
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#191919" />
                            <EmptyDataRowStyle HorizontalAlign="Center" />
                            <PagerStyle BackColor="#f9e5a9" ForeColor="#dcb74a" HorizontalAlign="Right" />
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            </asp:GridView>
                    </p>
                </fieldset>
                </asp:Panel>
           <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
          <%--<Triggers>
             <asp:AsyncPostBackTrigger ControlID="Button_IncluirMedicamento"  EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="btnSim"  EventName="Click" />
             <asp:AsyncPostBackTrigger ControlID="btnNao"  EventName="Click" />
          </Triggers>         --%> 
          <ContentTemplate>
               <asp:Panel ID="Panel_Atualizar" runat="server" Visible="false">
                    <div id="Div1" visible="false" style="POSITION: absolute; TOP: 0px; LEFT: 0px; WIDTH: 100%; HEIGHT: 200%; Z-INDEX: 100; MIN-HEIGHT: 100%; BACKGROUND-COLOR: #999; FILTER: alpha(opacity=45); moz-opacity: 0.3; opacity: 0.3"></div>   
                    <div id="Div2" visible="false" style="POSITION: fixed; background-color: #FFFFFF; background-position:center; background-repeat:no-repeat; TOP: 150px; LEFT: 25%; WIDTH: 500px; HEIGHT: 180px; Z-INDEX: 102; background-image: url('img/fundo_mensagem.png'); BORDER-RIGHT: #336699 2px solid; PADDING-RIGHT: 10px; BORDER-TOP: #336699 2px solid; PADDING-LEFT: 10px; PADDING-BOTTOM: 10px; BORDER-LEFT: #336699 2px solid; COLOR: #000000; PADDING-TOP: 0px; BORDER-BOTTOM: #336699 2px solid;  TEXT-ALIGN: justify; font-family:Verdana;">
                         <br />
                         <h2>Atualização do Fabricante</h2>
                         <br />
                         <p>
                            <span class="rotulo">Aplicação</span>
                            <span>
                              <asp:TextBox ID="TextBox_AplicacaoAlterar" runat="server" CssClass="campo"></asp:TextBox>
                            </span>
                         </p>
                    <div class="botoesroll">
                  <asp:LinkButton ID="lknSalvarAlteracao" runat="server" onclick="OnClick_SalvarAlteracao" >
                  <img id="imgsalvar" alt="Salvar" src="img/btn_salvar1.png"
                  onmouseover="imgsalvar.src='img/btn_salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn_salvar1.png';" /></asp:LinkButton>
                        </div>
                    <div class="botoesroll">
                  <asp:LinkButton ID="lknCancelarAlteracao" runat="server" onclick="OnClick_CancelarAlteracao" >
                  <img id="img1" alt="Cancelar" src="img/btn_cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn_cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn_cancelar1.png';" /></asp:LinkButton>
                        </div>
                    </div>
               </asp:Panel>
          </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
