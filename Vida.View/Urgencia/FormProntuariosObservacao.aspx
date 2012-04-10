<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormProntuariosObservacao.aspx.cs" Inherits="ViverMais.View.Urgencia.FormProntuariosObservacao" EnableEventValidation="false" MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="top">
    <h2>Pacientes em Observação</h2>
    <fieldset>
        <legend>Relação</legend>
        <p>
            <asp:GridView ID="GridView_Prontuarios" runat="server" OnRowDataBound="OnRowDataBound_FormataGrid"
             AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="OnSelectedIndexChanging_Paginacao"
             PageSize="20" PagerSettings-Mode="Numeric" Width="586px" >
             <Columns>
                <asp:TemplateField HeaderText="Identificador" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl='<%#bind("CodigoProntuario","~/Urgencia/FormEvolucaoEnfermagem.aspx?codigo={0}") %>' Text='<%#bind("NumeroProntuario") %>'></asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:BoundField HeaderText="Paciente" DataField="NomePaciente" 
                     ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle HorizontalAlign="Center" Width="300px" />
                 </asp:BoundField>
                <asp:BoundField HeaderText="Descrição" DataField="PacienteDescricao" 
                     ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundField>
                <asp:TemplateField HeaderText="Classificação de Risco" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="Image_Classificacao" ImageUrl='<%#bind("ClassificacaoRisco") %>' runat="server" Width="32px" Height="32px"/>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" PostBackUrl='<%#bind("CodigoProntuario","~/Urgencia/FormEvolucaoMedica.aspx?codigo={0}") %>'>Acesso Médico</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" Width="80px" />
                </asp:TemplateField>
             </Columns>
             <EmptyDataTemplate>
                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado"></asp:Label>
             </EmptyDataTemplate>
             <HeaderStyle CssClass="tab" />
             <RowStyle CssClass="tabrow" />
            </asp:GridView>
        </p>
    </fieldset>
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>
