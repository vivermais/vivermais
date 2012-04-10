<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="FormFarmacia.aspx.cs" Inherits="ViverMais.View.Farmacia.FormFarmacia" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" runat="server" ContentPlaceHolderID="head">
</asp:Content>

<asp:Content ID="c_body" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="top">
        <h2>Formulário para Cadastro de Farmácia</h2>
        <fieldset class="formulario">
            <legend>Farmácia</legend>
            <p>
                 <p>
                <span class="rotulo">Estabelecimento de Saúde</span>
                <span>
                    <asp:DropDownList ID="DropDownList_Unidade" runat="server">
                        <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:Label ID="Label_Estabelecimento" runat="server" Text=""></asp:Label>--%>
                </span>
                </p>
                   <p>
                    <span class="rotulo">Farmácia</span>
                    <span>
                    <asp:TextBox ID="TextBox_Farmacia" runat="server" CssClass="campo" Width="300px" MaxLength="100"></asp:TextBox>                    </span>
                </p>
         
                <p>
                    <span class="rotulo">Endereço</span>
                    <span>
                    <asp:TextBox ID="TextBox_Endereco" runat="server" CssClass="campo" Width="300px" MaxLength="200"></asp:TextBox>
                    </span>
                </p>
            
                <p>
                    <span class="rotulo">Telefone</span> 
                    <span>
                    <asp:TextBox ID="TextBox_Telefone" runat="server" CssClass="campo"></asp:TextBox>
                    </span>
                </p>
          
                <p>
                    <span class="rotulo">Responsável</span> 
                    <span>
                    <asp:TextBox ID="TextBox_Responsavel" runat="server" CssClass="campo" Width="300px" MaxLength="100"></asp:TextBox>
                    </span>
                </p>
      
                   <div class="botoesroll">
                    <asp:LinkButton ID="Button_Salvar" runat="server" OnClick="OnClick_Salvar" 
                         ValidationGroup="ValidationGroup_cadFarmacia">
                         <img id="imgsalvar" alt="Salvar" runat="server"  />
                        </asp:LinkButton>
                        </div>
                        
                       <div class="botoesroll">
                    <asp:LinkButton ID="Button_Cancelar" runat="server" 
                        PostBackUrl="~/Farmacia/Default.aspx">
                         <img id="imgcancelar" alt="Cancelar"  src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                        </asp:LinkButton>
                        </div>
                        
                    </span>
               
                <p>
                    <asp:CompareValidator ID="CompareValidator1" runat="server"
                        ErrorMessage="Estabelecimento de Saúde é Obrigatório." Operator="GreaterThan"
                        ControlToValidate="DropDownList_Unidade" ValueToCompare="-1"
                        Display="None" ValidationGroup="ValidationGroup_cadFarmacia">
                    </asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="TextBox_Farmacia" Display="None" 
                        ErrorMessage="Farmácia é Obrigatório!" Font-Size="XX-Small" 
                        ValidationGroup="ValidationGroup_cadFarmacia"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="TextBox_Endereco" Display="None" 
                        ErrorMessage="Endereço é Obrigatório!" 
                        ValidationGroup="ValidationGroup_cadFarmacia"></asp:RequiredFieldValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="TextBox_Responsavel" Display="None" 
                        ErrorMessage="Responsável é Obrigatório!" 
                        ValidationGroup="ValidationGroup_cadFarmacia"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Font-Size="XX-Small" runat="server" 
                        ControlToValidate="TextBox_Telefone" Display="None" 
                        ErrorMessage="Formato inválido para telefone!" 
                        ValidationExpression="^((\(\d{2}\))|(\(\d{2}\)\s))\d{4}\-\d{4}$" 
                        ValidationGroup="ValidationGroup_cadFarmacia"></asp:RegularExpressionValidator>
                        
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox_Farmacia"
                        Display="None" ErrorMessage="Há caracters inválidos no Nome da Farmácia." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFarmacia"></asp:RegularExpressionValidator>
                        
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="TextBox_Endereco"
                        Display="None" ErrorMessage="Há caracters inválidos no Endereço da Farmácia." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFarmacia"></asp:RegularExpressionValidator>
                        
                   <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="TextBox_Responsavel"
                        Display="None" ErrorMessage="Há caracters inválidos no Nome do Responsável." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadFarmacia"></asp:RegularExpressionValidator>
                        
                    <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server" 
                        ShowMessageBox="true" ShowSummary="false" 
                        ValidationGroup="ValidationGroup_cadFarmacia" />
                    <p>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                            ClearMaskOnLostFocus="false" InputDirection="LeftToRight" Mask="(99)9999-9999" 
                            MaskType="None" TargetControlID="TextBox_Telefone">
                        </cc1:MaskedEditExtender>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                    <p>
                    </p>
                </p>
            </p>
        </fieldset>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>