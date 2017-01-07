<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CutorderReport.aspx.cs" Inherits="ArtWebApp.Reports.Production.CutOrderReport.CutorderReport" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
         .Masterdiv {
            
            width: 600px;
            height:100%;
           
        }
           .hidden {
            display: none;
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
            height: 20px;
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
        .auto-style19 {
            font-size: small;
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
            width: 460px;
        }
        .auto-style45 {
            height: 24px;
        }
        .auto-style56 {
            width: 267px;
        }
        .auto-style57 {
            text-align: left;
            background-color: #996633;
        }
        .auto-style58 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            height: 20px;
        }
        .auto-style59 {
            border-style: solid;
            border-width: 1px;
            padding: 1px 4px;
            text-align: left;
            height: 20px;
        }
        .auto-style60 {
            width: 460px;
        }
        .auto-style61 {
            height: 23px;
            width: 460px;
        }
        .auto-style62 {
            height: 24px;
            width: 460px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
   

<table class="Masterdiv">
    <tr>
        <td>


            <table class="Masterdiv">
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
                        <td class="auto-style56">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="font-family: Calibri" colspan="3">
                <table class="auto-style2">
                    <tr>
                        <td class="Subtittles"><strong>CutOrder</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">
                            
                            <table class="auto-style2">
                                <tr class="auto-style19">
                                    <td class="auto-style12">CUT ORDER #</td>
                                    <td class="auto-style58">
                                                    <asp:Label ID="lbl_cutorder" runat="server"></asp:Label>
                                                </td>
                                    <td class="auto-style59">TO</td>
                                    <td class="auto-style58">
                                                <asp:Label ID="lbl_location" runat="server" Text="Label"></asp:Label></td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13">&nbsp;ATC #:</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_atc" runat="server" Text="Label"></asp:Label>
                                                </td>
                                    <td class="auto-style7">OUR STYLE # :</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_oursrtyle" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13">CUT ORDER TYPE #:</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_cutordertype" runat="server" Text="Label"></asp:Label>
                                                </td>
                                    <td class="auto-style7">CUT ORDER DATE</td>
                                    <td class="auto-style14">
                                                    <asp:Label ID="lbl_cutorderDate" runat="server" Text="Label"></asp:Label>
                                                </td>
                                </tr>
                                <tr class="auto-style19">
                                    <td class="auto-style13">Color</td>
                                    <td class="auto-style14" colspan="3">
                                                   <strong>
                                                   <asp:Label ID="lbl_color" runat="server" Text="Label"></asp:Label></strong></td>
                                </tr>
                            </table>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="Subtittles"><strong>CUT ORDER DETAIL</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style15" >
                          
                            <table  class="auto-style43" border="1" style="border-collapse:collapse; font-family: Calibri; font-size: medium; font-weight: bold; font-style: normal"">
                                <tr>
                                    <td class="auto-style44">&nbsp;Components &nbsp;</td>
                                    <td class="auto-style57">Value</td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Fab Qty</td>
                                    <td>
                                                    <asp:Label ID="lbl_fabricQty" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style61">Cut Qty</td>
                                    <td class="auto-style42">
                                                    <asp:Label ID="lbl_cutqty" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style62">Consumption</td>
                                    <td class="auto-style45">
                                                    <asp:Label ID="lbl_consumption" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Shrinkage</td>
                                    <td>
                                                    <asp:Label ID="lbl_shrinkage" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Cut Width</td>
                                    <td>
                                                    <asp:Label ID="lbl_cutwidth" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Shrinkage</td>
                                    <td>
                                                    <asp:Label ID="lblShrinkagge" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Marker Type</td>
                                    <td>
                                                    <asp:Label ID="lbl_markertype" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Marker/Patern Name</td>
                                    <td>
                                                    <asp:Label ID="lbl_markername" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Reason</td>
                                    <td>
                                                    <asp:Label ID="lbl_reason" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Added By</td>
                                    <td>
                                                    <asp:Label ID="lbl_addedBy" runat="server" Text="0"></asp:Label>
                                                </td>
                                </tr>
                                <tr>
                                    <td class="auto-style60">Added Date</td>
                                    <td>
                                                    <asp:Label ID="lbl_Addeddate" runat="server" Text="0"></asp:Label>
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
                <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
                                            <ContentTemplate>

                                                <asp:GridView ID="tbl_cutorderdata" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="CutOrderDet_PK" DataSourceID="SqlDataSource1" OnDataBound="tbl_cutorderdata_DataBound">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="PK" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" InsertVisible="False" SortExpression="CutOrderDet_PK">
                                                          
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_CutOrderDet_PK" runat="server" Text='<%# Bind("CutOrderDet_PK") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerNo" SortExpression="MarkerNo">
                                                         
                                                            <ItemTemplate>                                                             
                                                                 
                                                                
                                                                 
                                                                  
                                                               
                                                                <table class="tittlebar" style=" width: inherit; border-style: solid; background-color: #FFFFFF">
                                                                    <tr>
                                                                        <td>Marker Num</td>
                                                                        <td><asp:Label ID="Label1" Width="70" runat="server" Text='<%# Bind("MarkerNo") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>NoOfPc</td>
                                                                        <td><asp:Label ID="Label2" Width="70" runat="server" Text='<%# Bind("NoOfPc") %>'></asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>Qty</td>
                                                                        <td><asp:Label ID="lbl_totalQty" Width="70" CssClass="num" runat="server" onkeypress="return isNumberKey(event,this)"  onkeyup ="SplitQty(this)"   Text ='<%# Bind("Qty") %>'> </asp:Label></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>MarkerLength</td>
                                                                        <td><asp:Label ID="Label4" Width="70" runat="server" Text='<%# Bind("MarkerLength") %>'></asp:Label>;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>LayLength</td>
                                                                        <td><asp:Label ID="Label5" Width="70" runat="server" Text='<%# Bind("LayLength") %>'></asp:Label></td>
                                                                    </tr>
                                                                </table>
                                                            </ItemTemplate>
                                                            <ControlStyle Width="120px" />
                                                            <FooterStyle Width="200px" />
                                                            <HeaderStyle Width="120px" />
                                                            <ItemStyle Width="120px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MarkerDetails" SortExpression="MarkerDetails">
                                                          
                                                            <ItemTemplate>



                                                               
                            <asp:UpdatePanel ID="upd_table" runat="server">
                                            <ContentTemplate>
                            
                            <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                <asp:Table ID="Table1" runat="server" ViewStateMode="Enabled" Width="400px">
                                </asp:Table>
                            </asp:Panel>
                                                
                                                </ContentTemplate>
                                        </asp:UpdatePanel>



                                                               
                                                            </ItemTemplate>
                                                                <ControlStyle Width="300px" />
                                                            <FooterStyle Width="300px" />
                                                            <HeaderStyle Width="300px" />
                                                        </asp:TemplateField>
                                                        
                                                      
                                                    </Columns>
                                    <FooterStyle BackColor="#FFFFCC" ForeColor="#330099" />
                                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                                    <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                                    <RowStyle BackColor="White" ForeColor="#330099" />
                                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="#663399" />
                                    <SortedAscendingCellStyle BackColor="#FEFCEB" />
                                    <SortedAscendingHeaderStyle BackColor="#AF0101" />
                                    <SortedDescendingCellStyle BackColor="#F6F0C0" />
                                    <SortedDescendingHeaderStyle BackColor="#7E0000" />
                                </asp:GridView>

                                                      </ContentTemplate>
                                        </asp:UpdatePanel></td>
        </tr>
        <tr>
            <td colspan="3" class="auto-style45">
                
                                                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [CutOrderDet_PK], [MarkerNo], [NoOfPc], [Qty], [MarkerLength], [LayLength] FROM [CutOrderDetails] WHERE ([CutID] = @CutID)">
                                                    <SelectParameters>
                                                        <asp:SessionParameter DefaultValue="0" Name="CutID" SessionField="Cut_PKreport" Type="Decimal" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                
            </td>
        </tr>
     
        
          
            
                </table>

        </td>

    </tr>

</table>


        
        
    
    </form>
</body>
</html>

