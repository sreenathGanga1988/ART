<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JobContractOthers.aspx.cs" Inherits="ArtWebApp.Production.JobContractOthers" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
  <style>

    </style>
    <script src="../JQuery/GridJQuery.js">
    </script>


    <script>

        function Onselection(objref) {
            Check_Click(objref)
            
        }

        function OnSelectAllClick(objref) {
            checkAll(objref)
            
        }




            function Copywashing()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
                var txt_washing1 = document.getElementsByClassName("txt_washing1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_washing = gridView.rows[i].getElementsByClassName("txt_washing")[0];

                    txt_washing.value = txt_washing1.value;
                }
            }
            }

          function DryPorocess()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
              var txt_dryprocess1 = document.getElementsByClassName("txt_dryprocess1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_dryprocess = gridView.rows[i].getElementsByClassName("txt_dryprocess")[0];

                    txt_dryprocess.value = txt_dryprocess1.value;
                }
            }
          }

          function EMB()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
              var txt_emb1 = document.getElementsByClassName("txt_emb1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_emb = gridView.rows[i].getElementsByClassName("txt_emb")[0];

                    txt_emb.value = txt_emb1.value;
                }
            }
          }


         function Factorylog()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
             var txt_factorylog1 = document.getElementsByClassName("txt_factorylog1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_factorylog = gridView.rows[i].getElementsByClassName("txt_factorylogistic")[0];

                    txt_factorylog.value = txt_factorylog1.value;
                }
            }
         }


         function CompanyLog()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
             var txt_compnylog1 = document.getElementsByClassName("txt_compnylog1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_compnylog = gridView.rows[i].getElementsByClassName("txt_cmplogistic")[0];

                    txt_compnylog.value = txt_compnylog1.value;
                }
            }
          }


           function FabCom()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
               var txt_fabcomm1 = document.getElementsByClassName("txt_fabcomm1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_fabcomm = gridView.rows[i].getElementsByClassName("txt_fabcomission")[0];

                    txt_fabcomm.value = txt_fabcomm1.value;
                }
            }
           }


           function Garmentcom()
        {
            var gridView = document.getElementById("<%= tbl_podetails.ClientID %>");
               var txt_garmentcom1 = document.getElementsByClassName("txt_garmentcom1")[0];

            for (var i = 1; i < gridView.rows.length; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_garmentcom = gridView.rows[i].getElementsByClassName("txt_garcomission")[0];

                    txt_garmentcom.value = txt_garmentcom1.value;
                }
            }
           }


    </script>

        








