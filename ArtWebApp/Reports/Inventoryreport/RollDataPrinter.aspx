<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RollDataPrinter.aspx.cs" Inherits="ArtWebApp.Reports.Inventoryreport.RollDataPrinter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
          .Masterdiv {
            
            width: 600px;
            height:100%;
           
        }
    </style>
</head>
<body>


    <div class="Masterdiv" >

        <form id="form1" runat="server">
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <asp:SqlDataSource ID="RollGridSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        FabricRollmaster.Roll_PK, FabricRollmaster.RollNum, ISNULL(FabricRollmaster.SYard, 0) AS SYard, ISNULL(FabricRollmaster.AYard, 0) AS AYard, ISNULL(FabricRollmaster.AShade, 0) AS AShade, 
                         ISNULL(FabricRollmaster.MarkerType, 0) AS MarkerType, ISNULL(FabricRollmaster.AShrink, 0) AS AShrink, ISNULL(FabricRollmaster.ShrinkageGroup, 0) AS ShrinkageGroup, ISNULL(FabricRollmaster.AWidth, 0) 
                         AS AWidth, ISNULL(FabricRollmaster.WidthGroup, 0) AS WidthGroup, FabricRollmaster.SupplierDoc_pk,  SkuRawMaterialMaster.RMNum + ' ' + SkuRawMaterialMaster.Composition + ' ' + SkuRawMaterialMaster.Construction + ' ' + ISNULL(SkuRawMaterialMaster.Weight, '') + ISNULL(SkuRawMaterialMaster.Width, '') 
                         + '   ' + ISNULL(SkuRawmaterialDetail.ItemColor, '')+ ' (  ' + ISNULL(SkuRawmaterialDetail.ColorCode, '')+ '  ) '   + '   ' + ISNULL(SkuRawmaterialDetail.ItemSize, '') AS ItemDescription
FROM            FabricRollmaster INNER JOIN
                         SkuRawmaterialDetail ON FabricRollmaster.SkuDet_PK = SkuRawmaterialDetail.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk
WHERE        (FabricRollmaster.SupplierDoc_pk = @param1)">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="TextBox1" Name="param1" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="GridView1" runat="server" Width="600px" AutoGenerateColumns="False" DataSourceID="RollGridSource" OnRowDataBound="GridView1_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Roll" SortExpression="Roll">
                              
                                <ItemTemplate>


                                    <table>


                                    </table>

                                    <table class="auto-style1">
                                           <tr>
                                            <td colspan="6">
                                                <div ><h1 class="auto-style2">  <asp:Label ID="lbl_rollnum" Font-Size="Larger" runat="server" Text='<%# Bind("RollNum") %>' Font-Bold="True" Font-Underline="True"></asp:Label></h1></div>
                                            
                                            
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td colspan="6">
                                                <asp:Label ID="Label1" Text='<%# Bind("ItemDescription") %>'  runat="server" Font-Bold="True" Font-Size="Large" ></asp:Label>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="lbl_rollPk" Text='<%# Bind("Roll_PK") %>'  runat="server" Font-Bold="True" Font-Size="XX-Small" ></asp:Label>
                         </td>
                                            <td rowspan="4">
                                                <asp:Image ID="Image1" Height="150px" Width="150px" runat="server" />
                                            </td>
                                            <td>Syard</td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("SYard") %>'></asp:Label>
                                            </td>
                                            <td>A yards</td>
                                            <td>
                                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("AYard") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>A shade</td>
                                            <td>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("AShade") %>'></asp:Label>
                                            </td>
                                            <td>A shrinkage</td>
                                            <td>
                                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("AShrink") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>Shrinkage Group</td>
                                            <td>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("ShrinkageGroup") %>'></asp:Label>
                                            </td>
                                            <td>Width Group</td>
                                            <td>
                                                <asp:Label ID="Label8" runat="server" Text='<%# Bind("WidthGroup") %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td>A width</td>
                                            <td>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("AWidth") %>'></asp:Label>
                                            </td>
                                            <td>Type</td>
                                            <td>
                                                <asp:Label ID="Label9" runat="server" Text='<%# Bind("MarkerType") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    <div>
    
    </div>
    </form>
    </div>
    
</body>
</html>
