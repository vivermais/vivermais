<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormLoteMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormLoteMedicamento" MasterPageFile="~/Farmacia/MasterFarmacia.Master" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="c_head" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript" language="javascript">
    function showTooltip(obj) {
        if (obj.options[obj.selectedIndex].title == "") {
            obj.title = obj.options[obj.selectedIndex].text;
            obj.options[obj.selectedIndex].title = obj.options[obj.selectedIndex].text;
            for (i = 0; i < obj.options.length; i++) {
                obj.options[i].title = obj.options[i].text;
            }
        }
        else
            obj.title = obj.options[obj.selectedIndex].text;
    }
</script>
</asp:Content>

<asp:Content ID="c_body" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="Button_Salvar" />
        </Triggers>
        <ContentTemplate>
        <div id="top" />
            <h2>Formulário para Cadastro de Lote de Medicamento</h2>
            <fieldset class="formulario">
                <legend>Lote de Medicamento</legend>
                 <p>
                </p>
                <p>
                    <span class="rotulo">Medicamento</span>
                    <span style="margin-left:5px;">
                        <asp:DropDownList ID="DropDownList_Medicamento" runat="server" Width="300px">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Fabricante</span>
                    <span style="margin-left:5px;">
                        <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                            <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Lote</span>
                    <span style="margin-left:5px;">
                        <asp:TextBox ID="TextBox_Lote" CssClass="campo" runat="server"></asp:TextBox>
                    </span>
                </p>
                <p>
                    <span class="rotulo">Data de Validade</span>
                    <span style="margin-left:5px;">
                        <asp:TextBox ID="TextBox_Validade" CssClass="campo" runat="server"></asp:TextBox>
                        <cc1:MaskedEditExtender ID="MaskedEditExtender1" MaskType="Date" Mask="99/99/9999" InputDirection="LeftToRight"
                            TargetControlID="TextBox_Validade" runat="server">
                        </cc1:MaskedEditExtender>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                            TargetControlID="TextBox_Validade" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </span>
                </p>
                <p>
                    <span>
                        <asp:Button ID="Button_Salvar" runat="server" Text="Salvar" OnClick="OnClick_Salvar" ValidationGroup="ValidationGroup_cadLote"/>
                        <asp:Button ID="Button_Cancelar" runat="server" Text="Cancelar" PostBackUrl="~/Farmacia/Default.aspx" />
                    </span>
                </p>
                <p>
                    <asp:CompareValidator ID="CompareValidator4" Font-Size="XX-Small" runat="server" ErrorMessage="Medicamento é Obrigatório!" ControlToValidate="DropDownList_Medicamento" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_cadLote"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator3" Font-Size="XX-Small" runat="server" ErrorMessage="Fabricante é Obrigatório!" ControlToValidate="DropDownList_Fabricante" ValueToCompare="-1" Operator="GreaterThan" Display="None" ValidationGroup="ValidationGroup_cadLote"></asp:CompareValidator>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server" ErrorMessage="Lote é Obrigatório!" ControlToValidate="TextBox_Lote" Display="None" ValidationGroup="ValidationGroup_cadLote"></asp:RequiredFieldValidator>
                    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="TextBox_Lote"
                        Display="None" ErrorMessage="Há caracters inválidos no Nome do Lote." Font-Size="XX-Small"
                        ValidationExpression="^[0-9a-zA-Z\s]{1,}$" ValidationGroup="ValidationGroup_cadLote"></asp:RegularExpressionValidator>
                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server" ErrorMessage="Data de Validade é Obrigatório!" ControlToValidate="TextBox_Validade" Display="None" ValidationGroup="ValidationGroup_cadLote"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" Font-Size="XX-Small" runat="server" ErrorMessage="Data de Validade com formato inválido." Type="Date" Operator="DataTypeCheck" ControlToValidate="TextBox_Validade" ValidationGroup="ValidationGroup_cadLote" Display="None"></asp:CompareValidator>
                    <asp:CompareValidator ID="CompareValidator2" Font-Size="XX-Small" runat="server" ErrorMessage="Data de Validade deve ser igual ou maior que 01/01/1900." ControlToValidate="TextBox_Validade" Type="Date" ValueToCompare="01/01/1900" Operator="GreaterThanEqual" ValidationGroup="ValidationGroup_cadLote" Display="None"></asp:CompareValidator>
                    <asp:ValidationSummary ID="ValidationSummary1" Font-Size="XX-Small" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ValidationGroup_cadLote"/>
                </p>
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>