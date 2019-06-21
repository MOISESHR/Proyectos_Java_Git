using System.Web.Mvc;

namespace TGS.SGV.Web.Areas.TablaGeneral
{
    public class TablaGeneralAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TablaGeneral";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TablaGeneral_default",
                "TablaGeneral/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}