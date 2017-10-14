using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ArtWebApp.Plugin
{
    
    public static class ClsEmailer
    {

        

        public static void CutPlanApproved (int cutplanpk)
        {
            int locationpk = 0;
            String Locationname = "";
            String Ourstyle = "";
            String color = "";
            String Shrinkage = "";
            string cutplannum = "";
            String Approvedby = "";
            String approvedtime = "";
            using (ArtEntitiesnew enty = new DataModels.ArtEntitiesnew())
            {
              
                var cutplanmaster = from cutplnmstr in enty.CutPlanMasters
                                    where cutplnmstr.CutPlan_PK == cutplanpk
                                    select cutplnmstr;
                foreach(var element in cutplanmaster)
                {
                    locationpk = int.Parse( element.Location_PK.ToString ());

                    Locationname = element.LocationMaster.LocationName;
                    Ourstyle = element.AtcDetail.OurStyle;
                    color = element.FabDescription;
                    Shrinkage = element.ShrinkageGroup;
                     cutplannum = element.CutPlanNUM;
                    Approvedby = element.ApprovedBy;
                    approvedtime = element.ApprovedDate.ToString();
                }





            }


            String subject = Ourstyle + " / "  + color + " / " + Shrinkage + " / " + cutplannum +" / "  +  Locationname ;
           


            String body = "Dear Master, " + Environment.NewLine;
            body+= "  Pls arrange above marker";

            body += Environment.NewLine+ Environment.NewLine + " Approved By :"+Approvedby;
            body += Environment.NewLine + " Approved Time :" + approvedtime;       
            email23(subject, body, "capacitychart@atraco.ae", "Zohair", "Booking Notification ");
            MailingConcernedFactory(subject, body, factory);
        }



        public static void MailingConcernedFactory(String subject, String body, String factory)
        {

           
            CourierDataDataContext courdatacontext = new CourierDataDataContext(Program.ConnStr);
            var q = from user in courdatacontext.EmailGroup_tbls
                    where user.Factory_Name == factory
                    select new
                    {

                        email = user.EmailGroup

                    };


            foreach (var v in q)
            {

                email23(subject, body, v.email.ToString(), factory, "Booking Notification ");

            }
        }



        //public static void approvalorrejectnotifier(String touserName, String Buyer, String factory, String Year, String Month, String qty, String ourstyle, String actiontype)
        //{
        //    String subject = "Capacity Booking  " + actiontype;

        //    String body = " Capacity Booking for  " + Buyer.Trim() + " ,Style Number  " + ourstyle.Trim() + " Booked under factory  " + factory.Trim() + " for the month of  " + Year.Trim() + "-" + Month.Trim() + " is  " + actiontype.Trim() + "  by " + Program.uername;
        //    int balance = getbalancebyfactoryname(factory, int.Parse(Year), Month);
        //    String Capacityavailable = Environment.NewLine + "The Balance Capacity available of this Location is " + balance.ToString(); ;
        //    body = body + Capacityavailable;


        //    //   email23(subject, body, emailid, "User");
        //    email23(subject, body, "capacitychart@atraco.ae", "User", "Booking Notification");
        //    MailingConcernedFactory(subject, body, factory);
        //}





        //public static void DayClosedMail(String factoryname, String Factid, String DateofClosing)
        //{
        //    String subject = "Production Entry Closed For " + factoryname + "  for Date " + DateofClosing;

        //    String body = "Production Entry Closed For " + factoryname + "  for Date " + DateofClosing + "  by " + Program.uername;
        //    int balance = GetProductionoffactoryofDate(Factid, DateofClosing);
        //    String Capacityavailable = Environment.NewLine + "The ProductionQty  is " + balance.ToString(); ;
        //    body = body + Capacityavailable;



        //    email23(subject, body, "rajith@ashton-apparel.com", "User", "Production Notification");
        //    email23(subject, body, "santhosh_Ramakrishna@atraco.ae", "User", "Production Notification");

        //    //    MailingConcernedFactory(subject, body, factory);
        //}
        //public static void factorychangingNotifier(String Year, String Month, String ourstyle, String Atcnum, String Oldfactory, String newFactory)
        //{
        //    String subject = "Production Factory Changed for    " + Atcnum;

        //    String body = "Production Factory Changed for    " + Atcnum + " ,Style Number  " + ourstyle.Trim() + " Booked under factory  " + Oldfactory.Trim() + " for the month of  " + Year.Trim() + "-" + Month.Trim() + " to  " + newFactory.Trim() + "  by " + Program.uername;
        //    int balance = getbalancebyfactoryname(newFactory, int.Parse(Year), Month);
        //    String Capacityavailable = Environment.NewLine + "The Balance Capacity available of this Location is " + balance.ToString(); ;
        //    body = body + Capacityavailable;
        //    email23(subject, body, "capacitychart@atraco.ae", "User", "Booking Notification ");

        //}





        public static void email23(String subject, String body, String toadress, String toname, String Displayname)
        {
            var fromAddress = new MailAddress("atracogen@gmail.com", Displayname);
            var toAddress = new MailAddress(toadress, toname);
            const string fromPassword = "8812686ba";
            //const string subject = "Subject";
            //const string body = "Body";

            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword),
               
            };

    
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
               
                    smtp.Send(message);
               
            }
        }



//        public static int getbalancebyfactoryname(String factoryname, int Year, String Month)
//        {
//            int balance = 0;
//            DataTable dt = new DataTable();

