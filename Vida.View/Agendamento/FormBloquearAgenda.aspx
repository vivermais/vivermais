﻿<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormBloquearAgenda.aspx.cs" Inherits="ViverMais.View.Agendamento.FormBloquearAgenda"
    Title="Untitled Page" %>

<%@ Register Src="~/EstabelecimentoSaude/WUC_PesquisarEstabelecimento.ascx" TagName="TagName_Estabelecimento"
    TagPrefix="TagPrefix_Estabelecimento" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <script type="text/javascript">
        var TargetBaseControl = null;
        var SelectedValues;
        var SelectedItems = new Array();
        //Total no of checkboxes in a particular column inside the GridView.
        var CheckBoxes;
        //Total no of checked checkboxes in a particular column inside the GridView.
        var CheckedCheckBoxes;
        window.onload = function()
        {
            try
            {
                TargetBaseControl = document.getElementById('<%= this.GridviewAgendas.ClientID %>');
            }
            catch(err)
            {
                TargetBaseControl = null;
            }
            
            
            
            //Get total no of checkboxes in a particular column inside the GridView.
           try
           {
              CheckBoxes = parseInt('<%= this.GridviewAgendas.Rows.Count %>');
           }
           catch(err)
           {
              CheckBoxes = 0;
           }
            //Get total no of checked checkboxes in a particular column inside the GridView.
            CheckedCheckBoxes = 0;
            SelectedValues = document.getElementById('<%= this.HiddenSelectedValuesAgenda.ClientID %>');
            
           //Get an array of selected item's Ids.
           if(SelectedValues.value == '')
              SelectedItems = new Array();
           else
              SelectedItems = SelectedValues.value.split('|');
              
            if(TargetBaseControl != null)
                RestoreState();
        }

        function HeaderClick(CheckBox)
        {
           //Get target base & child control.
           var TargetBaseControl = document.getElementById('<%= this.GridviewAgendas.ClientID %>');
           var TargetChildControl = "RowLevelCheckBox";

           //Get all the control of the type INPUT in the base control.
           var Inputs = TargetBaseControl.getElementsByTagName("input");

           //Checked/Unchecked all the checkBoxes in side the GridView.
           for(var n = 0; n < Inputs.length; ++n)
           {
              if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf(TargetChildControl,0) >= 0)
              {
                Inputs[n].checked = CheckBox.checked;
                if(CheckBox.checked)
                    SelectedItems.push(document.getElementById(Inputs[n].id.replace('RowLevelCheckBox','hdnFldId')).value);
                else
                    DeleteItem(document.getElementById(Inputs[n].id.replace('RowLevelCheckBox','hdnFldId')).value);
              }
            }
             ///Update Selected Values. 
            SelectedValues.value = SelectedItems.join('|');
                                
            //Reset Counter
            CheckedCheckBoxes = CheckBox.checked ? CheckBoxes : 0;
        }

        function ChildClick(CheckBox, HCheckBox, Id)
        {
            //Modify Counter;            
           if(CheckBox.checked && CheckedCheckBoxes < CheckBoxes)
              CheckedCheckBoxes++;
           else if(CheckedCheckBoxes > 0) 
              CheckedCheckBoxes--;
            
           //get target control.
           var HeaderCheckBox = document.getElementById(HCheckBox);
           HeaderCheckBox.checked = true
           
           //Modify selected items array.
           if(CheckBox.checked)
              SelectedItems.push(Id);
           else
              DeleteItem(Id);
              
           //Update Selected Values. 
           SelectedValues.value = SelectedItems.join('|');
        }
        
        function RestoreState()
        {
           //Get all the control of the type INPUT in the base control.
           var Inputs = TargetBaseControl.getElementsByTagName('input');
                    
           //Header CheckBox
           var HCheckBox = null;
                    
           //Restore previous state of the all checkBoxes in side the GridView.
           for(var n = 0; n < Inputs.length; ++n)
           {
              if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf('RowLevelCheckBox',0) >= 0)
              {
                 if(IsItemExists(document.getElementById(Inputs[n].id.replace('RowLevelCheckBox','hdnFldId')).value) > -1)
                 {
                    Inputs[n].checked = true;          
                    CheckedCheckBoxes++;      
                 }
                 else
                    Inputs[n].checked = false;
               }
               else if(Inputs[n].type == 'checkbox' && Inputs[n].id.indexOf('HeaderLevelCheckBox',0) >= 0) 
                 HCheckBox = Inputs[n];
            }
                            
           //Change state of the header CheckBox.
           if(CheckBoxes != 0)
           {
               if(CheckedCheckBoxes < CheckBoxes)
                  HCheckBox.checked = false;
               else if(CheckedCheckBoxes == CheckBoxes)
                  HCheckBox.checked = true; 
           }
        }
        
        function DeleteItem(Text)
        {
           var n = IsItemExists(Text);
           if( n > -1)
              SelectedItems.splice(n,1);
        }
        
        function IsItemExists(Text)
        {
           for(var n = 0; n < SelectedItems.length; ++n)
              if(SelectedItems[n] == Text)
                 return n;
                         
              return -1;  
        }
        
        function DateValido(data)
        {
            var dataAtual = new Date('<%=DataAtual()%>');
            var DataComparar = new Date(data);
            if(DataComparar < dataAtual)
            {
                return false;
            }
            return true;
        }
        
    </script>

    <script type="text/C#" runat="server">
        public String DataAtual()
        {            
            return DateTime.Now.Date.ToString("dd/MM/yyyy");
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Bloqueio de Agenda</h2>
        <fieldset>
            <legend>Pesquisar</legend>
            <asp:Panel ID="Panel_Estabelecimento" runat="server">
                <p>
                    <span class="rotulo">Data Inicial</span> <span>
                        <asp:TextBox ID="tbxDataInicial" runat="server" CssClass="campo" onblur="DateValido(this.value)"></asp:TextBox>
                    </span>
                    <cc1:CalendarExtender runat="server" ID="CalendarExtender1" Format="dd/MM/yyyy" TargetControlID="tbxDataInicial"
                        Animated="true">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" InputDirection="LeftToRight"
                        TargetControlID="tbxDataInicial" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="Small" runat="server"
                        ControlToValidate="tbxDataInicial" ErrorMessage="A Data Inicial é obrigatória"
                        ValidationGroup="ValidationBloquear" ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="CustomValidatorDataInicial" runat="server" Display="Dynamic"
                        ErrorMessage="A data inicial deve ser maior que a data atual" OnServerValidate="CustomValidatorDataInicial_ServerValidate"
                        Font-Size="XX-Small" ValidationGroup="ValidationBloquear" ControlToValidate="tbxDataInicial"></asp:CustomValidator>
                    <%--<asp:CompareValidator ID="CompareValidatorDataInicial" runat="server" ControlToValidate="tbxDataInicial"
                        Operator="LessThan" Display="Dynamic" Type="Date" ValidationGroup="ValidationBloquear"></asp:CompareValidator>--%>
                    <%--<asp:CustomValidator ID="CustomValidator_DataInicial" runat="server" ClientValidationFunction="ValidarData"
                    ControlToValidate="tbxDataInicial" ErrorMessage="Data Inicial Inválida." ValidateEmptyText="true"
                    Font-Size="XX-Small"></asp:CustomValidator>--%>
                </p>
                <p>
                    <span class="rotulo">Data Final</span> <span>
                        <asp:TextBox ID="tbxDataFinal" runat="server" CssClass="campo"></asp:TextBox>
                    </span>
                    <cc1:CalendarExtender runat="server" ID="CalendarExtender2" Format="dd/MM/yyyy" TargetControlID="tbxDataFinal"
                        Animated="true">
                    </cc1:CalendarExtender>
                    <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" InputDirection="LeftToRight"
                        TargetControlID="tbxDataFinal" Mask="99/99/9999" MaskType="Date">
                    </cc1:MaskedEditExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="Small" runat="server"
                        ControlToValidate="tbxDataFinal" ErrorMessage="A Data Final é obrigatória" ValidationGroup="ValidationBloquear"
                        ForeColor="Red" Text="*">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorDataFinal" runat="server" ControlToValidate="tbxDataFinal"
                        ControlToCompare="tbxDataInicial" Type="Date" Operator="GreaterThan" Display="Dynamic"
                        ErrorMessage="A data final não pode ser menor que a data inicial" ValidationGroup="ValidationBloquear"></asp:CompareValidator>
                </p>
                <fieldset>
                    <legend>Estabelecimento</legend>
                    <TagPrefix_Estabelecimento:TagName_Estabelecimento ID="EAS" runat="server">
                    </TagPrefix_Estabelecimento:TagName_Estabelecimento>
                    <asp:UpdatePanel ID="UpdatePanel_Unidade" runat="server" UpdateMode="Conditional"
                        RenderMode="Block" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <p>
                                <span class="rotulo">Unidade</span> <span>
                                    <asp:DropDownList ID="DropDownList_Estabelecimento" runat="server" Height="20px"
                                        OnSelectedIndexChanged="DropDownList_Estabelecimento_SelectedIndexChanged" CssClass="campo"
                                        Width="380px" DataTextField="NomeFantasia" DataValueField="CNES" AutoPostBack="true">
                                        <asp:ListItem Text="SELECIONE..." Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </span>
                                <asp:CompareValidator ID="CompareValidatorUnidade" runat="server" ControlToValidate="DropDownList_Estabelecimento"
                                    ErrorMessage="O Estabelecimento é Obrigatório" Operator="NotEqual" ValueToCompare="0"
                                    ValidationGroup="ValidationBloquear">
                                </asp:CompareValidator>
                            </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </asp:Panel>
            <p>
                <asp:UpdatePanel ID="UpdatePanelProcedimento" runat="server" UpdateMode="Conditional"
                    ChildrenAsTriggers="true" RenderMode="Inline">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="DropDownList_Estabelecimento" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <span class="rotulo">Procedimento</span> <span>
                            <asp:DropDownList ID="ddlProcedimento" DataValueField="Codigo" DataTextField="Nome"
                                runat="server" CssClass="drop" AutoPostBack="true" OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<Dxm:ASPxComboBox ID="ASPxComboBox_Procedimento" runat="server" TextFormatString="{0}"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlProcedimento_SelectedIndexChanged"
                        Height="20px" FocusedStyle-Font-Bold="true" ItemStyle-BackColor="DimGray" AllowMouseWheel="true"
                        SelectedIndex="-1" DropDownStyle="DropDown" ShowShadow="false" ShowImageInEditBox="false"
                        EnableDefaultAppearance="false" Font-Size="13px" ButtonStyle-Width="20px" DropDownButton-Enabled="true"
                        DropDownButton-Image-SpriteProperties-CssClass="botaoCombo" IncrementalFilteringMode="Contains"
                        FilterMinLength="5" ClientInstanceName="ASPxComboBox_Procedimento" EnableCallbackMode="true"
                        CallbackPageSize="10" ValueType="System.String" TextField="Nome" ValueField="Codigo"
                        ItemStyle-CssClass="Dropbox-item" ListBoxStyle-BackColor="#f8f8f8" ListBoxStyle-Border-BorderStyle="Solid"
                        ListBoxStyle-Border-BorderWidth="1px">
                        <ClientSideEvents LostFocus="function(s, e) {if(ASPxComboBox_Procedimento.GetSelectedIndex() <0 )ASPxComboBox_Procedimento.PerformCallback(''); }" />
                    </Dxm:ASPxComboBox>--%>
                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </p>
            <asp:UpdatePanel ID="UpdatePanelEspecialidade" runat="server" RenderMode="Inline"
                UpdateMode="Conditional" ChildrenAsTriggers="true">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlProcedimento" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">Especialidade</span> <span>
                            <asp:DropDownList ID="ddlEspecialidade" runat="server" CssClass="drop" OnSelectedIndexChanged="ddlEspecialidade_SelectedIndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </span>
                        <asp:CompareValidator ID="CompareValidatorCBO" runat="server" ControlToValidate="ddlEspecialidade"
                            Operator="NotEqual" ValueToCompare="0" ValidationGroup="ValidationBloquear"></asp:CompareValidator>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanelSubGrupo" runat="server" RenderMode="Inline" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEspecialidade" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <p>
                        <span class="rotulo">SubGrupo</span> <span>
                            <asp:DropDownList ID="ddlSubGrupo" runat="server" CssClass="drop">
                            </asp:DropDownList>
                        </span>
                    </p>
                    <p>
                        <span class="rotulo">Profissional</span> <span>
                            <asp:DropDownList ID="ddlProfissional" runat="server" CssClass="drop">
                            </asp:DropDownList>
                        </span>
                    </p>
                </ContentTemplate>
            </asp:UpdatePanel>
            <p>
                <span class="rotulo">Turno</span> <span>
                    <asp:DropDownList ID="ddlTurno" runat="server" CssClass="drop">
                        <asp:ListItem Text="Manhã" Value="M"></asp:ListItem>
                        <asp:ListItem Text="Tarde" Value="T"></asp:ListItem>
                        <asp:ListItem Text="Noite" Value="N"></asp:ListItem>
                    </asp:DropDownList>
                </span>
            </p>
            <div class="botoesroll">
                <asp:LinkButton ID="btnVisualizarAgendas" runat="server" CausesValidation="true"
                    OnClick="btnPesquisarAgendas_Click" ValidationGroup="ValidationBloquear">
                        <img id="imgvizualizar" alt="Vizualizar Agendas" src="img/btn-vizualizaragendas1.png"
                            onmouseover="imgvizualizar.src='img/btn-vizualizaragendas2.png';"
                            onmouseout="imgvizualizar.src='img/btn-vizualizaragendas1.png';" />
                </asp:LinkButton>
            </div>
            <br />
            <br />
            <asp:HiddenField ID="HiddenSelectedValuesAgenda" runat="server" />
            <asp:Panel ID="PanelExibeAgenda" runat="server" Visible="false">
                <fieldset style="width: 690px; height: auto; margin-right: 0; padding: 10px 10px 5px 10px;">
                    <legend>Agendas</legend>
                    <div class="botoesroll">
                        <p>
                            <span class="legenda">Q.O. - Qtde Ofertada &nbsp;/</span> <span class="legenda">Q.A.
                                - Qtde Agendada &nbsp;/</span> <span style="background-color: #922929">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                            <span class="legenda">Agenda Bloqueada &nbsp;/</span>
                        </p>
                        <asp:GridView ID="GridviewAgendas" runat="server" DataKeyNames="Codigo" AutoGenerateColumns="false"
                            CssClass="gridview" AllowPaging="true" PageSize="10" OnRowDataBound="GridViewAgendas_RowDataBound"
                            OnPageIndexChanging="GridViewAgendas_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="javascript:HeaderClick(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="RowLevelCheckBox" />
                                        <asp:HiddenField ID="hdnFldId" runat="server" Value='<%# Eval("Codigo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Procedimento" HeaderText="Procedimento" />
                                <asp:BoundField DataField="Cbo" HeaderText="Especialidade" />
                                <asp:BoundField DataField="NomeProfissional" HeaderText="Profissional" />
                                <asp:BoundField DataField="Data" HeaderText="Data" />
                                <asp:BoundField DataField="Turno" HeaderText="Turno" />
                                <asp:BoundField DataField="Horario" HeaderText="Horário" />
                                <asp:BoundField DataField="Quantidade" HeaderText="Q.O" />
                                <asp:BoundField DataField="QuantidadeAgendada" HeaderText="Q.A" />
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Nenhuma Registro Encontrado!" Font-Size="Small"
                                    ForeColor="Red">
                                </asp:Label>
                            </EmptyDataTemplate>
                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="GridviewPager" />
                        </asp:GridView>
                        <br />
                </fieldset>
            </asp:Panel>
            <br />
            <p>
                <span class="rotulo">Motivo</span> <span>
                    <asp:TextBox ID="tbxMotivo" runat="server" TextMode="MultiLine" Width="400px" MaxLength="150"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMotivo" Font-Size="Small" Text="*"
                        runat="server" ControlToValidate="tbxMotivo" ErrorMessage="O Motivo é obrigatório"
                        Display="Dynamic" ForeColor="Red">
                    </asp:RequiredFieldValidator>
                </span>
            </p>
            <div class="botoesroll">
                <cc1:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" TargetControlID="btnSalvar"
                    ConfirmText="Confirma o Bloqueio das agendas Selecionadas?" />
                <asp:LinkButton ID="btnSalvar" runat="server" OnClick="btnSalvar_Click" CausesValidation="true"
                    ValidationGroup="ValidationBloquear">
                    <img id="imgsalvar" alt="Salvar" src="img/salvar_1.png"
                    onmouseover="imgsalvar.src='img/salvar_2.png';"
                    onmouseout="imgsalvar.src='img/salvar_1.png';" />
                </asp:LinkButton>
            </div>
            <div class="botoesroll">
                <asp:LinkButton ID="btnVoltar" runat="server" PostBackUrl="~/Agendamento/Default.aspx"
                    CausesValidation="false">
                <img id="img_voltar" alt="" src="img/voltar_1.png"
                onmouseover="img_voltar.src='img/voltar_2.png';"
                onmouseout="img_voltar.src='img/voltar_1.png';" />
                </asp:LinkButton></div>
        </fieldset>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" Font-Size="Small" runat="server" ShowMessageBox="true"
        ShowSummary="false" />
</asp:Content>
