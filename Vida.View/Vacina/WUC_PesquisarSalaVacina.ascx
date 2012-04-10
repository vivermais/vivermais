﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUC_PesquisarSalaVacina.ascx.cs"
    Inherits="ViverMais.View.Vacina.WUC_PesquisarSalaVacina" %>
<p>
    <span class="rotulo">Nome</span> <span>
        <asp:TextBox ID="TextBox_Nome" runat="server" CssClass="campo" MaxLength="200" Width="350px"></asp:TextBox>
        <asp:LinkButton ID="LinkButton_PesquisarNome" OnClick="OnClick_PesquisarNome" runat="server">
            <img src="img/procurar.png" alt="Pesquisar Por Nome" id="pequisanome" />
        </asp:LinkButton>
    </span>
</p>
<p>
    <span>
        <asp:LinkButton ID="LinkButton_ListarTodos" runat="server" OnClick="OnClick_ListarTodos">
                <img id="imglistar" alt="Listar Todos" src="img/btn_listar_todos1.png"
                  onmouseover="imglistar.src='img/btn_listar_todos2.png';"
                  onmouseout="imglistar.src='img/btn_listar_todos1.png';" />
        </asp:LinkButton>
    </span>
</p>
<asp:RequiredFieldValidator ID="RequiredFieldValidator_Nome" runat="server" ErrorMessage="Nome é Obrigatório."
    Display="None" ControlToValidate="TextBox_Nome"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator_Nome" runat="server"
    ErrorMessage="Informe pelo menos as duas primeiras letras para o nome da sala de vacina."
    ControlToValidate="TextBox_Nome" ValidationExpression="^\S{2}[\W-\w]*$" Display="None"></asp:RegularExpressionValidator>
<asp:ValidationSummary ID="ValidationSummary_Nome" runat="server" ShowMessageBox="true"
    ShowSummary="false" />