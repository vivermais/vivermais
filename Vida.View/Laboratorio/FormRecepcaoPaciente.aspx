<%@ Page Language="C#" MasterPageFile="~/Laboratorio/MasterLaboratorio.Master" AutoEventWireup="True"
    CodeBehind="FormRecepcaoPaciente.aspx.cs" Inherits="ViverMais.View.Laboratorio.FormRecepcaoPaciente"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
        <h2>Recepção do Paciente</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Unidade de Coleta:</span> <span>
                    <asp:DropDownList ID="ddlUnidadeColeta" CssClass="drop" runat="server" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <span class="rotulo">Nº atendimento:</span> <span>
                    <asp:TextBox ID="txtNumeroAtendimento" runat="server" Text="" CssClass="campo" />
                </span>
            </p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtCartaoSUS" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtRG" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtNomeUsual" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtNumeroConselhoSolicitante" 
                        EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtPesquisaNomeSolicitante" 
                        EventName="TextChanged" />
                </Triggers>
                <ContentTemplate>
                    <p class="titulo1">
                        Digite o cartão SUS ou RG para pesquisar um paciente</p>
                    <p>
                        <span class="rotulo">Cartão SUS:</span> <span>
                            <asp:TextBox ID="txtCartaoSUS" runat="server" Text="" CssClass="campo" OnTextChanged="txtCartaoSUS_TextChanged"
                                AutoPostBack="True" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nº RG:</span> <span>
                            <asp:TextBox ID="txtRG" runat="server" Text="" CssClass="campo" OnTextChanged="txtRG_TextChanged"
                                AutoPostBack="True" />
                            <asp:LinkButton ID="LinkButton1" runat="server">
                <img id="add" alt="Cadastrar Novo Paciente" src="img/add.png" />
                            </asp:LinkButton>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nome do Paciente:</span> <span>
                            <asp:TextBox ID="txtNome" runat="server" Text="" CssClass="campo" Width="300px" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Sexo:</span> <span>
                            <asp:TextBox ID="txtSexo" runat="server" Text="" CssClass="campo" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Idade:</span> <span>
                            <asp:TextBox ID="txtIdade" runat="server" Text="" CssClass="campo" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Telefone:</span> <span>
                            <asp:TextBox ID="txtTelefone" runat="server" Text="" CssClass="campo" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Endereço:</span> <span>
                            <asp:TextBox ID="txtEndereco" runat="server" Text="" CssClass="campo" Width="400px" />
                            <asp:LinkButton ID="LinkButton2" runat="server">
                <img id="altlog" alt="Alterar Logradouro do paciente" src="img/bt_alt_log.png" />
                            </asp:LinkButton>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Nome do Responsável:</span> <span>
                            <asp:TextBox ID="txtResponsavel" runat="server" Text="" CssClass="campo" Width="300px" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Local de Entrega:</span> <span>
                            <asp:DropDownList ID="ddlLocalEntrega" CssClass="drop" runat="server" DataTextField="Nome"
                                DataValueField="Codigo">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Solicitante:</span> <span>
                            <asp:TextBox ID="txtNumeroConselhoSolicitante" runat="server" Text="" CssClass="campo"
                                Width="80px" AutoPostBack="True" OnTextChanged="txtNumeroConselhoSolicitante_TextChanged" /></span>
                        <span>
                            <asp:DropDownList ID="ddlSolicitante" runat="server" CssClass="drop">
                            </asp:DropDownList>
                        </span>
                        <asp:LinkButton ID="LinkButton3" runat="server">
                <img id="abrirpesq" alt="Abrir e pesquisar na tabela de profissionais" src="img/bt_pesq_abrir.png" />
                        </asp:LinkButton>
                    </p>
                    <asp:Panel ID="PanelBuscaSolicitante" runat="server" Visible="false">
                        <asp:TextBox ID="txtPesquisaNomeSolicitante" runat="server" AutoPostBack="True"  
                            ontextchanged="txtPesquisaNomeSolicitante_TextChanged"></asp:TextBox>
                        <asp:GridView ID="gdvSolicitantes" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False">
                            <Columns>
                                <asp:BoundField DataField="Nome" HeaderText="Nome" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                    <p>
                        <span class="rotulo">Mnemônico:</span> <span>
                            <asp:TextBox ID="txtMnemonico" runat="server" Text="" CssClass="campo" AutoPostBack="True"
                                OnTextChanged="txtMnemonico_TextChanged" />
                        </span>
                    </p>
                    <p>
                        <span class="rotulo" style="font-weight:bold">Nome Usual:</span> <span>
                            <asp:TextBox ID="txtNomeUsual" runat="server" Text="" CssClass="campo" AutoPostBack="True"
                                OnTextChanged="txtNomeUsual_TextChanged" />
                        </span>
                          </p>
                          <br />
                    <div style="float:left; width:200px">
                        <span class="rotulo" style="margin-left:0px; width:189px; background-color:#0b9496; border-color:#0b9496; font-weight:bold; font-size:11px";>Exames Encontrados</span><br /> 
                        
                        <span>
                            <asp:ListBox ID="lbxExamesEncontrados" runat="server" DataTextField="SgNomeUsual"
                                DataValueField="Codigo" Width="200px" CssClass="listboxx" ></asp:ListBox>
                        </span>
                        </div>
                        <div style="float:left; width:40px; margin-left:20px; margin-right:10px; margin-top:20px;">
                        <div class="botoesroll">
                        <asp:LinkButton ID="imgButtonVai" runat="server" AlternateText="ImagemSetaDireita"
                            OnClick="imgButtonVai_Click">
                            <img id="imgright" alt="" src="img/right1.png"
                onmouseover="imgright.src='img/right2.png';"
                onmouseout="imgright.src='img/right1.png';" />
                            </asp:LinkButton>
                            </div>
                            <div class="botoesroll">
                        <asp:LinkButton ID="imgButtonVolta" runat="server" AlternateText="ImagemSetaEsquerda"
                            OnClick="imgButtonVolta_Click" Width="16px">
                            <img id="imgleft" alt="" src="img/left1.png"
                onmouseover="imgleft.src='img/left2.png';"
                onmouseout="imgleft.src='img/left1.png';" />
                            </asp:LinkButton>
                            </div>
                            </div>
                
                    <div style="float:left; width:200px" >
                        <span class="rotulo" style="margin-left:0px; width:189px; background-color:#0b9496; border-color:#0b9496; font-weight:bold; font-size:11px">Exames Solicitados</span>
                        <br />
                         <span>
                            <asp:ListBox ID="lbxExamesSelecionados" runat="server" Width="200px" CssClass="listboxx"></asp:ListBox>
                        </span>
                    </div>
                    <br />
                    <br />
                    <p>
                        <span class="rotulo">Urgente:</span> <span>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                                CssClass="radio1">
                                <asp:ListItem Value="1">Sim</asp:ListItem>
                                <asp:ListItem Selected="True" Value="0">Não</asp:ListItem>
                            </asp:RadioButtonList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <div class="botoesroll">
                <asp:LinkButton ID="btnAdicionar" runat="server" Text="Adicionar">
                 <img id="imgadiciona" alt="" src="img/adicionar1.png"
                onmouseover="imgadiciona.src='img/adicionar2.png';"
                onmouseout="imgadiciona.src='img/adicionar1.png';" />
                </asp:LinkButton>
                </div>
                
                <div class="botoesroll">
                <asp:LinkButton ID="btnNovoAtendimento" runat="server" Text="Novo Atendimento">
                <img id="imgnovoatende" alt="" src="img/novo-atendimento1.png"
                onmouseover="imgnovoatende.src='img/novo-atendimento2.png';"
                onmouseout="imgnovoatende.src='img/novo-atendimento1.png';" />
                </asp:LinkButton>
                </div>
           
        </fieldset>
    </div>
</asp:Content>
