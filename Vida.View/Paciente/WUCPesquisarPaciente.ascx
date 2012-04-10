﻿<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="WUCPesquisarPaciente.ascx.cs"
    Inherits="ViverMais.View.Paciente.WUCPesquisarPaciente" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<fieldset class="formulario">
    <legend>Pesquisa</legend>
    <p>
        <strong>Selecione a forma de pesquisa do Cartão Municipal de Saúde.</strong></p>
    <p style="padding-right: 40px;">
        <cc1:Accordion ID="Accordion1" runat="server" RequireOpenedPane="false" HeaderCssClass="accordionHeader"
            HeaderSelectedCssClass="accordionHeaderSelected" ContentCssClass="accordionContent">
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
                                <asp:TextBox ID="tbxNome" CssClass="campo" runat="server" Width="200px"></asp:TextBox>
                            </span>
                        </p>
                        <%--<p>--%>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                            RenderMode="Inline">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:CustomValidator ID="CustomValidatorNomePaciente" Font-Size="XX-Small" runat="server"
                                    ValidationGroup="WUCPesquisarPaciente" Display="Dynamic" ErrorMessage="Digite o Nome e Sobrenome do Paciente"
                                    OnServerValidate="CustomValidatorNomePaciente_ServerValidate"></asp:CustomValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Display="Dynamic"
                            runat="server" ErrorMessage="Existem caracteres inválidos no Nome do Paciente"
                            ControlToValidate="tbxNome" ValidationExpression="^[a-zA-Z\s]{1,40}$" Font-Size="XX-Small"
                            ValidationGroup="WUCPesquisarPaciente"></asp:RegularExpressionValidator><%--</p>--%>
                        <p>
                            <span class="rotulo">Nome da Mãe</span> <span>
                                <asp:TextBox ID="tbxNomeMae" CssClass="campo" runat="server" Width="200px"></asp:TextBox>
                            </span>
                        </p>
                        <%--<p>--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
                                RenderMode="Inline">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:CustomValidator ID="CustomValidatorNomeMae" runat="server" Display="Dynamic"
                                        ErrorMessage="Digite o Nome e Sobrenome da Mãe do Paciente" OnServerValidate="CustomValidatorNomeMae_ServerValidate"
                                        Font-Size="XX-Small" ValidationGroup="WUCPesquisarPaciente"></asp:CustomValidator>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic"
                                runat="server" ErrorMessage="Existem Caracteres inválidos no Nome da Mãe Paciente"
                                ControlToValidate="tbxNomeMae" ValidationExpression="^[a-zA-Z\s]{1,40}$" ValidationGroup="WUCPesquisarPaciente"></asp:RegularExpressionValidator><%--</p>--%>
                        <p>
                            <span class="rotulo">Data de Nascimento</span> <span>
                                <asp:TextBox ID="tbxDataNascimento" CssClass="campo" runat="server" Width="100px"></asp:TextBox>
                                <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" TargetControlID="tbxDataNascimento"
                                    Mask="99/99/9999" MaskType="Date" InputDirection="LeftToRight" ClearMaskOnLostFocus="true">
                                </cc1:MaskedEditExtender>
                                <asp:CompareValidator ID="compareData" runat="server" ControlToValidate="tbxDataNascimento"
                                    Display="Dynamic" Font-Size="XX-Small" ErrorMessage="A data de Nascimento inválida"
                                    Operator="DataTypeCheck" Type="Date" ValidationGroup="WUCPesquisarPaciente"></asp:CompareValidator></span>
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
                                Operator="DataTypeCheck" Type="Double" ValidationGroup="WUCPesquisarPaciente"></asp:CompareValidator>
                        </p>
                    </Content>
                </cc1:AccordionPane>
                <cc1:AccordionPane ID="AccordionPane3" runat="server">
                    <Header>
                        Identificação Biométrica</Header>
                    <Content>
                        <p>
                            <asp:LinkButton ID="lnkBiometria" runat="server" PostBackUrl="~/Paciente/Biometria.aspx">
                                <img id="img_biometria" runat="server" alt="" src="../img/bts/id_biometrica.png"
                                    onmouseover="img_biometria.src='../img/bts/id_biometrica2.png';" onmouseout="img_biometria.src='../img/bts/id_biometrica.png';" />
                            </asp:LinkButton>
                        </p>
                    </Content>
                </cc1:AccordionPane>
            </Panes>
        </cc1:Accordion>
    </p>
    <p style="margin: 10px 0 10px 20px;">
        <asp:ImageButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click1" ImageUrl="~/img/bts/id_pesquisar.png"
            Height="89px" Width="80px" ValidationGroup="WUCPesquisarPaciente" />
        <%--<asp:LinkButton ID="btnPesquisar" runat="server" OnClick="btnPesquisar_Click1" ValidationGroup="WUCPesquisarPaciente" >
                                    <img id="img_pesquisar" alt="Pesquisar" src="../img/bts/id_pesquisar.png" 
                                    onmouseover="img_pesquisar.src='../img/bts/id_pesquisar.png';" 
                                    onmouseout="img_pesquisar.src='../img/bts/id_pesquisar.png';"  />
                            </asp:LinkButton>--%>
    </p>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true"
        RenderMode="Inline">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <p>
                <span>
                    <asp:CustomValidator ID="CustomValidatorRequired" Font-Size="XX-Small" runat="server"
                        Display="Dynamic" ErrorMessage="Preencha um dos critérios de Busca" OnServerValidate="CustomValidatorRequired_ServerValidate"
                        ValidationGroup="WUCPesquisarPaciente"></asp:CustomValidator>
                </span>
                <asp:CustomValidator ID="CustomValidatorCampos" Font-Size="XX-Small" runat="server"
                    Display="Dynamic" ErrorMessage="Digite o Nome da Mãe ou a Data de Nascimento do Paciente"
                    OnServerValidate="CustomValidatorCampos_ServerValidate" ValidationGroup="WUCPesquisarPaciente"></asp:CustomValidator>
            </p>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <p>
        <span>
            <asp:UpdatePanel ID="UpdatePanel_ResultadoPesquisa" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnPesquisar" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:GridView ID="GridViewPacientes" runat="server" AutoGenerateColumns="False" Width="100%"
                        OnSelectedIndexChanging="GridViewPacientes_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" ButtonType="Link" 
                                SelectText="<img src='../img/bts/select.png' alt=''/>" ControlStyle-Height="23"
                                ControlStyle-Width="22"  HeaderText="Selecionar">
                            </asp:CommandField>
                            <asp:HyperLinkField DataNavigateUrlFields="Codigo" DataNavigateUrlFormatString="FormPaciente.aspx?codigo={0}"
                                DataTextField="Nome" HeaderText="Nome do Paciente">
                                <ControlStyle ForeColor="Black" />
                                <ItemStyle ForeColor="Black" />
                            </asp:HyperLinkField>
                            <asp:BoundField DataField="NomeMae" HeaderText="Nome da Mãe" />
                            <asp:BoundField DataField="DataNascimento" ItemStyle-HorizontalAlign="Center" HeaderText="Data de Nascimento"
                                DataFormatString="{0:dd/MM/yyyy}">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataTemplate>
                            <p>
                                Nenhum paciente foi localizado com os dados fornecidos.</p>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="tab" />
                        <RowStyle CssClass="tabrow" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--<asp:Label ID="lblMensagem" Font-Size="X-Small" runat="server" ForeColor="Red" Text="Nenhum paciente foi localizado com os dados fornecidos"></asp:Label>--%>
        </span>
    </p>
</fieldset>
