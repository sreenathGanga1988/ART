<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="OrderOnhandReportofYear.aspx.cs" Inherits="ArtWebApp.Reports.Production.MSI.OrderOnhandReportofYear" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 

  
    <link href="../../../css/style.css" rel="stylesheet" />
 
  
   
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
            <td style="color: #FFFFFF; text-align: center; background-color: #990000">Yearly ShorClosed Report</td>
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
                                 <ucc:DropDownListChosen ID="cmb_year" runat="server" DisableSearchThreshold="10" Width="200px">
                                     <asp:ListItem>2017</asp:ListItem>
                                     <asp:ListItem>2018</asp:ListItem>
                                     <asp:ListItem>2019</asp:ListItem>
                                     <asp:ListItem>2020</asp:ListItem>
                                 </ucc:DropDownListChosen>
                    
                
                            </td>
                            <td class="SearchButtonTD">
                                 
                                
                     
                                
                            </td>
                            <td>
                               
                                &nbsp;</td>
                            </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        <tr>
                            <td class="NormalTD">&nbsp;</td>
                            <td class="NormalTD">
                                &nbsp;</td>
                            <td class="SearchButtonTD">
                                <asp:Button ID="S" runat="server" OnClick="Button3_Click1" Text="S" />
                              
                            </td>
                            <td>&nbsp;</td>
                        </tr>

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                        

                </table>

                
               
               
            </td>
        </tr>
        
       
        <tr>
                <td class="ReportViewSection">
            <asp:UpdatePanel ID="upl_rpt" runat="server">
                <ContentTemplate>
                    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%">
                        <LocalReport ReportPath="Reports\RDLC\APO.rdlc">
                        </LocalReport>
                    </rsweb:ReportViewer>
                </ContentTemplate>

            </asp:UpdatePanel>

        </td>
        </tr>
    </table>
    </div>

<div>
        <table class="DataEntryTable">
                    <tr>
                      
                        <td class="auto-style8"><asp:UpdatePanel ID="upd_main" runat="server">
                                    <ContentTemplate>
                                       <%-- <ig:WebDropDown ID="cmb_ourstyle" runat="server" Width="189px" TextField="name"
        DropDownContainerHeight="300px" EnableDropDownAsChild="false"
        DropDownContainerWidth="200px" DropDownAnimationType="EaseOut" EnablePaging="True"
        PageSize="12" Height="22px" ValueField="pk" CurrentValue="Select OurStyle" AutoPostBack="True" OnDataBound="cmb_ourstyle_DataBound" OnValueChanged="cmb_ourstyle_ValueChanged">
                                            <DropDownItemBinding TextField="name" ValueField="pk" />
                                        </ig:WebDropDown>--%>

                                    </ContentTemplate>
                                </asp:UpdatePanel></td>
                        
                    </tr>
                   
                    
                    <tr>
                        <td class="NormalTD">
                           
                   
                        </td>
                    
                    </tr>
                   
                    
                </table>
                    
        <br />
                    
    </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>


           <%--<asp:UpdateProgress ID="PageUpdateProgress" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="0" DynamicLayout="true">
                                <ProgressTemplate>
                                   <div class="modal">
        <div class="center">
          <img  src="../../Image/loader.gif" style="position: relative; top: 45%;" > </img>
        </div>
    </div>
                                     
                                       
                                        
                                </ProgressTemplate>
                            </asp:UpdateProgress>--%>

</asp:Content>
