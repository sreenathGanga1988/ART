using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Production.CutOrder
{
    public partial class CutOrderDeletion : System.Web.UI.Page
    {
      

            BLL.CutOrderBLL.CutOrderData cdata = null;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    FillAtcCombo();

                }
            }


            public void FillAtcCombo()
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var q = from atcorder in entty.AtcMasters
                            select new
                            {
                                name = atcorder.AtcNum,
                                pk = atcorder.AtcId
                            };

                    // Create a table from the query.

                    var q1 = from order in entty.LocationMasters
                             where order.LocType == "F"
                             select new
                             {
                                 name = order.LocationName,
                                 pk = order.Location_PK
                             };
                    drp_Atc.DataSource = q.ToList();
                    drp_Atc.DataBind();

                    drp_fact.DataSource = q1.ToList();
                    drp_fact.DataTextField = "name";
                    drp_fact.DataValueField = "pk";
                    drp_fact.DataBind();
                    UpdatePanel2.Update();


                }
            }
            public void FillOurStyleCombo(int atcid)
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in entty.AtcDetails
                            where ponmbr.AtcId == atcid
                            select new
                            {
                                name = ponmbr.OurStyle,
                                pk = ponmbr.OurStyleID
                            };

                    drp_ourstyle.DataSource = q.ToList();
                    drp_ourstyle.DataBind();




                }
            }



            public void fillReason()
            {
                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in entty.ExtraRequestReasonMasters

                            select new
                            {
                                name = ponmbr.ExtraReason,
                                pk = ponmbr.ExtraReason_Pk
                            };

                    drp_reason.DataSource = q.ToList();
                    drp_reason.DataTextField = "name";
                    drp_reason.DataValueField = "pk";
                    drp_reason.DataBind();
                }
            }
            public void fillColorcombo()
            {

                cdata = new BLL.CutOrderBLL.CutOrderData();
                ddl_color.DataSource = cdata.GetFabricDescription(int.Parse(drp_Atc.SelectedValue.ToString()));
                ddl_color.DataTextField = "ItemDescription";
                ddl_color.DataValueField = "Skudet_pk";
                ddl_color.DataBind();

            }




            public void clearcontroll()
            {
                drp_Atc.SelectedIndex = -1;
                drp_fact.SelectedIndex = 0;
                drp_cutorderType.SelectedIndex = 0;
                drp_ourstyle.SelectedIndex = 0;
                txt_approvedcon.Text = "0";

                txt_cutQty.Text = "0";
                txt_deliveryQty.Text = "0";
                txt_shrinkage.Text = "0";
                txt_shrinkage.Text = "0";
                txt_fabAllocation.Text = "0";
            }

            /// <summary>
            /// valiadtes the form before saving athe data
            /// </summary>
            /// <returns></returns
            public Boolean validationcontrol()
            {
                lbl_errordisplayer.Text = "";
                Boolean sucess = false;
                if (drp_Atc.SelectedValue.ToString().Trim() == null || drp_Atc.SelectedValue.ToString().Trim() == "")
                {
                    MessageBoxShow("Enter the Atc#");
                }

                else if (drp_Atc.SelectedItem.Text == null || drp_Atc.SelectedItem.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Cut Plan #");
                }
                else if (drp_cutorderType.SelectedItem.Text == null || drp_cutorderType.SelectedItem.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Cut Plan Type ");
                }
                else if (txt_cutQty.Text == null || txt_cutQty.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Cut Oder Quantity ");
                }
                else if (txt_cutablewidth.Text == null || txt_cutablewidth.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Width ");
                }
                else if (txt_shrinkage.Text == null || txt_shrinkage.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Shrinkage ");
                }
                else if (txt_fabAllocation.Text == null || txt_fabAllocation.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Fabric Quantity ");
                }
                else if (txt_approvedcon.Text == null || txt_approvedcon.Text.Trim() == "")
                {
                    MessageBoxShow("Enter the Consumption  Quantity ");
                }
                else
                {
                    sucess = true;
                }
                return sucess;
            }



            public void MessageBoxShow(String mssg)
            {
                lbl_errordisplayer.Text = mssg;

            }

            public void FillAllcutorder(int atcid)
            {


                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in entty.CutOrderMasters
                            where ponmbr.AtcID == atcid
                            select new
                            {
                                name = ponmbr.Cut_NO,

                                //  name=ponmbr.CostingCount,
                                pk = ponmbr.CutID
                            };


                    drp_costingpk.DataSource = q.ToList();
                    drp_costingpk.DataBind();




                }

            }
            public void SaveCutOrder()
            {
                if (validationcontrol())
                {
                    BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();


                    cdata.CuitId = int.Parse(drp_costingpk.SelectedValue.ToString());
                    cdata.Atcid = int.Parse(drp_Atc.SelectedValue.ToString());
                    cdata.Ourstyleid = int.Parse(drp_ourstyle.SelectedValue.ToString());
                    cdata.FabDescription = ddl_color.SelectedItem.Text.Trim();

                    String skudet = ddl_color.SelectedItem.Value.ToString();
                    cdata.Skudet_pk = int.Parse(ddl_color.SelectedValue.ToString());
                    cdata.CutNum = drp_costingpk.SelectedItem.Text.ToString();
                    cdata.Tofactid = int.Parse(drp_fact.SelectedValue.ToString());
                    cdata.CutorderType = drp_cutorderType.SelectedItem.ToString().Trim();
                    cdata.CofabAllocation = Decimal.Parse(txt_fabAllocation.Text);
                    cdata.Cutablewidth = txt_cutablewidth.Text.Trim();
                    cdata.Shrinkage = txt_shrinkage.Text.Trim();
                    if (drp_cutorderType.SelectedItem.Text == "Extra")
                    {
                        cdata.ExtraReason_Pk = int.Parse(drp_reason.SelectedValue.ToString());
                    }
                    else
                    {
                        cdata.ExtraReason_Pk = 6;


                    }
                    cdata.CutOrderQty = decimal.Parse(txt_cutQty.Text);
                    cdata.BalToCutQty = decimal.Parse(txt_baltoCutQty.Text);
                    cdata.DeliveredQty = decimal.Parse(txt_deliveryQty.Text);
                    cdata.ApprovedConsumption = decimal.Parse(txt_approvedcon.Text);
                    string cutno = cdata.UpdateCutorder(cdata);
                    String msg = "Cutorder # : " + cutno + " is Updated Sucessfully";
                    MessgeboxUpdate("sucess", msg);
                    clearcontroll();

                }
            }
            protected void btn_saveCutorder_Click(object sender, EventArgs e)
            {
                SaveCutOrder();


            }

            protected void drp_ourstyle_DataBound(object sender, EventArgs e)
            {

            }

            protected void btn_show_Click(object sender, EventArgs e)
            {
                fillColorcombo();
                FillOurStyleCombo(int.Parse(drp_Atc.SelectedValue.ToString()));
                FillAllcutorder(int.Parse(drp_Atc.SelectedValue.ToString()));
            }

            protected void btn_showColor_Click(object sender, EventArgs e)
            {

            }

            protected void drp_ourstyle_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
            {

            }

            protected void Button1_Click(object sender, EventArgs e)
            {

            }

            protected void drp_cutorderType_SelectionChanged(object sender, Infragistics.Web.UI.ListControls.DropDownSelectionChangedEventArgs e)
            {
            }

            protected void Button1_Click1(object sender, EventArgs e)
            {
                fillcutorderData();
            }

            public void fillcutorderData()
            {
                int cutid = int.Parse(drp_costingpk.SelectedValue.ToString());

                using (ArtEntitiesnew entty = new ArtEntitiesnew())
                {
                    var q = from ponmbr in entty.CutOrderMasters
                            where ponmbr.CutID == cutid
                            select ponmbr;


                    foreach (var element in q)
                    {
                        drp_ourstyle.SelectedValue = element.OurStyleID.ToString();
                        //  ddl_color.SelectedItem.Text = element.Color.ToString();
                        ddl_color.SelectedValue = element.SkuDet_pk.ToString();
                        try
                        {
                            drp_reason.SelectedValue = int.Parse(element.ExtraReason_Pk.ToString()).ToString();
                        }
                        catch (Exception)
                        {


                        }
                        drp_fact.SelectedValue = int.Parse(element.ToLoc.ToString()).ToString();
                        txt_approvedcon.Text = element.ConsumptionQty.ToString();
                        txt_baltoCutQty.Text = element.BalanceQty.ToString();
                        txt_cutablewidth.Text = element.CutWidth.ToString();
                        txt_cutQty.Text = element.CutQty.ToString();
                        txt_deliveryQty.Text = element.DelivedQty.ToString();
                        drp_cutorderType.SelectedItem.Text = element.CutOrderType.ToString();
                        txt_fabAllocation.Text = element.FabQty.ToString();
                        txt_shrinkage.Text = element.Shrinkage.ToString();

                    }




                }

            }

            protected void drp_reason_SelectedIndexChanged(object sender, EventArgs e)
            {

            }

            protected void txt_fabAllocation_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    txt_approvedcon.Text = calculateconsumption().ToString();
                }
                catch (Exception)
                {


                }
            }

            protected void txt_cutQty_TextChanged(object sender, EventArgs e)
            {
                try
                {
                    txt_approvedcon.Text = calculateconsumption().ToString();
                }
                catch (Exception)
                {


                }
            }


            public float calculateconsumption()
            {

                float fabqty = float.Parse(txt_fabAllocation.Text);
                float cutqty = float.Parse(txt_cutQty.Text);

                float consumption = fabqty / cutqty;

                return consumption;

            }

            public void MessgeboxUpdate(String Messagetype, String Messg)
            {
                if (Messagetype == "sucess")
                {
                    Messaediv.Attributes["class"] = "success";
                    Messaediv.InnerText = Messg;
                }
                else
                {
                    Messaediv.Attributes["class"] = "error-message";
                    Messaediv.InnerText = Messg;
                }
            }

            protected void drp_cutorderType_SelectedIndexChanged(object sender, EventArgs e)
            {

                try
                {
                    if (drp_cutorderType.SelectedItem.Text == "Extra")
                    {
                        fillReason();
                    }
                    else
                    {


                        drp_reason.Items.Clear();
                        drp_reason.DataSource = null;

                        drp_reason.DataBind();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

            protected void btn_deletecutorder_Click(object sender, EventArgs e)
            {

                BLL.CutOrderBLL.CutOrderData cdata = new BLL.CutOrderBLL.CutOrderData();

                if (cdata.IsCutOrderDOMade(int.Parse(drp_costingpk.SelectedValue.ToString())))
                {

                    String msg = "Cannot Delete CutOrder as DO Made ";
                    MessgeboxUpdate("error", msg);
                }
                else
                {
                    cdata.DeleteCutOrder(int.Parse(drp_costingpk.SelectedValue.ToString()));
                    String msg = "Cutorder # " + drp_costingpk.SelectedItem.Text + " Deleted  Sucessfully";
                    MessgeboxUpdate("sucess", msg);
                }


            }

        }
}