<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DebitNoteGenerator.aspx.cs" Inherits="ArtWebApp.Accounts.DebitNoteGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../css/style.css" rel="stylesheet" />
    <style type="text/css">
    .auto-style1 {
        width: 281px;
        height: 197px;
    }
</style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="FullTable">
     <div class="RedHeaddingdIV"> Credit Note generator</div>
     <div>


         <table class="DataEntryTable">
             <tr>
                 <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                  <td class="SearchButtonTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
             </tr>
             <tr>
                 <td class="NormalTD">Credit Note For</td>
                  <td class="NormalTD">
                      <ucc:DropDownListChosen ID="drp_creditfor" runat="server" DisableSearchThreshold="10" Width="200px">
                          <asp:ListItem>Buyer</asp:ListItem>
                          <asp:ListItem>Supplier</asp:ListItem>
                          <asp:ListItem>Factory</asp:ListItem>
                      </ucc:DropDownListChosen>
                 </td>
                  <td class="SearchButtonTD">
                      <asp:Button ID="Button1" runat="server" Text="S" OnClick="Button1_Click" />
                 </td>
                <td class="NormalTD">Creditor name</td>
                  <td class="NormalTD">
                      <ucc:DropDownListChosen ID="cmb_credit" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                      </ucc:DropDownListChosen>
                 </td>
                 <td class="SearchButtonTD">
                     &nbsp;</td>
             </tr>
         </table>


     </div>
       <div>
           <table class="DataEntryTable">
                  <tr>
                  <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                  <td class="SearchButtonTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
               <tr>
                  <td class="NormalTD">Debit From </td>
                  <td class="NormalTD">
                      <ucc:DropDownListChosen ID="cmb_debitfrom" runat="server" DisableSearchThreshold="10" Width="200px">
                          <asp:ListItem>None</asp:ListItem>
                          <asp:ListItem>Atc</asp:ListItem>
                          <asp:ListItem>Factory</asp:ListItem>
                          <asp:ListItem>Buyer</asp:ListItem>
                          <asp:ListItem>Supplier</asp:ListItem>
                      </ucc:DropDownListChosen>
                   </td>
                  <td class="SearchButtonTD">
                      <asp:Button ID="Button3" runat="server" Text="S" OnClick="Button3_Click" />
                   </td>
                <td class="NormalTD">Debitor name </td>
                  <td class="NormalTD">
                      <ucc:DropDownListChosen ID="cmb_debitorname" runat="server" DataTextField="name" DataValueField="pk" DisableSearchThreshold="10" Width="200px">
                      </ucc:DropDownListChosen>
                   </td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
            
           </table>
     </div>
     <div>
         <table class="DataEntryTable">
                  <tr>
                  <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                  <td class="SearchButtonTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
               <tr>
                  <td class="NormalTD">Amount (in USD)</td>
                  <td class="NormalTD">
                      <asp:TextBox ID="txt_amount" runat="server"></asp:TextBox>
                   </td>
                  <td class="SearchButtonTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
            
               <tr>
                  <td class="NormalTD">Message</td>
                  <td colspan="2" rowspan="2">
                      <textarea id="txt_message" runat="server" class="auto-style1" name="S1"></textarea></td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
            
               <tr>
                  <td class="NormalTD">&nbsp;</td>
                <td class="NormalTD">&nbsp;</td>
                  <td class="NormalTD">&nbsp;</td>
                 <td class="SearchButtonTD">&nbsp;</td>
               </tr>
            
           </table>

     </div>
     <div><div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div></div>
     <div>
         <asp:Button ID="Button4" runat="server" Text="Create Credit Note" OnClick="Button4_Click" />
     </div>
     <div></div>
</div>
   
  
</asp:Content>
