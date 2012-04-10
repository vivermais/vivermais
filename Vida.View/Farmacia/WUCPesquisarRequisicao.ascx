﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WUCPesquisarRequisicao.ascx.cs"
    Inherits="ViverMais.View.Farmacia.WUCPesquisarRequisicao" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
  <link rel="stylesheet" href="style_form_farmacia.css" type="text/css" media="screen" />

 <fieldset class="formulario">
            <legend>Dados da busca</legend>
<p>
    <span class="rotulo">Farmácia</span>
    <span>
        <asp:DropDownList ID="DropDownList_Farmacia" runat="server" DataTextField="Nome" DataValueField="Codigo">
            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
        </asp:DropDownList>
    </span>
</p>
<p>
    <span class="rotulo">Número de Requisição</span><span>
        <asp:TextBox ID="TextBox_NumeroRequisicao" runat="server" CssClass="campo"></asp:TextBox>
    </span>
</p>
<p>
    <span class="rotulo">Data de Abertura</span><span>
        <asp:TextBox ID="TextBox_DataAbertura" runat="server" CssClass="campo"></asp:TextBox>
    </span>
</p>
<p>
    <span class="rotulo">Status</span><span>
        <asp:DropDownList ID="DropDownList_StatusRequisicao" runat="server">
            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
        </asp:DropDownList>
    </span>
</p>

    
       <div class="botoesroll">
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="OnClick_Pesquisar" CommandArgument="CommandArgument_PesquisarAlguns" ValidationGroup="ValidationGroup_PesquisarRM">
         <img id="imgpesquisa" alt="Nova RM" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" />
        </asp:LinkButton>
        </div>
        
       <div class="botoesroll">
        <asp:LinkButton ID="LinkButton3" runat="server" OnClick="OnClick_Pesquisar"  CommandArgument="CommandArgument_PesquisarTodas" ValidationGroup="ValidationGroup_PesquisarTodasRM">
        <img id="imglistatodos" alt="Nova RM" src="img/btn/listartodos1.png"
                  onmouseover="imglistatodos.src='img/btn/listartodos2.png';"
                  onmouseout="imglistatodos.src='img/btn/listartodos1.png';" />
        </asp:LinkButton>
       </div>
       
       <div class="botoesroll">
        <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OnClick_Limpar">
        <img id="imglimpar" alt="Nova RM" src="img/btn/limpar1.png"
                  onmouseover="imglimpar.src='img/btn/limpar2.png';"
                  onmouseout="imglimpar.src='img/btn/limpar1.png';" />
        </asp:LinkButton>
        </div>
    

<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
    </Triggers>
    <ContentTemplate>
        <asp:Panel ID="Panel_Pesquisa" runat="server" Visible="false">
            <br /><br /><br />
            <asp:GridView ID="GridView_Requisicoes" runat="server" AutoGenerateColumns="false"
                OnPageIndexChanging="OnPageIndexChanging_Requisicoes" AllowPaging="true" PageSize="20" PagerSettings-Mode="Numeric" BorderColor="Silver"
                                    Font-Size="X-Small" Width="100%">
                <Columns>
                    <asp:HyperLinkField HeaderText="Número"  DataTextField="Codigo" ItemStyle-ForeColor="Blue" DataNavigateUrlFormatString="~/Farmacia/FormItensRequisicaoMedicamentos.aspx?co_requisicao={0}" DataNavigateUrlFields="Codigo" />
                    <asp:BoundField DataField="NomeFarmacia" HeaderText="Farmácia" />
                    <asp:BoundField DataField="DataCriacao" HeaderText="Data de Abertura" DataFormatString="{0:dd/MM/yyyy}" />
                    <asp:BoundField DataField="Status" HeaderText="Status" />
                </Columns>
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Não foi encontrada requisição alguma com os dados informados."></asp:Label>
                </EmptyDataTemplate>
                <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#006666" ForeColor="White" HorizontalAlign="Center" />
            </asp:GridView>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>

</fieldset>
<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Selecione uma farmácia." Display="None" ControlToValidate="DropDownList_Farmacia" ValueToCompare="-1" Operator="GreaterThan" ValidationGroup="ValidationGroup_PesquisarRM"></asp:CompareValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Digite somente números no campo 'Número da Requisição'." ControlToValidate="TextBox_NumeroRequisicao" Display="None" ValidationGroup="ValidationGroup_PesquisarRM" ValidationExpression="^\d*$"></asp:RegularExpressionValidator>
<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Data de Abertura inválida." Display="None" ControlToValidate="TextBox_DataAbertura" Operator="DataTypeCheck" Type="Date" ValidationGroup="ValidationGroup_PesquisarRM"></asp:CompareValidator>
<asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="Data de Abertura deve ser igual ou maior que 01/01/1900." Display="None" ControlToValidate="TextBox_DataAbertura" Operator="GreaterThanEqual" ValueToCompare="01/01/1900" Type="Date" ValidationGroup="ValidationGroup_PesquisarRM"></asp:CompareValidator>

<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarRM"></asp:ValidationSummary>

<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="TextBox_DataAbertura">
</cc1:CalendarExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight" TargetControlID="TextBox_DataAbertura"
    Mask="99/99/9999" MaskType="Date">
</cc1:MaskedEditExtender>

<asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="Selecione uma farmácia." Display="None" ControlToValidate="DropDownList_Farmacia" ValueToCompare="-1" Operator="GreaterThan" ValidationGroup="ValidationGroup_PesquisarTodasRM"></asp:CompareValidator>
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_PesquisarTodasRM" />
