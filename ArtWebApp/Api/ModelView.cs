using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArtWebApp.Api
{
    public class ModelView
    {
    }
    public class ArtTaskModel
    {
        public string TaskName { get; set; }
        public string OrangeStatus { get; set; }
        public string LightColorStatus { get; set; }
       
    }
    public class ArtTaskModelLoccation
    {
        public string TaskName { get; set; }
        public string OrangeStatus { get; set; }
        public string LightColorStatus { get; set; }
        public string GreenStatus { get; set; }
        public  int Location_pk { get; set; }
        public int OurStyleID { get; set; }

    }
    public class TaskModel
    {
        public string Rollnum { get; set; }
        public string TaskTittle { get; set; }
        public string Location { get; set; }
        public string Pending { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
    }
}