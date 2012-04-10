﻿<%@ Page Language="C#" MasterPageFile="~/Farmacia/MasterFarmacia.Master" AutoEventWireup="True"
    CodeBehind="FormSubElencoMedicamento.aspx.cs" Inherits="ViverMais.View.Farmacia.FormSubElencoMedicamento"
    Title="ViverMais - Sub-Elenco de Medicamentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>--%>
            <div id="top">
                <h2>
                    Formulário de Sub-Elenco de Medicamentos</h2>
                <fieldset class="formulario">
                    <legend>Dados do Novo Sub-Elenco</legend>
                    <p>
                        <span style="color: Red; font-family: Verdana; font-size: 11px; float: right;">* campos
                            obrigatórios</span>
                    </p>
                    <p>
                        <span class="rotulo">Descrição:</span> <span style="margin-left: 5px;">
                            <asp:TextBox ID="tbxSubElenco" runat="server" Width="300px" CssClass="campo"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" Font-Size="XX-Small" runat="server"
                                ControlToValidate="tbxSubElenco" ErrorMessage="RequiredFieldValidator" SetFocusOnError="true">*</asp:RequiredFieldValidator>
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Medicamentos</legend>
                    <p>
                        <span class="rotulo">Medicamentos:</span> <span>
                            <asp:DropDownList ID="ddlMedicamento" runat="server" Width="395px"  Height="28px" CssClass="campo" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlMedicamento" ErrorMessage="RequiredFieldValidator" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Medicamentos">*</asp:RequiredFieldValidator>
                            <asp:ImageButton ID="btnAdicionar" runat="server" Text="Adicionar" OnClick="btnAdicionar_Click"
                                ValidationGroup="Medicamentos" ImageUrl="~/Farmacia/img/btn/add.gif" Width="19px" Height="19px" />
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Lista de Medicamentos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridMedicamentos" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="X-Small"
                                OnRowDeleting="gridMedicamentos_RowDeleting" DataKeyNames="Codigo">
                                <Columns>
                                    <asp:BoundField DataField="CodMedicamento" HeaderText="Código" />
                                    <asp:BoundField DataField="Nome" HeaderText="Medicamento" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                               <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label_Empty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>
                <%--<fieldset class="formulario">
                    <legend>Vincular Elencos</legend>
                    <p>
                        <span class="rotulo">Elenco:</span> <span>
                            <asp:DropDownList ID="ddlElenco" runat="server" Height="28px" CssClass="campo" >
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Font-Size="XX-Small" runat="server"
                                ControlToValidate="ddlElenco" ErrorMessage="RequiredFieldValidator" InitialValue="0"
                                SetFocusOnError="true" ValidationGroup="Elencos">*</asp:RequiredFieldValidator>
                            <asp:ImageButton ID="btnAdicionar2" runat="server" Text="Adicionar" OnClick="btnAdicionar2_Click"
                                ValidationGroup="Elencos" ImageUrl="~/Farmacia/img/btn/add.gif" Width="19px" Height="19px" />
                        </span>
                    </p>
                </fieldset>
                <fieldset class="formulario">
                    <legend>Lista de Elencos</legend>
                    <p>
                        <span>
                            <asp:GridView ID="gridElenco" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="X-Small"
                                OnRowDeleting="gridElenco_RowDeleting">
                                <Columns>
                                    <asp:BoundField DataField="Codigo" HeaderText="Código" />
                                    <asp:BoundField DataField="Nome" HeaderText="Elenco" />
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    <asp:Label ID="Label_Empty" runat="server" Text="Nenhum registro encontrado."></asp:Label>
                                </EmptyDataTemplate>
                                <HeaderStyle BackColor="#194129" Font-Bold="True" ForeColor="White" Height="20px" HorizontalAlign="Center"/>
                                    <RowStyle ForeColor="Black" BackColor="#f0f0f0" Font-Bold="true" Height="18px" HorizontalAlign="Center" />
                                    <PagerStyle BackColor="#194129" ForeColor="White" HorizontalAlign="Center" />
                            </asp:GridView>
                        </span>
                    </p>
                </fieldset>--%>
           
                           <div class="botoesroll">
                        <asp:LinkButton ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click">
                         <img id="imgsalvar" alt="Salvar" src="img/btn/salvar1.png"
                onmouseover="imgsalvar.src='img/btn/salvar2.png';"
                onmouseout="imgsalvar.src='img/btn/salvar1.png';" />
                        </asp:LinkButton>
                        </div>
                        
                        <div class="botoesroll">
                        <asp:LinkButton ID="btnCancelar" runat="server" Text="Cancelar" CausesValidation="False"
                            PostBackUrl="~/Farmacia/FormExibeSubElencoMedicamento.aspx">
                             <img id="imgcancelar" alt="Cancelar" src="img/btn/cancelar1.png"
                onmouseover="imgcancelar.src='img/btn/cancelar1.png';"
                onmouseout="imgcancelar.src='img/btn/cancelar1.png';" />
                            </asp:LinkButton>
                            </div>
                        <%--<asp:LinkButton ID="LinkButton_Excluir" runat="server" OnClick="OnClick_ExcluirSubElenco" Visible="false"
                         OnClientClick="javascript:return confirm('Tem certeza que deseja excluir este sub-elenco ?');">Excluir</asp:LinkButton>--%>
                    </p>
              
            </div>
      <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
