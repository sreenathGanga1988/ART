using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ArtWebApp.DataModels;
using ArtWebApp.BLL.ShippingBLL;

namespace ArtWebApp.BLL.ShippingBLL
{
    public class CourierTableData
    {
        public int CourierID { get; set; }
        public DateTime CourierDate { get; set; }
        public string TypeofSample { get; set; }
        public string Buyer { get; set; }
        public string AtcOrStyle { get; set; }
        public string Quantity { get; set; }
        public string Sender { get; set; }
        public string Reciever { get; set; }
        public string Destination { get; set; }

        public string AWBnum { get; set; }
        public string ISourAccount { get; set; }
        public string OtherAccount { get; set; }
        public string IsnormalCourier { get; set; }
        public string Weight { get; set; }
        public string ApprCost { get; set; }
        public string Remark { get; set; }
        public string Countryname{ get; set; }
        public string Couriertype { get; set; }
        public DateTime Addeddate { get; set; }


        public string InsertCourierData()
        {
            String Donum = "";
            using (DataModels.ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {

                CourierTable courierde = new CourierTable();
                courierde.CourierDate = this.CourierDate;
                courierde.TypeofSample = this.TypeofSample;
                courierde.Buyer = this.Buyer;
                courierde.AtcOrStyle = this.AtcOrStyle;
                courierde.Quantity = this.Quantity;
                courierde.Sender = this.Sender;
                courierde.Reciever = this.Reciever;
                courierde.Destination = this.Destination;
                courierde.AWBnum = this.AWBnum;
                courierde.ISourAccount = this.ISourAccount;
                courierde.OtherAccount = this.OtherAccount;
                courierde.IsnormalCourier = this.IsnormalCourier;
                courierde.Weight = this.Weight;
                courierde.ApprCost = decimal.Parse(this.ApprCost);
                courierde.Remark = this.Remark;
                courierde.Addeddate = this.Addeddate;
                courierde.Country = this.Countryname;
                courierde.Couriertype = this.Couriertype;
                courierde.AddedBY= HttpContext.Current.Session["Username"].ToString();
                enty.CourierTables.Add(courierde);
                enty.SaveChanges();
                Donum = courierde.CouDocNum= CodeGenerator.GetUniqueCode("COU", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(courierde.CourierID.ToString()));
                enty.SaveChanges();
            }
            return Donum;
        }
    }
}
        

       
    
    