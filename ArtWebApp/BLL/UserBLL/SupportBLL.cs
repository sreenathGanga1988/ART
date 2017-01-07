using ArtWebApp.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.BLL.UserBLL
{
    public class SupportBLL
    {
       
    }


    public class SupportTicketBLL
    {

        public int Support_pk { get; set; }
        public string Supportnum { get; set; }
        public string SupportTittle { get; set; }
        public string SupportDescription { get; set; }
        public string Priority { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public int Location_pk { get; set; }
        public string Status { get; set; }
        public string IsCompleted { get; set; }
        public DateTime CompletedDate { get; set; }




        public void supportClose(int support_pk)
        {
            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {


                var q = from sup in enty.SupportTickets
                        where sup.Support_pk == support_pk
                        select sup;

                foreach(var elemt in q)
                {
                    elemt.IsCompleted = "Y";
                    elemt.CompletedDate = DateTime.Now;
                }

                enty.SaveChanges();
            }
        }

        public String InsertSupportticket(SupportTicketBLL sptcketlbll)
        {

            using (ArtEntitiesnew enty = new ArtEntitiesnew())
            {
                SupportTicket sptckt = new SupportTicket();
                sptckt.SupportDescription = sptcketlbll.SupportDescription;
                sptckt.SupportTittle = sptcketlbll.SupportTittle;
                sptckt.Status = sptcketlbll.Status;
                sptckt.AddedBy = sptcketlbll.AddedBy;
                sptckt.AddedDate = DateTime.Now;
                sptckt.Priority = sptcketlbll.Priority;
                sptckt.Location_pk = sptcketlbll.Location_pk;
                sptckt.IsCompleted = sptcketlbll.IsCompleted;

                enty.SupportTickets.Add(sptckt);
                enty.SaveChanges();
                sptcketlbll.Supportnum = sptckt.Supportnum = CodeGenerator.GetUniqueCode("Support", HttpContext.Current.Session["lOC_Code"].ToString().Trim(), int.Parse(sptckt.Support_pk.ToString()));
                enty.SaveChanges();
            }


            return sptcketlbll.Supportnum;
        }
    }
}