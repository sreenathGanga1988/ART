using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ArtWebApp.DBTransaction;
using System.Drawing;

namespace ArtWebApp.Approvals
{
    public partial class ProcurementApproval : System.Web.UI.Page
    {
        List<String> Approverlist = new List<String>(new String[] { "Mannan", "siraj", "Abhishek" , "Sreenath" });

        List<String> forwaderlist = new List<String>(new String[] { "Mahendra", "Vijeesh", "Abhishek", "shaveed" ,"vineeth"});
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                string currentusername = HttpContext.Current.User.Identity.Name.ToString();

                if (Approverlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase) || forwaderlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase))
                {
                    String navtype = Request.QueryString["navtype"];

                    string v = Request.QueryString["navtype"];
                    if (navtype == "PO")
                    {
                        MultiView1.ActiveViewIndex = 0;
                    }
                    else if (navtype == "SPOApproval")
                    {
                        MultiView1.ActiveViewIndex = 1;
                    }
                    else if (navtype == "RO")
                    {
                        MultiView1.ActiveViewIndex = 2;
                    }
                    else if (navtype == "SRO")
                    {
                        MultiView1.ActiveViewIndex = 3;
                    }
                    else if (navtype == "WPO")
                    {
                        MultiView1.ActiveViewIndex = 4;
                    }
                    else if (navtype == "EBOM")
                    {
                        MultiView1.ActiveViewIndex = 5;
                    }
                    else if (navtype == "Missplaced")
                    {
                        MultiView1.ActiveViewIndex = 6;
                    }

                    if (forwaderlist.Contains(currentusername, StringComparer.OrdinalIgnoreCase))
                    {
                        btn_approveAll.Visible = false;
                        btn_spoapproval.Visible = false;
                        btn_stockro.Visible = false;
                        btn_wrongpo.Visible = false;
                        btn__approveextrabom.Visible = false;
                        btn_ro.Visible = false;
                        if (navtype == "PO")
                        {
                            setgridviewPO(currentusername);
                        }
                        else if (navtype == "SPOApproval")
                        {
                            setgridviewSPO();
                        }
                        else if (navtype == "RO")
                        {
                            setgridviewRO();
                        }
                        else if (navtype == "SRO")
                        {
                            setgridviewSRO();
                        }



                        
                                }
                }
                else
                {
                    Response.Redirect("../Authorisation.aspx?navtype=Approval");
                }

                //    if (HttpContext.Current.User.Identity.Name == "Mannan" || HttpContext.Current.User.Identity.Name == "Abhishek" || HttpContext.Current.User.Identity.Name == "Sreenath" || HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh" || HttpContext.Current.User.Identity.Name == "vineeth" || HttpContext.Current.User.Identity.Name == "siraj")
                //{
                    
                //}
                //else
                //{
                //    //string message = "You are  not Authorised for this action .You will be redirected to the Home Page.";
                //    //string url = "./Default2.aspx";
                //    //string script = "window.onload = function(){ alert('";
                //    //script += message;
                //    //script += "');";
                //    //script += "window.location = '";
                //    //script += url;
                //    //script += "'; }";
                //    //ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    
                //}
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
        }








        #region PO





        protected void btn_approveAll_Click(object sender, EventArgs e)
        {
            approvePO();
        }



        /// <summary>
        /// Approve PO
        /// </summary>
        public void approvePO()
        {

            ProcurementTransaction ptrans = new ProcurementTransaction();
            for (int i = 0; i < tbl_podata.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podata.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_podata.Rows[i].Cells[1].Text);
                    ptrans.ApprovePO(popk);
                }

            }

            tbl_podata.DataBind();
        }
        /// <summary>
        /// Forward PO for Approval in Tablet
        /// </summary>
        public void ForwardPOforApproval()
        {
            string currentusername = HttpContext.Current.User.Identity.Name.ToString();
            ProcurementTransaction ptrans = new ProcurementTransaction();
            for (int i = 0; i < tbl_podata.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_podata.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_podata.Rows[i].Cells[1].Text);
                    ptrans.ForwardApprovePO(popk);
                }

            }

            setgridviewPO(currentusername);
        }

        public void setgridviewPO(string currentusername)
        {

            if (HttpContext.Current.User.Identity.Name.Contains("Mahendra") || HttpContext.Current.User.Identity.Name.Contains("Vijeesh"))
            {

            //}
            //if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "vijeesh")
            //{
                var username = Session["username"].ToString().Trim();
                SqlDataSource1.SelectCommand = @"SELECT        ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, SUM(ProcurementDetails.POQty * ProcurementDetails.POUnitRate) 
                         AS POValue, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName, COUNT(POApproval.ForwardedBy) AS Isforwarded
FROM            ProcurementMaster INNER JOIN
                         SupplierMaster ON ProcurementMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         AtcMaster ON ProcurementMaster.AtcId = AtcMaster.AtcId INNER JOIN
                         CurrencyMaster ON ProcurementMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         ProcurementDetails ON ProcurementMaster.PO_Pk = ProcurementDetails.PO_Pk LEFT OUTER JOIN
                         POApproval ON ProcurementMaster.PO_Pk = POApproval.PO_PK
GROUP BY ProcurementMaster.PO_Pk, ProcurementMaster.PONum, AtcMaster.AtcNum, SupplierMaster.SupplierName, CurrencyMaster.CurrencyCode, ProcurementMaster.AddedDate, ProcurementMaster.AddedBy, 
                         ProcurementMaster.IsApproved, ProcurementMaster.IsDeleted, AtcMaster.MerchandiserName ,ProcurementMaster.IsNormal
HAVING        (ProcurementMaster.IsApproved = N'N') AND (ProcurementMaster.IsDeleted <> N'Y') AND (ProcurementMaster.IsNormal = N'Y')   AND(AtcMaster.MerchandiserName  like '" + Session["username"].ToString().Trim() + "') order by ProcurementMaster.PO_Pk";
            }
            
            tbl_podata.DataBind();

        }


        public void setgridviewSPO()
        {
            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh" ||  HttpContext.Current.User.Identity.Name == "Seenu")
            {

                SqlDataSource2.SelectCommand = @"SELECT        StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.DeliveryDate, StockPOMaster.AddedBy, StockPOMaster.AddedDate, 
                         SUM(StockPODetails.Unitprice * StockPODetails.POQty) AS POvalue, CurrencyMaster.CurrencyCode, UserMaster.Department_PK
FROM            StockPOMaster INNER JOIN
                         StockPODetails ON StockPOMaster.SPO_Pk = StockPODetails.SPO_PK INNER JOIN
                         SupplierMaster ON StockPOMaster.Supplier_Pk = SupplierMaster.Supplier_PK INNER JOIN
                         CurrencyMaster ON StockPOMaster.CurrencyID = CurrencyMaster.CurrencyID INNER JOIN
                         UserMaster ON StockPOMaster.AddedBy = UserMaster.UserName
GROUP BY StockPOMaster.SPO_Pk, StockPOMaster.SPONum, SupplierMaster.SupplierName, StockPOMaster.DeliveryDate, StockPOMaster.AddedBy, StockPOMaster.AddedDate, CurrencyMaster.CurrencyCode, 
                         StockPOMaster.IsApproved, UserMaster.Department_PK
HAVING        (StockPOMaster.IsApproved = N'N') AND (UserMaster.Department_PK = "+int.Parse(Session["Department_PK"].ToString()) +")";
            }
            tbl_generalpo.DataBind();

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            ForwardPOforApproval();
        }


        protected void tbl_podata_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String isforwarded = e.Row.Cells[12].Text;
                if (isforwarded.Trim() != "0")
                {
                    e.Row.BackColor = Color.Aqua;
                }
            }
        }

        protected void tbl_podata_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ProcurementTransaction ptrans = new ProcurementTransaction();
            int popk = int.Parse(tbl_podata.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {

                ptrans.ApprovePO(popk);
            }
            else if (e.CommandName == "Reject")
            {

                ptrans.DeletePO(popk);
            }
            else if (e.CommandName == "Show")
            {
                String ponum = tbl_podata.Rows[int.Parse(e.CommandArgument.ToString())].Cells[2].Text.Trim();
                Response.Redirect(String.Format("~/Reports/View.aspx?navtype={0}&POnum={1}", "PO", ponum));
            }
        }


        #endregion






        #region SPO
    
        /// <summary>
        /// Approve SPO
        /// </summary>
        public void approveSPO()
        {

            BLL.ProcurementBLL.StockPOMasterdata ptrans = new BLL.ProcurementBLL.StockPOMasterdata();
            for (int i = 0; i < tbl_generalpo.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_generalpo.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_generalpo.Rows[i].Cells[1].Text);
                    ptrans.ApproveSPO(popk);
                }

            }

            tbl_generalpo.DataBind();
        }
        /// <summary>
        /// Forward SPO for Approval in Tablet
        /// </summary>
        public void ForwardSPOforApproval()
        {
            BLL.ProcurementBLL.StockPOMasterdata ptrans = new BLL.ProcurementBLL.StockPOMasterdata();
            for (int i = 0; i < tbl_generalpo.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_generalpo.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_generalpo.Rows[i].Cells[1].Text);
                    ptrans.ForwardforSPOApproval(popk);
                }

            }

            tbl_generalpo.DataBind();
        }

        protected void btn_spoapproval_Click(object sender, EventArgs e)
        {
            approveSPO();
        }

        protected void btn_forwardspo_Click(object sender, EventArgs e)
        {
            ForwardSPOforApproval();
        }
        protected void tbl_generalpo_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
        #endregion




        #region RO

        public void setgridviewRO()
        {
            if (HttpContext.Current.User.Identity.Name == "mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {

                SqlDataSource3.SelectCommand = @"
SELECT        RO_Pk, RONum, FRMATC, TOATC, ISNULL(FRMTEMP, '') + '' + ISNULL(FRMCOMP, '') + '' + ISNULL(FRMCONS, '') + '' + ISNULL(FRMWEIG, '') + '' + ISNULL(FRMITEMCOLOR, '') + '' + ISNULL(FRMSUPPCOLOR, '') 
                         + '' + ISNULL(FRMITEMSIZE, '') + '' + ISNULL(FRMSUPPSIZE, '') AS DESCRIPTION, Qty, Qty * RATE AS POVALUE, UOM, LocationName, LocationAddress, IsForwarded, UserName
FROM            (SELECT        RequestOrderMaster.RONum, AtcMaster.AtcNum AS FRMATC, AtcMaster_1.AtcNum AS TOATC, Template_Master.Description AS TOTEMP, Template_Master_1.Description AS FRMTEMP, RequestOrderDetails.Qty, 
                         SkuRawMaterialMaster.Composition AS FRMCOMP, SkuRawMaterialMaster.Construction AS FRMCONS, SkuRawMaterialMaster.Weight AS FRMWEIG, SkuRawMaterialMaster.Width AS FROMWID, 
                         SkuRawmaterialDetail.ItemColor AS FRMITEMCOLOR, SkuRawmaterialDetail.SupplierColor AS FRMSUPPCOLOR, SkuRawmaterialDetail.ItemSize AS FRMITEMSIZE, 
                         SkuRawmaterialDetail.SupplierSize AS FRMSUPPSIZE, RequestOrderDetails.CUnitPrice AS RATE, UOMMaster.UomName AS UOM, SkuRawMaterialMaster_1.Composition, SkuRawMaterialMaster_1.Construction, 
                         LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, RequestOrderMaster.IsForwarded, RequestOrderMaster.AddedBy, 
                         UserMaster.UserName, UserMaster.Department_PK
FROM            SkuRawmaterialDetail INNER JOIN
                         RequestOrderMaster INNER JOIN
                         RequestOrderDetails ON RequestOrderMaster.RO_Pk = RequestOrderDetails.RO_Pk ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderDetails.FromSkuDet_PK INNER JOIN
                         SkuRawmaterialDetail AS SkuRawmaterialDetail_1 ON RequestOrderDetails.ToSkuDet_PK = SkuRawmaterialDetail_1.SkuDet_PK INNER JOIN
                         SkuRawMaterialMaster ON SkuRawmaterialDetail.Sku_PK = SkuRawMaterialMaster.Sku_Pk INNER JOIN
                         SkuRawMaterialMaster AS SkuRawMaterialMaster_1 ON SkuRawmaterialDetail_1.Sku_PK = SkuRawMaterialMaster_1.Sku_Pk INNER JOIN
                         AtcMaster ON SkuRawMaterialMaster.Atc_id = AtcMaster.AtcId INNER JOIN
                         AtcMaster AS AtcMaster_1 ON SkuRawMaterialMaster_1.Atc_id = AtcMaster_1.AtcId INNER JOIN
                         Template_Master ON SkuRawMaterialMaster.Template_pk = Template_Master.Template_PK INNER JOIN
                         Template_Master AS Template_Master_1 ON SkuRawMaterialMaster_1.Template_pk = Template_Master_1.Template_PK INNER JOIN
                         InventoryMaster ON RequestOrderDetails.InventoryItem_PK = InventoryMaster.InventoryItem_PK INNER JOIN
                         LocationMaster ON InventoryMaster.Location_PK = LocationMaster.Location_PK INNER JOIN
                         UOMMaster ON InventoryMaster.Uom_Pk = UOMMaster.Uom_PK INNER JOIN
                         UserMaster ON RequestOrderMaster.AddedBy = UserMaster.UserName
GROUP BY RequestOrderMaster.RONum, RequestOrderMaster.CreatedDate, RequestOrderMaster.AddedBy, AtcMaster.AtcNum, AtcMaster_1.AtcNum, Template_Master.Description, Template_Master_1.Description, 
                         RequestOrderDetails.Qty, SkuRawMaterialMaster.Composition, SkuRawMaterialMaster.Construction, SkuRawMaterialMaster.Weight, SkuRawMaterialMaster.Width, SkuRawmaterialDetail.ItemColor, 
                         SkuRawmaterialDetail.SupplierColor, SkuRawmaterialDetail.ItemSize, SkuRawmaterialDetail.SupplierSize, RequestOrderDetails.CUnitPrice, UOMMaster.UomName, SkuRawMaterialMaster_1.Composition, 
                         SkuRawMaterialMaster_1.Construction, SkuRawMaterialMaster_1.Weight, SkuRawMaterialMaster_1.Width, SkuRawmaterialDetail_1.ItemColor, SkuRawmaterialDetail_1.SupplierColor, 
                         SkuRawmaterialDetail_1.ItemSize, SkuRawmaterialDetail_1.SupplierSize, LocationMaster.LocationName, LocationMaster.LocationAddress, RequestOrderMaster.RO_Pk, RequestOrderMaster.IsApproved, 
                         RequestOrderMaster.IsForwarded, UserMaster.UserName, UserMaster.Department_PK
HAVING        (RequestOrderMaster.IsApproved = N'N') AND (UserMaster.Department_PK = " + Session["Department_PK"].ToString().Trim() + ")) AS TT";




            }
            tbl_ro.DataBind();

        }

        

        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveRO()
        {

            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_ro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_ro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_ro.Rows[i].Cells[1].Text);
                    rmmstr.GetROApproved(popk);
                }

            }

            tbl_ro.DataBind();
        }


        public void ForwardROforApproval()
        {
            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_ro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_ro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_ro.Rows[i].Cells[1].Text);
                    string fromatc = tbl_ro.Rows[i].Cells[3].Text;
                    string toatc = tbl_ro.Rows[i].Cells[4].Text;


                    rmmstr.ForwardROforApproval(popk,fromatc,toatc);
                }

            }

            tbl_ro.DataBind();
        }
        protected void tbl_ro_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int ro_pk = int.Parse(tbl_ro.Rows[int.Parse(e.CommandArgument.ToString())].Cells[1].Text);
            if (e.CommandName == "Approve")
            {
                BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
                rmmstr.GetROApproved(ro_pk);
            }
            else if (e.CommandName == "Reject")
            {
                BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
                rmmstr.GetRODeleted(ro_pk);
            }
            else if (e.CommandName == "Show")
            {

            }
        }
       
        protected void btn_forwardro_Click(object sender, EventArgs e)
        {
            ForwardROforApproval();
        }
        protected void btn_ro_Click(object sender, EventArgs e)
        {
            approveRO();
        }
        #endregion




        #region SRO
        public void setgridviewSRO()
        {
            if (HttpContext.Current.User.Identity.Name == "Mahendra" || HttpContext.Current.User.Identity.Name == "Vijeesh")
            {

                SqlDataSource4.SelectCommand = @"
SELECT        RequestOrderStockMaster.SRO_Pk, RequestOrderStockMaster.RONum, ISNULL(SkuRawMaterialMaster.RMNum, '') + ' ' + ISNULL(SkuRawMaterialMaster.Composition, '') 
                         + ' ' + ISNULL(SkuRawMaterialMaster.Construction, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierColor, '') + ' ' + ISNULL(SkuRawmaterialDetail.SupplierSize, '') AS dESCRIPTION, RequestOrderStockDetails.Qty, 
                         RequestOrderStockDetails.Qty * RequestOrderStockDetails.CUnitPrice AS Povalue, LocationMaster.LocationName + '  GStock ' AS fromLocation, RequestOrderStockDetails.ToSkuDet_PK, 
                         RequestOrderStockMaster.IsForwarded, RequestOrderStockMaster.IsApproved, RequestOrderStockMaster.AddedBy, UserMaster.Department_PK
FROM            SkuRawMaterialMaster INNER JOIN
                         SkuRawmaterialDetail ON SkuRawMaterialMaster.Sku_Pk = SkuRawmaterialDetail.Sku_PK INNER JOIN
                         RequestOrderStockDetails ON SkuRawmaterialDetail.SkuDet_PK = RequestOrderStockDetails.ToSkuDet_PK INNER JOIN
                         StockInventoryMaster ON RequestOrderStockDetails.SInventoryItem_PK = StockInventoryMaster.SInventoryItem_PK INNER JOIN
                         LocationMaster ON StockInventoryMaster.Location_Pk = LocationMaster.Location_PK INNER JOIN
                         RequestOrderStockMaster ON RequestOrderStockDetails.SRO_Pk = RequestOrderStockMaster.SRO_Pk INNER JOIN
                         UserMaster ON RequestOrderStockMaster.AddedBy = UserMaster.UserName
WHERE        (RequestOrderStockMaster.IsApproved = 'N') AND (UserMaster.Department_PK =  " + Session["Department_PK"].ToString().Trim() + ")";




            }
            tbl_sro.DataBind();

        }
        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveSRO()
        {

            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);
                    rmmstr.GetsTOCKROApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardSROforApproval()
        {
            BLL.ProcurementBLL.RequestOrderMasterData rmmstr = new BLL.ProcurementBLL.RequestOrderMasterData();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);


                    //rmmstr.ForwardsTOCKROforApproval(popk);
                    rmmstr.GetsTOCKROApproved(popk);
                }

            }

            tbl_ro.DataBind();
        }







        protected void btn_stockro_Click(object sender, EventArgs e)
        {
            approveSRO();
        }

        protected void btn_forwardsro_Click(object sender, EventArgs e)
        {
            ForwardSROforApproval();
        }

        #endregion












        #region WrongPO




        /// <summary>
        /// Approve WrongPO
        /// </summary>
        public void approveWrongPO()
        {

            BLL.ProcurementBLL.WrongPOActions rmmstr = new BLL.ProcurementBLL.WrongPOActions();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);
                    rmmstr.GetWrongPOApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardWrongPOforApproval()
        {
            BLL.ProcurementBLL.WrongPOActions rmmstr = new BLL.ProcurementBLL.WrongPOActions();
            for (int i = 0; i < tbl_sro.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_sro.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_sro.Rows[i].Cells[1].Text);


                    rmmstr.ForwardWrongPOApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }









        protected void btn_wrongpo_Click(object sender, EventArgs e)
        {
            approveWrongPO();
        }

        protected void btn_forwardwrongpo_Click(object sender, EventArgs e)
        {
            ForwardWrongPOforApproval();
        }


        #endregion


        #region EXTRABOM

        

        /// <summary>
        /// Approve RO
        /// </summary>
        public void approveEBOM()
        {

            BLL.ProcurementBLL.ExtraBOMActions wrngmdata = new BLL.ProcurementBLL.ExtraBOMActions();
            for (int i = 0; i < tbl_extrabom.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_extrabom.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_extrabom.Rows[i].Cells[1].Text);
                    wrngmdata.GetEBOMApproved(popk);
                }

            }

            tbl_sro.DataBind();
        }


        public void ForwardEBOMforApproval()
        {
            BLL.ProcurementBLL.ExtraBOMActions wrngmdata = new BLL.ProcurementBLL.ExtraBOMActions();
            for (int i = 0; i < tbl_extrabom.Rows.Count; i++)
            {
                String chk_isreq = ((tbl_extrabom.Rows[i].FindControl("chk_select") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int popk = int.Parse(tbl_extrabom.Rows[i].Cells[1].Text);


                    wrngmdata.ForwardEBOMApproval(popk);
                }

            }

            tbl_ro.DataBind();
        }

        protected void btn_forwardextrabom_Click(object sender, EventArgs e)
        {
            ForwardEBOMforApproval();
        }
        protected void btn__approveextrabom_Click(object sender, EventArgs e)
        {
            approveEBOM();
        }
        protected void btn__approveextrabom_Click1(object sender, EventArgs e)
        {
            approveEBOM();
        }




        #endregion

        protected void Button4_Click(object sender, EventArgs e)
        {
            BLL.InventoryBLL.InventoryMissingRequestData dodata = new BLL.InventoryBLL.InventoryMissingRequestData();
            for (int i = 0; i < tbl_misPlaced.Rows.Count; i++)
            {


                String chk_isreq = ((tbl_misPlaced.Rows[i].FindControl("chk_select0") as CheckBox).Checked == true ? "Y" : "N");
                if (chk_isreq == "Y")
                {
                    int loanpk = int.Parse(tbl_misPlaced.Rows[i].Cells[1].Text);
                    dodata.GetMissingInventoryApproved(loanpk);
                }

            }
            InventoryMisPlaced.DataBind();
            tbl_misPlaced.DataBind();
        }
    }
}