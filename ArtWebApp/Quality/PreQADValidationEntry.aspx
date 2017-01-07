<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="PreQADValidationEntry.aspx.cs" Inherits="ArtWebApp.Quality.PreQADValidationEntry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript" src="../JQuery/GridJQuery.js"></script>
 
    <link href="../css/style.css" rel="stylesheet" />

     <script type="text/javascript">

          function SetShrinkage() {
			//if (Searching == 'eID') {
		   
		    var num = document.getElementById('<%= txt_Shrinkage.ClientID %>').value;
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

              for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                  var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                  for (var i = 0; i < inputList.length; i++) {

                      if (inputList[i].type == "checkbox") {

                          if (inputList[i].checked) {

                              var txtbx = grdvw.rows[rowId].cells[7].children[0];
                              txtbx.value = num;

                          }





                      }
                  }


              }
			
         }




         function SetYardage() {
			//if (Searching == 'eID') {
		    
		    var num = document.getElementById('<%= txt_yardage.ClientID %>').value;
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++)
                 
                 {

                     if (inputList[i].type == "checkbox" )
                     {

                         if (inputList[i].checked)
                         {

                             var txtbx = grdvw.rows[rowId].cells[9].children[0];
                             txtbx.value = num;

                         }
                
                    

                 
			   
                     }
                 }

                 
		 }
			
         }
         function SetShade() {
			//if (Searching == 'eID') {
		    
		    var num = document.getElementById('<%= txt_shade.ClientID %>').value;
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++) {

                     if (inputList[i].type == "checkbox") {

                         if (inputList[i].checked) {

                             var txtbx = grdvw.rows[rowId].cells[11].children[0];
                             txtbx.value = num;

                         }





                     }
                 }


             }
			
         }
        
         function SetWidth() {
			//if (Searching == 'eID') {
		   
		    var num = document.getElementById('<%= txt_width.ClientID %>').value;
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++) {

                     if (inputList[i].type == "checkbox") {

                         if (inputList[i].checked) {

                             var txtbx = grdvw.rows[rowId].cells[13].children[0];
                             txtbx.value = num;

                         }





                     }
                 }


             }
			
         }
         function SetGSM() {
             //if (Searching == 'eID') {

             var num = document.getElementById('<%= txt_width.ClientID %>').value;
             var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++) {

                     if (inputList[i].type == "checkbox") {

                         if (inputList[i].checked) {

                             var txtbx = grdvw.rows[rowId].cells[15].children[0];
                             txtbx.value = num;

                         }





                     }
                 }


             }
             // }

         }








              function CopyShrinkage() {
			
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

              for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                  var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                  for (var i = 0; i < inputList.length; i++) {

                      if (inputList[i].type == "checkbox") {

                          if (inputList[i].checked) {
                              var lblbx = grdvw.rows[rowId].cells[6].children[0];
                              var txtbx = grdvw.rows[rowId].cells[7].children[0];
                              txtbx.value = lblbx.innerHTML;

                          }





                      }
                  }


              }
			
         }




         function CopyYardage() {
			
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++)
                 
                 {

                     if (inputList[i].type == "checkbox" )
                     {

                         if (inputList[i].checked)
                         {
                             var lblbx = grdvw.rows[rowId].cells[8].children[0];
                             var txtbx = grdvw.rows[rowId].cells[9].children[0];
                             txtbx.value = lblbx.innerHTML;

                         }
                
                    

                 
			   
                     }
                 }

                 
		 }
			
         }
         function CopyShade() {
		
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++) {

                     if (inputList[i].type == "checkbox") {

                         if (inputList[i].checked) {
                             var lblbx = grdvw.rows[rowId].cells[10].children[0];
                             var txtbx = grdvw.rows[rowId].cells[11].children[0];
                             txtbx.value = lblbx.innerHTML;

                         }





                     }
                 }


             }
			
         }
        
         function CopyWidth() {
			//if (Searching == 'eID') {
		   
		  
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

             for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                 var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                 for (var i = 0; i < inputList.length; i++) {

                     if (inputList[i].type == "checkbox") {

                         if (inputList[i].checked) {
                             var lblbx = grdvw.rows[rowId].cells[12].children[0];
                             var txtbx = grdvw.rows[rowId].cells[13].children[0];
                             txtbx.value = lblbx.innerHTML;

                         }





                     }
                 }


             }
			
         }
          function CopyGSM() {
				   
		  
			var grdvw = document.getElementById('<%= tbl_InverntoryDetails.ClientID %>');

              for (var rowId = 1; rowId < grdvw.rows.length; rowId++) {
                  var inputList = grdvw.rows[rowId].getElementsByTagName("input");

                  for (var i = 0; i < inputList.length; i++) {

                      if (inputList[i].type == "checkbox") {

                          if (inputList[i].checked) {
                              var lblbx = grdvw.rows[rowId].cells[14].children[0];
                              var txtbx = grdvw.rows[rowId].cells[15].children[0];
                              txtbx.value = lblbx.innerHTML;

                          }





                      }
                  }


              }
	
          }


          function CheckBoxSelectionValidation() {
             debugger;
             var gridView = document.getElementById("<%= tbl_InverntoryDetails.ClientID %>");

             for (var i = 1; i < gridView.rows.length; i++) {
                 var count = 0;
                 var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                 
                 var txtshrnk = gridView.rows[i].cells[7].getElementsByTagName('input')[0];
                 var txtyard = gridView.rows[i].cells[9].getElementsByTagName('input')[0];
                 var txtshade = gridView.rows[i].cells[11].getElementsByTagName('input')[0];
                 var txtwidth = gridView.rows[i].cells[13].getElementsByTagName('input')[0];
                 if (chkConfirm.checked) {
                     if (txtwidth.value == "" || txtshade.value == "" || txtshrnk.value == "" || txtyard.value == "") {
                         gridView.rows[i].style.backgroundColor = "red";
                         txtwidth.focus();
                         
                       return false;
                     }
                 } 
             }

           
         }









	</script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    

    
        <table class="FullTable">

            <tr>
                 <td class="NormalTD"  >
