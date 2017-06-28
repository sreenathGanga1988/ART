<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeBehind="DebitSupplierAgainstASNShortage.aspx.cs" Inherits="ArtWebApp.Accounts.Debitnote.DebitSupplierAgainstASNShortage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/style.css" rel="stylesheet" />

 
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

        &nbsp;DEBIT NOTES TO SUPPLIER AGAINST SHORTAGE IN ASN </div>
    <div>

        <table class="DataEntryTable">
            <tr>
                <td class="NormalTD">Debit From</td>
                <td class="NormalTD">
                          <ucc:DropDownListChosen ID="drp_supplier" runat="server" DataSourceID="supplierdata" DataTextField="SupplierName" DataValueField="Supplier_PK" DisableSearchThreshold="10" Width="200px">
                                     </ucc:DropDownListChosen>
                </td>
                <td class="NormalTD">
                    <asp:Button ID="BTN_SHOWasn" runat="server" Text="S" />
                </td>
                <td class="NormalTD">&nbsp;</td>
                 <td class="NormalTD">&nbsp;</td>
            </tr>
            </table>

    </div>
    <div>

        <asp:GridView ID="tbl_podata" runat="server" AutoGenerateColumns="False" ShowFooter="True" BackColor="White" BorderColor="#CC9966" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="SupplierDoc_pk" Font-Size="Smaller" style="font-size: small; font-family: Calibri; font-weight: 400;" Width="100%" DataSourceID="SalesDOData">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Po_PK" HeaderText="Po_PK" SortExpression="Po_PK" />
                <asp:BoundField DataField="SupplierDoc_pk" HeaderText="SupplierDoc_pk" InsertVisible="False" ReadOnly="True" SortExpression="SupplierDoc_pk" />
                <asp:BoundField DataField="AtracotrackingNum" HeaderText="AtracotrackingNum" SortExpression="AtracotrackingNum" />
                <asp:BoundField DataField="SupplierDocnum" HeaderText="SupplierDocnum" SortExpression="SupplierDocnum" />
                <asp:BoundField DataField="SupplierETA" HeaderText="SupplierETA" SortExpression="SupplierETA" />
                <asp:BoundField DataField="Containernum" HeaderText="Containernum" SortExpression="Containernum" />
                <asp:BoundField DataField="Remark" HeaderText="Remark" SortExpression="Remark" />
                <asp:BoundField DataField="PONum" HeaderText="PONum" SortExpression="PONum" />
                <asp:BoundField DataField="SYard" HeaderText="SYard" ReadOnly="True" SortExpression="SYard" />
                <asp:BoundField DataField="AYard" HeaderText="AYard" ReadOnly="True" SortExpression="AYard" />
                <asp:BoundField DataField="ShortageYards" HeaderText="ShortageYards" ReadOnly="True" SortExpression="ShortageYards" />
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
        <asp:SqlDataSource ID="SalesDOData" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT        Po_PK, SupplierDoc_pk, AtracotrackingNum, SupplierDocnum, SupplierETA, Containernum, Remark, PONum, SYard, AYard, SYard - AYard AS ShortageYards
FROM            (SELECT        SupplierDocumentMaster.AtracotrackingNum, SupplierDocumentMaster.SupplierDoc_pk, SupplierDocumentMaster.SupplierETA, SupplierDocumentMaster.Containernum, 
                                                    SupplierDocumentMaster.Remark, ProcurementMaster.PONum, SUM(FabricRollmaster.SYard) AS SYard, SUM(FabricRollmaster.AYard) AS AYard, FabricRollmaster.Po_PK, 
                                                    SupplierDocumentMaster.SupplierDocnum, SupplierDocumentMaster.Supplier_pk
                          FROM            SupplierDocumentMaster INNER JOIN
                                                    FabricRollmaster ON SupplierDocumentMaster.SupplierDoc_pk = FabricRollmaster.SupplierDoc_pk INNER JOIN
                                                    SupplierMaster ON SupplierDocumentMaster.Supplier_pk = SupplierMaster.Supplier_PK INNER JOIN
                                                    ProcurementMaster ON FabricRollmaster.Po_PK = ProcurementMaster.PO_Pk
                          GROUP BY SupplierDocumentMaster.SupplierDoc_pk, SupplierDocumentMaster.SupplierETA, SupplierDocumentMaster.Containernum, SupplierDocumentMaster.Remark, ProcurementMaster.PONum, 
                                                    SupplierDocumentMaster.AtracotrackingNum, FabricRollmaster.Po_PK, SupplierDocumentMaster.SupplierDocnum, SupplierDocumentMaster.Supplier_pk
                          HAVING         (SupplierDocumentMaster.Supplier_pk = @Param1)) AS TT
WHERE        (SYard &gt; AYard) AND (SupplierETA &gt; CONVERT(DATETIME, '2017-03-01 00:00:00', 102))">
            <SelectParameters>
                <asp:ControlParameter ControlID="drp_supplier" Name="Param1" PropertyName="SelectedValue" />
            </SelectParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="supplierdata" runat="server" ConnectionString="<%$ ConnectionStrings:ArtConnectionString %>" SelectCommand="SELECT [SupplierName], [Supplier_PK] FROM [SupplierMaster] ORDER BY [SupplierName]"></asp:SqlDataSource>

    </div>
       <div> <asp:Button ID="btn_sumbit" runat="server" OnClick="btn_sumbit_Click" Text="Create Debit Note" /></div>
        <div id="Messaediv" runat="server">
                 


                           <asp:Label ID="lbl_msg" runat="server" Text="*"></asp:Label>


                     
               </div>
</asp:Content>
