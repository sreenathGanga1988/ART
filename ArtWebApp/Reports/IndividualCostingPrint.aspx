<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IndividualCostingPrint.aspx.cs" Inherits="ArtWebApp.Reports.IndividualCostingPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
         .Masterdiv {
            
            width: 600px;
            height:100%;
           
        }

         .Mastertable {
            
            width: 600px;
            height:100%;
           border-left-style: solid;
            border-left-width: 2px;
            border-right-style: solid;
            border-right-width: 2px;
            border-top-style: solid;
            border-top-width: 2px;
            border-bottom-style: solid;
            border-bottom-width: 2px;
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
            border: 1px solid black;
           
        }
        .Subtittles{
            width:inherit;
            text-align: center;
            border-left-style: solid;
            border-top-style: solid;
            border-bottom-style: solid;
            border-right-style:solid;
            height: 31px;
        }
        .auto-style17 {
            font-size: x-small;
        }
        .auto-style19 {
            font-size: small;
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
        .auto-style33 {
            width:inherit;
            font-family: Calibri;
            font-size: small;
            border-style: solid;
            padding: 0;
        }
        .auto-style36 {
            width:inherit;
            padding: 0;
            font-size: small;
            border-left-style: solid;
            border-left-width: 1px;
            border-right-style: solid;
            border-right-width: 1px;
            border-top-style: solid;
            border-top-width: 1px;
            border-bottom-style: solid;
            border-bottom-width: 1px;
        }
        .auto-style37 {
            width:inherit;
            border-style: outset;
            padding: 0;
        }
        .auto-style39 {
            width: 83px;
            border-style: outset;
            padding: 0;
            height: 25px;
        }
        .auto-style42 {
            height: 23px;
        }
        .auto-style43 {
            width: 100%;
           
        }
        .auto-style44 {
            text-align: center;
            background-color: #996633;
        }
        .auto-style45 {
            height: 24px;
        }
        .auto-style46 {
            height: 23px;
            width: 83px;
        }
        .auto-style47 {
            height: 24px;
            width: 83px;
        }
        .auto-style48 {
            border-style: outset;
            padding: 0;
            width: 92px;
            height: 25px;
        }
        .auto-style49 {
            height: 24px;
            width: 92px;
        }
        .auto-style50 {
            height: 23px;
            width: 92px;
        }
        .auto-style51 {
            width: inherit;
            border-style: outset;
            padding: 0;
            height: 25px;
        }
        .auto-style52 {
            width: 102px;
            border-style: outset;
            padding: 0;
            height: 25px;
        }
        .auto-style53 {
            width: 62px;
            border-style: outset;
            padding: 0;
            height: 25px;
        }
        .auto-style54 {
            height: 24px;
            width: 62px;
        }
        .auto-style55 {
            height: 23px;
            width: 62px;
        }
        .auto-style56 {
            width: 267px;
        }
        .auto-style57 {
            text-align: left;
            background-color: #996633;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
   

<table class="Mastertable">
    <tr>
        <td>


            <table >
        <tr>
            <td colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="auto-style3">
                            <table class="auto-style2" border="1" style="border-collapse:collapse;"">
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
                        <td class="auto-style56">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: Calibri" colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="Subtittles"><strong>COSTING</strong></td>
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
                                <tr class="auto-style19">
                                    <td class="auto-style13">STYLE COSTING #:</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_costid" runat="server" Text="Label"></asp:Label>
                                                </td>
                                    <td class="auto-style7">COSTINGDATE</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_costdate" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="Subtittles"><strong>COSTING SUMMARY</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style15" bo>
                          
                            <table  class="auto-style43" border="1" style="border-collapse:collapse; font-family: Calibri; font-size: medium; font-weight: bold; font-style: normal"">
                                <tr>
                                    <td class="auto-style44">&nbsp;Components &nbsp;</td>
                                    <td class="auto-style57">Cost/Pc</td>
                                    <td class="auto-style57">Cost/Dz</td>
                                    <td class="auto-style57">% of FOB</td>
                                </tr>
                                <tr>
                                    <td>Fabric</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fabriccosts" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fabriccostperdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fabricpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style42">Trims</td>
                                    <td class="auto-style42">
                                                    <asp:Label ID="lblsummary_trimcost" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style42">
                                                    <asp:Label ID="lblsummary_trimcostdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style42">
                                                    <asp:Label ID="lblsummary_trimpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style45">CM Charges</td>
                                    <td class="auto-style45">
                                                    <asp:Label ID="lblsummary_cmcost" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style45">
                                                    <asp:Label ID="lblsummary_cmdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td class="auto-style45">
                                                    <asp:Label ID="lblsummary_cmpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Washing</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_washcost" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_washdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_washpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Embroidary Charges</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_Embroidary" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_Embroidary0" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_Embroidarypercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Commision Charges</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_comision" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_comisiondzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_comisionpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Other Charges</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_otherscost" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_othersdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_otherspercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Total Cost</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_totalcost" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_totalcostDZN" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_totalcostpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>FOB Value</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fobvalue" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fobvaluedzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_fobvaluepercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Margin Value</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_Marginvalue" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_Marginvaluedzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_marginvaluepercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td>Margin %</td>
                                    <td>
                                                    <asp:Label ID="lblsummary_marginpercent" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_marginpercentdzn" runat="server" Text="0"></asp:Label>
                                                </td>
                                    <td>
                                                    <asp:Label ID="lblsummary_others14" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style42"></td>
                                    <td class="auto-style42"></td>
                                    <td class="auto-style42"></td>
                                    <td class="auto-style42"></td>
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
                            <asp:GridView ID="tbl_fabdet" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="600px" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" BorderColor="#333300" />
                                <Columns>
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNO" />
                                    <asp:BoundField DataField="ItemDescription" HeaderText="Raw Material  Description" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                    <asp:BoundField DataField="Consumption" HeaderText="Consumption" />
                                    <asp:BoundField DataField="Priceperpc" HeaderText="Priceperpc" />
                                    <asp:BoundField DataField="PriceperDozen" HeaderText="PriceperDozen" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="Black" />
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
            <td style="font-size: small; font-family: Calibri" class="auto-style52">
                <strong>Total Fabric Cost</strong></td>
            <td style="font-size: small; font-family: Calibri" class="auto-style51">
                </td>
            <td style="font-size: small; font-family: Calibri" class="auto-style48">
                                                    <asp:Label ID="lbl_fabricdet" runat="server" Text="0" style="font-weight: 700"></asp:Label>
                                                </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style33" colspan="3"><strong>TRIMS COST DETAILS</strong></td>
                    </tr>
        <tr>
            <td style="font-size: small; font-family: Calibri" class="auto-style37" colspan="3">
                            <asp:GridView ID="tbl_trmdet" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" Width="600px" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False">
                                <AlternatingRowStyle BackColor="White" BorderColor="#333300" />
                                <Columns>
                                    <asp:BoundField DataField="RMNum" HeaderText="RMNO" />
                                    <asp:BoundField DataField="ItemDescription" HeaderText="Raw Material  Description" />
                                    <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                    <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                    <asp:BoundField DataField="Consumption" HeaderText="Consumption" />
                                    <asp:BoundField DataField="Priceperpc" HeaderText="Priceperpc" />
                                    <asp:BoundField DataField="PriceperDozen" HeaderText="PriceperDozen" />
                                </Columns>
                                 <FooterStyle BackColor="#CCCC99" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="Black" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <RowStyle BackColor="#F7F7DE" BorderColor="Gray" BorderStyle="Solid" BorderWidth="1px" />
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
                <strong>Total Trim Cost</strong></td>
            <td style="font-size: small; font-family: Calibri" class="auto-style53">
                </td>
            <td style="font-size: small; font-family: Calibri" class="auto-style48">
                                                    <asp:Label ID="lbl_trimdet" runat="server" Text="0" style="font-weight: 700"></asp:Label>
                                                </td>
                    </tr>
            <tr>

                <td class="auto-style47">Approved By</td>
                 <td class="auto-style54">
                     <asp:Label ID="lbl_approvedby" runat="server"></asp:Label>
                </td>

                 <td class="auto-style49"></td>

            </tr>
            <tr>

                <td class="auto-style46">Appr Date</td>
                 <td class="auto-style55">
                     <asp:Label ID="lbl_approveddate" runat="server"></asp:Label>
                </td>

                 <td class="auto-style50"></td>

            </tr>
                </table>

        </td>

    </tr>

</table>


        
        
    
    </form>
</body>
</html>
