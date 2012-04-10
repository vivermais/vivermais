﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormAtualizarUsuariosSenhador.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormAtualizarUsuariosSenhador" MasterPageFile="~/Urgencia/MasterUrgencia.Master" %>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <div id="top">
        <h2>Senhador
        </h2>
         <fieldset class="formulario"> 
         <legend>Atualização de Usuários</legend>
         <div>
      <h5>
            Para atualizar a lista de usuários com acesso ao senhador da unidade:
           
            <span style="font-weight:bold; color:#b51414">
            <asp:Label ID="Label_Unidade" runat="server" Text=""></asp:Label>
            </span><span style="font-weight:bold;">
            clique no botão abaixo.</span>
            </h5>
            
             <br />   <br />
            <asp:LinkButton ID="LinkButtonAtualizarUsuarios" runat="server"
             OnClick="OnClick_AtualizarUsuarios">
             <img id="imgatualizar" alt="Validar" src="img/btn-atualizar.png"
                onmouseover="imgatualizar.src='img/btn-atualizar2.png';"
                onmouseout="imgatualizar.src='img/btn-atualizar.png';" />
                </asp:LinkButton>
             
           
            <asp:UpdatePanel ID="UpdatePanel_UsuariosSenhador" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="LinkButtonAtualizarUsuarios" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbAtualizacaoIncompleta" runat="server" Visible="false"
                     Text="Atenção usuário, os profissionais listados abaixo não puderam ser atualizados! Por favor, tente novamente mais tarde."></asp:Label>
                        <asp:GridView ID="GridView_UsuariosNaoAtualizados" runat="server"
                         AutoGenerateColumns="false" Width="100%">
                         <Columns>
                            <asp:BoundField HeaderText="Nome" DataField="Nome" />
                            <asp:BoundField HeaderText="Cartão SUS" DataField="CartaoSUS" />
                            <asp:BoundField HeaderText="CBO" DataField="Especialidade" />
                         </Columns>
                         <HeaderStyle CssClass="tab" />
                         <RowStyle CssClass="tabrow" />
                         <EmptyDataRowStyle HorizontalAlign="Center" />
                         <EmptyDataTemplate>
                             <asp:Label ID="lbEmpty" runat="server" Text="Atualização realizada com sucesso."></asp:Label>
                         </EmptyDataTemplate>
                        </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        </fieldset>
    </div>
</asp:Content>