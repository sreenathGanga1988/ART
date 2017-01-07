<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="CutOrderForm.aspx.cs" Inherits="ArtWebApp.Production.CutOrder.CutOrderForm" %>
<%@ Register assembly="Infragistics35.Web.v12.1, Version=12.1.20121.2236, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.Web.UI.ListControls" tagprefix="ig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        
      
        
        </style>
    <link href="../../css/style.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="FullTable">
        <tr>
            <td class="RedHeadding">New Cut Order</td>
        </tr>
        <tr>
            <td>
               
                        <table class="DataEntryTable">
                            <tr>
                                <td  >Atc #</td>
                                <td  >
                                   
                                    <ucc:DropDownListChosen ID="drp_Atc" runat="server" DataTextField="name" DataValueField="pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td><td  >
                                    <asp:Button ID="btn_show" runat="server" OnClick="btn_show_Click" Text="S" ValidationGroup="a" style="width: 23px" />
                                </td>
                                <td >&nbsp;</td>
                                <td >
                                     
                                </td>
                                <td  >
                                    &nbsp;</td>
                                
                            </tr>
                            <tr>
                                <td >Color</td>
                                <td  colspan="5">

                                  
                                    <ucc:DropDownListChosen ID="ddl_color" runat="server" DataTextField="ItemDescription" DataValueField="Skudet_pk">
                                    </ucc:DropDownListChosen>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                 
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <table class="DataEntryTable">
                            <tr>
                                <td class="RedHeadding" colspan="6">Cut Order Details </td>
                            </tr>
                            <tr>
                                <td >our style</td>
                                <td >
                                  
                                  <ucc:DropDownListChosen ID="drp_ourstyle" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td >
                                    &nbsp;</td>
                                <td >
                                    &nbsp;</td>
                                <td >&nbsp;</td>
                                <td >&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Cut order #</td>
                                <td>
                                    <asp:TextBox ID="txt_cutno" runat="server" Width="131px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cutno" EnableTheming="True" ErrorMessage="Enter Cut Order #" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>
                                   
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td >CutOrder Type :</td>
                                <td >
                                
                                    <ucc:DropDownListChosen ID="drp_cutorderType"  AutoPostBack="true" runat="server" Width="200px" OnSelectedIndexChanged="drp_cutorderType_SelectedIndexChanged" >
                                        <asp:ListItem Value="Extra">Extra</asp:ListItem>
   <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                    </ucc:DropDownListChosen>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="drp_cutorderType" EnableTheming="True" ErrorMessage="Enter Cut Order Type" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td >Reason</td>
                                <td >
                                
                                       <ucc:DropDownListChosen ID="drp_reason" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                </td>
                                <td ></td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td>To factory</td>
                                <td>
                                      <ucc:DropDownListChosen ID="drp_fact" runat="server" DataTextField="Name" DataValueField="Pk" Width="200px">
                                    </ucc:DropDownListChosen>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="drp_fact" EnableTheming="True" ErrorMessage="Enter To factory" ForeColor="#FF3300">*</asp:RequiredFieldValidator></td>
                                <td>cutorder qty</td>
                                <td> <asp:TextBox ID="txt_cutQty" AutoPostBack="true" ValidationGroup="k" runat="server" OnTextChanged="txt_cutQty_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_cutQty" EnableTheming="True" ErrorMessage="Enter Cut Order Qty" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txt_cutQty" ErrorMessage="Enter  Numeric Value for Cut  Qty" ForeColor="#CC3300" ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td >CO fab Allocation</td>
                                <td >
                                    <asp:TextBox ID="txt_fabAllocation" AutoPostBack="true" ValidationGroup="k"  runat="server" OnTextChanged="txt_fabAllocation_TextChanged"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_fabAllocation" EnableTheming="True" ErrorMessage="Enter Cut Order fab Allocation" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txt_fabAllocation" ErrorMessage="Enter  Numeric Value for Fab Qty" ForeColor="#CC3300" ValidationExpression="^[1-9]\d*(\.\d+)?$">*</asp:RegularExpressionValidator>
                                </td>
                                <td >Cuttable Width</td>
                                <td >
                                    <asp:TextBox ID="txt_cutablewidth" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_cutablewidth" EnableTheming="True" ErrorMessage="Enter Cutable Width" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td ></td>
                                <td ></td>
                            </tr>
                            <tr>
                                <td>Shrinkage</td>
                                <td>
                                    <asp:TextBox ID="txt_shrinkage" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txt_shrinkage" EnableTheming="True" ErrorMessage="Enter Shrinkage" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td>Bal to Cut Qty</td>
                                <td>
                                    <asp:TextBox ID="txt_baltoCutQty" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txt_baltoCutQty" EnableTheming="True" ErrorMessage="Enter Bal to Cut Qty" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Delivery&nbsp; Qty</td>
                                <td>
                                    <asp:TextBox ID="txt_deliveryQty" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txt_deliveryQty" EnableTheming="True" ErrorMessage="Enter Delivery Qty" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td>Approved Consumption</td>
                                <td>
                                    <asp:TextBox ID="txt_approvedcon" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txt_approvedcon" EnableTheming="True" ErrorMessage="EnterApproved Consumption" ForeColor="#FF3300">*</asp:RequiredFieldValidator>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbl_errordisplayer" runat="server" Font-Italic="True" ForeColor="#FF3300" Text="*"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td colspan="6"><div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Button ID="btn_saveCutorder" runat="server" OnClick="btn_saveCutorder_Click" Text="Save" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
                                </td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
