<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SolidColorpackingInstruction.aspx.cs" Inherits="ArtWebApp.Merchandiser.ASQ.Packing.SolidColorpackingInstruction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    
 
    <link href="../../../css/style.css" rel="stylesheet" />
  

    <script src="../../../JQuery/GridJQuery.js"></script>
    <script type="text/javascript">


        function validateQty() {
            debugger;


             var gridView = document.getElementById("<%= tbl_podata.ClientID %>");


            var headerhtmlrow = headertable[0].rows[0];

            var balancetext = headertable[0].rows[3].getElementsByClassName("BalQty");

            for (var j = 0; j < balancetext.length; j++) {
                if (parseFloat(balancetext[0].value) == 0) {



                    return true;
                }
                else {
                    alert("Extra Qty Cannot be Allowed against Cut");
                    return false;

                }
            }



        }
        function totalcalculation()
        {
           
            try {
                solidcalculation()
            } catch (e) {
               
            }
           
        }




        function solidcalculation()
        {
             debugger;
           var gridView = document.getElementById("<%= tbl_podata.ClientID %>");

            var errordiv = document.getElementsByClassName("error-message")[0];
        
            

           for (var rownum = 1; rownum < gridView.rows.length; rownum++) {
               var grandtotal = 0;
               var ratiototal = 0;
                var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {

                    var txt_totalctn = gridView.rows[rownum].getElementsByClassName("txt_totalctnnew")[0];
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_pcperctnnew")[0];      
                    var lbl_totalpcexpected = gridView.rows[rownum].getElementsByClassName("lbl_totalpcexpectednew")[0];
                    var lbl_totalpcadded = gridView.rows[rownum].getElementsByClassName("lbl_totalpcaddednew")[0];

                    var expectedqty = parseFloat(txt_totalctn.value) * parseFloat(txt_pcperctn.value);
                    lbl_totalpcexpected.value = expectedqty.toString();

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
                                    errordiv.innerHTML = errordiv.innerHTML + 'New Pack Qty Exceeds the Pending Qty';
                                   textboxs[k].value = 0;
                                    totalqty = 0;
                                }
                                else
                                {
                                    errordiv.innerHTML = '';
                                }
                            //   else if()
                            //{
                              
                            //}
                                
                                if (parseFloat(ratiototal) > parseFloat(txt_pcperctn.value))
                                {
                                    alert("Ratio  Exceeds the PC/CTN")
                                    errordiv.innerHTML = errordiv.innerHTML + 'Ratio  Exceeds the PC/CTN';
                                   textboxs[k].value = 0;
                                    totalqty = 0;
                                }
                                else {
                                    errordiv.innerHTML = '';
                                }
                                var temgrandqty= parseFloat(grandtotal) + parseFloat(totalqty);
                                if (parseFloat(temgrandqty) > parseFloat(expectedqty)) {
                                   alert("Total Qty  Exceeds the Total ")

                                    errordiv.innerHTML = errordiv.innerHTML + 'Total Qty  Exceeds the Total';
                                   
                                   textboxs[k].value = 0;
                                  totalqty = 0;
                                }
                                else {
                                    errordiv.innerHTML = '';
                                }
                                calqty[k].value = totalqty;

                                grandtotal = parseFloat(grandtotal) + parseFloat(totalqty);
                                ratiototal = parseFloat(ratiototal) + parseFloat(textboxs[k].value);
                            }
                        }
                    }


                    lbl_totalpcadded.value = grandtotal.toString();
                }

                
            }

           

        }
          
           
        function copypcperctn()
        {

            debugger
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
            var errordiv = document.getElementsByClassName("txt_total")[0];
          
            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_pcperctnnew")[0];
                    txt_pcperctn.value = errordiv.value;


                    var pendingtable = gridView.rows[rownum].getElementsByClassName("PendingTable")[0];
                   
                    var totalqty = pendingtable.getElementsByClassName("ColorTotal")[0];
                  
                    var newctn = parseFloat(totalqty.value) / parseFloat(txt_pcperctn.value);
                    gridView.rows[rownum].getElementsByClassName("txt_totalctnnew")[0].value = newctn;


                
                }
            }
            totalcalculation();
            }
        
    function copyLength()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
        var errordiv = document.getElementsByClassName("txt_alllength")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_length")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

    }
        function copyWidth()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
            var errordiv = document.getElementsByClassName("txt_allwidth")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_width")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

            }
         function copyHeight()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
             var errordiv = document.getElementsByClassName("txt_allheight")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_height")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

            }
         

        function copyNNWeight()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
                    var errordiv = document.getElementsByClassName("txt_NNWeightAll")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_NNWeight")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

        }

        function copytxt_gross()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
                    var errordiv = document.getElementsByClassName("txt_grossAll")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_gross")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

            }

        
        function copyNetAll()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
            var errordiv = document.getElementsByClassName("txt_NetAll")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("txt_Netweight")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

        }


          function copyCTNUOM()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
              var errordiv = document.getElementsByClassName("drp_weightuomll")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("drp_weightuom")[0];
                    txt_pcperctn.value = errordiv.value;
                }
            }

        }

        
          function copyWeigthUOM()
        {
                var gridView = document.getElementById("<%= tbl_podata.ClientID %>");        
        
              var errordiv = document.getElementsByClassName("drp_NetUOMALL")[0];

            for (var rownum = 1; rownum < gridView.rows.length; rownum++)
            { var chkConfirm = gridView.rows[rownum].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked) {
                    var txt_pcperctn = gridView.rows[rownum].getElementsByClassName("drp_NetUOM")[0];
                    txt_pcperctn.value = errordiv.value;
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
    <style type="text/css">
        .auto-style1 {
            width: 4px;
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
                    <div class="error-message">



                       



                    </div>
                        <div >



                       



                    </div>
                         <div>
        

          <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;" >
                            


                            <tr>
                                <td colspan="4" class="auto-style11">
                                    
                                    
                                    <strong>Quick Fill </strong></td>
                            </tr>
                               


                         

                                   <tr>
                                
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_total" CssClass="txt_total"  placeholder=" PC/CTN" runat="server" Width="93px"></asp:TextBox>
                                    
                                </td>
                                       <td>

                                            <asp:Button ID="btn_pcperctn" OnClientClick="copypcperctn()" CssClass="btn_pcperctn" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px"    />
                                       </td>
                                <td>
                                  
                                    <table class="auto-style1" style="background-color: #FF9966">
                                        <tr>
                                            <td> 
                                                <asp:TextBox ID="txt_alllength"  CssClass="txt_alllength" runat="server" placeholder="Length" Width="50px"></asp:TextBox>
                                            </td>
                                             <td> X</td>
                                               <td >  
                                                   <asp:TextBox ID="txt_allwidth"  CssClass="txt_allwidth" runat="server" placeholder="Width" Width="50px"></asp:TextBox>
                                            </td>
                                            
                                            <td>X</td>
                                            <td> 
                                                <asp:TextBox ID="txt_allheight" CssClass="txt_allheight"  runat="server" placeholder="Height" Width="50px"></asp:TextBox>
                                            </td>
                                            <td>
                                                
                                                <asp:DropDownList ID="drp_weightuomll" CssClass="drp_weightuomll" runat="server">
                                                    <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                            </td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Button ID="btn_alllength" OnClientClick="copyLength()" CssClass="btn_alllength" runat="server" Text="S" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td >
                                                <asp:Button ID="btn_allWidth" OnClientClick="copyWidth()" CssClass="btn_allWidth"  runat="server" Text="S" />
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>
                                                <asp:Button ID="btn_allheigth" OnClientClick="copyHeight()"  CssClass="btn_allheigth" runat="server" Text="S" />
                                            </td>
                                            <td> <asp:Button ID="btn_weightUOMALL" OnClientClick="copyCTNUOM()" CssClass="btn_weightUOMALL" runat="server" Text="S" /></td>
                                            <td class="auto-style3">&nbsp;</td>
                                            <td>&nbsp;</td>
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
                                            
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td >
                                                <asp:Button ID="btn_NNWeight" OnClientClick="copyNNWeight()"  runat="server" CssClass="btn_NNWeight" Text="S" />
                                            </td>
                                        
                                            <td  >
                                                <asp:Button ID="btn_NetAll" OnClientClick="copyNetAll()" runat="server" CssClass="btn_NetAll" Text="S" />
                                            </td>
                                        
                                            <td >
                                                <asp:Button ID="btn_grossAll" OnClientClick="copytxt_gross()" runat="server" CssClass="btn_grossAll" Text="S" />
                                            </td>
                                           
                                            <td >
                                                <asp:Button ID="btn_NetUOMALL" runat="server" CssClass="btn_NetUOMALL" Text="S" />
                                            </td>
                                            <td ></td>
                                            
                                        </tr>
                                    </table>
                                    </td>
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
                                        <td class="auto-style10"  ><asp:GridView ID="tbl_podata" CssClass="dataenttry" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="PoPackId" OnRowCommand="tbl_podata_RowCommand" OnRowDataBound="tbl_podata_RowDataBound" OnSelectedIndexChanged="tbl_podata_SelectedIndexChanged" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
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
                                                              <tr>
                                                                <td>Color</td>
                                                                <td>
                                                                    <asp:Label ID="lbl_ColorName" runat="server" Text='<%# Bind("ColorName") %>'></asp:Label>
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
                                                <asp:TemplateField HeaderText="ASQ Details" ItemStyle-Width="200px" HeaderStyle-Width="200px" FooterStyle-Width="200px">
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

                                                         <asp:TemplateField HeaderText="CartonDetails" SortExpression="CartonDetails">
                                                    <ItemTemplate>
                                                        <table class="auto-style1" style="font-family: Calibri; line-height: normal; border: thin solid #000000; padding: inherit; margin: auto; background-color: #FFCC66;">
                                                            <tr>
                                                                <td>No of CTNS</td>
                                                                <td>Pc/Ctn</td>
                                                                <td>TotalPc</td>
                                                                <td>AddedPC</td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:TextBox ID="txt_totalctnnew"  CssClass="txt_totalctnnew" onchange="totalcalculation()" Text="0" runat="server" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_pcperctnnew" CssClass="txt_pcperctnnew" onchange="totalcalculation()" Text="0"  runat="server" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lbl_totalpcexpectednew" Enabled="false" CssClass="lbl_totalpcexpectednew"  runat="server" Width="50px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="lbl_totalpcaddednew" Enabled="false" CssClass="lbl_totalpcaddednew" Text="0"   runat="server" Width="50px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
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
                                                  <asp:TemplateField>
                                
                                <ItemTemplate>
                                    <table class="auto-style1" border="1">
                                        <tr>
                                            <td style="width:50px">Length</td>
                                            <td style="width:50px">Width</td>
                                           <td style="width:50px"">Height</td>
                                            <td style="width:50px">UOM</td>
                                          
                                        </tr>
                                        <tr>
                                            <td> <asp:TextBox ID="txt_length"  CssClass="txt_length" placeholder="Length"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_width"  CssClass="txt_width"   placeholder="Width" Text="0"  onkeypress="return isNumberKey(event,this)"  Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_height"  CssClass="txt_height" placeholder="Height"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small" Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:DropDownList ID="drp_weightuom" CssClass="drp_weightuomll" runat="server">
                                                 <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                </asp:DropDownList></td>
                                    
                                        </tr>
                                        <tr>
                                            <td style="width:50px">Net Wgt</td>
                                            <td style="width:50px">Weight</td>
                                           <td style="width:50px">Gross</td>
                                           <td style="width:50px">UOM</td>                                           
                                        </tr>
                                        <tr>
                                            <td> <asp:TextBox ID="txt_NNWeight"  CssClass="txt_NNWeight" placeholder="Length"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_Netweight"  CssClass="txt_Netweight"   placeholder="Width" Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td> <asp:TextBox ID="txt_gross"  CssClass="txt_gross" placeholder="Height"  Text="0"  onkeypress="return isNumberKey(event,this)" Font-Size="small"  Height="16px" Width="50px"  runat="server" ></asp:TextBox></td>
                                           <td><asp:DropDownList ID="drp_NetUOM" CssClass="drp_NetUOM" runat="server">
                                                 <asp:ListItem Value="KG">KG</asp:ListItem>
                                                     <asp:ListItem Value="LBS">LBS</asp:ListItem>
                                                </asp:DropDownList></td></td>
                                    
                                        </tr>
                                  

                                    </table>
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
        
        <tr>
            
            <td class="DataEntryTable">

                &nbsp;</td>
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
                            &nbsp;</td>
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


