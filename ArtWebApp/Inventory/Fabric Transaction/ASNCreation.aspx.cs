using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ArtWebApp.Inventory.Fabric_Transaction
{
    public partial class ASNCreation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_savercpt_Click(object sender, EventArgs e)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                if (!enty.SupplierDocumentMasters.Any(f => f.SupplierDocnum.Trim() == txt_doc.Text.Trim()))
                {

                    SupplierDocumentMaster sdcmstre = new SupplierDocumentMaster();
                    sdcmstre.SupplierDocnum = txt_doc.Text.Trim();
                    sdcmstre.Containernum = txt_container.Text.Trim();
                    sdcmstre.SupplierETA = DateTime.Parse(dtp_deliverydate.Value.ToString());
                    sdcmstre.Remark = txta_remark.Value.ToString();
                    sdcmstre.Supplier_pk = int.Parse(drp_supplier.SelectedValue.ToString ());
                    sdcmstre.AddedDate = DateTime.Parse(DateTime.Now.ToString());
                    sdcmstre.AddedBy = Session["Username"].ToString().Trim();

                    enty.SupplierDocumentMasters.Add(sdcmstre);
                    enty.SaveChanges();
                    sdcmstre.AtracotrackingNum = CodeGenerator.GetUniqueCode("ASN", "", int.Parse(sdcmstre.SupplierDoc_pk.ToString()));
                    enty.SaveChanges();
                    MessgeboxUpdate("sucess", "Supplier Invoice  Updated against " + sdcmstre.AtracotrackingNum);
                }
                else
                {

                    string message = "alert('Invoice Number Already Present')";
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", message, true);

                   
                }
            }
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

    }
}