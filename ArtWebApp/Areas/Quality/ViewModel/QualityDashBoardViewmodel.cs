using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Quality.ViewModel
{
    public class QualityDashBoardViewmodel
    {
        public DataTable PendingvalidationData { get; set; }
        public DataTable PendingInspection { get; set; }
        public DataTable PendingGrouping { get; set; }
       
    }
}