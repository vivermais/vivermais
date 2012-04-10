<%@ Page Language="C#" MasterPageFile="~/Agendamento/MasterAgendamento.Master" AutoEventWireup="true"
    CodeBehind="FormVinculoProcedimentoProgramaDeSaude.aspx.cs" Inherits="ViverMais.View.Agendamento.FormVinculoProcedimentoProgramaDeSaude"
    Title="Untitled Page" %>

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
                TargetBaseControl = document.getElementById('<%= this.GridviewVinculoCBO.ClientID %>');
            }
            catch(err)
            {
                TargetBaseControl = null;
            }
            
            
            
            //Get total no of checkboxes in a particular column inside the GridView.
           try
           {
              CheckBoxes = parseInt('<%= this.GridviewVinculoCBO.Rows.Count %>');
           }
           catch(err)
           {
              CheckBoxes = 0;
           }
            //Get total no of checked checkboxes in a particular column inside the GridView.
            CheckedCheckBoxes = 0;
            SelectedValues = document.getElementById('<%= this.HiddenSelectedValuesCBO.ClientID %>');
            
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
           var TargetBaseControl = document.getElementById('<%= this.GridviewVinculoCBO.ClientID %>');
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
           if(CheckedCheckBoxes < CheckBoxes)
              HCheckBox.checked = false;
           else if(CheckedCheckBoxes == CheckBoxes)
              HCheckBox.checked = true;  
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

        function ChildClick(CheckBox, HCheckBox, Id)
        {
            //Modify Counter;            
           if(CheckBox.checked && CheckedCheckBoxes < CheckBoxes)
              CheckedCheckBoxes++;
           else if(CheckedCheckBoxes > 0) 
              CheckedCheckBoxes--;
            
//            //Change state of the header CheckBox.
//           if(CheckedCheckBoxes < CheckBoxes)
//              HCheckBox.checked = false;
//           else if(CheckedCheckBoxes == CheckBoxes)
//              HCheckBox.checked = true;
            
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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div id="top">
        <h2>
            Formulário de Vínculo de Procedimento ao Programas de Saúde</h2>
        <fieldset class="formulario">
            <legend>Dados</legend>
            <p>
                <span class="rotulo">Programa de Saúde</span> <span>
                    <asp:DropDownList ID="ddlProgramaDeSaude" runat="server" CssClass="drop" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlProgramaDeSaude_SelectedIndexChanged" DataTextField="Nome"
                        DataValueField="Codigo">
                    </asp:DropDownList>
                </span>
            </p>
            <p>
                <asp:CompareValidator ID="CompareValidator_Programa" runat="server" ControlToValidate="ddlProgramaDeSaude"
                    Display="None" ErrorMessage="Programa é Obrigatório!" Font-Size="XX-Small" Operator="GreaterThan"
                    ValueToCompare="0">
                </asp:CompareValidator>
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidatorPrograma" runat="server" ErrorMessage="Selecione o programa de saúde"
                    InitialValue="0" Display="Dynamic" Operator="GreaterThan" va Font-Size="X-Small" Text="*" ControlToValidate="ddlProgramaDeSaude">
                </asp:RequiredFieldValidator>--%>
            </p>
            <p>
                <span class="rotulo">Procedimento</span>
                <Dxm:ASPxComboBox ID="ASPxComboBox_Procedimento" runat="server" TextFormatString="{0}"
                    Width="400px" AllowMouseWheel="true" SelectedIndex="-1" DropDownStyle="DropDown"
                    ShowShadow="false" ShowImageInEditBox="false" EnableDefaultAppearance="false" 
                    Height="20px" FocusedStyle-Font-Bold="true" ItemStyle-BackColor="DimGray" 
                   Font-Size="13px" ButtonStyle-Width="20px" DropDownButton-Enabled="true" DropDownButton-Image-SpriteProperties-CssClass="botaoCombo"  IncrementalFilteringMode="Contains" FilterMinLength="5"
                    ClientInstanceName="ASPxComboBox_Procedimento" EnableCallbackMode="true" CallbackPageSize="10"
                    ValueType="System.String" TextField="CodigoNomeProcedimento" ValueField="Codigo" ItemStyle-Height="10px"
                    ItemStyle-CssClass="Dropbox-item" ListBoxStyle-BackColor="#f8f8f8" ListBoxStyle-Border-BorderColor="#666"
                    ListBoxStyle-Border-BorderStyle="Solid" ListBoxStyle-Border-BorderWidth="1px">
                    <ClientSideEvents LostFocus="function(s, e) {
                                    if(!ASPxComboBox_Procedimento.GetInputElement().value)
                                        ASPxComboBox_Procedimento.PerformCallback(''); }" />
                </Dxm:ASPxComboBox>
            </p>
            <div class="botoesroll">
                <asp:ImageButton ID="btnSalvar" runat="server" OnClick="btnAddVinculo_Click" CausesValidation="true"
                    ImageUrl="~/Agendamento/img/salvar_1.png" />
            </div>
            <%--<p>
                <span class="rotulo">CBO</span>
                <asp:DropDownList ID="ddlCBO" runat="server" CssClass="drop" AutoPostBack="true" OnSelectedIndexChanged="ddlCBO_SelectedIndexChanged">
                </asp:DropDownList>
            </p>--%>
        </fieldset>
        <p>
            &nbsp;
        </p>
        <fieldset style="width: 880px; height: auto; margin-right: 0; padding: 10px 10px 20px 10px;">
            <legend>Vínculos</legend>
            <p>
                <asp:Panel runat="server" ID="PanelVinculosAtivosInativos" Visible="true">
                    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0">
                        <cc1:TabPanel ID="TabelPanel_Um" runat="server" HeaderText="Ativos">
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridViewVinculosAtivos" runat="server" AutoGenerateColumns="False"
                                            AllowSorting="True" CssClass="gridview" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewVinculosAtivos_OnPageIndexChanging"
                                            OnRowCommand="GridViewVinculosAtivos_RowCommand" GridLines="Vertical" DataKeyNames="Codigo">
                                            <Columns>
                                                <asp:BoundField DataField="Procedimento" HeaderText="Unidade"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Cbos" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnVisualizaCBO" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                            CausesValidation="false" CommandName="VisualizaCBO" Text="">
                                                            <asp:Image ID="edit" runat="server" ImageUrl="~/Agendamento/img/edit-peq.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdInativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                            CausesValidation="false" CommandName="Inativar" OnClientClick="javascript : return confirm('Tem certeza que deseja INATIVAR este Vínculo?');"
                                                            Text="">
                                                            <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LabelSemRegistro" runat="server" Text="Nenhum Registro Encontrado!"
                                                    Font-Size="X-Small" ForeColor="Blue"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle CssClass="GridviewPager" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                        <cc1:TabPanel ID="TabPanel_Dois" runat="server" HeaderText="Inativos">
                            <ContentTemplate>
                                <p>
                                    <span>
                                        <asp:GridView ID="GridViewVinculosInativos" runat="server" AutoGenerateColumns="False"
                                            AllowSorting="True" CssClass="gridview" AllowPaging="true" PageSize="10" OnPageIndexChanging="GridViewVinculosInativos_OnPageIndexChanging"
                                            OnRowCommand="GridViewVinculosInativos_RowCommand" GridLines="Vertical" DataKeyNames="Codigo">
                                            <Columns>
                                                <asp:BoundField DataField="Procedimento" HeaderText="Unidade" ReadOnly="true"></asp:BoundField>
                                                <asp:TemplateField HeaderText="Reativar" ItemStyle-Width="60px" ItemStyle-HorizontalAlign="Center"
                                                    ItemStyle-VerticalAlign="Middle">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="cmdReativar" runat="server" CommandArgument='<%# Eval("Codigo") %>'
                                                            CausesValidation="false" CommandName="Reativar" OnClientClick="javascript : return confirm('Tem certeza que deseja REATIVAR este Vínculo?');"
                                                            Text="">
                                                            <asp:Image ID="Inativar" runat="server" ImageUrl="~/Agendamento/img/desativar.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <asp:Label ID="LabelSemRegistro1" runat="server" Text="Nenhum Registro Encontrado"
                                                    Font-Size="X-Small" ForeColor="Red"></asp:Label>
                                            </EmptyDataTemplate>
                                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                                            <SelectedRowStyle CssClass="GridviewPager" />
                                        </asp:GridView>
                                    </span>
                                </p>
                            </ContentTemplate>
                        </cc1:TabPanel>
                    </cc1:TabContainer>
                </asp:Panel>
            </p>
        </fieldset>
        <asp:HiddenField ID="HiddenSelectedValuesCBO" runat="server" />
        <asp:Panel ID="PanelCbos" runat="server" Visible="false">
            <div id="cinza" style="position: fixed; top: 0px; left: 0px; width: 100%; height: 100%;
                z-index: 100; min-height: 100%; background-color: #000; filter: alpha(opacity=85);
                moz-opacity: 0.3; opacity: 0.3" visible="false">
            </div>
            <div id="mensagem" style="position: fixed; top: 40%; margin-top: -200px; left: 50%;
                margin-left: -300px; width: 600px; z-index: 102; background-color: #0d2639; border: #ffffff 5px solid;
                padding-right: 20px; padding-left: 20px; padding-bottom: 20px; color: #c5d4df;
                padding-top: 10px; text-align: justify; font-family: Verdana;" visible="false">
                <p style="height: 10px;">
                    <span style="float: right">
                        <asp:LinkButton ID="btnFechar" runat="server" Height="39px" Width="100%" OnClick="OnClick_btnFechar"
                            CausesValidation="false">
                                <img src="img/fechar-agendamento.png" id="imgInforme" alt=""/>
                        </asp:LinkButton>
                    </span>
                </p>
                <fieldset class="formularioMenor">
                    <legend>Informe os Cbos</legend>
                    <p>
                        <span class="rotulo">Procedimento</span><span><asp:Label ID="lblNomeProcedimento"
                            runat="server" Font-Bold="true"></asp:Label>
                        </span>
                    </p>
                    <p>
                        &nbsp;</p>
                    <p>
                        <asp:GridView ID="GridviewVinculoCBO" CssClass="gridview" runat="server" AllowSorting="True"
                            AllowPaging="true" PageSize="10" OnPageIndexChanging="GridviewVinculoCBO_OnPageIndexChanging"
                            AutoGenerateColumns="false" OnRowDataBound="GridviewVinculoCBO_RowDataBound"
                            GridLines="Vertical" DataKeyNames="Codigo" Width="400px">
                            <Columns>
                                <asp:BoundField DataField="Nome" HeaderText="CBO" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" onclick="javascript:HeaderClick(this);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="RowLevelCheckBox" />
                                        <asp:HiddenField ID="hdnFldId" runat="server" Value='<%# Eval("Codigo") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Nenhuma Agenda Encontrada!" Font-Size="Small"
                                    ForeColor="Red">
                                </asp:Label>
                            </EmptyDataTemplate>
                            <PagerStyle CssClass="GridviewSelected" ForeColor="Black" HorizontalAlign="Center" />
                            <SelectedRowStyle CssClass="GridviewPager" />
                        </asp:GridView>
                    </p>
                    <div class="botoesroll">
                        <asp:ImageButton ID="btnSalvarVinculoCbo" runat="server" OnClick="btnSalvarVinculoCbo_Click"
                            CausesValidation="true" ImageUrl="~/Agendamento/img/salvar_1.png" />
                    </div>
                </fieldset>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
