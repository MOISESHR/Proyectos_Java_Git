using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using TGS.SGV.Web.WsUsuarioServicio;
using TGS.SGV.Web.WsSolicitudServicio;
using TGS.SGV.Web.WsTablaGeneralServicio;
using TGS.SGV.Web.WsTrabajadorServicio;
using TGS.SGV.Web.WsEmpresaServicio;
using TGS.SGV.Web.WsParametroServicio;
using TGS.SGV.Web.WsUnidadServicio;
using TGS.SGV.Web.WsPerfilUsuarioServicio;
using TGS.SGV.Web.WsModuloSistemaServicio;
using TGS.SGV.Web.WsGoceVacacionalServicio;

namespace TGS.SGV.Web
{
    public static class UnityConfig
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IUsuarioServicio, UsuarioServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));
            
            container.RegisterType<ISolicitudServicio, SolicitudServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<ITablaGeneralServicio, TablaGeneralServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<ITrabajadorServicio, TrabajadorServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IEmpresaServicio, EmpresaServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IParametroServicio, ParametroServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IUnidadServicio, UnidadServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IPerfilUsuarioServicio, PerfilUsuarioServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IModuloSistemaServicio, ModuloSistemaServicioClient>
           (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            container.RegisterType<IGoceVacacionalServicio, GoceVacacionalServicioClient>
            (new HierarchicalLifetimeManager(), new InjectionConstructor("*"));

            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
           // No implemetado
        } 
    }
}