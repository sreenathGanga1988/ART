using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMaster
{
    public class ArtMVCMasterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ArtMVCMaster";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ArtMVCMaster_default",
                "ArtMVCMaster/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}