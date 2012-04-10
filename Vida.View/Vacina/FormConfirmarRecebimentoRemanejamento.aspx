<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormConfirmarRecebimentoRemanejamento.aspx.cs"
    MasterPageFile="~/Vacina/MasterVacina.Master" EnableEventValidation="false" Inherits="ViverMais.View.Vacina.FormConfirmarRecebimentoRemanejamento" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>
            Confirmar Imunobiológicos do Remanejamento</h2>
        <br />
        <br />
        <cc1:TabContainer ID="TabContainer_ItensRemanejamento" runat="server" Width="850px">
            <cc1:TabPanel ID="TabPanel_Um" runat="server" HeaderText="Informações Gerais">
                <ContentTemplate>
                    <fieldset class="formulario" style="width: 800px">
                        <legend>Movimento de Origem</legend>
                        <p>
                            <span class="rotulo">Data</span> <span>
                                <asp:Label ID="Label_DataMovimento" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Sala de Origem</span> <span>
                                <asp:Label ID="Label_SalaMovimento" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Sala de Destino</span> <span>
                                <asp:Label ID="Label_SalaDestino" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label></span>
                        </p>
                        <p>
                            <span class="rotulo">Data Envio</span> <span>
                                <asp:Label ID="Label_DataEnvioMovimento" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data Recebimento</span> <span>
                                <asp:Label ID="Label_DataRecebMovimento" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Responsável Envio</span> <span>
                                <asp:Label ID="Label_RespEnvMovimento" runat="server" Text="" Font-Bold="true" Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Responsável Recebimento</span> <span>
                                <asp:Label ID="Label_RespRecebMovimento" runat="server" Text="" Font-Bold="true"
                                    Font-Size="Smaller"></asp:Label>
                            </span>
                        </p>
                    </fieldset>
                </ContentTemplate>
            </cc1:TabPanel>
            <cc1:TabPanel ID="TabPanel_Imunos" runat="server" HeaderText="Imunobiológicos">
                <ContentTemplate>
                    <fieldset class="formulario" style="width: 800px">
                        <legend>Relação</legend>
                        <p>
                            <span>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView_Imunos" Width="100%" runat="server" AutoGenerateColumns="false"
                                            OnRowUpdating="OnRowUpdating_Imunos" OnRowCancelingEdit="OnRowCancelingEdit_Imunos"
                                            DataKeyNames="Codigo" OnRowEditing="OnRowEditing_Imunos" AllowPaging="true" PageSize="10"
                                            PagerSettings-Mode="Numeric" OnPageIndexChanging="OnPageIndexChanging_Imunos"
                                            OnRowDataBound="OnRowDataBound_Imunos" BackColor="White" BorderColor="#f9e5a9"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" Font-Names="Verdana">
                                            <Columns>
                                                <asp:BoundField HeaderText="Imunobiológico" DataField="NomeVacina" ItemStyle-Width="200px"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-Width="200px"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Aplicação" DataField="AplicacaoVacina" ItemStyle-Width="130px"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Lote" DataField="IdentificacaoLote" ItemStyle-Width="100px"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Validade" DataField="DataValidadeLote" DataFormatString="{0:dd/MM/yyy}"
                                                    ReadOnly="true" />
                                                <asp:BoundField HeaderText="Quantidade Enviada" DataField="QuantidadeRegistrada"
                                                    ReadOnly="true" />
                                                <asp:TemplateField HeaderText="Quantidade Confirmada">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%#bind("QuantidadeConfirmada") %>' Font-Bold="true"
                                                            Font-Size="Medium"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox_QuantidadeConfirmada" runat="server" CssClass="campo" MaxLength="5"
                                                            Width="30px" Text='<%#bind("QuantidadeConfirmada") %>'></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="ValidationGroup_ReceberImuno"
                                                            runat="server" ErrorMessage="O campo da quantidade é obrigatório." Display="None"
                                                            ControlToValidate="TextBox_QuantidadeConfirmada">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"
                                                            ControlToValidate="TextBox_QuantidadeConfirmada" ValidationGroup="ValidationGroup_ReceberImuno"
                                                            ValidationExpression="^\d*$" ErrorMessage="Digite somente números na quantidade."></asp:RegularExpressionValidator>
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                                                            ShowSummary="false" ValidationGroup="ValidationGroup_ReceberImuno" />
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="150px" HeaderText="Confirmar ?">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButtonConfirmar" runat="server" CommandName="Edit" ></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Update" Text="<img src='img/btn_confirmar.png' border='0' title='Confirmar recebimento?' />" OnClientClick="javascript:if (Page_ClientValidate('ValidationGroup_ReceberImuno')) return confirm('Tem certeza que deseja confirmar o recebimento deste imunobiológico ?'); return false;"></asp:LinkButton>
                                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Cancel" Text="<img src='img/btn_cancelar.png' border='0' title='Cancelar' />"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle HorizontalAlign="Center" Font-Bold="true" Font-Size="Smaller" />
                                            <EmptyDataTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="tab" BackColor="#dcb74a" Font-Bold="True" ForeColor="#383838"
                                                Height="24px" Font-Size="13px" />
                                            <FooterStyle BackColor="#B5C7DE" ForeColor="#dcb74a" />
                                            <RowStyle CssClass="tabrow" BackColor="#f9e5a9" ForeColor="#000000" />
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
        <div class="botoesroll">
            <asp:LinkButton ID="Lnk_Cancelar" runat="server" PostBackUrl="~/Vacina/FormReceberRemanejamento.aspx">
                  <img id="imgvoltar" alt="Voltar" src="img/btn_voltar1.png"
                  onmouseover="imgvoltar.src='img/btn_voltar2.png';"
                  onmouseout="imgvoltar.src='img/btn_voltar1.png';" /></asp:LinkButton>
        </div>
    </div>
</asp:Content>
