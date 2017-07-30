using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using ArtWebApp.DataModels;
using ArtWebApp.DBTransaction;
namespace ArtWebApp.BLL
{
    public class AtcData
    {
        AtcTransaction atran = null;
        
        public AtcData()
        {
            atran = new AtcTransaction();
        }

        int atcid = 0;

        public int Atcid
        {
            get { return atcid; }
            set { atcid = value; }
        }
        String AtcNum = "";

        public String AtcNum1
        {
            get { return AtcNum; }
            set { AtcNum = value; }
        }

       
        int buyerid = 0;

        public int Buyerid
        {
            get { return buyerid; }
            set { buyerid = value; }
        }
        int countryid = 0;

        public int Countryid
        {
            get { return countryid; }
            set { countryid = value; }
        }
        String merchandisername = "";

        public String Merchandisername
        {
            get { return merchandisername; }
            set { merchandisername = value; }
        }

      
        int noofstyles = 0;

        public int Noofstyles
        {
            get { return noofstyles; }
            set { noofstyles = value; }
        }
        DateTime inHouseDate;

        public DateTime InHouseDate
        {
            get { return inHouseDate; }
            set { inHouseDate = value; }
        }
        DateTime ShipDate;

        public DateTime ShipDate1
        {
            get { return ShipDate; }
            set { ShipDate = value; }
        }
        DateTime Finshdate;

        public DateTime Finshdate1
        {
            get { return Finshdate; }
            set { Finshdate = value; }
        }

        String iscompleted = "N";

        public String Iscompleted
        {
            get { return iscompleted; }
            set { iscompleted = value; }
        }
        String isclosed = "N";

        public String Isclosed
        {
            get { return isclosed; }
            set { isclosed = value; }
        }

        public Decimal Qty { get; set; }
        private DataTable ourstyledata;

        public DataTable Ourstyledata
        {
            get { return getourstyledetail(Atcid); }
            set {  ourstyledata = value; }
        }




        public DataTable getourstyledetail(int atcid)
        {
            DataTable dt = new DataTable();
            AtcTransaction atctrans = new AtcTransaction();
          dt=atctrans.GetOurStyleDetails(atcid);
          return dt;
        }

        public void createOurStyle(AtcData atcdata)
        {
            DataTable dt = CreateOurStyleDataTable(atcdata.Noofstyles, atcdata.AtcNum, atcdata.atcid);

          atran. insertourstyle(dt);
        }

        public DataTable CreateOurStyleDataTable(int totalstylenum, String AtcNum, int atcid)
        {
            String ourstyle = "";
            DataTable table = new DataTable();
            table.Columns.Add("OurstyleID", typeof(int));
            table.Columns.Add("atcid", typeof(int));

            table.Columns.Add("OurStyle", typeof(string));
            table.Columns.Add("BuyerStyle", typeof(string));
            table.Columns.Add("Qty", typeof(float));
            table.Columns.Add("Fob", typeof(float));
            table.Columns.Add("Catid", typeof(float));
            for (int i = 1; i < totalstylenum + 1; i++)
            {
                if (i < 9)
                {
                    ourstyle = "0" + i.ToString() + AtcNum;
                }
                else
                {
                    ourstyle = i.ToString() + AtcNum;
                }
                table.Rows.Add(i, atcid, ourstyle, "Edit ", 0.0, 0.0, 0.0);
            }
            return table;
        }

        
        public void UpdatemasterData(AtcData atcdata)
        {
           
            atran.UpdatemasterData(atcdata);
        }




        public void AtcForpproval()
        {



            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                AtcApproval atcdetapp = new AtcApproval();
                atcdetapp.AtcId = this.atcid;
                atcdetapp.Quantity = this.Qty;
                //  atcdetapp.IsForwarded = "Y";
                atcdetapp.AddedBY = HttpContext.Current.Session["Username"].ToString().Trim(); 
                atcdetapp.ForwardedBY = "N";
                atcdetapp.AddedDate = DateTime.Now;
                atcdetapp.IsApproved = "N";
                atcdetapp.IsForwarded = "N";

                entty.AtcApprovals.Add(atcdetapp);
                entty.SaveChanges();

            }
        }



    }

    public class ourstyleData
    {
        int ourstyleid = 0;

        public int Ourstyleid
        {
            get { return ourstyleid; }
            set { ourstyleid = value; }
        }
        int atcid = 0;

        public int Atcid
        {
            get { return atcid; }
            set { atcid = value; }
        }
        string ourstylenum = "";

        public string Ourstylenum
        {
            get { return ourstylenum; }
            set { ourstylenum = value; }
        }
        decimal fob = 0;

        public decimal Fob
        {
            get { return fob; }
            set { fob = value; }
        }

       
        float catid = 0;

        public float Catid
        {
            get { return catid; }
            set { catid = value; }
        }
        string BuyerStyle = "";

        public string BuyerStyle1
        {
            get { return BuyerStyle; }
            set { BuyerStyle = value; }
        }


        float Qty = 0;

        public float Qty1
        {
            get { return Qty; }
            set { Qty = value; }
        }






        public String AddedBY { get; set; }
        public String ForwardedBY { get; set; }


        public Decimal ProjectionQty { get; set; }



        public void OurStyleForpproval()
        {



            using (ArtEntitiesnew entty = new ArtEntitiesnew())
            {
                var q = from atcdet in entty.AtcDetailApprovals
                        where atcdet.OurStyleID == this.ourstyleid &&
                        atcdet.IsApproved == "N"
                        select atcdet;
                foreach(var element in q)
                {
                    entty.AtcDetailApprovals.Remove(element);
                 
                }
                entty.SaveChanges();
                AtcDetailApproval atcdetapp = new AtcDetailApproval();
                atcdetapp.OurStyleID = this.ourstyleid;
                atcdetapp.Quantity = this.ProjectionQty;
              //  atcdetapp.IsForwarded = "Y";
                atcdetapp.AddedBY = this.AddedBY;
                atcdetapp.ForwardedBY = this.ForwardedBY;
                atcdetapp.AddedDate = DateTime.Now;
                atcdetapp.IsApproved = "N";
                atcdetapp.IsForwarded = "N";

                entty.AtcDetailApprovals.Add(atcdetapp);
                entty.SaveChanges();

            }
        }




        public void ForwardOurStyleforApproval(int AtcDetailApproval_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from rmmstr in enty.AtcDetailApprovals
                        where rmmstr.OurStyleApproval_PK == AtcDetailApproval_pk
                        select rmmstr;

                foreach (var element in q)
                {
                    element.IsForwarded = "Y";
                    element.ForwardedBY = HttpContext.Current.Session["Username"].ToString().Trim();

                }

                enty.SaveChanges();

            }
        }


        public Boolean UpdateOurStyle(ourstyleData ourstl)
        {
            Boolean ischanged = false;

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {

                var q = from ourstyledata in enty.AtcDetails
                        where ourstyledata.OurStyleID == ourstyleid
                        select ourstyledata;

                foreach (var element in q)
                {
                    decimal oldfob = decimal.Parse(element.FOB.ToString());

                    element.FOB = ourstl.Fob;
                    element.CategoryID = decimal.Parse(ourstl.Catid.ToString());
                    element.BuyerStyle = ourstl.BuyerStyle1.Trim ();

                    if(oldfob== ourstl.Fob)
                    {
                        ischanged = false;
                    }else
                    {
                        ischanged = true;
                    }
                }
                enty.SaveChanges();


            }
            return ischanged;
        }




        public void markcostigObsolute()
        {

        }

       
    }

 


   
}