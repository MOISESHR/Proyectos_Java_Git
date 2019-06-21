using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGS.SGV.Modelo.Modelo;
using TGS.SGV.Modelo.Dto;
using TGS.SGV.Modelo.Base;

namespace TGS.SGV.Negocio.Contrato
{
   public interface IRolVacacionalNegocio : IDisposable
   {
        RolVacacionalResponse ObtenerFechaDerecho(RolVacacionalRequest parametro);
        RolVacacionalResponse ObtenerFechaDerechoRegulariza(string cipEmpleado);

        RolVacacionalResponse ObtenerDiasDisponiblesPagos(RolVacacionalRequest parametro);
        int ValidarModificarFechaPago(RolVacacionalRequest parametro);

        FechaDerechoResponse ListarRolesFechaDerecho(FechaDerechoRequest fechaDerechoRequest);

        FechaDerechoResponse ListarRolesFechaDerechoVacio(FechaDerechoRequest fechaDerechoRequest);

        FechaDerechoResponse ObtenerFechaDerecho(FechaDerechoRequest fechaDerechoRequest);

        SolicitudParametroResponse ValidarRolVacacional(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarRol(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarCruceGoceVacacional(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarCruceGoce(SolicitudParametroRequest solicitudParametroRequest);

        SolicitudParametroResponse ValidarRolActivoVacacional(SolicitudParametroRequest solicitudParametroRequest);

        Resultado<SolicitudParametroRequest> ValidarRolActivo(SolicitudParametroRequest solicitudParametroRequest);
    }    
}
