using System.Web.Mvc;

namespace ArtWebApp.Areas.ArtAdministrator
{
    public class ArtAdministratorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ArtAdministrator";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ArtAdministrator_default",
                "ArtAdministrator/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}