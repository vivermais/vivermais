<%@ Page Title="" Language="C#" MasterPageFile="~/Urgencia/MasterUrgencia.Master" AutoEventWireup="true"
    CodeBehind="FormEvolucao.aspx.cs" Inherits="Urgence.View.WebForm24" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="top">
        <h2>Formulário de Evolução</h2>
    
    <fieldset class="formulario">
        <legend>Dados do Registro Eletrônico de Atendimento</legend>
        <p>
            <span class="rotulo">N°:</span> <span style="margin-left: 5px;">
                <asp:Label ID="lblNumero" runat="server" Font-Bold="True" 
                ForeColor="Maroon"></asp:Label>
            </span>
        </p>
        <p>
            <span class="rotulo">Data:</span> 
            <span style="margin-left: 5px;">
                <asp:Label ID="lblData" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label></span></p>
        <p>
            <span class="rotulo">Paciente:</span> <span style="margin-left: 5px;">
                <asp:Label ID="lblPaciente" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label>
            </span>
        </p>
    </fieldset>
    <fieldset class="formulario">
        <legend>Registro de Evolução</legend>
        <p>
            <span class="rotulo">Registro de Evolução:</span> 
            <span style="margin-left: 5px;">
                <asp:TextBox ID="tbxObservacao" CssClass="campo" runat="server" Height="42px" Rows="3" TextMode="MultiLine"
                    Width="400px" MaxLength="200"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server" ControlToValidate="tbxObservacao"
                    ErrorMessage="RequiredFieldValidator" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </span>
        </p>
        <p>
            <span class="rotulo">Aprazamento: </span>
            <span style="margin-left: 5px;">
                <asp:TextBox ID="tbxAprazamento" CssClass="campo" runat="server" Height="42px" Rows="3" TextMode="MultiLine"
                    Width="400px" MaxLength="200"></asp:TextBox>
            </span>
        </p>
        <p>
            <span class="rotulo">Código de Identificação:</span>
            <span style="margin-left:5px;"><asp:TextBox ID="TextBox_CodigoIdentificacao" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBox_CodigoIdentificacao"
                ErrorMessage="RequiredFieldValidator" SetFocusOnError="true">*</asp:RequiredFieldValidator></span>
            
        </p>
        <p>
            <asp:Button ID="btnSalvar" runat="server" Text="Salvar"
                    Width="96px" OnClick="btnSalvar_Click1" />
        </p>
    </fieldset>
    <fieldset class="formulario">
        <legend>Histórico de Evoluções</legend>
        <p>
            <span>
                <asp:GridView ID="gridEvolucao" runat="server" AutoGenerateColumns="False" Width="640px">
                    <Columns>
                        <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="NomeProfissionalToString" HeaderText="Profissional" ItemStyle-Width="200px" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacao" HeaderText="Evolução" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Aprazamento" HeaderText="Aprazamento" ItemStyle-HorizontalAlign="Center"/>
                    </Columns>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow_left" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </span>
        </p>
    </fieldset>
    <fieldset class="formulario">
        <legend>Histórico de Procedimentos</legend>
        <p>
            <span>
                <asp:GridView ID="gridProcedimento" runat="server" AutoGenerateColumns="False" Width="640px"
                    DataKeyNames="CodigoProcedimento">
                    <Columns>
                        <asp:BoundField DataField="Data" HeaderText="Data" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ProcedimentoToString" HeaderText="Procedimento" ItemStyle-Width="600px" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="Quantidade" HeaderText="Qtd" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle HorizontalAlign="Center" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Observacao" HeaderText="Observação" ItemStyle-Width="600px" ItemStyle-HorizontalAlign="Center"/>
                    </Columns>
                    <HeaderStyle CssClass="tab" />
                    <RowStyle CssClass="tabrow_left" />
                    <EmptyDataTemplate>
                        <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
                    </EmptyDataTemplate>
                </asp:GridView>
            </span>
        </p>
    </fieldset>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
