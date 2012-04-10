<%@ Page Language="C#" MasterPageFile="~/Relatorio/RelatorioMaster.Master" AutoEventWireup="true"
    CodeBehind="RelatorioProducaoCNS.aspx.cs" Inherits="ViverMais.View.Relatorio.RelatorioProducaoCNS"
    Title="Untitled Page" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<!--<link rel="stylesheet" href="style_form_relatorio.css" type="text/css" media="screen" />!-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="top">
    <fieldset>
        <legend>Opções do Relatório</legend>
        <p>
            <asp:RadioButtonList ID="rbtnListTipoRelatorio" runat="server" RepeatDirection="Horizontal"
                Enabled="true">
                <asp:ListItem>Distrito</asp:ListItem>
                <asp:ListItem>Unidade</asp:ListItem>
            </asp:RadioButtonList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Selecione uma opção"
                ControlToValidate="rbtnListTipoRelatorio" Display="Dynamic"></asp:RequiredFieldValidator>
        </p>
        <p>
            <span class="rotulo">Data Inicial:</span><asp:TextBox ID="tbxDataInicial" Width="80px" runat="server" CssClass="campo"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditDataNascimento" runat="server" ClearMaskOnLostFocus="true"
                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="tbxDataInicial">
            </cc1:MaskedEditExtender>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="A Data Inicial não parece ser válida"
                ControlToValidate="tbxDataInicial" Display="Dynamic" Operator="DataTypeCheck"
                Type="Date"></asp:CompareValidator>
        </p>
        <br />
        <p>
            <span class="rotulo">Data Final:</span><asp:TextBox ID="tbxDataFinal" runat="server" Width="80px" CssClass="campo"></asp:TextBox>
            <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="true"
                InputDirection="LeftToRight" Mask="99/99/9999" MaskType="Date" TargetControlID="tbxDataFinal">
            </cc1:MaskedEditExtender>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="A Data Inicial não parece ser válida"
                ControlToValidate="tbxDataFinal" Display="Dynamic" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>
        </p>
        <br />
        <p style="margin-top:40px">
            <asp:LinkButton ID="lnkBtnPesquisar" runat="server" OnClick="lnkBtnPesquisar_Click">
             <img id="imgpesquisa" alt="Pesquisar" src="img/btn/pesquisar1.png"
                onmouseover="imgpesquisa.src='img/btn/pesquisar2.png';"
                onmouseout="imgpesquisa.src='img/btn/pesquisar1.png';" />
            </asp:LinkButton>
        </p>
    </fieldset>
    <p>
    <span class="rotulo" style="font-size:14px; font-weight:bold">
        Total: </span>
        
        <span class="campo" style="font-size:14px; font-weight:bold"><asp:Label ID="lblTotal" runat="server" Text="0"></asp:Label></span>
       
    </p>
    <br /><br /><br />
  
    <p>
        <span>
            <asp:GridView ID="GridResultado" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:BoundField DataField="Nome" HeaderText="Nome" />
                    <asp:BoundField DataField="Producao" HeaderText="Qtd" />
                </Columns>
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5b5b5b" Font-Bold="True" ForeColor="White" Font-Names="Verdana" />
                            <AlternatingRowStyle BackColor="#DCDCDC" />
            </asp:GridView>
        </span>
    </p>
    <asp:Chart ID="Chart1" runat="server" Width="600px" BackImageTransparentColor="Transparent"
        Palette="SeaGreen" ImageStorageMode="UseImageLocation" Height="500px">
        <Series>
            <asp:Series Name="Series1" BackGradientStyle="HorizontalCenter" BorderColor="180, 26, 59, 105"
                Color="220, 65, 140, 240">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea>
                <Area3DStyle Rotation="0" Perspective="10" Enable3D="True" Inclination="15" IsRightAngleAxes="False"
                    WallWidth="0" IsClustered="True" />
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    </div>
</asp:Content>
