using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ArtWebApp.Areas.Accounts.ViewModel
{
    public class AccountdashBoard
    {
        public DataTable PendingGeneralPoforPayable { get; set; }
        public DataTable PendingPOforPayable { get; set; }
        public DataTable PendingservicePoforPosting { get; set; }
        public DataTable PendingLocalExternalSalesforPosting { get; set; }

        public DataTable PendingInternalSalesForDebiting { get; set; }
        public DataTable PendingDebitnoteForPosting { get; set; }
        
    }
}