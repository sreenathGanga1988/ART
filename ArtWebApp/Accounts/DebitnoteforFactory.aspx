<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DebitnoteforFactory.aspx.cs" Inherits="ArtWebApp.Accounts.DebitnoteforFactory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />

 
          <script type="text/javascript" >

      


        function Onselection(objref)
        {
            Check_Click(objref)
            calculatesumofyardage();
        }

        function OnSelectAllClick(objref) {
            checkAll(objref)
            calculatesumofyardage();
        }


        //function to calculate the sum of user enter
        function calculatesumofyardage()
        {
            var gridView = document.getElementById("<%= tbl_podata.ClientID %>");
            var sum = 0
            for (var i = 1; i < gridView.rows.length - 1; i++)
            {
                var chkConfirm = gridView.rows[i].cells[0].getElementsByTagName('input')[0];
                if (chkConfirm.checked)
                {
                    var txt_syard = gridView.rows[i].getElementsByClassName("txt_syard")[0];

                    sum = sum + parseFloat(txt_syard.innerHTML);
                }

            } 
            var totalyardfooter = document.getElementsByClassName("totalyardfooter")[0];
            totalyardfooter.value = sum;
        }





    </script>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="RedHeaddingdIV">

        &nbsp;DEBIT NOTES TO FACTORY AGAINST SALES DO FROM HO</div>
    <div>

        <table class="DataEntryTable">
            <tr>
                <td class="NormalTD">Debit From</td>
                <td class="NormalTD">
                    <ucc:DropDownListChosen ID="drp_ToWarehouse" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" TextField="name" ValueField="pk" Width="200px">
                    </ucc:DropDownListChosen>
                </td>
                <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                 <td class="NormalTD">&nbsp;</td>
            </tr>
            <tr>
                <td class="NormalTD">Year</td>
                 <td class="NormalTD">
                     <ucc:DropDownListChosen ID="cmb_year" runat="server" DataSourceID="Year" DataTextField="YearName" DataValueField="YearName" DisableSearchThreshold="10" Width="200px">
                         <asp:ListItem>2017</asp:ListItem>
                         <asp:ListItem>2018</asp:ListItem>
                         <asp:ListItem>2019</asp:ListItem>
                         <asp:ListItem>2020</asp:ListItem>
                     </ucc:DropDownListChosen>
                     <asp:SqlDataSource ID="Year" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [YearName] FROM [YearMonthMaster]"></asp:SqlDataSource>
                </td>
               <td class="NormalTD">Month</td>
              <td class="NormalTD">
                  <ucc:DropDownListChosen ID="cmb_Month" runat="server" DataSourceID="Month" DataTextField="MonthName" DataValueField="MonthNum" DisableSearchThreshold="10" Width="200px">
                  </ucc:DropDownListChosen>
                  <asp:SqlDataSource ID="Month" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [MonthName], [MonthNum] FROM [YearMonthMaster] WHERE ([YearName] = @YearName)">
                      <SelectParameters>
                          <asp:ControlParameter ControlID="cmb_year" Name="YearName" PropertyName="SelectedValue" Type="String" />
                      </SelectParameters>
                  </asp:SqlDataSource>
                </td>
              <td class="NormalTD">
                  <asp:Button ID="S" runat="server" OnClick="Button3_Click1" Text="S" />
                </td>
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
              <td class="NormalTD">&nbsp;</td>
            </tr>
        </table>

    </div>
    <div>

        <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" ShowFooter="true" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SalesDO_PK" Font-Size="Smaller" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                
                                         <asp:TemplateField  ControlStyle-Width="10px" HeaderStyle-Width="10px" FooterStyle-Width="10px">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="OnSelectAllClick(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Onselection(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>    
                <asp:TemplateField HeaderText="SDO_PK" InsertVisible="False" SortExpression="SalesDO_PK">
                  
                    <ItemTemplate>
                        <asp:Label ID="lbl_sdopk" runat="server" Text='<%# Bind("SalesDO_PK") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SalesDONum" HeaderText="SalesDONum" SortExpression="SalesDONum" />
                <asp:BoundField DataField="ContainerNumber" HeaderText="ContainerNumber" SortExpression="ContainerNumber" />
                <asp:BoundField DataField="DoType" HeaderText="DoType" SortExpression="DoType" />
                <asp:BoundField DataField="FromLocation" HeaderText="FromLocation" SortExpression="FromLocation" />
                <asp:BoundField DataField="ToLocation" HeaderText="ToLocation" SortExpression="ToLocation" />
                <asp:TemplateField HeaderText="DOvalue" SortExpression="DOvalue">
                   <FooterTemplate>
                                                  <asp:TextBox ID="txt_totalyard" CssClass="totalyardfooter" runat="server"></asp:TextBox>

                                              </FooterTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" CssClass="txt_syard" runat="server" Text='<%# Bind("DOvalue") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="SalesDate" HeaderText="SalesDate" SortExpression="SalesDate" />
                <asp:BoundField DataField="AddedBy" HeaderText="AddedBy" SortExpression="AddedBy" />
                <asp:BoundField DataField="AddedDate" HeaderText="AddedDate" SortExpression="AddedDate" />
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
        <asp:SqlDataSource ID="SalesDOData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="GetSalesDOforDebitNote_SP" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="drp_ToWarehouse" Name="locationpk" PropertyName="SelectedValue" Type="Int32" />
                <asp:ControlParameter ControlID="lbl_fromdate" Name="fromdate" PropertyName="Text" Type="DateTime" />
                <asp:ControlParameter ControlID="lbl_todate" Name="todate" PropertyName="Text" Type="DateTime" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
       <div> <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Create Debit Note" /></div>
        <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
</asp:Content>
