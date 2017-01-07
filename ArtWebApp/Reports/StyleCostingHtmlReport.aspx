<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StyleCostingHtmlReport.aspx.cs" Inherits="ArtWebApp.Reports.StyleCostingHtmlReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 600px;
        }
        .auto-style2 {
            width: 100%;
            font-size: medium;
            font-family: Calibri;
            height: 4px;
        }
        .auto-style3 {
            width: 329px;
        }
        .auto-style4 {
            text-align: left;
            font-size: x-small;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style7 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: left;
        }
        .auto-style10 {
            font-size: 16pt;
        }
        .auto-style11 {
            text-align: center;
            font-size: large;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style12 {
            width: 141px;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style13 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: left;
            width: 141px;
        }
        .auto-style14 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style15 {
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
            height: 155px;
        }
        .auto-style16 {
            text-align: center;
            border-left-style: solid;
            border-top-style: outset;
            border-bottom-style: outset;
        }
        .auto-style17 {
            font-size: x-small;
        }
        .auto-style19 {
            font-size: small;
        }
        .auto-style20 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
        }
        .auto-style23 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 17px;
            width: 65px;
        }
        .auto-style24 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 16px;
            width: 70px;
        }
        .auto-style25 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 12px;
        }
        .auto-style27 {
            width: 100%;
            font-size: small;
            font-family: Calibri;
        }
        .auto-style28 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: center;
        }
        .auto-style30 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 71px;
            font-weight: bold;
        }
        .auto-style31 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 65px;
        }
        .auto-style32 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 70px;
        }
        .auto-style33 {
            font-family: Calibri;
            font-size: small;
            border-style: outset;
            padding: 0;
        }
        .auto-style34 {
            font-size: x-small;
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 7px;
        }
        .auto-style35 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            width: 71px;
            font-weight: bold;
            height: 7px;
        }
        .auto-style36 {
            padding: 0;
            font-size: small;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-style: outset;
            border-right-width: 1px;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-style: outset;
            border-bottom-width: 1px;
        }
        .auto-style37 {
            border-style: outset;
            padding: 0;
        }
        .auto-style38 {
            width: 80px;
            padding: 0;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-style: outset;
            border-right-width: 1px;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-style: outset;
            border-bottom-width: 1px;
        }
        .auto-style39 {
            border-style: outset;
            padding: 0;
        }
        .auto-style40 {
            padding: 0;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-style: outset;
            border-right-width: 1px;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-style: outset;
            border-bottom-width: 1px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
    <div class="auto-style1">
        <table class="auto-style1">
        <tr>
            <td colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style3">
                            <table class="auto-style2">
                                <tr>
                                    <td class="auto-style10"><strong>ATRACO INDUSTRIAL ENTERPRISES</strong></td>
                                </tr>
                                <tr>
                                    <td>P.O Box 16798,Dubai -U.A.E</td>
                                </tr>
                                <tr>
                                    <td>Tel:(971 4)8812686</td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: Calibri" colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style11"><strong>COSTING</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            
                            <table class="auto-style2">
                                <tr class="auto-style19">
                                    <td class="auto-style12">ATC #:</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_atc" runat="server" Text="Label"></asp:Label>
                                                </td>
                                    <td class="auto-style7">BUYER STYLE # :</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_buyerstyle" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13">STYLE GROUP #:</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_oursrtyle" runat="server" Text="Label"></asp:Label>
                                                </td>
                                    <td class="auto-style7">QUANTITY # : </td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_qty" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style16"><strong>COSTING SUMMARY</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style15">
                            <table class="auto-style2">
                                <tr>
                                    <td>
                                        <table class="auto-style2">
                                            <tr>
                                                <td class="auto-style20" colspan="2"><strong>FIRST COSTING</strong></td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>COSTING #</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_costingID1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>COSTING DATE :</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_costingdate1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style34"><strong>FOB :</strong></td>
                                                <td class="auto-style35">
                                                    <asp:Label ID="lbl_fob1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>TOTAL COST :</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_totalcost1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>MARGIN VALUE :</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_marginvalue1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style20"><strong>MARGIN :</strong></td>
                                                <td class="auto-style30">
                                                    <asp:Label ID="lbl_margin1" runat="server" Text="0" CssClass="auto-style17"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style31"><strong>3rd LAST COST</strong></td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style31">
                                                    <asp:Label ID="lbl_costingID2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style31">
                                                    <asp:Label ID="lbl_costingdate2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style31">
                                                    <asp:Label ID="lbl_fob2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style23">
                                                    <asp:Label ID="lbl_totalcost2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style31">
                                                    <asp:Label ID="lbl_marginvalue2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style31">
                                                    <asp:Label ID="lbl_margin2" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style32"><strong>2nd LAST COST</strong></td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <asp:Label ID="lbl_costingID3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <asp:Label ID="lbl_costingdate3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <asp:Label ID="lbl_fob3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style24">
                                                    <asp:Label ID="lbl_totalcost3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <asp:Label ID="lbl_marginvalue3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style32">
                                                    <asp:Label ID="lbl_margin3" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="font-weight: 700">
                                        <table class="auto-style2">
                                            <tr class="auto-style17">
                                                <td class="auto-style14"><strong>FINAL COSTING</strong></td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_costingID4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_costingdate4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_fob4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style25">
                                                    <asp:Label ID="lbl_totalcost4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_marginvalue4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="auto-style17">
                                                <td class="auto-style14">
                                                    <asp:Label ID="lbl_margin4" runat="server" Text="0"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style19">
                            <table class="auto-style27">
                                <tr>
                                    <td class="auto-style28" colspan="2"><strong>FIXED COMPONENT</strong></td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">FABRIC</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_fabric" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">TRIMS</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_trim" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">CM</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_cm" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table class="auto-style27">
                                <tr>
                                    <td class="auto-style28" colspan="4"><strong>OPTIONAL COMPONENT</strong></td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">WASH</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_wash" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">COMPANY LOGISTICS</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_companylogistic" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">DRY PROCESS</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_dryprocess" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">FACTORY LOGISTICS</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_factorylogistic" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">FAB COMMISION</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_fabcommision" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">OTHERS</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_others" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style17">
                                    <td class="auto-style14">GARMENT COMMISION</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_garmentcom" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style14">&nbsp;</td>
                                    <td class="auto-style14">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style36" colspan="3"><strong>FABRIC COST DETAILS</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style36" colspan="3">
                            <asp:GridView ID="tbl_fabdet" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="600px" ForeColor="Black" GridLines="Vertical">
                                <AlternatingRowStyle BackColor="White" />
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FBFBF2" />
                                <SortedAscendingHeaderStyle BackColor="#848384" />
                                <SortedDescendingCellStyle BackColor="#EAEAD3" />
                                <SortedDescendingHeaderStyle BackColor="#575357" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: small" class="auto-style38">
                            SUM</td>
                        <td style="font-size: small" class="auto-style40">
                            &nbsp;</td>
                        <td style="font-size: small" class="auto-style40">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style33" colspan="3"><strong>TRIMS COST DETAILS </strong></td>
                    </tr>
        <tr>
            <td style="font-size: small; font-family: Calibri" class="auto-style37" colspan="3">
                <asp:GridView ID="tbl_trmdet" runat="server" Width="589px" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="White" />
                    <FooterStyle BackColor="#CCCC99" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <RowStyle BackColor="#F7F7DE" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FBFBF2" />
                    <SortedAscendingHeaderStyle BackColor="#848384" />
                    <SortedDescendingCellStyle BackColor="#EAEAD3" />
                    <SortedDescendingHeaderStyle BackColor="#575357" />
                </asp:GridView>
                        </td>
                    </tr>
        <tr>
            <td style="font-size: small; font-family: Calibri" class="auto-style39">
                SUM</td>
            <td style="font-size: small; font-family: Calibri" class="auto-style37">
                &nbsp;</td>
            <td style="font-size: small; font-family: Calibri" class="auto-style37">
                &nbsp;</td>
                    </tr>

               <tr>
            <td style="font-size: small; font-family: Calibri" class="auto-style39" colspan="3">
               <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit For Approval" Width="100%" /></td>
                    </tr>
                </table>
      <%--      </td>
        </tr>
        <tr>
            <td>
                </td>
        </tr>
    </table>--%>
    </div>
    </form>
</body>
</html>
