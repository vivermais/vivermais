<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormBuscaDispensacao.aspx.cs"
    Inherits="ViverMais.View.Farmacia.FormBuscaDispensacao" EnableEventValidation="false"
    MasterPageFile="~/Farmacia/MasterFarmacia.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>
            Buscar Receita</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gridView_ResultadoReceitas" EventName="PageIndexChanging" />
            <asp:AsyncPostBackTrigger ControlID="GridViewAtendimentos" EventName="DataBound" />                
            </Triggers>
            <ContentTemplate>
                <fieldset class="formulario">
                    <legend>Pesquisa</legend>
                    <p>
                        <asp:RadioButtonList ID="rblPesquisaDispensacao" runat="server" CssClass="radio"
                            AutoPostBack="True" RepeatDirection="Horizontal" TextAlign="Left" CellPadding="0"
                            CellSpacing="0" OnSelectedIndexChanged="rblPesquisaDispensacao_SelectedIndexChanged"
                            Width="100px">
                            <asp:ListItem Value="R">Receita</asp:ListItem>
                            <asp:ListItem Value="P">Paciente</asp:ListItem>
                        </asp:RadioButtonList>
                    </p>
                    <asp:Panel ID="panelReceita" runat="server" Visible="false">
                        <p>
                            <span class="rotulo">N� da Receita</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxReceita" runat="server" CssClass="campo"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="RightToLeft"
                                    TargetControlID="tbxReceita" Mask="99999999999" MaskType="Number" AutoComplete="false">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:Panel ID="panelPaciente" runat="server" Visible="false">
                        <p>
                            <span>
                                <asp:LinkButton ID="lnkBiometria" runat="server" PostBackUrl="~/Farmacia/FormBiometriaGerarReceita.aspx">
                                <img id="img_newbiometria" alt="" src="../img/bts/bts_ident_bio.png"
                                onmouseover="img_newbiometria.src='../img/bts/bts_ident_bio_b.png';"
                                onmouseout="img_newbiometria.src='../img/bts/bts_ident_bio.png';" />
                                </asp:LinkButton>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Cart�o SUS</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" runat="server" MaxLength="15"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxNomePaciente" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome da M�e</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxNomeMae" CssClass="campo" runat="server"></asp:TextBox>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Nascimento</span> <span style="margin-left: 5px;">
                                <asp:TextBox ID="tbxDataNascimento" CssClass="campo" runat="server"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                                    TargetControlID="tbxDataNascimento" Mask="99/99/9999" MaskType="Date">
                                </cc1:MaskedEditExtender>
                            </span>
                        </p>
                    </asp:Panel>
                    <asp:LinkButton ID="btnBuscarPaciente" runat="server" ValidationGroup="group_pesquisaPaciente"
                        OnClick="OnClick_PesquisarDispensacao">
                  <img id="imgbuscapaciente" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgbuscapaciente.src='img/btn/pesquisar2.png';"
                  onmouseout="imgbuscapaciente.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Data de Nascimento com formato inv�lido!"
                        ControlToValidate="tbxDataNascimento" Operator="DataTypeCheck" Type="Date" ValidationGroup="group_pesquisaPaciente"
                        Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Nascimento deve ser igual ou maior que 01/01/1900!"
                        ControlToValidate="tbxDataNascimento" Operator="GreaterThanEqual" ValueToCompare="01/01/1900"
                        Type="Date" ValidationGroup="group_pesquisaPaciente" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="group_pesquisaPaciente" />
                </fieldset>
                <div id="Validadores">
                    <asp:CustomValidator ID="CustomValidator_PesquisaBusca" runat="server" OnServerValidate="OnServerValidate_PesquisaDispensacao"
                        Display="None" ValidationGroup="ValidationGroup_PesquisarDispensacao">
                    </asp:CustomValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                        ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarDispensacao" />
                </div>
                <asp:Panel ID="PanelResultadoPaciente" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Resultado</legend>
                        <p>
                            <span>
                                <asp:GridView ID="GridView_ResultadoPesquisaPaciente" runat="server" AutoGenerateColumns="false"
                                    AllowPaging="true" PageSize="20" OnPageIndexChanging="OnPageIndexChanging_PaginacaoPaciente"
                                    DataKeyNames="Codigo" PagerSettings-Mode="Numeric" Width="700px" BorderColor="Silver"
                                    Font-Size="X-Small">
                                    <Columns>
                                        <asp:BoundField HeaderText="Codigo" DataField="Codigo">
                                            <ItemStyle Width="315px" HorizontalAlign="Left" VerticalAlign="Bottom" CssClass="colunaEscondida" />
                                            <HeaderStyle CssClass="colunaEscondida" />
                                            <FooterStyle CssClass="colunaEscondida" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Nome" DataField="Nome">
                                            <ItemStyle Width="315px" HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="M�e" DataField="NomeMae">
                                            <ItemStyle Width="265px" HorizontalAlign="Left" VerticalAlign="Bottom" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Data de Nascimento" DataField="DataNascimento" DataFormatString="{0:dd/MM/yyyy}">
                                            <ItemStyle Width="110px" HorizontalAlign="Center" VerticalAlign="Bottom" />
                                        </asp:BoundField>
                                        <asp:TemplateField ItemStyle-Width="10px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="cmdSelect" CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'
                                                    runat="server" OnClick="OnClick_SelecionarPaciente" Width="10px" CausesValidation="false">
                                                    <asp:Image ID="imgSelect" ImageUrl="~/Farmacia/img/bt_edit.png" runat="server" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                    </EmptyDataTemplate>
                                    <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" />
                                    <PagerStyle BackColor="#006666" ForeColor="White" HorizontalAlign="Center" />
                                </asp:GridView>
                            </span>
                        </p>
                    </fieldset></asp:Panel>
                <asp:Panel ID="PanelResultado" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Resultado da busca</legend>
                        <p>
                            <span class="rotulo">Nome</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblNome" runat="server" Text="" CssClass="label"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Nome da M�e</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblNomeMae" runat="server" Text="" CssClass="label"></asp:Label>
                            </span>
                        </p>
                        <p>
                            <span class="rotulo">Data de Nascimento</span> <span style="margin-left: 5px;">
                                <asp:Label ID="lblNascimento" runat="server" Text="" CssClass="label"></asp:Label>
                            </span>
                        </p>
                        <asp:GridView ID="gridView_ResultadoReceitas" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="Codigo" PagerSettings-Mode="Numeric" PagerSettings-Visible="true"
                            VerticalAlign="Bottom" ForeColor="#333333" Width="700px" Font-Size="X-Small"
                            OnRowCommand="gridView_ResultadoReceitas_RowCommand" 
                            OnPageIndexChanging="gridView_ResultadoReceitas_PageIndexChanged" >
                            <RowStyle BackColor="#EFF3FB" Height="20px" />
                            <Columns>
                                <asp:BoundField HeaderText="N� da Receita" DataField="Codigo">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                                <asp:BoundField DataField="DataReceita" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data da Receita">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                                <asp:ButtonField CommandName="VisualizarAtendimentos" Text="Listar Atendimentos"
                                    ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="100px" 
                                    ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="100px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="VisualizarMedicamentos" Text="Listar Medicamentos"
                                    ItemStyle-VerticalAlign="Bottom" ItemStyle-Width="100px" 
                                    ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="100px" />
                                </asp:ButtonField>
                                <asp:ButtonField CommandName="NovoAtendimento" Text="Novo Atendimento" ItemStyle-VerticalAlign="Bottom"
                                    ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" >
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Bottom" Width="100px" />
                                </asp:ButtonField>
                                <asp:HyperLinkField DataNavigateUrlFields="Codigo" 
                                    DataNavigateUrlFormatString="~/Farmacia/FormGerarReceitaDispensacao.aspx?cod_receita={0}" 
                                    Text="Editar Receita" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="N�o foram encontradas receitas cadastradas para este paciente."></asp:Label>
                            </EmptyDataTemplate>
                            <PagerStyle BackColor="#006666" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </fieldset></asp:Panel>
                <asp:Panel ID="PanelMedicamentos" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Medicamentos Prescritos</legend>
                        <asp:GridView ID="GridView_Medicamentos" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="CodMedicamento" Font-Size="X-Small" ForeColor="#333333">
                            <RowStyle BackColor="#EFF3FB" Height="20px" Width="700px" />
                            <Columns>
                                <asp:BoundField DataField="CodMedicamento" HeaderText="C�digo" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="center" Width="150px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NomeMedicamento" HeaderText="Nome" ReadOnly="True">
                                    <ItemStyle HorizontalAlign="Left" Width="400px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                                <asp:BoundField DataField="QtdPrescrita" HeaderText="Quantidade">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="N�o existem medicamentos prescritos para esta receita."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </fieldset></asp:Panel>
                <asp:Panel ID="PanelAtendimento" runat="server" Visible="false">
                    <fieldset class="formulario">
                        <legend>Atendimento</legend>
                        <asp:GridView ID="GridViewAtendimentos" runat="server" AutoGenerateColumns="False" DataKeyNames="Codigo"
                            AllowPaging="True" VerticalAlign="Bottom" PagerSettings-Mode="Numeric" PagerSettings-Visible="true"
                            Width="700px" Font-Size="X-Small" ForeColor="#333333" 
                            OnPageIndexChanging="GridViewAtendimentos_PageIndexChanged" 
                            onrowcommand="GridViewAtendimentos_RowCommand1"
                            OnDataBound="GridViewAtendimentos_DataBound">
                            <RowStyle BackColor="#EFF3FB" Height="20px" />
                            <Columns>
                                <asp:HyperLinkField DataNavigateUrlFields="CodigoReceita,Codigo" DataNavigateUrlFormatString="FormDispensarMedicamentos.aspx?co_dispensacao={1}"
                                    DataTextField="NomeFarmacia" ItemStyle-Width="500px">
                                    <ItemStyle Width="500px" />
                                </asp:HyperLinkField>
                                <asp:ButtonField Text="Medicamentos Dispensados" CommandName="VisualizarMedicamentos"  />
                                <asp:BoundField DataField="DataAtendimento" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                                    HeaderText="Atendimento">
                                    <ItemStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Bottom" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="N�o existem dispensa��es para esta receita."></asp:Label>
                            </EmptyDataTemplate>
                            <PagerStyle BackColor="#006666" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="GridViewMedicamentosDispensados" runat="server" 
                            AutoGenerateColumns="False" Width="700px" Font-Size="X-Small" ForeColor="#333333">
                            <RowStyle BackColor="#EFF3FB" Height="20px" />
                            <Columns>
                                <asp:BoundField DataField="Medicamento" HeaderText="Medicamento" />
                                <asp:BoundField DataField="QtdDispensada" HeaderText="Quantidade Dispensada" />
                                <asp:BoundField DataField="QtdDias" HeaderText="Quandidade de Dias" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lbEmpty" runat="server" Text="N�o foram dispensados medicamentos."></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle BackColor="#006666" Font-Bold="True" ForeColor="White" />
                        </asp:GridView>
                    </fieldset></asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>