//            using (SqlConnection con = new SqlConnection(Program.ConnStr))
//            {
//                con.Open();


//                SqlCommand cmd = new SqlCommand(@"SELECT  ( MonthlyCapacity  -( isnull((SELECT SUM(BookQty) AS Expr1
//         FROM      OrderBooking_tbl  WHERE (ISApproved = N'N') AND (Year = @YEAR) AND (Month = @month) AND (Factory_Id = fm.Factory_ID )),0)   + isnull( (SELECT SUM(ConsumptionQty) AS Expr1
//         FROM      FinalBooking_master   WHERE (Year = @year) AND (Month = @month) AND (Factory_Id = fm.Factory_ID )),0))) as Balance
//FROM            Factory_Master fM
//WHERE        (Factory_name = @factoryname)", con);



//                cmd.Parameters.AddWithValue("@year", Year);
//                cmd.Parameters.AddWithValue("@month", Month);
//                cmd.Parameters.AddWithValue("@factoryname", factoryname);
//                SqlDataReader rdr = cmd.ExecuteReader();

//                dt.Load(rdr);


//                if (dt != null)
//                {
//                    if (dt.Rows.Count != 0)
//                    {
//                        if (dt.Rows[0][0].ToString().Trim() == "")
//                        {
//                            balance = 0;
//                        }
//                        else
//                        {
//                            balance = int.Parse(dt.Rows[0][0].ToString());
//                        }
//                    }
//                }


//            }
//            return balance;

//        }

//        public static int GetProductionoffactoryofDate(String factid, String Date)
//        {
//            int balance = 0;
//            DataTable dt = new DataTable();

//            using (SqlConnection con = new SqlConnection(Program.ConnStr))
//            {
//                con.Open();


//                SqlCommand cmd = new SqlCommand(@"SELECT     ISNULL(SUM(ProducedQty), 0) AS producedQty
//FROM            ActualProduced_tbl
//WHERE        (factid = @factid) AND (DateOfProd = @closedate)", con);



//                cmd.Parameters.AddWithValue("@factid", int.Parse(factid.ToString()));
//                cmd.Parameters.AddWithValue("@closedate", DateTime.Parse(Date));

//                SqlDataReader rdr = cmd.ExecuteReader();

//                dt.Load(rdr);


//                if (dt != null)
//                {
//                    if (dt.Rows.Count != 0)
//                    {
//                        if (dt.Rows[0][0].ToString().Trim() == "")
//                        {
//                            balance = 0;
//                        }
//                        else
//                        {
//                            balance = int.Parse(dt.Rows[0][0].ToString());
//                        }
//                    }
//                }


//            }
//            return balance;

//        }


//        ///load the comboboxes at the load event of the form


//        public static void LastCloseDateofFactory()
//        {
//            DateTime today = DateTime.Now.Date;
//            DataTable dt = new DataTable();

//            String Datepend = "";
//            using (SqlConnection con = new SqlConnection(Program.ConnStr))
//            {
//                con.Open();
//                SqlCommand cmd = new SqlCommand();
//                cmd.Connection = con;


//                String q3 = @"SELECT        MAX(ProductionDayClose_tbl.DateofProd) AS Closedate, Factory_Master.Factory_name
//FROM            ProductionDayClose_tbl INNER JOIN
//                         Factory_Master ON ProductionDayClose_tbl.factid = Factory_Master.Factory_ID
//GROUP BY ProductionDayClose_tbl.factid, Factory_Master.Factory_name";



//                //  cmd.CommandText = Query1;
//                cmd.CommandText = q3;

//                SqlDataReader rdr = cmd.ExecuteReader();

//                dt.Load(rdr);

//                if (dt != null)
//                {
//                    if (dt.Rows.Count > 0)
//                    {
//                        for (int i = 0; i < dt.Rows.Count; i++)
//                        {
//                            DateTime closeddate = DateTime.Parse(dt.Rows[i]["Closedate"].ToString()).Date;

//                            double daysUntilChristmas = today.Subtract(closeddate).TotalDays;

//                            if (daysUntilChristmas > 1)
//                            {
//                                String factoryname = dt.Rows[i]["Factory_name"].ToString();
//                                for (DateTime j = closeddate.AddDays(1); j < today; j = j.AddDays(1))
//                                {
//                                    if (Datepend == "")
//                                    {
//                                        Datepend = Datepend + j.ToString();
//                                    }
//                                    else
//                                    {
//                                        Datepend = Datepend + " ****** " + j.ToString();
//                                    }

//                                }

//                                Dayspendingtoclose(factoryname, Datepend, closeddate.ToString());
//                            }
//                        }
//                    }
//                }

//            }

//        }
//        public static void Dayspendingtoclose(String factoryname, String DateforClosing, String DayClosed)
//        {
//            String subject = "Production Entry Day Close Pending For " + factoryname + "  for Date " + DateforClosing;

//            String body = "Production Entry Day Close Pending For " + factoryname + "  for Date " + DateforClosing + "  Last day Closed was " + DayClosed;


//            if (factoryname == "AA2")
//            {

//            }
//            else if (factoryname == "MA2")
//            {

//            }
//            else if (factoryname == "MA3")
//            {

//            }
//            else if (factoryname == "MA1")
//            {

//            }

//            email23(subject, body, "sreenath_ganga@atraco.ae", "User", "Production Notification");
//            email23(subject, body, "santhosh_Ramakrishna@atraco.ae", "User", "Production Notification");
  // MailingConcernedFactory(subject, body, factory);
//        }







    }
}
}