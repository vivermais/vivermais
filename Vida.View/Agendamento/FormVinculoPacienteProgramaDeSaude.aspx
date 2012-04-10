<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormVinculoPacienteProgramaDeSaude.aspx.cs" Inherits="ViverMais.View.Agendamento.FormVinculoPacienteProgramaDeSaude"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Vínculo de Procedimento ao Programas de Saúde</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Programa de Saúde</span> <span>
                    <asp:DropDownList ID="ddlProgramaDeSaude" runat="server" CssClass="drop" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProgramaDeSaude_SelectedIndexChanged" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Forma de cadastro de paciente</span>
                <asp:RadioButtonList ID="rbtnFormaCadastro" runat="server" CssClass="radio" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rbtnFormaCadastro_SelectedIndexChanged">
                    <asp:ListItem Text="Arquivo(*.xls)" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Pesquisa de Paciente" Value="P"></asp:ListItem>
                </asp:RadioButtonList>
            </p>
            <asp:Panel ID="PanelPesquisaPaciente" runat="server" Visible="false">
                <fieldset class="formulario">
                    <legend>Pesquisa</legend>
                    <h4>
                        Selecione a forma de pesquisa do Cartão Municipal de Saúde.</h4>
                    <p style="padding-right: 40px;">
                        &nbsp;<cc1:Accordion ID="Accordion1" runat="server" SelectedIndex="-1" RequireOpenedPane="false"
                            HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
                            ContentCssClass="accordionContent" OnItemCommand="Accordion1_ItemCommand">
                            <HeaderTemplate>
                            </HeaderTemplate>
                            <ContentTemplate>
                            </ContentTemplate>
                            <Panes>
                                <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                    <Header>
                                        Dados do Paciente</Header>
                                    <Content>
                                        <p>
                                            <span class="rotulo">Nome Completo</span> <span>
                                                <asp:TextBox ID="tbxNome" CssClass="campo" runat="server" Width="200px" MaxLength="70"></asp:TextBox>
                                            </span>
                                            <asp:CustomValidator ID="CustomValidatorNomePaciente" Font-Size="XX-Small" runat="server"
                                                Display="Dynamic" ErrorMessage="Digite pelo menos o primeiro Nome e Sobrenome do Paciente"
                                                OnServerValidate="CustomValidatorNomePaciente_ServerValidate"></asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                                                runat="server" ErrorMessage="Existem Caracteres inválidos no Nome do Paciente"
                                                ControlToValidate="tbxNome" ValidationExpression="^[a-zA-Z\s]{1,40}$" Font-Size="XX-Small"></asp:RegularExpressionValidator>
                                        </p>
                                        <p>
                                            <span class="rotulo">Nome da Mãe</span> <span>
                                                <asp:TextBox ID="tbxNomeMae" CssClass="campo" runat="server" Width="200px" MaxLength="70"></asp:TextBox>
                                                <asp:CustomValidator ID="CustomValidatorNomeMae" runat="server" Display="Dynamic"
                                                    ErrorMessage="Digite pelo menos um Nome e Sobrenome da Mãe do Paciente" OnServerValidate="CustomValidatorNomeMae_ServerValidate"
                                                    Font-Size="XX-Small"></asp:CustomValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Font-Size="XX-Small"
                                                    Display="Dynamic" runat="server" ErrorMessage="Existem Caracteres inválidos no Nome da Mãe Paciente"
                                                    ControlToValidate="tbxNomeMae" ValidationExpression="^[a-zA-Z\s]{1,40}$"></asp:RegularExpressionValidator>
                                            </span>
                                        </p>
                                        <p>
                                            <span class="rotulo">Data de Nascimento</span> <span>
                                                <asp:TextBox ID="tbxDataNascimento" CssClass="campo" runat="server" Width="100px"></asp:TextBox>
                                                <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" TargetControlID="tbxDataNascimento"
                                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                                                </cc1:MaskedEditExtender>
                                                <asp:CustomValidator ID="CustomValidator_DataNascimento" runat="server" ClientValidationFunction="ValidarData"
                                                    ControlToValidate="tbxDataNascimento" ErrorMessage="A data de Nascimento não parece ser válida."
                                                    Font-Size="XX-Small">
                                                </asp:CustomValidator>
                                                <%--<asp:CompareValidator ID="compareData" runat="server" ControlToValidate="tbxDataNascimento"
                                            Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data de Nascimento inválida"
                                            Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                                            </span>
                                        </p>
                                    </Content>
                                </cc1:AccordionPane>
                                <cc1:AccordionPane ID="AccordionPane2" runat="server">
                                    <Header>
                                        Número do Cartão SUS</Header>
                                    <Content>
                                        <p>
                                            <span class="rotulo">Número do Cartão SUS</span> <span>
                                                <asp:TextBox ID="tbxCartaoSUS" CssClass="campo" runat="server" MaxLength="15"></asp:TextBox>
                                            </span>
                                            <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server"
                                                ControlToValidate="tbxCartaoSUS" Display="Dynamic" ErrorMessage="O campo Cartão SUS deve conter apenas Números"
                                                Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                                        </p>
                                    </Content>
                                </cc1:AccordionPane>
                                <cc1:AccordionPane ID="AccordionPane3" runat="server">
                                    <Header>
                                        Identificação Biométrica</Header>
                                    <Content>
                                        <p style="padding-left: 0px; margin-bottom: 15px; padding-bottom: 40px;">
                                            <asp:LinkButton ID="lnkBiometria" runat="server" PostBackUrl="~/Paciente/Biometria.aspx">
                                    <img id="img_biometria" alt="" src="../img/bts/id_biometrica.png" 
                                    onmouseover="img_biometria.src='../img/bts/id_biometrica2.png';" 
                                    onmouseout="img_biometria.src='../img/bts/id_biometrica.png';"  />
                                            </asp:LinkButton>
                                        </p>
                                        <p>
                                            &nbsp
                                        </p>
                                        <p>
                                            &nbsp
                                        </p>
                                    </Content>
                                </cc1:AccordionPane>
                            </Panes>
                        </cc1:Accordion>
                    </p>
                    <div class="botoesroll">
                        <asp:ImageButton ID="btnPesquisar" runat="server" CssClass="sep_buttons" OnClick="btnPesquisar_Click1"
                            ImageUrl="~/Paciente/img/bts/buscar_1.png" Width="134px" Height="38px" />
                        <asp:ImageButton ID="btnVoltar" runat="server" PostBackUrl="~/Home.aspx" CausesValidation="False"
                            ImageUrl="~/Paciente/img/bts/voltar_1.png" Width="134px" Height="38px" /></div>
                    <asp:CustomValidator ID="CustomValidatorRequired" Font-Size="XX-Small" runat="server"
                        Display="Dynamic" ErrorMessage="Preencha um dos critérios de Busca" OnServerValidate="CustomValidatorRequired_ServerValidate"></asp:CustomValidator>
                    <asp:CustomValidator ID="CustomValidatorCampos" Font-Size="XX-Small" runat="server"
                        Display="Dynamic" ErrorMessage="Digite o Nome da Mãe ou a Data de Nascimento do Paciente"
                        OnServerValidate="CustomValidatorCampos_ServerValidate"></asp:CustomValidator>
                    <br />
                    <br />
                    <p>
                        <asp:GridView ID="GridViewPacientes" runat="server" AutoGenerateColumns="False" Width="100%"
                            DataKeyNames="Codigo">
                            <Columns>
                                <asp:BoundField DataField="Nome" HeaderText="Nome do Paciente" />
                                <asp:BoundField DataField="NomeMae" HeaderText="Nome da Mãe" />
                                <asp:BoundField DataField="DataNascimento" ItemStyle-HorizontalAlign="Center" HeaderText="Data de Nascimento"
                                    DataFormatString="{0:dd/MM/yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <
                                <%--<asp:BoundField DataField="CartaoSUS" HeaderText="Cartão SUS" />--%>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="lblMensagem" Font-Size="X-Small" runat="server" ForeColor="Red" Text="Nenhum paciente foi localizado com os dados fornecidos"></asp:Label>
                            </EmptyDataTemplate>
                            <HeaderStyle CssClass="tab" />
                            <RowStyle CssClass="tabrow" />
                        </asp:GridView>
                        <%--                    </ContentTemplate>
                </asp:UpdatePanel>--%>
                        <%--<asp:Label ID="lblMensagem" Font-Size="X-Small" runat="server" ForeColor="Red" Text="Nenhum paciente foi localizado com os dados fornecidos"></asp:Label>--%>
                    </p>
                </fieldset>
            </asp:Panel>
            <asp:Panel ID="PanelImportacao" runat="server" Visible="false">
                <asp:FileUpload ID="FileUploadCNS" runat="server" CssClass="campo" Height="21px"
                    Width="288px" />
                <div class="botoesroll">
                    <asp:LinkButton ID="btnFazerUpload" runat="server" OnClick="btnFazerUpload_Click">
                            <img id="imgimportar" alt="Salvar" src="img/importar_1.png"
                            onmouseover="imgimportar.src='img/importar_2.png';"
                            onmouseout="imgimportar.src='img/importar_1.png';"  />
                    </asp:LinkButton>
                </div>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>
