using System.Web.Mvc;

namespace TGS.SGV.Web.Areas.Solicitud
{
    public class SolicitudAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Solicitud";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Solicitud_default",
                "Solicitud/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}