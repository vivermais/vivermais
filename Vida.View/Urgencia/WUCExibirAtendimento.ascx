<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCExibirAtendimento.ascx.cs"
    Inherits="ViverMais.View.Urgencia.WUCExibirAtendimento" %>
    
<fieldset class="formulario">
    <legend>Dados do Atendimento</legend>
    <p>
        <span class="rotulo">Número de Atendimento</span> <span>
            <asp:Label ID="Label_Numero" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Data de Atendimento</span> <span>
            <asp:Label ID="Label_Data" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Paciente</span> <span>
            <asp:Label ID="Label_Paciente" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Situação de Entrada</span> <span>
            <asp:Label ID="Label_Situacao" runat="server" Text="" Font-Bold="true" Font-Size="12px"></asp:Label>
        </span>
    </p>
    <p>
        <span class="rotulo">Descrição</span> <span>
            <asp:TextBox ID="TextBox_Descricao" runat="server" TextMode="MultiLine" Height="120px"
             Width="100%" CssClass="campo" ReadOnly="true" Font-Bold="true" Font-Size="12px"></asp:TextBox>
            <%--<asp:Label ID="Label_Descricao" runat="server" Text="" Font-Bold="true" Font-Size="Medium"></asp:Label>--%>
        </span>
    </p>
</fieldset>
