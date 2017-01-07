<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="SampleWithoutATC.aspx.cs" Inherits="ArtWebApp.Sampling.SampleWithoutATC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style type="text/css">
      
        .style2
        {
        }
        .style4
        {
            width: 142px;
        }
       
        .style8
        {
            width: 207px;
        }
        .style9
        {
            width: 212px;
            height: 23px;
        }
      
        .style12
        {
            height: 23px;
            width: 154px;
        }
      
        .style26
        {
        }
        .style28
        {
            width: 287px;
        }
    
        .style32
        {
        }
        .style33
        {
            width: 120px;
        }
        .style34
        {
            width: 132px;
        }
        .style35
        {
            width: 143px;
        }
        .style36
        {
            width: 193px;
        }
      
        .style39
        {
            width: 69px;
        }
        .style40
        {
            width: 85px;
        }
        .style41
        {
            width: 86px;
        }
        
        .auto-style3 {
            width: 85px;
            background-color: #FFFFFF;
        }
      
      
      
     
       
       
       
        .auto-style7 {
            width: 160px;
        }
      
      
      
     
       
       
       
    </style>

    <link href="../css/style.css" rel="stylesheet" />
    <div>
    
    </div>
    

    <div class="FullTable">
        <table class="DataEntryTable">
        <tr>
            <td class="RedHeadding" colspan="6"> Cutting Ticket
                </td>
        </tr>
        <tr>
            <td class="NormalTD">
                ATC No:</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_atcnum" runat="server"></asp:TextBox>
               
            </td>
            <td class="NormalTD">
                Buyer:</td>
            <td class="NormalTD">
             
                <asp:TextBox ID="txt_buyer" runat="server"></asp:TextBox>
            </td>
            <td class="NormalTD">
                </td>
            <td class="NormalTD">
                </td>
        </tr>
        <tr>
            <td class="NormalTD">
                Buyer Style</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_buyerstyle" runat="server"></asp:TextBox>
                
            </td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">
                MOVEX STYLE</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_movexstyle" runat="server"></asp:TextBox>
            </td>
            <td class="NormalTD">
                Development</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_development" runat="server" />
            </td>
            <td class="NormalTD">
                Proto</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_proto" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                1st Fit Sample</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_1stfitsample" runat="server" />
            </td>
            <td class="NormalTD">
                2nd Fit Sample</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_2ndfitsample" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                3rd Fit Sample</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_3rdfitsample" runat="server" />
            </td>
            <td class="NormalTD">
                Size Set</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_sizeset" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                Styling</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_styling" runat="server" />
            </td>
            <td class="NormalTD">
                MTL</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_mtl" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                Design</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_design" runat="server" />
            </td>
            <td class="NormalTD">
                Costing</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_costing" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                Booking</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_booking" runat="server" />
            </td>
            <td class="NormalTD">
                PP SAMPLE</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_ppsample" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                &nbsp;</td>
            <td class="NormalTD">
                photo sample</td>
            <td class="NormalTD">
                <asp:CheckBox ID="chk_photosample" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="style2" colspan="4">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <strong>Buyer Requirement</strong></td>
            <td class="style41">
                &nbsp;</td>
            <td class="style2">
                &nbsp;</td>
        </tr>
    </table>
    <table class="DataEntryTable">
        <tr>
            <td class="NormalTD">
                Fabric Type</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_fabric" runat="server" Width="131px"></asp:TextBox>
            </td>
            <td class="NormalTD">
                Width</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_width" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                Description</td>
            <td colspan="3">
                <asp:TextBox ID="txt_description" runat="server" Width="517px" 
                    BackColor="White" BorderColor="White"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="DataEntryTable">
        <tr>
            <td class="NormalTD">
                Merchandiser</td>
            <td class="NormalTD">
                <ucc:DropDownListChosen  ID="drp_merchandiser" runat="server" Height="16px" 
                    Width="126px" DataSourceID="SqlDataSource4" 
                    DataTextField="MerchandiserName" DataValueField="MerchandiserName" 
                   >
                    <asp:ListItem>Shari</asp:ListItem>
                </ucc:DropDownListChosen >
               
            </td>
            <td class="NormalTD">
                Master</td>
            <td class="NormalTD">
                <asp:TextBox ID="txt_master" runat="server" style="margin-left: 0px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                Designer</td>
            <td class="NormalTD">
                <ucc:DropDownListChosen  ID="drp_designer" runat="server" Height="16px" Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="Shariq">Shariq</asp:ListItem>
                    <asp:ListItem Value="Jobin">Jobin</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
            <td class="style8">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="NormalTD">
                Original Sample</td>
            <td class="NormalTD">
                <ucc:DropDownListChosen  ID="drp_originalsample" runat="server" Height="16px" 
                    Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
            <td class="style8">
                Style Diagram</td>
            <td>
                <ucc:DropDownListChosen  ID="drp_stylediagram" runat="server" Height="16px" 
                    Width="126px">
                    <asp:ListItem>Select</asp:ListItem>
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
        </tr>
        <tr>
            <td class="NormalTD">
                Spec.</td>
            <td class="NormalTD">
                <ucc:DropDownListChosen  ID="drp_spec" runat="server" Height="16px" Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem>Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
            <td class="style8">
                Pattern</td>
            <td>
                <ucc:DropDownListChosen  ID="drp_pattern" runat="server" 
                    Height="26px" Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                    <asp:ListItem Value="No">No</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
        </tr>
    </table>

  
    <table class="DataEntryTable">
        <tr>


            <td class="style26">
                Size</td>
            <td class="style34">
                <asp:TextBox ID="txt_size1" runat="server"></asp:TextBox>
            </td>
            <td class="style35">
                <asp:TextBox ID="txt_size2" runat="server"></asp:TextBox>
            </td>
            <td class="style32">
                &nbsp;</td>
            <td class="style33">
                <asp:TextBox ID="txt_size3" runat="server"></asp:TextBox>
            </td>
            <td class="style33">
                <asp:TextBox ID="txt_size4" runat="server"></asp:TextBox>
            </td>
            <td>
                Toatl</td>
        </tr>
        <tr>
            <td class="style26">
                Qty</td>
            <td class="style34">
            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_qty1" runat="server" AutoPostBack="true" Text="0" ontextchanged="txt_qty1_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
            <td class="style35">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_qty2" runat="server" AutoPostBack="true" 
                            ontextchanged="txt_qty2_TextChanged" Text="0"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="style32">
                &nbsp;</td>
            <td class="style33">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_qty3" runat="server" AutoPostBack="true" 
                            ontextchanged="txt_qty3_TextChanged" Text="0"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td class="style33">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_qty4" runat="server" AutoPostBack="true" Text="0" 
                    ontextchanged="txt_qty4_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
            <td>
             <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txt_total" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td class="style26" colspan="2">
                POCKETIN</td>
            <td class="style32" colspan="5">
                <ucc:DropDownListChosen  ID="drp_pocketin" runat="server" 
                    Height="16px" Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="White">White</asp:ListItem>
                    <asp:ListItem Value="Natural">Natural</asp:ListItem>
                    <asp:ListItem Value="DTM">DTM</asp:ListItem>
                    <asp:ListItem Value="100% CTN /TC">100% CTN /TC</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
        </tr>
        <tr>
            <td class="style26" colspan="2">
                INTERLININ</td>
            <td class="style32" colspan="5">
                <ucc:DropDownListChosen  ID="drp_interlinin" runat="server"
                    Height="16px" Width="126px">
                    <asp:ListItem Selected="True">Select</asp:ListItem>
                    <asp:ListItem Value="Fusible">Fusible</asp:ListItem>
                    <asp:ListItem Value="Non">Non</asp:ListItem>
                    <asp:ListItem Value="Woven">Woven</asp:ListItem>
                </ucc:DropDownListChosen >
            </td>
        </tr>
        <tr>
            <td class="style26" colspan="7">
                <table class="DataEntryTable">
                    <tr>
                        <td class="style8">
                            LABELS</td>
                        <td>
                            &nbsp;</td>
                        <td class="style36">
                            &nbsp;</td>
                        <td class="auto-style7">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Main Lables</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_mainlable" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            Button</td>
                        <td class="auto-style7">
                            <ucc:DropDownListChosen  ID="drp_button" runat="server"
                                Height="26px" Width="126px">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem Value="PLASTI">PLASTI</asp:ListItem>
                                <asp:ListItem Value="CHALK">CHALK</asp:ListItem>
                                <asp:ListItem Value="SHANK">SHANK</asp:ListItem>
                                <asp:ListItem Value="CLEA">CLEA</asp:ListItem>
                                <asp:ListItem Value="SNAP">SNAP</asp:ListItem>
                                <asp:ListItem Value="PRES">PRES</asp:ListItem>
                                <asp:ListItem Value="CHAL">CHAL</asp:ListItem>
                                <asp:ListItem Value="LOGO">LOGO</asp:ListItem>
                                <asp:ListItem Value="PEARL">PEARL</asp:ListItem>
                                <asp:ListItem Value="METAL">METAL</asp:ListItem>
                                <asp:ListItem Value="HORN">HORN</asp:ListItem>
                                <asp:ListItem Value="REVE">REVE</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Care Lable</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_carelable" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            Zipper Nick</td>
                        <td class="auto-style7">
                            <ucc:DropDownListChosen  ID="drp_zippernick" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="INVISIB">INVISIB</asp:ListItem>
                                <asp:ListItem Value="NYLON">NYLON</asp:ListItem>
                                <asp:ListItem Value="META">META</asp:ListItem>
                                <asp:ListItem Value="DTM">DTM</asp:ListItem>
                                <asp:ListItem Value="PLASTI">PLASTI</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Size Lable</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_sizelable" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            Lined</td>
                        <td class="auto-style7">
                            <ucc:DropDownListChosen  ID="drp_lined" runat="server"
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="SATIN">SATIN</asp:ListItem>
                                <asp:ListItem Value="TFFT">TFFT</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Joker</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_joker" runat="server"
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            Others</td>
                        <td class="auto-style7">
                            <ucc:DropDownListChosen  ID="drp_others" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="HK">HK</asp:ListItem>
                                <asp:ListItem Value="CORD">CORD</asp:ListItem>
                                <asp:ListItem Value="TAPE">TAPE</asp:ListItem>
                                <asp:ListItem Value="BUCK">BUCK</asp:ListItem>
                                <asp:ListItem Value="VELCR">VELCR</asp:ListItem>
                                <asp:ListItem Value="RIVET">RIVET</asp:ListItem>
                                <asp:ListItem Value="CIBON">CIBON</asp:ListItem>
                                <asp:ListItem Value="ELST">ELST</asp:ListItem>
                                <asp:ListItem Value="EYELET">EYELET</asp:ListItem>
                                <asp:ListItem Value="TOGGLE">TOGGLE</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Tape Lable</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_tapelable" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                <asp:ListItem Value="No">No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            Button Holes</td>
                        <td class="auto-style7">
                            <ucc:DropDownListChosen  ID="drp_buttonholes" runat="server" 
                                Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="Key Hole No">Key Hole No</asp:ListItem>
                                <asp:ListItem Value="Regular No">Regular No</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Thread</td>
                        <td>
                            <ucc:DropDownListChosen  ID="drp_thread" runat="server" Height="26px" Width="126px">
                                <asp:ListItem Selected="True">Select</asp:ListItem>
                                <asp:ListItem Value="DTM">DTM</asp:ListItem>
                                <asp:ListItem Value="Gold">Gold</asp:ListItem>
                                <asp:ListItem Value="Natur">Natur</asp:ListItem>
                                <asp:ListItem Value="Tona">Tona</asp:ListItem>
                            </ucc:DropDownListChosen >
                        </td>
                        <td class="style36">
                            &nbsp;</td>
                        <td class="auto-style7">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style8">
                            Washing Instruction</td>
                        <td colspan="3">
                            <asp:TextBox ID="txt_washinginstruction" runat="server" Width="480px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style8">
                              <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Submit"  
    
    /></td>
                        <td colspan="3">
                            <asp:Button ID="btn_print" runat="server" OnClick="btn_print_Click" Text="Print" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                               <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="Label1" runat="server" Text="*"></asp:Label>


                     
               </div></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
  

    </div>








    <asp:Label ID="lbl_msg" runat="server"></asp:Label>
    &nbsp;
    
    <p>
<asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT [BuyerStyle] FROM [BuyerStyleMaster]">
                </asp:SqlDataSource>
           <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    ProviderName="<%$ ConnectionStrings:ArtConnectionString.ProviderName %>" 
                    SelectCommand="SELECT [BuyerName] FROM [BuyerMaster]"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT [AtcNum] FROM [AtcMaster]"></asp:SqlDataSource>

         <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                    ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" 
                    SelectCommand="SELECT [MerchandiserName] FROM [MerchandiserMaster]">
                </asp:SqlDataSource>
    </p>

</asp:Content>
