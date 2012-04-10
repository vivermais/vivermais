<%@ Page Language="C#" MasterPageFile="~/EnvioBPA/Main.Master" AutoEventWireup="True"
    CodeBehind="ConsultaProtocolo.aspx.cs" Inherits="ViverMais.View.EnvioBPA.ConsultaProtocolo"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <div id="top">
    <h2>
        Consulta Protocolo
    </h2>
    
    <fieldset>

 <%-- <span class="rotulo">
        Login:
        </span>
        <span>
        <asp:Label ID="lblLogin" runat="server" Text="" CssClass="campo"></asp:Label>
        </span>--%>
   
    
    <p>
    <span class="rotulo">
        Protocolo:
        </span>
        
        <asp:TextBox ID="tbxProtocolo" runat="server" CssClass="campo"></asp:TextBox>
        
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            Font-Size="XX-Small" runat="server"
                            ControlToValidate="tbxProtocolo" Display="Dynamic" ErrorMessage="* Informe o Protocolo"
                            SetFocusOnError="True"></asp:RequiredFieldValidator>
        
    </p>
    <p>
        <asp:GridView ID="GridViewProtocolo" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Protocolo" />
                <asp:BoundField DataField="EstabelecimentoSaude" HeaderText="Unidade" />
                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                <asp:BoundField DataField="DataEnvio" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Data Envio" />
                <asp:BoundField DataField="Competencia" HeaderText="Competência" />
                <asp:HyperLinkField DataNavigateUrlFields="Arquivo" DataNavigateUrlFormatString="/EnvioBPA/upload/{0}"
                    DataTextField="Arquivo" HeaderText="Arquivo" />
            </Columns>
            <EmptyDataTemplate>
                <asp:Label ID="lbEmpty" runat="server" Text="Nenhum Registro foi Encontrado."></asp:Label>
            </EmptyDataTemplate>
        </asp:GridView>
    </p>
    <p>
    <div class="botoesroll">
        <asp:LinkButton ID="imgbtnEnviar" runat="server" OnClick="imgbtnEnviar_Click">
                    <img id="img_enviar" alt="" src="img/pesquisar_1.png"
                onmouseover="img_enviar.src='img/pesquisar_2.png';"
                onmouseout="img_enviar.src='img/pesquisar_1.png';"/>
        </asp:LinkButton>
        </div>
        <div class="botoesroll">
             <asp:LinkButton ID="LinkButton1" runat="server" 
                 PostBackUrl="~/EnvioBPA/RelatoriosAdministrativos.aspx" 
                 CausesValidation="False">
                    <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';"/>
        </asp:LinkButton>
        </div>
    </p>
    </fieldset>
    </div>
</asp:Content>
