<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="IncPesquisarEstoque.ascx.cs"
    Inherits="ViverMais.View.Farmacia.IncPesquisarEstoque" %>
<style type="text/css">
    .formulario2
    {
        width: 670px;
        height: auto;
        margin-left: 5px;
        margin-right: 5px;
        padding: 2px 2px 2px 2px;
    }
</style>

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

<%--<asp:Panel ID="Panel_UsuarioNaoVinculado" runat="server" Visible="false">
    <fieldset class="formulario2">
        <legend>Aviso!</legend>
        <p>
            <span>
                <asp:Label ID="lbEmpty" runat="server" Text="O Usuário Logado não está vinculado a farmácia alguma. Por favor, realizar este vinculo."></asp:Label>
            </span>
        </p>
    </fieldset>
</asp:Panel>--%>
<%--<asp:Panel ID="Panel_Farmacia" runat="server">--%>
<div id="top">
    <h2>
        Pesquisar Estoque</h2>
    <fieldset class="formulario">
        <legend><%--Farmácia:
            <asp:Label ID="Label_Farmacia" runat="server" Text=""></asp:Label>--%>
            Informações
        </legend>
        <p>
            <span class="rotulo">Farmácia</span><span>
                <asp:DropDownList ID="DropDownList_Farmacia" runat="server" DataTextField="Nome" DataValueField="Codigo">
                    <%--<asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>--%>
                </asp:DropDownList>
            </span>
        </p>
        <p>
         <span class="rotulo">Lote</span><span>
                <asp:TextBox ID="TextBox_Lote" CssClass="campo" runat="server" MaxLength="30"></asp:TextBox>
            </span>
        </p>
        <p>
            <span class="rotulo">Fabricante</span> <span>
                <asp:DropDownList ID="DropDownList_Fabricante" runat="server">
                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </p>
        <p>
            <span class="rotulo">Medicamento</span> <span>
                <asp:DropDownList ID="DropDownList_Medicamento" runat="server" Width="300px">
                    <asp:ListItem Text="Selecione..." Value="-1"></asp:ListItem>
                </asp:DropDownList>
            </span>
        </p>
        
            <span>
            <div class="botoesroll">
                <asp:LinkButton ID="Button_Pesquisar" runat="server"  OnClick="OnClick_Pesquisar"
                    ValidationGroup="ValidationGroup_Pesquisa">
                  <img id="imgpesquisar" alt="Pesquisar" src="img/btn/pesquisar1.png"
                  onmouseover="imgpesquisar.src='img/btn/pesquisar2.png';"
                  onmouseout="imgpesquisar.src='img/btn/pesquisar1.png';" /></asp:LinkButton>
                  </div>
                  
                  <div class="botoesroll">
                <asp:LinkButton ID="Button_Cancelar" runat="server"  PostBackUrl="~/Farmacia/Default.aspx">
                  <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                  onmouseover="imgcancelar.src='img/btn/cancelar2.png';"
                  onmouseout="imgcancelar.src='img/btn/cancelar1.png';" /></asp:LinkButton>
                  </div>
            </span>
        
        <p>
            <asp:CompareValidator ID="CompareValidator3" runat="server" Display="None" ControlToValidate="DropDownList_Farmacia"
                ErrorMessage="Farmácia é Obrigatório!" ValidationGroup="ValidationGroup_Pesquisa"
                Operator="GreaterThan" ValueToCompare="-1"></asp:CompareValidator>
            <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Medicamento é Obrigatório!"
                ControlToValidate="DropDownList_Medicamento" ValueToCompare="-1" Operator="GreaterThan"
                Display="None" ValidationGroup="ValidationGroup_Pesquisa"></asp:CompareValidator>--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="true"
                ShowSummary="false" ValidationGroup="ValidationGroup_Pesquisa" />
    </fieldset>
    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger EventName="Click" ControlID="Button_Pesquisar" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel_Resultado" runat="server" Visible="false">
                <fieldset class="formulario">
                    <legend>Resultado da Pesquisa</legend>
<%--                    <p>
                        <span class="rotulo">Medicamento</span> <span style="margin-left: 5px;">
                            <asp:Label ID="Label_Medicamento" runat="server" Text=""></asp:Label>
                        </span>
                    </p>--%>
                    <p>
                        <span>
                            <asp:GridView ID="GridView_Estoque" runat="server" AutoGenerateColumns="false" PagerSettings-Mode="Numeric"
                                Width="660px" AllowPaging="true" PageSize="8" OnPageIndexChanging="OnPageIndexChanging_Paginacao">
                                <Columns>
                                    <asp:BoundField HeaderText="Lote" DataField="NomeLote" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Medicamento" DataField="NomeMedicamento" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Fabricante" DataField="NomeFabricante" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Validade" DataField="DataValidadeLote" ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="Quantidade" DataField="QuantidadeEstoque" ItemStyle-HorizontalAlign="Center" />
                                </Columns>
                                <HeaderStyle CssClass="tab" />
                                <RowStyle CssClass="tabrow" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="lbEmpty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset></asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</asp:Panel>--%>
</div>
