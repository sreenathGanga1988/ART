using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCGPO
{
    public class ArtMVCGPOAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ArtMVCGPO";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ArtMVCGPO_default",
                "ArtMVCGPO/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}