</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>


     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>

             <table class="DataEntryTable">
        <tr class="RedHeadding">
            <td  colspan="4">JOB Contract Others</td>
        </tr>
                 <tr>
                     <td class="NormalTD">Factory</td>
                     <td class="NormalTD">
                         <ucc:DropDownListChosen ID="drp_factory" runat="server" DataSourceID="FactorydataSource" DataTextField="LocationName" DataValueField="Location_PK" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
                     </td>
                     <td class="NormalTD"></td>
                     <td class="NormalTD"></td>
                 </tr>
        <tr>
            <td class="NormalTD">atc</td>
             <td class="NormalTD" >
                  <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
            </td>
             <td class="NormalTD"></td>
        </tr>
        <tr>
            <td class="NormalTD">Buyer PO/.ASQ</td>
              <td class="NormalTD">
                  <ig:WebDropDown ID="drp_popack" runat="server" Width="200px" EnableMultipleSelection="True" TextField="POnum" ValueField="PoPackId">
                      <DropDownItemBinding TextField="POnum" ValueField="PoPackId" />
                  </ig:WebDropDown>
            </td>
              <td class="NormalTD">
                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
            </td>
              <td class="NormalTD"></td>
        </tr>
        <tr>
            <td>
                oPTIONAL cOMPONENTS</td>
            <td>
                 <asp:UpdatePanel ID="udp_optionalcombo"  UpdateMode="Conditional" runat="server">
                                        <ContentTemplate>
                                            <ig:WebDropDown ID="drp_optionalcomb" runat="server" Width="200px" EnableClosingDropDownOnSelect="False" EnableMultipleSelection="True" TextField="ComponentName" ValueField="CostComp_PK">
                                                <DropDownItemBinding TextField="ComponentName" ValueField="CostComp_PK" />
                                            </ig:WebDropDown>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
            </td>
            <td>

                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="S" />
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>


        </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

            <table style="border: thin double #C0C0C0; line-height: normal; vertical-align: middle;  text-align: center; white-space: normal; word-spacing: normal; letter-spacing: normal; background-color: #99CCFF; position: relative; width: 100%;">
                            <tr>
                                <td class="auto-style11" colspan="12"><strong>Quick Fill </strong></td>
                            </tr>
                            <tr>
                                <td colspan="12">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_washing" CssClass="txt_washing1" runat="server" placeholder="Enter Washing" Width="99px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_washing" runat="server" OnClientClick="Copywashing()" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_dryprocess" CssClass="txt_dryprocess1" runat="server" placeholder="Enter DryProcess" Width="93px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_dryprocess" OnClientClick="DryPorocess()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_emb" CssClass="txt_emb1" runat="server" placeholder="Enter EMB" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_emb" OnClientClick="EMB()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_factorylog" CssClass="txt_factorylog1" runat="server" placeholder="Enter factorylog" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_factorylog" OnClientClick="Factorylog()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_compnylog" CssClass="txt_compnylog1" runat="server" placeholder="Enter Compnylog" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_companylog" OnClientClick="CompanyLog()" runat="server" Font-Bold="True" Font-Size="X-Small" Height="20px" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="Textboxtd">
                                    <asp:TextBox ID="txt_fabcomm" CssClass="txt_fabcomm1" runat="server" placeholder="Enter fabcomm" Width="90px"></asp:TextBox>
                                </td>
                                <td class="ButtonTD">
                                    <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_fabcomm" OnClientClick="FabCom()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td><asp:TextBox ID="txt_garmentcom" CssClass="txt_garmentcom1" runat="server" placeholder="Enter Garmentcom" Width="90px"></asp:TextBox></td>
                                <td> <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_garmentcom" OnClientClick="Garmentcom()" runat="server" Font-Bold="True" Font-Size="X-Small" Text="Apply" Width="54px" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel></td>
                            </tr>
                        </table>



        <asp:GridView ID="tbl_podetails" CssClass="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large">
                            <Columns>      
              <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="PoPackId">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_popackid" runat="server" Text='<%# Bind("PoPackId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                <asp:BoundField DataField="POnum" HeaderText="POnum" />
                <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" />
                                <asp:TemplateField HeaderText="OurStyleID">
                                    
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_OurStyleID"   runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" />
                <asp:BoundField DataField="POQTY" HeaderText="POQTY" />
                                <asp:TemplateField HeaderText="Apprcm">
                                   
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_apprcm" runat="server" Text='<%# Bind("Apprcm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Washing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_washing"  CssClass="txt_washing"     Enabled ="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="DryProcess">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_dryprocess"  CssClass="txt_dryprocess"  Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                                  <asp:TemplateField HeaderText="Emb/Printing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_emb"    Width="70px" CssClass="txt_emb" Enabled="false"  Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Factory Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_factorylogistic" CssClass="txt_factorylogistic" Enabled="false" Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Company Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_cmplogistic" CssClass="txt_cmplogistic" Enabled ="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Fab Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_fabcomission" CssClass="txt_fabcomission" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Garment Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_garcomission" CssClass="txt_garcomission" Enabled="false"  Width="70px" Text="0" runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>

            </Columns>
            <FooterStyle BackColor="#FFFFCC" ForeColor="#000066" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="#FFFFCC" />
                            <PagerStyle BackColor="#FFFFCC" ForeColor="#330099" HorizontalAlign="Center" />
                            <RowStyle BackColor="White" ForeColor="#330099" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
                            <SortedAscendingCellStyle BackColor="#FEFCEB" />
                            <SortedAscendingHeaderStyle BackColor="#AF0101" />
                            <SortedDescendingCellStyle BackColor="#F6F0C0" />
                            <SortedDescendingHeaderStyle BackColor="#7E0000" />
                        </asp:GridView>
              </ContentTemplate>
     </asp:UpdatePanel>

    </div>


    <div class="DataEntryTable">

          <asp:UpdatePanel ID="upd_btn" UpdateMode="Conditional" runat="server">
         <ContentTemplate>
        <asp:Button ID="btn_JCSubmit" runat="server" Text="Submit" OnClick="btn_JCSubmit_Click" style="height: 26px" />
                </ContentTemplate>
     </asp:UpdatePanel>

    </div>

       <div id="Messaediv" runat="server">
                  <asp:UpdatePanel ID="upd_msg" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                            <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                                         </ContentTemplate>
                                    </asp:UpdatePanel>


                         


                     
               </div>
    <asp:SqlDataSource ID="FactorydataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Location_PK], [LocationName] FROM [LocationMaster] WHERE ([LocType] = @LocType)">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="F" Name="LocType" Type="String" />
                    </SelectParameters>
                </asp:SqlDataSource>


                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcNum], [AtcId] FROM [AtcMaster]"></asp:SqlDataSource>
            
    <br />
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
            
</asp:Content>
