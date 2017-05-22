<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PackingInstructionMaster.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.PackingInstructionMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
  
    <link href="../../css/style.css" rel="stylesheet" />

    <script src="../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript">

   
    function totalcalculation()
       {

      //  debugger;
           var gridView = document.getElementById("<%= tbl_podata.ClientID %>");

        var txt_totalctn = document.getElementsByClassName("txt_totalctn")[0];
        var txt_pcperctn = document.getElementsByClassName("txt_pcperctn")[0];

        
        
        var lbl_totalpcexpected = document.getElementsByClassName("lbl_totalpcexpected")[0];
        var lbl_totalpcadded = document.getElementsByClassName("lbl_totalpcadded")[0];
        
        if (txt_totalctn.value == "")
        {
            alert("Enter Total CTN")
        }
        else  if (txt_totalctn.value == "0") {
            alert(" Total CTN Cannot be 0")
        }
        else if (txt_pcperctn.value == "") {
            alert("Enter PC/CTN")
        }
        else if (txt_totalctn.value == "0") {
            alert(" Total PC/CTN Cannot be 0")
        }
        else
        {


            var expectedqty = parseFloat(txt_totalctn.value) * parseFloat(txt_pcperctn.value);
            lbl_totalpcexpected.innerHTML = expectedqty.toString();
            
            
            var grandtotal = 0;
            var ratiototal = 0;

            for (var rownum = 1; rownum < gridView.rows.length; rownum++) {
                var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {

                    var pendingtable = gridView.rows[rownum].getElementsByClassName("PendingTable")[0];

                    var newTable = gridView.rows[rownum].getElementsByClassName("newTable")[0];
                    var calctable = gridView.rows[rownum].getElementsByClassName("calctable")[0];

                    for (var i = 0; i < newTable.rows.length; i++) {
                        var row = newTable.rows[i];
                        for (var j = 0; j < row.cells.length; j++) {
                            var textboxs = row.cells[j].getElementsByClassName("Qty");
                            var pendingqty = pendingtable.rows[i].cells[j].getElementsByClassName("Qty");
                            var calqty = calctable.rows[i].cells[j].getElementsByClassName("Qty");
                            for (var k = 0; k < textboxs.length; k++) {

                                if (textboxs[k].value== "")
                                {
                                    textboxs[k].value=0;
                                }
                                var totalqty = parseFloat(textboxs[k].value) * parseFloat(txt_totalctn.value);

                                if (parseFloat(totalqty) > parseFloat(pendingqty[k].value))
                                {
                                    alert("New Pack Qty Exceeds the Pending Qty")
                                    textboxs[k].value = 0;
                                    totalqty = 0;
                                }
                           
                            //   else if()
                            //{

                            //}
                                calqty[k].value = totalqty;
                                if (parseFloat(ratiototal) > parseFloat(txt_pcperctn.value))
                                {
                                    alert("Ratio  Exceeds the PC/CTN")
                                    textboxs[k].value = 0;
                                    totalqty = 0;
                                }
                                var temgrandqty= parseFloat(grandtotal) + parseFloat(totalqty);
                                if (parseFloat(temgrandqty) > parseFloat(expectedqty)) {
                                    alert("Total Qty  Exceeds the Total ")
                                    textboxs[k].value = 0;
                                    totalqty = 0;
                                }
                                grandtotal = parseFloat(grandtotal) + parseFloat(totalqty);
                                ratiototal = parseFloat(ratiototal) + parseFloat(textboxs[k].value);
                            }
                        }
                    }



                }

                lbl_totalpcadded.innerHTML = grandtotal.toString();
            }

          

        }
          
           
          

         





         
       }

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
</style>

   
    -
    <style type="text/css">
        .auto-style8 {
            width: 202px;
        }
        .auto-style9 {
            width: 177px;
        }
        .auto-style10 {
            width: 170px;
        }
    </style>
    <style type="text/css">
        .auto-style8 {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 542px;
            height: 117px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 5px;
        }
    </style>
    <style type="text/css">
        .auto-style1 {
            width: 181px;
            height: 45px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   
        <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">Packing instruction<strong> </strong></td>
        </tr>
        <tr>
            
            <td class="DataEntryTable">

                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
                    <div>



                        <table class="auto-style1">
                            <tr>
                              <td class="NormalTD">

                                   Atc# 
                
                            </td>
                            <td class="NormalTD">
                               
                   
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcdatasource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                               
                            </td>
                            <td class="SearchButtonTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>
                            <tr>
                                 <td class="NormalTD">

                                PO Pack </td>
                            <td class="NormalTD">
                                 <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                     <ContentTemplate>
                                         <ig:WebDropDown ID="drp_popack" runat="server" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId" Width="200px">
                                                <DropDownItemBinding TextField="name" ValueField="pk" />
                                            </ig:WebDropDown>
                                         
                                     </ContentTemplate>
                                 </asp:UpdatePanel>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                <asp:Button ID="buttonAtc0" runat="server" Height="26px" Text="S" OnClick="buttonAtc0_Click" />
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>
                              <tr>
                            <td class="NormalTD">
                    
      
                                Packing Type</td>
                                 <td class="NormalTD">

                                     <ucc:DropDownListChosen ID="drp_type" runat="server" DisableSearchThreshold="10" Width="200px">
                                         <asp:ListItem Value="Ass Color Ass Size">Ass Color Ass Size</asp:ListItem>
                                          <asp:ListItem Value="Solid Color Assc Size">Solid Color Assc Size</asp:ListItem>
                                          <asp:ListItem Value="Solid Color Solid Size">Solid Color Solid Size</asp:ListItem>
                                          <asp:ListItem Value="Assc Color Solid Size">Assc Color Solid Size"</asp:ListItem>
                                          
                                         <asp:ListItem></asp:ListItem>
                            </ucc:DropDownListChosen>


                                    
                                </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="S" />
                                </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="4"><table class="gridtable">
                                    <tr>
                                        <td  >No of ctn</td>
                                        <td>pc per ctn</td>
                                        <td>&nbsp;</td>
                                        <td>Dimension</td>
                                        <td>Total PC</td>
                                        <td>Added PC</td>
                                        <td>Polybag Pc/Ctn</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txt_totalctn" CssClass="txt_totalctn" onchange="totalcalculation()" Width="100px" runat="server"></asp:TextBox>
                                        </td>
                                        <td> <asp:TextBox ID="txt_pcperctn" CssClass="txt_pcperctn" onchange="totalcalculation()"  Width="100px" runat="server"></asp:TextBox></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_totalpcexpected" CssClass="lbl_totalpcexpected" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_totalpcadded" CssClass="lbl_totalpcadded" runat="server" Text="0"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txt_polybagperctn" runat="server">0</asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="7">
                                            <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;">
                                                
                                                <tr>
                                                  
                                                    <td>
                                                        <table class="auto-style1" style="background-color: #FF9966">
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_alllength" runat="server" CssClass="txt_alllength" placeholder="Length" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>X</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_allwidth" runat="server" CssClass="txt_allwidth" placeholder="Width" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>X</td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_allheight" runat="server" CssClass="txt_allheight" placeholder="Height" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drp_weightuomll" runat="server" CssClass="drp_weightuomll">
                                                                        <asp:ListItem Value="KG">KG</asp:ListItem>
                                                                        <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                              
                                                            </tr>
                                                          
                                                        </table>
                                                    </td>
                                                    <td>
                                                        <table class="auto-style1" style="background-color: #00FFFF">
                                                            <tr>
                                                                <td style="background-color: #00FFFF">
                                                                    <asp:TextBox ID="txt_NNWeightAll" runat="server" CssClass="txt_NNWeightAll" placeholder="NNWeight" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_NetAll" runat="server" CssClass="txt_NetAll" placeholder="Net" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_grossAll" runat="server" CssClass="txt_grossAll" placeholder="Gross" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="drp_NetUOMALL" runat="server" CssClass="drp_NetUOMALL">
                                                                        <asp:ListItem Value="KG">KG</asp:ListItem>
                                                                        <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td>&nbsp;</td> </tr>
                                                         
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table></td>
                            </tr>
                        </table>



                    </div>

                    <table class="DataEntryTable">

                

                        <tr>
                            <td class="NormalTD" colspan="4">
                                <asp:UpdatePanel ID="updgrid" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>
                                        


                                           <table >
                                    <tr>
                                        <td class="auto-style10"  ><asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPackId" OnRowCommand="tbl_podata_RowCommand" OnRowDataBound="tbl_podata_RowDataBound" OnSelectedIndexChanged="tbl_podata_SelectedIndexChanged" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-CssClass="hidden" HeaderText="IDS" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <table class="tittlebar">
                                                            <tr>
                                                                <td>POPAckid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Outstyleid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_ourstyleid" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>atcid</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_atcid" runat="server" Text='<%# Bind("AtcId") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>lctn_pk</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_Location_PK" runat="server" Text='<%# Bind("Locaion_PK") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="hidden" />
                                                    <ItemStyle CssClass="hidden" />
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ASQ Details">
                                                    <ItemTemplate>
                                                        <table style="font-size: small; width: 150px"  >
                                                            <tr>
                                                                <td>ASQ</td>
                                                                <td>
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("ASQ") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>Location</td>
                                                                <td>
                                                                    <asp:Label ID="na" runat="server" Text='<%# Bind("LocationName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerPO</td>
                                                                <td>
                                                                    <asp:Label ID="na1" runat="server" Text='<%# Bind("BuyerPO") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>BuyerStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na2" runat="server" Text='<%# Bind("BuyerStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>OurStyle</td>
                                                                <td>
                                                                    <asp:Label ID="na3" runat="server" Text='<%# Bind("OurStyle") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ControlStyle Width="200px" />
                                                    <FooterStyle Width="200px" />
                                                    <HeaderStyle Width="200px" />
                                                    <ItemStyle Width="200px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pending" SortExpression="Details">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upd_table" runat="server">
                                                            <ContentTemplate>

                                                                  <table class="auto-style8">
                                                                       <tr>
                                                                            <td>Pending
                                                                                </td>
                                                                           </tr>

                                                                        <tr>
                                                                            <td>   <asp:Panel ID="panel1" runat="server" ViewStateMode="Enabled">
                                                                  
                                                                    <asp:Table ID="Table1" runat="server" Font-Size="X-Small" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>New
                                                                                </td>
                                                                           </tr>
                                                                        <tr>
                                                                            <td> 
                                                                                 <asp:Panel ID="panel3" runat="server" ViewStateMode="Enabled">
                                                                  
                                                                    <asp:Table ID="Table3" runat="server" Font-Size="X-Small" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel></td>
                                                                        </tr>
                                                                    </table>

                                                             
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="New" SortExpression="New">
                                                    <ItemTemplate>
                                                        <asp:UpdatePanel ID="upd_table2" runat="server">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="panel2" runat="server" ViewStateMode="Enabled">
                                                                    <asp:Table ID="Table2" runat="server" Font-Size="X-Small" ViewStateMode="Enabled" Width="400px">
                                                                    </asp:Table>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </ItemTemplate>
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
                                        </asp:GridView></td>
                                     
                                    </tr>
                                    <tr>
                                        <td >
                                             <textarea id="txt_instruction" cols="20" runat="server" name="S1" rows="2"></textarea></td>
                                       
                                    </tr>
                                               <tr>
                                                   <td class="auto-style10">
                                                       <asp:UpdatePanel ID="upd_bttn" runat="server" UpdateMode="Conditional">
                                                           <ContentTemplate>
                                                               <asp:Button ID="btn_savelist" runat="server" OnClick="btn_savelist_Click" Text="Save packing List" />
                                                           </ContentTemplate>
                                                       </asp:UpdatePanel>
                                                   </td>
                                               </tr>
                                </table>







                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD" colspan="4">
                                   
                            </td>
                        </tr>
                        <tr>
                            <td class="NormalTD" colspan="4">
                                <div id="Messaediv" runat="server">
                                    <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                </div>
                            </td>
                        </tr>

                        

                        

                        

                        

                </table>
                </ContentTemplate>
            </asp:UpdatePanel>

                 
            
                
               
               
            </td>
        </tr>
        
       <table class="DataEntryTable">
              
                   
                    
                    <tr>
                        <td class="NormalTD">
                         <%--   <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>
                        </td>
                        <td class="auto-style8">&nbsp;</td>
                        <td class="auto-style8">
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StyleColor.StyleColorid, StyleSize.StyleSizeID, StyleColor.AtcId, StyleColor.OurStyleID, StyleColor.OurStyle, StyleColor.GarmentColorCode, StyleColor.GarmentColor, StyleSize.SizeCode, StyleSize.SizeName, 000000 AS POQty FROM StyleColor INNER JOIN StyleSize ON StyleColor.AtcId = StyleSize.AtcId AND StyleColor.OurStyleID = StyleSize.OurStyleID WHERE (StyleColor.OurStyleID = @Param1)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="ourstylehiden" DefaultValue="0" Name="Param1" PropertyName="Value" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                            <asp:HiddenField ID="ourstylehiden" runat="server" />
                            <asp:SqlDataSource ID="atcdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT AtcId, AtcNum FROM AtcMaster"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                                SelectCommand="SELECT PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO AS ASQ, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO,
                                 PoPackMaster.PoPackId, POPackDetails.OurStyleID, AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable , POPackDetails.IsPackable
                                FROM PoPackMaster INNER JOIN POPackDetails ON PoPackMaster.PoPackId = POPackDetails.POPackId INNER JOIN AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID
                                 GROUP BY PoPackMaster.PoPacknum + ' / ' + PoPackMaster.BuyerPO, PoPackMaster.PoPacknum, PoPackMaster.BuyerPO, PoPackMaster.PoPackId, POPackDetails.OurStyleID, 
                                AtcDetails.OurStyle, AtcDetails.BuyerStyle, PoPackMaster.AtcId, PoPackMaster.IsCutable , POPackDetails.IsPackable HAVING (PoPackMaster.AtcId = @Param1) ORDER BY PoPackMaster.PoPackId">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                   
                    
                </table>
        <tr>
            <td>
                
            </td>
        </tr>
    </table>
   
  <asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="upd_bttn" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>
    

</asp:Content>


