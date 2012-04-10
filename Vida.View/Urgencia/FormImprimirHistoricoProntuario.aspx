﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormImprimirHistoricoProntuario.aspx.cs"
    Inherits="ViverMais.View.Urgencia.FormImprimirHistoricoProntuario" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <%--<CR:CrystalReportViewer ID="CrystalReportViewer_Historico" runat="server" AutoDataBind="true"
            DisplayGroupTree="False" />--%>
        <asp:DataList ID="DataList_AtestadoReceita" runat="server" DataKeyField="Codigo"
         OnItemDataBound="OnItemDataBound_ConfigurarDataListAtestados">
            <ItemTemplate>
                <asp:Label ID="TipoDocumento" runat="server" Text='<%#bind("TipoDocumento") %>'></asp:Label>
                
                <asp:Table ID="Tabela_AtestadoReceita" runat="server">
                    <asp:TableRow HorizontalAlign="Center" BorderColor="Black" Width="855px" Height="128px"
                        Style="background-repeat: no-repeat;">
                        <asp:TableCell>
                            <asp:Image ID="topo" runat="server" />
                            <%--<img alt="Topo" src="" runat="server" id="topo" />--%>
                            <%--<asp:Image ID="ImagemTopo" runat="server" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableHeaderRow Width="410px">
                        <asp:TableCell Width="410px">
                            <asp:Table ID="Table_Cabecalho" Width="410px" Height="10px" runat="server" HorizontalAlign="Left"
                                Font-Names="Verdana" Font-Size="11px">
<%--                                <asp:TableRow>
                                    <asp:TableCell>
                                    <p>&nbsp</p>
                                    </asp:TableCell>
                                </asp:TableRow>--%>
<%--                                <asp:TableRow>
                                    <asp:TableCell>
                                    <p>&nbsp</p>
                                    </asp:TableCell>
                                </asp:TableRow>--%>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Nome: "></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text='<%#bind("Paciente") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="CNS: "></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text='<%#bind("CartaoSUS") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="RG: "></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Text='<%#bind("RG") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Unidade: "></asp:Label>
                                        <asp:Label ID="Label10" runat="server" Text='<%#bind("EstabelecimentoAtendimento") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="CNES: "></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text='<%#bind("CNES") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Número de Atendimento: "></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text='<%#bind("NumeroAtendimento") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableHeaderRow>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow>
                        <asp:TableCell>
                                    <p><asp:Label ID="LabelSiglaReceita" runat="server" Text="R/" Visible="false"></asp:Label>
                                    &nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Justify" Width="410px" Font-Names="Verdana" Font-Size="12px"
                        >
                        <asp:TableCell HorizontalAlign="Justify" Width="410px">
                            <asp:Literal ID="Literal_Conteudo" runat="server" Text='<%#bind("Conteudo") %>'></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Left" Width="100%" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                            <asp:Label ID="Label_DataDocumento" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Bold="true" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                             <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Bold="true" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                            <asp:Label ID="Label4" runat="server" Text='<%#bind("NomeProfissional") %>'></asp:Label>
                            <br />
                            CRM-BA:<asp:Label ID="Label5" Font-Bold="true" runat="server" Font-Names="Verdana"
                                Font-Size="12px" Text='<%#bind("CRMProfissional") %>'></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Names="Verdana" Font-Size="12px"
                        Font-Bold="true">
                        <asp:TableCell Width="100%">
                            <p>&nbsp</p>
                            <asp:Image ID="rodape" runat="server" ImageUrl="~/Urgencia/img/rodape.jpg" />
                            <%--<img alt="Rodapé" src="" runat="server" id="rodape" />--%>
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Urgencia/img/rodape.jpg" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
            <SeparatorTemplate>
                <br style="page-break-before: always;" />
            </SeparatorTemplate>
            <FooterTemplate>
                <asp:Label ID="Label15" runat="server" Text="Nenhum(a) atestado/receita encontrado." Visible='<%#bool.Parse((DataList_AtestadoReceita.Items.Count == 0).ToString()) %>'></asp:Label>
            </FooterTemplate>
        </asp:DataList>
        <asp:DataList ID="DataList_ExameEletivo" runat="server"
            OnItemDataBound="OnItemDataBound_ConfigurarDataListExames">
            <ItemTemplate>
                <asp:Table ID="Tabela_Exames" runat="server">
                    <asp:TableRow HorizontalAlign="Center" BorderColor="Black" Width="855px" Height="128px"
                        Style="background-repeat: no-repeat;">
                        <asp:TableCell>
                            <asp:Image ID="topo" runat="server" ImageUrl="~/Urgencia/img/topo_exames_eletivos.jpg" />
                            <%--<img alt="Topo" src="" runat="server" id="topo" />--%>
                            <%--<asp:Image ID="ImagemTopo" runat="server" ImageUrl="~/Urgencia/img/topo_exames_eletivos.jpg" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableHeaderRow Width="410px">
                        <asp:TableCell Width="410px">
                            <asp:Table ID="Table_Cabecalho" Width="410px" Height="10px" runat="server" HorizontalAlign="Center"
                                Font-Names="Verdana" Font-Size="11px">
