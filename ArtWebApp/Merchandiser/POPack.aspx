<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="Merchandiser_POPack" Codebehind="POPack.aspx.cs" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--    <style type="text/css">
       
        .auto-style7 {
            text-align: left;
            font-weight: normal;
            font-family: Calibri;
            font-size: medium;
            height: 100%;
            width: 255%;
            text-align: left;
            background-color: #f3f2e5;
            text-transform: uppercase;
        }
       
        .auto-style8 {
        height: 27px;
    }
       
        </style>--%>
 
    
   

    <link href="../css/style.css" rel="stylesheet" />
    <script src="../JQuery/GridJQuery.js"></script>
   
 <script type="text/javascript">

       //calculate the sum of qty on keypress
       function sumofQty(objText) {
       
        
           var cell = objText.parentNode;
           
           var row = cell.parentNode;

           var sum = 0;
           var textboxs = row.getElementsByClassName("txtCalQty");

           for (var i = 0; i < textboxs.length; i++)
           {
               sum += parseFloat(textboxs[i].value);
           }



           var textboxtotalqtys = row.getElementsByClassName("ColorTotal");

           textboxtotalqtys[0].value = sum.toString();
         
           sumofcolortotal(row);
       }




       function sumofcolortotal(row)
       {
           var grd = row.parentNode;
           var textboxtotalqtys = grd.getElementsByClassName("ColorTotal");

           var sum = 0;
           for (var i = 0; i < textboxtotalqtys.length-1; i++) {
               sum += parseFloat(textboxtotalqtys[i].value);
           }
           textboxtotalqtys[textboxtotalqtys.length - 1].value = sum.toString();
       }


       function sumofSizeQty(objText) {


           var cell = objText.parentNode;

           var row = cell.parentNode;
           var grdv = row.parentNode;



           var sum = 0;
           var textboxs = row.getElementsByClassName("txtCalQty");

           for (var i = 0; i < textboxs.length; i++) {
               sum += parseFloat(textboxs[i].value);
           }



           var textboxtotalqtys = row.getElementsByClassName("ColorTotal");

           textboxtotalqtys[0].value = sum.toString();


       }



     </script>
  
    <style type="text/css">
        .auto-style8 {
            height: 27px;
        }
        .auto-style9 {
            width: 100%;
        }
        .auto-style10 {
            height: 27px;
            width: 200px;
            font-size: small;
        }
        .auto-style11 {
            font-size: small;
        }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="FullTable">
        <tr>
            <td class="NormalTD" style="color: #FFFFFF; text-align: center; background-color: #990000"><strong>ASQ&nbsp; </strong></td>
        </tr>
        <tr>
            <td class="DataEntryTable">



                 <table class="DataEntryTable">
                        <tr>
                            <td class="NormalTD">

                                   Atc# 
                
                            </td>
                            <td class="NormalTD">
                               
                   
                    
               <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="SqlDataSource1" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_atc_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                               
                            </td>
                            <td class="NormalTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="buttonAtc" runat="server" Text="S" Height="26px" OnClick="buttonAtc_Click" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td class="NormalTD">

                                ASQ :
                
                            </td>
                            <td class="NormalTD">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                       



                                        <ucc:DropDownListChosen ID="cmb_po" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_po_SelectedIndexChanged">
                            </ucc:DropDownListChosen>




                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                
                            </td>
                            <td class="NormalTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="Btn_showPO" runat="server" CssClass="NormalTD" Height="26px" OnClick="Btn_showPO_Click" Text="S" Width="23px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                     
                            </td>
                            <td>
                               
                    <asp:Button ID="btn_Showpanel" runat="server" Text="Add New ASQ" OnClick="btn_Showpanel_Click" CssClass="NormalTD" />
                     
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td class="NormalTD">

                                OurStyle :</td>
                            <td class="NormalTD">
                               
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                        <ucc:DropDownListChosen ID="cmb_ourstyle" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px" OnSelectedIndexChanged="cmb_po_SelectedIndexChanged">
                            </ucc:DropDownListChosen>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                
                               
                            </td>
                            <td class="NormalTD">
                                 
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Button ID="btn_confirmOurstyle" runat="server" OnClick="btn_confirmOurstyle_Click" Text="S" ValidationGroup="a" Width="23px" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                 
                                </td>
                            <td class="NormalTD">
                               
                                <asp:Label ID="lbl_errordisplayer" runat="server" Text="*" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                               
                                </td>
                            <td class="NormalTD">
                               
                                &nbsp;</td>
                            </tr>

                        <tr>
                            <td class="auto-style10">

                                PROJECTION qTY</td>
                            <td class="NormalTD">
                                   <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                <asp:Label ID="lbl_projection" runat="server" Text="0"></asp:Label>
                    
                  </ContentTemplate>
                                        </asp:UpdatePanel>
                               
                            </td>
                            <td class="NormalTD">
                                 
                                &nbsp;</td>
                            <td class="NormalTD">
                               
                                <span class="auto-style11">asq qTY(eXCLUDING cURRENT)</td>
                            <td class="NormalTD">
                               
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_ASQQTY" runat="server" Text="0"></asp:Label>
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
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                        </td>
                        <td class="NormalTD">
                            &nbsp;</td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="CustomValidator" OnServerValidate  ="CustomValidator1_ServerValidate" ValidationGroup="a"></asp:CustomValidator>
                        </td>
                        <td class="NormalTD">&nbsp;</td>
                        <td class="NormalTD">
                               
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT DISTINCT [AtcNum], [AtcId] FROM [AtcMaster] ORDER BY [AtcNum], [AtcId]">
                </asp:SqlDataSource>
                    
               
                               
                            </td>
                        <td class="NormalTD">

                            &nbsp;</td>
                    </tr>
                </table>
                
            </td>
        </tr>
       
        <tr>
            <td>
                <table class="DataEntryTable">
                    <tr>
                        <td >
                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="GrdDynamic" runat="server" AutoGenerateColumns="False" Width="100%" ShowHeaderWhenEmpty="True" ViewStateMode="Enabled" OnRowDataBound="GrdDynamic_RowDataBound">
                                    </asp:GridView>
                                    <asp:Table ID="Table1" runat="server"></asp:Table>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td class="auto-style8">
                            
                        </td>
                        <td class="auto-style8"></td>
                    </tr>
                    <tr>
                        <td  colspan="3">
                            <table class="auto-style9">
                                <tr>
                                    <td class="NormalTD">Po Status</td>
                                    <td  class="NormalTD">
                                        <asp:UpdatePanel ID="upd_postatus" runat="server">
                                            <ContentTemplate>
                                                <asp:Label ID="lbl_postatus" runat="server" Text="lbl_postatus" Font-Bold="True" Font-Italic="True" ForeColor="#FF3300"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td  class="NormalTD">&nbsp;</td>
                                    <td  class="NormalTD">&nbsp;</td>
                                    <td class="NormalTD">&nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="NormalTD">
                            <table class="DataEntryTable">
                                <tr>
                                    <td class="NormalTD">
                                        <div><asp:Button ID="btn_savePoPack" runat="server" Text="Save PoPack" CssClass="DivLeft" OnClick="btn_savePoPack_Click" Width="124px" ValidationGroup="K" /></div>
                                        
                                    </td>
                                    <td><div></div></td>
                                </tr>
                                <tr>
                                    <td class="NormalTD">
                     
                                <asp:HiddenField ID="ourstylehiden" runat="server" />
                     
                                    </td>
                                    <td>
                               
                                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT StyleColor.StyleColorid, StyleSize.StyleSizeID, StyleColor.AtcId, StyleColor.OurStyleID, StyleColor.OurStyle, StyleColor.GarmentColorCode, StyleColor.GarmentColor, StyleSize.SizeCode, StyleSize.SizeName, 000000 AS POQty FROM StyleColor INNER JOIN StyleSize ON StyleColor.AtcId = StyleSize.AtcId AND StyleColor.OurStyleID = StyleSize.OurStyleID WHERE (StyleColor.OurStyleID = @Param1)">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="ourstylehiden" DefaultValue="0" Name="Param1" PropertyName="Value" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

