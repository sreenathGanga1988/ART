<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="ASQMonthShipmentClosing.aspx.cs" Inherits="ArtWebApp.Production.Schedular.ASQMonthShipmentClosing" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
  
    <link href="../../css/style.css" rel="stylesheet" />
  <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="../../JQuery/GridJQuery.js">
   
    <script type="text/javascript">

        


     </script>
    <style type="text/css">
       body
{
    margin: 0;
    padding: 0;
    font-family: Arial;
}
.modal
{
    position: fixed;
    z-index: 999;
    height: 100%;
    width: 100%;
    top: 0;
    background-color: Black;
    filter: alpha(opacity=60);
    opacity: 0.6;
    -moz-opacity: 0.8;
}
.center
{
    z-index: 1000;
    margin: 300px auto;
    padding: 10px;
    width: 130px;
    background-color: White;
    border-radius: 10px;
    filter: alpha(opacity=100);
    opacity: 1;
    -moz-opacity: 1;
}
.center img
{
    height: 128px;
    width: 128px;
}

   
      
       
        .smalltable {
            width: 50px;
        }
       
      
       
        .auto-style1 {
            height: 27px;
        }
       
      
       
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    
     
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate> 
<div class="FullTable">
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">asq Month Shipment closing </td>
        </tr>
        <tr>
            <td >



                 <table >
                        <tr>
                            <td class="auto-style1" colspan="4">

                                   &nbsp;</td>
                            </tr>

                        

                        <tr>
                            <td class="NormalTD">

                                Year</td>
                            <td class="NormalTD">
                                 <ucc:DropDownListChosen ID="cmb_year" runat="server" DisableSearchThreshold="10" Width="200px" DataSourceID="Year" DataTextField="YearName" DataValueField="YearName">
                                     <asp:ListItem>2017</asp:ListItem>
                                     <asp:ListItem>2018</asp:ListItem>
                                     <asp:ListItem>2019</asp:ListItem>
                                     <asp:ListItem>2020</asp:ListItem>
                                 </ucc:DropDownListChosen>
                    
                
                                 <asp:SqlDataSource ID="Year" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [YearName] FROM [YearMonthMaster]"></asp:SqlDataSource>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">Month</td>
                            <td class="NormalTD">
                                <ucc:DropDownListChosen ID="cmb_Month" runat="server" DisableSearchThreshold="10" Width="200px" DataSourceID="Month" DataTextField="MonthName" DataValueField="MonthNum">
                                </ucc:DropDownListChosen>
                                <asp:SqlDataSource ID="Month" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [MonthName], [MonthNum] FROM [YearMonthMaster] WHERE ([YearName] = @YearName)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="cmb_year" Name="YearName" PropertyName="SelectedValue" Type="String" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                            <td class="SearchButtonTD">
                                <asp:Button ID="S" runat="server" OnClick="Button3_Click1" Text="S" />
                              
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">From </td>
                            <td class="NormalTD">
                                <asp:Label ID="lbl_fromdate" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="SearchButtonTD">To</td>
                            <td>
                                <asp:Label ID="lbl_todate" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>

                             <tr>
                            <td class="NormalTD">Target locked</td>
                            <td class="NormalTD">
                                <asp:Label ID="lbl_Targetlocked" runat="server" Text="0"></asp:Label>
                            </td>
                            <td class="SearchButtonTD">Shipment Closed</td>
                            <td>
                                <asp:Label ID="lbl_shipmentclosed" runat="server" Text="0"></asp:Label>
                            </td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="auto-style1" colspan="4">
                                <asp:Button ID="Button2" runat="server" OnClick="Button1_Click" Text="Close Shipment of Month" />
                            </td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
            <td>
                
                <asp:SqlDataSource ID="Yeardata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT * FROM [YearMonthMaster]"></asp:SqlDataSource>
                <br />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="YearPk" DataSourceID="Yeardata">
                    <Columns>
                        <asp:BoundField DataField="YearPk" HeaderText="Y Pk" InsertVisible="False" ReadOnly="True" SortExpression="YearPk" />
                        <asp:BoundField DataField="YearName" HeaderText="Year" SortExpression="YearName" />
                        <asp:BoundField DataField="MonthName" HeaderText="Month" SortExpression="MonthName" />
                        <asp:BoundField DataField="MonthNum" HeaderText="Month" SortExpression="MonthNum" />
                        <asp:BoundField DataField="IsShipmentClose" HeaderText="Shipment Closed" SortExpression="IsShipmentClose" />
                        <asp:BoundField DataField="IsTargetLocked" HeaderText="Target Locked" SortExpression="IsTargetLocked" />
                        <asp:BoundField DataField="ClosedBy" HeaderText="Closed By" SortExpression="ClosedBy" />
                        <asp:BoundField DataField="ClosedDate" HeaderText="Closed Date" SortExpression="ClosedDate" />
                        <asp:BoundField DataField="TargetLockedBy" HeaderText="Target Locked By" SortExpression="TargetLockedBy" />
                        <asp:BoundField DataField="TargetLockedDate" HeaderText="Target Locked Date" SortExpression="TargetLockedDate" />
                        <asp:BoundField DataField="FromDate" HeaderText="FromDate" SortExpression="FromDate" />
                        <asp:BoundField DataField="ToDate" HeaderText="ToDate" SortExpression="ToDate" />
                    </Columns>
                </asp:GridView>
                
            </td>
        </tr>
    </table>
    </div>

<div>
        <table class="DataEntryTable">
                    <tr>
                      
                        <td class="auto-style8"><asp:UpdatePanel ID="upd_main" runat="server">
                                    <ContentTemplate>
                          

                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td class="NormalTD">
                           
                            <table class="DataEntryTable">
                                <tr>
                                    <td>&nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        
                                                <div id="Messaediv" runat="server">
                                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                                </div>
                                          
                                    </td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                           
                        </td>
                    
                    </tr>
                   
                    
                </table>
                    
        <br />
                    
    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


</asp:Content>