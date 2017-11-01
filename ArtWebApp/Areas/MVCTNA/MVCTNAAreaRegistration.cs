using System.Web.Mvc;

namespace ArtWebApp.Areas.MVCTNA
{
    public class MVCTNAAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MVCTNA";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MVCTNA_default",
                "MVCTNA/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}