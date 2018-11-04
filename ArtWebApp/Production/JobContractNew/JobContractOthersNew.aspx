<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="JobContractOthersNew.aspx.cs" Inherits="ArtWebApp.Production.JobContractNew.JobContractOthersNew" %>
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
                     <td class="NormalTD">Factory/Vendor</td>
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
                  <ucc:DropDownListChosen ID="cmb_atc" runat="server" DataSourceID="atcdatasource" DataTextField="AtcNum" DataValueField="AtcId" DisableSearchThreshold="10" Width="200px">
                 </ucc:DropDownListChosen>
            </td>
             <td class="NormalTD">
                 <asp:Button ID="btn_showPO" runat="server" OnClick="btn_showPO_Click" Text="S" />
            </td>
             <td class="NormalTD"></td>
        </tr>
        <tr>
            <td class="NormalTD">OurStyle</td>
              <td class="NormalTD">
                  <ucc:DropDownListChosen ID="cmb_ourstyle" runat="server" DataSourceID="Ourstyledatasource" DataTextField="OurStyle" DataValueField="OurStyleID" DisableSearchThreshold="10" Width="200px">
                         </ucc:DropDownListChosen>
            </td>
              <td class="NormalTD">
                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="S" />
            </td>
              <td class="NormalTD"></td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                oPTIONAL cOMPONENTS<asp:UpdatePanel ID="udp_optionalcombo"  UpdateMode="Conditional" runat="server">
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
                 <tr>
                     <td>Remarks</td>
                     <td>
                         <asp:TextBox ID="txt_remarks" runat="server" Height="88px" TextMode="MultiLine" Width="197px"></asp:TextBox>
                     </td>
                     <td aria-disabled="True">&nbsp;</td>
                 </tr>
    </table>
         </ContentTemplate>
     </asp:UpdatePanel>


 </div>
    <div class="gridtable">

         <asp:UpdatePanel ID="upd_grid" UpdateMode="Conditional" runat="server">
         <ContentTemplate>

            



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
                                <asp:BoundField DataField="OurStyleID" HeaderText="OurStyleID" InsertVisible="False" ReadOnly="True" SortExpression="OurStyleID" />
                        <asp:BoundField DataField="OurStyle" HeaderText="OurStyle" SortExpression="OurStyle" />
                        <asp:BoundField DataField="AtcNum" HeaderText="AtcNum" SortExpression="AtcNum" />
                        <asp:BoundField DataField="WASH" HeaderText="WASH" ReadOnly="True" SortExpression="WASH" />
                                <asp:TemplateField HeaderText="Washing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_washing"  CssClass="txt_washing"     Enabled ="false"  Width="70px" Text='<%# Bind("EnteredWash") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                        <asp:BoundField DataField="DRY PROCESS" HeaderText="DRY PROCESS" ReadOnly="True" SortExpression="DRY PROCESS" />

                                  <asp:TemplateField HeaderText="DryProcess">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_dryprocess"  CssClass="txt_dryprocess"  Enabled="false"  Width="70px" Text='<%# Bind("EnteredDryProcess") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                        <asp:BoundField DataField="COMPANY LOGISTICS" HeaderText="COMPANY LOGISTICS" ReadOnly="True" SortExpression="COMPANY LOGISTICS" />
                                 <asp:TemplateField HeaderText="Company Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_cmplogistic" CssClass="txt_cmplogistic" Enabled ="false"  Width="70px" Text='<%# Bind("EnteredCompanyLogistic") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>


                        <asp:BoundField DataField="FACTORY LOGISTICS" HeaderText="FACTORY LOGISTICS" ReadOnly="True" SortExpression="FACTORY LOGISTICS" />
                                  <asp:TemplateField HeaderText="Factory Logistic">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_factorylogistic" CssClass="txt_factorylogistic" Enabled="false" Width="70px" Text='<%# Bind("EnteredFactoryLogistic") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                        <asp:BoundField DataField="EMBROIDERY" HeaderText="EMBROIDERY" ReadOnly="True" SortExpression="EMBROIDERY" />


                                  <asp:TemplateField HeaderText="Emb/Printing">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_emb"    Width="70px" CssClass="txt_emb" Enabled="false"  Text='<%# Bind("EnteredEmbroidary") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                         <HeaderStyle Width="70px" />
                                </asp:TemplateField>
                        <asp:BoundField DataField="PRINTING" HeaderText="PRINTING" ReadOnly="True" SortExpression="PRINTING" />

                             
                              
                              
            
                                <asp:TemplateField HeaderText="EnteredPrinting" SortExpression="EnteredPrinting">
                                  
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_printing"    Width="70px" CssClass="txt_emb" Enabled="false"  Text='<%# Bind("EnteredPrinting") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                            
                                       
                                    </ItemTemplate>
                                </asp:TemplateField>


                                 <asp:TemplateField HeaderText="Fab Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_fabcomission" CssClass="txt_fabcomission" Enabled="false"  Width="70px" Text='<%# Bind("EnteredFabCommision") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
                                    </ItemTemplate>
                                        <HeaderStyle Width="70px" />
                                </asp:TemplateField>

                                   <asp:TemplateField HeaderText="Garment Comission">
                                 
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_garcomission" CssClass="txt_garcomission" Enabled="false"  Width="70px" Text='<%# Bind("EnteredGarmentComission") %>' runat="server" onkeypress="return isNumberKey(event,this)" ></asp:TextBox>
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
    <asp:SqlDataSource ID="Ourstyledatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [OurStyleID], [OurStyle] FROM [AtcDetails] WHERE ([AtcId] = @AtcId)">
    <SelectParameters>
        <asp:ControlParameter ControlID="cmb_atc" Name="AtcId" PropertyName="SelectedValue" Type="Decimal" />
    </SelectParameters>
</asp:SqlDataSource>

                <asp:SqlDataSource ID="atcdatasource" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [AtcNum], [AtcId] FROM [AtcMaster]"></asp:SqlDataSource>
            
    <br />
                
</asp:Content>

