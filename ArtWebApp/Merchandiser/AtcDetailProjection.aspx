<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcDetailProjection.aspx.cs" Inherits="ArtWebApp.Merchandiser.AtcDetailProjection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    <link href="../css/style.css" rel="stylesheet" />
 
    <script src="../JQuery/GridJQuery.js"></script>
 
<style type="text/css">

</style>
 
  <script type="text/javascript">


      function calculatesum()
      {
          var sum = 0;

          var textboxs = document.getElementsByClassName("txt_qty");

          for (var i = 0; i < textboxs.length; i++) {
              var tempsum=0;
              
              try{
                 
                  tempsum=parseFloat(textboxs[i].value);

              } 
              catch(Exception)
              {
                  tempsum = 0;
                  textboxs[i].value = 0;
              }

              sum = sum + parseFloat(tempsum);
          }
          var ourstylelbl = document.getElementsByClassName("lbl_ourstyleproj");
          ourstylelbl[0].innerHTML = sum.toString();
      }

      </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="FullTable">
        <tr  class="RedHeadding">
            <td style="color: #FFFFFF; text-align: center; background-color: #990000"><strong>OurStyle Projection&nbsp; </strong></td>
        </tr>
        <tr>
            <td class="DataEntryTable">



                 <table class="DataEntryTable">
                        <tr>
                            <td class="NormalTD">

                                   Atc# 
                
                            </td>
                            <td class="NormalTD">
                               
                   
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                               
                            </td>
                            <td class="NormalTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" style="width: 23px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td colspan="4">

                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                        

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                
                               
                            </td>
                            </tr>

                </table>

                
               
               
            </td>
        </tr>
        <tr>
            <td class="DataEntryTable">
                
                <table class="DataEntryTable">
                    <tr>
                        <td class="NormalTD">
                            Atc Proj Qty</td>
                        <td class="NormalTD">
                              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                            <asp:Label ID="lbl_atcprojQty" runat="server" Text="Label"></asp:Label>
                                             </ContentTemplate>
                                </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            OurStyle proj Qty</td>
                        <td class="NormalTD"><asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                            <asp:Label ID="lbl_ourstyleproj" CssClass="lbl_ourstyleproj" runat="server" Text="Label"></asp:Label>
                                             </ContentTemplate>
                                </asp:UpdatePanel></td>
                        <td class="NormalTD">
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]">
                </asp:SqlDataSource>
                    
               
                               
                            </td>
                        <td class="NormalTD">

                            <asp:SqlDataSource ID="OurStyleData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        OurStyleID, OurStyle, BuyerStyle, FOB, ISNULL((
SELECT      sum(  Quantity)
FROM            AtcDetailApproval
WHERE        (IsFirst = N'Y') AND (OurStyleID = AtcDetails.OurStyleID)),0) AS IntialQty, Quantity AS RevisedQty, AtcId
FROM            AtcDetails
WHERE        (AtcId = @Param1)">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="cmb_atc" Name="Param1" PropertyName="SelectedValue" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </td>
                    </tr>
                </table>
                
            </td>
        </tr>
       
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td class="auto-style8">
                             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
<asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="OurStyleID" Font-Size="Large" style="font-size: x-small; font-family: Calibri" Width="100%" DataSourceID="OurStyleData">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_select" runat="server" onclick="Check_Click(this)" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="OurStyleID" InsertVisible="False" SortExpression="OurStyleID">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_OurStyleID" runat="server" Text='<%# Bind("OurStyleID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="AtcId" HeaderText="AtcId" SortExpression="AtcId" />
                                    <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                                    <asp:BoundField DataField="BuyerStyle" HeaderText="BuyerStyle" SortExpression="BuyerStyle" />
                                    <asp:BoundField DataField="FOB" HeaderText="FOB" SortExpression="FOB" />
                                

                                     <asp:TemplateField HeaderText="IntialQty" SortExpression="IntialQty">
                                      
                                        <ItemTemplate>
                                             <asp:Label ID="txt_IntialQty" runat="server" Text='<%# Bind("IntialQty") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="RevisedQty" SortExpression="RevisedQty">
                                      
                                        <ItemTemplate>
                                             <asp:TextBox ID="txt_qty"  CssClass="txt_qty" onchange="calculatesum()" runat="server" Text='<%# Bind("RevisedQty") %>'></asp:TextBox>
                                        </ItemTemplate>
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
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Submit Projection" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style8">
                            <div id="Messaediv" runat="server">
                                <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
