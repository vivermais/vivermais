<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormUnidadeMedidaMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormUnidadeMedidaMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="head" ID="c_head"  runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="ContentPlaceHolder1" ID="c_body" runat="server">
    <asp:UpdatePanel ID="UpdatePanel_Body" runat="server">
        <ContentTemplate>
            <div id="top">
                <h2>Formulário para Cadastro de Unidade de Medida</h2>
           
            <fieldset class="formulario">
                <legend>Unidade de Medida</legend>
                <p>
                    <span class="rotulo">Nome</span>
                    <span style="margin-left:5px;">
                        <asp:TextBox ID="TextBox_Nome" CssClass="campo" runat="server" Width="300px"></asp:TextBox>
                    </span>
                </p>
                <br />
                <p>
                    <span class="rotulo">Sigla</span>
                    <span style="margin-left:5px;">
                        <asp:TextBox ID="TextBox_Sigla" CssClass="campo" runat="server"></asp:TextBox>
                    </span>
                    
                </p>
                <br /><br />
                    <span>
                          <div class="botoesroll">
                           <asp:LinkButton ID="Button_Salvar" runat="server" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_Salvar" >
                  <img id="imgsalvar" alt="Salvar" src="img/btn/salvar1.png"
                  onmouseover="imgsalvar.src='img/btn/salvar2.png';"
                  onmouseout="imgsalvar.src='img/btn/salvar1.png';" /></asp:LinkButton>
                  </div>
                  <div class="botoesroll">
                           <asp:LinkButton ID="Button_Cancelar" runat="server" OnClick="OnClick_Salvar" PostBackUrl="~/Farmacia/Default.aspx" >
                  <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn/cancelar1.png';" /></asp:LinkButton>
                  </div>
                    </span>
                
            </fieldset>
            <div>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Nome é Obrigatório!" Display="None" ControlToValidate="TextBox_Nome" ValidationGroup="ValidationGroup_Salvar"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBox_Nome"
                        Display="None" ErrorMessage="Há caracters inválidos no Nome da Unidade de Medida." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_Salvar"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sigla é Obrigatório!" Display="None" ControlToValidate="TextBox_Sigla" ValidationGroup="ValidationGroup_Salvar"></asp:RequiredFieldValidator>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_Salvar" />
            </div>
             </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>