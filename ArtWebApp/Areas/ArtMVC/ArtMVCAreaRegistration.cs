using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVC
{
    public class ArtMVCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ArtMVC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
          
            context.MapRoute(
                "ArtMVC_default",
                "ArtMVC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}