<%--                                <asp:TableRow>
                                    <asp:TableCell>
                                    <p>&nbsp</p>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                    <p>&nbsp</p>
                                    </asp:TableCell>
                                </asp:TableRow>--%>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Nome: "></asp:Label>
                                        <asp:Label ID="Label7" runat="server" Text='<%#bind("Paciente") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="CNS: "></asp:Label>
                                        <asp:Label ID="Label2" runat="server" Text='<%#bind("CartaoSUS") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label3" runat="server" Font-Bold="true" Text="RG: "></asp:Label>
                                        <asp:Label ID="Label8" runat="server" Text='<%#bind("RG") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label9" runat="server" Font-Bold="true" Text="Unidade: "></asp:Label>
                                        <asp:Label ID="Label10" runat="server" Text='<%#bind("EstabelecimentoAtendimento") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="CNES: "></asp:Label>
                                        <asp:Label ID="Label12" runat="server" Text='<%#bind("CNES") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                                <asp:TableRow>
                                    <asp:TableCell>
                                        <asp:Label ID="Label13" runat="server" Font-Bold="true" Text="Número de Atendimento: "></asp:Label>
                                        <asp:Label ID="Label14" runat="server" Text='<%#bind("NumeroAtendimento") %>'></asp:Label>
                                    </asp:TableCell>
                                </asp:TableRow>
                            </asp:Table>
                        </asp:TableCell>
                    </asp:TableHeaderRow>
                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <%--<asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow HorizontalAlign="Justify" Width="410px" Font-Names="Verdana" Font-Size="12px"
                        >
                        <asp:TableCell HorizontalAlign="Justify" Width="410px">
                            <asp:Literal ID="Literal_Conteudo" runat="server" Text='<%#bind("Conteudo") %>'></asp:Literal>
                        </asp:TableCell>
                    </asp:TableRow>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Left" Width="100%" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                            <asp:Label ID="Label_DataDocumento" runat="server" Text=""></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
<%--                    <asp:TableRow>
                        <asp:TableCell>
                                    <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>--%>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Bold="true" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                             <p>&nbsp</p>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Bold="true" Font-Names="Verdana"
                        Font-Size="12px">
                        <asp:TableCell Width="100%">
                            <asp:Label ID="Label4" runat="server" Text='<%#bind("NomeProfissional") %>'></asp:Label>
                            <br />
                            CRM-BA:<asp:Label ID="Label5" Font-Bold="true" runat="server" Font-Names="Verdana"
                                Font-Size="12px" Text='<%#bind("CRMProfissional") %>'></asp:Label>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow HorizontalAlign="Center" Width="100%" Font-Names="Verdana" Font-Size="12px"
                        Font-Bold="true">
                        <asp:TableCell Width="100%">
                            <p>&nbsp</p>
                            <asp:Image ID="rodape" runat="server" ImageUrl="~/Urgencia/img/rodape.jpg" />
                            <%--<img alt="Rodapé" src="" runat="server" id="rodape" />--%>
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/Urgencia/img/rodape.jpg" />--%>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
            </ItemTemplate>
            <SeparatorTemplate>
                <br style="page-break-before: always;" />
            </SeparatorTemplate>
             <FooterTemplate>
                <asp:Label ID="Label15" runat="server" Text="Nenhum exame eletivo encontrado." Visible='<%#bool.Parse((DataList_ExameEletivo.Items.Count == 0).ToString()) %>'></asp:Label>
            </FooterTemplate>
        </asp:DataList>
    </div>
    </form>
</body>
</html>
