<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirReciboDispensacao.aspx.cs"
    Inherits="ViverMais.View.Vacina.FormImprimirReciboDispensacao" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Impressão do Comprovante de Vacinação</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<%--        <table style="width: 210px">
            <tr>
                <td>
                    <div align="center">
                        <asp:Label ID="Label1" runat="server" Text="Prefeitura do Salvador" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Secretária Municipal de Saúde"></asp:Label>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="ViverMais - Sistema Integrado em Saúde Pública"
                            Font-Bold="true"></asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label_UnidadeSaude" runat="server" Text='<%#bind("UnidadeSaude") %>'></asp:Label>
                        <br />
                        <br />
                    </div>
                    <div align="left">
                        <asp:Label ID="Label5" runat="server" Text="Paciente: " Font-Bold="true"></asp:Label><asp:Label
                            ID="Label_Paciente" runat="server" Text='<%#bind("Nome") %>'></asp:Label><br />
                        <asp:Label ID="Label6" runat="server" Text="CNS: " Font-Bold="true"></asp:Label>
                        <asp:Label ID="Label_CartaoSUS" runat="server" Text='<%#bind("CartaoSUS") %>'></asp:Label><br />
                        <asp:Label ID="Label7" runat="server" Text="Data: " Font-Bold="true"></asp:Label>
                        <asp:Label ID="Label_Data" runat="server" Text='<%#bind("Data") %>'></asp:Label><br />
                        <br />
                    </div>
                </td>
            </tr>
        </table>
        <asp:DataList ID="DataListCorpo" runat="server" ItemStyle-Width="210px" OnItemDataBound="OnItemDataBound_CarregaItensDispensacao">
            <ItemTemplate>
                <div align="left">
                    <asp:Label ID="Label5" runat="server" Text="Vacina: " Font-Bold="true"></asp:Label><asp:Label
                        ID="Label8" runat="server" Text='<%#bind("Vacina") %>'></asp:Label><br />
                    <asp:Label ID="Label6" runat="server" Text="Dose: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label9" runat="server" Text='<%#bind("Dose") %>'></asp:Label><br />
                    <asp:Panel ID="Panel_ProximaDose" runat="server" Visible="false">
                        <asp:Label ID="Label15" runat="server" Text="Próxima: " Font-Bold="true"></asp:Label>
                        <asp:Label ID="Label16" runat="server" Text='<%#bind("ProximaVacina") %>'></asp:Label><br />
                    </asp:Panel>
                    <asp:Label ID="Label7" runat="server" Text="Lote: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label10" runat="server" Text='<%#bind("Lote") %>'></asp:Label><br />
                    <asp:Label ID="Label11" runat="server" Text="Fabricante: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label12" runat="server" Text='<%#bind("Fabricante") %>'></asp:Label><br />
                    <asp:Label ID="Label13" runat="server" Text="Validade: " Font-Bold="true"></asp:Label>
                    <asp:Label ID="Label14" runat="server" Text='<%#bind("Validade") %>'></asp:Label><br />
                    <br />
                </div>
            </ItemTemplate>
        </asp:DataList>
        <div style="width: 210px; font-size: 12px;" align="center">
            Acesse seu Cartão de Vacina pelo site:
            <br />
            www.saude.salvador.ba.gov.br/ViverMais
            <br />
            <br />
            <br />
            <br />
        </div>
        .--%>
    </div>
    </form>
</body>
</html>
