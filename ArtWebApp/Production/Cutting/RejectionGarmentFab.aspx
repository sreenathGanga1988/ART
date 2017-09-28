<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="RejectionGarmentFab.aspx.cs" Inherits="ArtWebApp.Production.Cutting.RejectionGarmentFab" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />
    <script src="../../JQuery/GridJQuery.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div>
        <table class="DataEntryTable">
                         <tr>
                        <td class="RedHeadding" colspan="8" >Full Garment Rejection Request</td>
                    </tr>
                         <tr>
                        <td class="NormalTD" >fACTORY</td>
                        <td class="NormalTD" >
                               <asp:UpdatePanel ID="UPD_FACT" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                               <ucc:DropDownListChosen ID="drp_fact" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                               </ucc:DropDownListChosen>
                                      </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD
                            ">&nbsp;</td>
                        <td class="NormalTD" >&nbsp;</td>
                        <td class="NormalTD" >

                               &nbsp;</td>
                        <td class="ButtonTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                        <td class="NormalTD" >
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="NormalTD">atc&nbsp; : </td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_atc" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <ucc:DropDownListChosen ID="drp_atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_atc" runat="server" OnClick="btn_atc_Click" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">ourstyle&nbsp; #</td>
                        <td class="NormalTD">
                            <asp:UpdatePanel ID="upd_ourstyle" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="SearchButtonTD">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btn_OURSTYLE" runat="server" OnClick="btn_OURSTYLE_Click" Text="S" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                    </tr>
                    
               
                    </table>

    </div>

    <div>
       
            <asp:UpdatePanel ID="upd_grid" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                             <asp:GridView ID="tbl_podetails" runat="server" AutoGenerateColumns="False" BackColor="White"  DataKeyNames="RejFabReqID"  BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" style="font-size: x-small; font-family: Calibri" Width="100%" Font-Size="Large">
       
            <Columns> 
                <asp:TemplateField>  
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="checkAll" runat ="server" onclick="checkAll(this)"/>
                                    </HeaderTemplate>                                 
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_select" runat="server" onclick="Check_Click(this)"/>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                <asp:TemplateField HeaderText="Fabreqid" SortExpression="Fabreqid">
                 
                    <ItemTemplate>
                        <asp:Label ID="lbl_RejFabReqID" runat="server" Text='<%# Bind("RejFabReqID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Fabreqno" HeaderText="Fabreqno" SortExpression="Fabreqno" />
           
                <asp:BoundField DataField="Reqdate" HeaderText="Reqdate" SortExpression="Reqdate" />
                <asp:BoundField DataField="DepartmentName" HeaderText="DepartmentName" SortExpression="DepartmentName" />
                <asp:BoundField DataField="ReqQty" HeaderText="ReqQty" SortExpression="ReqQty" />
                <asp:BoundField DataField="ColorName" HeaderText="ColorName" SortExpression="ColorName" />
                <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                <asp:BoundField DataField="LocationName" HeaderText="LocationName" SortExpression="LocationName" />
                <asp:TemplateField HeaderText="Allowedfabric" SortExpression="Allowedfabric">
                   
                    <ItemTemplate>
                        <asp:TextBox ID="txt_allowed" runat="server" Text='<%# Bind("Allowedfabric") %>'></asp:TextBox>
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
        
             
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        RejectionExtraFabbReq.Fabreqid, RejectionExtraFabbReq.Fabreqno, RejectionExtraFabbReq.RejFabReqID, RejectionExtraFabbReq.Reqdate, RejectionExtraFabbReq.DepartmentName, 
                         RejectionExtraFabbReq.ReqQty, POPackDetails.ColorName, AtcDetails.OurStyle, LocationMaster.LocationName,0.0 as Allowedfabric
FROM            RejectionExtraFabbReq INNER JOIN
                         POPackDetails ON RejectionExtraFabbReq.PoPack_Detail_PK = POPackDetails.PoPack_Detail_PK INNER JOIN
                         AtcDetails ON POPackDetails.OurStyleID = AtcDetails.OurStyleID INNER JOIN
                         LocationMaster ON RejectionExtraFabbReq.Location_PK = LocationMaster.Location_PK"></asp:SqlDataSource>



    </div>
    <asp:Button ID="Button1" runat="server" Text="Save Request" OnClick="Button1_Click" />
    <div id="Messaediv" runat="server">
        <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>
    </div>
    <div >


    </div>
</asp:Content>
