using System.Web;
using System.Web.Optimization;

namespace TGS.SGV.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

           #region Configuraciones
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                        "~/Scripts/jquery/jquery-{version}.js",
                         "~/Scripts/jquery/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr/js").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                      "~/Scripts/js/bootstrap.min.js", 
                      "~/Scripts/js/hoverIntent.js",
                      "~/Scripts/js/superfish.js"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/main/js").Include(                      
                       "~/Scripts/js/wow.js",
                       "~/Scripts/js/sticky.js",
                       "~/Scripts/js/easing.js",
                       "~/Scripts/js/main.js",
                       "~/Scripts/js/summernote.js", 
                       "~/Scripts/js/custom.min.js",
                       "~/Scripts/js/bootstrap-filestyle.js"));      

            bundles.Add(new StyleBundle("~/Content/master/estilos").Include(                  
                  "~/Content/css/bootstrap.css",
                  "~/Content/css/font-awesome.css",
                  "~/Content/css/animate.min.css",
                 "~/Content/css/jquery.auto-complete.css",
                 "~/Content/css/summernote.min.css",
                   "~/Content/css/bootstrap-additions.min.css", 
                   "~/Content/Site.css"
                  ));

            
            bundles.Add(new ScriptBundle("~/bundles/angular/js").Include(
                "~/Scripts/angular/angular.js",
                "~/Scripts/angular/angular-route.js",
                "~/Scripts/angular/angular-animate.js",
                "~/Scripts/angular/angular-block-ui.js",
                "~/Scripts/angular/angular-sanitize.js",
                "~/Scripts/angular/angular-summernote.min.js" 

            ));
            bundles.Add(new ScriptBundle("~/bundles/angularjs/js").Include(
                    "~/Scripts/angular/angular.js"

                 ));
            bundles.Add(new ScriptBundle("~/bundles/angularBlocKUi/js").Include(
                 "~/Scripts/angular/angular-block-ui.min.js"
               ));
                        
            bundles.Add(new ScriptBundle("~/bundles/app/base/js").Include(
                    "~/Scripts/app/Base/base.module.js",
                    "~/Scripts/app/Base/base.constantes.js",
                    "~/Scripts/app/Base/utils.factory.js",
                    "~/Scripts/app/Base/Controles/js/datepicker.directive.js",
                    "~/Scripts/app/Base/Controles/js/angular-strap.compat.js",
                    "~/Scripts/app/Base/Controles/js/angular-strap.js",
                    "~/Scripts/app/Base/Controles/js/angular-strap.tpl.js",
                    "~/Scripts/app/Base/Controles/js/alert.js",
                    "~/Scripts/app/Base/Controles/js/uploadFile.directive.js",
                    "~/Scripts/app/Base/Controles/js/tituloDatosObligatorios.directive.js"
                    , "~/Scripts/app/Base/Controles/js/tooltip.js",
                    "~/Scripts/app/Base/Controles/js/parse-options.js",
                    "~/Scripts/app/Base/Controles/js/typeahead.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/Assets/js").Include(
                   "~/Scripts/Assets/bootstrap-datepicker/moment.js",
                   "~/Scripts/Assets/bootstrap-datepicker/es.js",
                   "~/Scripts/Assets/bootstrap-datepicker/bootstrap-datetimepicker.js",
                   "~/Scripts/Assets/bootstrap-datatables/jquery.dataTables.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.util.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.options.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.instances.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.factory.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.renderer.js",
                   "~/Scripts/Assets/bootstrap-datatables/angular-datatables.directive.js",
                   "~/Scripts/Assets/bootstrap-datatables/dataTables.responsive.js"
                 ));

           
            bundles.Add(new StyleBundle("~/bundles/Assets/css").Include(
                   "~/Content/Assets/bootstrap-datepicker/bootstrap-datetimepicker.css",
                  "~/Content/Assets/bootstrap-datatables/jquery.dataTables.css",
                  "~/Content/Assets/bootstrap-datatables/angular-datatables.css",
                  "~/Content/Assets/bootstrap-datatables/dataTables.bootstrap.css",   
                  "~/Content/Assets/bootstrap-datatables/responsive.dataTables.css",
                   "~/Content/angular-block-ui.min.css"
                  ));
            #endregion

            #region Modulo Solicitud

            bundles.Add(new ScriptBundle("~/bundles/app/Solicitud/Bandeja/js").Include(
                         "~/Scripts/app/Solicitud/Solicitud.module.js",
                         "~/Scripts/app/Solicitud/Solicitud.service.js", 
                         "~/Scripts/app/Solicitud/BandejaSolicitud.controller.js",
                         "~/Scripts/app/Solicitud/ComboVacacional.controller.js",
                         "~/Scripts/app/Solicitud/RegistrarSolicitud.controller.js"
                       ));
            bundles.Add(new ScriptBundle("~/bundles/app/Solicitud/RegistrarSolicitud/js").Include(
                             "~/Scripts/app/Solicitud/Solicitud.module.js",
                             "~/Scripts/app/Solicitud/Solicitud.service.js",
                             "~/Scripts/app/Solicitud/RegistrarSolicitud.controller.js"
                           ));

            bundles.Add(new ScriptBundle("~/bundles/app/Solicitud/ComboVacacional/js").Include(
                             "~/Scripts/app/Solicitud/Solicitud.module.js",
                             "~/Scripts/app/Solicitud/Solicitud.service.js",
                             "~/Scripts/app/Solicitud/ComboVacacional.controller.js"
                           ));

            #endregion

            #region Modulo Programacion

            bundles.Add(new ScriptBundle("~/bundles/app/Programacion/BandejaProgramacion/js").Include(
                 "~/Scripts/app/Programacion/Programacion.module.js",
                 "~/Scripts/app/Programacion/Programacion.service.js",
                 "~/Scripts/app/Programacion/BandejaProgramacion.controller.js",
                 "~/Scripts/app/Programacion/GoceVacacional.controller.js",
                 "~/Scripts/app/Programacion/EditarGoce.controller.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/app/Programacion/GoceVacacional/js").Include(
                 "~/Scripts/app/Programacion/Programacion.module.js",
                 "~/Scripts/app/Programacion/Programacion.service.js",
                 "~/Scripts/app/Programacion/GoceVacacional.controller.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/app/Programacion/EditarGoce/js").Include(
                 "~/Scripts/app/Programacion/Programacion.module.js",
                 "~/Scripts/app/Programacion/Programacion.service.js",
                 "~/Scripts/app/Programacion/EditarGoce.controller.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/app/Programacion/ReporteCuadroVacacional/js").Include(
              "~/Scripts/app/Programacion/Programacion.module.js",
              "~/Scripts/app/Programacion/Programacion.service.js",
              "~/Scripts/app/Programacion/ReporteCuadroVacacional.Controller.js"
            ));
            #endregion

            #region Modulo Evaluación Solicitud

            bundles.Add(new ScriptBundle("~/bundles/app/SolicitudEvaluacion/Bandeja/js").Include(
                         "~/Scripts/app/Solicitud/Solicitud.module.js",
                         "~/Scripts/app/Solicitud/Solicitud.service.js",
                         "~/Scripts/app/Solicitud/BandejaEvaluacion.controller.js",
                         "~/Scripts/app/Solicitud/EvaluacionModificar.controller.js"
                       ));

            bundles.Add(new ScriptBundle("~/bundles/app/SolicitudEvaluacion/Modificar/js").Include(
                         "~/Scripts/app/Solicitud/Solicitud.module.js",
                         "~/Scripts/app/Solicitud/Solicitud.service.js",
                         "~/Scripts/app/Solicitud/EvaluacionModificar.controller.js"
                       ));
            #endregion
            

            #region Modulo TablaGeneral
            bundles.Add(new ScriptBundle("~/bundles/app/TablaGeneral/js").Include(
                     "~/Scripts/app/TablaGeneral/TablaGeneral.module.js",
                     "~/Scripts/app/TablaGeneral/TablaGeneral.service.js"
                   ));

            #endregion

            #region Modulo Trabajador
            bundles.Add(new ScriptBundle("~/bundles/app/Trabajador/js").Include(
                     "~/Scripts/app/Trabajador/Trabajador.module.js",
                     "~/Scripts/app/Trabajador/Trabajador.service.js"
                   ));

            #endregion


            #region Modulo Seguridad                
            bundles.Add(new ScriptBundle("~/bundles/app/seguridad/login/js").Include(
                        "~/Scripts/app/Seguridad/Login.module.js",
                        "~/Scripts/app/Seguridad/Login.controller.js",
                         "~/Scripts/app/Seguridad/Login.service.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/seguridad/cambioclave/js").Include(
                        "~/Scripts/app/Seguridad/Login.module.js",
                        "~/Scripts/app/Seguridad/CambioClave.controller.js",
                         "~/Scripts/app/Seguridad/Login.service.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/seguridad/olvidoclave/js").Include(
                        "~/Scripts/app/Seguridad/Login.module.js",
                        "~/Scripts/app/Seguridad/Olvidoclave.controller.js",
                         "~/Scripts/app/Seguridad/Login.service.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/app/seguridad/perfiles/js").Include(
                        "~/Scripts/app/Seguridad/Login.module.js",
                        "~/Scripts/app/Seguridad/Perfil.controller.js",
                         "~/Scripts/app/Seguridad/Login.service.js"
                        ));

            #endregion

            #region Unidad
            bundles.Add(new ScriptBundle("~/bundles/app/Unidad/js").Include(
                        "~/Scripts/app/Unidad/Unidad.module.js",
                        "~/Scripts/app/Unidad/Unidad.service.js"
                      ));
            #endregion

            BundleTable.EnableOptimizations = false;
        }
    }
}
