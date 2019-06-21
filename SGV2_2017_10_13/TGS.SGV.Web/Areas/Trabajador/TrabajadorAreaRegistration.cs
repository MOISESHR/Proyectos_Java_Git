using System.Web.Mvc;

namespace TGS.SGV.Web.Areas.Trabajador
{
    public class TrabajadorAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Trabajador";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Trabajador_default",
                "Trabajador/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}