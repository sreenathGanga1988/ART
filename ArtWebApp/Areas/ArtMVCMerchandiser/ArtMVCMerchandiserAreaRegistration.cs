using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtMVCMerchandiser
{
    public class ArtMVCMerchandiserAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ArtMVCMerchandiser";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ArtMVCMerchandiser_default",
                "ArtMVCMerchandiser/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}