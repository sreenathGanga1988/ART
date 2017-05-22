<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="AtcEdit.aspx.cs" Inherits="ArtWebApp.Merchandiser.AtcEdit" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.GridControls" tagprefix="ig" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.EditorControls" tagprefix="ig" %>

<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.NavigationControls" tagprefix="ig1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style type="text/css">
      
    </style>
      <link href="../../css/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>

              <div>

                  <table class="FullTable">
                      <tr>
                          <td>   
                               <table class="DataEntryTable">
                      <tr>
                          <td class="RedHeadding" colspan="7">ATC Updation</td>
                      </tr>
                      <tr>
                          <td >Atc<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmb_atc" CssClass="auto-style13" EnableTheming="True" ErrorMessage="Buyer Required" ForeColor="#FF3300" InitialValue="Select Buyer">*</asp:RequiredFieldValidator>
                          </td>
                          <td >
                            
                                 <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                            </ucc:DropDownListChosen>
                          </td>
                          <td >
                              <asp:Button ID="Btn_atc" runat="server" OnClick="Btn_atc_Click" Text="S" ValidationGroup="S" style="width: 23px" />
                          </td>
                          <td ><span class="auto-style13">Country :</span></td>
                          <td >
                               <ucc:DropDownListChosen ID="cmb_country" runat="server" DataSourceID="SqlDataSource2" DataTextField="Description" DataValueField="CountryID" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                          </td>
                          <td>House Date :<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="dtp_housedate" CssClass="auto-style13" EnableTheming="True" ErrorMessage="Select HouseDate" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                          </td>
                          <td >
                              <ig:WebDatePicker ID="dtp_housedate" runat="server">
                              </ig:WebDatePicker>
                          </td>
                      </tr>
                      <tr>
                          <td ><span class="auto-style13">No of Style Group : </span>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_stylenum" CssClass="auto-style13" EnableTheming="True" ErrorMessage="Style No  Required" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                              <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txt_stylenum" CssClass="auto-style13" ErrorMessage="Only Numbers Allowed" ForeColor="#FF3300" ValidationExpression="\d+">*</asp:RegularExpressionValidator>
                          </td>
                          <td >
                              <asp:TextBox ID="txt_stylenum" runat="server" CssClass="auto-style13"></asp:TextBox>
                          </td>
                          <td >&nbsp;</td>
                          <td ><span >Ship Start Date </span>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="dtp_shipStartdate" CssClass="auto-style13" EnableTheming="True" ErrorMessage="Select Ship Date" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                          </td>
                          <td >
                              <ig:WebDatePicker ID="dtp_shipStartdate" runat="server">
                              </ig:WebDatePicker>
                          </td>
                          <td >Finish Date :
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="dtp_finishdate" CssClass="auto-style13" EnableTheming="True" ErrorMessage="Select Finish Date" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                          </td>
                          <td>
                              <ig:WebDatePicker ID="dtp_finishdate" runat="server">
                              </ig:WebDatePicker>
                          </td>
                      </tr>
                      <tr>
                          <td >ShipITReference:
                          </td>
                          <td ><asp:TextBox ID="txt_shipitrefernce" runat="server" CssClass="auto-style13"></asp:TextBox>
                          </td>
                          <td ></td>
                          <td >Merchandiser</td>
                          <td ><asp:TextBox ID="txt_merchandiser" runat="server" CssClass="auto-style13"></asp:TextBox>
                          </td>
                          <td >Atc#
                          </td>
                          <td >
                              <asp:Label ID="lbl_stylenum" runat="server" Visible="False"></asp:Label>
                          </td>
                      </tr>
                                   <tr>
                                       <td>Projection Qty</td>
                                       <td><cc2:NumericTextBox ID="txt_projqty" runat="server" Enabled="False" /></td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                       <td>&nbsp;</td>
                                   </tr>
                      <tr>
                          <td >
                              <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcNum], [AtcId] FROM [AtcMaster]"></asp:SqlDataSource>
                          </td>
                          <td >
                              <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [Description], [CountryID] FROM [CountryMaster]"></asp:SqlDataSource>
                          </td>
                          <td >&nbsp;</td>
                          <td >
                              <asp:Button ID="btn_update" runat="server" style="height: 26px; margin-bottom: 0px;" Text="Add " OnClick="btn_update_Click" />
                          </td>
                          <td rowspan="2">
                              <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Italic="True" Font-Names="Calibri" Font-Size="X-Small" Height="52px" />
                          </td>
                          <td >&nbsp;</td>
                          <td>&nbsp;</td>
                      </tr>
                      
                  </table>

                          </td>
                      </tr>
                      <tr>
                          <td>   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="tbl_atcdetail" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowDataBound="tbl_atcdetail_RowDataBound" ShowHeaderWhenEmpty="True" style="font-size: x-small; font-family: Calibri" Width="1027px" DataKeyNames="OurStyleID" OnRowCommand="tbl_atcdetail_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="OurStyleID" HeaderText="OurStyleID" />
                                <asp:BoundField DataField="atcid" HeaderText="atcid" />
                                <asp:BoundField DataField="Ourstyle" HeaderText="Ourstyle" />
                                <asp:TemplateField HeaderText="BuyerStyle" SortExpression="BuyerStyle">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Buyerstyle" Visible="false"  runat="server"  Text='<%# Bind("BuyerStyle") %>'></asp:Label>
                                        <asp:DropDownList ID="ddl_Buyerstyle" runat="server" DataSourceID="BuyerStyle1"  DataTextField="BuyerStyle" DataValueField="BuyerStyleID"  Font-Names="Calibri" Font-Size="X-Small" Height="16px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FOB" SortExpression="Fob">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtfob" runat="server" Font-Names="Calibri" Font-Size="X-Small" Height="15px" Text='<%# Bind("Fob") %>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Category">
                                    <ItemTemplate>
                                         <asp:Label ID="lbl_catid" runat="server" Visible="false" Text='<%# Bind("CategoryID") %>'></asp:Label>
                                        <asp:DropDownList ID="ddl_catid" runat="server" DataSourceID="SqlDataSource4" DataTextField="CategoryName" DataValueField="CategoryID" Font-Names="Calibri" Font-Size="X-Small" Height="16px">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField ButtonType="Button" CommandName="Update" HeaderText="Update" Text="Update" />
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
                  </table>
              

              </div>
              

          </ContentTemplate>
      </asp:UpdatePanel>
  

    <div>

       
             

       
                <br />

       
    </div>
     
    <div>

        <asp:Button ID="Btn_addetails" runat="server" Text="Save" OnClick="Btn_addetails_Click" />

         
           

    </div>

    <div>
  <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
       

    </div>

       <asp:SqlDataSource ID="BuyerStyle1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                SelectCommand="SELECT BuyerStyle, BuyerStyleID FROM BuyerStyleMaster"></asp:SqlDataSource>

       
                <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT [CategoryID], [CategoryName] FROM [GarmentCategory]">
                </asp:SqlDataSource>
</asp:Content>