<table class="DataEntryTable">
                <tr>
                    <td class="RedHeadding" colspan="5">
                        pre qad roll VALIDATION</td>
                </tr>
                <tr>
                    
                    <td class="NormalTD Col1TD"  >
                          
                        Atc</td>
                    <td class="NormalTD Col2TD"  >
                         

                          <asp:UpdatePanel ID="upd_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_atc" runat="server" Height="25px" Width="170px" DataSourceID="atcdata" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>

                    </td>
                    <td class="NormalTD Col3TD"  >
                          <asp:UpdatePanel ID="upd_btn_atc" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_atc" runat="server" Text="S" Width="33px"   OnClick="btn_atc_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                </tr>
    <tr>
                    <td class="NormalTD"  >
                        supplier invoice /ASN #:</td>
                    <td class="NormalTD" >
                             
                              <asp:UpdatePanel ID="UPD_ASN" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_asn" runat="server" Height="25px" Width="170px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel>
                       </td>
                    <td class="NormalTD"  >
                     <asp:UpdatePanel ID="upd_btn" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_Asn" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_Asn_Click" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                     
                    <td class="NormalTD"  >
                        </td>
                </tr>
                
                <tr>
                    <td class="NormalTD"  >
                        Fabric Details :
                    </td>
                    <td class="NormalTD" >
                             
                       <asp:UpdatePanel ID="upd_color" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                <ucc:DropDownListChosen ID="drp_color" runat="server" Height="25px" Width="200px" DisableSearchThreshold="10">
                        </ucc:DropDownListChosen>
                                                   </ContentTemplate>
                                            </asp:UpdatePanel></td>
                    <td class="NormalTD"  >
                         <asp:UpdatePanel ID="upd_btn_po" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                       <asp:Button ID="btn_fabric" runat="server" Text="S" Width="33px"  CssClass="auto-style10" OnClick="btn_fabric_Click1" /></td>
                     </ContentTemplate>
                                            </asp:UpdatePanel>  </td>
                    <td class="NormalTD"  >
                        </td>
                    <td class="NormalTD"  >
                        </td>
                </tr>
                <tr>
                    <td class="NormalTD" >

                          &nbsp;</td>
                    <td class="NormalTD" >
                        <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                     <asp:CheckBox ID="chk_woven"   runat="server" Text="Knit" AutoPostBack="True" OnCheckedChanged="chk_woven_CheckedChanged" />
                     </ContentTemplate>
                                            </asp:UpdatePanel>
                    </td>
                    <td class="NormalTD" >
                         &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="5" >

                          <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;" >
                            


                            <tr>
                                <td colspan="12" class="auto-style11">
                                    
                                    
                                    <strong>Quick Fill </strong></td>
                            </tr>



                            <tr>
                                <td colspan="12">
                                    


                                    <div>

                                  
                                    </div>
                                </td>
                            </tr>



                          <%--  <tr>
                                
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_yardage" placeholder="Enter Yard" runat="server" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_yard" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px"    OnClick="btn_yard_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_shade" placeholder="Enter Shade" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shade" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shade_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_Shrinkage"  placeholder="Enter Shrinkage" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shrinkage" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClick="btn_shrinkage_Click" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_width"  placeholder="Enter Width" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_width" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" Height="20px" OnClick="btn_width_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_gsm" placeholder="Enter GSM" runat="server" Width="90px"></asp:TextBox></td>
                                  <td class="ButtonTD">
                                      <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                      <asp:Button ID="btn_gsm" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_gsm_Click"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd"></td>
                                 <td class="Textboxtd"></td>
                            </tr>--%>

                                   <tr>
                                
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_yardage" placeholder="Enter Yard" runat="server" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_yard" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px"    OnClientClick="SetYardage()"  /></ContentTemplate>
                                            </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_shade" placeholder="Enter Shade" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shade" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply"  OnClientClick=" SetShade()" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_Shrinkage"  placeholder="Enter Shrinkage" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                    <asp:Button ID="btn_shrinkage" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" OnClientClick="SetShrinkage()" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_width"  placeholder="Enter Width" runat="server" Width="90px"></asp:TextBox></td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Button ID="btn_width" runat="server" Font-Bold="True" Font-Size="X-Small" Width="54px" Text="Apply" Height="20px" OnClientClick="SetWidth()"  /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_gsm" placeholder="Enter GSM" runat="server" Width="90px"></asp:TextBox></td>
                                  <td class="ButtonTD">
                                      <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                      <asp:Button ID="btn_gsm" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" OnClick="btn_gsm_Click" OnClientClick="SetGSM()" /></ContentTemplate>
                                            </asp:UpdatePanel></td>
                                <td class="Textboxtd"></td>
                                 <td class="Textboxtd"></td>
                            </tr>

                            <%--<tr>
                                <td colspan="2"  class="NormalTD">
                                    
                                    
                                    &nbsp;</td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_yardage" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Yard" Width="150px" runat="server" Height="22px" OnClick="btn_yardage_Click1"   />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_shadage" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Shade" Width="150px" runat="server" OnClick="btn_shadage_Click"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSupshrink" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Shrinkage" Width="150px" runat="server" OnClick="btnSupshrink_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_sswidth"  Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Width" Width="150px" runat="server" OnClick="btn_sswidth_Click"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="Button5" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier GSM" Width="150px" runat="server" OnClick="Button5_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>--%>

                              <tr>
                                <td colspan="2"  class="NormalTD">
                                    
                                    
                                    &nbsp;</td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_yardage" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Yard" Width="150px" runat="server" Height="22px" OnClientClick="CopyYardage()" OnClick="btn_yardage_Click2"   />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_shadage" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Shade" Width="150px" runat="server" OnClientClick="CopyShade()"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btnSupshrink" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Shrinkage" Width="150px" runat="server" OnClientClick="CopyShrinkage()" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_sswidth"  Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier Width" Width="150px" runat="server" OnClientClick="CopyWidth()"  />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td colspan="2"  class="NormalTD">
                                    <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                        <ContentTemplate>
                                            <asp:Button ID="Button5" Font-Bold="True" Font-Size="X-Small" Text="Copy Supplier GSM" Width="150px" runat="server" OnClientClick=" CopyGSM()" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table></td>
                </tr>
                <tr>
                    <td class="smallgridtable" colspan="5">
                        <asp:UpdatePanel ID="upd_grid"   UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="tbl_InverntoryDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="Roll_PK">
                                    <Columns>                                     
                                         <asp:TemplateField>
                                       
                                     
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Roll_PK" InsertVisible="False" SortExpression="Roll_PK"  ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_rollpk" runat="server" Text='<%# Bind("Roll_PK") %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden" />
                                            <ItemStyle CssClass="hidden" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Lotnum" HeaderText="Lot#" SortExpression="Lotnum" />
                                        <asp:BoundField DataField="RollNum" HeaderText="RollNum" SortExpression="RollNum" />
                                        <asp:BoundField DataField="UOM" HeaderText="UOM" SortExpression="UOM" />
                                          <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                                        <asp:TemplateField HeaderText="SShrink" SortExpression="SShrink">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sshrinkage" runat="server" Text='<%# Bind("SShrink") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AShrink" SortExpression="AShrink">
                                            
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ashrink"  Width="70px" onkeyup="enter(this)"  runat ="server" Text='<%# Bind("AShrink") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SYard" SortExpression="SYard">
                                           
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_syard" runat="server" Text='<%# Bind("SYard") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AYard" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_ayard" Width="70px" onkeyup="enter(this)"  runat="server" Text='<%# Bind("AYard") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SShade" SortExpression="SShade">
                                          
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sshade" runat="server" Text='<%# Bind("SShade") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AShade" SortExpression="AShade">
                                            
                                            <ItemTemplate>
                                                 <asp:TextBox ID="txt_ashade" Width="70px" onkeyup="enter(this)"  runat="server" Text='<%# Bind("AShade") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="SWidth" SortExpression="SWidth">
                                           
                                              <ItemTemplate>
                                                  <asp:Label ID="lbl_swidth" runat="server" Text='<%# Bind("SWidth") %>'></asp:Label>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AWidth" SortExpression="AWidth">
                                            
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_awidth" Width="70px" onkeyup="enter(this)"  runat="server" Text='<%# Bind("AWidth") %>'></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="SGsm" SortExpression="SGsm">
                                             
                                              <ItemTemplate>
                                                  <asp:Label ID="lbl_sgsm" runat="server" Text='<%# Bind("SGsm") %>'></asp:Label>
                                              </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="AGSM" SortExpression="AYard">
                                           
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_agsm" Width="70px" onkeyup="enter(this)"  runat="server" Text='<%# Bind("AGSM") %>'></asp:TextBox>
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
                                </asp:GridView>
                                  <asp:SqlDataSource ID="atcdata" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT AtcNum, AtcId FROM AtcMaster WHERE (IsClosed = N'N') ORDER BY AtcNum, AtcId">
                </asp:SqlDataSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr class="ButtonTR">
                    <td class="NormalTD"  >
                        <asp:Button ID="btn_submitData" runat="server" Text="Save Roll Data" OnClientClick="return CheckBoxSelectionValidation()"  OnClick="btn_submitData_Click" />
                    </td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD" >
                        &nbsp;</td>
                    <td class="NormalTD"  >
                        &nbsp;</td>
                    <td class="NormalTD"  >
                        &nbsp;</td>
                </tr>
            </table>

           </tr>
            </table>
      
            
        
   
    <div class="footer">
        
               
                               
    </div>

    
</asp:Content>
