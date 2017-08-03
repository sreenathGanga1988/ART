using System.Web.Mvc;

namespace ArtWebApp.Areas.CuttingMVC
{
    public class CuttingMVCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CuttingMVC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CuttingMVC_default",
                "CuttingMVC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}