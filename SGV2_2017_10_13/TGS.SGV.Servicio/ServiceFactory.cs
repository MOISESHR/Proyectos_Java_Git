using Microsoft.Practices.Unity;
using Unity.Wcf;
using System.Data.Entity;
using TGS.SGV.AccesoDatos;
using TGS.SGV.AccesoDatos.Contrato;
using TGS.SGV.AccesoDatos.Implementacion;
using TGS.SGV.Negocio.Implementacion;
using TGS.SGV.Negocio.Contrato;
using TGS.SGV.Servicio.Implementacion;
using TGS.SGV.Servicio.Contrato;
using TGS.SGV.Agente.Implementacion;
using TGS.SGV.Agente.Contrato;

namespace TGS.SGV.Servicio
{
	public class ServiceFactory : UnityServiceHostFactory
    {
        protected override void ConfigureContainer(IUnityContainer container)
        {
            container
            .RegisterType<DbContext, SGVContext>(new PerThreadLifetimeManager())
            .RegisterType<IUsuarioAccesoDatos, UsuarioAccesoDatos>()
            .RegisterType<IUsuarioAgente, UsuarioAgente>()
            .RegisterType<IUsuarioNegocio, UsuarioNegocio>()
            .RegisterType<IUsuarioServicio, UsuarioServicio>()

            .RegisterType<ISolicitudAccesoDatos, SolicitudAccesoDatos>()
            .RegisterType<ISolicitudNegocio, SolicitudNegocio>()
            .RegisterType<ISolicitudServicio, SolicitudServicio>()
             
            .RegisterType<ITablaGeneralAccesoDatos, TablaGeneralAccesoDatos>()
            .RegisterType<ITablaGeneralNegocio, TablaGeneralNegocio>()
            .RegisterType<ITablaGeneralServicio, TablaGeneralServicio>()

            .RegisterType<ITrabajadorAccesoDatos, TrabajadorAccesoDatos>()
            .RegisterType<ITrabajadorNegocio, TrabajadorNegocio>()
            .RegisterType<ITrabajadorServicio, TrabajadorServicio>()

            .RegisterType<IEmpresaAccesoDatos, EmpresaAccesoDatos>()
            .RegisterType<IEmpresaNegocio, EmpresaNegocio>()
            .RegisterType<IEmpresaServicio, EmpresaServicio>()

            .RegisterType<IPerfilUsuarioAccesoDatos, PerfilUsuarioAccesoDatos>()
            .RegisterType<IPerfilUsuarioNegocio, PerfilUsuarioNegocio>()
            .RegisterType<IPerfilUsuarioServicio, PerfilUsuarioServicio>()

            .RegisterType<IUnidadAccesoDatos, UnidadAccesoDatos>()
            .RegisterType<IUnidadNegocio, UnidadNegocio>()
            .RegisterType<IUnidadServicio, UnidadServicio>()

            .RegisterType<IParametroAccesoDatos, ParametroAccesoDatos>()
            .RegisterType<IParametroNegocio, ParametroNegocio>()
            .RegisterType<IParametroServicio, ParametroServicio>()

            .RegisterType<IPlantillaAccesoDatos, PlantillaAccesoDatos>()
            .RegisterType<IPlantillaNegocio, PlantillaNegocio>()

            .RegisterType<IRolVacacionalAccesoDatos, RolVacacionalAccesoDatos>()
            .RegisterType<IRolVacacionalNegocio, RolVacacionalNegocio>()

            .RegisterType<IPeriodoAccesoDatos, PeriodoAccesoDatos>()
            .RegisterType<IPeriodoNegocio, PeriodoNegocio>()

            .RegisterType<IModuloSistemaAccesoDatos, ModuloSistemaAccesoDatos>()
            .RegisterType<IModuloSistemaNegocio, ModuloSistemaNegocio>()
            .RegisterType<IModuloSistemaServicio, ModuloSistemaServicio>()            

            .RegisterType<ICorreoAccesoDatos, CorreoAccesoDatos>()
            .RegisterType<ICorreoNegocio, CorreoNegocio>()

            .RegisterType<IGoceVacacionalAccesoDatos, GoceVacacionalAccesoDatos>()
            .RegisterType<IGoceVacacionalNegocio, GoceVacacionalNegocio>()
            .RegisterType<IGoceVacacionalServicio, GoceVacacionalServicio>();

        }
    }    
}